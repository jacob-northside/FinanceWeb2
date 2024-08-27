Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class ActiveDirectoryTool
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Sub ADQuery()

    End Sub


    Protected Sub btnFindUserInfo_Click(sender As Object, e As EventArgs) Handles btnFindUserInfo.Click
        Try
            lblUserInfo.Text = ""

            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult
            Dim GrpCnt As Integer = 0
            Dim i As Integer = 0

            If rblSearchType.Items.Item(0).Selected = True Then  'Email Address
                osearcher.Filter = "(&(mail=" & txtSearchInfo.Text & "*))" ' search filter
            End If
            If rblSearchType.Items.Item(1).Selected = True Then  'UserID 
                osearcher.Filter = "(&(samaccountname=" & txtSearchInfo.Text & "*))" ' search filter
            End If
            If rblSearchType.Items.Item(2).Selected = True Then   'Last Name 
                osearcher.Filter = "(&(cn=*" & txtSearchInfo.Text & "))" ' search filter
            End If
            If rblSearchType.Items.Item(3).Selected = True Then  'Full Name 
                osearcher.Filter = "(&(cn=*" & txtSearchInfo.Text & "*))" ' search filter
            End If

            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()

            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    lblUserInfo.Text = lblUserInfo.Text & "cn: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & "<br/>" & _
                    "name: " & result.GetDirectoryEntry.Properties("name").Value & "<br/>" & _
                    "givenname: " & result.GetDirectoryEntry.Properties("givenname").Value & "<br/>" & _
                    "samaccountname: " & result.GetDirectoryEntry.Properties("samaccountname").Value & "<br/>" & _
                    "sn: " & result.GetDirectoryEntry.Properties("sn").Value & "<br/>" & _
                    "mail: " & result.GetDirectoryEntry.Properties("mail").Value & "<br/>" & _
                    "initials: " & result.GetDirectoryEntry.Properties("initials").Value & "<br/>" & _
                    "ou: " & result.GetDirectoryEntry.Properties("ou").Value & vbCrLf & _
                    "userPrincipalName: " & result.GetDirectoryEntry.Properties("userPrincipalName").Value & "<br/>" & _
                    "distinguishedName: " & result.GetDirectoryEntry.Properties("distinguishedName").Value & "<br/>" & _
                    "member: " & result.GetDirectoryEntry.Properties("member").Value & "<br/>  <br/> "
                    ' "member of:  " & result.GetDirectoryEntry.Properties("memberOf").Value & "<br/>  <br/>"

                    GrpCnt = result.Properties("memberOf").Count() - 1 '
                    While GrpCnt >= 0
                        lblUserInfo.Text = lblUserInfo.Text & "<br/>" & result.Properties("MemberOf").Item(GrpCnt)
                        GrpCnt -= 1

                    End While

                End If
            Next

            Exit Sub
 
             
            ''Creates a Directory Entry Instance with the Username and Password provided
            ''Dim deSystem As New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local", User, Pass)
            'Dim deSystem As New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")

            '' txtSearchInfo.Text

            ''Authenticacion type Secure
            'deSystem.AuthenticationType = AuthenticationTypes.Secure

            ''Creates a Directory Searcher Instance
            'Dim dsSystem As New DirectorySearcher(deSystem)

            ''sAMAccountName is equal to our username passed in.
            'dsSystem.Filter = "sAMAccountName=" & txtSearchInfo.Text

            ''Load the MemberOf Property
            'dsSystem.PropertiesToLoad.Add("MemberOf")

            ''Find the user data
            'Dim srSystem As SearchResult = dsSystem.FindOne()

            ''Above we get the number of groups the user is a memberOf, and store it in a variable. It is zero indexed, so we remove 1 so we can loop through it.
            'Dim NumberOfGroups As Integer = srSystem.Properties("memberOf").Count() - 1
            'Dim _Groups As String

            ''A temp string that we will use to get only what we need from the MemberOf string property
            'Dim tempString As String = String.Empty

            'While (NumberOfGroups >= 0)
            '    'Above we set tempString to the first index of "," starting from the zero to the element of itself.
            '    tempString = srSystem.Properties("MemberOf").Item(NumberOfGroups)
            '    tempString = tempString.Substring(0, tempString.IndexOf(",", 0))

            '    'Above, we remove the "CN=" from the beginning of the string
            '    tempString = tempString.Replace("CN=", "")

            '    'Finally, we trim any blank characters from the edges
            '    tempString = tempString.ToLower() 'Lets make all letters lowercase
            '    tempString = tempString.Replace(" ", "")

            '    'Obtains the total amount of groups sent by the User
            '    Dim GroupstoValidate As Integer = _Groups.Count - 1

            '    'It goes through the list of Active Directory groups in order to validate in which group the user is
            '    While (GroupstoValidate >= 0)

            '        'Confirms if the Groups is the same from the list provided by the System
            '        If tempString = _Groups(GroupstoValidate) Then
            '            ' Return tempString
            '            tempString = _Groups(GroupstoValidate).ToString
            '        End If

            '        'If no match is found, continue to the next group
            '        GroupstoValidate -= 1
            '    End While

            '    'If no match is found, continue to the next group
            '    NumberOfGroups -= 1
            'End While

            ''If the code reaches here, there was no match found
            ''   Return String.Empty

            'lblUserInfo.Text = lblUserInfo.Text & tempString

        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


End Class