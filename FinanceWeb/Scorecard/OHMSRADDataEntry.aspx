<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OHMSRADDataEntry.aspx.vb" Inherits="FinanceWeb.OHMSRADDataEntry" %>
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

.MxPanelHeight {
    max-height:600px;
}
 
</style>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>


                        <cc1:tabcontainer ID="OhmsAdminTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "false" >
                    <cc1:TabPanel runat = "server" HeaderText = "X-Ray Imaging Review" ID = "tpXRData">
                    <ContentTemplate>     
                          
                               
                        <asp:UpdatePanel runat="server" ID= "updMain" ><ContentTemplate>
         <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
             <asp:Table runat="server" BackColor="#CBE3FB"  Width="100%">
                 <asp:TableRow>
                     <asp:TableCell Height="5px"></asp:TableCell>
                 </asp:TableRow>
                 <asp:TableHeaderRow ID="LocatioinRow" Visible="true">
                     <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell Width="200px" ForeColor="Black" >
                         Select Location:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID ="ddlLocationSelect" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                     </asp:TableCell><asp:TableCell ></asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow>
                     <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell ForeColor="Black" >
                         Fiscal Year & Quarter:
                     </asp:TableHeaderCell>
                     <asp:TableCell>
                     </asp:TableCell>
                     <asp:TableCell>
                          <asp:DropDownList runat="server" ID ="ddlXIVFYQ" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableCell Height="10px"></asp:TableCell>
                 </asp:TableRow>
             </asp:Table>
             <br />
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
<asp:GridView ID="gvXRayImagingViews" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "ID"  
         CellPadding="4" BorderColor="Black"  BackColor="#CBE3FB"  
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" AllowSorting ="True" AllowPaging="true" PageSize="25"  
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small"  >
                    <AlternatingRowStyle BackColor="White"  />
    <Columns>

           <asp:TemplateField HeaderText="ID" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblXIVID" runat="server"   Text='<%# Eval("ID")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
        <asp:TemplateField HeaderText="" SortExpression="RN">
            <ItemTemplate>
            <asp:Label runat ="server" Text='<%# Eval("RN")%>' ></asp:Label>
                </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Check In Number" ItemStyle-Width = "100" SortExpression="CheckInNumber" >
                <ItemTemplate>
                    <asp:Label ID="lblXIVCheckInNumber" runat="server" Text='<%# Eval("CheckInNumber")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtXIVCheckInNumber" runat="server" Text='<%# Eval("CheckInNumber")%>'></asp:TextBox>
                </ItemTemplate>
              </asp:TemplateField>
                   <asp:TemplateField HeaderText="Exam Type" ItemStyle-Width = "50" SortExpression="Exam">
                <ItemTemplate>
                    <asp:Label ID="lblXIVExam" runat="server" Text='<%# Eval("Exam")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlXIVExam" AutoPostBack="true" OnSelectedIndexChanged="PopO" 
                              SelectedValue ='<%# Eval("Exam")%>'  >
                             <asp:ListItem Text="Select Exam" Value="Select Exam"></asp:ListItem>
                             <asp:ListItem Text="Chest" Value="Chest"></asp:ListItem>
                             <asp:ListItem Text="Abdomen" Value="Abdomen"></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                  <asp:TemplateField HeaderText="Technique" ItemStyle-Width = "75" SortExpression="Technique">
                <ItemTemplate>
                    <asp:Label ID="lblXIVTechnique" runat="server" Text='<%# Eval("Technique")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlXIVTechnique" SelectedValue ='<%# Eval("Technique")%>'>
                             <asp:ListItem Text="Good Detail" Value="Good Detail"></asp:ListItem>
                             <asp:ListItem Text="Underpenetrated" Value="Underpenetrated"></asp:ListItem>
                             <asp:ListItem Text="Over penetrated" Value="Over penetrated"></asp:ListItem>
                             <asp:ListItem Text="Artifacts" Value="Good Detail"></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                          <asp:TemplateField HeaderText="Positioning" ItemStyle-Width = "150" SortExpression="Positioning">
                <ItemTemplate>
                    <asp:Label ID="lblXIVPositioning" runat="server" Text='<%# Eval("Positioning")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlXIVPositioning"  SelectedValue ='<%# Eval("Positioning")%>'>
                             <asp:ListItem Text="Good" Value="Good" ></asp:ListItem>
                              <asp:ListItem Text="Rotated" Value="Rotated"></asp:ListItem>
                              <asp:ListItem Text="Motion" Value="Motion"></asp:ListItem>
                              <asp:ListItem Text="Clipped Anatomy" Value="Clipped Anatomy"></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Markers" ItemStyle-Width = "75" SortExpression="Markers">
                <ItemTemplate>
                    <asp:Label ID="lblXIVMarkers" runat="server" Text='<%# Eval("Markers")%>' Visible="false"></asp:Label>
                             <asp:DropDownList runat="server" ID="ddlXIVMarkers" SelectedValue ='<%# Eval("Markers")%>'>
                             <asp:ListItem Text="Lead Markers" Value="Lead Markers" ></asp:ListItem>
                             <asp:ListItem Text="CR Markers" Value="CR Markers"></asp:ListItem>
                             <asp:ListItem Text="No Marker" Value="No Marker"></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                         <asp:TemplateField HeaderText="Observation Shielded" ItemStyle-Width = "75" SortExpression="Shielding Observed">
                <ItemTemplate>
                    <asp:Label ID="lblXIVShielding" runat="server" Text='<%# Eval("ObservationShielded")%>' Visible="false" ></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlXIVObservation" AppendDataBoundItems="false">
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                         <asp:TemplateField HeaderText="Coned" ItemStyle-Width = "75" SortExpression="Coned">
                <ItemTemplate>
                    <asp:Label ID="lblXIVConed" runat="server" Text='<%# Eval("Coned")%>' Visible="false"></asp:Label>
                          <asp:DropDownList runat="server" ID="ddlXIVConed" SelectedValue ='<%# Eval("Coned")%>'>
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                         </asp:DropDownList>
                </ItemTemplate>
              </asp:TemplateField>
                             <asp:TemplateField HeaderText="Comment" ItemStyle-Width = "75" SortExpression="Comment">
                <ItemTemplate>
                    <asp:Label ID="lblXIVComment" runat="server" Text='<%# Eval("Comment")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtXIVComment" runat="server" Text='<%# Eval("Comment")%>'></asp:TextBox>
                </ItemTemplate>

              </asp:TemplateField>
         
        </Columns>
       <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>
            
