<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FeedbackHistory.aspx.vb" Inherits="FinanceWeb.FeedbackHistory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title title="Scorecard Feedback History"></title>
   
</head>
<body style="background-color:#d5eaff;">
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

        <asp:Label ID="Label4" runat="server" Text="Exception Comment:  "  ></asp:Label> <br />
             <asp:TextBox ID="txtMetricComment" runat="server" 
            Rows="5" TextMode="MultiLine" Width="399px"  ></asp:TextBox>
        <asp:GridView ID="gvMetricHistory" runat="server" AutoGenerateColumns="False" 
            BackColor="White" bordercolor="Black" borderStyle="Solid" BorderWidth="1px" 
            DataSourceID="FeedBack" font-Size="Small" ForeColor="Black" 
            HeaderStyle-BackColor="#214B9A" HeaderStyle-ForeColor="#FFCBA5" 
            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" height="100%" 
            PageSize="25" Width="400px">
            <AlternatingRowStyle BackColor="#FFE885" />
            <Columns>
                <asp:BoundField DataField="SCFeedback" HeaderText="SCFeedback" 
                    HtmlEncode="False" ItemStyle-Width="200px" SortExpression="SCFeedback" />
                <asp:BoundField DataField="SCFBDate" DataFormatString="{0:yyyy-MM-dd}" 
                    HeaderText="SCFBDate" SortExpression="SCFBDate" />
                <asp:BoundField DataField="SCFBOwner" HeaderText="SCFBOwner" 
                    SortExpression="SCFBOwner" />
            </Columns>
            <HeaderStyle BackColor="#214B9A" ForeColor="#FFCBA5" HorizontalAlign="Left" 
                Wrap="True" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnSubmitComment" runat="server" Text="Submit Comment" /><br /><br /> 
<%--
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height ="150px">
         </asp:Panel>--%>
        <asp:SqlDataSource ID="FeedBack" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
             SELECT c.SCFeedback, c.SCFBDate, c.SCFBOwner 
            FROM KPIS.vw_ScorecardData AS a 
            INNER JOIN KPIS.ScorecardFeedback AS c ON c.SCDID = a.SCDID AND c.SCFBActive = 1 
            where c.SCDID = @SCDID
            GROUP BY c.SCFeedback, c.SCFBDate, c.SCFBOwner, c.SCFBID ORDER BY c.SCFBDate DESC, c.SCFBID DESC">
            <SelectParameters>
          
                <asp:QueryStringParameter Name="SCDID" QueryStringField="ModID" />
            </SelectParameters>
        </asp:SqlDataSource>
   
    
        <asp:Label ID="lblMetricOwner" runat="server" Text="Metric Owner:  "></asp:Label>
        <br />
      <%--  <asp:Button ID="btnCloseForm" runat="server"   Text="Close" />--%>
    </asp:Panel>
   
    </form>
</body>
</html>
