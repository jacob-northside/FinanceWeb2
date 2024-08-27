<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/SiteDev.Master" AutoEventWireup="true" CodeBehind="DefaultDev.aspx.vb" Inherits="FinanceWeb._DefaultDev" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <style type="text/css">

          .ForceCenter {
              text-align:center;
              align-content:center;
              align-items:center;
              align-self:center;
          }

          </style>

    <section class="featured">

        <div class="content-wrapper">
            <%--    <hgroup class="title">
             <h1><%: Title %></h1> 
                 <h2>Finance Web</h2>
            </hgroup>--%>
            <asp:Table runat="server" Width="100%" Font-Size="Medium" ForeColor="White" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
                <asp:TableRow>
                    <asp:TableHeaderCell Width="30%" CssClass="ForceCenter">
                        Descriptive (What happened?)
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="40%" CssClass="ForceCenter">
                        Diagnostic (Why is it happening?)
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="30%" CssClass="ForceCenter">
                        Predictive (What may happen?)
                    </asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top" CssClass="ForceCenter">
                           <%-- many users x very high applicability--%>
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top" CssClass="ForceCenter">
                        <asp:Label runat="server"> <%--many users x high analytic value = maximum utility --%></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top" CssClass="ForceCenter">
                        <asp:Label runat="server"><%-- very few users x very high analytic value--%> </asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>--%>

    <div id="LeftPanel"
        style="float: left; width: 38%; border-right-style: dotted; border-right-width: thin; border-right-color: #214B9A; padding-left: 0px; padding-right: 10px; padding-top: 5px;">
        <h3>Welcome to the Financial Division Home Page</h3>
        <br />
        <h6>Financial Operations – Who are we?</h6>

        <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px;" id="FinOps" title="Financial Operations – Who are we?">
            <li>
                <h5>Accounting </h5>
            </li>
            <li>
                <h5>Campus Financial Operations  </h5>
            </li>
            <li>
                <h5>Finance Strategies & Project Management  </h5>
            </li>
            <li>
                <h5>Financial Systems Integration  </h5>
            </li>
            <li>
                <h5>Financial Planning & Analysis  </h5>
            </li>
            <li>
                <h5>Physician Financial Operations </h5>
            </li>
        </ul>

        <h6>Our Finance Professionals are your business partners. </h6>

        <h6>Our Goals: </h6>


        <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px;" id="Ul1" title="Our Goals:">
            <li>
                <h5>To produce information and analytics that enable:</h5>
            </li>
            <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px;">
                <li>Good decision making by educated operational leaders </li>
                <li>Focused measurement of financial performance</li>
                <li>Value-based activity and response by the end user</li>
                <li>Effective and desired results for the System</li>
            </ul>
        </ul>


        <h6>What do we do?</h6>
        <h5>Traditionally, Finance was an area focused on hindsight or “look-back” analysis.  Finance is now shifting to 
    <asp:Label ID="Label1" runat="server" Text="forward-thinking " Font-Bold="true" Font-Italic="true"></asp:Label>
            analysis.  
    Our team is not only reporting on the activities of the past, but also taking advantage of opportunities to 
    <asp:Label ID="Label2" runat="server" Text="drive value " Font-Bold="true" Font-Italic="true"></asp:Label>
            for the organization.  We do this by improving the quality and availability of data, information, analytics and ultimately, business intelligence.
        </h5>
        <br />
        <h6>What is our goal?</h6>
        <h5>Our goal is to perfect our baseline – our starting point.  How do we do this?  By creating a core set of real-time, validated and connected data that drives strong, effective output.  This starting point becomes the foundation for our work in the future.  The end result helps guide and position the Northside Health System for growth.</h5>

        <h5></h5>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <br />
        <br />
        <br />

    </div>
    <div id="RightPanel" style="float: right; width: 59%">

        <asp:Table ID="Table3" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Image ID="Image1" runat="server" BackColor="#214B9A" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif"
                        Height="10px" Width="10px" />
                    <cc1:DropShadowExtender ID="DropShadowExtender2" runat="server"
                        Enabled="True" TargetControlID="Image1" Opacity="5" Radius="5" Rounded="False" Width="3"></cc1:DropShadowExtender>
                </asp:TableCell>
                <asp:TableCell>  <h6> &nbsp; Finance Division News</h6>   </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                </asp:TableCell>
                <asp:TableCell>
                    <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px;"
                        id="ulFinanceDivisionNews" title="Finance Division News">
                        <li>
                            <h4><a id="A1" href="http://nshdsweb/" runat="server" target="_blank" title="Follow this link to open in new tab/window." style="text-decoration: underline">Historical Decision Support Web Site</a>  </h4>
                            <h5>This web site will be available until January 1, 2014 </h5>
                            <br />
                        </li>
                        <%--               <li>
               <h4> <asp:HyperLink ID="hlTrendstar" runat="server" 
               NavigateUrl="~/Applications/Applications.aspx?ID=TSTAR" Font-Underline="true">Trendstar </asp:HyperLink> </h4>
               <h5>Northside Hospital is in the process of transitioning from Trendstar to MPA. <br />
               Updated information will be provided when available. </h5><br />
               </li>--%>
                        <%--  <li>
               <h4> <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Black"
               NavigateUrl="#" Font-Underline="false">SRC </asp:HyperLink> </h4>
               <h5>Northside Hospital is in the process of transitioning from SRC to 
                <asp:HyperLink ID="HyperLink5" runat="server" 
               NavigateUrl="~/Applications/Applications.aspx?ID=AXIOM" Font-Underline="true">Axiom </asp:HyperLink>. <br />
               Updated information will be provided when available. </h5><br />
               
               </li>--%>

                        <li>
                            <h4>
                                <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="Black"
                                    NavigateUrl="http://nsmvaxapp.northside.local/axiom/c1/Axiom.UI.Start.application?client=windows" Font-Underline="true">Axiom </asp:HyperLink>
                            </h4>
                            <h5>For all Operating budget and Capital budget needs.
                                <asp:HyperLink ID="HyperLink3" runat="server"
                                    NavigateUrl="~/Applications/Applications.aspx?ID=AXIOM" Font-Underline="true">Axiom </asp:HyperLink>.
                                <br />
                                Updated information will be provided when available. </h5>
                            <br />

                        </li>
                    </ul>



                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>


            <%--        <asp:TableRow >
              <asp:TableCell >  <asp:Image ID="Image5" runat="server" BackColor="#214B9A" BorderColor="Black" 
                  BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif" 
                  Height="10px" Width="10px" /> 
                  
                    <cc1:DropShadowExtender ID="DropShadowExtender5" runat="server" 
                  Enabled="True" TargetControlID="Image5" Opacity="5" Radius="5" Rounded="False" Width="3">
              </cc1:DropShadowExtender>
                   </asp:TableCell>
               <asp:TableCell>     <h6> &nbsp;Monthly Stats Report - ???</h6>   </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow>
              <asp:TableCell>
              </asp:TableCell>
              <asp:TableCell>
                           <h5>What is the stats report??? Who maintains???? who to Contact???  </h5>
                     
                <h5>  <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/UploadFiles/MonthlyStatsReport/Monthly Stat Report.pdf" Target="_blank" Font-Underline="True">Monthly Stats Report</asp:HyperLink></h5>

              </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell></asp:TableCell>
              </asp:TableRow>--%>

            <%--   <asp:TableRow >
              <asp:TableCell >  <asp:Image ID="Image2" runat="server" BackColor="#214B9A" BorderColor="Black" 
                  BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif" 
                  Height="10px" Width="10px" /> 
                  
                    <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" 
                  Enabled="True" TargetControlID="Image2" Opacity="5" Radius="5" Rounded="False" Width="3">
              </cc1:DropShadowExtender>
                   </asp:TableCell>
               <asp:TableCell>     <h6> &nbsp;Upcoming Events</h6>   </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell></asp:TableCell>
              </asp:TableRow>
           <asp:TableRow>
              <asp:TableCell>    
          <asp:Image ID="Image3" runat="server" BackColor="#FFA15C" BorderColor="Black" 
                  BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif" 
                  Height="10px" Width="10px" /> 
                  
                    <cc1:DropShadowExtender ID="DropShadowExtender3" runat="server" 
                  Enabled="True" TargetControlID="Image3" Opacity="5" Radius="5" Rounded="False" Width="3">
              </cc1:DropShadowExtender>
                  
                  
                   </asp:TableCell>
             <asp:TableCell>     <h6> &nbsp;FSI Calendar updates </h6>   </asp:TableCell>
              </asp:TableRow>--%>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Image ID="Image2" runat="server" BackColor="#214B9A" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif"
                        Height="10px" Width="10px" />

                    <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server"
                        Enabled="True" TargetControlID="Image2" Opacity="5" Radius="5" Rounded="False" Width="3"></cc1:DropShadowExtender>
                </asp:TableCell>
                <asp:TableCell> <h6> &nbsp;FSI - Northside Hospital's single most trusted source for data and analytics. </h6>   </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                </asp:TableCell>
                <asp:TableCell>
                    <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px;"
                        id="DataStores" title="FSI - Northside Hospital's single most trusted source for data and analytics. ">
                        <li>
                            <%--<h5>  <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/UploadFiles/MonthlyStatsReport/Monthly Stat Report.pdf" Target="_blank" Font-Underline="True">Monthly Stats Report</asp:HyperLink></h5>--%>
                            <h5><a id="A2" href="http://nshdsweb/General%20users/Monthly%20Reporting/Monthly%20Stat%20Report.pdf" runat="server" target="_blank" title="Monthly Stats Report" style="text-decoration: underline">Monthly Stats Report</a> </h5>
                            <h5>The monthly Statistical Report contains metrics of trended volumes by facility for the current and prior fiscal years, as well as patient days by nursing unit for the current month. This statistical report is typically available on the 10th of each month.
                                <br />
                                Contact FSI team for any questions. </h5>
                            <br />
                        </li>
                        <li>
                            <h5>
                                <asp:HyperLink ID="HLDataDictionary" runat="server" NavigateUrl="~/References/DataDictionary/DataDictionary.aspx" Target="_self" Font-Underline="True" Enabled="True">Data Dictionary</asp:HyperLink>
                                - The data dictionary is a metadata repository for the centralized repository of information that is currently available to the finance division. It contains all the avalible data elements and the associated database structures.  </h5>
                            <br />
                        </li>
                        <li>
                            <h5>
                                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://ssrs.northside.local/Reports/Pages/Folder.aspx" Target="_blank" Font-Underline="True">SSRS (ACCESS RESTRICTED)</asp:HyperLink>
                                - SQL Server Reporting Services. This is one of the business intelligence reporting tools currently utilized by the finance division to deliver analytical reports. </h5>
                            <br />

                        </li>
                        <%--  <li>
          
          </li>--%>
                    </ul>



                    <br />

                    <br />




                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>



            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>



            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>



        </asp:Table>






    </div>

</asp:Content>
