<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Medquest Allocation.aspx.vb" Inherits="FinanceWeb.Medquest_Allocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" charset="utf-8">
     
              
        function filter() {
           
            var maybeObject = $get('<%=txtUserSearch.ClientID%>');
            if (maybeObject != null) {
                var suche = maybeObject.value.toLowerCase();
                var table = $get('<%=gvUserAccess.ClientID%>');
                var ele;
                var ele2;
                var ele3;
                var ele4;
                var ele5;
                var ele6;

                var yn;
                if (table != null)
                    for (var r = 1; r < table.rows.length; r++) {
                        ele = table.rows[r].cells[1].innerHTML.replace(/<[^>]+>/g, "");
                        ele2 = table.rows[r].cells[2].innerHTML.replace(/<[^>]+>/g, "");
                        ele3 = table.rows[r].cells[3].innerHTML.replace(/<[^>]+>/g, "");
                        ele4 = table.rows[r].cells[4].innerHTML.replace(/<[^>]+>/g, "");
                        ele5 = table.rows[r].cells[5].innerHTML.replace(/<[^>]+>/g, "");
                        ele6 = table.rows[r].cells[6].innerHTML.replace(/<[^>]+>/g, "");                        

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
                        if (yn > 0)
                            table.rows[r].style.display = '';
                        else table.rows[r].style.display = 'none';
                        ;
                    }
            }
            ;
        }

<%--        function CountCheckboxes() {

            var count = $('.chkCount :checkbox:checked').length;
            var TranDesc = $get('<%=txtTranDescs.ClientID%>');
            TranDesc.textContent = count;
        }--%>

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(filter);


        function CheckAll() {
            var count = 0;
            $('#' + '<%=cblTranDescs.ClientID%>' + '  input:checkbox').each(function () {
                count = count + 1;
            });
            for (i = 0; i < count; i++) {
                if ($('#' + '<%=cbAll.ClientID %>').prop('checked') == true) {
                    if ('#' + '<%=cblTranDescs.ClientID%>' + '_' + i) {
                        if (('#' + '<%=cblTranDescs.ClientID%>' + '_' + i).disabled != true)
                            $('#' + '<%=cblTranDescs.ClientID%>' + '_' + i + ':checkbox').prop('checked', true);
                    }
                }
                else {
                    if ('#' + '<%=cblTranDescs.ClientID%>' + '_' + i) {
                        if (('#' + '<%=cblTranDescs.ClientID%>' + '_' + i).disabled != true)
                            $('#' + '<%=cblTranDescs.ClientID%>' + '_' + i + ':checkbox').prop('checked', false);
                    }
                }
            }
            
        }



        function UnCheckAll() {
            var flag = 0;
            var count = 0;
            $('#' + '<%=cblTranDescs.ClientID%>' + '  input:checkbox').each(function () {
                count = count + 1;
            });
            for (i = 0; i < count; i++) {
                if ('#' + '<%=cblTranDescs.ClientID%>' + '_' + i) {
                    if ($('#' + '<%=cblTranDescs.ClientID%>' + '_' + i).prop('checked') == true) {
                        flag = flag + 1;
                    }
                }
            }
            if (flag == count)
                $('#' + '<%=cbAll.ClientID %>' + ':checkbox').prop('checked', true);
            else
                $('#' + '<%=cbAll.ClientID %>' + ':checkbox').prop('checked', false);
            
        }


    </script>



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
    max-height:500px;
}

