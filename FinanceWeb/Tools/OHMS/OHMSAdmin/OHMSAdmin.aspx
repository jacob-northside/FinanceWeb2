<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OHMSAdmin.aspx.vb" Inherits="FinanceWeb.OHMSAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">

     <style type="text/css">

    .hidden   
 {        display: none;    
          }


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

.modalBackground3 
{
    background-color: #D5EAFF !important;
    background-image: none !important;
    border: 1px solid #000000;
    font-size: medium;
    color: #003060;
    width: 300px;
    padding:5px;
    vertical-align:middle;
    text-align:center;
  

}

.panelLimiter
{
    max-height: 450px;
   
}

</style>


  <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


                      <cc1:TabContainer ID="OHMSAdminTabContainer" runat="server" width="1150px"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" BackColor = "#CBE3FB"  >
                    <cc1:TabPanel runat = "server" HeaderText = "Adjust Data" ID = "tpOhmsData" >
                    <ContentTemplate>         
    <asp:UpdatePanel runat="server" ID= "updODMain">

    <ContentTemplate>



       <asp:Panel ID="Panel1" runat = "server" HorizontalAlign = "Right"  > <%--   <asp:Button ID="lbOpenProFeeInst" runat="server" Text="Open Instructions"  OnClientClick="open_win()"  />--%> </asp:Panel>

    <asp:Table ID="Table1" runat = "server"> <asp:TableRow> <asp:TableHeaderCell   ForeColor = "SteelBlue" HorizontalAlign="Right">
    <asp:Label ID="Label2" runat = "server" Font-Size = "small">
    Select Metric:&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableHeaderCell><asp:TableCell HorizontalAlign = "Left" ColumnSpan = "6" Font-Size = "small"> 
        <asp:DropDownList ID="ddlODSelectedMetric" runat="server"  
            DataTextField="SCMName" DataValueField="SCMName" AutoPostBack = "true"
            AppendDataBoundItems="True" Font-Size = "small">
            <asp:listitem value="0">-- Select Metric (optional) --</asp:listitem>
                    </asp:DropDownList>
    </asp:TableCell></asp:TableRow>
    <asp:TableRow>
    <asp:TableHeaderCell  ForeColor = "SteelBlue" HorizontalAlign="Right">Date Range:&nbsp;&nbsp;&nbsp;</asp:TableHeaderCell>
    <asp:TableCell>           
        <asp:DropDownList ID="ddlODStartDate" runat="server"  
            DataTextField="SCMName" DataValueField="SCMName" AutoPostBack = "true"
            AppendDataBoundItems="false" Font-Size = "small">
                    </asp:DropDownList>                       
                 </asp:TableCell>
   <asp:TableCell > and </asp:TableCell>
   <asp:TableCell >                  
        <asp:DropDownList ID="ddlODEndDate" runat="server"  
            DataTextField="SCMName" DataValueField="SCMName" AutoPostBack = "true"
            AppendDataBoundItems="false" Font-Size = "small">
                    </asp:DropDownList>                   
               </asp:TableCell> <asp:TableCell Width = "100"></asp:TableCell>
               <asp:TableCell><asp:CheckBox ID = "chbWhereNull" visible = "true" runat = "server" Text = "Only Null Values" Font-Size = "X-Small" AutoPostBack="True"></asp:CheckBox> </asp:TableCell>

    </asp:TableRow>
    
    </asp:Table>
    <br />
    <asp:Panel runat = "server" ID = "pnlODScrollbars" CssClass = "panelLimiter" ScrollBars = "Auto">
             <asp:GridView ID="gvODData" runat="server" HeaderStyle-BackColor="#D5EAFF"
            HeaderStyle-ForeColor = "#003060" HeaderStyle-Font-Bold = "true"
            AlternatingRowStyle-BackColor = "#eee4ce" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" AllowSorting = "true" AllowPaging="True" PageSize = "30" 
        HorizontalAlign="center" Font-Size="smaller" Width = "100%" >
            <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
            </Columns>
            
    </asp:GridView>
    </asp:Panel><br />
    <asp:Table runat = "server" ID = "tblOD" Visible = "false" BorderColor = "#003060" BorderWidth = "2px">
    <asp:TableRow>
    <asp:TableHeaderCell HorizontalAlign="Right">Metric:</asp:TableHeaderCell>
    <asp:TableCell> 
    <asp:Label ID="lblODMetricName" runat = "server" Font-Size = "small" BackColor = "#eee4ce" ForeColor = "#003060" Font-Bold = "true" Width="400" ></asp:Label>
    </asp:TableCell> <asp:TableCell HorizontalAlign = "Right" Visible = "false" ID= "cellActiveCHBX" >
    <asp:CheckBox ID = "chbxODActive" visible = "true" runat = "server" Text = "Active" Font-Size = "X-Small"></asp:CheckBox>
    </asp:TableCell>
    </asp:TableRow>
        <asp:TableRow>
    <asp:TableHeaderCell HorizontalAlign="Right">Metric Owner:</asp:TableHeaderCell>
    <asp:TableCell>
    <asp:Label ID="lblODMetricOwner" runat = "server" Font-Size = "small" BackColor = "#eee4ce" ForeColor = "#003060" Font-Bold = "true" Width="400" ></asp:Label>
    </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableHeaderCell HorizontalAlign="Right">Date Recorded:</asp:TableHeaderCell>
    <asp:TableCell><asp:Label ID="lblODDateRecorded" runat = "server" Font-Size = "small" BackColor = "#eee4ce" ForeColor = "#003060" Font-Bold = "true" Width="400" > </asp:Label>
    </asp:TableCell>
    </asp:TableRow>
        <asp:TableRow ID = "ODRecordedValue">
    <asp:TableHeaderCell HorizontalAlign="Right">Recorded Value:</asp:TableHeaderCell>
    <asp:TableCell><asp:Textbox runat = "server" ID = "txtODRecValue" ></asp:Textbox>
    <asp:Label ID="lblODRecValue" runat = "server" Font-Size = "small" ForeColor = "#003060" Font-Bold = "true" Width="400" > </asp:Label>
    </asp:TableCell>
    </asp:TableRow>
            <asp:TableRow ID = "ODRecordedNumerator">
    <asp:TableHeaderCell HorizontalAlign="Right">Recorded Numerator:</asp:TableHeaderCell>
    <asp:TableCell><asp:Textbox runat = "server" ID = "txtODRecNum" ></asp:Textbox></asp:TableCell>
    </asp:TableRow>
        <asp:TableRow ID = "ODRecordedDenominator">
    <asp:TableHeaderCell HorizontalAlign="Right">Recorded Denominator:</asp:TableHeaderCell>
    <asp:TableCell><asp:Textbox runat = "server" ID = "txtODRecDen" ></asp:Textbox></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell> 
        <asp:Button runat = "server" Text = "Update Record" ID = "btnODUpdateRecord" /></asp:TableCell></asp:TableRow>
    </asp:Table>

           <asp:Label ID="ODModalFakeButton" runat = "server" />
   <asp:Panel ID="ODModalPanel" runat="server" Width="233px" CssClass = "modalBackground2" >
   <br />
   <asp:label ID = "ODModalExplanationLabel" runat = "server"></asp:label> 
   <asp:Textbox runat = "server" ID = "ODFeedback" ></asp:Textbox>
   <br /> <br />
      <asp:Button ID="ODModalOKButton" runat="server" Font-Size = "small" Text="Continue"/>
      <asp:Button ID="ODModalCancelButton" runat="server" Font-Size = "small" Text="Cancel"/>
      <br />
      <br />
   </asp:Panel>
   <br /> 
   <cc1:ModalPopupExtender ID="ODModalPopupExtender" 
             runat="server" 
             TargetControlID="ODModalFakeButton"
             PopupControlID="ODModalPanel"
             DropShadow="true"/>

                        <asp:Label ID="ODModalFakeButton2" runat = "server" />
   <asp:Panel ID="ODModalPanel2" runat="server" Width="150px" CssClass = "modalBackground3" >
   <br />
   <asp:label ID = "ODModalExplanationLabel2" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="ODModal2CancelButton" runat="server" Font-Size = "small" Text="Close"/>
      <br />
      <br />
   </asp:Panel>
   <br /> 
   <cc1:ModalPopupExtender ID="ODModalPopupExtender2" 
             runat="server" 
             TargetControlID="ODModalFakeButton2"
             PopupControlID="ODModalPanel2"
             DropShadow="true"/>

    </ContentTemplate>
    </asp:UpdatePanel>
    </ContentTemplate>
    </cc1:TabPanel>
    </cc1:TabContainer>

</asp:Content>
