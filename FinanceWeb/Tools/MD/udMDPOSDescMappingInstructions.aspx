<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="udMDPOSDescMappingInstructions.aspx.vb" Inherits="FinanceWeb.udMDPOSDescMappingInstructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
      
    <cc1:TabContainer ID="MDPOSMappingTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel runat = "server" HeaderText = "udMD POS Description Mapping - Unmapped" ID = "tpMDPOSUnmapped" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    The MD POS Mapping tool is designed to allow the users to keep the DWH.MD.udMD_POSDesc_Mapping data table up to date.
    <br />
    <asp:Image runat = "server" ImageUrl="MDPOSImages/Image1.png" Width="600px" />    <br />    <br />
    Users will be notified by a scheduled email when there are new rows to update, and they should then come here to fill them in.
    <br />
    Both the email and the Unmapped tab identify new rows as ones where <br /> 
        
        <asp:Table runat="server">
            <asp:TableRow><asp:TableCell>1.</asp:TableCell><asp:TableCell>a)</asp:TableCell><asp:TableCell>the Entity is '99'</asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell>b)</asp:TableCell><asp:TableCell>the Cost Center is '999'</asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell>b)</asp:TableCell><asp:TableCell> the VolType is empty</asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell>2.</asp:TableCell><asp:TableCell ColumnSpan="2">the POS Desc is not empty.</asp:TableCell></asp:TableRow>
        </asp:Table>
    <br />
    The Mapping Tool Unmapped tab currently excludes the IDGroup "PMA" and any IDGroups beginning with "UCASH".

    <br />
    <br />
    Select a row by clicking on it; the row will change to a darker blue color, and its information will be displayed in the light blue box below the table.
    <br />
        <asp:Image runat = "server" ImageUrl="MDPOSImages/Image2.png" Width="700px" />
        <br /><br />
    Update the Entity, CostCenter and VolType, and hit "Update". <br />  You should get a popup letting you know whether or not your data was submitted succesfully.
    <br />
        <asp:Image runat = "server" ImageUrl="MDPOSImages/Image3.png" Width="700px" />
        </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
     <cc1:TabPanel runat = "server" HeaderText = "udMD POS Description Mapping - Mapped" Visible="true" ID = "tpMDPOSMapped">
     <ContentTemplate>
     This page of the MD POS Mapping tool is designed to allow users to view and update any rows from the DWH.MD.udMD_POSDesc_Mapping table.
    <br />
    <br />
         <asp:Image runat = "server" ImageUrl="MDPOSImages/Mapped1.png" Width="500px"/><br /><br /><br />
         Only thirty rows are shown on a page; to view the next page, scroll down and hit one of the numbered pages at the bottom of the data table.
         <br />
         <asp:Image runat = "server" ImageUrl="MDPOSImages/Mapped6.png" Width="600px"/>
         <br /><br />

    The user can filter data by ID Group, using the drop down list provided at the top.
                  <br />
         <asp:Image runat = "server" ImageUrl="MDPOSImages/Mapped2.png" Width="500px"/>
         <br /><br />        
         <asp:Image runat = "server" ImageUrl="MDPOSImages/Mapped3.png" Width="500px"/>
         <br /><br />
         <br />If the user knows the POS Description they are looking for, they may also use the second filter to limit by POS Description as well.
             <br />
         <asp:Image runat = "server" ImageUrl="MDPOSImages/Mapped4.png" Width="500px"/>
         <br /><br />
    Select a row by clicking on it; the row will change to a darker blue color, and its information will be displayed in the light blue box below the table.
                  <br />
         <asp:Image runat = "server" ImageUrl="MDPOSImages/Mapped5.png" Width="600px"/>
         <br /><br />
         Update the Entity, CostCenter and VolType, and hit "Update". <br />  You should get a popup letting you know whether or not your data was submitted succesfully.
    <br /><br />

    
     </ContentTemplate>
     </cc1:TabPanel>
            
     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
