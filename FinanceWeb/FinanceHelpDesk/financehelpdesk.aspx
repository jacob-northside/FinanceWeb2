<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="financehelpdesk.aspx.vb" Inherits="FinanceWeb.financehelpdesk"  ValidateRequest="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <style type="text/css">

.accordion {   
    width: 400px;  
    max-height:225px;
    background-color:#CBE3FB;
    overflow-x: hidden; /* Hide horizontal scroll bar*/
    overflow-y: auto; /*Show vertical scroll bar*/
    
    
    /*height: expression(this.scrollHeight < 225 ? "225px" : "auto");*/
}   


.closedaccordion {
    max-height:150px;
}


.MaxPanelHeight {
    max-height:900px;
    max-width:400px;
}

.AdminMaxPanelHeight {
    max-height:600px;
}

.closedaccordionHeader {   
    border: 1px solid #003060;   
    color: #808080;   
    /*background-color: #2E4d7B;*/  
    background-color: #d3d3d3; 
    font-family: Arial, Sans-Serif;   
    font-size: 10px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}   

.accordionHeader3 {   
    border: 1px solid #003060;   
    color: white;   
    /*background-color: #2E4d7B;*/  
    background-color: #ff7d7d; 
    font-family: Arial, Sans-Serif;   
    font-size: 11px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}   

.accordionHeader2 {   
    border: 1px solid #003060;   
    color: #2b74bb;   
    /*background-color: #2E4d7B;*/  
    background-color: #ffbb77; 
    font-family: Arial, Sans-Serif;   
    font-size: 11px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}   

.accordionHeader1 {   
    border: 1px solid #003060;   
    color: #2b74bb;   
    /*background-color: #2E4d7B;*/  
    background-color: #ffffaa; 
    font-family: Arial, Sans-Serif;   
    font-size: 11px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}   

 .closedaccordionHeaderSelected {   
    border: 1px solid #003060;   
    color: white;   
    /*background-color: #5078B3;*/   
    background-color: #808080;
    font-family: Arial, Sans-Serif;   
    font-size: 11px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}     
         
.accordionHeaderSelected3 {   
    border: 1px solid #003060;   
    color: white;   
    /*background-color: #5078B3;*/   
    background-color: #ff0000;
    font-family: Arial, Sans-Serif;   
    font-size: 12px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
    
}   

.accordionHeaderSelected2 {   
    border: 1px solid #003060;   
    color: white;   
    /*background-color: #5078B3;*/  
    background-color: #ff8000; 
    font-family: Arial, Sans-Serif;   
    font-size: 12px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}   

.accordionHeaderSelected1 {   
    border: 1px solid #003060;   
    color: #2b74bb;   
    /*background-color: #5078B3;*/   
    background-color: #ffff2f;
    font-family: Arial, Sans-Serif;   
    font-size: 12px;   
    font-weight: bold;   
    padding: 3px;   
    margin-top: 3px;   
    cursor: pointer;   
}   
           
.accordionContent {   
    background-color: #D3DEEF;   
    border: 1px dashed #2F4F4F;   
    border-top: none;   
    padding: 0px;   
    padding-top: 10px;   
}   



</style>
  <%--  <script>

            //$(document).ready(function () {
            //    //Iterates through each of the rows in your customers <table>
            //    $('#Accordion1 accordionHeader').each(function () {
            //        //Checks the value and sets the color accordingly
            //        if ($(this).find('span:last').text().trim() === "PROCESSING") {
            //            $(this).css('background', 'green');
            //        }
            //    });
            //});
    

        //function onAccordionPaneChanged(sender, eventArgs) {
        //    var selPane = sender.get_SelectedIndex() + 1;
        //    alert('You selected Pane ' + selPane);
        //}
        //function Openpane3() {
        //    var behavior = $find('Accordion1_AccordionExtender');
        //    if (behavior) {
        //        behavior.set_SelectedIndex(0);
        //    }
        //    var behavior2 = $find('Accordion2_AccordionExtender');
        //    if (behavior2) {
        //        behavior2.set_SelectedIndex(0);
        //    }
        //}

    </script>--%>
  
     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    
<script runat="server">

    Protected Sub ErrorProcessClick_Handler(ByVal sender As Object, ByVal e As EventArgs)
        'This handler demonstrates an error condition. In this example
        ' the server error gets intercepted on the client and an alert is shown. 
        Throw New ArgumentException()

    End Sub



