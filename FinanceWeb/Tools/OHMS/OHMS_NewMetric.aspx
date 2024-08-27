<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OHMS_NewMetric.aspx.vb" Inherits="FinanceWeb.OHMS_NewMetric" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--       <asp:SqlDataSource ID="dsMetricGUICatLU" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="SELECT SCCategory, SCCID FROM KPIS.ScorecardCategory_LU WHERE (SCCActive = 1) order by SCCategory asc"></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsMetricGUITarType" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
select Distinct case when SCMDataType is NULL then 'raw' else SCMDataType end as SCMDataType from DWH.KPIS.ScorecardMetric order by case when SCMDataType is NULL then 'raw' else SCMDataType end asc"></asp:SqlDataSource>
                
                <asp:SqlDataSource ID="dsOhmsMetricSCMID" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="Select SCMName, SCMID from (
select RANK() over (partition by SCMID order by SCMEffectiveToDate desc) as rankin, SCMID, SCMName, SCMActive from DWH.KPIS.ScorecardMetric) a
where rankin = 1 and SCMActive = 1
Order by SCMName asc"></asp:SqlDataSource>

               <asp:SqlDataSource ID="dsOhmsULUName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select SCUName, case when FirstName is NULL and LastName is NULL then SCUName else FirstName + ' ' + LastName end as Name FROM DWH.KPIS.ScorecardUser_LU ul left JOIN DWH.dbo.Email_Distribution ed on ul.SCUName = ed.NetworkLogin group by SCUName, FirstName, LastName
Order by case when FirstName is NULL then 1 else 0 end asc, LastName asc, FirstName asc">
                </asp:SqlDataSource>

    <cc1:TabContainer ID="OhmsMetricGUI" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "true" > 
    <cc1:TabPanel runat = "server" HeaderText = "New Metric" ID = "OhmsMetricGUINM">
   <ContentTemplate>

    <br />
    <asp:Label ID= "Labelbox" runat = "server" font-size = "XX-Large" Font-Bold = "true" ForeColor = "#003060" BorderStyle="None">Organization Health Monitoring System</asp:Label><br />
    <asp:Label ID = "Labelbox2" runat ="server" font-size = "X-Large" Font-Bold = "true" ForeColor = "SteelBlue" BorderStyle="None"> Metric Request Form</asp:Label>
    <br /><br />
       <asp:Table ID="Table1" runat="server">
       <asp:TableRow><asp:TableCell>
    1. Metric Name:  
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell><asp:TableCell>
        1. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Required Field - Metric Name" ControlToValidate="txtMetricName" Font-Bold="True"></asp:RequiredFieldValidator>


    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    2. What is the Metric Objective?
    </asp:TableCell>
    <asp:TableCell></asp:TableCell><asp:TableCell>
        2. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricObjective" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    3. How is this Metric Defined?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        3. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricDefinition" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    4. How is this Metric Measured?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        4. </asp:TableCell><asp:TableCell><asp:TextBox runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    5. How is this Metric Calculated?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        5. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricCalculated" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    6. Source Sytem:
    </asp:TableCell> 
        <asp:TableCell></asp:TableCell><asp:TableCell>
        6. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricSourceSystem" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue">

    Targets</asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
    7. Current Target Value:
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell><asp:TableCell>
        7. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricTarget" runat="server"></asp:TextBox>
    </asp:TableCell><asp:TableCell>
        <asp:DropDownList ID="ddlMetricTargetType" runat="server" DataSourceID = "dsMetricGUITarType" dataTextField = "SCMDataType" DataValueField = "SCMDataType">
        </asp:DropDownList>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell> 
    8. Minimum Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        8. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricMin" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    9. Maximum Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        9. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricMax" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    10. Issue Warnings if Metric is Below Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        10. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricwMin" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    11. Issue Warnings if Metric is Above Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        11. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricwMax" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    12. Long Term Goal Value:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        12. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricLTGoal" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    13. Date to Reach Long Term Goal:
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        13. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricLTGoalDate" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    14. Is this Measurement a Cumulative Measure?
    </asp:TableCell>
            <asp:TableCell></asp:TableCell><asp:TableCell>
        14. </asp:TableCell><asp:TableCell>
            <asp:RadioButton ID="rbnewmetcum1" runat="server" groupName = "rbnewmetcum" Text= "Yes"/><asp:RadioButton ID="rbnewmetcum2"
                runat="server" GroupName= "rbnewmetcum" Text = "No"/>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    15. How often will this Metric be Measured?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        15. </asp:TableCell><asp:TableCell><asp:dropdownlist ID="ddlMetricFreq" runat="server">
                                   <asp:ListItem Value="'12'">Monthly</asp:ListItem>
                           <asp:ListItem Value="'52'">Weekly</asp:ListItem>
                           <asp:ListItem Value="'1'">Annually</asp:ListItem>
        </asp:dropdownlist>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    16. Who will be in charge of reporting the values of this Metric?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        16. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwner" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    17. What is their email address?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        17. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwneremail" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    18. What is their network login?
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell><asp:TableCell>
        18. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwnerlogin" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    19. Is this metric going to be Public or Private? <br />
    (i.e. available to other executives in the organization)
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        19. </asp:TableCell><asp:TableCell>
            <asp:RadioButton ID="rbnewmetPub1" runat="server" GroupName = "rbnewmetPub" Text = "Public" /><asp:RadioButton ID="rbnewmetPub2"
                runat="server" GroupName = "rbnewmetPub" Text = "Private" />
    </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue">
    Scorecard Data
    </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>
    20. What Scorecard(s) should this metric be associated with?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        20. 
    </asp:TableCell><asp:TableCell><asp:CheckBoxList runat="server" ID = "cblnewmetmetriclist">
        </asp:CheckBoxList></asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    21. What Category is this metric a member of?</asp:TableCell>
        <asp:TableCell></asp:TableCell><asp:TableCell>
        21. </asp:TableCell><asp:TableCell> 
            <asp:DropDownList ID="ddlnewMetCat" runat="server" dataSourceID = "dsMetricGUICatLU" DataTextField="SCCategory" DataValueField="SCCID">
            </asp:DropDownList>
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <br />
    <br />
       <asp:Button ID="btnNewMetricSubmit" runat="server" Text="Submit" />
       <asp:Label ID = "Whatisworng" runat ="server"></asp:Label>
<asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor = "#eee4ce" BorderColor="#666666" Font-Bold="True">
    

   <br />
   <center><asp:label ID = "explantionlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="OkButton" runat="server" Text="OK"/>

      
      <br />
      <br />
      
   </center>
   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender1" 
             runat="server" 
             TargetControlID="FakeButton"
             PopupControlID="Panel1"
             DropShadow="true"/>


    <br /><br /><br /><asp:Label runat = "server" ID = "Creatmetriclabel"></asp:Label>
    <small> Fields marked with * may not be left blank </small>

    </ContentTemplate> 
    </cc1:TabPanel>
    <cc1:TabPanel runat="server" ID ="OhmsMetricUM" HeaderText = "Update Metric">
      <ContentTemplate><br />
 <asp:Label ID="LabelwasTxtbox3" runat ="server" font-size = "XX-Large" Font-Bold = "true" ForeColor = "#003060">Organization Health Monitoring System </asp:Label> <br />
    
<asp:Label ID= "Labelwastxtbox2" runat ="server" font-size = "X-Large" Font-Bold = "true" ForeColor = "SteelBlue"> Metric Update Form </asp:Label>

    <br />
        
    <br />   
    <asp:UpdatePanel runat = "server" ID= "protectDDL">
    <ContentTemplate><asp:Label ID="Label1" runat = "server" Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue"> 
     Select Metric:</asp:Label>
        <asp:DropDownList ID="ddlMetricUpSelect" runat="server" AutoPostBack="True" >
        </asp:DropDownList>   
        </ContentTemplate> 
    </asp:UpdatePanel>

    <asp:UpdatePanel runat = "server" ID = "updatepanelupdatemetric" UpdateMode="Conditional" ViewStateMode="Disabled">
    <ContentTemplate>
    
                <asp:SqlDataSource ID= "dsSelectedMetricInfo" runat = "server"
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="Select * FROM DWH.KPIS.ScorecardMetric where ID = @ID" UpdateCommand = "Select * from DWH.KPIS.ScorecardMetric"
            >
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlMetricUpSelect" Name="ID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SCMName" />
                    </UpdateParameters>
        </asp:SqlDataSource>
        <br />
       
        <br />
        <br>
        <asp:Panel runat = "server" ID = "RequestedUpdatesPanel" Scrollbars = "Horizontal">
<br />  Updates:

        <asp:GridView runat = "server" ID = "GridRequestedUpdates" 
                DataSourceID = "dsSelectedMetricInfo" AutoGenerateColumns="False" 
                DataKeyNames="ID">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" Visible = "false"/>
            <asp:BoundField DataField="SCMID" HeaderText="SCMID" Visible = "False" SortExpression="SCMID" />
            <asp:BoundField DataField="SCMName" HeaderText="Name" 
                SortExpression="SCMName" />
            <asp:BoundField DataField="SCMObjective" HeaderText="Objective" 
                SortExpression="SCMObjective" />
            <asp:BoundField DataField="SCMMeasures" HeaderText="Measures" 
                SortExpression="SCMMeasures" />
            <asp:BoundField DataField="SCMDefinition" HeaderText="Definition" 
                SortExpression="SCMDefinition" />
            <asp:BoundField DataField="SCMCalculations" HeaderText="Calculations" 
                SortExpression="SCMCalculations" />
            <asp:BoundField DataField="SCMSourceSystem" HeaderText="SourceSystem" 
                SortExpression="SCMSourceSystem" />
            <asp:BoundField DataField="SCMLTTarget" HeaderText="Long Term Target" 
                SortExpression="SCMLTTarget" />
            <asp:BoundField DataField="SCMLTGoalDate" HeaderText="Long Term Goal Date" 
                SortExpression="SCMLTGoalDate" />
            <asp:BoundField DataField="SCMTarget" HeaderText="Target" 
                SortExpression="SCMTarget" />
