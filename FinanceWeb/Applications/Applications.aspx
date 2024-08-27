<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Applications.aspx.vb" EnableEventValidation = "false" Inherits="FinanceWeb.Applications" %>
 
 
<%-- <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>

<asp:Content ID="Content3" runat="server" contentplaceholderid="Maincontent">

   <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divAXIOM" runat="server" >
    <h2>Axiom Enterprise Performance Management Software </h2>
    <br />
   Axiom EPM is an integrated application used to manage Northside Hospital’s operational and capital budgeting, strategic planning and multi-year forecasting, productivity management and analytics, and physician practice planning.  It contains tools for reporting, building dashboards and an array of analytics, including service line volume analysis.
    <br />
    <br />
    <asp:HyperLink ID="hlAxiom" runat="server" NavigateUrl="http://nsmvaxapp.northside.local/Axiom/help/default.htm" Target="_blank">Axiom Help Documentation</asp:HyperLink>
    <br /><br />
    <asp:HyperLink ID="hlAxiomCorportate" runat="server" NavigateUrl="http://www.axiomepm.com/" Target="_blank">Axiom Consulting Website (external site)</asp:HyperLink>
     <br />
     <br />
   
  </div> 
  
 <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divGDDS" runat="server">
    <h2>Georgia Discharged Data System (GDDS)</h2>
    <br />
 <h5>   The GDDS Data (Georgia Discharged Data System) is a compilation discharged patient data for hospitals across the state of Georgia.  Hospitals are required to submit inpatient and outpatient data to the state on a quarterly basis.  The data sets includes patient demographics, diagnostic codes, procedure codes and revenue codes.  This data is commonly referred to as the ‘Competitive Data’ and is used to compare Northside’s statistics and market share against other hospitals in the Atlanta Metro Statistical Area, as well as across the state.
 <br />
 <br />
 </h5>
     <div id="GDDSlable" style="text-decoration: underline; font-weight: bold"> System Documentation </div>
    <div style="font-size: smaller">Georgia Hosptial Association<br />
    Published February 2012 </div>
    <br />
   <a href="../pdfDocumentation/GDDS/2012 Notes to the Study.pdf" target="_blank">Notes to the Sudy for Georgia Discharge Database</a>
   <br />
   <div style="font-size: small"> Pages - 25</div>
   <br />
     <a href="../pdfDocumentation/GDDS/GDDS DATA SCHEMA.pdf" target="_blank">GDDS Data Schema</a>
     <div style="font-size:small"> Pages - 13</div>
     <br /> 
     <a href="../pdfDocumentation/GDDS/GDDS Patient Type Definitions.pdf" target="_blank">Types of Patient Records Collected</a>
     <div style="font-size:small"> Pages - 1</div>
     <br />
    

      <a href="../pdfDocumentation/GDDS/Georgia_UB04_Record_Layout.pdf" target="_blank">GA UB04 Record layout</a>
     <div style="font-size:small"> Pages - 9</div>
     <br />

       <a href="../pdfDocumentation/GDDS/Cardiac Cath Codes.pdf" target="_blank">Cardiac Cath Codes</a>
     <div style="font-size:small"> Pages - 4</div>
     <br />
