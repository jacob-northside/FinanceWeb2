<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FRED.aspx.vb" Inherits="FinanceWeb.FRED" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <style type="text/css">
.accordion {   
    width: 250px;   
}   

.accordionHeader {   
    border: 1px solid #2F4F4F;   
    color: white;   
    background-color: #2E4d7B;   
    font-family: Arial, Sans-Serif;   
    font-size: 12px;   
    font-weight: bold;   
    padding: 5px;   
    margin-top: 5px;   
    cursor: pointer;   
}   
           
.accordionHeaderSelected {   
    border: 1px solid #2F4F4F;   
    color: white;   
    background-color: #5078B3;   
    font-family: Arial, Sans-Serif;   
    font-size: 12px;   
    font-weight: bold;   
    padding: 5px;   
    margin-top: 5px;   
    cursor: pointer;   
}   
           
.accordionContent {   
    background-color: #D3DEEF;   
    border: 1px dashed #2F4F4F;   
    border-top: none;   
    padding: 0px;   
    padding-top: 10px;   
}   



</style>
 
</asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"  >
        <scripts>
              <asp:ScriptReference Path="~/Scripts/UpdatePanelScripts.js" />
        </scripts>
     
     </asp:ScriptManagerProxy>

    <div id="FullPage" style="height: auto; width: auto;">
  

 <div id="LeftPnl" style="float:left; width:20%; height: 100%;"  > 
<%-- <div id="LeftPnl" style="float:left; width:250px;  " > --%>
     <asp:Image ID="Image1" runat="server" ImageUrl="FREDFont2.png" 
         Height="46px" Width="246px" />

 <asp:UpdatePanel ID="pnlEncounterChk" runat="server" UpdateMode="Conditional" > 
<ContentTemplate>
 
  <asp:RadioButtonList ID="rblDataType" runat="server" Font-Size ="X-Small"  RepeatDirection="Horizontal" AutoPostBack="True">
    <asp:ListItem Text="Encounter Report" Value ="0"></asp:ListItem>
    <asp:ListItem Text="Transaction Report" Value="1"></asp:ListItem>
    </asp:RadioButtonList>     

   <asp:DropDownList ID="ddlFREDTables" runat="server" AutoPostBack="true">
    </asp:DropDownList>  
    
    <cc1:Accordion ID="acrFREDSelect" runat="server" SelectedIndex="0"   
       HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent"
    FadeTransitions="true"
    TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false"
    SuppressHeaderPostbacks="true"
    autoSize="Limit" Height ="500px" 
    style="width:250px" Font-Size="X-Small"> 
    <Panes>
    <cc1:AccordionPane ID="apFREDSelect1"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"  
            ContentCssClass="accordionContent" 
            runat="server">
            <Header> 
                <asp:Label ID="lblFREDSelect1" runat="server" Text=""></asp:Label> 
                     </Header>
            <Content>  
                   <asp:LinkButton ID="FREDSelectAll1" runat="server">Select All</asp:LinkButton> &nbsp;&nbsp;&nbsp; &nbsp; 
               <asp:LinkButton ID="FREDUnSelectAll1" runat="server">Unselect All</asp:LinkButton>
                 <asp:CheckBoxList ID="cblFREDSelect1" 
             runat="server" 
             Font-Size="X-Small">
             </asp:CheckBoxList>
            </Content>
        </cc1:AccordionPane>
    <cc1:AccordionPane ID="apFREDSelect2"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
            <Header>
                <asp:Label ID="lblFREDSelect2" runat="server" Text=""></asp:Label></Header>
            <Content>  
               <asp:LinkButton ID="FREDSelectAll2" runat="server">Select All</asp:LinkButton> &nbsp;&nbsp;&nbsp; &nbsp; 
               <asp:LinkButton ID="FREDUnSelectAll2" runat="server">Unselect All</asp:LinkButton>
                 <asp:CheckBoxList ID="cblFREDSelect2" 
             runat="server" 
             Font-Size="X-Small">
             </asp:CheckBoxList>
            </Content>
        </cc1:AccordionPane>
	<cc1:AccordionPane ID="apFREDSelect3"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
            <Header>
                <asp:Label ID="lblFREDSelect3" runat="server" Text=""></asp:Label></Header>
            <Content>  
               <asp:LinkButton ID="FREDSelectAll3" runat="server">Select All</asp:LinkButton> &nbsp;&nbsp;&nbsp; &nbsp; 
               <asp:LinkButton ID="FREDUnSelectAll3" runat="server">Unselect All</asp:LinkButton>
                 <asp:CheckBoxList ID="cblFREDSelect3" 
             runat="server" 
             Font-Size="X-Small">
             </asp:CheckBoxList>
            </Content>
        </cc1:AccordionPane>
      
