<%@ Page Title="AddressGeoCoding Data Entry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddressGeoCoding.aspx.vb" Inherits="FinanceWeb.AddressGeoCode_DataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>--%>

    <asp:UpdatePanel runat="server" ID= "updAddresses" ><ContentTemplate>
<div>
    
</div>

                <asp:Panel ID="AddressDataEntry" runat="server" Width="99%" Visible="false">

<asp:Table ID="Search_Addresses" Width="100%" Visible="true" BackColor = "#2b74bb" CellSpacing = "2"  runat = "server" BorderWidth = "1px" BorderColor = "#003060" ForeColor="White"  >
    <asp:TableRow>
        <asp:TableCell>Search:</asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSearchAddress" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSearchCity" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSearchState" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSearchZIP" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSearchMatchStatus" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    <asp:TableCell HorizontalAlign = "Center" > <asp:TextBox runat = "server" ID= "txtSearchMatchSubStatus" Width = "95%" AutoPostBack="True"  ></asp:TextBox> </asp:TableCell>
    </asp:TableRow>  
    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White">
    <asp:TableHeaderCell ID = "tblSearchAddress" Width = "50px" Height = "20px" ></asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "165px" >Street Address</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "185px" >City</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "180px" >State</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "185px" >ZIP</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "185px" >Match Status</asp:TableHeaderCell>
    <asp:TableHeaderCell Width = "185px" >Match Substatus</asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>
        <%------------------------------------WORKING SPACE-------------------------------------------------%>
    <div style="overflow-x:auto; width:100%">
        <asp:GridView ID="Update_Addresses" runat="server" bordercolor="Black" 
        borderStyle="Solid" font-Size="Small"  HeaderStyle-BackColor="#214B9A" 
        HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true" PageSize="25"  Width="2000px"
          ForeColor="Black"  BackColor="White" BorderWidth="1px"  onrowdatabound="Update_Addresses_RowDataBound" 
            ShowHeaderWhenEmpty="True" visible="True" AutoGenerateColumns="False" AllowPaging="True" CellPadding="5" CellSpacing="1" >
           <AlternatingRowStyle BackColor="#FFE885" /> 

            <Columns>
           <asp:CommandField  ShowDeleteButton="false" 
                ShowEditButton="true"  ItemStyle-Width="75px">

            </asp:CommandField>

<%--            <asp:BoundField DataField="UID" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Wrap="true" ItemStyle-Width="0px" 
                HeaderText="UID" ReadOnly="True" 
                Visible="false"  >
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="UID" Value='<%# Eval("UID")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        
<%--             <asp:TemplateField  HeaderText="Street Address" ItemStyle-Width="350px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("STREET_ADDRESS")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:TextBox runat="server"  ID="STREET_ADDRESS_EditBox" Rows="2" TextMode="MultiLine" Text='<%# Eval("STREET_ADDRESS")%>' />
                </EditItemTemplate>
            </asp:TemplateField>   --%>            


        <asp:BoundField DataField="STREET_ADDRESS" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                HeaderStyle-Wrap="true" ItemStyle-Width="350px"
                HeaderText="Street Address" ReadOnly="True" 
                Visible="True"  >
            <HeaderStyle HorizontalAlign="Left"  Wrap="False" />
            </asp:BoundField>        
                
<%--             <asp:TemplateField  HeaderText="City" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("CITY")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="CITY_EditBox" Rows="2" TextMode="SingleLine" Text='<%# Eval("CITY")%>' />
                </EditItemTemplate>
            </asp:TemplateField>   --%>

        <asp:BoundField DataField="CITY" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="City" ReadOnly="True" ItemStyle-Width="100px"
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     

