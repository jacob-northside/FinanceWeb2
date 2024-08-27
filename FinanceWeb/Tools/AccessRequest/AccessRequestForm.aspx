<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="AccessRequestForm.aspx.vb" Inherits="FinanceWeb.AccessRequestForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            width: 300px;
            height: 140px;
        }

        .maxpanelheight
{
    max-height:600px;
}
    </style>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel runat="server" ID="upWholeThing">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" Visible="false">

                <asp:Label runat="server" ID="Developer"></asp:Label>
                <asp:Label runat="server" ID="Admin"></asp:Label>

            </asp:Panel>


            <cc1:TabContainer ID="tcAccessRequests" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1165px">

                <cc1:TabPanel runat="server" HeaderText="Request Access" ID="tpRequestAccess">
                    <ContentTemplate>
                        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
 <asp:Label runat="server" ID="lblOpenUserLogin"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
   <asp:Table runat="server">

                                <asp:TableRow>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Top">Network User ID:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell >
                                        <asp:TextBox CssClass="form-control"  runat="server" ID="txtReviewUserID"></asp:TextBox>
                                          <br />
                                        <asp:LinkButton runat="server" ID="lbSrchUsr" Font-Size="X-Small" Text="Don't know UserLogin?"></asp:LinkButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>User Name:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell >
                                        <asp:Label runat="server" ID="lblReviewUserName"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>                                   
                                    <asp:TableCell>Department:
                                    </asp:TableCell><asp:TableCell></asp:TableCell>
                                    
                                    <asp:TableCell  >
                                        <asp:DropDownList Width="250px" runat="server" CssClass="form-control" ID="ddlReviewUserDept"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>Email:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox Width="250px" runat="server" CssClass="form-control" ID="txtReviewUserEmail"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>Phone:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox Width="250px" runat="server" CssClass="form-control" ID="txtReviewUserPhone"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>                                
                            </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell  ColumnSpan="5">
  <asp:Panel runat="server" ID="pnlPendingRequests">
                                
                                Pending Requests:<br />
                        <asp:GridView ID="gv_UserPendingRequests" runat="server" Width="90%" HorizontalAlign="Center"
                                AllowSorting="True" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" BorderColor="Black"
                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#9FCFFF"
                                HeaderStyle-ForeColor="#003060" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-Wrap="true" ForeColor="#003060" DataKeyNames="RequestID"
                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                <AlternatingRowStyle BackColor="white" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5px" FooterStyle-Width="5px">
                                        <ItemStyle />
                                        <FooterStyle />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RequestType" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                        HeaderText="Request Type" ReadOnly="True"
                                        Visible="True"></asp:BoundField>
                                    <asp:BoundField DataField="RequestDescription" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                        HeaderText="Request Description" ReadOnly="True"
                                        Visible="True">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                        HeaderText="Status" ReadOnly="True"
                                        Visible="True"></asp:BoundField>
                                    <asp:BoundField DataField="RequestDate" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                        HeaderText="Request Submitted" ReadOnly="True"
                                        Visible="True"></asp:BoundField>
                                </Columns>
                            </asp:GridView>

                            </asp:Panel>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>                                    
                                    <asp:TableCell ColumnSpan="3">Add Request:<br />
 <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInitialRequestType" Width="200px" AutoPostBack="true"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                   <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:DropDownList Width="200px" runat="server" Visible="false" CssClass="form-control" ID="ddlInitialRequestDetailType"></asp:DropDownList>
                                        <asp:Label runat="server" ID="lblInitialRequest" Text="Please specify:  "></asp:Label><asp:TextBox Width="200px" runat="server" Visible="false" CssClass="form-control" ID="txtInitialRequestDetail"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Top">Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" Width="400px" CssClass="form-control" ID="txtInitialRequestDescription"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Top">Justification:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Width="400px" ID="txtInitialRequestJustification"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnInitialRequestSubmittal" Text="Submit Request" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                           
                        
                        </asp:Panel>
                    </ContentTemplate>

                </cc1:TabPanel>
            </cc1:TabContainer>


            <cc1:ModalPopupExtender ID="mpeUserSearch" runat="server" PopupControlID="SearchUserPanel" TargetControlID="fakeButtonModal1"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Label runat="server" ID="fakeButtonModal1"></asp:Label>
            <asp:Panel ID="SearchUserPanel" runat="server" Style="display: none; position: relative">
                <asp:Table runat="server" Width="600px" BackColor="White" BorderColor="Black" BorderStyle="Double" BorderWidth="3">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel CssClass="maxpanelheight" ScrollBars="Auto" runat="server" ID="pnlSrchUser" Visible="false" Width="600px">
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
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
               
                          <asp:Button CssClass="btn btn-success" ID="btnMPECloseSearch" runat="server" Text="Close" />                   
                            
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>



            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
