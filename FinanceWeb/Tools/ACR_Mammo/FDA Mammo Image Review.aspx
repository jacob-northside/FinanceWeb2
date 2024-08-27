 <%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FDA Mammo Image Review.aspx.vb" Inherits="FinanceWeb.FDAMammoImageReview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>


    <style>
        .formlabel {
            padding-top: 10px;
        }
        /*CSS form fancy checkboxes*/

        .funkyradio div {
            clear: both;
            /*margin: 0 50px;*/
            overflow: hidden;
        }

        .funkyradio label {
            /*min-width: 400px;*/
            width: 100%;
            height: 70px;
            border-radius: 0px;
            border: 2px solid #D1D3D4;
            font-weight: normal;
            padding-top: 18px;
        }

        .funkyradio input[type="radio"]:empty,
        .funkyradio input[type="checkbox"]:empty {
            display: none;
        }

            .funkyradio input[type="radio"]:empty ~ label,
            .funkyradio input[type="checkbox"]:empty ~ label {
                position: relative;
                line-height: 2.5em;
                text-indent: 3.25em;
                margin-top: .5em;
                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }

                .funkyradio input[type="radio"]:empty ~ label:before,
                .funkyradio input[type="checkbox"]:empty ~ label:before {
                    position: absolute;
                    display: block;
                    top: 0;
                    bottom: 0;
                    left: 0;
                    font-family: 'Glyphicons Halflings';
                    content: "\e085";
                    width: 2.5em;
                    background: #D1D3D4;
                    color: #fff;
                    text-indent: .9em;
                    border-radius: 0px;
                    padding-top: 18px;
                }

        .funkyradio input[type="radio"]:hover:not(:checked) ~ label:before,
        .funkyradio input[type="checkbox"]:hover:not(:checked) ~ label:before {
            font-family: 'Glyphicons Halflings';
            content: "\e013";
            text-indent: .9em;
            color: #C2C2C2;
        }

        .funkyradio input[type="radio"]:hover:not(:checked) ~ label,
        .funkyradio input[type="checkbox"]:hover:not(:checked) ~ label {
            color: #888;
        }

        .funkyradio input[type="radio"]:checked ~ label:before,
        .funkyradio input[type="checkbox"]:checked ~ label:before {
            font-family: 'Glyphicons Halflings';
            content: "\e013";
            text-indent: .9em;
            color: #333;
            background-color: #ccc;
        }

        .funkyradio input[type="radio"]:checked ~ label,
        .funkyradio input[type="checkbox"]:checked ~ label {
            color: #777;
        }

        .funkyradio input[type="radio"]:focus ~ label:before,
        .funkyradio input[type="checkbox"]:focus ~ label:before {
            box-shadow: 0 0 0 3px #999;
        }

        .funkyradio-success input[type="radio"]:checked ~ label:before,
        .funkyradio-success input[type="checkbox"]:checked ~ label:before,
        .funkyradio-success input[type="radio"]:checked ~ label.color,
        .funkyradio-success input[type="checkbox"]:checked ~ label.color {
            color: #fff;
            background-color: #5cb85c;
            border: 1px solid transparent;
        }

        .funkyradio-danger input[type="radio"]:checked ~ label:before,
        .funkyradio-danger input[type="checkbox"]:checked ~ label:before,
        .funkyradio-danger input[type="radio"]:checked ~ label.color,
        .funkyradio-danger input[type="checkbox"]:checked ~ label.color {
            color: #fff;
            background-color: #d9534f;
            border: 1px solid transparent;
        }


        .funkyradio-info input[type="radio"]:checked ~ label:before,
        .funkyradio-info input[type="checkbox"]:checked ~ label:before,
        .funkyradio-info input[type="radio"]:checked ~ label.color,
        .funkyradio-info input[type="checkbox"]:checked ~ label.color {
            color: #fff;
            background-color: #5bc0de;
            border: 1px solid transparent;
        }



        /*CSS form fancy checkboxes 2*/
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            width: 300px;
            height: 140px;
        }

        .mycheckbox input[type="checkbox"] 
{ 
    
    margin-right: 5px; 

}


    </style>
    <style type="text/css">

.CursorHand {
    cursor:pointer;
}

.panelmax 
{
    max-height:600px ;   
}

.hiddenlabel 
{
    display:none  !important;
}
.maxpanelheight
{
    max-height:400px;
}

.td
{
    padding:0;
    margin:0;    
}

        .InstructionsButton-control {
  height: 30px;
  font-size: 12px;
  line-height: 1.428571429;
  color: #444444;
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
        .InstructionsButton-control:hover {
      background-color: #c5d0d6;
  border-width:2px;
  outline: 0;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);

}

