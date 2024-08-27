<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OHMS.aspx.vb" Inherits="FinanceWeb.OHMS" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <style type="text/css">

.modalBackground2 
{
    background-color: #eee4ce !important;
    background-image: none !important;
    border: 1px solid #000000;
    font-size: medium;
    color: #003060;
    width: 300px;
    padding:5px;
    vertical-align:middle;
    text-align:center;
    
}
</style>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
        <script>
            // It is important to place this JavaScript code after ScriptManager1
            var xPos, yPos;
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            function BeginRequestHandler(sender, args) {
                if ($get('<%=ScrollPanel.ClientID%>') != null) {
                    // Get X and Y positions of scrollbar before the partial postback
                    xPos = $get('<%=ScrollPanel.ClientID%>').scrollLeft;
                    yPos = $get('<%=ScrollPanel.ClientID%>').scrollTop;
                }
            }

            function EndRequestHandler(sender, args) {
                if ($get('<%=ScrollPanel.ClientID%>') != null) {
                    // Set X and Y positions back to the scrollbar
                    // after partial postback
                    $get('<%=ScrollPanel.ClientID%>').scrollLeft = xPos;
                    $get('<%=ScrollPanel.ClientID%>').scrollTop = yPos;
                }
            }

            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);

            function open_win() {
                window.open("http://nsmvwebsvr02/FinanceWeb/Tools/ManagedCare/Profee/ProFeeInstructions.aspx", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
            }
 
</script> 

                    <asp:SqlDataSource ID="dsOhmsMetricSCMName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="Select case when SCMOwner is NULL then 'NULL' else SCMOwner end as SCMOwner, case when FirstName is NULL and LastName is NULL then SCMOwner else FirstName + ' ' + LastName end as Name from DWH.KPIS.ScorecardMetric met left join DWH.dbo.Email_Distribution dbo on met.SCMOwner = dbo.NetworkLogin group by SCMOwner, FirstName, LastName
Order by case when FirstName is NULL then 1 else 0 end asc, LastName asc, FirstName asc"></asp:SqlDataSource>       

                <asp:SqlDataSource ID="dsOhmsTitle" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select * FROM DWH.KPIS.ScorecardTitle_LU ">
                </asp:SqlDataSource> 
                 
               <asp:SqlDataSource ID="dsOhmsULUName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select SCUName, case when FirstName is NULL and LastName is NULL then SCUName else FirstName + ' ' + LastName end as Name FROM DWH.KPIS.ScorecardUser_LU ul left JOIN DWH.dbo.Email_Distribution ed on ul.SCUName = ed.NetworkLogin group by SCUName, FirstName, LastName
Order by case when FirstName is NULL then 1 else 0 end asc, LastName asc, FirstName asc">
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="dsOhmsMetricSCMID" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="Select SCMName, SCMID from (

select RANK() over (partition by SCMID order by SCMEffectiveToDate desc, SCMActive desc, ID desc) as rankin, SCMID, SCMName, SCMActive from DWH.KPIS.ScorecardMetric) a
where rankin = 1 and SCMActive = 1

Order by SCMName asc"></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsMetricGUITarType" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
select Distinct case when SCMDataType is NULL then 'raw' else SCMDataType end as SCMDataType from DWH.KPIS.ScorecardMetric order by case when SCMDataType is NULL then 'raw' else SCMDataType end asc"></asp:SqlDataSource>
                

       <asp:SqlDataSource ID="dsMetricGUICatLU" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
                declare @Value varchar(max)
                set @Value = '-- Enter New Category --'

	            select SCCategory, SCCID from (
	
                SELECT SCCategory, RANK() over (order by SCCategory asc) as rankin, SCCID FROM KPIS.ScorecardCategory_LU WHERE (SCCActive = 1) 

                Union

                Select  Distinct SCCategory, 99999999999 as rankin, 0 as SCCID from (select @Value as SCCategory from KPIS.ScorecardCategory_LU ) s
                      ) a      
                            order by rankin asc    
            "></asp:SqlDataSource>
                
                <cc1:tabcontainer ID="OhmsAdminTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "true" >
                    <cc1:TabPanel runat = "server" HeaderText = "Scorecard Data" ID = "OhmsAdminSD">
                    <ContentTemplate>        
                    
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
         <asp:Panel runat = "server">
         <asp:Panel runat = "server" > 
         <asp:Label ID= "labelwastxtbox1"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Scorecard Data </asp:Label><br /><br />
                       Select * FROM ScorecardData<br /> WHERE<br />
                 <asp:Table ID="Table1" runat="server" CellPadding="5"
                GridLines="horizontal" HorizontalAlign="left">
                   <asp:TableRow>
                     <asp:TableCell>Current Metric Name = </asp:TableCell>
                     <asp:TableCell><asp:DropDownList 
                    ID="namedscorecarddata" runat="server" DataSourceID="dsOhmsMetricSCMID" 
                    DataTextField="SCMName" DataValueField="SCMID"></asp:DropDownList> </asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton1" groupname = 'IDonoff' runat="server" Text="Yes" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton2" GroupName = 'IDonoff' runat="server" Text="No" Checked="True" /></asp:TableCell>
                     </asp:TableRow>
                   <asp:TableRow>
                   <asp:TableCell>Metric Owner = </asp:TableCell>
                   <asp:TableCell><asp:DropDownList ID = "ddldataowner" runat = "server" DataSourceID="dsOhmsMetricSCMName" DataTextField="Name" 
                    DataValueField="SCMOwner"></asp:DropDownList>
                   </asp:TableCell>
                   <asp:TableCell><asp:RadioButton ID="RadioButton9" groupname = 'dataMOwner' runat="server" Text="Yes" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton10" GroupName = 'dataMOwner' runat="server" Text="No" Checked="True" /></asp:TableCell>
                   </asp:TableRow><asp:TableRow>
                     <asp:TableCell>SCDFY = </asp:TableCell>
                     <asp:TableCell>                
                         <asp:DropDownList ID="ddlSCDFY" runat="server" DataSourceID="dsSCDFY" 
                    DataTextField="SCDFY" DataValueField="SCDFY">
                       </asp:DropDownList></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton3" runat="server" GroupName="SCDFYonoff" 
                    Text="Yes" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton4" runat="server" GroupName="SCDFYonoff" 
                    Text="No" Checked="True" /></asp:TableCell>
                     </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCDFM = </asp:TableCell> 
                     <asp:TableCell>                
                       <asp:DropDownList ID="ddlSCDFM" runat="server" DataSourceID="dsDFM" 
                    DataTextField="SCDFM" DataValueField="SCDFM">
                       </asp:DropDownList></asp:TableCell>
                     <asp:TableCell>                
                       <asp:RadioButton ID="RadioButton5" runat="server" GroupName="FMonoff" 
                    Text="Yes" /></asp:TableCell>
                     <asp:TableCell>                
                       <asp:RadioButton ID="RadioButton6" runat="server" GroupName="FMonoff" 
                    Text="No" Checked="True" /></asp:TableCell>
                     </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCDActual is </asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID = "rbNull" runat = "server" GroupName = "nullnot" Text = "NULL" Checked = "true" /> 
                     &nbsp;&nbsp;&nbsp;<asp:RadioButton ID = "rbNotNull" runat = "server" GroupName = "nullnot" Text = "NOT NULL"/>
                     </asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton7" runat="server" 
                    GroupName="isnull" Text="Yes" Checked="True" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton8" runat="server" GroupName="isnull" 
                    Text="No" /></asp:TableCell>
                     </asp:TableRow>
                   
                </asp:Table>

               </asp:Panel>
               <br />


                
                
                <asp:SqlDataSource ID="dsSCDFY" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    
                 SelectCommand="Select SCDFY from DWH.KPIS.ScorecardData Group by SCDFY Order by SCDFY desc">
                </asp:SqlDataSource>
                <br /> &nbsp;

                <asp:SqlDataSource ID="dsDFM" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select SCDFM from DWH.KPIS.ScorecardData group by SCDFM