</Panes>
 <HeaderTemplate>...</HeaderTemplate>
 <ContentTemplate>...</ContentTemplate>
     </cc1:Accordion>
     <asp:Label ID="lblCosting" runat="server" Text="" Visible = "false"  ></asp:Label>
     <asp:Label ID="lblFREDTable1" runat="server" Text="" Visible = "false" ></asp:Label>
     <asp:Label ID="lblFREDKey1" runat="server" Text="" Visible = "false" ></asp:Label>
     <asp:Label ID="lblFREDTable2" runat="server" Text=""  Visible = "false"></asp:Label>
     <asp:Label ID="lblFREDKey2" runat="server" Text=""  Visible = "false"></asp:Label>
     <asp:Label ID="lblFREDTable3" runat="server" Text=""  Visible = "false"></asp:Label>
     <asp:Label ID="lblFREDKey3" runat="server" Text=""  Visible = "false"></asp:Label>
</ContentTemplate>
<Triggers>
 <asp:AsyncPostBackTrigger controlID = "rblDataType" EventName ="SelectedIndexChanged" />
<%-- <asp:AsyncPostBackTrigger ControlID="ddlFREDTables" EventName="SelectedIndexChanged" /> --%>
</Triggers>
 </asp:UpdatePanel> 
    </div>

    <div id="MiddlePnl" 
            style="float: left; width:60%; height:auto;  padding-left: 5px; clear: none;">
    <%--    <div id="Div1" style="float: left; width:63%; padding-left: 10px;   ">--%>
     
    <asp:Panel ID="Panel2" runat="server" Width="100%" >

<div id="updateProgressDiv" style="display: none; height: 40px; width: 80px">
    <img src="dot-net-green.gif" /><br />
 <asp:Button ID="btnCancelReport" runat="server" Text="Cancel Data Preview" Font-Size ="X-Small" OnClientClick="Sys.WebForms.PageRequestManager.getInstance().abortPostBack();
                         alert('Data Preview Cancelled, Please wait.');" Visible="true"  />
        
</div>
    

 
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
    <ContentTemplate>
    
    </ContentTemplate>
    <Triggers> 
        
    </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="uPnlPreview" runat="server" UpdateMode="Always" >
<ContentTemplate>

      <asp:Button ID="btnRunReport" runat="server" Text="Preview Data" Visible = "True" Font-Size="X-Small" />
      <asp:Label ID="lblResults" runat="server" Text="Count: "></asp:Label> 
     
      <asp:Button ID="btnExportToExcel" runat="server" Text="Export" Visible = "True" Font-Size="X-Small"/>  &nbsp; 
      <asp:CheckBox ID="chbExcel2007" runat="server" Text ="Excel 2007 or later" Font-Size="X-Small"  /><br /> 




            <asp:Panel ID="PnlGV" runat="server"  ScrollBars="Auto" >

        <asp:GridView ID="gvDWHLookups" runat="server" AllowPaging="True"
            bordercolor="Black" 
        borderStyle="solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="50"   
          ForeColor="#000000" GridLines="Both"  BackColor="#ffffff" BorderWidth="1px" 
           Width="100%" height="100%" AllowSorting="false"  >
           <AlternatingRowStyle BackColor="#FFE885" /> 

 <PagerSettings Position="Bottom" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Size="X-Small" 
                Wrap="False" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" 
                Font-Size="Small" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <br /> 
 
                <asp:Label ID="lblSQLStatement" runat="server" Text="" Font-Size ="X-Small" Visible ="false"  ></asp:Label>
            </asp:Panel>
 </ContentTemplate> 
        
 <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btnRunReport" EventName ="Click" />  
<%--      <asp:AsyncPostBackTrigger ControlID="btnExportToExcel" EventName="Click" />  --%>
          <asp:PostBackTrigger ControlID ="btnExportToExcel" /> 
 </Triggers>
    </asp:UpdatePanel> 

  <asp:Panel ID="Panel4" runat="server" Font-Size ="X-Small" >
         </asp:Panel>  

  
 
