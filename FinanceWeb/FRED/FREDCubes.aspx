<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FREDCubes.aspx.vb" Inherits="FinanceWeb.FREDCubes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <style type="text/css">
  
    </style>
 
</asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"  >
        <scripts>
              <asp:ScriptReference Path="~/Scripts/UpdatePanelScripts.js" />
        </scripts>
     
     </asp:ScriptManagerProxy>

    <div id="FullPage" style="height: auto; width: auto;">
  

 <div id="LeftPnl" style="float:left; width:20%; height: 100%;"  > 
<%-- <div id="LeftPnl" style="float:left; width:250px;  " > --%>
     <asp:Image ID="Image1" runat="server" ImageUrl="FREDFont2.png" 
         Height="46px" Width="246px" />
 
    </div>

    <div id="MiddlePnl" 
            style="float: left; width:60%; height:auto;  padding-left: 5px; clear: none;">
    <%--    <div id="Div1" style="float: left; width:63%; padding-left: 10px;   ">--%>
     
    <asp:Panel ID="Panel2" runat="server" Width="100%" >
 
 
  <iframe runat="server" id="iframepdf" style="width: 632px; height: 544px; background-color: silver;" visible="true">
 </iframe>
  


      
    </asp:Panel></div>
        
          <div id="RightPnl" style="float:right; width: 18%; height:auto;  padding-left: 5px; padding-right: 5px;">
  <%--  <div id="RightPnl" style="float:right; width: 250px; padding-left: 5px; padding-right: 5px;">--%>
   
    </div>
</div>

</asp:Content>
