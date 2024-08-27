<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WFPPSInstructions.aspx.vb" Inherits="FinanceWeb.WFPPSInstructions" %>
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
                    <cc1:TabPanel runat = "server" HeaderText = "Wells Fargo PPS - Online Submittal Form" ID = "tpWFPPSSubmissionsMain" >
                    <ContentTemplate>      
    <asp:Panel runat = "server" Width = "100%"> 
    The Wells Fargo PPS Online Submittal Form is designed to take the place of the excel files in collecting and storing Wells Fargo PPS Deposit Information.
    <br /><br />
    First, the user needs to enter the Deposit Bag Number (and Deposit Slip Number, once it has become available) at the top, in the green text entry box.
    <br /><br />
    You'll notice that the Deposit Date is set only to today's date -- if a Deposit needs to be marked with a previous entry date, please contact an admin.
    <br /><br />
    The Deposit Bag Total will update automatically, based on your entries below.
    <br />
    <asp:Image runat = "server" ImageUrl="WellsFargoImages/Image1.png" Width="500px" />
    <br />
    <br />
    From the Deposit Location Dropdown, please select your Deposit Location.
    <br /><br />
    Each Deposit Bag is limited to only one Deposit Locations, and they will limit the dropdowns for your entries accordingly.
    <br />
    <asp:Image runat = "server" ImageUrl="WellsFargoImages/Image2.png" Width="500px" />
    <br />
    <br />
    Deposit Locations are grouped by Merchant IDs, so outlets such as GCS.A.Annex.Hospital are part of the Atlanta Location,
        <br /> and are not included in either of the GCS selections.     
    <br />
    <asp:Image runat = "server" ImageUrl="WellsFargoImages/Image3.png" Width="800px"/>
    <br />
    <br />
    Select the date for the EOD Collection Date in the first text area, the outlet from the dropdown, and the Total Cash and Manual Checks for that date at that outlet.
        <br /><br />
        The Outlet Total and the Deposit Bag Total at the top will update automatically after you enter values into the text areas.
        <br />
        <br />
        <asp:Image runat = "server" ImageUrl="WellsFargoImages/Image4.png" Width="800px"/>
    <br />
        Text entries will be ignored, and will cause a popup to inform you that your entry was invalid.
    <br />
    <br />
    <asp:Image runat = "server" ImageUrl="WellsFargoImages/Image5.png" Width="500px"/>
    <br />
    <br />
    If the Cash & Manual Checks to not agree with the EOD summary, please select "NO" from the dropdownlist.  
        <br />This will enable the text box to the right, and allow you enter an explanation for the discrepancy.
<br />
    <asp:Image runat = "server" ImageUrl="WellsFargoImages/Image6.png" Width="800px"/>
    <br />
    <br />
Hitting the Submit Bag at the top of the page will submit all rows with a valid WF PPS EOD Collection Date, <br />
         and will give you a popup saying how many rows were successfully submitted. <br /><br />
 Any rows without a valid WF PPS EOD Collection Date will be ignored; <br />
        This means that if you enter Cash or Manual Check numbers, they will alter totals in the Outlet Total boxes or the Deposit Bag Total box shown on the page, <br />but <b> their
        value will not be submitted, and the bag itself will not retain these values.</b>
<br />
<br />
If you would like to reset the page at any time, please use the Reset button in the top right corner of the page.   </asp:Panel>
     </ContentTemplate>
     </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Deposit Slip Ticket Number" ID="tpWFDSlip">
            <ContentTemplate>
                <asp:Image runat = "server" ImageUrl="WellsFargoImages/DepositSlip.png" Width="886px"/><br />
            </ContentTemplate>
        </cc1:TabPanel>
     <cc1:TabPanel runat = "server" HeaderText = "WF PPS Admin" Visible="false" ID = "tpWFPPSAdmin">
     <ContentTemplate>
     This page of the WF PPS tool is designed to allow Admins to make updates to any submitted rows, or to look over deposit totals.
    <br />
    <br />
         <asp:Image runat = "server" ImageUrl="WellsFargoImages/Admin1.png" Width="500px"/><br />
    To see data, you must first limit it by either Deposit Date, or EOD Collection date.  
         <br />This is done by selecting from the radio button in the top left, and entering a date in the text entry box.
    <br />
    In the example below, for instance, all sample rows with an EOD Collection date of June 3rd, 2015, 
         <br />are shown in the data grid, and their totals are shown in the top right.
    <br /><br />
    In the blue rectangle on the left, you can see all Deposit Bag Numbers associated with these rows.  
         <br />You can switch this to Deposit Slip Numbers by selecting from the drop-down at the top.
         <br /><br />
         If two different submissions have been made with the same Deposit Bag Number, <br />they will be differentiated by their Deposit Bag ID number, listed beside the Deposit Bag Number in parentheses. 
         <br /><br />
         If you uncheck a Deposit Bag Number (or Deposit Slip Number), it will disappear from the data table, <br />
          and its values will no longer contribute to the total in the top right.
         <br />
         Select All and Unselect All buttons are provided at the top of the blue rectangle to allow for fast changes to the checkboxes.
         <br />
         <asp:Image runat = "server" ImageUrl="WellsFargoImages/Admin2.png" Width="800px"/>
         <br />
         <br />

         In the bottom of the blue rectangle, you will see one more check box which says "See Full Bags".
         <br /><br />
         If you want to see all rows which make up the selected Deposit Bags, check this box.  <br />
         The rectangle on the left will still only display Bags which have at least one row from the selected Date limitation, 
         <br />but the data table will display all rows from the selected bags, regardless of whether or not they meet the selected Date limtiation.
         <br />
         <asp:Image runat = "server" ImageUrl="WellsFargoImages/Admin3.png" Width="800px"/>
         <br />
         <br />
         If you click on a row, it will populate the text entry boxes and drop down lists at the bottom of the page to the data pertaining to this row.  
         <br /><br />  You can delete a row with the Delete button, and update the row to the values in these text entry boxes and drop down lists with the Update Button.
         <br /><br />  If you click the Update button and your Deposit Bag Number, Deposit Slip Number, or Deposit Date have been altered, <br />you will see a popup asking whether you want
         to change these values for just this row or for the entire Deposit Bag.  <br />If you click "Full Bag", it will change these values on all rows with the same Deposit Bag ID -- 
         <br />so if another Deposit Bag has the same Deposit Bag Number, it will not be changed.
    <br />
   
<asp:Image runat = "server" ImageUrl="WellsFargoImages/Admin5.png" Width="500px"/>
    <br />
   
<br />
 
    
     </ContentTemplate>
     </cc1:TabPanel>
            
     </cc1:TabContainer>
    <br />
    </div>
    </form>
</body>
</html>
