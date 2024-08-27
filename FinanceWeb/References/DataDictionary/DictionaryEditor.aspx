<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DictionaryEditor.aspx.vb" Inherits="FinanceWeb.DictionaryEditor2" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">

     <div style="color: #000000">
     
     <asp:Label ID="lblUser" runat="server" Text="" visible="false"></asp:Label>

    <asp:Label ID="DatabaseSort" Visible = "false" runat="server" ></asp:Label>
    <asp:Label ID="DatabaseDir" Visible="false" runat="server"></asp:Label>
      
<asp:Panel ID="pnlDatabase" runat="server" Width="100%" > 
   
<%--McKesson Performace Analyitics <br />
MPA Proper<br />--%>
  
    <%--   <asp:SqlDataSource ID="dsSQLdBs" runat="server" 
          ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
          SelectCommand="SELECT a.Name,
                        b.dBDesc, b.dBCaveat, 
                        b.dBOwner, b.dBAccess, b.LastUpdated, b.TechnicalDescription      
                        FROM sys.sysdatabases a
                        left outer join DWH.Doc.fddatabase b on (a.name COLLATE DATABASE_DEFAULT = b.dBName COLLATE DATABASE_DEFAULT )
                        WHERE HAS_DBACCESS(name) = 1
                        and (Active = 1 or Active is null ) 
                        and sid NOT IN (0x01,0x00) 
                        order by name " 
          UpdateCommand="
                    DECLARE @rowcount int

                    UPDATE [DWH].[DOC].[FDDatabase] SET 
                    dBDesc = @dbDesc, dBCaveat = @dbCaveat, Active = 1, dbOwner = @dbOwner, dbAccess = @dbAccess, 
                    LastUpdated = sysdatetime(), LastUpdatedPerson = @UserID, TechnicalDescription = @TechnicalDescription  
                    WHERE (dBName = @name) 
                    set @rowcount = @@ROWCOUNT 

                    IF @rowcount = 0
                    BEGIN
                    INSERT INTO [DWH].[DOC].[FDDatabase] 
                    ([dBName],[dBDesc],[dBCaveat],[Active], [dbOwner], [dbAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription]) 
                       VALUES 
                    (@name, @dbDesc, @dBCaveat, 1, @dbOwner, @dbAccess, sysdatetime(), @UserID, @TechnicalDescription)
                    END"
          DeleteCommand="DECLARE @rowcount int

                    UPDATE [DWH].[DOC].[FDDatabase] SET 
                    Active = 0, 
                    LastUpdated = sysdateTime(), 
                    LastUpdatedPerson = @UserID   
                    WHERE (dBName = @name) 
                    set @rowcount = @@ROWCOUNT 

                    IF @rowcount = 0
                    BEGIN
                    INSERT INTO [DWH].[DOC].[FDDatabase] 
                    ([dBName],[dBDesc],[dBCaveat],[Active], [dbOwner], [dbAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription]) 
                    VALUES 
                    (@name, '', '', 0, '','', sysdatetime(), @UserID, @TechnicalDescription)
                    END
                    Update DWH.Doc.FDColumnData set 
                    active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = @UserID  
                    WHERE   dBName = @name
                    Update DWH.Doc.FDTables set  
                    active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = @UserID  
                    WHERE  dBName = @name 
                    Update DWH.Doc.FDSchema set  
                    active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = @UserID  
                    WHERE  dBName = @name 	">

          <DeleteParameters>            
          
              <asp:ControlParameter ControlID="lblUser" Name="UserID" PropertyName="text" />
              <asp:Parameter Name="name" Type="String" />
              <asp:Parameter Name="TechnicalDescription" type ="String"  />
          </DeleteParameters>
          <UpdateParameters>
           
              <asp:Parameter Name="dBDesc"  Type="String" />
              <asp:Parameter Name="dBCaveat"  Type="String" />    
             <asp:Parameter Name="dbOwner" Type="String" />
              <asp:Parameter Name="dbAccess" Type="String" />
              <asp:ControlParameter ControlID="lblUser" Name="UserID" PropertyName="text" />
              <asp:Parameter Name="TechnicalDescription" Type="string" />
              <asp:Parameter Name="name" Type="String" />
              <asp:Parameter Name="dBCaveat" />
           </UpdateParameters>
    </asp:SqlDataSource>--%>
     This is a list of existing SQL Server databases for the BIDS group.<br /> 
     Use Edit and Delete controls in grid view below to modify existing databases.
    <br />
  <%--  HBI server<br /> DO not add this tag back we don't need to know which server the data is coming from--%> 
    <br />
    <asp:GridView ID="gvFDdatabase" runat="server" 
           AllowSorting="True" AutoGenerateColumns="False" BorderColor="Black" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames="name" 
         BackColor="White" BorderWidth="1px" 
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />
                
        <Columns>

           <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="false" 
                ShowEditButton="true" >
            <HeaderStyle Width="3%" />
            </asp:CommandField>
            <asp:ButtonField  CausesValidation="True" 
                CommandName="ViewDatabase" headerstyle-width="3%" DataTextField="name" 
                HeaderText="Name of Database" SortExpression="name" 
                Text="eval(&quot;name&quot;) ">
            <HeaderStyle Width="3%" />
            <ItemStyle Font-Bold="True" Font-Overline="False" Font-Underline="False" />
            </asp:ButtonField>

            <asp:BoundField DataField="name" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Name of Database" ReadOnly="True" SortExpression="name" 
                Visible="False">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>


            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" DataField="dBDesc" 
                HeaderStyle-Width="24%" HeaderStyle-Wrap="true" HeaderText="Description"  
                ItemStyle-Wrap="true" SortExpression="dBDesc" AccessibleHeaderText="Desc">
            <ControlStyle BackColor="#003060" ForeColor="White" Width="95%" />
            <HeaderStyle Width="24%" Wrap="True"  />
            <ItemStyle Wrap="True" />
            
            </asp:BoundField>
            
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                DataField="TechnicalDescription" HeaderStyle-Width="20%" 
                HeaderStyle-Wrap="true" HeaderText="Technical Desc." ItemStyle-Wrap="true" 
                SortExpression="TechnicalDescription">
            <ControlStyle BackColor="#003060" ForeColor="White" Width="95%" />
            <HeaderStyle Width="20%" Wrap="True" />
            <ItemStyle Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBCaveat" HeaderStyle-Width="10%" 
                HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="dBCaveat">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dbOwner" HeaderStyle-Width="10%" 
                HeaderText="Owner" SortExpression="dBOwner">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBAccess" HeaderStyle-Width="10%" 
                HeaderText="Access" SortExpression="dBAccess">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="LastUpdated" HeaderStyle-Width="10%" 
                HeaderText="Term Last Updated" ReadOnly="true" SortExpression="LastUpdated">
            <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
    
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC"  HorizontalAlign="Left"/>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        
           <PagerSettings Mode="NumericFirstLast" />
        
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="Small"/>
                      <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <br />
    <br />