Order by SCDFM asc">
                </asp:SqlDataSource>

                


                
<asp:SqlDataSource ID="dsSelectID" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    
                    SelectCommand="SELECT SCMName FROM KPIS.ScorecardMetric GROUP BY SCMName
Order by SCMName asc">
                </asp:SqlDataSource>

             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />

             <br />
<asp:Panel runat = "server"> 
<br />
                <asp:Button ID="btnSCData" runat="server" Text="Execute" />
                <br /> <br /> </asp:Panel>
                <br />
                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                     DataKeyNames="SCDID" DataSourceID="dsOhmsData2" AllowPaging="True" 
                    AllowSorting="True" PageSize="20" BackColor="White" 
                 BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <AlternatingRowStyle BackColor="#EEE4CE" />
                    <Columns>
                        <asp:BoundField DataField = "ID" HeaderText = "ID" SortExpression="ID"/>
                        <asp:BoundField DataField = "SCMName" HeaderText = "SCMName" 
                            SortExpression = "SCMName"/>
                        <asp:BoundField DataField = "SCMOwner" HeaderText = "SCMOwner" 
                            SortExpression = "SCMOwner"/>
                        <asp:BoundField DataField="SCDID" HeaderText="SCDID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="SCDID" />
                        <asp:CheckBoxField DataField="SCDActive" HeaderText="SCDActive" 
                            SortExpression="SCDActive" />
                        <asp:BoundField DataField="SCDActual" HeaderText="SCDActual" 
                            SortExpression="SCDActual" />
                        <asp:BoundField DataField="SCDFY" HeaderText="SCDFY" SortExpression="SCDFY" />
                        <asp:BoundField DataField="SCDFM" HeaderText="SCDFM" SortExpression="SCDFM" />
                        <asp:BoundField DataField="SCDFD" HeaderText="SCDFD" SortExpression="SCDFD" />
                        <asp:BoundField DataField = "SCDModifyDate" HeaderText = "SCDModifyDate" 
                            SortExpression = "SCDModifyDate" />
                        <asp:BoundField DataField = "SCDUpdated" HeaderText = "SCDUpdated" 
                            SortExpression = "SCDUpdated" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <asp:SqlDataSource ID="dsOhmsData2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    
                    
                    SelectCommand=" 
                    SELECT d.ID, m.SCMName, m.SCMOwner, d.SCDID, d.SCDActive, d.SCDActual, d.SCDFY, d.SCDFM, d.SCDFD, 
d.SCDModifyDate, d.SCDUpdated  FROM DWH.KPIS.[ScorecardData] d 
left join DWH.KPIS.ScorecardMetric m on d.ID = m.ID
WHERE (SCMID = @SCMID or @Val = 0) 
and ([SCDFY] = @SCDFYselect or @Val2 = 0) and ([SCDFM] = @SCDFMselect or @Val3 = 0) 
and ((SCDActual IS NULL or @NULLyn = 0) or @Val4 = 0)
and ((SCDActual IS NOT NULL or @NULLyn = 1) or @Val4 = 0)
and (m.SCMOwner = @MOwner or @Val5 = 0)" 
                    
                    UpdateCommand="UPDATE DWH.KPIS.ScorecardData SET SCDFY = @SCDFY, SCDFM = @SCDFM, SCDFD = @SCDFD, ID = @ID, SCDActual = @SCDActual, SCDActive = @SCDActive, SCDModifyDate = @SCDModifyDate, SCDUpdated = @SCDUpdated WHERE (SCDID = @SCDID)" 
                    >

                    <SelectParameters>
                        <asp:ControlParameter ControlID="namedscorecarddata" Name="SCMID" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton1" Name="Val" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="ddlSCDFY" Name="SCDFYselect" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton3" Name="Val2" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="ddlSCDFM" Name="SCDFMselect" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton5" Name="Val3" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="rbNULL" Name="NULLyn" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="RadioButton7" Name="Val4" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="ddlDataOwner" Name="MOwner" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton9" Name="Val5" 
                            PropertyName="Checked" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SCDFY" />
                        <asp:Parameter Name="SCDFM" />
                        <asp:Parameter Name="SCDFD" />
                        <asp:Parameter Name="ID" />
                        <asp:Parameter Name="SCDActual" />
                        <asp:Parameter Name="SCDActive" />
                        <asp:Parameter Name="SCDModifyDate" />
                        <asp:Parameter Name="SCDUpdated" />
                        <asp:Parameter Name="SCDID" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <br />
                <br />
                </asp:Panel>
             </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSCData" EventName="Click" />
            </Triggers>
             
                        </asp:UpdatePanel>      

                <br />
        Insert Into ScorecardData<br />
        <br />
        <asp:Table runat = "server">
        <asp:TableRow runat="server">
        <asp:TableHeaderCell HorizontalAlign = "Left" runat="server">
        
        ID: </asp:TableHeaderCell><asp:TableCell runat="server"><asp:TextBox ID="txtsdID" runat="server" ></asp:TextBox>
</asp:TableCell><asp:TableCell Font-Size = "XX-Small" runat="server" > Go to Scorecard Metric to find IDs </asp:TableCell>
        </asp:TableRow><asp:TableRow runat="server">
                <asp:TableHeaderCell HorizontalAlign = "Left" runat="server">
        Actual Value:</asp:TableHeaderCell><asp:TableCell runat="server"><asp:TextBox ID="txtSDSCDActual" runat="server"></asp:TextBox>
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableHeaderCell HorizontalAlign = "Left" runat="server">
        Fiscal Year:</asp:TableHeaderCell><asp:TableCell runat="server"><asp:TextBox ID="txtSDSCDFY" runat="server"></asp:TextBox>
</asp:TableCell></asp:TableRow><asp:TableRow runat="server">
                <asp:TableHeaderCell HorizontalAlign = "Left" runat="server">
        Fiscal Month:</asp:TableHeaderCell><asp:TableCell runat="server"><asp:TextBox ID="txtSDSCDFM" runat="server"></asp:TextBox>
</asp:TableCell> </asp:TableRow><asp:TableRow runat="server">
                <asp:TableHeaderCell HorizontalAlign = "Left" runat="server">
        Fiscal Day:</asp:TableHeaderCell><asp:TableCell runat="server"><asp:TextBox ID="txtSDSCDFD" runat="server"></asp:TextBox>
</asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <asp:Button ID="SDInsertButton" runat="server" Text="Insert" />
        <br />

        <asp:Label ID="FakeButtonLabelData" runat = "server" />
   <asp:Panel ID="DataPopupPanel" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
<asp:label ID = "DataExplanationlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="Button4" runat="server" Text="OK" />

      
      <br />
      <br />

   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="DataModalPopupExtender" 
             runat="server" 
             TargetControlID="FakeButtonLabelData"
             PopupControlID="DataPopupPanel"
             DropShadow="True" DynamicServicePath="" BehaviorID="_content_DataModalPopupExtender"/>



                     </ContentTemplate>            
                    </cc1:TabPanel>

