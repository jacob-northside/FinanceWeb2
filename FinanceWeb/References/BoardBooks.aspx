<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" EnableEventValidation = "false"
CodeBehind="BoardBooks.aspx.vb" Inherits="FinanceWeb.BoardBooks" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <style type="text/css">
.pln{color:#000}.tag{font-weight:bold}.tag{color:#a31515}.atn{color:red}.pun,.opn,.clo{color:#000}.atv{color:#00f}
        .style1
        {
            font-size: 100%;
            vertical-align: baseline;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
            background:;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h4>Quarterly Reporting</h4>
   <h5>** <u>Please note that only authorized users can open the following files.</u> **<br /><br /></h5>

     <asp:Repeater ID="FileListRepeater" runat="server">
    <ItemTemplate>
        <div>
            <asp:HyperLink runat="server" ID="lnkFile"  />
        </div>
    </ItemTemplate>
</asp:Repeater>
<br />
<br /> 
    <%-- <h5>  <asp:HyperLink ID="HyperLink1" Font-Underline ="true" runat="server" NavigateUrl="file:///J:\Shared\FSI\BoardBooks\FY 2013 Q4 Financial Book.lnk">
              FY 2013 Q4 Financial Book TEST</asp:HyperLink> &nbsp; </h5> --%>
              <br /> 
              <br />
    <asp:Label ID="Label1" runat="server" Text="Label" visible ="False" ></asp:Label>
</asp:Content>

