<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProFeeInstructions.aspx.vb" Inherits="FinanceWeb.ProFeeInstructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager runat="server"></asp:ScriptManager>
      
    <cc1:TabContainer ID="ProFeeToolTabs" runat="server" 
            ActiveTabIndex = "0" UseVerticalStripPlacement = "False"  >
                    <cc1:TabPanel Visible="false" runat = "server" HeaderText = "Schedule Updates" ID = "ProFeeMainSchedules" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    The Pro Fee Update tool is designed to allow the user to update the Effective Date, End Date, Default description and Manager’s Notes to any of the Fee Schedules stored in the database.
    <br />
    <br />
    To select the Fee Schedule to update, the user must first select a Payor from the initial drop down list.  
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/Image1.png" Width="500px" />
    <br />
    <br />
    This will limit the “Select Fee Schedule:” dropdown below to only list Schedule types that the selected Payor has.  If the user does not select a Fee Schedule from this dropdown, it will not filter by Schedule type.
    The final DropDown, "Select TINs", will only list TIN combinations from the selected Payor and Fee Schedule. If the user does not select a TIN List from this dropdown, it will not filter by TINs.  
    The TIN filter limits Fee Schedules to those who have the exact same TINs as listed in the dropdown.  
    For example, if you select "GCS" in the dropdown, a Fee Schedule which has both ACC and GCS as selected TINs will not show up.
    <br />
    <br />
    Below the dropdowns, it will display all Fee Schedule descriptions for that particular Payor (or Payor and Schedule) in a table.  Fee Schedules can be selected by clicking on the desired row.  The selected row will be highlighted in blue, and its update information will be displayed in the information underneath.
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab1Image2.png" Width="800px"/>
    <br />
    <br />
    Update Information displays first the labels of the Payor, Fee Schedule and TINs from the selected row.  Below these labels are the fields the user can update: starting and end dates, Default, and Manager’s Notes.  Default and Manager’s Notes are free text boxes, updated through typing.  The Date range has drop downs, displayed by clicking on the text box, which will allow the user to select a date from a calendar display.
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab1Image3.png" Width="500px"/>
    <br />
    <br />
    To the right of these entry fields is a box labeled “TINs:”
This will by default, check all TINs belonging to the Fee Schedule, as viewed in the label to the left.  All leftover TIN information will be displayed in the Other textbox at the bottom.
<br />
<br />
After selected, deselecting, or updating the other Free Text box, the user can commit all modified information to the database by clicking the “Update” button to the right of the TINs box.  A pop-up will then tell alert the user whether it has been successfully updated.
<br />
<br />
There are some circumstances in which there are multiple fee schedules for the same Payor, Schedule Type and date range.  If these cannot be differentiated by either the Default or ManagersNotes column, the page will give a warning and a selection table.
<br />
<br />
<asp:Image ID="Image1" runat = "server" ImageUrl="ProFeeImages/ProFeeTab1Image4.png" Width = "600" />
<br />
<br />
The selection table will display the attributed TINs as well as sample of the row details for each schedule.  By clicking on any of the ten rows for each Fee Schedule, you will select that particular Fee Schedule, and the Update Information will change accordingly. The warning banner and the red borders will also disappear.
     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
     <cc1:TabPanel Visible="false" runat = "server" HeaderText = "TIN Updates" ID = "ProFeeTINSchedules">
     <ContentTemplate>
     This page of the ProFee Update tool is designed to allow the user to update the Effective Date, End Date, Default description and Manager’s Notes to a single TIN for any of the Fee Schedules stored in the database.
    <br />
    <br />
    To select the TIN/Fee Schedule combination to update, the user must first select a TIN from the initial drop down list.  
    <br />
    <br />
    This will limit the “Select Payor:” dropdown below to only list Payors using the selected TIN.  If the user does not select a Payor from this dropdown, it will not filter by Payor.
    <br />
    <br />
    Below the dropdowns, it will display all Fee Schedule descriptions for that particular TIN (or TIN and Payor) in a table.  Fee Schedules can be selected by clicking on the desired row.  The selected row will be highlighted in blue, and its update information will be displayed in the information underneath.
    <br />
<asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab2.png" Width = "600" />
    <br />
    Update Information displays first the labels of the TIN, Payor and Fee Schedule from the selected row.  Below these labels are the fields the user can update: starting and end dates, Default, and Manager’s Notes.  Default and Manager’s Notes are free text boxes, updated through typing.  The Date range has drop downs, displayed by clicking on the text box, which will allow the user to select a date from a calendar display.
    <br />
    
