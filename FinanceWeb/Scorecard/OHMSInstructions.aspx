<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OHMSInstructions.aspx.vb" Inherits="FinanceWeb.OHMSInstructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
    

    <cc1:TabContainer ID="tcOHMSInstructionsTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel runat = "server" HeaderText = "OHMS Main Page" ID = "tpOHMSMainPage" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"  Height = "800px" ScrollBars = "auto" > 
    <br />
    <asp:Image runat = "server" ImageUrl="Images/OHMSMainPageInstructions.png" Width="805px" />
    <br />
     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
     </cc1:TabContainer>
    </div>
    </form>
</body>
</html>
