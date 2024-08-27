<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MediaCenter.aspx.vb" Inherits="FinanceWeb.MediaCenter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<%--<style type="text/css">
body {
text-align:center;
}
</style>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >


<asp:Panel runat = "server" HorizontalAlign = "center" > 
<br />

<span id="video" >
<object id="vidplayer" name="player" width="600" height="400" 
      classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" 
      codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701"
      standby="Loading Microsoft Windows Media player components..." type="application/x-oleobject">
      <PARAM NAME="url" VALUE="<% =FileName %>"> 
      <param name="uiMode" value="full">
      <param name="autoStart" value="true">
      <param name="loop" value="true">
      <param name="fullScreen" value = "<% =BoolFullScreen %>" />
      <embed type="application/x-mplayer2" 
      pluginspage="http://microsoft.com/windows/mediaplayer/en/download/" 
      showcontrols="true" uimode="full" width="240" height="320" 
      src="" autostart="true" loop="true">
</object><br>
</span><br />
        <asp:DropDownList ID="ddlSelectedVideo" runat="server"  
            DataTextField="DisplayName" DataValueField="urlName" AutoPostBack = "true"
            AppendDataBoundItems="True" Font-Size = "small">
            <asp:listitem value="">-- Select Value --</asp:listitem>
            <asp:listitem value="../../Videos/Wildlife.wmv"> Wildlife </asp:listitem>
                    <asp:listitem value="../../Videos/WFPPS Instructional Video FIN.wmv"> WFPPS </asp:listitem>
            
                    </asp:DropDownList>
  
<asp:Button runat = "server" id = "BtnFullScreen" text = "View Full Screen" />
</asp:Panel>

</asp:Content>