</script>
        
    <script>

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

        var xPos2, yPos2;
        var prm2 = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=ScrollPanelp3.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos2 = $get('<%=ScrollPanelp3.ClientID%>').scrollLeft;
                yPos2 = $get('<%=ScrollPanelp3.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=ScrollPanelp3.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanelp3.ClientID%>').scrollLeft = xPos2;
                $get('<%=ScrollPanelp3.ClientID%>').scrollTop = yPos2;
            }
        }

        prm2.add_beginRequest(BeginRequestHandler);
        prm2.add_endRequest(EndRequestHandler);

        var xPoscat, yPoscat;
        var prmcat = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=ScrollPanelCats.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPoscat = $get('<%=ScrollPanelCats.ClientID%>').scrollLeft;
                yPoscat = $get('<%=ScrollPanelCats.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=ScrollPanelCats.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanelCats.ClientID%>').scrollLeft = xPoscat;
                $get('<%=ScrollPanelCats.ClientID%>').scrollTop = yPoscat;
            }
        }

        prmcat.add_beginRequest(BeginRequestHandler);
        prmcat.add_endRequest(EndRequestHandler);

        var xPosdep, yPosdep;
        var prmdep = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=ScrollPanelDeps.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPosdep = $get('<%=ScrollPanelDeps.ClientID%>').scrollLeft;
                yPosdep = $get('<%=ScrollPanelDeps.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=ScrollPanelDeps.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=ScrollPanelDeps.ClientID%>').scrollLeft = xPosdep;
                $get('<%=ScrollPanelDeps.ClientID%>').scrollTop = yPosdep;
            }
        }

        prmdep.add_beginRequest(BeginRequestHandler);
        prmdep.add_endRequest(EndRequestHandler);


        function open_win(id) {

            var url = "https://financeweb.northside.local/FinanceHelpDesk/FinanceHelpDeskCaseAttachments/?CaseNo=" + id;
            myWindow = window.open(url, 'FinanceHelpDeskAttachments', 'height=700,width=620, scrollbars, resizable');
            myWindow.focus();

        }

        function open_win2() {

            var textbox = document.getElementById('<%= txtSpecificID.ClientID%>');
            var url = "https://financeweb.northside.local/FinanceHelpDesk/FinanceHelpDeskCaseListing/?CaseNo=" + textbox.value;          

                myWindow = window.open(url, 'FinanceHelpDeskCaseInfo', 'height=700,width=620, scrollbars, resizable');
                myWindow.focus()
            ;

        }

        function open_win3(id) {
            myWindow = window.open("https://financeweb.northside.local/FinanceHelpDesk/FinanceHelpDeskInstructions/?t=" + id, "Finance Help Desk Instructions", "height=768,width=800, scrollbars, resizable");
            myWindow.focus();
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>


          <cc1:tabcontainer ID="tcFinanceHelpDesk"  runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "Finance Help Desk" ID = "tpFinHelpMain" ScrollBars="Auto" >
                    <ContentTemplate>   
    
   
    <asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>

    <asp:Table Width="100%" runat ="server" CssClass="collapsetable">
        <asp:TableRow>
            <asp:TableHeaderCell>
                 <asp:Panel runat="server" BackColor="#6da9e3" ForeColor="White" HorizontalAlign="Center">Your Case Dashboard</asp:Panel> 
            </asp:TableHeaderCell>
            
            <asp:TableHeaderCell> 
                <asp:Panel runat="server"  BackColor="#6da9e3" ForeColor="White" HorizontalAlign="Center">Submit a New Case</asp:Panel> 
            </asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell VerticalAlign ="top">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="400">
                            <cc1:Accordion ID="Accordion1" CssClass ="accordion" runat="server"  HeaderCssClass="accordionHeader3" 
                    HeaderSelectedCssClass="accordionHeaderSelected3" RequireOpenedPane="false" SelectedIndex="0"  >
                      <HeaderTemplate >
                          <asp:Panel ID ="pnlTest" runat ="server" ScrollBars ="Auto" CssClass ="MaxPanel">
                          <asp:Label ID="lblCaseNumber" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                          <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("title")%>' />
                              <br />
                          <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("fname")%>' /> --
                          <asp:Label ID="lblOpenDate" runat="server" Text='<%# Eval("start_date")%>' />
                              <br />
                          <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("sname")%>' />
                      </asp:Panel>
                      </HeaderTemplate>
                      <ContentTemplate>
                          <asp:Panel runat="server">
                            Case Submitted by <asp:Label ID="Label2" Font-Size="Small" runat="server" Text='<%# Eval("uid")%>' /><br />
                              Email:  <asp:Label ID="Label3" Font-Size="Small" runat="server" Text='<%# Eval("uemail")%>' /><br />
                              Phone:  <asp:Label ID="Label4" Font-Size="Small" runat="server" Text='<%# Eval("uphone")%>' /><br />
                              Location:  <asp:Label ID="Label5" Font-Size="Small" runat="server" Text='<%# Eval("ulocation")%>' /> <br />
                              ComputerName:  <asp:Label ID="Label6" Font-Size="Small" runat="server" Text='<%# Eval("uComputerName")%>' /><br /><br />

                              Department: <asp:Label ID="Label7" Font-Size="Small" runat="server" Text='<%# Eval("dname")%>' /><br />
                              Category: <asp:Label ID="Label8" Font-Size="Small" runat="server" Text='<%# Eval("cname")%>' /><br /><br />

                              Description:  <asp:Label ID="Label9" Font-Size="Small" runat="server" Text='<%# Eval("description")%>' /><br /><br />
                              Notes:<br />
                       
                        <asp:Label ID="Label1" Font-Size="Small" runat="server" Text='<%# Eval("note").ToString().Replace(";newline;", "<BR><BR>")%>' />
                            </asp:Panel>
                      </ContentTemplate>

                </cc1:Accordion>   
                        </asp:TableCell>
                        </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                 <cc1:Accordion ID="Accordion2" CssClass ="accordion" runat="server" HeaderCssClass="accordionHeader2" 
                    HeaderSelectedCssClass="accordionHeaderSelected2" RequireOpenedPane="false" SelectedIndex ="-1">
                      <HeaderTemplate >
                          <asp:Panel ID ="pnlTest" runat ="server" >
                          <asp:Label ID="lblCaseNumber" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                          <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("title")%>' />
                              <br />
                          <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("fname")%>' /> --
                          <asp:Label ID="lblOpenDate" runat="server" Text='<%# Eval("start_date")%>' />
                              <br />
                          <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("sname")%>' />
                      </asp:Panel>
                      </HeaderTemplate>
                      <ContentTemplate>
                          <asp:Panel runat="server">
                            Case Submitted by <asp:Label ID="Label2" Font-Size="Small" runat="server" Text='<%# Eval("uid")%>' /><br />
                              Email:  <asp:Label ID="Label3" Font-Size="Small" runat="server" Text='<%# Eval("uemail")%>' /><br />
                              Phone:  <asp:Label ID="Label4" Font-Size="Small" runat="server" Text='<%# Eval("uphone")%>' /><br />
                              Location:  <asp:Label ID="Label5" Font-Size="Small" runat="server" Text='<%# Eval("ulocation")%>' /> <br />
                              ComputerName:  <asp:Label ID="Label6" Font-Size="Small" runat="server" Text='<%# Eval("uComputerName")%>' /><br /><br />

                              Department: <asp:Label ID="Label7" Font-Size="Small" runat="server" Text='<%# Eval("dname")%>' /><br />
                              Category: <asp:Label ID="Label8" Font-Size="Small" runat="server" Text='<%# Eval("cname")%>' /><br /><br />

                              Description:  <asp:Label ID="Label9" Font-Size="Small" runat="server" Text='<%# Eval("description")%>' /><br /><br />
                              Notes:<br />
                       
                        <asp:Label ID="Label1" Font-Size="Small" runat="server" Text='<%# Eval("note").ToString().Replace(";newline;", "<BR><BR>")%>' />
                            </asp:Panel>

                      </ContentTemplate>
                </cc1:Accordion>
                            </asp:TableCell>
                  </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
      <cc1:Accordion ID="Accordion3" CssClass ="accordion" runat="server"         HeaderCssClass="accordionHeader1" 
                    HeaderSelectedCssClass="accordionHeaderSelected1" RequireOpenedPane="false" SelectedIndex="-1">
                      <HeaderTemplate >
                          <asp:Panel ID ="pnlTest" runat ="server">
                          <asp:Label ID="lblCaseNumber" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                          <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("title")%>' />
                              <br />
                          <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("fname")%>' /> --
                          <asp:Label ID="lblOpenDate" runat="server" Text='<%# Eval("start_date")%>' />
                              <br />
                          <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("sname")%>' />
                      </asp:Panel>
                      </HeaderTemplate>
                      <ContentTemplate>
                          <asp:Panel runat="server">
                            Case Submitted by <asp:Label ID="Label2" Font-Size="Small" runat="server" Text='<%# Eval("uid")%>' /><br />
                              Email:  <asp:Label ID="Label3" Font-Size="Small" runat="server" Text='<%# Eval("uemail")%>' /><br />
                              Phone:  <asp:Label ID="Label4" Font-Size="Small" runat="server" Text='<%# Eval("uphone")%>' /><br />
                              Location:  <asp:Label ID="Label5" Font-Size="Small" runat="server" Text='<%# Eval("ulocation")%>' /> <br />
                              ComputerName:  <asp:Label ID="Label6" Font-Size="Small" runat="server" Text='<%# Eval("uComputerName")%>' /><br /><br />

                              Department: <asp:Label ID="Label7" Font-Size="Small" runat="server" Text='<%# Eval("dname")%>' /><br />
                              Category: <asp:Label ID="Label8" Font-Size="Small" runat="server" Text='<%# Eval("cname")%>' /><br /><br />

                              Description:  <asp:Label ID="Label9" Font-Size="Small" runat="server" Text='<%# Eval("description")%>' /><br /><br />
                              Notes:<br />
                       
                        <asp:Label ID="Label1" Font-Size="Small" runat="server" Text='<%# Eval("note").ToString().Replace(";newline;", "<BR><BR>")%>' />
                            </asp:Panel>
                      </ContentTemplate>
                </cc1:Accordion>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                     
            </asp:TableCell>
            
            <asp:TableCell VerticalAlign ="Top">
                <asp:Panel runat="server" BackColor="#CBE3FB">
                    <asp:Table runat="server" CellPadding ="0" CellSpacing ="0" CssClass ="collapsetable"><asp:TableRow><asp:TableCell>
                    <asp:Table runat="server" ID="tblNewUser" CellPadding ="0" CellSpacing ="0"  CssClass ="collapsetable">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6"><b>Contact Information</b></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowSelectUser" Visible="false">
                            <asp:TableCell Width="1px"></asp:TableCell>
                            <asp:TableCell>Select User: </asp:TableCell>
                            <asp:TableCell Width="2px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList Width="233px" runat="server" ID="ddlSelectUser" AutoPostBack ="true" AppendDataBoundItems ="true" Visible="false">
                                    <asp:ListItem Value="null" Text="Select User (optional)"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell Width="1px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rwUN" >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>User Name: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox Width="229px" runat="server" ID="txtUserName"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rwEmail">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>E-Mail: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox Width="229px" runat="server" ID="txtUserEmail"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rwDept">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Department: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList Width="233px" runat="server" ID="ddlDepartment" AppendDataBoundItems ="true">
                                     <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rwIP">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>IP Address: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox Width="229px"  runat="server" ID="txtIPAdd"></asp:Textbox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rwCName">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Computer Name: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox Width="229px"  runat="server" ID="txtCompName"></asp:Textbox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rwPhone">
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>Phone: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox Width="229px"  runat="server" ID="txtPhones"></asp:Textbox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell Width="3px" BackColor="White"></asp:TableCell>
                        <asp:TableCell VerticalAlign="Top">
                             <asp:Table runat="server" >
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6"><b>Classification</b></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell Width="1px"></asp:TableCell>
                            <asp:TableCell>Category: </asp:TableCell>
                            <asp:TableCell Width="2px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlSelectCategory" AppendDataBoundItems ="true" AutoPostBack="true" >
                                    <asp:ListItem Value="null" Text="Select Category"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell Width="1px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow id="rowStatus" Visible="false">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Status: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlSelectStatus" Visible="false" >
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow id="rowPriority" Visible="false">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Priority: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlSelectPriority" Visible="false" >
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowAssign" Visible="false" >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Assign To: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>   
                                <asp:DropDownList runat="server" ID="ddlAssignUser" AppendDataBoundItems ="true" Visible="false" >
                                    <asp:ListItem Value="null" Text="Select Representative"></asp:ListItem> <%--THIS VALUE IS "null", not "NULL", not "Null" - "null". If you change it, things will break (like line 914)--%>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowDeadline" Visible="false" >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Deadline: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtAssignDeadline" Text="12/31/9999" Visible="false"></asp:TextBox>
                            <cc1:calendarextender ID="CalendarExtender3" 
                                runat="server" TargetControlID="txtAssignDeadline" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow id="RowTime" Visible="false">
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Time Spent: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox runat="server" ID="txtTimeSpent" Visible="false" Width ="80px"></asp:Textbox>&nbsp;(minutes)
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow></asp:Table></asp:TableCell>
                      </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">Case Information</asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell ColumnSpan ="3">
                                     <asp:Table runat="server" Width="100%">
                                         <asp:TableRow HorizontalAlign="Center">
                                             <asp:TableCell></asp:TableCell>
                                             <asp:TableCell>Title:&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtTitle" Width="300px"></asp:TextBox>&nbsp;
                                                 <asp:Label runat="server" ForeColor="Red">*</asp:Label></asp:TableCell>
                                             <asp:TableCell ForeColor="Red"></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="3"  HorizontalAlign ="Center">Description:<asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="3"  HorizontalAlign ="Center"><asp:TextBox runat="server" TextMode = "MultiLine"  Rows="5" Width="90%" ID="txtDescription"></asp:TextBox></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                                         <asp:TableRow ID="adminrow" Visible="false">
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">Solution:</asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow ID="adminrow2" Visible="false"><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center"><asp:TextBox runat="server" TextMode = "MultiLine"  Rows="5" Width ="90%" ID="txtSolution"></asp:TextBox></asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                 <asp:TableRow ID="adminrow3" Visible="false">
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                         <asp:CheckBox runat="server" Checked="false" ID="chbDontSendMail" Text ="Don't send mail to user" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                         <asp:Button Visible="true" runat="server" ID="btnSubmitNewCase" Text ="Submit Case" />
                                         <asp:Button runat="server" ID="btnResetFrom" Text="Clear Form" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                    </asp:Table>

                        
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell ColumnSpan="2">
                                <cc1:Accordion ID="Accordion4" CssClass ="closedaccordion" runat="server"         HeaderCssClass="closedaccordionHeader"
                    HeaderSelectedCssClass="closedaccordionHeaderSelected" RequireOpenedPane="false" SelectedIndex="-1">
                      <HeaderTemplate >
                          <asp:Panel ID ="pnlTest" runat ="server">
                          <asp:Label ID="lblCaseNumber" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                          <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("title")%>' />
                              <br />
                          <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("fname")%>' /> --
                          <asp:Label ID="lblOpenDate" runat="server" Text='<%# Eval("start_date")%>' />
                              <br />
                          <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("sname")%>' />
                      </asp:Panel>
                      </HeaderTemplate>
                      <ContentTemplate>
                          <asp:Panel runat="server">
                            Case Submitted by <asp:Label ID="Label2" Font-Size="Small" runat="server" Text='<%# Eval("uid")%>' /><br />
                              Email:  <asp:Label ID="Label3" Font-Size="Small" runat="server" Text='<%# Eval("uemail")%>' /><br />
                              Phone:  <asp:Label ID="Label4" Font-Size="Small" runat="server" Text='<%# Eval("uphone")%>' /><br />
                              Location:  <asp:Label ID="Label5" Font-Size="Small" runat="server" Text='<%# Eval("ulocation")%>' /> <br />
                              ComputerName:  <asp:Label ID="Label6" Font-Size="Small" runat="server" Text='<%# Eval("uComputerName")%>' /><br /><br />

                              Department: <asp:Label ID="Label7" Font-Size="Small" runat="server" Text='<%# Eval("dname")%>' /><br />
                              Category: <asp:Label ID="Label8" Font-Size="Small" runat="server" Text='<%# Eval("cname")%>' /><br /><br />

                              Description:  <asp:Label ID="Label9" Font-Size="Small" runat="server" Text='<%# Eval("description")%>' /><br /><br />
                              Solution:  <asp:Label ID="Label10" Font-Size="Small" runat="server" Text='<%# Eval("solution")%>' /><br /><br />
                              Notes:<br />
                       
                        <asp:Label ID="Label1" Font-Size="Small" runat="server" Text='<%# Eval("note").ToString().Replace(";newline;", "<BR><BR>")%>' />

                            </asp:Panel>
                      </ContentTemplate>
                </cc1:Accordion>

            </asp:TableCell>
        </asp:TableFooterRow>

    </asp:Table>

              <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" >
       <asp:Table runat="server" Width="100%" Height="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
           <asp:TableRow>
               <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                   <asp:Label ID="explantionlabel" runat="server"></asp:Label>
               </asp:TableCell>
               <asp:TableCell Width="10px"></asp:TableCell>
           </asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell></asp:TableCell>
               <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center" >
                   <asp:Table runat="server" Width="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >
                       <asp:TableRow>
                           <asp:TableCell Width="50%" ID="tcOpenAttch" HorizontalAlign="Center" >
                               <asp:Button ID="btnOpenAttach" runat="server" Text="Attach Files" />
                           </asp:TableCell>
                           <asp:TableCell Width="50%" HorizontalAlign="Center" >
                               <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="Close" />
                           </asp:TableCell>
                       </asp:TableRow>
                   </asp:Table>
          
         </asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="Panel1"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

             <asp:Table runat="server" CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" Width="100%" >
                  <asp:TableRow>
                      <asp:TableCell HorizontalAlign="Right">
                            <asp:Button runat="server" Font-Size="Small" Text="Open Instructions"  OnClientClick="open_win3(0)"  />
                      </asp:TableCell>
                  </asp:TableRow>
              </asp:Table>


        </ContentTemplate>
        </asp:UpdatePanel>
                        
</ContentTemplate>
                        
</cc1:TabPanel>
         
    <cc1:TabPanel runat = "server" HeaderText = "View Open Cases" ID = "tpOpenCases" Visible ="false" >
                    <ContentTemplate>   
    
   
    <asp:UpdatePanel runat="server" ID= "UpdatePanel1" ><ContentTemplate>

        <asp:Table runat="server" Width="100%" CssClass="collapsetable">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top" RowSpan="2">
                    <asp:Label runat="server" Text="View open cases for "></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlViewOtherCases" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                    <asp:Panel runat="server" ID="ScrollPanel" ScrollBars="Both" CssClass="MaxPanelHeight" Width="400px" Height="900px" HorizontalAlign="Center" >
        <asp:GridView ID="gvOpenCases" runat="server"  HeaderStyle-BackColor = "#6da9e3" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" 
            RowStyle-BorderColor = "#003060" RowStyle-BorderWidth="1px" AllowSorting ="true" 
            RowStyle-HorizontalAlign = "Center" AutoGenerateColumns="false" 
                HorizontalAlign="Left" Font-Size="Smaller" >
          
                                     <Columns>
                                      <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                      <asp:TemplateField HeaderText = "" >
                                            <ItemTemplate>              
                                                <asp:Panel runat = "server">
                                                    <asp:Image ID="imgDR" Visible="false" runat = "server" ImageUrl="Images/ClipDarkRed.bmp" Width="11px"/>
                                                    <asp:Image ID="imgDO" Visible="false" runat = "server" ImageUrl="Images/ClipDarkOrange.bmp" Width="11px"/>
                                                    <asp:Image ID="imgDY" Visible="false" runat = "server" ImageUrl="Images/ClipDarkYellow.bmp" Width="11px"/>
                                                    <asp:Image ID="imgLR" Visible="false" runat = "server" ImageUrl="Images/ClipLightRed.bmp" Width="11px"/>
                                                    <asp:Image ID="imgLO" Visible="false" runat = "server" ImageUrl="Images/ClipLightOrange.bmp" Width="11px"/>
                                                    <asp:Image ID="imgLY" Visible="false" runat = "server" ImageUrl="Images/ClipLightYellow.bmp" Width="11px"/>
                                                </asp:Panel>                 
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:BoundField DataField="ID" HeaderText="ID"  
                                              SortExpression="ID"></asp:BoundField>
                                      <asp:TemplateField HeaderText = "" >
                                            <ItemTemplate>              
                                                <asp:Panel runat = "server">
                                                    <asp:Image ID="imgDRHR" Visible="false" runat = "server" ImageUrl="Images/HourGlassDarkRed.bmp" Width="11px"/>
                                                    <asp:Image ID="imgDOHR" Visible="false" runat = "server" ImageUrl="Images/HourGlassDarkOrange.bmp" Width="11px"/>
                                                    <asp:Image ID="imgDYHR" Visible="false" runat = "server" ImageUrl="Images/HourGlassDarkYellow.bmp" Width="11px"/>
                                                    <asp:Image ID="imgLRHR" Visible="false" runat = "server" ImageUrl="Images/HourGlassLightRed.bmp" Width="11px"/>
                                                    <asp:Image ID="imgLOHR" Visible="false" runat = "server" ImageUrl="Images/HourGlassLightOrange.bmp" Width="11px"/>
                                                    <asp:Image ID="imgLYHR" Visible="false" runat = "server" ImageUrl="Images/HourGlassLightYellow.bmp" Width="11px"/>
                                                    <asp:Label ID="lblAsterisk" Visible="false" runat="server" Font-Bold="true" ForeColor="#003060">*</asp:Label>
                                                </asp:Panel>                 
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                          <asp:BoundField DataField="Title" HeaderText="Title"  
                                              SortExpression="Title"></asp:BoundField>
                                          
                                          <asp:BoundField DataField="entered_by" HeaderText="User Name"  SortExpression="User Name"></asp:BoundField>
                                         
                                         <%--<asp:BoundField DataField="User Name" HeaderText="User Name" 
                                              SortExpression="User Name"></asp:BoundField>--%>


                                          <asp:BoundField DataField="Date Submitted" HeaderText="Date Submitted" 
                                              SortExpression="dtSubmitted"></asp:BoundField>
                                          <asp:BoundField DataField="Priority" HeaderText="Priority"  HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden"
                                              SortExpression="Priority"></asp:BoundField>
                                          <asp:BoundField DataField="Status" HeaderText="Status"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="Status"></asp:BoundField>
                                          <asp:BoundField DataField="uemail" HeaderText="uemail"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="uemail"></asp:BoundField>
                                          <asp:BoundField DataField="department" HeaderText="department"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="department"></asp:BoundField>
                                          <asp:BoundField DataField="ulocation" HeaderText="ulocation"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="ulocation"></asp:BoundField>
                                          <asp:BoundField DataField="uComputerName" HeaderText="uComputerName"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="uComputerName"></asp:BoundField>
                                          <asp:BoundField DataField="uphone" HeaderText="uphone" ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="uphone"></asp:BoundField>
                                          <asp:BoundField DataField="entered_by" HeaderText="entered_by"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="entered_by"></asp:BoundField>
                                          <asp:BoundField DataField="category" HeaderText="category"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="category"></asp:BoundField>
                                          <asp:BoundField DataField="rep" HeaderText="rep"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="rep"></asp:BoundField>
                                          <asp:BoundField DataField="time_spent" HeaderText="time_spent"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="time_spent"></asp:BoundField>
                                          <asp:BoundField DataField="start_date" HeaderText="start_date"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="start_date"></asp:BoundField>
                                          <asp:BoundField DataField="close_date" HeaderText="close_date"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="close_date"></asp:BoundField>
                                          <asp:BoundField DataField="title2" HeaderText="title"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="title"></asp:BoundField>
                                          <asp:BoundField DataField="description" HeaderText="description"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="description"></asp:BoundField>
                                          <asp:BoundField DataField="solution" HeaderText="solution" ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="solution"></asp:BoundField>
                                          <asp:BoundField DataField="notes" HeaderText="notes"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="notes"></asp:BoundField>
                                         <asp:BoundField DataField="Attachments" HeaderText="Attachments"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="Attachments"></asp:BoundField>
                                         <asp:BoundField DataField="Deadline" HeaderText="Deadline"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="Attachments"></asp:BoundField>
                                         <asp:BoundField DataField="UsrSubmit" HeaderText="UsrSubmit"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="UsrSubmit"></asp:BoundField>
                                      </Columns>

        </asp:GridView>
</asp:Panel>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel runat="server" Width="100%" BackColor="#6da9e3" ForeColor="White" Font-Bold="true" Font-Size="Medium" HorizontalAlign="Center">Update Case</asp:Panel>
                </asp:TableCell>
                </asp:TableRow><asp:TableRow>
                 <asp:TableCell VerticalAlign ="Top">
                <asp:Panel runat="server" BackColor="#CBE3FB">
                    <asp:Table runat="server" CellPadding ="0" CellSpacing ="0" CssClass ="collapsetable"><asp:TableRow><asp:TableCell VerticalAlign="Top">
                    <asp:Table runat="server" ID="Table1" CellPadding ="0" CellSpacing ="0"  CssClass ="collapsetable">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6"><b>Contact Information</b></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>User Name: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblUpdateUserName" Visible="false"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlUpdateUser"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>E-Mail: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtUpdateEmail"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Department: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlUpdateDept" AppendDataBoundItems ="true">
                                     <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>IP Address: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox runat="server" ID="txtUpdateIPAdd"></asp:Textbox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Computer Name: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox runat="server" ID="txtUpdateCompName"></asp:Textbox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>Phone: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox runat="server" ID="txtUpdatePhone"></asp:Textbox>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>Entered By: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblUpdateEntered"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell> </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell Width="3px" BackColor="White"></asp:TableCell>
                        <asp:TableCell>
                             <asp:Table runat="server" >
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6"><b>Classification</b></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="1px"></asp:TableCell>
                            <asp:TableCell>Category: </asp:TableCell>
                            <asp:TableCell Width="2px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlUpdateCategory" AppendDataBoundItems ="true" >
                                    <asp:ListItem Value="null" Text="Select Category"></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell Width="1px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Status: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlUpdateStatus" >
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Priority: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlUpdatePriority" >
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Assign To: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlUpdateRep"  >
                                    
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Deadline: </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtUpdateDeadline" ></asp:TextBox>
                            <cc1:calendarextender ID="CalendarExtender4" 
                                runat="server" TargetControlID="txtUpdateDeadline" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
                            </asp:TableCell>
                            </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Time Spent: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Textbox runat="server" ID="txtUpdateTime" Width ="80px"></asp:Textbox>&nbsp;(minutes)
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Start Date: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell >
                                <asp:Label runat="server" ID="lblUpdateStartDate" ></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>Close Date: </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblUpdateEndDate" ></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                             </asp:Table></asp:TableCell>
                      </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">Case Information</asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell ColumnSpan ="3">
                                     <asp:Table runat="server" Width="100%">
                                         <asp:TableRow HorizontalAlign="Center">
                                             <asp:TableCell></asp:TableCell>
                                             <asp:TableCell>Title:&nbsp;&nbsp;<br /><asp:TextBox runat="server" ID="txtUpdateTitle" Width="90%"></asp:TextBox>&nbsp;
                                                 </asp:TableCell>
                                             <asp:TableCell ForeColor="Red"><asp:Label runat="server" ForeColor="Red">*</asp:Label></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="2"  HorizontalAlign ="Center">Description:<asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="2"  HorizontalAlign ="Center"><asp:TextBox runat="server" TextMode = "MultiLine"  Rows="5" Width="90%" ID="txtUpdateDesc"></asp:TextBox></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                          <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">Notes:</asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center"><asp:Label runat="server" Width ="90%" ID="lblUpdateNotes"></asp:Label></asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                          <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">Enter Additional Notes:</asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center"><asp:TextBox runat="server" TextMode = "MultiLine"  Rows="5" Width ="90%" ID="txtUpdateNewNotes"></asp:TextBox></asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                         <asp:TableRow><asp:TableCell HorizontalAlign="Center"><asp:CheckBox ID="chbHideNotes" runat="server" Text="Hide From End User" Checked="false" /></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                                         <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">Solution:</asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center"><asp:TextBox runat="server" TextMode = "MultiLine"  Rows="5" Width ="90%" ID="txtUpdateSolution"></asp:TextBox></asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                         <asp:CheckBox runat="server" Checked="false" ID="chbKB" Text ="Enter in Knowledge Base" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                         <asp:CheckBox runat="server" Checked="false" ID="chbDontEmailUser" Text ="Don't send email to user" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">

                                           <asp:Table runat="server" Width="50%" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >
                                               <asp:TableRow>
                                                   <asp:TableCell Width="50%" ID="tcViewAttch" HorizontalAlign="Center" >
                                                       <asp:Button ID="btnViewAttach" runat="server" Text="View Attachments" />
                                                   </asp:TableCell>
                                                   <asp:TableCell Width="50%" HorizontalAlign="Center" >
                                                       <asp:Button runat="server" ID="btnUpdateCase" Text ="Save Case" />
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                               <asp:TableRow>
                                                    <asp:TableCell Height="20px">
                                                    </asp:TableCell>
                                               </asp:TableRow>
                                           </asp:Table>                                         
                                        
                                     </asp:TableCell>
                                 </asp:TableRow>
 
                    </asp:Table>

                        
                </asp:Panel>
            </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

                     <asp:Label ID="FakeButton2" runat = "server" />
   <asp:Panel ID="Panel2" runat="server" Width="233px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explanationlabel2" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                 TargetControlID ="FakeButton2"
                 PopupControlID="Panel2"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


              <asp:Table runat="server" CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" Width="100%" >
                  <asp:TableRow>
                      <asp:TableCell HorizontalAlign="Right">
                            <asp:Button runat="server" Font-Size="Small" Text="Open Instructions"  OnClientClick="open_win3(1)"  />
                      </asp:TableCell>
                  </asp:TableRow>
              </asp:Table>


        </ContentTemplate>
        </asp:UpdatePanel>
                        
</ContentTemplate>
        
</cc1:TabPanel>

               <cc1:TabPanel runat = "server" HeaderText = "Search Cases" ID = "tpSearchCases" Visible ="true" >
                    <ContentTemplate>   
    
   
    <asp:UpdatePanel runat="server" ID= "UpdatePanel3" ><ContentTemplate>

	<asp:Panel runat = "server" ID = "pnlEasySearch">
		    <asp:Table runat="server" Width="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >
                <asp:TableRow>
                    <asp:TableCell Width="100px" >
                        <asp:TextBox runat="server" ID="txtEasySearch" Width="130px"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="5px" >
                    </asp:TableCell>
                    <asp:TableCell >
                        <asp:Button runat="server" ID="btnEasySearch" Text="Search Keywords" />
                    </asp:TableCell>
                    <asp:TableCell Width="5px" >
                    </asp:TableCell>
                    <asp:TableCell >
                        <asp:LinkButton Visible="true" ID="lbAdvSearch" runat="server" Text="Adv" Font-Size="X-Small"></asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell Width="600px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right">
                        <asp:TextBox runat="server" ID="txtSpecificID" onkeypress="return isNumberKey(event)" Width="70px"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="5px" >
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" >
                        <asp:Button runat="server" ID="btnSpecificID" Text ="View Case Number" OnClientClick="open_win2()"/>
                    </asp:TableCell>
                </asp:TableRow>
		    </asp:Table>

	</asp:Panel>
	<asp:Panel runat = "server" ID = "pnlAdvSearch" visible="false">
        
	    <asp:Table runat="server" Width="500px" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Right">
                    <asp:LinkButton Visible="true" ID="lbResetAdv" runat="server" Text="Reset Adv" Font-Size="X-Small"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton Visible="true" ID="lbCloseAdv" runat="server" Text="Close Adv" Font-Size="X-Small"></asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" Font-Size="Larger" BackColor="#6da9e3" ForeColor="White" >
                    Case Specifications
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>                
                <asp:TableCell BackColor="#CBE3FB">
                    <asp:Panel runat="server" BackColor="#CBE3FB" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
                        <asp:TableRow>
                            <asp:TableCell Width="50px"></asp:TableCell>
                                  <asp:TableCell VerticalAlign="Top">
                    Submitted By:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchReportedBy" AppendDataBoundItems="false"  AutoPostBack="true"></asp:DropDownList>
                    <br />
                    <asp:TextBox Width="250px" runat="server" Visible="false" ID="txtAdvSearchReportedBy"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell >
                <asp:TableCell VerticalAlign="Top">
                    Entered By:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                     <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchSubmittedBy" AppendDataBoundItems="false" AutoPostBack="true"></asp:DropDownList>
                    <br />
                    <asp:TextBox Width="250px" runat="server" Visible="false" ID="txtAdvSearchSubmittedBy"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    Assigned To:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchAssignedTo" AppendDataBoundItems="false"  AutoPostBack="true"></asp:DropDownList>
                    <br />
                    <asp:TextBox Width="250px" runat="server" Visible="false" ID="txtAdvSearchAssignedTo"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Category:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchCategory" AppendDataBoundItems="true">
                        <asp:ListItem Text="(Optional)" Value=" -- (none selected) -- "></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Department:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchDepartment" AppendDataBoundItems="true">
                         <asp:ListItem Text="(Optional)" Value=" -- (none selected) -- "></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Status:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchStatus" AppendDataBoundItems="true">
                         <asp:ListItem Text="(Optional)" Value=" -- (none selected) -- "></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Priority:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList Width="250px" runat="server" ID="ddlAdvSearchPriority" AppendDataBoundItems="true">
                        <asp:ListItem Text="(Optional)" Value=" -- (none selected) -- "></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                </asp:TableCell>          
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" Font-Size="Larger" BackColor="#6da9e3" ForeColor="White" >Contains</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BackColor="#CBE3FB">
                     <asp:Panel runat="server" BackColor="#CBE3FB" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Width="50px"></asp:TableCell>
                <asp:TableCell>Keywords:</asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtAdvSearchKeywords"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell VerticalAlign="Top">Search Fields:</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    <asp:CheckBoxList runat="server" ID="cblAdvSearchKeywords">
                        <asp:ListItem Text="Title" Value="Title" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Description" Value="Description"  Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Notes" Value="Notes"  Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Solution" Value="Solution"  Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Attachment Name" Value="Attach" Selected="False"></asp:ListItem>
                    </asp:CheckBoxList>
                </asp:TableCell>
            </asp:TableRow>
                    </asp:Table>
                         </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" Font-Size="Larger" BackColor="#6da9e3" ForeColor="White" >
                    Dates:
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" BackColor="#CBE3FB">
                     <asp:Panel runat="server" BackColor="#CBE3FB" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                    <asp:DropDownList runat="server" ID="ddlAdvSearchDateOptions" AutoPostBack="true">
                        <asp:ListItem Text="All Dates" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Open Between" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Start Date Between" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Close Date Between" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="trAdvSearchDates" Visible="false">
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0" Width="100%" >
                        <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtAdvSearchStartDate"></asp:TextBox>
                    <cc1:calendarextender ID="CalendarExtender1" 
                                runat="server" TargetControlID="txtAdvSearchStartDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
                </asp:TableCell>
                <asp:TableCell Width="5px">
                </asp:TableCell>
                <asp:TableCell>and</asp:TableCell>
                <asp:TableCell Width="5px">
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtAdvSearchEndDate"></asp:TextBox>
                    <cc1:calendarextender ID="CalendarExtender2" 
                                runat="server" TargetControlID="txtAdvSearchEndDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                              </cc1:calendarextender> 
                </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
                    </asp:Table>
                         </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" Font-Size="Larger" BackColor="#6da9e3" ForeColor="White" >
                    Additional:
                </asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" BackColor="#CBE3FB">
                     <asp:Panel runat="server" BackColor="#CBE3FB" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    <asp:CheckBoxList runat="server" ID="chblAdvSearchAddtnl">
                        <asp:ListItem Text="Has Note(s)" Selected="False"></asp:ListItem>
                        <asp:ListItem Text="Has Attachment(s)" Selected="False"></asp:ListItem>
                    </asp:CheckBoxList>
                </asp:TableCell>
            </asp:TableRow>
                </asp:Table>
                         </asp:Panel>
                    </asp:TableCell>
                            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="btnAdvSearch" Text="Advanced Search" />
                </asp:TableCell>
            </asp:TableRow>
	    </asp:Table>

	</asp:Panel>
	
	<asp:Panel runat = "server" ID = "pnlSearchResults" visible = "false">
        <asp:Table runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top" RowSpan="2">
                    <asp:Panel runat="server" ID="ScrollPanelp3" ScrollBars="Vertical" CssClass="MaxPanelHeight" Width="400px" Height="700px" HorizontalAlign="Center" >
        <asp:GridView ID="gvSearchedCases" runat="server"  HeaderStyle-BackColor = "#6da9e3" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" AllowPaging="true" AllowSorting ="true"
            RowStyle-BorderColor = "#003060" RowStyle-BorderWidth="1px" PageSize="50"
            RowStyle-HorizontalAlign = "Center" AutoGenerateColumns="false" 
                HorizontalAlign="Left" Font-Size="Smaller" >
          
                                     <Columns>
                                      <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                          <asp:BoundField DataField="ID" HeaderText="ID"   HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden"
                                              SortExpression="ID"></asp:BoundField>
                                          <asp:BoundField DataField="Title" HeaderText="Title"  
                                              SortExpression="Title"></asp:BoundField>
                                          <asp:BoundField DataField="username" HeaderText="User Name" 
                                              SortExpression="username"></asp:BoundField>
                                          <asp:BoundField DataField="Date Submitted" HeaderText="Date Submitted" 
                                              SortExpression="dtSubmitted"></asp:BoundField>
										  <asp:BoundField DataField="Date Closed" HeaderText="Date Closed" 
                                              SortExpression="dtClosed"></asp:BoundField>
                                          <asp:BoundField DataField="Priority" HeaderText="Priority"  HeaderStyle-CssClass ="hidden" ItemStyle-CssClass ="hidden"
                                              SortExpression="Priority"></asp:BoundField>
                                          <asp:BoundField DataField="Status" HeaderText="Status"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="Status"></asp:BoundField>
                                          <asp:BoundField DataField="uemail" HeaderText="uemail"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="uemail"></asp:BoundField>
                                          <asp:BoundField DataField="department" HeaderText="department"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="department"></asp:BoundField>
                                          <asp:BoundField DataField="ulocation" HeaderText="ulocation"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="ulocation"></asp:BoundField>
                                          <asp:BoundField DataField="uComputerName" HeaderText="uComputerName"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="uComputerName"></asp:BoundField>
                                          <asp:BoundField DataField="uphone" HeaderText="uphone" ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="uphone"></asp:BoundField>
                                          <asp:BoundField DataField="entered_by" HeaderText="entered_by"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="entered_by"></asp:BoundField>
                                          <asp:BoundField DataField="category" HeaderText="category"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="category"></asp:BoundField>
                                          <asp:BoundField DataField="fname" HeaderText="rep"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="fname"></asp:BoundField>
                                          <asp:BoundField DataField="time_spent" HeaderText="time_spent"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="time_spent"></asp:BoundField>
                                          <asp:BoundField DataField="start_date" HeaderText="start_date"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="start_date"></asp:BoundField>
                                          <asp:BoundField DataField="close_date" HeaderText="close_date"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="close_date"></asp:BoundField>
                                          <asp:BoundField DataField="title2" HeaderText="title"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="title"></asp:BoundField>
                                          <asp:BoundField DataField="description" HeaderText="description"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="description"></asp:BoundField>
                                          <asp:BoundField DataField="solution" HeaderText="solution" ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="solution"></asp:BoundField>
                                          <asp:BoundField DataField="note" HeaderText="note"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="note"></asp:BoundField>
                                          <asp:BoundField DataField="sname" HeaderText="sname"  ItemStyle-CssClass ="hidden" HeaderStyle-CssClass ="hidden"
                                              SortExpression="sname"></asp:BoundField>
                                      </Columns>

        </asp:GridView>
</asp:Panel>
                 </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel runat="server" Height="100%" Width="100%" Font-Size="Larger" BackColor="#6da9e3" ForeColor="White" HorizontalAlign="Center">Case Information</asp:Panel>
                </asp:TableCell>
                </asp:TableRow>
            <asp:TableRow>
                 <asp:TableCell VerticalAlign ="Top">
                <asp:Panel runat="server" BackColor="#CBE3FB" ID="pnlSelectedResults">
                    <asp:Table runat="server" CellPadding ="0" CellSpacing ="0" CssClass ="collapsetable">
                       <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                         Case Number 
                                         <asp:Label runat="server" ID="lblSearchCaseNumber"></asp:Label>
                                     </asp:TableCell>
                                 </asp:TableRow>
                        <asp:TableRow>
                        <asp:TableCell VerticalAlign="Top" Width="355px" >
                    <asp:Table runat="server" ID="Table2" CellPadding ="0" CellSpacing ="0" CssClass ="collapsetable" >
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6"><b>User Information</b></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>User Name: </b></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchUserName"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>E-Mail: </b></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchEmail"> </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Department: </b></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchDept" >
                                </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell><b>Phone: </b></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchPhone"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell><b>Entered By: </b></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchEntered"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell> </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell Width="3px" BackColor="White"></asp:TableCell>
                        <asp:TableCell Width="355px" >
                             <asp:Table Width="355px" runat="server" CellPadding ="0" CellSpacing ="0" CssClass ="collapsetable">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6"><b>Classification</b></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="1px"></asp:TableCell>
                            <asp:TableCell><b>Category: </b></asp:TableCell>
                            <asp:TableCell Width="2px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchCategory" >
                                </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell Width="1px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Status: </b></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchStatus" >
                                </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Priority: </b></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchPriority" >
                                </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Assigned To: </b></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchRep" >
                                </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Time Spent: </b></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblSearchTime" ></asp:Label>&nbsp;(minutes)
                            </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Start Date: </b></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell >
                                <asp:Label Width="100%" runat="server" ID="lblSearchStartDate" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Close Date: </b></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell >
                                <asp:Label runat="server" ID="lblSearchEndDate" ></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell Height="3px"></asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table></asp:TableCell>
                      </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" Width="703px" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell ColumnSpan ="3">
                                     <asp:Table runat="server" Width="100%">
                                         <asp:TableRow HorizontalAlign="Center">
                                             <asp:TableCell></asp:TableCell>
                                             <asp:TableCell><b>Title:&nbsp;&nbsp;</b><asp:Label runat="server" ID="lblSearchTitle" ></asp:Label>&nbsp;
                                                 <asp:Label runat="server" ForeColor="Red"></asp:Label></asp:TableCell>
                                             <asp:TableCell ForeColor="Red"></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="3"  HorizontalAlign ="Center"><b>Description:</b><asp:Label runat="server" ForeColor="Red" Text=""></asp:Label></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="3"  HorizontalAlign ="Center">
												<asp:Label runat="server"  Width="90%" ID="lblSearchDesc"></asp:Label>
											 </asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                          <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" Width="703px" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"><b>Notes:</b></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center">
											 <asp:Label runat="server" Width ="90%" ID="lblSearchNotes"></asp:Label></asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                                         <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" Width="703px" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"><b>Solution:</b></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center">
												<asp:Label runat="server" Width ="90%" ID="lblSearchSolution"></asp:Label>
											</asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                         <asp:TableRow><asp:TableCell HorizontalAlign="Center">
                                             <asp:Button runat="server" ID="btnSearchReopenCase"  Font-Size="Small"  Visible="false" Text="Re-Open Case" /> 
                                                       </asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>

                    </asp:Table>

                        
                </asp:Panel>
            </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
		</asp:Panel>

                     <asp:Label ID="FakeButtonp3" runat = "server" />
   <asp:Panel ID="Panelp3" runat="server" Width="233px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                    <asp:label ID = "explanationlabelp3" runat = "server"></asp:label> 
               </asp:TableCell>
               <asp:TableCell Width="10px"></asp:TableCell> 
           </asp:TableRow>
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
                    <asp:Button ID="OkButtonp3" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
               </asp:TableCell>
           </asp:TableRow>        
           <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtenderp3" runat="server"
                 TargetControlID ="FakeButtonp3"
                 PopupControlID="Panelp3"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

              <asp:Table runat="server" CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" Width="100%" >
                  <asp:TableRow>
                      <asp:TableCell HorizontalAlign="Right">
                            <asp:Button runat="server" Font-Size="Small" Text="Open Instructions"  OnClientClick="open_win3(2)"  />
                      </asp:TableCell>
                  </asp:TableRow>
              </asp:Table>


        </ContentTemplate>
        </asp:UpdatePanel>
                        
</ContentTemplate>
        
</cc1:TabPanel>

               <cc1:TabPanel runat = "server" HeaderText = "Administrative" ID = "tpAdministrative" Visible ="false">
                    <ContentTemplate>   
    
    <asp:UpdatePanel runat="server" ID= "UpdatePanelp4" ><ContentTemplate>
        <asp:Panel runat="server">
            <asp:DropDownList runat="server" ID="ddlManageWhat" AutoPostBack="true" >
                <asp:ListItem Text="Manage Categories" Value="1"></asp:ListItem>
                <asp:ListItem Text="Manage Departments" Value="2"></asp:ListItem>
                <asp:ListItem Text="Manage Users" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </asp:Panel>

                 <asp:Panel ID="pnlDeps" runat = "server"  Visible="false" >

                                            <asp:Table runat="server">
                           <asp:TableRow>
                               <asp:TableCell>
                                    Department:
                               </asp:TableCell>
                               <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtNewDepName" ></asp:TextBox>
                               </asp:TableCell>
                           </asp:TableRow>
                           <asp:TableRow>
                               <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                    <asp:Button runat="server" ID="btnNewDep" Text="Add New Department" />
                               </asp:TableCell>
                           </asp:TableRow>
                       </asp:Table>

                     
                     
                     <asp:Panel runat="server" ID="ScrollPanelDeps" ScrollBars = "Auto" CssClass="AdminMaxPanelHeight">
                         <asp:Table runat="server">
                             <asp:TableFooterRow>
                                 <asp:TableCell>

    <asp:GridView ID="gvDepartments" runat="server"
           AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A" 
         HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames="department_id" 
         BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0"
            >   
        <AlternatingRowStyle BackColor="white"  />
                
        <Columns>

           <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                ShowEditButton="true" ShowSelectButton = "true" SelectText = "" >
            <HeaderStyle Width="55px" />
            </asp:CommandField>

            <asp:TemplateField HeaderText = "" >
                <ItemTemplate>              
                <asp:Panel runat = "server">
                    <asp:LinkButton ID="btnActivate" runat = "server" Text='<%# Bind("ActiveStatus")%>' CommandName="Delete"></asp:LinkButton>
                </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="department_id" HeaderStyle-HorizontalAlign="Left" 
                ControlStyle-Width="80px" HeaderStyle-Wrap="true" 
                HeaderText="Department ID" ReadOnly="True" SortExpression="department_id" 
                Visible="False">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            <ItemStyle Width = "80px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText = "Department" SortExpression ="dname" AccessibleHeaderText = "Dept"  >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest" Width = "95%" runat = "server">
                    <asp:Label ID="lblDept" runat = "server" Text='<%# Bind("dname")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtDept" Width = "95%" runat = "server" Text='<%# Bind("dname")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>


            </Columns>
        </asp:GridView>
                                                                      </asp:TableCell>
                                 <asp:TableCell VerticalAlign="Top">

                          <asp:CheckBox runat="server" ID="chkInactiveDeps" AutoPostBack="true" Text="Show Inactive Departments" Checked="false" />
                                                                     </asp:TableCell>
                             </asp:TableFooterRow>
                         </asp:Table>
                          </asp:Panel>
                     </asp:Panel>
        
        <asp:SqlDataSource ID="dsCatReps" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="select  sid, uid as userid, fname from WebFD.FinanceHelpDesk.tblUsers
                where IsRep = 1 order by fname "></asp:SqlDataSource>

                   <asp:Panel ID="pnlCats" runat = "server"  >
                       <asp:Table runat="server">
                           <asp:TableRow>
                               <asp:TableCell>
                                    Category:
                               </asp:TableCell>
                               <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtNewCatName" ></asp:TextBox>
                               </asp:TableCell>
                           </asp:TableRow>
                           <asp:TableRow>
                               <asp:TableCell>
                                    Rep:
                               </asp:TableCell>
                               <asp:TableCell>
                                    <asp:DropDownList runat="server" ID="ddlNewCatRep" 
                                        DataSourceID="dsCatReps" DataTextField="fname" DataValueField="sid"></asp:DropDownList>
                               </asp:TableCell>
                           </asp:TableRow>
                           <asp:TableRow>
                               <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                    <asp:Button runat="server" ID="btnAddCat" Text="Add New Category" />
                               </asp:TableCell>
                           </asp:TableRow>
                       </asp:Table>
                      
                      
                     <br /><br />
                       Filter Reps: <asp:DropDownList runat="server" ID="ddlCatReps" AutoPostBack="true"
                                        ></asp:DropDownList>
                     <asp:Panel runat="server" ID="ScrollPanelCats" ScrollBars = "Auto" CssClass="AdminMaxPanelHeight">
                         <asp:Table runat="server">
                             <asp:TableRow><asp:TableCell>

    <asp:GridView ID="gvCategories" runat="server"
           AllowSorting="False" AutoGenerateColumns="False" BorderColor="Black" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A" 
         HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Wrap="true"  ForeColor="Black" DataKeyNames="category_id" 
         BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0"
            >   
        <AlternatingRowStyle BackColor="white"  />
                
        <Columns>

           <asp:CommandField ItemStyle-Width="55px"  UpdateText="Update<br>"
                ShowEditButton="true" ShowSelectButton = "true" SelectText = "" >
            <HeaderStyle Width="55px" />
            </asp:CommandField>
            <asp:TemplateField HeaderText = "" >
                <ItemTemplate>              
                <asp:Panel runat = "server">
                    <asp:LinkButton ID="btnActivate" runat = "server" Text='<%# Bind("ActiveStatus")%>' CommandName="Delete"></asp:LinkButton>
                </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="category_id" HeaderStyle-HorizontalAlign="Left" 
                ControlStyle-Width="80px" HeaderStyle-Wrap="true" 
                HeaderText="Category ID" ReadOnly="True" SortExpression="category_id" 
                Visible="False">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            <ItemStyle Width = "80px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText = "Category" SortExpression ="cname" AccessibleHeaderText = "Cat" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest" Width = "95%" runat = "server">
                    <asp:Label ID="lblCat" runat = "server" Text='<%# Bind("cname")%>'></asp:Label>
                </asp:Panel>
                    <asp:TextBox ID="txtCat" Width = "95%" runat = "server" Text='<%# Bind("cname")%>' Visible = "false" ></asp:TextBox>
                
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText = "Rep" SortExpression ="fname" AccessibleHeaderText = "Rep" >
                <ItemTemplate>              
                <asp:Panel CssClass = "paneltest" Width = "95%" runat = "server">
                    <asp:Label ID="lblRep" runat = "server" Text='<%# Bind("fname")%>'></asp:Label>
                </asp:Panel>
                      <asp:DropDownList ID="ddlRep" Width = "95%" DataTextField="fname" DataValueField="sid" runat = "server" SelectedValue='<%# Bind("rep_id")%>' 
                        Visible = "false" DataSourceID ="dsCatReps" >
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>


            </Columns>
        </asp:GridView>

                                 
                                           </asp:TableCell>
                                 <asp:TableCell VerticalAlign="Top">
                                     <asp:CheckBox runat="server" ID="ChkActiveCategories" Text="Show Inactive Categories" Checked="false" AutoPostBack="true" />
                                 </asp:TableCell>

                             </asp:TableRow>
                         </asp:Table>
                     </asp:Panel>
                       </asp:Panel>

         <asp:UpdatePanel ID="pnlUsers" runat="server" Visible="False">
                            <ContentTemplate>
        <asp:SqlDataSource ID="SqlDSManageUsers" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="select * from WebFD.FinanceHelpDesk.tblUsers order by fname "></asp:SqlDataSource>


                               <%-- <table><tr><td>--%>
                                    <div style="overflow-x:auto;width:1124px">
                                                         <asp:Table ID="Search_Users" runat="server" BackColor="#2b74bb" BorderColor="#003060" BorderWidth="1px" CellSpacing="2" ForeColor="White" HorizontalAlign="Center" Visible="true" Width="87%">
                                                             <asp:TableRow>
                                                                 <asp:TableCell>Search:</asp:TableCell>
                                                                 <asp:TableCell HorizontalAlign="Left"> <asp:TextBox runat = "server" ID= "tbl_Search_UserID" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
                                                                 <asp:TableCell HorizontalAlign="Left"> <asp:TextBox runat = "server" ID= "tbl_Search_FirstName" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
                                                                 <asp:TableCell HorizontalAlign="Left"> <asp:TextBox runat = "server" ID= "tbl_Search_LastName" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
                                                             </asp:TableRow>
                                                             <asp:TableHeaderRow BackColor="#4A8fd2" ForeColor="White">
                                                                 <asp:TableHeaderCell ID="tbl_tblTSelect" Height="20px" Width="100px"></asp:TableHeaderCell>
                                                                 <%--<asp:TableHeaderCell ID = "tblTEdit" Visible = "false" Width = "60px" Height = "20px" >&nbsp;</asp:TableHeaderCell>--%>
                                                                 
                                                                 <asp:TableHeaderCell Width="130px">UserID</asp:TableHeaderCell>
                                                                 <asp:TableHeaderCell Width="130px">First Name</asp:TableHeaderCell>
                                                                 <asp:TableHeaderCell Width="185px">Last Name</asp:TableHeaderCell>
                                                             </asp:TableHeaderRow>
                                                         </asp:Table>

                                        <%--DataSourceID="SqlDSManageUsers"    TAKEN OUT--%>

        <style type="text/css">
            .ManageUsersCSS {padding: 10px;}
        </style>


                                                         <asp:GridView ID="gvManageUsers" runat="server" AutoGenerateColumns="False" BorderColor="Black" BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A" 
                            HeaderStyle-ForeColor="white" Width="120%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="ManageUsersCSS" HeaderStyle-Wrap="true" CellSpacing="2" ForeColor="Black" BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" DataKeyNames="sid"  AllowPaging="True" PageSize="20" HorizontalAlign="Center">   
                                <AlternatingRowStyle BackColor="white"   />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True"/>
                                        <asp:BoundField DataField="sid" HeaderStyle-Width="5%" HeaderText="sid" ReadOnly="True" SortExpression="sid" />
                                        <asp:BoundField DataField="uid" HeaderText="userid" HeaderStyle-Width="10%" SortExpression="userid" />
                                        <asp:BoundField HeaderStyle-Width="10%" DataField="firstname" HeaderText="First Name" />
                                        <asp:BoundField HeaderStyle-Width="10%" DataField="lastname" HeaderText="Last Name" />
                                        <asp:BoundField HeaderStyle-Width="20%" DataField="email1" HeaderText="Email Address" />
                                        <asp:BoundField HeaderStyle-Width="20%" DataField="phone" HeaderText="Phone Number" />
                                        <asp:BoundField HeaderStyle-Width="10%" DataField="location1" HeaderText="IP Address" />
                                        <asp:BoundField HeaderStyle-Width="10%" DataField="location2" HeaderText="Computer Name" />
                                        <asp:BoundField HeaderStyle-Width="5%" DataField="department" HeaderText="Department" />
                                        <asp:BoundField HeaderStyle-Width="5%" DataField="isrep" HeaderText="Is Rep?"  HeaderStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderStyle-Width="5%" DataField="Active" HeaderText="Active"  HeaderStyle-HorizontalAlign="Center"/>
                                    </Columns>
                                                             <HeaderStyle BackColor="#214B9A" ForeColor="White" HorizontalAlign="Left" Wrap="True" />
                                </asp:GridView>
                                        </div>
                                           </td>
                                    <td>

                                <asp:Table ID="tblEditUserProfile" runat="server" CellPadding="0" CellSpacing="0" CssClass="collapsetable" Width="500px" HorizontalAlign="Center" Visible="True">
                                    <asp:TableRow>
                                        <asp:TableCell BackColor="#6da9e3" ColumnSpan="5" Font-Size="Larger" ForeColor="White" HorizontalAlign="Center">
                    User Profile
                </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BackColor="#CBE3FB">
                    <asp:Panel runat="server" BackColor="#CBE3FB" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
                        <asp:TableRow>
                            <asp:TableCell Width="40px"></asp:TableCell>
                                  <asp:TableCell VerticalAlign="Top">
                    User Login:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="lblUsrLogin0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ForeColor ="Red"></asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell >
                <asp:TableCell VerticalAlign="Top">
                    First Name:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrFirstName0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    Last Name:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrLastName0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    E-Mail Address:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrEmail0"></asp:TextBox>
                </asp:TableCell>                
                <asp:TableCell ForeColor ="Red">*</asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Phone Number:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrPhone0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    IP Address:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrIPAddress0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Computer Name:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrCompName0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Department:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrDepartment"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>      
                 <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Is Rep:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:checkbox Width="250px" runat="server" ID="chkbx_is_rep"></asp:checkbox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>                   
                  <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Active:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:checkbox Width="250px" runat="server" ID="chkbx_is_active"></asp:checkbox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>                                        
                    </asp:Table>
                    </asp:Panel>
                </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell BackColor="#CBE3FB" Height="5px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell runat="server" ColumnSpan="5" HorizontalAlign="Center"><asp:Button runat="server" Text="Add or Update Profile" ID="btnAddorUpdateProfile"></asp:Button>
                                    </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                                   <%-- </td></tr></table> '' Puts gridviews side by side--%>
                                                                            
                                         
                            </ContentTemplate>
        </asp:UpdatePanel>

                        <asp:Label ID="FakeButtonp4" runat = "server" />
   <asp:Panel ID="Panelp4" runat="server" Width="233px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                    <asp:label ID = "explanationlabelp4" runat = "server"></asp:label> 
               </asp:TableCell>
               <asp:TableCell Width="10px"></asp:TableCell> 
           </asp:TableRow>
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow>
               <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
                    <asp:Button ID="OkButtonp4" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
               </asp:TableCell>
           </asp:TableRow>        
           <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtenderp4" runat="server"
                 TargetControlID ="FakeButtonp4"
                 PopupControlID="Panelp4"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

              <asp:Table runat="server" CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" Width="100%" >
                  <asp:TableRow>
                      <asp:TableCell HorizontalAlign="Right">
                            <asp:Button runat="server" Font-Size="Small" Text="Open Instructions"  OnClientClick="open_win3(3)"  />
                      </asp:TableCell>
                  </asp:TableRow>
              </asp:Table>

        
</ContentTemplate>
</asp:UpdatePanel>

                        
</ContentTemplate>
                   
</cc1:TabPanel>
                <cc1:TabPanel runat = "server" HeaderText = "User Profile" ID = "tpUserProfile" Visible ="true">
                    <ContentTemplate>   
    
   
    <asp:UpdatePanel runat="server" ID= "UpdatePanel2" ><ContentTemplate>
            <asp:Table runat="server" Width="500px" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >

            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" Font-Size="Larger" BackColor="#6da9e3" ForeColor="White" >
                    User Profile
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>                
                <asp:TableCell BackColor="#CBE3FB">
                    <asp:Panel runat="server" BackColor="#CBE3FB" >
                    <asp:Table runat="server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
                        <asp:TableRow>
                            <asp:TableCell Width="40px"></asp:TableCell>
                                  <asp:TableCell VerticalAlign="Top">
                    User Login:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblUsrLogin"></asp:Label>
                </asp:TableCell>
				<asp:TableCell ForeColor ="Red"></asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell >
                <asp:TableCell VerticalAlign="Top">
                    First Name:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrFirstName"></asp:TextBox>
                </asp:TableCell>
				<asp:TableCell ForeColor ="Red">*</asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    Last Name:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrLastName"></asp:TextBox>
                </asp:TableCell>
				<asp:TableCell ForeColor ="Red">*</asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    E-Mail Address:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
					<asp:TextBox Width="250px" runat="server" ID="txtUsrEmail"></asp:TextBox>
                </asp:TableCell>				
				<asp:TableCell ForeColor ="Red">*</asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Phone Number:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
					<asp:TextBox Width="250px" runat="server" ID="txtUsrPhone"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    IP Address:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrIPAddress"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Computer Name:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox Width="250px" runat="server" ID="txtUsrCompName"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell>
                    Department:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddlUsrDepartment" AppendDataBoundItems ="true">
                        <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
                        </asp:TableRow>						
                    </asp:Table>
                    </asp:Panel>
                </asp:TableCell>          
            </asp:TableRow>
                  <asp:TableRow>                
                <asp:TableCell BackColor="#CBE3FB" Height="5px"></asp:TableCell>
                      </asp:TableRow>
<%--            <asp:TableRow>
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="btnUpdateProfile" Text="Update Profile" />
                </asp:TableCell>
            </asp:TableRow>--%>
	    </asp:Table>

 

        </ContentTemplate>
        </asp:UpdatePanel>
                        <asp:Table runat="server">
                                        <asp:TableRow runat="server">
                <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" runat="server"><asp:Button runat="server" ID="btnUpdateProfile" Text="Update Profile" />
</asp:TableCell>
            </asp:TableRow>
                        </asp:Table>

           <asp:Table runat="server" CssClass="supercollapsetable" CellPadding="0" CellSpacing="0" Width="100%" >
                  <asp:TableRow runat="server">
                      <asp:TableCell HorizontalAlign="Right" runat="server"><asp:Button runat="server" Font-Size="Small" Text="Open Instructions"  OnClientClick="open_win3(4)"  />
</asp:TableCell>
                  </asp:TableRow>
              </asp:Table>

                        
</ContentTemplate>
                   
</cc1:TabPanel>

                   </cc1:tabcontainer>
</asp:Content>
