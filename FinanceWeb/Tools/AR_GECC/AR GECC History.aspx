<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AR GECC History.aspx.vb" Inherits="FinanceWeb.AR_GECC_History" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Finance Help Desk -- Case Attachments</title>
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

        <asp:Panel runat="server" ID="hiddenThings" Visible="false">

            <asp:Label runat="server" ID="SelectedDetailID"></asp:Label>

        </asp:Panel>


        <cc1:TabContainer ID="MDPOSMappingTabs" runat="server"
            ActiveTabIndex="0" UseVerticalStripPlacement="False">
            <cc1:TabPanel runat="server" HeaderText="AR Activity Report Attachments" ID="tpMDPOSUnmapped">
                <ContentTemplate>

                    <h3></h3>

                    <asp:Panel runat="server" Visible="true" ID="EmptyPanel">
                        <asp:Panel BackColor="#6da9e3" runat="server" Width="600px" Height="40" ForeColor="White">
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            No AR Activity Detail row has been selected.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel runat="server" Visible="false" ID="FullPanel">


                        <asp:Panel runat="server" Width="600px" ForeColor="White">
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px" VerticalAlign="Top">
                                        <asp:GridView Width="600px" ID="gvHeaderDisplay" runat="server" HeaderStyle-BackColor="#6da9e3" HeaderStyle-BorderColor="#003060"
                                            HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="xx-small" HeaderStyle-BorderWidth="1px"
                                            RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                                            RowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" DataKeyNames="ActivityID" BackColor="#6da9e3"
                                            HorizontalAlign="Left" Font-Size="Smaller">

                                            <Columns>
                                                <asp:BoundField DataField="LetterCodes" HeaderText="" ItemStyle-Width="10px" />
                                                <asp:BoundField DataField="RowSource" HeaderText="Source" ItemStyle-Width="10px"
                                                    SortExpression="RowSource"></asp:BoundField>
                                                <asp:BoundField DataField="ItemDescription" HeaderText="Description" ItemStyle-Width="10px"
                                                    SortExpression="ItemDescription"></asp:BoundField>
                                                <asp:BoundField DataField="NetAmount" HeaderText="Source Amount" ItemStyle-Width="10px"
                                                    SortExpression="NetAmount"></asp:BoundField>
                                                <asp:BoundField DataField="DepositDate" HeaderText="Deposit Date" ItemStyle-Width="10px"
                                                    SortExpression="DepositDate"></asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="10px"
                                                    SortExpression="Status"></asp:BoundField>
                                            </Columns>

                                        </asp:GridView>

                                    </asp:TableCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>

                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:Label runat="server" Visible="false" ID="lbldateforgreen"></asp:Label>
                                        <asp:GridView Width="396px" ID="gvInitialDisplay" runat="server" HeaderStyle-BackColor="#cbe3fb" HeaderStyle-BorderColor="#003060"
                                            HeaderStyle-ForeColor="#003060" ForeColor="#003060" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="xx-small" HeaderStyle-BorderWidth="1px"
                                            RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                                            RowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" DataKeyNames="TFID"
                                            HorizontalAlign="Left" Font-Size="Smaller">

                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ItemStyle-Width="0px" Visible="True" SelectText="" />
                                                <%--                     <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Panel runat="server">
                                            <asp:Image ID="imgDRHR" Visible="false" runat="server" ImageUrl="Images/HourGlassDarkRed.bmp" Width="11px" />
                                            <asp:Image ID="imgDOHR" Visible="false" runat="server" ImageUrl="Images/HourGlassDarkOrange.bmp" Width="11px" />
                                            <asp:Image ID="imgDYHR" Visible="false" runat="server" ImageUrl="Images/HourGlassDarkYellow.bmp" Width="11px" />
                                            <asp:Image ID="imgLRHR" Visible="false" runat="server" ImageUrl="Images/HourGlassLightRed.bmp" Width="11px" />
                                            <asp:Image ID="imgLOHR" Visible="false" runat="server" ImageUrl="Images/HourGlassLightOrange.bmp" Width="11px" />
                                            <asp:Image ID="imgLYHR" Visible="false" runat="server" ImageUrl="Images/HourGlassLightYellow.bmp" Width="11px" />
                                            <asp:Label ID="lblAsterisk" Visible="false" runat="server" Font-Bold="true" ForeColor="#003060">*</asp:Label>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="LetterCodes" HeaderText="" ItemStyle-Width="10px" />
                                                <asp:BoundField DataField="ChangeType" HeaderText="Change" ItemStyle-Width="10px"
                                                    SortExpression="ChangeType"></asp:BoundField>
                                                <asp:BoundField DataField="ActivityDate" HeaderText="Activity Date" DataFormatString="{0:d}"
                                                    SortExpression="ActivityDate"></asp:BoundField>

                                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" DataFormatString="{0:0,0.00}"></asp:BoundField>
                                                <asp:BoundField DataField="ModifyDate" HeaderText="Date Modified" DataFormatString="{0:d}"
                                                    SortExpression="ModifyDate"></asp:BoundField>
                                                <asp:BoundField DataField="UserDisplay" HeaderText="User"
                                                    SortExpression="UserDisplay"></asp:BoundField>
                                                <asp:BoundField DataField="ModifyDate" HeaderText="Date Modified"
                                                    SortExpression="ModifyDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" ControlStyle-CssClass="hidden"></asp:BoundField>
                                            </Columns>

                                        </asp:GridView>

                                    </asp:TableCell>

                                    <%--   <asp:TableCell Width="60%">

                        <asp:Table runat="server" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell Height="5px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="10px"></asp:TableCell>
                                <asp:TableCell Width="150px" HorizontalAlign="left" VerticalAlign="Top">
                            Facility:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="CaseNumber"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Width="10px"></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell HorizontalAlign="left" VerticalAlign="Top">
                            Category:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="CaseTitle"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell HorizontalAlign="left" VerticalAlign="Top">
                            Status:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="CaseUser"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell HorizontalAlign="left" VerticalAlign="Top">
                            Deposit Date
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="CaseDesc"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell HorizontalAlign="left" VerticalAlign="Top">
                            WF Cash Received
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="CaseCash"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Height="5px"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                    </asp:TableCell>--%>
                                </asp:TableRow>
                            </asp:Table>


                        </asp:Panel>

                        <br />
                        <asp:Label runat="server" Text="Test" CssClass="hidden"></asp:Label>
                        <asp:Panel runat="server" ScrollBars="Auto">
                            <asp:GridView DataKeyNames="ActivityID" runat="server" ID="gv_Selected_Grids" ShowHeader="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="ActivityID"></asp:BoundField>
                                    <asp:BoundField DataField="LetterCodes"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel runat="server" ScrollBars="Auto">
                                                <asp:GridView ID="gv_AR_MainData" runat="server"
                                                    AutoGenerateColumns="false"
                                                    BorderColor="#003060" BackColor="#CBE3FB"
                                                    HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Wrap="true" ForeColor="Black"
                                                    BorderWidth="1px"
                                                    BorderStyle="Solid" HeaderStyle-VerticalAlign="Bottom" CellPadding="2" CellSpacing="2" HeaderStyle-BackColor="#4A8fd2"
                                                    Font-Size="X-Small">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ActivityDate" HeaderText="Activity Date" DataFormatString="{0:d}"
                                                            SortExpression="ActivityDate"></asp:BoundField>

                                                        <asp:TemplateField HeaderText="Change">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("ChangeType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount Affected">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("AmountAffected", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Facility">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Facility")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("CashCategory")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("DetailStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bank Batch #">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BankBatchNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="STAR Batch #">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("STARBatchNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="# Patients">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("NoPatients")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cash Received">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Cash_Received", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AR Posted">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("AR_Posted", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Misc Posted">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Misc_Posted", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Interest">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Interest", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Transfers">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Transfers", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Unresolved">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Unresolved", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Carry Forward">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Carry_Forward", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="STAR Variance">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("STARVariance", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Bank Variance">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BankVariance", "{0:0,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Modified">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("ModifyDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Modified By">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("UserDisplay")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>


                        </asp:Panel>

                    </asp:Panel>

                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

    </form>
</body>
</html>

