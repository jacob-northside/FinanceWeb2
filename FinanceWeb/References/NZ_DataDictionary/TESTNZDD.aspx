<%@ Page Title="NZ Data Dictionary" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TESTNZDD.aspx.vb" Inherits="FinanceWeb.NZ_DataDictionary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>
    
</div>

      <div id="LeftPnl"  style="float: left; width: 18%; height: 100%;" >
  <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Height="45px" Width="571px"  >
      <asp:Label ID="Label1" runat="server" 
          Text="Available Description Tables: "></asp:Label><br />
           
     <asp:DropDownList ID="ddlDD_Tables" runat="server" Width="200px"  Height="26px" >
                    <asp:ListItem Text="DWH.DOC.NZ_FDTables" Value="0"></asp:ListItem>
                    <asp:ListItem Text="DWH.DOC.NZ_FDSchema" Value="1"></asp:ListItem>
                    <asp:ListItem Text="DWH.DOC.NZ_FDColumnData" Value="2"></asp:ListItem>
     </asp:DropDownList>
      <asp:Button ID="btnLoadData" runat="server" Text="Load Data" Font-Size="X-Small" Height="26px" Width="99px" />
    </asp:Panel></div>




<%--    <div id="RightPnl" style="float: left; width: 80%;">
    <asp:Panel ID="Panel2" runat="server" >--%>


            <asp:GridView ID="Update_Schema" runat="server" bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="Black"  BackColor="White" BorderWidth="1px" 
           Width="100%" ShowHeaderWhenEmpty="True" visible="false" AutoGenerateColumns="False" >
           <AlternatingRowStyle BackColor="#FFE885" /> 

            <Columns>
           <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="false" 
                ShowEditButton="true" >
            <HeaderStyle Width="3%" />
            </asp:CommandField>
        
                
        <asp:BoundField DataField="dBName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="dBName" ReadOnly="True" 
                Visible="True"  >
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>        
                
        <asp:BoundField DataField="SchemaName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaName" ReadOnly="True" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="SchemaDesc" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaDesc"  
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="SchemaCaveat" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaCaveat" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="SchemaAccess" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaAccess" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="LastUpdated" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="LastUpdated" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="LastUpdatedPerson" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="LastUpdatedPerson" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="TechnicalDescription" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="TechnicalDescription" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
              
                
                
            </Columns>


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





    <%--</asp:Panel></div>--%>

</asp:Content>