<%--             <asp:Table runat="server" Font-Size="Smaller">
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Exam
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>
                         Check In Number
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>
                         Technique
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>
                         Positioning
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>Markers</asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>Observation Shielded</asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableHeaderCell>Coned</asp:TableHeaderCell>
                 </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlExamType" >
                             <asp:ListItem Text="Select Exam" Value="Select Exam" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Chest" Value="Chest"></asp:ListItem>
                             <asp:ListItem Text="Abdomen" Value="Abdomen"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtCheckInNumber"></asp:TextBox>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlTechnique">
                             <asp:ListItem Text="Good Detail" Value="Good Detail" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="Underpenetrated" Value="Underpenetrated"></asp:ListItem>
                             <asp:ListItem Text="Over penetrated" Value="Over penetrated"></asp:ListItem>
                             <asp:ListItem Text="Artifacts" Value="Good Detail"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlPositioning">
                             <asp:ListItem Text="Good" Value="Good" Selected="True"></asp:ListItem>
                              <asp:ListItem Text="Rotated" Value="Rotated"></asp:ListItem>
                              <asp:ListItem Text="Motion" Value="Motion"></asp:ListItem>
                              <asp:ListItem Text="Clipped Anatomy" Value="Clipped Anatomy"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlMarkers">
                             <asp:ListItem Text="Lead Markers" Value="Lead Markers" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="CR Markers" Value="CR Markers"></asp:ListItem>
                             <asp:ListItem Text="No Marker" Value="No Marker"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlObservation" AppendDataBoundItems="false">
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlConed">
                             <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                             <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                     <asp:TableCell></asp:TableCell>
                 </asp:TableRow>
             </asp:Table>

             <br />
             <br />
             Additional Comments: <asp:TextBox runat="server" Width="400px" TextMode="MultiLine" Height="50px" ID="txtObservComments"></asp:TextBox> 
             <br />--%>
             <br />
             
             <asp:Button runat="server" ID="btnSubmitObservation" Text="Submit Exams" />
        </asp:Panel>          
             
         </asp:Panel>

                     <asp:Label ID="FakeButton2" runat = "server" />
   <asp:Panel ID="Panel2" runat="server" Width="300px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "explanationlabel" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="OkButton2" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server"
                 TargetControlID ="FakeButton2"
                 PopupControlID="Panel2"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


                            
