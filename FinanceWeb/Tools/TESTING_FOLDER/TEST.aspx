<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TEST.aspx.vb" Inherits="FinanceWeb.TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>--%>



<%@ Page Language="C#" %>


<%@ Import Namespace="System.DirectoryServices" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Data" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
<style type="text/css">
body {
font-family: Arial, Verdana;
font-size: 11pt;
font-color: #000000;
margin: 0;
padding: 0 10px 0 10px;
text-align: left
}
</style>


</head>
<body>
<form id="Form1" runat="server">
<asp:ScriptManager ID="AtlasScriptCore" runat="server"
EnablePartialRendering="true" />
<div id="content">
<h3>
Active Directory Searcher</h3>
<div id="searchbox">
Type Users Surname<br />
<asp:TextBox ID="txtFullName" runat="server">
</asp:TextBox>
<asp:Button ID="btnDetails" runat="server" Text="Get AD

Details" OnClick="GetADDetails" />


</div><br />
<div id="Results">
<asp:UpdatePanel ID="UpdatePanel1" runat="server"
UpdateMode="Conditional">
<ContentTemplate>
<asp:GridView ID="ADUserProperties"
runat="server">
</asp:GridView>
<asp:Literal runat="server" ID="SysMessage"
EnableViewState="false" ></asp:Literal>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="btnDetails" />
</Triggers>
</asp:UpdatePanel>


</div>
</div>
</form>
</body>
</html>