<cc1:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="uPnlPreview">
                <Animations>
                    <OnUpdating>
                        <Parallel duration="0">
                           <%-- place the update progress div over the gridview control --%>
                            <ScriptAction Script="onUpdating();" />  
                            <%-- disable the search button --%>                       
                            <EnableAction AnimationTarget="btnRunReport" Enabled="false" />
                            <EnableAction AnimationTarget="btnExportToExcel" Enabled="false" />
                            <%-- fade-out the GridView --%>
                            <FadeOut minimumOpacity=".5" />  
                        </Parallel>
                    </OnUpdating>
                    <OnUpdated>
                        <Parallel duration="0">
                            <%-- fade back in the GridView --%>
                            <FadeIn minimumOpacity=".5" />
                            <%-- re-enable the search button --%>  
                            <EnableAction AnimationTarget="btnRunReport" Enabled="true" />
                            <EnableAction AnimationTarget="btnExportToExcel" Enabled="true" />
                            <%--find the update progress div and place it over the gridview control--%>
                            <ScriptAction Script="onUpdated();" /> 
                        </Parallel> 
                    </OnUpdated>
                   
                </Animations>
 </cc1:UpdatePanelAnimationExtender>

  





      
    </asp:Panel></div>
        
          <div id="RightPnl" style="float:right; width: 18%; height:auto;  padding-left: 5px; padding-right: 5px;">
  <%--  <div id="RightPnl" style="float:right; width: 250px; padding-left: 5px; padding-right: 5px;">--%>
    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Font-Size="Small" Height="100%"  >
    <asp:UpdatePanel ID="upReset" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
         <asp:Button ID="btnResetFilters" runat="server" Text="Reset Filters" Font-Size="X-Small"  /><br />
  
     <cc1:Accordion ID="acrFREDFilter" runat="server" SelectedIndex="-1"   
       HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent"
    AutoSize="None"
    FadeTransitions="true"
    TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false"
    SuppressHeaderPostbacks="true"
    style="width:250px" Font-Size="X-Small"> 
    <Panes>

      <cc1:AccordionPane ID="apFRED1"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
              
            <Header>
              <asp:UpdatePanel ID="upFREEFILTER1" runat="server" UpdateMode="Conditional" >
              <ContentTemplate>
                  <asp:Label ID="lblFREEFilter1" runat="server" Text=" Filter #1"></asp:Label>
              </ContentTemplate>
              <Triggers>
              </Triggers>
              </asp:UpdatePanel>
            </Header>
                <Content>  

              <asp:UpdatePanel ID="upFREDFilter1" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
           <%--Create list of available tables to limit on --%>
            <asp:RadioButtonList ID="rblFREDFilter1" runat="server" RepeatDirection="Vertical" Font-Size ="X-Small" AutoPostBack ="true"></asp:RadioButtonList> 
            <asp:DropDownList ID="ddlFREDFilter1" runat="server" AutoPostBack="true"  > </asp:DropDownList><br />
            <asp:UpdatePanel ID="up2FREDFilter1" runat="server" UpdateMode ="Conditional">
            <ContentTemplate>
               
            <asp:DropDownList ID="ddlFREDFilterOption1" runat="server" AutoPostBack="true"  > 
                <asp:ListItem>=</asp:ListItem>
                <asp:ListItem>&#8800;</asp:ListItem>
                <asp:ListItem><</asp:ListItem>
                <asp:ListItem><=</asp:ListItem>
                <asp:ListItem>></asp:ListItem>
                <asp:ListItem>>=</asp:ListItem>
                <asp:ListItem>like</asp:ListItem> 
                <asp:ListItem>contains</asp:ListItem>
                <asp:ListItem>not like</asp:ListItem>
                <asp:ListItem>does not contain</asp:ListItem>
                <asp:ListItem>between</asp:ListItem>
                <asp:ListItem>not between</asp:ListItem>
                <asp:ListItem>DATE between</asp:ListItem>
                <asp:ListItem>DATE not between</asp:ListItem>
                <asp:ListItem>NULL</asp:ListItem>
                <asp:ListItem>NOT NULL</asp:ListItem>
             </asp:DropDownList><br />
                <asp:Panel ID="pnltxtFREDFilter1" runat="server"  >
                        <asp:TextBox ID="txtFREDFilter1" runat="server" Font-Size="X-Small" AutoPostBack="false"></asp:TextBox><br />
                        <asp:Label ID="lblFREDFilter1" runat="server" Text="* for more than one value use the following syntax. " Font-Size="X-Small">
                                </asp:Label><br />
                        <asp:Label ID="lblFREDFilter1b" runat="server" Text="abc,def,xyz" Font-Size="X-Small"></asp:Label>
                    </asp:Panel>
                <asp:Panel ID="pnldtpFREDFilter1" runat="server">
                             <asp:TextBox ID="dtpFREDFilter1StartDate" runat="server" text="2013-07-01" Width="100px" />
                             <cc1:calendarextender ID="CalendarExtender11" 
                                runat="server" TargetControlID="dtpFREDFilter1StartDate"
                                format="yyyy-MM-dd"  
                                PopupButtonID="Image1" TodaysDateFormat="yyyy-MM-dd">
                              </cc1:calendarextender> 
                         &nbsp;    
                    <asp:Label ID="Label25" runat="server" ForeColor="Black" Text="Start Date"></asp:Label><br />
       
                    <asp:TextBox ID="dtpFREDFilter1EndDate" runat="server" text="2013-07-01" Width="100px"/>
                            <cc1:calendarextender ID="Calendarextender12" 
                              runat="server" TargetControlID="dtpFREDFilter1EndDate" 
                                PopupButtonID="Image1" Format="yyyy-MM-dd">
                          </cc1:calendarextender>
                          &nbsp; 
                <asp:Label ID="Label26" runat="server" ForeColor="Black" Text="   End Date"></asp:Label>  
                 </asp:Panel>
                <asp:Panel ID="pnlnumFREDFilter1" runat="server">
                            <asp:Label ID="Label27" runat="server" Text="between"></asp:Label><br />
                            <asp:TextBox ID="numFREDFilter1Start" runat="server" Font-Size="X-Small"></asp:TextBox>
                            <asp:Label ID="Label28" runat="server" Text="  and  "></asp:Label> 
                            <asp:TextBox ID="numFREDFilter1End" runat="server" Font-Size="X-Small"></asp:TextBox><br />
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlFREDFilterOption1" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
         
           </ContentTemplate>
          <Triggers > 
           <asp:AsyncPostBackTrigger ControlID="rblFREDFilter1" EventName ="SelectedIndexChanged" />  
          </Triggers>
            </asp:UpdatePanel>
            
                  
                <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
            </Content>




                  </cc1:AccordionPane>        

  			       <cc1:AccordionPane ID="apFRED2"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
            <Header>
             <asp:UpdatePanel ID="upFREEFILTER2" runat="server" UpdateMode="Conditional" >
              <ContentTemplate>
                  <asp:Label ID="lblFREEFilter2" runat="server" Text="Filter #2"></asp:Label>
              </ContentTemplate>
              <Triggers>
              </Triggers>
              </asp:UpdatePanel>
              </Header>
            <Content>  
            <asp:UpdatePanel ID="upFREDFilter2" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>

            <%--Create list of available tables to limit on --%>
            <asp:RadioButtonList ID="rblFREDFilter2" runat="server" RepeatDirection="Vertical" Font-Size ="X-Small" AutoPostBack ="true"></asp:RadioButtonList> 


            <asp:DropDownList ID="ddlFREDFilter2" runat="server" AutoPostBack="true"> </asp:DropDownList><br />
       
       <asp:UpdatePanel ID="up2FREDFilter2" runat="server" UpdateMode ="Conditional">
       <ContentTemplate>
            <asp:DropDownList ID="ddlFREDFilterOption2" runat="server" AutoPostBack="true"  > 
                <asp:ListItem>=</asp:ListItem>
                <asp:ListItem>&#8800;</asp:ListItem>
                <asp:ListItem><</asp:ListItem>
                <asp:ListItem><=</asp:ListItem>
                <asp:ListItem>></asp:ListItem>
                <asp:ListItem>>=</asp:ListItem>
                <asp:ListItem>like</asp:ListItem> 
                <asp:ListItem>contains</asp:ListItem>
                <asp:ListItem>not like</asp:ListItem>
                <asp:ListItem>does not contain</asp:ListItem>
                <asp:ListItem>between</asp:ListItem>
                <asp:ListItem>not between</asp:ListItem>
                <asp:ListItem>DATE between</asp:ListItem>
                <asp:ListItem>DATE not between</asp:ListItem>
                <asp:ListItem>NULL</asp:ListItem>
                <asp:ListItem>NOT NULL</asp:ListItem>
             </asp:DropDownList><br />



                <asp:Panel ID="pnltxtFREDFilter2" runat="server"  >
                        <asp:TextBox ID="txtFREDFilter2" runat="server" Font-Size="X-Small" AutoPostBack="false" ></asp:TextBox><br />
                        <asp:Label ID="lblFREDFilter2" runat="server" Text="* for more than one value use the following syntax. " Font-Size="X-Small"> 
                                </asp:Label><br />
                        <asp:Label ID="lblFREDFilter2b" runat="server" Text="abc,def,xyz" Font-Size="X-Small"></asp:Label>
                    </asp:Panel>
                <asp:Panel ID="pnldtpFREDFilter2" runat="server">
                             <asp:TextBox ID="dtpFREDFilter2StartDate" runat="server" text="2012-07-01" Width="100px" />
                             <cc1:calendarextender ID="CalendarExtender13" 
                                runat="server" TargetControlID="dtpFREDFilter2StartDate"
                                format="yyyy-MM-dd"  
                                PopupButtonID="Image1" TodaysDateFormat="yyyy-MM-dd">
                              </cc1:calendarextender> 
                         &nbsp;    
                    <asp:Label ID="Label32" runat="server" ForeColor="Black" Text="Start Date"></asp:Label><br />
       
                    <asp:TextBox ID="dtpFREDFilter2EndDate" runat="server" text="2012-07-01" Width="100px"/>
                            <cc1:calendarextender ID="Calendarextender14" 
                              runat="server" TargetControlID="dtpFREDFilter2EndDate" 
                                PopupButtonID="Image1" Format="yyyy-MM-dd">
                          </cc1:calendarextender>
                          &nbsp; 
                <asp:Label ID="Label33" runat="server" ForeColor="Black" Text="   End Date"></asp:Label>  
                 </asp:Panel>
                <asp:Panel ID="pnlnumFREDFilter2" runat="server">
                            <asp:Label ID="Label34" runat="server" Text="between"></asp:Label><br />
                            <asp:TextBox ID="numFREDFilter2Start" runat="server" Font-Size="X-Small"></asp:TextBox>
                            <asp:Label ID="Label35" runat="server" Text="  and  "></asp:Label> 
                            <asp:TextBox ID="numFREDFilter2End" runat="server" Font-Size="X-Small"></asp:TextBox><br />
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlFREDFilterOption2" EventName="SelectedIndexChanged" />
                    </Triggers>
     </asp:UpdatePanel>
         
                       </ContentTemplate>
            <Triggers > 
                 <asp:AsyncPostBackTrigger ControlID="rblFREDFilter2" EventName ="SelectedIndexChanged" />  
    
            </Triggers>
            </asp:UpdatePanel>
            
                  
                <asp:Label ID="Label36" runat="server" Text=""></asp:Label>
            </Content>
                  </cc1:AccordionPane>        
				   <cc1:AccordionPane ID="apFRED3"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
            <Header>  <asp:UpdatePanel ID="upFREEFILTER3" runat="server" UpdateMode="Conditional" >
              <ContentTemplate>
                  <asp:Label ID="lblFREEFilter3" runat="server" Text="Filter #3"></asp:Label>
              </ContentTemplate>
              <Triggers>
              </Triggers>
              </asp:UpdatePanel>
              </Header>
            <Content>  
            <asp:UpdatePanel ID="upFREDFilter3" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>

            <%--Create list of available tables to limit on --%>
            <asp:RadioButtonList ID="rblFREDFilter3" runat="server" RepeatDirection="Vertical" Font-Size ="X-Small" AutoPostBack ="true"></asp:RadioButtonList> 


            <asp:DropDownList ID="ddlFREDFilter3" runat="server" AutoPostBack="true"  > </asp:DropDownList><br />
       
       <asp:UpdatePanel ID="up2FREDFilter3" runat="server" UpdateMode ="Conditional">
       <ContentTemplate>
            <asp:DropDownList ID="ddlFREDFilterOption3" runat="server" AutoPostBack="true"  > 
                <asp:ListItem>=</asp:ListItem>
                <asp:ListItem>&#8800;</asp:ListItem>
                <asp:ListItem><</asp:ListItem>
                <asp:ListItem><=</asp:ListItem>
                <asp:ListItem>></asp:ListItem>
                <asp:ListItem>>=</asp:ListItem>
                <asp:ListItem>like</asp:ListItem> 
                <asp:ListItem>contains</asp:ListItem>
                <asp:ListItem>not like</asp:ListItem>
                <asp:ListItem>does not contain</asp:ListItem>
                <asp:ListItem>between</asp:ListItem>
                <asp:ListItem>not between</asp:ListItem>
                <asp:ListItem>DATE between</asp:ListItem>
                <asp:ListItem>DATE not between</asp:ListItem>
                <asp:ListItem>NULL</asp:ListItem>
                <asp:ListItem>NOT NULL</asp:ListItem>
             </asp:DropDownList><br />



                <asp:Panel ID="pnltxtFREDFilter3" runat="server">
                        <asp:TextBox ID="txtFREDFilter3" runat="server" Font-Size="X-Small" AutoPostBack="false"></asp:TextBox><br />
                        <asp:Label ID="lblFREDFilter3" runat="server" Text="* for more than one value use the following syntax. " Font-Size="X-Small"></asp:Label><br />
                        <asp:Label ID="lblFREDFilter3b" runat="server" Text="abc,def,xyz" Font-Size="X-Small"></asp:Label>
                    </asp:Panel>
                <asp:Panel ID="pnldtpFREDFilter3" runat="server">
                             <asp:TextBox ID="dtpFREDFilter3StartDate" runat="server" text="2013-07-01" Width="100px" />
                             <cc1:calendarextender ID="CalendarExtender15" 
                                runat="server" TargetControlID="dtpFREDFilter3StartDate"
                                format="yyyy-MM-dd"  
                                PopupButtonID="Image1" TodaysDateFormat="yyyy-MM-dd">
                              </cc1:calendarextender> 
                         &nbsp;    
                    <asp:Label ID="Label39" runat="server" ForeColor="Black" Text="Start Date"></asp:Label><br />
       
                    <asp:TextBox ID="dtpFREDFilter3EndDate" runat="server" text="2013-07-01" Width="100px"/>
                            <cc1:calendarextender ID="Calendarextender16" 
                              runat="server" TargetControlID="dtpFREDFilter3EndDate" 
                                PopupButtonID="Image1" Format="yyyy-MM-dd">
                          </cc1:calendarextender>
                          &nbsp; 
                <asp:Label ID="Label40" runat="server" ForeColor="Black" Text="   End Date"></asp:Label>  
                 </asp:Panel>
                <asp:Panel ID="pnlnumFREDFilter3" runat="server">
                            <asp:Label ID="Label41" runat="server" Text="between"></asp:Label><br />
                            <asp:TextBox ID="numFREDFilter3Start" runat="server" Font-Size="X-Small"></asp:TextBox>
                            <asp:Label ID="Label42" runat="server" Text="  and  "></asp:Label> 
                            <asp:TextBox ID="numFREDFilter3End" runat="server" Font-Size="X-Small"></asp:TextBox><br />
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlFREDFilterOption3" EventName="SelectedIndexChanged" />
                    </Triggers>
     </asp:UpdatePanel>
         
                       </ContentTemplate>
            <Triggers > 
                 <asp:AsyncPostBackTrigger ControlID="rblFREDFilter3" EventName ="SelectedIndexChanged" />  
    
            </Triggers>
            </asp:UpdatePanel>
            
                  
                <asp:Label ID="Label43" runat="server" Text=""></asp:Label>
            </Content>
                  </cc1:AccordionPane>        
				   <cc1:AccordionPane ID="apFRED4"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
            <Header>
            <asp:UpdatePanel ID="upFREEFILTER4" runat="server" UpdateMode="Conditional" >
              <ContentTemplate>
                  <asp:Label ID="lblFREEFilter4" runat="server" Text="Filter #4"></asp:Label>
              </ContentTemplate>
              <Triggers>
              </Triggers>
              </asp:UpdatePanel>
              </Header>
            <Content>  
            <asp:UpdatePanel ID="upFREDFilter4" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>

            <%--Create list of available tables to limit on --%>
            <asp:RadioButtonList ID="rblFREDFilter4" runat="server" RepeatDirection="Vertical" Font-Size ="X-Small" AutoPostBack ="true"></asp:RadioButtonList> 


            <asp:DropDownList ID="ddlFREDFilter4" runat="server" AutoPostBack="true"  > </asp:DropDownList><br />
       
       <asp:UpdatePanel ID="up2FREDFilter4" runat="server" UpdateMode ="Conditional">
       <ContentTemplate>
            <asp:DropDownList ID="ddlFREDFilterOption4" runat="server" AutoPostBack="true"  > 
                <asp:ListItem>=</asp:ListItem>
                <asp:ListItem>&#8800;</asp:ListItem>
                <asp:ListItem><</asp:ListItem>
                <asp:ListItem><=</asp:ListItem>
                <asp:ListItem>></asp:ListItem>
                <asp:ListItem>>=</asp:ListItem>
                <asp:ListItem>like</asp:ListItem> 
                <asp:ListItem>contains</asp:ListItem>
                <asp:ListItem>not like</asp:ListItem>
                <asp:ListItem>does not contain</asp:ListItem>
                <asp:ListItem>between</asp:ListItem>
                <asp:ListItem>not between</asp:ListItem>
                <asp:ListItem>DATE between</asp:ListItem>
                <asp:ListItem>DATE not between</asp:ListItem>
                <asp:ListItem>NULL</asp:ListItem>
                <asp:ListItem>NOT NULL</asp:ListItem>
             </asp:DropDownList><br />



                <asp:Panel ID="pnltxtFREDFilter4" runat="server">
                        <asp:TextBox ID="txtFREDFilter4" runat="server" Font-Size="X-Small" AutoPostBack="false"></asp:TextBox><br />
                        <asp:Label ID="lblFREDFilter4" runat="server" Text="* for more than one value use the following syntax. " Font-Size="X-Small"> </asp:Label><br />
                        <asp:Label ID="lblFREDFilter4b" runat="server" Text="abc,def,xyz" Font-Size="X-Small"></asp:Label>
                    </asp:Panel>
                <asp:Panel ID="pnldtpFREDFilter4" runat="server">
                             <asp:TextBox ID="dtpFREDFilter4StartDate" runat="server" text="2014-07-01" Width="100px" />
                             <cc1:calendarextender ID="CalendarExtender17" 
                                runat="server" TargetControlID="dtpFREDFilter4StartDate"
                                format="yyyy-MM-dd"  
                                PopupButtonID="Image1" TodaysDateFormat="yyyy-MM-dd">
                              </cc1:calendarextender> 
                         &nbsp;    
                    <asp:Label ID="Label46" runat="server" ForeColor="Black" Text="Start Date"></asp:Label><br />
       
                    <asp:TextBox ID="dtpFREDFilter4EndDate" runat="server" text="2014-07-01" Width="100px"/>
                            <cc1:calendarextender ID="Calendarextender18" 
                              runat="server" TargetControlID="dtpFREDFilter4EndDate" 
                                PopupButtonID="Image1" Format="yyyy-MM-dd">
                          </cc1:calendarextender>
                          &nbsp; 
                <asp:Label ID="Label47" runat="server" ForeColor="Black" Text="   End Date"></asp:Label>  
                 </asp:Panel>
                <asp:Panel ID="pnlnumFREDFilter4" runat="server">
                            <asp:Label ID="Label48" runat="server" Text="between"></asp:Label><br />
                            <asp:TextBox ID="numFREDFilter4Start" runat="server" Font-Size="X-Small"></asp:TextBox>
                            <asp:Label ID="Label49" runat="server" Text="  and  "></asp:Label> 
                            <asp:TextBox ID="numFREDFilter4End" runat="server" Font-Size="X-Small"></asp:TextBox><br />
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlFREDFilterOption4" EventName="SelectedIndexChanged" />
                    </Triggers>
     </asp:UpdatePanel>
         
                       </ContentTemplate>
            <Triggers > 
                 <asp:AsyncPostBackTrigger ControlID="rblFREDFilter4" EventName ="SelectedIndexChanged" />  
    
            </Triggers>
            </asp:UpdatePanel>
            
                  
                <asp:Label ID="Label50" runat="server" Text=""></asp:Label>
            </Content>
                  </cc1:AccordionPane>        
				   <cc1:AccordionPane ID="apFRED5"
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" 
            runat="server">
            <Header>
             <asp:UpdatePanel ID="upFREEFILTER5" runat="server" UpdateMode="Conditional" >
              <ContentTemplate>
                  <asp:Label ID="lblFREEFilter5" runat="server" Text="Filter #5"></asp:Label>
              </ContentTemplate>
              <Triggers>
              </Triggers>
              </asp:UpdatePanel>
              </Header>
            <Content>  
            <asp:UpdatePanel ID="upFREDFilter5" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>

            <%--Create list of available tables to limit on --%>
            <asp:RadioButtonList ID="rblFREDFilter5" runat="server" RepeatDirection="Vertical" Font-Size ="X-Small" AutoPostBack ="true"></asp:RadioButtonList> 


            <asp:DropDownList ID="ddlFREDFilter5" runat="server" AutoPostBack="true"  > </asp:DropDownList><br />
       
       <asp:UpdatePanel ID="up2FREDFilter5" runat="server" UpdateMode ="Conditional">
       <ContentTemplate>
            <asp:DropDownList ID="ddlFREDFilterOption5" runat="server" AutoPostBack="true"  > 
                <asp:ListItem>=</asp:ListItem>
                <asp:ListItem>&#8800;</asp:ListItem>
                <asp:ListItem><</asp:ListItem>
                <asp:ListItem><=</asp:ListItem>
                <asp:ListItem>></asp:ListItem>
                <asp:ListItem>>=</asp:ListItem>
                <asp:ListItem>like</asp:ListItem> 
                <asp:ListItem>contains</asp:ListItem>
                <asp:ListItem>not like</asp:ListItem>
                <asp:ListItem>does not contain</asp:ListItem>
                <asp:ListItem>between</asp:ListItem>
                <asp:ListItem>not between</asp:ListItem>
                <asp:ListItem>DATE between</asp:ListItem>
                <asp:ListItem>DATE not between</asp:ListItem>
                <asp:ListItem>NULL</asp:ListItem>
                <asp:ListItem>NOT NULL</asp:ListItem>
             </asp:DropDownList><br />



                <asp:Panel ID="pnltxtFREDFilter5" runat="server">
                        <asp:TextBox ID="txtFREDFilter5" runat="server" Font-Size="X-Small" AutoPostBack="false"></asp:TextBox><br />
                        <asp:Label ID="lblFREDFilter5" runat="server" Text="* for more than one value use the following syntax. " Font-Size="X-Small">
                                </asp:Label><br />
                        <asp:Label ID="lblFREDFilter5b" runat="server" Text="abc,def,xyz" Font-Size="X-Small"></asp:Label>
                    </asp:Panel>
                <asp:Panel ID="pnldtpFREDFilter5" runat="server">
                             <asp:TextBox ID="dtpFREDFilter5StartDate" runat="server" text="2015-07-01" Width="100px" />
                             <cc1:calendarextender ID="CalendarExtender19" 
                                runat="server" TargetControlID="dtpFREDFilter5StartDate"
                                format="yyyy-MM-dd"  
                                PopupButtonID="Image1" TodaysDateFormat="yyyy-MM-dd">
                              </cc1:calendarextender> 
                         &nbsp;    
                    <asp:Label ID="Label53" runat="server" ForeColor="Black" Text="Start Date"></asp:Label><br />
       
                    <asp:TextBox ID="dtpFREDFilter5EndDate" runat="server" text="2015-07-01" Width="100px"/>
                            <cc1:calendarextender ID="Calendarextender20" 
                              runat="server" TargetControlID="dtpFREDFilter5EndDate" 
                                PopupButtonID="Image1" Format="yyyy-MM-dd">
                          </cc1:calendarextender>
                          &nbsp; 
                <asp:Label ID="Label54" runat="server" ForeColor="Black" Text="   End Date"></asp:Label>  
                 </asp:Panel>
                <asp:Panel ID="pnlnumFREDFilter5" runat="server">
                            <asp:Label ID="Label55" runat="server" Text="between"></asp:Label><br />
                            <asp:TextBox ID="numFREDFilter5Start" runat="server" Font-Size="X-Small"></asp:TextBox>
                            <asp:Label ID="Label56" runat="server" Text="  and  "></asp:Label> 
                            <asp:TextBox ID="numFREDFilter5End" runat="server" Font-Size="X-Small"></asp:TextBox><br />
                    </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlFREDFilterOption5" EventName="SelectedIndexChanged" />
                    </Triggers>
     </asp:UpdatePanel>
         
                       </ContentTemplate>
            <Triggers > 
                 <asp:AsyncPostBackTrigger ControlID="rblFREDFilter5" EventName ="SelectedIndexChanged" />  
 
            </Triggers>
            </asp:UpdatePanel>
            
                  
                <asp:Label ID="Label57" runat="server" Text=""></asp:Label>
            </Content>
                  </cc1:AccordionPane>        
				  






    </Panes>
     </cc1:Accordion>
        </ContentTemplate>
    <Triggers> 
        <asp:AsyncPostBackTrigger ControlID="btnResetFilters" EventName="Click" /> 
     
     
    </Triggers>
    </asp:UpdatePanel>

      </asp:Panel> 
    </div>
</div>

</asp:Content>