</ContentTemplate>
</asp:UpdatePanel>




    
</ContentTemplate>
    





</cc1:TabPanel>
                   <cc1:TabPanel runat = "server" HeaderText = "Repeat Analysis" ID = "tpXRayMRI">
                    <ContentTemplate>     
            
     
                        
          
                        <asp:UpdatePanel runat="server" ID= "updatePanelMRIXRAY" ><ContentTemplate>
         <asp:Panel runat="server" ScrollBars="Auto">
             <asp:Table runat="server"  BackColor="#CBE3FB"  Width="100%">
                 <asp:TableRow>
                     <asp:TableCell Height="5px"></asp:TableCell>
                 </asp:TableRow>
                 <asp:TableHeaderRow Visible="true">
                     <asp:TableCell></asp:TableCell><asp:TableHeaderCell Width="200px" ForeColor="Black">
                         Select Location:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID ="ddlLocation2" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableHeaderRow Visible="true">
                     <asp:TableCell></asp:TableCell><asp:TableHeaderCell ForeColor="Black">
                         Select Modality:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID ="ddlModality" AutoPostBack ="true" >
                             <asp:ListItem Value ="CT" Text="CT"></asp:ListItem>
                            <asp:ListItem Value ="X-Ray" Text="X-Ray" Selected="True"></asp:ListItem>
                              <asp:ListItem Value ="MRI" Text="MRI"></asp:ListItem>
                         </asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow>
                     <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell ForeColor="Black" >
                         Fiscal Year & Quarter:
                     </asp:TableHeaderCell>
                     <asp:TableCell>
                     </asp:TableCell>
                     <asp:TableCell>
                          <asp:DropDownList runat="server" ID ="ddlRRFYQ" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                         <asp:LinkButton runat="server" ForeColor="Red" Font-Bold="true" ID="lbchangeDate2" Visible="false" ToolTip="Incorrect Date?  Click Here">*</asp:LinkButton>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow ID="HideIfMRI">
                     <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell ForeColor="Black" >
                         Week Start Date:
                     </asp:TableHeaderCell>
                     <asp:TableCell>
                     </asp:TableCell>
                     <asp:TableCell>
                          <asp:DropDownList runat="server" ID ="ddlRRWeekSelect" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                          <asp:LinkButton runat="server" ForeColor="Red" Font-Bold="true" ID="lbChangeDate" Visible="false" ToolTip="Incorrect Date?  Click Here">*</asp:LinkButton>
                         <asp:Label runat="server" ID="lblRRFakeWeek" Visible="false"></asp:Label>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
             </asp:Table>
             <br />

             <asp:GridView ID="gvRR" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "ID"  
         CellPadding="6" CellSpacing="5" BorderColor="Black"  BackColor="#CBE3FB"  
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" 
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small"  >
                    <AlternatingRowStyle BackColor="White"  />
    <Columns>

           <asp:TemplateField HeaderText="ID" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblRRID" runat="server"   Text='<%# Eval("ID")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
        <asp:TemplateField HeaderText="" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
            <asp:Label ID="lblRRRN" runat ="server" Text='<%# Eval("RN")%>' ></asp:Label>
                </ItemTemplate>
        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Reason" ItemStyle-Width = "125px"  >
                <ItemTemplate>
                    <asp:Label ID="lblRRReason" runat="server" Text='<%# Eval("Reason")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
             <asp:TemplateField HeaderText="Repeat Count" ItemStyle-Width = "75px" >
                <ItemTemplate>
                    <asp:Label ID="lblRRCount" runat="server" Text='<%# Eval("RepeatCount")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtRRCount" runat="server" Text='<%# Eval("RepeatCount")%>'></asp:TextBox>
                </ItemTemplate>

              </asp:TemplateField>
                             <asp:TemplateField HeaderText="Comment" ItemStyle-Width = "200px">
                <ItemTemplate>
                    <asp:Label ID="lblRRComment" runat="server" Text='<%# Eval("Comments")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtRRComment" runat="server" Text='<%# Eval("Comments")%>' Width="200px"></asp:TextBox>
                </ItemTemplate>

              </asp:TemplateField>
         
        </Columns>
       <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>

             <br />

                          <asp:GridView ID="gvRRTotalExposures" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "ID"  
         CellPadding="6" CellSpacing="5" BorderColor="Black"  BackColor="#CBE3FB"  
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" 
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small"  >
                    <AlternatingRowStyle BackColor="White"  />
    <Columns>

           <asp:TemplateField HeaderText="ID" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblRRID" runat="server"   Text='<%# Eval("ID")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
            <asp:Label ID="lblRRRN" runat ="server" Text="" Width="20px" ></asp:Label>
                </ItemTemplate>
        </asp:TemplateField>
                          <asp:TemplateField HeaderText="" ItemStyle-Width = "125px" >
                <ItemTemplate>
                    <asp:Label ID="lblRRReason" runat="server" Text='<%# Eval("Reason")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
             <asp:TemplateField HeaderText="" ItemStyle-Width = "75px" >
                <ItemTemplate>
                    <asp:Label ID="lblRRCount" runat="server" Text='<%# Eval("RepeatCount")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtRRCount" runat="server" Text='<%# Eval("RepeatCount")%>'></asp:TextBox>
                </ItemTemplate>

              </asp:TemplateField>
                             <asp:TemplateField HeaderText="Comment" ItemStyle-Width = "200px">
                <ItemTemplate>
                    <asp:Label ID="lblRRComment" runat="server" Text='<%# Eval("Comments")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtRRComment" runat="server" Text='<%# Eval("Comments")%>' Width="200px"></asp:TextBox>
                </ItemTemplate>

              </asp:TemplateField>
         
        </Columns>
       <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>

            <%-- <asp:Table runat="server">
                 <asp:TableRow>
                     <asp:TableHeaderCell ColumnSpan ="2">Repeat Reason</asp:TableHeaderCell>
                     <asp:TableHeaderCell ColumnSpan ="3">Count</asp:TableHeaderCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Artifact
                     </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtArtifact" ></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Clipped Anatomy
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtClipped"></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Equipment/Injector Failure
                     </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtEquipmentFailure"></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Motion
                     </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtMotion"></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow ID="rowXRayOnly" Visible="false">
                     <asp:TableHeaderCell>
                         Technique
                     </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtTechnique"></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Positioning
                     </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtPositioning"></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableHeaderCell>
                         Wrong Side/Wrong Exam
                     </asp:TableHeaderCell><asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox runat="server" ID="txtWrongSide"></asp:TextBox>
                     </asp:TableCell>
                 </asp:TableRow>
             </asp:Table>--%>

             <br />
             <br />

             <br />
             <asp:Button runat="server" ID="btnRepeatSubmission" Text="Submit Data" />

          
         </asp:Panel>

                                               <asp:Label ID="FakeButtonRR" runat = "server" />
   <asp:Panel ID="PanelRR" runat="server" Width="400px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "ExplanationLabelRR" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Table runat="server" VerticalAlign="Middle" HorizontalAlign="Center">
             <asp:TableRow>
                 <asp:TableCell ColumnSpan="5"><asp:Button ID="btnOKAYRR" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                 <asp:TableCell Width="5px"></asp:TableCell>
                 <asp:TableCell>
                     <asp:Button ID="btnRRConfirm" BorderStyle="Outset" BorderWidth="2px" Visible="false" runat="server" Font-Size = "small" Text="Confirm"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5px"></asp:TableCell>
                 <asp:TableCell>
         <asp:Button ID="btnCancel" BorderStyle="Outset" BorderWidth="2px" Visible="false" runat="server" Font-Size = "small" Text="Cancel"/>  
                 </asp:TableCell>
                 <asp:TableCell Width="5px"></asp:TableCell>
             </asp:TableRow>
         </asp:Table>                     

                   </asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtenderRR" runat="server"
                 TargetControlID ="FakeButtonRR"
                 PopupControlID="PanelRR"
                DropShadow="true"></cc1:ModalPopupExtender>

                                               <asp:Label ID="FakeCorrectDatePanel" runat = "server" />
   <asp:Panel ID="pnlModalPopupWrongDate" runat="server" Width="600px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "Label6" runat = "server">Did this entry have the wrong date?  You can change this within the first week of submission.<br />  Select correct date here:</asp:label><br />
               <asp:Label runat="server" Font-Size="X-Small"> Dates already attributed to another entry will not be displayed  </asp:Label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Table runat="server" VerticalAlign="Middle" HorizontalAlign="Center">
             <asp:TableRow>
                 <asp:TableCell ColumnSpan="5" HorizontalAlign="Center"> <asp:DropDownList runat="server" ID="ddlCorrectDateFYQ" AutoPostBack="true" ></asp:DropDownList>  </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                 <asp:TableCell ColumnSpan="5" HorizontalAlign="Center"> <asp:DropDownList runat="server" ID="ddlCorrectDateWeek"></asp:DropDownList> <asp:Label runat="server" ID="lblFakeCorrectWeek" Visible="false"></asp:Label>  </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                 <asp:TableCell Width="5px"></asp:TableCell>
                 <asp:TableCell>
                     <asp:Button ID="btnCorrectDateConfirm" BorderStyle="Outset" BorderWidth="2px" Visible="true" runat="server" Font-Size = "small" Text="Confirm"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5px"></asp:TableCell>
                 <asp:TableCell>
         <asp:Button ID="btnCorrectDateCancel" BorderStyle="Outset" BorderWidth="2px" Visible="true" runat="server" Font-Size = "small" Text="Cancel"/>  
                 </asp:TableCell>
                 <asp:TableCell Width="5px"></asp:TableCell>
             </asp:TableRow>
         </asp:Table>                     

                   </asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupWrongDate" runat="server"
                 TargetControlID ="FakeCorrectDatePanel"
                 PopupControlID="pnlModalPopupWrongDate"
                DropShadow="true"></cc1:ModalPopupExtender>



                          </ContentTemplate>
                            </asp:UpdatePanel>
    
