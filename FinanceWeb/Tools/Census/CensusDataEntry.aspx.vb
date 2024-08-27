Imports FinanceWeb.WebFinGlobal
Imports System.Data.SqlClient

Public Class CensusDataEntry
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FetchMissingDataReport()
            If IsPostBack Then

            Else
                If User.Identity.IsAuthenticated = True Then

                Else

                End If

            End If

        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub Calendar_SelectionChange(sender As Object, e As EventArgs)

        Try
            Dim SQL As String = "Select * from DWH.UD.CENSUS_DATA where Calendar_Date = @CDATE;"
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim RetrieveLocations As SqlCommand = New SqlCommand(SQL, conn)
                RetrieveLocations.Parameters.Add("@CDATE", SqlDbType.Date).Value = CalendarDate_Selection.SelectedDate.ToShortDateString()

                Dim myReader As SqlDataReader
                myReader = RetrieveLocations.ExecuteReader()

                A_Total_ADC.Text = ""
                A_Census.Text = ""
                A_LD.Text = ""
                A_IP_ADC.Text = ""
                A_OBV.Text = ""
                C_Total_ADC.Text = ""
                C_Census.Text = ""
                C_LD.Text = ""
                C_IP_ADC.Text = ""
                C_OBV.Text = ""
                F_Total_ADC.Text = ""
                F_Census.Text = ""
                F_LD.Text = ""
                F_IP_ADC.Text = ""
                F_OBV.Text = ""

                D_Total_ADC.Text = ""
                D_Census.Text = ""
                D_LD.Text = ""
                D_IP_ADC.Text = ""
                D_OBV.Text = ""
                L_Total_ADC.Text = ""
                L_Census.Text = ""
                L_LD.Text = ""
                L_IP_ADC.Text = ""
                L_OBV.Text = ""

                A_OB.Text = ""
                C_OB.Text = ""
                F_OB.Text = ""
                D_OB.Text = ""
                L_OB.Text = ""

                A_DELS.Text = ""
                C_DELS.Text = ""
                F_DELS.Text = ""
                D_DELS.Text = ""
                L_DELS.Text = ""

                A_NB.Text = ""
                C_NB.Text = ""
                F_NB.Text = ""
                D_NB.Text = ""
                L_NB.Text = ""

                'Iterate through the results
                While myReader.Read()
                    If myReader("FACILITY") = "A" Then
                        A_Total_ADC.Text = myReader("TOTAL_ADC")
                        A_Census.Text = myReader("CENSUS")
                        A_LD.Text = myReader("LABOR_DELIVERY")
                        A_IP_ADC.Text = myReader("IP_ADC")
                        A_OBV.Text = myReader("OBV")
                        A_OB.Text = SafeString(myReader, "OB")
                        A_DELS.Text = SafeString(myReader, "DELS")
                        A_NB.Text = SafeString(myReader, "NB")
                    ElseIf myReader("FACILITY") = "C" Then
                        C_Total_ADC.Text = myReader("TOTAL_ADC")
                        C_Census.Text = myReader("CENSUS")
                        C_LD.Text = myReader("LABOR_DELIVERY")
                        C_IP_ADC.Text = myReader("IP_ADC")
                        C_OBV.Text = myReader("OBV")
                        C_OB.Text = SafeString(myReader, "OB")
                        C_DELS.Text = SafeString(myReader, "DELS")
                        C_NB.Text = SafeString(myReader, "NB")
                    ElseIf myReader("FACILITY") = "F" Then
                        F_Total_ADC.Text = myReader("TOTAL_ADC")
                        F_Census.Text = myReader("CENSUS")
                        F_LD.Text = myReader("LABOR_DELIVERY")
                        F_IP_ADC.Text = myReader("IP_ADC")
                        F_OBV.Text = myReader("OBV")
                        F_OB.Text = SafeString(myReader, "OB")
                        F_DELS.Text = SafeString(myReader, "DELS")
                        F_NB.Text = SafeString(myReader, "NB")
                    ElseIf myReader("FACILITY") = "D" Then
                        D_Total_ADC.Text = myReader("TOTAL_ADC")
                        D_Census.Text = myReader("CENSUS")
                        D_LD.Text = myReader("LABOR_DELIVERY")
                        D_IP_ADC.Text = myReader("IP_ADC")
                        D_OBV.Text = myReader("OBV")
                        D_OB.Text = SafeString(myReader, "OB")
                        D_DELS.Text = SafeString(myReader, "DELS")
                        D_NB.Text = SafeString(myReader, "NB")
                    ElseIf myReader("FACILITY") = "L" Then
                        L_Total_ADC.Text = myReader("TOTAL_ADC")
                        L_Census.Text = myReader("CENSUS")
                        L_LD.Text = myReader("LABOR_DELIVERY")
                        L_IP_ADC.Text = myReader("IP_ADC")
                        L_OBV.Text = myReader("OBV")
                        L_OB.Text = SafeString(myReader, "OB")
                        L_DELS.Text = SafeString(myReader, "DELS")
                        L_NB.Text = SafeString(myReader, "NB")
                    End If
                End While
            End Using

            A_CalendarDate.Text = CalendarDate_Selection.SelectedDate.ToShortDateString()
            C_CalendarDate.Text = CalendarDate_Selection.SelectedDate.ToShortDateString()
            F_CalendarDate.Text = CalendarDate_Selection.SelectedDate.ToShortDateString()
            D_CalendarDate.Text = CalendarDate_Selection.SelectedDate.ToShortDateString()
            L_CalendarDate.Text = CalendarDate_Selection.SelectedDate.ToShortDateString()


        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Public Function SafeString(book As SqlDataReader, colname As String)

        Dim x As Integer = book.GetOrdinal(colname)

        If book.IsDBNull(x) Then
            Return ""
        Else
            Return book.GetValue(x).ToString
        End If

    End Function
    'Protected Sub ShowCalendar_Btn_Click(sender As Object, e As System.EventArgs) Handles ShowCalendar_Btn.Click
    '    Try

    '        If Cherokee_Calendar.Visible = False Then
    '            Cherokee_Calendar.Visible = True
    '        Else
    '            Cherokee_Calendar.Visible = False
    '        End If

    '    Catch ex As Exception
    '        Debug.Print(ex.ToString)
    '    End Try
    'End Sub

    Protected Sub CensusData_Submit_Click(sender As Object, e As System.EventArgs) Handles CensusData_Submit.Click
        Try
            Dim MergeString As String = "Merge DWH.UD.CENSUS_DATA AS Target Using (VALUES "

            Dim A_String As String = "('A', @A_Total_ADC, @A_Census, @A_Labor_Delivery, @A_IP_ADC, @A_OBV, @A_Calendar_Date, @A_OB, @A_DELS, @A_NB)"
            Dim C_String As String = "('C', @C_Total_ADC, @C_Census, @C_Labor_Delivery, @C_IP_ADC, @C_OBV, @C_Calendar_Date, @C_OB, @C_DELS, @C_NB)"
            Dim F_String As String = "('F', @F_Total_ADC, @F_Census, @F_Labor_Delivery, @F_IP_ADC, @F_OBV, @F_Calendar_Date, @F_OB, @F_DELS, @F_NB)"
            Dim D_String As String = "('D', @D_Total_ADC, @D_Census, @D_Labor_Delivery, @D_IP_ADC, @D_OBV, @D_Calendar_Date, @D_OB, @D_DELS, @D_NB)"
            Dim L_String As String = "('L', @L_Total_ADC, @L_Census, @L_Labor_Delivery, @L_IP_ADC, @L_OBV, @L_Calendar_Date, @L_OB, @L_DELS, @L_NB)"

            Dim MergeStringEND As String = " ) as Source (FACILITY, TOTAL_ADC, CENSUS, LABOR_DELIVERY, IP_ADC, OBV, CALENDAR_DATE, OB, DELS, NB) " &
                                           " on Source.FACILITY = Target.FACILITY and Source.CALENDAR_DATE = Target.CALENDAR_DATE " &
                                           " WHEN MATCHED THEN " &
                                           " UPDATE SET Target.TOTAL_ADC = Source.TOTAL_ADC, Target.CENSUS = Source.CENSUS, " &
                                           " Target.LABOR_DELIVERY = Source.LABOR_DELIVERY, Target.IP_ADC = Source.IP_ADC, " &
                                           " Target.OBV = Source.OBV, Target.OB = Source.OB, Target.DELS = Source.Dels, Target.NB = Source.NB " &
                                           " WHEN NOT MATCHED THEN " &
                                           " INSERT(FACILITY, TOTAL_ADC, CENSUS, LABOR_DELIVERY, IP_ADC, OBV, CALENDAR_DATE, OB, DELS, NB) " &
                                           " VALUES (Source.FACILITY, Source.TOTAL_ADC, Source.CENSUS, Source.LABOR_DELIVERY, Source.IP_ADC, Source.OBV, Source.CALENDAR_DATE, Source.OB, Source.DELS, Source.NB);"

            Dim Chk_Ct As Int16 = 0

            If A_Checkbox.Checked Then
                MergeString += A_String
                Chk_Ct += 1
            End If

            If C_Checkbox.Checked Then
                If Chk_Ct > 0 Then
                    MergeString += ", " + C_String
                Else
                    MergeString += C_String
                    Chk_Ct += 1
                End If
            End If

            If F_Checkbox.Checked Then
                If Chk_Ct > 0 Then
                    MergeString += ", " + F_String
                Else
                    MergeString += F_String
                    Chk_Ct += 1
                End If
            End If

            If D_Checkbox.Checked Then
                If Chk_Ct > 0 Then
                    MergeString += ", " + D_String
                Else
                    MergeString += D_String
                    Chk_Ct += 1
                End If
            End If

            If L_Checkbox.Checked Then
                If Chk_Ct > 0 Then
                    MergeString += ", " + L_String
                Else
                    MergeString += L_String
                    Chk_Ct += 1
                End If
            End If

            MergeString += MergeStringEND
            Debug.Print(MergeString)

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim InsertCommand As SqlCommand = New SqlCommand(MergeString, conn)
                If A_Checkbox.Checked Then
                    InsertCommand.Parameters.Add("@A_Total_ADC", SqlDbType.Int).Value = A_Total_ADC.Text
                    InsertCommand.Parameters.Add("@A_Census", SqlDbType.Int).Value = A_Census.Text
                    InsertCommand.Parameters.Add("@A_Labor_Delivery", SqlDbType.Int).Value = A_LD.Text
                    InsertCommand.Parameters.Add("@A_IP_ADC", SqlDbType.Int).Value = A_IP_ADC.Text
                    InsertCommand.Parameters.Add("@A_OBV", SqlDbType.Int).Value = A_OBV.Text
                    InsertCommand.Parameters.Add("@A_Calendar_Date", SqlDbType.Date).Value = A_CalendarDate.Text
                    InsertCommand.Parameters.Add("@A_OB", SqlDbType.Int).Value = A_OB.Text
                    InsertCommand.Parameters.Add("@A_DELS", SqlDbType.Int).Value = A_DELS.Text
                    InsertCommand.Parameters.Add("@A_NB", SqlDbType.Int).Value = A_NB.Text
                End If
                If C_Checkbox.Checked Then
                    InsertCommand.Parameters.Add("@C_Total_ADC", SqlDbType.Int).Value = C_Total_ADC.Text
                    InsertCommand.Parameters.Add("@C_Census", SqlDbType.Int).Value = C_Census.Text
                    InsertCommand.Parameters.Add("@C_Labor_Delivery", SqlDbType.Int).Value = C_LD.Text
                    InsertCommand.Parameters.Add("@C_IP_ADC", SqlDbType.Int).Value = C_IP_ADC.Text
                    InsertCommand.Parameters.Add("@C_OBV", SqlDbType.Int).Value = C_OBV.Text
                    InsertCommand.Parameters.Add("@C_Calendar_Date", SqlDbType.Date).Value = C_CalendarDate.Text
                    InsertCommand.Parameters.Add("@C_OB", SqlDbType.Int).Value = C_OB.Text
                    InsertCommand.Parameters.Add("@C_DELS", SqlDbType.Int).Value = C_DELS.Text
                    InsertCommand.Parameters.Add("@C_NB", SqlDbType.Int).Value = C_NB.Text
                End If
                If F_Checkbox.Checked Then
                    InsertCommand.Parameters.Add("@F_Total_ADC", SqlDbType.Int).Value = F_Total_ADC.Text
                    InsertCommand.Parameters.Add("@F_Census", SqlDbType.Int).Value = F_Census.Text
                    InsertCommand.Parameters.Add("@F_Labor_Delivery", SqlDbType.Int).Value = F_LD.Text
                    InsertCommand.Parameters.Add("@F_IP_ADC", SqlDbType.Int).Value = F_IP_ADC.Text
                    InsertCommand.Parameters.Add("@F_OBV", SqlDbType.Int).Value = F_OBV.Text
                    InsertCommand.Parameters.Add("@F_Calendar_Date", SqlDbType.Date).Value = F_CalendarDate.Text
                    InsertCommand.Parameters.Add("@F_OB", SqlDbType.Int).Value = F_OB.Text
                    InsertCommand.Parameters.Add("@F_DELS", SqlDbType.Int).Value = F_DELS.Text
                    InsertCommand.Parameters.Add("@F_NB", SqlDbType.Int).Value = F_NB.Text
                End If
                If D_Checkbox.Checked Then
                    InsertCommand.Parameters.Add("@D_Total_ADC", SqlDbType.Int).Value = D_Total_ADC.Text
                    InsertCommand.Parameters.Add("@D_Census", SqlDbType.Int).Value = D_Census.Text
                    InsertCommand.Parameters.Add("@D_Labor_Delivery", SqlDbType.Int).Value = D_LD.Text
                    InsertCommand.Parameters.Add("@D_IP_ADC", SqlDbType.Int).Value = D_IP_ADC.Text
                    InsertCommand.Parameters.Add("@D_OBV", SqlDbType.Int).Value = D_OBV.Text
                    InsertCommand.Parameters.Add("@D_Calendar_Date", SqlDbType.Date).Value = D_CalendarDate.Text
                    InsertCommand.Parameters.Add("@D_OB", SqlDbType.Int).Value = D_OB.Text
                    InsertCommand.Parameters.Add("@D_DELS", SqlDbType.Int).Value = D_DELS.Text
                    InsertCommand.Parameters.Add("@D_NB", SqlDbType.Int).Value = D_NB.Text
                End If
                If L_Checkbox.Checked Then
                    InsertCommand.Parameters.Add("@L_Total_ADC", SqlDbType.Int).Value = L_Total_ADC.Text
                    InsertCommand.Parameters.Add("@L_Census", SqlDbType.Int).Value = L_Census.Text
                    InsertCommand.Parameters.Add("@L_Labor_Delivery", SqlDbType.Int).Value = L_LD.Text
                    InsertCommand.Parameters.Add("@L_IP_ADC", SqlDbType.Int).Value = L_IP_ADC.Text
                    InsertCommand.Parameters.Add("@L_OBV", SqlDbType.Int).Value = L_OBV.Text
                    InsertCommand.Parameters.Add("@L_Calendar_Date", SqlDbType.Date).Value = L_CalendarDate.Text
                    InsertCommand.Parameters.Add("@L_OB", SqlDbType.Int).Value = L_OB.Text
                    InsertCommand.Parameters.Add("@L_DELS", SqlDbType.Int).Value = L_DELS.Text
                    InsertCommand.Parameters.Add("@L_NB", SqlDbType.Int).Value = L_NB.Text
                End If

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                InsertCommand.ExecuteNonQuery()
                conn.Close()
            End Using

            A_Total_ADC.Text = ""
            A_Census.Text = ""
            A_LD.Text = ""
            A_IP_ADC.Text = ""
            A_OBV.Text = ""
            A_CalendarDate.Text = ""
            A_OB.Text = ""
            A_DELS.Text = ""
            A_NB.Text = ""

            C_Total_ADC.Text = ""
            C_Census.Text = ""
            C_LD.Text = ""
            C_IP_ADC.Text = ""
            C_OBV.Text = ""
            C_CalendarDate.Text = ""
            C_OB.Text = ""
            C_DELS.Text = ""
            C_NB.Text = ""

            F_Total_ADC.Text = ""
            F_Census.Text = ""
            F_LD.Text = ""
            F_IP_ADC.Text = ""
            F_OBV.Text = ""
            F_CalendarDate.Text = ""
            F_OB.Text = ""
            F_DELS.Text = ""
            F_NB.Text = ""

            D_Total_ADC.Text = ""
            D_Census.Text = ""
            D_LD.Text = ""
            D_IP_ADC.Text = ""
            D_OBV.Text = ""
            D_CalendarDate.Text = ""
            D_OB.Text = ""
            D_DELS.Text = ""
            D_NB.Text = ""

            L_Total_ADC.Text = ""
            L_Census.Text = ""
            L_LD.Text = ""
            L_IP_ADC.Text = ""
            L_OBV.Text = ""
            L_CalendarDate.Text = ""
            L_OB.Text = ""
            L_DELS.Text = ""
            L_NB.Text = ""

            Messages_Div.InnerText = "Rows inserted successfully!"


        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Messages_Div.InnerText = "Row Merge Error!"
        End Try
    End Sub
    Protected Sub FetchMissingDataReport()
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter

            Dim SQL As String = "DWH.UD.CensusData_MissingRecords"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim SQLCommand As SqlCommand = New SqlCommand(SQL, conn)
                SQLCommand.CommandType = CommandType.StoredProcedure
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.SelectCommand = SQLCommand
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                conn.Close()
            End Using


            MissingDataReport.DataSource = ds
            MissingDataReport.DataMember = "LookUpData"
            MissingDataReport.DataBind()




        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



End Class