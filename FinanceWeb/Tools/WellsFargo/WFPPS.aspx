<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="WFPPS.aspx.vb" MasterPageFile="~/Site.Master"  Inherits="FinanceWeb.WFPPS" %>
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
        
        .Linking
        {
            font-size:small;
            color:#003060;

         }
        .Linking:hover {
    color: #6da9e3;
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

    <script>
        function open_win() {
            window.open("https://financeweb.northside.local/Tools/WellsFargo/WFPPSInstructions.aspx", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
        }
        function open_win2() {
            window.open("https://financeweb.northside.local/Tools/WellsFargo/WFPPSInstructions.aspx?t=2", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
        }
        function open_win3() {
            window.open("https://financeweb.northside.local/Tools/WellsFargo/WFPPSInstructions.aspx?t=1", "ProFeeInstructions", "height=768,width=900, scrollbars, resizable");
        }


    </script>

       <cc1:tabcontainer ID="WFPPSTabs" runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "Wells Fargo PPS - Online Submittal Form" ID = "tpWFSubmissions" >
                    <ContentTemplate>    
<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>
    <asp:Panel runat ="server" >
        <asp:Table runat="server" CssClass="collapsetable" CellPadding ="0" CellSpacing="0" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" ForeColor="#003060" Font-Size="Larger" Font-Bold="true" >
                     Cash Bag Automation and Tracking tool <br />to support the Wells Fargo PPS online cash collections process.
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="lbOpenWFPPSInst" BorderStyle="Outset" BorderWidth="2px" CssClass="Printbutton"  Font-Size="Larger" runat="server" Text="Open Instructions"  OnClientClick="open_win()"  />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Font-Size="Smaller">New user access to the WF PPS tool should be directed to the Treasury department, Treasury@northside.com <br /> Other issues with the tool should be submitted through the 
                    <a class="Linking" id="A2" href="https://financeweb.northside.local/FinanceHelpDesk/financehelpdesk.aspx" 
                            runat="server" title="THIS IS NOT THE 'IS' HELP DESK">BIDS Help Desk</a>. </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
 <%--       <div id="divPrint">--%>
        <asp:Panel runat ="server" >
        <asp:Table runat ="server">
            <asp:TableHeaderRow >
                <asp:TableHeaderCell Width="300px">Northside Hospital</asp:TableHeaderCell>
                <asp:TableCell ></asp:TableCell>
                <asp:TableHeaderCell Width="210px">Deposit Date</asp:TableHeaderCell>
                <asp:TableCell><asp:Panel runat="server"><asp:Label ID="lblDepositDate" runat ="server"  Width="150px" height ="17px" BackColor ="#ccffcc" BorderWidth="2px" BorderColor="WhiteSmoke" BorderStyle="Inset"></asp:Label>
                   </asp:Panel>
                </asp:TableCell>
                <asp:TableCell RowSpan="3" Width ="80px"></asp:TableCell>
                <asp:TableCell RowSpan ="3" HorizontalAlign ="Center" VerticalAlign ="middle" ><asp:Button  CssClass="Printbutton" Font-Bold="true" Width="150px" Font-Size="Large"  ID="btnSubmitTheBag" BorderStyle="Outset" BorderWidth="2px" runat ="server" Text ="Submit Bag" />
                    </asp:TableCell>
                <asp:TableCell RowSpan="3" Width ="60px"></asp:TableCell>
                <asp:TableCell RowSpan="3"><asp:LinkButton ID="btnreset"  CssClass="Printbutton" runat="server" Text ="Reset"></asp:LinkButton></asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Wells Fargo PPS Deposit</asp:TableHeaderCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableHeaderCell>Deposit Bag #</asp:TableHeaderCell>
                <asp:TableCell><asp:TextBox ID="txtDepositBag" runat ="server" BackColor ="#ccffcc"></asp:TextBox></asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow Visible ="true">
                <asp:TableHeaderCell></asp:TableHeaderCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableHeaderCell>Deposit Slip #</asp:TableHeaderCell>
                <asp:TableCell VerticalAlign="Middle" ><asp:TextBox ID="txtDepositSlip" runat ="server" BackColor ="#ccffcc"></asp:TextBox>&nbsp;
                    <asp:ImageButton runat = "server" ImageUrl="WellsFargoImages/QuestionButton.png" Width="15px"  CssClass="Printbutton" OnClientClick="open_win3()" />
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableHeaderRow>
                <asp:TableHeaderCell></asp:TableHeaderCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableHeaderCell>Deposit Bag Total</asp:TableHeaderCell>
                <asp:TableCell><asp:Label ID="lblDepositTotal" runat ="server" Width="150px" height ="17px" BackColor ="#ccffcc" BorderWidth="2px" BorderColor="WhiteSmoke" BorderStyle="Inset"></asp:Label></asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow><asp:TableCell Height="30px"></asp:TableCell></asp:TableRow>
            <asp:TableHeaderRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableHeaderCell>Deposit Location</asp:TableHeaderCell>
                <asp:TableCell>
                    <asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlDepositLoc"  AppendDataBoundItems="false" AutoPostBack ="true" >             
                           </asp:DropDownList>
                </asp:TableCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </asp:Panel>
     <asp:Panel runat="server" Width="1125px" BackColor="#CBE3FB">
    <asp:Table runat="server" ID="MainTable" CellPadding ="0" CellSpacing="0" BorderWidth ="0" CssClass="collapsetable" >
        <asp:TableHeaderRow Font-Size="X-Small" VerticalAlign="Bottom">
            <asp:TableHeaderCell></asp:TableHeaderCell>
            <asp:TableHeaderCell Width ="100px">WF PPS EOD Collection Date&nbsp;&nbsp;</asp:TableHeaderCell>
            <asp:TableHeaderCell>Outlet Taken At&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableHeaderCell>
            <asp:TableHeaderCell>Cash&nbsp;&nbsp;</asp:TableHeaderCell>
            <asp:TableHeaderCell>Manual Checks&nbsp;&nbsp;</asp:TableHeaderCell>
            <asp:TableHeaderCell>Outlet Total&nbsp;&nbsp;</asp:TableHeaderCell>
            <asp:TableHeaderCell height="50px" Width ="120px">Cash & Manual Checks agree to EOD Summary?&nbsp;&nbsp;</asp:TableHeaderCell>
            <asp:TableHeaderCell>If no, please explain:&nbsp;&nbsp;</asp:TableHeaderCell>
        </asp:TableHeaderRow> 
        <asp:TableRow Font-Size ="Small">
            <asp:TableCell HorizontalAlign="Right" Width="50px">1.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" Height ="14px" runat ="server" ID="txtDate1"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate1" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet1"  AppendDataBoundItems="false" width="100%">             
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width="100px" runat ="server" ID ="txtCash1"  AutoPostBack ="true"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual1" AutoPostBack ="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke" BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal1"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree1" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain1" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">2.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate2"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate2" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet2"  AppendDataBoundItems="false"   width="100%">
                </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash2" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual2" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal2"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree2" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain2" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">3.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate3"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate3" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet3"  AppendDataBoundItems="false"   width="100%">
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash3" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual3" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal3"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree3" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain3" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">4.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate4"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate4" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet4" AppendDataBoundItems="false"  width="100%">
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash4" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual4" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal4"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree4" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain4" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">5.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate5"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate5" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet5" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash5" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual5" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal5"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree5" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain5" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">6.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate6"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate6" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet6" AppendDataBoundItems="false"  width="100%">
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash6" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual6" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal6"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree6" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain6" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">7.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate7"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate7" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet7" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash7" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual7" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal7"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree7" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain7" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">8.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate8"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate8" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet8" AppendDataBoundItems="false" width="100%" >
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash8" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual8" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal8"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree8" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain8" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">9.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate9"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate9" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet9" AppendDataBoundItems="false" width="100%" >
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash9" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual9" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal9"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree9" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain9" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">10.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate10"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate10" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet10" AppendDataBoundItems="false" width="100%" >
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash10" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual10" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal10"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree10" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain10" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">11.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate11"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate11" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet11" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash11" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual11" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal11"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree11" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain11" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">12.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate12"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate12" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet12" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash12" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual12" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal12"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree12" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain12" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">13.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate13"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate13" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet13" AppendDataBoundItems="false" width="100%" >
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash13" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual13" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal13"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree13" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain13" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">14.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate14"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate14" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet14" AppendDataBoundItems="false" width="100%" >
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash14" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual14" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal14"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree14" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain14" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">15.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate15"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate15" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet15" AppendDataBoundItems="false"  width="100%">
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash15" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual15" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal15"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree15" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain15" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">16.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate16"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate16" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet16" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash16" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual16" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal16"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree16" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain16" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">17.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate17"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate17" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet17" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash17" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual17" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal17"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree17" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain17" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">18.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate18"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate18" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet18" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash18" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual18" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal18"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree18" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain18" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">19.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate19"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate19" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet19" AppendDataBoundItems="false" width="100%" >
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash19" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual19" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal19"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree19" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain19" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">20.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate20"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate20" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet20" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash20" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual20" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal20"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree20" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain20" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">21.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate21"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate21" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet21" AppendDataBoundItems="false"  width="100%">
               </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash21" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual21" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal21"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree21" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain21" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">22.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate22"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate22" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet22" AppendDataBoundItems="false"  width="100%">
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash22" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual22" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal22"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree22" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain22" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">23.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate23"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate23" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet23" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash23" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual23" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal23"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree23" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain23" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">24.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate24"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate24" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet24" AppendDataBoundItems="false" width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash24" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual24" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal24"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree24" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain24" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">25.</asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtDate25"></asp:TextBox><cc1:CalendarExtender runat="server" TargetControlID="txtDate25" /></asp:TableCell>
            <asp:TableCell><asp:DropDownList Font-Size ="12px" runat ="server" ID ="ddlOutlet25" AppendDataBoundItems="false"  width="100%" >
              </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID ="txtCash25" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="100px" runat ="server" ID="txtManual25" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            <asp:TableCell BorderColor="WhiteSmoke"  BorderStyle="Inset" BackColor="#ccffff" width ="100px"><asp:Label runat ="server" ID="lblTotal25"></asp:Label></asp:TableCell>
            <asp:TableCell HorizontalAlign="Center"><asp:DropDownList Font-Size ="12px" runat ="server" ID="ddlAgree25" AutoPostBack="true">
                                <asp:ListItem Text="YES" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                           </asp:DropDownList></asp:TableCell>
            <asp:TableCell><asp:TextBox Width ="200px" runat ="server" ID="txtExplain25" Enabled ="false"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    </asp:Table> 
</asp:Panel>
   <%--    <asp:GridView ID="gvDeposits" runat="server"
           AllowSorting="False" AutoGenerateColumns="false" BorderColor="#2b74bb" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#ffcc99"  
         HeaderStyle-ForeColor="#000000" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Wrap="true"  ForeColor="Black" 
         BackColor="White" BorderWidth="1px" 
            >   
        <AlternatingRowStyle BackColor="#F2EAD9"  />
        <Columns>
           <asp:CommandField ItemStyle-Width="1px" ShowDeleteButton="false" 
                ShowEditButton="true" ShowSelectButton = "true" SelectText = "" EditText ="" CancelText ="" UpdateText ="" >
            <HeaderStyle Width="100px" />
            </asp:CommandField>
            <asp:TemplateField HeaderText = "WF PPS EOD Collection Date" ItemStyle-Width = "140px" >
                <ItemTemplate>              
                <asp:Panel ScrollBars = "Auto" Width = "95%" runat = "server">
                    <asp:Label ID="lblDepositDate" runat = "server" Text='<%# Bind("CollectionDate")%>'></asp:Label>
                </asp:Panel>
                  <asp:TextBox ID="txtDepositDate" Width = "95%" runat = "server" Text='<%# Bind("CollectionDate")%>' visible ="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText = "Outlet Taken At" ItemStyle-Width = "250px">
                <ItemTemplate>
                <asp:Panel ScrollBars = "Auto" Width = "95%" runat = "server">
                    <asp:Label ID="lblDepositOutlet" runat = "server" Text='<%# Bind("Outlet")%>'></asp:Label>
                </asp:Panel>
                    <asp:DropDownList ID="ddlDepositOutlet" runat ="server" Visible = "false" ></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC"  HorizontalAlign="Left"/>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="Small"/>
                      <EditRowStyle  Font-Size="Small" /> 
    </asp:GridView>--%>
          <%--  </div>--%>
       
             <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> <asp:Button BorderStyle="Outset" BorderWidth="2px" Visible="false"  ID="btnOptInstructions" Font-Size="small" runat="server" Text="Slip Instructions"  OnClientClick="open_win3()"  />
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Table runat="server">
             <asp:TableRow>
                 <asp:TableCell>
                      <asp:Button id="PrintButton"  CssClass="Printbutton" BorderStyle="Outset" BorderWidth="2px" Font-Size = "small" Visible="false" text="Print" runat="Server" />
                      <asp:Button ID="OkButton"  CssClass="Printbutton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/>
          <asp:Button ID="SubmitButton"  CssClass="Printbutton" Visible ="false" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Submit"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5"></asp:TableCell>
                 <asp:TableCell>         
              <asp:Button ID="CancelButton"  CssClass="Printbutton" Visible ="false" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/>
                 </asp:TableCell>
             </asp:TableRow>
         </asp:Table>

             <asp:Label ID="lblHoldOverSQL" runat ="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblHoldOverrows" runat ="server" Visible="false" Text=""></asp:Label>
                   </asp:TableCell>
</asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="Panel1"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

           </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>

</ContentTemplate>
</cc1:TabPanel>
     <%--       <cc1:TabPanel runat = "server" HeaderText = "PPS Management" ID = "tpAdmin" Visible="false" >
                            <ContentTemplate>    
            <asp:UpdatePanel runat="server" ID= "upAdminMain"> 
             <ContentTemplate>
                         <asp:Table runat="server" CssClass="collapsetable" CellPadding ="0" CellSpacing="0" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" ForeColor="#003060" Font-Size="Larger" Font-Bold="true" >
                     Cash Bag Automation and Tracking tool <br />to support the Wells Fargo PPS online cash collections process.
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                     <asp:Button BorderStyle="Outset" BorderWidth="2px"  ID="Button1" Font-Size="Larger" runat="server" Text="Open Instructions"  OnClientClick="open_win2()"  />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
                 <br />
         <asp:Panel runat ="server" >
             <asp:Table runat="server" CssClass="collapsetable" >
                 <asp:TableHeaderRow>
                     <asp:TableHeaderCell ColumnSpan="2">Select Date</asp:TableHeaderCell>
                     <asp:TableCell Width="100px"></asp:TableCell>
                     <asp:TableHeaderCell HorizontalAlign="Center">Cash</asp:TableHeaderCell>
                     <asp:TableCell Width="5px"></asp:TableCell>
                     <asp:TableHeaderCell HorizontalAlign="Center">Manual Checks</asp:TableHeaderCell>
                     <asp:TableCell Width="5px"></asp:TableCell>
                     <asp:TableHeaderCell HorizontalAlign="Center">Total</asp:TableHeaderCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow>
                     <asp:TableCell>
                         <asp:RadioButtonList runat="server" ID="rblAdminWhichDate" Font-Size="X-Small" RepeatDirection="Vertical" AutoPostBack="true">
                            <asp:ListItem Text="Deposit Date" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Collection Date" Value="1"></asp:ListItem>
                         </asp:RadioButtonList>
                     </asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtAdminWhichDate" AutoPostBack="true" ></asp:TextBox>
                         <cc1:CalendarExtender runat="server" TargetControlID="txtAdminWhichDate" />
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell HorizontalAlign="Center">
                         <asp:Label runat="server" ID="lblTotalCash"></asp:Label>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell HorizontalAlign="Center">
                         <asp:Label runat="server" ID="lblTotalManual"></asp:Label>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell HorizontalAlign="Center">
                         <asp:Label runat="server" ID="lblTotal"></asp:Label>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableCell Height="625px" Width="140px" RowSpan="2">
                         <asp:Panel runat="server" BackColor="#6da9e3" Width="140px" Height="625px">
                             <asp:DropDownList runat="server" ID="ddlBagOrSlip" Font-Size="X-Small" AutoPostBack="true" >
                                 <asp:ListItem Text="Deposit Bag Number" Value="Bag"></asp:ListItem>
                                 <asp:ListItem Text="Deposit Slip Number" Value ="Slip"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:Table runat="server" CssClass="collapsetable"><asp:TableRow><asp:TableCell>
                             <asp:LinkButton ID="lbAdminSelectAll" ForeColor="White" Font-Size="XX-Small" runat="server">Select All</asp:LinkButton> </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                             <asp:LinkButton ID="lbAdminUnSelectAll" ForeColor="White" Font-Size="XX-Small"  runat="server">Unselect All</asp:LinkButton> 
                            </asp:TableCell></asp:TableRow>
                                 <asp:TableRow ><asp:TableCell Height="545px" ColumnSpan="2">
                                     <asp:Panel runat="server" Width="125px" Height="545px" ScrollBars="Auto">
                             <asp:CheckBoxList Font-Size="X-Small" runat="server" ForeColor="White" ID="cblDepositBags" AutoPostBack="true" ></asp:CheckBoxList></asp:Panel>
                             </asp:TableCell></asp:TableRow> 
                                 <asp:TableRow> <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                                     <asp:CheckBox ID="cbSeeFull" runat="server" Checked="false" Text="See Full Bags" Font-Size="X-Small" ForeColor="White" AutoPostBack="true" />
                                               </asp:TableCell></asp:TableRow>
                             </asp:Table> 
                         </asp:Panel>
                     </asp:TableCell>
                     <asp:TableCell ColumnSpan ="7">
                         <asp:Panel runat="server" ScrollBars="Auto" Width ="1000px" Height="500px"> 
                             <asp:GridView runat="server" ID="gvAdminView" AutoGenerateColumns="false" BackColor="White" AllowPaging="true"
                                 AllowSorting="true" PageSize="30" Font-Size="X-Small" HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="2">
                                 <AlternatingRowStyle BackColor="#CBE3FB" />
                                    <Columns>
                                          <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                             <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass ="hidden"
                                                 HeaderStyle-CSSClass ="hidden"
                                                  SortExpression="ID"></asp:BoundField>
                                              <asp:BoundField DataField="DepositBagNumber" HeaderText="Deposit Bag Number" 
                                                  SortExpression="DepositBagNumber"></asp:BoundField>
                                              <asp:BoundField DataField="DepositBagID" HeaderStyle-Width="80px" HeaderText="Deposit Bag ID" 
                                                  SortExpression="DepositBagID"></asp:BoundField>
                                              <asp:BoundField DataField="DepositSlipNumber" HeaderText="Deposit Slip Number" 
                                                  SortExpression="DepositSlipNumber"></asp:BoundField>
                                              <asp:BoundField DataField="dd" HeaderText="Deposit Date" 
                                                  SortExpression="DepositDate"></asp:BoundField>
                                              <asp:BoundField DataField="EODd" HeaderText="Collection Date" 
                                                  SortExpression="EODCollectionDate"></asp:BoundField>
                                              <asp:BoundField DataField="OutletTA" HeaderText="Outlet Taken At"
                                                  SortExpression="OutletTA"></asp:BoundField>
                                              <asp:BoundField DataField="Cash" HeaderText="Cash"
                                                  SortExpression="Cash"></asp:BoundField>
                                              <asp:BoundField DataField="ManualChecks" HeaderText="Manual Checks" 
                                                  SortExpression="ManualChecks"></asp:BoundField>
                                              <asp:BoundField DataField="Total" 
                                                  HeaderText="Total" SortExpression="Total">
                                              </asp:BoundField>
                                              <asp:BoundField DataField="AgreeToEOD" HeaderText="Agree To EOD?" 
                                                  SortExpression="AgreeToEOD"></asp:BoundField>
                                              <asp:BoundField DataField="Explain"  HeaderText="Explanation" SortExpression="Explain">
                                              </asp:BoundField>
                                              <asp:BoundField DataField="SubmissionBy"  HeaderText="Submitted By" SortExpression="SubmissionBy">
                                              </asp:BoundField>
                                              <asp:BoundField DataField="LEd"  HeaderText="Date Last Edited" SortExpression="LastEditedDate">
                                              </asp:BoundField>
                                          </Columns>
                             </asp:GridView>
                         </asp:Panel>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableCell ColumnSpan="6">
                         <asp:Panel runat="server" BorderColor ="#003060" BorderStyle="Solid" BorderWidth="1px" Width="900px" Height="100px">
                             <asp:Table runat="server" CssClass="collapsetable">
                                 <asp:TableRow>
                                     <asp:TableCell Width="1px"></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Deposit Bag Number</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="70px" ID="txtEditDepBag"></asp:TextBox> </asp:TableCell>
                                     <asp:TableCell Width="1px"></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Deposit Slip Number</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="70px" ID="txtEditDepSlip"></asp:TextBox> </asp:TableCell>
                                     <asp:TableCell Width="1px"></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Deposit Date</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="70px" ID="txtEditDepDate"></asp:TextBox> 
                                         <cc1:CalendarExtender runat="server" TargetControlID="txtEditDepDate" />
                                     </asp:TableCell>
                                     <asp:TableCell Width="1px"></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Collection Date</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="70px" ID="txtEditEODDate"></asp:TextBox>
                                         <cc1:CalendarExtender runat="server" TargetControlID="txtEditEODDate" />
                                     </asp:TableCell>
                                     <asp:TableCell Width="1px"></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Outlet Taken At</asp:TableCell>
                                     <asp:TableCell><asp:DropDownList Font-Size="X-Small" runat="server" ID="ddlEditOutlet" AppendDataBoundItems ="false">
                                         
                                                    </asp:DropDownList></asp:TableCell>

                                 </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Cash</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="70px" ID="txtEditCash"></asp:TextBox></asp:TableCell>
                                     <asp:TableCell></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Manual Checks</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="70px" ID="txtEditManual"></asp:TextBox></asp:TableCell>
                                     <asp:TableCell></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Agree to EOD?</asp:TableCell>
                                     <asp:TableCell><asp:DropDownList Font-Size="X-Small" Width="70px" runat="server" ID="ddlEditAgree">
                                                        <asp:ListItem Text="YES" Value="True" ></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="False"> </asp:ListItem>
                                                    </asp:DropDownList></asp:TableCell>

                                     <asp:TableCell></asp:TableCell>
                                     <asp:TableCell Font-Size="X-Small">Explanation</asp:TableCell>
                                     <asp:TableCell><asp:TextBox Font-Size="X-Small" runat="server" Width="250px" ID="txtEditExplain"></asp:TextBox></asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table>
                         </asp:Panel>
                     </asp:TableCell>
                     <asp:TableCell>
                         <asp:Table runat="server" Height="100px" Width="100px" CssClass="collapsetable" >
                             <asp:TableRow>
                                 <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                                     <asp:Button runat="server" ID="btnAdminUpdate" Text="Update" />
                                 </asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                                     <asp:Button runat="server" ID="btnAdminDelete" Text="Delete" />
                                 </asp:TableCell>
                             </asp:TableRow>
                         </asp:Table>
                     </asp:TableCell>
                 </asp:TableRow>
             </asp:Table>

                      <asp:Label ID="FakeButton2" runat = "server" />
        <asp:Panel ID="Panel2" runat="server" Width="300px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="80%" CssClass="collapsetable">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                    <asp:label ID = "ExplanationLabel2" runat = "server"></asp:label> 
                </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> 

           </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow></asp:Table>
           <asp:Table runat="server" width="100%" Height="20%" CssClass="collapsetable" >
        <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="btnMPE2OK" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell>
          <asp:TableCell>  <asp:Button ID="btnMPE2Cancel" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
            <asp:TableCell>  <asp:Button ID="btnMPE2nvm" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                 TargetControlID ="FakeButton2"
                 PopupControlID="Panel2"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

             </asp:Panel>
                 </ContentTemplate>
                </asp:UpdatePanel>
</ContentTemplate>
</cc1:TabPanel>--%>
            <cc1:TabPanel runat = "server" HeaderText = "PPS Management" ID = "tpAdminReject" Visible="false" >
                            <ContentTemplate>    
            <asp:UpdatePanel runat="server" ID= "UpdatePanel1"> 
             <ContentTemplate>
                
                 <asp:Table runat="server">
                     <asp:TableRow>
                         <asp:TableCell>
                             <asp:Panel runat="server" BackColor ="#CBE3FB">
                             <asp:Table runat="server" Width="100%">
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" Font-Bold="true">
                                        Limit Deposit Bag Search:
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
                    </asp:Panel>

                    
                    <br />
                    <asp:Panel runat="server" ID="ScrollPanel" CssClass="MaxPanelHeight" Width="450px" Height="600px" HorizontalAlign="Center" >
        <asp:GridView ID="gvSubmittedBags" runat="server"  HeaderStyle-BackColor = "#6da9e3" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" CellPadding="3"
            RowStyle-BorderColor = "#003060" RowStyle-BorderWidth="1px" AllowPaging="true" AllowSorting ="true" 
            RowStyle-HorizontalAlign = "Center" AutoGenerateColumns="false" 
                HorizontalAlign="Left" Font-Size="Smaller" >
          
                                     <Columns>
                                      <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
<%--                                      <asp:TemplateField HeaderText = "" >
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
                                       </asp:TemplateField>--%>
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
                                          <asp:BoundField DataField="Total" HeaderText="Total" 
                                              SortExpression="Total"></asp:BoundField>
                                          <asp:BoundField DataField="Agree" HeaderText="Agree to EOD?" 
                                              SortExpression="Agree"></asp:BoundField>
                                         
                                      </Columns>

        </asp:GridView>
</asp:Panel>
                         </asp:TableCell>
                         <asp:TableCell Width="10px">
                         </asp:TableCell>
                         <asp:TableCell>
                              <asp:Label ID="lblSelectBagID" runat="server" Text ="" Visible ="False"></asp:Label>
                             Deposit Date: <asp:Label ID="lblSelectBagDD" runat="server" Text =""></asp:Label>
                             <br />
                             Deposit Bag Number: <asp:Label ID ="lblSelectBagDepNo" runat="server" Text =""></asp:Label>
                             <br />
                             Deposit Slip Number: <asp:Label ID ="lblSelectBagDepSlip" runat="server" Text =""></asp:Label>
                             <br />
                             Deposit Bag Total: <asp:Label ID ="lblSelectTotal" runat="server" Text =""></asp:Label>
                             <br />
                             Deposit Bag Submitted By: <asp:Label ID ="lblSelectSubBy" runat="server" Text =""></asp:Label>
                                     <asp:Panel runat = "server" ID = "pnlScrollPanelSelectedBag" scrollbars = "auto" height = "400px" width = "100%">
                                  <asp:GridView ID="gvSelectedBag" runat="server" AutoGenerateColumns="False" 
                                      HeaderStyle-BackColor = "#4A8fd2" BackColor="#CBE3FB"
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" CellPadding ="3"
            AlternatingRowStyle-BackColor = "White" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" 
                HorizontalAlign="Left" Font-Size="X-small" PageSize="50"   
                                      >
                                      <Columns>
                                      <%--<asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />--%>
 <%--                                         <asp:BoundField DataField="DepositBagNumber" HeaderText="Deposit Bag Number" ReadOnly="True" 
                                              SortExpression="DepositBagNumber"></asp:BoundField>
                                          <asp:BoundField DataField="DepositSlipNumber" HeaderText="Deposit Slip Number" ReadOnly="True" 
                                              SortExpression="DepositSlipNumber"></asp:BoundField>
                                          <asp:BoundField DataField="DepositDisplay" HeaderText="Deposit Date" 
                                              ReadOnly="True" SortExpression="DepositDate"></asp:BoundField>--%>
                                          <asp:BoundField DataField="EODCollectionDate" HeaderText="EOD Collection Date" 
                                              SortExpression="EODCollectionDate"></asp:BoundField>
                                          <asp:BoundField DataField="Entity" HeaderText="Entity" ReadOnly="True"
                                              SortExpression="Entity"></asp:BoundField>
                                          <asp:BoundField DataField="OutletTA" HeaderText="Outlet Taken At" ReadOnly="True"
                                              SortExpression="OutletTA"></asp:BoundField>
                                          <asp:BoundField DataField="Cash" HeaderText="Cash" ReadOnly="True"
                                              SortExpression="Cash"></asp:BoundField>
                                          <asp:BoundField DataField="ManualChecks" ReadOnly="True"
                                              HeaderText="Manual Checks" SortExpression="ManualChecks">
                                          </asp:BoundField>
                                          <asp:BoundField DataField="AgreeToEOD" HeaderText="Agree To EOD?" ReadOnly="True" 
                                              SortExpression="AgreeToEOD"></asp:BoundField>
                                          <asp:BoundField DataField="Explain" ReadOnly="True" HeaderText="Explanation" SortExpression="Explain">
                                          </asp:BoundField>
<%--                                          <asp:BoundField DataField="DC_FROM" HeaderText="DC_FROM" ReadOnly="True" 
                                              SortExpression="DC_FROM" visible = "false"></asp:BoundField>
                                          <asp:BoundField DataField="DC_TO" ReadOnly="True" HeaderText="DC_TO" SortExpression="DC_TO" visible = "false">
                                          </asp:BoundField> --%>

                                      </Columns>
                                  </asp:GridView></asp:Panel>
                             <br />
                             <asp:Table runat="server">
                                 <asp:TableRow>
                                     <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtRejectReason" TextMode="MultiLine" Height="150px" Width="400px"></asp:TextBox>
                                     </asp:TableCell>
                                     <asp:TableCell Width="20"></asp:TableCell>
                                     <asp:TableCell VerticalAlign="Middle">
                                        <asp:Button ID="btnRejectBag" BorderStyle="Outset" BorderWidth="2px" runat ="server" Text ="Reject Bag" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table>                            
                             
                         </asp:TableCell>
                     </asp:TableRow>
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
               <cc1:TabPanel runat = "server" HeaderText = "PPS Management" ID = "tpSuperAdmin" Visible="false" >
                            <ContentTemplate>    
            <asp:UpdatePanel runat="server" ID= "UpdatePanel2"> 
             <ContentTemplate>
                
                 <asp:Table runat="server">
                     <asp:TableRow>
                         <asp:TableCell>
                             <asp:Panel runat="server" BackColor ="#CBE3FB">
                             <asp:Table runat="server" Width="100%">
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" Font-Bold="true">
                                        Limit Deposit Bag Search:
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        <asp:DropDownList runat="server" ID="ddlSAEntity" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center">
                                         <asp:DropDownList runat="server" ID="ddlSALoc" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                     </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                        <asp:DropDownList runat="server" ID="ddlSASubBy" AutoPostBack="true"></asp:DropDownList>
                                     </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Deposit Bag Number (Optional): <asp:TextBox ID="txtSADepBag" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                         <asp:TableCell HorizontalAlign="Center">
                                             Deposit Slip Number (Optional): <asp:TextBox ID="txtSADepSlip" Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
                                         </asp:TableCell>
                                         </asp:TableRow><asp:TableRow>
                                     <asp:TableCell HorizontalAlign="Center" >
                                         Deposit Date Range:
                                         <asp:TextBox runat="server" ID="txtSAStartDate" Width="75px" font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender3" 
                                                runat="server" TargetControlID="txtSAStartDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                         &nbsp&nbsp To &nbsp;&nbsp;
                                         <asp:TextBox runat="server" ID="txtSAEndDate" Width="75px"  font-Size="Small" AutoPostBack="true"></asp:TextBox>
                                            <cc1:calendarextender ID="CalendarExtender4" 
                                                runat="server" TargetControlID="txtSAEndDate" Format = "yyyy-MM-dd" TodaysDateFormat = "yyyy-MM-dd" >
                                            </cc1:calendarextender> 
                                     </asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table>                                            
                    </asp:Panel>

                    
                    <br />
                    <asp:Panel runat="server" ID="Panel2" CssClass="MaxPanelHeight" Width="450px" Height="600px" HorizontalAlign="Center" >
        <asp:GridView ID="gvSABags" runat="server"  HeaderStyle-BackColor = "#6da9e3" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" CellPadding="3"
            RowStyle-BorderColor = "#003060" RowStyle-BorderWidth="1px" AllowPaging="true" AllowSorting ="true" 
            RowStyle-HorizontalAlign = "Center" AutoGenerateColumns="false" 
                HorizontalAlign="Left" Font-Size="Smaller" >
          
                                     <Columns>
                                      <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
<%--                                      <asp:TemplateField HeaderText = "" >
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
                                       </asp:TemplateField>--%>
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
                                          <asp:BoundField DataField="Total" HeaderText="Total" 
                                              SortExpression="Total"></asp:BoundField>
                                          <asp:BoundField DataField="Agree" HeaderText="Agree to EOD?" 
                                              SortExpression="Agree"></asp:BoundField>
                                         
                                      </Columns>

        </asp:GridView>
</asp:Panel>
                         </asp:TableCell>
                         <asp:TableCell Width="10px">
                         </asp:TableCell>
                         <asp:TableCell>
                              <asp:Label ID="lblSABagID" runat="server" Text ="" Visible ="False"></asp:Label>
                             Deposit Date: <asp:Textbox ID="txtSADepDate" runat="server" Text =""></asp:Textbox>
                             <cc1:CalendarExtender runat="server" TargetControlID="txtSADepDate" />   
                             <br />
                             Deposit Bag Number: <asp:Textbox ID ="txtSADepBagNo" runat="server" Text =""></asp:Textbox>
                             <br />
                             Deposit Slip Number: <asp:Textbox ID ="txtSADepSlipNo" runat="server" Text =""></asp:Textbox>
                             <br />
                             Deposit Bag Total: <asp:Label ID ="lblSADepBagTotal" runat="server" Text =""></asp:Label>
                             <br />
                             Deposit Bag Submitted By: <asp:Label ID ="lblSASubBy" runat="server" Text =""></asp:Label>
                                     <asp:Panel runat = "server" ID = "Panel4" scrollbars = "auto" Width="95%" height = "400px" >
                                  <asp:GridView ID="gvSARows" runat="server" AutoGenerateColumns="False" 
                                      HeaderStyle-BackColor = "#4A8fd2" BackColor="#CBE3FB"
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" CellPadding ="1"
            AlternatingRowStyle-BackColor = "White" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" DataKeyNames="ID" 
                HorizontalAlign="Left" Font-Size="X-small" PageSize="50"   
                                      >
                                      <Columns>
                                       <asp:CommandField ItemStyle-Width="55px" UpdateText="Update<br>"
                                            ShowEditButton="true" ShowSelectButton = "true" SelectText = "" >
                                        <HeaderStyle Width="55px" />
                                        </asp:CommandField>
                                      <%--<asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />--%>
 <%--                                         <asp:BoundField DataField="DepositBagNumber" HeaderText="Deposit Bag Number" ReadOnly="True" 
                                              SortExpression="DepositBagNumber"></asp:BoundField>
                                          <asp:BoundField DataField="DepositSlipNumber" HeaderText="Deposit Slip Number" ReadOnly="True" 
                                              SortExpression="DepositSlipNumber"></asp:BoundField>
                                          <asp:BoundField DataField="DepositDisplay" HeaderText="Deposit Date" 
                                              ReadOnly="True" SortExpression="DepositDate"></asp:BoundField>--%>
                   <asp:TemplateField HeaderText = "EOD Collection Date" SortExpression ="EODCollectionDate" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSAEODCollectionDate" runat = "server" Text='<%# Bind("EODCollectionDate")%>'></asp:Label>
                    <asp:TextBox Font-Size="X-Small" Width="50px"  ID="txtSAEODCollectionDate" runat = "server" Text='<%# Bind("EODCollectionDate")%>' Visible = "false" ></asp:TextBox>  
                    <cc1:CalendarExtender runat="server"  Format="mm/dd/yyyy" TargetControlID="txtSAEODCollectionDate" />             
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText = "Entity" SortExpression ="Entity" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSAEntity" runat = "server" Text='<%# Bind("Entity")%>'></asp:Label>
                    <asp:TextBox Font-Size="X-Small" Width="50px"   ID="txtSAEntity" runat = "server" Text='<%# Bind("Entity")%>' Visible = "false" ></asp:TextBox>               
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText = "Outlet Taken At" SortExpression ="OutletTA" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSAOutletTA" runat = "server" Text='<%# Bind("OutletTA")%>'></asp:Label>
                    <asp:DropDownList Font-Size="X-Small" ID="ddlSAOutletTA" runat="server" Visible="false" ></asp:DropDownList>
                    <%--<asp:TextBox Font-Size="X-Small"  ID="txtSAOutletTA"  runat = "server" Text='<%# Bind("OutletTA")%>' Visible = "false" ></asp:TextBox>   --%>            
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText = "Cash" SortExpression ="Cash" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSACash" runat = "server" Text='<%# Bind("Cash")%>'></asp:Label>
                    <asp:TextBox Font-Size="X-Small" Width="50px"  ID="txtSACash" runat = "server" Text='<%# Bind("Cash")%>' Visible = "false" ></asp:TextBox>               
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText = "Manual Checks" SortExpression ="ManualChecks" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSAManualChecks" runat = "server" Text='<%# Bind("ManualChecks")%>'></asp:Label>
                    <asp:TextBox Font-Size="X-Small" Width="50px"  ID="txtSAManualChecks" runat = "server" Text='<%# Bind("ManualChecks")%>' Visible = "false" ></asp:TextBox>               
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText = "Agree To EOD?" SortExpression ="AgreeToEOD" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSAAgreeToEOD" runat = "server" Text='<%# Bind("AgreeToEOD")%>'></asp:Label>
                    <asp:TextBox Font-Size="X-Small"  Width="50px"  ID="txtSAAgreeToEOD"  runat = "server" Text='<%# Bind("AgreeToEOD")%>' Visible = "false" ></asp:TextBox>               
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText = "Explanation" SortExpression ="Explain" >
                <ItemTemplate>                              
                    <asp:Label ID="lblSAExplain" runat = "server" Text='<%# Bind("Explain")%>'></asp:Label>
                    <asp:TextBox Font-Size="X-Small"  ID="txtSAExplain" runat = "server" Text='<%# Bind("Explain")%>' Visible = "false" ></asp:TextBox>               
                </ItemTemplate>
            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "MerchantID" SortExpression ="MerchantID" Visible="false" >
                <ItemTemplate>                              

          
                </ItemTemplate>
            </asp:TemplateField>
                                     </Columns>
                                  </asp:GridView></asp:Panel>
                             <br />
                             <asp:Table runat="server">
                                 <asp:TableRow>
                                     <asp:TableCell RowSpan="3">
                                        <asp:TextBox runat="server" ID="txtSARejectReason" TextMode="MultiLine" Height="150px" Width="400px"></asp:TextBox>
                                     </asp:TableCell>
                                     <asp:TableCell Width="20"></asp:TableCell>
                                     <asp:TableCell VerticalAlign="Middle">
                                          <asp:Button ID="btnSAUpdateBag" BorderStyle="Outset" BorderWidth="2px" runat ="server" Text ="Update Bag" />
                                       
                                     </asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow>

                                 </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell Width="20"></asp:TableCell>
                                     <asp:TableCell VerticalAlign="Middle">
                                        <asp:Button ID="btnSARejectReason" BorderStyle="Outset" BorderWidth="2px" runat ="server" Text ="Reject Bag" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                             </asp:Table>                            
                             
                         </asp:TableCell>
                     </asp:TableRow>
                 </asp:Table>

         <asp:Label ID="FakeButtonSA" runat = "server" />
        <asp:Panel ID="Panel5" runat="server" Width="300px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="80%" CssClass="collapsetable">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell>
               <asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
                    <asp:label ID = "mpeSAexplanationLabel" runat = "server"></asp:label> 
                </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> 

           </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow></asp:Table>
           <asp:Table runat="server" width="100%" Height="20%" CssClass="collapsetable" >
        <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="mpeSAOKButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell>
          <asp:TableCell>  <asp:Button ID="mpeSACancelButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
            <asp:TableCell>  <asp:Button ID="Button4" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/></asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="mpeSA" runat="server"
                 TargetControlID ="FakeButtonSA"
                 PopupControlID="Panel5"
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