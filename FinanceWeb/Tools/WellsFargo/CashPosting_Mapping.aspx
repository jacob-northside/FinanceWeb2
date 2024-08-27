<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CashPosting_Mapping.aspx.vb" Inherits="FinanceWeb.CashPosting_Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


       <cc1:tabcontainer ID="PFO_Group_Mapping" runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "PFO Group Mapping" ID = "tpPFO_Group" >
                    <ContentTemplate>    
<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>
 
    <asp:DropDownList runat="server" ID="ddlBankAcctNo" AutoPostBack ="true" ></asp:DropDownList>

    <asp:Panel runat ="server" ScrollBars="Auto" Width ="1000px" Height="350px" >
                              <asp:GridView runat="server" ShowHeaderWhenEmpty ="true"  ID="gvUnMapped" AutoGenerateColumns="false" 
                                  BackColor="#CBE3FB" AllowPaging="true" CellSpacing ="5" HeaderStyle-Font-Size ="X-Small"
                                 AllowSorting="true" PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" 
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5" >
                                 <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                         <%-- <asp:CommandField ShowEditButton="true" EditText ="Edit" ItemStyle-Font-Size="X-Small"
                                               UpdateText="Confirm<br>" CancelText="Cancel"
                                               Visible="True" />--%>
                                    <%--         <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass ="hidden" 
                                                 HeaderStyle-CSSClass ="hidden"
                                                  SortExpression="ID"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Bank Account No" SortExpression="Bank_Account_Number" >
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblIDGroup" runat="server" Text='<%# Bind("Bank_Account_Number")%>'></asp:Label>
                                                </asp:Panel>
                                                <asp:TextBox Font-Size="X-Small" ID="txtIDGroup" Width="95%" runat="server" Text='<%# Bind("Bank_Account_Number")%>' Visible="false"></asp:TextBox>

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
                <asp:TableCell Font-Bold ="true">ID Group</asp:TableCell>
                <asp:TableCell><asp:TextBox Width="80px" runat="server" ID="txtIDGroup"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Cost Center (Optional)</asp:TableCell>
                <asp:TableCell><asp:TextBox  Width="80px"  runat="server" ID="txtCostCenter"></asp:TextBox></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">PFO Group</asp:TableCell>
                <asp:TableCell><asp:TextBox runat="server" ID="txtPFO_Group"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                <asp:Button runat="server" ID="btnAddLine" BorderStyle="Outset" BorderWidth="2px"  Text="Add New Grouping" /></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
        </asp:Table></asp:Panel>
        
               <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3"  VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
     </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableCell>
         <asp:Button ID="ConfirmButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Confirm" />

                   </asp:TableCell>
         <asp:TableCell></asp:TableCell><asp:TableCell>
             <asp:Button ID="CancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Cancel" /></asp:TableCell>
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