<asp:TemplateField ConvertEmptyStringToNull="true" HeaderText="Data Type" 
                   SortExpression="SCMDataType">
                   <EditItemTemplate>
                       <asp:DropDownList ID="ddlDataType" runat="server">
                           <asp:ListItem Value="'none'">Select One</asp:ListItem>
                           <asp:ListItem Value="NULL"> </asp:ListItem>
                           <asp:ListItem Value="'money'">money</asp:ListItem>
                           <asp:ListItem Value="'percent'">percent</asp:ListItem>
                           <asp:ListItem Value="NULL">number</asp:ListItem>
                       </asp:DropDownList>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Label ID="DATA_TYPE2" runat="server" Text='<%# Eval("SCMDataType") %>'> &gt;</asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>


            <asp:CheckBoxField DataField="SCMCumulative" HeaderText="Cumulative" 
                SortExpression="SCMCumulative" />
            <asp:BoundField DataField="SCMMax" HeaderText="Max" 
                SortExpression="SCMMax" />
            <asp:BoundField DataField="SCMMin" HeaderText="Min" 
                SortExpression="SCMMin" />
            <asp:BoundField DataField="SCMwMax" HeaderText="Warning Max" 
                SortExpression="SCMwMax" />
            <asp:BoundField DataField="SCMwMin" HeaderText="Warning Min" 
                SortExpression="SCMwMin" />
            <asp:BoundField DataField="SCMUpdateMethod" HeaderText="Update Method" 
                SortExpression="SCMUpdateMethod" />
            <asp:BoundField DataField="SCMUpdateInput" HeaderText="Update Input" 
                SortExpression="SCMUpdateInput" />
            <asp:BoundField DataField="SCMUpdateFrequency" HeaderText="Update Frequency" 
                SortExpression="SCMUpdateFrequency" />
            <asp:BoundField DataField="SCMOwner" HeaderText="Metric Owner" 
                SortExpression="SCMOwner" />
            <asp:BoundField DataField="SCMCategory" HeaderText="Category" 
                SortExpression="SCMCategory" />
            <asp:BoundField DataField="SCMEffectiveFromDate" visible = "false"
                HeaderText="SCMEffectiveFromDate" SortExpression="SCMEffectiveFromDate" />
            <asp:BoundField DataField="SCMEffectiveToDate" HeaderText="Effective To Date" 
                SortExpression="SCMEffectiveToDate" />
            <asp:CheckBoxField DataField="SCMActive" Visible = "false" HeaderText="SCMActive" 
                SortExpression="SCMActive" />
            <asp:BoundField DataField="SCMUpdated" visible = "false" HeaderText="SCMUpdated" 
                SortExpression="SCMUpdated" />
        
        </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

        </asp:GridView>
        
 
        </asp:Panel>
        
        <br />        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlMetricUpSelect" 
                EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel></ContentTemplate>

    </cc1:TabPanel>
    <cc1:TabPanel runat = "server" HeaderText = "Create New Scorecard" ID = "panelcreatescorecard">
 
                    <ContentTemplate> 
                     <br /><asp:Label ID="Label2" runat ="server" font-size = "XX-Large" Font-Bold = "true" ForeColor = "#003060">Organization Health Monitoring System </asp:Label><br />
                    <asp:Label ID="Label3" runat = "server" Font-Bold = "true" Font-Size = "X-Large" ForeColor = "SteelBlue">
                    Create New Scorecard </asp:Label> <br /> <br />
                    <asp:Table ID="Tableb" runat = "server"> <asp:TableRow> <asp:TableHeaderCell font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce"> 
                    Scorecard Owner:</asp:TableHeaderCell><asp:TableCell>
                    <asp:textbox runat = "server" id = "txtnewSCOwner"></asp:textbox> </asp:TableCell></asp:TableRow> 
                    <asp:TableRow><asp:TableHeaderCell font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce" > 
                    Scorecard Title:</asp:TableHeaderCell> <asp:TableCell> 
                    <asp:textbox runat = "server" id = "txtnewSCName"></asp:textbox></asp:TableCell></asp:TableRow></asp:Table>
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
                          <asp:TableHeaderCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue"> Select Users who may view this scorecard: <br /></asp:TableHeaderCell>
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

