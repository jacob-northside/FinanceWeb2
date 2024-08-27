<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CathLabMapping.aspx.vb" Inherits="FinanceWeb.CathLabMapping" %>

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

            <cc1:TabContainer ID="CathLabMapping" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1150px">

                <cc1:TabPanel runat="server" HeaderText="Case Overwrite" ID="tpCathCaseEvent">
                    <ContentTemplate>
                        <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px">
                            <asp:Table runat="server">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Proc Case Event ID:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblEventID" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Proc ID:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblProcID" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Module:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblModule" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">First Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblFirstName" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Last Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblLastName" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Location:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblLocation" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Case Start Date (MM/DD/YYYY)</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtEnterStartDate"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Case End Date (MM/DD/YYYY)</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtEnterEndDate"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Patient on Table Date (MM/DD/YYYY)</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtEnterTableDate"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Proc Date:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblProcDate" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button runat="server" ID="btnUpdateEvent" Text="Update Dates" /></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                        </asp:Panel>

                        <br />

                        <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px" Visible="true">
                            <asp:Table runat="server">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server" Font-Bold="true" ColumnSpan="15">Enter data and click 'Filter Table' to apply filters to the table. Click 'Reset Table' to remove filters.</asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table runat="server">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Case Event ID:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtFilterCaseEventID"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Patient Last Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtFilterLastName"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Patient First Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtFilterFirstName"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Patient Location:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:RadioButtonList runat="server" AutoPostBack="true" RepeatDirection="Horizontal" ID="rblLocation">
                                            <asp:ListItem Value="NSA" Text="NSA" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="NHC" Text="NHC" Selected="False"> </asp:ListItem>
                                            <asp:ListItem Value="NSF" Text="NSF" Selected="False"> </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Minimum Case Duartion:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtFilterCaseDuration"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Minimum Table to Start Duration:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtFilterTableToStartDuration"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Minimum Procedure Date:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtProcDate"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button runat="server" ID="btnFilterTable" Text="Filter Table" />
                                        <asp:Button runat="server" ID="btnResetTable" Text="Reset Table" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <style>
                .HiddenRow{display:none}
                .HiddenHeader{display:none}
            </style>


                            <asp:GridView runat="server" ShowHeaderWhenEmpty="true" ID="gvShowCathEvent" AutoGenerateColumns="false" Width="99%" BorderColor="#003060" BorderWidth="1px"
                                BackColor="#CBE3FB" AllowPaging="true" HeaderStyle-Font-Size="X-Small" HorizontalAlign="Center"
                                AllowSorting="true" PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White">
                                <AlternatingRowStyle BackColor="white" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                                    <asp:BoundField DataField="proc_case_event_id" HeaderText="Event ID"
                                        SortExpression="proc_case_event_id"></asp:BoundField>
                                    <asp:BoundField DataField="procid" HeaderText="Proc ID"
                                        SortExpression="procid"></asp:BoundField>
                                    <asp:BoundField DataField="module" HeaderText="Module"
                                        SortExpression="module"></asp:BoundField>
                                    <asp:BoundField DataField="location" HeaderText="Patient Location"
                                        SortExpression="location" />
                                    <asp:BoundField DataField="Patient_firstname" HeaderText="First Name"
                                        SortExpression="Patient_firstname"></asp:BoundField>
                                    <asp:BoundField DataField="Patient_lastname" HeaderText="Last Name"
                                        SortExpression="Patient_lastname"></asp:BoundField>
                                    <asp:BoundField DataField="case_start" HeaderText="Case Start"
                                        SortExpression="case_start"></asp:BoundField>
                                    <asp:BoundField DataField="case_end" HeaderText="Case End"
                                        SortExpression="case_end"></asp:BoundField>
                                    <asp:BoundField DataField="patient_on_table" HeaderText="Patient on Table"
                                        SortExpression="patient_on_table"></asp:BoundField>
                                    <asp:BoundField DataField="Case_Duration" HeaderText="Case Duration"
                                        SortExpression="Case_Duration"></asp:BoundField>
                                    <asp:BoundField DataField="on_table_to_case_start" HeaderText="Table to Start Duration"
                                        SortExpression="on_table_to_case_start"></asp:BoundField>
                                    <asp:BoundField DataField="procedure_date" HeaderText="Procedure_Date" HeaderStyle-CssClass="HiddenHeader"
                                        SortExpression="Procedure_Date" ItemStyle-CssClass="HiddenRow" DataFormatString="{0:MM/dd/yyyy }" HtmlEncode="false"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <br />
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

                <cc1:TabPanel runat="server" HeaderText="Employee Mapping" ID="tp_CathEmployeeMapping">
                    <ContentTemplate>
                        <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px">
                            <asp:Table runat="server">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Employee ID:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblEmployeeID" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Group Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:TextBox runat="server" ID="txtEnterGroupName"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">NPI:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblNPI" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblDisplayName" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Location:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="lblPrimaryLocation" Text="Select a row from the table"></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button runat="server" ID="btnMapGroup" Text="Map Group Name" /></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <br />

                        <asp:Panel runat="server" ScrollBars="Auto" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1130px">
                            <asp:Table runat="server">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">Group Name:</asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:DropDownList runat="server" ID="ddlSelectGroupName" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table runat="server">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell><asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server" HorizontalAlign="left" ColumnSpan="1"><asp:Label runat="server" Text="Only show Employee IDs which need to be mapped" ></asp:Label></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:CheckBox Font-Size="X-Small" AutoPostBack="true" runat="server" ID="cbShowOnlyMapped" Checked="false" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:GridView runat="server" ShowHeaderWhenEmpty="true" ID="gvShowEmployee" AutoGenerateColumns="false" Width="90%" BorderColor="#003060" BorderWidth="1px"
                                BackColor="#CBE3FB" AllowPaging="true" HeaderStyle-Font-Size="X-Small" HorizontalAlign="Center"
                                AllowSorting="true" PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White">
                                <AlternatingRowStyle BackColor="white" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                                    <asp:BoundField DataField="empid" HeaderText="Empoyee ID"
                                        SortExpression="empid"></asp:BoundField>
                                    <asp:BoundField DataField="npi" HeaderText="NPI"
                                        SortExpression="NPI" />
                                    <asp:BoundField DataField="display_name" HeaderText="Name"
                                        SortExpression="display_name" />
                                    <asp:BoundField DataField="primary_location" HeaderText="Location"
                                        SortExpression="primary_location" />
                                    <asp:BoundField DataField="groupname" HeaderText="Group Name"
                                        SortExpression="groupname"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <br />
                        </asp:Panel>


                        <asp:Label ID="FakeButton3" runat="server" />
                        <asp:Panel ID="Panel3" runat="server" Width="233px" BackColor="#6DA9E3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB" runat="server" ColumnSpan="2">
                                        <asp:Label ID="explantionlabel3" runat="server" Width="125px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center" runat="server">
                                        <asp:Button ID="OkButton3" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="Small" Text="OK" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="ConfirmButton3" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="CancelButton3" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Cancel" /></asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell Height="10px" runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" X="875" Y="200"
                            TargetControlID="FakeButton3"
                            PopupControlID="Panel3"
                            DropShadow="True" BehaviorID="_content_ModalPopupExtender3">
                        </cc1:ModalPopupExtender>



                        <asp:Label ID="FakeButton4" runat="server" />
                        <asp:Panel ID="Panel4" runat="server" Width="233px" BackColor="#6DA9E3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB" runat="server" ColumnSpan="2">
                                        <asp:Label ID="explantionlabel4" runat="server" Width="125px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center" runat="server">
                                        <asp:Button ID="OkButton4" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="Small" Text="OK" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server"></asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="ConfirmButton4" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Button ID="CancelButton4" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="False" Font-Size="Small" Text="Cancel" /></asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell Height="10px" runat="server"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" X="875" Y="200"
                            TargetControlID="FakeButton4"
                            PopupControlID="Panel4"
                            DropShadow="True" BehaviorID="_content_ModalPopupExtender4">
                        </cc1:ModalPopupExtender>

                    </ContentTemplate>
                </cc1:TabPanel>

            </cc1:TabContainer>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