<asp:Label ID="Label1" runat="server" Text="MPA Proper data "></asp:Label>
   <%-- MPA Proper gridview--%>
   <asp:GridView ID="gvFDDatabaseMPA" runat="server" 
        AllowSorting="True" AutoGenerateColumns="False" BorderColor="Black" 
        BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"   DataKeyNames="name"
         BackColor="White" BorderWidth="1px" Width="100%">
        <AlternatingRowStyle BackColor="#FFE885" />
        <Columns>
            <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="false" 
                ShowEditButton="true"  >
            <HeaderStyle Width="3%" />
            </asp:CommandField>
            <asp:ButtonField CausesValidation="false" 
                CommandName="ViewDatabase" headerstyle-width="3%" DataTextField="name" 
                HeaderText="Name of Database" SortExpression="name">
            <HeaderStyle Width="3%" />
            <ItemStyle Font-Bold="True" />
            </asp:ButtonField>
            <asp:BoundField DataField="name" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" HeaderText="Name of Database" 
                ReadOnly="True" SortExpression="name" Visible="False">
            <HeaderStyle HorizontalAlign="Left" Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" DataField="dBDesc" 
                HeaderStyle-Width="24%" HeaderStyle-Wrap="true" HeaderText="Description" 
                ItemStyle-Wrap="true" SortExpression="dBDesc">
            <ControlStyle BackColor="#003060" ForeColor="White" Width="95%" />
            <HeaderStyle Width="24%" Wrap="True" />
            <ItemStyle Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                DataField="TechnicalDescription" HeaderStyle-Width="20%" 
                HeaderStyle-Wrap="true" HeaderText="Technical Desc." ItemStyle-Wrap="true" 
                SortExpression="TechnicalDescription">
            <ControlStyle BackColor="#003060" ForeColor="White" Width="95%" />
            <HeaderStyle Width="20%" Wrap="True" />
            <ItemStyle Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBCaveat" HeaderStyle-Width="10%" 
                HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="dBCaveat">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dbOwner" HeaderStyle-Width="10%" 
                HeaderText="Owner" SortExpression="dBOwner">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBAccess" HeaderStyle-Width="10%" 
                HeaderText="Access" SortExpression="dBAccess">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="LastUpdated" HeaderStyle-Width="10%" 
                HeaderText="Term Last Updated" ReadOnly="true" SortExpression="LastUpdated">
            <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
      <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />

            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size="Small" />
              <EditRowStyle  Font-Size="Small" /> 
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

    
   <%-- NS1 server--%>
    <br />
    <asp:GridView ID="gvFDdatabaseNS1" runat="server" 
        AllowSorting="True" AutoGenerateColumns="False" BorderColor="Black" 
       BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"   DataKeyNames="name"
         BackColor="White" BorderWidth="1px" 
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />

        <Columns>
            <asp:CommandField HeaderStyle-Width="100px" ShowDeleteButton="false" 
                ShowEditButton="true">
            <HeaderStyle Width="40px" />
            </asp:CommandField>
            <asp:ButtonField CausesValidation="false" 
                CommandName="ViewDatabase" headerstyle-width="50px" DataTextField="name" 
                HeaderText="Name of Database*">
            <HeaderStyle Width="50px" />
            <ItemStyle Font-Bold="True" />
            </asp:ButtonField>
            <asp:BoundField DataField="name" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="200px" HeaderStyle-Wrap="true" 
                HeaderText="Name of Database*" ReadOnly="True" SortExpression="name" 
                Visible="False">
            <HeaderStyle HorizontalAlign="Left" Width="200px" Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="250px" DataField="dBDesc" 
                HeaderStyle-Width="250px" HeaderStyle-Wrap="true" HeaderText="Description" 
                ItemStyle-Wrap="true" SortExpression="dBDesc">
            <ControlStyle BackColor="#003060" ForeColor="White" Width="250px" />
            <HeaderStyle Width="250px" Wrap="True" />
            <ItemStyle Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="250px" 
                DataField="TechnicalDescription" HeaderStyle-Width="250px" 
                HeaderStyle-Wrap="true" HeaderText="Technical Desc." ItemStyle-Wrap="true" 
                SortExpression="TechnicalDescription">
            <ControlStyle BackColor="#003060" ForeColor="White" Width="250px" />
            <HeaderStyle Width="250px" Wrap="True" />
            <ItemStyle Wrap="True" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBCaveat" HeaderStyle-Width="200px" 
                HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="dBCaveat">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dbOwner" HeaderStyle-Width="200px" 
                HeaderText="Owner" SortExpression="dBOwner">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-BackColor="#003060" 
                ControlStyle-ForeColor="#FFFFFF" DataField="dBAccess" HeaderStyle-Width="200px" 
                HeaderText="Access" SortExpression="dBAccess">
            <ControlStyle BackColor="#003060" ForeColor="White" />
            <HeaderStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="LastUpdated" HeaderStyle-Width="100px" 
                HeaderText="Term Last Updated" ReadOnly="true" SortExpression="LastUpdated">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
        </Columns>
      <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
  <%--  *Please note that only the name can be sorted.--%>
   </asp:Panel>
    

