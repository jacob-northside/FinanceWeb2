<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ARSnapShotsInstructions.aspx.vb" Inherits="FinanceWeb.ARSnapShotsInstructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
      

    <cc1:TabContainer ID="tcARSnapShotInstructions" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"   >
                            <cc1:TabPanel runat = "server" HeaderText = "<b>AR Dashboard</b>" ID = "tpARDashBoard"  >
                    <ContentTemplate>    
    <asp:Panel ID ="Panel1" runat="server" Width="100%">
        This is the AR SnapShots Dashboard.  It is designed to provide you with a quick summary of how AR has changed since the last month end.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboard.png" Width="1400px" />
        <br /><br />
        The left-most panel is designed for navigation throughout the AR Snapshots Tool.  The page you are currently located on will be bolded, and all the other buttons will navigate you to their corresponding tab.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboardNav.png" Width="222px" /><br />
        <br />
        <br />        
        The cross tables in the top left provide a brief summary of the facility-based statistics for the total Account Balance located in AR both yesterday (the most recent SnapShot date) and at the most recent month end.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboard2.png" Width="320px" /><br />
        <br />
        The Pie Charts to the right tell how long the accounts have been located in AR.  THe sizes are determined by the total Account Balance, and the colors reference the number of days the account has been in AR, with a legend located on the far right.

        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboard3.png" Width="1100px" /><br />
        <br />
        The bottom left cross table shows the Payors who have had the largest changes in AR since the most recent month end.  This is determined by taking the absolute value of the differences of the two Total Account Balances.
        <br />
        The +/- to the right of the AR Difference shows whether it has gone up (+) or down (-) since the month end, and is highlighted in either green or red, respectively, for easy reference.
        <br />
        <br />
        The area between the cross-tables with the blue background allows the user to select which facilities they want to look at, and to select a Grouping to use for the Tree Map located in the middle as well as the AR Difference cross table.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboard4.png" Width="1100px" /><br />
        <br />
        The bar graph at the bottom shows the change in AR over the last 18 months (or as far back as the data allows), and is split out by Facility.  
        <br /><br />

        <br />
        <br />
    </asp:Panel>  
     </ContentTemplate>
     </cc1:TabPanel>

                    <cc1:TabPanel runat = "server" HeaderText = "<b>AR SnapShots</b>" ID = "tpMainPage"  >
                    <ContentTemplate>    
    <asp:Panel ID ="ARSnapShots" runat="server" Width="100%">
        This is the main page for the AR SnapShots tool.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots1.png" Width="1400px" />
        <br /><br />
        It displays the Total Balance for all patients in AR or PA on a specific day in a cross table format.  
        <br />The balances are sorted into AR Date Buckets based on the number of days between the Final Bill date and the date when the SnapShot was taken.  
        <br />
        By default, you will see the SnapShot from yesterday.  
        <br />If you want to look at the data from a different date, enter the desired SnapShot Date into the text entry box at the top left, or click the button to the right and select your date from the calendar provided.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots2.png" Width="364px" /><br />
        <br />
        <br />        
        There are several vertical groupings provided in the dropdown on the left.  Select your desired grouping, and it will automatically update the cross-table accordingly.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots3.png" Width="600px" /><br />
        <br />
        Please note that currently, the data is grouped such that all of the balance for a patient is placed into the first Insurance Plan the patient has with a non-zero balance on the day of the SnapShot.
        <br />i.e. A Patient has a $1000 Balance.  InsurancePlan1 has a $0 Balance, InsurancePlan2 has a $300 Balance, InsurancePlan3 has a $500 Balance, and InsurancePlan4 has a $100 Balance.
        <br /> In this circumstance, all $1000 will be relegated to InsurancePlan2.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots4.png" Width="600px" /><br />
        <br />
        The filters on the left are synchronized with the filters on the right, and will limit down the data you view in the cross table.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots5.png" Width="1400px" /><br />
        <br />
        If you click on a cell (or multiple cells, either by holding down your mouse button and selecting an area or holding down the ctrl key as you click multiple cells), 
        <br />it will highlight the cell in a darker blue and display encounter-level information for the selected information in the table below.
                <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots6.png" Width="1400px" /><br />
        <br />
        The filters on the right are organized into groups.  SnapShot Filters includes the basic filters available for most tabs available on this tool.  
         <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots7.png" Width="206px" /><br />
        <br />
        You can expand or hide the group by clicking the + symbol next to the group name.
         <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShots8.png" Width="200px" /><br />
        <br />
    </asp:Panel>  
     </ContentTemplate>
     </cc1:TabPanel>
     <cc1:TabPanel runat = "server" HeaderText = "<b>AR SnapShots Raw Data File</b>" ID = "tpRawData">
     <ContentTemplate>
     This page of the AR SnapShot tool is designed to allow the user to view all encounter-level data that was captured on the date selected from the AR SnapShot page.

     <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsRawData1.png" Width="1400px" /><br />
        <br />
        <br />
        The left-most panel is designed for navigation throughout the AR Snapshots Tool.  The page you are currently located on will be bolded, and all the other buttons will navigate you to their corresponding tab.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboardNav.png" Width="222px" /><br />
        <br /><br />

         You can filter the Raw Data either using the filters available on the right, or by selecting from the DropDown list provided at the top.  
         <br />This DropDown list shows all the groups that can be selected in the vertical groupings in the Cross Table on the AR SnapShot page.
          <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsRawData2.png" Width="532px" /><br />
        <br />
         You can then select your desired group filter from the second DropDown list.
          <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsRawData3.png" Width="515px" /><br />
        <br />
         If neither DropDownList is set to the '(None)' option, the data will be filtered accordingly. 
          <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsRawData4.png" Width="1400px" /><br />
        <br />

     </ContentTemplate>
     </cc1:TabPanel>
                 <cc1:TabPanel runat = "server" HeaderText = "<b>AR SnapShots -- Custom Buckets</b>" ID = "tpCustomBuckets" >
                    <ContentTemplate>      
    <asp:Panel ID="pnlSearchUser" runat = "server" Width = "100%"> 
        This page of the AR SnapShot tool is designed similarly to the AR SnapShot page.
                <br /><br />
        The left-most panel is designed for navigation throughout the AR Snapshots Tool.  The page you are currently located on will be bolded, and all the other buttons will navigate you to their corresponding tab.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboardNav.png" Width="222px" /><br />
        <br />
        <br />
        However, instead of providing the user with the standard AR Date Bucket layout, it allows users to create their own bucket sizes.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsCustomBuckets1.png" Width="1400px" /><br />
        <br />
        Enter a positive integer in the text box provided, click anywhere outside the text box, and the Cross Table will automatically adjust to your desired Bucket size.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsCustomBuckets2.png" Width="1400px" /><br />
        <br />
        Please note that invalid bucket sizes (0 or lower) will be sorted into (PA), 0-360, and >360 Buckets. 
        <br />
        <br />
        

     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
         <cc1:TabPanel runat = "server" HeaderText = "<b>AR SnapShot -- Variance Comparison</b>" ID = "tpVarCompare" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    This page of the AR SnapShot tool is designed to allow the user to compare Balances between two dates.
                <br /><br />
        The left-most panel is designed for navigation throughout the AR Snapshots Tool.  The page you are currently located on will be bolded, and all the other buttons will navigate you to their corresponding tab.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboardNav.png" Width="222px" /><br />
        <br />
        <br /> Select your desired dates in the text entry boxes in the top left.
        <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsVarCompare1.png" Width="1400px" /><br />
    <br />
        Initially, you will see the % Variance between the two days.  
        <br />This is calculated by subtracting the Account Balance on Date 1 from the Account Balance on Date 2, and then dividing by the Account Balance on Date 1.
        <br />
        <br />
        There is a DropDown List which is initially set to "Show % Variance Only".  
        <br />If you select the other option ("Show Full Comparison"), the cross table will show the Total Balance on each date, the dollar amount of the variance between them, as well as the % Variance.
        <br /> You can switch it back by changing the selection in the DropDown List again.
                <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsVarCompare2.png" Width="1400px" /><br />
    <br />
    <br />
        You may see cells highlighted in green or red.  These point out the exceptionally high or low variances between the two dates.  
        <br /> Initially, this deviation is set to plus or minus 25%.
        <br /> The user can change this by setting a different value in the % Variance Deviation text entry box.
                         <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsVarCompare3.png" Width="1400px" /><br />
    <br />
    <br />
        Please be aware when using the filters on the right, that if you de-select "Include empty values" in either Account Balance -- Date 1 or Account Balance -- Date 2, you will be limiting the data so that it only sees the Account Balance from that day.
        <br />  All Variances will thus be -100% if you de-select on the Date 1 filter, or empty if you de-select on the Date 2 filter.
                                 <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsVarCompare4.png" Width="1400px" /><br />
    <br />
     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
        <cc1:TabPanel runat = "server" HeaderText = "<b>AR SnapShot -- Encounter Comparison</b>" ID = "tpEncCompare" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    
        This page of the AR SnapShot tool is designed to allow the user to compare Encounter-Level Balances between two dates.
                <br /><br />
        The left-most panel is designed for navigation throughout the AR Snapshots Tool.  The page you are currently located on will be bolded, and all the other buttons will navigate you to their corresponding tab.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboardNav.png" Width="222px" /><br />
        <br />
        <br /> Select your desired dates in the text entry boxes in the top left.
                                       <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsEncCompare1.png" Width="1400px" /><br />
    <br />
        Initally, it will filter the data to only show encounters where the absolute difference between the Account Balance on the two dates is larger than $100,000.00
        <br /><br />
        You can change this by updating the value in the text entry box provided beneath the words "View Accounts with a difference greater than:"
                                       <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsEncCompare2.png" Width="1400px" /><br />
    <br />
        The user can select a row by clicking on it, and more details can then be seen in the data table at the bottom of the screen.
                                               <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsEncCompare3.png" Width="1400px" /><br />
    <br />
     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
                <cc1:TabPanel runat = "server" HeaderText = "<b>Available SnapShot Dates</b>" ID = "tpSSD" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    
        This page of the AR SnapShot tool is designed to allow the user to view which dates SnapShots are available for, as well as allowing the user to see the Total Balance on that date at a glance.
                                       <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/ARSnapShotsSSD.png" Width="1400px" /><br />

    <br />
        Dates which are not available will be flagged with a "No" under the Pulled column, and their Balances should be negative.
        <br /> Dates listed go from 7/15/2015, when the SnapShots first began to today, which will not be available yet.
                <br /><br />
        The left-most panel is designed for navigation throughout the AR Snapshots Tool.  The page you are currently located on will be bolded, and all the other buttons will navigate you to their corresponding tab.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/ARDashboardNav.png" Width="222px" /><br />
        <br />
     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
