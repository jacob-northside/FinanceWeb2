<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ENTDataEntry.aspx.vb" Inherits="FinanceWeb.ENTDataEntry" MaintainScrollPositionOnPostback="true" Title="" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">





    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <style>
        /*Adding this section for pretty pagination on gridview*/
        .pagination-sk {
    /*display: inline-block;*/
    padding-left: 0;
    margin: 20px 0;
    border-radius: 4px;
}
 
.pagination-sk table > tbody > tr > td {
    display: inline;
}
 
.pagination-sk table > tbody > tr > td > a,
.pagination-sk table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;
    color: #dd4814;
    background-color: #ffffff;
    border: 1px solid #dddddd;
    margin-left: -1px;
}
 
.pagination-sk table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;    
    margin-left: -1px;
    z-index: 2;
    color: #aea79f;
    background-color: #f5f5f5;
    border-color: #dddddd;
    cursor: default;
}
 
.pagination-sk table > tbody > tr > td:first-child > a,
.pagination-sk table > tbody > tr > td:first-child > span {
    margin-left: 0;
    border-bottom-left-radius: 4px;
    border-top-left-radius: 4px;
}
 
.pagination-sk table > tbody > tr > td:last-child > a,
.pagination-sk table > tbody > tr > td:last-child > span {
    border-bottom-right-radius: 4px;
    border-top-right-radius: 4px;
}
 