<asp:Panel ID="pnlSchema" runat="server" Visible ="false" Width ="100%" >
 &nbsp;<asp:Button ID="btnReturnToDatabase" runat="server" 
        Text="Return to Database list" /> <br />
    <asp:Label ID="SchemaSort" Visible = "false" runat="server" ></asp:Label>
    <asp:Label ID="SchemaDir" Visible="false" runat="server"></asp:Label>
    <asp:Label ID="lblDatabaseSelected" runat="server" Text="" Visible="false" ></asp:Label>
    <br />
    Existing SQL Server information for the following: <br /> 
    Database: <asp:Label ID="lblSchemaDatabase" runat="server" Font-Bold="True"></asp:Label>  <br /> 
    Use Edit and Delete controls in grid view below to modify existing schemas. <br /> 
   
   <br />
    <asp:GridView ID="gvFDSchema" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True"   BorderColor="Black"
               BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames="TABLE_SCHEMA" 
         BackColor="White" BorderWidth="1px" PageSize="25" 
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />
         <Columns> 
         <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" 
                HeaderStyle-Width="3%">
            <HeaderStyle Width="3%" />
            </asp:CommandField>
            
            <asp:ButtonField Text="View" CausesValidation ="false" 
                        headerstyle-width="3%" CommandName="ViewTables" 
                 DataTextField="TABLE_SCHEMA" HeaderText="Schema" SortExpression="TABLE_SCHEMA" >
                         <HeaderStyle Width="3%" />
         
             <ItemStyle Font-Bold="True" />
         
            </asp:ButtonField>

            <asp:BoundField DataField="TABLE_SCHEMA" HeaderText="Schema"  ReadOnly="true" HeaderStyle-Width ="10%" 
                SortExpression="TABLE_SCHEMA" Visible="False" >
             <HeaderStyle Width="10%" />
             </asp:BoundField>
             <asp:BoundField ControlStyle-BackColor="#003060" 
                 ControlStyle-ForeColor="#FFFFFF" DataField="SchemaDesc" HeaderStyle-Width="20%" 
                 HeaderText="Description" SortExpression="SchemaDesc">
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="20%" />
             </asp:BoundField>
             <asp:BoundField ControlStyle-BackColor="#003060" 
                 ControlStyle-ForeColor="#FFFFFF" DataField="TechnicalDescription" 
                 HeaderStyle-Width="19%" HeaderText="Technical Desc." 
                 SortExpression="TechnicalDescription">
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="19%" />
             </asp:BoundField>
             <asp:BoundField ControlStyle-BackColor="#003060" 
                 ControlStyle-ForeColor="#FFFFFF" DataField="SchemaCaveat" 
                 HeaderStyle-Width="10%" HeaderStyle-Wrap="false" HeaderText="Caveat(s)" 
                 SortExpression="SchemaCaveat">
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Wrap="False" />
             </asp:BoundField>
            <asp:BoundField DataField="cntTable" HeaderText="# of Tables" ReadOnly="True"  HeaderStyle-Width ="5%"
                SortExpression="cntTable" >
                
             <HeaderStyle Width="5%" />
             </asp:BoundField>
                
            <asp:BoundField DataField="SchemaOwner" HeaderText="Owner"  HeaderStyle-Width ="10%" 
                ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%"
                 SortExpression="SchemaOwner" >

              <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="10%" />
             </asp:BoundField>

            <asp:BoundField DataField="SchemaAccess" HeaderText="Access"  HeaderStyle-Width ="10%" 
                ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                 SortExpression="SchemaAccess" >
                
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="10%" />
             </asp:BoundField>
                
            <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="10%"
                     SortExpression="LastUpdated" ReadOnly ="true"  >
             <HeaderStyle Width="10%" />
             </asp:BoundField>
        </Columns>
        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    <asp:GridView ID="gvFDSchemaNS1" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True"  BorderColor="Black" 
       BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
       HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
       HeaderStyle-Wrap="true"  ForeColor="Black"    DataKeyNames="TABLE_SCHEMA" 
       BackColor="White" BorderWidth="1px" PageSize="25" 
       Width="100%" >   

        <AlternatingRowStyle BackColor="#FFE885"  />
         <Columns> 
         <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" 
                HeaderStyle-Width="40px">
            <HeaderStyle Width="40px" />
            </asp:CommandField>
            
            <asp:ButtonField Text="View" CausesValidation ="false" 
                        headerstyle-width="50px" CommandName="ViewTables" 
                 DataTextField="TABLE_SCHEMA" HeaderText="Schema" SortExpression="TABLE_SCHEMA" >
           
              <HeaderStyle Width="50px" />
           
             <ItemStyle Font-Bold="True" />
           
            </asp:ButtonField>

            <asp:BoundField DataField="TABLE_SCHEMA" HeaderText="Schema"  ReadOnly="true" HeaderStyle-Width ="200px" 
                SortExpression="TABLE_SCHEMA" Visible="False" >
             <HeaderStyle Width="200px" />
             </asp:BoundField>
             <asp:BoundField ControlStyle-BackColor="#003060" 
                 ControlStyle-ForeColor="#FFFFFF" DataField="SchemaDesc" 
                 HeaderStyle-Width="250px" HeaderText="Description" SortExpression="SchemaDesc">
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="250px" />
             </asp:BoundField>
             <asp:BoundField ControlStyle-BackColor="#003060" 
                 ControlStyle-ForeColor="#FFFFFF" DataField="TechnicalDescription" 
                 HeaderStyle-Width="250px" HeaderText="Technical Desc." 
                 SortExpression="TechnicalDescription">
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="250px" />
             </asp:BoundField>
             <asp:BoundField ControlStyle-BackColor="#003060" 
                 ControlStyle-ForeColor="#FFFFFF" DataField="SchemaCaveat" 
                 HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="SchemaCaveat">
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Wrap="False" />
             </asp:BoundField>
            <asp:BoundField DataField="cntTable" HeaderText="# of Tables" ReadOnly="True"  HeaderStyle-Width ="85px"
                SortExpression="cntTable" >
                
             <HeaderStyle Width="85px" />
             </asp:BoundField>
                
            <asp:BoundField DataField="SchemaOwner" HeaderText="Owner"  HeaderStyle-Width ="200px" 
                ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                 SortExpression="SchemaOwner" >

              <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="200px" />
             </asp:BoundField>

            <asp:BoundField DataField="SchemaAccess" HeaderText="Access"  HeaderStyle-Width ="200px" 
                ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                 SortExpression="SchemaAccess" >
                
             <ControlStyle BackColor="#003060" ForeColor="White" />
             <HeaderStyle Width="200px" />
             </asp:BoundField>
                
            <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="170px"
                     SortExpression="LastUpdated" ReadOnly ="true"  >
             <HeaderStyle Width="170px" />
             </asp:BoundField>
        </Columns>
       <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
   </asp:Panel>

