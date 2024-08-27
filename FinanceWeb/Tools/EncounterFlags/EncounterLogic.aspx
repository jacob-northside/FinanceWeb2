<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EncounterLogic.aspx.vb" Inherits="FinanceWeb.EncounterLogic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



   

<%-- <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      
</asp:Content> --%>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
<%--  <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"  >
        <scripts>
              <asp:ScriptReference Path="~/Scripts/WebForms/MSAjax/MicrosoftAjax.js" />
        </scripts>
    </asp:ScriptManagerProxy>--%> 

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

      <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
        <cc1:TabContainer ID="TCFlagLogic" runat="server" ActiveTabIndex = "0" UseVerticalStripPlacement = "False" Width="1150px">
        <cc1:TabPanel runat="server" HeaderText="MPA Flag Logic" ID="tpMPAFlagViewer" >

<ContentTemplate> 
           
    <asp:UpdatePanel runat="server" ID= "udpMain">
        <ContentTemplate>
               

  <asp:Panel ID="Panel1" runat="server">  
           <%--       <asp:SqlDataSource ID="dsMPAFlags" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="SELECT Flag FROM MPA.dimEncounterFlags WHERE (Flag IS NOT NULL) and Active = 1  UNION SELECT CategoryA AS Flag FROM MPA.dimEncounterFlags 
                    AS dimEncounterFlags_2 WHERE (CategoryA IS NOT NULL) and Active = 1  UNION SELECT CategoryB AS Flag FROM MPA.dimEncounterFlags AS 
                    dimEncounterFlags_1 WHERE (CategoryB IS NOT NULL) and Active = 1  "></asp:SqlDataSource>--%>

<br /><asp:DropDownList ID="ddlFlagViewer" runat="server" ></asp:DropDownList>


                
                 <asp:LinkButton ID="lbViewFlagRules" runat="server">View Flag Rules</asp:LinkButton>

     <br /> <br />
                
                <asp:Label ID="lblFlagRules" runat="server" Text=" "></asp:Label>


          <asp:Label ID="Label2" runat="server" Text="Associated Flags:  " Font-Bold="True"></asp:Label>
          <asp:Label ID="lblAssociatedFlags" runat="server" Text=" "></asp:Label>
          <br /> <br /> 
        <asp:Panel ID="pnlSimpleFlags" runat="server" >
          <label>Simple Rules directly against single tables </label>
   
        <asp:GridView ID="gvFlagRules" runat="server"   >
      
        </asp:GridView><br />
      <label>* Rule numbers are simply an order selection of the rules from the database. This rules engine does not apply hierarchical logic to building the flags. </label>
        </asp:Panel>
     <asp:Panel ID="pnlComplexFlags" runat ="server"> 
             <label>Complex Rules against subqueries </label>
         <asp:GridView ID="gvComplexRules" runat="server"> 
         </asp:GridView><br />
         <label>* Rule numbers are simply an order selection of the rules from the database. This rules engine does not apply hierarchical logic to building the flags.</label>


     </asp:Panel>
        
          <asp:Repeater ID="cFlagRules" runat="server">
<HeaderTemplate>
 
                       <br /> 
      
                <table border="1" width="100%">
                <tr> 
                    <th>Rule #*</th>
                    <th>Flag Name</th>
                    <th>Facility Specific</th>
                    <th>Table Location</th>
                    <th>Field</th>
                    <th>Operator</th>
                    <th>Value</th>
                </tr>
            
</HeaderTemplate>
<ItemTemplate>
                 <tr> 
                    <td>Rule #<%#Container.DataItem("ID")%></td>
                     <td>Flag Name<%#Container.DataItem("Flag")%></td>
                <td><%#Container.DataItem("Facility")%> </td>
                        <td><%#Container.DataItem("Table_Name")%> </td>
                    
                    <td><%#Container.DataItem("Field")%> </td>
                    <td><%#Container.DataItem("Operator")%> </td>
                    <td><%#Container.DataItem("Value")%> </td>
                </tr>

            
</ItemTemplate>
<SeparatorTemplate>
 

            
</SeparatorTemplate>
            <FooterTemplate >
 
                 </table>  
                <label>* Rule numbers are simply an order selection of the rules from the database. This rules engine does not apply hierarchical logic to building the flags. </label>

            
</FooterTemplate>
</asp:Repeater>

 


        
                </asp:Panel>

 
        </ContentTemplate>
        </asp:UpdatePanel>
           
