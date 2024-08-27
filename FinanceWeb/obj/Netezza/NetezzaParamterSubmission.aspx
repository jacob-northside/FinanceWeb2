<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NetezzaParamterSubmission.aspx.vb" Inherits="FinanceWeb.NetezzaParamterSubmission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<head runat="server">
    <title></title>
</head>--%>
<body style="background-color:#d5eaff; width:100%; height:100%; margin:0; padding:0;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:Panel runat="server" Width="100%" Height="100%" BackColor="#d5eaff">
            <asp:Table runat="server">
                <asp:TableHeaderRow>
                    <asp:TableCell>
                        Select Schema:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddlSchemaSelect" AppendDataBoundItems="false" AutoPostBack ="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Width="5px">
                    </asp:TableCell>
                    <asp:TableCell>
                        Select Table:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddlTableSelect" AppendDataBoundItems="false" AutoPostBack ="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Width="5px">
                    </asp:TableCell>
                    <asp:TableCell>
                        Select Column:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList runat="server" ID="ddlColumnSelect" AppendDataBoundItems="false" AutoPostBack ="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Width="5px">
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button runat="server" ID="btnUpdateSelectedColumn" Text="Update" />
                    </asp:TableCell>
                </asp:TableHeaderRow>
            </asp:Table>



        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
