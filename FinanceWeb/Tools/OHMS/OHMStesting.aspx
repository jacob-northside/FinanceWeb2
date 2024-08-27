<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OHMS.aspx.vb" Inherits="OHMS.OHMS" %>

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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Select * FROM ScorecardData<br /> WHERE<br />

                <asp:Table ID="Table1" runat="server" CellPadding="5"
                GridLines="horizontal" HorizontalAlign="left">
                   <asp:TableRow>
                     <asp:TableCell>ID = </asp:TableCell>
                     <asp:TableCell><asp:DropDownList 
                    ID="namedscorecarddata" runat="server" DataSourceID="dsSelectID" 
                    DataTextField="SCMName" DataValueField="ID"></asp:DropDownList> </asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton1" groupname = 'IDonoff' runat="server" Text="Yes" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton2" GroupName = 'IDonoff' runat="server" Text="No" /></asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCDFY = </asp:TableCell>
                     <asp:TableCell>                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="dsSCDFY" 
                    DataTextField="SCDFY" DataValueField="SCDFY">
                </asp:DropDownList></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton3" runat="server" GroupName="SCDFYonoff" 
                    Text="Yes" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton4" runat="server" GroupName="SCDFYonoff" 
                    Text="No" /></asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCDFM = </asp:TableCell> 
                     <asp:TableCell>                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="dsDFM" 
                    DataTextField="SCDFM" DataValueField="SCDFM">
                </asp:DropDownList></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="FMonoff" 
                    Text="Yes" /></asp:TableCell>
                     <asp:TableCell>                <asp:RadioButton ID="RadioButton6" runat="server" GroupName="FMonoff" 
                    Text="No" /></asp:TableCell>
                   </asp:TableRow>
                   <asp:TableRow>
                     <asp:TableCell>SCDActual</asp:TableCell>
                     <asp:TableCell>IS NULL</asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton7" runat="server" 
                    GroupName="isnull" Text="Yes" /></asp:TableCell>
                     <asp:TableCell><asp:RadioButton ID="RadioButton8" runat="server" GroupName="isnull" 
                    Text="No" /></asp:TableCell>
                     </asp:TableRow>
                   
                </asp:Table>

               

                <br />
                <br /> 
                

                
                &nbsp;<asp:SqlDataSource ID="dsSCDFY" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select SCDFY from DWH.KPIS.ScorecardData Group by SCDFY">
                </asp:SqlDataSource>
                <br /> &nbsp;

                <asp:SqlDataSource ID="dsDFM" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    SelectCommand="Select SCDFM from DWH.KPIS.ScorecardData group by SCDFM">
                </asp:SqlDataSource>




                
<asp:SqlDataSource ID="dsSelectID" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    
                    SelectCommand="SELECT ID, SCMName FROM KPIS.ScorecardMetric GROUP BY ID, SCMName">
                </asp:SqlDataSource>
                &nbsp;&nbsp;
                <br />
                <br />
                <br />
                &nbsp;&nbsp