</ContentTemplate>
        
</cc1:TabPanel>
       <%--     <cc1:TabPanel runat="server" HeaderText="Update MPA Flag Logic" ID="tpMPAFlagUpdate" >

                <ContentTemplate>

           
    <asp:UpdatePanel runat="server" ID= "UpdatePanel1">
        <ContentTemplate>

            <asp:DropDownList runat="server" ID="ddlUpdateFlag"  AppendDataBoundItems="true" AutoPostBack="true" >
                <asp:ListItem Text="New Flag" Value="--XXNEWFLAGSELECTEDXX--"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtUpdateFlagName" AutoPostBack="true"></asp:TextBox>
            <br />

            <asp:Panel ID="pnlNewSimple" runat="server">
            Simple Rules
                
            <asp:Table runat="server">
                <asp:TableHeaderRow><asp:TableCell>Add new Rule</asp:TableCell> </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>Facility</asp:TableCell>
                    <asp:TableCell>Table Location</asp:TableCell>
                    <asp:TableCell>Field</asp:TableCell> 
                    <asp:TableCell>Operator</asp:TableCell>
                    <asp:TableCell>Value</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:DropDownList runat="server" ID="ddlSimpleFac">
                        <asp:ListItem Text="All" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Atlanta" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Cherokee" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Forsyth" Value="F"></asp:ListItem>
                                   </asp:DropDownList></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddlSimpleTables" ></asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Textbox runat="server" ID="txtSimpleField" ></asp:Textbox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddlSimpleOperator">
                            <asp:ListItem Text="In" Value="IN"></asp:ListItem>
                            <asp:ListItem Text="Not In" Value="NOT IN"></asp:ListItem>
                            <asp:ListItem Text="Between" Value ="BETWEEN"></asp:ListItem>
                            <asp:ListItem Text="Not Between" Value="NOT BETWEEN"></asp:ListItem>
                            <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtSimpleValue"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="btnSimpleValidate" Text="Validate" BorderStyle="Outset" BorderWidth="2px"  Font-Size="Small" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            </asp:Panel>

                
            <asp:Panel ID="pnlNewComplex" runat="server">
                Complex Rules
            <asp:Table runat="server">
                <asp:TableHeaderRow><asp:TableCell>Add new Rule</asp:TableCell> </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>Facility</asp:TableCell>
                    <asp:TableCell>Table Location</asp:TableCell>
                    <asp:TableCell>Field</asp:TableCell> 
                    <asp:TableCell>Operator</asp:TableCell>
                    <asp:TableCell>Value</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:DropDownList runat="server" ID="ddlComplexFac">
                        <asp:ListItem Text="All" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Atlanta" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Cherokee" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Forsyth" Value="F"></asp:ListItem>
                                   </asp:DropDownList></asp:TableCell>
                    <asp:TableCell>
                        <asp:Textbox runat="server" ID="txtComplexTable" ></asp:Textbox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Textbox runat="server" ID="txtComplexField" ></asp:Textbox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddlComplexOperator">
                            <asp:ListItem Text="In" Value="IN"></asp:ListItem>
                            <asp:ListItem Text="Not In" Value="NOT IN"></asp:ListItem>
                            <asp:ListItem Text="Between" Value ="BETWEEN"></asp:ListItem>
                            <asp:ListItem Text="Not Between" Value="NOT BETWEEN"></asp:ListItem>
                            <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtComplexValue" ></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                         <asp:Button runat="server" ID="btnComplexValidate" Text="Validate" BorderStyle="Outset" BorderWidth="2px"  Font-Size="Small" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            </asp:Panel>

            <asp:Label ID="FakeButton" runat = "server" />
         <asp:Panel ID="hiddenpanel" runat="server" Width="233px" BackColor="#6da9e3" >
                <asp:Table runat ="server" Width ="100%" Height ="100%">
                    <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                    <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                        <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                    <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                         <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
                        <asp:Button ID="mpeYesButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Visible="false" Text="Yes"/>
                        <asp:Button ID="mpeNoButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Visible="false" Text="No"/>
                         </asp:TableCell></asp:TableRow>        
                    <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                 </asp:Table>
             </asp:Panel>
   
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="hiddenpanel"
                DropShadow="true"></cc1:ModalPopupExtender>

            </ContentTemplate>
        </asp:UpdatePanel>
        </ContentTemplate>

            </cc1:TabPanel>--%>
    </cc1:TabContainer>  

</asp:Content>