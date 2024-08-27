<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Armored Car Tracking.aspx.vb" Inherits="FinanceWeb.Armored_Car_Tracking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .MaxgvLocationHeight {
    max-height:400px;
}

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

</style>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <script type="text/javascript" charset="utf-8">



        function filter() {
            var maybeObject = $get('<%=txtFilter.ClientID%>');
            if (maybeObject != null) {
                var suche = maybeObject.value.toLowerCase();
                var table = $get('<%=gvLocationOptions.ClientID%>');
                var ele;
                var ele2;
                var ele3;
                var ele4;
                var ele5;
                var ele6;
                var ele7;
                var yn;
                for (var r = 1; r < table.rows.length; r++) {
                    ele = table.rows[r].cells[1].innerHTML.replace(/<[^>]+>/g, "");
                    ele2 = table.rows[r].cells[2].innerHTML.replace(/<[^>]+>/g, "");
                    ele3 = table.rows[r].cells[3].innerHTML.replace(/<[^>]+>/g, "");
                    ele4 = table.rows[r].cells[4].innerHTML.replace(/<[^>]+>/g, "");
                    ele5 = table.rows[r].cells[5].innerHTML.replace(/<[^>]+>/g, "");
                    ele6 = table.rows[r].cells[6].innerHTML.replace(/<[^>]+>/g, "");
                    ele7 = table.rows[r].cells[7].innerHTML.replace(/<[^>]+>/g, "");
                    ele8 = table.rows[r].cells[8].innerHTML.replace(/<[^>]+>/g, "");
                    yn = 0;
                    if (ele.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele2.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele3.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele4.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele5.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele6.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele7.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    else if (ele8.toLowerCase().indexOf(suche) >= 0)
                        yn = 1;
                    if (yn > 0)
                        table.rows[r].style.display = '';
                    else table.rows[r].style.display = 'none';
                }
            }
            ;
        }
  
        filter();

        var xPos2, yPos2;
        var prm2 = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler2(sender, args) {
            if ($get('<%=ScrollPanelgvLocationOptions.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos2 = $get('<%=ScrollPanelgvLocationOptions.ClientID%>').scrollLeft;
                yPos2 = $get('<%=ScrollPanelgvLocationOptions.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler2(sender, args) {
            if ($get('<%=ScrollPanelgvLocationOptions.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanelgvLocationOptions.ClientID%>').scrollLeft = xPos2;
                $get('<%=ScrollPanelgvLocationOptions.ClientID%>').scrollTop = yPos2;
            }
        }

        prm2.add_beginRequest(BeginRequestHandler2);
        prm2.add_endRequest(EndRequestHandler2);

    </script>

    <asp:UpdatePanel runat="server" ID="upWholeThing">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" Visible="false">

                <asp:Label runat="server" ID="sortmap"></asp:Label>
                <asp:Label runat="server" ID="sortunmap"></asp:Label>
                <asp:Label runat="server" ID="selectedAccID"></asp:Label>

            </asp:Panel>


            <cc1:TabContainer ID="tcArmoredCarTracking" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1151px">

                <cc1:TabPanel runat="server" HeaderText="Report Missed Pickup" ID="tpMissedPickup">
                    <ContentTemplate>
                        <asp:Panel runat="server" ScrollBars="Auto" Width="1133px" HorizontalAlign="Center">
                            <b>Please select your location from the table below.</b><br />
                            If you cannot find your practice, or it is listed with the incorrect address, please contact Philip Thao (Philip.Thao@northside.com)<br />
                            <br />
                            Search Locations:
                            <asp:TextBox runat="server" Width="300px" ID="txtFilter" CssClass="Txtform-control" oninput="filter()" onkeyup="filter()"></asp:TextBox>

                            <br />
                            <asp:Panel runat="server" ID="ScrollPanelgvLocationOptions" CssClass="MaxgvLocationHeight" ScrollBars="Auto">
                                <asp:GridView runat="server" ID="gvLocationOptions" DataKeyNames="VendRefNo"
                                    AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                    ForeColor="Black" HeaderStyle-Wrap="true"
                                    BorderWidth="1px" AllowSorting="True"
                                    BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                    Font-Size="X-Small" CssClass="CursorHand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ItemStyle-Width="2px" ShowSelectButton="True" Visible="True" SelectText="" />
                                        <asp:BoundField ItemStyle-Width="50px" SortExpression="Vendor" DataField="Vendor" HeaderText="Vendor" />
                                        <asp:BoundField ItemStyle-Width="50px" SortExpression="VendorReferenceNo" DataField="VendorReferenceNo" HeaderText="Vendor Ref No" />
                                        <asp:BoundField SortExpression="Location_Name" DataField="Location_Name" HeaderText="Location Name" />
                                        <asp:BoundField SortExpression="Location_Address1" DataField="Location_Address1" HeaderText="Address 1" />
                                        <asp:BoundField SortExpression="Location_Address2" DataField="Location_Address2" HeaderText="Address 2" />
                                        <asp:BoundField SortExpression="Location_City" DataField="Location_City" HeaderText="City" />
                                        <asp:BoundField SortExpression="Location_State" DataField="Location_State" HeaderText="State" />
                                        <asp:BoundField SortExpression="Location_Zip" DataField="Location_Zip" HeaderText="ZIP" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                        <br />
                        <asp:Panel runat="server" Width="100%" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" BackColor="#CBE3FB" Visible="false" ID="pnlSubmittal" HorizontalAlign="Center">
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
  <asp:Table runat="server" >
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">Location Name:</asp:TableCell><asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="2" Width="300px" Font-Bold="true">
                                        <asp:Label runat="server" ID="lblLocationName"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">Address:</asp:TableCell><asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="2" Font-Bold="true">
                                        <asp:Label runat="server" ID="lblAddress"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblAddress2"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">Contact Info:</asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell><asp:TableCell>Name:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtContactName" CssClass="Txtform-control"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="20px"></asp:TableCell><asp:TableCell>Email:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtContactEmail" CssClass="Txtform-control"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell><asp:TableCell>Phone Number:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtContactPhone" CssClass="Txtform-control"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">Date Missed:</asp:TableCell><asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:TextBox runat="server" ID="txtMissedDate" CssClass="Txtform-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" TargetControlID="txtMissedDate" ID="CalEx" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                          
                   
                            <br />
                            <asp:Button runat="server" ID="btnSubmitMissedDate" Width="200px" Text="Submit" CssClass="loginform-control" />
                            <br />
                            <br />
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


