<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="udMDPOSDescriptionMapping.aspx.vb" Inherits="FinanceWeb.udMDPOSDescriptionMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <script>

        function open_win() {
            window.open("https://financeweb.northside.local/Tools/MD/udMDPOSDescMappingInstructions.aspx", "MDPOSDescMappingInstructions", "height=768,width=800, scrollbars, resizable");
        }
        function open_win2() {
            window.open("https://financeweb.northside.local/Tools/MD/udMDPOSDescMappingInstructions.aspx?t=1", "MDPOSDescMappingInstructions", "height=768,width=800, scrollbars, resizable");
        }

    </script>

       <cc1:tabcontainer ID="udMDPOS" runat="server"
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "udMD POS Description Mapping - Unmapped" ID = "tpPOSUnmapped" >
                    <ContentTemplate>    
<asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>
      <asp:Panel runat = "server" HorizontalAlign = "Right" > <asp:Button ID="lbOpenMDPOSInst" runat="server" Text="Open Instructions"  OnClientClick="open_win()"  /> 
          <br />
          

      </asp:Panel>
    <asp:Panel runat ="server" ScrollBars="Auto" Width ="1000px" Height="350px" >
                              <asp:GridView runat="server" ShowHeaderWhenEmpty ="true"  ID="gvUnMapped" AutoGenerateColumns="false" 
                                  BackColor="#CBE3FB" AllowPaging="true" CellSpacing ="5" HeaderStyle-Font-Size ="Smaller"
                                 AllowSorting="true" PageSize="30" Font-Size="Small" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" 
                                  HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5">
                                 <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                          <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                             <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass ="hidden" 
                                                 HeaderStyle-CSSClass ="hidden"
                                                  SortExpression="ID"></asp:BoundField>
                                              <asp:BoundField DataField="IDGroup" HeaderStyle-Width="100px" HeaderText="ID Group" 
                                                  SortExpression="IDGroup"></asp:BoundField>
                                              <asp:BoundField DataField="POSDesc" HeaderText="POS Description"  
                                                  SortExpression="POSDesc"></asp:BoundField>
                                              <asp:BoundField DataField="NP9_BillDr" HeaderText="Bill Dr (NP9)" HeaderStyle-Width="65px"  
                                                  SortExpression="NP9_BillDr"></asp:BoundField>
                                              <asp:BoundField DataField="Entity" HeaderStyle-Width="65px" HeaderText="Entity"  
                                                  SortExpression="Entity"></asp:BoundField>
                                              <asp:BoundField DataField="CostCenter" HeaderStyle-Width="65px"  HeaderText="Cost Center"  
                                                  SortExpression="CostCenter"></asp:BoundField>
                                              <asp:BoundField DataField="VolType" HeaderText="Vol Type" 
                                                  SortExpression="VolType"></asp:BoundField>                                             
                                          </Columns>
                             </asp:GridView>
        <br />
        <br />
        &nbsp;<asp:Label ID="lblAllMapped" Font-Size="Larger" Width="450px" runat="server" Visible="false" BackColor="#CBE3FB" ForeColor="#003060" ><br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;All POS Descriptions Mapped<br />&nbsp;</asp:Label>
    </asp:Panel>

        <br />
        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor ="#003060" BorderStyle="Solid" BorderWidth="1px" Width="800px" >
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">ID Group</asp:TableCell>
                <asp:TableCell><asp:Label runat="server" ID="lblIDGrp"></asp:Label></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">POS Description</asp:TableCell>
                <asp:TableCell ><asp:Label runat="server" ID="lblPOSDesc"></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Bill Dr (NP9)</asp:TableCell>
                <asp:TableCell ><asp:Label runat="server" ID="lblBillDrNP9"></asp:Label></asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Entity</asp:TableCell>
                <asp:TableCell><asp:TextBox Width="80px" runat="server" ID="txtEntity"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Cost Center</asp:TableCell>
                <asp:TableCell><asp:TextBox  Width="80px"  runat="server" ID="txtCostCenter"></asp:TextBox></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">VolType</asp:TableCell>
                <asp:TableCell><asp:TextBox runat="server" ID="txtVolType"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                <asp:Button runat="server" ID="btnUpdateUnmapped" BorderStyle="Outset" BorderWidth="2px"  Text="Update" /></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
        </asp:Table></asp:Panel>
        
               <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                 TargetControlID ="FakeButton"
                 PopupControlID="Panel1"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>
   
    </ContentTemplate>
    </asp:UpdatePanel>
                        </ContentTemplate>
                        </cc1:TabPanel>
 <cc1:TabPanel runat = "server" HeaderText = "udMD POS Description Mapping - Mapped" ID = "tpPOSMapped" >
                    <ContentTemplate>    