<cc1:TabPanel runat = "server" HeaderText = "Scorecard Metric" ID = "OhmsAdminSM">
                    <ContentTemplate>
                                        
         <asp:Label ID= "label3"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Scorecard Metric </asp:Label> <br /><br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel runat = "server" ID = "OHMSSMPANEL" ScrollBars = "Horizontal">
                Select * FROM ScorecardMetric<br /> WHERE
                <br />
                               <asp:Table ID="Table2" runat="server" CellPadding="5"
                GridLines="horizontal" HorizontalAlign="left">
                   <asp:TableRow>
                     <asp:TableCell>SCMID = </asp:TableCell>
                     <asp:TableCell>                <asp:DropDownList ID="ddlMetricSCMID" runat="server" 
                    DataSourceID="dsOhmsMetricSCMID" DataTextField="SCMName" DataValueField="SCMID">
                       </asp:DropDownList> </asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbMetricSCMID1" runat="server" GroupName="rbMetricSCMID" 
                    Text="True" /></asp:TableCell>
                     <asp:TableCell>               <asp:RadioButton ID="rbMetricSCMID2" runat="server" GroupName="rbMetricSCMID" 
                    Text="False" Checked="True" /></asp:TableCell>
                                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCMOwner = </asp:TableCell>
                     <asp:TableCell>                 <asp:DropDownList ID="ddlMetricSCMOwner" runat="server" 
                    DataSourceID="dsOhmsMetricSCMName" DataTextField="Name" 
                    DataValueField="SCMOwner">
                       </asp:DropDownList></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbMetricSCOwner1" runat="server" 
                    GroupName="rbMetricSCOwner" Text="True" /></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbMetricSCOwner2" runat="server" 
                    GroupName="rbMetricSCOwner" Text="False" Checked="True" /></asp:TableCell>
                                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCMCategory = </asp:TableCell> 
                     <asp:TableCell>                <asp:DropDownList ID="ddlMetricSCCat" runat="server" 
                    DataSourceID="dsOhmsMetricSCCat" DataTextField="SCCategory" 
                    DataValueField="SCMCategory">
                       </asp:DropDownList> </asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbMetricSCCat1" runat="server" GroupName="rbMetricSCCat" 
                    Text="True" /> </asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbMetricSCCat2" runat="server" GroupName="rbMetricSCCat" 
                    Text="False" Checked="True" /> </asp:TableCell>
                                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCMActive = </asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID = "rbMetActive" runat = "server" GroupName = "rbactiveonezero" Text = "1" Checked="True" />
                     <asp:RadioButton ID = "rbMetNotActive" runat = "server" GroupName = "rbactiveonezero" Text = "0" />
                     </asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbMetricActive1" runat="server" GroupName="rbMetricActive" 
                    Text="True" /></asp:TableCell>
                     <asp:TableCell>                                    <asp:RadioButton ID="RrbMetricActive2" runat="server" GroupName="rbMetricActive" 
                    Text="False" Checked="True" /></asp:TableCell>
                                   </asp:TableRow>
                   
                    </asp:Table>
               
                <br />
                <br />
                <br />
                <br />
                <asp:SqlDataSource ID="dsOhmsMetricSCCat" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select SCMCategory, SCCategory from DWH.KPIS.ScorecardMetric met join DWH.KPIS.ScorecardCategory_LU cat on met.SCMCategory = cat.SCCID group by SCMCategory, SCCategory order by SCCategory asc">
                    </asp:SqlDataSource>

                <br />
                <br />
                <br />
                <br />

                <br />
                <asp:Button ID="Button2" runat="server" Text="Execute" />
                <br />
                <br />
                <asp:Panel ID = "ScrollPanel" runat = "server" ScrollBars = "Vertical" Height = "300"> 
                <asp:GridView ID="GridView1" runat="server" AllowPaging="False" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="dsOHMS" Width="100%" BackColor="White" BorderColor="#CCCCCC" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <AlternatingRowStyle BackColor="#EEE4CE" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="SCMID" HeaderText="SCMID" SortExpression="SCMID" />
                        <asp:BoundField DataField="SCMName" HeaderText="SCMName" 
                            SortExpression="SCMName" />
                        <asp:BoundField DataField="SCMTarget" HeaderText="SCMTarget" 
                            SortExpression="SCMTarget" />
                        <asp:BoundField DataField="SCMOwner" HeaderText="SCMOwner" 
                            SortExpression="SCMOwner" />
                        <asp:BoundField DataField="SCMCategory" HeaderText="SCMCategory" 
                            SortExpression="SCMCategory" />
                        <asp:BoundField DataField="SCMEffectiveFromDate" 
                            HeaderText="SCMEffectiveFromDate" SortExpression="SCMEffectiveFromDate" />
                        <asp:BoundField DataField="SCMEffectiveToDate" HeaderText="SCMEffectiveToDate" 
                            SortExpression="SCMEffectiveToDate" />
                        <asp:CheckBoxField DataField="SCMActive" HeaderText="SCMActive" 
                            SortExpression="SCMActive" />
                        <asp:BoundField DataField="SCMUpdated" HeaderText="SCMUpdated" 
                            SortExpression="SCMUpdated" />
                        
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView></asp:Panel>
                <asp:SqlDataSource ID="dsOHMS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    
                    
                    
                    InsertCommand="INSERT INTO KPIS.ScorecardMetric(SCMName) VALUES (@InsertName)" SelectCommand="SELECT [ID]
      ,[SCMID]
      ,[SCMName]
      ,[SCMObjective]
      ,[SCMMeasures]
      ,[SCMDefinition]
      ,[SCMCalculations]
      ,[SCMSourceSystem]
      ,[SCMLTTarget]
      ,[SCMLTGoalDate]
      ,[SCMTarget]
      ,[SCMDataType]
      ,[SCMCumulative]
      ,[SCMMax]
      ,[SCMMin]
      ,[SCMwMax]
      ,[SCMwMin]
      ,[SCMUpdateMethod]
      ,[SCMUpdateInput]
      ,[SCMUpdateFrequency]
      ,[SCMOwner]
      ,[SCMCategory]
      ,[SCMEffectiveFromDate]
      ,[SCMEffectiveToDate]
      ,[SCMActive]
      ,[SCMUpdated]
  FROM [DWH].[KPIS].[ScorecardMetric]
