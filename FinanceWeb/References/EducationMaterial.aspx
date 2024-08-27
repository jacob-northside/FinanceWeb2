<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EducationMaterial.aspx.vb" Inherits="FinanceWeb.EducationMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h5>  <asp:Label ID="Label1" runat="server" Font-Bold="true" 
        Text="The following is a list of past educational presentations related to Financial Operations (Note:  some of these presentations are dated, however, many are still useful from a conceptual standpoint):"></asp:Label></h5>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Finance & Accounting: " Font-Underline="true" Font-Bold="true"></asp:Label><br /><br />
              <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px; " id="FinOps" title="Finance & Accounting">
               <li>  <h5> <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/General%20users/Investment%20Analysis%20Class_files/frame.htm" 
        Target="_blank">Capital Investment Analysis/Finance 101</asp:HyperLink>  &nbsp;- General introduction to healthcare finance, capital budgeting, and investment analysis.
</h5> <br /></li>
               <li>  <h5>   <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline ="true"  
        NavigateUrl="http://nshdsweb/General%20users/Cost%20Accounting%20101_files/frame.htm" 
        Target="_blank">Cost Accounting</asp:HyperLink> 
        &nbsp; Guideline to the Cost accounting process   </h5> <br />   </li>
               <li><h5>    <asp:HyperLink ID="HyperLink8" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/General%20users/Reference/GLOSSARY%20OF%20TERMS.htm" 
        Target="_blank">Glossary of Financial Terms</asp:HyperLink>
        &nbsp; Definition of key financial terms. </h5><br /> </li>
                   </ul>
 
     <asp:Label ID="Label3" runat="server" Text="Reimbursement Related: " Font-Underline="true" Font-Bold="true"></asp:Label><br />
    <br />
     <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px; " id="Ul1" title="Reimbursement Related">
     <li>  <h5><asp:HyperLink ID="HyperLink6" runat="server" Font-Underline ="true"  
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/Medicare%20Inpatient%20Reimbursement_files/frame.htm" Target="_blank">Medicare Inpatient Reimbursement</asp:HyperLink>&nbsp;and NSH        
    <asp:HyperLink ID="HyperLink7" runat="server" Font-Underline ="true"  
        NavigateUrl="http://nshdsweb/General%20users/NSH%20Medicare%20Outlier%20Example.htm" 
        Target="_blank">Medicare Outlier Examples</asp:HyperLink>  </h5> <br /> </li>
     <li><h5>    <asp:HyperLink ID="HyperLink9" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/Medicare%20Inpatient%20Reimbursement_files/frame.htm" 
        Target="_blank">Medicare Patient Reimbursement</asp:HyperLink> &nbsp;– Presentation on how Medicare reimbursement is calculated. </h5><br />   </li>
     <li>   <h5>  <asp:HyperLink ID="HyperLink15" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/Reimbursement%20Lunch%20and%20Learn%20Feb07_files/frame.htm" Target="_blank">Reimbursement Lunch and Learn Feb 07</asp:HyperLink>&nbsp;– Presentation on many different types of reimbursement methodologies. </h5><br /> </li>
     <li><h5>   <asp:HyperLink ID="HyperLink10" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/CPA%20Rollout_files/frame.htm" 
        Target="_blank">CPA Rollout Presentation</asp:HyperLink>&nbsp;– Discussion on Contract IDs and Expected Payments</h5><br /> </li>
     </ul>
    
         <asp:Label ID="Label4" runat="server" Text="Trendstar Related: " Font-Underline="true" Font-Bold="true"></asp:Label><br />
    <br />
     <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px; " id="Ul2" title="Trendstar Related">
     <li>  <h5>    <asp:HyperLink ID="HyperLink5" runat="server" Font-Underline ="true" 
        NavigateUrl="http://nshdsweb/General%20users/Trendstar_files/frame.htm" 
        Target="_blank">Trendstar</asp:HyperLink> &nbsp;- Presentation on the uses and benefits of Trendstar </h5><br /> </li>
     <li> <h5> <asp:HyperLink ID="HyperLink19" runat="server" Font-Underline ="true" 
        NavigateUrl="http://nshdsweb/General%20users/TSTAR%20Process%20Flowchart_files/frame.htm" 
        Target="_blank">Trendstar Flow Chart</asp:HyperLink>
   &nbsp;- Visual diagram of source systems that interface with Trendstar. </h5> <br /></li>
         </ul>
         
         <asp:Label ID="Label5" runat="server" Text="General Healthcare: " Font-Underline="true" Font-Bold="true"></asp:Label><br />
    <br />
     <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px; " id="Ul3" title="General Healthcare">
     <li>  <h5>  <asp:HyperLink ID="HyperLink14" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/UB04presentation01232007.pdf" 
        Target="_blank">UB04 Presentation</asp:HyperLink>&nbsp;– Detailed presentation on the use of UB04 Uniform Bill.</h5> <br /></li>
     <li>  <h5>  <asp:HyperLink ID="HyperLink16" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/Pricing%20Transparency%20Feb07_files/frame.htm" Target="_blank">Pricing Transparency Feb 07</asp:HyperLink>&nbsp;- Informational presentation on pricing transparency within the Healthcare industry. </h5></li>
     </ul>
     <br />
     <br />

<%--  LINKS THAT WERE DEEMED OLD AND SHOULD NOT BE USED
   <h5>    <asp:HyperLink ID="HyperLink11" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/Departmental%20Write-off%20Calculation_files/frame.htm" 
        Target="_blank">Departmental Write-off Calculation Presentation</asp:HyperLink></h5>
     <h5>   <asp:HyperLink ID="HyperLink12" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/NSH%20Medicare%20Outlier%20Example.htm" 
        Target="_blank">NSH Medicare Outlier Examples</asp:HyperLink></h5>
        <h5>   <asp:HyperLink ID="HyperLink13" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/internal%20users/Presentations/Overview%20of%20Radiology_files/frame.htm" 
        Target="_blank">Overview of Radiology</asp:HyperLink></h5>
           <h5>  <asp:HyperLink ID="HyperLink17" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Presentations/UB%20Revenue%20Code%20Reporting.pdf" 
        Target="_blank">UB Revenue Code Reporting</asp:HyperLink></h5>
         <h5>  <asp:HyperLink ID="HyperLink18" runat="server" Font-Underline ="true"
        NavigateUrl="http://nshdsweb/Internal%20users/Reference/Glossary%20of%20Terms.htm" 
        Target="_blank">Glossary of Terms</asp:HyperLink></h5>
               <h5>    <asp:HyperLink ID="HyperLink3" runat="server" Font-Underline ="true"  
        NavigateUrl="http://nshdsweb/General%20users/Education/Financial%20Skills%20for%20Dir%20and%20Mgrs_files/frame.htm" Target="_blank">Financial Skills</asp:HyperLink>  &nbsp; Guideline to financial tools and processes          </h5>  
  <h5>   <asp:HyperLink ID="HyperLink4" runat="server" Font-Underline ="true"  
        NavigateUrl="http://nshdsweb/General%20users/OPERATING%20BUDGET%20EDUCATION_files/frame.htm" 
        Target="_blank">Operating Budget Process</asp:HyperLink>
  Guideline to the operating budget process </h5>
   --%>
    

 
  
  
   
 
   

  
 
 

   
  
 <br />

    <br />
    <br />
</asp:Content>
