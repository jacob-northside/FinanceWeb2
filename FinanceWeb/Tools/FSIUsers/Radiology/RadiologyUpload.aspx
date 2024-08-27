<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RadiologyUpload.aspx.vb" Inherits="FinanceWeb.RadiologyUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PanelUpload" runat="server" Visible="True">
        Radiology HCPCS Codes:
    Please select an Excel file to import:<br />
    <asp:FileUpload ID="FileUploadExcel" runat="server" />
    <br /><br />   
    <asp:Button ID="ButtonUploadFile" runat="server" Text="Upload File" />
    <br /><br />
    <asp:Label ID="LabelUpload" runat="server" Text=""></asp:Label>



 </asp:Panel> 

</asp:Content>