<asp:Label ID="Button1" runat = "server" />
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
             TargetControlID="Button1"
             PopupControlID="Panel2"
             DropShadow="true"/>

               </ContentTemplate>
                </asp:UpdatePanel>
                    </ContentTemplate>
                    </cc1:TabPanel>
                 
    </cc1:TabContainer>

--%> <%--Last Verison--%>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
       <asp:SqlDataSource ID="dsMetricGUICatLU" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
                declare @Value varchar(max)
                set @Value = '-- Request New Category --'

	            select SCCategory, SCCID from (
	
                SELECT SCCategory, RANK() over (order by SCCategory asc) as rankin, SCCID FROM KPIS.ScorecardCategory_LU WHERE (SCCActive = 1) 

                Union

                Select  Distinct SCCategory, 99999999999 as rankin, 0 as SCCID from (select @Value as SCCategory from KPIS.ScorecardCategory_LU ) s
                      ) a      
                            order by rankin asc     
            "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsMetricGUITarType" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
select Distinct case when SCMDataType is NULL then 'raw' else SCMDataType end as SCMDataType from DWH.KPIS.ScorecardMetric order by case when SCMDataType is NULL then 'raw' else SCMDataType end asc"></asp:SqlDataSource>
                
<%--                <asp:SqlDataSource ID="dsOhmsMetricSCMID" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="Select SCMName, SCMID from (
select RANK() over (partition by SCMID order by SCMEffectiveToDate desc) as rankin, SCMID, SCMName, SCMActive from DWH.KPIS.ScorecardMetric) a
where rankin = 1 and SCMActive = 1
Order by SCMName asc"></asp:SqlDataSource>--%>

<%--            <asp:SqlDataSource ID="dsOhmsULUName" runat="server" 
                ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                SelectCommand="Select SCUName, case when FirstName is NULL and LastName is NULL then SCUName else FirstName + ' ' + LastName end as Name FROM DWH.KPIS.ScorecardUser_LU ul left JOIN DWH.dbo.Email_Distribution ed on ul.SCUName = ed.NetworkLogin where SCUName <> @Username  group by SCUName, FirstName, LastName Order by case when FirstName is NULL then 1 else 0 end asc, LastName asc, FirstName asc">
                                <SelectParameters>
                        <asp:ControlParameter Name="Username" 
                            DefaultValue='Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")' />
                    </SelectParameters>
            </asp:SqlDataSource>--%>

<%--   <asp:TabContainer ID="OhmsMetricGUI" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "true" >--%> 
<%--   <asp:TabPanel runat = "server" HeaderText = "New Metric" ID = "OhmsMetricGUINM">--%>
   <cc1:tabcontainer ID="OhmsMetricGUI" runat="server" 
   ActiveTabIndex = "0" UseVerticalStripPlacement = "true" > 
   <cc1:TabPanel runat = "server" HeaderText = "Metric" ID = "OhmsMetricGUINM">
 
   <ContentTemplate>
     <asp:Label ID= "Labelbox" runat = "server" font-size = "XX-Large" 
           Font-Bold = "True" ForeColor = "#003060" BorderStyle="None">Organization Health Monitoring System</asp:Label>


<br />
    <asp:Label ID = "Labelbox2" runat ="server" font-size = "X-Large" 
           Font-Bold = "True" ForeColor = "SteelBlue" BorderStyle="None"> Metric Request Form</asp:Label>



    <br /><br />      
    <asp:UpdatePanel runat = "server" ID= "protectDDL">
        <ContentTemplate>
       <asp:panel runat = "server" HorizontalAlign ="Right">
       <asp:DropDownList runat = "server" ID = "UpdateorNew" AutoPostBack="True">
       <asp:ListItem Text="Submit New Metric" Value="0"></asp:ListItem>
       <asp:ListItem Text="Update Existing Metric" Value="1"></asp:ListItem>
       </asp:DropDownList><br /><asp:Label runat = "server" ID = "lblSelectMetric" Font-Bold = "true" Font-Size = "Medium" ForeColor = "SteelBlue"> 
     Select Metric:</asp:Label>
        <asp:DropDownList ID="ddlMetricUpSelect" runat="server" AutoPostBack="True" AppendDataBoundItems = "true" >
        <asp:listitem value="novalueselected">-- Select Metric --</asp:listitem>
        </asp:DropDownList> 
          </asp:panel>
        
