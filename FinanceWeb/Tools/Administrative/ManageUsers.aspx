<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManageUsers.aspx.vb" Inherits="FinanceWeb.ManageUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >

     <style type="text/css">
     .shrinkselect
{
max-height: 200px;
}
</style>



 <%--   <asp:DropDownList ID="ddlAllUsers" runat="server" Height="16px" Width="368px">
        
    </asp:DropDownList>
 
    <asp:CheckBoxList ID="cblAllUsers" runat="server">
    </asp:CheckBoxList>
--%>
     
   
    <cc1:TabContainer ID="tcManageUsers" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement="True">
        <cc1:TabPanel runat="server" HeaderText="Modify Roles" ID="tbRoles">
        <ContentTemplate>
    <asp:Label ID="Label1" runat="server" Text="User ID:    "></asp:Label> 
    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>&nbsp; &nbsp;    
    <asp:Button ID="btnCheckUser" runat="server" Text="Check User" /><br />
    <asp:GridView ID="gvUserRoles" runat="server"  
      CellPadding="4" BorderColor="Black"    BackColor="White" 
          HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" AllowSorting ="True"  
         BorderStyle="Solid"   HeaderStyle-BackColor="#214B9A"  
        Font-Size="X-Small"   >
        
           <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
        
                    <AlternatingRowStyle BackColor="#FFE885" />



    		        <Columns>
                        <asp:CommandField DeleteText="Remove Role" ShowDeleteButton="True" />
                    </Columns>
   <EditRowStyle BackColor="#2461BF" />
      
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />



    		  <HeaderStyle HorizontalAlign="Left" />
        <PagerSettings Position="TopAndBottom" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
     <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
<br /> 
    <asp:Label ID="Label2" runat="server" Text="All Roles:  "></asp:Label>
    <asp:DropDownList ID="ddlAllRoles" runat="server">
    </asp:DropDownList>  &nbsp; 