<asp:Panel id="pnlTables" runat="server" Visible="false"   Width ="100%" >
  &nbsp; <asp:Button ID="btnReturnSchema" runat="server" Text="Return to Schema list" /><br />
   <asp:Label ID="TableSort" Visible = "false" runat="server" ></asp:Label>
   <asp:Label ID="TableDir" Visible="false" runat="server"></asp:Label>
   <asp:Label ID="lblSchemaSelected" runat="server" Text="" visible="false" ></asp:Label> <br />
   Existing SQL Server information for the following <br />
   Schema: <asp:Label ID="lblTableSchema" runat="server" Text="" Font-Bold="true"> </asp:Label>  <br />  
   Database:<asp:Label ID="lblTableDatabase" runat="server" Text="" Font-Bold="true" ></asp:Label><br /> 
   Use Edit and Delete controls in grid view below to modify existing tables. <br /> 
    <br />
    <asp:GridView ID="gvFDTables" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True"     BorderColor="Black" 
   BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  DataKeyNames="Table_name"  
         BackColor="White" BorderWidth="1px" PageSize="25" 
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />
           <Columns>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" 
                HeaderStyle-Width="3%">
            <HeaderStyle Width="3%" />
            </asp:CommandField>
            
            <asp:ButtonField Text="View" CausesValidation ="false" 
                        headerstyle-width="3%" CommandName="ViewTableData" 
                   DataTextField="Table_name" HeaderText="Table Name" SortExpression="Table_name" >
                    <HeaderStyle Width="3%" />
               <ItemStyle Font-Bold="True" />
            </asp:ButtonField>
            
               <asp:BoundField DataField="Table_name" HeaderText="Table Name" ReadOnly="True"  HeaderStyle-Width ="10%"
                   SortExpression="Table_name" Visible="False" >
               <HeaderStyle Width="10%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" DataField="TableDesc" 
                   HeaderStyle-Width="20%" HeaderText="Description" SortExpression="TableDesc">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="20%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                   DataField="TechnicalDescription" HeaderStyle-Width="19%" 
                   HeaderText="Technical Desc." SortExpression="TechnicalDescription">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="19%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="TableCaveat" HeaderStyle-Width="8%" 
                   HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="TableCaveat">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="cntColumn" HeaderText="# of Columns" 
                   ReadOnly="True"  HeaderStyle-Width ="5%"
                   SortExpression="cntColumn" >
               <HeaderStyle Width="5%" />
               </asp:BoundField>

               <asp:BoundField DataField="TableOwner" HeaderText="Owner"  HeaderStyle-Width ="8%"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF"  
                   SortExpression="TableOwner" >
             
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="8%" />
               </asp:BoundField>
             
               <asp:BoundField DataField="TableAccess" HeaderText="Access"  HeaderStyle-Width ="8%"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                   SortExpression="TableAccess" >

               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="8%" />
               </asp:BoundField>

              <asp:BoundField DataField="Table_Type" HeaderText="Type" HeaderStyle-Wrap="false" HeaderStyle-Width ="8%"
                                   SortExpression="Table_Type" ReadOnly = "true">
                    <HeaderStyle Width="8%" Wrap="False" />
                    </asp:BoundField>


               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="8%"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
               <HeaderStyle Width="8%" />
               </asp:BoundField>
           </Columns>
       <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size ="Small" />
             <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

  <asp:GridView ID="gvFDTablesNS1" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True"  BorderColor="Black" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
         BackColor="White" BorderWidth="1px" PageSize="25"  DataKeyNames="Table_name"
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />

           <Columns>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" 
                HeaderStyle-Width="40px">
            <HeaderStyle Width="40px" />
            </asp:CommandField>
            
            <asp:ButtonField Text="View" CausesValidation ="false" 
                        headerstyle-width="50px" CommandName="ViewTableData" 
                   DataTextField="Table_name" HeaderText="Table Name" SortExpression="Table_name" >
                    <HeaderStyle Width="50px" />
               <ItemStyle Font-Bold="True" />
            </asp:ButtonField>
            
               <asp:BoundField DataField="Table_name" HeaderText="Table Name" ReadOnly="True"  HeaderStyle-Width ="200px"
                   SortExpression="Table_name" Visible="False" >
               <HeaderStyle Width="200px" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="TableDesc" 
                   HeaderStyle-Width="250px" HeaderText="Description" SortExpression="TableDesc">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="TechnicalDescription" 
                   HeaderStyle-Width="250px" HeaderText="Technical Desc." 
                   SortExpression="TechnicalDescription">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="TableCaveat" 
                   HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="TableCaveat">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="cntColumn" HeaderText="# of Columns" 
                   ReadOnly="True"  HeaderStyle-Width ="100px"
                   SortExpression="cntColumn" >
               <HeaderStyle Width="100px" />
               </asp:BoundField>


                 <asp:BoundField DataField="Table_Type" HeaderText="Type" HeaderStyle-Wrap="false"
                  ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF"  
                   SortExpression="Table_Type" >

               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Wrap="False" />
               </asp:BoundField>

               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="TableOwner" 
                   HeaderStyle-Width="250px" HeaderText="Owner" SortExpression="TableOwner">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="TableAccess" 
                   HeaderStyle-Width="250px" HeaderText="Access" SortExpression="TableAccess">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>

               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="170px"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
               <HeaderStyle Width="170px" />
               </asp:BoundField>
           </Columns>
        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
   </asp:Panel>
         