</ContentTemplate>
</asp:UpdatePanel>



    <br />
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ViewStateMode="Disabled">
           <ContentTemplate>


       <asp:Table ID="Table1" runat="server">
       <asp:TableRow><asp:TableCell>
    1. Metric Name:  
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
           <asp:TableCell>
        1. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricName" runat="server"> <%-- ValidationGroup="MetricRequestForm" >--%> </asp:TextBox>
         <%--  &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required" ControlToValidate="txtMetricName" ValidationGroup="MetricRequestForm"></asp:RequiredFieldValidator>--%>

    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    2. What is the Metric Objective?
    </asp:TableCell>
    <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        2. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricObjective" runat="server"> <%-- ValidationGroup="MetricRequestForm"--%></asp:TextBox>&nbsp;&nbsp;

    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    3. How is this Metric Defined?
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        3. </asp:TableCell><asp:TableCell> 
               <asp:TextBox ID="txtMetricDefinition" runat="server"> <%--ValidationGroup="MetricRequestForm"--%></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    4. How is this Metric Calculated?
    </asp:TableCell>
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        4. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricCalculated" runat="server" ></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    5. Source Sytem:
    </asp:TableCell> 
        <asp:TableCell></asp:TableCell>
               <asp:TableCell>
        5. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricSourceSystem" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell Font-Bold = "true" Font-Size = "Large" ForeColor = "SteelBlue">

    Targets</asp:TableCell></asp:TableRow><asp:TableRow>
               <asp:TableCell>
    6. Current Target Value:
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        6. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricTarget" runat="server"> <%-- ValidationGroup="MetricRequestForm">--%></asp:TextBox> <%-- &nbsp;&nbsp;<asp:RequiredFieldValidator
                ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required" ControlToValidate="txtMetricTarget" ValidationGroup="MetricRequestForm"></asp:RequiredFieldValidator>--%>
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
                ID="CalendarExtender1" runat="server" TargetControlID = "txtMetricLTGoalDate">
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
                           <asp:ListItem Value="0">Request Other Frequency</asp:ListItem>
        </asp:dropdownlist><asp:TextBox runat = "server" ID = "secret" visible = "false"> 1 </asp:TextBox>
    </asp:TableCell><asp:TableCell><asp:TextBox runat = "server" ID = "ReqFreq" Visible = "false"></asp:TextBox></asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    16. Who will be in charge of reporting the values of this Metric?
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        16. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwner" runat="server"></asp:TextBox>
    </asp:TableCell><asp:TableCell><asp:Label runat = "server" ID = "reqfreqdesc" Font-Size = "X-Small" Visible = "false"></asp:Label></asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    17. What is their email address?
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        17. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwneremail" runat="server"></asp:TextBox>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow><asp:TableCell>
    18. What is their network login?
    </asp:TableCell>
        <asp:TableCell>*</asp:TableCell>
               <asp:TableCell>
        18. </asp:TableCell><asp:TableCell><asp:TextBox ID="txtMetricOwnerlogin" runat="server"><%-- ValidationGroup="MetricRequestForm">--%></asp:TextBox> <%-- &nbsp;&nbsp;<asp:RequiredFieldValidator
                ID="RequiredFieldValidator3" runat="server" ErrorMessage="This field is required" ControlToValidate="txtMetricOwnerlogin" ValidationGroup="MetricRequestForm"></asp:RequiredFieldValidator>--%>
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
       <asp:Button ID="btnNewMetricSubmit" runat="server" Text="Submit" />
       <asp:Label ID = "Whatisworng" runat ="server"></asp:Label>
<asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" BackColor = "#eee4ce" BorderColor="#666666" Font-Bold="True">
    

   <br />
   <center><asp:label ID = "explantionlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="OkButton" runat="server" Text="OK"/>

      
      <br />
      <br />
      
   </center>
   </asp:Panel>
   <br />
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender1" 
             runat="server"  
             
             TargetControlID="FakeButton"
             PopupControlID="Panel1"
             DropShadow="true"/>


    <br /><br /><br />
               <asp:Label runat = "server" ID = "Creatmetriclabel"></asp:Label>
    <small> Fields marked with * may not be left blank </small>
    <asp:Panel runat = "server" width = "100%" HorizontalAlign = "Right"><asp:Label runat = "server" ID = "contactlabel" Font-Size = "Smaller" Width = "200"></asp:Label></asp:Panel>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="ddlMetricUpSelect" 
                EventName="SelectedIndexChanged" />
</Triggers>
</asp:UpdatePanel>



    
</ContentTemplate>
    


</cc1:TabPanel>
 <%--   </asp:TabPanel>--%>
    <%--<asp:TabPanel runat="server" ID ="OhmsMetricUM" HeaderText = "Update Metric">--%>
    <%--<cc1:TabPanel runat="server" ID ="OhmsMetricUM" HeaderText = "Update Metric">
    <ContentTemplate><br />
 <asp:Label ID="LabelwasTxtbox3" runat ="server" font-size = "XX-Large" Font-Bold = "true" ForeColor = "#003060">Organization Health Monitoring System </asp:Label> <br />
    
