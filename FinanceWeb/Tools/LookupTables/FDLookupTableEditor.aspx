<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FDLookupTableEditor.aspx.vb" Inherits="FinanceWeb.FDLookupTableEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
  
  <div id="LeftPnl"  style="float: left; width: 18%; height: 100%;" >
  <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left"  >
      <asp:Label ID="Label1" runat="server" 
          Text="Available BIDS Data Warehouse Lookup Tables*"></asp:Label><br />
           
  <asp:DropDownList ID="ddlLookUps" runat="server">
    </asp:DropDownList>
      <asp:Button ID="btnLoadData" runat="server" Text="Load Data" />
     
      <br />
      <br />
    <h5>  *To edit data you must log in as a valid current Northside employee and BIDS 
        approved modifier.
    </h5>
     
    </asp:Panel></div>
    
    <div id="RightPnl" style="float: left; width: 80%;">
    <asp:Panel ID="Panel2" runat="server" >
        <asp:Label ID="lblGVLookup" runat="server" Text=""></asp:Label><br />

         <asp:Button ID="btnExportToExcel" runat="server" Text="Export" 
            Visible = "false" Font-Size="X-Small"/>

        <asp:GridView ID="gvDWHLookups" runat="server" AllowPaging="false" 
            AllowSorting="false" bordercolor="Black" 
        borderStyle="solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="#000000" GridLines="Both"  BackColor="#ffffff" BorderWidth="1px" 
           Width="100%" AutoGenerateEditButton="True">
           <AlternatingRowStyle BackColor="#FFE885" /> 

 <PagerSettings Position="TopAndBottom" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

    </asp:Panel></div>

</asp:Content>
