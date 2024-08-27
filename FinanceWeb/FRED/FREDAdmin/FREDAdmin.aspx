<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FREDAdmin.aspx.vb" Inherits="FinanceWeb.FREDAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
 
     <style type="text/css">
     .ScrollPanelMax
{
max-height: 600px;
}

.modalBackground2 
{
    background-color: #eee4ce !important;
    background-image: none !important;
    border: 1px solid #000000;
    max-height: 500px;
    font-size: medium;
    color: #003060;
    width: 300px;
    padding:5px;
    vertical-align:middle;
    text-align:center;
    
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server" >
      <cc1:TabContainer ID="tcFREDAdmin" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement="True">
       <cc1:TabPanel runat="server" HeaderText="FRED User Administration" ID="tbFREDUsers">
      <ContentTemplate> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>
                  <asp:Label ID="Label1" runat="server" Text="UserID"></asp:Label>
            <asp:TextBox ID="txtFREDUserID" runat="server"></asp:TextBox>
                    <br />
            <asp:RadioButtonList ID="rblActiveStatus" runat="server" RepeatDirection="Horizontal" Font-Size ="X-Small"  >
                    <asp:ListItem  Text ="Active" Value ="True"></asp:ListItem>
                    <asp:ListItem  Text ="Inactive" Value ="False"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:CheckBox ID="chbFREDAdmin" runat="server" text="Administrative Access" Font-Size ="X-Small" />
                    <br />
                    <asp:CheckBox ID="chbCosting" runat="server" text="Allow Costing Data" Font-Size ="X-Small" />
                    <br />
                    <asp:Button ID="btnFREDUser" runat="server" Text="Update FRED"  Font-Size ="X-Small" />
                    <br />
                    
                    <asp:GridView ID="gvFREDUsers" runat="server" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" 
                           Width="80%" height="100%" 
          HeaderStyle-Wrap="true" PageSize="25"   
                    DataSourceID="FREDUsers" >
                    <AlternatingRowStyle BackColor="#FFE885" />
                    <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="USERName" HeaderText="USERName" 
                            SortExpression="USERName" />
                        <asp:BoundField DataField="FREDUser" HeaderText="FREDUser" ReadOnly="True" 
                            SortExpression="FREDUser" />
                        <asp:BoundField DataField="FREDAdmin" HeaderText="FREDAdmin" ReadOnly="True" 
                            SortExpression="FREDAdmin" />
                        <asp:BoundField DataField="CostingAccess" HeaderText="CostingAccess" 
                            SortExpression="CostingAccess" /></Columns>
                        <HeaderStyle BackColor="#214B9A" ForeColor="#FFCBA5" HorizontalAlign="Left" 
                            Wrap="True" /></asp:GridView>
                            
                            <asp:SqlDataSource ID="FREDUsers" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand="SELECT DISTINCT x.UserName , 
   CASE WHEN x.UserName IS NOT NULL THEN 'True' END AS FREDUser,
    CASE WHEN y.UserName IS NOT NULL THEN 'True' ELSE 'False' END AS FREDAdmin
,case when a.CostingAccess = 1 then 'True' else 'False' end CostingAccess 
FROM (SELECT a.UserName, c.RoleName FROM dbo.aspnet_Users AS a
     INNER JOIN dbo.aspnet_UsersInRoles AS b ON a.UserId = b.UserId 
     INNER JOIN dbo.aspnet_Roles AS c ON b.RoleId = c.RoleId WHERE (c.RoleName IN ('FRED'))) x 
left join FRED.USERAccess AS a ON a.USERID = x.UserName 
LEFT OUTER JOIN (SELECT a.UserName, c.RoleName FROM dbo.aspnet_Users AS a 
INNER JOIN dbo.aspnet_UsersInRoles AS b ON a.UserId = b.UserId 
INNER JOIN dbo.aspnet_Roles AS c ON b.RoleId = c.RoleId WHERE (c.RoleName IN ('FREDAdmin'))) AS y ON a.USERID = y.UserName   
ORDER BY x.UserName "></asp:SqlDataSource><br />
                    <br /><asp:Panel ID="Panel1" runat="server"><asp:Label ID="Label2" runat="server" Text="Add New User to FRED"></asp:Label>
                        <br /><asp:TextBox ID="txtSearchInfo" runat="server"></asp:TextBox>&#160; &#160; 
                        <asp:Button ID="AddToFRED" runat="server" Text="ADD TO FRED" />&#160; &#160; &#160; &#160; 
                        <asp:Label ID="lblNEWFRED" runat="server" Text=""></asp:Label>
                        <asp:RadioButtonList ID="rblSearchType" runat="server" 
        RepeatDirection="Horizontal" Font-Size="X-Small">
                            <asp:ListItem>Email </asp:ListItem>
                            <asp:ListItem Selected="True">User ID</asp:ListItem>
                            <asp:ListItem>Last Name</asp:ListItem>
                            <asp:ListItem>Full Name</asp:ListItem></asp:RadioButtonList>
                        <br /><asp:Button ID="btnFindUserInfo" runat="server" Text="Find User Info" />
                        <br /><asp:Label ID="lblUserInfo" runat="server" Text=""></asp:Label></asp:Panel>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnFindUserInfo" EventName="Click"  />
<asp:AsyncPostBackTrigger controlID="AddToFRED" EventName="Click"/>
</Triggers>
</asp:UpdatePanel>

        
</ContentTemplate>
      </cc1:TabPanel>

       <cc1:TabPanel runat="server" HeaderText="FRED Tables" ID="tbFREDTables"> 
         <ContentTemplate> 
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:Label ID="Label6" runat="server" Text="Select flag for what type of data will be pulled."></asp:Label><br /> 
             <asp:Button ID="btnResetFredTableTab" runat="server" Text="Reset" />


             <asp:RadioButtonList ID="rblDataType" runat="server"  RepeatDirection ="Horizontal" Font-Size ="X-Small" >
                    <asp:ListItem Text="Encounter" Value ="Encounter"></asp:ListItem>
                    <asp:ListItem Text="Transaction" Value ="Transaction"></asp:ListItem>
             </asp:RadioButtonList> 

            
            <asp:Label ID="lbldb" runat="server" Text="Select the database to search: " Visible="True"></asp:Label><br /> 
              <asp:DropDownList ID="ddlAvilableDatabase" runat="server">
           </asp:DropDownList> &nbsp; &nbsp; &nbsp; 
            <asp:LinkButton ID="lbSelectdB" runat="server">Select Database</asp:LinkButton><br /><br /> 

              <asp:Label ID="lblschema" runat="server" Text="Select the Schema to search: " Visible="False"></asp:Label><br /> 
                   <asp:DropDownList ID="ddlSchema" runat="server">
           </asp:DropDownList> &nbsp; &nbsp; &nbsp; 
                       <asp:LinkButton ID="lbSelectSchema" runat="server">Select Schema</asp:LinkButton><br /><br /> 

         <asp:Label ID="lblTable" runat="server" Text="Select the table that will be added to the tool: " Visible="False"></asp:Label><br /> 
                       <asp:DropDownList ID="ddlSelectTable" runat="server">
           </asp:DropDownList> &nbsp; &nbsp; &nbsp; 
                       <asp:LinkButton ID="lbSelectTable" runat="server">Select Table</asp:LinkButton><br /> 
       <asp:TextBox ID="txtTableAlias" runat="server" Text=""></asp:TextBox> &nbsp; &nbsp; <asp:Label ID="lbltable2" runat ="server" Text="Visible Name" Visible="False"></asp:Label>
       <br /><br />
       <%--       <asp:RadioButtonList ID="rblFREDType" runat="server"  RepeatDirection ="Horizontal" Font-Size ="X-Small" Visible="False">
                    <asp:ListItem Text="FRED HQ" Value ="HQ"></asp:ListItem>
                    <asp:ListItem Text="FREDMQ" Value ="MQ"></asp:ListItem>
              </asp:RadioButtonList>  --%>
                     <asp:Label ID="lblcolumn" runat="server" Text="Select Patient Account Facility (Linking Column): " Visible="False"></asp:Label><br />
                       <asp:DropDownList ID="ddlSelectColumn" runat="server">
           </asp:DropDownList> &nbsp; &nbsp; &nbsp; 
                     
       <asp:Button ID="btnAddToFRED" runat="server" Text="Add Table to FRED" /><br />

      <asp:TextBox ID="txtColumnAlias" runat="server" Text=""></asp:TextBox> &nbsp; &nbsp; <asp:Label ID="lblcolumn2" runat ="server" Text="Visible Name" Visible="False"></asp:Label>
       <br /><br />
                 
                </ContentTemplate>
                 <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnAddToFRED" EventName="Click" />
                 </Triggers>
               </asp:UpdatePanel>
         
</ContentTemplate>  
       </cc1:TabPanel>
       
       <cc1:TabPanel runat="server" HeaderText="FRED Columns" ID="tbFREDColumns" ScrollBars="Auto"> 
           <ContentTemplate> 
             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
              <ContentTemplate> 
                 <%--  <asp:Label ID="Label7" runat="server" Text="Select the correct FRED Environmenment"></asp:Label> 
               <asp:RadioButtonList ID="rblFREDEnvironment" runat="server"  RepeatDirection ="Horizontal" Font-Size ="X-Small" Visible="true" AutoPostBack="True">
                    <asp:ListItem Text="FRED HQ" Value ="HQ"></asp:ListItem>
                    <asp:ListItem Text="FREDMQ" Value ="MQ"></asp:ListItem>
              </asp:RadioButtonList> --%>


               <asp:Panel Id = "pnlgvTOScrollPanel" runat = "server" scrollbars = "auto"> 
                 <asp:GridView ID = "gvTableOverview" runat = "server" AutoGenerateColumns = "False"
              BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" 
                           Width="80%" height="100%" 
          HeaderStyle-Wrap="true">
                 
                 
                  <%-- onrowcancelingedit = "gvTableOverview_RowCancelingEdit"
                 onrowediting = "gvTableOverview_RowEditing" onrowupdating = "gvTableOverview_RowUpdating" onrowdatabound = "gvTableOverview_RowDataBound"--%>
                 <columns>
                 <asp:TemplateField HeaderText = "Select" ShowHeader = "false">
                 <ItemTemplate>
                 <asp:LinkButton ID = "btngvTOselect" runat = "server" CommandName = "Select" Text = "Select" ></asp:LinkButton>
                 </ItemTemplate>
                 </asp:TemplateField>                                 
                 
                  <asp:TemplateField HeaderText = "Edit" ShowHeader = "false">
                 <ItemTemplate>
                 <asp:LinkButton ID = "btngvTOedit" runat = "server" CommandName = "Edit" Text = "Edit" ></asp:LinkButton>
                 </ItemTemplate>
                 <EditItemTemplate>
                 <asp:LinkButton ID = "btngvTOupdate" runat = "server" CommandName = "Update" Text = "Update"></asp:LinkButton>
                 <asp:LinkButton ID = "btngvTOcancel" runat = "server" CommandName = "Cancel" Text = "Cancel"></asp:LinkButton>
                 </EditItemTemplate>
                 </asp:TemplateField>



                 <asp:TemplateField HeaderText = "Table ID"> <ItemTemplate>
                 <asp:Label ID = "lblgvTO1" runat = "server" Text = '<%# Eval("ID") %>' ></asp:Label>
                 </ItemTemplate></asp:TemplateField>

             <asp:TemplateField  HeaderText = "FRED Type"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("FREDType") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:RadioButtonList ID = "rblgvTOFREDType" runat = "server" Text = '<%# Eval("FREDType") %>'>
                  <asp:ListItem Text="Encounter" Value ="0"></asp:ListItem>
                  <asp:ListItem Text="Transaction" Value ="1"></asp:ListItem>
                 </asp:RadioButtonList>
                 </EditItemTemplate> </asp:TemplateField>

             <asp:TemplateField  HeaderText = "Table Alias"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("Table_Alias") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvTOAlias" runat = "server" Text = '<%# Eval("Table_Alias") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "FRED Connect"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOFredConnect" runat = "server" Text = '<%# Eval("COLUMN_NAME") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:DropDownList ID = "ddlgvTOFredConnect" runat = "server" >
                 </asp:DropDownList>
                 </EditItemTemplate> </asp:TemplateField>

                <asp:TemplateField HeaderText = "Visible On Web"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOWebVis" runat = "server" Text = '<%# Eval("VisibleOnWeb") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvTOWebVis" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Active"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOActive" runat = "server" Text = '<%# Eval("Active") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvTOActive" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Costing"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOCosting" runat = "server" Text = '<%# Eval("Costing") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvTOCosting" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "PHI"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOPHI" runat = "server" Text = '<%# Eval("PHI") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvTOPHI" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Financial"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOFinancial" runat = "server" Text = '<%# Eval("Financial") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvTOFinancial" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "BO_Financial"> <ItemTemplate>
                 <asp:Label ID = "lblgvTOBO_Financial" runat = "server" Text = '<%# Eval("BO_Financial") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvTOBO_Financial" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField  HeaderText = "Grid Color"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("GridColor") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvTOGridColor" runat = "server" Text = '<%# Eval("GridColor") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
           
                 </columns>
                 
                 </asp:GridView></asp:Panel>
                <br /> 
<%--     <asp:Label ID="lblFredAvailableTables" runat="server" Text="Available Tables" Visible="true"></asp:Label> &nbsp; &nbsp; &nbsp; 
     <asp:DropDownList ID="ddlAvailableTables" runat="server" Visible="True">
     </asp:DropDownList> &nbsp; &nbsp; 
     <asp:LinkButton ID="lbEditTable" runat="server" Visible="true">Select Table</asp:LinkButton><br /><br /> --%>
 <%--     <asp:Panel ID="pnlEditTable" runat="server" Visible="False">--%>
  <%--            <asp:Label ID="lblSelectedTableID" runat="server" Text="1" Visible ="false" ></asp:Label>
          <asp:Label ID="Label5" runat="server" Text="Selected Table: "></asp:Label>
          <asp:Label ID="lblEditTable" runat="server" Text=""></asp:Label>   &nbsp; &nbsp; &nbsp; 
 
            <asp:RadioButtonList ID="rblTableVisibleStatus" runat="server" RepeatDirection ="Horizontal" Font-Size ="X-Small" >
      <asp:ListItem Text="Visible" Value ="Visible"></asp:ListItem>
      <asp:ListItem Text="Not-Visible" Value ="Not-Visible"></asp:ListItem>
      </asp:RadioButtonList> 
            <asp:Label ID="Label4" runat="server" Text="Table Alias"></asp:Label> 
      <asp:TextBox ID="txtEditTableAlias" runat="server"></asp:TextBox><br />
      <asp:RadioButtonList ID="rblTableType" runat="server"  RepeatDirection ="Horizontal" Font-Size ="X-Small" >
        <asp:ListItem Text="Encounter" Value ="Encounter"></asp:ListItem>
        <asp:ListItem Text="Transaction" Value ="Transaction"></asp:ListItem>
      </asp:RadioButtonList> 
      <asp:RadioButtonList ID="rblEditTableActive" runat="server"  RepeatDirection ="Horizontal" Font-Size ="X-Small" >
        <asp:ListItem Text="Active" Value ="Active"></asp:ListItem>
        <asp:ListItem Text="InActive" Value ="InActive"></asp:ListItem>
      </asp:RadioButtonList><br />
      <asp:Label ID = "lblSelectTableColor" runat = "server" Text = "Select Color:"> </asp:Label>
      <asp:TextBox ID = "txtEditTableColor" runat = "server"></asp:TextBox> <br />
          <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
        <asp:Button ID="lbSaveTableEdits" runat="server" Text="Make Edits to Table" /><br />--%>
        
        <asp:Panel ID = "pnlgvECScrollPanel" runat = "server" ScrollBars = "Auto" >
          <asp:GridView ID="gvEditColumns" runat="server"   
          AllowPaging ="True" AllowSorting ="True" AutoGenerateColumns ="False"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" 
                           Width="80%" height="100%" 
          HeaderStyle-Wrap="true" PageSize="25" DataKeyNames="ID" >
             <AlternatingRowStyle BackColor="#FFE885" />
                    <Columns>
                                        <asp:TemplateField HeaderText = "Edit" ShowHeader = "false">
                 <ItemTemplate>
                 <asp:LinkButton ID = "btngvECedit" runat = "server" CommandName = "Edit" Text = "Edit" ></asp:LinkButton>
                 </ItemTemplate>
                 <EditItemTemplate>
                 <asp:LinkButton ID = "btngvECupdate" runat = "server" CommandName = "Update" Text = "Update"></asp:LinkButton>
                 <asp:LinkButton ID = "btngvECcancel" runat = "server" CommandName = "Cancel" Text = "Cancel"></asp:LinkButton>
                 </EditItemTemplate>
                 </asp:TemplateField>
                       
                        <asp:BoundField DataField="ID" HeaderText="ID" 
                            SortExpression="ID" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="TABLE_ID" HeaderText="TABLE_ID" 
                            SortExpression="TABLE_ID" Visible = "false" />
                        <asp:BoundField DataField="COLUMN_NAME" HeaderText="COLUMN_NAME" 
                            SortExpression="COLUMN_NAME" ReadOnly="true"  />
             <asp:TemplateField SortExpression= "Column_Alias"  HeaderText = "Column Alias"><ItemTemplate>
                 <asp:Label runat = "server" Text = '<%# Eval("Column_Alias") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox ID = "txtgvECAlias" runat = "server" Text = '<%# Eval("Column_Alias") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "VisibleOnWeb" SortExpression = "VisibleOnWeb"> <ItemTemplate>
                 <asp:Label ID = "lblgvECVisibleOnWeb" runat = "server" Text = '<%# Eval("VisibleOnWeb") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECVisibleOnWeb" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Active" SortExpression = "Active"> <ItemTemplate>
                 <asp:Label ID = "lblgvECActive" runat = "server" Text = '<%# Eval("Active") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECActive" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>


                 <asp:TemplateField HeaderText = "Costing" SortExpression = "Costing"> <ItemTemplate>
                 <asp:Label ID = "lblgvECCosting" runat = "server" Text = '<%# Eval("Costing") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECCosting" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "PHI" SortExpression = "PHI"> <ItemTemplate>
                 <asp:Label ID = "lblgvECPHI" runat = "server" Text = '<%# Eval("PHI") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECPHI" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "Financial" SortExpression = "Financial"> <ItemTemplate>
                 <asp:Label ID = "lblgvECFinancial" runat = "server" Text = '<%# Eval("Financial") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECFinancial" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "BO_Financial" SortExpression = "BO_Financial"> <ItemTemplate>
                 <asp:Label ID = "lblgvECBO_Financial" runat = "server" Text = '<%# Eval("BO_Financial") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECBO_Financial" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                                  <asp:TemplateField HeaderText = "sDefault" SortExpression = "sDefault"> <ItemTemplate>
                 <asp:Label ID = "lblgvECsDefault" runat = "server" Text = '<%# Eval("sDefault") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:CheckBox ID = "cblgvECsDefault" runat = "server" >
                 </asp:CheckBox>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField HeaderText = "FREDConnect" SortExpression = "FREDConnect"> <ItemTemplate>
                 <asp:Label ID = "lblgvECFREDConnect" runat = "server" Text = '<%# Eval("FREDConnect") %>'></asp:Label>
                 </ItemTemplate> </asp:TemplateField>


              </Columns><HeaderStyle BackColor="#214B9A" ForeColor="#FFCBA5" HorizontalAlign="Left" 
                            Wrap="True" />
                            </asp:GridView>
              </asp:Panel>

<%--          <asp:SqlDataSource ID="EditColumns" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    
                
                    SelectCommand="SELECT ID, TABLE_ID, COLUMN_NAME, Column_Alias, VisibleOnWeb, Active, Costing, sDefault, FREDConnect FROM FRED.AvailableColumns WHERE (TABLE_ID = @TABLE_ID)" 
                    
                    UpdateCommand="UPDATE FRED.AvailableColumns SET Column_Alias = @Column_Alias, VisibleOnWeb = @VisibleOnWeb, Active = @Active, Costing = @Costing, sDefault = @sDefault, FREDConnect = @FREDConnect WHERE (ID = @ID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSelectedTableID" DefaultValue="1" Name="TABLE_ID" 
                        PropertyName="text" Type="Int32" />
                       
                </SelectParameters>
                <UpdateParameters>
                 
                    <asp:Parameter Name="Column_Alias" />
                    <asp:Parameter Name="VisibleOnWeb" />
                    <asp:Parameter Name="Active" />
                    <asp:Parameter Name="Costing" />
                    <asp:Parameter Name="sDefault" />
                    <asp:Parameter Name="FREDConnect" />
                    
                    <asp:Parameter Name="ID" />
                    
                </UpdateParameters>
            </asp:SqlDataSource>--%>
         <br />
   </asp:Panel>
                <asp:Label ID="FakeButton" runat = "server" />
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" 
             runat="server" 
             
             TargetControlID="FakeButton"
             PopupControlID="pnlMPE1"
             DropShadow="true"/>

   <asp:Panel ID="pnlMPE1" runat="server" Width="233px" CssClass = "modalBackground2" Style="display: none" >
                     <asp:Label ID="lblUpdateMsg" runat="server" Text=""></asp:Label>
                  <br />
                  <asp:GridView runat = "server" ID = "gvColumnchange1" AutoGenerateColumns ="True" ></asp:GridView>
                  <br />
                  <asp:Label ID = "lblUpdateMsg2" runat = "server" Text = "Columns Inactivated: "></asp:Label>
                  <asp:GridView runat = "Server" ID = "gvColumnChange2" AutoGenerateColumns ="True" ></asp:GridView>
         
      <asp:Button ID="OkButton" runat="server" Font-Size = "small" Text="Close"/>

 </asp:Panel>

          <asp:Button ID="btnUpdateTable" runat="server" Text="Update Table w/ New columns " />
         </asp:Panel>   

         </ContentTemplate>
         <Triggers>
         <%--<asp:AsyncPostBackTrigger ControlID="EditColumns" EventName ="UpdateCommand"  />--%>
         </Triggers>     
         </asp:UpdatePanel> 
</ContentTemplate>
        
</cc1:TabPanel>

       <cc1:TabPanel runat="server" HeaderText="FRED Favorites" ID="tpFREDFavorites" ScrollBars="Auto"> 
           <ContentTemplate> 
             <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
              <ContentTemplate> 

              <asp:SqlDataSource ID="dsFavoriteOwners" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand="select distinct Userlogin, 
case when FirstName is null and LastName is null then Userlogin 
	when LastName is null then FirstName + ' -- ' + Userlogin 
	when FirstName is null then LastName
	else FirstName + ' ' + LastName end
	as Username from WebFD.FRED.FredFavorites f
join DWH.dbo.Email_Distribution e on f.Userlogin = e.NetworkLogin"></asp:SqlDataSource>

 <asp:SqlDataSource ID= "dsFredFavorites" runat = "server"
            ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
            SelectCommand="
            select ID, Userlogin, FavoriteName, LoadDate, LastView from FRED.FredFavorites

            where Userlogin = @Userlogin">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlFredFavoriteOwner" Name="Userlogin" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
        </asp:SqlDataSource>

        

              <asp:Label runat = "server" text =  "Select Favorite Owner: " ></asp:Label>
             <asp:DropDownList runat="server" ID = "ddlFredFavoriteOwner" DataSourceID = "dsFavoriteOwners" DataTextField = "Username" DataValueField = "Userlogin"
             AppendDataBoundItems="True" AutoPostBack = "true" ></asp:DropDownList>

             <br />
             <br />
             <asp:Panel runat = "server" ID = "pnlgvFFScrollPanel" ScrollBars = "Auto" >
             <asp:GridView runat = "server" ID = "gvFredFavorites" AllowPaging ="True" AllowSorting ="True" AutoGenerateColumns ="True"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" AutoGenerateSelectButton = "true"
                           Width="80%" height="100%"
            HeaderStyle-Wrap="true" PageSize="25" DataKeyNames="ID" DataSourceID = "dsFredFavorites" ></asp:GridView>
            </asp:Panel>
            <br />
            <br />
            <asp:Label runat = "server" ID = "lblgvFFQuery"></asp:Label>

             <asp:Panel runat = "server" ID = "pnlgvFFColsScrollPanel" ScrollBars = "Auto" >
             <asp:Label runat = "server" Text = "Columns Selected: " ></asp:Label>
             <asp:GridView runat = "server" ID = "gvFredFavoritesColumns" AllowPaging ="True" AllowSorting ="True" AutoGenerateColumns ="True"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" 
                           Width="80%" height="100%" 
            HeaderStyle-Wrap="true" PageSize="25" DataKeyNames="ID" ></asp:GridView>
            </asp:Panel><br /><br />
                         <asp:Panel runat = "server" ID = "pnlgvFFFilsScrollPanel" ScrollBars = "Auto" >
             <asp:Label runat = "server" Text = "Filters Selected: " ></asp:Label>
             <asp:GridView runat = "server" ID = "gvFredFavoritesFilters" AllowPaging ="True" AllowSorting ="True" AutoGenerateColumns ="True"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" 
                           Width="80%" height="100%" 
            HeaderStyle-Wrap="true" PageSize="25" DataKeyNames="ID" ></asp:GridView>
            </asp:Panel>
              </ContentTemplate>
              </asp:UpdatePanel>
              </ContentTemplate>
              </cc1:TabPanel>

                     <cc1:TabPanel runat="server" HeaderText="Recent Queries" ID="tpRecentViews" ScrollBars="Auto"> 
           <ContentTemplate> 
             <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
              <ContentTemplate> 

              <asp:SqlDataSource ID="dsQueryOwners" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand="
                    select top 1 ' -- See All -- ' as Userlogin, 
                    ' -- See All -- ' as Username from [WebFD].[FRED].[RecentQueries]

                    union 

                    select distinct USERID, 
case when FirstName is null and LastName is null then USERID 
	when LastName is null then FirstName + ' -- ' + USERID 
	when FirstName is null then LastName
	else FirstName + ' ' + LastName end
	as Username from [WebFD].[FRED].[RecentQueries] f
join DWH.dbo.Email_Distribution e on f.[USERID] = e.NetworkLogin
order by Username asc
"></asp:SqlDataSource>

 <asp:SqlDataSource ID= "dsFredQueries" runat = "server"
            ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
            SelectCommand="
                    select  
                    case when FirstName is null and LastName is null then USERID 
	                when LastName is null then FirstName + ' -- ' + USERID 
	                when FirstName is null then LastName
	                else FirstName + ' ' + LastName end
	                as Username,
                    SQL,
                    DateRun
                    
                     from [WebFD].[FRED].[RecentQueries] f
                    join DWH.dbo.Email_Distribution e on f.[USERID] = e.NetworkLogin
            where [USERID] = @Userlogin or @Userlogin = ' -- See All -- '
            
            order by DateRun desc
            ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlFredQuerier" Name="Userlogin" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
        </asp:SqlDataSource>

        

              <asp:Label runat = "server" text =  "Select Favorite Owner: " ></asp:Label>
             <asp:DropDownList runat="server" ID = "ddlFredQuerier" DataSourceID = "dsQueryOwners" DataTextField = "Username" DataValueField = "Userlogin"
             AppendDataBoundItems="True" AutoPostBack = "true" ></asp:DropDownList>

             <br />
             <br />
                                      <asp:Panel runat = "server" ID = "pnlgvRQScrollPanel"  ScrollBars = "Auto" CssClass = "ScrollPanelMax" >
              <asp:GridView runat = "server" ID = "gvFredQueries" AllowPaging ="True" AllowSorting ="True" AutoGenerateColumns ="True"    
             BorderColor="Black" BorderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" DataSourceID = "dsFredQueries"
                           Width="80%" height="100%" 
            HeaderStyle-Wrap="true" PageSize="25" ></asp:GridView>
            </asp:Panel>
             </ContentTemplate>
             </asp:UpdatePanel>
             </ContentTemplate>
             </cc1:TabPanel>



          <cc1:TabPanel runat="server" HeaderText="Lookup Tables" ID="tpLookupTables" ScrollBars="Auto"> 
           <ContentTemplate> 
             <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
              <ContentTemplate> 

                            <asp:SqlDataSource ID="dsLookupTables" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand="select top 1 ' -- Select Table -- ' as Table_name, 0 as ID from WebFD.FRED.LookupTables union select Table_name, ID from WebFD.FRED.LookupTables order by 1 "></asp:SqlDataSource>

                                                <asp:SqlDataSource ID="dsLookupColumns" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand=" select Column_Name, ID from WebFD.FRED.LookupColumns where TABLE_ID = @TableID ">
                                        <SelectParameters>
                        <asp:ControlParameter ControlID="ddlFREDLookupTables" Name="TableID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                    
                    </asp:SqlDataSource>
                   <asp:SqlDataSource ID="dsFREDTables" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand="select top 1 ' -- Select Table -- ' as Table_Alias, 0 as ID from WebFD.FRED.LookupTables union select Table_Alias, ID from WebFD.FRED.AvailableTables order by 1 "></asp:SqlDataSource>

                    
                                                <asp:SqlDataSource ID="dsFREDColumns" runat="server" ConnectionString="<%$ ConnectionStrings:WebFDconn %>" 
                    
                    SelectCommand=" select Column_Name, ID from WebFD.FRED.AvailableColumns where TABLE_ID = @TableID ">
                                        <SelectParameters>
                        <asp:ControlParameter ControlID="ddlFREDTables" Name="TableID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                    
                    </asp:SqlDataSource>

              <asp:Panel runat = "server">
             <asp:Table runat = "server">
             <asp:TableRow>
             <asp:TableCell horizontalalign = "center" Columnspan = "3">
              <asp:Label runat = "server" font-size = "larger" text = "Add Connection: "></asp:Label>
             </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
             <asp:TableCell>
             <asp:Label runat = "server" text = "Select Lookup Table: "></asp:Label>
             </asp:TableCell>
             <asp:TableCell>
             <asp:DropDownList width = "100%" runat="server" ID = "ddlFREDLookupTables" DataSourceID = "dsLookupTables" DataTextField = "Table_name" DataValueField = "ID"
              AutoPostBack = "true" >
             </asp:DropDownList>
             </asp:TableCell>
             <asp:TableCell text = "(Alias LookUpLink)"></asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
             <asp:TableCell text = "Select Column: ">
             </asp:TableCell>
             <asp:TableCell>
             <asp:DropDownList runat="server" ID = "ddlFREDLookupColumns" DataSourceID = "dsLookupColumns" DataTextField = "Column_Name" DataValueField = "ID"
              AutoPostBack = "true" width = "100%" enabled = "False" ></asp:DropDownList>
             </asp:TableCell>
             </asp:TableRow> 
            <asp:TableRow>
             <asp:TableCell text = "Select FRED Table: "></asp:TableCell>
             <asp:TableCell><asp:DropDownList runat="server" ID = "ddlFREDTables" DataSourceID = "dsFREDTables" DataTextField = "Table_Alias" DataValueField = "ID"
              AutoPostBack = "true" width = 100% >
              </asp:DropDownList></asp:TableCell>
             <asp:TableCell text = "(Alias FREDLink)"></asp:TableCell>
             </asp:TableRow>
              <asp:TableRow>
             <asp:TableCell text = "Select Column: ">
             </asp:TableCell>
             <asp:TableCell><asp:DropDownList runat="server" ID = "ddlFREDColumns" DataSourceID = "dsFREDColumns" DataTextField = "Column_Name" DataValueField = "ID"
              AutoPostBack = "true" width = "100%" enabled = "False"  >
             </asp:DropDownList></asp:TableCell>
             </asp:TableRow>
             <asp:TableRow><asp:TableCell text = "Join String: "></asp:TableCell>
             <asp:TableCell><asp:Textbox width = "100%" runat = "server" ID = "txtLUJoinString"></asp:Textbox></asp:TableCell>
             </asp:TableRow>
             <asp:TableRow><asp:TableCell columnspan = "3" horizontalalign = "center">
             <asp:Button ID = "btnAddConnection" text = "Add Connection" enabled = "false" runat = "server" /> 
             </asp:TableCell></asp:TableRow>
             </asp:Table>
              
              
             
             <br />
              <asp:Label runat = "server" font-size = "larger" text = "Current Connections: "></asp:Label>
              </asp:Panel>
              <asp:Panel runat = "server" ScrollBars = "Auto"> 
              <asp:GridView runat = "server" ID = "gvCurrentConnections" AutoGenerateColumns = "False"
              BorderColor="Black" BorderStyle="Solid" font-Size="Smaller"  HeaderStyle-BackColor="#214B9A" 
               HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
                    ForeColor="Black"  BackColor="White" BorderWidth="1px" 
                           Width="80%" height="100%" 
          HeaderStyle-Wrap="true">
                 
                  <columns>
                           
   
                  <asp:TemplateField HeaderText = "Edit" ShowHeader = "false">
                 <ItemTemplate>
                 <asp:LinkButton ID = "btngvCCedit" runat = "server" CommandName = "Edit" Text = "Edit" ></asp:LinkButton>
                 </ItemTemplate>
                 <EditItemTemplate>
                 <asp:LinkButton ID = "btngvCCupdate" runat = "server" CommandName = "Update" Text = "Update"></asp:LinkButton>
                 <asp:LinkButton ID = "btngvCCcancel" runat = "server" CommandName = "Cancel" Text = "Cancel"></asp:LinkButton>
                 </EditItemTemplate>
                 </asp:TemplateField>


                 <asp:TemplateField visible = "false" HeaderText = "Table ID"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCId" runat = "server" Text = '<%# Eval("ID") %>' ></asp:Label>
                 </ItemTemplate></asp:TemplateField>

                 <asp:TemplateField visible = "false" HeaderText = "FRED Table ID"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCFTId" runat = "server" Text = '<%# Eval("FREDTable_ID") %>' ></asp:Label>
                 </ItemTemplate></asp:TemplateField>


                 <asp:TemplateField HeaderText = "FRED Table Name"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCFTName" runat = "server" Text = '<%# Eval("FREDTable_Name") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:DropDownList font-size = "small" ID = "ddlgvCCFTNAME" runat = "server" AutoPostBack = "true"  OnSelectedIndexChanged = "DropDownListTesting" >
                 </asp:DropDownList>
                 </EditItemTemplate> </asp:TemplateField>

                                  <asp:TemplateField visible = "false" HeaderText = "FRED Column ID"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCFCId" runat = "server" Text = '<%# Eval("FREDColumn_ID") %>' ></asp:Label>
                 </ItemTemplate></asp:TemplateField>


                 <asp:TemplateField HeaderText = "FRED Column Name"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCFCName" runat = "server" Text = '<%# Eval("FREDColumn_Name") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:DropDownList font-size = "small" ID = "ddlgvCCFCNAME" runat = "server" AutoPostBack = "true"  OnSelectedIndexChanged = "ddlgvCCCol_SelectedIndexChanged" >
                 </asp:DropDownList>
                 </EditItemTemplate> </asp:TemplateField>

                 <asp:TemplateField visible = "false" HeaderText = "LookUp Table ID"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCLTId" runat = "server" Text = '<%# Eval("LUTable_ID") %>' ></asp:Label>
                 </ItemTemplate></asp:TemplateField>


                 <asp:TemplateField HeaderText = "LookUp Table Name"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCLTName" runat = "server" Text = '<%# Eval("LUTable_Name") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:DropDownList font-size = "small" ID = "ddlgvCCLTNAME" runat = "server" AutoPostBack = "true"  OnSelectedIndexChanged = "ddlgvCCLTNAME_SelectedIndexChanged" >
                 </asp:DropDownList>
                 </EditItemTemplate> </asp:TemplateField>

                                                   <asp:TemplateField visible = "false" HeaderText = "Lookup Column ID"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCLCId" runat = "server" Text = '<%# Eval("LUColumn_ID") %>' ></asp:Label>
                 </ItemTemplate></asp:TemplateField>


                 <asp:TemplateField HeaderText = "LookUp Column Name"> <ItemTemplate>
                 <asp:Label ID = "lblgvCCLCName" runat = "server" Text = '<%# Eval("LUColumn_Name") %>'></asp:Label>
                 </ItemTemplate> <EditItemTemplate >
                 <asp:DropDownList font-size = "small" ID = "ddlgvCCLCNAME" runat = "server" AutoPostBack = "true"  OnSelectedIndexChanged = "ddlgvCCCol_SelectedIndexChanged" >
                 </asp:DropDownList>
                 </EditItemTemplate> </asp:TemplateField>

                <asp:TemplateField  HeaderText = "Table JoinString"><ItemTemplate>
                 <asp:Label  runat = "server" Text = '<%# Eval("JoinString") %>' ></asp:Label> </ItemTemplate>
                 <EditItemTemplate>
                 <asp:TextBox font-size = "small" width = "250px" ID = "txtgvCCJoinString" runat = "server" Text = '<%# Eval("JoinString") %>'></asp:TextBox>
                 </EditItemTemplate> </asp:TemplateField>
                 </columns> </asp:GridView>
                 </asp:Panel>


                           </ContentTemplate>
             </asp:UpdatePanel>
             </ContentTemplate>
             </cc1:TabPanel>
   </cc1:TabContainer>
  
  


</asp:Content>
