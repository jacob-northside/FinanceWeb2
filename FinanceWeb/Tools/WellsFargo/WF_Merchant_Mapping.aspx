<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WF_Merchant_Mapping.aspx.vb" Inherits="FinanceWeb.WF_Merchant_Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
.pnlMaxx 
{
   max-height:600px;
    
}


.hidden
{
    display:none  !important;
}
        </style>
     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


       <cc1:tabcontainer ID="tcWF_Merchant_Mapping" runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "WF Merchant Mapping" ID = "tpWFMerchMap" >
                    <ContentTemplate>    
<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>

    <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="800px">
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold="true">Merchant:</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddlMerchantSelect" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                <asp:TableCell></asp:TableCell>               
                <asp:TableCell>
                    <asp:CheckBox runat="server" ID="cbMerchShowActives" Text="Show Inactives" Checked="false" AutoPostBack="true" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
        </asp:Table>
    </asp:Panel>


    <asp:Panel runat ="server" ScrollBars="Auto"  Width ="1200px" CssClass="pnlMaxx" >
                              <asp:GridView runat="server" ShowHeaderWhenEmpty="true" ID="gvUnMapped" AutoGenerateColumns="false"
                                  BackColor="#CBE3FB" AllowPaging="true" CellSpacing="5" HeaderStyle-Font-Size="X-Small"
                                  AllowSorting="true" PageSize="18" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5" DataKeyNames="MerchantLocID">
                                 <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                          <asp:CommandField ShowEditButton="true" EditText ="Edit" ItemStyle-Font-Size="X-Small"
                                               UpdateText="Confirm<br>" CancelText="Cancel"
                                               Visible="True" />
                                             <asp:BoundField DataField="MerchantLocID" HeaderText="MerchantLocID" ItemStyle-CssClass="hidden"
                                                 HeaderStyle-CssClass="hidden"
                                                 SortExpression="MerchantLocID"></asp:BoundField>
           
                                        <asp:TemplateField HeaderText="Merchant ID" SortExpression="MerchIDSort">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblMerchantID" runat="server" Text='<%# Bind("MerchantID")%>'></asp:Label>
                                                </asp:Panel>
                                         </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store ID" SortExpression="StoreSort">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblStoreID" runat="server" Text='<%# Bind("StoreID")%>'></asp:Label>
                                                </asp:Panel>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terminal ID" SortExpression="TermSort">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblTerminalID" runat="server" Text='<%# Bind("TerminalID")%>'></asp:Label>
                                                </asp:Panel>                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Merchant Description" SortExpression="MerchantDescription" >
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblMerchantDescription" runat="server" Text='<%# Bind("MerchantDescription")%>'></asp:Label>
                                                </asp:Panel>
                                                <asp:TextBox Font-Size="X-Small" ID="txtMerchantDescription" Width="95%" runat="server" Text='<%# Bind("MerchantDescription")%>' Visible="false"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Outlet Type" SortExpression="OutletTypeID">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">                                     
                                                    <asp:Label Font-Size="X-Small" ID="lblOutletType" CssClass="hidden" runat="server" Text='<%# Bind("OutletTypeID_Null")%>'></asp:Label>
                                                </asp:Panel>
                                                <asp:DropDownList runat="server" ID="ddlOutletType" Enabled ="false"></asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="# Business Days" SortExpression="BusinessDayCount">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblBusinessDayCount" CssClass="hidden" runat="server" Text='<%# Bind("BusinessDayCount_Null")%>'></asp:Label>
                                                </asp:Panel>
                                                <asp:DropDownList runat="server" ID="ddlBusinessDayCount" Enabled="false">
                                                    <asp:ListItem Text="(Select days per week)" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="5 days per week" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="7 days per week" Value="7"></asp:ListItem>
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                          <%--              <asp:TemplateField HeaderText="POS Dashboard Category" SortExpression="POSCategory_Null" Visible="false">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblPOSCategory" CssClass="hidden" runat="server" Text='<%# Bind("POSCategory_Null")%>'></asp:Label>
                                                </asp:Panel>
                                                <asp:DropDownList runat="server" ID="ddlPOSCategory" Enabled="false">
                                                    </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="POS Dashboard Name" SortExpression="POSFacility_Null">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblPOSFacility" CssClass="hidden" runat="server" Text='<%# Bind("POSFacility_Null")%>'></asp:Label>
                                                </asp:Panel>
                                                <asp:DropDownList runat="server" ID="ddlPOSFacility" Enabled="false">
                                                    </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Panel runat="server">
                                                    <asp:LinkButton Font-Size="X-Small" ID="btnFlipActivation" runat="server" Text='<%# Bind("Activation")%>' CommandName="AugmentActive" CommandArgument='<%# Bind("MerchantLocID")%>'></asp:LinkButton>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                            
                                          </Columns>
                             </asp:GridView>
        <br />
        <br />
    </asp:Panel>

        <br />
        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor ="#003060" BorderStyle="Solid" BorderWidth="1px" Width="800px" >
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Merchant ID</asp:TableCell>
                <asp:TableCell><asp:TextBox Width="150px" runat="server" ID="txtMerchID"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Store ID</asp:TableCell>
                <asp:TableCell><asp:TextBox Width="150px" runat="server" ID="txtStoreID"></asp:TextBox></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                               
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold="true">Merchant Description</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" Width="150px"  ID="txtMerchDesc"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold="true">Terminal ID</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" Width="150px" ID="txtTerminalID"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                <asp:Button runat="server" ID="btnAddLine" BorderStyle="Outset" BorderWidth="2px"  Text="Add New Mapping" /></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
        </asp:Table></asp:Panel>
        
               <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="333px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel" Width="250" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="5"  VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
     </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableCell>
         <asp:Button ID="ConfirmButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Confirm" />

                   </asp:TableCell>
         <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center">
             <asp:Button ID="CancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Cancel" /></asp:TableCell>
         <asp:TableCell Width="10px"></asp:TableCell>
     </asp:TableRow>        
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
                        </ContentTemplate>
                        </cc1:TabPanel>
 
           </cc1:tabcontainer>


</asp:Content>