</ContentTemplate>
    




</cc1:TabPanel>
                                 <cc1:TabPanel runat = "server" HeaderText = "Hand Hygiene" ID = "tpHandHygiene" Visible="false">
                    <ContentTemplate>     
                        
     
               
     
               
     
                        <asp:UpdatePanel runat="server" ID= "UpdatePanelHH" ><ContentTemplate>
         <asp:Panel ID="HHPanel3" runat="server" ScrollBars="Auto">
             <asp:Table runat="server" BackColor="#CBE3FB"  Width="100%">
                 <asp:TableRow>
                     <asp:TableCell Height="5px"></asp:TableCell>
                 </asp:TableRow>
                 <asp:TableHeaderRow Visible="true">
                     <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell Width="200px" ForeColor="Black">
                         Select Location:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID ="ddlHHLocationSelect" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow >
                     <asp:TableCell></asp:TableCell><asp:TableHeaderCell ForeColor="Black">
                         Select Data Entry Date:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID="ddlEntryDateSelect" AutoPostBack="true" AppendDataBoundItems="false"></asp:DropDownList>
                     </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                     <asp:TableCell Height="10px"></asp:TableCell>
                 </asp:TableRow>
             </asp:Table>

             <br />
             <br />
             <asp:Panel CssClass="MxPanelHeight" 
                  ID="ScrollPanelgvSubmitData" runat="server" ScrollBars="Auto">
