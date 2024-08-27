<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ENT Data Entry.aspx.vb" Inherits="FinanceWeb.ENT_Data_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



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

        </style>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>

            <cc1:TabContainer ID="ARActivityReportTabs" runat="server"
                ActiveTabIndex="0" UseVerticalStripPlacement="false">
                <cc1:TabPanel runat="server" HeaderText="CPT Codes Rates" ID="tpCPTRates">
                    <ContentTemplate>


                        <asp:Panel runat="server" ID="hiddenThings" Visible="false">
                            <asp:Label runat="server" ID="ARDetailmap"></asp:Label>
                            <asp:Label runat="server" ID="ARDetaildir"></asp:Label>       
                            <asp:Label runat="server" ID="Mimic"></asp:Label>
                        </asp:Panel>

                        <asp:Panel ID="Panel1" runat="server">

                            <asp:Table runat="server" BackColor="#CBE3FB" Width="100%" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell Height="5px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow Visible="true">

                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableHeaderCell Width="200px" ForeColor="Black">
                         Group
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList CssClass="TextAreaGeneral2" runat="server" ID="ddlCPT_IDGroup"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableHeaderCell Width="200px" ForeColor="Black">
                         POS
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList CssClass="TextAreaGeneral2" runat="server" ID="ddlCPT_POS"></asp:DropDownList>
                                    </asp:TableCell>
                                    
                                </asp:TableHeaderRow>
                                <asp:TableHeaderRow Visible="true">

                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableHeaderCell Width="200px" ForeColor="Black">
                         POD
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList CssClass="TextAreaGeneral2" runat="server" ID="ddlCPT_POD"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableHeaderCell Width="200px" ForeColor="Black">
                         Category
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList CssClass="TextAreaGeneral2" runat="server" ID="ddlCPT_Category"></asp:DropDownList>
                                    </asp:TableCell>

                                </asp:TableHeaderRow>
                                <asp:TableHeaderRow Visible="true">

                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableHeaderCell Width="200px" ForeColor="Black">
                         CPT
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList CssClass="TextAreaGeneral2" runat="server" ID="ddlCPT_CPT"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableHeaderCell Width="200px" ForeColor="Black">
                         Alt Category
                                    </asp:TableHeaderCell>
                                    <asp:TableCell Width="10px"></asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:DropDownList CssClass="TextAreaGeneral2" runat="server" ID="DDLCPT_AltCategory"></asp:DropDownList>
                                    </asp:TableCell>

                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="10">
                                        <asp:Button CssClass="ButtonGeneral" runat="server" ID="btnCPT_Search" Text="Search" Width="200px" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />

                            </asp:Panel>

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

                        <br />
                    </ContentTemplate>


                </cc1:TabPanel>


            </cc1:TabContainer>


        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="updMain" ID="upProg_Pacman">
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
