<%@ Page Title="" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" Inherits="FinanceWeb.FinancialOperations_FinOpPage1" Codebehind="FinancialSystemsIntegration.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="~/SiteHome.css" rel="stylesheet" type="text/css" runat="server"/> 
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
 
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
  
 <div id="MiddleSection" style="overflow:hidden;" >  
 <%-- This div is required to keep the next two divs the same height allowing the dotted line to be equal reguardless of which div is taller --%>
       <div id="LeftPanel" style="float: left; width: 50%;
            border-right-style: dotted; border-right-width: thin; border-right-color: #214B9A;
             border-top-style: dotted; border-top-width: thin; border-top-color: #214B9A;
             position: inherit; clear: inherit; padding-bottom:100%; margin-bottom:-100%;" > 
         <%-- The Padding at the bottom and margin allow the two divs to maintain the same height reguardless if one is longer than the other  --%>
      
         <h6><u><b>Data Stores:</b></u></h6>         
                
         <%--     <ul style="list-style-type: circle; list-style-position: outside; font-size: small; margin-top: 0px; "          id="DataStores" title="Data Stores">
          <li>DAA (Owned by IT)</li>
          <li>Trendstar/MPA (Star)</li>
          <li>Physician Datamarts</li>
          <li>General Ledger</li>
          <li>Payables</li>
          <li>Materials Management</li>
          <li>GDDS (GHA Competitive Data)</li> 
          </ul>
          <br />--%>
                     <ul style="list-style-type: circle; list-style-position: outside; font-size: small; margin-top: 0px; "          id="Ul1" title="Data Stores">
          <br /> <li><b>Daily Activity Analysis (DAA) </b><br />Daily load of patient level charges from Star maintained by the IS department.</li>
<li><b>Trendstar/MPA (<asp:HyperLink ID="hlTSTAR" NavigateUrl ="~/Applications/Applications.aspx?ID=TSTAR" runat="server">TSTAR</asp:HyperLink> /MPA)</b><br /> McKesson business analytic data sourced from the <asp:HyperLink ID="HyperLink6" NavigateUrl ="~/Applications/Applications.aspx?ID=STAR" runat="server">Star</asp:HyperLink> &nbsp;financials data system</li>
<li><b>Physician Practices (MD)</b> <br /> Physician practice EMR data for each practice Northside has acquired.  </li>
<li><b>General Ledger  <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Applications/Applications.aspx?ID=HEFM?ID2=GL">(HEFM)</asp:HyperLink></b><br /> McKesson Horizon Enterprise Financial general ledger system data updated daily.</li>
<li><b>Payables <asp:HyperLink ID="hlHEFM" runat="server" NavigateUrl="~/Applications/Applications.aspx?ID=HEFM?ID2=AP">(HEFM)</asp:HyperLink></b><br /> McKesson Horizon Enterprise Financial accounts payable system data updated daily.</li>
<li><b>Fixed Assets (FA) <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Applications/Applications.aspx?ID=HEFM?ID2=FA">(HEFM)</asp:HyperLink></b><br />
 McKesson Horizon Enterprise Financial fixed assets updated daily. 
</li>
<li><b>Project Accounting (Projects) <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Applications/Applications.aspx?ID=HEFM?ID2=Projects">(HEFM)</asp:HyperLink></b><br />
McKesson Horizon Enterprise Financial project accounting updated daily. 
</li>
<%--<li><b>Receivables <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Applications/FDHEFMApp.aspx">(HEFM)</asp:HyperLink></b>  <br /> McKesson Horizon Enterprise Financial accounts receivable system data updated daily.</li>--%>
<li><b>Supply Chain Management <asp:HyperLink ID="hlStrategicSourcing" runat="server" NavigateUrl="~/Applications/Applications.aspx?ID=HEMM">(MSSS)</asp:HyperLink> </b><br />
 McKesson Strategic Supply Sourcing (formaly know as Horizon Enterprise Material Management) system data that contains facility purchase orders and invoices updated daily.</li>
<li> <b>Georgia Discharge Data System (GDDS)</b> <br /> Georgia Hospital Association data for each hospital in the state, commonly referred to as the competitive database,  updated quarterly. </li>
            </ul>
       
          </div>
 
          <div id="RightPanel" style="float: left; width: 48%;
               border-top-style: dotted; border-top-width: thin; border-top-color: #214B9A;
               position: inherit; padding-bottom: 100%; margin-bottom:-100%;" >
                 <%-- The Padding at the bottom and margin allow the two divs to maintain the same height reguardless if one is longer than the other  --%>
 <h6><u><b>Deployment tools and Mechanisms</b></u></h6>
       <br />   <asp:Table ID="Table3" runat="server">
 
          <asp:TableRow>
          <asp:TableCell> </asp:TableCell>
                    <asp:TableCell>
                  
               <h5>SQL Service Reporting Services (SSRS) - maintained by FSI  </h5>
                       <h4>   <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                       NavigateUrl="http://ssrs.northside.local/Reports/Pages/Folder.aspx" Target="_blank">SSRS Reports</asp:HyperLink>