<asp:GridView ID="gvSubmitData" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "dID"  
         CellPadding="4" BorderColor="Black"  BackColor="#CBE3FB"  
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" AllowSorting ="True" AllowPaging="true" PageSize="25"  
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small"  >
                    <AlternatingRowStyle BackColor="White"  />
    <Columns>

           <asp:TemplateField HeaderText="dID" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server"   Text='<%# Eval("dID")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
         <asp:TemplateField HeaderText="Metric Name" ItemStyle-Width = "100" SortExpression="Metric Name" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Metric Name")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                   <asp:TemplateField HeaderText="Entity" ItemStyle-Width = "50" SortExpression="Entity">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Entity")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                  <asp:TemplateField HeaderText="Location" ItemStyle-Width = "75" SortExpression="Location">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                          <asp:TemplateField HeaderText="Location Description" ItemStyle-Width = "150" SortExpression="LocationDesc">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("LocationDesc")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Target" ItemStyle-Width = "75" SortExpression="Target">
                <ItemTemplate>
                    <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("Target")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
           <asp:TemplateField HeaderText="Current Numerator" SortExpression ="CurrentNumerator">
                <ItemTemplate>
                    <asp:Label ID="lblCurrentNumerator" runat="server" Visible = "false" Text='<%# Eval("CurrentNumerator")%>'></asp:Label>
                     <asp:TextBox ID="txtCurrNum" AutoPostBack="true" runat="server" OnTextChanged="ChangeCurrentValue" Text='<%# Eval("CurrentNumerator")%>' Visible="true" ReadOnly ="false"></asp:TextBox>
                </ItemTemplate>
