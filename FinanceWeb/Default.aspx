<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="FinanceWeb._Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <style type="text/css">
.Background1 
{
    background-image:  url(Images/HomeImages/Northside_Night_Bankshot_72DPI.jpg);
    height:762px;
    width:1142px;
    background-size:100%;

}

.Background3
{
    background-image: url(Images/HomeImages/4122-05.jpg);
    height:762px;
    width:1142px;
    background-size:100%;
        
}

.OpacityBox 
{
    position:relative;
    background: rgb(204, 204, 204); /* Fallback for older browsers without RGBA-support */
    background: rgba(204, 204, 204, 0.4);
        
}

.OpacityBox3 
{
    position:relative;
    background: rgb(204, 204, 204); /* Fallback for older browsers without RGBA-support */
    background: rgba(204, 204, 204, 0.6);
        
}
        </style>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <%--<asp:Timer runat="server" ID="TimerImages" Interval="3000"  ></asp:Timer>
    <section class="featured">
        <div>
             <asp:UpdatePanel ID="BannerUpdatePanel" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TimerImages" />
                </Triggers>
                <ContentTemplate>
                    
                        <asp:Table runat="server"  Width="99%" CellPadding="0" Height="200px" BackColor ="#7ac0da" BorderColor="#003060" BorderWidth="5px" CellSpacing="0"><asp:TableRow>
                            <asp:TableCell  Width="450px" VerticalAlign="Middle" HorizontalAlign="Center" Height="198px" >
                    <asp:Image runat="server" ID="BannerImage1" src="Images/DevImages/NSH.png" Height="200px" />
                    <asp:Image runat="server" ID="BannerImage2" Visible="false" src="Images/DevImages/542725761.png" Height="200px"/>
                    <asp:Image runat="server" ID="BannerImage3" Visible="false" src="Images/DevImages/171605160.png" Height="200px"/>
                    <asp:Image runat="server" ID="BannerImage4" Visible="false" src="Images/DevImages/477762599.png" Height="200px"/>
                    <asp:Image runat="server" ID="BannerImage5" Visible="false" src="Images/DevImages/134367900.png" Height="200px"/>
                    <asp:Image runat="server" ID="BannerImage6" Visible="false" src="Images/DevImages/667586499.png" Height="200px"/>                               
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Middle">
                    <asp:Label ForeColor="#003060" Font-Size="X-Large" ID="BannerWords" runat="server" Text="Northside Hospital - Finance Division"></asp:Label>
                            </asp:TableCell>
                                                  </asp:TableRow> </asp:Table>

                </ContentTemplate>
            </asp:UpdatePanel>
             
        </div>
    </section>--%>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>--%>
    <b> <asp:Label runat="server" Font-Size="Larger"> Welcome to the Financial Division Home Page</asp:Label></b><br />
    If you are new to the Finance Division Website, please login in the top right to access the tools.<br />
    <br />
    <asp:Panel ID="pnlBack" runat="server" CssClass="Background1">
        <asp:Table Visible="true" ID="tblFront" runat="server" CssClass="OpacityBox" Width="100%" Height="100%">
            <asp:TableRow>
                <asp:TableCell Height="50px"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="50px"></asp:TableCell>
                <asp:TableCell  BorderColor="#003060" BorderWidth="3px" BorderStyle="Double" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#eee4ce" ForeColor="#003060" Font-Bold="true" Font-Size="Large">
                    Finance Web News
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell BorderColor="#003060" BorderWidth="3px" BorderStyle="Double" BackColor="White" Height="100px" Width="650px">
                    <asp:Table runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell Width="2px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:Label Font-Size="Large" Font-Bold="true" runat="server" Text="Business Intelligence & Data Strategies<br>Support Phone Line" Width="100%"></asp:Label><br />
                                <asp:Label Font-Size="X-Large" runat="server" Text="404-255-5278<br>" Width="100%"></asp:Label><br />for BIDS supported data and processes
                            </asp:TableCell>
                        </asp:TableRow>
                       
                        <asp:TableRow>
                            <asp:TableCell Height="2px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                
                <asp:TableCell BorderColor="#003060" BorderWidth="3px" BorderStyle="Double" BackColor="White" Height="100px" Width="650px">
                    <asp:Table runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label Font-Size="Medium" runat="server" ID="lblNewsC" Width="100%"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>                     
                        <asp:TableRow>
                            <asp:TableCell Width="2px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                                <asp:Label Font-Size="Large" runat="server" ID="lblNewsA" Width="100%"></asp:Label>
                            </asp:TableCell>
                         
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label Font-Size="Medium" runat="server" ID="lblNewsB" Width="100%"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Height="2px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    
                    
                </asp:TableCell>                
                <%--<asp:TableCell Width="50px"></asp:TableCell>
                <asp:TableCell BorderColor="#003060" BorderWidth="1px" BorderStyle="Double" BackColor="White" Height="50px" Width="300px" HorizontalAlign="Center">Image2</asp:TableCell>--%>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
           <%-- <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell BorderColor="#003060" BorderWidth="1px" BorderStyle="Double" BackColor="White" Height="50px" Width="300px" HorizontalAlign="Center">Image3</asp:TableCell>
                <asp:TableCell Width="50px"></asp:TableCell>
                <asp:TableCell BorderColor="#003060" BorderWidth="1px" BorderStyle="Double" BackColor="White" Height="50px" Width="300px" HorizontalAlign="Center">Image4</asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>



</asp:Content>
