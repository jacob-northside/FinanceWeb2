<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PP Activity Report.aspx.vb" Inherits="FinanceWeb.PP_Activity_Report" %>

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


.GridViewRowHeaderStyle > th 
{
    text-align:center;
}
 
.GridViewRowStyle
{
    background-color: #CBE3FB;
    color: #003060;

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
.GridViewAlternatingRowStyle
{
    background-color: #FFFFFF;
    color: #003060;
}
 
.GridViewRowStyle td, .GridViewAlternatingRowStyle td
{
    border: 1px solid #003060;
    align-content:center;
    padding-left:1em;
    padding-right:1em;
    padding-bottom:.5em;
    padding-top:.5em;

}

.MxPanelHeight {
    max-height:600px;
}
 
.CompressedTable {
    border-spacing:0px;
}
.CompressedTable tr th {
    padding:0px;    
} 
.CompressedTable td {
    padding:0px;    
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
.PnlDesign
        {
            border: solid 1px #000000;
            max-height: 300px;
            width: 230px;
            overflow-y: scroll;
            background-color: white;
            font-size: 15px;
            font-family: Arial;
            margin-top:0px;            
        }
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

 .chkCount option:hover {
            background-color:green;
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



</style>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


    <script>


        function CheckAllGroup() {
            var count = 0;
            $('#' + '<%=cblGroup.ClientID%>' + '  input:checkbox').each(function () {
                count = count + 1;
            });
            for (i = 0; i < count; i++) {
                if ($('#' + '<%=cbAllGroups.ClientID %>').prop('checked') == true) {
                    if ('#' + '<%=cblGroup.ClientID%>' + '_' + i) {
                        if (('#' + '<%=cblGroup.ClientID%>' + '_' + i).disabled != true)
                            $('#' + '<%=cblGroup.ClientID%>' + '_' + i + ':checkbox').prop('checked', true);
                    }
                }
                else {
                    if ('#' + '<%=cblGroup.ClientID%>' + '_' + i) {
                        if (('#' + '<%=cblGroup.ClientID%>' + '_' + i).disabled != true)
                            $('#' + '<%=cblGroup.ClientID%>' + '_' + i + ':checkbox').prop('checked', false);
                    }
                }
            }

        }



        function UnCheckAllGroup() {
            var flag = 0;
            var count = 0;
            $('#' + '<%=cblGroup.ClientID%>' + '  input:checkbox').each(function () {
                count = count + 1;
            });
            for (i = 0; i < count; i++) {
                if ('#' + '<%=cblGroup.ClientID%>' + '_' + i) {
                    if ($('#' + '<%=cblGroup.ClientID%>' + '_' + i).prop('checked') == true) {
                        flag = flag + 1;
                    }
                }
            }
            if (flag == count)
                $('#' + '<%=cbAllGroups.ClientID %>' + ':checkbox').prop('checked', true);
            else
                $('#' + '<%=cbAllGroups.ClientID %>' + ':checkbox').prop('checked', false);

        }

        function open_win(id) {

            var url = "https://financeweb.northside.local/Tools/AR/Activity_Report_Attachments/?Detail_ID=" + id;
            myWindow = window.open(url, 'AR Activity Report Attachments', 'height=700,width=620, scrollbars, resizable');
            myWindow.focus();

        }

        function open_ssrs() {

            var url = "http://ssrs.northside.local/Reports/Pages/Report.aspx?ItemPath=%2fFinance%2fAccounting+-+Operational%2fAccounts+Receivable%2fAR%2fAR+Activity+Sheet+Monthly+Summary";
            myWindow = window.open(url, 'AR Activity Reports');
            myWindow.focus();
            
        }

        function open_instructions() {

            var url = "https://financeweb.northside.local/Tools/AR/Activity_Report_Instructions";
            myWindow = window.open(url, 'AR Activity Report Instructions', 'height=900,width=1200, scrollbars, resizable');
            myWindow.focus();

        }

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=ScrollPanel.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=ScrollPanel.ClientID%>').scrollLeft;
                yPos = $get('<%=ScrollPanel.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=ScrollPanel.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanel.ClientID%>').scrollLeft = xPos;
                $get('<%=ScrollPanel.ClientID%>').scrollTop = yPos;
            }
        }

        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);



        var xPosAdminAssign, yPosAdminAssign;
        
        function BeginRequestHandlerAdminAssign(sender, args) {
            if ($get('<%=ScrollPanelAssignments.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPosAdminAssign = $get('<%=ScrollPanelAssignments.ClientID%>').scrollLeft;
                yPosAdminAssign = $get('<%=ScrollPanelAssignments.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandlerAdminAssign(sender, args) {
            if ($get('<%=ScrollPanelAssignments.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanelAssignments.ClientID%>').scrollLeft = xPosAdminAssign;
                $get('<%=ScrollPanelAssignments.ClientID%>').scrollTop = yPosAdminAssign;
            }
        }

        prm.add_beginRequest(BeginRequestHandlerAdminAssign);
        prm.add_endRequest(EndRequestHandlerAdminAssign);

        function DisableButton() {
            document.getElementById("<%=btnConfirmSrs.ClientID%>").disabled = true;
        }
        window.onbeforeunload = DisableButton;

        function DisableButton2() {
            document.getElementById("<%=btnConfirmSrs2.ClientID%>").disabled = true;
        }
        window.onbeforeunload = DisableButton2;

        function DisableButton3() {
            document.getElementById("<%=btnConfirmSrs3.ClientID%>").disabled = true;
        }
        window.onbeforeunload = DisableButton3;




    </script>



    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>

    <cc1:TabContainer ID="ARActivityReportTabs" runat="server"
        ActiveTabIndex="0" UseVerticalStripPlacement="false">
        <cc1:TabPanel runat="server" HeaderText="Activity Report Data Entry" ID="tpARData">
            <ContentTemplate>
        

                        <asp:Panel runat="server" ID="hiddenThings" Visible="false">
                        <asp:Label runat="server" ID="ARDetailmap" ></asp:Label>
                        <asp:Label runat="server" ID="ARDetaildir"></asp:Label>
                        <asp:Label runat="server" ID="UserName"></asp:Label>
                        <asp:Label runat="server" ID="Admin"></asp:Label>
                        <asp:Label runat="server" ID="Assign"></asp:Label>
                        <asp:Label runat="server" ID="Nettingmap"></asp:Label>
                        <asp:Label runat="server" ID="Nettingdir"></asp:Label>
                        <asp:Label runat="server" ID="Research"></asp:Label>
                        <asp:Label runat="server" ID="usersdir"></asp:Label>
                        <asp:Label runat="server" ID="userssort"></asp:Label>
                        <asp:Label runat="server" ID="Developer"></asp:Label>
                            <asp:Label runat="server" ID="Mimic"></asp:Label>
                        </asp:Panel>

                        <asp:Panel runat="server" BorderColor="#003060"  >
                            <asp:Table CssClass="CompressedTable" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Button Text="Instructions" runat="server" Font-Size="X-Small" OnClientClick="open_instructions()" />
                                    </asp:TableCell>                                    
                                    <asp:TableCell Width="60px" HorizontalAlign="Right">
                                        <asp:Button Text="Reports" OnClientClick="open_ssrs()" runat="server" Font-Size="X-Small" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
</asp:Panel>
                        <asp:Panel ID="Panel1" runat="server">
                           
                            <asp:Table runat="server" BackColor="#CBE3FB" Width="100%" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow Visible="true">

                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px" ForeColor="Black">
                         <b>Assigned User:</b>
                                    </asp:TableCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList runat="server" ID="ddlARAssignedUser" Height="30px" CssClass="TextAreaGeneral2" Width="200px" ></asp:DropDownList>
                                    </asp:TableCell>
      
                                    <asp:TableCell ColumnSpan="5" RowSpan="3">
                                        <asp:Table runat="server" HorizontalAlign="Center" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow >
               
                                                <asp:TableCell VerticalAlign="Top">
                                                   <b>Category:</b><br />
                                                    <asp:CheckBoxList Width="175px" runat="server" ID="cblARCategory" ></asp:CheckBoxList>
                                                </asp:TableCell>                                               
                                                <asp:TableCell VerticalAlign="Top">
                                                    <b>Status:</b><br />
                                                    <asp:CheckBoxList Width="125px" runat="server" ID="cblARCategoryStatus"></asp:CheckBoxList>
                                                    <br /><br />
                                                    <b>Practice(s):</b><br />
                                                    <asp:TextBox ID="txtGroupNameDisplay" placeholder="(All Practices Included)" runat="server" CssClass="TextAreaGeneral2" ReadOnly="true"
                                                        Height="28px" Width="220px" Style="margin-bottom: auto; text-align: center; cursor: pointer;"></asp:TextBox>
                                                    <div style="position: absolute; white-space: nowrap;">
                                                        <asp:Panel ID="pnlcbGroupNames" runat="server" CssClass="PnlDesign" Style="" Width="230px">
                                                            <asp:CheckBox ID="cbAllGroups" runat="server" AutoPostBack="true" Text="Select All" Font-Size="X-Small" onclick="CheckAllGroup();" Width="230px" Checked="true" />
                                                            <asp:CheckBoxList ID="cblGroup" CssClass="chkCount" AutoPostBack="true" Font-Size="X-Small" runat="server" onclick="UnCheckAllGroup();" Width="230px">                                                             
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                        <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="txtGroupNameDisplay"
                                                            PopupControlID="pnlcbGroupNames" Position="Bottom">
                                                        </cc1:PopupControlExtender>
                                                    </div>                                        
                                                   
                                                </asp:TableCell>
                                                <asp:TableCell VerticalAlign="Top">
                                                    <b>Show Rows with: </b>
                                                    <asp:CheckBoxList Width="150px" ID="cblARBalancedRows" runat="server"  >
                                                        <asp:ListItem Text="Bank Variance" Value="Bank" Selected="True" ></asp:ListItem>
                                                        <asp:ListItem Text="STAR Variance" Value="STAR" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Fully Balanced" Value="Balanced" Selected="False"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </asp:TableCell>
                                           
                                            </asp:TableRow>

                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell Width="200px">
                        <b> Activity Report For:</b>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtARDate" runat="server" CssClass="TextAreaGeneral2" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server" TargetControlID="txtARDate"></cc1:CalendarExtender>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>                               
                                <asp:TableRow>
                                    <asp:TableCell Height="20px">

                                    </asp:TableCell>
                                    <asp:TableCell>General Search:</asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtSrch" CssClass="TextAreaGeneral2" Width="200px"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="2px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="10" >
                                        <asp:Button runat="server" ID="btn_ActivityReportSearch" CssClass="ButtonGeneral" Text="Search" Width="200px" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />

                            <asp:Table runat="server" ID="tblOutOfBalance" BackColor="#CBE3FB" Width="100%" HorizontalAlign="Center" BorderColor="#003060" BorderWidth="1px" >
                                <asp:TableRow>
                                    <asp:TableCell >
                          <asp:Label runat="server" ID="lblOutofBalance" ForeColor="Red" Visible="false" ></asp:Label>
                                    </asp:TableCell>
                          </asp:TableRow></asp:Table>

                            <asp:Panel runat="server" Width="100%" ID="ScrollPanel" HorizontalAlign="Center" ScrollBars="Auto">
                                <asp:GridView ID="gv_AR_MainData" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ActivityID" HeaderStyle-HorizontalAlign="Right"
                                    BorderColor="#003060" BackColor="#CBE3FB" RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px"
                                    HeaderStyle-ForeColor="White" CellPadding="2" CellSpacing="2"
                                    HeaderStyle-Wrap="true" ForeColor="Black"
                                    BorderWidth="1px" AllowSorting="True" AllowPaging="true" PageSize="15"
                                    BorderStyle="Solid" HeaderStyle-VerticalAlign="Top" HeaderStyle-BackColor="#4A8fd2"
                                    Font-Size="X-Small">
                                    <RowStyle CssClass="GridViewRowStyle"/>
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewRowHeaderStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ActivityID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARDetail_ID" runat="server" Text='<%# Eval("ActivityID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Row_ID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblARRow_ID" runat="server" Text='<%# Eval("Row_ID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="DetailID" Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblARDetail_ID" runat="server"   Text='<%# Eval("Detail_ID")%>'></asp:Label>
                </ItemTemplate>
   </asp:TemplateField>
                <asp:TemplateField HeaderText="BaseID"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblARBase_ID" runat="server"   Text='<%# Eval("Base_ID")%>'></asp:Label>
                </ItemTemplate>
           </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Assigned To">
                                            <ItemTemplate>

                                                <asp:Label ID="lblARAssignedUser" runat="server" Visible="false" Text='<%# Eval("AssignedUser")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlARRowAssignedUser" OnSelectedIndexChanged="ddlARRowAssignedUser_SelectedIndexChanged1" Enabled="false" runat="server" AutoPostBack="true"></asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Panel runat="server">
                                                    <asp:LinkButton ID="lbRowHistory" runat="server" Text="History" CommandName="RowHistory" CommandArgument='<%# Bind("ActivityID")%>'></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEditDetailRow" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Bind("ActivityID")%>'></asp:LinkButton>
                                                    <asp:Label ID="lblRowLocked" runat="server" Text='<%# Eval("RowLocked")%>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lbUpdateDetailRow" runat="server" Text="Update" Visible="false" CommandName="UpdateRow" CommandArgument='<%# Bind("ActivityID")%>'></asp:LinkButton>
                                                    <br />
                                                    <asp:LinkButton ID="btnSplitDetailRow" runat="server" Text="Split" Visible="false" CommandName="SplitRow" CommandArgument='<%# Bind("ActivityID")%>'></asp:LinkButton>
                                                    <br />
                                                    <asp:LinkButton ID="btnCancelDetailRow" runat="server" Text="Unlock" CommandName="UnlockRow" CommandArgument='<%# Bind("ActivityID")%>' Visible="false"></asp:LinkButton>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                               <asp:TemplateField HeaderText="Deposit Date" ItemStyle-Width="100" SortExpression="sortDepositDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARDepositDate" runat="server" Text='<%# Eval("DepositDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Practice" ItemStyle-Width="100" SortExpression="Facility">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARPractice" runat="server" Text='<%# Eval("Facility")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Batch #" ItemStyle-Width="75" SortExpression="BankBatchNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARBatchNo" runat="server" Text='<%# Eval("BankBatchNumber")%>'></asp:Label>
                                                <asp:TextBox ID="txtARBatchNo" Width="70px" Font-Size="X-Small" runat="server" Text='<%# Eval("BankBatchNumber")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STAR Batch #" ItemStyle-Width="75" SortExpression="STARBatchNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARSTARBatchNo" runat="server" Text='<%# Eval("STARBatchNumber")%>'></asp:Label>
                                                <asp:TextBox ID="txtARSTARBatchNo" Width="70px" Font-Size="X-Small" runat="server" Text='<%# Eval("STARBatchNumber")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="# Patients" ItemStyle-Width="75" SortExpression="NoPatients">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARNoPatients" runat="server" Text='<%# Eval("NoPatients")%>'></asp:Label>
                                                <asp:TextBox ID="txtARNoPatients" Width="70px" Font-Size="X-Small" runat="server" Text='<%# Eval("NoPatients")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
       
         

                                        <asp:TemplateField HeaderText="Type" ItemStyle-Width="125" SortExpression="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlARType" Enabled ="false" Visible="false" runat="server" Font-Size="X-Small"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtARType" Rows="2" Font-Size="X-Small" Wrap="true" runat="server" Text='<%# Eval("Type")%>' Visible="false"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cash Received" SortExpression="Cash_Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARWF_Cash_Received" runat="server" Text='<%# Eval("Cash_Received", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtARWF_Cash_Received" Width="100px" Font-Size="X-Small" runat="server" Text='<%# Eval("Cash_Received")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AR Posted" SortExpression="AR_Posted">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARARPosted" runat="server" Text='<%# Eval("AR_Posted", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtARARPosted" Width="75px" runat="server" Text='<%# Eval("AR_Posted", "{0:f}")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Misc. Posted" ItemStyle-Width="75" SortExpression="MiscAmt">
                                            <ItemTemplate>
                                                
                                               <asp:CheckBox ID="cbMiscPosted" Enabled="false" AutoPostBack="true" OnCheckedChanged="cbMiscClicked" runat="server" Checked='<%# Eval("MiscChecked")%>' />
                                                <br /><asp:Label ID="lblARMiscPosted" Visible="true" runat="server" Text='<%# Eval("MiscAmt", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtARMiscPosted" Width="100px" Font-Size="X-Small" runat="server" Text='<%# Eval("MiscAmt", "{0:f}")%>' Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblARMR" runat="server" Text='<%# Eval("MiscChecked")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Interest" ItemStyle-Width="75" SortExpression="Interest">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARInterest" runat="server" Text='<%# Eval("Interest", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:TextBox ID="txtARInterest" Width="100px" Font-Size="X-Small" runat="server" Text='<%# Eval("Interest", "{0:f}")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfers" ItemStyle-Width="75" SortExpression="Transfer_Sum">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Transfer_Sum", "{0:0,0.00}")%>' ID="lblTransfers"></asp:Label>
                                                <asp:LinkButton ID="btnRowTransfers" runat="server" Text='<%# Eval("Transfers")%>' CommandName="Transfers" CommandArgument='<%# Bind("ActivityID")%>' Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unresolved" ItemStyle-Width="75" SortExpression="Unresolved">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbARUnresolved" Enabled="false" AutoPostBack="true" OnCheckedChanged="cbUnresolvedClicked" runat="server" Checked='<%# Eval("UnresolvedFlag")%>' />
                                                <asp:Label ID="lblARUnresolved" runat="server" Text='<%# Eval("Unresolved", "{0:0,0.00}")%>'></asp:Label>
                                                <asp:Label ID="lblARUR" runat="server" Text ='<%# Eval("UnresolvedFlag")%>' Visible="false" ></asp:Label>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Carry Forward" ItemStyle-Width="75" SortExpression="Carry_Forward">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbARCarryForward" runat="server" Enabled="false" Checked='<%# Eval("Carry_ForwardFlag")%>' />
                                                <asp:Label ID="lblARCarryForward" runat="server" Text='<%# Eval("Carry_Forward", "{0:0,0.00}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- Column 16 --%>
                                        <asp:TemplateField HeaderText="Research" ItemStyle-Width="75" SortExpression="ResearchFlag">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbARResearch" runat="server" Enabled="false" Checked='<%# Eval("ResearchFlag")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comments" ItemStyle-Width="100" SortExpression="CategorizedComments">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARCommentsUnr" runat="server" Text='<%# Eval("CategorizedCommentsMini")%>'></asp:Label>
                                              <asp:LinkButton runat="server" Visible='<%#Eval("CatCommentFlag")%>' CommandArgument='<%# Eval("CategorizedComments")%>' ID="lbARCommentsUnrPopup" Text="(expand)" OnClick="Popup_Click"></asp:LinkButton>
                                                <asp:Label ID="lblARComments" runat="server" Text='<%# Eval("TextboxCommentsMini")%>'></asp:Label>
                                                <asp:LinkButton runat="server" Visible='<%#Eval("TextCommentFlag")%>' CommandArgument='<%# Eval("TextboxComments")%>' ID="lbARCommentsPopup" Text="(expand)" OnClick="Popup_Click"></asp:LinkButton>
                                                <asp:TextBox ID="txtARComments" Width="100px" TextMode="MultiLine" Rows="3" Font-Size="X-Small" Wrap="true" runat="server" Text='<%# Eval("TextboxComments")%>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attachments" ItemStyle-Width="75" SortExpression="Attachments">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imggray" Visible="false" runat="server" ImageUrl="Images/PaperClip.png" Width="11px" />
                                              
                                                <asp:LinkButton ID="btnRowAttachment" runat="server" Text='<%# Eval("Attachments")%>' Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
        <%--                                <asp:TemplateField HeaderText="Misc. GL Acct #s" SortExpression="MGLDD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARMisc_GL_Acct_NosDDL" runat="server" Text='<%# Eval("MGLDD")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblARMisc_GL_Comment" runat="server" Text='<%# Eval("MiscGLComment")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblARMisc_GL_Acct_Nos" runat="server" Text='<%# Eval("MGLDD_Display")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlARRowMisc_GL_Acct_Nos" Font-Size="X-Small" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlMiscGL_SelectedIndexChanged1"></asp:DropDownList>
                                                <asp:TextBox ID="txtARMisc_GL_Acct_Nos" TextMode="MultiLine" Rows="3" Font-Size="X-Small" Wrap="true" runat="server" Text='<%# Eval("MiscGLComment")%>' Visible="false"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>                                     
                                        <asp:TemplateField HeaderText="Category" ItemStyle-Width="50" SortExpression="CashCategory">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARCategory" runat="server" Text='<%# Eval("CashCategory")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlARCategory" Font-Size="X-Small" Visible="false" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="75" SortExpression="DetailStatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARCategoryStatus" runat="server" Text='<%# Eval("DetailStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STAR Variance" ItemStyle-Width="75" SortExpression="STARVariance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARSTARVariance" runat="server" Text='<%# Eval("STARVariance", "{0:0,0.00}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Variance" ItemStyle-Width="75" SortExpression="BankVariance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARBankVariancee" runat="server" Text='<%# Eval("BankVariance", "{0:0,0.00}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

  


                                <br />


                            </asp:Panel>
                            <br />
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button runat="server" Visible="false" UseSubmitBehavior="false" Width="150px" ID="btnNetRows" Font-Size="Medium" Text="Net Rows" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button ID="btnNewCashJournal" UseSubmitBehavior="false" CssClass="ButtonGeneral" runat="server" Width="250px" Text="New Cash Journal" Font-Size="Large" />
                                        
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button runat="server" Visible="false" ID="btnSubmitAllRows" Width="150px" Font-Size="Medium" Text="Update Data" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="200px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button ID="btnRunDailyProcessing" UseSubmitBehavior="false" Width="150px" runat="server" Text="Run Daily Processing" Visible="false" ForeColor="Red" />
                                        
                                    </asp:TableCell>
                                    <asp:TableCell  HorizontalAlign="Center">
                                        <asp:Button ID="btnResetToday" UseSubmitBehavior="false" runat="server" Width="150px" Text="Reset Today's Data" Visible="false" ForeColor="Red" />
                                        </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Button ID="btnHoliday" UseSubmitBehavior="false" runat="server" Width="150px" Text="Northside Holiday" Visible="false" ForeColor="Red" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                                              </asp:TableRow>                 
                            </asp:Table>

                        </asp:Panel>


                        <asp:Label ID="FakeButton2" runat="server" />
                        <asp:Panel ID="Panel2" runat="server" Width="400px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
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

                        <asp:Label ID="FakeButtonSrs" runat="server" />
                        <asp:Panel ID="pnlSrsMPE" runat="server" Width="300px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" ColumnSpan="3" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="srsExplanationLabel" ForeColor="Red" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">

                                        <asp:UpdateProgress runat="server" ID="tstUpdateProgress">
                                            <ProgressTemplate>
                                                <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='images/PngB.png'" onmouseout="this.src='images/PngA.png'" />                                             
                                                
                                            </ProgressTemplate>

                                        </asp:UpdateProgress>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnConfirmSrs"  UseSubmitBehavior="false" BorderStyle="Outset" BorderWidth="2px" ForeColor="Red" runat="server" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancelSrs" BorderStyle="Outset" BorderWidth="2px" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeSerious" runat="server"
                            TargetControlID="FakeButtonSrs"
                            PopupControlID="pnlSrsMPE"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                    <asp:Label ID="FakeButtonSerious2" runat="server" />
                        <asp:Panel ID="pnlSrsMPE2" runat="server" Width="300px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" ColumnSpan="3" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="srsExplanationLabel2" ForeColor="Red" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">

                                        <asp:UpdateProgress runat="server" ID="UpdateProgress1">
                                            <ProgressTemplate>
                                                <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='images/PngB.png'" onmouseout="this.src='images/PngA.png'" />

                                            </ProgressTemplate>

                                        </asp:UpdateProgress>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnConfirmSrs2" UseSubmitBehavior="false" BorderStyle="Outset" BorderWidth="2px" ForeColor="Red" runat="server" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancelSrs2" BorderStyle="Outset" BorderWidth="2px" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />

                       <cc1:ModalPopupExtender ID="mpeSerious2" runat="server"
                           TargetControlID="FakeButtonSerious2"
                           PopupControlID="pnlSrsMPE2"
                           DropShadow="true">
                        </cc1:ModalPopupExtender>

                        <asp:Label ID="FakeButtonSerious3" runat="server" />
                        <asp:Panel ID="pnlSrsMPE3" runat="server" Width="300px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" ColumnSpan="3" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="srsExplanationLabel3" ForeColor="Red" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">

                                        <asp:UpdateProgress runat="server" ID="UpdateProgress2">
                                            <ProgressTemplate>
                                                <img src="../AR/Images/PngA.png" id="Pngs" onmouseover="this.src='images/PngB.png'" onmouseout="this.src='images/PngA.png'" />

                                            </ProgressTemplate>

                                        </asp:UpdateProgress>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnConfirmSrs3"  UseSubmitBehavior="false" BorderStyle="Outset" BorderWidth="2px" ForeColor="Red" runat="server" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancelSrs3" BorderStyle="Outset" BorderWidth="2px" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />

                        <cc1:ModalPopupExtender ID="mpeSerious3" runat="server"
                            TargetControlID="FakeButtonSerious3"
                            PopupControlID="pnlSrsMPE3"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                        <asp:Label ID="FakeButtonConfirmNoData" runat="server" />
                        <asp:Panel ID="pnlSrsConfirmNoData" runat="server" Width="300px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" ColumnSpan="3" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="lblExplainNoData" ForeColor="Red" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                     <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button Width="75px" ID="btnConfirmNoData" UseSubmitBehavior="false" BorderStyle="Outset" BorderWidth="2px" ForeColor="Red" runat="server" Text="Yes" />
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button Width="75px" ID="btnCancelNoData" BorderStyle="Outset" BorderWidth="2px" runat="server" Text="No" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeConfirmNoData" runat="server"
                            TargetControlID="FakeButtonConfirmNoData"
                            PopupControlID="pnlSrsConfirmNoData"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>



                        <asp:Label ID="FakeButtonUnresolvedComments" runat="server" />
                            <asp:Panel ID="pnlUnresolvedRows" runat="server" Width="700px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                             <asp:Table runat="server" Width="100%" Height="100%">
                                    <asp:TableRow>
                                        <asp:TableCell Height="20px"><asp:Label runat="server" Visible="false" ID="lblUnresolvedID"></asp:Label> </asp:TableCell></asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                            <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvUnresolved" AutoGenerateColumns="false"
                                                BackColor="#CBE3FB" AllowPaging="false" CellSpacing="5" HeaderStyle-Font-Size="X-Small"
                                                PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5" DataKeyNames="CommentID, Display">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>
                                                    <asp:BoundField DataField="CommentID" HeaderText="CommentID" ItemStyle-CssClass="hidden"
                                                        HeaderStyle-CssClass="hidden"
                                                        SortExpression="QuestionID"></asp:BoundField>
                                                       <asp:TemplateField HeaderText="Reason" SortExpression="Display">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:CheckBox runat="server" ID="cbUnresolvedSelect" Checked ='<%# Bind("Checked")%>' />
                                                                <asp:Label Font-Size="X-Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Display")%>'></asp:Label>
                                                            </asp:Panel>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comment" HeaderStyle-Wrap="true" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label Font-Size="X-Small" Visible="false" ID="lblResponse" runat="server" Text='<%# Bind("Comment")%>'></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox Font-Size="X-Small" ID="txtResponse" Width="95%"                                                                
                                                               runat="server" Text='<%# Bind("Comment")%>' Visible="true"></asp:TextBox>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Height="20px" ColumnSpan="10" HorizontalAlign="Center">
                                            <asp:Label runat="server" ID="lblOTherRequired" Visible="false" Text="Selection of 'Other' requires comment" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </asp:TableCell></asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                            <asp:Button ID="btnOkayUnresolvedComments" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                </asp:Table>
                            </asp:Panel>
                            <br />
                            <cc1:ModalPopupExtender ID="mpeUnresolvedComments" runat="server"
                                TargetControlID="FakeButtonUnresolvedComments"
                                PopupControlID="pnlUnresolvedRows"
                                DropShadow="true">
                            </cc1:ModalPopupExtender>


                        <asp:Label ID="FakeButtonMiscSelections" runat="server" />
                        <asp:Panel ID="pnlMiscRows" runat="server" Width="500px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px">
                                        <asp:Label runat="server" Visible="false" ID="lblMiscID"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvMiscSelections" AutoGenerateColumns="false"
                                            BackColor="#CBE3FB" AllowPaging="false" CellSpacing="5" HeaderStyle-Font-Size="X-Small" BorderColor="#003060" BorderWidth="1px"
                                            PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5" DataKeyNames="Misc_Identity">
                                            <AlternatingRowStyle BackColor="white" />
                                            <Columns>
                                                <asp:BoundField DataField="Misc_Identity" HeaderText="Misc_Identity" ItemStyle-CssClass="hidden"
                                                    HeaderStyle-CssClass="hidden"
                                                    SortExpression="Misc_Identity"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Misc GL Code" SortExpression="Display">
                                                    <ItemTemplate>
                                                        <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                            <asp:CheckBox runat="server" ID="cbUnresolvedSelect" Checked='<%# Bind("Checked")%>' />
                                                            <asp:Label Font-Size="X-Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Display")%>'></asp:Label>
                                                            <asp:TextBox Font-Size="X-Small" Width="100px" ID="txtOtherComment" runat="server" Visible="false" Text='<%# Bind("Comment")%>'></asp:TextBox>
                                                        </asp:Panel>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Wrap="true" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                            <asp:Label Font-Size="X-Small" Visible="false" ID="lblResponse" runat="server" Text='<%# Bind("miscAmt", "{0:0,0.00}")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <asp:TextBox Font-Size="X-Small" ID="txtMiscAmt" Width="95%"
                                                            runat="server" Text='<%# Bind("miscAmt", "{0:0,0.00}")%>' Visible="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px" ColumnSpan="10" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lblMiscRedFlag" Visible="false" Text="Selection of 'Other' requires comment" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnOKMiscCodes" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeMiscSelections" runat="server"
                            TargetControlID="FakeButtonMiscSelections"
                            PopupControlID="pnlMiscRows"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                        

                        <asp:Label ID="FakeTransferButton" runat="server" />
                        <asp:Panel ID="Panel3" runat="server" Width="700px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="5px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label runat="server" ID="lblDetailIDTransfers" Visible="false"></asp:Label>
                                        <asp:GridView ID="gvTransfers" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="ID"
                                            BorderColor="#003060" BackColor="#CBE3FB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="true" ForeColor="Black"
                                            BorderWidth="1px" AllowSorting="False" AllowPaging="False"
                                            BorderStyle="Solid" HeaderStyle-VerticalAlign="Bottom" CellPadding="2" CellSpacing="2" HeaderStyle-BackColor="#4A8fd2"
                                            Font-Size="X-Small">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="TFID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTFID" runat="server" Text='<%# Eval("TFID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TTID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTTID" runat="server" Text='<%# Eval("TTID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transfer From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTFFAC" runat="server" Visible="false" Text='<%# Eval("TFFAC")%>'></asp:Label>
                                                        <asp:DropDownList ID="ddlTFFAC" Width="225px" CssClass="TextAreaGeneral2" Height="30px" Enabled="false" runat="server">                                                          
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transfer To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTTFAC" runat="server" Visible="false" Text='<%# Eval("TTFAC")%>'></asp:Label>
                                                        <asp:DropDownList ID="ddlTTFAC" Width ="225px" CssClass="TextAreaGeneral2" Height="30px" runat="server">
                                                          
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTAmount" runat="server" Text='<%# Eval("Amount")%>' Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTAmount" runat="server" CssClass="TextAreaGeneral2" Height="30px" Width="50px" Text='<%# Eval("Amount")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Editable" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <asp:Button ID="btnAddTransfer" Text="Add Transfer" runat="server" />
                                    </asp:TableCell><asp:TableCell Width="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px" ColumnSpan="10" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lblExplanationTransfer" Visible="false" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnOkayTransfers" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                        
                                        <asp:Button ID="btnCancelTransfers" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeTransfers" runat="server"
                            TargetControlID="FakeTransferButton"
                            PopupControlID="Panel3"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                        <asp:Label ID="FakeSplitButton" runat="server" />
                        <asp:Panel ID="Panel4" runat="server" Width="450px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="5px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label runat="server" ID="lblSplitDetailID" Visible="false"></asp:Label>
                                        <asp:GridView ID="gvSplitRows" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="SplitID"
                                            BorderColor="#003060" BackColor="#CBE3FB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="true" ForeColor="Black"
                                            BorderWidth="1px" AllowSorting="False" AllowPaging="False"
                                            BorderStyle="Solid" HeaderStyle-VerticalAlign="Bottom" CellPadding="2" CellSpacing="2" HeaderStyle-BackColor="#4A8fd2"
                                            Font-Size="X-Small">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Detail_ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSplitDetail_ID" runat="server" Text='<%# Eval("SplitID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSplitRN" runat="server" Text='<%# Eval("RN")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STAR Batch #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSplitBatch_Number" runat="server" Visible="false" Text='<%# Eval("STARBatchNo")%>'></asp:Label>
                                                        <asp:TextBox ID="txtSplitBatch_Number" runat="server" Width="100px" Text='<%# Eval("STARBatchNo")%>' AutoPostBack="true" OnTextChanged="txtSplit_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AR Posted">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSplitWF_Cash_Received" runat="server" Visible="false" Text='<%# Eval("AmountSplit", "{0:f}")%>'></asp:Label>
                                                        <asp:TextBox ID="txtSplitWF_Cash_Received" runat="server" Width="100px" Text='<%# Eval("AmountSplit", "{0:f}")%>'  AutoPostBack="true" OnTextChanged="txtSplitTotal_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        Total: 
               <asp:Label runat="server" ID="lblSplitTotal" ></asp:Label>
                                        <br />
                                        <asp:Label runat="server" ID="lblSplitRemaining" ></asp:Label>
                                        <br />
                                        <asp:Button ID="btnSubmitSplit" Text="Split Rows" runat="server"  />
                                    </asp:TableCell><asp:TableCell Width="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px" ColumnSpan="10" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lblExplanationSplit" Visible="false" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnCancelSplit" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeSplit" runat="server"
                            TargetControlID="FakeSplitButton"
                            PopupControlID="Panel4"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                        <asp:Label ID="lblFakeNetButton" runat="server" />
                        <asp:Panel ID="pnlFakeNet" runat="server" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="900px" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"> </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        Select Facility: 
                                        <asp:DropDownList runat="server" ID="ddlNetRowFacility" AutoPostBack="true"></asp:DropDownList><br />
                                        <asp:Panel runat="server" ScrollBars="Auto" Height="500px" Width="800px">
                                            <asp:GridView ID="gvNettingRows" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="ActivityID" HeaderStyle-HorizontalAlign="Right"
                                                BorderColor="#003060" BackColor="#CBE3FB" RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px"
                                                HeaderStyle-ForeColor="White" CellPadding="2" CellSpacing="2"
                                                HeaderStyle-Wrap="true" ForeColor="Black"
                                                BorderWidth="1px" AllowSorting="True" AllowPaging="false"
                                                BorderStyle="Solid" HeaderStyle-VerticalAlign="Top" HeaderStyle-BackColor="#4A8fd2"
                                                Font-Size="X-Small">
                                                <RowStyle CssClass="GridViewRowStyle" />
                                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                <HeaderStyle CssClass="GridViewRowHeaderStyle" />

                                                <Columns>
                                                    <asp:TemplateField HeaderText="ActivityID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARRow_ID" runat="server" Text='<%# Eval("ActivityID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:CheckBox ID="cbNetRow" runat="server"></asp:CheckBox>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Batch #" ItemStyle-Width="75px" SortExpression="BankBatchNumber">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARBatchNo" Width="75px" runat="server" Text='<%# Eval("BankBatchNumber")%>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="125" SortExpression="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash Received" ItemStyle-Width="75" SortExpression="Cash_Received">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARWF_Cash_Received" runat="server" Text='<%# Eval("Cash_Received")%>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="AR Posted" ItemStyle-Width="75" SortExpression="NumericAR_Posted">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARARPosted" runat="server" Text='<%# Eval("AR_Posted")%>'></asp:Label>
                                                            <asp:TextBox ID="txtARARPosted" runat="server" Text='<%# Eval("AR_Posted")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Comments" ItemStyle-Width="100" SortExpression="TextboxComments">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARComments" runat="server" Text='<%# Eval("TextboxComments")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--              <asp:TemplateField HeaderText="Attachments" ItemStyle-Width="75" SortExpression="Attachments">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imggray" Visible="false" runat="server" ImageUrl="Images/ClipGray.bmp" Width="11px" />
                                                            <asp:ImageButton ID="imgwhite" Visible="false" runat="server" ImageUrl="Images/ClipWhite.bmp" Width="11px" />
                                                            <asp:ImageButton ID="imgblue" Visible="false" runat="server" ImageUrl="Images/ClipLightBlue.bmp" Width="11px" />                                                            
                                                            <asp:LinkButton ID="btnRowAttachment" runat="server" Text='<%# Eval("Attachments")%>' Visible="false"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Deposit Date" ItemStyle-Width="100" SortExpression="sortDepositDate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARDepositDate" runat="server" Text='<%# Eval("DepositDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category" ItemStyle-Width="50" SortExpression="CashCategory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARCategory" runat="server" Text='<%# Eval("CashCategory")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="75" SortExpression="DetailStatus">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARCategoryStatus" runat="server" Text='<%# Eval("DetailStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </asp:Panel>
                                        <br />
                                        Comment:
                                            <asp:TextBox runat="server" ID="txtNettingComment" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnOKNetRows" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Net Selected Rows" />
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancelNetRows" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeNettingRows" runat="server"
                            TargetControlID="lblFakeNetButton"
                            PopupControlID="pnlFakeNet"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>


                     

                        <asp:Label ID="FakeNewCashJournalButton" runat="server" />
                        <asp:Panel ID="Panel5" runat="server" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="400px" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"> </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Table runat="server" CssClass="CompressedTable">
                                            <asp:TableRow>
                                                <asp:TableCell Width="5px"></asp:TableCell>
                                                <asp:TableCell>
Select Facility: 
                                                </asp:TableCell>
                                                <asp:TableCell Width="5px"></asp:TableCell>
                                                <asp:TableCell>
  <asp:DropDownList runat="server" ID="ddlNCJFacility" Width="140px"></asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Height="5px">
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
 Select Type:
                                                </asp:TableCell>
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
 <asp:DropDownList runat="server" ID="ddlNCJType" Width="140px"></asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Height="5px">
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
Enter Deposit Date:
                                                </asp:TableCell>
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
 <asp:TextBox runat="server" ID="txtNCJDepositDate" Width="136px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2"
                                            runat="server" TargetControlID="txtNCJDepositDate"></cc1:CalendarExtender>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Height="5px">
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
 Enter Cash Received:
                                                </asp:TableCell>
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
 <asp:TextBox runat="server" Width="136px" ID="txtNCJCashReceived"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Height="5px"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>                                                    
                                       
                                    </asp:TableCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px" ColumnSpan="10" HorizontalAlign="Center" ID="tclblErrorNCJ">
                                        <asp:Label runat="server" ID="lblErrorNCJ" Visible="false" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnSubmitNewRow" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Submit New Row" />
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancelSubmitNewRow" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeNewCashJournal" runat="server"
                            TargetControlID="FakeNewCashJournalButton"
                            PopupControlID="Panel5"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

     



            </ContentTemplate>


        </cc1:TabPanel>

        <cc1:TabPanel runat="server" HeaderText="Administrative" ID="tpAdministrative" Visible="false">
            <ContentTemplate>




<%--                <asp:UpdatePanel runat="server" ID="UpdatePanelp4">
                    <ContentTemplate>--%>

                        <asp:Table runat="server" BackColor="#CBE3FB" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell Height="5px"></asp:TableCell>
                            </asp:TableRow>
                             <asp:TableHeaderRow>
                                <asp:TableCell>
                    
                                </asp:TableCell>
                                <asp:TableHeaderCell Width="100px" ForeColor="Black"> Manage: </asp:TableHeaderCell>
                                <asp:TableCell></asp:TableCell><asp:TableCell>
                                    <asp:DropDownList runat="server" ID="ddlManageWhat" AutoPostBack="true">
                                        <%--<asp:ListItem Text="Assignments" Value="User_Responsibility"></asp:ListItem>
                                        <asp:ListItem Text="BAI Type Codes" Value="BAITypeCodes_LU"></asp:ListItem>
                                        <asp:ListItem Text="Bank Account Numbers" Value="BankAccountNumber_LU"></asp:ListItem>
                                        <asp:ListItem Text="Cash Journal Types" Value="CashCategory_Type_LU"></asp:ListItem>
                                        <asp:ListItem Text="Lockbox Codes" Value="LockboxCodes_LU"></asp:ListItem>
                                        <asp:ListItem Text="Misc GL Codes" Value="MiscGLCodes_LU"></asp:ListItem>
                                        <asp:ListItem Text="Overrides" Value="Overrides_LU"></asp:ListItem>
                                        <asp:ListItem Text="Unresolved Reasons" Value="Unresolved_LU"></asp:ListItem>
                                        <asp:ListItem Text="Users" Value="Users"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell Height="10px"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                        <br />


                        <asp:Panel ID="pnlAssignments" runat="server" Visible="true">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                        You may use the textbox here to filter the User Assignments grid below.  <br />
                                        If the keyword has any conflicts with existing conflicts, you will see them below, and how they are programmed to be resolved.  
                                        <br />Please note:  Assignments are only processed during Daily Processing.  Creating a new assignment will not automatically assign today's entries.<br />
                                    </asp:TableCell>
                                    </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="150px">
                                    Select User:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                       <asp:DropDownList runat="server" ID="ddlAssignUserSelection"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Select Practice:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="ddlAssignUserPractice">                                            
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Select Column:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="ddlAssignUserColumn">
                                            <asp:ListItem Text="(Select Column)" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Cash Category" Value="Cash Category"></asp:ListItem>
                                            <asp:ListItem Text="Type" Value="Type"></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Keyword:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                       <asp:TextBox runat="server" ID="txtAssignUserKeyword" AutoPostBack="true"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5"><asp:Label runat="server" ID="lblFYIAssignments" Text="If you submit this as a new keyword, this is a sample of how possible conflicts will be assigned:" ></asp:Label> </asp:TableCell>
                                </asp:TableRow>
                                                        <asp:TableRow>
                                 
                                    <asp:TableCell ColumnSpan="5">
                                        <asp:GridView ID="gvAssignUserConflicts" runat="server"
                                            AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                            BorderStyle="Solid" Font-Size="X-Small" HeaderStyle-Font-Size="X-Small" HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="#214B9A"
                                            HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Wrap="true" ForeColor="Black" Width="600px"
                                            BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0">
                                            <AlternatingRowStyle BackColor="white" />

                                            <Columns>                                                                                               

                                                <asp:BoundField DataField="OriginalType" 
                                                    HeaderText="Type from WFAllActivity" 
                                                    >                                        
                                 
                                                </asp:BoundField>

                                                <asp:BoundField DataField="OldKeyword" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Wrap="true"
                                                    HeaderText="Previous Assignment Keyword" ReadOnly="True" SortExpression="OldKeyword"
                                                    >
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />

                                                </asp:BoundField>

                                                <asp:BoundField DataField="CurrentKeyword" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Wrap="true"
                                                    HeaderText="New Assignment Keyword" ReadOnly="True" SortExpression="CurrentKeyword"
                                                    >
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />

                                                </asp:BoundField>

                                                <asp:BoundField DataField="OldAssignedUser" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Wrap="true"
                                                    HeaderText="Previous User Assigned" ReadOnly="True" SortExpression="OldAssignedUser"
                                                    >
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="True" />

                                                </asp:BoundField>
      
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAddAssignment" Text="Add This Assignment" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminActiveAssignments" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>



                            <asp:Panel runat="server" ID="ScrollPanelAssignments" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    
                                    <asp:TableFooterRow>
                                        <asp:TableCell ColumnSpan="5">

                                            <asp:GridView ID="gvAssignments" runat="server" DataKeyNames="ID"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" 
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-Width="10px">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">                                                              
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IDGroup" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Practice" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VarName" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Column" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VarValue" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Keyword" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />                                                        
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Assigned To" >
                                                        <ItemTemplate>                                  
                                                            <asp:Label runat="server" ID="lblAssignedTo" CssClass="hidden" ></asp:Label> 
                                                            <asp:DropDownList ID="ddlAssignedTo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignedUser_SelectedIndexChanged"></asp:DropDownList>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveAssignment" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveAssignation" CommandArgument='<%# Bind("ID")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>
           
                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Panel ID="pnlBAITypeCodes" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                        All rows with these BAI Type Codes (excluding 115, which is Lockbox, and comes through via Lockbox Detail) will be pulled from WF All Activity for all non-PPS Bank Account Numbers<br />
                                        If you are looking to pull data from a PPS Bank Account Number, and cannot do so via an override, you will need to discuss with your Website Administrator.<br />
                                        The Transaction Description currently serves no purpose except to clarify what the BAI Type Code means here.                                           
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="150px">
                                    Select Category:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="ddlAdminBAICategories"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter BAI Type Code:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminBAITypeCodes" AutoPostBack="true"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                  
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter BAI Transaction Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminBAITypeDesc"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Short Description (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminBAIShortDesc"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAdminAddBAICode" Text="Add This Code" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminBAICodes" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>



                            <asp:Panel runat="server" ID="pnlBAIScrollBar" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvBAITypeCodes" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="BAITypeCode"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>                                                  
                                                    <asp:BoundField DataField="BAITypeCode" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="BAI Type Code" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Tran Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblTranDescription" runat="server" Text='<%# Bind("TranDescription")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtTranDescription" Text='<%# Bind("TranDescription")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblTranShortDesc" runat="server" Text='<%# Bind("ShortDescription")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtTranShortDesc" Text='<%# Bind("ShortDescription")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Activity_Category")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:DropDownList ID="ddlCategory" runat="server" Visible="false" ></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveAssignment" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveCode" CommandArgument='<%# Bind("BAITypeCode")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>

                            </asp:Panel>
                            <asp:Panel ID="pnlAdminBankAccountNumbers" runat="server" Visible="false">

                                <asp:Table runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="5">
                                        This is the list of Bank Account Numbers from which we will be pulling Bank (WF All Activity) data.<br />
                                        Any Account with a Description ending in "PPS" will only pull through Overrides -- standard rows, defined by BAI Type Codes, will be ignored.
                                                                                   
                                                                                   
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Width="250px">
                                    Select Facility:
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList runat="server" ID="ddlAdminBANFacilities">
                                                <asp:ListItem Value="(Select Facility)" Text="(Select Facility)"></asp:ListItem>
                                                <asp:ListItem Value="A" Text="Atlanta"></asp:ListItem>
                                                <asp:ListItem Value="C" Text="Cherokee"></asp:ListItem>
                                                <asp:ListItem Value="D" Text="Duluth"></asp:ListItem>
                                                <asp:ListItem Value="F" Text="Forsyth"></asp:ListItem>
                                                <asp:ListItem Value="L" Text="Gwinnett"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                    Enter Bank Account Number:
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtAdminBankAccountNumber"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell>
                                    Enter Bank Account Description Description:
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtBankDescription"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                            <asp:Button runat="server" ID="btnAdminAddBAN" Text="Add This Bank Account Number" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Height="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:CheckBox runat="server" ID="cbAdminBANs" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>


                                <asp:Panel runat="server" ID="pnlScrollBankAdmin" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminBankAcctNos" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="AccountNumber"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="AccountNumber" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Bank Account Number" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Practice Name">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DisplayDescription")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("DisplayDescription")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Practice">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Bind("IDGroup")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:DropDownList ID="ddlFacility" runat="server" Visible="false">                                                               
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveBAN" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveBAN" CommandArgument='<%# Bind("AccountNumber")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="pnlAdminCashJournalTypes" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                        This is the list of Types that can be submitted for Cash Journals.                                                                                   
                                                                                   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="250px">
                                    Enter Type:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAdminCJType" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Full Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminCJDescription"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Short Description (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminCJShort"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAddAdminCJType" Text="Add This Type" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminCJType" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminCJScroll" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminCJTypes" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="Type"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Type" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Type" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Full Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("FullDisplay")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("FullDisplay")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortDesc" runat="server" Text='<%# Bind("ShortDisplay")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortDesc" Text='<%# Bind("ShortDisplay")%>' Visible ="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveType" CommandArgument='<%# Bind("Type")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:Label ID="lblSubmittable" runat="server" Text='<%# Bind("Submittable")%>' Visible="False"></asp:Label>
                                                                <asp:LinkButton ID="btnSubmittableType" runat="server" Text='<%# Bind("Submit")%>' CommandName="ReverseSubmittal" CommandArgument='<%# Bind("Type")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="pnlAdminLockBoxCodes" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                        Rows coming from Lockbox Detail will display their Types with the descriptions corresponding to these Lockbox Numbers.  <br />
                                        The Lockbox Name is not currently being used for anything except for informational purpose.                                                                                 
                                                                                   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="250px">
                                    Enter Lockbox Number:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAdminLockboxCode" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminLockboxType"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Lockbox Name (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminLockboxName"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAdminAddLB" Text="Add This Lockbox Number" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminLockboxTypes" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminLockboxScrolling" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminLBTypes" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="LockboxCode"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="LockboxCode" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Type" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Full Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Display")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("Display")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortDesc" runat="server" Text='<%# Bind("LockboxName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortDesc" Text='<%# Bind("LockboxName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Roll Up">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRollup" runat="server" Text='<%# Bind("Rollup")%>' CommandName="FlipRollup" CommandArgument='<%# Bind("LockboxCode")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveType" CommandArgument='<%# Bind("LockboxCode")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="pnlAdminMiscGLCodes" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">                                                                                                                                                                         
                                        List of Misc GL Codes that can be selected -- Department and SubAcct are not currently being used for anything in the webtool.                           
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="250px">
                                    Enter Department Number (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAdminMiscGL_Department" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter SubAcct (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminMiscGL_SubAcct"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Full Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminMiscGL_FullDesc"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Display Description (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminMiscGL_ShortDesc"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAddAdminMiscGLCode" Text="Add This Misc GL Code" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminMiscActive" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminMiscGLScrolling" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminMiscGL" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="Misc_Identity"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("Department")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDepartment" Text='<%# Bind("Department")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubAcct">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblSubAcct" runat="server" Text='<%# Bind("SubAcct")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtSubAcct" Text='<%# Bind("SubAcct")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Full Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("FullDisplay")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("FullDisplay")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortDesc" runat="server" Text='<%# Bind("DropDownDisplay")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortDesc" Text='<%# Bind("DropDownDisplay")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveType" CommandArgument='<%# Bind("Misc_Identity")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="pnlAdminOverrides" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                        Overrides are used either to change the Category from the Default value, or to pull in rows that would otherwise be ignored.<br />
                                        For now, all Overrides must be submitted through the Web Admin.                                           
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                            </asp:Table>

                            
                        </asp:Panel>

                        <asp:Panel ID="pnlAdminUnresolvedReasons" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                        This is the list of Reasons that that can be selected when marking a row as Unresolved.                                                                                   
                                                                                   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="250px">
                                    Enter Full Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAdminUnresolvedFull" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Short Description (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminUnresolvedShort"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAddUnresolved" Text="Add This Reason" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbUnresolvedActive" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminUnresolvedScrolling" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminUnresolvedReasons" runat="server"
                                                AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="UnresolvedId"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>                                                  
                                                    <asp:TemplateField HeaderText="Full Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("UnresolvedFullName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("UnresolvedFullName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortDesc" runat="server" Text='<%# Bind("UnresolvedDropDownList")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortDesc" Text='<%# Bind("UnresolvedDropDownList")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveType" CommandArgument='<%# Bind("UnresolvedId")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                                               

                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="pnlAdminUsers" runat="server" Visible="false">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">
                                                                                                              
                                                                                   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="250px">
                                    Enter UserLogin:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAdminADUserLogin" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    User Name:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" ID="lblAdminADUserName"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Display Name (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Textbox runat="server" ID="txtAdminADDisplayName"></asp:Textbox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
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
                                                    <asp:TableCell><asp:Button runat="server" ID="btnAdminUsrSrch" Text="Search" /></asp:TableCell>
                                                    <asp:TableCell> <asp:LinkButton runat="server" ID="lbCloseUsrSrch" Font-Size="X-Small" Text="Close Search"></asp:LinkButton></asp:TableCell>
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
                                                    <asp:TableCell Width="5px" ></asp:TableCell>
                                                    <asp:TableCell ColumnSpan="10" VerticalAlign="Middle">
                                                        <asp:Label runat="server" ID="lblAdminUsrResults"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </asp:Panel>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button runat="server" ID="btnAdminAddUser" Text="Add User" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="4"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminUserActives" Checked="false" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminUsersScrolling" ScrollBars="Auto" >
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminUsers" runat="server"
                                                AllowSorting="True" AllowPaging="true" PageSize="15" AutoGenerateColumns="False" BorderColor="Black" HeaderStyle-Font-Size="Smaller"
                                                BorderStyle="Solid"  Font-Size="small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="UserLogin"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="3" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                                        ShowEditButton="true" ShowSelectButton="true" SelectText="">
                                                        <HeaderStyle Width="55px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="UserLogin" HeaderStyle-HorizontalAlign="Left" SortExpression="UserLogin"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="User Login" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UserFullName" HeaderStyle-HorizontalAlign="Left" SortExpression="UserFullName"
                                                        HeaderStyle-Wrap="true"
                                                        HeaderText="Full Name" ReadOnly="True">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Short Name" SortExpression="DisplayName">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("DisplayName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortName" Text='<%# Bind("DisplayName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email Address" SortExpression="EmailAddress">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtEmailAddress" Text='<%# Bind("EmailAddress")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="WebTool Access" SortExpression="Clicky">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnWebToolRights" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RevokeAccess" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Admin Rights" SortExpression="Admin">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnAdminRights" runat="server" Text='<%# Bind("Admin")%>' CommandName="FlipAdmin" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Daily Processing Rights" SortExpression="RunProcessing">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnDailyProcessingRights" runat="server" Text='<%# Bind("RunProcessing")%>' CommandName="FlipDP" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Locking Limit" SortExpression="MultipleLocks">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblLocking" runat="server" Text='<%# Bind("LockLimit")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtLocking" Text='<%# Bind("MultipleLocks")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Netting Rights" SortExpression="Netting">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnNettingRights" runat="server" Text='<%# Bind("Netting")%>' CommandName="FlipNetting" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Researching Rights" SortExpression="Researching">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnResearchingRights" runat="server" Text='<%# Bind("Researching")%>' CommandName="FlipResearch" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assignment Rights" SortExpression="AssignRights">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnAssignRights" runat="server" Text='<%# Bind("AssignRights")%>' CommandName="FlipAssignments" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Permission Grant Rights" SortExpression="AssignRoles">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnPermissionRights" runat="server" Text='<%# Bind("AssignRoles")%>' CommandName="FlipRoles" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reset Day Rights" SortExpression="ResetRights">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnResetRights" runat="server" Text='<%# Bind("ResetRights")%>' CommandName="FlipReset" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Holiday Rights" SortExpression="HolidayRights">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnHolidayRights" runat="server" Text='<%# Bind("HolidayRights")%>' CommandName="FlipHoliday" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
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
                                        <asp:Label ID="explanationlabelAdmin" runat="server"></asp:Label>
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
                        <cc1:ModalPopupExtender ID="mpeAdminPage" runat="server"
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


           <%--         </ContentTemplate>
                </asp:UpdatePanel>--%>


            </ContentTemplate>


        </cc1:TabPanel>


    </cc1:TabContainer>


            </ContentTemplate>
            </asp:UpdatePanel>
                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="updMain" ID="upProg_Pacman"> 
                    <progresstemplate>
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

                    </progresstemplate>
                </asp:UpdateProgress>

</asp:Content>
