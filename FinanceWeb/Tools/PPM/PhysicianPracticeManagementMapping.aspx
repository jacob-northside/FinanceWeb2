<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PhysicianPracticeManagementMapping.aspx.vb" Inherits="FinanceWeb.PhysicianPracticeManagementMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>

    <asp:Panel runat="server" ID="hiddenThings" Visible="false">

        <asp:Label runat="server" ID="entersortmap"></asp:Label>
        <asp:Label runat="server" ID="entersortdir"></asp:Label>       

    </asp:Panel>

       <cc1:tabcontainer ID="PFO_Group_Mapping" runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "PPM Mapping" ID = "tpPFO_Group" >
                    <ContentTemplate>    

 
    <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px">

        <asp:Table runat="server" >
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">Enter User Login</asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"><asp:TextBox runat="server" ID="txtEnterUserID"></asp:TextBox>

<br />
                    <asp:LinkButton runat="server" ID="lbSrchUsr" Font-Size="X-Small" Text="Don't know UserLogin?"></asp:LinkButton><br />
                    <asp:Panel runat="server" ID="pnlSrchUser" Visible="False" BackColor="#CBE3FB">
                        <asp:Table runat="server"><asp:TableRow runat="server"><asp:TableCell Width="5px" runat="server"></asp:TableCell>
<asp:TableCell runat="server">
                                                        Enter Name:
                                </asp:TableCell>
<asp:TableCell runat="server"><asp:TextBox runat="server" ID="txtAdminUsrSrch"></asp:TextBox>
</asp:TableCell>
<asp:TableCell runat="server"><asp:Button runat="server" ID="btnAdminUsrSrch" Text="Search" />
</asp:TableCell>
<asp:TableCell runat="server"><asp:LinkButton runat="server" ID="lbCloseUsrSrch" Font-Size="X-Small" Text="Close Search"></asp:LinkButton>
</asp:TableCell>
<asp:TableCell Width="5px" runat="server"></asp:TableCell>
</asp:TableRow>
<asp:TableRow runat="server"><asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle" runat="server"><asp:UpdateProgress runat="server" ID="updateProgressSearching"><ProgressTemplate>
                                            <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='../AR/Images/PngB.png'" onmouseout="this.src='../AR/Images/PngA.png'" />
                                        
</ProgressTemplate>
</asp:UpdateProgress>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow runat="server"><asp:TableCell Width="5px" runat="server"></asp:TableCell>
<asp:TableCell ColumnSpan="10" VerticalAlign="Middle" runat="server"><asp:Label runat="server" ID="lblAdminUsrResults"></asp:Label>
</asp:TableCell>
</asp:TableRow>
</asp:Table>



                    </asp:Panel>
</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell>
</asp:TableCell>
                <asp:TableCell>Select Source System
                </asp:TableCell>
                <asp:tableCell></asp:tableCell>
                <asp:TableCell >
                    <asp:DropDownList runat="server" ID="ddlEnterSourceSystem" AutoPostBack="true"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>Select Practice</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddlEnterPractice" AutoPostBack="true"></asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="10px">
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button runat="server" ID="btnClearPractice" Text="Clear Practice" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>Select Role:</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddlEnterRole"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="btnEnterNewRole" Text="Add Role" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
   </asp:Panel>
        <br />
        <asp:GridView runat="server" ShowHeaderWhenEmpty="true" ID="gvShowAllEntries" AutoGenerateColumns="false" Width="90%" BorderColor="#003060" BorderWidth="1px"
            BackColor="#CBE3FB" AllowPaging="true" CellSpacing="5" HeaderStyle-Font-Size="X-Small"
            AllowSorting="true" PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5" >
            <AlternatingRowStyle BackColor="white" />
            <Columns>
                <asp:BoundField  HeaderText=""></asp:BoundField>
                <asp:BoundField DataField="Person" HeaderText="Person"
                    SortExpression="Person"></asp:BoundField>
                <asp:BoundField DataField="Role" HeaderText="Role"
                    SortExpression="Role"></asp:BoundField>
                <asp:BoundField DataField="SourceSystem" HeaderText="SourceSystem"
                    SortExpression="SourceSystem"></asp:BoundField>
                <asp:BoundField DataField="GroupID" HeaderText="GroupID"
                    SortExpression="GroupID"></asp:BoundField>
                <asp:BoundField DataField="PracticeName" HeaderText="PracticeName"
                    SortExpression="PracticeName"></asp:BoundField>
                <asp:BoundField DataField="GroupRollup" HeaderText="GroupRollup"
                    SortExpression="GroupRollup"></asp:BoundField>

<%--                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Panel runat="server">
                            <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveRow" CommandArgument='<%# Bind("DepartmentID")%>'></asp:LinkButton>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>--%>

            </Columns>
        </asp:GridView>
        



                      
        
               <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6DA9E3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow runat="server"><asp:TableCell Height="20px" runat="server"></asp:TableCell></asp:TableRow>
           <asp:TableRow runat="server"><asp:TableCell Width="10px" runat="server"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB" runat="server"><asp:label ID = "explantionlabel" runat = "server"></asp:label>
</asp:TableCell><asp:TableCell Width="10px" runat="server"></asp:TableCell> </asp:TableRow><asp:TableRow runat="server"><asp:TableCell Height="20px" runat="server"></asp:TableCell></asp:TableRow>
     <asp:TableRow runat="server"><asp:TableCell ColumnSpan="3"  VerticalAlign="Middle" HorizontalAlign="Center" runat="server"><asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "Small" Text="OK"/>
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
         <asp:TableCell runat="server"><asp:Button ID="ConfirmButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Confirm" />
</asp:TableCell>
         <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"><asp:Button ID="CancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Cancel" />
</asp:TableCell>
     </asp:TableRow>        
     <asp:TableRow runat="server"><asp:TableCell Height="10px" runat="server"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="Panel1"
                DropShadow="True" BehaviorID="_content_ModalPopupExtender1" 
                 ></cc1:ModalPopupExtender>
   
                        </ContentTemplate>
                        </cc1:TabPanel>
 
           </cc1:tabcontainer>

    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