<asp:TextBox ID="txtRole" runat="server" Visible="False"></asp:TextBox>
&nbsp; 
    <asp:Button ID="btnAddNewRole" runat="server" Text="Add Role to User" 
    CausesValidation="False" /><br /><br />


        </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel runat="server" HeaderText="Find user Info." ID="tpUserInfo">
         <ContentTemplate>
    <asp:TextBox ID="txtSearchInfo" runat="server"></asp:TextBox> &nbsp; &nbsp; 
    <asp:RadioButtonList ID="rblSearchType" runat="server" 
        RepeatDirection="Horizontal" Font-Size="X-Small">
        <asp:ListItem>Email </asp:ListItem>
        <asp:ListItem Selected="True">User ID</asp:ListItem>
        <asp:ListItem>Last Name</asp:ListItem>
        <asp:ListItem>Full Name</asp:ListItem>
    </asp:RadioButtonList>
    <br />

    <asp:Button ID="btnFindUserInfo" runat="server" Text="Find User Info" /><br /> 
 
    
    <asp:Label ID="lblUserInfo" runat="server" Text=""></asp:Label>
        
        </ContentTemplate>
        </cc1:TabPanel>

                <cc1:TabPanel runat="server" HeaderText="Email Distribution" ID="tpEmailDistAdmin">
        <ContentTemplate>
        <asp:Table ID = "tblBasicSearch" runat = "server"> 
        <asp:TableRow>
        <asp:TableCell ColumnSpan = "4" horizontalalign = "center">
        <asp:Label runat = "server" Text = "Search DWH.dbo.Email_Distribution :" ></asp:Label>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell>
        <asp:Label runat = "server" Text = "Name: "></asp:Label></asp:TableCell><asp:TableCell> <asp:Textbox runat = "server" ID = "txtEDSearchName"></asp:Textbox>
        </asp:TableCell><asp:TableCell columnspan = "2" rowspan = "2" horizontalalign = "center">
        <asp:Button runat = "server" Text = "Search" font-size = "large" ID = "btnEDSearchSimple" height = "50px" width = "100px" />
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell> <asp:Label runat = "server" Text = "Userlogin: "></asp:Label></asp:TableCell><asp:TableCell> <asp:Textbox runat = "server" ID = "txtEDSearchlogin"></asp:Textbox></asp:TableCell>
        <asp:TableCell></asp:TableCell></asp:TableRow>
        <asp:TableRow>
        <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell width = "50px" ></asp:TableCell><asp:TableCell horizontalalign = "right" width = "100px">
        <asp:LinkButton runat = "server" ID = "lbSearchAdv" Text = "Advanced Search" Font-Size = "smaller"></asp:LinkButton>
        </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <asp:Panel runat = "server" visible = "false" ID = "pnlAdvancedSearch" scrollbars = "Auto">
        <asp:Table runat = "server">
        <asp:TableRow>
        <asp:TableCell Text = "NetworkLogin"> </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID = "ddlAdvNetworkLogin" runat = "server" >
            <asp:ListItem>N/A</asp:ListItem>
            <asp:ListItem>like</asp:ListItem>
            <asp:ListItem>Starts With</asp:ListItem>
            <asp:ListItem>Ends With</asp:ListItem>
            <asp:ListItem>is Null</asp:ListItem>
            <asp:ListItem>is Not Null</asp:ListItem>
        </asp:DropDownList> </asp:TableCell>
        <asp:TableCell><asp:TextBox  runat = "server" ID = "txtAdvNetLogin"></asp:TextBox> </asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
        <asp:TableCell Text = "Email"> </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID = "ddlAdvEmail" runat = "server" >
            <asp:ListItem>N/A</asp:ListItem>
            <asp:ListItem>like</asp:ListItem>
            <asp:ListItem>Starts With</asp:ListItem>
            <asp:ListItem>Ends With</asp:ListItem>
            <asp:ListItem>is Null</asp:ListItem>
            <asp:ListItem>is Not Null</asp:ListItem>
        </asp:DropDownList> </asp:TableCell>
        <asp:TableCell><asp:TextBox  runat = "server" ID = "txtAdvEmail"></asp:TextBox> </asp:TableCell>
        </asp:TableRow>
                        <asp:TableRow>
        <asp:TableCell Text = "FirstName"> </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID = "ddlAdvFirstName" runat = "server" >
            <asp:ListItem>N/A</asp:ListItem>
            <asp:ListItem>like</asp:ListItem>
            <asp:ListItem>Starts With</asp:ListItem>
            <asp:ListItem>Ends With</asp:ListItem>
            <asp:ListItem>is Null</asp:ListItem>
            <asp:ListItem>is Not Null</asp:ListItem>
        </asp:DropDownList> </asp:TableCell>
        <asp:TableCell><asp:TextBox  runat = "server" ID = "txtAdvFirstName"></asp:TextBox> </asp:TableCell>
        </asp:TableRow>
                        <asp:TableRow>
        <asp:TableCell Text = "LastName"> </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID = "ddlAdvLastName" runat = "server" >
            <asp:ListItem>N/A</asp:ListItem>
            <asp:ListItem>like</asp:ListItem>
            <asp:ListItem>Starts With</asp:ListItem>
            <asp:ListItem>Ends With</asp:ListItem>
            <asp:ListItem>is Null</asp:ListItem>
            <asp:ListItem>is Not Null</asp:ListItem>
        </asp:DropDownList> </asp:TableCell>
        <asp:TableCell><asp:TextBox  runat = "server" ID = "txtAdvLastName"></asp:TextBox> </asp:TableCell>
        </asp:TableRow>
                        <asp:TableRow>
        <asp:TableCell Text = "Title"> </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID = "ddlAdvTitle" runat = "server" >
            <asp:ListItem>N/A</asp:ListItem>
            <asp:ListItem>like</asp:ListItem>
            <asp:ListItem>Starts With</asp:ListItem>
            <asp:ListItem>Ends With</asp:ListItem>
            <asp:ListItem>is Null</asp:ListItem>
            <asp:ListItem>is Not Null</asp:ListItem>
        </asp:DropDownList> </asp:TableCell>
        <asp:TableCell><asp:TextBox  runat = "server" ID = "txtAdvTitle"></asp:TextBox> </asp:TableCell>
        </asp:TableRow>
                        <asp:TableRow>
        <asp:TableCell  runat = "server" Text = "Phone"> </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID = "ddlAdvPhone" runat = "server" >
            <asp:ListItem>N/A</asp:ListItem>
            <asp:ListItem>like</asp:ListItem>
            <asp:ListItem>Starts With</asp:ListItem>
            <asp:ListItem>Ends With</asp:ListItem>
            <asp:ListItem>is Null</asp:ListItem>
            <asp:ListItem>is Not Null</asp:ListItem>
        </asp:DropDownList> </asp:TableCell>
        <asp:TableCell><asp:TextBox runat = "server" ID = "txtAdvPhone"></asp:TextBox> </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <asp:CheckBoxList runat = "server" ID = "cblUserSearch" RepeatColumns = "3"  Font-Size="X-Small"> </asp:CheckBoxList>
        <br />
        <asp:Button runat = "server" ID = "btnAdvancedSearch" Text = "Search" />
        <br />
        <asp:LinkButton runat = "server" ID = "lbCloseAdv" Text = "Close Advanced Options"></asp:LinkButton>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel runat = "server" ID = "pnlEDSearches" ScrollBars = "Auto">
        <asp:GridView runat = "server" ID = "gvEDSearches" AllowPaging ="True" AllowSorting ="True" AutoGenerateColumns ="False"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" AutoGenerateSelectButton = "true"
                           Width="80%" height="100%"
            HeaderStyle-Wrap="true" PageSize="15" >
            <columns>
                        <asp:TemplateField HeaderText = "Edit" ShowHeader = "false">
                 <ItemTemplate>
                 <asp:LinkButton ID = "btngvTOedit" runat = "server" CommandName = "Edit" Text = "Edit" ></asp:LinkButton>
                 </ItemTemplate>
                 <EditItemTemplate>
                 <asp:LinkButton ID = "btngvTOupdate" runat = "server" CommandName = "Update" Text = "Update"></asp:LinkButton>
                 <asp:LinkButton ID = "btngvTOcancel" runat = "server" CommandName = "Cancel" Text = "Cancel"></asp:LinkButton>
                 </EditItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField visible = False>
                 <ItemTemplate>
                 <asp:Label ID = "lblIdUser" runat = "server" Text = '<%# Eval("IdUser") %>' ></asp:Label> </ItemTemplate>
                 </asp:TemplateField>
                 
             <asp:TemplateField  HeaderText = "Network Login"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("NetworkLogin") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvEDNetworkLogin" runat = "server" Text = '<%# Eval("NetworkLogin") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
                 
             <asp:TemplateField  HeaderText = "Email"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("Email") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvEDEmail" runat = "server" Text = '<%# Eval("Email") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
                 
             <asp:TemplateField  HeaderText = "First Name"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("FirstName") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvEDFirstName" runat = "server" Text = '<%# Eval("FirstName") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
                 
             <asp:TemplateField  HeaderText = "Last Name"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("LastName") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvEDLastName" runat = "server" Text = '<%# Eval("LastName") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
                 
             <asp:TemplateField  HeaderText = "Title"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("Title") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvEDTitle" runat = "server" Text = '<%# Eval("Title") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>

                 </columns>
        </asp:GridView></asp:Panel>
        <br />
        <asp:Panel ID = "pnlgvDetails" runat = "server" visible = "false" Scrollbars = "Auto">
        <asp:GridView runat = "server" ID = "gvEDUserDetails" AllowSorting ="True" AutoGenerateColumns ="False"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" AutoGenerateSelectButton = "False"
                           Width="80%" height="100%"
            HeaderStyle-Wrap="true">
            <columns>
                                    <asp:TemplateField HeaderText = "Edit" ShowHeader = "false">
                 <ItemTemplate>
                 <asp:LinkButton ID = "btngvUDedit" runat = "server" CommandName = "Edit" Text = "Edit" ></asp:LinkButton>
                 </ItemTemplate>
                 <EditItemTemplate>
                 <asp:LinkButton ID = "btngvUDupdate" runat = "server" CommandName = "Update" Text = "Update"></asp:LinkButton>
                 <asp:LinkButton ID = "btngvUDcancel" runat = "server" CommandName = "Cancel" Text = "Cancel"></asp:LinkButton>
                 </EditItemTemplate>
                 </asp:TemplateField>

                             <asp:TemplateField HeaderText = "Column Name">
                 <ItemTemplate >
                 <asp:Label ID = "lblgvUDColumnName" runat = "server" Text = '<%# Eval("Column Name") %>' ></asp:Label> </ItemTemplate>
                 </asp:TemplateField>

                              <asp:TemplateField  HeaderText = "Table Value"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("Column Value") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvUDColumnValue" runat = "server" Text = '<%# Eval("Column Value") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
            </columns>
        </asp:GridView>
        </asp:Panel>
       
        </ContentTemplate>
        </cc1:TabPanel>

         <cc1:TabPanel runat="server" HeaderText="Add to Email Dist" ID="TabPanel1">
        <ContentTemplate>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
        <asp:Panel runat="server">
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan ="4">
                        Add New User
                    </asp:TableCell>
                    <asp:TableCell Width="10"></asp:TableCell>
                    <asp:TableCell RowSpan="7" VerticalAlign="Middle" HorizontalAlign="Center">
                        <asp:Panel runat="server" >
                            Add Features:<br />
                            <asp:CheckBoxList runat = "server" ID = "cblAddBoxes" RepeatColumns = "3"  Font-Size="X-Small"> </asp:CheckBoxList>
                        </asp:Panel>
                    </asp:TableCell>
                    </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        First Name:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtAddFirstName"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Last Name:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtAddLastName"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Network Login:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtAddNetworkLogin"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Email:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtAddEmail"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell ForeColor ="Red" Font-Bold ="true">
                        *
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Title:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtAddTitle"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
               <asp:TableRow>
                    <asp:TableCell>
                        Phone:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtAddPhone"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Height="10px">
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan ="4">
                       <asp:Button ID="btnAddUser" runat="server" Text="Add User" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

        </asp:Panel>
       
           <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Table runat="server">
             <asp:TableRow>
                 <asp:TableCell>
                      <asp:Button id="PrintButton"  CssClass="Printbutton" BorderStyle="Outset" BorderWidth="2px" Font-Size = "small" Visible="false" text="Print" runat="Server" />
                      <asp:Button ID="OkButton"  CssClass="Printbutton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
          <asp:Button ID="SubmitButton"  CssClass="Printbutton" Visible ="false" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Submit"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5"></asp:TableCell>
                 <asp:TableCell>         
              <asp:Button ID="CancelButton"  CssClass="Printbutton" Visible ="false" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/>
                 </asp:TableCell>
             </asp:TableRow>
         </asp:Table>

             <asp:Label ID="lblHoldOverSQL" runat ="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblHoldOverrows" runat ="server" Visible="false" Text=""></asp:Label>
                   </asp:TableCell>
</asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="Panel1"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>
            </ContentTemplate>
</asp:UpdatePanel>
            
        </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel runat="server" HeaderText="Misc." ID="tpMisc">
        <ContentTemplate>
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    DataSourceID="nsmvdwhsvr01" AllowPaging="True" AllowSorting="True" Visible="True" PagerSettings-PageButtonCount="10" PageSize="25">
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="User ID" 
            SortExpression="UserName" />
        <asp:BoundField DataField="RoleName" HeaderText="Role Name" 
            SortExpression="RoleName" />
    </Columns>
</asp:GridView>
 <asp:SqlDataSource ID="nsmvdwhsvr01" runat="server" 
    ConnectionString="<%$ ConnectionStrings:WebFDConnectionString %>" SelectCommand="  select UserName, RoleName  
  from WebFD.dbo.vw_aspnet_Users a 
  join WebFD.dbo.vw_aspnet_UsersInRoles b on a.UserId = b.UserId 
  join WebFD.dbo.vw_aspnet_Roles c on b.RoleId = c.RoleId   
  order by 1, 2 "></asp:SqlDataSource>


        </ContentTemplate>
     

        </cc1:TabPanel>

    </cc1:TabContainer>
   
  


</asp:Content>