<%--             <asp:TemplateField  HeaderText="State" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("STATE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="STATE_EditBox" Rows="2" TextMode="SingleLine" Text='<%# Eval("STATE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>   --%>
                
        <asp:BoundField DataField="STATE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="State"  ItemStyle-Width="100px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     

<%--             <asp:TemplateField  HeaderText="ZIP" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ZIP")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="ZIP_EditBox" Rows="2" TextMode="Number" Text='<%# Eval("ZIP")%>' />
                </EditItemTemplate>
            </asp:TemplateField>--%>   
                
        <asp:BoundField DataField="ZIP" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="ZIP" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     
                
             <asp:TemplateField  HeaderText="Match Status" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("MATCH_STATUS")%>' ID="Match_Status_Cell"/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:Label ID="Match_Status_Cell" runat="server" Text='<%# Eval("MATCH_STATUS")%>' Visible="false"/>
                    <asp:DropDownList runat="server" ID="ddlMatch_Status">
                        <asp:ListItem Value="" Text=""/>
                        <asp:ListItem Value="Match" Text="Match"/>
                        <asp:ListItem Value="No_Match" Text="No_Match"/>
                        <asp:ListItem Value="Tie" Text="Tie"/>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>   

<%--        <asp:BoundField DataField="MATCH_STATUS" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Match Status" ItemStyle-Width="100px"
                Visible="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField> --%>    

             <asp:TemplateField  HeaderText="Match Substatus" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="Match_Substatus_Cell" runat="server" Text='<%# Eval("MATCH_SUBSTATUS")%>'/>
                </ItemTemplate>
                <EditItemTemplate  >
                    <asp:Label ID="Match_Substatus_Cell" runat="server" Text='<%# Eval("MATCH_SUBSTATUS")%>' Visible="false"/>
            <%--<asp:TextBox runat="server"  ID="MATCH_SUBSTATUS_EditBox" Rows="2" TextMode="SingleLine" Text='<%# Eval("MATCH_SUBSTATUS")%>' />--%>
                        <asp:DropDownList runat="server" ID="ddlMatch_Substatus">
                            <asp:ListItem Value="" Text=""/>
                            <asp:ListItem Value="Exact" Text="Exact"/>
                            <asp:ListItem Value="Non_Exact" Text="Non_Exact"/>
                        </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>  
                
<%--        <asp:BoundField DataField="MATCH_SUBSTATUS" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Match Substatus" ItemStyle-Width="100px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>     --%>

             <asp:TemplateField  HeaderText="Latitude" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("LATITUDE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="LATITUDE_EditBox" Rows="2" TextMode="Number" Text='<%# Eval("LATITUDE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="LATITUDE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Latitude" ItemStyle-Width="100px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField> --%> 

             <asp:TemplateField  HeaderText="Longitude" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("LONGITUDE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="LONGITUDE_EditBox" Rows="2" TextMode="Number" Text='<%# Eval("LONGITUDE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="LONGITUDE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Longitude" ItemStyle-Width="100px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Matched Street Address" ItemStyle-Width="350px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("MATCHED_STREET_ADDRESS")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="MATCHED_STREET_ADDRESS_EditBox" Rows="2" TextMode="MultiLine" Text='<%# Eval("MATCHED_STREET_ADDRESS")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="MATCHED_STREET_ADDRESS" HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="350px"
                 HeaderStyle-Wrap="true" 
                HeaderText="Match Street Address" 
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Matched City" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("MATCHED_CITY")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="MATCHED_CITY_EditBox" TextMode="SingleLine" Text='<%# Eval("MATCHED_CITY")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="MATCHED_CITY" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Match City" ItemStyle-Width="100px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Matched State" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("MATCHED_STATE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="MATCHED_STATE_EditBox" TextMode="SingleLine" Text='<%# Eval("MATCHED_STATE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="MATCHED_STATE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Matched State" ItemStyle-Width="100px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Matched ZIP" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("MATCHED_ZIP")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="MATCHED_ZIP_EditBox" TextMode="Number" Text='<%# Eval("MATCHED_ZIP")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="MATCHED_ZIP" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Matched ZIP" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="TIGER LineID" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("TIGER_LINEID")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="TIGER_LINEID_EditBox" TextMode="Number" Text='<%# Eval("TIGER_LINEID")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="TIGER_LINEID" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="TIGER LineID" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField> --%> 

             <asp:TemplateField  HeaderText="Street Side" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("STREET_SIDE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="STREET_SIDE_EditBox" TextMode="SingleLine" Text='<%# Eval("STREET_SIDE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="STREET_SIDE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Street Side" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="State FIPS Code" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("STATE_FIPS_CODE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="STATE_FIPS_CODE_EditBox" TextMode="Number" Text='<%# Eval("STATE_FIPS_CODE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="STATE_FIPS_CODE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="State FIPS Code" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="County FIPS Code" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("COUNTY_FIPS_CODE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="COUNTY_FIPS_CODE_EditBox" TextMode="Number" Text='<%# Eval("COUNTY_FIPS_CODE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="COUNTY_FIPS_CODE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="County FIPS Code" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Census Tract Code" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("CENSUS_TRACT_CODE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="CENSUS_TRACT_CODE_EditBox" TextMode="Number" Text='<%# Eval("CENSUS_TRACT_CODE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="CENSUS_TRACT_CODE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Census Tract Code" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Census Block Code" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("CENSUS_BLOCK_CODE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="CENSUS_BLOCK_CODE_EditBox" TextMode="Number" Text='<%# Eval("CENSUS_BLOCK_CODE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="CENSUS_BLOCK_CODE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Census Block Code" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>

             <asp:TemplateField  HeaderText="Match Date" ItemStyle-Width="50px" ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("MATCH_DATE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="MATCH_DATE_EditBox" TextMode="Date" Text='<%# Eval("MATCH_DATE")%>' />
                </EditItemTemplate>
            </asp:TemplateField>  

<%--        <asp:BoundField DataField="MATCH_DATE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Match Date" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField> --%> 

             <asp:TemplateField  HeaderText="Vintage" ItemStyle-Width="50px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("VINTAGE")%>'/>
                </ItemTemplate>
                <EditItemTemplate >
            <asp:TextBox runat="server"  ID="VINTAGE_EditBox" TextMode="Number" Text='<%# Eval("VINTAGE")%>' />
                </EditItemTemplate>
            </asp:TemplateField> 

<%--        <asp:BoundField DataField="VINTAGE" HeaderStyle-HorizontalAlign="Left" 
                 HeaderStyle-Wrap="true" 
                HeaderText="Vintage" ItemStyle-Width="50px"
                Visible="True" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            </asp:BoundField>  --%>
    </Columns>


 <PagerSettings Position="TopAndBottom" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        </div>

            </asp:Panel>
        <%---------------------------------------------------------------------------------------%>
        <%------------------------------------WORKING SPACE-------------------------------------------------%>
    <%--</asp:Panel></div>--%>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
