<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PP Activity Report Attachments.aspx.vb" Inherits="FinanceWeb.PP_Activity_Report_Attachments" %>
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


                        <asp:Panel BackColor="#6da9e3" runat="server" Width="600px" ForeColor="White">
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="150px" HorizontalAlign="left" VerticalAlign="Top">
                            Practice:
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
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:FileUpload ID="fileInput" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnUpload" Text="Upload" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </asp:Panel>

                        <br />
                        <asp:Label runat="server" Text="Test" CssClass="hidden"></asp:Label>

                        <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="0"
                            HeaderStyle-Font-Size="Medium" AllowSorting="false" Font-Size="Small" Width="600px"
                            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3">
                            <AlternatingRowStyle BackColor="white" />
                            <Columns>

                                <asp:BoundField HeaderText="File Id" DataField="AttachID" HeaderStyle-CssClass="hidden" />

                                <asp:BoundField HeaderText="ContentType" DataField="ContentType"
                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                <asp:BoundField HeaderText="File Name" DataField="FileName" ItemStyle-Width="300px" />

                                <asp:TemplateField HeaderText="Preview">

                                    <ItemTemplate>

                                        <asp:Image ID="Image1" runat="server" Height="80" Width="80" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                            CommandArgument='<%# Eval("AttachID")%>'></asp:LinkButton>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Submitted By" DataField="UserID" ItemStyle-Width="300px" />

                                <asp:BoundField HeaderText="Date Submitted" DataField="DateAdded" ItemStyle-Width="300px" />


                            </Columns>

                        </asp:GridView>


                    </asp:Panel>

                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

    </form>
</body>
</html>

