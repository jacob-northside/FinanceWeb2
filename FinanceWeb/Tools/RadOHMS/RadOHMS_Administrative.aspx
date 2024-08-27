<%@ Page Title="Rad OHMS Administrative Tool" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RadOHMS_Administrative.aspx.vb" Inherits="FinanceWeb.RadOHMS_Administrative" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>


            <div runat="server" id="FlashMessages" style="text-align:center; color:red" visible="false"></div>
    <asp:Table runat="server" BackColor="#CBE3FB" Width="100%" HorizontalAlign="Center">
        <asp:TableHeaderRow Visible="true">
            <asp:TableHeaderCell Width="200px" ForeColor="Black">
Edit Permissions for User:
            </asp:TableHeaderCell>
            <asp:TableCell Width="200px">
                <asp:TextBox runat="server" ID="Add_UserIDPermissions" />
            </asp:TableCell><asp:TableCell Width="175px">    <asp:Button runat="server" ID="FetchUserPermissions_Btn" Text="Load User Permissions" /></asp:TableCell>
            <asp:TableCell><asp:Button runat="server" ID="SelectAllLocations_Btn" Text="Select All Locations" /></asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell Visible="false">
                <asp:LinkButton runat="server" ID="lbSrchUsr" Font-Size="X-Small" Text="Don't know UserLogin?"></asp:LinkButton>
                <asp:Panel runat="server" ID="pnlSrchUser" Visible="false" BackColor="#CBE3FB">
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
                                <asp:LinkButton runat="server" ID="lbCloseUsrSrch" Font-Size="X-Small" Text="Close Search"></asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell Width="5px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:UpdateProgress runat="server" ID="updateProgressSearching">
                                    <ProgressTemplate>
                                        <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='images/PngB.png'" onmouseout="this.src='images/PngA.png'" />
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
            </asp:TableCell></asp:TableRow></asp:Table><style>
                .no_padding { padding: 0px; margin: 0px; vertical-align: middle}
                .no_padding_top { padding: 0px; margin: 0px; vertical-align: top}
                .submit { vertical-align: bottom; margin-top:15px}

        </style><script>

                    function getEventTarget(e) {
                        e = e || window.event;
                        return e.target || e.srcElement;
                    }

                    var ul = document.getElementById('MainContent_ListPermissionLocations');
                    var img_src = '<img class="RedMinus" onclick="remove();" src="/Images/RedMinus.png" height="10px" width="10px">'

                    function addlocation(event) {
                        var ul = document.getElementById("MainContent_UserLocations");
                        var assigned_locations = ul.getElementsByTagName("li");
                        var location_array = [];

                                       
                        for (var i = 0; i < assigned_locations.length; ++i) {
                            // do something with items[i], which is a <li> element
                            console.log(assigned_locations[i].innerHTML.replace(img_src, "").replace(/^\d{1,10} - /, ""))

                            location_array[i] = assigned_locations[i].innerHTML.replace(img_src, "").replace(/^\d{1,10} - /, "");
                        }

                        var target = getEventTarget(event);
                        var selection = $(target).closest('tr').find('.locationcss').text().replace(img_src, "").replace(/^\d{1,10} - /, "")
                        console.log("Selection: " + selection);
                        console.log("Array: " + location_array);

                        if ($.inArray(selection, location_array) == -1 && $.inArray("All Locations", location_array)) {
                            $('ul#MainContent_UserLocations').append("<li class=\"no_padding\"><img class=\"RedMinus\" onClick=\"remove();\" src=\"/Images/RedMinus.png\" height=\"10px\" width=\"10px\"/>" + $(target).closest('tr').find('.locationcss').text().replace(img_src, "") + "</li>");
                            $('#MainContent_Locations_Field').val(function (i, val) { if (val == '') { return $(target).closest('tr').find('.locationcss').text().replace(img_src, "").replace(/^\d{1,10} - /, "") } else { return val + '|' + $(target).closest('tr').find('.locationcss').text().replace(img_src, "").replace(/^\d{1,10} - /, ""); } });
                        }

                    };

                    $(document).ready(function () {
                        if ($('#MainContent_Locations_Field').val() != "") {
                            console.log("On Page Load: " + $('#MainContent_Locations_Field').val());
                            $('#MainContent_Locations_Field').val().split('|').forEach(function (i, e, array) {
                                var target = getEventTarget(event);
                                console.log(i + " -- " + e);
                                $('ul#MainContent_UserLocations').append("<li>" + img_src + i + "</li>");
                            })
                        }
                    });

                    function remove() {  // Account for  the removed term being the front/middle + back + only one in list
                        $(event.target).closest("li").remove();
                        console.log("Remove : " + $('#MainContent_Locations_Field').val().indexOf($(event.target).closest('li').text().replace(/^\d{1,10} - /, "")));
                        if ($('#MainContent_Locations_Field').val().indexOf($(event.target).closest('li').text() + '|') >= 0)
                        {
                            $('#MainContent_Locations_Field').val($('#MainContent_Locations_Field').val().replace($(event.target).closest('li').text() + '|', ''));
                        }
                        else if ($('#MainContent_Locations_Field').val().indexOf('|' + $(event.target).closest('li').text()) >= 0)
                        {
                            $('#MainContent_Locations_Field').val($('#MainContent_Locations_Field').val().replace('|' + $(event.target).closest('li').text(), ''));
                        }
                        else if ($('#MainContent_Locations_Field').val().indexOf($(event.target).closest('li').text()) >= 0)
                        {
                            $('#MainContent_Locations_Field').val($('#MainContent_Locations_Field').val().replace($(event.target).closest('li').text(), ''));
                        }
                    }


    </script><table width="99%">
    <tr>
        <td width="50%">
    <asp:GridView style="margin: 0px;" runat="server" ID="ListPermissionLocations" BorderColor="Black"
        BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Wrap="true" PageSize="25"
        ForeColor="Black" BackColor="White" BorderWidth="1px"
        Width="100%" ShowHeaderWhenEmpty="True" Visible="False" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" textmode="Multiline" AllowSorting="True"><HeaderStyle HorizontalAlign="Left" Wrap="True" BackColor="#214B9A" ForeColor="#FFCBA5"></HeaderStyle>

<RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
        <AlternatingRowStyle BackColor="#84b1ff" BorderColor="Red" />

        <Columns>
            <asp:TemplateField ItemStyle-CssClass="no_padding">
                <ItemTemplate>
                    <img src="/Images/GreenPlus.png" height="10px" width="10px" onclick="addlocation()" />
                </ItemTemplate>
            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" /><ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>

            <asp:BoundField DataField="LocationDesc" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                HeaderText="Location Name" ReadOnly="True"
                Visible="True">
                <HeaderStyle HorizontalAlign="Left" Wrap="True" />
            <ItemStyle CssClass="locationcss"></ItemStyle>
</asp:BoundField>
        </Columns>
    </asp:GridView>
        </td>
        <td width="50%" class="no_padding_top">
            <h4 style="display:inline; border-bottom-style:solid; border-bottom-color:black;">Selected User: </h4><h4 style="display:inline; color:red;  border-bottom-style:solid; border-bottom-color:black;" runat="server" id="Locations_SelectedUser">No User Selected</h4><h4 >Selected Locations: </h4><ul runat="server" id="UserLocations" style="padding:0px; margin: 0px; list-style: none;">
        </ul><asp:HiddenField runat="server" ID="Locations_Field" Value="" />
            <asp:Button runat="server" ID="SubmitPermissionChanges_Btn" Text="Submit Permission Changes" CssClass="submit"/>

            </td>
        </tr>
</table>



</asp:Content>