</div> 

   <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divHBI" runat="server">
      <h2>Horizon Business Insight</h2>
    <br />
    <div id="HBIlabel" style="text-decoration: underline; font-weight: bold"> System Documentation </div>
    <div style="font-size: smaller">McKesson Corporation<br />
    Documentation is for Release 15.0<br />
    Published April 2010 </div>
    <br />

     <a href="../pdfDocumentation/HBIDoc/Table%20of%20Contents.pdf" target="_blank">Table of Contents</a>
     <br />
     <br />

   </div> 
       
      <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divHEFM" runat="server">
         <h2>Horizen Enterprise Fiscal Management (HEFM)</h2>
    <br />
      <ul style="list-style-type: circle; list-style-position: outside; font-size: small; margin-top: 0px; " runat="server" id="Ul1" title="HEFM">
    <li id="liGL" runat="server"><b>HEFM General Ledger (GL):</b> <br /> 
    HEFM General Ledger offers journal processing, subsystem integration and multi-entity processing as part of an integrated healthcare financial management solution with accounts payable and materials management.</li>
    <li id="liAP" runat="server"><b>HEFM Payables (AP):</b><br />
    HEFM Payables provides cash and vendor management, multi-entity processing, invoicing, bank reconciliation and positive payment processing via electronic data interchange.
    </li>
        <li id="liFA" runat="server"><b>HEFM Fixed Assets (FA):</b><br />
    HEFM Fixed Assets manages capital purchases, including tracking fixed asset locations and their components, automates depreciation, and allows collection of fixed assets data.
    </li>
        <li id="liProjects" runat="server"><b>HEFM Project Accounting (Projects):</b><br />
    HEFM Project Accounting tracks funds and projects, and manages project spending, funding requirements and capital assets.
    </li>
       
    </ul>
     
   <div id="HEFMlabel" style="text-decoration: underline; font-weight: bold"> System Documentation </div>
    <div style="font-size: smaller">McKesson Corporation<br />
    All User Guides are for Release 15.0.0. <br />
    Published May 2010 </div>
    <br />
    
   <a href="../pdfDocumentation/HEFMSignOnUsersGuide.pdf" target="_blank">Sign On User Guide</a>
   <br />

   <div style="font-size: small"> Pages - 34</div>
   <br />
     <a href="../pdfDocumentation/HEFMAssetsUsersGuide.pdf" target="_blank">Assets Users Guide</a>
     <div style="font-size:small"> Pages - 309</div>
     <br /> 
     <a href="../pdfDocumentation/HEFMControlUsersGuide.pdf" target="_blank">Control Guide</a>
     <div style="font-size:small"> Pages - 201</div>
     <br />
     <a href="../pdfDocumentation/HEFMLedgerUsersGuide.pdf" target="_blank">Ledger Users Guide</a>
     <div style="font-size:small"> Pages - 406</div>
     <br />
     <a href="../pdfDocumentation/HEFMPayablesUsersGuide.pdf" target="_blank">Payables User Guide</a>
     <div style="font-size:small"> Pages - 575</div>
     <br />
     <a href="../pdfDocumentation/HEFMProjectsUsersGuide.pdf" target="_blank">Projects User Guide p. 502</a>
     <div style="font-size:small"> Pages - 502</div>
     <br /> 
     <br />
   </div> 
      <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divHEMM" runat="server">
        <h2>Strategic Supply Sourcing</h2> 
    <h5> Formally known as Horizon Enterprise Materials Management (HEMM)</h5>
    <br />
    <h5>McKesson Supply Chain Management links the entire hospital supply chain into a single, integrated process. From requisitioning through invoice matching, contract compliance and rebate attainment, McKesson Supply Chain Management automates and streamlines all supply chain management functions.<br /><br /></h5>

     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl ="https://www.mckessonstrategicsupplysourcing.com/production/sign_in" Target="_blank">Strategic Supply Sourcing</asp:HyperLink>&nbsp;&nbsp; External link.
    <br /><br />
     Documentation currently unavailable. 
     <br />
     <br />

   </div> 
      <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divSTAR" runat="server">
         <h2>STAR</h2>
    <br />
    <%-- <input id="btnMyButton" onClick="RunSTAR('C:\Program Files\McKesson\STAR Navigator 17\HBOC32\MckBoot2.exe')" type="button" value="STAR" /><br />
     <a href="javascript:void" onclick="runCmd('c:\\Program Files\\McKesson\\STAR Navigator 17\\HBOC32\\MckBoot2.exe')">STAR</a>
     <br />
      <a href="javascript:void" onclick="runCmd('c:\\Windows\\system32\\cacls.exe')">STAR2</a><br />
          <a href="javascript:void" 
    onclick="runCmd('calc')">calc (just like that)</a><br />--%>
  
    <div id="Div1" style="text-decoration: underline; font-weight: bold"> System Documentation </div>
    <div style="font-size: smaller">McKesson Corporation<br />
    All User Guides are for Release 17.0. <br />
    Published October 2011 </div>
    <br />
    This is an interactive user manual. <br />

     <a href="../pdfDocumentation/StarDoc/STAR%202000%20Library%2018.0/Frontpg.pdf" target="_blank">User Guide</a>
     <br />
     <br />

   </div> 
      <div style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A;" id="divTSTAR" runat="server">
         <div style="color: #000000">
    <h2>Trendstar  </h2>
    <h5 style="color: #FF0000"> 
    (Please note this page is arraigned similarly to the previous website for ease of use.<br /> 
    Some links are disabled but reference updated links.)
    </h5>
    <br />
    
    <h5>Trendstar is a collection of clinical and financial modules that operate together as a Decision Support system.  Northside Hospital owns Trendstar modules consisting of Clinical Cost Accounting, Resource Utilization Analyst, Hospital Systems Library, and Contract Payment Advisor.  Applied Decision Products are Windows/PC based.  DsPathfinder, and Outcomes Advisor are at various stages of implementation.  The main source system for Trendstar is STAR Financials.    </h5>
    <br />
        <h5><asp:HyperLink ID="hlTstarflowchart" runat="server" Font-Underline ="true" NavigateUrl="http://nshdsweb/General%20users/TSTAR%20Process%20Flowchart_files/frame.htm" Target="_blank">Trendstar Flow Chart</asp:HyperLink> 
        &nbsp; Visual diagram of source systems that interface with Trendstar.</h5><br />
      <h5><asp:HyperLink ID="HyperLink12" runat="server" Font-Strikeout ="true" Font-Underline ="true">Trendstar Documentation</asp:HyperLink> &nbsp; Trendstar reference material created by McKesson.  
        <asp:Label ID="Label4" runat="server" Text="(See System Documentation below.) " ForeColor="Red"></asp:Label>
        </h5><br />


        <h5> <asp:HyperLink ID="hlTrendstarDataReference" runat="server" NavigateUrl="file://nshdsfile/files/DS/DS%20Resources/Trendstar%20Doc/Trendstar%20Data%20Item%20Reference.xls" Font-Underline ="true">Trendstar Data Referenece </asp:HyperLink>
          &nbsp; Listing of all available reporting data fields in Trendstar.  Not all fields are verified for accuracy and completeness.  <br />
          <asp:Label ID="Label2" runat="server" Text="(This data is being maintained in the " ForeColor="Red"></asp:Label><asp:HyperLink ID="HyperLink2" runat="server" Font-Underline ="true"  NavigateUrl="~/References/DataDictionary.aspx">Data Dictionary</asp:HyperLink>) </h5><br />

          <h5> <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="file://nshdsfile/files/DS/DS%20Resources/Trendstar%20Doc/Trendstar%20Data%20Dictionary.doc" Font-Underline ="true">Trendstar Data Dictionary </asp:HyperLink>
          &nbsp;  Definition of all fields in Trendstar Data Reference manual. <br />
          <asp:Label ID="Label3" runat="server" Text="(This data is being maintained in the " ForeColor="Red"></asp:Label><asp:HyperLink ID="HyperLink4" runat="server" Font-Underline ="true"  NavigateUrl="~/References/DataDictionary.aspx">Data Dictionary</asp:HyperLink>)  </h5><br />
          <br />
          <h5>Trendstar Release Notes: </h5> 
       <ul style="list-style-type: circle; list-style-position: outside; font-size: medium; margin-top: 0px; list-style-image: none;" 
            id="TrendstarReleaseNotes" title="Trend Star Release Notes:">
          <li><h5> <asp:HyperLink ID="HyperLink5" Font-Underline ="true"  runat="server" NavigateUrl="file://nshdsfile/files/DS/DS%20Resources/Trendstar%20Doc/TStar_ReleaseNotes_v2010.1.4.pdf" Target="_blank" >Version 2010.1.4 - February 2011</asp:HyperLink> </h5></li>
          <li> <h5><asp:HyperLink ID="HyperLink6" Font-Underline ="true" runat="server" NavigateUrl="file://nshdsfile/files/DS/DS%20Resources/Trendstar%20Doc/Tstar_ReleaseNotes_v2011.1.0.pdf" Target="_blank">Version 2011.1.0 - April 2011</asp:HyperLink> </h5></li>
          <li> <h5><asp:HyperLink ID="HyperLink7" Font-Underline ="True" runat="server" 
                  NavigateUrl="file://nshdsfile/files/DS/DS%20Resources/Trendstar%20Doc/Tstar_ReleaseNotes_v2011.1.1.pdf" Target="_blank">Version 2011.1.1 - September 2011</asp:HyperLink> </h5></li>
          <li><h5> <asp:HyperLink ID="HyperLink8" Font-Underline ="True" runat="server" 
                  NavigateUrl="file://nshdsfile/Files/DS/DS%20Resources/Trendstar%20Doc/Tstar_ReleaseNotes_v2011.1.2.pdf" Target="_blank"> Version 2011.1.2 - December 2011</asp:HyperLink> </h5></li>
          <li> <h5><asp:HyperLink ID="HyperLink9" Font-Underline ="True" runat="server" 
                  NavigateUrl="file://nshdsfile/Files/DS/DS%20Resources/Trendstar%20Doc/Tstar_ReleaseNotes_v2012.1.0.pdf" Target="_blank">Version 2012.1.0 - April 2012</asp:HyperLink></h5> </li>
          </ul>
 <h5><b>Clinical Cost Accounting and Resource Utilization Analyst </b></h5>  
