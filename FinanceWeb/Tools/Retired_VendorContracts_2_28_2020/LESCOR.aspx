<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LESCOR.aspx.vb" Inherits="FinanceWeb.LESCOR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

    <script>


        function open_win() {


            var url = "https://financeweb.northside.local/Tools/VendorContracts/LESCOR_FAQ.aspx";
            myWindow = window.open(url, 'LESCOR FAQ', 'height=1000,width=850, scrollbars, resizable');
            myWindow.focus();

        }

        <%--      function open_win2() {

            var textbox = document.getElementById('<%= lblApproveContractID.ClientID%>');
            var url = "http://financeweb.northside.local/Tools/VendorContracts/VendorContractAttachments/?ContractID=" + textbox.innerText + "&ContractStatus=0";
            myWindow = window.open(url, 'Vendor Contract Attachments', 'height=700,width=620, scrollbars, resizable');
            myWindow.focus();

        }

        function open_win3() {

            var textbox = document.getElementById('<%= lblLegalContractID.ClientID%>');
            var url = "http://financeweb.northside.local/Tools/VendorContracts/VendorContractAttachments/?ContractID=" + textbox.innerText + "&ContractStatus=0";
            myWindow = window.open(url, 'Vendor Contract Attachments', 'height=700,width=620, scrollbars, resizable');
            myWindow.focus();

        }--%>

    

    </script>



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

.RedLeft {

    border-color:red;
    border-width:1px;
    border-style:solid;
    border-right-style:none;
    
}

.RedRight {

    border-color:red;
    border-width:1px;
    border-style:solid;
    border-left-style:none;
    
}

