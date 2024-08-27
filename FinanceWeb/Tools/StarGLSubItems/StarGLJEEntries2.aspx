<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StarGLJEEntries2.aspx.vb" Inherits="FinanceWeb.StarGLJEEntries2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


     <div >
          
                  <br />
                  <asp:Panel ID="pnlGLCorrectiveEntries2" runat="server" Enabled="true" 
                      Visible="true">
                     <%-- <asp:Label ID="Label6" runat="server" ForeColor="Black" 
                          Text="HEFM Journal Entires (BEFORE EXCEL FILE)   "></asp:Label><br />--%>
  
                   
                       <div id="Corrections" runat="server" >
                                                    
                    <asp:Table ID="Table2" runat="server" CaptionAlign="Top" HorizontalAlign="Left">
                    <asp:TableRow ID="TableRow0" runat="server"  >
                        <asp:TableCell ID="TableCell19" runat="server">
                          <asp:Label ID="Label4" runat="server" Text="Check the following box(es) to generate the journal entries for each facility."></asp:Label><br />
                    </asp:TableCell> 
                        <asp:TableCell ID="TableCell20" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell21" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell1" runat="server"> <br />
                          <asp:CheckBox ID="chbAtlanta" runat="server" Font-Size="Small" Text="Atlanta" /></asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="TableCell4" runat="server">
                          <asp:CheckBox ID="chbCherokee" runat="server" Font-Size="Small" Text="Cherokee" /></asp:TableCell>
                        <asp:TableCell ID="TableCell5" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell6" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow3" runat="server">
                        <asp:TableCell ID="TableCell7" runat="server">
                           <asp:CheckBox ID="chbForsyth" runat="server" Font-Size="Small" Text="Forsyth" /></asp:TableCell>
                        <asp:TableCell ID="TableCell8" runat="server">    </asp:TableCell>
                        <asp:TableCell ID="TableCell9" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell ID="TableCell10" runat="server" HorizontalAlign="Left" VerticalAlign="Top">
                           <asp:Button ID="btnGenerateList" runat="server" Text="Generate list of files" />   <br />
                            <asp:Label ID="lblFiles" runat="server" Text="List of un-finailized entries. (Fac-Date)"></asp:Label>
                           <asp:DropDownList ID="ddlFiles" runat="server"></asp:DropDownList>     
                                   <asp:Button ID="btnGenerateFiles" runat="server" Text="Generate Preview(s)" /> 
                           </asp:TableCell>

                        <asp:TableCell ID="TableCell11" runat="server">
                      <div ID="ATL" runat="server" visible="False">
                          <asp:GridView ID="gvAtlanta" runat="server" AutoGenerateColumns="False" 
                              BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                              CellPadding="4" Font-Size="X-Small" ForeColor="Black" 
                              HeaderStyle-BackColor="#214B9A" HeaderStyle-ForeColor="#FFCBA5" 
                              HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true">
                <AlternatingRowStyle BackColor="#FFE885" />
                    <Columns><asp:BoundField DataField="Fac" HeaderText="Entity" ReadOnly="True" SortExpression="Fac" />
                             <asp:BoundField DataField="Dept" HeaderText="Department" ReadOnly="True" 
                                      SortExpression="Dept" />
                             <asp:BoundField DataField="SubAcct" HeaderText="Subaccount" ReadOnly="True" 
                                      SortExpression="SubAcct" />
                             <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                             <ControlStyle Width="1px" />
                             <FooterStyle Width="1px" />
                             <HeaderStyle Width="1px" />
                             <ItemStyle Width="1px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                             <ControlStyle Width="1px" />
                             <FooterStyle Width="1px" />                                  
                             <HeaderStyle Width="1px" />
                             <ItemStyle Width="1px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Description" HeaderText="Description" 
                                      ReadOnly="True" SortExpression="Description" />
                             <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                             <ControlStyle Width="1px" />
                             <FooterStyle Width="1px" />
                             <HeaderStyle Width="1px" />
                             <ItemStyle Width="1px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                             <ControlStyle Width="1px" />
                             <FooterStyle Width="1px" />
                             <HeaderStyle Width="1px" />
                             <ItemStyle Width="1px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Debit" DataFormatString="{0:c}" HeaderText="Debit" 
                                      ReadOnly="True" SortExpression="Debit" />
                             <asp:BoundField DataField="Credit" DataFormatString="{0:c}" HeaderText="Credit" 
                                      ReadOnly="True" SortExpression="Credit" />
                             <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                      SortExpression="ID" >
                             <ControlStyle Width="1px" />
                             <FooterStyle Width="1px" />
                             <HeaderStyle Width="1px" />
                             <ItemStyle Width="1px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="FY" HeaderText="FY" ReadOnly="True" 
                                      SortExpression="FY" Visible="true">
                             <ControlStyle Width="1px" />
                             <FooterStyle Width="1px" />
                             <HeaderStyle Width="1px" />
                             <ItemStyle Width="1px" />
                             </asp:BoundField>
                             </Columns>
                <PagerSettings Position="TopAndBottom" />
                <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <EditRowStyle BackColor="#2461BF" />
                <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left" />
                              <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
                              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                              <SortedAscendingCellStyle BackColor="#F5F7FB" />
                              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                              <SortedDescendingCellStyle BackColor="#E9EBEF" />
                              <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView><br />
                            <asp:Button ID="BtnGenerateAtlanta" runat="server" Text="Finalize Atlanta File and Export to Excel" Visible = "false"  />
                            <asp:Button ID="btnExportAtlanta" runat="server" Text="Finalize Atlanta File and Generate Excel File" Visible="false"  />    
                            <asp:Label ID="lblAtlMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                             <asp:HyperLink ID="hlDownload" runat="server" Text="" Visible ="false" Target="_blank"></asp:HyperLink><br />
                               <asp:RadioButtonList ID="rblAtlanta" runat="server" RepeatDirection="Horizontal" Visible = "false" Font-Size="Smaller">
                                    <asp:ListItem Value="xlsx">Excel 2007 or newer</asp:ListItem>                             
                                    <asp:ListItem Selected="True" Value="xls">Excel 97 - 2003</asp:ListItem>
                                 </asp:RadioButtonList>
                           </div>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell12" runat="server" HorizontalAlign="Left" VerticalAlign="Top"></asp:TableCell></asp:TableRow>
                        <asp:TableRow ID="TableRow5" runat="server">
                        <asp:TableCell ID="TableCell13" runat="server" HorizontalAlign="Left" VerticalAlign="Top">
                                               
                         <asp:LinkButton ID="lbAtlantaDownloads" runat="server" Text="View Past Atlanta Corrections"></asp:LinkButton>
                                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                              EmptyDataText = "No files uploaded" AllowSorting="True">    
                         <Columns>        
                    <asp:BoundField DataField="Text" HeaderText="Previous Atlanta Files" />       
                    <asp:TemplateField>       
                    <ItemTemplate>          
                    <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>     
                    </ItemTemplate>     
                    </asp:TemplateField>   
                    </Columns>
                     </asp:GridView>   
                   </asp:Panel>
                      <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" 
                          runat="server" Enabled="True" TargetControlID="Panel1" Collapsed="True" 
                          CollapsedSize="0"  AutoCollapse="False" ExpandControlID="lbAtlantaDownloads"
                            CollapseControlID="lbAtlantaDownloads" AutoExpand="False" ScrollContents="False" 
                            TextLabelID="lbAtlantaDownloads" CollapsedText="View Past Atlanta Corrections..."
                            ExpandedText="Hide Corrections...." ImageControlID="Image1">
                      </cc1:CollapsiblePanelExtender>
                        <br />
                          <asp:LinkButton ID="lbCherokeeDownloads" runat="server" Text="View Past Cherokee Corrections"></asp:LinkButton> 
                            <asp:Panel ID="Panel3" runat="server">
                    <asp:GridView ID="GVCherokeeDownloads" runat="server" AutoGenerateColumns="false" 
                              EmptyDataText = "No files uploaded" AllowSorting="True">    
                        <Columns>        
