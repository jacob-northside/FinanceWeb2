<%@ Page Title="" Language="vb" AutoEventWireup="false"  MasterPageFile="~/Site.Master"  CodeBehind="ErrorTracking.aspx.vb" Inherits="FinanceWeb.ErrorTracking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

         <style type="text/css">
     .ScrollPanelMax
{
max-height: 400px;
}

.modalBackground2 
{
    background-color: #eee4ce !important;
    background-image: none !important;
    border: 1px solid #000000;
    max-height: 500px;
    font-size: medium;
    color: #003060;
    width: 300px;
    padding:5px;
    vertical-align:middle;
    text-align:center;
    
}
</style>
    

                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <script>

        function SelectAllCheckboxesSpecific(spanChk) {

            if ($get('<%=gvNewErrors.ClientID%>') != null) {
                var IsChecked = spanChk.checked;

                var Chk = spanChk;

                var items = $get('<%=gvNewErrors.ClientID%>').getElementsByTagName('input');

                for (i = 0; i < items.length; i++) {

                    if (items[i].id != Chk && items[i].type == "checkbox") {

                        if (items[i].checked != IsChecked) {

                            items[i].click();

                        }

                    }

                }

            }
        }

        
    </script>


      <cc1:TabContainer ID="tcErrorTracking" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement="True">
       <cc1:TabPanel runat="server" HeaderText="Error Tracking" ID="tpErrorTracking">
      <ContentTemplate> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:Label runat = "server" Text = "New Errors"></asp:Label> 
                
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>User</asp:TableCell>
                        <asp:TableCell><asp:DropDownList runat="server" ID="ddlUserID" AutoPostBack="true"></asp:DropDownList> </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Page</asp:TableCell>
                        <asp:TableCell><asp:DropDownList runat="server" ID="ddlPage" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Code Block</asp:TableCell>
                        <asp:TableCell><asp:DropDownList runat="server" ID="ddlCodeBlock" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                
                <asp:Panel CssClass = "ScrollPanelMax" runat = "server" ScrollBars = "Auto"> 
            <asp:GridView runat = "server" ID = "gvNewErrors" AutoGenerateColumns = "False"
             BorderColor="Black" BorderStyle="Solid" font-Size="Smaller"  HeaderStyle-BackColor="#6da9e3" 
               HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px"  AllowPaging="true" PageSize="50"
                           Width="80%" height="100%" 
          HeaderStyle-Wrap="true">
              <AlternatingRowStyle BackColor="#CBE3FB" />
          <columns>
          <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />

                          <asp:TemplateField HeaderText="Update Row">
                   <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

     </HeaderTemplate>

                <ItemTemplate>
                  <asp:CheckBox ID="chk" runat="server" Text="" />
                </ItemTemplate>
                </asp:TemplateField>

          <asp:TemplateField HeaderText = "User ID"> <ItemTemplate>
          <asp:Label ID = "lblgvNEUserId" runat = "server" Text = '<%# Eval("USERID") %>' ></asp:Label>
          </ItemTemplate></asp:TemplateField>

          <asp:TemplateField HeaderText = "IP Address"> <ItemTemplate>
          <asp:Label ID = "lblgvNEIP" runat = "server" Text = '<%# Eval("IPAddress") %>' ></asp:Label>
          </ItemTemplate></asp:TemplateField>

          <asp:TemplateField HeaderText = "Web Page"> <ItemTemplate>
          <asp:Label ID = "lblgvNEPage" runat = "server" Text = '<%# Eval("Page") %>' ></asp:Label>
          </ItemTemplate></asp:TemplateField>

          <asp:TemplateField HeaderText = "Code Block"> <ItemTemplate>
          <asp:Label ID = "lblgvNECodeBlock" runat = "server" Text = '<%# Eval("CodeBlock") %>' ></asp:Label>
          </ItemTemplate></asp:TemplateField>

                    <asp:TemplateField HeaderText = "Date Logged"> <ItemTemplate>
          <asp:Label ID = "lblgvNEDateLogged" runat = "server" Text = '<%# Eval("DateLogged") %>' ></asp:Label>
          </ItemTemplate></asp:TemplateField>

          </columns>
          </asp:GridView>
          </asp:Panel>
          <br />
          <br />
                      <asp:RadioButtonList ID="rblActiveStatus" runat="server" RepeatDirection="Horizontal" Font-Size ="X-Small"  >
                    <asp:ListItem  Text ="Active" Value ="1"></asp:ListItem>
                    <asp:ListItem  Text ="Inactive" Value ="0"></asp:ListItem>
                    </asp:RadioButtonList><br />
                    <asp:Label ID = "lblExceptionCode" runat = "server"></asp:Label>
                    <br />
                    <asp:Textbox ID = "txtUpdComment" runat = "server" width = "700px" height = "100px"></asp:Textbox>
                    <br /><br />
                    <asp:Button ID = "btnUpdateError" runat = "server" Text = "Update" />
                    <asp:Button ID = "btnDeleteError" runat = "server" Text = "Delete" />
            </ContentTemplate>
            </asp:UpdatePanel>

        
</ContentTemplate>
      </cc1:TabPanel>
      </cc1:TabContainer>
      </asp:Content>