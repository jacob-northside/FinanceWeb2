<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" EnableEventValidation = "false" CodeBehind="StarGLBalancing.aspx.vb" Inherits="FinanceWeb.StarGLBalancing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
    <script type="text/javascript">
    function open_win() {
        window.open("https://financeweb.northside.local/Tools/StarGLSubItems/StarGLBalancingSub.aspx", "FARDAP", "height=768,width=1024, scrollbars, resizable");
     }
 
</script>
           <style type="text/css">


          }
           </style>

</asp:Content>


<asp:Content ID="Maincontent" runat="server" contentplaceholderid="Maincontent">
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <br />
   <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Start Date"></asp:Label>     &nbsp;  
      <asp:TextBox ID="StartDateTextBox" runat="server" text="07/01/2013" />
           <cc1:calendarextender ID="CalendarExtender1" 
    runat="server" TargetControlID="StartDateTextBox" 
    PopupButtonID="Image1">
</cc1:calendarextender>  
       <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="   End Date"></asp:Label> &nbsp;  
    <asp:TextBox ID="EndDateTextBox" runat="server" text="07/01/2013" />
    <cc1:calendarextender ID="EndDateTextBox_CalendarExtender" 
    runat="server" TargetControlID="EndDateTextBox" 
    PopupButtonID="Image1">
</cc1:calendarextender>
           
 &nbsp; &nbsp;     <asp:LinkButton ID="lbLoadNewDates" runat="server">Load New Dates</asp:LinkButton> &nbsp; &nbsp; 
  <asp:CheckBox ID="chbIncludeIgnored" runat="server" Font-Size="X-Small" Text="Include Ignored Records" />
        <asp:CheckBox ID="cb_UnbalancedAccounts" runat="server" Font-Size="X-Small" Text="Show Balanced Accounts" />
     <h5>   <asp:RadioButtonList ID="rblFacilityFilter" runat="server" RepeatDirection="Horizontal">
         <asp:ListItem Value="%" Selected ="True"> All</asp:ListItem>
         <asp:ListItem Value="A">Atlanta</asp:ListItem>
         <asp:ListItem Value="C">Cherokee</asp:ListItem>
         <asp:ListItem Value="F">Forsyth</asp:ListItem>
         <asp:ListItem Value="D">Duluth</asp:ListItem>
         <asp:ListItem Value="L">Gwinnett</asp:ListItem>
        </asp:RadioButtonList></h5>
   <asp:Panel ID="Panel4" runat="server"    Width="100%"  >
   <asp:Label ID="lblMessage" runat="server" 
        Visible="False" ForeColor="Red"></asp:Label>  
   &nbsp; &nbsp; &nbsp;     &nbsp; &nbsp; &nbsp;     &nbsp; &nbsp;  &nbsp;  &nbsp;  <br /> 
<%-- 
    <asp:LinkButton ID="lbOpenFARDAP" runat="server" OnClientClick="open_win()" Font-Underline="False">Open FARDAP</asp:LinkButton>