.pagination-sk table > tbody > tr > td > a:hover,
.pagination-sk table > tbody > tr > td > span:hover,
.pagination-sk table > tbody > tr > td > a:focus,
.pagination-sk table > tbody > tr > td > span:focus {
    color: #97310e;
    background-color: #eeeeee;
    border-color: #dddddd;
}
    /*Wanted to change the default hover color*/
   /*.table-hover tbody tr:hover td{
  background-color: #8ec8ff;
}*/


    </style>


    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>

            <cc1:TabContainer ID="DWH_UD_ENT_CPTCODES_RATES_TabCC" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1150px">
                <cc1:TabPanel runat="server" HeaderText="DWH.UD.ENT_CPTCODES_RATES" ID="tpPFO_Group">
                    <ContentTemplate>

                        <div class="PageSummary">
                            This page is for the updating/inserting/deleting of CPT rate records in the table DWH.UD.ENT_CPTCODES_RATES in the Netezza database. Changes inserted into 
               this webpage/tool will not be reflected in any Aginity/Netezza queries until the SQL Server and Netezza databases have had a chance to sync at 4:30 AM.

        </br></br>
                <ul>
                    <li>If you would like to bill two locations at the same rate you will need to enter both locations separated by a bar (|). For example, to bill a CPT code at the normal rate 
                in locations 11 and 24 you would insert "11|24" (without quotes) in the POS field for that CPT code.
                    </li>
                    <li>The GROUP and CPT fields cannot be edited since they provide the references needed to update a row. If you need to change those you'll need to delete the row and insert a new one 
                with the updates.
                    </li>
                </ul>

                            <a href="#" onclick="$('#Insert_CPTRATES').toggle(100)">Click here to insert a new row</a><asp:Label runat="server" ID="ResponseMsg_CPT" Style="padding-left: 10px; font: bold"></asp:Label>
                        </div>

                        <style>
            .insertrows {
                padding: 2px 2px 2px 2px;
                box-sizing:border-box;
            }
        </style>

                        <div style="width: 99%; display: none;" id="Insert_CPTRATES">
                            <asp:Table ID="Table1" Width="100%" Visible="true" BackColor="#5bc0de" CellSpacing="2" runat="server" BorderWidth="1px" BorderColor="#003060" ForeColor="Black" Height="50px">
                                <asp:TableRow CssClass="insertrows">
                                    <asp:TableCell HorizontalAlign="Left" Width="5%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="Group_Insert" AutoPostBack="False" placeholder="Group" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="GroupI_Regex"
                                            ControlToValidate="Group_Insert"
                                            ValidationExpression="[A-Z]{1,}(_[A-Z]{1,})?"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Letters only"
                                            ForeColor="Red"
                                            runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="4%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="POD_Insert" AutoPostBack="False" placeholder="POD" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="PODI_Regex"
                                            ControlToValidate="POD_Insert"
                                            ValidationExpression="\d+"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="4%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="CPT_Insert" AutoPostBack="False" placeholder="CPT" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="CPT_Insert_Regex"
                                            ControlToValidate="CPT_Insert"
                                            ValidationExpression="\d+"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="4%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="POS_Insert" AutoPostBack="False" placeholder="POS" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="POS_Insert_Regex"
                                            ControlToValidate="POS_Insert"
                                            ValidationExpression="\d+"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />


                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="20%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="Category_Insert" AutoPostBack="False" placeholder="Category" Width="95%"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="20%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="AltCat_Insert" AutoPostBack="False" placeholder="Alt Category" Width="95%"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="7%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="Rate_Insert" AutoPostBack="False" placeholder="Rate" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Rate_Insert_Regex"
                                            ControlToValidate="Rate_Insert"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="7%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="AltRate_Insert" AutoPostBack="False" placeholder="Alt Rate" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="AltRate_Insert_Regex"
                                            ControlToValidate="AltRate_Insert"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />


                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="12%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="EffFrom_Insert" AutoPostBack="False" placeholder="Effective From" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EffFrom_Insert_regex"
                                            ControlToValidate="EffFrom_Insert"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="12%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="EffTo_Insert" AutoPostBack="False" placeholder="Effective To" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EffTo_Insert_Regex"
                                            ControlToValidate="EffTo_Insert"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="10">
                                        <asp:Button runat="server" Text="Insert new row" ID="CPTCODESRATES_Insert" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </div>

                        <asp:Table ID="Search_tbls" Width="99%" Visible="true" BackColor="#2b74bb" CellSpacing="2" runat="server" BorderWidth="1px" BorderColor="#003060" ForeColor="White" Height="50px">
                            <asp:TableRow>

                                <asp:TableCell HorizontalAlign="Center">Search:</asp:TableCell>
                                <asp:TableCell HorizontalAlign="Left">
                                    <asp:TextBox runat="server" ID="tbl_SearchCPT" Width="95%" AutoPostBack="True" placeholder="Search CPT Codes" CssClass="darkfont"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Left">
                                    <asp:TextBox runat="server" ID="tbl_SearchCategories" Width="95%" AutoPostBack="True" placeholder="Search Categories" CssClass="darkfont"></asp:TextBox>
                                </asp:TableCell>

                            </asp:TableRow>
                        </asp:Table>



                        <asp:GridView ID="Update_CPTCODESRATES" runat="server" PageSize="25"
                            Width="99%" ShowHeaderWhenEmpty="True" Visible="true" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" textmode="Multiline" AllowSorting="True" CssClass="table table-striped"
                            HeaderStyle-Font-Size="X-Small" Font-Size="Small">
                            <RowStyle CssClass="cursor-pointer" HorizontalAlign="Left" VerticalAlign="Top" />
                            <EditRowStyle CssClass="GridViewEditRow" />
                            <Columns>
                                <asp:CommandField HeaderStyle-Width="3%" ShowDeleteButton="True"
                                    ShowEditButton="true">
                                    <HeaderStyle Width="3%" BackColor="#2b74bb" />
                                </asp:CommandField>

                                <%--                        <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Left"  
                HeaderStyle-Width="3%" HeaderStyle-Wrap="true" 
                HeaderText="ID" 
                Visible="False" ReadOnly="true">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" BackColor="#2b74bb"/>
            </asp:BoundField>    --%>

                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:BoundField DataField="GROUPID" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="3%" HeaderStyle-Wrap="true"
                                    HeaderText="Group"
                                    Visible="True" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                </asp:BoundField>


                                <asp:TemplateField HeaderText="POD" HeaderStyle-Width="5%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("POD_ID")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox runat="server" ID="POD_ID_EditBox" Text='<%# Eval("POD_ID")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="POD_Regex"
                                            ControlToValidate="POD_ID_EditBox"
                                            ValidationExpression="\d+"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="CPT" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="3%" HeaderStyle-Wrap="true"
                                    HeaderText="CPT"
                                    Visible="True" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="POS" HeaderStyle-Width="3%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("POS")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="POS_EditBox" Text='<%# Eval("POS")%>' Width="100%" />
                                        <%--                                        <asp:RegularExpressionValidator id="POS_REGEX"
                   ControlToValidate="POS_EditBox"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Numbers only"
                   ForeColor="Red"
                   runat="server"/>--%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Category" HeaderStyle-Width="20%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("CATEGORY")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="Category_EditBox" Text='<%# Eval("CATEGORY")%>' Width="100%" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Alt Category" HeaderStyle-Width="20%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("ALT_CATEGORY")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="AltCategory_EditBox" Text='<%# Eval("ALT_CATEGORY")%>' Width="100%" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="7%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RATE", "{0:C}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="Rate_EditBox" Text='<%# Eval("Rate")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="Rate_Regex"
                                            ControlToValidate="Rate_EditBox"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Alt Rate" HeaderStyle-Width="7%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ALT_RATE", "{0:C}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="ALT_RATE_EditBox" Text='<%# Eval("ALT_RATE")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="ALTRate_Regex"
                                            ControlToValidate="ALT_RATE_EditBox"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Effective From" HeaderStyle-Width="11%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_FROM", "{0:MM/dd/yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="EFFECTIVE_FROM_EditBox" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_FROM", "{0:MM/dd/yyyy}")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="EFFFROM_Regex"
                                            ControlToValidate="EFFECTIVE_FROM_EditBox"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Effective To" HeaderStyle-Width="12%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_TO", "{0:MM/dd/yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="EFFECTIVE_TO_EditBox" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_TO", "{0:MM/dd/yyyy}")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="EFFTO_Regex"
                                            ControlToValidate="EFFECTIVE_TO_EditBox"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </EditItemTemplate>
                                </asp:TemplateField>


                            </Columns>


                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />


                            <PagerSettings Position="Bottom" />
                            <PagerStyle CssClass="pagination-sk" />
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" BorderColor="Red" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />


                        </asp:GridView>




                    </ContentTemplate>

                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="DWH.UD.ENT_GA_PROVIDERS" ID="TabPanel1">
                    <ContentTemplate>

                        <div class="PageSummary">
                            This page is for the updating/inserting/deleting of CPT rate records in the table DWH.UD.ENT_GA_PROVIDERS in the Netezza database. Changes inserted into 
               this webpage/tool will not be reflected in any Aginity/Netezza queries until the SQL Server and Netezza databases have had a chance to sync at 4:30 AM.

        </br></br>
                <ul>
                    <li>The PROVIDER_TYPE, MD_NAME, NPI, and CATEGORY fields cannot be edited since they provide the references needed to update a row. If you need to change those you'll need to delete the row and insert a new one 
                with the updates.
                    </li>
                </ul>
                            <a href="#" onclick="$('#Insert_GAPROVIDERS').toggle(100)">Click here to insert a new row</a><asp:Label runat="server" ID="ResponseMsg_GA" Style="padding-left: 10px; font: bold"></asp:Label>
                        </div>



                        <div style="width: 99%; display: none;" id="Insert_GAPROVIDERS">
                            <asp:Table ID="Table2" Width="100%" Visible="true" BackColor="#5bc0de" CellSpacing="2" runat="server" BorderWidth="1px" BorderColor="#003060" ForeColor="Black" Height="50px">
                                <asp:TableRow CssClass="insertrows">
                                    <asp:TableCell HorizontalAlign="Left" Width="4%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="POD_Insert_GA" AutoPostBack="False" placeholder="POD" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="POD_Insert_GA_RE"
                                            ControlToValidate="POD_Insert_GA"
                                            ValidationExpression="\d+"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="8%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="Provider_Insert_GA" AutoPostBack="False" placeholder="Provider" Width="95%"></asp:TextBox>

                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="8%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="MD_Insert_GA" AutoPostBack="False" placeholder="MD Name" Width="95%"></asp:TextBox>


                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="7%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="NPI_Insert_GA" AutoPostBack="False" placeholder="NPI" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="NPI_Insert_GA_RE"
                                            ControlToValidate="NPI_Insert_GA"
                                            ValidationExpression="\d+"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />


                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="4%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="POS_Insert_GA" AutoPostBack="False" placeholder="POS" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="POS_Insert_GA_RE"
                                            ControlToValidate="POS_Insert_GA"
                                            ValidationExpression="\d+"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="17%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="Category_Insert_GA" AutoPostBack="False" placeholder="Category" Width="95%"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="7%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="Rate_Insert_GA" AutoPostBack="False" placeholder="Rate" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Rate_Insert_GA_RE"
                                            ControlToValidate="Rate_Insert_GA"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Left" Width="17%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="AltCategory_Insert_GA" AutoPostBack="False" placeholder="Alt Category" Width="95%"></asp:TextBox>
                                    </asp:TableCell>


                                    <asp:TableCell HorizontalAlign="Left" Width="7%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="AltRate_Insert_GA" AutoPostBack="False" placeholder="Alt Rate" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="AltRate_Insert_GA_RE"
                                            ControlToValidate="AltRate_Insert_GA"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />


                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="10%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="EffFrom_Insert_GA" AutoPostBack="False" placeholder="Effective From" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EffFrom_Insert_GA_RE"
                                            ControlToValidate="EffFrom_Insert_GA"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="10%" CssClass="insertrows">
                                        <asp:TextBox runat="server" ID="EffTo_Insert_GA" AutoPostBack="False" placeholder="Effective To" Width="95%"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="EffTo_Insert_GA_RE"
                                            ControlToValidate="EffTo_Insert_GA"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow HorizontalAlign="Center">
                                    <asp:TableCell ColumnSpan="11">
                                        <asp:Button runat="server" Text="Insert new row" ID="GAProviders_Insert" />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </div>

                        <style>
        .darkfont {
            color:black
        }
    </style>
                        <asp:Table ID="Search_Provider_tbls" Width="99%" Visible="true" BackColor="#2b74bb" CellSpacing="2" runat="server" BorderWidth="1px" BorderColor="#003060" ForeColor="White" Height="50px">
                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center">Search:</asp:TableCell>
                                <asp:TableCell HorizontalAlign="Left">
                                    <asp:TextBox runat="server" ID="tbl_SearchNPI" Width="95%" AutoPostBack="True" placeholder="Search NPI" CssClass="darkfont"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Left">
                                    <asp:TextBox runat="server" ID="tbl_SearchGACategories" Width="95%" AutoPostBack="True" placeholder="Search Categories" CssClass="darkfont"></asp:TextBox>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                        <asp:GridView ID="Update_GAProviders" runat="server" PageSize="25"
                            Width="99%" ShowHeaderWhenEmpty="True" Visible="true" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" textmode="Multiline" AllowSorting="True" CssClass="table table-striped"
                            HeaderStyle-Font-Size="X-Small" Font-Size="Small">
                            <RowStyle CssClass="cursor-pointer" HorizontalAlign="Left" VerticalAlign="Top" />
                            <EditRowStyle CssClass="GridViewEditRow" />
                            <Columns>
                                <asp:CommandField HeaderStyle-Width="5%" ShowDeleteButton="true"
                                    ShowEditButton="true">
                                    <HeaderStyle Width="5%" BackColor="#2b74bb" />
                                </asp:CommandField>

                                <%--                        <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Left"  
                HeaderStyle-Width="3%" HeaderStyle-Wrap="true" 
                HeaderText="ID" 
                Visible="True" ReadOnly="true">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" BackColor="#2b74bb"/>
            </asp:BoundField>   --%>


                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="POD" HeaderStyle-Width="4%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("POD_ID")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox runat="server" ID="POD_ID_EditBox" Text='<%# Eval("POD_ID")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="POD_Regex"
                                            ControlToValidate="POD_ID_EditBox"
                                            ValidationExpression="\d+"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="PROVIDER_TYPE" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="3%" HeaderStyle-Wrap="true"
                                    HeaderText="Provider Type"
                                    Visible="True" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                </asp:BoundField>

                                <%--<asp:TemplateField  HeaderText="Provider Type" HeaderStyle-Width="10%">
    <HeaderStyle HorizontalAlign="Left"  Wrap="True" BackColor="#2b74bb"/>
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("PROVIDER_TYPE")%>' />
    </ItemTemplate>
    <EditItemTemplate >
        <asp:TextBox runat="server"  ID="ProviderType_EditBox" Text='<%# Eval("PROVIDER_TYPE")%>' width="100%" />
    </EditItemTemplate>
</asp:TemplateField>--%>

                                <asp:BoundField DataField="MD_NAME" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="10%" HeaderStyle-Wrap="true"
                                    HeaderText="MD Name"
                                    Visible="True" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                </asp:BoundField>

                                <%--<asp:TemplateField  HeaderText="MD Name" HeaderStyle-Width="10%">
    <HeaderStyle HorizontalAlign="Left"  Wrap="True" BackColor="#2b74bb"/>
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("MD_NAME")%>' />
    </ItemTemplate>
    <EditItemTemplate >
        <asp:TextBox runat="server"  ID="MDName_EditBox" Text='<%# Eval("MD_NAME")%>' width="100%" />
    </EditItemTemplate>
</asp:TemplateField>--%>

                                <asp:BoundField DataField="NPI" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="3%" HeaderStyle-Wrap="true"
                                    HeaderText="NPI"
                                    Visible="True" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                </asp:BoundField>


                                <%--<asp:TemplateField  HeaderText="NPI" HeaderStyle-Width="10%">
    <HeaderStyle HorizontalAlign="Left"  Wrap="True" BackColor="#2b74bb"/>
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("NPI")%>' />
    </ItemTemplate>
    <EditItemTemplate >
        <asp:TextBox runat="server"  ID="NPI_EditBox" Text='<%# Eval("NPI")%>' width="100%" />
                                        <asp:RegularExpressionValidator id="NPI_Regex"
                   ControlToValidate="NPI_EditBox"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Numbers only"
                   ForeColor="Red"
                   runat="server"/>

    </EditItemTemplate>
</asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="POS" HeaderStyle-Width="5%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("POS")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox runat="server" ID="POS_EditBox" Text='<%# Eval("POS")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="POS_Regex"
                                            ControlToValidate="POS_EditBox"
                                            ValidationExpression="\d+"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="Numbers only"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Category" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" HeaderStyle-Wrap="true"
                                    HeaderText="Category"
                                    Visible="True" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                </asp:BoundField>




                                <%--<asp:TemplateField  HeaderText="Category" HeaderStyle-Width="10%">
    <HeaderStyle HorizontalAlign="Left"  Wrap="True" BackColor="#2b74bb"/>
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Eval("CATEGORY")%>' />
    </ItemTemplate>
    <EditItemTemplate >
        <asp:TextBox runat="server"  ID="Category_EditBox" Text='<%# Eval("CATEGORY")%>' width="100%" />
    </EditItemTemplate>
</asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="RATE" HeaderStyle-Width="5%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RATE", "{0:C}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox runat="server" ID="RATE_EditBox" Text='<%# Eval("RATE")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="RATE_Regex"
                                            ControlToValidate="RATE_EditBox"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ALT_CATEGORY" HeaderStyle-Width="20%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("ALT_CATEGORY")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="ALT_CATEGORY_EditBox" Text='<%# Eval("ALT_CATEGORY")%>' Width="100%" />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ALT_RATE" HeaderStyle-Width="5%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ALT_RATE", "{0:C}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox runat="server" ID="ALT_RATE_EditBox" Text='<%# Eval("ALT_RATE")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="ALT_RATE_Regex"
                                            ControlToValidate="ALT_RATE_EditBox"
                                            ValidationExpression="\d+\.\d{2}"
                                            Display="Dynamic"
                                            EnableClientScript="true"
                                            ErrorMessage="Format: 00.00"
                                            ForeColor="Red"
                                            runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Effective From" HeaderStyle-Width="10%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_FROM", "{0:MM/dd/yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="EFFECTIVE_FROM_EditBox" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_FROM", "{0:MM/dd/yyyy}")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="EFFFROM_Regex"
                                            ControlToValidate="EFFECTIVE_FROM_EditBox"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </EditItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Effective To" HeaderStyle-Width="10%">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#2b74bb" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_TO", "{0:MM/dd/yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="EFFECTIVE_TO_EditBox" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_TO", "{0:MM/dd/yyyy}")%>' Width="100%" />
                                        <asp:RegularExpressionValidator ID="EFFTO_Regex"
                                            ControlToValidate="EFFECTIVE_TO_EditBox"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            Display="Static"
                                            EnableClientScript="true"
                                            ErrorMessage="MM/DD/YYYY"
                                            ForeColor="Red"
                                            runat="server" />

                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />


                            <PagerSettings Position="Bottom" />
                            <PagerStyle CssClass="pagination-sk" />
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" BorderColor="Red" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />


                        </asp:GridView>


                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>

        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
