<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LESCOR User Details.aspx.vb" Inherits="FinanceWeb.LESCORUserDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LESCOR - User Details</title>
</head>
<body>
   
    <form id="form1" runat="server">

         <style type="text/css">
            .hidden   
 {        display: none;    
          }

             </style>

<%--     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>

    

            <cc1:TabContainer ID="UserDetails" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel runat = "server" HeaderText = "User Department Permissions" ID = "tpUserDepartments" >
                    <ContentTemplate>

                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    UserLogin:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtnewUser"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button runat="server" ID="btnSearchNewUser" Text="Search" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    <asp:LinkButton runat="server" ID="lbSrchUsr" Font-Size="X-Small" Text="Don't know UserLogin?"></asp:LinkButton>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <asp:Panel runat="server" ID="pnlSrchUser" Visible="false" BackColor="#CBE3FB">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell>
                                                        Enter Name:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminUsrSrch"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnAdminUsrSrch" Text="Search" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:LinkButton runat="server" ID="lbCloseUsrSrch" Font-Size="X-Small" Text="Close Search"></asp:LinkButton>
                                    </asp:TableCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                                        <asp:UpdateProgress runat="server" ID="updateProgressSearching">
                                            <ProgressTemplate>
                                                <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='../AR/Images/PngB.png'" onmouseout="this.src='../AR/Images/PngA.png'" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="10" VerticalAlign="Middle">
                                        <asp:Label runat="server" ID="lblAdminUsrResults"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:Panel>

     

                <asp:Panel runat="server" Visible="true" ID="EmptyPanel">
            <asp:Panel BackColor="#6da9e3" runat="server" Width="600px" Height="40" ForeColor="White">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            <asp:Label runat="server" ID="lblEmptyExplanation"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

             </asp:Panel>   
            </asp:Panel>

        <asp:Panel runat="server" Visible="false" ID="FullPanel" >

                       
            <asp:Panel BackColor="#6da9e3" runat="server" Width="600px" ForeColor="White">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell Height="5px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="10px"></asp:TableCell>
                        <asp:TableCell Width="150px" HorizontalAlign="left" VerticalAlign="Top">
                            User Login:
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblUserLogin" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="10px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell HorizontalAlign="left" VerticalAlign="Top">
                            User Name:
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="lblUserName" ></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>                           
                    <asp:TableRow>
                        <asp:TableCell Height="5px"></asp:TableCell>
                    </asp:TableRow>
                   
                </asp:Table>

             </asp:Panel>   
            
               <br />
            <asp:Label runat="server" Text="Test" CssClass="hidden"></asp:Label>
            <asp:Panel runat="server" ID="ScrollPanel" ScrollBars="Auto">
<asp:GridView ID="gvDepartments" runat="server" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="false" CellSpacing ="0" 
    HeaderStyle-Font-Size ="Medium" AllowSorting="false" Font-Size="Small" width ="100%" DataKeyNames="DepartmentID, Position"
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3">
                                 <AlternatingRowStyle BackColor="white" />
    <Columns>
        
        <asp:TemplateField HeaderText="This User has access to the following Departments">
            <ItemTemplate>
                <asp:Panel CssClass="paneltest" Width="95%" runat="server">                    
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("DepartmentName")%>' Visible="true"></asp:Label>
                </asp:Panel>              
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Position">
            <ItemTemplate>
                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                    <asp:Label ID="lblPosition" runat="server" Text='<%# Bind("RoleFull")%>' Visible="true"></asp:Label>
                </asp:Panel>
                <asp:TextBox runat="server" ID="txtPosition" Text='<%# Bind("RoleFull")%>' Visible="false"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:Panel runat="server">
                    <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveAccess" CommandArgument='<%# Bind("Flipper")%>'></asp:LinkButton>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Receive Emails">
            <ItemTemplate>
                <asp:Panel runat="server">
                    <asp:LinkButton ID="btnFlipEmails" runat="server" Text='<%# Bind("Emails")%>' CommandName="Emails" CommandArgument='<%# Bind("Flipper")%>'></asp:LinkButton>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>
            </asp:Panel>
            <asp:Table runat="server">
                <asp:TableRow>
<asp:TableCell>
                <asp:Panel runat="server" ID="pnlGrantAccess" Visible="true">
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>Position:</asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlGrantPosition" AutoPostBack="true"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Department:</asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlGrantDepartment" AutoPostBack="true"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3">
                                <asp:Button runat="server" ID="btnGrantAccess" Text="Grant Access" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            


            </asp:Panel>

               


                        </ContentTemplate>
                        </cc1:TabPanel>
                </cc1:TabContainer>

                <asp:Label ID="FakeButtonp4" runat="server" />
                <asp:Panel ID="Panelp4" runat="server" Width="400px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                    <asp:Table runat="server" Width="100%" Height="100%">
                        <asp:TableRow>
                            <asp:TableCell Height="20px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="10px"></asp:TableCell>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                <asp:Label ID="mpeExplanationLabel" runat="server"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Height="20px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell  VerticalAlign="Middle" HorizontalAlign="Center">
                                <asp:Button ID="ConfirmButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Confirm" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="OkayButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="CancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Height="10px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <br />
                <cc1:ModalPopupExtender ID="mpeDetails" runat="server"
                    TargetControlID="FakeButtonp4"
                    PopupControlID="Panelp4"
                    DropShadow="true">
                </cc1:ModalPopupExtender>


            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