<asp:UpdatePanel runat="server" ID= "updMappedMain" ><ContentTemplate>
       
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>
<asp:Button ID="btnOpenMDPOSInst2" runat="server" Text="Open Instructions"  OnClientClick="open_win2()" Width="125px"  /> 
                                                                          </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow><asp:TableCell Width="200px" > 
    <asp:Label runat ="server" ForeColor="#4A8fd2" Text ="Select ID Group: "></asp:Label></asp:TableCell><asp:TableCell Width="600px">
    <asp:DropDownList runat="server" ID="ddlIDGroupLimit"  Font-Size ="12px" AppendDataBoundItems="false" AutoPostBack ="true"></asp:DropDownList></asp:TableCell>
        <asp:TableCell>
            <asp:Button Width="125px" ID="btnExportMDPOS" runat="server" Text="Export to Excel" />
        </asp:TableCell>
        </asp:TableRow><asp:TableRow><asp:TableCell>
    
    <asp:Label runat ="server" ForeColor ="#4A8fd2" Text ="Select POS Description: "></asp:Label></asp:TableCell><asp:TableCell>
    <asp:DropDownList runat="server" ID="ddlDescLimit" Enabled ="false"  Font-Size ="12px" AppendDataBoundItems="false" AutoPostBack ="true"></asp:DropDownList>
    </asp:TableCell>
            

                       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5">
 <asp:Panel runat ="server" ScrollBars="Auto" Width ="1000px" Height="500px" >
                      <asp:GridView runat="server" ID="gvEditMapped" AutoGenerateColumns="false" BackColor="#CBE3FB" AllowPaging="true" CellSpacing ="5" HeaderStyle-Height="40px" HeaderStyle-Wrap="true" 
                                 AllowSorting="true" PageSize="50" Font-Size="Smaller" HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5">
                                 <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                          <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                             <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass ="hidden"
                                                 HeaderStyle-CSSClass ="hidden"
                                                  SortExpression="ID"></asp:BoundField>
                                              <asp:BoundField DataField="IDGroup" HeaderStyle-Width="100px" HeaderText="ID Group" 
                                                  SortExpression="IDGroup"></asp:BoundField>
                                              <asp:BoundField DataField="POSDesc" HeaderText="POS Description" 
                                                  SortExpression="POSDesc"></asp:BoundField>
                                              <asp:BoundField DataField="NP9_BillDr" HeaderText="Bill Dr (NP9)" HeaderStyle-Width="65px"    
                                                  SortExpression="NP9_BillDr"></asp:BoundField>
                                              <asp:BoundField DataField="Entity" HeaderStyle-Width="65px" HeaderText="Entity" 
                                                  SortExpression="Entity"></asp:BoundField>
                                              <asp:BoundField DataField="CostCenter" HeaderStyle-Width="65px"  HeaderText="Cost Center" 
                                                  SortExpression="CostCenter"></asp:BoundField>
                                              <asp:BoundField DataField="VolType" HeaderText="Vol Type" 
                                                  SortExpression="VolType"></asp:BoundField>
                                             <asp:BoundField DataField="DeptDesc" HeaderText="Dept Description"
                                                 SortExpression="DeptDesc"></asp:BoundField>
                                    </Columns>
                             </asp:GridView></asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
   
        <br />
        <asp:Panel runat="server" BackColor="#CBE3FB" BorderColor ="#003060" BorderStyle="Solid" BorderWidth="1px" Width="800px" >
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell Width="10px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">ID Group</asp:TableCell>
                <asp:TableCell><asp:Label runat="server" ID="lblEditIDGrp"></asp:Label></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">POS Description</asp:TableCell>
                <asp:TableCell ><asp:Label runat="server" ID="lblEditPOSDesc"></asp:Label></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Bill Dr (NP9)</asp:TableCell>
                <asp:TableCell ><asp:Label runat="server" ID="lblEditBillDrNP9"></asp:Label></asp:TableCell>
                <asp:TableCell Width="10px"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Entity</asp:TableCell>
                <asp:TableCell><asp:TextBox Width="80px" runat="server" ID="txtEditEntity"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Font-Bold ="true">Cost Center</asp:TableCell>
                <asp:TableCell><asp:TextBox  Width="80px"  runat="server" ID="txtEditCostCenter"></asp:TextBox></asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell Font-Bold ="true">VolType</asp:TableCell>
                <asp:TableCell><asp:TextBox runat="server" ID="txtEditVolType"></asp:TextBox></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Middle">
                <asp:Button runat="server" ID="btnUpdateMapped" BorderStyle="Outset" BorderWidth="2px"  Text="Update" /></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
        </asp:Table></asp:Panel>
        
               <asp:Label ID="FakeButton2" runat = "server" />
   <asp:Panel ID="Panel2" runat="server" Width="233px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explantionlabel2" runat = "server"></asp:label> 
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

    </ContentTemplate>
    <Triggers>

        <asp:PostBackTrigger ControlID="btnExportMDPOS" />

    </Triggers>
    </asp:UpdatePanel>
                        </ContentTemplate>
                        </cc1:TabPanel>

           </cc1:tabcontainer>


</asp:Content>
