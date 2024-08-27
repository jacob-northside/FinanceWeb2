Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports FinanceWeb.WebFinGlobal



Public Class AddressGeoCode_DataEntry
    Inherits System.Web.UI.Page
    Dim SQL As String = ""
    Dim SQL_Clause As String = ""
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then
            Else
                If User.Identity.IsAuthenticated = True Then
                    AddressDataEntry.Visible = True
                    LoadLookUpGrid()
                Else
                End If
            End If

        Catch ex As Exception
            LogWebFinError("fist" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub LoadLookUpGrid()
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter
            Dim SelectedTable As String = ""
            SQL_Clause = ""

            SQL = "Select * from DWH.UD.ADDRESSGEOCODE"

            Debug.Print(SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?

                Update_Addresses.DataSource = ds
                Update_Addresses.DataMember = "LookUpData"
                Update_Addresses.DataBind()
            End Using

        Catch ex As Exception
            LogWebFinError("fist" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_Addresses_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_Addresses.RowEditing
        Try
            Update_Addresses.EditIndex = e.NewEditIndex
            Update_Addresses.DataSource = DirectCast(ViewState("TBL"), DataTable)
            Update_Addresses.DataBind()

            LoadLookUpGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_Addresses_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_Addresses.RowCancelingEdit
        Try
            Update_Addresses.EditIndex = -1
            Update_Addresses.DataSource = DirectCast(ViewState("TBL"), DataTable)
            Update_Addresses.DataBind()
            LoadLookUpGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub Update_Addresses_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_Addresses.RowUpdating
        Try
            Dim UID As HiddenField = Update_Addresses.Rows(e.RowIndex).FindControl("UID")
            Dim MATCH_STATUS As DropDownList = DirectCast(Update_Addresses.Rows(e.RowIndex).FindControl("ddlMatch_Status"), DropDownList)
            Dim MATCH_SUBSTATUS As DropDownList = DirectCast(Update_Addresses.Rows(e.RowIndex).FindControl("ddlMatch_Substatus"), DropDownList)
            Dim LATITUDE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("LATITUDE_EditBox")
            Dim LONGITUDE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("LONGITUDE_EditBox")
            Dim MATCHED_STREET_ADDRESS As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("MATCHED_STREET_ADDRESS_EditBox")
            Dim MATCHED_CITY As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("MATCHED_CITY_EditBox")
            Dim MATCHED_STATE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("MATCHED_STATE_EditBox")
            Dim MATCHED_ZIP As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("MATCHED_ZIP_EditBox")
            Dim TIGER_LINEID As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("TIGER_LINEID_EditBox")
            Dim STREET_SIDE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("STREET_SIDE_EditBox")
            Dim STATE_FIPS_CODE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("STATE_FIPS_CODE_EditBox")
            Dim COUNTY_FIPS_CODE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("COUNTY_FIPS_CODE_EditBox")
            Dim CENSUS_TRACT_CODE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("CENSUS_TRACT_CODE_EditBox")
            Dim CENSUS_BLOCK_CODE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("CENSUS_BLOCK_CODE_EditBox")
            Dim MATCH_DATE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("MATCH_DATE_EditBox")
            Dim VINTAGE As TextBox = Update_Addresses.Rows(e.RowIndex).FindControl("VINTAGE_EditBox")

            Dim UpdateString As String
            UpdateString = "Update DWH.UD.ADDRESSGEOCODE Set MATCH_STATUS = '" & MATCH_STATUS.Text & "'," & _
            " MATCH_SUBSTATUS = '" & MATCH_SUBSTATUS.Text & "'," & _
            " LATITUDE = '" & LATITUDE.Text & "'," & _
            " LONGITUDE = '" & LONGITUDE.Text & "'," & _
            " MATCHED_STREET_ADDRESS = '" & MATCHED_STREET_ADDRESS.Text & "'," & _
            " MATCHED_CITY = '" & MATCHED_CITY.Text & "'," & _
            " MATCHED_STATE = '" & MATCHED_STATE.Text & "'," & _
            " MATCHED_ZIP = '" & MATCHED_ZIP.Text & "'," & _
            " TIGER_LINEID = '" & TIGER_LINEID.Text & "'," & _
            " STREET_SIDE = '" & STREET_SIDE.Text & "'," & _
            " STATE_FIPS_CODE = '" & STATE_FIPS_CODE.Text & "'," & _
            " COUNTY_FIPS_CODE = '" & COUNTY_FIPS_CODE.Text & "'," & _
            " CENSUS_TRACT_CODE = '" & CENSUS_TRACT_CODE.Text & "'," & _
            " CENSUS_BLOCK_CODE = '" & CENSUS_BLOCK_CODE.Text & "'," & _
            " MATCH_DATE = '" & MATCH_DATE.Text & "'," & _
            " VINTAGE = '" & VINTAGE.Text & "'" & _
            " where UID = '" & UID.Value & "'"
            Debug.Print(UpdateString)

            ' Take out below and sub in LoadGridView();
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)
                UpdateCommand.ExecuteNonQuery()
            End Using
            Update_Addresses.EditIndex = -1
            Update_Addresses.DataBind()
            LoadLookUpGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub Search_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearchAddress.TextChanged, txtSearchCity.TextChanged, txtSearchState.TextChanged, txtSearchZIP.TextChanged, txtSearchMatchStatus.TextChanged, txtSearchMatchSubStatus.TextChanged
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim search_addresses_sql As String = ""

        Try
            Dim filter As String = ""

            search_addresses_sql = "Select * from DWH.UD.ADDRESSGEOCODE WHERE STREET_ADDRESS like '%" & txtSearchAddress.Text & "%'" & _
                " and CITY like '%" & txtSearchCity.Text & "%'" & _
                " and STATE like '%" & txtSearchState.Text & "%'" & _
                " and ZIP like '%" & txtSearchZIP.Text & "%'" & _
                " and MATCH_STATUS like '%" & txtSearchMatchStatus.Text & "%'" & _
                " and MATCH_SUBSTATUS like '%" & txtSearchMatchSubStatus.Text & "%'"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(search_addresses_sql, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                Debug.Print(search_addresses_sql)
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?

                Update_Addresses.DataSource = ds
                Update_Addresses.DataMember = "LookUpData"
                Update_Addresses.DataBind()
            End Using
            conn.Close()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_Addresses_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso Update_Addresses.EditIndex = e.Row.RowIndex Then

            Dim Match_Substatus As Label = DirectCast(e.Row.FindControl("Match_Substatus_Cell"), Label)
            Dim ddlMatch_Substatus As DropDownList = DirectCast(e.Row.FindControl("ddlMatch_Substatus"), DropDownList)
            ddlMatch_Substatus.Items.FindByValue(Match_Substatus.Text).Selected = True

            Dim Match_Status As Label = DirectCast(e.Row.FindControl("Match_Status_Cell"), Label)
            Dim ddlMatch_Status As DropDownList = DirectCast(e.Row.FindControl("ddlMatch_Status"), DropDownList)

            Debug.Print(Match_Status.Text)

            ddlMatch_Status.Items.FindByValue(Match_Status.Text).Selected = True


        End If
    End Sub

End Class