<asp:Panel ID="pnlColumns" runat="server" Visible="false"  Width="100%" > 
&nbsp;<asp:Button ID="btnReturnTables" runat="server" Text="Return to Table list"  /><br />
    <asp:Label ID="ColumnSort" Visible ="false" runat="server"></asp:Label>
    <asp:Label ID="ColumnDir" Visible = "false" runat="server"></asp:Label> 
    <asp:Label ID="lblTableSelected" runat="server" Text="" visible="false" ></asp:Label> <br />
Existing SQL Server information for the following <br />
Table:  <asp:Label ID="lblColumnTable" runat="server" Text="" Font-Bold="true"> </asp:Label>  <br />  
Schema: <asp:Label ID="lblColumnSchema" runat="server" Text="" Font-Bold="true"> </asp:Label>  <br />  
Database:<asp:Label ID="lblColumnDatabase" runat="server" Text="" Font-Bold="true" ></asp:Label><br /> 
Use Edit and Delete controls in grid view below to modify existing columns. <br />
<%--
<asp:Label ID="lblColumn" runat="server"></asp:Label>
 <asp:Label ID="lblcolTableSelected" runat="server" text="" font-bold="true"></asp:Label> in the <asp:Label ID="lblcolSchemaSelected" runat="server" Text="" Font-Bold="true"></asp:Label> schema in the 
 <asp:Label ID="lblcolDatabaseSelected" runat="server" Text="" Font-Bold="true"></asp:Label>
 --%>
<br />

 <asp:GridView ID="gvFDColumns" runat="server" AutoGenerateColumns ="False" 
        AllowSorting="True"  BorderColor="Black" 
  BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames ="COLUMN_NAME" 
         BackColor="White" BorderWidth="1px" PageSize="25" 
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />

           <Columns>
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" 
                HeaderStyle-Width="3%">
            <HeaderStyle Width="3%" />
            </asp:CommandField>
               <asp:BoundField DataField="COLUMN_NAME" HeaderText="Column Name" 
                   ReadOnly="True"  HeaderStyle-Width ="8%"
                   SortExpression="COLUMN_NAME" >
               <HeaderStyle Width="8%" />
               <ItemStyle Font-Bold="true" />
               </asp:BoundField>

               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                   DataField="ColumnDesc" HeaderStyle-Width="19%" HeaderText="Description" 
                   SortExpression="ColumnDesc">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="19%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                   DataField="TechnicalDescription" HeaderStyle-Width="19%" 
                   HeaderText="Technical Desc." SortExpression="TechnicalDescription">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="19%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="columnCaveat" 
                   HeaderStyle-Width="5%" HeaderStyle-Wrap="false" HeaderText="Caveat(s)" 
                   SortExpression="columnCaveat">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Wrap="False" />
               </asp:BoundField>

               <asp:BoundField DataField="ORDINAL_POSITION" HeaderText="Column Order" 
                   ReadOnly="True" HeaderStyle-Width="5%" HeaderStyle-Wrap="true"
                   SortExpression="ORDINAL_POSITION" >
               <HeaderStyle Width="5%" Wrap="True" />
               </asp:BoundField>

               <asp:BoundField DataField="DATA_TYPE" HeaderText="Data Type" ReadOnly="True" HeaderStyle-Width="5%" 
                   SortExpression="DATA_TYPE" >
               <HeaderStyle Width="5%" />
               </asp:BoundField>

               <asp:BoundField DataField="Size" HeaderText="Size" ReadOnly="True" HeaderStyle-Width="3%" 
                   SortExpression="Size" >
               <HeaderStyle Width="3%" />
               </asp:BoundField>

               <asp:BoundField DataField="COLUMN_DEFAULT" HeaderText="Default Value" 
                   ReadOnly="True" HeaderStyle-Width="10%" 
                   SortExpression="COLUMN_DEFAULT" >
               <HeaderStyle Width="10%" />
               </asp:BoundField>

               <asp:BoundField DataField="IS_NULLABLE" HeaderText="Nullable" ReadOnly="True"  HeaderStyle-Width="5%" 
                   SortExpression="IS_NULLABLE" >
               <HeaderStyle Width="5%" />
               </asp:BoundField>

               <asp:BoundField DataField="isKey" HeaderText="Key(s)" ReadOnly="True"  HeaderStyle-Width="3%" 
                   SortExpression="isKey" >
               <HeaderStyle Width="3%" />
               </asp:BoundField>

               <asp:BoundField DataField="ColumnOwner" HeaderText="Owner"  HeaderStyle-Width ="5%"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF"  
                   SortExpression="ColumnOwner" >
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="5%" />
               </asp:BoundField>
             
               <asp:BoundField DataField="ColumnAccess" HeaderText="Access"  HeaderStyle-Width ="5%"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                   SortExpression="ColumnAccess" >
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="5%" />
               </asp:BoundField>

               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="5%"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
               <HeaderStyle Width="5%" />
               </asp:BoundField>
           </Columns>
    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

       <asp:GridView ID="gvFDColumnsMPA" runat="server" 
        AutoGenerateColumns ="False" AllowSorting="True"  BorderColor="Black" 
       BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
         BackColor="White" BorderWidth="1px" PageSize="25" DataKeyNames ="COLUMN_NAME"  
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />

           <Columns>

            <%--   <asp:BoundField DataField="DATA_TYPE" HeaderText="Data Type"  HeaderStyle-Width="5%" 
                            ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width ="85%" 
                   SortExpression="DATA_TYPE" >
               <HeaderStyle Width="5%" />
               </asp:BoundField>--%>
               
        <%--       <asp:BoundField DataField="isKey" HeaderText="Key(s)" ReadOnly="True"  HeaderStyle-Width="3%" 
                   SortExpression="isKey" >
               <HeaderStyle Width="3%" />
               </asp:BoundField>

                     <asp:DropDownList ID="ddlIsKey" runat="server"  SelectedValue='<%# Bind("isKey", "{0}") %>' >
