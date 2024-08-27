<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="WFPPSDelinquency.aspx.vb" MasterPageFile="~/Site.Master"  Inherits="FinanceWeb.WFPPSDelinquency" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css" media="print">
        .Printbutton
        {
            display:none;
         }
    </style>
<%--      <style type="text/css">

.vertScroll {   
    width: 400px;  
    max-height:225px;
    background-color:#CBE3FB;
    overflow-x: hidden; /* Hide horizontal scroll bar*/
    overflow-y: auto; /*Show vertical scroll bar*/
    
    
    /*height: expression(this.scrollHeight < 225 ? "225px" : "auto");*/
}   
          </style>--%>

<%--
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
--%>
  <%-- 
<asp:ScriptManager runat="server"></asp:ScriptManager>--%>
 <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

<%--    <script>
        function open_win() {
            window.open("http://financeweb.northside.local/Tools/WellsFargo/WFPPSInstructions.aspx", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
        }
        function open_win2() {
            window.open("http://financeweb.northside.local/Tools/WellsFargo/WFPPSInstructions.aspx?t=2", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
        }
        function open_win3() {
            window.open("http://financeweb.northside.local/Tools/WellsFargo/WFPPSInstructions.aspx?t=1", "ProFeeInstructions", "height=768,width=900, scrollbars, resizable");
        }


    </script>--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $("[src*=Open]").live("click", function () {

            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")

            $(this).attr("src", "../../Images/Close.png");

        });

        $("[src*=Close]").live("click", function () {

            $(this).attr("src", "../../Images/Open.png");

            $(this).closest("tr").next().remove();

        });


</script>

       <cc1:tabcontainer ID="WFPPSTabs" runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
            <cc1:TabPanel runat = "server" HeaderText = "PPS Matching" ID = "tpAdminReject" Visible="true" >
                            <ContentTemplate>    
            <asp:UpdatePanel runat="server" ID= "UpdatePanel1"> 
             <ContentTemplate>
                
                 <asp:Table runat="server"><asp:TableRow><asp:TableCell>
                 <asp:Table runat="server">
                     <asp:TableRow>
                         <asp:TableCell>
            <asp:Table runat="server">
                <asp:TableHeaderRow><asp:TableHeaderCell >
                    <asp:Button runat="server" BorderWidth="1px" BorderColor="#003060" Width="100%" Height="100%"
                         ID="btnDCSrch" BackColor="Green" Text="Discrepancy Search" BorderStyle="Solid"  /></asp:TableHeaderCell></asp:TableHeaderRow>
                <asp:TableRow><asp:TableCell ID="DCCell" Visible="true">

                                                <asp:Table runat="server" Width="100%">
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" Font-Bold="true">
                                        Limit Discrepancy Search:
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        <asp:DropDownList runat="server" ID="ddlDiscMerch" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center">
                                         Hide rows with No PPS Submissions: <asp:DropDownList runat="server" ID="ddlPPSSubmits" AutoPostBack="true">
                                             <asp:ListItem Text="Yes" Value="0" Selected="True">                                                 
                                             </asp:ListItem>
                                             <asp:ListItem Text="No" Value="1">                                                 
                                             </asp:ListItem>
                                                                            </asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        Show rows where Collections Match: <asp:DropDownList runat="server" ID="ddlShowMatch" AutoPostBack="true">
                                            <asp:ListItem Text="Yes" Value="1">                                                 
                                             </asp:ListItem>
                                             <asp:ListItem Text="No" Value="0" Selected="True">                                                 
                                             </asp:ListItem>
                                        </asp:DropDownList>
                                     </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                         Collection Date Range:
                                         <asp:TextBox runat="server" ID="txtDiscCollStartDate" Width="75px" font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender3" 
                                                runat="server" TargetControlID="txtDiscCollStartDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                         &nbsp&nbsp To &nbsp;&nbsp;
                                         <asp:TextBox runat="server" ID="txtDiscCollEndDate" Width="75px"  font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender4" 
                                                runat="server" TargetControlID="txtDiscCollEndDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                     </asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table> 

                    <asp:Panel runat="server" ID="Panel1" CssClass="MaxPanelHeight" Width="450px" HorizontalAlign="Center" >
        <asp:GridView ID="gvInstamedvsPPS" runat="server"  HeaderStyle-BackColor = "#6da9e3" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" CellPadding="3"
            RowStyle-BorderColor = "#003060" RowStyle-BorderWidth="1px" AllowPaging="true" AllowSorting ="true" 
            RowStyle-HorizontalAlign = "Center" AutoGenerateColumns="false" 
                HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames="CollectionDate, MerchantDescription" >
          <AlternatingRowStyle BackColor="#CBE3FB" />
                                     <Columns>
                                      <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                          <asp:BoundField DataField="CollectDisplay" HeaderText="Collection Date"  
                                              SortExpression="CollectionDate"></asp:BoundField>
                                          <asp:BoundField DataField="MerchantDescription" HeaderText="Merchant Description"  
                                              SortExpression="MerchantDescription"></asp:BoundField>
                                          <asp:BoundField DataField="Total_PPS_Collections" HeaderText="PPS Collections" 
                                              SortExpression="Total_PPS_Collections"></asp:BoundField>
                                         <asp:BoundField DataField="Total_Instamed_Collections" HeaderText="Instamed Collections" 
                                              SortExpression="Total_Instamed_Collections"></asp:BoundField>                                        
                                      </Columns>

        </asp:GridView>
</asp:Panel>
                </asp:TableCell> </asp:TableRow>
               <asp:TableHeaderRow><asp:TableHeaderCell>
                   <asp:Button runat="server" BorderWidth="1px" BorderColor="#003060" Width="100%" Height="100%"
                         ID="btnGenSrch" BackColor="Gray" Text="General Search" BorderStyle="Solid"  /></asp:TableHeaderCell></asp:TableHeaderRow>
                <asp:TableRow><asp:TableCell ID="GenCell" Visible="false">
 <asp:Panel ID="pnlSpecSearch" runat="server" BackColor ="#CBE3FB">
            <asp:Table runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
    <asp:Table runat="server" Width="100%">
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" Font-Bold="true">
                                        Limit WF PPS Deposit Bag Search:
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        <asp:DropDownList runat="server" ID="ddlFilterEntity" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center">
                                         <asp:DropDownList runat="server" ID="ddlFilterLocation" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        <asp:DropDownList runat="server" ID="ddlFilterSubmitter" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Deposit Bag Number (Optional): <asp:TextBox ID="txtFilterDepBagNum" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Deposit Slip Number (Optional): <asp:TextBox ID="txtFilterDepSlipNum" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                         Deposit Date Range:
                                         <asp:TextBox runat="server" ID="txtDDStartDate" Width="75px" font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender1" 
                                                runat="server" TargetControlID="txtDDStartDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                         &nbsp&nbsp To &nbsp;&nbsp;
                                         <asp:TextBox runat="server" ID="txtDDEndDate" Width="75px"  font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender2" 
                                                runat="server" TargetControlID="txtDDEndDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                     </asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table>     
                    </asp:TableCell>
                    <asp:TableCell>
                                 <asp:Table runat="server" Width="100%">
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" Font-Bold="true">
                                        Limit Instamed Search:
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        <asp:DropDownList runat="server" ID="ddlIMOutletTakenAt" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow>
                                    <asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Patient Account Facility (Optional): <asp:TextBox ID="txtIMPAF" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Transaction ID (Optional): <asp:TextBox ID="txtIMTranID" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Amount Range (Optional): <asp:TextBox ID="txtIMAmountLow" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                             &nbsp&nbsp To &nbsp;&nbsp; <asp:TextBox ID="txtIMAmountHigh" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                         Transaction Date Range:
                                         <asp:TextBox runat="server" ID="txtIMStartDate" Width="75px" font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender5" 
                                                runat="server" TargetControlID="txtIMStartDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                         &nbsp&nbsp To &nbsp;&nbsp;
                                         <asp:TextBox runat="server" ID="txtIMEndDate" Width="75px"  font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender6" 
                                                runat="server" TargetControlID="txtIMEndDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                     </asp:TableCell>
                                 </asp:TableRow>
                                     <asp:TableRow>
                                         <asp:TableCell>&nbsp;</asp:TableCell>
                                     </asp:TableRow>
                             </asp:Table>     
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
                                                                
                    </asp:Panel>
                </asp:TableCell></asp:TableRow>
            </asp:Table>
                          

                         </asp:TableCell>
                         </asp:TableRow>
                     <asp:TableRow>
                         <asp:TableCell>
                    WF PPS Search Results
                    <br />
                    <asp:Panel runat="server" ID="ScrollPanel" CssClass="MaxPanelHeight" Width="450px" HorizontalAlign="Center" >
        <asp:GridView ID="gvSubmittedBags" runat="server"  HeaderStyle-BackColor = "#6da9e3" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" CellPadding="3"
            RowStyle-BorderColor = "#003060" RowStyle-BorderWidth="1px" AllowPaging="true" AllowSorting ="true" 
            RowStyle-HorizontalAlign = "Center" AutoGenerateColumns="false" 
                HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames= "DepositBagID">
          <AlternatingRowStyle BackColor="#CBE3FB" />
                                     <Columns>
 <%--                                     <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />--%>
   <asp:TemplateField HeaderText="Select Bag"  ControlStyle-Width="5px">

                <ItemTemplate>
                  <asp:CheckBox ID="chkBag" runat="server" Text="" />
                </ItemTemplate>
                </asp:TemplateField>
        <asp:TemplateField>

            <ItemTemplate>

                <img alt = "" style="cursor: pointer" src="../../Images/Open.png" />

                <asp:Panel ID="pnlSubmissionRow" runat="server" Style="display: none">

                    <asp:GridView ID="gvSubmissionRow" runat="server" AutoGenerateColumns="false" CellPadding="5"  CellSpacing ="8"
                        BackColor="white" BorderColor ="#003060" BorderWidth="1px" Font-Size="X-Small" HeaderStyle-ForeColor="White" 
                        HeaderStyle-BackColor="#4A8fd2" HeaderStyle-Wrap="true"  HeaderStyle-Font-Size ="X-Small" HeaderStyle-HorizontalAlign="Center"
                         DataKeyNames="ID"
                        >
                         <AlternatingRowStyle BackColor="#CBE3FB" />
                        <Columns>

                         <asp:TemplateField HeaderText="Select Row"  ItemStyle-Width="5px">
      <%--            <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

            </HeaderTemplate>--%>

                    <ItemTemplate>
                  <asp:CheckBox ID="chkRow" runat="server" Text="" />
                    </ItemTemplate>
                    </asp:TemplateField>

                            <asp:BoundField ItemStyle-Width="5px" DataField="ID" HeaderText="ID"  HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden"  />

                            <asp:BoundField ItemStyle-Width="75px" DataField="EODCollectionShort" HeaderText="EOD Collection Date"  />

                            <asp:BoundField ItemStyle-Width="200px" DataField="OutletTA" HeaderText="Outlet TA" />

                            <asp:BoundField ItemStyle-Width="50px" DataField="Cash" DataFormatString="{0:C}" HeaderText="Cash" ItemStyle-HorizontalAlign="Right"   />

                            <asp:BoundField ItemStyle-Width="50px" DataField="ManualChecks" DataFormatString="{0:C}" HeaderText="Manual Checks" ItemStyle-HorizontalAlign="Right"   />

                             <asp:BoundField ItemStyle-Width="40px" DataField="AgreeToEOD" HeaderText="Agree To EOD" />

                           <asp:BoundField ItemStyle-Width="150px" DataField="Explain" HeaderText="Explain"  HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden"/>

                            <asp:BoundField ItemStyle-Width="150px" DataField="Comment" HeaderText="Comment" HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden" />

                        </Columns>

                    </asp:GridView>

                </asp:Panel>

            </ItemTemplate>

        </asp:TemplateField>
                                          <asp:BoundField DataField="DepositBagID" HeaderText="DepositBagID"   HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden"
                                              SortExpression="DepositBagID"></asp:BoundField>
                                          <asp:BoundField DataField="Entity" HeaderText="Entity"  
                                              SortExpression="Entity"></asp:BoundField>
                                          <asp:BoundField DataField="DepositBagNumber" HeaderText="Deposit Bag Number" 
                                              SortExpression="DepositBagNumber"></asp:BoundField>
                                         <asp:BoundField DataField="DepositSlipNumber" HeaderText="Deposit Slip Number" 
                                              SortExpression="DepositSlipNumber"></asp:BoundField>
                                          <asp:BoundField DataField="DepositDate" HeaderText="Date Submitted" 
                                              SortExpression="dtDeposited"></asp:BoundField>
                                          <asp:BoundField DataField="SubmissionFullName" HeaderText="Submitted By" 
                                              SortExpression="SubmissionFullName"></asp:BoundField>
                                          <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" 
                                              SortExpression="Total"></asp:BoundField>
                                          <asp:BoundField DataField="RelevantTotal" Visible="false" DataFormatString="{0:C}"  HeaderText="Relevant Total" 
                                              SortExpression="RelevantTotal"></asp:BoundField>
                                          <asp:BoundField DataField="Agree" HeaderText="Agree to EOD?" 
                                              SortExpression="Agree"></asp:BoundField>
                                         
                                      </Columns>

        </asp:GridView>
</asp:Panel>
                         </asp:TableCell>
                         <asp:TableCell><asp:Button runat="server" ID="btnMovePPS" Text ="->" /> </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                         <asp:TableCell>
                             Instamed Search Results<br />
                              <asp:Panel runat="server" ID="IMScrollPanel" CssClass="MaxPanelHeight" Width="450px" HorizontalAlign="Center" >
                             <asp:GridView ID="gvInstamed" runat="server" AutoGenerateColumns="false"
          BackColor="white" AllowPaging="true" CellSpacing ="5" HeaderStyle-Font-Size ="Small"
                                 AllowSorting="true" Font-Size="X-Small"
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5"
            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" height="100%" 
            PageSize="10" BorderColor ="#003060" BorderWidth="1px" DataKeyNames="TransactionID, RN"
     >
        <AlternatingRowStyle BackColor="#CBE3FB" />
                       <Columns>

                         <asp:TemplateField HeaderText="">
<%--                 <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

            </HeaderTemplate>--%>

                    <ItemTemplate>
                  <asp:CheckBox ID="chkIM" runat="server" Text="" Checked='<%# Bind("Checked")%>' />
                    </ItemTemplate>
                    </asp:TemplateField>

                            <asp:BoundField ItemStyle-Width="75px" DataField="TransDate" HeaderText="Transaction Date" SortExpression="TDSort" />

                            <asp:BoundField ItemStyle-Width="150px" DataField="MerchantDescription" HeaderText="Merchant Description" SortExpression="MerchantDescription" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="PatientAccountNumber" HeaderText="Patient Account Number" SortExpression="PatientAccountNumber" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="Amount" DataFormatString="{0:C}"  HeaderText="Amount" SortExpression="Amount" />

                            <asp:BoundField ItemStyle-Width="150px" DataField="TransactionID" HeaderText="TransactionID" SortExpression="TransactionID" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="CheckAccountType" HeaderText="Check Account Type" SortExpression="CheckAccountType" />

                             <asp:BoundField ItemStyle-Width="150px" DataField="Comment" HeaderText="Comment" SortExpression="Comment" />

                        </Columns>

                    </asp:GridView>
        </asp:Panel>
                         </asp:TableCell>
                         <asp:TableCell><asp:Button runat="server" ID="btnMoveIM" Text ="->" /> </asp:TableCell>
                     </asp:TableRow>
                 </asp:Table>
                </asp:TableCell><asp:TableCell>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3">
                                Match WorkBench
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                WF PPS WorkBench <br />
                                Total: <asp:Label runat="server" ID="lblWFPPSWorkBenchTotal"></asp:Label><br />
                                <asp:Label runat="server" ID="lblWFPPSWBBags" Text="Full Bags" Visible="false"></asp:Label>
                                <br />

                                <asp:Label runat="server" ID="lblWFPPSWBRows" Text="Individual Rows" Visible="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="10px"></asp:TableCell>
                            <asp:TableCell>
                                Instamed WorkBench <br />
                                Total: <asp:Label runat="server" ID="lblIMWorkBenchTotal"></asp:Label><br />
                                        <asp:GridView ID="gvIMWorkBench" runat="server" AutoGenerateColumns="false"
          BackColor="white" AllowPaging="true" CellSpacing ="5" HeaderStyle-Font-Size ="Small"
                                 AllowSorting="true" Font-Size="X-Small"
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5"
            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" height="100%" 
            PageSize="10" BorderColor ="#003060" BorderWidth="1px" DataKeyNames="TransactionID, RN"
     >
        <AlternatingRowStyle BackColor="#CBE3FB" />
                       <Columns>

                            <asp:BoundField ItemStyle-Width="75px" DataField="TransDate" HeaderText="Transaction Date" SortExpression="TDSort" />

                            <asp:BoundField ItemStyle-Width="150px" DataField="MerchantDescription" HeaderText="Merchant Description" SortExpression="MerchantDescription" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="PatientAccountNumber" HeaderText="Patient Account Number" SortExpression="PatientAccountNumber" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="Amount" DataFormatString="{0:C}"  HeaderText="Amount" SortExpression="Amount" />

                  <asp:TemplateField HeaderText="Matched">

                    <ItemTemplate>
                  <asp:CheckBox ID="chkIMMatch" runat="server" Text="" Checked='<%# Bind("Matched")%>' />
                    </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="">

                    <ItemTemplate>
                  <asp:CheckBox ID="chkIMIgnore" runat="server" Text="" Checked='<%# Bind("Ignored")%>' />
                    </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="">

                    <ItemTemplate>
                  <asp:CheckBox ID="chkIMTrack" runat="server" Text="" Checked='<%# Bind("Tracking")%>' />
                    </ItemTemplate>
                    </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell></asp:TableRow>
                 </asp:Table>

         <asp:Label ID="FakeButton3" runat = "server" />
        <asp:Panel ID="Panel3" runat="server" Width="300px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="80%" CssClass="collapsetable">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                    <asp:label ID = "ExplanationLabelReject" runat = "server"></asp:label> 
                </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> 

           </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow></asp:Table>
           <asp:Table runat="server" width="100%" Height="20%" CssClass="collapsetable" >
        <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="btnMPROK" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell>
          <asp:TableCell>  <asp:Button ID="btnMPRCancel" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
            <asp:TableCell>  <asp:Button ID="btnMPRnvm" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtenderReject" runat="server"
                 TargetControlID ="FakeButton3"
                 PopupControlID="Panel3"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


                 </ContentTemplate>
                </asp:UpdatePanel>
                                </ContentTemplate>
                </cc1:TabPanel>
           </cc1:tabcontainer>
</asp:Content>
<%--
   </div>
    </form>
</body>
</html>--%>