--%>
            <asp:Button ID="lbOpenFARDAP2" runat="server" Text="Open FARDAP"  OnClientClick="open_win()"  />


            &nbsp; &nbsp; &nbsp;     &nbsp; &nbsp; &nbsp;     &nbsp; &nbsp; &nbsp;     &nbsp; &nbsp;  &nbsp;  &nbsp;  &nbsp;  
  <%--  <asp:HyperLink ID="hlOpenJECorrections" runat="server" 
         NavigateUrl="~/FinancialOperations/StarGLSubItems/StarGLJEEntries.aspx" 
         Target="_self" Font-Underline="False">Generate JE Corrective Entries </asp:HyperLink>
    --%>

              &nbsp; &nbsp; 
  <br /> <br />
             <asp:Label ID="Label8" runat="server" 
           Text="FARGLD – Entries that have a Department code of 9999, indicating suspense activity."></asp:Label><br />
       <br />

       <asp:Label ID="Label4" runat="server" Text="INSTRUCTIONS:" Font-Bold="True"></asp:Label>
       <asp:BulletedList ID="BulletedList1" runat="server" BulletStyle="Disc">
           <asp:ListItem>Enter correct department and subaccount for each transaction (if 
           record can be ignored, click the ignore checkbox at the end of the row).</asp:ListItem>
           <asp:ListItem>When all corrections are complete, select “Update Edited Records” 
           button. (Note: if additional corrections need to be made after selecting this 
           button, make the necessary corrections and select the “Update Edited Records” 
           button again)</asp:ListItem>
           <asp:ListItem>After the edited records are updated, select the “Generate JE 
           Corrective Entries” button.</asp:ListItem>
       </asp:BulletedList>

        
       <asp:Label ID="Label3" runat="server" Text=" "></asp:Label>&nbsp;   <br />

      <asp:Panel runat="server" Width="100%"  > 