.standardButton-control {
  height: 28px;
  font-size: 11px;
  line-height: 1.1;
  color: #444444;
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




        </style>

    <style type="text/css" media="print">
        .Printbutton
        {
            display:none;
         }
        
        .Linking
        {
            font-size:small;
            color:#003060;

         }
        .Linking:hover {
    color: #6da9e3;
    }


    </style>

    <script type="text/javascript" charset="utf-8">
        function open_Instructions() {

            var url = "https://financeweb.northside.local/Tools/ACR_Mammo/FDA%20Mammo%20Image%20Review%20FAQ.aspx";
            myWindow = window.open(url, 'FDA Mammo Image Review Instructions');
            myWindow.focus();

        }

        function filter() {
            var maybeObject = $get('<%=txtTechNameFilter.ClientID%>');
        var table = $get('<%=gvTechAssessment.ClientID%>');
        if (maybeObject != null && table != null) {
            var suche = maybeObject.value.toLowerCase();
            var ele;
            var yn;
            for (var r = 1; r < table.rows.length; r++) {
                ele = table.rows[r].cells[1].innerHTML.replace(/<[^>]+>/g, "");
                yn = 0;
                if (ele.toLowerCase().indexOf(suche) >= 0)
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
        if ($get('<%=ScrollPanelTechAssessment.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos2 = $get('<%=ScrollPanelTechAssessment.ClientID%>').scrollLeft;
                yPos2 = $get('<%=ScrollPanelTechAssessment.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler2(sender, args) {
            if ($get('<%=ScrollPanelTechAssessment.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanelTechAssessment.ClientID%>').scrollLeft = xPos2;
                $get('<%=ScrollPanelTechAssessment.ClientID%>').scrollTop = yPos2;
            }
        }

        prm2.add_beginRequest(BeginRequestHandler2);
        prm2.add_endRequest(EndRequestHandler2);

    </script>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel runat="server" ID="upWholeThing">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" Visible="false">

                <asp:Label runat="server" ID="sortmap"></asp:Label>
                <asp:Label runat="server" ID="sortunmap"></asp:Label>
                <asp:Label runat="server" ID="selectedAccID"></asp:Label>
                <asp:Label runat="server" ID="gvAccessionmap"></asp:Label>
                <asp:Label runat="server" ID="gvAccessiondir"></asp:Label>

            </asp:Panel>


            <cc1:TabContainer ID="MammoImageReview" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1151px">

                <cc1:TabPanel runat="server" HeaderText="Manage Technologists" ID="tpOpenVendor">
                    <ContentTemplate>
                        <asp:Panel runat="server" ScrollBars="Auto" Width="1133px">
                            <%--  <asp:UpdatePanel runat="server" ID="upOpen">
                       <ContentTemplate>--%>

                            <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1133px">
                                <asp:Table runat="server">

                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ColumnSpan="5" Width="900">
                                            <asp:Label runat="server" ID="lblOpenUserLogin" Font-Size="Large" ForeColor="#003060"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">
                                            <asp:Button runat="server" Width="125px" Text="Instructions" CssClass="InstructionsButton-control" OnClientClick="open_Instructions()" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                        <asp:TableCell>
                                        Manage Your Technologists
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Width="350px">
                                        You have access to the following departments:
                                        </asp:TableCell>
                                        <asp:TableCell Width="300px" HorizontalAlign="Left">
                                            <asp:DropDownList Width="100%" runat="server" ID="ddlManageDepartments" AutoPostBack="true" CssClass="form-control" />
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>

                            </asp:Panel>
                            <br />

                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Width="5px">                               
                                    </asp:TableCell>
                                    <asp:TableCell Width="500px" VerticalAlign="Top" HorizontalAlign="Center">
                                        <asp:Panel runat="server" BorderColor="#003060" BorderWidth="1" Width="100%">
                                            <asp:GridView ID="TechNames_Gridview" runat="server" Width="90%" HorizontalAlign="Center"
                                                AllowSorting="True" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="UserLogin"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="5px" FooterStyle-Width="5px">
                                                        <ItemStyle />
                                                        <FooterStyle />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="no_padding">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="/Images/GreenPlus.png" runat="server" ID="btnAddTech" Height="10px" Width="10px"
                                                                CommandName="AddTech" CommandArgument='<%# Bind("UserLogin")%>' />
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                                        HeaderText="All Technologists" ReadOnly="True"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle CssClass="locationcss"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:LinkButton runat="server" ID="AddRemoveUsers_Btn">Add missing Technologist</asp:LinkButton>
                                        </asp:Panel>
                                    </asp:TableCell>

                                    <asp:TableCell Width="500px" VerticalAlign="Top">

                                        <asp:Panel runat="server" BorderColor="#003060" BorderWidth="1" Width="100%">
                                            <asp:GridView ID="YourTechRoster_Gridview" runat="server" Width="90%" HorizontalAlign="Center"
                                                AllowSorting="True" AllowPaging="true" PageSize="25" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="UserLogin"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="5px">
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="no_padding">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="/Images/RedMinus.png" runat="server" ID="btnRemoveTech" Height="10px" Width="10px"
                                                                CommandName="RemoveTech" CommandArgument='<%# Bind("UserLogin")%>' />
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <%--                                                <asp:BoundField DataField="UserLogin" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                                    HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                                    HeaderText="Your Techs" ReadOnly="True"
                                                    Visible="True">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    <ItemStyle CssClass="locationcss"></ItemStyle>
                                                </asp:BoundField>--%>
                                                    <asp:BoundField DataField="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                                        HeaderText="Your Technologists" ReadOnly="True"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle CssClass="locationcss"></ItemStyle>
                                                    </asp:BoundField>

                                                </Columns>
                                            </asp:GridView>
                                            <%-- <br />
                                   <h4 class="no_padding_top" style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #000000; margin-top: 20px">Pending changes:</h4>--%>
                                            <br />
                                            <asp:GridView BorderColor="#003060" BorderWidth="1" Width="90%" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False" ID="gvPendingChanges" BackColor="White" ForeColor="#003060" BorderStyle="None">
                                                <Columns>

                                                    <asp:TemplateField ItemStyle-CssClass="no_padding">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="/Images/RedMinus.png" runat="server" ID="btnTechMinus" Height="10px" Width="10px" Visible="false"
                                                                CommandName="RemoveTech" CommandArgument='<%# Bind("UserLogin")%>' />
                                                            <asp:ImageButton ImageUrl="/Images/GreenPlus.png" runat="server" ID="btnTechPlus" Height="10px" Width="10px" Visible="false"
                                                                CommandName="RemoveTech" CommandArgument='<%# Bind("UserLogin")%>' />
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="UserName"
                                                        HeaderText="Pending Changes" ReadOnly="True"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:Button runat="server" ID="btnPendingChanges" CssClass="btn-block btn-info" Text="Apply Pending Changes" />

                                        </asp:Panel>
                                    </asp:TableCell>


                                </asp:TableRow>
                            </asp:Table>

                            <asp:Label runat="server" ID="FakeButtonFirstTab"></asp:Label>
                            <cc1:ModalPopupExtender ID="mpeFirstTab" runat="server" PopupControlID="pnlMPEFirstTab" TargetControlID="FakeButtonFirstTab"
                                BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlMPEFirstTab" Height="120px" runat="server" CssClass="modalPopup" align="center" Style="display: none; position: relative">
                                <div style="padding: 10px">

                                    <asp:Label runat="server" ID="lblMPEFirstTabeExplanation" ForeColor="Red"></asp:Label><br />

                                    <br />
                                </div>

                                <%--<div style="width: 100%; bottom: 10px">--%>

                                <asp:Button CssClass="btn btn-danger" ID="Button2" runat="server" Text="Close" />


                                <%--</div>--%>
                            </asp:Panel>

                            <cc1:ModalPopupExtender ID="AddRemoveUser_Modal" runat="server" PopupControlID="AddRemoveUser_Panel" TargetControlID="fakeButtonModal1"
                                BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>

                            <asp:Label runat="server" ID="fakeButtonModal1"></asp:Label>
                            <asp:Panel ID="AddRemoveUser_Panel" runat="server" align="center" Style="display: none; position: relative">
                                <asp:Table runat="server" Width="600px" BackColor="White" BorderColor="Black" BorderStyle="Double" BorderWidth="3">
                                    <asp:TableRow>
                                        <asp:TableCell Width="200px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Center" Width="200px">
                                            <div style="padding: 10px">
                                                <label for="AddRemoveUser_ID" class="control-label formlabel" style="display: inline">Add User</label><br />
                                                <asp:Label runat="server" ID="lblErrorAddingRemoving" ForeColor="Red"></asp:Label><br />
                                                Employee UserLogin:
                                                <asp:TextBox runat="server" ID="AddRemoveUser_ID" CssClass="form-control"></asp:TextBox>
                                                <%--<input type="text" class="form-control" id="AddRemoveUser_ID" placeholder="Employee ID" />--%>
                                                <br />

                                            </div>
                                        </asp:TableCell><asp:TableCell Width="200px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                                            <asp:LinkButton runat="server" ID="lbSrchUsr" Font-Size="X-Small" Text="Don't know UserLogin?"></asp:LinkButton>
                                            <asp:Panel CssClass="maxpanelheight" ScrollBars="Auto" runat="server" ID="pnlSrchUser" Visible="false" Width="600px">
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
                                                            <asp:Button runat="server" ID="btnAdminUsrSrch" CssClass="standardButton-control" Text="Search" />
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
                                                                    <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='../AR/Images/PngB.png'" onmouseout="this.src='../AR/Images/PngA.png'" />
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
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Center">
                                            <%--<div style="width: 100%; bottom: 10px">--%>
                                            <asp:Button CssClass="btn btn-success" ID="AddRemoveUser_Submit" runat="server" Text="Submit" />
                                            <asp:Button CssClass="btn btn-danger" ID="AddRemoveUser_CloseBtn" runat="server" Text="Close" />


                                            <%--</div>--%>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>



                            </asp:Panel>

                        </asp:Panel>
                    </ContentTemplate>


                    <%-- </asp:UpdatePanel>
               </ContentTemplate>--%>
                </cc1:TabPanel>


                <cc1:TabPanel runat="server" HeaderText="Image Evaluations" ID="tpImageEvaluations">
                    <ContentTemplate>
                        <asp:Panel runat="server" Width="1133px" ScrollBars="Auto">


                            <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1133px">
                                <asp:Table runat="server">

                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ColumnSpan="5">
                                            <asp:Label runat="server" ID="Label1" Font-Size="Large" ForeColor="#003060"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell>
                                        Filter Technologists by departments:
                                        </asp:TableCell>
                                        <asp:TableCell Width="300px">
                                            <asp:DropDownList Width="100%" runat="server" ID="TechFilterDDL" AutoPostBack="true" CssClass="form-control" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell>
                                        Filter Technologists by Name:
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtFilterYourTechs" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>

                            </asp:Panel>
                            <br />

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="5px">                               
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Top" Width="400px">
                                        <asp:Panel runat="server" Width="100%">
                                            <asp:GridView ID="YourTechs_Gridview" runat="server" CssClass="CursorHand"
                                                AllowSorting="True" AllowPaging="true" PageSize="25" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="UserLogin"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="10px" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="10px" />
                                                    </asp:CommandField>

                                                    <asp:BoundField DataField="DisplayName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                                        HeaderStyle-Width="95%" HeaderStyle-Wrap="true"
                                                        HeaderText="Your Technologists" ReadOnly="True"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                        <ItemStyle CssClass="locationcss"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>

                                        </asp:Panel>
                                    </asp:TableCell>

                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:Panel Visible="false" ID="pnlShowCreateNewID" runat="server">

                                            <asp:GridView ID="gvTechIssues" runat="server" Width="700px"
                                                AllowSorting="True" AllowPaging="true" PageSize="25" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="Small"
                                                HeaderStyle-Wrap="true" ForeColor="Black"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="10px">
                                                        <HeaderStyle Width="10px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="FY" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Fiscal Year" ReadOnly="True"
                                                        Visible="True"></asp:BoundField>
                                                    <asp:BoundField DataField="FQ" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Fiscal Quarter" ReadOnly="True"
                                                        Visible="True"></asp:BoundField>
                                                    <asp:BoundField DataField="ExamIssue" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Exam Issue" ReadOnly="True"
                                                        Visible="True"></asp:BoundField>
                                                    <asp:BoundField DataField="ErrorCount" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Issue Count" ReadOnly="True"
                                                        Visible="True"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />


                                            <asp:Button runat="server" CssClass="form-control" ID="btnNewAccessionID" />
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel Visible="false" ID="pnlShowAccessionIDs" runat="server" Width="100%">
                                            Existing Accession IDs for
                                   <asp:Label runat="server" ID="lblUserSelected"></asp:Label>:
                                   <asp:GridView ID="gvAccessions" runat="server" CssClass="CursorHand" Width="700px"
                                       AllowSorting="True" AllowPaging="true" PageSize="25" AutoGenerateColumns="False" BorderColor="Black"
                                       BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                       HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                       HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="ACCID"
                                       BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                       <AlternatingRowStyle BackColor="white" />

                                       <Columns>
                                           <asp:CommandField ItemStyle-Width="10px" ShowSelectButton="true" SelectText="">
                                               <HeaderStyle Width="10px" />
                                           </asp:CommandField>
                                           <asp:BoundField DataField="AccessionNumber" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                               HeaderStyle-Wrap="true" SortExpression="AccessionNumber"
                                               HeaderText="Accession Number" ReadOnly="True"
                                               Visible="True">
                                               <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                               <ItemStyle CssClass="locationcss"></ItemStyle>
                                           </asp:BoundField>
                                           <asp:BoundField DataField="Department" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                               HeaderStyle-Wrap="true"
                                               HeaderText="Dept" ReadOnly="True" SortExpression="Department"
                                               Visible="True">
                                               <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                               <ItemStyle CssClass="locationcss"></ItemStyle>
                                           </asp:BoundField>
                                           <asp:BoundField DataField="ImageReviewDateDisplay" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="locationcss"
                                               HeaderStyle-Wrap="true" SortExpression="ImageReviewDate"
                                               HeaderText="Review Date" ReadOnly="True"
                                               Visible="True">
                                               <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                               <ItemStyle CssClass="locationcss"></ItemStyle>
                                           </asp:BoundField>
                                           <asp:BoundField DataField="ImageReviewDate"
                                               ItemStyle-CssClass="hidden"
                                               HeaderStyle-CssClass="hidden"></asp:BoundField>
                                           <asp:BoundField DataField="Dept_ID"
                                               ItemStyle-CssClass="hidden"
                                               HeaderStyle-CssClass="hidden"></asp:BoundField>
                                           <asp:BoundField DataField="DateCreated" HeaderStyle-HorizontalAlign="Left"
                                               HeaderStyle-Wrap="true" SortExpression="DateCreated" ItemStyle-Width="120px" ControlStyle-Width="120px"
                                               HeaderText="Date Entered" ReadOnly="True"
                                               Visible="True">
                                               <HeaderStyle HorizontalAlign="Left" Wrap="True" />

                                           </asp:BoundField>
                                           <asp:BoundField DataField="LastUpdated" HeaderStyle-HorizontalAlign="Left"
                                               HeaderStyle-Wrap="true" SortExpression="LastUpdated" ItemStyle-Width="120px" ControlStyle-Width="120px"
                                               HeaderText="Last Updated" ReadOnly="True"
                                               Visible="True">
                                               <HeaderStyle HorizontalAlign="Left" Wrap="True" />

                                           </asp:BoundField>
                                       </Columns>
                                   </asp:GridView>
                                            <br />

                                        </asp:Panel>
                                    </asp:TableCell>


                                </asp:TableRow>
                            </asp:Table>
                            <asp:Panel runat="server" ID="pnlAccessionDetails" Visible="false">
                                <br />
                                <asp:Panel runat="server" Width="100%" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">

                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell Height="5px"></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="5px"></asp:TableCell>
                                            <asp:TableCell Width="800px" ForeColor="#003060">
                                       <b>Northside Hospital Breast Imaging Peer Review</b>
                                       <br />
                                       This form is only utilized for Peer Review of Mammograms in order to comply with FDA and MQSA Equip initiative to assure compliance with standards for clinical image quality.
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableHeaderRow>
                                            <asp:TableCell Height="10px"></asp:TableCell>
                                        </asp:TableHeaderRow>
                                    </asp:Table>
                                </asp:Panel>

                                <asp:Table runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                        </asp:TableCell>
                                        <asp:TableCell>
                               <b>Accession ID:</b>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox CssClass="form-control" Width="100%" runat="server" ID="txtAccessionNumber"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell>
                               <b>Person Being Reviewed:</b>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblPersonReviewed" Width="100%" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell>
                              <b> Department where Imaging was performed:</b>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList runat="server" ID="EvalFacility_DDL" CssClass="form-control" Width="100%" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                        </asp:TableCell>
                                        <asp:TableCell>
                               <b>Date of Image Review:</b>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtImageDate"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calextSubEff"
                                                runat="server" TargetControlID="txtImageDate" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell>
                               <b>Were the Images acceptable?</b>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>

                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <div class="funkyradio">
                                                <div class="funkyradio-success">
                                                    <asp:RadioButton ID="rbAccept" AutoPostBack="true" runat="server" Text="Yes, images are of acceptable quality." />
                                                    <%--<input type="radio" name="radio" id="AcceptImg_Btn" />
                                                        <label for="AcceptImg_Btn" class="color">Yes, images are of acceptable quality.</label>--%>
                                                </div>
                                                <div class="funkyradio-danger">
                                                    <asp:RadioButton ID="rbReject" AutoPostBack="true" runat="server" Text="No, image quality is not acceptable." />
                                                    <%-- <input type="radio" name="radio" id="RejectImg_Btn" />
                                                        <label for="RejectImg_Btn" class="color">No, image quality is not acceptable.</label>--%>
                                                </div>
                                            </div>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>


                                <asp:Panel runat="server" ID="pnlExamIssues" Visible="false" Width="100%" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">

                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell Width="5">

                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="5" Width="1000px" BackColor="White" BorderColor="#003060" BorderWidth="1">
                                                <asp:GridView ID="gvExamIssuesMain" runat="server" Width="100%" HorizontalAlign="Center"
                                                    PageSize="25" AutoGenerateColumns="False"
                                                    Font-Size="Small"
                                                    ForeColor="Black" DataKeyNames="Description" ShowHeader="false"
                                                    BackColor="white" CellPadding="5" CellSpacing="3">


                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="5px"
                                                            HeaderText="" ReadOnly="True"
                                                            Visible="True"></asp:BoundField>
                                                        <asp:BoundField DataField="Description" ItemStyle-Width="150px"
                                                            HeaderText="" ReadOnly="True"
                                                            Visible="True"></asp:BoundField>
                                                        <asp:TemplateField ItemStyle-CssClass="no_padding">
                                                            <ItemTemplate>
                                                                <asp:DataList ID="gvExamIssuesDetail1" runat="server" RepeatDirection="Horizontal">
                                                                    <ItemTemplate>
                                                                        <asp:Panel runat="server" Width="100px">
                                                                            <asp:DropDownList runat="server" ID="ddlIssueYesNo" CssClass="form-control"
                                                                                SelectedValue='<%# Eval("Data")%>'>
                                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox runat="server" Visible='<%#Eval("IsOther")%>' ID="txtIssueOtherComment" CssClass="form-control" Text='<%# Eval("IssueComments")%>'></asp:TextBox>
                                                                            <%-- <input runat="server" type="number" value='<%# Eval("Data")%>' class="form-control tableinput" min="0" id="txtIssueCount" />--%>
                                                                            <asp:Label runat="server" Visible="false" ID="lblIssueDesc" Text='<%# Eval("Description")%>'></asp:Label>
                                                                        </asp:Panel>

                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                                </asp:DataList>
                                                                <asp:DataList ID="gvExamIssuesDetail2" runat="server" RepeatDirection="Horizontal">
                                                                    <ItemTemplate>
                                                                        <asp:Panel runat="server" Width="100px">
                                                                            <asp:Label runat="server" Text='<%# Eval("Data")%>'></asp:Label>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                                </asp:DataList>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                </asp:GridView>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Height="10px"></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell Width="1000px" ColumnSpan="5" BackColor="White" BorderColor="#003060" BorderWidth="1">
                                                <asp:Table runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="5px"></asp:TableCell>
                                                        <asp:TableCell>
                                                            <label class="control-label formlabel">Corrective Action Taken: </label>
                                                            <div class="funkyradio">
                                                                <div class="funkyradio-info">
                                                                    <asp:CheckBox Width="900px" ID="rbCorr" AutoPostBack="true" runat="server" Text="Problems reviewed with technologist; make sure to sign and date form." />

                                                                    <%--<input type="radio" name="radio" id="Corr1" />
                                   <label for="Corr1" class="color">Problems reviewed with technologist, sign and date below.</label>--%>
                                                                </div>
                                                                <%--                                   <div class="funkyradio-info">
                                                                    <asp:RadioButton Width="900px" ID="rbnoCorr" AutoPostBack="true" runat="server" Text="No corrective action taken." />                                                         
                                                                </div>
                                                            <div class="funkyradio-info">
                                                                    <asp:RadioButton Width="900px" ID="rbCorr2" AutoPostBack="true" runat="server" Text="Technologist provided written requirements for ACR Clinical Image quality to read, sign, and date." />
                                                                </div>
                                                                <div class="funkyradio-info">
                                                                    <asp:RadioButton Width="900px" ID="rbCorr3" AutoPostBack="true" runat="server" Text="Technologist required to have 4 hours of additional training in mammography." />
                                                                </div>
                                                                <div class="funkyradio-info">
                                                                    <asp:RadioButton Width="900px" ID="rbCorr4" AutoPostBack="true" runat="server" Text="Technologist required to have 8 hours of additional training in mammography and perform 10 supervised mammograms." />
                                                                </div>--%>
                                                            </div>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>

                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <%-- <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell ColumnSpan="8">
                                                <asp:Table runat="server" Width="1050px" BackColor="White" BorderColor="#003060" BorderWidth="1">

                                                    <asp:TableRow>
                                                        <asp:TableCell Width="5px"></asp:TableCell>

                                                        <asp:TableCell><b>Improvement after Corrective Action?</b></asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell>
                                                            <div class="funkyradio">
                                                                <asp:Table runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <div class="funkyradio-success">
                                                                                <asp:RadioButton Width="150px" ID="rbCorrectiveActionYes" AutoPostBack="true" runat="server" Text="Yes" />
                                                                            </div>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell>
                                                                            <div class="funkyradio-danger">
                                                                                <asp:RadioButton Width="150px" ID="rbCorrectiveActionNo" AutoPostBack="true" runat="server" Text="No" />
                                                                            </div>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>


                                                            </div>
                                                        </asp:TableCell>

                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell><b>Improvement after Additional Corrective Action?</b></asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell>
                                                            <div class="funkyradio">
                                                                <asp:Table runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <div class="funkyradio-success">
                                                                                <asp:RadioButton Width="150px" BackColor="White" ID="rbAddCorrectiveActionYes" AutoPostBack="true" runat="server" Text="Yes" />
                                                                            </div>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell>
                                                                            <div class="funkyradio-danger">
                                                                                <asp:RadioButton Width="150px" BackColor="White" ID="rbAddCorrectiveActionNo" AutoPostBack="true" runat="server" Text="No" />
                                                                            </div>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>


                                                            </div>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>--%>
                                    </asp:Table>


                                    <br />



                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server" Width="100%" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell ColumnSpan="5">
                                                <b>Comments:</b>
                                                <br />
                                                <asp:Label runat="server" ID="lblExistingComments" Width="900px"></asp:Label><br />
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtComments" TextMode="MultiLine" Height="100px" Width="900px"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>

                                </asp:Panel>

                                <asp:Table runat="server">

                                    <asp:TableRow>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button Width="300" runat="server" ID="btnSubmitAccession" Text="Submit" CssClass="form-control" />
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>

                            </asp:Panel>


                        </asp:Panel>
                        <asp:Label runat="server" ID="fakeButtonAccessions"></asp:Label>
                        <cc1:ModalPopupExtender ID="mpeSubmittingAccession" runat="server" PopupControlID="ModalPopupAccessions" TargetControlID="fakeButtonAccessions"
                            BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="ModalPopupAccessions" Height="120px" runat="server" CssClass="modalPopup" align="center" Style="display: none; position: relative">
                            <div style="padding: 10px">
                                <asp:Label runat="server" ID="ExplanationLabelACCGood" ForeColor="Black"></asp:Label><br />
                                <asp:Label runat="server" ID="ExplanationLabelACCErrors" ForeColor="Red"></asp:Label><br />
                                <asp:Label runat="server" ID="focusObject" Visible="false"></asp:Label>
                                <br />
                            </div>

                            <%--<div style="width: 100%; bottom: 10px">--%>
                            <asp:Button CssClass="btn btn-success" ID="btnMPEAccOkay" runat="server" Text="Okay" />
                            <asp:Button CssClass="btn btn-danger" ID="btnMPEAccClose" runat="server" Text="Close" />


                            <%--</div>--%>
                        </asp:Panel>

                    </ContentTemplate>


                    <%-- </asp:UpdatePanel>
               </ContentTemplate>--%>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Tech Monitoring" ID="tpTechMonitoring" Visible="false">
                    <ContentTemplate>

                        <asp:Table runat="server" Width="100%" CssClass="supercollapsetable">

                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center">
                                    <asp:Table runat="server" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Center">
                                                Filter Techs:&nbsp;&nbsp;                                
                                    <asp:TextBox Width="200px" runat="server" CssClass="standardButton-control" ID="txtTechNameFilter" oninput="filter()" onkeyup="filter()"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Center" Width="200px">
                                                <asp:CheckBoxList CssClass="mycheckbox" AutoPostBack="true" ForeColor="#003060" runat="server" ID="cblFacsFilter"></asp:CheckBoxList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell HorizontalAlign="Center" ID="trVisible3">

                                    <asp:Label runat="server" ID="lblTechy" Text="Please Select Technologist to Assess"></asp:Label><br />
                                    <br />
                                    <asp:DropDownList runat="server" ID="ddlSelectTechDisplay" AutoPostBack="true" Width="200px" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="Display FQ" Value="FQ"></asp:ListItem>
                                        <asp:ListItem Text="Display FM" Value="FM"></asp:ListItem>
                                    </asp:DropDownList>
                                </asp:TableCell>

                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="500px">
                                    <asp:Panel ID="ScrollPanelTechAssessment" runat="server" CssClass="maxpanelheight" ScrollBars="Auto">
                                        <asp:GridView runat="server" ID="gvTechAssessment" DataKeyNames="UserLogin, UserName" AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                            ForeColor="Black" HeaderStyle-Wrap="true"
                                            BorderWidth="1px" AllowSorting="True" Width="500px"
                                            BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                            Font-Size="X-Small" CssClass="CursorHand">
                                            <AlternatingRowStyle BackColor="White" />

                                            <Columns>
                                                <asp:CommandField ItemStyle-Width="2px" ShowSelectButton="True" Visible="True" SelectText="" />
                                                <asp:BoundField DataField="UserName" />
                                                <asp:BoundField DataField="Departments" HeaderText="Current Departments" />
                                                <asp:BoundField DataField="MaxReviewDate" HeaderText="Last Image Review Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                </asp:TableCell>
                                <asp:TableCell Width="10px"></asp:TableCell>
                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Center">
                                    <asp:GridView runat="server" ID="gvTechQuarters" Visible="false" AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                        HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                        ForeColor="Black" HeaderStyle-Wrap="true"
                                        BorderWidth="1px" AllowSorting="True" Width="400px"
                                        BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                        Font-Size="X-Small">
                                        <AlternatingRowStyle BackColor="White" />

                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="75px" HeaderText="" DataField="QuarterName" />
                                            <asp:BoundField HeaderText="Cases Not Acceptable" DataField="NotAcceptable" />
                                            <asp:BoundField HeaderText="Cases Acceptable" DataField="Acceptable" />
                                            <asp:BoundField HeaderText="Total Errors" DataField="ErrorCount" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView runat="server" ID="gvTechMonths" Visible="false" AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                        HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                        ForeColor="Black" HeaderStyle-Wrap="true"
                                        BorderWidth="1px" AllowSorting="True" Width="400px"
                                        BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                        Font-Size="X-Small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField ItemStyle-Width="75px" HeaderText="" DataField="MonthName" />
                                            <asp:BoundField HeaderText="Cases Not Acceptable" DataField="NotAcceptable" />
                                            <asp:BoundField HeaderText="Cases Acceptable" DataField="Acceptable" />
                                            <asp:BoundField HeaderText="Total Errors" DataField="ErrorCount" />
                                        </Columns>
                                    </asp:GridView>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TRVisible1" Visible="false">
                                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                                    <asp:GridView runat="server" ID="gvTechActionHistory" Visible="false" AutoGenerateColumns="false" ShowHeader="true" BorderColor="#003060" BackColor="#CBE3FB"
                                        HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Font-Bold="false"
                                        ForeColor="Black" HeaderStyle-Wrap="true"
                                        BorderWidth="1px" AllowSorting="True" Width="800px"
                                        BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                        Font-Size="Small" CssClass="CursorHand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Corrective Action" DataField="CorrectiveAction" />
                                            <asp:BoundField HeaderText="Date Recorded" DataField="DateRecorded" />
                                            <asp:BoundField HeaderText="User" DataField="UserName" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="btnRemoveCorrectiveAction" Text="Remove" Visible='<%# Bind("btnVisible")%>'
                                                        CssClass="standardButton-control" CommandName="RemoveCorrectiveAction" CommandArgument='<%# Bind("CorrID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>


                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TRVisible2" Visible="false">
                                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                                    <br />
                                    <asp:Button runat="server" ID="btnAddAction" CssClass="standardButton-control" Text="Add Corrective Action" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnRecordImprovement" CssClass="standardButton-control" Text="Has there been Improvement?" />

                                </asp:TableCell>

                            </asp:TableRow>

                        </asp:Table>

                        <cc1:ModalPopupExtender ID="mpeCorrectiveActions" runat="server" PopupControlID="pnlModalCorrectiveActions" TargetControlID="fakebuttonCorrectiveActions"
                            BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>

                        <asp:Label runat="server" ID="fakebuttonCorrectiveActions"></asp:Label>
                        <asp:Panel runat="server" ID="pnlModalCorrectiveActions" HorizontalAlign="center" BorderColor="#003060" BackColor="White" BorderWidth="3px" BorderStyle="Double">
                            <asp:Table CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" runat="server" HorizontalAlign="Center" ForeColor="#003060" Font-Bold="true" Font-Size="Large">
                                <asp:TableRow>
                                    <asp:TableCell Width="20px"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" VerticalAlign="Top" CssClass="StandardWidthTable">
                                        <asp:Panel runat="server" ID="pnlPopupCorrectiveAction">
                                            <asp:DropDownList CssClass="form-control" DataTextField="CorrectiveAction" DataValueField="CorrID" runat="server" ID="ddlCorrectiveAction"></asp:DropDownList><br />
                                            <br />
                                            <asp:Button runat="server" Width="200px" CssClass="standardButton-control" ID="btnAddCorrectiveAction" Text="Confirm" /><br />
                                            <asp:Label runat="server" ID="lblCorrectiveActionError" ForeColor="Red"></asp:Label>
                                        </asp:Panel>

                                    </asp:TableCell>
                                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right">
                                        <asp:Button ID="btnMasterCloseLogin" CssClass="CloseButton" runat="server" Text="X" BackColor="White" BorderStyle="None" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="25px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>


                    </ContentTemplate>
                </cc1:TabPanel>



            </cc1:TabContainer>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
