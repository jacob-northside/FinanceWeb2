<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MD POS Desc Mapping.aspx.vb" Inherits="FinanceWeb.MD_POS_Desc_Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>

    <cc1:tabcontainer id="udMDPOS" runat="server"
        activetabindex="0" useverticalstripplacement="False" width="1150px">
                    <cc1:TabPanel runat = "server" HeaderText = "POS Description Mapping - Voltypes" ID = "tpPOSVoltypes" >
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="hiddenThings" Visible="false">
                            <asp:Label runat="server" ID="VolTypemap"></asp:Label>
                            <asp:Label runat="server" ID="VolTypedir"></asp:Label>              
                        </asp:Panel>

    <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1000px">
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell Font-Bold="true">ID Group:</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="154px" runat="server" ID="ddlVoltypeIDGroup" ></asp:DropDownList></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold="true">Status:</asp:TableCell>
                <asp:TableCell RowSpan="5" VerticalAlign="Top">
                    <asp:CheckBoxList runat="server" ID="cblVolTypeStatuses" ></asp:CheckBoxList>
                </asp:TableCell>            
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold="true">POS Desc Search:</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="150px" runat="server" ID="txtVolTypePOSDesc"></asp:TextBox></asp:TableCell>
 

            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:Button runat="server" ID="btnSearch" BorderStyle="Outset" BorderWidth="2px" Text="Search" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <asp:Panel runat ="server" ScrollBars="Auto" Width ="1000px" Height="500px" >
                              <asp:GridView runat="server" ShowHeaderWhenEmpty ="true" DataKeyNames="poskey"  ID="gvUnMapped" AutoGenerateColumns="false" 
                                  BackColor="#CBE3FB" AllowPaging="false" CellSpacing ="5" HeaderStyle-Font-Size ="Smaller"
                                 AllowSorting="true"  Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" 
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5">
                                 <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                          
                                             <asp:BoundField HeaderText="" ></asp:BoundField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                                               
                                                <asp:CheckBox runat="server" ID="cbBulkUpdate" Checked="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                              <asp:BoundField DataField="IDGroup" HeaderStyle-Width="100px" HeaderText="ID Group" 
                                                  SortExpression="IDGroup"></asp:BoundField>
                                              <asp:BoundField DataField="POSDesc" HeaderText="POS Description"  
                                                  SortExpression="POSDesc"></asp:BoundField>
                                              <asp:BoundField DataField="Explanation" HeaderText="Status" HeaderStyle-Width="100px"
                                                  SortExpression="Explanation"></asp:BoundField>
                                              <asp:BoundField DataField="VolTypes" HeaderStyle-Width="65px" HeaderText="Current VolType(s)"
                                                  SortExpression="VolTypes"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Assign VolType">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssignedVolType" runat="server" Visible="false" Text='<%# Eval("VolID")%>'></asp:Label>
                                                <asp:DropDownList Width="150px" Height="30px" ID="ddlAssignedVolType"  OnSelectedIndexChanged="ddlAssignedVolType_SelectedIndexChanged1" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                       
                                          </Columns>
                             </asp:GridView>
        <br />
      
    </asp:Panel>

                        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1000px">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Font-Bold="true">Update Selected Rows:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="ddlBulkUpdateVolTypes"></asp:DropDownList>
                                        </asp:TableCell>                                      
                                </asp:TableRow>
  
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                                        <asp:Button runat="server" ID="btnBulkUpdate" BorderStyle="Outset" BorderWidth="2px" Text="Update" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>


        <br />

               <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
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
                        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="VolType Admin" ID="tpAdministrative" >
            <ContentTemplate>

                <asp:Panel ID="pnlAdminCashJournalTypes" runat="server">

                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="5">
                                        This is the list of VolTypes that can be assigned on this POS Desc Mapping tool.                                                                                   
                                                                                   
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="250px">
                                    Enter VolType:
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtAdminVolype" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                       
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Button runat="server" ID="btnAddAdminVolType" Text="Add New VolType" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Height="5px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>


                    <asp:Panel runat="server" ID="pnlAdminVolTypeScroll" ScrollBars="Auto" CssClass="MxPanelHeight">
                        <asp:Table runat="server">
                            <asp:TableFooterRow>
                                <asp:TableCell>

                                    <asp:GridView ID="gvAdminVolTypes" runat="server"
                                        AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                        BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                        HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="VolID"
                                        BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                        <AlternatingRowStyle BackColor="white" />

                                        <Columns>
                                            <asp:BoundField HeaderText="" ControlStyle-Width="10px" ItemStyle-Width="10px"></asp:BoundField>
                                            <asp:BoundField DataField="VolType" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true"
                                                HeaderText="Type" ReadOnly="True">
                                                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                            </asp:BoundField>                                          

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Panel runat="server">
                                                        <asp:LinkButton ID="btnRemoveType" runat="server" Text="Remove" CommandName="RemoveType" CommandArgument='<%# Bind("VolID")%>'></asp:LinkButton>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:TableCell>

                            </asp:TableFooterRow>
                        </asp:Table>
                    </asp:Panel>

                </asp:Panel>

                </contentTemplate>
            </cc1:TabPanel>

                </cc1:tabcontainer>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
