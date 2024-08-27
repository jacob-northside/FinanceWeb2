<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataDictionary.aspx.vb" Inherits="FinanceWeb.DataDictionary2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <script type="text/javascript">
        var teclatextbox = false;
        function TeclaTextbox(presionada) {
            teclatextbox = presionada;
        }

        function BodyKeyDown(e) {
            var key;
            if (window.event)//IE 
                key = window.event.keyCode;
            else//Firefox 
                key = e.which;
            if (!e) var e = window.event; //take event 
            if (key == 8 && !teclatextbox)//BACKSPACE and it is not inside a textbox 
            {//stop the default behavior
                e.cancelBubble =
            true;
                e.returnValue =
            false;
                if (e.stopPropagation) {
                    e.stopPropagation();
                    e.preventDefault();
                }
                return false;
            }
            return true;
        }
        
              </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <div style="color: #000000; ">   
   <h2>Search Criteria</h2>
    <asp:Panel ID="pnlSearchTools" runat="server">
   <div style ="font-size:small">
     <asp:TextBox ID="txtBasicSearch" runat="server" width="60%"></asp:TextBox>&nbsp &nbsp 
            <asp:Button ID="btnBasicSearch" runat="server" Text="Search"/>&nbsp &nbsp 
            <asp:Button ID="btnAdvancedOptions" runat="server" Text="Advance Options" 
           style="height: 26px" />&nbsp &nbsp 
       <br />
       <asp:Label ID="lblCount" runat="server" Text="Count:0"></asp:Label><br />
   <asp:Label ID="Label1" runat="server" Text="Limit data sources to: "></asp:Label>
   <asp:RadioButtonList ID="rdlDataSources" runat="server" Font-Size="X-Small" 
               RepeatDirection="Horizontal">
               <asp:ListItem Selected="true">All</asp:ListItem>
               <asp:ListItem>DWH Proper</asp:ListItem>
               <asp:ListItem>MPA Proper</asp:ListItem>
               <asp:ListItem>GDDS Proper</asp:ListItem>
           </asp:RadioButtonList>



       <asp:Panel ID="pnlAdvancedOptions" runat="server" Visible="False">
     
           <br />
           <asp:Label ID="Label2" runat="server" Text="Limit Search to: "></asp:Label>
                <asp:RadioButtonList ID="rdlDataTypes" runat="server" Font-Size="X-Small" 
               RepeatDirection="Horizontal">
               <asp:ListItem>Columns Only</asp:ListItem>
               <asp:ListItem>Tables Only</asp:ListItem>
               <asp:ListItem>Schemas Only</asp:ListItem>
               <asp:ListItem>Databases Only</asp:ListItem>
           </asp:RadioButtonList>
         
          <asp:Button ID="btnResetAdvanced" runat="server" Text="Reset" />   
           &nbsp;&nbsp;&nbsp; 
          <asp:Button ID="btnHideAdvanced" runat="server" Text="Hide" />


       </asp:Panel>

       <br />
       <asp:Label ID = "lblRecommendations" runat = "server" Visible = "False" Text = "Your search found zero matches.  Below are the closest recommendations based on your search values." bordercolor = "Red"
       font-size = "medium" borderstyle = "solid" borderwidth = "1px"></asp:Label>  
       <asp:TextBox ID="txtSortDir" runat="server" Visible = "false" ></asp:TextBox>
       <asp:TextBox ID="txtField" runat="server" Visible = "false"></asp:TextBox>

   <asp:GridView ID="gvDataDictionary" runat="server"   AllowPaging ="True"
        AllowSorting ="true"    AutoGenerateColumns="False" bordercolor="Black" 
        borderStyle="solid" font-Size="Small"  HeaderStyle-BackColor="#4A8fd2" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="#000000" GridLines="Both"  BackColor="#000000" BorderWidth="1px" 
           Width="100%">
          
           <AlternatingRowStyle BackColor="#FFE885" />
           <Columns>
               <asp:BoundField DataField="DataName" HeaderText="Data Name" ReadOnly="True" HeaderStyle-Width ="15%"
                   SortExpression="DataName">
               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  width="15%" />
               </asp:BoundField>
               <asp:BoundField DataField="DataType" HeaderText="Data Type" ReadOnly="True" HeaderStyle-Width ="10%"
                   SortExpression="DataType">
               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  width="10%" />
               </asp:BoundField>
               <asp:BoundField DataField="DataDesc" HeaderText="Description" ReadOnly="True" HeaderStyle-Width ="2%"
                   SortExpression="DataDesc">
               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="22%"/>
               </asp:BoundField>
               <asp:BoundField DataField="TechnicalDescription" HeaderText="Technical Description" ReadOnly="True" HeaderStyle-Width ="23%"
                   SortExpression="TechnicalDescription">
               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  Width="23%"/>
               </asp:BoundField>
               <asp:BoundField DataField="DataCaveat" HeaderText="Caveat" ReadOnly="True" HeaderStyle-Width ="10%"
                   SortExpression="DataCaveat">
               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="10%" />
               </asp:BoundField>
               <asp:BoundField DataField="DataPath" HeaderText="Data Path" ReadOnly="True" HeaderStyle-Width ="20%"
                   SortExpression="DataPath" >
               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="20%" />
               </asp:BoundField>
           </Columns>
           <PagerSettings Position="TopAndBottom" FirstPageText="First" 
               LastPageText="Last" NextPageText="Next" PageButtonCount="25" 
               PreviousPageText="Previous" />
         <EditRowStyle BackColor="#003060" />
            <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
        
           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           
         <%--    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
          
                       
       </asp:GridView>
     </div>
    </asp:Panel> 
   </div>
  <br />
             
</asp:Content>