WHERE (SCMID = @SCMID or @SCMIDButton = 0)
and ((@SCMOwner = 'NULL' and SCMOwner is NULL or SCMOwner = @SCMOwner) or @SCMOwnerButton = 0)
and (SCMCategory = @SCMCategory or @SCMCategoryButton = 0)
and (SCMActive = @SCMActive or @SCMActiveButton = 0) 
order by ID asc" 
                    
                    
                    
                    UpdateCommand="UPDATE KPIS.ScorecardMetric SET SCMID = @SCMID, SCMName = @SCMName, SCMObjective = @SCMObjective, SCMMeasures = @SCMMeasures, SCMDefinition = @SCMDefinition, SCMCalculations = @SCMCalculations, SCMSourceSystem = @SCMSourceSystem, SCMLTTarget = @SCMLTTarget, SCMLTGoalDate = @SCMLTGoalDate, SCMTarget = @SCMTarget, SCMDataType = @SCMDataType, SCMCumulative = @SCMCumulative, SCMMax = @SCMMax, SCMMin = @SCMMin, SCMwMax = @SCMwMax, SCMwMin = @SCMwMin, SCMUpdateMethod = @SCMUpdateMethod, SCMUpdateInput = @SCMUpdateInput, SCMUpdateFrequency = @SCMUpdateFrequency, SCMOwner = @SCMOwner, SCMCategory = @SCMCategory, SCMEffectiveFromDate = @SCMEffectiveFromDate, SCMEffectiveToDate = @SCMEffectiveToDate, SCMActive = @SCMActive, SCMUpdated = @SCMUpdated WHERE (ID = @ID)" 
                    >

                    <InsertParameters>
                        <asp:Parameter Name="InsertName" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlMetricSCMID" Name="SCMID" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="rbMetricSCMID1" Name="SCMIDButton" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="ddlMetricSCMOwner" Name="SCMOwner" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="rbMetricSCOwner1" Name="SCMOwnerButton" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="ddlMetricSCCat" Name="SCMCategory" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="rbMetricSCCat1" Name="SCMCategoryButton" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="rbMetricActive1" Name="SCMActiveButton" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="rbMetActive" Name="SCMActive" 
                            PropertyName="Checked" />   
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SCMID" />
                        <asp:Parameter Name="SCMName" />
                        <asp:Parameter Name="SCMObjective" />
                        <asp:Parameter Name="SCMMeasures" />
                        <asp:Parameter Name="SCMDefinition" />
                        <asp:Parameter Name="SCMCalculations" />
                        <asp:Parameter Name="SCMSourceSystem" />
                        <asp:Parameter Name="SCMLTTarget" />
                        <asp:Parameter Name="SCMLTGoalDate" />
                        <asp:Parameter Name="SCMTarget" />
                        <asp:Parameter Name="SCMDataType" />
                        <asp:Parameter Name="SCMCumulative" />
                        <asp:Parameter Name="SCMMax" />
                        <asp:Parameter Name="SCMMin" />
                        <asp:Parameter Name="SCMwMax" />
                        <asp:Parameter Name="SCMwMin" />
                        <asp:Parameter Name="SCMUpdateMethod" />
                        <asp:Parameter Name="SCMUpdateInput" />
                        <asp:Parameter Name="SCMUpdateFrequency" />
                        <asp:Parameter Name="SCMOwner" />
                        <asp:Parameter Name="SCMCategory" />
                        <asp:Parameter Name="SCMEffectiveFromDate" />
                        <asp:Parameter Name="SCMEffectiveToDate" />
                        <asp:Parameter Name="SCMActive" />
                        <asp:Parameter Name="SCMUpdated" />
                        <asp:Parameter Name="ID" />
                    </UpdateParameters>
                    </asp:SqlDataSource>
<br />
                
                </asp:Panel>

</ContentTemplate>

</asp:UpdatePanel>
                  <asp:UpdatePanel runat = "server" ID= "protectDDL">
        <ContentTemplate>
       <asp:panel ID="Panel3" runat = "server" HorizontalAlign ="left">
       <br />
       <br />
       <asp:DropDownList runat = "server" ID = "UpdateorNew" AutoPostBack="True">
       <asp:ListItem Text="Submit New Metric" Value="0"></asp:ListItem>
       <asp:ListItem Text="Update Existing Metric" Value="1"></asp:ListItem>
       </asp:DropDownList><br />
          </asp:panel>
        
</ContentTemplate>
</asp:UpdatePanel>

    <br />
       <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ViewStateMode="Disabled">
           <ContentTemplate>


       <asp:Table ID="Table5" runat="server">
       <asp:TableRow><asp:TableCell>
    1. Metric Name:  
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
           <asp:TableCell>
        1. </asp:TableCell><asp:TableCell ColumnSpan = "2" ><asp:TextBox ID="txtMetricName" runat="server" Width="400"> </asp:TextBox>
         
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    2. What is the Metric Objective?
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        2. </asp:TableCell><asp:TableCell ColumnSpan = "2"><asp:TextBox ID="txtMetricObjective" runat="server" Width="400"> </asp:TextBox>&nbsp;&nbsp;

    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    3. How is this Metric Defined?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        3. </asp:TableCell><asp:TableCell ColumnSpan = "2"> 
               <asp:TextBox ID="txtMetricDefinition" runat="server" Width="400"> </asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    4. How is this Metric Calculated?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        4. </asp:TableCell><asp:TableCell ColumnSpan = "2"><asp:TextBox ID="txtMetricCalculated" runat="server" Width="400" ></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    5. Source Sytem:
    </asp:TableCell> 
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        5. </asp:TableCell><asp:TableCell ColumnSpan = "2"><asp:TextBox ID="txtMetricSourceSystem" runat="server" Width="400"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue">

    Targets</asp:TableCell></asp:TableRow><asp:TableRow>
               <asp:TableCell>
    6. Current Target Value:
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        6. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricTarget" runat="server"> </asp:TextBox> 
    </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID="ddlMetricTargetType" runat="server" DataSourceID = "dsMetricGUITarType" dataTextField = "SCMDataType" DataValueField = "SCMDataType">
        </asp:DropDownList>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow>
               <asp:TableCell ToolTip='If measured value is in the critical threshold, the metric is considered "Unhealthy" or Red on the Scorecard'> 
    7. Critical Threshold at or below Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        7. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricMin" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow>
               <asp:TableCell ToolTip='If measured value is in the critical threshold, the metric is considered "Unhealthy" or Red on the Scorecard'>
    8. Critical Threshold at or above Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        8. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricMax" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow>
               <asp:TableCell ToolTip='If measured value is between this value and the critical threshold, the metric is in the "Warning" status, Yellow, on the Scorecard'>
    9. Issue Warnings if Metric is at or Below Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell ToolTip='If measured value is between this value and the critical threshold, the metric is in the "Warning" status, Yellow, on the Scorecard'>
        9. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricwMin" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    10. Issue Warnings if Metric is at or Above Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        10. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricwMax" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    11. Long Term Goal Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        11. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricLTGoal" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    12. Date to Reach Long Term Goal:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        12. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricLTGoalDate" runat="server"></asp:TextBox><cc1:CalendarExtender
                ID="CalendarExtender1" runat="server" TargetControlID = "txtMetricLTGoalDate" Format = "MM/dd/yyyy">
            </cc1:CalendarExtender>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    13. What is the Improvement Trend?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        13. </asp:TableCell><asp:TableCell>
            <asp:RadioButton ID="rbincreasing" runat="server" groupName = "rbimpchoices" Text= "Increasing"/><asp:RadioButton ID="rbdecreasing"
                runat="server" GroupName= "rbimpchoices" Text = "Decreasing"/>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    14. Is this Measurement a Cumulative Measure?
    </asp:TableCell>
            <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        14. </asp:TableCell><asp:TableCell>
            <asp:RadioButton ID="rbnewmetcum1" runat="server" groupName = "rbnewmetcum" Text= "Yes"/>
               <asp:RadioButton ID="rbnewmetcum2"
                runat="server" GroupName= "rbnewmetcum" Text = "No"/>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    15. How often will this Metric be Measured?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        15. </asp:TableCell><asp:TableCell><asp:dropdownlist ID="ddlMetricFreq" runat="server"  AutoPostBack = "true">
                           <asp:ListItem Value="12">Monthly</asp:ListItem>
                           <asp:ListItem Value="52">Weekly</asp:ListItem>
                           <asp:ListItem Value="1">Annually</asp:ListItem>
                           <asp:ListItem Value="0">Enter new Frequency</asp:ListItem>
        </asp:dropdownlist><asp:TextBox runat = "server" ID = "secret" visible = "false"> 1 </asp:TextBox>
    </asp:TableCell><asp:TableCell><asp:TextBox runat = "server" ID = "ReqFreq" Visible = "false"></asp:TextBox></asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    16. Who will be in charge of reporting the values of this Metric?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        16. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwner" runat="server"></asp:TextBox>
    </asp:TableCell><asp:TableCell><asp:Label runat = "server" ID = "reqfreqdesc" Font-Size = "X-Small" Visible = "false"></asp:Label></asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    17. What is their email address?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        17. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwneremail" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    18. What is their network login?
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        18. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwnerlogin" runat="server"></asp:TextBox> 
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    19. Is this metric going to be Public or Private? <br />
    (i.e. available to other executives in the organization)
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        19. </asp:TableCell><asp:TableCell>
            <asp:RadioButton ID="rbnewmetPub1" runat="server" GroupName = "rbnewmetPub" Text = "Public" /><asp:RadioButton ID="rbnewmetPub2"
                runat="server" GroupName = "rbnewmetPub" Text = "Private" />
    </asp:TableCell></asp:TableRow>
    <asp:TableRow>
               <asp:TableCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue">

    Scorecard Data
    </asp:TableCell></asp:TableRow><asp:TableRow>
               <asp:TableCell><asp:Label ID="GrayCell" runat="server">
    20. What Scorecard(s) should this metric be associated with?</asp:Label>
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        20. </asp:TableCell><asp:TableCell><asp:CheckBoxList runat="server" ID = "cblnewmetmetriclist">
        </asp:CheckBoxList><asp:Label runat = "server" Visible = "false" ID = "InvGrayBox" ForeColor = "Gray" > N/A </asp:Label></asp:TableCell>
               
    </asp:TableRow><asp:TableRow><asp:TableCell>
    21. What Category is this metric a member of?</asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        21. </asp:TableCell><asp:TableCell ColumnSpan = "3"> 
            <asp:DropDownList ID="ddlnewMetCat" runat="server" dataSourceID = "dsMetricGUICatLU" DataTextField="SCCategory" DataValueField="SCCID" AutoPostBack="True">
            </asp:DropDownList>
    </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell><asp:TextBox ID="CatRequest" runat="server" Visible="False"></asp:TextBox></asp:TableCell></asp:TableRow>
    </asp:Table>
    <br />
    <br />
       <asp:Button ID="Button1" runat="server" Text="Submit" />
       <asp:Label ID = "Whatisworng" runat ="server"></asp:Label>
<asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel4" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="OkButton" runat="server" Text="OK"/>

      
      <br />
      <br />
      
  
   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender1" 
             runat="server"  
             
             TargetControlID="FakeButton"
             PopupControlID="Panel4"
             DropShadow="true"/>


    <br /><br /><br />
               <asp:Label runat = "server" ID = "Creatmetriclabel"></asp:Label>
    <small> Fields marked with * may not be left blank </small>
    <asp:Panel ID="Panel5" runat = "server" width = "100%" HorizontalAlign = "Right"><asp:Label runat = "server" ID = "contactlabel" Font-Size = "Smaller" Width = "200"></asp:Label></asp:Panel>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="UpdateorNew" 
                EventName="SelectedIndexChanged" />
<asp:AsyncPostBackTrigger ControlID="GridView1" 
                EventName="SelectedIndexChanged" />
</Triggers>
</asp:UpdatePanel>


        
        <br />
               
               <asp:Label ID="FakeButtonLabel" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
   <asp:label ID = "MetricInsertRequirements" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="metOkButton" runat="server" Text="OK" />

      
      <br />
      <br />
      
   
   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="MetricModalPopupExtender" 
             runat="server" 
             TargetControlID="FakeButtonLabel"
             PopupControlID="Panel1"
             DropShadow="true"/>

                    
                    </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat = "server" HeaderText = "Create New SC" ID = "panelcreatescorecard">

<ContentTemplate> 
                     
 
                     
 

                     
 
                     <br />
                    <asp:Label runat = "server" Font-Bold = "true" Font-Size = "X-Large" ForeColor = "SteelBlue">
                    Create New Scorecard </asp:Label> <br /> <br />
                    <asp:Table ID="Table6" runat = "server"> <asp:TableRow> <asp:TableHeaderCell font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce"> 
                    Scorecard Owner:</asp:TableHeaderCell><asp:TableCell>
                    <asp:textbox runat = "server" id = "txtnewSCOwner" ></asp:textbox> </asp:TableCell><asp:TableCell> </asp:TableCell></asp:TableRow>
                    <asp:TableRow><asp:TableHeaderCell font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce" > 
                    Scorecard Title:</asp:TableHeaderCell> <asp:TableCell> 
                        
                    <asp:textbox runat = "server" id = "txtnewSCName"> </asp:textbox></asp:TableCell><asp:TableCell>
                        </asp:TableCell>
                    </asp:TableRow></asp:Table>
                    <br /><br /> 
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                     <ContentTemplate>
                     <br />
                        <br />
                         <asp:Table runat="server" ID = "organization">
                         <asp:TableRow>
                         <asp:TableHeaderCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue" HorizontalAlign="Left"> Select Metrics: </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                         </asp:TableRow>
                         <asp:TableRow>
                         <asp:TableCell VerticalAlign = "Top">
                        <asp:Panel ID="Panel2" runat = "server" ScrollBars = "Vertical" > <asp:Label ID="Label4" runat = "server" Font-Bold = "true" ForeColor = "SteelBlue" BackColor = "#eee4ce"> Their Metrics: </asp:Label> <br />
                        <asp:CheckBoxList ID="cblCNSMetricSelect" runat="server" >
                        </asp:CheckBoxList></asp:Panel>
                        </asp:TableCell>
                         <asp:TableCell VerticalAlign = "Top">
                         <asp:Panel ID="Panel6" runat = "server">
                         <asp:Label ID="Label6" runat = "server" Font-Bold = "true" ForeColor = "SteelBlue" BackColor = "#eee4ce"> Select Other Users who may view this scorecard: <br /></asp:Label> 
                         <asp:CheckBoxList ID="cblCNSUsersSelect" runat="server">
                        </asp:CheckBoxList>  
                          </asp:Panel>            
                         </asp:TableCell>
                         </asp:TableRow>
                         </asp:Table>
                         
                         <asp:TableCell RowSpan = "2" VerticalAlign = "Bottom">
                        <asp:Panel ID="ScrollPanelCNS" runat = "server" ScrollBars = "Vertical" > 
                        <asp:Label runat = "server" Font-Bold = "true" ForeColor = "SteelBlue" BackColor = "#eee4ce"> Other Metrics: </asp:Label> <br />
                        <asp:CheckBoxList ID="cblpublicMetrics" runat="server">
                        </asp:CheckBoxList></asp:Panel>                          
                          </asp:TableCell>
                        <br />
                        <asp:Panel ID="Panel7" runat = "server" ScrollBars = "Vertical">
                        <asp:Button ID="btnCNSPreview" runat="server" Text="Preview Metrics" /> 
                        <br />
                        <br />
                        <asp:gridview runat="server" ID = "gridCNSPreviewMetric" AlternatingRowStyle-BackColor = "#eee4ce" HeaderStyle-BackColor = "SteelBlue" HeaderStyle-Font-Bold ="true" HeaderStyle-ForeColor = "White">
                        </asp:gridview>   </asp:Panel>
                        <br />
                        <asp:Button ID="btnCNSSubmit" runat="server" Text="Submit" />
 



<asp:Label ID="FakeLabelMPE2" runat = "server" />
   <asp:Panel ID="MPE2Panel" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
   <asp:label ID = "ExplanationNewSClabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="OkButton2" runat="server" Text="OK" />

      
      <br />
      <br />
      
   
   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender2" 
             runat="server"              
             TargetControlID="FakeLabelMPE2"
             PopupControlID="MPE2Panel"
             DropShadow="true"/>

               </ContentTemplate>
                </asp:UpdatePanel>
                    
</ContentTemplate>


