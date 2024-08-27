<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OHMS_FeedbackHistory.aspx.vb" Inherits="FinanceWeb.OHMS_FeedbackHistory" %>
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
        <asp:Label ID="Label5" runat="server" Text="Location:  " ></asp:Label>
        <asp:Label ID="lblLocation" runat="server" Text =""  Font-Bold="true" ></asp:Label><br />
        <asp:Label ID="Label3" runat="server" Text="Date:  "  ></asp:Label> 
        <asp:Label ID="lblDate" runat="server" Text="" Font-Bold ="true"   ></asp:Label> <br /> 
        <asp:Label ID="Label1" runat="server" Text="Target Value: "  ></asp:Label>
        <asp:Label ID="lblTargetValue" runat="server" Text="" Font-Bold ="true"  ></asp:Label> <br />  
        <asp:Label ID="Label8" runat="server" Text="Actual Value:  "  ></asp:Label> 
        <asp:Label ID="lblActualValue" runat="server" Text="" Font-Bold ="true"  ></asp:Label><br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Comment:  "  ></asp:Label> <br />
             <asp:TextBox ID="txtMetricComment" runat="server" 
            Rows="5" TextMode="MultiLine" Width="399px"  ></asp:TextBox><br /><br />
        <asp:GridView ID="gvMetricHistory" runat="server" AutoGenerateColumns="False" 
            BackColor="White" bordercolor="#003060" borderStyle="Solid" BorderWidth="1px" 
            DataSourceID="FeedBack" font-Size="Small" ForeColor="Black" 
            HeaderStyle-BackColor="#4A8fd2" HeaderStyle-ForeColor="White" CellPadding="5"
            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true" height="100%" 
            PageSize="25" Width="400px">
            <AlternatingRowStyle BackColor="#FFE885" />
            <Columns>
                <asp:BoundField DataField="Feedback" HeaderText="Comment" 
                    HtmlEncode="False" ItemStyle-Width="200px" SortExpression="SCFeedback" />
                <asp:BoundField DataField="FBDate" DataFormatString="{0:yyyy-MM-dd}" 
                    HeaderText="Date" SortExpression="FBDate" />
                <asp:BoundField DataField="FBUser" HeaderText="User" 
                    SortExpression="FBUser" />
            </Columns>

        </asp:GridView>
        <br />
        <asp:Button ID="btnSubmitComment" runat="server" Text="Submit Comment" /><br /><br /> 
<%--
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height ="150px">
         </asp:Panel>--%>
        <asp:SqlDataSource ID="FeedBack" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
             SELECT c.Feedback, c.FBDate
             , case when  e.FirstName is null and e.LastName is null then c.FBUser else ISNULL(e.FirstName, '') + ISNULL(e.LastName, '') end as FBUser
            FROM DWH.KPIS.DEV_OHMS_Data AS a 
            INNER JOIN DWH.KPIS.DEV_OHMS_Feedback AS c ON c.dID = a.dID AND c.FBActive = 1 and a.Active = 1 
            left join DWH.dbo.Email_Distribution e on c.FBUser = e.NetworkLogin
            where c.dID = @dID
            GROUP BY c.Feedback, c.FBDate, case when  e.FirstName is null and e.LastName is null then c.FBUser else ISNULL(e.FirstName, '') + ISNULL(e.LastName, '') end, c.FBID 
            ORDER BY c.FBDate DESC, c.FBID DESC">
            <SelectParameters>
          
                <asp:QueryStringParameter Name="dID" QueryStringField="ModID" />
            </SelectParameters>
        </asp:SqlDataSource>
   
    
        <asp:Label ID="lblMetricOwner" runat="server" Text="Metric Owner:  "></asp:Label>
        <br />
      <%--  <asp:Button ID="btnCloseForm" runat="server"   Text="Close" />--%>
    </asp:Panel>
   
    </form>
</body>
</html>
