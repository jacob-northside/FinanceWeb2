<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StarGLBalancingSub.aspx.vb" Inherits="FinanceWeb.StarGLBalancingSub" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <title></title>
</head>
<body>


    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
   <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Start Date"></asp:Label> 
      <asp:TextBox ID="StartDateTextBox" runat="server" text="07/01/2013"
        AutoPostBack="True" CausesValidation="True" />
           <cc1:calendarextender ID="CalendarExtender1" 
    runat="server" TargetControlID="StartDateTextBox" 
    PopupButtonID="Image1">
</cc1:calendarextender>  
       <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="   End Date"></asp:Label> 
    <asp:TextBox ID="EndDateTextBox" runat="server" text="07/01/2013"
        AutoPostBack="True" CausesValidation="True" />
    <cc1:calendarextender ID="EndDateTextBox_CalendarExtender" 
    runat="server" TargetControlID="EndDateTextBox" 
    PopupButtonID="Image1">
</cc1:calendarextender>
   <h5> <asp:RadioButtonList ID="rblFacilityFilter" runat="server" 
           RepeatDirection="Horizontal" AutoPostBack="true" CausesValidation="True">
         <asp:ListItem Value="%" Selected ="True"> All</asp:ListItem>
         <asp:ListItem Value="A">Atlanta</asp:ListItem>
         <asp:ListItem Value="C">Cherokee</asp:ListItem>
         <asp:ListItem Value="F">Forsyth</asp:ListItem>
       <asp:ListItem Value="D">Duluth</asp:ListItem>
       <asp:ListItem Value="L">Gwinnett</asp:ListItem>
        </asp:RadioButtonList> </h5>
 
           <asp:Label ID="lblMessage" runat="server" 
        Visible="False" ForeColor="Red"></asp:Label>

<asp:Panel ID="Panel5" runat="server">
  <br /> <asp:Label ID="Label7" runat="server" Text="FARDAP – Suspense activity from the Star Financials."></asp:Label><br />
   <asp:Label ID="Label10" runat="server" 
        Text="This data is for reference purposes only to correct FARGLD suspense activity. "></asp:Label> <br />
    <asp:Label ID="Label3" runat="server" Text="Does not include any entries that do not contain a Revenue Department and/or Patient Indicator."></asp:Label>
   <br /> <br />
      <asp:GridView ID="gvFARDAP" runat="server"  Font-Size="X-Small"
         DataSourceID="SqlDataSource1"    AutoGenerateColumns="False"
      CellPadding="4"
       BorderColor="Black"  BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
         HeaderStyle-ForeColor="white" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
         BackColor="White" BorderWidth="1px" AllowSorting="True"  >
            <AlternatingRowStyle BackColor="#CBE3FB" />
            
        <Columns>
            <asp:BoundField DataField="FY" HeaderText="FY" SortExpression="FY" />
            <asp:BoundField DataField="FM" HeaderText="FM" SortExpression="FM" />
            <asp:BoundField DataField="CY" HeaderText="CY" SortExpression="CY" Visible="false" />
            <asp:BoundField DataField="CM" HeaderText="CM" SortExpression="CM" Visible="false" />
            <asp:BoundField DataField="FAC" HeaderText="FAC" SortExpression="FAC" />
            <asp:BoundField DataField="PAT_ACCT_NBR" HeaderText="PAT_ACCT_NBR" 
                SortExpression="PAT_ACCT_NBR" />
            <asp:BoundField DataField="PAT_NAME" HeaderText="PAT_NAME" 
                SortExpression="PAT_NAME" />
            <asp:BoundField DataField="JNL_CODE" HeaderText="JNL_CODE" 
                SortExpression="JNL_CODE" />
            <asp:BoundField DataField="TRANS_CODE" HeaderText="TRANS_CODE" 
                SortExpression="TRANS_CODE" />
            <asp:BoundField DataField="PAT_IND" HeaderText="PAT_IND" 
                SortExpression="PAT_IND" />
            <asp:BoundField DataField="FIN_CLS" HeaderText="FIN_CLS" 
                SortExpression="FIN_CLS" />
            <asp:BoundField DataField="REV_DEPT" HeaderText="REV_DEPT" 
                SortExpression="REV_DEPT" />
            <asp:BoundField DataField="JNL_AMT" HeaderText="JNL_AMT" 
                SortExpression="JNL_AMT" DataFormatString="{0:C}" />
            <asp:BoundField DataField="JNL_TYPE" HeaderText="JNL_TYPE" 
                SortExpression="JNL_TYPE" />
            <asp:BoundField DataField="RECLASS" HeaderText="RECLASS" 
                SortExpression="RECLASS" />
            <asp:BoundField DataField="REV_TYPE" HeaderText="REV_TYPE" 
                SortExpression="REV_TYPE" />
            <asp:BoundField DataField="TRAN_DATE" HeaderText="TRAN_DATE" 
                SortExpression="TRAN_DATE" DataFormatString="{0:d}" />
            <asp:BoundField DataField="FARDAP_DATE" HeaderText="FARDAP_DATE" 
                SortExpression="FARDAP_DATE" DataFormatString="{0:d}" />
        </Columns>
        <PagerSettings Position="TopAndBottom" />
     <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
   <EditRowStyle BackColor="#2461BF" />
      
           <HeaderStyle BackColor="#4A8fd2" Font-Bold="True" ForeColor="white" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
			
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
        
        SelectCommand="SELECT FY, FM, CY, CM, FAC, PAT_ACCT_NBR, PAT_NAME, JNL_CODE, TRANS_CODE, PAT_IND, FIN_CLS, REV_DEPT, JNL_AMT, JNL_TYPE, RECLASS, REV_TYPE, TRAN_DATE, FARDAP_DATE FROM DWH.GL.FARDAP where TRAN_DATE between @StartDate and @EndDate  and (PAT_IND is not null  and REV_DEPT is not null) and FAC like @FAC  order by FAC, TRAN_DATE desc, PAT_ACCT_NBR  desc, JNL_AMT desc " >

    <SelectParameters>
        <asp:ControlParameter ControlID="StartDateTextBox" Name="StartDate"  DefaultValue="(dateadd(Day,-3, SYSDATETIMe()))" 
            PropertyName="Text" />
 <asp:ControlParameter ControlID="EndDateTextBox" Name="EndDate"  DefaultValue="SYSDATETIMe()" PropertyName="Text" />

        <asp:ControlParameter ControlID="rblFacilityFilter" DefaultValue="%" Name="FAC" 
            PropertyName="SelectedValue" />

    </SelectParameters>
    </asp:SqlDataSource>

   <h5> <asp:Label ID="Label4" runat="server" Text="For reference purposes only: Pat_Ind: O - reclasses SubAcct to 4002,  I - reclasses SubAcct to 4001, E - reclasses SubAcct to 4002.  "></asp:Label></h5>


            </asp:Panel>
          </div>
    </form>
</body>
</html>