<%--                    <ContentTemplate>
                                        
         <asp:Label ID= "label13"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Create New Scorecard </asp:Label>
                     <br /><br />
                     <asp:Table runat = "server" ID = "SCOwnTit" > <asp:TableRow><asp:TableHeaderCell Font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce" >
                    &nbsp;&nbsp;&nbsp; Scorecard Owner:&nbsp;&nbsp;&nbsp;</asp:TableHeaderCell> <asp:TableCell>
                    <asp:textbox runat = "server" id = "txtnewSCOwner" ></asp:textbox></asp:TableCell></asp:TableRow>
                    <asp:TableRow></asp:TableRow><asp:TableRow><asp:TableHeaderCell Font-Bold = "true" ForeColor = "#003606" BackColor = "#eee4ce" >
                    &nbsp;&nbsp;&nbsp;Scorecard Title:&nbsp;&nbsp;&nbsp;</asp:TableHeaderCell> <asp:TableCell>
                    <asp:textbox runat = "server" id = "txtnewSCName"></asp:textbox></asp:TableCell></asp:TableRow> </asp:Table>
                    <br /><br /> 
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                     <ContentTemplate>
                     <br />
                        <br />
                         <asp:Table runat="server" ID = "organization">
                         <asp:TableRow>
                         <asp:TableHeaderCell  Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue" HorizontalAlign="Left">Select Metrics: </asp:TableHeaderCell>
                         </asp:TableRow>
                         <asp:TableRow>
                         <asp:TableCell>
                        
                        <asp:CheckBoxList ID="cblCNSMetricSelect" runat="server" DataSourceID="dsOhmsMetricSCMID" DataTextField="SCMName" DataValueField="SCMID">
                        </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                         <asp:Table runat="server" ID = "extraorganized">
                         <asp:TableRow>
                         <asp:TableCell>
                         <asp:gridview runat="server" ID = "gridCNSPreviewMetric" AlternatingRowStyle-BackColor = "#eee4ce" HeaderStyle-BackColor = "SteelBlue" HeaderStyle-Font-Bold ="true" HeaderStyle-ForeColor = "White">
                        </asp:gridview>    
                        <br /> <br /><br />
                        <br />                    
                         </asp:TableCell>
                         </asp:TableRow>
                         <asp:TableRow>
                          <asp:TableHeaderCell  Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue" HorizontalAlign="Left" > Select Users who may view this scorecard: <br /></asp:TableHeaderCell>
                         </asp:TableRow>
                         <asp:TableRow>
                         <asp:TableCell>
                        <asp:CheckBoxList ID="cblCNSUsersSelect" runat="server" DataSourceID="dsOhmsULUName" DataTextField="Name" DataValueField="SCUName">
                        </asp:CheckBoxList>  
                        </asp:TableCell>
                         </asp:TableRow>                     
                         </asp:Table>   
                        </asp:TableCell>
                       </asp:TableRow>
                       </asp:Table>
                        <asp:Button ID="btnCNSPreview" runat="server" Text="Preview Metrics" />
                        <br /><br />
                         <asp:Button ID="btnCNSSubmit" runat="server" Text="Submit" />

                         <asp:Label ID="Label1" runat = "server" />
   <asp:Panel ID="Panel2" runat="server" Width="233px" BackColor = "#eee4ce" BorderColor="#666666" Font-Bold="True">
    

   <br />
   <center><asp:label ID = "ExplanationNewSClabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="OkButton2" runat="server" Text="OK" />

      
      <br />
      <br />
      
   </center>
   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender2" 
             runat="server" 
             TargetControlID="Label1"
             PopupControlID="Panel2"
             DropShadow="true"/>


               </ContentTemplate>
                </asp:UpdatePanel>
                    </ContentTemplate>--%>
                    </cc1:TabPanel>

<cc1:TabPanel runat = "server" HeaderText = "Scorecard" ID = "OhmsAdminSC" ScrollBars = "Auto">
            <ContentTemplate>
                                        
         <asp:Label ID= "label7"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Scorecard </asp:Label> <br /><br />

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                Select * FROM Scorecard <br /> WHERE<br />
                <asp:Table ID="Table4" runat="server" CellPadding="5" GridLines="horizontal" 
                    HorizontalAlign="left">
                    <asp:TableRow>
                        <asp:TableCell>SCTID = </asp:TableCell>
                        <asp:TableCell>        <asp:DropDownList ID="ddlSCTID" 
                           runat="server" DataSourceID="dsOhmsTitle" 
            DataTextField="SCTitle" DataValueField="SCTID">
                        </asp:DropDownList> </asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCSCTID1" 
                           runat="server" GroupName="rbSCSCTID" 
            Text="True" Checked="True" /> </asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCSCTID2" 
                           runat="server" GroupName="rbSCSCTID" 
            Text="False"  /> </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>SCMID = </asp:TableCell>
                        <asp:TableCell>
        <asp:DropDownList ID="ddlSCMID" runat="server" DataSourceID="dsOhmsMetricSCMID" 
            DataTextField="SCMName" DataValueField="SCMID">
                        </asp:DropDownList></asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCSCMID1" 
                           runat="server" GroupName="rbSCSCMID" 
            Text="True" /></asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCSCMID2" 
                           runat="server" GroupName="rbSCSCMID" 
            Text="False" Checked="True" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>SCOwner = </asp:TableCell>
                        <asp:TableCell>        <asp:DropDownList ID="ddlSCOwner" 
                           runat="server" DataSourceID="dsOhmsSCOWner" 
            DataTextField="Name" DataValueField="SCOwner">
                        </asp:DropDownList></asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCOwner1" 
                           runat="server" GroupName="rbSCOwner" 
            Text="True" /></asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCOwner2" 
                           runat="server" GroupName="rbSCOwner" 
            Text="False" Checked="True" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>SCActive = </asp:TableCell>
                        <asp:TableCell>
                        <asp:RadioButton runat = "server" GroupName = "rbSCactiveyn" ID = "rbSCactiveonezero" Text = "1" Checked = "true" />
                        <asp:RadioButton runat = "server" GroupName = "rbSCactiveyn" ID = "rbsCactiveonzeronot" Text = "0"/>
                        </asp:TableCell>
                        <asp:TableCell>        <asp:RadioButton ID="rbSCActive1" 
                           runat="server" GroupName="rbSCActive" 
            Text="True" /></asp:TableCell>
                        <asp:TableCell><asp:RadioButton 
            ID="rbSCActive2" runat="server" GroupName="rbSCActive" Text="False" Checked="True" />
        <br /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Button ID="Button5" runat="server" Text="Execute" />
                <br />
                <br />
                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCID" 
                    DataSourceID="dsOhmsSC" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <AlternatingRowStyle BackColor="#EEE4CE" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="SCID" HeaderText="SCID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="SCID" />
                        <asp:BoundField DataField="SCTID" HeaderText="SCTID" SortExpression="SCTID" />
                        <asp:BoundField DataField = "SCTitle" HeaderText = "Scorecard Title" SortExpression = "SCTitle" ReadOnly = "true" />
                        <asp:BoundField DataField="SCMID" HeaderText="SCMID" SortExpression="SCMID" />
                        <asp:BoundField DataField = "SCMName" HeaderText = "Metric Name" SortExpression = "SCMName" ReadOnly = "true" />
                        <asp:BoundField DataField="SCOwner" HeaderText="SCOwner" 
                            SortExpression="SCOwner" />
                        <asp:CheckBoxField DataField="SCActive" HeaderText="SCActive" 
                            SortExpression="SCActive" />
                        <asp:BoundField DataField="SCModifyDate" HeaderText="SCModifyDate" 
                            SortExpression="SCModifyDate" />
                        <asp:BoundField DataField="SCUpdated" HeaderText="SCUpdated" 
                            SortExpression="SCUpdated" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <br />
            </ContentTemplate>
                </asp:UpdatePanel>
        <br />
        Insert into Scorecard<br />
        <br />
        <asp:Table runat = "server">
        <asp:TableRow>
        <asp:TableHeaderCell HorizontalAlign = "Left">
        SCTID:</asp:TableHeaderCell><asp:TableCell>
        <asp:TextBox ID="txtSCSCTID" runat="server"></asp:TextBox>
        </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableHeaderCell HorizontalAlign = "Left">
        SCMID:</asp:TableHeaderCell><asp:TableCell>
        <asp:TextBox ID="txtSCSCMID" runat="server"></asp:TextBox>
        </asp:TableCell></asp:TableRow>
        </asp:Table><br />
        <br />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ScorecardInsert" runat="server" Text="Insert" />
      
             <asp:Label ID="SCFakeButton" runat = "server" />
   <asp:Panel ID="SCPopupPanel" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
   <asp:label ID = "SCExplanationLabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="SCOKbutton" runat="server" Text="OK" />

      
      <br />
      <br />

   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="SCModalPopupExtender" 
             runat="server" 
             TargetControlID="SCFakeButton"
             PopupControlID="SCPopupPanel"
             DropShadow="true"/>



        <br />

        <br />
       
        <asp:SqlDataSource ID="dsOhmsSC" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="Select sc.SCID, sc.SCTID, tt.SCTitle, sc.SCMID, m.SCMName, tt.SCOwner, sc.SCActive, sc.SCModifyDate, sc.SCUpdated
 FROM DWH.KPIS.Scorecard sc