--%>

               <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="false" 
                   ShowEditButton="true">
               <HeaderStyle Width="3%" />
               </asp:CommandField>
               <asp:BoundField DataField="COLUMN_NAME" HeaderStyle-Width="8%" 
                   HeaderText="Column Name" ReadOnly="True" SortExpression="COLUMN_NAME">
               <HeaderStyle Width="8%" />
               <ItemStyle Font-Bold="True" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                   DataField="ColumnDesc" HeaderStyle-Width="19%" HeaderText="Description" 
                   SortExpression="ColumnDesc">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="19%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="95%" 
                   DataField="TechnicalDescription" HeaderStyle-Width="19%" 
                   HeaderText="Technical Desc." SortExpression="TechnicalDescription">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="19%" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="columnCaveat" 
                   HeaderStyle-Width="5%" HeaderStyle-Wrap="false" HeaderText="Caveat(s)" 
                   SortExpression="columnCaveat">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="85%" 
                   DataField="ORDINAL_POSITION" HeaderStyle-Width="5%" HeaderStyle-Wrap="true" 
                   HeaderText="Column Order" SortExpression="ORDINAL_POSITION">
               <ControlStyle BackColor="#003060" ForeColor="White" Width="85%" />
               <HeaderStyle Width="5%" Wrap="True" />
               </asp:BoundField>
               <asp:TemplateField ConvertEmptyStringToNull="true" HeaderText="Data Type" 
                   SortExpression="DATA_TYPE">
                   <EditItemTemplate>
                       <asp:DropDownList ID="ddlDataType" runat="server">
                           <asp:ListItem Value="Reference">Reference</asp:ListItem>
                           <asp:ListItem Value="numeric w/ decimal">numeric w/ decimal</asp:ListItem>
                           <asp:ListItem Value="numeric w/ decimal (Average)">numeric w/ decimal (Average) </asp:ListItem>
                           <asp:ListItem Value="numeric w/ decimal (No Total)">numeric w/ decimal (No Total) </asp:ListItem>
                           <asp:ListItem Value="numeric w/o decimal">numeric w/o decimal </asp:ListItem>
                           <asp:ListItem Value="numeric w/o decimal (Average)">numeric w/o decimal (Average) </asp:ListItem>
                           <asp:ListItem Value="numeric w/o decimal (No Total)">numeric w/o decimal (No Total) </asp:ListItem>
                           <asp:ListItem Value="text">text </asp:ListItem>
                           <asp:ListItem Value="text (Sort as numeric)">text (Sort as numeric) </asp:ListItem>
                           <asp:ListItem Value="date and time">date and time </asp:ListItem>
                           <asp:ListItem Value="date only">date only </asp:ListItem>
                           <asp:ListItem Value="time only">time only </asp:ListItem>
                           <asp:ListItem Value="flag (1-Yes/0-No)">flag (1-Yes/0-No)</asp:ListItem>
                       </asp:DropDownList>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Label ID="DATA_TYPE2" runat="server" Text='<%# Eval("DATA_TYPE") %>'> &gt;</asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" ControlStyle-Width="85%" DataField="Size" 
                   HeaderStyle-Width="3%" HeaderText="Size" SortExpression="Size">
               <ControlStyle BackColor="#003060" ForeColor="White" Width="85%" />
               <HeaderStyle Width="3%" />
               </asp:BoundField>
               <asp:BoundField DataField="COLUMN_DEFAULT" HeaderStyle-Width="10%" 
                   HeaderText="Default Value" ReadOnly="True" SortExpression="COLUMN_DEFAULT">
               <HeaderStyle Width="10%" />
               </asp:BoundField>
               <asp:BoundField DataField="IS_NULLABLE" HeaderStyle-Width="5%" 
                   HeaderText="Nullable" readonly="true" SortExpression="IS_NULLABLE">
               <ControlStyle BackColor="#003060" ForeColor="White" Width="85%" />
               <HeaderStyle Width="5%" />
               </asp:BoundField>

    <asp:TemplateField ConvertEmptyStringToNull="true" HeaderText = "Key(s)" SortExpression ="isKey" >
              <EditItemTemplate >
                    <asp:DropDownList ID="ddlIsKey" runat="server"  >
              <asp:ListItem Value="YES">YES</asp:ListItem>
              <asp:ListItem Value = "NO">NO</asp:ListItem>
              </asp:DropDownList>
                          </EditItemTemplate>
              <ItemTemplate>
                <asp:Label ID="isKey" runat="server"  Text='<%# Eval("isKey") %>'> ></asp:Label>
              
              </ItemTemplate>
              </asp:TemplateField>


               <asp:BoundField DataField="ColumnOwner" HeaderText="Owner"  HeaderStyle-Width ="5%"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF"  
                   SortExpression="ColumnOwner" >
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="5%" />
               </asp:BoundField>
             
               <asp:BoundField DataField="ColumnAccess" HeaderText="Access"  HeaderStyle-Width ="5%"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                   SortExpression="ColumnAccess" >
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="5%" />
               </asp:BoundField>

               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="5%"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
               <HeaderStyle Width="5%" />
               </asp:BoundField>
           </Columns>
        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
               <EditRowStyle  Font-Size="Small" /> 
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

     
  <asp:GridView ID="gvFDColumnsNS1" runat="server" AutoGenerateColumns ="False" 
        AllowSorting="True"  BorderColor="Black"  BorderStyle="Solid" 
        Font-Size="Small" HeaderStyle-BackColor="#214B9A"  
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
         BackColor="White" BorderWidth="1px" PageSize="25" 
           Width="100%" >   
        <AlternatingRowStyle BackColor="#FFE885"  />
           <Columns>

          <%--     <asp:BoundField DataField="value" HeaderText="dB Description" ReadOnly="True" 
                   SortExpression="value" />