.MaxgvHelpHeight {
    max-height:300px;
}
    .ProcessingBackground {
                        position: fixed;
    z-index: 100002;
    height: 100%;
    width: 100%;
    left: 0;
    top: 0;
    background-color: Black;
    /*-moz-opacity: 0.8;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;*/
    background: rgba(64, 64, 64, 0.5)
        }


        .AjaxLoader1 {
            background: white url('https://bids.northside.local/Images/Pacman-1.1s-75px.gif') no-repeat center center;
            height:100px;

        }

        .AjaxLoader2 {
            background: white url('https://bids.northside.local/Images/Pacman-2.2s-75px.gif') no-repeat center center;
            height:100px;
        }

 .ProcessingPopupPanel {
 z-index: 100003;
    margin: 300px auto;
    padding: 10px;
    width: 130px;  
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;

    border-radius:8px;
    -moz-border-radius: 8px;
        -webkit-border-radius: 8px;
        -khtml-border-radius: 8px;
        border-color:#4A8fd2;
        background-color:white;
        border-width:3px;
        border-style:double;
        align-content:center;
        
          }


      .TextAreaGeneral {
         border-radius:4px;
         -moz-border-radius: 4px;
        -webkit-border-radius: 4px;
        -khtml-border-radius: 4px;
         height:22px;
         width:100px;
         border-width:0px;
         padding:5px;
      
     }
           .TextAreaGeneral2 {
         border-radius:4px;
         -moz-border-radius: 4px;
        -webkit-border-radius: 4px;
        -khtml-border-radius: 4px;
         height:22px;
         width:100px;
         border-width:1px;
         border-color:#004797;
         padding:5px;
      
     }
            


         .GridViewFocus {

         padding:2px;
         font-size:12px;
         cursor:pointer;
         border-color:#004797;
         border-width:1px;
         border-style:solid;
         

      
     }

         
          .ButtonGeneral {
         border-radius:4px;
         -moz-border-radius: 4px;
        -webkit-border-radius: 4px;
        -khtml-border-radius: 4px;
         height:34px;   
         width:100px;      
         border-width:2px;
         border-style:outset; 
         background-color:  #dcdcdc;   
         border-color:#808080; 
     }
          .ButtonGeneral:hover{
          background-color:  #808080; 
          border-color:#444444; 
          }

         .ButtonGeneral:disabled{
              background-color:  #efefef; 
              border-color:#dcdcdc; 
              border-style:solid;
          }

          .ButtonGeneral:disabled:hover{
              background-color:  #efefef; 
              border-color:#dcdcdc; 
              
          }
      
               .GridViewFocus2 th{
         padding:5px;
         font-size:12px;
         border-color:#004797;
         border-width:1px;
         border-style:solid;
         
     }

     .GridViewFocus2 td {
         padding:5px;
         font-size:12px;
         border-color:#004797;
         border-width:1px;
         border-style:solid;

         

     }

     
