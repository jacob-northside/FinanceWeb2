<%@ Page Title="NZ Data Dictionary" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Netezza Data Dictionary.aspx.vb" Inherits="FinanceWeb.NZ_DataDictionary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>
<div>
    
</div>

      <div id="LeftPnl"  style="float: left; width: 18%; height: 100%;" >

  <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Height="45px" Width="1170px"  >
      <asp:Label ID="Label1" runat="server" 
          ></asp:Label><br />
           
     <asp:DropDownList ID="ddlDD_Tables" runat="server" Width="200px"  Height="26px" >
                    <asp:ListItem Text="DWH.DOC.NZ_FDSchema" Value="1"></asp:ListItem>           
                    <asp:ListItem Text="DWH.DOC.NZ_FDTables" Value="0"></asp:ListItem>
                    <asp:ListItem Text="DWH.DOC.NZ_FDColumnData" Value="2"></asp:ListItem>
                    <asp:ListItem Text="DWH.DOC.NZ_FDMapping" Value="3"></asp:ListItem>
     </asp:DropDownList>
      <asp:Button ID="btnLoadData" runat="server" Text="Load Data" Font-Size="X-Small" Height="26px" Width="99px" />
      <asp:Button ID="ReturntoSchemaBtn" visible="false" runat="server" Font-Size="X-Small" Height="26px" Text="Go to schema list" Width="127px" />
      <asp:Button ID="ReturntoTablesBtn" runat="server" visible="false" Font-Size="X-Small" Height="26px" Text="Go to table list" Width="125px" />         
       <div  style="width: 618px; display: inline-block; height: 25px;">
           <asp:HyperLink ID="DDExcelLink" Visible="false" runat="server" NavigateUrl="\\Nshdsfile\files\Shared\FDW\Documentation\Aginity_Data_Dictionary.xlsx" Text="Click here to access the NZ Data Dictionary Excel file."> </asp:HyperLink>
      </div>
       </asp:Panel>
        </div>


<%--    <div id="RightPnl" style="float: left; width: 80%;">
    <asp:Panel ID="Panel2" runat="server" >--%>

<%---------------------------------------------------------------------------------------%>
            <asp:GridView ID="Update_Schema" runat="server" bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="Black"  BackColor="White" BorderWidth="1px" 
           Width="99%" ShowHeaderWhenEmpty="True" visible="false" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" textmode="Multiline" AllowSorting="True"  >
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <AlternatingRowStyle BackColor="#FFE885" BorderColor="Red" /> 

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

        <asp:TemplateField HeaderText="SchemaName">
            <ItemTemplate>
                <asp:Panel runat="server">
                    <asp:LinkButton ID="Update_Schema_Name" runat="server" Text='<%# Bind("SchemaName")%>' CommandName="ViewFilteredData"></asp:LinkButton>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
                
  <%--       <asp:BoundField DataField="SchemaName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaName" ReadOnly="True" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>   --%>  
                
<%--        <asp:BoundField DataField="SchemaDesc" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="50%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaDesc"  
                Visible="True" >
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>   --%>  
                <%----------------------------------------------------------------------------------------%>
<asp:TemplateField  HeaderText="Schema Description">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("SchemaDesc")%>' />
    </ItemTemplate>
    <EditItemTemplate >

        <asp:TextBox runat="server"  ID="SchemaDesc_EditBox" Rows="4" TextMode="MultiLine" Text='<%# Eval("SchemaDesc")%>' />
    </EditItemTemplate>
</asp:TemplateField>
<%----------------------------------------------------------------------------------------%>
<asp:TemplateField HeaderStyle-Width="10px"  HeaderText="Visible In DD">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("visible_in_dd")%>' />
    </ItemTemplate>
    <EditItemTemplate >
        <asp:DropDownList runat="server" id="schema_active" Text='<%# Eval("visible_in_dd")%>'>
            <asp:ListItem Text="NULL" Value=""/>
            <asp:ListItem Text="Active" Value="1"/>
            <asp:ListItem Text="Inactive" Value="0"/>
        </asp:DropDownList> 
    </EditItemTemplate>
