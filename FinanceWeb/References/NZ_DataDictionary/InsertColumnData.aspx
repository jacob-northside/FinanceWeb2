<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InsertColumnData.aspx.vb" Inherits="FinanceWeb.InsertColumnData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insert New Column to DWH.DOC.NZ_FDColumnData</title>
</head>
<body>
    <asp:Panel runat="server" Visible="true" ID="FullPanel">
        <h3>Insert New Column</h3>
        <asp:Label runat="server" ID="Insert_Columns_AlertBox" Visible="false" Text="ALERT" />

        <form id="form1" runat="server">
            <asp:Table ID="InsertColumns" Width="99%" Visible="false" BackColor="#2b74bb" CellSpacing="2" runat="server" BorderWidth="1px" BorderColor="#003060" ForeColor="White">
                <asp:TableRow>

                    <asp:TableCell HorizontalAlign="Left">
                        <asp:TextBox runat="server" ID="InsertColumnName" Width="99%" AutoPostBack="False"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" ID="InsertColumnDesc" Width="99%" AutoPostBack="False"></asp:TextBox>
                    </asp:TableCell>
                <asp:TableCell Width="5%"><asp:Button runat="server" ID="InsertColData_BTN" text="Insert" Width="100%" Height="100%" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableHeaderRow BackColor="#4A8fd2" ForeColor="White">
                    <asp:TableHeaderCell Width="25%">Column Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="50%">Column Description</asp:TableHeaderCell>
                    <asp:TableHeaderCell Height="20px" Width="10%"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </form>
    </asp:Panel>
</body>
</html>