.PnlDesign
        {
            border: solid 1px #000000;
            max-height: 300px;
            width: 230px;
            overflow-y: scroll;
            background-color: white;
            font-size: 15px;
            font-family: Arial;
            width: 450px;
        }
        .txtbox
        {
            background-image: url(img/drpdwn.png);
            background-position: right center;
            background-repeat: no-repeat;
            cursor: pointer;
            cursor: hand;
            background-size: 20px 30px;
        }

        .chkCount option:hover {
            background-color:green;
        }



        </style>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel runat="server" ID="upWholeThing">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" CssClass="hidden">

                <asp:Label runat="server" ID="HideGalen" CssClass="hidden"></asp:Label>
                <asp:Label runat="server" ID="HideMQ" CssClass="hidden"></asp:Label>
                <asp:Label runat="server" ID="HideSTAR" CssClass="hidden"></asp:Label>
                <asp:Label runat="server" ID="HideOther" CssClass="hidden"></asp:Label>
                <asp:Label runat="server" ID="RememberedStartDate"></asp:Label>
                <asp:Label runat="server" ID="RememberedEndDate"></asp:Label>
                <asp:Label runat="server" ID="RememberedText"></asp:Label>
                <asp:Label runat="server" ID="RememberedCheck"></asp:Label>
                <asp:Label runat="server" ID="MainMap"></asp:Label>
                <asp:Label runat="server" ID="MainDir"></asp:Label>
                <asp:Label runat="server" ID="SummaryMap"></asp:Label>
                <asp:Label runat="server" ID="SummaryDir"></asp:Label>
                <asp:Label runat="server" ID="AdminMap"></asp:Label>
                <asp:Label runat="server" ID="AdminDir"></asp:Label>

            </asp:Panel>

            <cc1:TabContainer ID="tcMQAllocation" runat="server"
                ActiveTabIndex="1" UseVerticalStripPlacement="False" Width="1151px">

                <cc1:TabPanel runat="server" Visible="false" HeaderText="User Administration" ID="tpUserAdmin">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" Visible="false" AutoPostBack="true" ID="ddlUservsLocation" CssClass="TAform-control" Width="200px">                            
                            <asp:ListItem Text="Manage User Access" Value="User" Selected="True"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:Panel runat="server" ID="pnlManageUsers" Visible="true">

                            <br />

                            <asp:Panel CssClass="MaxPanel" BackColor="#CBE3FB" ScrollBars="Auto" runat="server" ID="pnlNewUserSrch" Width="1000px">
                                <asp:Table runat="server" >
                                    <asp:TableRow>
                                        <asp:TableCell rowspan="10" Width="5px"></asp:TableCell>
                                        <asp:TableCell Width="150px" ColumnSpan="2">
                                                        Find New User:
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" Width="200px" CssClass="TextAreaGeneral" ID="txtNewUserSearch" Placeholder="(Search Name)" ></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button runat="server" ID="btnNewUserSearch" CssClass="ButtonGeneral" Width="150px" Text="Search" />
                                        </asp:TableCell>
                                     
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                     
                                    <asp:TableRow>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                        <asp:TableCell ColumnSpan="10" VerticalAlign="Middle">
                                            <asp:Panel runat="server" ScrollBars="Auto" CssClass="MaxgvHelpHeight">
                                                <asp:RadioButtonList Visible="true" runat="server" ID="rblNewUserSearchResults" AppendDataBoundItems="false">
                                                </asp:RadioButtonList>
                                            </asp:Panel>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="10" VerticalAlign="Middle">
                                            <asp:Button CssClass="ButtonGeneral" runat="server" Width="250px" ID="btnAddUser" Text="Add Selected User" />
                                        </asp:TableCell>
                                    </asp:TableRow>
    
                                </asp:Table>
                            </asp:Panel>
                            <br />
                            Search Users:
                            <asp:TextBox runat="server" CssClass="TextAreaGeneral2" oninput="filter()" onkeyup="filter()" ID="txtUserSearch" Width="200px"></asp:TextBox>
                         
              <asp:Panel
                  ID="Panel2" runat="server" ScrollBars="Auto" CssClass="MaxPanel">
                  <asp:GridView ID="gvUserAccess" runat="server" AutoGenerateColumns="False"
                      CellPadding="4" BorderColor="Black" BackColor="White" DataKeyNames="UserLogin"
                      HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                      HeaderStyle-Wrap="true" ForeColor="Black" CssClass="GridViewFocus2"
                      BorderWidth="1px" AllowSorting="True" PageSize="50"
                      BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                      Font-Size="X-Small">
                      <AlternatingRowStyle BackColor="#CBE3FB" />
                      <Columns>
                          <asp:BoundField HeaderText="UserLogin" DataField="UserLogin" SortExpression="UserLogin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField HeaderText="Employee Number" DataField="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField HeaderText="First Name" DataField="FirstName" SortExpression="FirstName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField HeaderText="Last Name" DataField="LastName" SortExpression="LastName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField HeaderText="Email Address" DataField="EmailAddress" SortExpression="EmailAddress" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField HeaderText="Dept" DataField="Dept" SortExpression="Dept" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    
                          <asp:TemplateField HeaderText="Galen Access" ItemStyle-Width="100" SortExpression="Galen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                              <ItemTemplate>
                                  <asp:Label ID="lblGalenAccess" runat="server" Visible="false" Text='<%# Eval("Galen")%>'></asp:Label>
                                  <asp:DropDownList runat="server" ID="ddlGalenAcces" AutoPostBack="true" CssClass="TextAreaGeneral2" Height="30px"
                                      OnSelectedIndexChanged="ddlGalenAccess_SelectedIndexChanged1">
                                  </asp:DropDownList>                                  
                              </ItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="MQ Access" ItemStyle-Width="100" SortExpression="MQ" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                              <ItemTemplate>
                                  <asp:Label ID="lblMQAccess" runat="server" Visible="false" Text='<%# Eval("MQ")%>'></asp:Label>
                                  <asp:DropDownList runat="server" ID="ddlMQAcces" AutoPostBack="true" CssClass="TextAreaGeneral2" Height="30px"
                                      OnSelectedIndexChanged="ddlMQAccess_SelectedIndexChanged1">
                                  </asp:DropDownList>
                              </ItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="STAR Access" ItemStyle-Width="100" SortExpression="STAR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                              <ItemTemplate>
                                  <asp:Label ID="lblSTARAccess" runat="server" Visible="false" Text='<%# Eval("STAR")%>'></asp:Label>
                                  <asp:DropDownList runat="server" ID="ddlSTARAcces" AutoPostBack="true" CssClass="TextAreaGeneral2" Height="30px"
                                      OnSelectedIndexChanged="ddlSTARAccess_SelectedIndexChanged1">
                                  </asp:DropDownList>
                              </ItemTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Other Access" ItemStyle-Width="100" SortExpression="Other" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                              <ItemTemplate>
                                  <asp:Label ID="lblOtherAccess" runat="server" Visible="false" Text='<%# Eval("Other")%>'></asp:Label>
                                  <asp:DropDownList runat="server" ID="ddlOtherAcces" AutoPostBack="true" CssClass="TextAreaGeneral2" Height="30px"
                                      OnSelectedIndexChanged="ddlOtherAccess_SelectedIndexChanged1">

                                  </asp:DropDownList>
                              </ItemTemplate>
                          </asp:TemplateField>


                      </Columns>

                  </asp:GridView>
              </asp:Panel>

          
                        </asp:Panel>

                    </ContentTemplate>





                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Allocation" ID="tpMQAllocation">
                    <ContentTemplate>


                        <br />

                        <asp:Panel runat="server" ScrollBars="Auto">
                            <asp:Table runat="server" BackColor="#CBE3FB" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow Visible="true">
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell Width="130px" ForeColor="#003060">
                         Date Range:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="5px"></asp:TableCell>
                                    <asp:TableCell Width="85px">
                                        <asp:TextBox runat="server" CssClass="TextAreaGeneral" Width="80px" ID="txtStartRange"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calextSubEff"
                                            runat="server" TargetControlID="txtStartRange" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                    </asp:TableCell>
                                    <asp:TableCell Width="2px">-</asp:TableCell>
                                    <asp:TableCell Width="85px">
                                        <asp:TextBox runat="server" CssClass="TextAreaGeneral" Width="80px" ID="txtEndRange"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server" TargetControlID="txtEndRange" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                    </asp:TableCell>
                                    <asp:TableCell RowSpan="10" Width="10px" VerticalAlign="Top" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell RowSpan="10" VerticalAlign="Top" HorizontalAlign="Center">
                                        <asp:Panel
                                            ID="Panel1" runat="server" ScrollBars="Auto" CssClass="MaxPanel">
                                            <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" BorderColor="Black" BackColor="White"
                                                HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" CssClass="GridViewFocus2"
                                                BorderWidth="1px" AllowSorting="True" PageSize="50"
                                                BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                                Font-Size="X-Small">
                                                <AlternatingRowStyle BackColor="#CBE3FB" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="As Of Date" DataField="AsOfDate" DataFormatString="{0:d}" SortExpression="AsOfDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Total Net Amount" DataField="NetAmount" SortExpression="NetAmount" DataFormatString="{0:0,0.00}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:TemplateField HeaderText="Total Galen Claimed" ItemStyle-Width="200" SortExpression="GalenClaimed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGalenClaimed" Visible='<%# Eval("GalenVision")%>' runat="server" Text='<%# Eval("GalenClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total MQ Claimed" ItemStyle-Width="200" SortExpression="MedquestClaimed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMQClaimed" Visible='<%# Eval("MedquestVision")%>' runat="server" Text='<%# Eval("MedquestClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total STAR Claimed" ItemStyle-Width="200" SortExpression="STARClaimed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTARClaimed" Visible='<%# Eval("STARVision")%>' runat="server" Text='<%# Eval("STARClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Other Claimed" ItemStyle-Width="200" SortExpression="OtherClaimed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOtherClaimed" Visible='<%# Eval("OtherVision")%>' runat="server" Text='<%# Eval("OtherClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Unclaimed" SortExpression="Unclaimed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnclaimed" runat="server" Text='<%# Eval("Unclaimed", "{0:0,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </asp:Panel>

                                    </asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell><asp:TableHeaderCell ForeColor="#003060">
                         Text Search:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="4">
                                        <asp:TextBox runat="server" CssClass="TextAreaGeneral" Width="230px" ID="txtSearchValue"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell><asp:TableHeaderCell ForeColor="#003060">
                         Tran Desc(s):
                                    </asp:TableHeaderCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="4">


                                        <asp:TextBox ID="txtTranDescs" placeholder="(All Tran Descs Included)" runat="server" CssClass="TextAreaGeneral2" ReadOnly="true"
                                            Height="28px" Width="230px" Style="margin-bottom: auto; text-align: center; cursor: pointer;"></asp:TextBox>
                                        <asp:Panel ID="PnlTranDescs" runat="server" CssClass="PnlDesign" Style="" Width="230px">
                                            <asp:CheckBox ID="cbAll" AutoPostBack="true" runat="server" Text="Select All" Font-Size="X-Small" onclick="CheckAll();" Width="230px" Checked="true" />
                                            <asp:CheckBoxList ID="cblTranDescs" AutoPostBack="true" CssClass="chkCount" Font-Size="X-Small" runat="server" onclick="UnCheckAll();" Width="230px">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                        <cc1:PopupControlExtender ID="PceSelectTranDesc" runat="server" TargetControlID="txtTranDescs"
                                            PopupControlID="PnlTranDescs" Position="Bottom">
                                        </cc1:PopupControlExtender>


                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="6">
                                        <asp:CheckBox runat="server" ID="cbShowBalanced" Text="Show Claimed Rows" Checked="False" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="ButtonGeneral" Width="150px" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <br />
                            <br />

                            <asp:Panel
                                ID="ScrollPanelMainData" runat="server" ScrollBars="Auto" CssClass="MaxPanel">
                                <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="UniqueActivityID"
                                    CellPadding="4" BorderColor="Black" BackColor="#CBE3FB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Wrap="true" ForeColor="Black"
                                    BorderWidth="1px" AllowSorting="True" PageSize="50"
                                    BorderStyle="Solid" HeaderStyle-BackColor="#4A8fd2"
                                    Font-Size="X-Small">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField HeaderText="" ItemStyle-Width="3px" ControlStyle-Width="3px" />
                                        <asp:BoundField HeaderText="As Of Date" DataField="AsOfDate" DataFormatString="{0:d}" SortExpression="AsOfDate" />
                                        <asp:BoundField HeaderText="Tran Desc" DataField="TranDesc" SortExpression="TranDesc" />
                                        <asp:BoundField HeaderText="Full Desc" DataField="Description" SortExpression="Description" />
                                        <asp:BoundField HeaderText="Customer Ref No" DataField="CustomerRefNo" SortExpression="CustomerRefNo" />
                                        <asp:BoundField HeaderText="Bank Reference" DataField="BankReference" SortExpression="BankReference" />
                                        <asp:BoundField HeaderText="Net Amount" DataField="NetAmount" SortExpression="NetAmount" DataFormatString="{0:0,0.00}" />
                                        <asp:TemplateField HeaderText="Galen Claimed" ItemStyle-Width="200" SortExpression="GalenClaimed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGalenClaimed" Visible='<%# Eval("GalenVision")%>' runat="server" Text='<%# Eval("GalenClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtGalenClaimed" Visible='<%# Eval("GalenPermission")%>' Width="60px" CssClass="TextAreaGeneral2" runat="server" Text='<%# Eval("GalenClaimed", "{0:0,0.00}")%>' ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MQ Claimed" ItemStyle-Width="200" SortExpression="MedquestClaimed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMQClaimed" Visible='<%# Eval("MedquestVision")%>' runat="server" Text='<%# Eval("MedquestClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtMQClaimed" Visible='<%# Eval("MedquestPermission")%>' Width="60px" CssClass="TextAreaGeneral2" runat="server" Text='<%# Eval("MedquestClaimed", "{0:0,0.00}")%>' ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STAR Claimed" ItemStyle-Width="200" SortExpression="STARClaimed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTARClaimed" Visible='<%# Eval("STARVision")%>' runat="server" Text='<%# Eval("STARClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtSTARClaimed" Visible='<%# Eval("STARPermission")%>' Width="60px" CssClass="TextAreaGeneral2" runat="server" Text='<%# Eval("STARClaimed", "{0:0,0.00}")%>' ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Other Claimed" ItemStyle-Width="200" SortExpression="OtherClaimed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOtherClaimed" Visible='<%# Eval("OtherVision")%>' runat="server" Text='<%# Eval("OtherClaimed", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtOtherClaimed" Visible='<%# Eval("OtherPermission")%>' Width="60px" CssClass="TextAreaGeneral2" runat="server" Text='<%# Eval("OtherClaimed", "{0:0,0.00}")%>' ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unclaimed" SortExpression="Unclaimed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnclaimed" runat="server" Text='<%# Eval("Unclaimed", "{0:0,0.00}")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comments" ItemStyle-Width="100" SortExpression="Comments">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("CommentsMini")%>'></asp:Label>
                                                <asp:LinkButton runat="server" Visible='<%#Eval("CommentFlag")%>' CommandArgument='<%# Eval("Comments")%>' ID="lbCommentsPopup" Text="(expand)" OnClick="Popup_Click"></asp:LinkButton>
                                                <asp:TextBox ID="txtComments" Width="100px" CssClass="TextAreaGeneral2" TextMode="MultiLine" Rows="3" Font-Size="X-Small" Wrap="true" runat="server"  Visible="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                            </asp:Panel>
                            <br />
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button runat="server" Font-Size="Medium" ID="btnSubmitRows" Width="200px" Text="Update Rows" CssClass="ButtonGeneral" />
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
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="upWholeThing" ID="upProg_Pacman">
        <ProgressTemplate>
            <div class="ProcessingBackground">
                <div class="ProcessingPopupPanel">

                    <asp:Table Width="100%" BackColor="White" CellPadding="0" CellSpacing="0" runat="server" HorizontalAlign="Center" ForeColor="#003060" Font-Bold="true">
                        <asp:TableRow>
                            <asp:TableCell runat="server" Height="75px" ID="tcAjaxLoader" ColumnSpan="2">
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="17px"></asp:TableCell>
                            <asp:TableCell ColumnSpan="2" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#003060" Font-Bold="true">   
                            Processing
                            </asp:TableCell>

                        </asp:TableRow>
                    </asp:Table>


                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