<asp:GridView ID="gvFarDAP" runat="server" AutoGenerateColumns ="False" 
      DataKeyNames = "ID"  
         CellPadding="4" BorderColor="Black"    BackColor="White" 
          HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Left" 
        HeaderStyle-Wrap="true"  ForeColor="Black"  
       BorderWidth="1px" AllowSorting ="True"  
         BorderStyle="Solid"   HeaderStyle-BackColor="#4A8fd2"  
        Font-Size="X-Small" >
                    <AlternatingRowStyle BackColor="#CBE3FB" />
    <Columns>
            <asp:TemplateField Visible="False">
                <HeaderTemplate>
                    <asp:CheckBox ID = "chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged2" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" 
                        OnCheckedChanged="OnCheckedChanged2" Checked="True" />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="ID" ItemStyle-Width = "25"  Visible=false  >
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server"   Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
          <asp:TemplateField HeaderText="FY" ItemStyle-Width = "50">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("FY") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>		
   <asp:TemplateField HeaderText="FM" ItemStyle-Width = "75">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("FM") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="FAC" ItemStyle-Width = "100">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("FAC") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="Dept" ItemStyle-Width = "40">
                <ItemTemplate>
                    <asp:Label ID="lblDept" runat="server" visible="false"  Text='<%# Eval("Dept") %>'></asp:Label>
                     <asp:TextBox ID="TextBox171" runat="server" Width="40" Text='<%# Eval("Dept") %>' Visible="true" ReadOnly ="false"></asp:TextBox>
                   
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
</asp:TemplateField>
   <asp:TemplateField HeaderText="SubAcct" ItemStyle-Width = "40">
                <ItemTemplate>
              <asp:Label ID="Label17" runat="server" Visible = "false"   Text='<%# Eval("SubAcct") %>'></asp:Label> 
                 <asp:TextBox ID="TextBox17" runat="server" Width="40" Text='<%# Eval("SubAcct") %>' Visible="true" ReadOnly ="false"></asp:TextBox>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
            </asp:TemplateField>
   <asp:TemplateField HeaderText="TranDate" ItemStyle-Width = "150">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%# Eval("TranDate") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField>   
    <asp:TemplateField HeaderText="Debit" ItemStyle-Width = "150">
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
     <asp:TemplateField HeaderText="Credit" ItemStyle-Width = "150">
                <ItemTemplate>
                    <asp:Label ID="Label20" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
     <asp:TemplateField HeaderText="Qty" ItemStyle-Width = "50">
                <ItemTemplate>
                    <asp:Label ID="Label21" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
     <asp:TemplateField HeaderText="Contr Acct" ItemStyle-Width = "100">
                <ItemTemplate>
                    <asp:Label ID="Label22" runat="server" Text='<%# Eval("ContrAcct") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
    <asp:TemplateField HeaderText="Pat_Acct_Fac" ItemStyle-Width = "150">
                <ItemTemplate>
                    <asp:Label ID="Label23" runat="server" Text='<%# Eval("Pat_Acct_Fac") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
     <asp:TemplateField HeaderText="comments" ItemStyle-Width = "150">
                <ItemTemplate>
                    <asp:Label ID="lblComments" runat="server" Visible = "false" Text='<%# Eval("comments") %>'></asp:Label>
				    <asp:TextBox ID="TextBox24" runat="server" Text='<%# Eval("comments") %>' Visible="true" ReadOnly="false"></asp:TextBox>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
     <asp:TemplateField HeaderText="Tran Code" ItemStyle-Width = "100">
                <ItemTemplate>
                    <asp:Label ID="Label25" runat="server" Text='<%# Eval("TranCode")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
     <asp:TemplateField HeaderText="TranCodeDesc" ItemStyle-Width = "150">
                <ItemTemplate>
                    <asp:Label ID="Label26" runat="server" Text='<%# Eval("TranCodeDesc") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
   </asp:TemplateField> 
   
            <asp:TemplateField >
              <HeaderTemplate>
              <asp:Label ID="lblIgnoreRecord" Text="Ignore Record" runat="server"></asp:Label>
                    <asp:CheckBox ID = "chkAllIR" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                </HeaderTemplate>
            <ItemTemplate> 
            <asp:CheckBox ID="chkIgnore" runat="server" Enabled = "true" value='<%# Eval("IgnoreRecord") %>' />
             
            </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField> 


         <asp:TemplateField >
             <HeaderTemplate>
              <asp:Label ID="lblHoldRecord" Text="Hold Records" runat="server"></asp:Label>
               <%--      <asp:CheckBox ID = "chkAllIR" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />--%>
              </HeaderTemplate>
            <ItemTemplate> 
            <asp:CheckBox ID="chkHold" runat="server" Enabled = "true" value='<%# Eval("HoldRecord") %>' />
             
            </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField> 
            
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width = "70">
             <HeaderTemplate>
              <asp:Label ID="GLEffectiveDate" Text="GL Effective Date" runat="server"></asp:Label>
               <%--      <asp:CheckBox ID = "chkAllIR" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />--%>
              </HeaderTemplate>
            <ItemTemplate > 
          <%--  <asp:Calendar id="GLEffectiveDate" runat = "server" enabled ="true" text ='<%# Eval("GLEffectiveDate")  %>'   /> --%>
               <%-- <asp:Label ID="Label5" runat="server" Text="GL Eff: "></asp:Label>--%>
             
                <asp:TextBox ID="GLEffectiveDate" runat="server" width ="70"
                    Text='<%# Eval("GLEffectiveDate", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
               <asp:Label ID="lblGLEffectiveDate" runat="server" width ="70"
                                text ="YYYY-MM-dd" Enabled = "False"  ></asp:Label>
                <br />
         
            </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField> 


        </Columns>
    <HeaderStyle HorizontalAlign="Left" />

        <PagerSettings Position="TopAndBottom" />
     <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
   <EditRowStyle BackColor="#2461BF" />
      
           <HeaderStyle BackColor="#4A8fd2" Font-Bold="True" ForeColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F6FCFC" ForeColor="#000000" HorizontalAlign="left"  />
           <RowStyle BackColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Top" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
           </asp:Panel> 
       <br />

 <asp:Button ID="btnUpdate2" runat="server" Text="Update Edited Records" 
           OnClick = "Update2" Visible = "false"/>
       &nbsp;<asp:Label ID="lblCount" runat="server"></asp:Label>

    <br />  <br />
      <asp:Button ID="hlOpenJECorrections2" runat="server" Text="Generate JE Corrective Entries" PostBackUrl="~/Tools/StarGLSubItems/StarGLJEEntries.aspx" />
           <br />
           <br />
     </asp:Panel>

         </ContentTemplate> 
    </asp:UpdatePanel>
 
  
 
  
            </asp:Content>
 

