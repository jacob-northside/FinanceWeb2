<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FinanceHelpDeskInstructions.aspx.vb" Inherits="FinanceWeb.FinanceHelpDeskInstructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
      

    <cc1:TabContainer ID="tcFinHelpDeskInstructions" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel runat = "server" HeaderText = "Finance Help Desk" ID = "tpMainPage" >
                    <ContentTemplate>    
    <asp:Panel ID ="MainPanelUser" runat="server" Width="100%">
        The Finance Help Desk is designed to help users communicate technical issues they encounter with the appropriate admins.  It allows the Finance Department to search for solutions to similar problems and to submit files to admins if they can't solve it themselves.
        <br /><br />
        This is the Finance Help Desk Main Dashboard.  The Finance Conference Room is currently logged in, and the according saved contact information has been filled in for the Contact Information.
        <br />
        There is empty space on the left below "Your Case Dashboard".  This means that the user who is logged in currently has no active cases.
        <br />
        At the bottom of the page are the user's most recent closed cases (up to ten), in gray rectangles across the bottom.  You can click on the case to expand it and take a brief look at the case summary.
        <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/UserDashboardImage1.png" Width="793px" /><br />
        <br />
        <br />        
        Please provide a short summary of your issue in the title box, and then add any information you believe might be helpful in resolving your case into the description box.
        <br />
        If there is additional user contact information that has not been filled in yet (for instance, E-Mail or Phone number) you can enter it here, beneath Contact Information.  <br />
        However, please be mindful that this information will not be remembered unless you update it on the User Profile tab.
        <br /><br />
        In order to submit a new case, the user must select the appropriate category from the Classification dropdown.  
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage2.png" Width="774px" /><br /><br />
        <br />Although the case will be reassigned to the appropriate admin if you select the wrong category, it will require additional time for your case to be resolved, so please make your best judgement.
        <br /><br />
        After filling in relevant case information, please hit the "Submit Case" button.  You will see a popup telling you whether or not your case has been submitted successfully.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage3.png" Width="778px" /><br />
        <br /><br />
        If your case has been submitted, it will now be available in your case dashboard on the left, in an orange box.
        <br />
        Before closing the popup, if you have any files or images you would like to attach to the case, so that the admin has access to relevant data or can see what is happening on your screen, hit the "Attach Files" button.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage4.png" Width="420px" /><br />
        <br />
        The Case Attachments popup will show up, giving you a brief description of your case.  Hit the browse button to select a file, and the Upload button to attach it once it has been selected.
        <br />
        Submitted files will appear in a table below the case information.  
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage5.png" Width="421px" /><br />
        <br />
        Images will provide a small preview of what the picture looks like, and you can download all successful attachments using the respective Download buttons to the right.
        <br /><br />
        Returning to the Help Desk Case Dashboard, you can click on the case you submitted to expand the case summary.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage6.png" Width="628px" /><br />

        <br />
        The title of the case will be the very first thing visible in the header box.  Below that will be the name of the admin who has been assigned to your case; this may change if it gets reassigned.
        <br />
        Your contact information will be visible after the case summary has been expanded, and you can scroll down to see relevant case information.<br />
        If notes or solutions have been added to the case as it is worked on, they will be visible here.


    </asp:Panel>  
    <asp:Panel ID="MainPanelAdmin" Visible="false" runat = "server" Width = "100%"> 
         The Finance Help Desk is designed to help admins keep track of communicate technical issues their users encounter.  It allows the admins to search for solutions to similar problems and to download files users have provided that are relevant to the case.
        <br /><br />
        This is the Finance Help Desk Main Dashboard.  Chelsea Weirich is currently logged in, and her saved contact information has automatically been filled in.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage1.png" Width ="785px" /><br /><br />
        On the left below "Your Case Dashboard", you can see all of her active cases.  This includes both cases that she submitted to other adminsand that have been submitted to her.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage6.png" Width ="700px" /><br /><br />
        <br />
        You will note that the cases are grouped in three distinct colors: red, orange, and yellow.  These colors specify the priority of the case (High, medium, or low, respectively).  
        <br />All cases submitted by non-admin users are initally considered medium priority cases.
        <br /><br /> Cases on the dashboard are organized first by priority and then by when they were opened, with the most recent being on the top.
        <br />
        At the bottom of the page are the user's most recent closed cases (up to ten), in gray rectangles across the bottom.  You can click on the case to expand it and take a brief look at the case summary.
        <br />
        <br />
        <br />       
        Admins can submit cases for users other than themselves.   You can select a user from the dropdown, which is sorted alphabetically by user name.  
        <br />The dropdown itself shows the userlogin and user name information as it has been submitted to the help desk.
        <br />
        <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage2.png" Width ="775px" /><br />
        <br />
        <br />
        <br />  All information stored in the FinanceHelpDesk database for this user will be filled into the contact information automatically.
        <br />If there is additional user contact information that has not been filled in yet (for instance, E-Mail or Phone number) you can enter it here, beneath Contact Information.  <br />
        However, please be mindful that this information will not be remembered unless they updated it on their User Profile tab.
        <br /><br />
        In order to submit a new case, the user must select the appropriate category from the Classification dropdown.  
        <br /> Selecting a category will automaticaly select the respective admin from the Assign To dropdown below.  You may override this if you believe it should be sent to a different rep.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage3.png" Width ="773px" /><br />
        Please provide a short summary of your issue in the title box, and then add any information you believe might be helpful in resolving your case into the description box.
        <br />
        After filling in relevant case information, please hit the "Submit Case" button.  You will see a popup telling you whether or not your case has been submitted successfully.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage4.png" Width="774px" /><br />
        <br /><br />
        If your case has been submitted, it will now be available in your case dashboard on the left.  In this instance, the priority was left as the defualt (Medium), so it is an orange case.
        <br />
        Before closing the popup, if you have any files or images you would like to attach to the case, so that the admin has access to relevant data or can see what is happening on your screen, hit the "Attach Files" button.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage5.png" Width="776px" /><br />
        <br />
        The Case Attachments popup will show up, giving you a brief description of your case.  Hit the browse button to select a file, and the Upload button to attach it once it has been selected.
        <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage4.png" Width="420px" /><br />
        <br />
        Submitted files will appear in a table below the case information.  
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/UserProfileImage5.png" Width="421px" /><br />
        <br />
        Images will provide a small preview of what the picture looks like, and you can download all successful attachments using the respective Download buttons to the right.
        <br /><br />
        Returning to the Help Desk Case Dashboard, you can click on the case you submitted to expand the case summary. <br />
        You can see the details in the blue panel beneat the header.  Click the header again if you want to close it.
        <br /><br />
         <asp:Image runat = "server" ImageUrl="Images/AdminDashboardImage7.png" Width="700px" /><br />

        <br />
        The title of the case will be the very first thing visible in the header box.  
        <br />
        The contact information of the user who submitted the case will be visible after the case summary has been expanded, and you can scroll down to see relevant case information.<br />
        If notes or solutions have been added to the case as it is worked on, they will be visible here.
      </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
     <cc1:TabPanel Visible="false" runat = "server" HeaderText = "View Open Cases" ID = "tpOpenCases">
     <ContentTemplate>
     This page of the Finance Help Desk is designed to allow admins to interact with and update the cases they have been assigned.
    <br />
    <br />
    The table on the left will show all open cases assigned to the admin selected in the dropdown at the top.  By default, this will be the admin currently logged in.  
    <br />
    <br />
    The cases will be organized first by priority, following the same red, orange, yellow (high, medium, low) scheme as the main page and second by Start Date, with the most recent at the top.  
    <br />
    <br />
        <asp:Image runat = "server" ImageUrl="Images/OpenCasesImage1.png" Width="773px" /><br />
    <br />
    Select a case by clicking on it.  This will highlight the row you have selected in a darker version of the same color, and it will fill case data into the fields in the Update Case display to the right.