join DWH.KPIS.ScorecardTitle_LU tt on sc.SCTID = tt.SCTID
left join DWH.KPIS.ScorecardMetric m on sc.SCMID = m.SCMID and m.SCMActive = 1
where
(sc.SCTID = @SCTID or @SCTIDButton = 0)
and (sc.SCMID = @SCMID or @SCMIDButton = 0)
and (SCActive = @SCActive or @SCActiveButton = 0)
and (tt.SCOwner = @SCOwner or @SCOButton = 0)

order by SCID, SCMID, m.ID" 
            
            
            UpdateCommand="UPDATE KPIS.Scorecard SET SCTID = @SCTID, SCMID = @SCMID, SCActive = @SCActive, SCModifyDate = @SCModifyDate, SCUpdated = @SCUpdated, SCMCategory = @SCMCategory WHERE (SCID = @SCID)" 
            >

            <SelectParameters>
                <asp:ControlParameter ControlID="ddlSCTID" Name="SCTID" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="rbSCSCTID1" Name="SCTIDButton" 
                    PropertyName="Checked" />
                <asp:ControlParameter ControlID="ddlSCMID" Name="SCMID" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="rbSCSCMID1" Name="SCMIDButton" 
                    PropertyName="Checked" />
                <asp:ControlParameter ControlID="rbSCActive1" Name="SCActiveButton" 
                    PropertyName="Checked" />
                <asp:ControlParameter ControlID="ddlSCOwner" Name="SCOwner" 
                    PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="rbSCOwner1" Name="SCOButton" 
                    PropertyName="Checked" />
                <asp:ControlParameter ControlID= "rbSCactiveonezero" Name = "SCActive"
                    PropertyName = "Checked" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="SCTID" />
                <asp:Parameter Name="SCMID" />
                <asp:Parameter Name="SCActive" />
                <asp:Parameter Name="SCModifyDate" />
                <asp:Parameter Name="SCUpdated" />
                <asp:Parameter Name="SCMCategory" />
                <asp:Parameter Name="SCID" />
            </UpdateParameters>
                </asp:SqlDataSource>           
                    </ContentTemplate>
            </cc1:TabPanel>
<cc1:TabPanel runat = "server" HeaderText = "Scorecard Cat LU" ID = "OhmsAdminCatLU" ScrollBars = "Auto">
    <ContentTemplate>
                                        
         <asp:Label ID= "label11"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Scorecard Category Lookup </asp:Label><br /><br />
    
        <asp:GridView ID="GridView7" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCCID" 
            DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#CCCCCC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <AlternatingRowStyle BackColor="#EEE4CE" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="SCCID" HeaderText="SCCID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="SCCID" />
                <asp:BoundField DataField="SCCategory" HeaderText="SCCategory" 
                    SortExpression="SCCategory" />
                <asp:CheckBoxField DataField="SCCActive" HeaderText="SCCActive" 
                    SortExpression="SCCActive" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
             
            SelectCommand="SELECT * FROM DWH.KPIS.ScorecardCategory_LU" 
            UpdateCommand="UPDATE KPIS.ScorecardCategory_LU SET SCCategory = @SCCategory, SCCActive = @SCCActive where SCCID = @SCCID">

            <UpdateParameters>
                <asp:Parameter Name="SCCategory" />
                <asp:Parameter Name="SCCActive" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        Insert into ScorecardCategory_LU<br />
        <br /><asp:Label runat = "server" Font-Bold = "true">
        SCCategory: </asp:Label>
        <asp:TextBox ID="txtSCCSCCat" runat="server"></asp:TextBox>
        <br />
        <br />

        <asp:Button ID="scCategoryLUInsert" runat="server" Text="Insert" />
        <br />   

                       <asp:Label ID="CatFakeButton" runat = "server" />
   <asp:Panel ID="CatPopupPanel" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
 <asp:label ID = "CatExplanationLabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="CatOkButton" runat="server" Text="OK" />

      
      <br />
      <br />

   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="CatModalPopupExtender" 
             runat="server" 
             TargetControlID="CatFakeButton"
             PopupControlID="CatPopupPanel"
             DropShadow="true"/>




                    </ContentTemplate>
    </cc1:TabPanel>

 

<cc1:TabPanel runat = "server" HeaderText = "Scorecard Title LU" ID = "OhmsAdminTLU" ScrollBars = "Auto">
    <ContentTemplate>
                                        
         <asp:Label ID= "label9"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Scorecard Title Lookup </asp:Label> <br /><br />
        <asp:GridView ID="GridView5" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCTID" 
            DataSourceID="dsOhmsTL" BackColor="White" BorderColor="#CCCCCC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <AlternatingRowStyle BackColor="#EEE4CE" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="SCTID" HeaderText="SCTID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="SCTID" />
                <asp:BoundField DataField="SCTitle" HeaderText="SCTitle" 
                    SortExpression="SCTitle" />
                <asp:BoundField DataField = "SCOwner" HeaderText = "Scorecard Owner" SortExpression = "SCOwner" />
                <asp:CheckBoxField DataField="SCTActive" HeaderText="SCTActive" 
                    SortExpression="SCTActive" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:SqlDataSource ID="dsOhmsTL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="SELECT KPIS.ScorecardTitle_LU.* FROM KPIS.ScorecardTitle_LU" 
            
            UpdateCommand="UPDATE KPIS.ScorecardTitle_LU SET SCTitle = @SCTitle, SCOwner = @SCOwner, SCTActive = @SCTActive WHERE (SCTID = @SCTID)" 
            >

            <UpdateParameters>
                <asp:Parameter Name="SCTitle" />
                <asp:Parameter Name="SCTActive" />
                <asp:Parameter Name="SCTID" />
                <asp:Parameter Name="SCOwner" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />


    
        Insert Into ScorecardTitle<br />