<h5> CCA/RUA combines clinical and financial data for discharged patients.  CCA's reporting and modeling options are used to produce standard and customized reports.  You can produce a wide array of reports as well as LOS, marketing, demographic analyses, and reports detailed by ICD-9 diagnosis and procedure codes. </h5>
          <br />
          <h5>   <asp:HyperLink ID="HyperLink10" Font-Underline ="true"  runat="server" Font-Strikeout="true">CCA Documentation </asp:HyperLink> 
          &nbsp; - CCA Reference Manual created by McKesson
          <asp:Label ID="Label7" runat="server" Text=" (see below - Clinical Cost Accounting link ) " ForeColor="Red"></asp:Label>
                    </h5>
<h5> <asp:HyperLink ID="HyperLin9k" Font-Underline ="true"  runat="server" NavigateUrl="http://nshdsweb/General%20Users/CCA%20Manual.pdf">CCA Reference Manual </asp:HyperLink> &nbsp; Documentation created by Decision Support. </h5> <br /> 

<h5><b>Hospital Systems Library</b></h5> 
<h5>HSL is a financial reporting system for Northside Hospital.  HSL contains revenue expenses, man hours and other important statistics at the departmental level.  It contains the Hospital's fixed budget and allows for the production of a flexible budget.  This module is used to generate monthly budget reports. </h5><br />

  <h5>   <asp:HyperLink ID="HyperLink13" Font-Underline ="true"  runat="server" Font-Strikeout="true">HSL Documentation </asp:HyperLink> 
          &nbsp; HSL reference manual created by McKesson.  
           <asp:Label ID="Label5" runat="server" Text="See Hospital Systems Library below. " ForeColor="Red"></asp:Label>
          </h5><br />

