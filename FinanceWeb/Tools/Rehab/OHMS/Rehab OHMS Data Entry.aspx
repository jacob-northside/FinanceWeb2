<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Rehab OHMS Data Entry.aspx.vb" Inherits="FinanceWeb.Rehab_OHMS_Data_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">

    .CursorHand {
    cursor:pointer;
}


        .Txtform-control {
  height: 20px;   
   padding: 4px 6px; 
  color: #555555;
  vertical-align: middle;
  background-color: #ffffff;
  background-image: none;
  border: 1px solid #cccccc;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
  -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
          transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
    
}

.Txtform-control:focus {
  border-color: #66afe9;
  outline: 0;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
}

.Txtform-control::-webkit-input-placeholder{
    color:lightgray;
}

.MaxPanel {
    max-height:600px;
}

        </style>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <script type="text/javascript" charset="utf-8">


            function filter() {
                var maybeObject = $get('<%=HideDenom.ClientID%>');
                if (maybeObject != null) {
                    if (maybeObject.innerText.toLowerCase() == 'false') {
                        var grid = $get('<%=gvSubmitData.ClientID%>');
                        var ele4;
                        var ele5;
                        var ele6;
                        var num;
                        var denom;
                        for (var r = 1; r < grid.rows.length; r++) {
                            num = -1
                            denom = -1
                            col4 = grid.rows[r].cells[4]
                            col5 = grid.rows[r].cells[5]
                            col6 = grid.rows[r].cells[6]
                            for (j = 0; j < col4.childNodes.length; j++) {
                                if (col4.childNodes[j].type == "text") {
                                    if (!isNaN(col4.childNodes[j].value) && col4.childNodes[j].value != "") {
                                        num = parseInt(col4.childNodes[j].value)
                                    }
                                };
                            };
                            for (k = 0; k < col5.childNodes.length; k++) {
                                if (col5.childNodes[k].type == "text") {
                                    if (!isNaN(col5.childNodes[k].value) && col5.childNodes[k].value != "") {
                                        denom = parseInt(col5.childNodes[k].value)
                                    }
                                };
                            };

                            if (denom <= 0) {
                                col6.innerText = ""
                            }
                            else col6.innerText = parseFloat(num * 100 / denom).toFixed(2) + "%";

                        }
                    }
                }
                ;
            };

            (function () {
                var focusElement;
                function restoreFocus() {
                    if (focusElement) {
                        if (focusElement.id) {
                            $('#' + focusElement.id).focus();
                        } else {
                            $(focusElement).focus();
                        }
                    }
                }

                $(document).ready(function () {
                    $(document).on('focusin', function (objectData) {
                        focusElement = objectData.currentTarget.activeElement;
                    });
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(restoreFocus);
                });
            })();
           


  
        
    </script>


    <asp:UpdatePanel runat="server" ID="upWholeThing">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" CssClass="hidden">

                <asp:Label runat="server" ID="RecordedDate" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="HideDenom" CssClass="hidden"></asp:Label>

            </asp:Panel>


            <cc1:TabContainer ID="tcRehabOHMS" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1151px">

                <cc1:TabPanel runat="server" HeaderText="Outpatient Appointment Availability" ID="tpOPApptAvailability">
                    <ContentTemplate>
                        <asp:Table runat="server" CssClass="StandardWidthTable" Width="800px">
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell Width="200px">
                                    Select Location:
                                </asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList Width="200px" runat="server" AutoPostBack="true" CssClass="TAform-control" ID="ddlOPApptLocation"></asp:DropDownList>
                                </asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    Select Date to Record:
                                </asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox Width="190px" runat="server" ID="txtOPApptDate" AutoPostBack="true" CssClass="Txtform-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" TargetControlID="txtOPApptDate" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                                    <asp:GridView runat="server" ID="gvOPApptResults" DataKeyNames="SpecID"
                                        AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                        HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                        ForeColor="Black" HeaderStyle-Wrap="true" HeaderStyle-Height="30px"
                                        BorderWidth="1px"
                                        BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                        Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="10px" />
                                            <asp:BoundField ItemStyle-Width="200px" DataField="SpecialtyDescription" HeaderText="Specialty" />

                                            <asp:TemplateField HeaderText="Next Third Appointment">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtValues" Width="200px" Text='<%# Bind("NextAvailableAppt", "{0:d}")%>' CssClass="Txtform-control"></asp:TextBox>
                                                    <cc1:CalendarExtender runat="server" TargetControlID="txtValues" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comment">
                                                <ItemTemplate>
                                                    <asp:TextBox Width="300px" CssClass="Txtform-control" Text='<%# Bind("Comment")%>' runat="server" ID="txtComment" Placeholder="(Optional)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--                            <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button Width="100px" runat="server" ID="btnSubmitRecord" Text="Submit" CssClass="TAform-control" 
                                            CommandName="SubmitLine" CommandArgument='<%# Bind("SpecID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">

                                    <asp:Button Width="200px" Visible="false" runat="server" ID="btnSubmitRecords" Text="Submit" CssClass="TAform-control" />

                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <br />
                        <br />


                    </ContentTemplate>


                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Specialty Administration" ID="tpSpecAdmin" Visible="false">
                    <ContentTemplate>
                        <asp:Table runat="server" CssClass="StandardWidthTable">
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
Add New Specialty:
                                </asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtNewSpecialty" CssClass="Txtform-control" Width="200px"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button Font-Size="Small" runat="server" ID="btnAddSpec" Text="Add" Width="100px" CssClass="TAform-control" Height="25px" />

                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Height="20px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableHeaderRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableHeaderCell>
                                    <b>Manage Locations</b>
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell ColumnSpan="3">
                                    <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlLocationChoices" CssClass="TAform-control" Width="200px"></asp:DropDownList>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell ColumnSpan="5">

                                    <asp:GridView runat="server" ID="gvLocationSpecialties" DataKeyNames="SpecID"
                                        AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                        HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                        ForeColor="Black" HeaderStyle-Wrap="true" HeaderStyle-Height="30px"
                                        BorderWidth="1px"
                                        BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                        Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="10px" />
                                            <asp:BoundField DataField="SpecialtyDescription" HeaderText="Specialty" />
                                            <asp:BoundField DataField="Status" HeaderText="Current Status" ItemStyle-Width="200px" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button Width="150px" Height="25px" runat="server" ID="btnAddRemoveSpec" Text='<%# Bind("Button")%>' CssClass="TAform-control"
                                                        CommandName='AddRemoveSpec' CommandArgument='<%# Bind("SpecID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" Visible="false" HeaderText="User Administration" ID="tpUserAdmin">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlUservsLocation" CssClass="TAform-control" Width="200px">
                            <asp:ListItem Text="Manage User Access by Locations" Value="Loc"></asp:ListItem>
                            <asp:ListItem Text="Manage User Access by User" Value="User"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:Panel runat="server" ID="pnlManageUsers" Visible="false">
                            Search Users:
                            <asp:TextBox runat="server" ID="txtUserSearch" CssClass="Txtform-control" Width="200px"></asp:TextBox>
                            <asp:GridView runat="server" ID="gvUserManagement" DataKeyNames="UserLogin"
                                AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                ForeColor="Black" HeaderStyle-Wrap="true" HeaderStyle-Height="30px"
                                BorderWidth="1px" CssClass="CursorHand"
                                BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ItemStyle-Width="10px" ShowSelectButton="True" Visible="True" SelectText="" />

                                    <asp:BoundField DataField="UserLogin" HeaderText="UserLogin" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button Width="100px" runat="server" ID="btnAddRemoveSpec" Text='Remove All Access' CssClass="TAform-control"
                                                CommandName='RemoveUser' CommandArgument='<%# Bind("UserLogin")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                            <br />
                            <asp:Label runat="server" ID="lblSelectedUser"></asp:Label>
                            has access to update data for the following Locations:<br />
                            <asp:Label runat="server" ID="lblNone" Text="None"></asp:Label>
                            <asp:GridView runat="server" ID="gvUserCurrentDepts" DataKeyNames="LocID"
                                AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                ForeColor="Black" HeaderStyle-Wrap="true" HeaderStyle-Height="30px"
                                BorderWidth="1px"
                                BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="LocationDesc" HeaderText="Location" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button Width="100px" runat="server" ID="btnAddRemoveSpec" Text='Remove Access' CssClass="TAform-control"
                                                CommandName='RemoveLocAccess' CommandArgument='<%# Bind("LocID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>

                            <asp:DropDownList runat="server" ID="ddlAddDepartment"></asp:DropDownList>
                            <asp:Button runat="server" ID="btnAddDepartment" Text="Add Location" />
                        </asp:Panel>


                        <asp:Panel runat="server" ID="pnlManageDepts">
                            Select Location:
                            <asp:DropDownList runat="server" ID="ddlAdminLocation"></asp:DropDownList>

                            Users who currently have access here:
                            <asp:GridView runat="server" ID="GridView1" DataKeyNames="UserLogin"
                                AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                ForeColor="Black" HeaderStyle-Wrap="true" HeaderStyle-Height="30px"
                                BorderWidth="1px" CssClass="CursorHand"
                                BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ItemStyle-Width="10px" ShowSelectButton="True" Visible="True" SelectText="" />

                                    <asp:BoundField DataField="UserLogin" HeaderText="UserLogin" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button Width="100px" runat="server" ID="btnAddRemoveSpec" Text='Remove All Access' CssClass="TAform-control"
                                                CommandName='RemoveUser' CommandArgument='<%# Bind("UserLogin")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>

                            <br />
                            Add User:<asp:TextBox Placeholder="(Enter UserLogin)" runat="server" ID="txtCheckUserExists" AutoPostBack="true"
                                CssClass="Txtform-control" Width="200px"></asp:TextBox>
                            <asp:LinkButton runat="server" ID="lbDontKnowUserLogin" Text="Don't know UserLogin?"></asp:LinkButton>

                            <asp:Label runat="server" ID="lblSelectedUserToAdd"></asp:Label>
                            <asp:Button CssClass="TAform-control" Visible="false" runat="server" ID="btnAdminAddUser" Text="Add User" />
                        </asp:Panel>

                    </ContentTemplate>





                </cc1:TabPanel>

                <cc1:TabPanel runat="server" HeaderText="Monthly Data Entry" ID="tpMonthlyEntry">
                    <ContentTemplate>


                        <br />




                        <asp:Panel runat="server" ScrollBars="Auto">
                            <asp:Table runat="server" BackColor="#CBE3FB" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow Visible="true">
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell Width="200px" ForeColor="Black">
                         Metric:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlSelectMonthlyMetric" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell><asp:TableHeaderCell ForeColor="Black">
                         Location:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlSelectMonthlyLocation" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trObj" Visible="false">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        Metric Objective:
                                        <asp:Label runat="server" ID="lblMetricObjective"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trDef" Visible="false">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        Metric Definition:
                                                <asp:Label runat="server" ID="lblMetricDefinition"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" Font-Size="Small" Font-Italic="true">
                                                *You can enter missing data for historical months, but if you want to update old data, you will need to contact your administrator
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <br />
                            <br />
                            <asp:Panel
                                ID="ScrollPanelgvSubmitData" runat="server" ScrollBars="Auto" CssClass="MaxPanel">
                                <asp:GridView ID="gvSubmitData" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="dID" CssClass="GridViewFocus"
                                    CellPadding="4" BorderColor="Black" BackColor="#CBE3FB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Wrap="true" ForeColor="Black"
                                    BorderWidth="1px" AllowSorting="True"
                                    BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                    Font-Size="X-Small">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <%--   <asp:TemplateField HeaderText="dID" ItemStyle-Width="25" ItemStyle-CssClass="hidden" HeaderStyle-CssClass ="hidden" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDID" runat="server" Text='<%# Eval("dID")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle Width="150px"></ItemStyle>
                                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Metric Name" ItemStyle-Width="200" SortExpression="Metric Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMetricName" runat="server" Text='<%# Eval("Metric Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dept" ItemStyle-Width="50" SortExpression="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location Description" ItemStyle-Width="200" SortExpression="LocationDesc">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("LocationDesc")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="60" SortExpression="ddate">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("dDate", "{0:MMMM yy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Numerator" SortExpression="CurrentNumerator">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrentNumerator" runat="server" Visible="false" Text='<%# Eval("CurrentNumerator")%>'></asp:Label>
                                                <asp:TextBox ID="txtCurrNum" runat="server" onkeyup="filter()" Text='<%# Eval("CurrentNumerator")%>' Visible="true" ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Denominator" SortExpression="Current Denominator">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDept" runat="server" BorderStyle="Solid" Visible="false" Text='<%# Eval("Current Denominator")%>'></asp:Label>
                                                <asp:TextBox Enabled='<%# Eval("EnableDenominator")%>' ID="txtCurrDenom" runat="server" onkeyup="filter()" Text='<%# Eval("Current Denominator")%>' Visible="true" ReadOnly="false"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Current Value" HeaderText="Value"
                                            SortExpression="Current Value"></asp:BoundField>
                                        <%--                              <asp:TemplateField HeaderText="% Value" SortExpression="Current Value">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrVal" runat="server" Text='<%# Eval("Current Value")%>'></asp:Label>
                                                 
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                        <asp:BoundField DataField="AllowUpdate" HeaderText="AllowUpdate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="AllowUpdate"></asp:BoundField>
                                        <asp:BoundField DataField="color" HeaderText="color" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="color"></asp:BoundField>
                                        <asp:BoundField DataField="RedMax" HeaderText="RedMax" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="RedMax"></asp:BoundField>
                                        <asp:BoundField DataField="RedMin" HeaderText="RedMin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="RedMin"></asp:BoundField>
                                        <asp:BoundField DataField="wMax" HeaderText="wMax" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="wMax"></asp:BoundField>
                                        <asp:BoundField DataField="wMin" HeaderText="wMin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="wMin"></asp:BoundField>
                                        <asp:BoundField DataField="DataType" HeaderText="DataType" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
                                            SortExpression="DataType"></asp:BoundField>

                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button runat="server" Font-Size="Medium" ID="btnMonthlyDataUpdateRows" Text="Update Rows" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>




                        </asp:Panel>


                    </ContentTemplate>




                </cc1:TabPanel>

            </cc1:TabContainer>

            <cc1:ModalPopupExtender ID="mpeStandard" runat="server" PopupControlID="pnlModalPopupStandard" TargetControlID="fakeButtonStandard"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Label runat="server" ID="fakeButtonStandard"></asp:Label>
            <asp:Panel runat="server" ID="pnlModalPopupStandard" HorizontalAlign="center" BorderColor="#003060" BackColor="White" BorderWidth="3px" BorderStyle="Double">
                <asp:Table CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" runat="server" HorizontalAlign="Center" ForeColor="#003060" Font-Bold="true">
                    <asp:TableRow>
                        <asp:TableCell Width="20px"></asp:TableCell>
                        <asp:TableCell ColumnSpan="2" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="StandardWidthTable">
                            <asp:Label Font-Bold="false" runat="server" ID="lblExplanationLabel"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right">
                            <asp:Button ID="btnClosePopup" CssClass="CloseButton" runat="server" Text="X" BackColor="White" BorderStyle="None" />
                        </asp:TableCell>
                    </asp:TableRow>


                </asp:Table>
            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