&nbsp;&nbsp;
        <asp:Table runat = "server"> <asp:TableRow><asp:TableCell> 
        <br /><asp:Label runat = "server" Font-Bold = "true">
        SCTitle:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </asp:Label></asp:TableCell> <asp:TableCell> 
        <asp:TextBox ID="txtSCTSCTitle" runat="server"></asp:TextBox>
        </asp:TableCell></asp:TableRow><asp:TableRow> <asp:TableCell> 
        <asp:Label ID="Label2" runat = "server" Font-Bold = "true">
        SCOwner:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </asp:Label></asp:TableCell><asp:TableCell> 
        <asp:TextBox ID="txtSCTSCOwner" runat="server"></asp:TextBox>
        </asp:TableCell>
        </asp:TableRow></asp:Table><br /><br />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SCTitleInsertButton" runat="server" Text="Insert" />
        <br />

               <asp:Label ID="TitleFakeButton" runat = "server" />
   <asp:Panel ID="TitlePopupPanel" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
  <asp:label ID = "TitleExplanationLabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="TitleOkButton" runat="server" Text="OK" />

      
      <br />
      <br />
      
  </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="TitleModalPopupExtender" 
             runat="server" 
             TargetControlID="TitleFakeButton"
             PopupControlID="TitlePopupPanel"
             DropShadow="true"/>




        <br />
        <br />    
                    </ContentTemplate>
    </cc1:TabPanel>
 
<cc1:TabPanel runat = "server" HeaderText = "Scorecard User LU" ID = "OhmsAdminUL" ScrollBars = "Auto">
                    <ContentTemplate>
                                                            
         <asp:Label ID= "label5"  runat = "server" Font-Bold = "true" Font-Size = "Larger" ForeColor = "SteelBlue">
                    Scorecard User Lookup </asp:Label> <br /><br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                Select * FROM ScorecardUser_LU
                <br />
                WHERE<br />
                               <asp:Table ID="Table3" runat="server" CellPadding="5"
                GridLines="horizontal" HorizontalAlign="left">
                   <asp:TableRow>
                     <asp:TableCell>SCUName = </asp:TableCell>
                     <asp:TableCell>                <asp:DropDownList ID="ddlULUName" runat="server" DataSourceID="dsOhmsULUName" 
                    DataTextField="Name" DataValueField="SCUName"></asp:DropDownList></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbULUName1" runat="server" GroupName="rbULUN" 
                    Text="True" /></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbULUN2" runat="server" GroupName="rbULUN" Text="False" Checked="True" /></asp:TableCell>
                                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCTID = </asp:TableCell>
                     <asp:TableCell>                <asp:DropDownList ID="ddlULTitle" runat="server" DataSourceID="dsOhmsTitle" 
                    DataTextField="SCTitle" DataValueField="SCTID">
                       </asp:DropDownList></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbULTID1" runat="server" GroupName="rbULTID" Text="True" /></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbULTID2" runat="server" GroupName="rbULTID" 
                    Text="False" Checked="True" /></asp:TableCell>
                                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCUActive</asp:TableCell>
                     <asp:TableCell>= 1</asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbULActive1" runat="server" GroupName="rbULActive" 
                    Text="True" /></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="rbULActive2" runat="server" GroupName="rbULActive" 
                    Text="False" Checked="True" /></asp:TableCell>
                                   </asp:TableRow>
                   
                </asp:Table>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />

                <asp:Button ID="Button3" runat="server" Text="Execute" />


<br />
                <br />

                <br />
                <br />

                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCUID" 
                    DataSourceID="dsOhmsUL" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <AlternatingRowStyle BackColor="#EEE4CE" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="SCUID" HeaderText="SCUID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="SCUID" />
                        <asp:BoundField DataField="SCTID" HeaderText="SCTID" SortExpression="SCTID" />
                        <asp:BoundField DataField="SCUName" HeaderText="SCUName" 
                            SortExpression="SCUName" />
                        <asp:BoundField DataField="SCUUpdateFrequency" HeaderText="SCUUpdateFrequency" 
                            SortExpression="SCUUpdateFrequency" />
                        <asp:CheckBoxField DataField="SCUActive" HeaderText="SCUActive" 
                            SortExpression="SCUActive" />
                        <asp:BoundField DataField="SCCModifyDate" HeaderText="SCCModifyDate" 
                            SortExpression="SCCModifyDate" />
                        <asp:BoundField DataField="SCCUpdated" HeaderText="SCCUpdated" 
                            SortExpression="SCCUpdated" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <asp:SqlDataSource ID="dsOhmsUL" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="SELECT * FROM DWH.KPIS.ScorecardUser_LU
where 
(SCUName = @SCUName or @SCUButton = 0)
and (SCTID = @SCTID or @SCTIDButton = 0)
and (SCUActive = 1 or @SCActiveButton = 0)" UpdateCommand="UPDATE KPIS.ScorecardUser_LU SET SCTID = @SCTID, SCUName = @SCUName, SCUUpdateFrequency = @SCUUpdateFrequency, SCUActive = @SCUActive, SCCModifyDate = @SCCModifyDate, SCCUpdated = @SCCUpdated
where SCUID = @SCUID" 
                    >

                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlULUName" Name="SCUName" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="rbULUName1" Name="SCUButton" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="ddlULTitle" Name="SCTID" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="rbULTID1" Name="SCTIDButton" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="rbULActive1" Name="SCActiveButton" 
                            PropertyName="Checked" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SCTID" />
                        <asp:Parameter Name="SCUName" />
                        <asp:Parameter Name="SCUUpdateFrequency" />
                        <asp:Parameter Name="SCUActive" />
                        <asp:Parameter Name="SCCModifyDate" />
                        <asp:Parameter Name="SCCUpdated" />
                        <asp:Parameter Name="SCUID" />
                    </UpdateParameters>
                </asp:SqlDataSource>
<br />
            </ContentTemplate>
                        </asp:UpdatePanel>
        <br />
        Insert into ScorecardUser_LU<br />


                <br />
                <asp:Table runat = "server">
                <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign = "Left">
                

                        SCTID:</asp:TableHeaderCell> <asp:TableCell>
                        <asp:TextBox ID="txtboxSCMID0" runat="server"></asp:TextBox>
                        </asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableHeaderCell HorizontalAlign = "Left">
    
                SCUName:</asp:TableHeaderCell><asp:TableCell>
                <asp:TextBox ID="txtboxSCMName0" runat="server"></asp:TextBox>
                </asp:TableCell>  
                </asp:TableRow>
                </asp:Table>     
                <br /><br />
                <asp:Button ID="OhmsULInsert" runat="server" Text="Insert" />

                       <asp:Label ID="ULFakeButton" runat = "server" />
   <asp:Panel ID="ULPopupPanel" runat="server" Width="233px" CssClass = "modalBackground2"  Font-Bold="True">
    

   <br />
   <asp:label ID = "ULExplanationlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="ULOKbutton" runat="server" Text="OK" />

      
      <br />
      <br />

   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="ULModalPopupExtender" 
             runat="server" 
             TargetControlID="ULFakeButton"
             PopupControlID="ULPopupPanel"
             DropShadow="true"/>


                    </ContentTemplate>
                    </cc1:TabPanel>
</cc1:tabcontainer>
                
               
                
            



        <br />
        <br />

        <asp:SqlDataSource ID="dsOhmsSCOWner" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="Select SCOwner, case when FirstName is NULL and LastName is NULL then SCOwner else FirstName + ' ' + LastName end as Name FROM DWH.KPIS.ScorecardTitle_LU ul left JOIN DWH.dbo.Email_Distribution ed on ul.SCOwner = ed.NetworkLogin group by SCOwner, FirstName, LastName
Order by case when FirstName is NULL then 1 else 0 end asc, LastName asc, FirstName asc"></asp:SqlDataSource>

 




    </asp:Content>