<h5><b>Management Cost Accounting</b></h5> 
<h5>MCA is used to collect volume information and allocate cost to individual charge codes within each department.  MCA is the link between the financial data in HSL and the clinical/demographic data in CCA/RUA.  MCA is also used by Management Services to develop productivity. </h5><br />

  <h5>   <asp:HyperLink ID="HyperLink11" Font-Underline ="true" Font-Strikeout="true"  runat="server">MCA Documentation</asp:HyperLink>&nbsp;  -  MCA Reference Manual created by McKesson    
  <asp:Label ID="Label6" runat="server" Text="(see below - Management Cost Accounting link )" ForeColor="Red"></asp:Label>
  </h5><br />
        
        <h5><b>STAR</b></h5> 
<h5>STAR is the core  hospital-based transaction system that includes applications for patient care and financials.  In order to use this documentation you must select the library appropriate for your search.  The Patient Accounting Library contains documentation on the STAR to Trendstar Interface.  </h5><br />


 <h5>   <asp:HyperLink ID="HyperLink122" Font-Underline ="true"  runat="server" NavigateUrl="../pdfDocumentation/StarDoc/STAR 2000 Library 18.0/Frontpg.pdf" Target="_blank">STAR</asp:HyperLink> &nbsp; -  STAR Reference Manual created by McKesson (see below - Management Cost Accounting link)   </h5>
 
 
 
 
 
          <br />
   <h5><asp:Label ID="Label1" Font-Bold="true" Font-Underline ="true" 
   runat="server" Text=" System Documentation"></asp:Label><br />
   Trendstar reference material created by McKesson Corporation<br />
 
    All User Guides are for Version 1-2 <br />
    Published December 2012  </h5>
    <br />
    

     <a href="../pdfDocumentation/TSTARClinicalCostAccounting.pdf" target="_blank">Clinical Cost Accounting </a>
   <br />
   <div style="font-size: small"> Pages - 2,190</div>
   <br />
     <a href="../pdfDocumentation/TSTARContractPaymentAdvisor.pdf" target="_blank">Contract Payment Advisor</a>
     <br />
   <div style="font-size: small"> Pages - 988</div>
    <br />
     <a href="../pdfDocumentation/TSTARGeneralInformation.pdf" target="_blank">General Information</a>
      <br />
   <div style="font-size: small"> Pages - 236</div>
    <br />
     <a href="../pdfDocumentation/TSTARHospitalSystemsLibrary.pdf" target ="_blank">Hospital Systems Library</a>
      <br />
   <div style="font-size: small"> Pages - 1,130</div>
    <br />
     <a href="../pdfDocumentation/TSTARManagementCostAccounting.pdf" target ="_blank">Management Cost Accounting</a>
       <br />
   <div style="font-size: small"> Pages - 656</div>
    <br />
     <a href="../pdfDocumentation/TSTARResourceUtilizationAnalyst.pdf" target ="_blank">Resource Utilization Analyst</a>
       <br />
   <div style="font-size: small"> Pages - 72</div>
    <br />
     <a href="../pdfDocumentation/TSTARSystemManagersManual.pdf" target ="_blank">System Managers Manual</a>
   <br />
   <div style="font-size: small"> Pages - 314</div>
    <br />
 
   <br />
    <br />

 

