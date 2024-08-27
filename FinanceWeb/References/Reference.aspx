<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Reference.aspx.vb" Inherits="FinanceWeb.Reference" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
 
<asp:Content ID="Content2" runat="server" contentplaceholderid="MainContent">
    <div id="LeftPanel"   
              style="float: left; width: 38%; border-right-style: dotted; border-right-width: thin; border-right-color: #214B9A;">
    
             <asp:Table ID="Table1" runat="server">
              <asp:TableRow >
              <asp:TableCell  Width="10px">  <asp:Image ID="Image1" runat="server" BackColor="#FFA15C" BorderColor="Black" 
                  BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/SqCornflowerBlue.gif" 
                  Height="10px" Width="10px" /> 
                  
                    <cc1:DropShadowExtender ID="DropShadowExtender1" runat="server" 
                  Enabled="True" TargetControlID="Image1" Opacity="5" Radius="5" Rounded="False" Width="3">
              </cc1:DropShadowExtender>
                   </asp:TableCell>
              <asp:TableCell>     <h6> <b> &nbsp;Reference</b></h6>   </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow>
              <asp:TableCell></asp:TableCell>
              <asp:TableCell></asp:TableCell>
              </asp:TableRow>
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell><h4>
                  <asp:HyperLink ID="HyperLink3" runat="server" Font-Underline="true" NavigateUrl="~/References/BoardBooks.aspx">Board Books</asp:HyperLink>
              </h4>
                 <h5> Portal to view the Quarterly Financial books. Access to these reports is restricted by end user Northside network access.  </h5><br />
                  </asp:TableCell>
              </asp:TableRow>
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell>
               <asp:Panel ID="pnlDataDictionary" runat="server" Visible="false"> 

              <h4> 
                   <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="true" NavigateUrl="~/References/DataDictionary/DataDictionary.aspx">Data Dictionary</asp:HyperLink>
              </h4>                            
              <h5>Tool to search documentation  on databases, schemas, tables, and columns. Populated by the FSI team.
               <asp:HyperLink ID="HyperLink9" runat="server" Font-Underline="true" NavigateUrl="~/pdfDocumentation/DataDictionary.pdf" Target="_blank">Manual </asp:HyperLink>
               </h5><br />
                              </asp:Panel>
              </asp:TableCell>
              </asp:TableRow>
                <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell><h4>
              <asp:HyperLink ID="HyperLink6" runat="server" Font-Underline="true" NavigateUrl="~/References/FDLookupTables.aspx"   >DWH Lookups</asp:HyperLink>
              </h4>
                 <h5> Data warehouse lookup tables. This is a tool to directly search all the support tables in the data warehouse. </h5><br /> 
                  </asp:TableCell>
              </asp:TableRow>

   <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell><h4>
              <asp:HyperLink ID="HyperLink5" runat="server" Font-Underline="true" NavigateUrl="~/References/EducationMaterial.aspx"   >Education Materials</asp:HyperLink>
              </h4>
                 <h5> Educational presentions, training aids, and other miscellaneous materials that my be useful. </h5><br /> 
                  </asp:TableCell>
              </asp:TableRow>            

                   <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell><h4>
              <asp:HyperLink ID="HyperLink7" runat="server" Font-Underline="true" NavigateUrl="~/References/NorthsideForms.aspx"   >Northside Forms</asp:HyperLink>
              </h4>
                 <h5> Various Northside Hospital financial request forms. <br />
                 For Northside Hospital Employee Forms see  <asp:HyperLink
                      ID="HyperLink8"  NavigateUrl ="http://nsmweb/HumanResources/Forms/Default.aspx" Target ="_blank" runat="server">Human Resources Forms</asp:HyperLink></h5><br /> 
                  </asp:TableCell>
              </asp:TableRow>

           
              <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell><h4> 
              <asp:Panel ID="pnlDataDictionaryEditor" runat="server" Visible="false"> 

                  <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="true" NavigateUrl="~/References/DataDictionary/DictionaryEditor.aspx">Search Data Dictionary</asp:HyperLink>
              </h4>
                 <h5>  Tool to update existing data warehouse definitions. Searchable by the public but only editable by the FSI team.
                    <asp:HyperLink ID="HyperLink10" runat="server" Font-Underline="true" NavigateUrl="~/pdfDocumentation/DataDictionary.pdf" Target="_blank">Manual </asp:HyperLink>
                 </h5><br />
                 </asp:Panel>
                  </asp:TableCell>
              </asp:TableRow>               
       
                 
                  <asp:TableRow >
              <asp:TableCell></asp:TableCell>
              <asp:TableCell><h4>
                  <asp:HyperLink ID="HyperLink4" runat="server" Font-Underline="true" NavigateUrl="http://ssrs.northside.local/Reports/Pages/Folder.aspx" Target ="_blank" >SSRS Reports</asp:HyperLink>
              </h4>
                 <h5> SQL Server Reporting Services - Reporting tools developed by the finance division to support Northside business activites.    </h5><br />
                  </asp:TableCell>
              </asp:TableRow>


            

              </asp:Table>
              <br />

 

     
  
</div> 
     <div id="RightPanel" style="float: right; width: 60%">
     </div>


</asp:Content>





