<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Surgery_TOT_DataEntry.aspx.vb" Inherits="FinanceWeb.Surgery_TOT_DataEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">

.modalBackground2 
{
    background-color: #eee4ce !important;
    background-image: none !important;
    border: 1px solid #000000;
    font-size: medium;
    color: #003060;
    width: 300px;
    padding:5px;
    vertical-align:middle;
    text-align:center;
    
}
</style>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


    <cc1:TabContainer ID="SurgeryTOTabs" runat="server"
        ActiveTabIndex="0" UseVerticalStripPlacement="false">
        <cc1:TabPanel runat="server" HeaderText="SurgeryTO Data Entry" ID="tpXRData">
            <ContentTemplate>





                <asp:UpdatePanel runat="server" ID="updMain">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:Table runat="server" BackColor="#CBE3FB" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow ID="trDateRanges" Visible="true">
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell ForeColor="Black">
                         Select Date Range:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox Width="100px" runat="server" ID="txtSurgeryTO_StartDate" Visible="True" AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server" TargetControlID="txtSurgeryTO_StartDate" Format="yyyy-MM-dd" TodaysDateFormat="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </asp:TableCell><asp:TableCell Width="100px">through</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox Width="100px" runat="server" ID="txtSurgeryTO_EndDate" Visible="True" AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2"
                                            runat="server" TargetControlID="txtSurgeryTO_EndDate" Format="yyyy-MM-dd" TodaysDateFormat="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </asp:TableCell>
                                    <asp:TableCell Width="50%"></asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow>
                                    <asp:TableCell></asp:TableCell><asp:TableHeaderCell ForeColor="Black">
                         Select Location:
                                    </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="ddlSurgeryTOLocation" AutoPostBack="true"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <asp:Panel runat="server" Width="100%" HorizontalAlign="Left" ScrollBars="Auto">
                                <asp:GridView ID="gvSurgeryTO_Data" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="SurgeryTO_ID"
                                    CellPadding="4" BorderColor="Black" BackColor="#CBE3FB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Wrap="true" ForeColor="Black"
                                    BorderWidth="1px" AllowSorting="True" AllowPaging="true" PageSize="15"
                                    BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                    Font-Size="X-Small">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Location" ItemStyle-Width="100" SortExpression="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_Location" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-Width="25" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_ID" runat="server" Text='<%# Eval("SurgeryTO_ID")%>'></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Procedure Date" ItemStyle-Width="100" SortExpression="ProcedureDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_ProcDate" runat="server" Text='<%# Eval("ProcDate_Display")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Act OR In" ItemStyle-Width="100" SortExpression="ActORIn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_ActORIn" runat="server" Text='<%# Eval("ActORIn")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surgeon" ItemStyle-Width="50" SortExpression="Surgeon">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_Surgeon" runat="server" Text='<%# Eval("Surgeon")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Turn Over Time" ItemStyle-Width="75" SortExpression="TurnOverTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_TurnOverTime" runat="server" Text='<%# Eval("TurnOverTime")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clinical Supervisor" ItemStyle-Width="75" SortExpression="ClinicalSupervisor_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_Clinic_Sup" runat="server" Text='<%# Eval("ClinicalSuperVisor_ID")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSurgeryTO_Clinic_Sup" AutoPostBack="true" OnSelectedIndexChanged="ddlClinicalSup_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clinical Supervisor Present" ItemStyle-Width="75" SortExpression="Clinical_Supervisor_Present">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_Clinic_Sup_Pres" runat="server" Text='<%# Eval("Clinical_Supervisor_Present")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSurgeryTO_Clinic_Sup_Pres" SelectedValue='<%# Eval("Clinical_Supervisor_Present")%>'>
                                                    <asp:ListItem Text="(Select Value)" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delay Code" ItemStyle-Width="75" SortExpression="Delay_Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_DelayCode" runat="server" Text='<%# Eval("Delay_Code")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblHiddenDelayCode" runat="server" Text='<%# Eval("Delay_Code")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSurgeryTO_DelayCode" AutoPostBack="true" OnSelectedIndexChanged="ddlDelayCode_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Delay Code" ItemStyle-Width="75" SortExpression="Sub_Delay_Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_SubDelayCode" runat="server" Text='<%# Eval("Sub_Delay_Code")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSurgeryTO_SubDelayCode">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--         <asp:TemplateField HeaderText="Reason for CS Absence" ItemStyle-Width = "75" SortExpression="Reason_for_CS_Absence">
                <ItemTemplate>
                    <asp:Label ID="lblSurgeryTO_CS_Absence" runat="server" Text='<%# Eval("Reason_for_CS_Absence")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtSurgeryTO_CS_Absence" runat="server" Text='<%# Eval("Reason_for_CS_Absence")%>'></asp:TextBox>
                </ItemTemplate>

              </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Circulator" ItemStyle-Width="150" SortExpression="Circulator1_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_Circ1" runat="server" Text='<%# Eval("Circulator1_ID")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList Width="200px" runat="server" ID="ddlSurgeryTO_Circ1">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OR Calling" ItemStyle-Width="75" SortExpression="ORCalling">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_ORCalling" runat="server" Text='<%# Eval("ORCalling")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSurgeryTO_ORCalling" SelectedValue='<%# Eval("ORCalling")%>'>
                                                    <asp:ListItem Text="(Select Value)" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Surgical Tech" ItemStyle-Width="75" SortExpression="SurgicalTech_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_SurgTech" runat="server" Text='<%# Eval("SurgicalTech_ID")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList Width="200px" runat="server" ID="ddlSurgeryTO_SurgTech">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                         <asp:TemplateField HeaderText="Expeditor" ItemStyle-Width = "75" SortExpression="Expeditor_Name">
                <ItemTemplate>
                    <asp:Label ID="lblSurgeryTO_Expeditor" runat="server" Text='<%# Eval("Expeditor_ID")%>' Visible="false" ></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlSurgeryTO_Expeditor" >
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>--%>
                                        <%--  <asp:TemplateField HeaderText="Pick Ticket Posted" ItemStyle-Width = "75" SortExpression="Pick_Ticket_Posted">
                <ItemTemplate>
                    <asp:Label ID="lblSurgeryTO_PickTick" runat="server" Text='<%# Eval("Pick_Ticket_Posted")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlSurgeryTO_PickTick" SelectedValue ='<%# Eval("Pick_Ticket_Posted")%>'>
                              <asp:ListItem Text="(Select Value)" Value="" ></asp:ListItem>
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Circ Directly to Preop" ItemStyle-Width = "75" SortExpression="Circ_Directly_to_Preop">
                <ItemTemplate>
                    <asp:Label ID="lblSurgeryTO_CircDirect" runat="server" Text='<%# Eval("Circ_Directly_to_Preop")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlSurgeryTO_CircDirect" SelectedValue ='<%# Eval("Circ_Directly_to_Preop")%>'>
                              <asp:ListItem Text="(Select Value)" Value="" ></asp:ListItem>
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Surg Tech in OR" ItemStyle-Width = "75" SortExpression="Surg_Tech_in_OR">
                <ItemTemplate>
                    <asp:Label ID="lblSurgeryTO_SurgTechOR" runat="server" Text='<%# Eval("Surg_Tech_in_OR")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddl_SurgeryTO_SurgTechOR" SelectedValue ='<%# Eval("Surg_Tech_in_OR")%>'>
                              <asp:ListItem Text="(Select Value)" Value="" ></asp:ListItem>
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                      <asp:TemplateField HeaderText="Exp Case Cart Checked within 45 mins" ItemStyle-Width = "75" SortExpression="Exp_Case_Cart_Checked_within_45_mins">
                <ItemTemplate>
                    <asp:Label ID="lblSurgeryTO_CaseCart" runat="server" Text='<%# Eval("Exp_Case_Cart_Checked_within_45_mins")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlSurgeryTO_CaseCart" SelectedValue ='<%# Eval("Exp_Case_Cart_Checked_within_45_mins")%>'>
                              <asp:ListItem Text="(Select Value)" Value="" ></asp:ListItem>
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Turnover Delay Notes" ItemStyle-Width="75" SortExpression="Turnover_Delay_Notes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSurgeryTO_TurnDelayNotes" runat="server" Text='<%# Eval("Turnover_Delay_Notes")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtSurgeryTO_TurnDelayNotes" runat="server" Text='<%# Eval("Turnover_Delay_Notes")%>'></asp:TextBox>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                </asp:GridView>

                                <%--             <asp:Table runat="server" Font-Size="Smaller">
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Exam
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>
                         Check In Number
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>
                         Technique
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>
                         Positioning
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>Markers</asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>Observation Shielded</asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>Coned</asp:TableHeaderCell>
                 </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlExamType" >
                             <asp:ListItem Text="Select Exam" Value="Select Exam" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Chest" Value="Chest"></asp:ListItem>
                             <asp:ListItem Text="Abdomen" Value="Abdomen"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtCheckInNumber"></asp:TextBox>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlTechnique">
                             <asp:ListItem Text="Good Detail" Value="Good Detail" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Underpenetrated" Value="Underpenetrated"></asp:ListItem>
                             <asp:ListItem Text="Over penetrated" Value="Over penetrated"></asp:ListItem>
                             <asp:ListItem Text="Artifacts" Value="Good Detail"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlPositioning">
                             <asp:ListItem Text="Good" Value="Good" Selected="True"></asp:ListItem>
                              <asp:ListItem Text="Rotated" Value="Rotated"></asp:ListItem>
                              <asp:ListItem Text="Motion" Value="Motion"></asp:ListItem>
                              <asp:ListItem Text="Clipped Anatomy" Value="Clipped Anatomy"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlMarkers">
                             <asp:ListItem Text="Lead Markers" Value="Lead Markers" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="CR Markers" Value="CR Markers"></asp:ListItem>
                             <asp:ListItem Text="No Marker" Value="No Marker"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlObservation" AppendDataBoundItems="false">
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlConed">
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                 </asp:TableRow>
             </asp:Table>

             <br />
             <br />
             Additional Comments: <asp:TextBox runat="server" Width="400px" TextMode="MultiLine" Height="50px" ID="txtObservComments"></asp:TextBox> 
             <br />--%>
                                <br />


                            </asp:Panel>
                            <br />
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnSubmitSurgeryTO" Font-Size="Large" Text="Update Data" /></asp:TableCell></asp:TableRow>
                            </asp:Table>

                        </asp:Panel>

                        <asp:Label ID="FakeButton2" runat="server" />
                        <asp:Panel ID="Panel2" runat="server" Width="300px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="explanationlabel" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="OkButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server"
                            TargetControlID="FakeButton2"
                            PopupControlID="Panel2"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>


                    </ContentTemplate>
                </asp:UpdatePanel>




            </ContentTemplate>





        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Administrative" ID="tpAdministrative" Visible="false">
            <ContentTemplate>




                <asp:UpdatePanel runat="server" ID="UpdatePanelp4">
                    <ContentTemplate>

                        <asp:Table runat="server" BackColor="#CBE3FB" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell Height="5px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableHeaderRow>
                                <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell ForeColor="Black" Width="100px">
                         Select Location:
                                </asp:TableHeaderCell><asp:TableCell Width="10px"></asp:TableCell>
                                <asp:TableCell Width="400px">
                                    <asp:DropDownList runat="server" ID="ddlAdminLocation" AutoPostBack="true"></asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell Height="10px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableHeaderRow>
                                <asp:TableCell>
                    
                                </asp:TableCell>
                                <asp:TableHeaderCell ForeColor="Black"> Manage: </asp:TableHeaderCell>
                                <asp:TableCell></asp:TableCell><asp:TableCell>
                                    <asp:DropDownList runat="server" ID="ddlManageWhat" AutoPostBack="true">
                                        <asp:ListItem Text="Clinical Supervisors" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Circulators" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Surgical Techs" Value="3"></asp:ListItem>
                                        <%--<asp:ListItem Text="Manage Expeditors" Value="4"></asp:ListItem>--%>
                                        <asp:ListItem Text="Delay Codes" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Sub Delay Codes" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell Height="10px"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                        <br />


                        <asp:Panel ID="pnlClinSups" runat="server" Visible="true">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Staff Name:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtStaffName_SearchSubmit" AutoPostBack="true"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnNewStaff" Text="Add New Clinical Supervisor" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>



                            <asp:Panel runat="server" ID="ScrollPanelDeps" ScrollBars="Auto" CssClass="AdminMaxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvSurgeryTOStaff" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="SurgeryTO_Staff_ID"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>

                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>

                                                    <asp:BoundField DataField="SurgeryTO_Staff_ID" HeaderStyle-HorizontalAlign="Left"
                                                        ControlStyle-Width="80px" HeaderStyle-Wrap="true"
                                                        HeaderText="SurgeryTO_Staff_ID" ReadOnly="True" SortExpression="SurgeryTO_Staff_ID"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle Width="80px" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Staff Name" SortExpression="StaffName" AccessibleHeaderText="Staff Name">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblStaffName" runat="server" Text='<%# Bind("StaffName")%>'></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtStaffName" Width="95%" runat="server" Text='<%# Bind("StaffName")%>' Visible="false"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Clinical Supervisor">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnActivateClinSup" runat="server" Text='<%# Bind("ClinSupStatus")%>' CommandName="ClinicalSupervisor" CommandArgument='<%# Bind("SurgeryTO_Staff_ID")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Circulator">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnActivateCirculator" runat="server" Text='<%# Bind("CirculatorStatus")%>' CommandName="Circulator" CommandArgument='<%# Bind("SurgeryTO_Staff_ID")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Surgical Tech">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnActivateSurgTech" runat="server" Text='<%# Bind("SurgTechStatus")%>' CommandName="Surgical_Tech" CommandArgument='<%# Bind("SurgeryTO_Staff_ID")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText = "Expeditor" >
                <ItemTemplate>              
                <asp:Panel runat = "server">
                    <asp:LinkButton ID="btnActivateExpeditor" runat = "server" Text='<%# Bind("ExpeditorStatus")%>' CommandName="Expeditor" CommandArgument='<%# Bind("SurgeryTO_Staff_ID")%>'></asp:LinkButton>
                </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Top">

                                            <asp:CheckBox runat="server" ID="chkInactiveStaff" AutoPostBack="true" Text="Show Other Staff Members" Checked="false" />
                                        </asp:TableCell>
                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Panel ID="pnlCodes" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Code Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtCodeDesc_SrchSubmit" AutoPostBack="true"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnNewCode" Text="Add New Delay Code" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>



                            <asp:Panel runat="server" ID="Panel4" ScrollBars="Auto" CssClass="AdminMaxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvGFSOT_Codes" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="SurgeryTO_Code_ID"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>

                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>

                                                    <asp:BoundField DataField="SurgeryTO_Code_ID" HeaderStyle-HorizontalAlign="Left"
                                                        ControlStyle-Width="80px" HeaderStyle-Wrap="true"
                                                        HeaderText="SurgeryTO_Code_ID" ReadOnly="True" SortExpression="SurgeryTO_Code_ID"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle Width="80px" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnInActivateCode" runat="server" Text='<%# Bind("ActiveStatus")%>' CommandName='<%# Bind("Code_Classification")%>' CommandArgument='<%# Bind("SurgeryTO_Code_ID")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Code Description" SortExpression="Code_Name" AccessibleHeaderText="Code Description">
                                                        <ItemTemplate>
                                                            <asp:Panel Width="95%" runat="server">
                                                                <asp:Label ID="lblStaffName" runat="server" Text='<%# Bind("Code_Name")%>'></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtAnythingElse" Width="95%" runat="server" Text='<%# Bind("Code_Name")%>' Visible="false"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Parent Code" SortExpression="Parent_Delay_Code">
                                                        <ItemTemplate>
                                                            <asp:Panel Width="95%" runat="server">
                                                                <asp:Label ID="lblParent_Delay_Code" runat="server" Text='<%# Bind("Parent_Delay_Code")%>'></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtParent_Delay_Code" Width="95%" runat="server" Text='<%# Bind("Parent_Delay_Code")%>' Visible="false"></asp:TextBox>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Top">

                                        </asp:TableCell>
                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Label ID="FakeButtonp4" runat="server" />
                        <asp:Panel ID="Panelp4" runat="server" Width="233px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="explanationlabelp4" runat="server"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="OkButtonp4" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtenderp4" runat="server"
                            TargetControlID="FakeButtonp4"
                            PopupControlID="Panelp4"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                        <asp:Table runat="server" CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Right">
                           
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                    </ContentTemplate>
                </asp:UpdatePanel>


            </ContentTemplate>


        </cc1:TabPanel>
    </cc1:TabContainer>


</asp:Content>
