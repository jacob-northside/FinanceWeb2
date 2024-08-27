<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Activity_Report_Instructions.aspx.vb" Inherits="FinanceWeb.Activity_Report_Instructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR Activity Report Instructions</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>

            <cc1:TabContainer ID="tcActivityReportFAX" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False">
                <cc1:TabPanel runat="server" HeaderText="AR Activity Report Instructions" ID="tpARFAQ">
                    <ContentTemplate>

                        <asp:Panel runat="server" Width="100%">                            
                            <iframe src="Images/AR%20Daily%20Activity%20Report%20Procedure.pdf"
                                style="width: 1100px; height: 800px" frameborder="0"></iframe>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>


            </cc1:TabContainer>
            <br />
        </div>
    </form>
</body>
</html>
