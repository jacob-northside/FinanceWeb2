<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="NorthsideForms.aspx.vb" Inherits="FinanceWeb.NorthsideForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h5><asp:Label ID="Label1" runat="server" Text="Northside Hospital Request Forms:"></asp:Label> <br />
 </h5><br /> 

       <asp:Label ID="Label2" runat="server" Text="Forms: " Font-Underline="true" Font-Bold="true"></asp:Label><br /><br />
              <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px; " id="FinOps" title="Finance & Accounting">
               <li>  <h5>  <asp:HyperLink ID="HyperLink1" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Capital/z_Projects/Capital%20Project%20Worksheet.xls">
               Capital Project Budget Form.</asp:HyperLink> &nbsp; </h5> <br /></li>
               <li>  <h5> <asp:HyperLink ID="HyperLink3" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Decision%20Support/Forms/Case%20Mix%20Report%20Request%20Form.xls">
                             Case Mix Request Form </asp:HyperLink> &nbsp;  </h5> <br />   </li>
               <li><h5> <asp:HyperLink ID="HyperLink4" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Decision%20Support/Forms/Operating%20Budget%20Contingency%20Form.xls">
                             Contingency Request Form</asp:HyperLink> </h5> <br /> </li>
               <li><h5> <asp:HyperLink ID="HyperLink5" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Forms/COST%20CENTER%20CHANGE%20FORM.xls">
                             Cost Center Change Form </asp:HyperLink> </h5>  <br /> </li>
               <li><h5> <asp:HyperLink ID="HyperLink6" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Decision%20Support/Forms/Physician%20Request%20Policy%20&%20Form%20Case%20Mix%20Patient%20Info.doc">
                             Physician Information Request Form  </asp:HyperLink>   </h5><br /> </li>
               <li><h5> <asp:HyperLink ID="HyperLink7" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Sign-On%20Forms/Sign-On_Form.doc">
                             Sign-on Request Form  </asp:HyperLink>   </h5><br /> </li>
               <li><h5> <asp:HyperLink ID="HyperLink8" Font-Underline ="true" runat="server" NavigateUrl="file:///P:/Forms/Travel%20&%20Ed%20Req%20Form.xls">
                             Travel & Education Request Form </asp:HyperLink>  </h5><br /> </li>
               <li><h5> <asp:HyperLink ID="HyperLink2" Font-Underline ="true" runat="server" Target="_blank" NavigateUrl="file:///P:/Decision%20Support/Budget/Expense%20Definitions%20&%20Zero%20Based.doc">
                             Zero Based Expense Form </asp:HyperLink>  </h5></li>    
              </ul>
  
  <h5> Please note that all forms are available on the Northside Hospital network. Access is restricted by your user account information. </h5>
</asp:Content>