.td
{
    padding:0;
    margin:0;    
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

 /*input[type=radio] {

     border: 1px solid #e2e2e2;
        background: #fff;
        color: #333;
        font-size: 1.2em;
        margin: 5px 0 6px 0;
        padding: 5px;
        width: 14px;

}

label{ 
    width: auto; display: inline; }*/

    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel runat="server" ID="upWholeThing">
        <ContentTemplate>

            <asp:Panel runat="server" ID="hiddenThings" Visible="false">

                <asp:Label runat="server" ID="sortmap"></asp:Label>
                <asp:Label runat="server" ID="sortunmap"></asp:Label>
                <asp:Label runat="server" ID="mapdir"></asp:Label>
                <asp:Label runat="server" ID="unmapdir"></asp:Label>
                <asp:Label runat="server" ID="Admin"></asp:Label>
                <asp:Label runat="server" ID="searchmap"></asp:Label>
                <asp:Label runat="server" ID="searchdir"></asp:Label>
                <asp:Label runat="server" ID="Developer" Text="0"></asp:Label>
                <asp:Label runat="server" ID="AttachIndex"></asp:Label>
                <asp:Label runat="server" ID="Legal" Text="0"></asp:Label>
                <asp:Label runat="server" ID="Approvaldir"></asp:Label>
                <asp:Label runat="server" ID="Approvalmap"></asp:Label>
                <asp:Label runat="server" ID="Legaldir"></asp:Label>
                <asp:Label runat="server" ID="Legalmap"></asp:Label>
                <asp:Label runat="server" ID="Deptdir"></asp:Label>
                <asp:Label runat="server" ID="Deptmap"></asp:Label>
                <asp:Label runat="server" ID="Userdir"></asp:Label>
                <asp:Label runat="server" ID="Usermap"></asp:Label>
                <asp:Label runat="server" ID="Reviewedmap"></asp:Label>
                <asp:Label runat="server" ID="Revieweddir"></asp:Label>

            </asp:Panel>


            <cc1:TabContainer ID="VendorContracts" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="False" Width="1165px">

                <cc1:TabPanel runat="server" HeaderText="The Welcome Page" ID="tpOpenVendor">
                    <ContentTemplate>
                        <%--  <asp:UpdatePanel runat="server" ID="upOpen">
                       <ContentTemplate>--%>

                        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                            <asp:Table runat="server">

                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <asp:Label runat="server" ID="lblOpenUserLogin" Font-Size="Large" ForeColor="#003060"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <b>LESCOR is used to submit contracts for review by Northside Legal Services.</b><br />
                                        <br />
                                        <i>If you do not have a contract, and need Legal Services to prepare one, please call
                                           <asp:Label runat="server" ID="lblLegalPhoneContact"></asp:Label>.</i>
                                    </asp:TableCell>


                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <asp:Label runat="server" ID="lblOpenSummary"></asp:Label>
                                    </asp:TableCell>

                                    <asp:TableCell Width="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trCBUnVis1">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                           To continue using LESCOR, please confirm the below by clicking the checkbox provided.                                           
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <asp:CheckBox AutoPostBack="true" runat="server" ID="cbAgreements" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCBVis1">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        To submit a new contract, please select the Contract Cost Center:
                                           <asp:DropDownList Width="250px" runat="server" ID="ddlDepartmentNo" AutoPostBack="true"></asp:DropDownList>
                                        <br />
                                        <br />
                                        <i>If you are missing a Cost Center, please contact  
                                           <asp:Label runat="server" ID="lblCostCenterEmail"></asp:Label>.</i>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trCBVis2">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <b>Authorized Users based on Cost Center:</b><br />
                                        <asp:GridView ID="gvHierarchies" runat="server" BorderWidth="1" BorderColor="#003060" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="0"
                                            HeaderStyle-Font-Size="x-Small" AllowSorting="false" Font-Size="Small"
                                            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3">
                                            <AlternatingRowStyle BackColor="white" />
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="10px" />
                                                <asp:BoundField ItemStyle-Width="300px" HeaderText="Role" DataField="RoleMatch" />
                                                <asp:BoundField ItemStyle-Width="300px" HeaderText="Users" DataField="Users" />
                                                <asp:BoundField ItemStyle-Width="10px" />
                                            </Columns>

                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trCBVis5">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell Font-Italic="true">
                                        If you believe the information above is in error, please email the correct information to
                                        <asp:Label runat="server" ID="lblEmailHierarchyContact"></asp:Label>.<br />
                                    </asp:TableCell>

                                    <asp:TableCell Width="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trCBVis3">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                           This submission process will require the following approvals:  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trCBVis4">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <b>Current Approval Requirements:</b>
                                        <asp:GridView ID="gvSubmissionRequirements" runat="server" BorderWidth="1" BorderColor="#003060" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="0"
                                            HeaderStyle-Font-Size="x-Small" AllowSorting="false" Font-Size="Small"
                                            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3">
                                            <AlternatingRowStyle BackColor="white" />
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="10px" />
                                                <asp:BoundField ItemStyle-Width="300px" HeaderText="Contract Price Annual Total" DataField="ImpactRange" />
                                                <asp:BoundField ItemStyle-Width="300px" HeaderText="Authorized Approvals" DataField="ApprovalRequired" />
                                                <asp:BoundField ItemStyle-Width="10px" />
                                            </Columns>

                                        </asp:GridView>

                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow ID="trCBVis6">
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trCBVis7">
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                          <i>Please proceed to one of the tabs at the top.</i> 

                                    </asp:TableCell>
                                    <%--<asp:TableCell BorderColor="Red" BorderStyle="Dashed" BorderWidth="2" ColumnSpan ="5" BackColor="White">
                                           &nbsp;&nbsp;Requestor must refrain from disclosing any comments by Legal Services to any vendor except expressly permitted by Legal Services. 
                                           <br />&nbsp;&nbsp;These comments are privileged and, if disclosed, could undermine Northside's bargaining power. 
                                           <br />&nbsp;&nbsp;This includes comments added to the contract as well as emails containing comments by Legal Services.
                                       </asp:TableCell>--%>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
<asp:LinkButton OnClientClick="open_win()" runat="server" Font-Italic="true" Font-Size="Small">LESCOR FAQ</asp:LinkButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </asp:Panel>
                        <br />



                        <asp:Label ID="FakeButtonWelcomePage" runat="server" />
                        <asp:Panel ID="Panel7" runat="server" Width="400px" BackColor="#6da9e3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="ExplanationLabelWelcome" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnWelcomeOK" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button ID="btnWelcomeConfirm" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Confirm" />

                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell><asp:TableCell>
                                        <asp:Button ID="btnWelcomeCancel" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeWelcomePage" runat="server"
                            TargetControlID="FakeButtonWelcomePage"
                            PopupControlID="Panel7"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>




                    </ContentTemplate>


                    <%-- </asp:UpdatePanel>
               </ContentTemplate>--%>
                </cc1:TabPanel>

                <cc1:TabPanel Visible="false" runat="server" HeaderText="Submit Vendor Contract" ID="tpSubmitContract">
                    <ContentTemplate>
                        <%--<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>--%>

                        <asp:Panel runat="server" ID="pnlSubmitting">
                            <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcContractNameBack1" Width="225px" Font-Bold="true">
                                            <asp:Label runat="server" ID="asteriskContractName" ForeColor="Red" Font-Bold="true"></asp:Label>Contract Name:
                                        </asp:TableCell>

                                        <asp:TableCell ID="tcContractNameBack2">
                                            <asp:TextBox Width="246px" runat="server" ID="txtSubmitContractName"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Width="80px" CssClass="td" HorizontalAlign="Right">
                                            <asp:LinkButton runat="server" Font-Size="X-Small" ID="lbFullReset" Text="Reset Page"></asp:LinkButton>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Width="120px" Font-Bold="true">Date:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label Width="80px" runat="server" ID="lblSubDate"></asp:Label>
                                            <asp:Label Width="80px" runat="server" ID="lblSubmissionContractID" CssClass="hiddenlabel"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <%-- <asp:TableCell Font-Bold="true">Effective Date</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="80px" runat="server" ID="txtSubEffectiveDate" ></asp:TextBox>
                    <cc1:CalendarExtender ID="calextSubEff"
                        runat="server" TargetControlID="txtSubEffectiveDate" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                </asp:TableCell>   --%>
                                        <asp:TableCell Font-Bold="true" ID="tcDesiredLengthBack1">
                                            <asp:Label runat="server" ID="asteriskLengthOfContract" ForeColor="Red" Font-Bold="true"></asp:Label>Desired Length of Contract:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcDesiredLengthBack2">
                                            <asp:TextBox Width="246px" runat="server" ID="txtSubmitContractLength"></asp:TextBox>
                                        </asp:TableCell>

                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Requestor:</asp:TableCell>
                                        <asp:TableCell Width="250px">
                                            <asp:Label Width="150px" runat="server" ID="lblSubRequestor"></asp:Label>
                                            <asp:Label Width="80px" runat="server" ID="lblSubRequestorID" Visible="false"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <%-- <asp:TableCell Font-Bold="true">Expiration Date</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="80px" runat="server" ID="txtSubExpirationDate"></asp:TextBox>
                    <cc1:CalendarExtender ID="calextSubExp"
                        runat="server" TargetControlID="txtSubExpirationDate" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                </asp:TableCell>--%>
                                        <asp:TableCell ID="tcAutoRenewBack1" Width="275px" Font-Bold="true">
                                            <asp:Label runat="server" ID="asteriskAutoRenew" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            Should the Contract Auto-Renew?
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcAutoRenewBack2">
                                            <asp:RadioButtonList runat="server" AutoPostBack="true" RepeatDirection="Horizontal" ID="rblSubAutoRenewal">
                                                <asp:ListItem Value="Yes" Text="Yes"> </asp:ListItem>
                                                <asp:ListItem Value="No" Text="No" Selected="True"> </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcCostCenterBack1" Font-Bold="true"><asp:Label runat="server" Text="Contract Cost Center Number:" ToolTip="Primary Contract Cost Center"></asp:Label></asp:TableCell>
                                        <asp:TableCell ID="tcCostCenterBack2">
                                            <asp:Label Width="250px" runat="server" ID="lblDepartmentNoNew"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcRenewalTermBack1" Font-Bold="true">
                                            <asp:Label runat="server" ID="asteriskDesiredRenewalTerm" ForeColor="Red" Font-Bold="true"></asp:Label>Desired Renewal Term:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcRenewalTermBack2">
                                            <asp:TextBox Width="246px" runat="server" ID="txtSubRenewalTerm" Enabled="false"></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                        <%-- <asp:TableCell Font-Bold="true">Auto Renewal</asp:TableCell>
                <asp:TableCell>
                    <asp:RadioButtonList runat="server" AutoPostBack="true" RepeatDirection="Horizontal" ID="rblSubAutoRenewal">
                        <asp:ListItem Value="Yes" Text="Yes"> </asp:ListItem>
                        <asp:ListItem Value="No" Text="No" Selected="True"> </asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell>--%>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcVendorNameBack1" Font-Bold="true" VerticalAlign="Top">
                                            <asp:Label runat="server" ID="asteriskVendorName" ForeColor="Red" Font-Bold="true"></asp:Label>Vendor Name:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcVendorNameBack2">
                                            <asp:TextBox Width="246px" runat="server" ID="txtSubmitVendorName"></asp:TextBox>

                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcAnnualCostBack1" Font-Bold="true">
                                            <asp:Label runat="server" ID="asteriskContractCost" ForeColor="Red" Font-Bold="true"></asp:Label>Annual Contract Cost:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcAnnualCostBack2">
                                            <asp:DropDownList Width="250px" runat="server" ID="ddlSubmitContractCost"></asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true" ID="tcContractTypeBack1">
                                            <asp:Label runat="server" ID="asteriskContractType" ForeColor="Red" Font-Bold="true"></asp:Label>Contract Type:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcContractTypeBack2">
                                            <asp:DropDownList runat="server" ID="ddlSubContractType" Width="250px"></asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcPurposeBack1" Font-Bold="true">
                                            <asp:Label runat="server" ID="asteriskContractPurpose" ForeColor="Red" Font-Bold="true"></asp:Label>Purpose of the Contract:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcPurposeBack2">
                                            <asp:TextBox Width="246px" TextMode="MultiLine" Rows="3" runat="server" ID="txtSubmitContractPurpose"></asp:TextBox>

                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcContractingPartyBack1" Font-Bold="true">
                                            <asp:Label runat="server" ID="asteriskContractingParty" ForeColor="Red" Font-Bold="true"></asp:Label>Northside Contracting Party:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcContractingPartyBack2">
                                            <asp:DropDownList Width="250px" runat="server" ID="ddlSubmitContractParty" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true" ID="tcExpenseAccountBack1">
                                            <asp:Label runat="server" ID="asteriskExpenseAccount" ForeColor="Red" Font-Bold="true"></asp:Label>Budget Expense Account:
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcExpenseAccountBack2">
                                            <asp:DropDownList runat="server" Width="250px" ID="ddlSubmitContractBudgetAcct"></asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ID="tcSpecifiedPartyBack1">
                                            <asp:Label runat="server" ID="asteriskPleaseSpecify" ForeColor="Red" Font-Bold="true"></asp:Label><asp:Label runat="server" ID="lblSubmitContractPartyPleaseSpecify">Please Specify:</asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell ID="tcSpecifiedPartyBack2">
                                            <asp:TextBox Width="246px" runat="server" ID="txtSubmitContractParty" Visible="false"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>



                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>
                            <br />
                            <asp:Panel runat="server" ID="pnlNormalCountFix" BackColor="#CBE3FB" BorderColor="red" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" Font-Bold="true" ForeColor="Red" VerticalAlign="Middle">
                                            Unless otherwise noted, all information on this page is required.
                        <asp:Label runat="server" ID="lblNormalCountFix" Visible="false"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>

                            <asp:Panel runat="server">
                                <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvSubmissionQuestions" AutoGenerateColumns="false"
                                    BackColor="#CBE3FB" AllowPaging="true" CellSpacing="0" HeaderStyle-Font-Size="X-Small" BorderColor="#003060" BorderWidth="1px"
                                    PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" Width="1145px"
                                    HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="1" DataKeyNames="QuestionID">
                                    <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                        <asp:BoundField HeaderText=""></asp:BoundField>
                                        <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden"
                                            SortExpression="QuestionID"></asp:BoundField>
                                        <%-- <asp:BoundField DataField="ContractID" HeaderText="ContractID" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden"
                                            SortExpression="ContractID"></asp:BoundField>--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Panel runat="server">
                                                    <asp:Label runat="server" ID="asteriskQuestion" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DisplayingNo" HeaderText=""></asp:BoundField>

                                        <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="100%" runat="server">
                                                    <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                        <asp:TableRow>
                                                            <asp:TableCell ColumnSpan="5" Width="1000px">
                                                                <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                            </asp:TableCell>

                                                            <asp:TableCell Width="140px" HorizontalAlign="Right" VerticalAlign="Top">
                                                                <asp:TextBox Font-Size="Small" ID="txtResponse" Width="95%"
                                                                    AutoPostBack="true" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList Font-Size="Small" ID="ddlResponse"
                                                                    Width="95%" AutoPostBack="true" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:RadioButtonList runat="server" ID="rblResponse" OnSelectedIndexChanged="rblResponse_SelectedIndexChanged2"
                                                                    AutoPostBack="true" RepeatDirection="Horizontal">
                                                                </asp:RadioButtonList>
                                                                <asp:CheckBox runat="server" ID="cbResponse" OnCheckedChanged="cbResponse_CheckedChanged1" AutoPostBack="true" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow ID="trResponses">
                                                            <asp:TableCell Width="25px"></asp:TableCell>
                                                            <asp:TableCell VerticalAlign="Top" Width="150px">
                                                                <asp:Label runat="server" ID="lblResponseAsk" Visible="false" Font-Bold="true" ForeColor="Red" Text="Please explain"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell></asp:TableCell>
                                                            <asp:TableCell Width="700px" ColumnSpan="3" VerticalAlign="Top" HorizontalAlign="Right">
                                                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <asp:Label runat="server" ID="lblResponseDatelbl" Visible="false">Date:</asp:Label>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell RowSpan="2" HorizontalAlign="Right">
                                                                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" Width="600px" ID="txtResponseComment" Visible="false"></asp:TextBox>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <asp:TextBox runat="server" ID="txtResponseDate" Width="75" Visible="false"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calExtRespDate"
                                                                                runat="server" TargetControlID="txtResponseDate" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>

                                                </asp:Panel>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--                                     <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                </asp:Panel>           

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Wrap="true" HeaderStyle-Width="75px" >
                                            <ItemTemplate>                                             
                                                <asp:TextBox Font-Size="X-Small" ID="txtResponse" Width="95%"
                                                    
                                                    AutoPostBack="true" runat="server"  Visible="false"></asp:TextBox>
                                                <asp:DropDownList Font-Size="X-Small" ID="ddlResponse"
                                                   
                                                    Width="95%" AutoPostBack="true" runat="server" Visible="false"></asp:DropDownList>
                                                <asp:RadioButtonList runat="server" ID="rblResponse" OnSelectedIndexChanged="rblResponse_SelectedIndexChanged1"
                                                    AutoPostBack="true" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                               
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>          --%>
                                    </Columns>
                                </asp:GridView>

                            </asp:Panel>
                            <br />
                            <asp:Panel runat="server" Visible="false" ID="pnlSpecialCountFix" BackColor="#CBE3FB" BorderColor="red" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center">
                                            <asp:Label runat="server" ID="lblSpecialCountFix" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>

                            <asp:Panel runat="server" BorderColor="#003060" Width="1145px" BackColor="#CBE3FB" BorderWidth="1px">
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell Width="15px"></asp:TableCell>
                                        <asp:TableCell ColumnSpan="5" VerticalAlign="Middle">
                                            Please select and upload the contract, as well as all exhibits and schedules:<br />
                                            <asp:FileUpload ID="fileSubmissionAttachmentInput" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <%-- <asp:Button ID="btnSubmissionAttachmentUpload" Text="Upload" runat="server" />--%>
                                            <%-- <asp:Button ID="btnAsyncUpload" runat="server"
                        Text="Async_Upload" OnClick="Async_Upload_File" />--%>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px">
                    
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>                    
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="5">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="85px" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell ColumnSpan="5" Width="100%">

                                            <asp:GridView ID="gvSubmissionAttachments" runat="server" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="3"
                                                HeaderStyle-Font-Size="small" AllowSorting="false" Font-Size="Small" Width="800px" BorderColor="#003060" BorderWidth="1px"
                                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3" PageSize="20">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="" ControlStyle-Width="1px"></asp:TemplateField>
                                                    <asp:BoundField HeaderText="File Id" DataField="AttachmentID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                    <asp:BoundField HeaderText="ContentType" DataField="ContentType"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                    <asp:BoundField HeaderText="Uploaded Documents" DataField="FileName" ItemStyle-Width="400px" />

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">

                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Eval("downloadtext")%>' OnClick="DownloadSubmissionAttachmentFile"
                                                                CommandArgument='<%# Eval("AttachmentID")%>'></asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkRemove" runat="server" Text='<%# Eval("removetext")%>' OnClick="RemoveSubmissionAttachmentFile"
                                                                CommandArgument='<%# Eval("AttachmentID")%>'></asp:LinkButton>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                </Columns>

                                            </asp:GridView>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:Panel>
                            <asp:Panel runat="server" Width="1145px">

                                <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvSpecialQuestionSubmission" AutoGenerateColumns="false"
                                    BackColor="#CBE3FB" AllowPaging="false" CellSpacing="0" Width="1145px"
                                    Font-Size="Small" ShowHeader="false" CellPadding="1" DataKeyNames="QuestionID" BorderColor="#003060" BorderWidth="1px">
                                    <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:Panel runat="server">
                                                    <asp:Label runat="server" ID="asteriskQuestion" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden"
                                            SortExpression="QuestionID"></asp:BoundField>
                                        <asp:TemplateField HeaderText="" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="100%" runat="server">
                                                    <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                        <asp:TableRow>
                                                            <asp:TableCell ColumnSpan="5" Width="1000px">
                                                                <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                            </asp:TableCell>

                                                            <asp:TableCell Width="140px" HorizontalAlign="Right">
                                                                <asp:TextBox Font-Size="Small" ID="txtResponse" Width="95%"
                                                                    AutoPostBack="true" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList Font-Size="Small" ID="ddlResponse"
                                                                    Width="95%" AutoPostBack="true" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:RadioButtonList Font-Size="Small" runat="server" ID="rblResponse" OnSelectedIndexChanged="rblResponse_SelectedIndexChanged1"
                                                                    AutoPostBack="true" RepeatDirection="Horizontal">
                                                                </asp:RadioButtonList>
                                                                <asp:CheckBox runat="server" ID="cbResponse" OnCheckedChanged="cbResponse_CheckedChanged2" AutoPostBack="true" />

                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow ID="trResponses">
                                                            <asp:TableCell Width="25px"></asp:TableCell>
                                                            <asp:TableCell VerticalAlign="Top" Width="150px">
                                                                <asp:Label runat="server" ID="lblResponseAsk" Visible="false" Font-Bold="true" ForeColor="Red" Text="Please explain"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell></asp:TableCell>
                                                            <asp:TableCell ColumnSpan="3" Width="800px" VerticalAlign="Top" HorizontalAlign="Right">
                                                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <asp:Label runat="server" ID="lblResponseDatelbl">Date:</asp:Label>
                                                                        </asp:TableCell><asp:TableCell></asp:TableCell>
                                                                        <asp:TableCell RowSpan="2">
                                                                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" Width="600px" ID="txtResponseComment" Visible="false"></asp:TextBox>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow>
                                                                        <asp:TableCell>
                                                                            <asp:TextBox runat="server" ID="txtResponseDate" Width="75" Visible="false"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="calExtRespDate"
                                                                                runat="server" TargetControlID="txtResponseDate" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>

                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>

                                                </asp:Panel>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="" HeaderStyle-Wrap="true" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:TextBox Font-Size="Small" ID="txtResponse" Width="95%"
                                AutoPostBack="true" runat="server" Visible="false"></asp:TextBox>
                            <asp:DropDownList Font-Size="Small" ID="ddlResponse"
                                Width="95%" AutoPostBack="true" runat="server" Visible="false">
                            </asp:DropDownList>
                            <asp:RadioButtonList runat="server" ID="rblResponse" OnSelectedIndexChanged="rblResponse_SelectedIndexChanged1"
                                AutoPostBack="true" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                            <br />
                            <asp:TextBox runat="server" ID="txtResponseComment" Visible="false"></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </asp:Panel>

                            <asp:Panel runat="server" ID="Panel3" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center">
                        Enter any additional comments here (optional):
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Center">
                                            <asp:TextBox runat="server" ID="txtSubmissionComments" TextMode="MultiLine" Height="30px" Width="600px"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>



                            <br />
                            <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center">
                    You will review this form and submit your request on the next page.
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <%-- <asp:TableCell HorizontalAlign="Center">
                    <asp:Button runat="server" ID="btnAddAttachment" Text="Add Attachment" OnClientClick="open_win()" />
                    
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>--%><asp:TableCell HorizontalAlign="Center">
                    <asp:Button runat="server" ID="btnSubmitContract" Width="100px" Text="Next" />
                </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlPrinting" Visible="false">
                            <asp:Panel runat="server" BackColor="White" Width="1145px" HorizontalAlign="Center" BorderColor="Red" BorderStyle="Dashed" BorderWidth="2">
                                <asp:TextBox runat="server" ID="testfocus" Width="1145px" Font-Bold="true" Font-Size="Medium" ReadOnly="true" BackColor="Transparent" BorderStyle="None">
                To submit your request, please confirm the following information is correct and then click the Submit Request button below.
                                </asp:TextBox>
                            </asp:Panel>
                            <br />
                            <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Width="225px" Font-Bold="true">Contract Name:</asp:TableCell>

                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubmitContractName"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Width="120px" Font-Bold="true">Date:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label Width="80px" runat="server" ID="lblSubDate2"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>

                                        <asp:TableCell Width="275px" Font-Bold="true">Desired Length of Contract:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label Width="80px" runat="server" ID="lblSubmitContractLength"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Requestor:</asp:TableCell>
                                        <asp:TableCell Width="250px">
                                            <asp:Label Width="150px" runat="server" ID="lblSubRequestor2"></asp:Label>

                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>

                                        <asp:TableCell Font-Bold="true">Should the Contract Auto-Renew?</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubAutoRenewal"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true"><asp:Label runat="server" Text="Contract Cost Center Number:" ToolTip="Primary Contract Cost Center"></asp:Label></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblDepartmentNo"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Desired Renewal Term:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubRenewalTerm" Enabled="false"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true" VerticalAlign="Top">Vendor Name:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblContractName"></asp:Label>

                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Annual Contract Cost:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubmitContractCost"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Contract Type:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubContractType"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Purpose of the Contract:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubmitContractPurpose"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Northside Contracting Party:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubmitContractParty"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" ID="lblSubmitContractParyExplain"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell Font-Bold="true">Budget Expense Account:</asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label runat="server" ID="lblSubmitContractBudgetAcct"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell Width="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Height="10px"></asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>
                            <br />

                            <asp:Panel runat="server" Width="1145px">
                                <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvSubmissionQuestionsPrintScreen" AutoGenerateColumns="false"
                                    BackColor="#f5efe2" AllowPaging="false" CellSpacing="0" HeaderStyle-Font-Size="X-Small"
                                    Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true"
                                    HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="1" DataKeyNames="QuestionID">
                                    <AlternatingRowStyle BackColor="white" />
                                    <Columns>

                                        <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden"
                                            SortExpression="QuestionID"></asp:BoundField>
                                        <asp:BoundField DataField="DisplayingNo" HeaderText="" ItemStyle-Width="10px"></asp:BoundField>

                                        <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell ColumnSpan="5" Width="1000px">
                                                            <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                        </asp:TableCell>

                                                        <asp:TableCell Width="100px" HorizontalAlign="Right">
                                                            <asp:Label Font-Size="Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="trResponses">
                                                        <asp:TableCell Width="25px"></asp:TableCell>
                                                        <asp:TableCell VerticalAlign="Top" Width="150px">
                                                            <asp:Label runat="server" ID="lblResponseAsk" Visible='<%# Bind("ViewComment")%>' Font-Bold="true" ForeColor="Red" Text="Please explain"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell ColumnSpan="3" HorizontalAlign="right" Width="700px">
                                                            <asp:Table Width="100%" HorizontalAlign="Left" runat="server" CellPadding="2" CellSpacing="2">
                                                                <asp:TableRow>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:Label Width="75px" Font-Size="Small" ID="lblSubResponseDate" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerDate", "{0:d}")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:Label Width="600px" Font-Size="Small" ID="lblSubResponseComment" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerComment")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--              <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                </asp:Panel>           

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Wrap="true" HeaderStyle-Width="75px" >
                                            <ItemTemplate>                      
												 <asp:Label Font-Size="X-Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   --%>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                            </asp:Panel>

                            <asp:Panel runat="server" Width="1145px">
                                <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvSpecialQuestionsPrintScreen" AutoGenerateColumns="false"
                                    BackColor="#f5efe2" AllowPaging="false" CellSpacing="0" HeaderStyle-Font-Size="X-Small"
                                    Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" ShowHeader="false"
                                    HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="1" DataKeyNames="QuestionID">
                                    <AlternatingRowStyle BackColor="white" />
                                    <Columns>

                                        <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden"
                                            SortExpression="QuestionID"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="5px"></asp:TableCell>
                                                        <asp:TableCell ColumnSpan="5" Width="1000px">
                                                            <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                        </asp:TableCell>

                                                        <asp:TableCell Width="100px" HorizontalAlign="Right">
                                                            <asp:Label Font-Size="Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="5px"></asp:TableCell>
                                                        <asp:TableCell Width="25px"></asp:TableCell>
                                                        <asp:TableCell VerticalAlign="Top">
                                                            <asp:Label runat="server" ID="lblResponseAsk" Visible='<%# Bind("ViewComment")%>' Font-Bold="true" ForeColor="Red" Text="Please explain"></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell ColumnSpan="3" HorizontalAlign="right" Width="700px">
                                                            <asp:Table Width="100%" HorizontalAlign="Left" runat="server" CellPadding="0" CellSpacing="0">
                                                                <asp:TableRow>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:Label Width="75px" Font-Size="Small" ID="lblSubResponseDate" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerDate", "{0:d}")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:Label Width="600px" Font-Size="Small" ID="lblSubResponseComment" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerComment")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--   <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                            <ItemTemplate>
                                                <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                    <asp:Label Font-Size="X-Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                </asp:Panel>           

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Wrap="true" HeaderStyle-Width="75px" >
                                            <ItemTemplate>                      
												 <asp:Label Font-Size="X-Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
												 <br>
												 <asp:Label runat="server" ID="lblSubResponseComment" Text='<%# Bind("ResponseComment")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>      --%>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </asp:Panel>

                            <asp:Panel runat="server" ID="Panel4" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                <asp:Table runat="server" Width="100%" CellPadding="5" CellSpacing="5">
                                    <asp:TableRow>
                                        <asp:TableCell VerticalAlign="Top">
                        Additional comments:
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Top">
                                            <asp:Label runat="server" ID="lblSubmissionComments" Height="30px" Width="600px"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="5">
                                            <asp:Panel runat="server">

                                                <asp:GridView ID="gvPrintAttachments" runat="server" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="0"
                                                    HeaderStyle-Font-Size="X-Small" AllowSorting="false" Font-Size="Small" Width="600px" BorderColor="#003060" BorderWidth="1px"
                                                    HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3" PageSize="20">
                                                    <AlternatingRowStyle BackColor="white" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="File Id" DataField="AttachmentID" />
                                                        <asp:BoundField HeaderText="File Name" DataField="FileName" />
                                                    </Columns>
                                                </asp:GridView>

                                            </asp:Panel>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:Panel>
                            <br />


                            <br />
                            <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px" CssClass="Printbutton">
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableHeaderRow>
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" ForeColor="Red" Font-Bold="true">
                    Please confirm the information above before clicking Submit.
                                        </asp:TableCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <%--<asp:TableCell HorizontalAlign="Center">
                    <asp:Button runat="server" ID="btnPrintContract" Text="Print Form" CssClass="Printbutton" />
                </asp:TableCell>--%>
                                        <asp:TableCell></asp:TableCell><asp:TableCell HorizontalAlign="Center">
                                            <asp:Button runat="server" ID="btnConfirmContract" Text="Submit Request" CssClass="Printbutton" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="20px"></asp:TableCell><asp:TableCell HorizontalAlign="Center">
                                            <asp:Button runat="server" ID="btnCancelPrint" Text="Edit Form" CssClass="Printbutton" />
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>


                        <asp:Label ID="FakeButton" runat="server" />
                        <asp:Panel ID="Panel1" runat="server" Width="400px" BackColor="#6da9e3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="explantionlabel" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button ID="ConfirmButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Confirm" />

                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell><asp:TableCell>
                                        <asp:Button ID="CancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                            TargetControlID="FakeButton"
                            PopupControlID="Panel1"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>




                    </ContentTemplate>
                    <%--  </asp:UpdatePanel>
                        </ContentTemplate>--%>
                </cc1:TabPanel>

                <cc1:TabPanel runat="server" Visible="false" HeaderText="Approve Contracts" ID="tpPendingContracts">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblApproveContractID" CssClass="hiddenlabel"></asp:Label>
                        <%--     <asp:UpdatePanel runat="server" ID="upPendingMain">
                       <ContentTemplate>--%>

                        <asp:Button runat="server" ID="btnRefreshApprovalPage" Text="Refresh" />
                        <asp:Table runat="server" CellPadding="0" CellSpacing="0">
                            <asp:TableRow>
                                <asp:TableCell VerticalAlign="Top">
                                    Contracts Seeking your Approval:<br />
                                    <asp:Panel runat="server">
                                        <asp:Table runat="server" Width="400px" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow Visible="false">

                                                <asp:TableCell CssClass="td">
                                                    &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lbDisplaySrch" Text="Filter"></asp:LinkButton>

                                                    <asp:Panel runat="server" ID="pnlSrchPanel" Visible="false">
                                                        <asp:Table runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">Submission Date Between</asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                                    &nbsp;&nbsp;<asp:TextBox runat="server" Width="75px" AutoPostBack="true" ID="txtSrchSubStart"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExtSubStart"
                                                                        runat="server" TargetControlID="txtSrchSubStart" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                </asp:TableCell><asp:TableCell>and</asp:TableCell><asp:TableCell>
                                                                    <asp:TextBox runat="server" Width="75px" ID="txtSrchSubEnd" AutoPostBack="true"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExtSubEnd"
                                                                        runat="server" TargetControlID="txtSrchSubEnd" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>

                                                    </asp:Panel>
                                                </asp:TableCell>

                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="center" CssClass="td">

                                                    <asp:Panel runat="server" Width="400px" ID="pnlNoApprovals" Visible="false" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                                                        <br />
                                                        There are no contracts currently waiting on your approval.
                                                           <br />
                                                        <br />
                                                    </asp:Panel>

                                                    <asp:GridView Width="400px" ID="gvWaitingonYou" runat="server" HeaderStyle-BackColor="#6da9e3" HeaderStyle-BorderColor="#003060"
                                                        HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="xx-small" HeaderStyle-BorderWidth="1px"
                                                        RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px" AllowSorting="true" HeaderStyle-HorizontalAlign="Center"
                                                        RowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" DataKeyNames="ContractID"
                                                        HorizontalAlign="Left" Font-Size="Smaller" CssClass="CursorHand" RowStyle-BackColor="#CBE3FB">

                                                        <Columns>
                                                            <asp:CommandField ShowSelectButton="True" ItemStyle-Width="0px" Visible="True" SelectText="" />
                                                            <%--                                                               <asp:TemplateField HeaderText="">
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
                                                            <asp:BoundField DataField="ContractID" HeaderText="ID" ItemStyle-Width=" 10px"
                                                                SortExpression="ContractID"></asp:BoundField>
                                                            <asp:BoundField DataField="DepartmentNo" HeaderText="Dept #"
                                                                SortExpression="DepartmentNo"></asp:BoundField>
                                                            <asp:BoundField DataField="ContractName" HeaderText="Contract Name"
                                                                SortExpression="ContractName"></asp:BoundField>
                                                            <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor"></asp:BoundField>
                                                            <asp:BoundField DataField="DateAdded" HeaderText="Date" DataFormatString="{0:d}"
                                                                SortExpression="DateAdded"></asp:BoundField>
                                                        </Columns>

                                                    </asp:GridView>
                                                    <br />
                                                </asp:TableCell>

                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell>Recently Reviewed Contracts:</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="center" CssClass="td">

                                                    <asp:Panel runat="server" Width="400px" ID="pnlNoReviewedContracts" Visible="false" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                                                        There are no recently reviewed contracts in your Cost Centers.
                                                    </asp:Panel>

                                                    <asp:GridView Width="400px" ID="gvRecentlyReceivedContracts" runat="server" HeaderStyle-BackColor="#6da9e3" HeaderStyle-BorderColor="#003060"
                                                        HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="xx-small" HeaderStyle-BorderWidth="1px"
                                                        RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px" AllowSorting="true" HeaderStyle-HorizontalAlign="Center"
                                                        RowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" DataKeyNames="ContractID"
                                                        HorizontalAlign="Left" Font-Size="Smaller" CssClass="CursorHand" RowStyle-BackColor="#CBE3FB">

                                                        <Columns>
                                                            <asp:CommandField ShowSelectButton="True" ItemStyle-Width="0px" Visible="True" SelectText="" />
                                                            <%--                                                               <asp:TemplateField HeaderText="">
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
                                                            <asp:BoundField DataField="ContractID" HeaderText="ID" ItemStyle-Width=" 10px"
                                                                SortExpression="ContractID"></asp:BoundField>
                                                            <asp:BoundField DataField="DepartmentNo" HeaderText="Dept #"
                                                                SortExpression="DepartmentNo"></asp:BoundField>
                                                            <asp:BoundField DataField="ContractName" HeaderText="Contract Name"
                                                                SortExpression="ContractName"></asp:BoundField>
                                                            <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor"></asp:BoundField>
                                                            <asp:BoundField DataField="DateAdded" HeaderText="Date" DataFormatString="{0:d}"
                                                                SortExpression="DateAdded"></asp:BoundField>
                                                            <asp:BoundField DataField="UserStatus" ItemStyle-CssClass="hiddenlabel" HeaderStyle-CssClass="hiddenlabel"
                                                                SortExpression="UserStatus"></asp:BoundField>
                                                        </Columns>

                                                    </asp:GridView>
                                                    <br />
                                                </asp:TableCell>

                                            </asp:TableRow>
                                        </asp:Table>

                                    </asp:Panel>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Left">

                                    <asp:Panel runat="server" ID="pnlApproval" Visible="false">

                                        <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvApprovalCurrent" AutoGenerateColumns="false" Width="715px"
                                            BackColor="#f5efe2" AllowPaging="false" CellSpacing="0" HeaderStyle-Font-Size="X-Small" BorderWidth="1px" BorderColor="#003060" BorderStyle="Solid"
                                            Font-Size="Small" HeaderStyle-Height="30px" HeaderStyle-Wrap="true" ShowHeader="true"
                                            HeaderStyle-BackColor="White" HeaderStyle-ForeColor="#003060" CellPadding="1">
                                            <AlternatingRowStyle BackColor="white" />
                                            <Columns>

                                                <asp:BoundField DataField="UserName" HeaderText="Approval/Rejections"
                                                    SortExpression="UserName"></asp:BoundField>
                                                <asp:BoundField DataField="Status"
                                                    SortExpression="Status"></asp:BoundField>
                                                <asp:BoundField DataField="ModifyDate" DataFormatString="{0:d}" HeaderText="Date Reviewed"
                                                    SortExpression="ModifyDate"></asp:BoundField>
                                                <asp:BoundField DataField="Comment" HeaderText="Comment"
                                                    SortExpression="Comment"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>

                                        <br />
                                        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="715px">
                                            <asp:Table runat="server">
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell Width="120px">Contract Name:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractName"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Date Submitted:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveDate"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell Width="140px"> Desired Length of Contract:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractLength"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Requestor:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveRequestor"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Should the Contract Auto-Renew?</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveAutoRenewal"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell><asp:Label runat="server" Text="Contract Cost Center Number:" ToolTip="Primary Contract Cost Center"></asp:Label></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveDepartmentNo"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Desired Renewal Term:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveRenewalTerm"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Vendor Name:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveVendorName"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Annual Contract Cost:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractCost"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Contract Type:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractType"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Purpose of the Contract:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractPurpose"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Northside Contracting Party:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractParty"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblApproveContractPartyExplain"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Budget Expense Account:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblApproveContractBudgetAcct"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Height="10px"></asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel runat="server" Width="715px">
                                            <asp:GridView Width="715px" runat="server" ShowHeaderWhenEmpty="false" ID="gvApproveQuestions1" AutoGenerateColumns="false"
                                                BackColor="#f5efe2" AllowPaging="false" CellSpacing="0" HeaderStyle-Font-Size="X-Small"
                                                Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"
                                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="1" DataKeyNames="QuestionID">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>
                                                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                                        HeaderStyle-CssClass="hidden"
                                                        SortExpression="QuestionID"></asp:BoundField>
                                                    <asp:BoundField DataField="DisplayingNo" HeaderText=""></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                                        <ItemTemplate>
                                                            <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                                <asp:TableRow>
                                                                    <asp:TableCell ColumnSpan="5" Width="500px">
                                                                        <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                                    </asp:TableCell>

                                                                    <asp:TableCell Width="100px" HorizontalAlign="Right">
                                                                        <asp:Label Font-Size="Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="25px"></asp:TableCell>
                                                                    <asp:TableCell VerticalAlign="Top">
                                                                        <asp:Label runat="server" ID="lblResponseAsk" Visible='<%# Bind("ViewComment")%>' Text="Please explain"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell></asp:TableCell>
                                                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Top" HorizontalAlign="Right">
                                                                        <asp:Table Width="100%" HorizontalAlign="right" runat="server" CellPadding="0" CellSpacing="0">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell VerticalAlign="Top">
                                                                                    <asp:Label Font-Size="Small" ID="lblSubResponseDate" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerDate", "{0:d}")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell VerticalAlign="Top">
                                                                                    <asp:Label Width="300px" Font-Size="Small" ID="lblSubResponseComment" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerComment")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />
                                        </asp:Panel>

                                        <asp:Panel runat="server" Width="715px">
                                            <asp:GridView Width="715px" runat="server" ShowHeaderWhenEmpty="false" ID="gvApproveQuestions2" AutoGenerateColumns="false"
                                                BackColor="#f5efe2" AllowPaging="false" CellSpacing="0" HeaderStyle-Font-Size="X-Small" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" ShowHeader="false"
                                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="1" DataKeyNames="QuestionID">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>

                                                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                                        HeaderStyle-CssClass="hidden"
                                                        SortExpression="QuestionID"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                                        <ItemTemplate>
                                                            <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                                    <asp:TableCell ColumnSpan="5" Width="500px">
                                                                        <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                                    </asp:TableCell>

                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label Font-Size="Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                                    <asp:TableCell Width="25px"></asp:TableCell>
                                                                    <asp:TableCell VerticalAlign="Top">
                                                                        <asp:Label runat="server" ID="lblResponseAsk" Visible='<%# Bind("ViewComment")%>' Text="Please explain"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell></asp:TableCell>
                                                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Right">

                                                                        <asp:Table Width="100%" HorizontalAlign="Left" runat="server" CellPadding="0" CellSpacing="0">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label Font-Size="Small" ID="lblSubResponseDate" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerDate", "{0:d}")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label Width="300px" Font-Size="Small" ID="lblSubResponseComment" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerComment")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>

                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="Panel2" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="715px">
                                            <asp:Table runat="server" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Right" Font-Bold="true" Width="175px">
                        Requestor comments:
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="500px">
                                                        <asp:Label runat="server" ID="lblApproveRequestorComments"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                                                        <asp:GridView ID="gvApprovalAttachments" runat="server" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="0"
                                                            HeaderStyle-Font-Size="small" AllowSorting="false" Font-Size="Small" Width="600px" BorderColor="#003060" BorderWidth="1px"
                                                            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3" PageSize="20">
                                                            <AlternatingRowStyle BackColor="white" />
                                                            <Columns>

                                                                <asp:BoundField HeaderText="File Id" DataField="AttachmentID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                <asp:BoundField HeaderText="ContentType" DataField="ContentType"
                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                                <asp:BoundField HeaderText="File Name" DataField="FileName" ItemStyle-Width="300px" />

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">

                                                                    <ItemTemplate>

                                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadApprovalAttachmentFile"
                                                                            CommandArgument='<%# Eval("AttachmentID")%>'></asp:LinkButton>

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>


                                                            </Columns>

                                                        </asp:GridView>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </asp:Panel>
                                        <br />


                                        <asp:Panel ID="pnlSubApproval" runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="715px">
                                            <asp:Table runat="server" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                                                        Your comments:
                                                        <asp:TextBox runat="server" ID="txtApproveComments" TextMode="MultiLine" Height="30px" Width="400px"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <%--<asp:TableCell HorizontalAlign="Center">
                                                              
                                                              <asp:Button runat="server" ID="btnApproveAttachments" Text="View Attachments" OnClientClick="open_win2()" />
                                                          </asp:TableCell>--%>
                                                    <asp:TableCell></asp:TableCell><asp:TableCell>
                                                        <asp:Button runat="server" ID="btnApproveContract" Text="Approve Form" />
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell><asp:TableCell>
                                                        <asp:Button runat="server" ID="btnRejectContract" Text="Reject Form" />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </asp:Panel>


                                    </asp:Panel>


                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>



                        <br />



                        <asp:Label ID="FakeButtonApproval" runat="server" />
                        <asp:Panel ID="pnlApprovalMPE" runat="server" Width="400px" BackColor="#6da9e3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="ExplanationLabelApproval" runat="server"></asp:Label>
                                        <asp:Label runat="server" ID="hiddenRejectorApprove" Visible="false"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="Button4" Visible="false" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Button ID="btnConfirmApprovalRejection" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="true" Font-Size="small" Text="Confirm" />

                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell><asp:TableCell HorizontalAlign="Left">
                                        <asp:Button ID="btnCancelApprovalRejection" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="true" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeApprovalPage" runat="server"
                            TargetControlID="FakeButtonApproval"
                            PopupControlID="pnlApprovalMPE"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>




                    </ContentTemplate>
                    <%--     </asp:UpdatePanel>
               </ContentTemplate>--%>
                </cc1:TabPanel>

                <cc1:TabPanel runat="server" Visible="false" HeaderText="Browse Contracts" ID="tpLegalTab">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblLegalContractID" CssClass="hiddenlabel"></asp:Label>
                        <asp:Table runat="server" CellPadding="0" CellSpacing="0" >
                            <asp:TableRow>
                                <asp:TableCell VerticalAlign="Top">
                                    <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                                        <asp:Table runat="server" Width="1145px" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow VerticalAlign="Top">
                                                <asp:TableCell Width="1px" RowSpan="4"></asp:TableCell>
                                                <asp:TableCell Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" RowSpan="4">
                                                    <asp:CheckBox BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#d5eaff" Font-Size="X-Small"  runat="server" ID="cbLegalShowPending" Text="Pending Dept Approval" Checked="true" />
                                                    <asp:CheckBox BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#bbffbb" Font-Size="X-Small"  runat="server" ID="cbLegalShowQueue" Text="Pending Legal Approval" Checked="true" />                                                    
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ffcae4" Font-Size="X-Small" runat="server" ID="cbLegalNoResponse" Text="No Response" Checked="false" />                                                    
                                                    <asp:CheckBox BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#bbffbb" Font-Size="X-Small"  runat="server" ID="cbLegalShowApproved" Text="Approved" Checked="false" />
                                                    <asp:CheckBox BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ff7d7d" Font-Size="X-Small"  runat="server" ID="cbLegalShowRejected" Text="Deleted" Checked="false" />
                                                    <asp:CheckBox BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#eeeeee" Font-Size="X-Small" runat="server" ID="cbLegalShowClosed" Text="Closed" Checked="false" /><br />
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ffcae4" Font-Size="X-Small" runat="server" ID="cbLegalLoadedinMeditract" Text="Loaded" Checked="false" />
                                                    <%-- <asp:LinkButton Font-Size="X-Small" runat="server" ID="lbLegalDisplaySrch" Text="Advanced Search"></asp:LinkButton>--%>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" RowSpan="4" Width="150px" Visible="false" ID="tcLegalOptions">
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ff7d7d" Font-Size="X-Small" runat="server" ID="cbLegalShowQueueH" Text="Legal Queue - High" Checked="true" />
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ffbb77" Font-Size="X-Small" runat="server" ID="cbLegalShowQueueM" Text="Legal Queue - Medium" Checked="true" />
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ffffaa" Font-Size="X-Small" runat="server" ID="cbLegalShowQueueL" Text="Legal Queue - Low" Checked="true" />
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ff7d7d" Font-Size="X-Small" runat="server" ID="cbLegalPreliminary" Text="Preliminary Review" Checked="true" />
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ffbb77" Font-Size="X-Small" runat="server" ID="cbLegalLegal" Text="Legal Review" Checked="true" />
                                                    <asp:CheckBox Visible="false" BorderColor="#003060" BorderWidth="1" Width="160px" Height="20px" BackColor="#ffffaa" Font-Size="X-Small" runat="server" ID="cbLegalNegotiation" Text="Negotiation" Checked="true" />
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                                                    Submission Date Between:                                                              
                       
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="4">
                                                    <asp:TextBox runat="server" Width="75px" AutoPostBack="true" ID="txtLegalSrchSubStart"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="calLegalExtSubStart"
                                                        runat="server" TargetControlID="txtLegalSrchSubStart" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                                    &nbsp;&nbsp;and&nbsp;&nbsp;
                                                               <asp:TextBox runat="server" Width="75px" ID="txtLegalSrchSubEnd" AutoPostBack="true"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="calLegalExtSubEnd"
                                                        runat="server" TargetControlID="txtLegalSrchSubEnd" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                                                </asp:TableCell>

                                                <%-- <asp:TableCell >Contract Name:
                                                           <br />
                                                               <asp:TextBox runat="server" ID="txtLegalContractName" Width="100px" />
                                                           </asp:TableCell>--%>
                                                <%--<asp:TableCell ColumnSpan="3" >Specific Question:
                                                           <br />
                                                               <asp:DropDownList runat="server" AutoPostBack="true" Width="312px" ID="ddlLegalQuestionSearch" />
                                                           </asp:TableCell>
                                                       </asp:TableRow>                                                       
                                                       <asp:TableRow>
                                                           <asp:TableCell></asp:TableCell> <asp:TableCell></asp:TableCell>   <asp:TableCell></asp:TableCell>                                                       
                                                          <asp:TableCell >
                                                               &nbsp;&nbsp;Answer:</asp:TableCell>                                                         
                                                           <asp:TableCell>
                                                               <asp:DropDownList runat="server" Width="125px" ID="ddlLegalQuestionResponse" />
                                                           </asp:TableCell>--%>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell ColumnSpan="2" Width="200px">
                                                    Assigned To:<br />
                                                    <asp:DropDownList Width="200px" runat="server" ID="ddlLegalAssignedTo" AutoPostBack="true" />
                                                </asp:TableCell>
                                                <%--  <asp:TableCell></asp:TableCell>--%>

                                                <%--<asp:TableCell CssClass="td" rowspan="3" Width="800px" >
                                              <asp:Panel runat="server" ID="pnlLegalSrchPanel" >
                                                   <asp:Table runat="server" CellPadding="0" CellSpacing="0">

                                                       <asp:TableRow>
                                                           <asp:TableCell></asp:TableCell>--%>
                                                <asp:TableCell Width="107px">
                                                    Request ID:
                                                           <br />
                                                    <asp:TextBox runat="server" ID="txtLegalContractID" Width="75px" />
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    Contract Cost Center:
                                                           <br />
                                                    <asp:DropDownList runat="server" ID="ddlLegalContractCostCenter" Width="200px" />
                                                </asp:TableCell>
                                                <asp:TableCell></asp:TableCell>

                                               
                                            </asp:TableRow>

                                            <%--  <asp:TableRow>
                                                           <asp:TableCell></asp:TableCell>
                                                           <asp:TableCell >Vendor Name:
                                                           <br />
                                                               <asp:TextBox runat="server" ID="txtLegalVendor" Width="150px" />
                                                           </asp:TableCell>
                                                          <asp:TableCell >Annual Contract Cost:
                                                           <br />
                                                               <asp:DropDownList runat="server" ID="ddlLegalContractCost" Width="150px" />
                                                           </asp:TableCell>
                                                       </asp:TableRow>--%>
                                            <%--<asp:TableRow>
                                                           <asp:TableCell></asp:TableCell>
                                                            <asp:TableCell>Contract Purpose:
                                                           <br />
                                                               <asp:TextBox runat="server" Width="150px" ID="txtLegalContractPurpose" />
                                                           </asp:TableCell>
                                                       </asp:TableRow>--%>

                                            <%--   <asp:TableRow>  <asp:TableCell></asp:TableCell>    <asp:TableCell></asp:TableCell>                                                     
                                                           <asp:TableCell >
                                                               &nbsp;&nbsp;Answer Comment:    </asp:TableCell>                                                         
                                                           <asp:TableCell>                                                       
                                                               <asp:TextBox runat="server" Width="121px" ID="txtLegalQuestionComment" />
                                                           </asp:TableCell>
                                                       </asp:TableRow>--%>

                                            <%--<asp:TableRow>
                                                           <asp:TableCell></asp:TableCell>
                                                           <asp:TableCell >Attachment Name:
                                                           <br />
                                                               <asp:TextBox runat="server" Width="150px" ID="txtLegalContractAttachmentName" />
                                                           </asp:TableCell>
                                                       </asp:TableRow>     --%>
                                            <%--  </asp:Table>



                                               </asp:Panel>                                               
                                           </asp:TableCell>
                                       </asp:TableRow>--%>
                                            <asp:TableRow>
                                                
                                                <asp:TableCell ColumnSpan="2">
                                                    Requestor:
                                                            <br />
                                                    <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlLegalRequestor" Width="200px" />

                                                </asp:TableCell><%--<asp:TableCell></asp:TableCell>--%>
                                                <%--</asp:TableRow>
                                       <asp:TableRow>    --%>

                                                <asp:TableCell ColumnSpan="2">
                                                    Keywords:<br />
                                                    <asp:TextBox Width="300px" runat="server" ID="txtLegalSearchKeywords" />
                                                </asp:TableCell>
                                                <%-- </asp:TableRow>
                                       <asp:TableRow>--%>
                                            </asp:TableRow>
                                           
                                            <asp:TableRow>
                                                <asp:TableCell Height="45px" Width="2px"></asp:TableCell>
                                                <asp:TableCell Width="85px" VerticalAlign="Bottom" HorizontalAlign="Left">
                                                    <asp:Button Width="80px" runat="server" ID="btnLegalAdvSearch" Text="Search" />
                                                </asp:TableCell>
                                                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right">
                                                    <asp:Button Width="80px" runat="server" ID="btnResetSearch" Text="Reset" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Panel runat="server" ID="pnlNoResultsFound" Visible="false" BackColor="#CBE3FB" BorderColor="#003060" BorderWidth="1px" Width="1145px" Font-Bold="true">
                                        <asp:Table runat="server">
                                            <asp:TableRow>
                                                <asp:TableCell></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Width="50px"></asp:TableCell>
                                                <asp:TableCell>
                                            No results found.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Height="10px"></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                    </asp:Panel>

                                    <asp:GridView Width="1145px" ID="gvLegalResults" runat="server" HeaderStyle-BackColor="#6da9e3" HeaderStyle-BorderColor="#003060"
                                        HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="x-small" HeaderStyle-BorderWidth="1px"
                                        RowStyle-BorderColor="#003060" RowStyle-BorderWidth="1px" AllowSorting="true" HeaderStyle-HorizontalAlign="Left"
                                        RowStyle-HorizontalAlign="Left" AutoGenerateColumns="false" DataKeyNames="ContractID" AllowPaging="true" PageSize="15"
                                        HorizontalAlign="Left" Font-Size="Smaller" CssClass="CursorHand">

                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" ItemStyle-Width="0px" Visible="True" SelectText="" />
                                            <%--<asp:TemplateField HeaderText="">
                                           <ItemTemplate>
                                               <asp:Panel runat="server">
                                                   <asp:Image ID="imgDRHR" Visible="false" runat="server" ImageUrl="...FinanceHelpDesk/Images/HourGlassDarkRed.bmp" Width="11px" />
                                                   <asp:Image ID="imgDOHR" Visible="false" runat="server" ImageUrl="...FinanceHelpDesk/Images/HourGlassDarkOrange.bmp" Width="11px" />
                                                   <asp:Image ID="imgDYHR" Visible="false" runat="server" ImageUrl="...FinanceHelpDesk/Images/HourGlassDarkYellow.bmp" Width="11px" />
                                                   <asp:Image ID="imgLRHR" Visible="false" runat="server" ImageUrl="...FinanceHelpDesk/Images/HourGlassLightRed.bmp" Width="11px" />
                                                   <asp:Image ID="imgLOHR" Visible="false" runat="server" ImageUrl="...FinanceHelpDesk/Images/HourGlassLightOrange.bmp" Width="11px" />
                                                   <asp:Image ID="imgLYHR" Visible="false" runat="server" ImageUrl="...FinanceHelpDesk/Images/HourGlassLightYellow.bmp" Width="11px" />
                                                   <asp:Label ID="lblAsterisk" Visible="false" runat="server" Font-Bold="true" ForeColor="#003060">*</asp:Label>
                                               </asp:Panel>
                                           </ItemTemplate>
                                       </asp:TemplateField>--%>
                                            <asp:BoundField DataField="ContractID" HeaderText="ID" ItemStyle-Width=" 10px"
                                                SortExpression="ContractID"></asp:BoundField>
                                            <%-- <asp:BoundField DataField="DepartmentNo" HeaderText="Dept #"
                                           SortExpression="DepartmentNo"></asp:BoundField>--%>
                                            <asp:BoundField DataField="VendorName" HeaderText="Vendor Name"
                                                SortExpression="VendorName"></asp:BoundField>
                                            <asp:BoundField DataField="LegalName" HeaderText="Contract Name" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="LegalName"></asp:BoundField>
                                            <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor"></asp:BoundField>
                                            <asp:BoundField DataField="DateAdded" HeaderText="Date Submitted" DataFormatString="{0:d}"
                                                SortExpression="DateAdded"></asp:BoundField>
                                            <asp:BoundField DataField="LegalUserName" HeaderText="Assigned"
                                                SortExpression="LegalUserName"></asp:BoundField>
                                            <asp:BoundField DataField="DateApproved" HeaderText="Date Approved" DataFormatString="{0:d}"
                                                SortExpression="DateApproved"></asp:BoundField>
                                            <asp:BoundField DataField="Deadline" HeaderText="Deadline" DataFormatString="{0:d}"
                                                SortExpression="Deadline"></asp:BoundField>
                                            <asp:BoundField DataField="DeadlineColor" HeaderStyle-CssClass="hidden"
                                                SortExpression="DeadlineColor" ItemStyle-CssClass="hidden"></asp:BoundField>
                                            <asp:BoundField DataField="UserStatus" HeaderStyle-CssClass="hidden"
                                                SortExpression="UserStatus" ItemStyle-CssClass="hidden"></asp:BoundField>
                                            <asp:BoundField DataField="LegalStatus" HeaderStyle-CssClass="hidden"
                                                SortExpression="LegalStatus" ItemStyle-CssClass="hiddenlabel"></asp:BoundField>
                                            <asp:BoundField DataField="LegalPriority" HeaderStyle-CssClass="hidden"
                                                SortExpression="LegalPriority" ItemStyle-CssClass="hiddenlabel"></asp:BoundField>
                                            <asp:BoundField DataField="LegalFullStatus" HeaderText="Contract Status"
                                                SortExpression="LegalFullStatus"></asp:BoundField>
                                            <asp:BoundField DataField="Approvers" HeaderText="Pending Approval By"
                                                SortExpression="Approvers"></asp:BoundField>
                                        </Columns>

                                    </asp:GridView>

                                </asp:TableCell>

                            </asp:TableRow>


                            <asp:TableRow>
                                <asp:TableCell>

                                    <asp:Panel runat="server" ID="pnlLegalUpdates" Visible="false">

                                        <asp:Panel runat="server" ID="pnlLegalFields" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                            <asp:Table runat="server" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Center" Width="200px">
															Legal Administrator Assigned:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left">
                                                        <asp:DropDownList Width="200px" runat="server" ID="ddlLegalResultAssignedLegal"></asp:DropDownList>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Center" Width="200px">
															Contract Status:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left">
                                                        <asp:DropDownList Width="200px" runat="server" ID="ddlLegalResultContractStatus"></asp:DropDownList>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Center" Width="200px">
															Legal Contract Name:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left">
                                                        <asp:TextBox Width="196px" runat="server" ID="txtLegalResultLegalContractName"></asp:TextBox>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Center" Width="200px">
															Contract Priority:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left">
                                                        <asp:DropDownList Width="200px" runat="server" ID="ddlLegalResultContractPriority"></asp:DropDownList>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Center" Width="200px">
															Referral Assignment:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left">
                                                        <asp:TextBox Width="196px" runat="server" ID="txtLegalResultReferralAssignment"></asp:TextBox>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Center" Width="200px">
                                                            Contract Deadline:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox Width="196px" runat="server" ID="txtLegalResultDeadline"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="calExtResultDeadline"
                                                            runat="server" TargetControlID="txtLegalResultDeadline" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>

                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblUserRequestedDeadline" Font-Size="Small" Font-Italic="true"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:Panel>
                                        <br />


                                        <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvLegalApproval" AutoGenerateColumns="false" Width="1145px"
                                            BackColor="#f5efe2" AllowPaging="false" CellSpacing="0" HeaderStyle-Font-Size="X-Small" BorderWidth="1px" BorderColor="#003060" BorderStyle="Solid"
                                            Font-Size="Small" HeaderStyle-Height="30px" HeaderStyle-Wrap="true" ShowHeader="true"
                                            HeaderStyle-BackColor="White" HeaderStyle-ForeColor="#003060" CellPadding="1">
                                            <AlternatingRowStyle BackColor="white" />
                                            <Columns>

                                                <asp:BoundField DataField="UserName" HeaderText="Approval/Rejections"
                                                    SortExpression="UserName"></asp:BoundField>
                                                <asp:BoundField DataField="Status"
                                                    SortExpression="Status"></asp:BoundField>
                                                <asp:BoundField DataField="Role" HeaderText="Hierarchy"
                                                    SortExpression="Role"></asp:BoundField>
                                                <asp:BoundField DataField="ModifyDate" DataFormatString="{0:d}" HeaderText="Date Reviewed"
                                                    SortExpression="ModifyDate"></asp:BoundField>
                                                <asp:BoundField DataField="Comment" HeaderText="Comment"
                                                    SortExpression="Comment"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>

                                        <br />
                                        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                            <asp:Table runat="server">
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell Width="225px">Contract Name:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractName"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Date Submitted:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultDate"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell Width="225px"> Desired Length of Contract:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractLength"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Requestor:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultRequestor"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Should the Contract Auto-Renew?</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultAutoRenewal"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell><asp:Label runat="server" Text="Contract Cost Center Number:" ToolTip="Primary Contract Cost Center"></asp:Label></asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultDepartmentNo"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Desired Renewal Term:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultRenewalTerm"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Vendor Name:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultVendorName"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Annual Contract Cost:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractCost"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Contract Type:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractType"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Purpose of the Contract:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractPurpose"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Northside Contracting Party:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractParty"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblLegalResultContractPartyExplain"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                    <asp:TableCell>Budget Expense Account:</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" ID="lblLegalResultContractBudgetAcct"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Height="10px"></asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel runat="server" Width="1145px">
                                            <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvLegalResultQuestions1" AutoGenerateColumns="false"
                                                BackColor="#f5efe2" AllowPaging="false" CellSpacing="2" HeaderStyle-Font-Size="X-Small"
                                                Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"
                                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3" DataKeyNames="QuestionID">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>
                                                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                                        HeaderStyle-CssClass="hidden"
                                                        SortExpression="QuestionID"></asp:BoundField>
                                                    <asp:BoundField DataField="DisplayingNo" HeaderText=""></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                                        <ItemTemplate>
                                                            <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                                <asp:TableRow>
                                                                    <asp:TableCell ColumnSpan="4">
                                                                        <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell></asp:TableCell>
                                                                    <asp:TableCell Width="100px" HorizontalAlign="Right">
                                                                        <asp:Label Font-Size="Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="25px"></asp:TableCell>
                                                                    <asp:TableCell VerticalAlign="Top">
                                                                        <asp:Label runat="server" ID="lblResponseAsk" Visible='<%# Bind("ViewComment")%>' Text="Please explain"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell></asp:TableCell>
                                                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Right">
                                                                        <asp:Table Width="100%" HorizontalAlign="Left" runat="server" CellPadding="0" CellSpacing="0">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label Font-Size="Small" ID="lblSubResponseDate" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerDate", "{0:d}")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label Width="600px" Font-Size="Small" ID="lblSubResponseComment" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerComment")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />
                                        </asp:Panel>

                                        <asp:Panel runat="server" Width="1145px">
                                            <asp:GridView runat="server" ShowHeaderWhenEmpty="false" ID="gvLegalResultQuestions2" AutoGenerateColumns="false"
                                                BackColor="#f5efe2" AllowPaging="false" CellSpacing="2" HeaderStyle-Font-Size="X-Small" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" ShowHeader="false"
                                                HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="3" DataKeyNames="QuestionID">
                                                <AlternatingRowStyle BackColor="white" />
                                                <Columns>

                                                    <asp:BoundField DataField="QuestionID" HeaderText="QuestionID" ItemStyle-CssClass="hidden"
                                                        HeaderStyle-CssClass="hidden"
                                                        SortExpression="QuestionID"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                                        <ItemTemplate>
                                                            <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                                    <asp:TableCell ColumnSpan="4">
                                                                        <asp:Label Font-Size="Small" ID="lblSubQuestion" runat="server" Text='<%# Bind("Question")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell></asp:TableCell>
                                                                    <asp:TableCell Width="100px" HorizontalAlign="Right">
                                                                        <asp:Label Font-Size="Small" ID="lblSubResponse" runat="server" Text='<%# Bind("Response")%>'></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="5px"></asp:TableCell>
                                                                    <asp:TableCell Width="25px"></asp:TableCell>
                                                                    <asp:TableCell VerticalAlign="Top">
                                                                        <asp:Label runat="server" ID="lblResponseAsk" Visible='<%# Bind("ViewComment")%>' Text="Please explain"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell></asp:TableCell>
                                                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Right">
                                                                        <asp:Table Width="100%" HorizontalAlign="Left" runat="server" CellPadding="0" CellSpacing="0">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label Font-Size="Small" ID="lblSubResponseDate" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerDate", "{0:d}")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label Font-Size="Small" ID="lblSubResponseComment" Visible='<%# Bind("ViewComment")%>' runat="server" Text='<%# Bind("AnswerComment")%>'></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="Panel6" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                            <asp:Table runat="server" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Center" Width="160px">
                        Requestor comments:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left">
                                                        <asp:Label runat="server" ID="lblLegalResultRequestorComments"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>

                                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                                        <asp:GridView ID="gvLegalAttachments" runat="server" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing="5"
                                                            HeaderStyle-Font-Size="small" AllowSorting="false" Font-Size="Small" Width="600px" BorderColor="#003060" BorderWidth="1px"
                                                            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5" PageSize="20">
                                                            <AlternatingRowStyle BackColor="white" />
                                                            <Columns>

                                                                <asp:BoundField HeaderText="File Id" DataField="AttachmentID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                                <asp:BoundField HeaderText="ContentType" DataField="ContentType"
                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                                <asp:BoundField HeaderText="File Name" DataField="FileName" ItemStyle-Width="300px" />

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">

                                                                    <ItemTemplate>

                                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadLegalAttachmentFile"
                                                                            CommandArgument='<%# Eval("AttachmentID")%>'></asp:LinkButton>

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>


                                                            </Columns>

                                                        </asp:GridView>

                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:Panel>
                                        <br />


                                        <asp:Panel ID="pnlLegalComments" runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
                                            <asp:Table runat="server" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Left">
                                                        Comments:
                                                   <asp:Label runat="server" ID="lblLegalResultComments" Width="600px"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Left">
                                                        Add Comments:
                                                   <asp:TextBox runat="server" ID="txtLegalResultComments" TextMode="MultiLine" Rows="5" Width="600px"></asp:TextBox>
                                                    </asp:TableCell>
                                                    <%-- <asp:TableCell>
                                                   <asp:CheckBox runat="server" ID="cbHideLegalComments" Text="Legal Users Only" Checked="true" Font-Size="X-Small" />
                                               </asp:TableCell>--%>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Height="3px"></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <%--<asp:TableCell HorizontalAlign="Center">

                                                <asp:Button runat="server" ID="btnLegalResultAttachments" Text="View Attachments" OnClientClick="open_win3()" />
                                            </asp:TableCell>--%>
                                                    <asp:TableCell HorizontalAlign="Center">
                                                        <asp:Button runat="server" ID="btnLegalUpdateContract" Text="Update Contract" />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </asp:Panel>
                                    </asp:Panel>



                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                        <br />

                        <asp:Label ID="FakeButtonLegalmpe" runat="server" />
                        <asp:Panel ID="pnlLegalMPE" runat="server" Width="400px" BackColor="#6da9e3">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="explanationlabelLegal" runat="server"></asp:Label>
                                    </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="Button1" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button ID="Button2" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Confirm" />

                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell><asp:TableCell>
                                        <asp:Button ID="Button3" BorderStyle="Outset" BorderWidth="2px" runat="server" Visible="false" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeLegalTab" runat="server"
                            TargetControlID="FakeButtonLegalmpe"
                            PopupControlID="pnlLegalMPE"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>

                    </ContentTemplate>

                </cc1:TabPanel>

                <cc1:TabPanel runat="server" HeaderText="Administrative" ID="tpAdministrative" Visible="false">
                    <ContentTemplate>

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
                                        <asp:ListItem Text="Departments" Value="Departments"></asp:ListItem>
                                        <asp:ListItem Text="Users" Value="Users"></asp:ListItem>
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell Height="10px"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <br />

                        <asp:Panel ID="pnlAdminDepartments" runat="server" Visible="true">

                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5">                                                                                                                                                                         
                                        List of Departments which can submit Vendor Contracts -- to manage users for a Department, select it in the grid below.                           
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="250px">
                                    Enter Cost Center Number:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAdminDepartment" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Select Facility:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="ddlAdminFacility" Width="153px">
                                            <asp:ListItem Text="Atlanta" Value="10" />
                                            <asp:ListItem Text="Cherokee" Value="22" />
                                            <asp:ListItem Text="Forsyth" Value="6" />
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Full Description:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminDepartmentName"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    Enter Display Description (Optional):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtAdminDepartmentShortName"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnAddDepartment" Text="Add This Department" Width="153px" UseSubmitBehavior="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <%--<asp:TableCell Width="5px"></asp:TableCell>--%>
                                    <asp:TableCell ColumnSpan="2">
                                        Filter by Department Name or Cost Center Number:
                                   <asp:TextBox runat="server" ID="txtAdminDepFilter"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnAdminDepSearch" Text="Search" UseSubmitBehavior="true" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminDeptsActive" Checked="true" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminDepartmentScrolling" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminDepartments" runat="server"
                                                AllowSorting="True" AllowPaging="true" PageSize="25" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="DepartmentID"
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
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("DepartmentNo")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <%-- <asp:TextBox runat="server" ID="txtDepartment" Text='<%# Bind("DepartmentNo")%>' Visible="false"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Facility">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblFacility" runat="server" Text='<%# Bind("Facility")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <%--<asp:DropDownList runat="server" ID="ddlFacility" Text='<%# Bind("Facility")%>' Visible="false"></asp:DropDownList>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Full Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DepartmentName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtDescription" Text='<%# Bind("DepartmentName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Description">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortDesc" runat="server" Text='<%# Bind("DepartmentDisplayName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortDesc" Text='<%# Bind("DepartmentDisplayName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveDept" CommandArgument='<%# Bind("DepartmentID")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>

                                    <asp:TableRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvDeptUserAccess" runat="server"
                                                AllowSorting="True" AllowPaging="true" PageSize="100" AutoGenerateColumns="False" BorderColor="Black"
                                                BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                                HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="UserLogin" HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="false"
                                                BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                                <AlternatingRowStyle BackColor="white" />

                                                <Columns>
                                                    <asp:TemplateField HeaderText="The following Users have access to this Department">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblUser" runat="server" Text='<%# Bind("UserName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtUser" Text='<%# Bind("UserName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Position">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Bind("Position")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtPosition" Text='<%# Bind("Position")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveAccess" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </asp:TableCell>
                                    </asp:TableRow>

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
                                        <asp:TextBox runat="server" ID="txtAdminADDisplayName"></asp:TextBox>
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
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button runat="server" ID="btnAdminAddUser" Text="Add User" UseSubmitBehavior="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <%--<asp:TableCell></asp:TableCell>--%>
                                    <asp:TableCell ColumnSpan="2">
                                        Filter by Name or UserLogin:
                                        <asp:TextBox runat="server" ID="txtFilterUsers"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnFilterUserTable" Text="Search" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:CheckBox runat="server" ID="cbAdminUserActives" Checked="true" Text="Show Inactives" AutoPostBack="true" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <asp:Panel runat="server" ID="pnlAdminUsersScrolling" ScrollBars="Auto" CssClass="MxPanelHeight">
                                <asp:Table runat="server">
                                    <asp:TableFooterRow>
                                        <asp:TableCell>

                                            <asp:GridView ID="gvAdminUsers" runat="server"
                                                AllowSorting="True" AllowPaging="true" PageSize="15" AutoGenerateColumns="False" BorderColor="Black" HeaderStyle-Font-Size="Smaller"
                                                BorderStyle="Solid" Font-Size="small" HeaderStyle-BackColor="#214B9A"
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
                                                    <asp:TemplateField HeaderText="Short Name" SortExpression="UserDisplayName">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("UserDisplayName")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtShortName" Text='<%# Bind("UserDisplayName")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email" SortExpression="UserEmail">
                                                        <ItemTemplate>
                                                            <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("UserEmail")%>' Visible="true"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:TextBox runat="server" ID="txtEmail" Text='<%# Bind("UserEmail")%>' Visible="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Manage Depts">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="lbManageDepts" runat="server" Text='Manage'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Webtool Access" SortExpression="UserClicky">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnUserRights" runat="server" Text='<%# Bind("UserClicky")%>' CommandName="FlipActive" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admin Rights" SortExpression="Admin">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnAdminRights" runat="server" Text='<%# Bind("AdminClicky")%>' CommandName="FlipAdmin" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Legal Team" SortExpression="LegalTeam">
                                                        <ItemTemplate>
                                                            <asp:Panel runat="server">
                                                                <asp:LinkButton ID="btnLegalRights" runat="server" Text='<%# Bind("Legal")%>' CommandName="FlipLegal" CommandArgument='<%# Bind("UserLogin")%>'></asp:LinkButton>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </asp:TableCell>

                                    </asp:TableFooterRow>
                                    <%--       <asp:TableRow>
                                   <asp:TableCell>

                                       <asp:GridView ID="gvUserDepts" runat="server"
                                           AllowSorting="True" AllowPaging="true" PageSize="100" AutoGenerateColumns="False" BorderColor="Black"
                                           BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A"
                                           HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="false"
                                           HeaderStyle-Wrap="true" ForeColor="Black" DataKeyNames="Flipper"
                                           BackColor="#CBE3FB" BorderWidth="1px" CellPadding="5" CellSpacing="3">
                                           <AlternatingRowStyle BackColor="white" />

                                           <Columns>
                                               <asp:TemplateField HeaderText="This User has access to the following Departments">
                                                   <ItemTemplate>
                                                       <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                           <asp:Label ID="lblUser" runat="server" Text='<%# Bind("Department")%>' Visible="true"></asp:Label>
                                                       </asp:Panel>
                                                       <asp:TextBox runat="server" ID="txtUser" Text='<%# Bind("Department")%>' Visible="false"></asp:TextBox>
                                                   </ItemTemplate>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Position">
                                                   <ItemTemplate>
                                                       <asp:Panel CssClass="paneltest" Width="95%" runat="server">
                                                           <asp:Label ID="lblPosition" runat="server" Text='<%# Bind("RoleFull")%>' Visible="true"></asp:Label>
                                                       </asp:Panel>
                                                       <asp:TextBox runat="server" ID="txtPosition" Text='<%# Bind("RoleFull")%>' Visible="false"></asp:TextBox>
                                                   </ItemTemplate>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="">
                                                   <ItemTemplate>
                                                       <asp:Panel runat="server">
                                                           <asp:LinkButton ID="btnRemoveType" runat="server" Text='<%# Bind("Clicky")%>' CommandName="RemoveAccess" CommandArgument='<%# Bind("Flipper")%>'></asp:LinkButton>
                                                       </asp:Panel>
                                                   </ItemTemplate>
                                               </asp:TemplateField>

                                           </Columns>
                                       </asp:GridView>

                                   </asp:TableCell>
                               </asp:TableRow>--%>
                                    <%--                  <asp:TableRow>
                                   <asp:TableCell>

                                   </asp:TableCell>
                               </asp:TableRow>
                               <asp:TableRow>
                                   <asp:TableCell>
                                       <asp:Panel runat="server" ID="pnlGrantAccess" Visible="false">
                                           <asp:Table runat="server">                                               
                                               <asp:TableRow>
                                                   <asp:TableCell>Position:</asp:TableCell>
                                                   <asp:TableCell>
                                                       <asp:DropDownList runat="server" ID="ddlGrantPosition"></asp:DropDownList>
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                               <asp:TableRow>
                                                   <asp:TableCell>Department:</asp:TableCell>
                                                   <asp:TableCell>
                                                       <asp:DropDownList runat="server" ID="ddlGrantDepartment"></asp:DropDownList>
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                               <asp:TableRow>
                                                   <asp:TableCell ColumnSpan="3">
                                                       <asp:Button runat="server" ID="btnGrantAccess" Text ="Grant Access" />
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                           </asp:Table>
                                       </asp:Panel>
                                   </asp:TableCell>
                               </asp:TableRow>--%>
                                </asp:Table>
                            </asp:Panel>

                        </asp:Panel>


                        <asp:Label ID="FakeButtonp4" runat="server" />
                        <asp:Panel ID="Panelp4" runat="server" Width="400px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="explanationlabelAdmin" runat="server"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="OkButtonp4" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeAdminPage" runat="server"
                            TargetControlID="FakeButtonp4"
                            PopupControlID="Panelp4"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>



                        <asp:Label ID="FakeButtonMultipleCCGroupings" runat="server" />
                        <asp:Panel ID="pnlMultipleCCGroupings" runat="server" Width="400px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px">
                            <asp:Table runat="server" Width="100%" Height="100%">
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                                        <asp:Label ID="lblCCGroupins" runat="server"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell></asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                                        <asp:Button ID="btnConfirmCCGroupings" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Confirm" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnCancelCCGroupings" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="10px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <br />
                        <cc1:ModalPopupExtender ID="mpeMultipleCCGroupings" runat="server"
                            TargetControlID="FakeButtonMultipleCCGroupings"
                            PopupControlID="pnlMultipleCCGroupings"
                            DropShadow="true">
                        </cc1:ModalPopupExtender>


                    </ContentTemplate>

                </cc1:TabPanel>

            </cc1:TabContainer>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
