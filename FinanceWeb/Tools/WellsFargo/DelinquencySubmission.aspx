<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="DelinquencySubmission.aspx.vb" Inherits="FinanceWeb.DelinquencySubmission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <style type="text/css" media="print">

        .MaxPanelHeight {
            max-height:600px;
        }

        .Printbutton
        {
            display:none;
         }
    </style>

     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>





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

   function SelectAllCheckboxesSpecific(spanChk) {

        if ($get('<%=gvWFAllActivity.ClientID%>') != null) {
            var IsChecked = spanChk.checked;

            var Chk = spanChk;

            var items = $get('<%=gvWFAllActivity.ClientID%>').getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {

                if (items[i].id != Chk && items[i].type == "checkbox") {

                    if (items[i].checked != IsChecked) {

                        items[i].click();

                    }

                }

            }

        }
    }

</script>
    <asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>

    <asp:Panel ID="pnlAllActivity1" runat="server" Visible="false">


    <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" Width="1145px">
        <asp:Panel ID="pnlDepositSlip" runat="server" >
            <asp:Table runat="server" >
                <asp:TableRow>
                    <asp:TableCell Width="5px"></asp:TableCell>
                    <asp:TableCell Font-Bold="true" Font-Size="Large" ForeColor="#003060" Width="225px">
                        Facility:
                     </asp:TableCell>
                    <asp:TableCell Width="5px"></asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:DropDownList runat="server" ID="ddlFacility" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        <asp:Label runat="server" ID="lblFacility" Visible="false" Font-Bold="true"  Font-Size="Large" ForeColor="#4A8fd2"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell Font-Bold="true" Font-Size="Large" ForeColor="#003060" Width="300px">
                        Deposit Slip Number: 
                    </asp:TableCell>
                    <asp:TableCell Width="5px"></asp:TableCell>
                    <asp:TableCell>
                        <asp:Textbox runat="server" ID="txtDepositSlip" CssClass="form-control" AutoPostBack="true"></asp:Textbox>
                        <asp:Label runat="server" Visible="false" ID="lblDepositSlip" Font-Bold="true"  Font-Size="Large" ForeColor="#4A8fd2"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell><asp:Label runat="server" ID="lblDepSlipActive" Visible="false" Font-Size="Small" ForeColor="#003060">*This Deposit Slip Number is Delinquent*</asp:Label></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            
            
        </asp:Panel>
        </asp:Panel>
        </asp:Panel>
 <asp:Panel ID="pnlInstamed1" runat="server" Visible="false">
                      <asp:Panel ID="pnlIMTitle" Width="400px" runat="server" HorizontalAlign="Center">
            <asp:Table runat="server" >
                <asp:TableRow>
                    <asp:TableCell Font-Bold="true" Font-Size="Large" ForeColor="#003060" Width="150px">
                        Transaction Date: 
                    </asp:TableCell>
                    <asp:TableCell Width="5px"></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" ID="lblTranDate" Font-Bold="true"  Font-Size="Large" ForeColor="#4A8fd2"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Height="5px"></asp:TableCell>
                </asp:TableRow>
                               <asp:TableRow>
                    <asp:TableCell Font-Bold="true" Font-Size="Large" ForeColor="#003060" Width="150px">
                        Outlet Taken At: 
                    </asp:TableCell>
                    <asp:TableCell Width="5px"></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" ID="lblMerchDesc" Font-Bold="true"  Font-Size="Large" ForeColor="#4A8fd2"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell><asp:Label runat="server" ID="lblIMDelinquency" Visible="false" Font-Size="Small" ForeColor="#003060">*This Combination is Delinquent*</asp:Label></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
    </asp:Panel>
     </asp:Panel>
                <br />
        Explain why this is being marked as delinquent:
        <br />
        
                <asp:TextBox runat="server" ID="txtComment" TextMode = "MultiLine"  Rows="3"></asp:TextBox>
        <br />
        <asp:Panel runat="server" ID="pnlAA3" Visible="false">
        <br />
        <asp:Button runat="server" ID="btnIgnoreSlip" Text="Mark Deposit Slip as Ignored" /><br />
        <asp:Label runat="server" Font-Size="X-Small" Text="*All Bags submitted with this Deposit Slip in the future will not show up on Delinquency log" ID="lblExplainIgnoreDepositSlip">
        </asp:Label>
        <br /></asp:Panel><br />
        <asp:Label runat="server" Font-Size="Medium" Font-Bold="true" ForeColor="#4A8fd2"> WF PPS Submissions</asp:Label>
       <br />
        <asp:Label runat="server" ID="lblWFPPSNoRecords" Text="No Records found" Visible="false"></asp:Label>
            <asp:GridView ID="gvPPS_Submissions" runat="server" AutoGenerateColumns="false"
          BackColor="white" AllowPaging="true" CellSpacing ="5" HeaderStyle-Font-Size ="Smaller"
                                 AllowSorting="true" 
                                  HeaderStyle-BackColor="#003060" HeaderStyle-ForeColor="White" CellPadding="5"
            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" height="100%" 
            PageSize="10" BorderColor ="#003060" BorderWidth="1px"
    DataKeyNames="DepositBagID" >
        <AlternatingRowStyle BackColor="#CBE3FB" />
    <Columns>

       <asp:TemplateField ControlStyle-Width="5px">
               <%--    <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

            </HeaderTemplate>--%>

                <ItemTemplate>
                  <asp:CheckBox ID="chkBag" runat="server" Text="" Checked='<%# Bind("Ignore")%>' />
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

                         <asp:TemplateField HeaderText="Ignore Row"  ItemStyle-Width="5px">
      <%--            <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

            </HeaderTemplate>--%>

                    <ItemTemplate>
                  <asp:CheckBox ID="chkRow" runat="server" Text="" Checked='<%# Bind("Ignored")%>' />
                    </ItemTemplate>
                    </asp:TemplateField>

                            <asp:BoundField ItemStyle-Width="5px" DataField="ID" HeaderText="ID"   />

                            <asp:BoundField ItemStyle-Width="75px" DataField="EODCollectionShort" HeaderText="EOD Collection Date"  />

                            <asp:BoundField ItemStyle-Width="200px" DataField="OutletTA" HeaderText="Outlet TA" />

                            <asp:BoundField ItemStyle-Width="50px" DataField="Cash" DataFormatString="{0:C}" HeaderText="Cash" ItemStyle-HorizontalAlign="Right"   />

                            <asp:BoundField ItemStyle-Width="50px" DataField="ManualChecks" DataFormatString="{0:C}" HeaderText="Manual Checks" ItemStyle-HorizontalAlign="Right"   />

                             <asp:BoundField ItemStyle-Width="40px" DataField="AgreeToEOD" HeaderText="Agree To EOD" />

                           <asp:BoundField ItemStyle-Width="150px" DataField="Explain" HeaderText="Explain" />

                            <asp:BoundField ItemStyle-Width="150px" DataField="Comment" HeaderText="Comment" />

                        </Columns>

                    </asp:GridView>

                </asp:Panel>

            </ItemTemplate>

        </asp:TemplateField>

        <asp:BoundField ItemStyle-Width="150px" DataField="DepositBagNumber" HeaderText="Deposit Bag Number" SortExpression="DepositBagNumber" />

        <asp:BoundField ItemStyle-Width="150px" DataField="DepositDate" HeaderText="Deposit Date" SortExpression="DDSort" />

        <asp:BoundField ItemStyle-Width="150px" DataField="RelevantTotal" Visible="false" DataFormatString="{0:C}" HeaderText="Relevant Total" SortExpression="RelevantTotal" />

        <asp:BoundField ItemStyle-Width="150px" DataField="BagTotal" DataFormatString="{0:C}" HeaderText="Bag Total" SortExpression="BagTotal" />

        <asp:BoundField ItemStyle-Width="150px" DataField="SubmissionFullName" HeaderText="Submission By" SortExpression="SubmissionFullName" />

        <asp:BoundField ItemStyle-Width="150px" DataField="Comment" HeaderText="Comment" SortExpression="Comment" />

    </Columns>