</div> 

   </div> 


<%--<div id="Default" style="color: #000000; border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #214B9A; visibility: hidden;" runat="server">

    <br /><br /> 

    
     Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Mauris massa. Vestibulum lacinia arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. 
<br /><br />
Curabitur sodales ligula in libero. Sed dignissim lacinia nunc. Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. 
<br />
<br />
Nulla metus metus, ullamcorper vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. Proin quam. Etiam ultrices. 
<br />
<br />
Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi lacinia molestie dui. Praesent blandit dolor. Sed non quam. In vel mi sit amet augue congue elementum. Morbi in ipsum sit amet pede facilisis laoreet. Donec lacus nunc, viverra nec, blandit vel, egestas et, augue. Vestibulum tincidunt malesuada tellus. Ut ultrices ultrices enim. Curabitur sit amet mauris. Morbi in dui quis est pulvinar ullamcorper. 
<br />
<br />
Nulla facilisi. Integer lacinia sollicitudin massa. Cras metus. Sed aliquet risus a tortor. Integer id quam. Morbi mi. Quisque nisl felis, venenatis tristique, dignissim in, ultrices sit amet, augue. Proin sodales libero eget ante. Nulla quam. Aenean laoreet. Vestibulum nisi lectus, commodo ac, facilisis ac, ultricies eu, pede. Ut orci risus, accumsan porttitor, cursus quis, aliquet eget, justo. Sed pretium blandit orci. Ut eu diam at pede suscipit sodales. 
</div>--%>
</asp:Content>