<asp:Label ID= "Labelwastxtbox2" runat ="server" font-size = "X-Large" Font-Bold = "true" ForeColor = "SteelBlue"> Metric Update Form </asp:Label>

    <br />
        
    <br />   


    <asp:UpdatePanel runat = "server" ID = "updatepanelupdatemetric" UpdateMode="Conditional" ViewStateMode="Disabled">
    <ContentTemplate>
    
                <asp:SqlDataSource ID= "dsSelectedMetricInfo" runat = "server"
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="Select * FROM DWH.KPIS.ScorecardMetric where ID = @ID" UpdateCommand = "Select * from DWH.KPIS.ScorecardMetric"
            >
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlMetricUpSelect" Name="ID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SCMName" />
                    </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <%-- %><asp:panel runat = "server" ID = "MetUpdateScrollbarpanel" Scrollbars = "Horizontal">
        <asp:GridView runat = "server" ID = "UpdatemetricGridView" 
            DataSourceID = "dsSelectedMetricInfo" DataKeyNames="ID" 
                AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#EEE4CE" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="SCMID" HeaderText="SCMID" SortExpression="SCMID" />
                <asp:BoundField DataField="SCMName" HeaderText="SCMName" 
                    SortExpression="SCMName" />
                <asp:BoundField DataField="SCMObjective" HeaderText="SCMObjective" 
                    SortExpression="SCMObjective" />
                <asp:BoundField DataField="SCMMeasures" HeaderText="SCMMeasures" 
                    SortExpression="SCMMeasures" />
                <asp:BoundField DataField="SCMDefinition" HeaderText="SCMDefinition" 
                    SortExpression="SCMDefinition" />
                <asp:BoundField DataField="SCMCalculations" HeaderText="SCMCalculations" 
                    SortExpression="SCMCalculations" />
                <asp:BoundField DataField="SCMSourceSystem" HeaderText="SCMSourceSystem" 
                    SortExpression="SCMSourceSystem" />
                <asp:BoundField DataField="SCMLTTarget" HeaderText="SCMLTTarget" 
                    SortExpression="SCMLTTarget" />
                <asp:BoundField DataField="SCMLTGoalDate" HeaderText="SCMLTGoalDate" 
                    SortExpression="SCMLTGoalDate" />
                <asp:BoundField DataField="SCMTarget" HeaderText="SCMTarget" 
                    SortExpression="SCMTarget" />
                <asp:BoundField DataField="SCMDataType" HeaderText="SCMDataType" 
                    SortExpression="SCMDataType" />
                <asp:CheckBoxField DataField="SCMCumulative" HeaderText="SCMCumulative" 
                    SortExpression="SCMCumulative" />
                <asp:BoundField DataField="SCMMax" HeaderText="SCMMax" 
                    SortExpression="SCMMax" />
                <asp:BoundField DataField="SCMMin" HeaderText="SCMMin" 
                    SortExpression="SCMMin" />
                <asp:BoundField DataField="SCMwMax" HeaderText="SCMwMax" 
                    SortExpression="SCMwMax" />
                <asp:BoundField DataField="SCMwMin" HeaderText="SCMwMin" 
                    SortExpression="SCMwMin" />
                <asp:BoundField DataField="SCMUpdateMethod" HeaderText="SCMUpdateMethod" 
                    SortExpression="SCMUpdateMethod" />
                <asp:BoundField DataField="SCMUpdateInput" HeaderText="SCMUpdateInput" 
                    SortExpression="SCMUpdateInput" />
                <asp:BoundField DataField="SCMUpdateFrequency" HeaderText="SCMUpdateFrequency" 
                    SortExpression="SCMUpdateFrequency" />
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
            <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
</asp:panel> --%>
<%--
        
        <br />
        <br>
        <asp:Panel runat = "server" ID = "RequestedUpdatesPanel" Scrollbars = "Horizontal">
<br />  Updates:

        <asp:GridView runat = "server" ID = "GridRequestedUpdates" 
                DataSourceID = "dsSelectedMetricInfo" AutoGenerateColumns="False" 
                DataKeyNames="ID">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" Visible = "false"/>
            <asp:BoundField DataField="SCMID" HeaderText="SCMID" Visible = "False" SortExpression="SCMID" />
            <asp:BoundField DataField="SCMName" HeaderText="Name" 
                SortExpression="SCMName" />
            <asp:BoundField DataField="SCMObjective" HeaderText="Objective" 
                SortExpression="SCMObjective" />
            <asp:BoundField DataField="SCMMeasures" HeaderText="Measures" 
                SortExpression="SCMMeasures" />
            <asp:BoundField DataField="SCMDefinition" HeaderText="Definition" 
                SortExpression="SCMDefinition" />
            <asp:BoundField DataField="SCMCalculations" HeaderText="Calculations" 
                SortExpression="SCMCalculations" />
            <asp:BoundField DataField="SCMSourceSystem" HeaderText="SourceSystem" 
                SortExpression="SCMSourceSystem" />
            <asp:BoundField DataField="SCMLTTarget" HeaderText="Long Term Target" 
                SortExpression="SCMLTTarget" />
            <asp:BoundField DataField="SCMLTGoalDate" HeaderText="Long Term Goal Date" 
                SortExpression="SCMLTGoalDate" />
            <asp:BoundField DataField="SCMTarget" HeaderText="Target" 
                SortExpression="SCMTarget" />