</asp:TemplateField>
           <asp:TemplateField HeaderText="Current Denominator" SortExpression="Current Denominator">
                <ItemTemplate>
                    <asp:Label ID="lblDept" runat="server" BorderStyle="Solid" visible="false"  Text='<%# Eval("Current Denominator")%>'></asp:Label>
                     <asp:TextBox ID="txtCurrDenom" AutoPostBack="true" runat="server" Enabled="false"  OnTextChanged="ChangeCurrentValue"  Text='<%# Eval("Current Denominator")%>' Visible="true" ReadOnly ="false"></asp:TextBox>
                   
                </ItemTemplate>


</asp:TemplateField>
                 <asp:TemplateField HeaderText="Current Value" SortExpression="Current Value" >
                <ItemTemplate> <asp:Label ID="lblCurrVal" runat="server" Text='<%# Eval("Current Value")%>'></asp:Label>
                    <%--<asp:Panel Width="100%" Height="100%" ID="pnlColor" runat="server">
                   
</asp:Panel>--%>
                </ItemTemplate>
                 </asp:TemplateField>
        <asp:BoundField DataField="color" HeaderText="color" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="color"></asp:BoundField>
                                 <asp:TemplateField HeaderText="Submission Date" ItemStyle-Width = "75" SortExpression="dDate">
                <ItemTemplate>
                    <asp:Label ID="lblDisplayDate" runat="server" Text='<%# Eval("DisplayDate")%>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                <asp:BoundField DataField="RedMax" HeaderText="RedMax" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="RedMax"></asp:BoundField>
                <asp:BoundField DataField="RedMin" HeaderText="RedMin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="RedMin"></asp:BoundField>
                <asp:BoundField DataField="wMax" HeaderText="wMax" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="wMax"></asp:BoundField>
                <asp:BoundField DataField="wMin" HeaderText="wMin" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="wMin"></asp:BoundField>
        <asp:BoundField DataField="DataType" HeaderText="DataType" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"
            SortExpression="DataType"></asp:BoundField>

        </Columns>
       <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>
             </asp:Panel>
  <br />
             <asp:Table runat="server" Width="100%">
                 <asp:TableRow>
                     <asp:TableCell HorizontalAlign="Center">
