<%@ Page Title="Census Data Entry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CensusDataEntry.aspx.vb" Inherits="FinanceWeb.CensusDataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" Visible="false">


    <style>
        .FacilityBox {
            text-align: left;
            padding: 5px;
        }

        .DataEntryTable {
            width: 99%;
            padding:2px;
            margin:2px;
            
        }

        .Submit {
            margin:15px 0px 0px 65px
        }

        .nopadding {
            padding: 0px;
            margin: 0px;
        }

        .headers {
            text-align: left;
            padding-left: 5px;
        }

        .rows {
            text-align: left;
        }

        .Calendar {
            margin: 15px;
            width: 30%;
            display: inline-block;
        }

        .IncompleteRows {
            display: inline-block;
            margin: 15px;
            float: right;
        }

        .Input_txt {
            width: 50px;
        }

        .locationcss { text-align:center; padding:0px 10px 0px 10px}
    </style>

    <div runat="server" id="Messages_Div"></div>

    <table class="DataEntryTable">
        <tr>
            <th class="headers"></th>
            <th class="headers">Facility&nbsp;&nbsp;</th>
            <th class="headers">Total ADC</th>
            <th class="headers">Census</th>
            <th class="headers">Labor & Delivery</th>
            <th class="headers">IP ADC</th>
            <th class="headers">OBV</th>
            <th class="headers">OB</th>
            <th class="headers">DELS</th>
            <th class="headers">NB</th>
            <th class="headers">Calendar Date</th>
        </tr>
        <tr>
            <th class="rows">
                <asp:CheckBox runat="server" ID="A_Checkbox" Checked="true" Text="" /></th>
            <td class="FacilityBox">A</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_Total_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;P</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_Census" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_LD" runat="server"></asp:TextBox>&nbsp;&nbsp;Q</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_IP_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;D</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_OBV" runat="server"></asp:TextBox>&nbsp;&nbsp;E</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_OB" runat="server"></asp:TextBox>&nbsp;&nbsp;L</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_DELS" runat="server"></asp:TextBox>&nbsp;&nbsp;H</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_NB" runat="server"></asp:TextBox>&nbsp;&nbsp;S + T</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="A_CalendarDate" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th class="rows">
                <asp:CheckBox runat="server" ID="C_Checkbox" Checked="true" Text="" /></th>
            <td class="FacilityBox">C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_Total_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_Census" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_LD" runat="server"></asp:TextBox>&nbsp;&nbsp;F</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_IP_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;D</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_OBV" runat="server"></asp:TextBox>&nbsp;&nbsp;E</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_OB" runat="server"></asp:TextBox>&nbsp;&nbsp;(zero)</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_DELS" runat="server"></asp:TextBox>&nbsp;&nbsp;J</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_NB" runat="server"></asp:TextBox>&nbsp;&nbsp;BF + BG</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="C_CalendarDate" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th class="rows">
                <asp:CheckBox runat="server" ID="F_Checkbox" Checked="true" Text="" /></th>
            <td class="FacilityBox">F</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_Total_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_Census" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_LD" runat="server"></asp:TextBox>&nbsp;&nbsp;F or Q</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_IP_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;D</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_OBV" runat="server"></asp:TextBox>&nbsp;&nbsp;E</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_OB" runat="server"></asp:TextBox>&nbsp;&nbsp;(zero)</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_DELS" runat="server"></asp:TextBox>&nbsp;&nbsp;J</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_NB" runat="server"></asp:TextBox>&nbsp;&nbsp;R + S</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="F_CalendarDate" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <th class="rows">
                <asp:CheckBox runat="server" ID="D_Checkbox" Checked="true" Text="" /></th>
            <td class="FacilityBox">D</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_Total_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;C + O</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_Census" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_LD" runat="server"></asp:TextBox>&nbsp;&nbsp;O</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_IP_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;D</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_OBV" runat="server"></asp:TextBox>&nbsp;&nbsp;E</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_OB" runat="server"></asp:TextBox>&nbsp;&nbsp;(zero)</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_DELS" runat="server"></asp:TextBox>&nbsp;&nbsp;H</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_NB" runat="server"></asp:TextBox>&nbsp;&nbsp;P + Q</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="D_CalendarDate" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
            <th class="rows">
                <asp:CheckBox runat="server" ID="L_Checkbox" Checked="true" Text="" /></th>
            <td class="FacilityBox">L</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_Total_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;C + O</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_Census" runat="server"></asp:TextBox>&nbsp;&nbsp;C</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_LD" runat="server"></asp:TextBox>&nbsp;&nbsp;O</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_IP_ADC" runat="server"></asp:TextBox>&nbsp;&nbsp;D</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_OBV" runat="server"></asp:TextBox>&nbsp;&nbsp;E</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_OB" runat="server"></asp:TextBox>&nbsp;&nbsp;(zero)</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_DELS" runat="server"></asp:TextBox>&nbsp;&nbsp;H</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_NB" runat="server"></asp:TextBox>&nbsp;&nbsp;P + Q</td>
            <td class="rows">
                <asp:TextBox CssClass="Input_txt" ID="L_CalendarDate" runat="server"></asp:TextBox></td>
        </tr>
    </table>

    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                <%--<div class="Calendar">--%>
                <h4 class="nopadding">Choose Calendar Date:</h4>
                <br />
                <asp:Calendar CssClass="nopadding" ID="CalendarDate_Selection" runat="server" OnSelectionChanged="Calendar_SelectionChange" Visible="True"></asp:Calendar>
                <br />
                <asp:Button CssClass="Submit" runat="server" ID="CensusData_Submit" Text="Submit Census Data" />
                <%--  </div>--%>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top"><br />
    <b>Contacts:</b><br /><br />
    Atlanta &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -- House.Coordinator@Northside.com <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Laurie Welter, Cathy Sumrell, Kelly Inoue)<br />
    Cherokee &nbsp; -- Donna Whitehead &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;770-224-1999<br />
    Forsyth &nbsp;&nbsp;&nbsp;&nbsp; -- Carolyn Booker &nbsp;&nbsp;W 770-844-3218   <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;C 678-429-0470<br />
                <br /><br />
    Note: If Census volumes are not available, the SSRS subscription will automatically reflect this as N/A (no need to enter values).

            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">
                <div class="IncompleteRows">
                    <h4 class="nopadding">Missing Data Report (last 7 days): </h4>
                    <asp:GridView Style="margin: 0px;" runat="server" ID="MissingDataReport" BorderColor="Black"
                        BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Wrap="true" PageSize="25"
                        ForeColor="Black" BackColor="White" BorderWidth="1px"
                        Width="100%" ShowHeaderWhenEmpty="True" Visible="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" textmode="Multiline" AllowSorting="True">
                        <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#214B9A" ForeColor="#FFCBA5"></HeaderStyle>

                        <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
                        <AlternatingRowStyle BackColor="#84b1ff" BorderColor="Red" />

                        <Columns>
                            <asp:BoundField DataField="Calendar_Date"
                                HeaderStyle-Wrap="true"
                                HeaderText="Calendar Date" ReadOnly="True"
                                Visible="True">
                                <ItemStyle CssClass="nopadding"></ItemStyle>
                                <HeaderStyle CssClass="nopadding" Width="40%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Atlanta"
                                HeaderStyle-Wrap="true"
                                HeaderText="A" ReadOnly="True"
                                Visible="True">
                                <ItemStyle CssClass="locationcss nopadding"></ItemStyle>
                                <HeaderStyle CssClass="locationcss" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Cherokee"
                                HeaderStyle-Wrap="true"
                                HeaderText="C" ReadOnly="True"
                                Visible="True">
                                <ItemStyle CssClass="locationcss"></ItemStyle>
                                <HeaderStyle CssClass="locationcss nopadding" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Forsyth"
                                HeaderStyle-Wrap="true"
                                HeaderText="F" ReadOnly="True"
                                Visible="True">
                                <ItemStyle CssClass="locationcss nopadding"></ItemStyle>
                                <HeaderStyle CssClass="locationcss" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Duluth"
                                HeaderStyle-Wrap="true"
                                HeaderText="D" ReadOnly="True"
                                Visible="True">
                                <ItemStyle CssClass="locationcss nopadding"></ItemStyle>
                                <HeaderStyle CssClass="locationcss" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Lawrenceville"
                                HeaderStyle-Wrap="true"
                                HeaderText="L" ReadOnly="True"
                                Visible="True">
                                <ItemStyle CssClass="locationcss nopadding"></ItemStyle>
                                <HeaderStyle CssClass="locationcss" />
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>

                </div>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>




</asp:Content>