<asp:TemplateField ConvertEmptyStringToNull="true" HeaderText="Data Type" 
                   SortExpression="SCMDataType">
                   <EditItemTemplate>
                       <asp:DropDownList ID="ddlDataType" runat="server">
                           <asp:ListItem Value="'none'">Select One</asp:ListItem>
                           <asp:ListItem Value="NULL"> </asp:ListItem>
                           <asp:ListItem Value="'money'">money</asp:ListItem>
                           <asp:ListItem Value="'percent'">percent</asp:ListItem>
                           <asp:ListItem Value="NULL">number</asp:ListItem>
                       </asp:DropDownList>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Label ID="DATA_TYPE2" runat="server" Text='<%# Eval("SCMDataType") %>'> &gt;</asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>


            <asp:CheckBoxField DataField="SCMCumulative" HeaderText="Cumulative" 
                SortExpression="SCMCumulative" />
            <asp:BoundField DataField="SCMMax" HeaderText="Max" 
                SortExpression="SCMMax" />
            <asp:BoundField DataField="SCMMin" HeaderText="Min" 
                SortExpression="SCMMin" />
            <asp:BoundField DataField="SCMwMax" HeaderText="Warning Max" 
                SortExpression="SCMwMax" />
            <asp:BoundField DataField="SCMwMin" HeaderText="Warning Min" 
                SortExpression="SCMwMin" />
            <asp:BoundField DataField="SCMUpdateMethod" HeaderText="Update Method" 
                SortExpression="SCMUpdateMethod" />
            <asp:BoundField DataField="SCMUpdateInput" HeaderText="Update Input" 
                SortExpression="SCMUpdateInput" />
            <asp:BoundField DataField="SCMUpdateFrequency" HeaderText="Update Frequency" 
                SortExpression="SCMUpdateFrequency" />
            <asp:BoundField DataField="SCMOwner" HeaderText="Metric Owner" 
                SortExpression="SCMOwner" />
            <asp:BoundField DataField="SCMCategory" HeaderText="Category" 
                SortExpression="SCMCategory" />
            <asp:BoundField DataField="SCMEffectiveFromDate" visible = "false"
                HeaderText="SCMEffectiveFromDate" SortExpression="SCMEffectiveFromDate" />
            <asp:BoundField DataField="SCMEffectiveToDate" HeaderText="Effective To Date" 
                SortExpression="SCMEffectiveToDate" />
            <asp:CheckBoxField DataField="SCMActive" Visible = "false" HeaderText="SCMActive" 
                SortExpression="SCMActive" />
            <asp:BoundField DataField="SCMUpdated" visible = "false" HeaderText="SCMUpdated" 
                SortExpression="SCMUpdated" />
        
        </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

        </asp:GridView>
        --%>
       <%--  <asp:Table runat = "server" ID = "RequestedUpdates">
        <asp:TableHeaderRow >
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Name </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Objective </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Measures </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Definition </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Calculations </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> SourceSystem </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Long Term Target </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Long Term GoalDate </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Target </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Data Type </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Cumulative </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Max </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Min </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Warning Max </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Warning Min </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Update Method </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> UpdateFrequency </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Metric Owner </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Category </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Effective To Date </center> </asp:TableHeaderCell>
        <asp:TableHeaderCell BackColor = "SteelBlue" Font-Bold = "true" BorderColor = "#999999" ForeColor = "White" > <center> Active </center> </asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableRow BackColor = "#eee4ce">
        <asp:TableCell><asp:TextBox runat = "server" ID = "txtUpdMetricName" Text='<%# DataBinder.Eval(datasetmain, 
       "Tables[Table1].DefaultView.[0].SCMName") %>'>> </asp:TextBox> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>
        <asp:TableCell> </asp:TableCell>

        </asp:TableRow>
        </asp:Table>--%>
 <%--       </asp:Panel>
        
        <br />        </ContentTemplate>

        </asp:UpdatePanel></ContentTemplate>
