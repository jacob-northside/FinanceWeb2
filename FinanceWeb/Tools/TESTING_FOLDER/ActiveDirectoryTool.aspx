<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ActiveDirectoryTool.aspx.vb" Inherits="FinanceWeb.ActiveDirectoryTool" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     
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


</asp:Content>