<asp:Button runat="server" Font-Size="Medium" ID="btnHHUpdateRows" Text="Update Rows" />
                     </asp:TableCell>
                 </asp:TableRow>
             </asp:Table>
           
             

             
         </asp:Panel>

                     <asp:Label ID="FakeButtonHH" runat = "server" />
   <asp:Panel ID="PanelHH" runat="server" Width="233px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "ExplanationLabelHH" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="ButtonHH3" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtenderHH" runat="server"
                 TargetControlID ="FakeButtonHH"
                 PopupControlID="PanelHH"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


       
             <asp:Label ID="FakeButtonHH2" runat = "server" />
   <asp:Panel ID="PanelHHPageChange" runat="server" Width="460px" BackColor="#6da9e3" >
       <asp:Table runat ="server" Width ="100%" Height ="100%" CssClass="collapsetable" CellPadding="0" CellSpacing="0">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "ExplanationLabelHHPC" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Table runat="server">
             <asp:TableRow>
                 <asp:TableCell>
          <asp:Button ID="SubmitButtonHHPageChange"  CssClass="Printbutton" Visible ="true" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Update Rows"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5"></asp:TableCell>
                 <asp:TableCell>         
              <asp:Button ID="btnChangePage"  CssClass="Printbutton" Visible ="true" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Change Page (Data Will be Lost)"/>
                 </asp:TableCell>
                 <asp:TableCell Width="5"></asp:TableCell>
                 <asp:TableCell>         
              <asp:Button ID="CancelButtonHHPageChange"  CssClass="Printbutton" Visible ="true" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="Cancel"/>
                 </asp:TableCell>
             </asp:TableRow>
         </asp:Table>
                   </asp:TableCell>
</asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
       <asp:Label runat="server" Visible ="False" ID="hiddenLblpass"></asp:Label>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="ModalPopupExtenderHH2" runat="server"
                 TargetControlID ="FakeButtonHH2"
                 PopupControlID="PanelHHPageChange"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>


                            </ContentTemplate>
                            </asp:UpdatePanel>
    
</ContentTemplate>
    



</cc1:TabPanel>
                            
<cc1:TabPanel runat = "server" HeaderText = "Radiology Bonfire" ID = "tpRadiologyBonfire" Visible="false">
                    <ContentTemplate>     
                      
                              
                        
                        <asp:UpdatePanel runat="server" ID= "upRadiologyBonfire" ><ContentTemplate>
         <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
             <asp:Table runat="server" BackColor="#CBE3FB"  Width="100%">
                 <asp:TableRow>
                     <asp:TableCell Height="5px"></asp:TableCell>
                 </asp:TableRow>
                 <asp:TableHeaderRow Visible="true">
                     <asp:TableCell Width="10px"></asp:TableCell><asp:TableHeaderCell Width="200px" ForeColor="Black" >
                         Select Location:
                     </asp:TableHeaderCell>
                     <asp:TableCell></asp:TableCell>
                     <asp:TableCell>
                         <asp:DropDownList runat="server" ID ="ddlBonfireLocation" AutoPostBack ="true" AppendDataBoundItems ="false"></asp:DropDownList>
                     </asp:TableCell><asp:TableCell ></asp:TableCell>
                 </asp:TableHeaderRow>
                 <asp:TableRow>
                     <asp:TableCell Height="10px"></asp:TableCell>
                 </asp:TableRow>
             </asp:Table>
             <br />
    <asp:Panel runat="server" Width="100%" HorizontalAlign="Center">
