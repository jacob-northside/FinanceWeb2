<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UnderConstruction.aspx.vb" Inherits="FinanceWeb.UnderConstruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">

</asp:Content>
 
<asp:Content ID="Content3" runat="server" contentplaceholderid="MainContent">





     <asp:Label ID="Label1" runat="server" Text="This area of the website is still under construction. Sorry for the inconvenience. " ForeColor="Black"></asp:Label>
    <asp:LinkButton
        ID="lbReturnHome" runat="server" PostBackUrl="~/Default.aspx" >Return to Home Page</asp:LinkButton>
    <br />
        <asp:Table runat="server" ID="tblLescor" BorderColor="#003060" BorderWidth="3px" BorderStyle="Double" BackColor="White" Height="100px" Width="650px">
       
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label Font-Size="Medium" runat="server" ID="lblNewsB" Width="100%" Text="For Access to BIDS Supported Tools and Analytics including LESCOR, you will need to Login in the top right of this page using your IS Security Login. "></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="2px"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    <br />


    <asp:Image ID="Image1" runat="server" Height="516px" 
        ImageUrl="~/Images/Construction2.gif" Width="837px" 
        DescriptionUrl="~/Default.aspx" />
        <br />
    <asp:Timer ID="Timer1" runat="server" Interval="16000">
    </asp:Timer>

</asp:Content>