<br />
After selected, deselecting, or updating the other Free Text box, the user can commit all modified information to the database by clicking the “Update” button to the right of the TINs box.  This will only update the Fee Schedule information for that particular TIN; a different TIN following the same Fee Schedule will retain its previous information.  A pop-up will then tell alert the user whether it has been successfully updated.
<br />
<br />
 
    
     </ContentTemplate>
     </cc1:TabPanel>
                 <cc1:TabPanel Visible="false" runat = "server" HeaderText = "Galen Contract ID LU" ID = "GalenInstructions" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    The Galen Contract ID LU Update tool is designed to allow the user to quickly fill in missing STARINSPLANCODEs from the ContractID_Galen_LU data table.
    <br />
    <br />
    The initial information displayed is the first 100 rows that are missing STARINSPLANCODEs, ordered by INSPLAN.  
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab3Image1.png" Width="650px" />
    <br />
    <br />
    Clicking on a row will highlight that row in blue and display information in a grid below containing the twenty-five "best guesses" at what the most similar non-blank rows are.  The closest matches will be on top.  
    <br />
    <br />
    Above this gridview is a text box for the STARINSPLANCODE and an update button.  The text box will automatically be filled with the STARINSPLANCODE from the selected row in the bottom grid, though the user can input their own value if none of the suggestions are what they are looking for.
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab3Image2.png" Width="800px"/>
    <br />
    <br />
    The Update button will update all checked rows with the STARINSPLANCODE in the textbox to the left.  Other empty values in the row will be automatically updated via a trigger in the database itself.
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab3Image3.png" Width="650px"/>
    <br />
    <br />
    The user may also choose to limit the rows they are updating based on INSPLAN, using the dropdown.
<br />
<br />
The grid will still only display the top 100 empty rows, but they will now be filtered by INSPLAN.  If the user wishes to update more than one hundred rows with the same INSPLAN, it will require multiple transactions.
<br />
<br />
<asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab3Image4.png" Width = "600" />
<br />
<br />
The check box at the top of the column allows the user to select or unselect all rows currently in the gridview for speed and easy updating.  Please note that doing this will highlight the bottom row, and populate the grid below accordingly.
<br />
<br />
<asp:Image  runat = "server" ImageUrl="ProFeeImages/ProFeeTab3Image5.png" Width = "800" />

     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
         <cc1:TabPanel runat = "server" HeaderText = "Galen STARINSPLAN" ID = "TabPanel1" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    The Galen STARINSPLAN Update tool is designed to allow the user to export data from the ContractID_Galen_LU data
        <asp:Label runat="server" Visible="false" Text=" or change the STARINSPLANCODE from an existing row in the ContractID_Galen_LU data table" ID="lblEdit1" ></asp:Label>.
    <br />
    <br />
    Initially, the gridview has all data available, both to view and export with the "Export to Excel" button in the upper right.  
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image1.png" Width="650px" />
    <br />
    <br />
    Each page initially contains 50 rows.  You can change this to 100 or 250 rows per page using the dropdown on the right.<br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image2.png" Width="790px"/>
        <br /><br />
        You can switch to a different page by scrolling to the bottom of the gridview and clicking on one of the page numbers in the bottom left corner.   
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image3.png" Width="800px"/>
    <br />
    <br />
    The data can be limited in three ways.  Data Limiting works both for the data on the page and the data you export by clicking the "Export to Excel" button.
        <br />
    First, it can be limited by the date range, with the checkbox in the upper left hand corner.  If this is unchecked, all data is available.  
    If it is checked, only data that is currently active will be shown.  "Currently active" implies either today's date falls between the starting and ending date, or the ending date is empty.
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image4.png" Width="650px"/>
    <br />
    <br />
    The user may also choose to limit the rows they are updating based on INSPLAN_NAME, IDGroup, or both, using the dropdowns at the top.
    Selecting an INSPLAN_NAME will limit the second dropdown, IDGroup, to only IDGroups for that INSPLAN_NAME.  If you select the IDGroup first, all INSPLANs will remain available, and if you choose one, the IDGroup filter will be reset to the "-- (optional) --" selection, leaving all data available.
<br />
<br />
<asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image5.png" Width = "600" />
<br />
<br />
        <asp:Panel runat="server" ID="pnlEdit1" Visible="false">
If you would like to edit the data, hit the "Edit Insplans" checkbox next to the "Open Instructions" button at the top right.  
        This will change your view, and display a table at the bottom showing information about the selected row.
<br />
<br />
<asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image6.png" Width = "600" />
<br />
<br />

After selecting a row by clicking on it, the data will be visible in the bottom.  When you hit the Update button, the STARINSPLANCODE for this row will be changed to the value entered in the STARINSPLANCODE textbox.
<br />
<br />
<asp:Image runat = "server" ImageUrl="ProFeeImages/ProFeeTab4Image7.png" Width = "600" />
<br />
<br />


        </asp:Panel>
        
     </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>


     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