</asp:GridView>

        <asp:Panel ID="pnlAllActivity2" runat="server" Visible="false">
        <br /><asp:Label runat="server" Font-Size="Medium" Font-Bold="true" ForeColor="#4A8fd2">WF All Activity </asp:Label><br />
            <asp:Label runat="server" ID="lblAANoRecords" Text="No Records found" Visible="false"></asp:Label>
         <asp:Panel runat="server" ID="ScrollPanel_gvWFAllActivity"  ScrollBars ="Auto" CssClass ="MaxPanelHeight" >
                    <asp:GridView ID="gvWFAllActivity" runat="server" AutoGenerateColumns="false"
          BackColor="white" AllowPaging="true" CellSpacing ="5" HeaderStyle-Font-Size ="Small"
                                 AllowSorting="true" Font-Size="X-Small"
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5"
            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" height="100%" 
            PageSize="10" BorderColor ="#003060" BorderWidth="1px"
    DataKeyNames="UniqueActivityID" >
        <AlternatingRowStyle BackColor="#CBE3FB" />
                       <Columns>

                         <asp:TemplateField HeaderText="Ignore Row">
                 <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

            </HeaderTemplate>

                    <ItemTemplate>
                  <asp:CheckBox ID="chkWF" runat="server" Text="" Checked='<%# Bind("Ignored")%>' />
                    </ItemTemplate>
                    </asp:TemplateField>

                            <asp:BoundField ItemStyle-Width="150px" DataField="BankName" HeaderText="Bank Name" SortExpression="BankName" />

                            <asp:BoundField ItemStyle-Width="100px" DataField="AcctName" HeaderText="Account Name" SortExpression="AcctName" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="NetAmount" DataFormatString="{0:C}"  HeaderText="Net Amount" SortExpression="NetAmount" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="ValueDate" HeaderText="Value Date" SortExpression="VDSort" />

                            <asp:BoundField ItemStyle-Width="75px" DataField="AsOfDate" HeaderText="As Of Date" SortExpression="AODSort" />

                             <asp:BoundField ItemStyle-Width="150px" DataField="Comment" HeaderText="Comment" SortExpression="Comment" />

                        </Columns>

                    </asp:GridView>
        </asp:Panel>

        <asp:Button runat="server" ID="btnIgnoreRows" Text="Mark Selected Rows as Ignored" />

    </asp:Panel>

 
     <asp:Panel ID="pnlInstamed2" runat="server" Visible="false">
                 <br /><asp:Label runat="server" Font-Size="Medium" Font-Bold="true" ForeColor="#4A8fd2">Instamed </asp:Label><br />
         <asp:Panel runat="server" ID="Panel2"  ScrollBars ="Auto" CssClass ="MaxPanelHeight" >
             <asp:Label runat="server" ID="lblInstamedNoRecords" Text="No Records found" Visible="false"></asp:Label>
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
                  <asp:CheckBox ID="chkIM" runat="server" Text="" Checked='<%# Bind("Ignored")%>' />
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
         <br />
              <asp:Button runat="server" ID="btnIgnoreInstamed" Text="Mark Selected Rows as Ignored" />
         <br />
         </asp:Panel>
                 <asp:Label ID="mpe1FakeButton" runat = "server" />
        <asp:Panel ID="mpe1Panel" runat="server" Width="300px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="80%" CssClass="collapsetable">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                    <asp:label ID = "mpe1ExplanationLabel" runat = "server"></asp:label> 
                </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> 

           </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow></asp:Table>
           <asp:Table runat="server" width="100%" Height="20%" CssClass="collapsetable" >
        <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="mpe1btnOK" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell>
          <asp:TableCell>  <asp:Button ID="mpe1btnCancel" Visible="false" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="mpe1" runat="server"
                 TargetControlID ="mpe1FakeButton"
                 PopupControlID="mpe1Panel"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>
   
<%--    </form>
</body>
</html>--%>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>