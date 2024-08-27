<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StarDeptMapping.aspx.vb" Inherits="FinanceWeb.StarDeptMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>--%>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" Visible="false">

                <asp:Label runat="server" ID="entersortmap"></asp:Label>
                <asp:Label runat="server" ID="entersortdir"></asp:Label>

            </asp:Panel>



            <cc1:TabContainer ID="Star_Dept_Mapping" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1150px">

                <cc1:TabPanel runat="server" HeaderText="Star Dept Mapping" ID="tp_Star_Dept_Mapping">
                    <ContentTemplate>
                        <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px">
                            <asp:Table runat="server">
                                <asp:TableRow runat="server" VerticalAlign="Top">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server" VerticalAlign="Middle">Facility:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RadioButtonList runat="server" AutoPostBack="true" RepeatDirection="Vertical" ID="rblFacility" RepeatColumns="3">
                                            <asp:ListItem Value="A" Text="Atlanta" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="D" Text="Duluth" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="C" Text="Cherokee" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="L" Text="Lawrenceville" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="F" Text="Forsyth" Selected="False"> </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Location:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:DropDownList runat="server" ID="ddlSelectOutpatientLocation" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Admitting Dept:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtEnterAdmittingRCC"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Merchant Description:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtEnterMerchantDesc" TextMode="MultiLine" Rows="3"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" VerticalAlign="Top">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Flag Name:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RadioButtonList runat="server" AutoPostBack="true" RepeatDirection="Vertical" ID="rblFlagName">
                                            <asp:ListItem Value="PAS Status" Text="PAS Status" Selected="True"> </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" VerticalAlign="Top">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Flag Value:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RadioButtonList runat="server" AutoPostBack="true" RepeatDirection="Vertical" ID="rblFlagValue">
                                            <asp:ListItem Value="PAS" Text="PAS" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="NON PAS" Text="NON PAS" Selected="False"> </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button runat="server" ID="btnAddMapping" Text="Map New Location" /></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button runat="server" ID="btnUpdateMapping" Text="Update Existing Location" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px">
                            <asp:Table runat="server">
                                <asp:TableRow runat="server"></asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server" HorizontalAlign="left" ColumnSpan="4"><asp:Label runat="server" Text="Only show locations which need to be mapped" Font-Bold="true"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:CheckBox Font-Size="X-Small" AutoPostBack="true" runat="server" ID="cbShowOnlyMapped" Checked="false" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server" Font-Bold="true" ColumnSpan="4">Optional Filters:</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Facility: </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:DropDownList runat="server" ID="ddlFilterFacility" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Location:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:DropDownList runat="server" ID="ddlFilterLoc" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                                </asp:TableRow>
                                <%--            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server">Admitting Dept:</asp:TableCell>
                <asp:TableCell runat="server"><asp:DropDownList runat="server" ID="ddlFilterRCC" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>--%>
                            </asp:Table>
                            <asp:GridView runat="server" ShowHeaderWhenEmpty="true" ID="gvShowResults" AutoGenerateColumns="false" Width="90%" BorderColor="#003060" BorderWidth="1px"
                                BackColor="#CBE3FB" AllowPaging="true" CellSpacing="5" HeaderStyle-Font-Size="X-Small" HorizontalAlign="Center"
                                AllowSorting="true" PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5">
                                <AlternatingRowStyle BackColor="white" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="View Details" />
                                    <asp:BoundField DataField="ID" HeaderText="ID"
                                        SortExpression="ID"></asp:BoundField>
                                    <asp:BoundField DataField="Facility" HeaderText="Facility"
                                        SortExpression="Facility"></asp:BoundField>
                                    <asp:BoundField DataField="Location" HeaderText="Outpatient Location"
                                        SortExpression="Location"></asp:BoundField>
                                    <asp:BoundField DataField="Revenue_Code" HeaderText="Admitting Dept"
                                        SortExpression="Revenue_Code"></asp:BoundField>
                                    <asp:BoundField DataField="PAS_STATUS" HeaderText="PAS Value"
                                        SortExpression="PAS_STATUS"></asp:BoundField>
                                    <asp:BoundField DataField="Merchant_Desc" HeaderText="Merchant Desc"
                                        SortExpression="Merchant_Desc"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                        <asp:Label ID="FakeButton" runat="server" />
                        <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6DA9E3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB" runat="server" ColumnSpan="2">
                                        <asp:Label ID="explantionlabel" runat="server" Width="125px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center" runat="server">
                                        <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="Small" Text="OK" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="ConfirmButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="CancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Cancel" /></asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell Height="10px" runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" X="875" Y="200"
                            TargetControlID="FakeButton"
                            PopupControlID="Panel1"
                            DropShadow="True" BehaviorID="_content_ModalPopupExtender1">
                        </cc1:ModalPopupExtender>



                        <asp:Label ID="FakeButton2" runat="server" />
                        <asp:Panel ID="Panel2" runat="server" Width="233px" BackColor="#6DA9E3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB" runat="server" ColumnSpan="2">
                                        <asp:Label ID="explantionlabel2" runat="server" Width="125px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center" runat="server">
                                        <asp:Button ID="OkButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="Small" Text="OK" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="ConfirmButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="CancelButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Cancel" /></asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell Height="10px" runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" X="875" Y="200"
                            TargetControlID="FakeButton2"
                            PopupControlID="Panel2"
                            DropShadow="True" BehaviorID="_content_ModalPopupExtender2">
                        </cc1:ModalPopupExtender>

                    </ContentTemplate>
                </cc1:TabPanel>

            </cc1:TabContainer>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