--%>
               <asp:CommandField HeaderStyle-Width="40px" ShowDeleteButton="false" 
                   ShowEditButton="true">
               <HeaderStyle Width="40px" />
               </asp:CommandField>
               <asp:BoundField DataField="COLUMN_NAME" HeaderStyle-Width="200px" 
                   HeaderText="Column Name" ReadOnly="True" SortExpression="COLUMN_NAME">
               <ItemStyle Font-Bold="True" />
               <HeaderStyle Width="200px" />
               </asp:BoundField>
               <asp:BoundField DataField="ColumnDesc" HeaderText="Description"  HeaderStyle-Width ="250px"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                   SortExpression="ColumnDesc" >   

               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>

              <asp:BoundField DataField="TechnicalDescription" HeaderText="Technical Desc."  HeaderStyle-Width ="250px"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF"   
                   SortExpression="TechnicalDescription" >

               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>

               <asp:BoundField ControlStyle-BackColor="#003060" 
                   ControlStyle-ForeColor="#FFFFFF" DataField="columnCaveat" 
                   HeaderStyle-Wrap="false" HeaderText="Caveat(s)" SortExpression="columnCaveat">
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="ORDINAL_POSITION" HeaderStyle-Width="80px" 
                   HeaderStyle-Wrap="true" HeaderText="Column Order" ReadOnly="True" 
                   SortExpression="ORDINAL_POSITION">
               <HeaderStyle Width="80px" Wrap="True" />
               </asp:BoundField>
               <asp:BoundField DataField="DATA_TYPE" HeaderStyle-Width="100px" 
                   HeaderText="Data Type" ReadOnly="True" SortExpression="DATA_TYPE">
               <HeaderStyle Width="100px" />
               </asp:BoundField>
               <asp:BoundField DataField="Size" HeaderStyle-Width="80px" HeaderText="Size" 
                   ReadOnly="True" SortExpression="Size">
               <HeaderStyle Width="80px" />
               </asp:BoundField>
               <asp:BoundField DataField="COLUMN_DEFAULT" HeaderStyle-Width="200px" 
                   HeaderText="Default Value" ReadOnly="True" SortExpression="COLUMN_DEFAULT">
               <HeaderStyle Width="200px" />
               </asp:BoundField>
               <asp:BoundField DataField="IS_NULLABLE" HeaderStyle-Width="80px" 
                   HeaderText="Nullable" ReadOnly="True" SortExpression="IS_NULLABLE">
               <HeaderStyle Width="80px" />
               </asp:BoundField>
               <asp:BoundField DataField="isKey" HeaderStyle-Width="80px" HeaderText="Key(s)" 
                   ReadOnly="True" SortExpression="isKey">
               <HeaderStyle Width="80px" />
               </asp:BoundField>

               <asp:BoundField DataField="ColumnOwner" HeaderText="Owner"  HeaderStyle-Width ="250px"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF"  
                   SortExpression="ColumnOwner" >
             
               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>
             
               <asp:BoundField DataField="ColumnAccess" HeaderText="Access"  HeaderStyle-Width ="250px"
                   ControlStyle-BackColor="#003060" ControlStyle-ForeColor="#FFFFFF" 
                   SortExpression="ColumnAccess" >

               <ControlStyle BackColor="#003060" ForeColor="White" />
               <HeaderStyle Width="250px" />
               </asp:BoundField>

               <asp:BoundField DataField="LastUpdated" HeaderText="Term Last Updated" HeaderStyle-Width="170px"
                    SortExpression="LastUpdated" ReadOnly ="true"  >
               <HeaderStyle Width="170px" />
               </asp:BoundField>
           </Columns>
          <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"  Font-Size ="Small"  />
              <EditRowStyle  Font-Size="Small" /> 
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
     
</asp:Panel>
 
<br />
<br />
<asp:Label ID="Label2" runat="server" Text="Terms & Definitions " Font-Underline="True"  Font-Bold="True"></asp:Label>
         <br />
      <asp:Label ID ="AllPages" runat = "server" Text="All Pages" Font-Underline = "true"></asp:Label>  <br />
<asp:Label ID="Label3" runat="server" Text="Description " font-bold="true"> </asp:Label> - Explanation of what the data element is. (Editable)  <br />
<asp:Label ID = "Label4" runat ="Server" Text="Technical Desc. " Font-Bold="True"></asp:Label>  - Explanation of any logic or formulas that maybe applied to the data element. (Editable) <br />
<asp:Label ID = "Label5" runat="server" Text="Caveat(s) " Font-Bold="true"></asp:Label> - Any limitations of the data element that need to be highlighted. (Editable)<br />
<asp:Label ID="Label6" runat="server" Text="Owner " Font-Bold="true"></asp:Label> - Data element steward. (Editable)<br />
<asp:Label ID="Label9" runat="server" Text="Access " Font-Bold="true"></asp:Label> - Any limitaions on access to the data element. (Editable)<br />

         <asp:Label ID="Label8" runat="server" Text="Term Last Updated " Font-Bold="true"></asp:Label> - Date that the data element was last updated. (System Generated) <br />
<asp:Label ID ="Label24" runat = "server" Text="Database specific" Font-Underline = "true"></asp:Label>
         <br />  
<asp:Label ID="Label7" runat="server" Text="Name of Database " Font-Bold="true"></asp:Label> - Name pulled directly from SQL Server database. (System Generated)<br />
         <br />