<br />
 <br />
          <asp:Image runat = "server" ImageUrl="Images/OpenCasesImage2.png" Width="773px" /><br />
    <br />
         Make any changes or updates to the case selected and hit the "Save Case" button.<br />
         In this instance, I am updating the case titled "Low Priority" and changing its priority level from medium to low.  
         <br />You'll notice that as soon as the popup displays "Successfully Submitted Case", the row has been moved from the orange to the yellow section.
         <br /><br />
          <asp:Image runat = "server" ImageUrl="Images/OpenCasesImage3.png" Width="777px" /><br />
         <br />
         All cases with attachments will have an icon to the left of the ID in the grid to the left. <br />
          If you select that case in the grid and then hit the view attachments button, the Case Attachments page will popup.  <br /><br />
         <asp:Image runat = "server" ImageUrl="Images/UserProfileImage5.png" Width="421px" /><br /><br />

         Admins can also attach files - Hit the browse button to select a file, and the Upload button to attach it once it has been selected
         <br />
          Submitted files will appear in a table below the case information.  
         <br />
          Images will provide a small preview of what the picture looks like, and you can download all successful attachments using the respective Download buttons to the right.
     </ContentTemplate>
     </cc1:TabPanel>
                 <cc1:TabPanel runat = "server" HeaderText = "Search Cases" ID = "tpSearchCases" >
                    <ContentTemplate>      
    <asp:Panel ID="pnlSearchUser" runat = "server" Width = "100%"> 
        This is the Search Cases Tab.
        <br />
        For a basic search, enter the keywords you're looking for in the textbox provided, and then hit the Search Keywords button.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage1.png" Width="790px" /><br />
        <br />
        You can perform an advanced search by hitting the Adv link to the right of the Search Keywords button.  This will make several advanced search options available.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage2.png" Width="789px" /><br />
        <br />
        Dropdowns will not limit the search if they are set to '(Optional)'. 
        <br />
        <br />
        User filters (Submitted By, Entered By, or Assigned To) can be limited either by the specific User, by selecting them in a dropdown, or can be searched via a Text Search.
        <br /> To do this, select '(Text Search)' in the dropdown, and then type your filter in the text area that becomes available.
        <br /><br />
        The basic search automatically searches for matches agaist the title, description, notes, and solution, which is how the Advanced Search also defaults.  <br />
        You can choose where the Advanced Search looks for its keyword matches using the checkboxes in the Contains panel.
        <br />
        <br />
        If you want to add a date filter, select an option from the dropdown, and then select a starting and ending date in the text areas that become available.
        <br />
        Checking the Additional checkboxes at the bottom adds in a filter on either having notes or having attachments.  Leaving them unchecked does not filter on anything.
        <br />
        <br />
        After you hit the "Search Keywords" button or the "Advanced Search" button, the tool will search through all cases for the ones fitting your keywords most closely.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage3.png" Width="790px" /><br />

        <br /> The tool's best guess for the cases that meet your search criteria will initally be at the top of the table.
        <br /> You can sort the cases by one of the other columns by clicking the header names ot the top fo the table.
        <br /><br /> Keywords you Searched on will be bolded and highlighted in blue.

        <br />
        <br />
        If you want to look into the details of a specific case, and you know its case number, you can enter it into the text area in the top right corner and hit the "View Case Number" button.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage4.png" Width="790px" /><br />
        <br />
        You will see a popup with the details of the case number entered, and if it's one of your cases, you can make edits to the case information, add it to the knowledge base so other users can search for it, or view the attachments.

     </asp:Panel>
     <asp:Panel ID="pnlSearchAdmin" Visible="false" runat = "server" Width = "100%"> 
        This is the Search Cases Tab.
        <br />
         When performing a search, non-admin users will only be able to see their own cases, or cases who were entered into the Knowledge Base, using the checkbox on the View Open Cases tab.
        <br />         For a basic search, enter the keywords you're looking for in the textbox provided, and then hit the Search Keywords button.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage1.png" Width="790px" /><br />
        <br />
        You can perform an advanced search by hitting the Adv link to the right of the Search Keywords button.  This will make several advanced search options available.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage2.png" Width="789px" /><br />
        <br />
        Dropdowns will not limit the search if they are set to '(Optional)'. 
        <br />
        <br />
        User filters (Submitted By, Entered By, or Assigned To) can be limited either by the specific User, by selecting them in a dropdown, or can be searched via a Text Search.
        <br /> To do this, select '(Text Search)' in the dropdown, and then type your filter in the text area that becomes available.
        <br /><br />
        The basic search automatically searches for matches agaist the title, description, notes, and solution, which is how the Advanced Search also defaults.  <br />
        You can choose where the Advanced Search looks for its keyword matches using the checkboxes in the Contains panel.
        <br />
        <br />
        If you want to add a date filter, select an option from the dropdown, and then select a starting and ending date in the text areas that become available.
        <br />
        Checking the Additional checkboxes at the bottom adds in a filter on either having notes or having attachments.  Leaving them unchecked does not filter on anything.
        <br />
        <br />
        After you hit the "Search Keywords" button or the "Advanced Search" button, the tool will search through all cases for the ones fitting your keywords most closely.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage3.png" Width="790px" /><br />

        <br /> The tool's best guess for the cases that meet your search criteria will initally be at the top of the table.
        <br /> You can sort the cases by one of the other columns by clicking the header names ot the top fo the table.
        <br /><br /> Keywords you Searched on will be bolded and highlighted in blue.

        <br />
        <br />
        If you want to look into the details of a specific case, and you know its case number, you can enter it into the text area in the top right corner and hit the "View Case Number" button.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/SearchCasesImage4.png" Width="790px" /><br />
        <br />
        You will see a popup with the details of the case number entered. <br /> Admins can view all cases, however non-admins can only view their own cases and cases entered into the Knowledge Base.<br />
        In this popup, you can make edits to the case information, add it to the knowledge base so other users can search for it, or view the attachments.

     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
         <cc1:TabPanel runat = "server" Visible="false" HeaderText = "Administrative" ID = "tpAdministrative" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    The Administrative tab is designed to allow admins to manage available category and department lists.
        <br />
        The admin can switch between the two with the dropdown at the top.
        <br />
        <br />
        <asp:Image runat = "server" ImageUrl="Images/Administrative.png" Width="786px" /><br />
    <br />
        You can add a new category or department in the text area at the top; Categories are required to have a default Representative that will be assigned to the cases for that Category.
        <br />
        <br />
        Existing rows can be edited or deleted in the grid below by clicking the respective buttons.
    <br />
   


     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
        <cc1:TabPanel runat = "server" HeaderText = "User Profile" ID = "tpUserProfile" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    
        Welcome to the Northside Hospital Finance Division Finance Help Desk.
        <br />
        The first time you visit the Help Desk, you will be redirected to this tab, and you may note that the first tab (Finance Help Desk) is grayed out.
        <br />
        Please enter your User Profile information in the text areas provided, and select your Department from the dropdown.
        <br />
        The purpose of this information is to allow Help Desk representatives to get in touch with you regarding any cases you submit.
        <br /><br />
        <asp:Image runat = "server" ImageUrl="Images/userProfileImage1.png" Width="792px" /><br />
        <br />
        When you hit the "Update Profile" information, all data you have entered will be stored so you don't have to re-enter it the next time you visit the Help Desk.
        <br />
        After you have updated your profile for the first time, the <asp:LinkButton ID="lbViewMainTab" runat="server">Finance Help Desk main tab</asp:LinkButton> will become enabled, and you will automatically be redirected there, where you can submit a case to the Help Desk.
   


     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>

     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
