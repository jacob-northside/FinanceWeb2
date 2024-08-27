<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FREDInstructions.aspx.vb" Inherits="FinanceWeb.FREDInstructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
      
    <cc1:TabContainer ID="FREDTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel runat = "server" HeaderText = "Pivot Instructions" ID = "SBInstructions" >
                    <ContentTemplate>      
                                   <cc1:TabContainer ID="Complication" runat="server" ActiveTabIndex="0"  UseVerticalStripPlacement ="true">
                                        <cc1:TabPanel runat="server" HeaderText="Summary">
                                            <ContentTemplate>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell >
                                     <strong>1: Open Edit Mode </strong>
                                        <br /> Click the blue Edit button in the upper right of the page.  You will see a pop-up -- please disregard and close by clicking the "OK" button.
                                    <br /><br />
                                    <strong>2: Build Rows and Columns </strong>
                                    <br />
                                    Choose the desired fields along the top and left by <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;A) Left Clicking the Rectangles where it says (None); Select an additional field by clicking the Rectangle next to it reading "+" <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;B) Right Clicking the Rectangles where it says (None) and clicking "Select Columns", then selecting all desired columns <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;C) Right Clicking the Rectangles where it says (None) and clicking "Custom Expression", then typing<br />
                                    <br /> You can nest as many fields as desired, but performance will suffer.
                                    <br /><br />
                                    <strong>3: Adding Aggregated Metrics </strong>
                                   <br />
                                    Choose the desired fields along the bottom by <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;A) Left Clicking the Rectangles where it says (Row Count); Select an additional field by clicking the Rectangle next to it reading "+"<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select the aggregation method with the drop down at the top of the pop-up.<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;B) Right Clicking the Rectangles where it says (Row Count) and clicking "Select Columns", then selecting all desired columns <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select the aggregation method for the highlighted column with the drop down at the bottom of the pop-up.<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;C) Right Clicking the Rectangles where it says (Row Count) and clicking "Custom Expression", then typing<br />
                                    <br /> Please note that adding multiple aggregated metrics requires selecting "(Column Names)" across either the left or the top
                                    <br /><br />
                                    <strong>4: Filter </strong>
                                   <br /> Open or close the filter menu by clicking the grey funnel in the menu bar
                                    <br /><br />
                                    <strong>5: Save Bookmark </strong>
                                   <br /> Any Cross Tables you want to save should be turned into a bookmark.   Click the button that looks like a banner with a star in it, name your bookmark in the text entry box, and click the "+" button.  The next time you open the analytic, you can select your bookmark from the dropdown and your saved settings will be displayed.
                              </asp:TableCell>
                                <asp:TableCell >

                                  
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table> 
                                     </ContentTemplate>   </cc1:TabPanel>
                                       <cc1:TabPanel runat="server" HeaderText="Detailed">
                                                      <ContentTemplate>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell ID="SumDetInstructionsCell" Width="50%" Font-Size="Smaller" VerticalAlign="Top">

                                    <asp:Table runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="10"><strong>1: Open Edit Mode </strong></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="5"></asp:TableCell>
                                            <asp:TableCell ColumnSpan="9">Click the blue Edit button in the upper right of the page.  You will see a pop-up -- please disregard and close by clicking the "OK" button.<br /><br /></asp:TableCell>                                           
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="10"><strong>2: Build Rows and Columns </strong></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="9">Choose the desired fields along the top and left by </asp:TableCell>                                            
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell><asp:TableCell Width="5"></asp:TableCell>
                                            <asp:TableCell ColumnSpan="8">A) Left Clicking the Rectangles where it says (None); Select an additional field by clicking the Rectangle next to it reading "+" 
                                                <br />&nbsp;&nbsp; You can rearrange the order by dragging and dropping
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell><asp:TableCell Width="5"></asp:TableCell>
                                            <asp:TableCell ColumnSpan="8">B) Right Clicking the Rectangles where it says (None) and clicking "Select Columns", then selecting all desired columns </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell><asp:TableCell Width="5"></asp:TableCell>
                                            <asp:TableCell ColumnSpan="8">C) Right Clicking the Rectangles where it says (None) and clicking "Custom Expression", then typing </asp:TableCell>
                                        </asp:TableRow>
                                                                                <asp:TableRow>
                                            <asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="9">You can nest as many fields as desired, but performance will suffer.<br /><br /> </asp:TableCell>                                            
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="10"><strong>3: Adding Aggregated Metrics </strong> </strong></asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                            
                                    
                                   <br />
                                    Choose the desired fields along the bottom by <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;A) Left Clicking the Rectangles where it says (Row Count); Select an additional field by clicking the Rectangle next to it reading "+"<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select the aggregation method with the drop down at the top of the pop-up.<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;B) Right Clicking the Rectangles where it says (Row Count) and clicking "Select Columns", then selecting all desired columns <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select the aggregation method for the highlighted column with the drop down at the bottom of the pop-up.<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;C) Right Clicking the Rectangles where it says (Row Count) and clicking "Custom Expression", then typing<br />
                                    <br /> Please note that adding multiple aggregated metrics requires selecting "(Column Names)" across either the left or the top
                                    <br /><br />
                                    <strong>4: Filter </strong>
                                   <br /> Open or close the filter menu by clicking the grey funnel in the menu bar
                                    <br /><br />
                                    <strong>5: Save Bookmark </strong>
                                   <br /> Any Cross Tables you want to save should be turned into a bookmark.   Click the button that looks like a banner with a star in it, name your bookmark in the text entry box, and click the "+" button.  The next time you open the analytic, you can select your bookmark from the dropdown and your saved settings will be displayed.
                                </asp:TableCell>
                                <asp:TableCell ID="SumDetPictureCell" Width="600px" Height="600px" HorizontalAlign="Center">
                                    <asp:Panel ScrollBars="Auto" Width="600px" Height="600px" runat="server" Font-Size="X-Small" >
                                         <asp:Image runat = "server" ImageUrl="Images/EditButton_1.bmp" Width="200px"/>
                                        <br />1. Enter Edit Mode<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/EditButton_2.bmp" Width="300px"/>
                                        <br />1. Disregard Pop-Up<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/ColumnSelection_3.bmp" Width="600px"/>
                                        <br />2. Column/Row Locations<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/ColumnSelection_4.png" />
                                        <br />2-A. Select Column Grouping from "(None)" Drop-Down<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/ColumnSelection_6.bmp" Width="600px"/>
                                        <br />2-A. Select Second Column Grouping from "+" Drop-Down<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/ColumnSelection_7.png" Width="600px"/>
                                        <br />2-A. Nested Results<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/ColumnSelection_8.png" />
                                        <br />2-A. Drag Columns to switch their Nesting Order <br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/ColumnSelection_9.png" Width="600px"/>
                                        <br />2-A. Revised Nesting Order Results <br /><br /><br />

                                    </asp:Panel>                                  
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                                       </ContentTemplate> </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Advanced">
                                            <ContentTemplate>
                                                <asp:LinkButton runat="server" ID="lbCustomExpressionsTD"> How to Adjust PYTD and CYTD Reports in Spotfire </asp:LinkButton>
                                                <br />
                                                <asp:LinkButton runat="server" ID="lbSpotfireHelpCust"> Spotfire Custom Expressions Help </asp:LinkButton>
                                                <asp:Table runat="server" Width="100%" Height="100%">
                                                    <asp:TableRow>
                                                        <asp:TableCell ID="tcCustomExpressionsTD" Visible="false">
                                        <asp:Image runat = "server" ImageUrl="Images/HowToAdjustTD.png"  />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                        <asp:TableRow>
                                                        <asp:TableCell ID="tcSpotfireHelpCustomExpressions" Visible="false">
                                                            <iframe id="SpotfireHelpFrame" runat="server" src="http://mckexplorer/SpotfireWeb/Help/GUID-A18A2E86-92DB-4E65-BE3F-64BA3AD8B76F.html"
                                                                scrolling="auto" width="100%" height="800px" frameborder="1"></iframe>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>

                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>  




     </ContentTemplate>
     </cc1:TabPanel>
          <cc1:TabPanel runat = "server" HeaderText = "Patient Filter Instructions"  >
                    <ContentTemplate>      
                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement ="true">
                                        <cc1:TabPanel runat="server" HeaderText="Summary">
                                            <ContentTemplate>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell ID="PatFilInstructionsCell">
                                       <strong>1: Select Column </strong>
                                        <br /> Select either Patient Account Facility or Medical Record Number from the drop down list in the Sandbox Patient Filter section.
                                    <br /><br />
                                    <strong>2: Enter Values </strong>
                                    <br />
                                    In the large text area provided, type or paste in your list of values.  <br />
                                    Patients can be separated by spaces, commas, tabs, or line breaks (So you can copy directly from excel).<br />
                                    Please limit lists to no more than 50,000 values, for performance purposes.
                                    <br /><br />
                                    <strong>3: Load List </strong>
                                   <br />
                                    Click the "Load List" button to filter the Pivot by the patients you have entered in the text area.
                                    
                                    <br /><br />
                                    <strong>4: Reset List </strong>
                                   <br /> Click the Reset button at the top of the Sandbox Patient Filter section at any time to clear the Patient Filter List and show all values in the Pivot.
                                    <br /><br />
                                   
                                </asp:TableCell>
                                <asp:TableCell ID="PatFilPictureCell">

                                  
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table></ContentTemplate>
                                        </cc1:TabPanel>
    <%--                                    <cc1:TabPanel runat="server" HeaderText="Detailed">
                                            <ContentTemplate>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell ID="PatFilDetInstructionsCell">
                                       <strong>1: Select Column </strong>
                                        <br /> Select either Patient Account Facility or Medical Record Number from the drop down list in the Sandbox Patient Filter section.
                                    <br /><br />
                                    <strong>2: Enter Values </strong>
                                    <br />
                                    In the large text area provided, type or paste in your list of values.  <br />
                                    Patients can be separated by spaces, commas, tabs, or line breaks (So you can copy directly from excel).<br />
                                    Please limit lists to no more than 50,000 values, for performance purposes.
                                    <br /><br />
                                    <strong>3: Load List </strong>
                                   <br />
                                    Click the "Load List" button to filter the Pivot by the patients you have entered in the text area.
                                    
                                    <br /><br />
                                    <strong>4: Reset List </strong>
                                   <br /> Click the Reset button at the top of the Sandbox Patient Filter section at any time to clear the Patient Filter List and show all values in the Pivot.
                                    <br /><br />
                                </asp:TableCell>
                                <asp:TableCell ID="PatFilDetPictureCell">

                                  
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                                            </ContentTemplate>
                                        </cc1:TabPanel>--%>