</asp:TemplateField>


 <%--       <asp:BoundField DataField="SchemaCaveat" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaCaveat" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="SchemaAccess" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="SchemaAccess" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="LastUpdated" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="LastUpdated" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="LastUpdatedPerson" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="LastUpdatedPerson" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="TechnicalDescription" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="TechnicalDescription" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     --%>

            </Columns>


            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />


 <PagerSettings Position="TopAndBottom" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" BorderColor="Red" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>


    <%---------------------------------------------------------------------------------------%>
                <asp:Panel ID="PanelTables" runat="server" Width="100%" Visible="false">

<asp:Table ID="Search_tbls" Width="99%" Visible="true" BackColor = "#2b74bb" CellSpacing = "2"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
        <asp:TableCell>Search:</asp:TableCell>
    <asp:TableCell HorizontalAlign = "Left" > <asp:TextBox runat = "server" ID= "tbl_SearchSchemas" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Left" > <asp:TextBox runat = "server" ID= "tbl_SearchTables" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
        <asp:TableCell>
        <asp:Table runat="server">
            <asp:TableRow>
<%--                <asp:TableCell><asp:CheckBox ID="Columns_WildcardSrch" runat="server" AutoPostBack="true" OnCheckedChanged="Columns_WildcardChecked" Text="Wildcard" Checked="true" Font-Size="Small" /></asp:TableCell>
                <asp:TableCell><asp:CheckBox ID="Columns_ExactSrch" runat="server" AutoPostBack="true" OnCheckedChanged="Columns_ExactChecked" Text="Exact" Checked="false" Font-Size="Small" /></asp:TableCell>--%>
                <asp:TableCell VerticalAlign="Top"><asp:RadioButtonList CssClass="radioButtonList" runat="server" ID="Tables_Srch_RadioBtn" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="Wildcard" Selected="True" Value="Wildcard"/>
                                   <asp:ListItem Text="Exact" Selected="False" Value="Exact"/>
                               </asp:RadioButtonList></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:TableCell>    
    </asp:TableRow>  

    <asp:TableHeaderRow  BackColor="#4A8fd2" Width="100%" ForeColor = "White">
    <asp:TableHeaderCell ID = "tbl_tblTSelect" Width = "10%" Height = "20px" ></asp:TableHeaderCell>
    <%--<asp:TableHeaderCell ID = "tblTEdit" Visible = "false" Width = "60px" Height = "20px" >&nbsp;</asp:TableHeaderCell>--%>
    <asp:TableHeaderCell Width = "30%" >Schema Name</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "30%" >Table Name</asp:TableHeaderCell>
    <asp:TableHeaderCell Width="30%"><asp:CheckBox ID="Tables_EmptyFilter" runat="server" AutoPostBack="true" Text="Only show empty descriptions"  Font-Size="Small" /></asp:TableHeaderCell>



    </asp:TableHeaderRow>
    </asp:Table>

            <asp:GridView ID="Update_Tables" runat="server" bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="Black"  BackColor="White" BorderWidth="1px" 
           Width="100%" ShowHeaderWhenEmpty="True" visible="true" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" CellSpacing="1" AllowSorting="True" >
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
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
              
        <asp:TemplateField HeaderText="TableName">
            <ItemTemplate>
<%--                <asp:Panel runat="server">--%>
                    <asp:LinkButton ID="Update_Table_Name" runat="server" Text='<%# Bind("TableName")%>' CommandName="ViewFilteredData"></asp:LinkButton>
<%--                </asp:Panel>--%>
            </ItemTemplate>
        </asp:TemplateField>             
                              
                  
<%--        <asp:BoundField DataField="TableName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Table Name"  
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>   
                
<%--        <asp:BoundField DataField="TableDesc" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="100%" HeaderStyle-Wrap="true" 
                HeaderText="Table Description" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     --%>
                <asp:TemplateField  HeaderText="Table Description">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("TableDesc")%>' />
    </ItemTemplate>
    <EditItemTemplate >
        <asp:TextBox runat="server"  ID="TableDesc_EditBox" Rows="4" TextMode="MultiLine" Text='<%# Eval("TableDesc")%>' />
    </EditItemTemplate>
