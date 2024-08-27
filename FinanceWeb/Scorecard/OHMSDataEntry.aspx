<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OHMSDataEntry.aspx.vb" Inherits="FinanceWeb.OHMSDataEntry" %>
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
</style>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


                        <cc1:tabcontainer ID="OhmsAdminTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "true" >
                    <cc1:TabPanel runat = "server" HeaderText = "Scorecard Data" ID = "OhmsAdminSD">
                    <ContentTemplate>     
                        <asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>
         <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
             <asp:Table runat="server">
                 <asp:TableHeaderRow>
                     <asp:TableHeaderCell>
                         Select Metric:
                     </asp:TableHeaderCell>
                     <asp:TableCell Width="5px">
                     </asp:TableCell>
                     <asp:TableCell ColumnSpan="5">
                         <asp:DropDownList runat="server" ID ="ddlMetricSelect" AutoPostBack = "true"
                                 >
                         </asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow>
                     <asp:TableCell Height="5px"></asp:TableCell>
                 </asp:TableRow>
                 <asp:TableHeaderRow ID="LocatioinRow" Visible="true">
                     <asp:TableHeaderCell>
                         Select Location:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell ColumnSpan="5">
                         <asp:DropDownList runat="server" ID ="ddlLocationSelect" AutoPostBack ="true" ></asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow >
                     <asp:TableHeaderCell>
                         Select Data Entry Range:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtEntryDateStart"></asp:TextBox>
                         <cc1:CalendarExtender ID="calStartDate"
                             runat="server" TargetControlID="txtEntryDateStart" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                     </asp:TableCell>
                     <asp:TableCell>and</asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtEntryDateEnd"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1"
                             runat="server" TargetControlID="txtEntryDateEnd" Format="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></cc1:CalendarExtender>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow >
<asp:TableCell ColumnSpan="10" HorizontalAlign="Center"> <asp:Button Text="Refresh" ID="btnRefresh" runat="server" />  </asp:TableCell>
                 </asp:TableRow>

             </asp:Table>

             <br />
             <br />
             <asp:Panel Height="500px" ID="ScrollPanelgvSubmitData" runat="server" ScrollBars="Auto">
<asp:GridView ID="gvSubmitData" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "dID"  
         CellPadding="4" BorderColor="Black"  BackColor="#CBE3FB"  
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" AllowSorting ="True" AllowPaging="true" PageSize="25"  
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small"  >
                    <AlternatingRowStyle BackColor="White"  />
    <Columns>

           <asp:TemplateField HeaderText="dID" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server"   Text='<%# Eval("dID")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
         <asp:TemplateField HeaderText="Metric Name" ItemStyle-Width = "100" SortExpression="Metric Name" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Metric Name")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                   <asp:TemplateField HeaderText="Entity" ItemStyle-Width = "50" SortExpression="Entity">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Entity")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                  <asp:TemplateField HeaderText="Location" ItemStyle-Width = "75" SortExpression="Location">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                          <asp:TemplateField HeaderText="Location Description" ItemStyle-Width = "150" SortExpression="LocationDesc">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("LocationDesc")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Target" ItemStyle-Width = "75" SortExpression="Target">
                <ItemTemplate>
                    <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("Target")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
           <asp:TemplateField HeaderText="Current Numerator" SortExpression ="CurrentNumerator">
                <ItemTemplate>
                    <asp:Label ID="lblCurrentNumerator" runat="server" Visible = "false" Text='<%# Eval("CurrentNumerator")%>'></asp:Label>
                     <asp:TextBox ID="txtCurrNum" AutoPostBack="true" runat="server" OnTextChanged="ChangeCurrentValue" Text='<%# Eval("CurrentNumerator")%>' Visible="true" ReadOnly ="false"></asp:TextBox>
                </ItemTemplate>
</asp:TemplateField>
           <asp:TemplateField HeaderText="Current Denominator" SortExpression="Current Denominator">
                <ItemTemplate>
                    <asp:Label ID="lblDept" runat="server" BorderStyle="Solid" visible="false"  Text='<%# Eval("Current Denominator")%>'></asp:Label>
                     <asp:TextBox ID="txtCurrDenom" AutoPostBack="true" runat="server"  OnTextChanged="ChangeCurrentValue"  Text='<%# Eval("Current Denominator")%>' Visible="true" ReadOnly ="false"></asp:TextBox>
                   
                </ItemTemplate>


</asp:TemplateField>
                 <asp:TemplateField HeaderText="Current Value" SortExpression="Current Value" >
                <ItemTemplate> <asp:Label ID="lblCurrVal" runat="server" Text='<%# Eval("Current Value")%>'></asp:Label>
                    <%--<asp:Panel Width="100%" Height="100%" ID="pnlColor" runat="server">
                   
</asp:Panel>--%>
                </ItemTemplate>
                 </asp:TemplateField>
        <asp:BoundField DataField="color" HeaderText="color" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="color"></asp:BoundField>
                                 <asp:TemplateField HeaderText="Submission Date" ItemStyle-Width = "75" SortExpression="dDate">
                <ItemTemplate>
                    <asp:Label ID="lblDisplayDate" runat="server" Text='<%# Eval("DisplayDate")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                <asp:BoundField DataField="RedMax" HeaderText="RedMax" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="RedMax"></asp:BoundField>
                <asp:BoundField DataField="RedMin" HeaderText="RedMin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="RedMin"></asp:BoundField>
                <asp:BoundField DataField="wMax" HeaderText="wMax" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="wMax"></asp:BoundField>
                <asp:BoundField DataField="wMin" HeaderText="wMin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="wMin"></asp:BoundField>
        <asp:BoundField DataField="DataType" HeaderText="DataType" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="DataType"></asp:BoundField>

        </Columns>
       <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>
             </asp:Panel>

             <br />
             <asp:Button runat="server" ID="UpdateButton" Text="Update Rows" />

             
         </asp:Panel>

                     <asp:Label ID="FakeButton2" runat = "server" />
   <asp:Panel ID="Panel2" runat="server" Width="233px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explanationlabel" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server"
                 TargetControlID ="FakeButton2"
                 PopupControlID="Panel2"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


       
             <asp:Label ID="FakeButtonPageChange" runat = "server" />
   <asp:Panel ID="Panel3PageChange" runat="server" Width="460px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabelPageChange" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Table runat="server">
             <asp:TableRow>
                 <asp:TableCell>
          <asp:Button ID="SubmitButtonPageChange"  CssClass="Printbutton" Visible ="true" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Update Rows"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5"></asp:TableCell>
                 <asp:TableCell>         
              <asp:Button ID="btnChangePage"  CssClass="Printbutton" Visible ="true" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Change Page (Data Will be Lost)"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5"></asp:TableCell>
                 <asp:TableCell>         
              <asp:Button ID="CancelButtonPageChange"  CssClass="Printbutton" Visible ="true" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/>
                 </asp:TableCell>
             </asp:TableRow>
         </asp:Table>
                   </asp:TableCell>
</asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
       <asp:Label runat="server" Visible ="False" ID="hiddenLblpass"></asp:Label>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButtonPageChange"
                 PopupControlID="Panel3PageChange"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


                            </ContentTemplate>
                            </asp:UpdatePanel>
    </ContentTemplate>
    </cc1:TabPanel>
                        </cc1:tabcontainer>
                        
    
    </asp:Content>