<%--                                        <cc1:TabPanel runat="server" HeaderText="Advanced">
                                            <ContentTemplate>

                                            </ContentTemplate>
                                        </cc1:TabPanel>--%>
                                    </cc1:TabContainer>  




     </ContentTemplate>
     </cc1:TabPanel>
          <cc1:TabPanel runat = "server" HeaderText = "Advanced Query Builder Instructions"  >
                    <ContentTemplate>      
                                    <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement ="true">
                                        <cc1:TabPanel runat="server" HeaderText="Summary">
                                            <ContentTemplate>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell ID="AdvQInstructionsCell">
                                    The goal of the Advanced Query Builder is to help you find a list of patients who meets certain criteria.  Each criterion you want to limit your data set by is depicted by a Rule.  
                                    <br />When you pull your set of patients, the patient must meet all Rules shown in Section 4, "Current SandBox Rules". 
                                       <br /><br />
                                    <asp:Table runat="server">
                                        <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell>
                                    <strong>1: Create Rules </strong>
                                        <br /> To add a rule, define the following criterion:<br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;A) Table<br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;B) Column<br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;C) Operand <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;D) Value <br />
                                    <br />
                                    When you have entered the Rule criterion, hit the ">" arrow button, and it the Rule will be created and moved to Section 4.
                                    <br /><br />
                                    <strong>2: Remove Rules </strong>
                                    <br />
                                    If you want to remove a rule, click it in Section 4, "Current SandBox Rules".  This will highlight a row.  Then click the "Remove Selected Rule" button in Section 5; the highlighted row will be removed from Section 4.
                                    <br /><br />
                                    <strong>3: Pull Patients </strong>
                                   <br />
                                    When you are satisfied with the Set of Rules showing in Section 4, click the "Pull Patients" button in Section 7.  This will run your Rule Set, and find all the encounters that met all the specified criterion, returning them in Section 8.  
                                    <br /> Encounters can be returned either with Patient Account Facility, or with Medical Record Number, as specified in the Select Column dropdown in Section 7.
                                                                                                           
                                    <br /> 
                                    <br /><br />
                                    <strong>4: Limit SandBox </strong>
                                   <br /> Click the "Limit and Return to SandBox" button to place all the patients from Section 8 in the Sandbox Patient Filter TextBox in the SandBox and filter the Pivot by the results.
                                    <br /> Please limit lists to no more than 50,000 values for performance purposes.
                                    <br /><br />
                                   </asp:TableCell></asp:TableRow>
                                        </asp:Table>
                                    <br />
                                    If you have constructed a Rule Set that you would like to use again in the future, with all the Rules visible in Section 4, enter a name fo rthe Rule Set in Section Six, and click the "Save Rule Set" Button.
                                    <br />
                                    If the name in the Current Rule Set Name text area belongs to a Rule Set you already own, it will overwrite the existing Rule Set.
                                    <br />
                                    Rule Sets are user-specific -- users can have different Rule Sets with the same name, and they are not shared between users.
                                    <br />
                                    You can access your Rule Set at any time by selecting it from the drop-down in Section 2.  
                                    <br />

                                </asp:TableCell>
                                <asp:TableCell ID="AdvQPictureCell">

                                  
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                                       </ContentTemplate> </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Detailed">
                                            <ContentTemplate>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell ID="AdvQDetInstructionsCell">
                                   The goal of the Advanced Query Builder is to help you find a list of patients who meets certain criteria.  Each criterion you want to limit your data set by is depicted by a Rule.  
                                    <br />When you pull your set of patients, the patient must meet all Rules shown in Section 4, "Current SandBox Rules". 
                                       <br /><br />
                                    <asp:Table runat="server">
                                        <asp:TableRow><asp:TableCell Width="10px"></asp:TableCell><asp:TableCell>
                                    <strong>1: Create Rules </strong>
                                        <br /> To add a rule, define the following criterion:<br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;A) Table<br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;B) Column<br />
                                         &nbsp;&nbsp;&nbsp;&nbsp;C) Operand <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;D) Value <br />
                                    <br />
                                    When you have entered the Rule criterion, hit the ">" arrow button, and it the Rule will be created and moved to Section 4.
                                    <br /><br />
                                    <strong>2: Remove Rules </strong>
                                    <br />
                                    If you want to remove a rule, click it in Section 4, "Current SandBox Rules".  This will highlight a row.  Then click the "Remove Selected Rule" button in Section 5; the highlighted row will be removed from Section 4.
                                    <br /><br />
                                    <strong>3: Pull Patients </strong>
                                   <br />
                                    When you are satisfied with the Set of Rules showing in Section 4, click the "Pull Patients" button in Section 7.  This will run your Rule Set, and find all the encounters that met all the specified criterion, returning them in Section 8.  
                                    <br /> Encounters can be returned either with Patient Account Facility, or with Medical Record Number, as specified in the Select Column dropdown in Section 7.
                                                                                                           
                                    <br /> 
                                    <br /><br />
                                    <strong>4: Limit SandBox </strong>
                                   <br /> Click the "Limit and Return to SandBox" button to place all the patients from Section 8 in the Sandbox Patient Filter TextBox in the SandBox and filter the Pivot by the results.
                                    <br /> Please limit lists to no more than 50,000 values for performance purposes.
                                    <br /><br />
                                   </asp:TableCell></asp:TableRow>
                                        </asp:Table>
                                    <br />
                                    If you have constructed a Rule Set that you would like to use again in the future, with all the Rules visible in Section 4, enter a name fo rthe Rule Set in Section Six, and click the "Save Rule Set" Button.
                                    <br />
                                    If the name in the Current Rule Set Name text area belongs to a Rule Set you already own, it will overwrite the existing Rule Set.
                                    <br />
                                    Rule Sets are user-specific -- users can have different Rule Sets with the same name, and they are not shared between users.
                                    <br />
                                    You can access your Rule Set at any time by selecting it from the drop-down in Section 2.  
                                    <br />
                                </asp:TableCell>
                                <asp:TableCell ID="AdvQDetPictureCell">
                                    <asp:Panel ScrollBars="Auto" Width="600px" Height="600px" runat="server" Font-Size="X-Small" >
                                         <asp:Image runat = "server" ImageUrl="Images/Rule1.png" Width="600px"/>
                                        <br />1. Hit the Arrow Button to add rule.  <br /> (This rule finds all Encounters where the Patient's age was equal to 42) <br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/Rule2.png" Width="600px"/>
                                        <br />1. Rule has been added to current Rule Set.   <br />Each Row in Section 4 is a Rule in your current Rule Set.<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/Rule3.png"/>
                                        <br />1. Second Rule has been added to current Rule Set. <br />This Rule Set looks for all patients with a Date of Service on a charge between 10/1/2015 and 12/1/2015, who were 42 years old.<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/Rule4.png" Width="600px"/>
                                        <br />3. Hit the Pull Patients button to find all patients meeting the Rules listed in your currently viewed Rule Set.<br /><br /><br />
                                        <asp:Image runat = "server" ImageUrl="Images/Rule5.png" Width="500px"/>
                                        <br />3-4. The patients have been pulled, resulting in 2,478 Patients found.  <br />The count is visible under the Pull Patients button and all distinct Patient Account Facilities or Medical Record Numbers can be viewed in section 8, depending on which column you selected in Section 7.<br />
                                            Hit the "Limit and Return to SandBox" button in Section 9 to view the Pivots and filter by these patients.<br /><br /><br />
                                    </asp:Panel> 
                                  
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                                       </ContentTemplate> </cc1:TabPanel>
                                    <%--    <cc1:TabPanel runat="server" HeaderText="Advanced">

                                        </cc1:TabPanel>--%>
                                    </cc1:TabContainer>  




     </ContentTemplate>
     </cc1:TabPanel>
     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