</asp:TemplateField>

    <asp:TemplateField  HeaderText="Visible in DD">
    <ItemTemplate>
    <asp:Label runat="server" Text='<%# Eval("visible_in_dd")%>' />
    </ItemTemplate>
        <EditItemTemplate >
            <asp:dropdownlist runat="server"  ID="Tables_Active_DDL" Rows="4" TextMode="MultiLine" Text='<%# Eval("visible_in_dd")%>'>
                <asp:ListItem Text="NULL" Value=""/>
                <asp:ListItem Text="Active" Value="1"/>
                <asp:ListItem Text="Inactive" Value="0"/>
            </asp:dropdownlist>
        </EditItemTemplate>
    </asp:TemplateField>



<%--        <asp:BoundField DataField="Active" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Active" Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     --%>

<%--
        <asp:BoundField DataField="TableCaveat" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Table Caveat" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="Active" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Active" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="TableOwner" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Table Owner" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="TableAccess" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Table Access" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     



                        <asp:BoundField DataField="LastUpdated" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Last Updated" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>    

                        <asp:BoundField DataField="LastUpdatedPerson" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Last Updated Person" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>    

                        <asp:BoundField DataField="TechnicalDescription" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Technical Description" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>    --%>



            </Columns>
                

            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
                

 <PagerSettings Position="TopAndBottom" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                    </asp:Panel>
    <%---------------------------------------------------------------------------------------%>
        <asp:Panel ID="PanelColumns" runat="server" Width="100%" Visible="false">

        <style type="text/css">
            .radioButtonList { list-style:none; margin: 0; padding: 0;}
            .radioButtonList.horizontal li { display: inline;}
            .radioButtonList label{display:inline;}
        </style>
            
<asp:Table ID="SearchColumnDatatbl" Width="99%" Visible="true" BackColor = "#2b74bb" CellSpacing = "2"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
        <asp:TableCell Width = "5%">Search:</asp:TableCell>
    <asp:TableCell HorizontalAlign = "Left" > <asp:TextBox runat = "server" ID= "Cols_SearchID" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Left" > <asp:TextBox runat = "server" ID= "Cols_SearchColumnName" Width = "100%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
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
    <asp:TableHeaderCell Width="10%">ID</asp:TableHeaderCell>
    <asp:TableHeaderCell Width="30%"><span style="display:inline;float:left;">Column Name</span><asp:Button runat="server" id="ColumnData_AddRow_Btn" Text="Add new row" OnClientClick="OpenInsertWindow()" style="display:inline; padding:0px;font-size:12px;vertical-align:middle;float: right;"/></asp:TableHeaderCell> 
    <asp:TableHeaderCell Width="20%"><asp:CheckBox ID="Columns_EmptyFilter" runat="server" AutoPostBack="true" Text="Only show empty descriptions" Font-Size="Small" /></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>



            <asp:GridView ID="Update_Columns" runat="server" bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"
          ForeColor="Black"  BackColor="White" BorderWidth="1px" 
           Width="99%" ShowHeaderWhenEmpty="True" visible="true" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" AllowSorting="True" >
           <AlternatingRowStyle BackColor="#FFE885" /> 

            <Columns>
           <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="false" 
                ShowEditButton="true" >
            <HeaderStyle Width="3%" />
            </asp:CommandField>
        
                
        <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="ID" ReadOnly="True" 
                Visible="True"  >
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>        
                
        <asp:BoundField DataField="ColumnName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Column Name" ReadOnly="True" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
<%--        <asp:BoundField DataField="ColumnDesc" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="30%" HeaderStyle-Wrap="true" 
                HeaderText="Column Description" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     --%>
                                <asp:TemplateField  HeaderText="Column Description">
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("ColumnDesc")%>' />
    </ItemTemplate>
    <EditItemTemplate >

        <asp:TextBox runat="server"  ID="ColumnDesc_EditBox" Rows="4" TextMode="MultiLine" Text='<%# Eval("ColumnDesc")%>' />
    </EditItemTemplate>
</asp:TemplateField>