<asp:BoundField DataField="Text" HeaderText="Previous Cherokee Files" />       
 <asp:TemplateField>       
 <ItemTemplate>          
 <asp:LinkButton ID="lnkCherokeeDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>     
 </ItemTemplate>     
 </asp:TemplateField>   

 </Columns>
                    </asp:GridView>       
                       </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" 
                                runat="server" Enabled="True" TargetControlID="Panel3" Collapsed="True" 
                                CollapsedSize="0"  AutoCollapse="False" ExpandControlID="lbCherokeeDownloads"
                                CollapseControlID="lbCherokeeDownloads" AutoExpand="False" ScrollContents="False" 
                                TextLabelID="lbCherokeeDownloads" CollapsedText="View Past Cherokee Corrections..."
                                ExpandedText="Hide Corrections...." ImageControlID="Image1">
                            </cc1:CollapsiblePanelExtender>
                        <br />
                             <asp:LinkButton ID="lbForsythDownloads" runat="server" Text="View Past Forsyth Corrections"></asp:LinkButton>  
                          <asp:Panel ID="Panel2" runat="server">
                                <asp:GridView ID="GVForsythDownloads" runat="server" AutoGenerateColumns="false" 
                                          EmptyDataText = "No files uploaded" AllowSorting="True">    
                                    <Columns>        
                                    <asp:BoundField DataField="Text" HeaderText="Previous Forsyth Files" />       
                                     <asp:TemplateField>       
                                     <ItemTemplate>          
                                     <asp:LinkButton ID="lnkForsythDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>     
                                     </ItemTemplate>     
                                     </asp:TemplateField>   

                                     </Columns>
                                     </asp:GridView>      <br />
                              </asp:Panel>
                          <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" 
                          runat="server" Enabled="True" TargetControlID="Panel2" Collapsed="True" 
                          CollapsedSize="0"  AutoCollapse="False" ExpandControlID="lbForsythDownloads"
                            CollapseControlID="lbForsythDownloads" AutoExpand="False" ScrollContents="False" 
                            TextLabelID="lbForsythDownloads" CollapsedText="View Past Forsyth Corrections..."
                            ExpandedText="Hide Corrections...." ImageControlID="Image1">
                      </cc1:CollapsiblePanelExtender>
                       <br />
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell14" runat="server">
                            <div ID="Cherokee" runat="server" style=" padding-right: 10px;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                              
                          <asp:GridView ID="gvCherokee" runat="server" AutoGenerateColumns="False" 
                              BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                              CellPadding="4" Font-Size="X-Small" ForeColor="Black" 
                              HeaderStyle-BackColor="#214B9A" HeaderStyle-ForeColor="#FFCBA5" 
                              HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true">
                              <AlternatingRowStyle BackColor="#FFE885" />
                              <Columns>
                                  <asp:BoundField DataField="Fac" HeaderText="Entity" ReadOnly="True" 
                                      SortExpression="Fac" />
                                  <asp:BoundField DataField="Dept" HeaderText="Department" ReadOnly="True" 
                                      SortExpression="Dept" />
                                  <asp:BoundField DataField="SubAcct" HeaderText="Subaccount" ReadOnly="True" 
                                      SortExpression="SubAcct" />
                                  <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                                  <ControlStyle Width="1px" />
                                  <FooterStyle Width="1px" />
                                  <HeaderStyle Width="1px" />
                                  <ItemStyle Width="1px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                                  <ControlStyle Width="1px" />
                                  <FooterStyle Width="1px" />
                                  <HeaderStyle Width="1px" />
                                  <ItemStyle Width="1px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Description" HeaderText="Description" 
                                      ReadOnly="True" SortExpression="Description" />
                                  <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                                  <ControlStyle Width="1px" />
                                  <FooterStyle Width="1px" />
                                  <HeaderStyle Width="1px" />
                                  <ItemStyle Width="1px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                                  <ControlStyle Width="1px" />
                                  <FooterStyle Width="1px" />
                                  <HeaderStyle Width="1px" />
                                  <ItemStyle Width="1px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Debit" DataFormatString="{0:c}" HeaderText="Debit" 
                                      ReadOnly="True" SortExpression="Debit" />
                                  <asp:BoundField DataField="Credit" DataFormatString="{0:c}" HeaderText="Credit" 
                                      ReadOnly="True" SortExpression="Credit" />
                                  <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"  
                                      SortExpression="ID">
                                  <ControlStyle Width="1px" />
                                  <FooterStyle Width="1px" />
                                  <HeaderStyle Width="1px" />
                                  <ItemStyle Width="1px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="FY" HeaderText="FY" ReadOnly="True"  
                                      SortExpression="FY">
                                  <ControlStyle Width="1px" />
                                  <FooterStyle Width="1px" />
                                  <HeaderStyle Width="1px" />
                                  <ItemStyle Width="1px" />
                                  </asp:BoundField>
                              </Columns>
                              <PagerSettings Position="TopAndBottom" />
                              <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                              <EditRowStyle BackColor="#2461BF" />
                              <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
                              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                              <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left" />
                              <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
                              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                              <SortedAscendingCellStyle BackColor="#F5F7FB" />
                              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                              <SortedDescendingCellStyle BackColor="#E9EBEF" />
                              <SortedDescendingHeaderStyle BackColor="#4870BE" />
                          </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                           <asp:Button ID="btnGenerateCherokee" runat="server" Text="Finalize Cherokee File and Export to Excel" visible="false" />
                           <asp:Button ID="btnExportCherokee" runat = "server"  Text="Finalize Cherokee File and Generate Excel File" Visible ="false" />
                           <asp:Label ID="lblCheMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:HyperLink ID="hlDownloadCherokee" runat="server" Text="" Visible ="false" Target="_blank"></asp:HyperLink>  <br /> 
                              <asp:RadioButtonList ID="rblCherokee" runat="server" RepeatDirection="Horizontal" Visible="false" Font-Size="Smaller">
                                  <asp:ListItem Value="xlsx">Excel 2007 or newer</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="xls">Excel 97 - 2003</asp:ListItem>
                                  </asp:RadioButtonList> 
                             </div>
                        </asp:TableCell><asp:TableCell ID="TableCell15" runat="server">
                         
                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow6" runat="server">
                        <asp:TableCell ID="TableCell16" runat="server"></asp:TableCell><asp:TableCell ID="TableCell17" runat="server">
                  <div ID="FORSYTH" runat="server" style=" padding-right: 10px;">
                          <asp:GridView ID="gvForsyth" runat="server" AutoGenerateColumns="False" 
                              BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                              CellPadding="4" Font-Size="X-Small" ForeColor="Black" 
                              HeaderStyle-BackColor="#214B9A" HeaderStyle-ForeColor="#FFCBA5" 
                              HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true">
                    <AlternatingRowStyle BackColor="#FFE885" />
                    <Columns>
                    <asp:BoundField DataField="Fac" HeaderText="Entity" ReadOnly="True" 
                                      SortExpression="Fac" />
                    <asp:BoundField DataField="Dept" HeaderText="Department" ReadOnly="True" 
                                      SortExpression="Dept" />
                    <asp:BoundField DataField="SubAcct" HeaderText="Subaccount" ReadOnly="True" 
                                      SortExpression="SubAcct" />
                    <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                    <ControlStyle Width="1px" />
                    <FooterStyle Width="1px" />
                    <HeaderStyle Width="1px" />
                    <ItemStyle Width="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                    <ControlStyle Width="1px" />
                    <FooterStyle Width="1px" />
                    <HeaderStyle Width="1px" />
                    <ItemStyle Width="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                                      ReadOnly="True" SortExpression="Description" />
                    <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                    <ControlStyle Width="1px" />
                    <FooterStyle Width="1px" />
                    <HeaderStyle Width="1px" />
                    <ItemStyle Width="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Spacer" HeaderText="" ReadOnly="True" 
                                      SortExpression="Spacer">
                    <ControlStyle Width="1px" />
                    <FooterStyle Width="1px" />
                    <HeaderStyle Width="1px" />
                    <ItemStyle Width="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Debit" DataFormatString="{0:c}" HeaderText="Debit" 
                                      ReadOnly="True" SortExpression="Debit" />
                    <asp:BoundField DataField="Credit" DataFormatString="{0:c}" HeaderText="Credit" 
                                      ReadOnly="True" SortExpression="Credit" />
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                      SortExpression="ID">
                    <ControlStyle Width="1px" />
                    <FooterStyle Width="1px" />
                    <HeaderStyle Width="1px" />
                    <ItemStyle Width="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FY" HeaderText="FY" ReadOnly="True" 
                                      SortExpression="FY">
                    <ControlStyle Width="1px" />
                    <FooterStyle Width="1px" />
                    <HeaderStyle Width="1px" />
                    <ItemStyle Width="1px" />
                    </asp:BoundField>
                    </Columns>
                    <PagerSettings Position="TopAndBottom" />
                    <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <EditRowStyle BackColor="#2461BF" />
                    <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left" />
                              <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <asp:Button ID="btnGenerateForsyth" runat="server" Text="Finalize Forsyth File and Export to Excel" Visible="false"/>
                          <asp:Button ID="btnExportForsyth" runat = "server"  Text="Finalize Forsyth File and Generate Excel File"   Visible ="false" />
                          <asp:Label ID="lblForMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                          <asp:HyperLink ID="hlDownloadForsyth" runat="server" Text="" Visible ="false" Target="_blank"></asp:HyperLink>
                        <br />
                       <asp:RadioButtonList ID="rdlForsyth" runat="server" RepeatDirection="Horizontal" Visible="false" Font-Size="Smaller">
                         <asp:ListItem  Value="xlsx">Excel 2007 or newer</asp:ListItem>
                        <asp:ListItem Selected="True" Value="xls">Excel 97 - 2003</asp:ListItem>
                        </asp:RadioButtonList>
               <asp:Panel ID="pnlExportHeader" runat="server" Visible="false">
                              <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Text="Hospital Journal Entry "></asp:Label> <br /><br />
                               <asp:Label ID="lblEntity" runat="server" Text="Entity:"></asp:Label>  <br />
                               <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label><br />
                               <asp:Label ID="lblPeriod" runat="server" Text="Period:"></asp:Label>    <br />
                              <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label><br />
                              <asp:Label ID="lblBatch" runat="server" Text="Batch:"></asp:Label><br />
                              <asp:Label ID="Label12" runat="server" Text="JE Ref#: "></asp:Label><br />
                              <asp:Label ID="Label13" runat="server" Text="JE(J) or ACCR(A):"></asp:Label><br />
                              <asp:Label ID="Label14" runat="server" Text="Reversing:"></asp:Label><br /><br />
                              <asp:Label ID="Label15" runat="server" Text="Description: Clear suspense activity"></asp:Label>
                              </asp:Panel>
                       </div>
                   
                    </asp:TableCell><asp:TableCell ID="TableCell18" runat="server">
                    </asp:TableCell></asp:TableRow></asp:Table></div><br /></asp:Panel></div></asp:Content>