</h4>
                    </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                       <h5>Quarterly Financials Book produced by Brenda Riordan and team </h5>
                       <h4>  <asp:HyperLink ID="HyperLink4" runat="server" Font-Underline="True"
                  NavigateUrl="../References/BoardBooks.aspx">Finance Division Quarterly Reporting (ACCESS RESTRICTED)</asp:HyperLink></h4>
                                      
                    </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                  
                  <h5>McKesson Performance Analytics environment (aka - HBI) </h5> 
                      <h4><asp:HyperLink ID="hlSpotFire" runat="server" Font-Underline="True"
                      NavigateUrl="http://nsmhbidb/hbi_viewer/default.asp"  Target="_blank">Explorer Analytics (Log-In Required)</asp:HyperLink></h4>  
                    </asp:TableCell>
                    </asp:TableRow>
              
                  <%--  <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    <h5>Add text here</h5>
                    <h4><asp:HyperLink ID="hlAdHoc1" runat="server" Font-Underline="True"
                    NavigateUrl="#" Target="_blank">Ad Hoc Report #1</asp:HyperLink></h4>
                    </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    <h5>Add text here</h5>
                    <h4><asp:HyperLink ID="hlAdHoc2" runat="server" Font-Underline="True"
                     NavigateUrl="#" Target="_blank">Ad Hoc Report #2</asp:HyperLink></h4>
                    </asp:TableCell>
                    </asp:TableRow>--%>
          </asp:Table>
        </div>
     </div>
          
          <div id="Bottom" style="float: left; width: 100%; 
                   position: inherit; clear: inherit;"  >
<div id="FullBottom"  >
  <div id="LeftPanel2" style="float: left; width:50%; 
         border-right-style: dotted; border-right-width: thin; border-right-color: #214B9A;
             border-top-style: dotted; border-top-width: thin; border-top-color: #214B9A;
             position: inherit; clear: inherit; ">
       
   <h6>   <asp:Label ID="Label1" runat="server" Text="   "></asp:Label></h6><br /><br />
  
   <br /><br /><br /><br /><br /><br />

       </div>

       <div id="RightPanel2"  style="float: left; width: 48%;
               border-top-style: dotted; border-top-width: thin; border-top-color: #214B9A;
               position: inherit; clear: inherit; ">
            <h6>  <b><u>   &nbsp;<asp:Label ID="Label2" runat="server" Text="Quick links"></asp:Label></u></b></h6>    <br />
           <asp:Table ID="Table4" runat="server">
        <%--    <asp:TableRow>
           <asp:TableCell><br /></asp:TableCell>
          <asp:TableCell><h5>Facilities Planning Bed Information Reports - Maintained by Facilities Planning and Development</h5>
            <h5><asp:HyperLink ID="hlBedInformationReport" runat="server" Font-Underline="True"
                  NavigateUrl="file:///P:/Shared%20Files/Facilities%20Planning/Bed%20Information%20Report">Bed Information Reports</asp:HyperLink> </h5>
           </asp:TableCell>
           </asp:TableRow>--%>
       
            <asp:TableRow >
            <asp:TableCell></asp:TableCell>
            <asp:TableCell> <h5>    <asp:HyperLink ID="hlSSISInternalPractices" runat="server" Font-Underline="True"
               NavigateUrl="~/pdfDocumentation/SSISInternalPractices.pdf" Target="_blank">SSIS Internal Practices </asp:HyperLink></h5>
               <h5>SSIS Internal Practices developed by FSI team. Last updated: 2013-07-30</h5> 
                      </asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow >
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>  <h5><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/UploadFiles/MonthlyStatsReport/Monthly Stat Report.pdf" Target="_blank" Font-Underline="True">Monthly Stats Report</asp:HyperLink></h5> 
            <h5>The monthly Statistical Report contains metrics of trended volumes by facility for the current and prior fiscal years, as well as patient days by nursing unit for the current month. This statistical report is typically available on the 10th of each month. <br /> Contact FSI team for any questions. </h5> 
          
</asp:TableCell>
            </asp:TableRow>
          
          
              </asp:Table>
            <br /><br /><br />
             </div>
             </div>
           
     <asp:Table ID="Table1" runat="server">
              <asp:TableRow >
              <asp:TableCell Width="10px" >  <asp:Image ID="Image1" runat="server" BackColor="#FFA15C" BorderColor="Black" 
                  BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif" 
                  Height="10px" Width="10px" /> 
                  
                    <cc1:DropShadowExtender ID="DropShadowExtender2" runat="server" 
                  Enabled="True" TargetControlID="Image1" Opacity="5" Radius="5" Rounded="False" Width="3">
              </cc1:DropShadowExtender>
                   </asp:TableCell>
              <asp:TableCell>     <h6><b> &nbsp;People That Get it Done</b>  </h6>   </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell>
              
<i>by Andy Warren</i> <br /><br />
This editorial follows up on <u>Are you Easy to Work With </u> with the question: are you one of those people that gets it done at work? <br />
As a manager I want people that are easy to work with, but I want people that get it done. <br /><br />

What does that mean? Let's make a list: <br />
<ul>
<li>They take ownership of the assignment. They ask the questions and they tell me what they need.</li>
<li>They work around obstacles</li>
<li>They hold themselves accountable to the deadline</li>
<li>They raise an alarm as soon as the deadline is in danger</li>
</ul>

It's what I want from my plumber, my lawn guy, even my doctor. It's asking someone to take on work, having them work through the requirements and agree to a timeline, and getting it done. <br /><br />
Getting it done doesn't mean working 20 hour days. In very rare cases it might, but it's really about ownership. As a manager (or homeowner even) I identify work that needs to be done and assign someone to do the work. I want that person to be a true professional and ask the questions that lead us both to a solid understanding of the work and the risks, and then I want them to own it, coming back to me proactively when they need help or guidance. <br /><br />

Getting it done sometimes means being creative. Sometimes it means being persistent. It always means being reliable. Are you one of those people the boss knows will get it done? <br />
And if not, why not?<br />

              
              
              </asp:TableCell>
              </asp:TableRow>
              </asp:Table>

                  <asp:LinkButton ID="lbAdministrative" runat="server" Style="float: left; width: 100%; 
                   position: inherit; clear: inherit; " PostBackUrl="~/Tools/FSIUsers/Administrative/ManageUsers.aspx" Visible="False">Admin</asp:LinkButton>

              </div>



</asp:Content>
 


