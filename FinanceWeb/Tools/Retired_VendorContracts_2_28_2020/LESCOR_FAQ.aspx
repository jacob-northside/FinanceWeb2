<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LESCOR_FAQ.aspx.vb" Inherits="FinanceWeb.LESCOR_FAQ" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LESCOR FAQ</title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
      
    <cc1:TabContainer ID="LESCORTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel runat = "server" HeaderText = "LESCOR FAQ" ID = "tpLESCORFAQ" >
                    <ContentTemplate>      

    <asp:Panel runat = "server" Width = "100%"> 
        
            <iframe src="Images/FAQ%20for%20LESCOR.pdf"
  style="width:800px; height:900px " frameborder="0"></iframe>
    </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>


     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
