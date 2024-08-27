<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataDictionaryNew.aspx.vb" Inherits="FinanceWeb.DataDictionaryNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
   
         <style type="text/css">

    .scrollpanel
 {        
     max-height: 600px;
     overflow-x: auto; /* Hide horizontal scroll bar*/
     overflow-y: auto; /*Show vertical scroll bar*/
          }

        .panellinks
    {
     max-width: 175px;
     overflow-x: auto; /* Show horizontal scroll bar*/
     overflow-y: hidden; /* Hide vertical scroll bar*/
    }

                .panellinks2
    {
     max-width: 120px;
     overflow-x: auto; /* Show horizontal scroll bar*/
     overflow-y: hidden; /* Hide vertical scroll bar*/
    }

                    .paneltest
 {        
     max-height: 100px;
     max-width: 170px;
     overflow-x: hidden; /* Hide horizontal scroll bar*/
     overflow-y: auto; /*Show vertical scroll bar*/
          }

                       .lnks
    {
        background-color: #4A8fd2 ;
        color:white;

    }

    .lnks:hover 
    {
        background-color: #214B9A;
                   }
             </style>
    
             <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

     <script type="text/javascript">
        var teclatextbox = false;
        function TeclaTextbox(presionada) {
            teclatextbox = presionada;
        }

        function BodyKeyDown(e) {
            var key;
            if (window.event)//IE 
                key = window.event.keyCode;
            else//Firefox 
                key = e.which;
            if (!e) var e = window.event; //take event 
            if (key == 8 && !teclatextbox)//BACKSPACE and it is not inside a textbox 
            {//stop the default behavior
                e.cancelBubble =
            true;
                e.returnValue =
            false;
                if (e.stopPropagation) {
                    e.stopPropagation();
                    e.preventDefault();
                }
                return false;
            }
            return true;
        }
        
        function open_win(id) {

            var url = "https://financeweb.northside.local/References/DataDictionary/DictionaryEditor3.aspx/" + id;
            myWindow = window.open(url, 'height=700,width=1200, scrollbars, resizable');
            myWindow.focus();

        }

              </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>

 
    <div style="color: #000000; ">   
   <h2>Search Criteria</h2>
    <asp:Panel ID="pnlSearchTools" runat="server">
   <div style ="font-size:small">
     <asp:TextBox ID="txtBasicSearch" runat="server" width="60%"></asp:TextBox>&nbsp &nbsp 
            <asp:Button ID="btnBasicSearch" Height="30px" runat="server" Text="Search"/>&nbsp &nbsp 
            <asp:Button ID="btnAdvancedOptions" Height="30px" runat="server" Text="Advance Options" 
            />&nbsp &nbsp 
       <br />
       <asp:Label ID="lblCount" runat="server" Text="Count:0"></asp:Label><br />
   <asp:Label Visible="false" ID="Label1" runat="server" Text="Limit data sources to: "></asp:Label>
   <asp:RadioButtonList ID="rdlDataSources" Visible="false" runat="server" Font-Size="X-Small" 
               RepeatDirection="Horizontal">
               <asp:ListItem Selected="true">All</asp:ListItem>
               <asp:ListItem>DWH Proper</asp:ListItem>
           </asp:RadioButtonList>



       <asp:Panel ID="pnlAdvancedOptions" runat="server" Visible="False">
     
           <br />
           <asp:Label ID="Label2" runat="server" Text="Limit Search to: "></asp:Label>
                <asp:RadioButtonList ID="rdlDataTypes" runat="server" Font-Size="X-Small" 
               RepeatDirection="Horizontal">
               <asp:ListItem>Columns Only</asp:ListItem>
               <asp:ListItem>Tables Only</asp:ListItem>
               <asp:ListItem>Schemas Only</asp:ListItem>
               <asp:ListItem>Databases Only</asp:ListItem>
           </asp:RadioButtonList>
         
          <asp:Button ID="btnResetAdvanced" runat="server" Text="Reset" />   
           &nbsp;&nbsp;&nbsp; 
          <asp:Button ID="btnHideAdvanced" runat="server" Text="Hide" />


       </asp:Panel>

       <br />
       <asp:Label ID = "lblRecommendations" runat = "server" Visible = "False" Text = "Your search found zero matches." bordercolor = "Red"
       font-size = "medium" borderstyle = "solid" borderwidth = "1px"></asp:Label>  
       <asp:TextBox ID="txtSortDir" runat="server" Visible = "false" ></asp:TextBox>
       <asp:TextBox ID="txtField" runat="server" Visible = "false"></asp:TextBox>
       <asp:Label runat="server" ID="SortDir" Visible="false" Text="desc"></asp:Label>
       <asp:Label runat="server" ID="Sorts" Visible="false" Text="ord"></asp:Label>
        
           <br />
    <asp:Table runat = "server" ID="tblHeaders" Visible="false" >
    <asp:TableHeaderRow  BackColor="#4A8fd2" ForeColor = "White" Font-Size="Medium" HorizontalAlign="Center" VerticalAlign="Top" BorderColor="#003060" BorderWidth="2px" BorderStyle="Solid"  >
        <asp:TableHeaderCell Height="40px"  Width = "10px" ></asp:TableHeaderCell>
    <asp:TableHeaderCell Height="30px"  Width = "180px" ><asp:LinkButton runat="server" Text="Data Name" ForeColor="White" CssClass="lnks" ID="lnkSch1" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "80px" ><asp:LinkButton runat="server" Text="Data Type" ForeColor="White" CssClass="lnks" ID="lnkSch2" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "160px" ><asp:LinkButton runat="server" Text="Description" ForeColor="White" CssClass="lnks" ID="lnkSch3" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "160px" ><asp:LinkButton runat="server" Text="Technical Desc." ForeColor="White" CssClass="lnks" ID="lnkSch4" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "160px" ><asp:LinkButton runat="server" Text="Caveat(s)" ForeColor="White" CssClass="lnks" ID="lnkSch5" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "70px" ><asp:LinkButton runat="server" Text="Data Base" ForeColor="White" CssClass="lnks" ID="lnkSch6" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "95px" ><asp:LinkButton runat="server" Text="Schema" ForeColor="White" CssClass="lnks" ID="lnkSch7" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "150px" ><asp:LinkButton runat="server" Text="Table" ForeColor="White" CssClass="lnks" ID="lnkSch8" ></asp:LinkButton></asp:TableHeaderCell>
    <asp:TableHeaderCell  Width = "180px" ><asp:LinkButton runat="server" Text="Column" ForeColor="White" CssClass="lnks" ID="lnkSch9" ></asp:LinkButton></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    </asp:Table>
    <asp:Panel ID="ScrollPanel" runat = "server" CssClass ="scrollpanel" >
    <asp:GridView ID="gvDataDictionary" runat="server"  CssClass="collapsetable"
           AllowSorting="True" AutoGenerateColumns="False" BorderColor="#003060" 
      BorderStyle="Solid" Font-Size="Small" HeaderStyle-BackColor="#214B9A" 
         HeaderStyle-ForeColor="#FFCBA5" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Wrap="true"  ForeColor="Black" PageSize="100" AllowPaging="true"
         BackColor="#CBE3FB" BorderWidth="1px" CellPadding="0" CellSpacing="0"
            HeaderStyle-CssClass="hidden">   
        <AlternatingRowStyle BackColor="white"  />
                
        <Columns>
            
           <asp:TemplateField HeaderText="Data Name" ItemStyle-Width="175px">
                <ItemTemplate>
                    <asp:Panel CssClass="panellinks" Width="100%" runat="server" >
                        <asp:Label runat="server" ID="lblDataName" Text ='<%# Container.DataItem("ColumnName")%>' ></asp:Label>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

           <asp:BoundField DataField="DataType" HeaderStyle-HorizontalAlign="Left" 
                ControlStyle-Width="80px" HeaderStyle-Wrap="true" 
                HeaderText="Data Type" ReadOnly="True" SortExpression="DataType" 
                Visible="true">
            <HeaderStyle HorizontalAlign="Left"  Wrap="True" />
            <ItemStyle Width = "80px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText = "Description" SortExpression ="ColumnDesc" ItemStyle-Width = "170px" >
                <ItemTemplate>              
                <asp:Panel CssClass="paneltest" Width = "98%" runat = "server">
                    <asp:Label ID="lblGVDescription" runat = "server" Text='<%# Container.DataItem("ColumnDesc")%>'></asp:Label>
                </asp:Panel>              
                </ItemTemplate>
            </asp:TemplateField>

           <asp:TemplateField HeaderText = "Technical Description" SortExpression ="TechnicalDescription" ItemStyle-Width = "170px" >
                <ItemTemplate>              
                <asp:Panel CssClass="paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblGVTechDescription" runat = "server" Text='<%# Container.DataItem("TechnicalDescription")%>'></asp:Label>
                </asp:Panel>              
                </ItemTemplate>
            </asp:TemplateField>

           <asp:TemplateField HeaderText = "Caveat(s)" SortExpression ="Caveat" ItemStyle-Width = "170px" >
                <ItemTemplate>              
                <asp:Panel CssClass="paneltest"  Width = "98%" runat = "server">
                    <asp:Label ID="lblGVCaveat" runat = "server" Text='<%# Container.DataItem("Caveat")%>'></asp:Label>
                </asp:Panel>              
                </ItemTemplate>
            </asp:TemplateField>

           <asp:TemplateField HeaderText="DB" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Panel ScrollBars="Auto" Width="100%" runat="server">
                        <asp:LinkButton Font-Size="X-Small" ID="lnkGVDB" runat="server" Text='<%# Container.DataItem("DB")%>'
                             OnClientClick= '<%# "open_win(""?DataBase=" + CType(Container.DataItem, System.Data.DataRowView)("DB") + """)"%>'
                           ></asp:LinkButton>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Schema" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Panel CssClass="panellinks2" Width="100%" runat="server">
                        <asp:LinkButton Font-Size="X-Small"  ID="lnkGVSchema" runat="server" Text='<%# Container.DataItem("Schema")%>' 
                            OnClientClick= '<%# "open_win(""?DataBase=" + CType(Container.DataItem, System.Data.DataRowView)("DB") + "&Schema=" + CType(Container.DataItem, System.Data.DataRowView)("Schema") + """)"%>' >

                        </asp:LinkButton>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Table" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Panel CssClass="panellinks2" Width="100%" runat="server">
                        <asp:LinkButton Font-Size="X-Small"  ID="lnkGVTable" runat="server" Text='<%# Container.DataItem("Table")%>' 
                           OnClientClick= '<%# "open_win(""?DataBase=" +  CType(Container.DataItem, System.Data.DataRowView)("DB") + "&Schema=" + CType(Container.DataItem, System.Data.DataRowView)("Schema") + "&Table=" + CType(Container.DataItem, System.Data.DataRowView)("Table") + """)"%>' >

                        </asp:LinkButton>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Column" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Panel CssClass="panellinks2" Width="100%" runat="server">
                        <asp:LinkButton Font-Size="X-Small"  ID="lnkGVCol" runat="server" Text='<%# Container.DataItem("Column")%>'  
                        OnClientClick= '<%# "open_win(""?DataBase=" + CType(Container.DataItem, System.Data.DataRowView)("DB") + "&Schema=" + CType(Container.DataItem, System.Data.DataRowView)("Schema") + "&Table=" + CType(Container.DataItem, System.Data.DataRowView)("Table") + "&Column=" + CType(Container.DataItem, System.Data.DataRowView)("Column") + """)"%>' >

                        </asp:LinkButton>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            
 
        </Columns>
    
             <HeaderStyle BackColor="#214B9A" Font-Bold="True" ForeColor="#F6FCFC"  HorizontalAlign="Left"/>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        
           <PagerSettings Mode="NumericFirstLast" />
        
           <RowStyle BackColor="#CBE3FB" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#6da9e3" Font-Bold="True" ForeColor="#333333" Font-Size="Small"/>
                      <EditRowStyle  Font-Size="Small" /> 
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
     </asp:Panel>
    <br />

     </div>
    </asp:Panel> 
   </div>
  <br />
                 </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
