<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FDA Mammo Image Review FAQ.aspx.vb" Inherits="FinanceWeb.FDA_Mammo_Image_Review_FAQ" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FDA Mammography Image Review FAQ</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>

            <cc1:TabContainer ID="tcFDAMammoFAX" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False">
                <cc1:TabPanel runat="server" HeaderText="FDA Mammo Image Review Instructions" ID="tpLESCORFAQ">
                    <ContentTemplate>

                        <asp:Panel runat="server" Width="100%">
                         
                            <iframe src="Images/FDA%20Mammo%20Image%20Review%20Instructions%202.pdf"
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