<asp:Label ID ="Label25" runat = "server" Text="Schema specific" Font-Underline = "true"></asp:Label>
<br /> <asp:Label ID="Label10" runat="server" Text="Schema " Font-Bold="true"></asp:Label> - A logical grouping of tables in the database. (System Generated)<br />
 <asp:Label ID="Label11" runat="server" Text="# of Tables " Font-Bold="true"></asp:Label> - The number of tables and views in the schema. (System Generated)<br />
         <br />
<asp:Label ID ="Label26" runat = "server" Text="Table specific" Font-Underline = "true"></asp:Label>         <br /> 
 <asp:Label ID="Label12" runat="server" Text="Table Name " Font-Bold="true"></asp:Label> - Name of the Table or view. (System Generated)<br />
 <asp:Label ID="Label13" runat="server" Text="# of Columns " Font-Bold="true"></asp:Label> - Number of columns in the table or view. (System Generated)<br />
 <asp:Label ID="Label14" runat="server" Text="Type " Font-Bold="true"></asp:Label> - Base Table or View. (System Generated)<br />
         <br />
<asp:Label ID ="Label27" runat = "server" Text="Column specific" Font-Underline = "true"></asp:Label>         <br />
 <asp:Label ID="Label15" runat="server" Text="Column Name " Font-Bold="true"></asp:Label> - Name of the column. (System Generated)<br />
 <asp:Label ID="Label16" runat="server" Text="Column Order " Font-Bold="true"></asp:Label> - Ordinal location of the column in the database. (System Generated)<br />
 <asp:Label ID="Label17" runat="server" Text="Data Type " Font-Bold="true"></asp:Label> - Data type of the column in the database. (System Generated)<br />

 <asp:Label ID="Label18" runat="server" Text="Size " Font-Bold="true"></asp:Label> - Data element size, if available. (System Generated)<br />
 <asp:Label ID="Label19" runat="server" Text="Default Value " Font-Bold="true"></asp:Label> - Default size of the data element. (System Generated)<br />
 <asp:Label ID="Label28" runat="server" Text="Nullable " Font-Bold="true"></asp:Label> -If the data element is allowed to be NULL, without data. (System Generated)<br />
 <asp:Label ID="Label29" runat="server" Text="Key(s)" Font-Bold="true"></asp:Label> - Keys allow a database to perform efficiently. While there are several types of keys, this tool does not differentiate between the types. (System Generated)<br />

 <br /><br /> 
 <asp:Label ID="Label34" runat="server" Text="Example:" Font-Bold ="true" Font-Underline ="true"></asp:Label> <br />
 
  <asp:Label ID="Label30" runat="server" Text="Name of Database" Font-Bold="true"></asp:Label> - DWH <br />
  <asp:Label ID="Label31" runat="server" Text="Description" Font-Bold="true"></asp:Label> - BIDS Data Warehouse<br /><br />
  <asp:Label ID="Label32" runat="server" Text="Schema" Font-Bold="true"></asp:Label> - MD2 <br />
  <asp:Label ID="Label33" runat="server" Text="Description" Font-Bold ="true"></asp:Label> - Physician Practice billing repository with extended data<br />
         <asp:Label ID="Label35" runat="server" Text="Caveat(s)" Font-Bold="true"></asp:Label> - Excludes MMIS, and practices maintained in MD<br />
         <asp:Label ID="Label36" runat="server" Text="# of Tables" Font-Bold="true"></asp:Label> - 15 <br />
         <asp:Label ID="Label37" runat="server" Text="Owner" Font-Bold="true"> </asp:Label>- BIDS <br />
<br />
         <asp:Label ID="Label38" runat="server" Text="Table" Font-Bold="true"> </asp:Label> - Charges <br />
         <asp:Label ID="Label39" runat="server" Text="Description" Font-Bold="true"></asp:Label> - Charges level detail data. <br />
         <asp:Label ID="Label40" runat="server" Text="Technical Desc." Font-Bold="true"></asp:Label> -Charges made to the groups <br />
<asp:Label ID="Label41" runat="server" Text="# of Columns" Font-Bold="true"></asp:Label> - 52 <br />
         <asp:Label ID="Label42" runat="server" Text="Owner" Font-Bold="true" ></asp:Label> - Practice Billing Systems <br /> 
         <asp:Label ID="Label43" runat="server" Text="Type" Font-Bold="true"></asp:Label> - BASE TABLE <br />
         <br />
          
 <asp:Label ID="Label20" runat="server" Text="Column Name " Font-Bold="true"></asp:Label> - BillingEntity<br />
 <asp:Label ID="Label21" runat="server" Text="Description " Font-Bold="true"></asp:Label> - Entity responsible for the joint billing activities for one or more providers to the served users. BillingEntity should represent the billing entity or source system that the charge will be billed out of.  <br />
 <asp:Label ID = "Label22" runat ="Server" Text="Technical Desc. " Font-Bold="True"></asp:Label> - For GCS - [NSH] = STAR and [NSH CIP] = Nexgen<br />
<asp:Label ID = "Label23" runat="server" Text="Caveat(s) " Font-Bold="true"></asp:Label> - 1 - [NSH] is STAR billed by Northsides Business Office, [NSH CIPS] is Nextgen billed by GCS. 2 - If BillingEntity is NSH the charge amount should be $0.00. This is not always true especially for older charges where mistakes were made. If we find non-$0 charges in future loads, GCS needs to be notified to evaluate. She would have to explain why non-$0 charges appear on NSH flagged BillingEntity records. 3 - This field should be equivalent to the Business Office’s definition of Technical (billed by STAR) vs Profee (billed by NextGen). <br />
<asp:Label ID="Label44" runat="server" Text="Column Order" Font-Bold="true"></asp:Label> - 21 <br />
         <asp:Label ID="Label45" runat="server" Text="Data Type" Font-Bold="true"></asp:Label> - varcahr<br />
         <asp:Label ID="Label46" runat="server" Text="Size" Font-Bold="true"></asp:Label> - 10 <br />
         <asp:Label ID="Label47" runat="server" Text="Nullable" Font-Bold="true"></asp:Label> - YES <br />
     <br />
     <br />
</div> 
</asp:Content>

