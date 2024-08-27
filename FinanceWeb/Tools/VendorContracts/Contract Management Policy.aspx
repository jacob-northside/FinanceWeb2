<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Contract Management Policy.aspx.vb" Inherits="FinanceWeb.Contract_Management_Policy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LESCOR CM Policy</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>

            <cc1:TabContainer ID="tcPaymentTransferFAX" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1120px">
                <cc1:TabPanel runat="server" HeaderText="Contract Management Policy" ID="tpPTFAQ">
                    <ContentTemplate>

                        <asp:Panel runat="server" Width="100%">
                            <iframe src="Images/37101-Contract Management Policy.pdf"
                                style="width: 1100px; height: 800px" frameborder="0"></iframe>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="LESCOR Guide" ID="tpGuide">
                    <ContentTemplate>
                        Please note that this guide was designed with Gwinnett users in mind.<br /> Atlanta, Cherokee, and Forsyth users can connect directly to the intranet, making page 4 inapplicable.
                        <br /><br />
                        <asp:Panel runat="server" Width="100%">
                            <iframe src="Images/LESCOR Presentation.pdf"
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