<br />
                <asp:Button ID="btnSCData" runat="server" Text="Execute" />
                <br /> <br /> &nbsp;
                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="SCDID" DataSourceID="dsOhmsData2" AllowPaging="True" 
                    AllowSorting="True" PageSize="20">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="SCDID" HeaderText="SCDID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="SCDID" />
                        <asp:BoundField DataField="SCDFY" HeaderText="SCDFY" SortExpression="SCDFY" />
                        <asp:BoundField DataField="SCDFM" HeaderText="SCDFM" SortExpression="SCDFM" />
                        <asp:BoundField DataField="SCDFD" HeaderText="SCDFD" SortExpression="SCDFD" />
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                        <asp:BoundField DataField="SCDActual" HeaderText="SCDActual" 
                            SortExpression="SCDActual" />
                        <asp:CheckBoxField DataField="SCDActive" HeaderText="SCDActive" 
                            SortExpression="SCDActive" />
                        <asp:BoundField DataField="SCDModifyDate" HeaderText="SCDModifyDate" 
                            SortExpression="SCDModifyDate" />
                        <asp:BoundField DataField="SCDUpdated" HeaderText="SCDUpdated" 
                            SortExpression="SCDUpdated" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="dsOhmsData2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
                    
                    
                    SelectCommand="SELECT * FROM DWH.KPIS.[ScorecardData] WHERE ([ID] = @IDselect or @Val = 0) and ([SCDFY] = @SCDFYselect or @Val2 = 0) and ([SCDFM] = @SCDFMselect or @Val3 = 0) and (SCDActual IS NULL or @Val4 = 0)" 
                    UpdateCommand="UPDATE KPIS.ScorecardData SET SCDFY = @SCDFY, SCDFM = @SCDFM, SCDFD = @SCDFD, ID = @ID, SCDActual = @SCDActual, SCDActive = @SCDActive, SCDModifyDate = @SCDModifyDate, SCDUpdated = @SCDUpdated WHERE (SCDID = @SCDID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="namedscorecarddata" Name="IDselect" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton1" DefaultValue="" Name="Val" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="DropDownList1" Name="SCDFYselect" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton3" Name="Val2" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="DropDownList2" Name="SCDFMselect" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="RadioButton5" Name="Val3" 
                            PropertyName="Checked" />
                        <asp:ControlParameter ControlID="RadioButton7" Name="Val4" 
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSCData" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <br />
    
        ScorecardMetric<br />
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
            DataSourceID="dsOHMS">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
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
        </asp:GridView>
        <asp:SqlDataSource ID="dsOHMS" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" SelectCommand="SELECT [ID]
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
  FROM [DWH].[KPIS].[ScorecardMetric]" 
            
            UpdateCommand="UPDATE KPIS.ScorecardMetric SET SCMID = @SCMID, SCMName = @SCMName, SCMObjective = @SCMObjective, SCMMeasures = @SCMMeasures, SCMDefinition = @SCMDefinition, SCMCalculations = @SCMCalculations, SCMSourceSystem = @SCMSourceSystem, SCMLTTarget = @SCMLTTarget, SCMLTGoalDate = @SCMLTGoalDate, SCMTarget = @SCMTarget, SCMDataType = @SCMDataType, SCMCumulative = @SCMCumulative, SCMMax = @SCMMax, SCMMin = @SCMMin, SCMwMax = @SCMwMax, SCMwMin = @SCMwMin, SCMUpdateMethod = @SCMUpdateMethod, SCMUpdateInput = @SCMUpdateInput, SCMUpdateFrequency = @SCMUpdateFrequency, SCMOwner = @SCMOwner, SCMCategory = @SCMCategory, SCMEffectiveFromDate = @SCMEffectiveFromDate, SCMEffectiveToDate = @SCMEffectiveToDate, SCMActive = @SCMActive, SCMUpdated = @SCMUpdated WHERE (ID = @ID)" 
            InsertCommand="INSERT INTO KPIS.ScorecardMetric(SCMName) VALUES (@InsertName)">
            <InsertParameters>
                <asp:Parameter Name="InsertName" />
            </InsertParameters>
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
        Insert into ScorecardMetric<br />
        <br />
        SCMID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtboxSCMID" runat="server"></asp:TextBox>
        <br />
        SCMName:&nbsp; 
        <asp:TextBox ID="txtboxSCMName" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
        <br />
        <br />
        ScorecardUserLookup<br />
        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCUID" 
            DataSourceID="dsOhmsUL">
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
        </asp:GridView>
        <asp:SqlDataSource ID="dsOhmsUL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="SELECT * FROM DWH.KPIS.ScorecardUser_LU" UpdateCommand="UPDATE KPIS.ScorecardUser_LU SET SCTID = @SCTID, SCUName = @SCUName, SCUUpdateFrequency = @SCUUpdateFrequency, SCUActive = @SCUActive, SCCModifyDate = @SCCModifyDate, SCCUpdated = @SCCUpdated
where SCUID = @SCUID">
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
        Scorecard<br />
        <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCID" 
            DataSourceID="dsOhmsSC">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="SCID" HeaderText="SCID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="SCID" />
                <asp:BoundField DataField="SCTID" HeaderText="SCTID" SortExpression="SCTID" />
                <asp:BoundField DataField="SCMID" HeaderText="SCMID" SortExpression="SCMID" />
                <asp:BoundField DataField="SCOwner" HeaderText="SCOwner" 
                    SortExpression="SCOwner" />
                <asp:CheckBoxField DataField="SCActive" HeaderText="SCActive" 
                    SortExpression="SCActive" />
                <asp:BoundField DataField="SCModifyDate" HeaderText="SCModifyDate" 
                    SortExpression="SCModifyDate" />
                <asp:BoundField DataField="SCUpdated" HeaderText="SCUpdated" 
                    SortExpression="SCUpdated" />
                <asp:BoundField DataField="SCMCategory" HeaderText="SCMCategory" 
                    SortExpression="SCMCategory" />
            </Columns>
        </asp:GridView>
    
        <asp:SqlDataSource ID="dsOhmsSC" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="Select * FROM DWH.KPIS.Scorecard" 
            UpdateCommand="UPDATE KPIS.Scorecard SET SCTID = @SCTID, SCMID = @SCMID, SCOwner = @SCOwner, SCActive = @SCActive, SCModifyDate = @SCModifyDate, SCUpdated = @SCUpdated, SCMCategory = @SCMCategory WHERE (SCID = @SCID)">
            <UpdateParameters>
                <asp:Parameter Name="SCTID" />
                <asp:Parameter Name="SCMID" />
                <asp:Parameter Name="SCOwner" />
                <asp:Parameter Name="SCActive" />
                <asp:Parameter Name="SCModifyDate" />
                <asp:Parameter Name="SCUpdated" />
                <asp:Parameter Name="SCMCategory" />
                <asp:Parameter Name="SCID" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView5" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SCTID" 
            DataSourceID="dsOhmsTL">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="SCTID" HeaderText="SCTID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="SCTID" />
                <asp:BoundField DataField="SCTitle" HeaderText="SCTitle" 
                    SortExpression="SCTitle" />
                <asp:CheckBoxField DataField="SCTActive" HeaderText="SCTActive" 
                    SortExpression="SCTActive" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsOhmsTL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PRDConn %>" 
            SelectCommand="SELECT KPIS.ScorecardTitle_LU.* FROM KPIS.ScorecardTitle_LU" 
            UpdateCommand="UPDATE KPIS.ScorecardTitle_LU SET SCTitle = @SCTitle, SCTActive = @SCTActive WHERE (SCTID = @SCTID)">
            <UpdateParameters>
                <asp:Parameter Name="SCTitle" />
                <asp:Parameter Name="SCTActive" />
                <asp:Parameter Name="SCTID" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
    




















    </div>
    </form>
</body>
</html>
