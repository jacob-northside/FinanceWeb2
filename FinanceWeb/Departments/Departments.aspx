<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Departments.aspx.vb" Inherits="FinanceWeb.Departments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">


</asp:Content> 
<asp:Content ID="Content2" runat="server" contentplaceholderid="Maincontent">
         <div id="divAccounting" style="border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">   

    <h3>Accounting</h3><br />
    <%--     <h5>Functional Area 1 - Holly</h5>
         <h5>Functional Area 2 - David</h5>
         <h5>Functional Area 3 - Yolanda</h5>
         <h5>Functional Area 4 - Kevin</h5>
         <h5>Functional Area 5 - Vivian</h5>--%>
     </div>

     <div id="divCampusOps" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">         

    <h3>Campus Operations</h3><br />
      <%--   <h5>Functional Area 1 - Atlanta - Mitch L.</h5>
         <h5>Functional Area 2 - Cherokee - Michael H. J. </h5>
         <h5>Functional Area 3 - Forsyth - Eric C. </h5>--%>
</div>

<div id="divFSI" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">               
    
    <h3>FSI - Financial Systems Integration</h3><br />
         
    <asp:Table ID="Table2" runat="server">
              <asp:TableRow >
              <asp:TableCell  Width="10px">  <asp:Image ID="Image2" runat="server" BackColor="#FFA15C" BorderColor="Black" 
                  BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif" 
                  Height="10px" Width="10px" /> 
                  
             

                    <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" 
                  Enabled="True" TargetControlID="Image2" Opacity="5" Radius="5" Rounded="False" Width="3">
              </cc1:DropShadowExtender>
                   </asp:TableCell>
              <asp:TableCell>     <h6> <b> &nbsp;What is FSI and how can we help you today.</b></h6>   </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow>
              <asp:TableCell></asp:TableCell>
              <asp:TableCell> 
<h5><b>Vision:</b> Provide the right information to the right people before they know they need it.  </h5> 

<%-- <h5><b>Customers:</b> Executive management, Management, and Data Analysts (with a focus on financial areas)</h5>  --%><br />

<h5> <b>How:</b> We must be Northside Hospital's single most trusted source for data, analytics and process automation. We must know where the data comes from, what the data means, and how to turn the data into information. From a technical perspective, we must know how data is input, how data is transferred to us, how to store the data and how to deploy the data so end users can understand it. From a business perspective, we must know how the source systems work, how/why the business areas work, and how the information feeds the larger picture.</h5><br />
 
            <%--    <h5><b>Goal: </b> What are the goals for the FSI team</h5>--%>
              </asp:TableCell>
              </asp:TableRow>
              
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell>
            
              </asp:TableCell>
              </asp:TableRow>
         
          
              </asp:Table>
</div>
<div id="divFPandA" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">            

    <h3>FP & A - Financial Planning and Analysis ????</h3><br />
   <%--      <h5>Functional Area 1 - Katherin</h5>
         <h5>Functional Area 2 - K. Moody</h5>
         <h5>Functional Area 3 - A. Bledso</h5>
         <h5>Functional Area 4 - Eric H. </h5>--%>
         </div>
<div id="divManagedCare" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">       

    <h3>Managed Care </h3><br />
         
</div>

<div id="divProductivityMgt" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">   

    <h3>Productivity Management</h3><br />
         
</div>
<div id="divRevenueIntegraty" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color:#214B9A;" runat="server">  

    <h3>Revenue Integraty</h3><br />
         
</div>





     
     
      <div id="LeftPanel"   
              style="float: left; width: 38%; border-right-style: dotted; border-right-width: thin; border-right-color: #214B9A;">


          </div>
          <div id="RightPanel" style="float: right; width: 60%">
          </div>
        </asp:Content>
 

