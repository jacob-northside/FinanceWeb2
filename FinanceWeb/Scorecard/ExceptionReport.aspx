<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExceptionReport.aspx.vb" Inherits="FinanceWeb.ExceptionReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title title="Scorecard Exception Report"></title>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
        <asp:Label ID="lblMetricID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblUserID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Metric:  " ></asp:Label>
        <asp:Label ID="lblMetric" runat="server" Text="" Font-Bold="true"  ></asp:Label><br /> 
        <asp:Label ID="Label3" runat="server" Text="Fiscal Year:  "  ></asp:Label> 
        <asp:Label ID="lblFiscalYear" runat="server" Text="" Font-Bold ="true"   ></asp:Label> &nbsp; &nbsp; &nbsp; 
        <asp:Label ID="Label5" runat="server" Text="Fiscal Month:  "  ></asp:Label> 
        <asp:Label ID="lblFiscalMonth" runat="server" Text="" Font-Bold ="true"  ></asp:Label><br />
        <asp:Label ID="Label1" runat="server" Text="Target Value: "  ></asp:Label> 
        <asp:Label ID="lblTargetValue" runat="server" Text="" Font-Bold ="true"  ></asp:Label> &nbsp; &nbsp; &nbsp;  
        <asp:Label ID="Label8" runat="server" Text="Actual Value:  "  ></asp:Label> 
        <asp:Label ID="lblActualValue" runat="server" Text="" Font-Bold ="true"  ></asp:Label><br />

        <asp:Label ID="Label4" runat="server" Text="Excpetion Comment:  "  ></asp:Label> <br />
             <asp:TextBox ID="txtExceptionComment" runat="server" 
            Rows="5" TextMode="MultiLine" Width="399px"  ></asp:TextBox><br />
        <asp:Button ID="btnSubmitComment" runat="server" Text="Submit Comment" /><br /><br /> 

        <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height ="150px">
       
        <asp:GridView ID="gvExceptionHistory" runat="server" 
            AutoGenerateColumns="False" DataSourceID="FeedBack"
         bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"   
          ForeColor="Black"  BackColor="White" BorderWidth="1px" 
         Width="400px" height="100%"  >
           <AlternatingRowStyle BackColor="#FFE885" />        
       
            <Columns>
                <asp:BoundField DataField="SCFeedback" HeaderText="SCFeedback" 
                    SortExpression="SCFeedback" HtmlEncode ="False"  ItemStyle-Width="200px" />
                <asp:BoundField DataField="SCFBDate" HeaderText="SCFBDate" 
                    SortExpression="SCFBDate" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="SCFBOwner" HeaderText="SCFBOwner" 
                    SortExpression="SCFBOwner" />
            </Columns>
            <HeaderStyle BackColor="#214B9A" ForeColor="#FFCBA5" HorizontalAlign="Left" 
                Wrap="True" />
        </asp:GridView>
        <asp:SqlDataSource ID="FeedBack" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="SELECT c.SCFeedback, c.SCFBDate, c.SCFBOwner FROM KPIS.vw_ScorecardData AS a INNER JOIN KPIS.ScorecardFeedback AS c ON c.SCDID = a.SCDID AND c.SCFBActive = 1 INNER JOIN (SELECT SCMID, MAX(SCDFullDate) AS FullDate FROM KPIS.vw_ScorecardData WHERE (SCDID = @SCDID)  GROUP BY SCMID) AS b ON a.SCMID = b.SCMID AND a.SCDFullDate = b.FullDate GROUP BY c.SCFeedback, c.SCFBDate, c.SCFBOwner, c.SCFBID ORDER BY c.SCFBDate DESC, c.SCFBID DESC">
            <SelectParameters>
          
                <asp:QueryStringParameter Name="SCDID" QueryStringField="ModID" />
            </SelectParameters>
        </asp:SqlDataSource>
     </asp:Panel>
    
        <asp:Label ID="lblMetricOwner" runat="server" Text="Metric Owner:  "></asp:Label>
        <br />
        
    </asp:Panel>
   
    </form>
</body>
</html>
