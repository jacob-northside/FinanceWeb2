<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DictionaryEditor3.aspx.vb" Inherits="FinanceWeb.DictionaryEditor3" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <style type="text/css">

    .paneltest
 {        
     max-height: 50px;
     overflow-x: hidden; /* Hide horizontal scroll bar*/
     overflow-y: auto; /*Show vertical scroll bar*/
          }
          
    .panellinks
    {
     max-width: 150px;
     overflow-x: auto; /* Show horizontal scroll bar*/
     overflow-y: hidden; /* Hide vertical scroll bar*/
    }

        .paneltest
 {        
     max-width: 250px;
     overflow-x: auto; /* Hide horizontal scroll bar*/
     overflow-y: auto; /*Show vertical scroll bar*/
          }

     .hidden   
 {        display: none;    
          }         

    .changewidth
 {
      width: 100px;
          }

    .lnks
    {
        background-color: #4A8fd2 ;
        color:white;

    }

    .lnks:hover 
    {
        background-color: #214B9A;
                   }

    .GridPanel
 {
    max-height: 400px;
    
 }
 
    .TableBorders
 {
     border-right:1px solid #003060;
     border-left:1px solid #003060;
 }
 
          </style>

            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <script>

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=ScrollPanel.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=ScrollPanel.ClientID%>').scrollLeft;
                yPos = $get('<%=ScrollPanel.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=ScrollPanel.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanel.ClientID%>').scrollLeft = xPos;
                $get('<%=ScrollPanel.ClientID%>').scrollTop = yPos;
            }
        }

        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);

        var xPos2, yPos2;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler2(sender, args) {
            if ($get('<%=SchemaScrollPanel.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos2 = $get('<%=SchemaScrollPanel.ClientID%>').scrollLeft;
                yPos2 = $get('<%=SchemaScrollPanel.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler2(sender, args) {
            if ($get('<%=SchemaScrollPanel.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=SchemaScrollPanel.ClientID%>').scrollLeft = xPos2;
                $get('<%=SchemaScrollPanel.ClientID%>').scrollTop = yPos2;
            }
        }

        prm.add_beginRequest(BeginRequestHandler2);
        prm.add_endRequest(EndRequestHandler2);

        var xPos3, yPos3;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler3(sender, args) {
            if ($get('<%=TableScrollPanel.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos3 = $get('<%=TableScrollPanel.ClientID%>').scrollLeft;
                yPos3 = $get('<%=TableScrollPanel.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler3(sender, args) {
            if ($get('<%=TableScrollPanel.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=TableScrollPanel.ClientID%>').scrollLeft = xPos3;
                $get('<%=TableScrollPanel.ClientID%>').scrollTop = yPos3;
            }
        }

        prm.add_beginRequest(BeginRequestHandler3);
        prm.add_endRequest(EndRequestHandler3);

        var xPos4, yPos4;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler4(sender, args) {
            if ($get('<%=ColumnScrollPanel.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos4 = $get('<%=ColumnScrollPanel.ClientID%>').scrollLeft;
                yPos4 = $get('<%=ColumnScrollPanel.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler4(sender, args) {
            if ($get('<%=ColumnScrollPanel.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ColumnScrollPanel.ClientID%>').scrollLeft = xPos4;
                $get('<%=ColumnScrollPanel.ClientID%>').scrollTop = yPos4;
            }
        }

        prm.add_beginRequest(BeginRequestHandler4);
        prm.add_endRequest(EndRequestHandler4);
    </script>

     <div style="color: #000000">
      <asp:UpdatePanel runat="server" ID= "updMain">

    <ContentTemplate>   
     <asp:Label ID="lblUser" runat="server" Text="" visible="false"></asp:Label>

    <asp:Label ID="DatabaseSort" Visible = "false" runat="server" ></asp:Label>
    <asp:Label ID="DatabaseDir" Visible="false" runat="server"></asp:Label>
         
<asp:Panel ID="pnlDatabase" runat="server" Width="100%" > 
   

     This is a list of existing SQL Server databases for the BIDS group.<br /> 
     Use Edit and Delete controls in grid view below to modify existing databases.
    <br />
  <%--  HBI server<br /> DO not add this tag back we don't need to know which server the data is coming from--%> 
    <br />
    <asp:Table runat = "server" >
    <asp:TableHeaderRow  BackColor="#6da9e3" ForeColor = "White"  >
    <asp:TableHeaderCell ID = "tblDBSelect" Width = "70px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell ID = "tblDBEdit" Visible = "false" Width = "70px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "120px" >Name of Database</asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "285px" >Description</asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "285px" >Technical Desc.</asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "200px" >Caveat(s)</asp:TableHeaderCell>
<%--    <asp:TableHeaderCell  Width = "155px" >Owner</asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "135px" >Access</asp:TableHeaderCell>--%>
    <asp:TableHeaderCell  Width = "150px" >Term Last Updated</asp:TableHeaderCell></asp:TableHeaderRow>
    </asp:Table>
    <asp:Panel ID="ScrollPanel" runat = "server" ScrollBars = "Auto" >
    <asp:GridView ID="gvFDdatabase" runat="server"
           AllowSorting="True" AutoGenerateColumns="False" BorderColor="Black" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A" 
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames="name" 
         BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0"
            HeaderStyle-CssClass="hidden">   
        <AlternatingRowStyle BackColor="white"  />
                
        <Columns>

           <asp:CommandField ItemStyle-Width="55px" ShowDeleteButton="false" UpdateText="Update<br>"
                ShowEditButton="true" ShowSelectButton = "true" SelectText = "" >
            <HeaderStyle Width="55px" />
            </asp:CommandField>
            <asp:ButtonField  CausesValidation="True" 
                CommandName="ViewDatabase" ControlStyle-Width = "80px" DataTextField="name" 
                HeaderText="Name of Database" SortExpression="name" 
                Text="eval(&quot;name&quot;) ">
            <HeaderStyle />
            <ItemStyle Font-Bold="True" Font-Overline="False" Font-Underline="False" Width = "80px" />
            </asp:ButtonField>

            <asp:BoundField DataField="name" HeaderStyle-HorizontalAlign="Left" 
                ControlStyle-Width="80px" HeaderStyle-Wrap="true" 
                HeaderText="Name of Database" ReadOnly="True" SortExpression="name" 
                Visible="False">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            <ItemStyle Width = "80px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText = "Description" SortExpression ="dbDesc" AccessibleHeaderText = "Desc" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest" Width = "95%" runat = "server">
                    <asp:Label ID="lblDBGVDescription" runat = "server" Text='<%# Bind("dbDesc")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtDBGVDescription" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("dbDesc")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText = "Technical Desc." SortExpression ="TechnicalDescription"  ItemStyle-Width = "290px">
                <ItemTemplate>
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblDBGVTechDesc" runat = "server" Text='<%# Bind("TechnicalDescription")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtDBGVTechDesc" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TechnicalDescription")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>

 

            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBCaveat" ControlStyle-Width="120px" 
                HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="dBCaveat">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="120px" />
            <ItemStyle Width = "120px" />
            </asp:BoundField>

   <%--         <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dbOwner" ControlStyle-Width="120px" 
                HeaderText="Owner" SortExpression="dBOwner">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="120px" />
            <ItemStyle Width = "120px" />
            </asp:BoundField>--%>

 <%--           <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBAccess" ControlStyle-Width="120px" 
                HeaderText="Access" SortExpression="dBAccess">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="120px" />
            <ItemStyle Width = "120px" />
            </asp:BoundField>--%>

            <asp:BoundField DataField="LastUpdated" ControlStyle-Width="120px" 
                HeaderText="Term Last Updated" ReadOnly="true" SortExpression="LastUpdated">
            <HeaderStyle Width="120px" />
            <ItemStyle Width = "120px" />
            </asp:BoundField>
        </Columns>
    
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC"  HorizontalAlign="Left"/>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        
           <PagerSettings Mode="NumericFirstLast" />
        
           <RowStyle BackColor="#CBE3FB" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#6da9e3" Font-Bold="True" ForeColor="#333333" Font-Size="Small"/>
                      <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
     </asp:Panel>
    <br />

 
   </asp:Panel>
    

<asp:Panel ID="pnlSchema" runat="server" Visible ="false" Width ="100%" >
 &nbsp;<asp:Button ID="btnReturnToDatabase" runat="server" 
        Text="Return to Database list" /> <br />
    <asp:Label ID="SchemaSort" Visible = "false" runat="server" Text="TABLE_SCHEMA" ></asp:Label>
    <asp:Label ID="SchemaDir" Visible="false" runat="server" Text=
        
        
        ></asp:Label>
    <asp:Label ID="lblDatabaseSelected" runat="server" Text="" Visible="false" ></asp:Label>
    <br />
    <asp:Table runat="server" Width="100%"><asp:TableRow><asp:TableCell>
Existing SQL Server information for the following: </asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell> Database: <asp:Label ID="lblSchemaDatabase" runat="server" Font-Bold="True"></asp:Label> </asp:TableCell></asp:TableRow>
              <asp:TableRow><asp:TableCell> Use Edit and Delete controls in grid view below to modify existing schemas. 
                                                         </asp:TableCell>
                <asp:TableCell Width="150px">
                    <asp:CheckBox Visible="false" AutoPostBack="true" runat="server" ID="chbSchemaInactive" Text="Include Inactive Rows" Checked="false" /> </asp:TableCell>
              </asp:TableRow></asp:Table>
  

   <asp:Table BackColor = "#2b74bb" CellSpacing = "3"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
    <asp:TableCell >Search: </asp:TableCell>
    <asp:TableCell ></asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSGVSearchDesc" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSGVSearchTechDesc" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSGVSearchCaveat" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell ></asp:TableCell>
<%--    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSGVSearchOwner" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSGVSearchAccess" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>--%>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSGVSearchUpdate" Width = "95%" AutoPostBack="True"  ></asp:TextBox> 
                             <cc1:calendarextender ID="CalendarExtender11" 
                                runat="server" TargetControlID="txtSGVSearchUpdate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
    </asp:TableCell>
    </asp:TableRow>    
    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White"  >
    <asp:TableHeaderCell ID = "tblSSelect" Width = "70px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell ID = "tblSEdit" Visible = "false" Width = "70px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "120px" ><asp:LinkButton runat="server" Text="Schema" ForeColor="White" CssClass="lnks" ID="lnkSch1" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "270px" ><asp:LinkButton runat="server" Text="Description" ForeColor="White" CssClass="lnks" ID="lnkSch2" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "270px" ><asp:LinkButton runat="server" Text="Technical Desc." ForeColor="White" CssClass="lnks" ID="lnkSch3" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "250px" ><asp:LinkButton runat="server" Text="Caveat(s)" ForeColor="White" CssClass="lnks"  ID="lnkSch4" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "70px" ><asp:LinkButton runat="server" Text="# of Tables" ForeColor="White" CssClass="lnks"  ID="lnkSch5" ></asp:LinkButton></asp:TableHeaderCell>
<%--    <asp:TableHeaderCell Width = "105px" ><asp:LinkButton runat="server" Text="Owner" ForeColor="White" CssClass="lnks" ID="lnkSch6" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "110px" ><asp:LinkButton runat="server" Text="Access" ForeColor="White" CssClass="lnks"  ID="lnkSch7" ></asp:LinkButton></asp:TableHeaderCell>--%>
    <asp:TableHeaderCell Width = "100px" ><asp:LinkButton runat="server" Text="Term Last Updated" ForeColor="White" CssClass="lnks"  ID="lnkSch8" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell ID="schemaActive" Visible ="false" Width = "60px" ><asp:LinkButton runat="server" Text="Active" ForeColor="White" CssClass="lnks"  ID="lnkSch9" >Active</asp:LinkButton></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>
    
   <asp:Panel ID="SchemaScrollPanel" runat = "server" ScrollBars = "Auto" CssClass = "GridPanel" >
    <asp:GridView ID="gvFDSchema" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True"   BorderColor="Black"
               BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames="TABLE_SCHEMA" 
         BackColor="#CBE3FB" BorderWidth="2px" PageSize="25" HeaderStyle-CssClass = "hidden" 
            >   
        <AlternatingRowStyle BackColor="white"  />
         <Columns> 
         <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" ShowSelectButton = "true" UpdateText ="Update<br />"
                HeaderStyle-Width="3%" SelectText = ""> 
            <HeaderStyle Width="3%" />
            <ItemStyle Width = "52px" />
            </asp:CommandField>
            
            <asp:ButtonField Text="View" CausesValidation ="false" 
                        headerstyle-width="3%" CommandName="ViewTables" 
                 DataTextField="TABLE_SCHEMA" HeaderText="Schema" SortExpression="TABLE_SCHEMA" >
                         <HeaderStyle Width="3%" />
             <ItemStyle Width = "90px" Font-Bold="True" />
         
            </asp:ButtonField>

            <asp:BoundField DataField="TABLE_SCHEMA" HeaderText="Schema"  ReadOnly="true" 
                SortExpression="TABLE_SCHEMA" Visible="False" ControlStyle-Width="125px" >
             
             </asp:BoundField>

            <asp:TemplateField HeaderText = "Description" SortExpression ="SchemaDesc" ItemStyle-Width = "300px" >
                <ItemTemplate>              
                <asp:Panel ID="Panel1" CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblSGVDescription" runat = "server" Text='<%# Bind("SchemaDesc")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtSGVDescription" Width = "98%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("SchemaDesc")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText = "Technical Desc." SortExpression ="TechnicalDescription" ItemStyle-Width = "300px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblSGVTechDesc" runat = "server" Text='<%# Bind("TechnicalDescription")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtSGVTechDesc" Width = "98%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TechnicalDescription")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText = "Caveat(s)" SortExpression ="SchemaCaveat" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblSGVCaveat" runat = "server" Text='<%# Bind("SchemaCaveat")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtSGVCaveat" Width = "98%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("SchemaCaveat")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="cntTable" HeaderText="# of Tables" ReadOnly="True"  HeaderStyle-Width ="5%"
                SortExpression="cntTable" >
                
             <HeaderStyle Width="5%" />
             <ItemStyle Width = "40px" />
             </asp:BoundField>

  <%--          <asp:TemplateField HeaderText = "Owner" SortExpression ="SchemaOwner" ItemStyle-Width = "100px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblSGVOwner" runat = "server" Text='<%# Bind("SchemaOwner")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtSGVOwner" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("SchemaOwner")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>--%>
                

<%--          <asp:TemplateField HeaderText = "Access" SortExpression ="SchemaAccess" ItemStyle-Width = "100px" >
                <ItemTemplate>              
                <asp:Panel  CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblSGVAccess" runat = "server" Text='<%# Bind("SchemaAccess")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtSGVAccess" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("SchemaAccess")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>--%>

               
            <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="10%"
                     SortExpression="LDUpdated" ReadOnly ="true"  >
             <HeaderStyle Width="10%" />
             <ItemStyle Width = "95px" />
             </asp:BoundField>


           <asp:TemplateField HeaderText = "Active" SortExpression ="Active" ItemStyle-Width = "15px" >
                <ItemTemplate>              
                <asp:Panel Width = "95%" runat = "server">
                    <asp:Checkbox ID="chbSGVActiveDisabled" Enabled="false" runat = "server" Checked='<%# Bind("Active")%>'></asp:Checkbox>
                </asp:Panel>
                    <asp:Checkbox ID="chbSGVActive" Width = "95%" runat = "server" Checked='<%# Bind("Active")%>' Visible = "false" ></asp:Checkbox>
                
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>
        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#CBE3FB" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#6da9e3" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        </asp:Panel>
        
        

        </asp:Panel>

<asp:Panel id="pnlTables" runat="server" Visible="false"   Width ="100%" >
  &nbsp; <asp:Button ID="btnReturnSchema" runat="server" Text="Return to Schema list" /><br />
   <asp:Label ID="TableSort" Visible = "false" runat="server" Text="Table_name" ></asp:Label>
   <asp:Label ID="TableDir" Visible="false" runat="server" Text="asc"></asp:Label>
   <asp:Label ID="lblSchemaSelected" runat="server" Text="" visible="false" ></asp:Label> <br />
    <asp:Table runat="server" Width="100%"> <asp:TableRow><asp:TableCell>
   Existing SQL Server information for the following</asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
   Schema: <asp:Label ID="lblTableSchema" runat="server" Text="" Font-Bold="true"> </asp:Label> </asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>
   Database:<asp:Label ID="lblTableDatabase" runat="server" Text="" Font-Bold="true" ></asp:Label></asp:TableCell></asp:TableRow>
         <asp:TableRow><asp:TableCell>
   Use Edit and Delete controls in grid view below to modify existing tables. </asp:TableCell>
             <asp:TableCell Width ="150px"><asp:CheckBox ID="chbTablesInactive" AutoPostBack="true" runat="server" Visible="false" Checked="false" Text="Include Inactive Rows" Font-Size="Small" /></asp:TableCell>
         </asp:TableRow> 
</asp:Table>
    <br /> 
    
   <asp:Table BackColor = "#2b74bb" CellSpacing = "3"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
    <asp:TableCell >Search: </asp:TableCell>
    <asp:TableCell ></asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchDesc" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchTechDesc" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchCaveat" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell ></asp:TableCell>
<%--    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchOwner" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchAccess" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>--%>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchType" Width = "98%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchUpdate" Width = "100%" AutoPostBack="True"  ></asp:TextBox> 
                             <cc1:calendarextender ID="CalendarExtender1" 
                                runat="server" TargetControlID="txtTblSearchUpdate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
    </asp:TableCell>
    </asp:TableRow>    
    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White"  >
    <asp:TableHeaderCell ID = "tblTSelect" Width = "60px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell ID = "tblTEdit" Visible = "false" Width = "60px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "180px" ><asp:LinkButton runat="server" Text="Table Name" ForeColor="White" CssClass="lnks" ID="lnkTbl1" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "230px" ><asp:LinkButton runat="server" Text="Description" ForeColor="White" CssClass="lnks" ID="lnkTbl2" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "260px" ><asp:LinkButton runat="server" Text="Technical Desc." ForeColor="White" CssClass="lnks" ID="lnkTbl3" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "150px" ><asp:LinkButton runat="server" Text="Caveat(s)" ForeColor="White" CssClass="lnks"  ID="lnkTbl4" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "75px" ><asp:LinkButton runat="server" Text="# of Columns" ForeColor="White" CssClass="lnks"  ID="lnkTbl5" ></asp:LinkButton></asp:TableHeaderCell>
<%--    <asp:TableHeaderCell Width = "100px" ><asp:LinkButton runat="server" Text="Owner" ForeColor="White" CssClass="lnks" ID="lnkTbl6" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "110px" ><asp:LinkButton runat="server" Text="Access" ForeColor="White" CssClass="lnks"  ID="lnkTbl7" ></asp:LinkButton></asp:TableHeaderCell>--%>
    <asp:TableHeaderCell Width = "80px" ><asp:LinkButton runat="server" Text="Type" ForeColor="White" CssClass="lnks"  ID="lnkTbl8" ></asp:LinkButton></asp:TableHeaderCell>   
    <asp:TableHeaderCell Width = "115px" ><asp:LinkButton runat="server" Text="Term Last Updated" ForeColor="White" CssClass="lnks"  ID="lnkTbl9" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell ID="tableActive" Visible ="false" Width = "60px" ><asp:LinkButton runat="server" Text="Active" ForeColor="White" CssClass="lnks"  ID="lnkTbl10" >Active</asp:LinkButton></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>

     <asp:Panel ID="TableScrollPanel" runat = "server" ScrollBars = "Auto" CssClass = "GridPanel" >
    <asp:GridView ID="gvFDTables" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True"     BorderColor="Black" 
   BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  DataKeyNames="Table_name"  
         BackColor="#CBE3FB" BorderWidth="1px" PageSize="25" 
            HeaderStyle-CssClass="hidden" >   
        <AlternatingRowStyle BackColor="White"  />
           <Columns>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" ShowSelectButton="true" UpdateText ="Update<br />"
                HeaderStyle-Width="3%" SelectText = "">
            <HeaderStyle Width="3%" />
            <ItemStyle Width = "52px" />
            </asp:CommandField>
            
<%--            <asp:ButtonField Text="View" CausesValidation ="false" 
                        headerstyle-width="3%" CommandName="ViewTableData" ControlStyle-Width="250px" ItemStyle-Wrap="true" 
                   DataTextField="Table_name" HeaderText="Table Name" SortExpression="Table_name" >
                    <HeaderStyle Width="3%" />
               <ItemStyle Width = "250px" Font-Bold="True" />
            </asp:ButtonField>--%>

            <asp:TemplateField HeaderText="Table Name" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Panel CssClass="panellinks" Width="100%" runat="server">
                        <asp:LinkButton ID="lnkTGVTable" runat="server" Text='<%# Bind("Table_name")%>' CommandName="ViewTableData"></asp:LinkButton>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            
               <asp:BoundField DataField="Table_name" HeaderText="Table Name" ReadOnly="True"  HeaderStyle-Width ="5px"
                   SortExpression="Table_name" Visible="False" >
               <HeaderStyle Width="5px" />
               </asp:BoundField>

            <asp:TemplateField HeaderText = "Description" SortExpression ="TableDesc" ItemStyle-Width = "250px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltech"  Width = "98%" runat = "server">
                    <asp:Label ID="lblTGVDescription" runat = "server" Text='<%# Bind("TableDesc")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtTGVDescription" Width = "98%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TableDesc")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>



            <asp:TemplateField HeaderText = "Technical Desc." SortExpression ="TechnicalDescription" ItemStyle-Width = "250px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblTGVTechDesc" runat = "server" Text='<%# Bind("TechnicalDescription")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtTGVTechDesc" Width = "98%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TechnicalDescription")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText = "Caveat(s)" SortExpression ="TableCaveat" ItemStyle-Width = "200px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblTGVCaveat" runat = "server" Text='<%# Bind("TableCaveat")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtTGVCaveat" Width = "98%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TableCaveat")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>

 
               <asp:BoundField DataField="cntColumn" HeaderText="# of Columns" 
                   ReadOnly="True"  HeaderStyle-Width ="40px"
                   SortExpression="cntColumn" >
               <HeaderStyle Width="5%" />
                   <ItemStyle Width="40px" />
               </asp:BoundField>

<%--            <asp:TemplateField HeaderText = "Owner" SortExpression ="TableOwner" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblTGVOwner" runat = "server" Text='<%# Bind("TableOwner")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtTGVOwner" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TableOwner")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>--%>

             
<%--             <asp:TemplateField HeaderText = "Access" SortExpression ="TableAccess" ItemStyle-Width = "120px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblTGVAccess" runat = "server" Text='<%# Bind("TableAccess")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtTGVAccess" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TableAccess")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>--%>



              <asp:BoundField DataField="Table_Type" HeaderText="Type" HeaderStyle-Wrap="false" HeaderStyle-Width ="8%"
                                   SortExpression="Table_Type" ReadOnly = "true">
                    <HeaderStyle Width="60px" Wrap="False" />
                  <ItemStyle Width="60px" />
                    </asp:BoundField>


               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="8%"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
               <HeaderStyle Width="95px" />
                   <ItemStyle Width="95px" />
               </asp:BoundField>

              <asp:TemplateField HeaderText = "Active" SortExpression ="Active" ItemStyle-Width = "15px" >
                <ItemTemplate>              
                <asp:Panel ID="Panel5" CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Checkbox ID="chbTGVActiveDisabled" Enabled="false" runat = "server" Checked='<%# Bind("Active")%>'></asp:Checkbox>
                </asp:Panel>
                    <asp:Checkbox ID="chbTGVActive" Width = "98%" runat = "server" Checked='<%# Bind("Active")%>' Visible = "false" ></asp:Checkbox>
                
                </ItemTemplate>
            </asp:TemplateField>

           </Columns>
       <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#CBE3FB" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#CBE3FB" Font-Bold="True" ForeColor="#333333" Font-Size ="Small" />
             <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
         </asp:Panel>


   </asp:Panel>
         
<asp:Panel ID="pnlColumns" runat="server" Visible="false"  Width="100%" > 
&nbsp;<asp:Button ID="btnReturnTables" runat="server" Text="Return to Table list"  /><br />
    <asp:Label ID="ColumnSort" Visible ="false" runat="server" Text ="COLUMN_NAME"></asp:Label>
    <asp:Label ID="ColumnDir" Visible = "false" runat="server" Text="asc"></asp:Label> 
    <asp:Label ID="lblTableSelected" runat="server" Text="" visible="false" ></asp:Label> 
      <asp:Table runat="server" Width="100%"> <asp:TableRow><asp:TableCell>
Existing SQL Server information for the following</asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
Table:  <asp:Label ID="lblColumnTable" runat="server" Text="" Font-Bold="true"> </asp:Label> </asp:TableCell></asp:TableRow>  <asp:TableRow><asp:TableCell>
Schema: <asp:Label ID="lblColumnSchema" runat="server" Text="" Font-Bold="true"> </asp:Label>  </asp:TableCell></asp:TableRow>  <asp:TableRow><asp:TableCell>
Database:<asp:Label ID="lblColumnDatabase" runat="server" Text="" Font-Bold="true" ></asp:Label></asp:TableCell></asp:TableRow>  <asp:TableRow><asp:TableCell>
Use Edit and Delete controls in grid view below to modify existing columns. </asp:TableCell> 
    <asp:TableCell Width ="150px"><asp:CheckBox ID="chbColumnsInactive" AutoPostBack="true" runat="server" Visible="false" Checked="false" Text="Include Inactive Rows" Font-Size="Small" /></asp:TableCell>
    </asp:TableRow></asp:Table>
<%--
<asp:Label ID="lblColumn" runat="server"></asp:Label>
 <asp:Label ID="lblcolTableSelected" runat="server" text="" font-bold="true"></asp:Label> in the <asp:Label ID="lblcolSchemaSelected" runat="server" Text="" Font-Bold="true"></asp:Label> schema in the 
 <asp:Label ID="lblcolDatabaseSelected" runat="server" Text="" Font-Bold="true"></asp:Label>
 --%>
<br />

    
   <asp:Table BackColor = "#2b74bb" CellSpacing = "3"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
    <asp:TableCell >Search: </asp:TableCell>
    <asp:TableCell ></asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchDesc" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchTechDesc" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchCaveat" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
   <%-- <asp:TableCell ></asp:TableCell>--%>
<%--    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchOwner" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchAccess" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>--%>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchType" Width = "98%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtColumnSearchUpdate" Width = "100%" AutoPostBack="True"  ></asp:TextBox> 
                             <cc1:calendarextender ID="CalendarExtender2" 
                                runat="server" TargetControlID="txtColumnSearchUpdate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
    </asp:TableCell>
    </asp:TableRow>    
    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White"  >
    <asp:TableHeaderCell ID = "tblCSelect" Width = "65px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell ID = "tblCEdit" Visible = "false" Width = "65px" Height = "20px" >&nbsp;</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "170px" ><asp:LinkButton runat="server" Text="Column Name" ForeColor="White" CssClass="lnks" ID="lnkCol1" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "280px" ><asp:LinkButton runat="server" Text="Description" ForeColor="White" CssClass="lnks" ID="lnkCol2" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "250px" ><asp:LinkButton runat="server" Text="Technical Desc." ForeColor="White" CssClass="lnks" ID="lnkCol3" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "250px" ><asp:LinkButton runat="server" Text="Caveat(s)" ForeColor="White" CssClass="lnks"  ID="lnkCol4" ></asp:LinkButton></asp:TableHeaderCell>
  <%--  <asp:TableHeaderCell Width = "60px" ><asp:LinkButton runat="server" Text="# of Columns" ForeColor="White" CssClass="lnks"  ID="lnkCol5" ></asp:LinkButton></asp:TableHeaderCell>--%>
<%--    <asp:TableHeaderCell Width = "100px" ><asp:LinkButton runat="server" Text="Owner" ForeColor="White" CssClass="lnks" ID="lnkCol6" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "110px" ><asp:LinkButton runat="server" Text="Access" ForeColor="White" CssClass="lnks"  ID="lnkCol7" ></asp:LinkButton></asp:TableHeaderCell>--%>
    <asp:TableHeaderCell Width = "120px" ><asp:LinkButton runat="server" Text="Data Type" ForeColor="White" CssClass="lnks"  ID="lnkCol8" ></asp:LinkButton></asp:TableHeaderCell>   
    <asp:TableHeaderCell Width = "115px" ><asp:LinkButton runat="server" Text="Term Last Updated" ForeColor="White" CssClass="lnks"  ID="lnkCol9" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell ID="columnActive" Visible ="false" Width = "50px" ><asp:LinkButton runat="server" Text="Active" ForeColor="White" CssClass="lnks"  ID="lnkCol10" >Active</asp:LinkButton></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>

    <asp:Panel ID="ColumnScrollPanel" runat = "server" ScrollBars = "Auto" CssClass = "GridPanel" >
 <asp:GridView ID="gvFDColumns" runat="server" AutoGenerateColumns ="False" 
        AllowSorting="True"  BorderColor="Black" 
  BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames ="COLUMN_NAME" 
         BackColor="#CBE3FB" BorderWidth="1px" PageSize="25" 
           Width="100%" HeaderStyle-CssClass="hidden" >   
        <AlternatingRowStyle BackColor="White"  />

           <Columns>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="false"  ShowSelectButton="true" UpdateText ="Update<br />"
                HeaderStyle-Width="3%" SelectText = "">
            <HeaderStyle Width="3%" />
            </asp:CommandField>

<%--               <asp:BoundField DataField="COLUMN_NAME" HeaderText="Column Name" 
                   ReadOnly="True"  HeaderStyle-Width ="8%"
                   SortExpression="COLUMN_NAME" >
               <HeaderStyle Width="8%" />
               <ItemStyle Font-Bold="true" />
               </asp:BoundField>--%>

            <asp:TemplateField HeaderText="Column Name" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Panel CssClass="panellinks" Width="100%" runat="server">
                        <asp:Label ID="lblCGVColumnName" runat = "server" Text='<%# Bind("COLUMN_NAME")%>'></asp:Label>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>


             <asp:TemplateField HeaderText = "Description" SortExpression ="ColumnDesc" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblCGVDescription" runat = "server" Text='<%# Bind("ColumnDesc")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtCGVDescription" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("ColumnDesc")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>



            <asp:TemplateField HeaderText = "Technical Desc." SortExpression ="TechnicalDescription" ItemStyle-Width = "240px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblCGVTechDesc" runat = "server" Text='<%# Bind("TechnicalDescription")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtCGVTechDesc" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("TechnicalDescription")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


           <asp:TemplateField HeaderText = "Caveat(s)" SortExpression ="columnCaveat" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblCGVCaveat" runat = "server" Text='<%# Bind("columnCaveat")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtCGVCaveat" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("columnCaveat")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>

   <%--            <asp:BoundField DataField="ORDINAL_POSITION" HeaderText="Column Order" 
                   ReadOnly="True" HeaderStyle-Width="5%" HeaderStyle-Wrap="true"
                   SortExpression="ORDINAL_POSITION" >
               <HeaderStyle Width="5%" Wrap="True" />
               </asp:BoundField>--%>

               <asp:BoundField DataField="DATA_TYPE" HeaderText="Data Type" ReadOnly="True" HeaderStyle-Width="5%" 
                   SortExpression="DATA_TYPE" >
               <HeaderStyle Width="5%" />
               </asp:BoundField>

 <%--              <asp:BoundField DataField="Size" HeaderText="Size" ReadOnly="True" HeaderStyle-Width="3%" 
                   SortExpression="Size" >
               <HeaderStyle Width="3%" />
               </asp:BoundField>--%>

<%--              <asp:BoundField DataField="COLUMN_DEFAULT" HeaderText="Default Value" 
                   ReadOnly="True" HeaderStyle-Width="10%" 
                   SortExpression="COLUMN_DEFAULT" >
               <HeaderStyle Width="10%" />
               </asp:BoundField>--%>
               
 <%--              <asp:BoundField DataField="IS_NULLABLE" HeaderText="Nullable" ReadOnly="True"  HeaderStyle-Width="5%" 
                   SortExpression="IS_NULLABLE" >
               <HeaderStyle Width="5%" />
               </asp:BoundField>--%>

<%--               <asp:BoundField DataField="isKey" HeaderText="Key(s)" ReadOnly="True"  HeaderStyle-Width="3%" 
                   SortExpression="isKey" >
               <HeaderStyle Width="3%" />
               </asp:BoundField>--%>


<%--            <asp:TemplateField HeaderText = "Owner" SortExpression ="ColumnOwner" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblCGVOwner" runat = "server" Text='<%# Bind("ColumnOwner")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtCGVOwner" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("ColumnOwner")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>--%>
             

<%--             <asp:TemplateField HeaderText = "Access" SortExpression ="ColumnAccess" ItemStyle-Width = "290px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "95%" runat = "server">
                    <asp:Label ID="lblCGVAccess" runat = "server" Text='<%# Bind("ColumnAccess")%>'></asp:Label>
                </asp:Panel>
             <asp:TextBox ID="txtCGVAccess" Width = "95%" TextMode = "MultiLine" Rows = "3" runat = "server" Text='<%# Bind("ColumnAccess")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>--%>

               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="5%"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
                   <ItemStyle Width="95px" />
               <HeaderStyle Width="95px" />
               </asp:BoundField>

            <asp:TemplateField HeaderText = "Active" SortExpression ="Active" ItemStyle-Width = "15px" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest"  Width = "98%" runat = "server">
                    <asp:Checkbox ID="chbCGVActiveDisabled" Enabled="false" runat = "server" Checked='<%# Bind("Active")%>'></asp:Checkbox>
                </asp:Panel>
                    <asp:Checkbox ID="chbCGVActive" Width = "98%" runat = "server" Checked='<%# Bind("Active")%>' Visible = "false" ></asp:Checkbox>
                
                </ItemTemplate>
            </asp:TemplateField>

           </Columns>
                  

    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#CBE3FB" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        </asp:Panel>

</asp:Panel>
 
<br />
<br />
<asp:Label ID="Label2" runat="server" Text="Terms & Definitions " Font-Underline="True"  Font-Bold="True"></asp:Label>
         <br />
      <asp:Label ID ="AllPages" runat = "server" Text="All Pages" Font-Underline = "true"></asp:Label>  <br />
<asp:Label ID="Label3" runat="server" Text="Description " font-bold="true"> </asp:Label> - Explanation of what the data element is. (Editable)  <br />
<asp:Label ID = "Label4" runat ="Server" Text="Technical Desc. " Font-Bold="True"></asp:Label>  - Explanation of any logic or formulas that maybe applied to the data element. (Editable) <br />
<asp:Label ID = "Label5" runat="server" Text="Caveat(s) " Font-Bold="true"></asp:Label> - Any limitations of the data element that need to be highlighted. (Editable)<br />
<%--<asp:Label ID="Label6" runat="server" Text="Owner " Font-Bold="true"></asp:Label> - Data element steward. (Editable)<br />
<asp:Label ID="Label9" runat="server" Text="Access " Font-Bold="true"></asp:Label> - Any limitaions on access to the data element. (Editable)<br />--%>

         <asp:Label ID="Label8" runat="server" Text="Term Last Updated " Font-Bold="true"></asp:Label> - Date that the data element was last updated. (System Generated) <br /><br />
        <asp:Panel runat="server" ID="pnlDBDefinitions">
<asp:Label ID ="Label24" runat = "server" Text="Database specific" Font-Underline = "true"></asp:Label>
         <br />  
<asp:Label ID="Label7" runat="server" Text="Name of Database " Font-Bold="true"></asp:Label> - Name pulled directly from SQL Server database. (System Generated)<br />
         <br />
 <asp:Label ID="Label34" runat="server" Text="Example:" Font-Bold ="true" Font-Underline ="true"></asp:Label> <br />
  <asp:Label ID="Label30" runat="server" Text="Name of Database" Font-Bold="true"></asp:Label> - DWH <br />
  <asp:Label ID="Label31" runat="server" Text="Description" Font-Bold="true"></asp:Label> - BIDS Data Warehouse<br /><br />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSchemaDefinitions" Visible="false">
<asp:Label ID ="Label25" runat = "server" Text="Schema specific" Font-Underline = "true"></asp:Label>
<br /> <asp:Label ID="Label10" runat="server" Text="Schema " Font-Bold="true"></asp:Label> - A logical grouping of tables in the database. (System Generated)<br />
 <asp:Label ID="Label11" runat="server" Text="# of Tables " Font-Bold="true"></asp:Label> - The number of tables and views in the schema. (System Generated)<br />
         <br />
             <asp:Label ID="Label1" runat="server" Text="Example:" Font-Bold ="true" Font-Underline ="true"></asp:Label> <br />
  <asp:Label ID="Label32" runat="server" Text="Schema" Font-Bold="true"></asp:Label> - MD2 <br />
  <asp:Label ID="Label33" runat="server" Text="Description" Font-Bold ="true"></asp:Label> - Physician Practice billing repository with extended data<br />
         <asp:Label ID="Label35" runat="server" Text="Caveat(s)" Font-Bold="true"></asp:Label> - Excludes MMIS, and practices maintained in MD<br />
         <asp:Label ID="Label36" runat="server" Text="# of Tables" Font-Bold="true"></asp:Label> - 15 <br />
        <%-- <asp:Label ID="Label37" runat="server" Text="Owner" Font-Bold="true"> </asp:Label>- FSI <br />--%>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTableDefinitions" Visible="false">
<asp:Label ID ="Label26" runat = "server" Text="Table specific" Font-Underline = "true"></asp:Label>         <br /> 
 <asp:Label ID="Label12" runat="server" Text="Table Name " Font-Bold="true"></asp:Label> - Name of the Table or view. (System Generated)<br />
 <asp:Label ID="Label13" runat="server" Text="# of Columns " Font-Bold="true"></asp:Label> - Number of columns in the table or view. (System Generated)<br />
 <asp:Label ID="Label14" runat="server" Text="Type " Font-Bold="true"></asp:Label> - Base Table or View. (System Generated)<br />
         <br />
             <asp:Label ID="Label48" runat="server" Text="Example:" Font-Bold ="true" Font-Underline ="true"></asp:Label> <br />
         <asp:Label ID="Label38" runat="server" Text="Table" Font-Bold="true"> </asp:Label> - Charges <br />
         <asp:Label ID="Label39" runat="server" Text="Description" Font-Bold="true"></asp:Label> - Charges level detail data. <br />
         <asp:Label ID="Label40" runat="server" Text="Technical Desc." Font-Bold="true"></asp:Label> -Charges made to the groups <br />
<asp:Label ID="Label41" runat="server" Text="# of Columns" Font-Bold="true"></asp:Label> - 52 <br />
         <%--<asp:Label ID="Label42" runat="server" Text="Owner" Font-Bold="true" ></asp:Label> - Practice Billing Systems <br /> --%>
         <asp:Label ID="Label43" runat="server" Text="Type" Font-Bold="true"></asp:Label> - BASE TABLE <br />
         <br />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlColumnDefinitions" Visible="false">
<asp:Label ID ="Label27" runat = "server" Text="Column specific" Font-Underline = "true"></asp:Label>         <br />
 <asp:Label ID="Label15" runat="server" Text="Column Name " Font-Bold="true"></asp:Label> - Name of the column. (System Generated)<br />
<%-- <asp:Label ID="Label16" runat="server" Text="Column Order " Font-Bold="true"></asp:Label> - Ordinal location of the column in the database. (System Generated)<br />--%>
 <asp:Label ID="Label17" runat="server" Text="Data Type " Font-Bold="true"></asp:Label> - Data type of the column in the database. (System Generated)<br />

<%-- <asp:Label ID="Label18" runat="server" Text="Size " Font-Bold="true"></asp:Label> - Data element size, if available. (System Generated)<br />
 <asp:Label ID="Label19" runat="server" Text="Default Value " Font-Bold="true"></asp:Label> - Default size of the data element. (System Generated)<br />
 <asp:Label ID="Label28" runat="server" Text="Nullable " Font-Bold="true"></asp:Label> -If the data element is allowed to be NULL, without data. (System Generated)<br />
 <asp:Label ID="Label29" runat="server" Text="Key(s)" Font-Bold="true"></asp:Label> - Keys allow a database to perform efficiently. While there are several types of keys, this tool does not differentiate between the types. (System Generated)<br />
 --%><br /><br /> 
             <asp:Label ID="Label49" runat="server" Text="Example:" Font-Bold ="true" Font-Underline ="true"></asp:Label> <br />
 <asp:Label ID="Label20" runat="server" Text="Column Name " Font-Bold="true"></asp:Label> - BillingEntity<br />
 <asp:Label ID="Label21" runat="server" Text="Description " Font-Bold="true"></asp:Label> - Entity responsible for the joint billing activities for one or more providers to the served users. BillingEntity should represent the billing entity or source system that the charge will be billed out of.  <br />
 <asp:Label ID = "Label22" runat ="Server" Text="Technical Desc. " Font-Bold="True"></asp:Label> - For GCS - [NSH] = STAR and [NSH CIP] = Nexgen<br />
<asp:Label ID = "Label23" runat="server" Text="Caveat(s) " Font-Bold="true"></asp:Label> - 1 - [NSH] is STAR billed by Northsides Business Office, [NSH CIPS] is Nextgen billed by GCS. 2 - If BillingEntity is NSH the charge amount should be $0.00. This is not always true especially for older charges where mistakes were made. If we find non-$0 charges in future loads, GCS needs to be notified to evaluate. She would have to explain why non-$0 charges appear on NSH flagged BillingEntity records. 3 - This field should be equivalent to the Business Office’s definition of Technical (billed by STAR) vs Profee (billed by NextGen). <br />
<%--<asp:Label ID="Label44" runat="server" Text="Column Order" Font-Bold="true"></asp:Label> - 21 <br />--%>
         <asp:Label ID="Label45" runat="server" Text="Data Type" Font-Bold="true"></asp:Label> - varchar<br />
<%--         <asp:Label ID="Label46" runat="server" Text="Size" Font-Bold="true"></asp:Label> - 10 <br />
         <asp:Label ID="Label47" runat="server" Text="Nullable" Font-Bold="true"></asp:Label> - YES <br />--%>
        </asp:Panel>

 <br />
          
         <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat="server" Width="100%" Height="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
           <asp:TableRow>
               <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                   <asp:Label ID="explantionlabel" runat="server"></asp:Label>
               </asp:TableCell>
               <asp:TableCell Width="10px"></asp:TableCell>
           </asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell></asp:TableCell>
               <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center" >
                   <asp:Table runat="server" Width="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >
                       <asp:TableRow>
                           <asp:TableCell Width="100%" HorizontalAlign="Center" >
                               <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Close" />
                           </asp:TableCell>
                       </asp:TableRow>
                   </asp:Table>
          
         </asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="Panel1"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

     </ContentTemplate>

     </asp:UpdatePanel>
</div> 

</asp:Content>