<asp:GridView ID="gvBonfire" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "dID"  
         CellPadding="4" BorderColor="Black"  BackColor="#CBE3FB"  
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" 
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small"  >
                    <AlternatingRowStyle BackColor="White"  />
    <Columns>

           <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblBonfireID" runat="server"   Text='<%# Eval("dID")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
         <asp:TemplateField HeaderText="   Customer Service Essentials Technique"  HeaderStyle-VerticalAlign="Middle"  ItemStyle-Width = "300" SortExpression="Name" >
                <ItemTemplate>
                    <asp:Label ID="lblBonfireName" runat="server" Text='<%# Eval("Name")%>' ></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Count of Employees who Completed the Bonfire" ItemStyle-Width = "100" SortExpression="Numerator" >
                <ItemTemplate>
                    <asp:Label ID="lblBonfireNumerator" runat="server" Text='<%# Eval("Numerator")%>' Visible="false" ></asp:Label>
                    <asp:TextBox ID="txtBonfireNumerator" runat="server" Text='<%# Eval("Numerator")%>' Enabled ="false"></asp:TextBox>
                </ItemTemplate>
              </asp:TemplateField>
                 <asp:TemplateField HeaderText="Count of Total Employees" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"  ItemStyle-Width = "100" SortExpression="Denominator" >
                <ItemTemplate>
                    <asp:Label ID="lblBonfireDenominator" runat="server" Text='<%# Eval("Denominator")%>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtBonfireDenominator" runat="server" Text='<%# Eval("Denominator")%>' Enabled ="false"></asp:TextBox>
                </ItemTemplate>
              </asp:TemplateField>
                             <asp:TemplateField HeaderText="EnableBox" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="lblEnableBox" runat="server"   Text='<%# Eval("EnableBox")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
         
        </Columns>
       <EditRowStyle BackColor="#2461BF" />
    </asp:GridView>

             <br />
             
             <asp:Button runat="server" ID="btnSubmitBonfire" Text="Update Bonfires" />
        </asp:Panel>          
             
         </asp:Panel>

                     <asp:Label ID="lblBonfireFakeBtn" runat = "server" />
   <asp:Panel ID="pnlBonfirePopup" runat="server" Width="300px" BackColor="#6da9e3"  BorderColor="#003060" BorderStyle="Solid" BorderWidth="1px"  >
       <asp:Table runat ="server" Width ="100%" Height ="100%">
           <asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
           <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign ="Middle" BackColor="#CBE3FB">
   <asp:label ID = "lblBonfireExceptions" runat = "server"></asp:label> 
 </asp:TableCell><asp:TableCell Width="10px"></asp:TableCell> </asp:TableRow><asp:TableRow><asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
     <asp:TableRow><asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center"> 
         <asp:Button ID="btnOkayBonfireModalPopup" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size = "small" Text="OK"/></asp:TableCell></asp:TableRow>        
     <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
       </asp:Table>
   </asp:Panel>
   <br /> 
             <cc1:ModalPopupExtender ID="mpeBonfirePopups" runat="server"
                 TargetControlID ="lblBonfireFakeBtn"
                 PopupControlID="pnlBonfirePopup"
                DropShadow="true"
                 ></cc1:ModalPopupExtender>

</ContentTemplate>
</asp:UpdatePanel>

    
</ContentTemplate>
    





</cc1:TabPanel>

                        </cc1:tabcontainer>
                        
    
    </asp:Content>