Imports System
Imports System.IO
Imports System.DirectoryServices
Imports System.Data
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Data.SqlClient
Imports System.Security.Principal

Public Class SiteMaster
    Inherits MasterPage


    Dim test_flag As Boolean = True
    Dim load_ct As Int16 = 0



    Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Dim _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As System.EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie As HttpCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If ((Not requestCookie Is Nothing) AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue)) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie As HttpCookie = New HttpCookie(AntiXsrfTokenKey) With {.HttpOnly = True, .Value = _antiXsrfTokenValue}
            If (FormsAuthentication.RequireSSL And Request.IsSecureConnection) Then
                responseCookie.Secure = True
            End If
            Response.Cookies.Set(responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Private Sub master_Page_PreLoad(sender As Object, e As System.EventArgs)
        Try
            If (Not IsPostBack) Then
                ' Set Anti-XSRF token
                ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
                ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, String.Empty)
            Else
                ' Validate the Anti-XSRF token
                If (Not DirectCast(ViewState(AntiXsrfTokenKey), String) = _antiXsrfTokenValue _
                    Or Not DirectCast(ViewState(AntiXsrfUserNameKey), String) = If(Context.User.Identity.Name, String.Empty)) Then
                    Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
                End If
            End If

            Dim Rgx As New Regex("%3frs.Command.*")
            'Debug.Print("$$>> " & Request.RawUrl.IndexOf("ReturnUrl"))
            'Debug.Print("$RAW: " & Request.RawUrl)
            'Debug.Print("ABS>> " & Request.Url.AbsoluteUri)
            'Debug.Print("ABSREG>> " & Rgx.Replace(Request.Url.AbsoluteUri, ""))
            'Debug.Print(Rgx.Replace(Request.Url.AbsoluteUri, "") & "?rs.Command=Render")

            If Request.RawUrl.IndexOf("ReturnUrl") > 0 Then
                Debug.Print(Request.RawUrl.IndexOf("&"))

                '' Click this link from the WF Delinquency Submission Report
                ''http: //localhost:63107/Tools/WellsFargo/DelinquencySubmission.aspx?DSN=1048&amp;FAC=Forsyth&amp;END=END?rs.Command=Render&amp;rs:Format=aspx

                '' If we get redirected to Default.aspx with a return of WF_DS The parameters will be doubled like:
                ''http: //financeweb.northside.local/Default.aspx?ReturnUrl=%2fTools%2fWellsFargo%2fDelinquencySubmission.aspx%3fDSN%3d1029%26FAC%3dAtlanta%26END%3dEND%3frs.Command%3dRender%26rs%3aFormat%3daspx&DSN=1029&FAC=Atlanta&END=END?rs.Command=Render&rs:Format=aspx
                '' Request.RawURL -->               /Default.aspx?ReturnUrl=%2fTools%2fWellsFargo%2fDelinquencySubmission.aspx%3fDSN%3d1048%26amp%3bFAC%3dForsyth%26amp%3bEND%3dEND%3frs.Command%3dRender%26amp%3brs%3aFormat%3daspx&DSN=1048&amp;FAC=Forsyth&amp;END=END?rs.Command=Render&amp;rs:Format=aspx

                '' Try stipping off after &


                If Request.RawUrl.IndexOf("Default.aspx?ReturnUrl=%2fTools%2fWellsFargo%2fDelinquencySubmission.aspx") > 0 And Request.RawUrl.IndexOf("&") > 0 Then
                    Response.Redirect(Rgx.Replace(Request.Url.AbsoluteUri, "") & "?rs.Command=Render")
                End If
                Debug.Print("STOP")
            End If





        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim search As DirectorySearcher
            Dim entry As DirectoryEntry
            Dim temp As String = ""

            If IsPostBack Then
                'If lblWelcome.Text <> "" Then
                '    LoginUser.Visible = False
                '    LoginStatus1.Visible = True
                'End If
            Else
                entry = New DirectoryEntry("LDAP://DC=northside, DC=local")
                search = New DirectorySearcher(entry)
                search.Filter = "(&(objectClass=user)(samaccountname=" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "))"
                'Dim i As Integer = search.Filter.Length

                If search.Filter.ToString = "(&(objectClass=user)(samaccountname=))" Then
                    Exit Sub
                End If
                For Each AdObj As SearchResult In search.FindAll()
                    temp = temp & AdObj.GetDirectoryEntry.Properties.Item("cn").Value
                Next

                lblWelcome.Text = temp
                'If lblWelcome.Text <> "" Then
                '    LoginUser.Visible = False
                '    LoginStatus1.Visible = True
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class