--%>
 <%--   </asp:TabPanel>--%>
 <%--   <asp:TabPanel runat = "server" HeaderText = "Create New Scorecard" ID = "panelcreatescorecard">--%>
 <%--   </cc1:TabPanel>--%>--%>
    <cc1:TabPanel runat = "server" HeaderText = "Create New Scorecard" ID = "panelcreatescorecard">
                    <ContentTemplate> 
                     
 
                     
 

                     
 
                     <br /><asp:Label ID="Label1" runat ="server" font-size = "XX-Large" Font-Bold = "true" ForeColor = "#003060">Organization Health Monitoring System </asp:Label><br />
                    <asp:Label runat = "server" Font-Bold = "true" Font-Size = "X-Large" ForeColor = "SteelBlue">
                    Create New Scorecard </asp:Label> <br /> <br />
                    <asp:Table runat = "server"> <%--<asp:TableRow> <asp:TableHeaderCell font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce"> 
                    Scorecard Owner:</asp:TableHeaderCell><asp:TableCell>
                    <asp:textbox runat = "server" id = "txtnewSCOwner" ValidationGroup="CNSValidation"></asp:textbox> </asp:TableCell><asp:TableCell> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="This field is required"
                     ValidationGroup="CNSValidation" ControlToValidate="txtnewSCOwner"></asp:RequiredFieldValidator></asp:TableCell></asp:TableRow>--%> 
                    <asp:TableRow><asp:TableHeaderCell font-Bold = "true" ForeColor = "#003060" BackColor = "#eee4ce" > 
                    Scorecard Title:</asp:TableHeaderCell> <asp:TableCell> 
                        
                    <asp:textbox runat = "server" id = "txtnewSCName"> <%-- ValidationGroup="CNSValdiation"--%></asp:textbox></asp:TableCell><asp:TableCell>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="This field is required" ControlToValidate="txtnewSCName" ValidationGroup="CNSValdiation" SetFocusOnError="False"></asp:RequiredFieldValidator>
                    --%></asp:TableCell>
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
                        <asp:Panel runat = "server" ScrollBars = "Vertical" > <asp:Label runat = "server" Font-Bold = "true" ForeColor = "SteelBlue" BackColor = "#eee4ce"> Your Metrics: </asp:Label> <br />
                        <asp:CheckBoxList ID="cblCNSMetricSelect" runat="server" >
                        </asp:CheckBoxList></asp:Panel>
                        </asp:TableCell>
                         <asp:TableCell VerticalAlign = "Top">
                         <asp:Panel runat = "server">
                         <asp:Label runat = "server" Font-Bold = "true" ForeColor = "SteelBlue" BackColor = "#eee4ce"> Select Other Users who may view this scorecard: <br /></asp:Label> 
                         <asp:CheckBoxList ID="cblCNSUsersSelect" runat="server">
                        </asp:CheckBoxList>  
                          </asp:Panel>            
                         </asp:TableCell>
                         </asp:TableRow>
                         </asp:Table>
                         
<%--                          <asp:TableCell RowSpan = "2" VerticalAlign = "Bottom">
                        <asp:Panel ID="Panel3" runat = "server" ScrollBars = "Vertical" > 
                        <asp:Label ID="Label2" runat = "server" Font-Bold = "true" ForeColor = "SteelBlue" BackColor = "#eee4ce"> Public Metrics: </asp:Label> <br />
                        <asp:CheckBoxList ID="cblpublicMetrics" runat="server">
                        </asp:CheckBoxList></asp:Panel>                          
                          </asp:TableCell>--%>
                        <br />
                        <asp:Panel runat = "server" ScrollBars = "Vertical">
                        <asp:Button ID="btnCNSPreview" runat="server" Text="Preview Metrics" /> 
                        <br />
                        <br />
                        <asp:gridview runat="server" ID = "gridCNSPreviewMetric" AlternatingRowStyle-BackColor = "#eee4ce" HeaderStyle-BackColor = "SteelBlue" HeaderStyle-Font-Bold ="true" HeaderStyle-ForeColor = "White">
                        </asp:gridview>   </asp:Panel>
                        <br />
                        <asp:Button ID="btnCNSSubmit" runat="server" Text="Submit" />
 



<asp:Label ID="Button1" runat = "server" />
   <asp:Panel ID="Panel2" runat="server" Width="233px"  BackColor = "#eee4ce" BorderColor="#666666" Font-Bold="True">
    

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
             TargetControlID="Button1"
             PopupControlID="Panel2"
             DropShadow="true"/>

               </ContentTemplate>
                </asp:UpdatePanel>
                    
</ContentTemplate>
                   <%-- </asp:TabPanel>--%>
                    


</cc1:TabPanel>
 <%--   </asp:TabContainer>--%>
    </cc1:tabcontainer>
<%--    </div>
    </form>
</body>
</html>--%>

</asp:Content>