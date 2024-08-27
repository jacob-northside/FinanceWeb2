<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ColumnLookup.aspx.vb" Inherits="FinanceWeb.ColumnLookup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Column Lookup</title>

</head>
<body>


    <asp:Panel runat="server" Visible ="true" ID="FullPanel">  
       <h3>Available Columns</h3>
    <asp:Label runat="server" id="Lookup_Columns_AlertBox" visible ="false" text="ALERT"/>

       <form id="form1" runat="server">

        <asp:Table ID="SearchColumns" Width="99%" Visible="true" BackColor = "#2b74bb" CellSpacing = "2"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
        <asp:TableCell Width = "5%">Search:</asp:TableCell>

    <asp:TableCell HorizontalAlign = "Left" > <asp:TextBox runat = "server" ID= "SearchColumnName" Width = "100%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell>
        <asp:Table runat="server">
            <asp:TableRow>
<%--                <asp:TableCell><asp:CheckBox ID="Columns_WildcardSrch" runat="server" AutoPostBack="true" OnCheckedChanged="Columns_WildcardChecked" Text="Wildcard" Checked="true" Font-Size="Small" /></asp:TableCell>
                <asp:TableCell><asp:CheckBox ID="Columns_ExactSrch" runat="server" AutoPostBack="true" OnCheckedChanged="Columns_ExactChecked" Text="Exact" Checked="false" Font-Size="Small" /></asp:TableCell>--%>
                <asp:TableCell VerticalAlign="Top"><asp:RadioButtonList CssClass="radioButtonList" runat="server" ID="Columns_Srch_RadioBtn" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="Wildcard" Selected="True" Value="Wildcard"/>
                                   <asp:ListItem Text="Exact" Selected="False" Value="Exact"/>
                               </asp:RadioButtonList></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:TableCell>
    
    </asp:TableRow>  

    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White">
    <asp:TableHeaderCell Height = "20px" Width="10%"></asp:TableHeaderCell>

    <asp:TableHeaderCell Width="50%">Column Name</asp:TableHeaderCell>
    
    </asp:TableHeaderRow>
    </asp:Table>






        <asp:GridView ID="Lookup_Columns" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="True" BackColor="White" bordercolor="Black" borderStyle="Solid" BorderWidth="1px" CellPadding="5" CellSpacing="1" font-Size="Small" ForeColor="Black" HeaderStyle-BackColor="#214B9A" HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" PageSize="25" ShowHeaderWhenEmpty="True" visible="true" Width="99%">
                  <alternatingrowstyle backcolor="#FFE885" />
            <Columns>
            </Columns>      
                       <pagersettings position="TopAndBottom" />
        <rowstyle horizontalalign="Left" verticalalign="Top" />
        <headerstyle backcolor="#214B9A" font-bold="True" forecolor="#F6FCFC" />
        <footerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
        <pagerstyle backcolor="#F6FCFC" forecolor="#000000" horizontalalign="left" />
        <rowstyle backcolor="#ffffff" horizontalalign="Left" verticalalign="Top" />
        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
        <sortedascendingcellstyle backcolor="#F5F7FB" />
        <sortedascendingheaderstyle backcolor="#6D95E1" />
        <sorteddescendingcellstyle backcolor="#E9EBEF" />
        <sorteddescendingheaderstyle backcolor="#4870BE" />             
        </asp:GridView>
           </form>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PRDconn %>" SelectCommand="Select * from DWH.DOC.NZ_FDColumnData" />--%>
        </asp:Panel>

    </body>
    </html>