<%--        <asp:BoundField DataField="ColumnCaveat" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Column Caveat" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="Active" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Active" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="ColumnOwner" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Column Owner" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="ColumnAccess" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Column Access" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     



                        <asp:BoundField DataField="LastUpdated" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Last Updated" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>    

                        <asp:BoundField DataField="LastUpdatedPerson" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Last Updated Person" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>    

                        <asp:BoundField DataField="TechnicalDescription" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Technical Description" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>    --%>



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
            </asp:Panel>
        <%---------------------------------------------------------------------------------------%>
        <%------------------------------------WORKING SPACE-------------------------------------------------%>
        <asp:Panel ID="PanelMapping" runat="server" Width="100%" Visible="false">
        <style type="text/css">
            .radioButtonList { list-style:none; margin: 0; padding: 0;}
            .radioButtonList.horizontal li { display: inline;}
            .radioButtonList label{display:inline;}
        </style>

<asp:Table ID="SearchMappingtbl" Width="99%" Visible="true" BackColor = "#2b74bb" CellSpacing = "2"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
        <asp:TableCell>Search:</asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchID" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchSchemaName" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchTableName" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtTblSearchColumnName" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
       
     <asp:TableCell>
        <asp:Table runat="server">
            <asp:TableRow>
<%--                <asp:TableCell><asp:CheckBox ID="Columns_WildcardSrch" runat="server" AutoPostBack="true" OnCheckedChanged="Columns_WildcardChecked" Text="Wildcard" Checked="true" Font-Size="Small" /></asp:TableCell>
                <asp:TableCell><asp:CheckBox ID="Columns_ExactSrch" runat="server" AutoPostBack="true" OnCheckedChanged="Columns_ExactChecked" Text="Exact" Checked="false" Font-Size="Small" /></asp:TableCell>--%>
                <asp:TableCell VerticalAlign="Top"><asp:RadioButtonList CssClass="radioButtonList" runat="server" ID="Mapping_Srch_RadioBtn" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="Wildcard" Selected="True" Value="Wildcard"/>
                                   <asp:ListItem Text="Exact" Selected="False" Value="Exact"/>
                               </asp:RadioButtonList> </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:TableCell>

    </asp:TableRow>  
    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White">
    <asp:TableHeaderCell ID = "tblTSelect" Width = "5%" Height = "20px" ></asp:TableHeaderCell>
    <%--<asp:TableHeaderCell ID = "tblTEdit" Visible = "false" Width = "60px" Height = "20px" >&nbsp;</asp:TableHeaderCell>--%>
    <asp:TableHeaderCell Width = "10%" >ID</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "20%" >Schema Name</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "20%" >Table Name</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "20%" >Column Name</asp:TableHeaderCell>
    <asp:TableHeaderCell Width="25%"><a href="/References/NZ_DataDictionary/ColumnLookup.aspx" style="font-size:16px; color:white; background:#c49f68;" onclick="window.open(this.href,'targetWindow','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=400px,height=200px');return false;">Open Column Explorer</a></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>
        <%------------------------------------WORKING SPACE-------------------------------------------------%>

            <div style="overflow-x:auto;width:100%">
            <asp:GridView ID="Update_Mapping" runat="server" bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="Black"  BackColor="White" BorderWidth="1px" 
           Width="99%" ShowHeaderWhenEmpty="True" visible="true" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" AllowSorting="True" >
           <AlternatingRowStyle BackColor="#FFE885" /> 

            <Columns>
           <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="false" 
                ShowEditButton="true" >
            <HeaderStyle Width="3%" />
            </asp:CommandField>
        
                
        <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="ID" ReadOnly="True" 
                Visible="True"  >
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>        
                
        <asp:BoundField DataField="SchemaName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Schema Name" ReadOnly="True" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="TableName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Table Name"  
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="ColumnName" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Column Name" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="ColumnDataID" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Column Data ID" 
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
        <asp:BoundField DataField="Active" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Width="10%" HeaderStyle-Wrap="true" 
                HeaderText="Active" 
                Visible="True"
            ReadOnly="True">
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
                </div>
            </asp:Panel>

    <%--</asp:Panel></div>--%>
        </ContentTemplate>
        </asp:UpdatePanel>

        <script>
            function OpenInsertWindow() {
                window.open("/References/NZ_DataDictionary/InsertColumnData.aspx", 'targetWindow', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=400px,height=200px');
                return false;
            }

    </script>
</asp:Content>
