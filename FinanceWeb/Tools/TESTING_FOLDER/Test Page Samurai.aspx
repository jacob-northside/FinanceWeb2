<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Test Page Samurai.aspx.vb" Inherits="FinanceWeb.Test_Page_Samurai" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 

     <style type="text/css">

.modalBackground2 
{
    background-color: #6da9e3 !important;
    background-image: none !important;
    border: 1px solid #003060;
    font-size: medium;
    color: #003060;
    width: 300px;
    padding:5px;
    vertical-align:middle;
    text-align:center;
  

}
</style>

    <div>

   
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

                     var xPos2, yPos2;
                     var prm2 = Sys.WebForms.PageRequestManager.getInstance();

                     function BeginRequestHandler2(sender, args) {
                         if ($get('<%=ScrollPanelp2.ClientID%>') != null) {
                             // Get X and Y positions of scrollbar before the partial postback
                             xPos2 = $get('<%=ScrollPanelp2.ClientID%>').scrollLeft;
                             yPos2 = $get('<%=ScrollPanelp2.ClientID%>').scrollTop;
                         }
                     }

                     function EndRequestHandler2(sender, args) {
                         if ($get('<%=ScrollPanelp2.ClientID%>') != null) {
                             // Set X and Y positions back to the scrollbar
                             // after partial postback
                             $get('<%=ScrollPanelp2.ClientID%>').scrollLeft = xPos2;
                             $get('<%=ScrollPanelp2.ClientID%>').scrollTop = yPos2;
                         }
                     }

                     prm2.add_beginRequest(BeginRequestHandler2);
                     prm2.add_endRequest(EndRequestHandler2);

                     var xPos3, yPos3;
                     // var prm3 = Sys.WebForms.PageRequestManager.getInstance();

                     function BeginRequestHandler3(sender, args) {
                         if ($get('<%=ScrollPanelp3.ClientID%>') != null) {
                             // Get X and Y positions of scrollbar before the partial postback
                             xPos3 = $get('<%=ScrollPanelp3.ClientID%>').scrollLeft;
                             yPos3 = $get('<%=ScrollPanelp3.ClientID%>').scrollTop;
                         }
                     }

                     function EndRequestHandler3(sender, args) {
                         if ($get('<%=ScrollPanelp3.ClientID%>') != null) {
                             // Set X and Y positions back to the scrollbar
                             // after partial postback
                             $get('<%=ScrollPanelp3.ClientID%>').scrollLeft = xPos3;
                             $get('<%=ScrollPanelp3.ClientID%>').scrollTop = yPos3;
                         }
                     }

                     prm.add_beginRequest(BeginRequestHandler3);
                     prm.add_endRequest(EndRequestHandler3);

                     var xPos4, yPos4;
                    // var prm3 = Sys.WebForms.PageRequestManager.getInstance();

                     function BeginRequestHandler4(sender, args) {
                         if ($get('<%=ScrollPanel2p3.ClientID%>') != null) {
                             // Get X and Y positions of scrollbar before the partial postback
                             xPos4 = $get('<%=ScrollPanel2p3.ClientID%>').scrollLeft;
                             yPos4 = $get('<%=ScrollPanel2p3.ClientID%>').scrollTop;
                         }
                     }

                     function EndRequestHandler4(sender, args) {
                         if ($get('<%=ScrollPanel2p3.ClientID%>') != null) {
                             // Set X and Y positions back to the scrollbar
                             // after partial postback
                             $get('<%=ScrollPanel2p3.ClientID%>').scrollLeft = xPos4;
                             $get('<%=ScrollPanel2p3.ClientID%>').scrollTop = yPos4;
                         }
                     }

                     prm.add_beginRequest(BeginRequestHandler4);
                     prm.add_endRequest(EndRequestHandler4);


                     var xPos5, yPos5;
                     // var prm3 = Sys.WebForms.PageRequestManager.getInstance();

                     function BeginRequestHandler5(sender, args) {
                         if ($get('<%=pnlScrollPanelGalenUpdates.ClientID%>') != null) {
                             // Get X and Y positions of scrollbar before the partial postback
                             xPos5 = $get('<%=pnlScrollPanelGalenUpdates.ClientID%>').scrollLeft;
                             yPos5 = $get('<%=pnlScrollPanelGalenUpdates.ClientID%>').scrollTop;
                         }
                     }

                     function EndRequestHandler5(sender, args) {
                         if ($get('<%=pnlScrollPanelGalenUpdates.ClientID%>') != null) {
                             // Set X and Y positions back to the scrollbar
                             // after partial postback
                             $get('<%=pnlScrollPanelGalenUpdates.ClientID%>').scrollLeft = xPos5;
                             $get('<%=pnlScrollPanelGalenUpdates.ClientID%>').scrollTop = yPos5;
                         }
                     }

                     prm.add_beginRequest(BeginRequestHandler5);
                     prm.add_endRequest(EndRequestHandler5);


                     function open_win() {
                         window.open("https://financeweb.northside.local/Tools/ManagedCare/Profee/ProFeeInstructions.aspx", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
                     }
                     function open_win2() {
                         window.open("https://financeweb.northside.local/Tools/ManagedCare/Profee/ProFeeInstructions.aspx?t=1", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
                     }
                     function open_win3() {
                         window.open("https://financeweb.northside.local/Tools/ManagedCare/Profee/ProFeeInstructions.aspx?t=2", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
                     }
                     function open_win4() {
                         window.open("https://financeweb.northside.local/Tools/ManagedCare/Profee/ProFeeInstructions.aspx?t=3", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");
                     }



                     function SelectAllCheckboxesSpecific(spanChk) {

                         if ($get('<%=gvGalenBlanks.ClientID%>') != null) {
                             var IsChecked = spanChk.checked;

                             var Chk = spanChk;

                             var items = $get('<%=gvGalenBlanks.ClientID%>').getElementsByTagName('input');

                             for (i = 0; i < items.length; i++) {

                                 if (items[i].id != Chk && items[i].type == "checkbox") {

                                     if (items[i].checked != IsChecked) {

                                         items[i].click();

                                     }

                                 }

                             }

                         }
                     }
 
</script>    

         <asp:SqlDataSource ID="dsPayorList" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
    SelectCommand="SELECT Distinct PayorName
  FROM [DWH].[ProFee].[FeeSchedule]
  order by PayorName asc"
    ></asp:SqlDataSource>  

                    <cc1:tabcontainer ID="ProFeeToolTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False" width="1150px" >
                    <cc1:TabPanel runat = "server" HeaderText = "Schedule Updates" ID = "ProFeeMainSchedules" >
                    <ContentTemplate>         
    <asp:UpdatePanel runat="server" ID= "udpMain">

    <ContentTemplate>
   <%--  <div style=" z-index: 1"> --%>
<%--    <asp:Label ID="Label1" runat = "server" Font-Bold = "true" Font-Size = "XX-Large" ForeColor = "#003060">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pro Fee Update Tool
    </asp:Label><br /><br />--%>
       <%-- <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Tools/ManagedCare/Profee/ProFeeInstructions.aspx" Target="_blank" Text="Instructions">HyperLink</asp:HyperLink>--%>
       <asp:Panel runat = "server" HorizontalAlign = "Right" > <asp:Button ID="lbOpenProFeeInst" runat="server" Text="Open Instructions"  OnClientClick="open_win()"  /> </asp:Panel>

    <asp:Table ID="Table1" runat = "server"> <asp:TableRow> <asp:TableHeaderCell   ForeColor = "#4A8fd2" HorizontalAlign="Right">
    <asp:Label ID="Label2" runat = "server" Font-Size = "small">
    Select Payor:&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableHeaderCell><asp:TableCell HorizontalAlign = "Left" Font-Size = "small"> 
        <asp:DropDownList ID="ddlSelectedPayor" runat="server" DataSourceID="dsPayorList" 
            DataTextField="PayorName" DataValueField="PayorName" AutoPostBack = "true"
            AppendDataBoundItems="True" Font-Size = "small">
            <asp:listitem value="novalueselected">-- Select Value --</asp:listitem>
                    </asp:DropDownList>
    </asp:TableCell></asp:TableRow><asp:TableRow></asp:TableRow><asp:TableRow></asp:TableRow><asp:TableRow><asp:TableHeaderCell Font-Bold = "true" ForeColor = "#4A8fd2" HorizontalAlign="Right">
                    <asp:SqlDataSource ID= "dsPossibleFeeSched" runat = "server"
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
            declare @Value varchar(max)
            set @Value = '  -- None --  '

            Select Distinct FeeSchedName from (select @Value as FeeSchedName from DWH.ProFee.FeeSchedule ) s

            Union

            select Distinct FeeSchedName from DWH.ProFee.FeeSchedule
            where PayorName = @PayorName or @PayorName = 'novalueselected'
            order by FeeSchedName asc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlSelectedPayor" Name="PayorName" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
        </asp:SqlDataSource>
    <asp:Label ID="Label3" runat = "server" Font-Size = "small">
    Select Fee Schedule:&nbsp;&nbsp;&nbsp;<br />(Optional)&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableHeaderCell><asp:TableCell HorizontalAlign = "Left" Font-Size = "small"> 
    <asp:DropDownList runat = "server" ID = "ddlSelectedFS" DataSourceID = "dsPossibleFeeSched" Font-Size = "small" DataTextField = "FeeSchedName" DataValueField = "FeeSchedName" AutoPostBack = "true" AppendDataBoundItems="False" Enabled="False">
    </asp:DropDownList>
    </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableHeaderCell Font-Bold = "true" ForeColor = "#4A8fd2" HorizontalAlign="Right">

    <asp:SqlDataSource ID= "dsTestingDataSource" runat = "server"
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
            
                    if OBJECT_ID('tempdb..#tempCat') is not null
	begin 
	drop table tempCat
	end 
                if OBJECT_ID('tempdb..#tempCat2') is not null
	begin 
	drop table tempCat2
	end 

create table #tempCat (rownumber int, TIN varchar(max))
        declare @cat2 NVARCHAR(max)
        create table #tempCat2 (rowlist varchar(max), TIN varchar(max))
        insert into #tempCat2 (rowlist, TIN ) values (-1, '  -- None --  ')

        DECLARE @concatenated NVARCHAR(max)
        declare @counter integer = (select MAX(rankin) from (
        select FeeSchedID, 
        DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] desc ,[ManagersNotes]) 
        as rankin  
        from DWH.ProFee.FeeSchedule where PayorName = @PayorName and (FeeSchedName = @FeeSchedName or @FeeSchedName = '  -- None --  ') 
        ) x )

		declare @i integer = 1
		
		while @i < @counter + 1
		begin
        set @concatenated = ''
        
        SELECT @concatenated = @concatenated + case when TIN = '' then '' when TIN = ' ' then '' else TIN + ', ' end
        FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, 
        DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate] desc, [Default] ,[ManagersNotes]) 
        as rankin  from DWH.ProFee.FeeSchedule where PayorName = @PayorName and (FeeSchedName = @FeeSchedName or @FeeSchedName = '  -- None --  ') ) 
        x where rankin = @i) order by TIN, FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate], [Default] ,[ManagersNotes]
              
        insert into #tempCat (rownumber, TIN) 
        Select top 1 @i , left(@concatenated, LEN(@concatenated) - 1) as TIN 
        FROM DWH.ProFee.FeeSchedule where 
        FeeSchedID in (select FeeSchedID from (select FeeSchedID, DENSE_RANK() over (partition by PayorName 
        order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate] desc, [Default] ,[ManagersNotes]) 
        as rankin  from DWH.ProFee.FeeSchedule where PayorName = @PayorName and 
        (FeeSchedName = @FeeSchedName or @FeeSchedName = '  -- None --  ') ) x where rankin = @i) 
        order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate], [Default] ,[ManagersNotes]
        
        
        set @i = @i + 1
        end
        
        declare @counter2 int = (select COUNT(distinct TIN) from #tempCat)
        declare @j int = 0
        
        while @j < @counter2 + 1
        begin
        set @cat2 = '('
        select @cat2 = @cat2 + convert(varchar, rownumber) + ', '
        from (select *, Dense_Rank() over (order by TIN) as rnk from #tempCat) x where rnk = @j
        
        insert into #tempCat2 ( rowlist ,TIN) Select top 1 left(@cat2, LEN(@cat2) - 1) + ')' as rowlist , TIN
        from (select *, Dense_Rank() over (order by TIN) as rnk from #tempCat) x where rnk = @j
        
        set @j	= @j + 1
        
        end
        
        select * from #tempCat2
            
            ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlSelectedPayor" Name="PayorName" 
                            PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="ddlSelectedFS" Name="FeeSchedName" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
        </asp:SqlDataSource>

    <asp:Label runat = "server" Font-Size = "small">
    Select TINs:&nbsp;&nbsp;&nbsp;<br />(Optional)&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableHeaderCell><asp:TableCell HorizontalAlign = "Left" Font-Size = "small"> 
    <asp:DropDownList runat = "server" ID = "ddlSelectedTINs" DataSourceID = "dsTestingDataSource" Font-Size = "small" DataTextField = "TIN" DataValueField = "rowlist" AutoPostBack = "true" AppendDataBoundItems="False" Enabled="False">
    </asp:DropDownList>
    </asp:TableCell></asp:TableRow></asp:Table>

    <asp:Label runat = "server" ID = "testinglabel1"></asp:Label>

    <br />

     <%--  <asp:SqlDataSource ID= "dsGridView" runat = "server"
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule
  where PayorName = @PayorName
  and FeeSchedName = @FSName">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlSelectedPayor" Name="PayorName" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlSelectedFS" Name="FSName" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SCMName" />
                    </UpdateParameters>
        </asp:SqlDataSource> --%>
        <asp:Panel runat = "server" ID = "ScrollPanel" ScrollBars = "Vertical" Width="100%">
        
     <asp:GridView ID="GridView1" runat="server"  HeaderStyle-BackColor = "#4A8fd2" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" BackColor="#CBE3FB" 
            AlternatingRowStyle-BackColor = "White" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" AllowPaging="False" 
                HorizontalAlign="Left" Font-Size="small" >
            <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
            </Columns>
            <%-- DataSourceID = "dsGridView"--%>
    </asp:GridView></asp:Panel><br />
        
        <asp:Panel runat = "server" Width = "100%" Visible = "false" font-Size = "small" ID = "PanelRed" HorizontalAlign = "Center" BorderColor = "Red" BorderWidth = "2" BackColor = "#CBE3FB"> 
            <asp:Label runat = "server" ID = "CountLabel" ForeColor = "#4A8fd2" Font-Bold = "true"></asp:Label>
        </asp:Panel><br />
        <asp:Panel runat = "server" ID = "CurrentFSPanel" Visible="False" HorizontalAlign="left" BorderStyle="NotSet">
            <asp:Label runat = "server" visible = "False" ID = "FakelblPayorName"></asp:Label>
            <asp:Label runat = "server" visible = "false" ID = "FakelblFSName"></asp:Label>
            <asp:Label runat = "server" visible = "false" ID = "FakelblStartDate"></asp:Label>
            <asp:Label runat = "server" visible = "false" ID = "FakelblEndDate"></asp:Label>
            <asp:Label runat = "server" visible = "false" ID = "FakelblDefault"></asp:Label>
            <asp:Label runat = "server" visible = "false" ID = "FakelblManagersNotes"></asp:Label>
    <asp:Table runat = "server" HorizontalAlign="Left" Width = "100%">
        <asp:TableRow> 
            <asp:TableCell VerticalAlign="Top">
                <asp:Table ID="Table2" runat = "server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
                    <asp:TableRow>
                        <asp:TableCell Width="5px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="Label4" runat = "server" Height="21px" Font-Size = "small" BackColor = "#CBE3FB" ForeColor = "#4A8fd2" Font-Bold = "true" Width="150" > Payor:&nbsp;&nbsp;</asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Font-Size = "small">
                            &nbsp;&nbsp;<asp:Textbox runat = "server" ID = "txtPayorName" ></asp:Textbox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="5px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="Label7" runat = "server" Height="21px" BackColor = "#CBE3FB" ForeColor = "#4A8fd2" font-size = "small" Font-Bold = "true" Width="150" >Fee Schedule:&nbsp;&nbsp;</asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Font-Size = "small">
                            &nbsp;&nbsp;<asp:Textbox runat = "server" ID = "txtFSName"></asp:Textbox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="5px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="Label8" runat = "server" Height="21px" BackColor = "#CBE3FB" ForeColor = "#4A8fd2" Font-Size = "small" Font-Bold = "true" Width="150" >TINs:&nbsp;&nbsp;</asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Font-Size = "small" >
                            &nbsp;&nbsp;<asp:Label runat = "server" ID = "lblPreviousTIN"></asp:Label>
                            <asp:Label runat = "server" visible = "False" ID = "fakelabelTIN"></asp:Label>
                            <asp:Label runat = "server" visible = "False" ID = "fakelableLoadDate"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell RowSpan="2">
                <asp:Panel ID="ChecklistPanel" runat="server" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Outset" BorderWidth="2" >
                    <asp:Table runat = "server">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell BackColor = "#4A8fd2" ForeColor = "White" Font-Size = "small"  Font-Bold = "true" HorizontalAlign = "Center" ColumnSpan="2">TINs:</asp:TableHeaderCell>
                        </asp:TableHeaderRow> 
                        <asp:TableRow>
                            <asp:TableCell Font-Size = "small" HorizontalAlign="Left">
                                <asp:CheckBox ID = "cbALL" runat = "server" Text = "ALL" />
                            </asp:TableCell>
                            <asp:TableCell Font-Size = "small" HorizontalAlign="Left">
                                <asp:CheckBox ID = "cbMANGSPC" runat = "server" Text = "MANG (SPC Only)" />
                            </asp:TableCell>
                        </asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID="cbACC" runat="server" Text = "ACC"/></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left"><asp:CheckBox ID = "cbNAPS" runat = "server" Text = "NAPS" /></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID="cbAGA" runat="server" Text = "AGA"/></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left"><asp:CheckBox ID="cbNCPS" runat="server" Text = "NCPS"/></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID = "cbGCS" runat = "server" Text = "GCS" /></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID = "cbNPCPS" runat = "server" Text = "NPCPS" /></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID = "cbGSPS" runat = "server" Text = "GSPS" /></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left"><asp:CheckBox ID="cbNSPSex" runat="server" Text = "NSPS (excl gyn onc)"/></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID = "cbGU" runat = "server" Text = "GU" /></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left"><asp:CheckBox ID="cbNSPSonly" runat="server" Text = "NSPS (gyn onc only)"/></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID = "cbMANG" runat = "server" Text = "MANG (All)" /></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left"><asp:CheckBox ID="cbNSPSinc" runat="server" Text = "NSPS (incl gyn onc)"/></asp:TableCell></asp:TableRow>
                     <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:CheckBox ID = "cbMANGPCP" runat = "server" Text = "MANG (PCP Only)" /></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left"><asp:CheckBox ID="cbPNFM" runat="server" Text = "PNFM" /></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "Small" HorizontalAlign = "Left"><asp:CheckBox ID = "cbUSA" runat = "server" Text = "USA" /></asp:TableCell></asp:TableRow>
                <asp:TableRow><asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:Label runat = "server">&#160;&#160;Other: </asp:Label></asp:TableCell>
                    <asp:TableCell Font-Size = "small" HorizontalAlign="Left">
            <asp:TextBox runat = "server" ID = "txtFree" ></asp:TextBox></asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
    </asp:TableCell>
    </asp:TableRow><asp:TableRow>
    <asp:TableCell>
    <asp:Panel runat = "server" ID = "Organization" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Outset" BorderWidth="2" Font-Bold="True">
        <asp:Table runat = "server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
    <asp:TableRow>
        <asp:TableCell Width="5px"></asp:TableCell>
        <asp:TableCell HorizontalAlign="Right" font-Size = "small">
            <asp:Label Height="21px" runat = "server"  BackColor = "#4A8fd2" ForeColor = "White" Font-Bold = "true" Width="150"  > Dates:&nbsp;&nbsp;</asp:Label>
        </asp:TableCell> 
        <asp:TableCell>
            <asp:Panel runat = "server" Font-Size = "medium"> 
                <asp:TextBox runat = "server" ID = "txtCurrentED" font-size = "small"></asp:TextBox>
                <asp:Label runat = "server" Font-Size = "small" Width = "75">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                <asp:TextBox runat = "server" ID = "txtCurrentEndD" Font-Size = "small" ></asp:TextBox>
                    <cc1:CalendarExtender runat="server" TargetControlID="txtCurrentED">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender runat ="server" TargetControlID = "txtCurrentEndD" >
                    </cc1:CalendarExtender>
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell HorizontalAlign="Right" Font-Size = "small">
            <asp:Label Height="56px" ID="Label5" runat = "server" BackColor = "#4A8fd2" ForeColor = "White" Font-Bold = "true" Width="150" > Default:&nbsp;&nbsp;</asp:Label>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat = "server" ID = "txtCurrentDefault" TextMode="MultiLine" Width="400" Rows="3" Font-Size = "small"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell HorizontalAlign="Right">
            <asp:Label Height="53px" BackColor = "#4A8fd2" ForeColor = "White" Font-Bold = "true" Font-Size = "small" Width="150" ID="Label6" runat = "server"> Manager's Notes:&nbsp;&nbsp;</asp:Label>
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat = "server" ID = "txtCurrentMN" TextMode = "MultiLine" Font-Size = "small" Width = "400" Rows="3"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <br />
    
    </asp:Panel></asp:TableCell><asp:TableCell VerticalAlign="Bottom"><asp:Button ID = "TrialButton" visible = "false" runat = "server" Text = "Update" Height = "40" Width = "200" Font-Bold = "true" Font-Size = "small" /></asp:TableCell></asp:TableRow>
           
    </asp:Table>
    <br /><%--<div style = "clear:both;">--%>
    <br /><asp:Panel runat = "server" HorizontalAlign = "Center"> 
    <asp:Button runat = "server" ID="UpdateButton" Text="Update"
                Height="40" Width="200" Font-Bold="True" 
                Font-Size="small"/>  <br /><%-- </div>--%></asp:Panel>
    </asp:Panel>
    <asp:Label runat = "server" ForeColor = "White"> &nbsp; </asp:Label>
    <asp:Label ID="FakeButton" runat = "server" />
   <asp:Panel ID="Panel1" runat="server" Width="233px" CssClass = "modalBackground2" >
    

   <br />
   <asp:label ID = "explantionlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="OkButton" runat="server" Font-Size = "small" Text="OK"/>

      
      <br />
      <br />
      
   
   </asp:Panel>
   <br /> 
<%--     </div>--%>


  <%-- <div style="float: left; clear: both;"> --%>
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender1" 
             runat="server" 
             TargetControlID="FakeButton"
             PopupControlID="Panel1"
             DropShadow="true"/>
             <asp:Panel runat = "server" Height = "2" BackColor = "White"></asp:Panel>
    <%--<asp:Panel runat = "server" ID = "redpanel" Height = "5" BackColor = "Red" visible = "false"></asp:Panel>--%>

<asp:Panel runat = "server" ScrollBars = "Auto"   BorderStyle="Solid" 
           BorderWidth="10px" BorderColor="#FF3300" ID="redpanel" Visible = "false" > 
    
    <%-- <asp:Label runat = "server" ID = "TestingGrid2" ></asp:Label>--%>
   
     <asp:GridView ID="GridView2" runat="server"  HeaderStyle-BackColor = "#4A8fd2" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "true" BackColor ="#CBE3FB"
            AlternatingRowStyle-BackColor = "white" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" AllowPaging="False" 
        HorizontalAlign="Left" Font-Size="small" Width = "100%" >
            <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
            </Columns>
            
    </asp:GridView>
    </asp:Panel><%--</div>--%>
    <%-- <asp:Label runat = "server" ID = "labeltesting2"> </asp:Label>--%>
    </ContentTemplate>
    </asp:UpdatePanel>
    </ContentTemplate>
  </cc1:TabPanel>
                     <cc1:TabPanel runat = "server" HeaderText = "TIN Updates" ID = "tabProFeeTins">
                    <ContentTemplate>    
                    <asp:UpdatePanel runat = "server" ID = "udpmain2">
                    <ContentTemplate>
                             <asp:SqlDataSource ID="dsTINList" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
    SelectCommand="SELECT Distinct TIN
  FROM [DWH].[ProFee].[FeeSchedule]
  --where EndDate = '9999-12-31'
  order by TIN asc"
    ></asp:SqlDataSource>  

                           <asp:Panel runat = "server" HorizontalAlign = "Right"> <asp:Button ID="lbOpenProFeeInst2" runat="server" Text="Open Instructions"  OnClientClick="open_win2()"  /> </asp:Panel>
                        <asp:SqlDataSource ID= "dsPossiblePayorp2" runat = "server"
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="
            declare @Value varchar(max)
            set @Value = '  -- None --  '

            Select Distinct PayorName from (select @Value as PayorName from DWH.ProFee.FeeSchedule ) s

            Union

            select Distinct PayorName from DWH.ProFee.FeeSchedule
            where TIN = @TIN or @TIN = 'novalueselected'
            order by PayorName asc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlSelectedTINp2" Name="TIN" 
                            PropertyName="SelectedValue" />                  
                             </SelectParameters>
        </asp:SqlDataSource>
    <asp:Table ID="Table101" runat = "server"> <asp:TableRow> <asp:TableHeaderCell Font-Bold = "true"  ForeColor = "#4A8fd2" HorizontalAlign="Right">
    <asp:Label ID="Label102" runat = "server" Font-Size = "small">
    Select TIN:&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableHeaderCell><asp:TableCell HorizontalAlign = "Left" Font-Size = "small"> 
        <asp:DropDownList ID="ddlSelectedTINp2" runat="server" DataSourceID="dsTINList" 
            DataTextField="TIN" DataValueField="TIN" AutoPostBack = "true"
            AppendDataBoundItems="True" Font-Size = "small">
            <asp:listitem value="novalueselected">-- Select Value --</asp:listitem>
                    </asp:DropDownList>
    </asp:TableCell></asp:TableRow><asp:TableRow></asp:TableRow><asp:TableRow></asp:TableRow>

    <asp:TableRow><asp:TableHeaderCell Font-Bold = "true" ForeColor = "#4A8fd2" HorizontalAlign="Right">

 
    <asp:Label ID="Label103" runat = "server" Font-Size = "small">
    Select Payor:&nbsp;&nbsp;&nbsp;<br />(Optional)&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableHeaderCell><asp:TableCell HorizontalAlign = "Left" Font-Size = "small"> 
    <asp:DropDownList runat = "server" ID = "ddlSelectedPayorp2" DataSourceID = "dsPossiblePayorp2" Font-Size = "small" DataTextField = "PayorName" DataValueField = "PayorName" AutoPostBack = "true" AppendDataBoundItems="False" Enabled="False">
    </asp:DropDownList>
    </asp:TableCell></asp:TableRow></asp:Table>
    <br /><br />
            <asp:Panel runat = "server" ID = "ScrollPanelp2" ScrollBars = "Vertical" Width="100%" >
     <asp:GridView ID="GridView1p2" runat="server"  HeaderStyle-BackColor = "#4A8fd2" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "true" BackColor="#CBE3FB"
            AlternatingRowStyle-BackColor = "white" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" AllowPaging="False" 
                HorizontalAlign="Left" Font-Size="small" >
            <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
            </Columns>
            <%-- DataSourceID = "dsGridView"--%>
    </asp:GridView></asp:Panel><br />
    
    <asp:Panel runat = "server" ID = "p2PanelCurrentFS" Visible = "False"><br />
        <asp:Table ID="p2Table2" runat = "server" CssClass="collapsetable" CellSpacing="0" CellPadding="0">
            <asp:TableRow>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="p2Label8" runat = "server"  BackColor = "#CBE3FB" ForeColor = "#4A8fd2" Font-Size = "small" Font-Bold = "true" Width="150" >TIN:&nbsp;&nbsp;</asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Font-Size = "small" >
                    &nbsp;&nbsp;<asp:Label runat = "server" ID = "p2lblPreviousTIN"></asp:Label>
                    <asp:Label runat = "server" visible = "False" ID = "p2fakelabelTIN"></asp:Label>
                    <asp:Label runat = "server" visible = "False" ID = "p2fakelableLoadDate"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="p2Label4" runat = "server" Font-Size = "small" BackColor = "#CBE3FB" ForeColor = "#4A8fd2" Font-Bold = "true" Width="150" > Payor:&nbsp;&nbsp;</asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Font-Size = "small">
                    &nbsp;&nbsp;<asp:Label runat = "server" ID = "p2lblPayorName" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="p2Label7" runat = "server" BackColor = "#CBE3FB" ForeColor = "#4A8fd2" font-size = "small" Font-Bold = "true" Width="150" >Fee Schedule:&nbsp;&nbsp;</asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Font-Size = "small">
                    &nbsp;&nbsp;<asp:Label runat = "server" ID = "p2lblFSName"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table><br />
        <asp:Panel runat = "server" ID = "p2Organization" BackColor="#CBE3FB" BorderColor="#003060" BorderStyle="Outset" BorderWidth="2" Font-Bold="True" width = "62%">
            <asp:Table runat = "server" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
                <asp:TableRow>
                    <asp:TableCell Width="5px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" Font-Size = "small">
                        <asp:Label runat = "server"  BackColor = "#4A8fd2" ForeColor = "White" Font-Bold = "true" Width="150" >Dates:&nbsp;&nbsp;</asp:Label>
                    </asp:TableCell> 
                    <asp:TableCell>
                        <asp:Panel runat = "server" Font-Size = "small"> 
                            <asp:TextBox runat = "server" ID = "p2txtCurrentED" font-size = "small"></asp:TextBox>
                            <asp:Label runat = "server" Width = "75">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                            <asp:TextBox runat = "server" ID = "p2txtCurrentEndD" Font-Size = "small" ></asp:TextBox>
                                <cc1:CalendarExtender runat="server" TargetControlID="p2txtCurrentED">
                                </cc1:CalendarExtender>
                                <cc1:CalendarExtender runat ="server" TargetControlID = "p2txtCurrentEndD" ></cc1:CalendarExtender>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" Font-Size = "small">
                        <asp:Label ID="p2Label5" Height="56px" runat = "server" BackColor = "#4A8fd2" ForeColor = "White" Font-Bold = "true" Width="150" > Default:&nbsp;&nbsp;</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat = "server" ID = "p2txtCurrentDefault" TextMode="MultiLine" Width="400" Rows="3" Font-Size = "small"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell  HorizontalAlign="Right">
                        <asp:Label ID="p2Label6" runat = "server" Height="53px" BackColor = "#4A8fd2" ForeColor = "White" Font-Bold = "true" Font-Size = "small" Width="150"> Manager's Notes:&nbsp;&nbsp;</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat = "server" ID = "p2txtCurrentMN" TextMode = "MultiLine" Font-Size = "small" Width = "400" Rows="3"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table><br />
        </asp:Panel><br />
        <asp:panel runat = "server" horizontalalign = "center">
            <asp:Button ID = "p2UpdateButton" runat = "server" Text = "Update" Height = "40" Width = "200" Font-Bold = "true" Font-Size = "small" /><%--</asp:TableCell></asp:TableRow>--%>
           </asp:panel>
    </asp:Panel>

        <asp:Label ID="FakeButton2" runat = "server" />
   <asp:Panel ID="Panel1p2" runat="server" Width="233px" CssClass = "modalBackground2" >
    

   <br />
      <br /> <br />
      <asp:Button ID="OkButton2" runat="server" Font-Size = "small" Text="OK"/>

      
      <br />
      <br />
      
   
   </asp:Panel>
   <br /> 
<%--     </div>--%>


  <%-- <div style="float: left; clear: both;"> --%>
   
   <cc1:ModalPopupExtender ID="ModalPopupExtender2" 
             runat="server" 
             TargetControlID="FakeButton2"
             PopupControlID="Panel1p2"
             DropShadow="true"/>
    </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
  </cc1:TabPanel>
                     <cc1:TabPanel runat = "server" HeaderText = "Galen Contract ID LU" ID = "GalenLU">
                    <ContentTemplate>    
                    <asp:UpdatePanel runat = "server" ID = "udpgalen">
                    <ContentTemplate>
                       
       <asp:SqlDataSource ID="dsINSPLANList" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
    SelectCommand="SELECT DISTINCT [INSPLAN_NAME]
  FROM [DWH].[dbo].[ContractID_Galen_LU]
  where STARINSPLANCODE is null
  order by 1
  "
    ></asp:SqlDataSource> 
                             

                         <asp:Panel runat = "server" HorizontalAlign = "Right"> <asp:Button ID="lbOpenProFeeInst3" runat="server" Text="Open Instructions"  OnClientClick="open_win3()"  /> </asp:Panel>
      <br />
      <asp:Label ID="galenLabel" runat = "server" Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2" >
    Select INSPLAN_NAME:&nbsp;&nbsp;&nbsp;</asp:Label>
        <asp:DropDownList ID="ddlSelectedINSPLAN" runat="server" DataSourceID="dsINSPLANList" 
            DataTextField="INSPLAN_NAME" DataValueField="INSPLAN_NAME" AutoPostBack = "true"
            AppendDataBoundItems="True" Font-Size = "small">
            <asp:listitem value="novalueselected">-- (optional) --</asp:listitem>
                    </asp:DropDownList><br /><br />
      <asp:Panel runat = "server" ID = "ScrollPanelp3" BorderColor = "#003060" BorderWidth = "1px" width = "100%" ScrollBars = "Vertical"  height = "300px">
     <asp:GridView ID="gvGalenBlanks" runat="server"  HeaderStyle-BackColor = "#4A8fd2" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" 
            AlternatingRowStyle-BackColor = "white" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" BackColor="#CBE3FB" 
                HorizontalAlign="Left" Font-Size="Smaller" >
            <AlternatingRowStyle BackColor="white" />
            <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />

                <asp:TemplateField HeaderText="Update Row">
                   <HeaderTemplate>

         <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxesSpecific(this);" runat="server" />

     </HeaderTemplate>

                <ItemTemplate>
                  <asp:CheckBox ID="chk" runat="server" Text="" />
                </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            
            <HeaderStyle BackColor="#4A8fd2" Font-Bold="False" ForeColor="White" />
            <RowStyle BorderColor="#003060" HorizontalAlign="Center" />
            
    </asp:GridView>
    </asp:Panel><br /><br />&nbsp;&nbsp;&nbsp;<asp:TextBox runat = "server" ID = "txtgalenchosenStarInsplanCode"> </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:label ID = "explantionlabel2" runat = "server"></asp:label> <asp:label ID = "testinglabelgalen" runat = "Server"></asp:label>
       <asp:Button runat = "server" ID="btnGalenUpdate" Text="Update"
                Height="40" Width="200" Font-Bold="True" 
                Font-Size="small"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     <asp:Button runat = "server" ID="btnGalenUpdateAll" Text="Update All"
                Height="40" Width="200" Font-Bold="True" 
                Font-Size="small"/>
    <br /><br />
    <asp:Panel runat = "server" ID = "ScrollPanel2p3" ScrollBars = "Vertical" Width="100%" height = "200px">
         <asp:GridView ID="gvGalenChosen" runat="server"  HeaderStyle-BackColor = "#4A8fd2" 
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "true" 
            AlternatingRowStyle-BackColor = "white" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" AllowPaging="False" BackColor="#CBE3FB" 
        HorizontalAlign="Left" Font-Size="smaller" Width = "100%" >
            <Columns>
            <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
            </Columns>
            
    </asp:GridView>
    </asp:Panel> 

        <asp:Label ID="GalenFakeButton" runat = "server" />
   <asp:Panel ID="GalenPanel1" runat="server" Width="233px" CssClass = "modalBackground2" >
    

   <br />
   <asp:label ID = "Galenexplantionlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="GalenOkButton" runat="server" Font-Size = "small" Text="OK"/>

      
      <br />
      <br />
      
   
   </asp:Panel>
   <br /> 
   
   <cc1:ModalPopupExtender ID="ModalPopupExtenderGalen" 
             runat="server" 
             TargetControlID="GalenFakeButton"
             PopupControlID="GalenPanel1"
             DropShadow="true"/>
                    </ContentTemplate>
    </asp:UpdatePanel>
                        
                    </ContentTemplate>

                    </cc1:TabPanel>

                       <cc1:TabPanel runat = "server" HeaderText = "Galen STARINSPLAN Update" ID = "tpGalenUpdates">
                    <ContentTemplate>    
                     <asp:UpdatePanel runat = "server" ID = "udpgalenUpdates">
                    <ContentTemplate>
                       
                                  <asp:SqlDataSource ID="dsINSPLANList2" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
    SelectCommand="
               declare @Value varchar(max)
            set @Value = ' -- (optional) -- '

            Select Distinct [INSPLAN_NAME] from (select @Value as [INSPLAN_NAME] from [DWH].[dbo].[ContractID_Galen_LU] ) s
    
    union
    
    SELECT DISTINCT [INSPLAN_NAME]
  FROM [DWH].[dbo].[ContractID_Galen_LU]
  --where IDGroup = isnull(@IDGroup, ' -- (optional) -- ') or isnull(@IDGroup, ' -- (optional) -- ') = ' -- (optional) -- '
  order by 1
  "
    >
<%--                      <SelectParameters>
                        <asp:ControlParameter ControlID="ddlGalenUpdatesIDGroup" Name="IDGroup" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>--%>
    </asp:SqlDataSource> 
                                      <asp:SqlDataSource ID="dsGUIDGroup" runat="server" ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
    SelectCommand="
               declare @Value varchar(max)
            set @Value = ' -- (optional) -- '

            Select Distinct IDGroup from (select @Value as IDGroup from [DWH].[dbo].[ContractID_Galen_LU] ) s
    
    union
    
    SELECT DISTINCT [IDGroup]
  FROM [DWH].[dbo].[ContractID_Galen_LU]
  where INSPLAN_NAME = @InsplanName or @InsplanName = ' -- (optional) -- '
  order by 1
  "
    >
                         <SelectParameters>
                        <asp:ControlParameter ControlID="ddlGalenUpdatesInpslanName" Name="InsplanName" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
    </asp:SqlDataSource>  

                         <asp:Panel runat = "server" HorizontalAlign = "Right"><asp:CheckBox runat="server" AutoPostBack="true" Text="Edit Insplans" ID="cbEditInsplans" />&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Button ID="lbOpenProFeeInst4" runat="server" Text="Open Instructions"  OnClientClick="open_win4()"  /> </asp:Panel>
      <br /><asp:Table runat = "server" ID = "tblGalenUpdatesSelectFormat"><asp:TableRow><asp:TableCell>
      <asp:Label ID="lblGUSelectInsName" runat = "server" Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2" >
    Select INSPLAN_NAME:&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableCell><asp:TableCell>
        <asp:DropDownList ID="ddlGalenUpdatesInpslanName" runat="server" DataSourceID="dsINSPLANList2" 
            DataTextField="INSPLAN_NAME" DataValueField="INSPLAN_NAME" AutoPostBack = "true"
            AppendDataBoundItems="false" Font-Size = "small">
                     </asp:DropDownList></asp:TableCell><asp:TableCell width = "100px"></asp:TableCell><asp:TableCell>
                    <asp:CheckBox runat="server" Text = "Active Insplans Only" AutoPostBack="true" ID = "cbGUActiveInsplans"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell RowSpan="2" Width="20px"></asp:TableCell>
                    <asp:TableCell RowSpan="2" VerticalAlign="Middle" HorizontalAlign="Center">
                        <asp:Button runat="server" Text="Export to Excel" ID="btnExportGalen" /><br />
                      
                    </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell> <asp:Label ID="lblGUSelectIDGroup" runat = "server" Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2" >
    Select IDGroup:&nbsp;&nbsp;&nbsp;</asp:Label></asp:TableCell><asp:TableCell>
        <asp:DropDownList ID="ddlGalenUpdatesIDGroup" runat="server" DataSourceID="dsGUIDGroup" 
            DataTextField="IDGroup" DataValueField="IDGroup" AutoPostBack = "true"
            AppendDataBoundItems="False" Font-Size = "small">
                    </asp:DropDownList></asp:TableCell><asp:TableCell></asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="50 rows per page" Value="50"> </asp:ListItem>
                    <asp:ListItem Text="100 rows per page" Value="100"></asp:ListItem>
                    <asp:ListItem Text="250 rows per page" Value="250"></asp:ListItem>
                     </asp:DropDownList> </asp:TableCell>
        </asp:TableRow>
        </asp:Table>  
        <br />
        <asp:Panel runat = "server" ID = "pnlScrollPanelGalenUpdates" scrollbars = "auto" height = "800px" width = "95%">
                                  <asp:GridView ID="gvGalenUpdates" runat="server" AutoGenerateColumns="False" 
                                      DataKeyNames="INSPLAN,IDGroup,INSPLAN_NAME,DC_FROM" 
                                      DataSourceID="dsGVGalenUpdates"
                                      HeaderStyle-BackColor = "#4A8fd2" BackColor="#CBE3FB"
            HeaderStyle-ForeColor = "White" HeaderStyle-Font-Bold = "false" 
            AlternatingRowStyle-BackColor = "White" RowStyle-BorderColor = "#003060" 
            RowStyle-HorizontalAlign = "Center" AllowPaging="True" AllowSorting="True" 
                HorizontalAlign="Left" Font-Size="X-small" PageSize="50"   
                                      >
                                      <Columns>
                                      <asp:CommandField ShowSelectButton="True" Visible="True" SelectText="" />
                                          <asp:BoundField DataField="INSPLAN" HeaderText="INSPLAN" ReadOnly="True" 
                                              SortExpression="INSPLAN"></asp:BoundField>
                                          <asp:BoundField DataField="IDGroup" HeaderText="ID Group" ReadOnly="True" 
                                              SortExpression="IDGroup"></asp:BoundField>
                                          <asp:BoundField DataField="INSPLAN_NAME" HeaderText="INSPLAN NAME" 
                                              ReadOnly="True" SortExpression="INSPLAN_NAME"></asp:BoundField>
                                          <asp:BoundField DataField="STARINSPLANCODE" HeaderText="STAR INSPLAN CODE" 
                                              SortExpression="STARINSPLANCODE"></asp:BoundField>
                                          <asp:BoundField DataField="CONTRACTID" HeaderText="CONTRACT ID" ReadOnly="True"
                                              SortExpression="CONTRACTID"></asp:BoundField>
                                          <asp:BoundField DataField="CONTRACT_NAME" HeaderText="CONTRACT NAME" ReadOnly="True"
                                              SortExpression="CONTRACT_NAME"></asp:BoundField>
                                          <asp:BoundField DataField="CONTRACTID_SUM" HeaderText="CONTRACT ID SUM" ReadOnly="True"
                                              SortExpression="CONTRACTID_SUM"></asp:BoundField>
                                          <asp:BoundField DataField="CONTRACTID_SUM_NAME" ReadOnly="True"
                                              HeaderText="CONTRACT ID SUM NAME" SortExpression="CONTRACTID_SUM_NAME">
                                          </asp:BoundField>
                                          <asp:BoundField DataField="FromDate" HeaderText="DC FROM" ReadOnly="True" 
                                              SortExpression="DC_FROM"></asp:BoundField>
                                          <asp:BoundField DataField="ToDate" ReadOnly="True" HeaderText="DC TO" SortExpression="DC_TO">
                                          </asp:BoundField>
<%--                                          <asp:BoundField DataField="DC_FROM" HeaderText="DC_FROM" ReadOnly="True" 
                                              SortExpression="DC_FROM" visible = "false"></asp:BoundField>
                                          <asp:BoundField DataField="DC_TO" ReadOnly="True" HeaderText="DC_TO" SortExpression="DC_TO" visible = "false">
                                          </asp:BoundField> --%>

                                      </Columns>
                                  </asp:GridView></asp:Panel>
                                  <asp:SqlDataSource ID="dsGVGalenUpdates" runat="server" 
                                      ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="select *, convert(varchar(25),DC_FROM , 107) as FromDate, 
                                      convert(varchar(25),DC_TO , 107) as ToDate   From DWH.dbo.ContractID_Galen_LU  
where (INSPLAN_NAME = @InsplanName or @InsplanName = ' -- (optional) -- ')
and (IDGroup = @IDGroup or @IDGroup = ' -- (optional) -- ')
and (convert(date, getdate()) between DC_FROM and DC_TO or @checked = 0 or DC_TO is null)">
                                      <SelectParameters>
                                          <asp:ControlParameter ControlID="ddlGalenUpdatesInpslanName" 
                                              Name="InsplanName" PropertyName="SelectedValue" />
                                           <asp:ControlParameter ControlID="ddlGalenUpdatesIDGroup" Name="IDGroup" PropertyName="SelectedValue" />
                                         <asp:ControlParameter ControlID="cbGUActiveInsplans" Name="checked" PropertyName="Checked" />
                                      </SelectParameters>
                                  </asp:SqlDataSource>
                                  <br /><br />
                                  <asp:Table ID="tblGalenEdits" Visible ="false" runat = "server">
                                  <asp:TableRow>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">Insplan</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesINSPLAN" ></asp:Label>
                                  </asp:TableCell><asp:TableCell width = "50px"></asp:TableCell>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">ContractID</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesContractID" ></asp:Label>
                                  </asp:TableCell>
                                  </asp:TableRow>
                                  <asp:TableRow>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">IDGroup</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesIDGroup" ></asp:Label>
                                  </asp:TableCell><asp:TableCell width = "50px"></asp:TableCell>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">ContractName</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesContractName" ></asp:Label>
                                  </asp:TableCell>
                                  </asp:TableRow>
                                  <asp:TableRow>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">INSPLAN_NAME</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesINSPLAN_NAME" ></asp:Label>
                                  </asp:TableCell><asp:TableCell width = "50px"></asp:TableCell>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">ContractID_SUM</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesContractIDSUM" ></asp:Label>
                                  </asp:TableCell>
                                  </asp:TableRow>
                                  <asp:TableRow>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">STARINSPLANCODE</asp:TableCell><asp:TableCell>
                                  <asp:Textbox runat = "server" ID = "txtGalenUpdatesSTARINSPLANCODE" ></asp:Textbox>
                                  </asp:TableCell><asp:TableCell width = "50px"></asp:TableCell>
                                  <asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">ContractID_SUM_NAME</asp:TableCell><asp:TableCell>
                                  <asp:Label runat = "server" ID = "lblGalenUpdatesContractIDSUMNAME" ></asp:Label>
                                  </asp:TableCell>
                                  </asp:TableRow>
                                  <asp:TableRow></asp:TableRow>
                                  <asp:TableRow><asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2">Admission Date between: </asp:TableCell>
                                  <asp:TableCell><asp:Label runat = "server" ID = "lblGalenUpdatesFromDate"></asp:Label> <asp:Label visible = "false" runat = "server" Id = "lblGalenUpdatesDC_FROM"></asp:Label>
                                  </asp:TableCell>
                                  <asp:TableCell width = "50px"></asp:TableCell><asp:TableCell Font-Bold = "true" Font-Size = "small" ForeColor = "#4A8fd2"> and </asp:TableCell>
                                  <asp:TableCell><asp:Label runat = "server" ID = "lblGalenUpdatesToDate"></asp:Label><asp:Label visible = "false" runat = "server" Id = "lblGalenUpdatesDC_TO"></asp:Label>
                                  </asp:TableCell>
                                  </asp:TableRow>
                                  </asp:Table>
                                  <br />
                                  <asp:Button runat = "server" Visible ="false" Text = "Update" ID = "btnGUUpdateCode" />

        <asp:Label ID="GalenUpdatesFakeButton" runat = "server" />
   <asp:Panel ID="GalenUpdatesPanel1" runat="server" Width="233px" CssClass = "modalBackground2" >
    

   <br />
   <asp:label ID = "GalenUpdatesexplantionlabel" runat = "server"></asp:label> 
   <br /> <br />
      <asp:Button ID="GalenUpdatesOkButton" runat="server" Font-Size = "small" Text="OK"/>

      
      <br />
      <br />
      
   
   </asp:Panel>
   <br /> 
   
   <cc1:ModalPopupExtender ID="ModalPopupExtenderGalenUpdates" 
             runat="server" 
             TargetControlID="GalenUpdatesFakeButton"
             PopupControlID="GalenUpdatesPanel1"
             DropShadow="true"/>
                    </ContentTemplate>
                          <Triggers>

          <asp:PostBackTrigger ControlID ="btnExportGalen" /> 
          
 </Triggers>
    </asp:UpdatePanel>
                        
                    </ContentTemplate>

                    </cc1:TabPanel>


<%--                    <cc1:TabPanel runat = "server" HeaderText = "Temporary" ID = "Temporary">
                    <ContentTemplate>
                    <br />
                    <asp:Button ID = "btnDeleteAetna" runat = "server" Text = "Delete Aetna Fee Schedules" />
                    </ContentTemplate>
                    </cc1:TabPanel>--%>

  </cc1:tabcontainer>
</div>
</asp:Content>

