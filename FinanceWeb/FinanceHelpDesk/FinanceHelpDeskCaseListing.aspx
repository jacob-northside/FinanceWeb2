<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FinanceHelpDeskCaseListing.aspx.vb" Inherits="FinanceWeb.FinanceHelpDeskCaseListing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

      <script>
            function open_win(id) {

            var url = "https://financeweb.northside.local/FinanceHelpDesk/FinanceHelpDeskCaseAttachments/?CaseNo=" + id;
            myWindow = window.open(url, 'FinanceHelpDeskAttachments', 'height=700,width=620, scrollbars, resizable');
            myWindow.focus();

        }


    </script>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
        
        <asp:UpdatePanel runat="server" ID="updates">
            <ContentTemplate>
    <asp:Panel runat="server" Visible ="false" ID="FullPanel">

<asp:Table  runat="server" Width="600px" CellPadding ="0" CellSpacing ="0" CssClass ="collapsetable">
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top">
                    <asp:Table runat="server" Width="100%"  ID="Table1" CellPadding ="0" CellSpacing ="0"  CssClass ="collapsetable">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan ="6" BackColor="#6da9e3" ForeColor="White">
                                <b>Details for Case</b>
                                <asp:Label runat="server" ID="lblCaseNumber"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                       <asp:TableRow >
                            <asp:TableCell width="50px"></asp:TableCell>
                            <asp:TableCell><b>Case ID:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblCaseNumber2"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>User Name:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblUserName"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>E-Mail:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblEmail"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell><b>Phone:</b> </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblPhone"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>IP Address:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblIPAdd"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Computer Name:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblCompName"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Start Date:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblStartDate"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Close Date:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblCloseDate"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Department:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblDept"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell><b>Category:</b> </asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblCategory"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell><b>Assigned To:</b> </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblAssignedTo"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell><b>Status:</b> </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow >
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell> </asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>
                                
                            </asp:TableCell>
                            <asp:TableCell ForeColor ="Red"></asp:TableCell>
                            <asp:TableCell ></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell Width="3px" BackColor="White"></asp:TableCell>
                      </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"><b>Case Information</b></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell ColumnSpan ="3">
                                     <asp:Table runat="server" Width="100%">
                                         <asp:TableRow HorizontalAlign="Center">
                                             <asp:TableCell></asp:TableCell>
                                             <asp:TableCell><b>Title:</b>&nbsp;&nbsp;<asp:TextBox runat="server" Visible="false" ID="txtUpdateTitle" Width="300px"></asp:TextBox>
                                                 <asp:Label runat="server" ID="lblTitle" Width="300px" Visible="true"></asp:Label>
                                                 &nbsp;
                                                 <asp:Label runat="server" ForeColor="Red"></asp:Label></asp:TableCell>
                                             <asp:TableCell ForeColor="Red"></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="3"  HorizontalAlign ="Center"><b>Description:</b><asp:Label runat="server" ForeColor="Red" Text=""></asp:Label></asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow>
                                             <asp:TableCell ColumnSpan="3"  HorizontalAlign ="Center"><asp:TextBox Visible="false" runat="server" TextMode = "MultiLine"  Rows="5" Width="90%" ID="txtUpdateDesc"></asp:TextBox>
                                                 <asp:Label runat="server" Width="90%" ID="lblDesc"></asp:Label>
                                             </asp:TableCell>
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                          <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"><b>Notes:</b></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center"><asp:Label runat="server" Width ="90%" ID="lblUpdateNotes"></asp:Label></asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                          <asp:TableRow ID="trAdditionalNotes" Visible="false">
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"><b>Enter Additional Notes:</b></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow ID="trAdditionalNotes2" Visible="false"><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center">
                                                 <asp:TextBox runat="server" TextMode = "MultiLine"  Rows="5" Width ="90%" ID="txtUpdateNewNotes"></asp:TextBox>
                                             </asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                         <asp:TableRow><asp:TableCell HorizontalAlign="Center"><asp:CheckBox ID="chbHideNotes" runat="server" Text="Hide From End User" Checked="false" /></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                                         <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3"><b>Solution:</b></asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow><asp:TableCell HorizontalAlign ="Center" ColumnSpan ="3">
                                     <asp:Table runat="server" Width ="100%">
                                         <asp:TableRow>
                                             
                                             <asp:TableCell HorizontalAlign="Center">
                                                 <asp:TextBox Visible="false" runat="server" TextMode = "MultiLine"  Rows="5" Width ="90%" ID="txtUpdateSolution"></asp:TextBox>
                                                 <asp:Label runat ="server" Width="90%" ID="lblSolution"></asp:Label>
                                             </asp:TableCell>
                                             
                                         </asp:TableRow>
                                         <asp:TableRow><asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
                                     </asp:Table>
                                               </asp:TableCell></asp:TableRow>
                                 <asp:TableRow ID="trKB" Visible="false">
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                         <asp:CheckBox runat="server" Checked="false" ID="chbKB" Text ="Enter in Knowledge Base" />
                                     </asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow>
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                       <%--  <asp:CheckBox runat="server" Checked="false" ID="chbDontEmailUser" Text ="Don't send email to user" />--%>
                                     </asp:TableCell>
                                 </asp:TableRow>
                                 <asp:TableRow ID="trUpdateCase" Visible="false">
                                     <asp:TableCell BackColor="#6da9e3" HorizontalAlign="Center" ForeColor="White" ColumnSpan ="3">
                                          <asp:Table runat="server" Width="50%" CssClass="collapsetable" CellPadding="0" CellSpacing="0" >
                                               <asp:TableRow>
                                                   <asp:TableCell Width="50%" ID="tcViewAttch" HorizontalAlign="Center" >
                                                       <asp:Button ID="btnViewAttach" runat="server" Text="View Attachments" />
                                                   </asp:TableCell>
                                                   <asp:TableCell Width="50%" HorizontalAlign="Center" >
                                                       <asp:Button runat="server" ID="btnUpdateCase" Text ="Save Case" />
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                           </asp:Table> 
                                        
                                     </asp:TableCell>
                                 </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Height="10px"></asp:TableCell>
    </asp:TableRow>
                    </asp:Table>
        </asp:Panel>
        <asp:Panel runat="server" Visible="true" ID="EmptyPanel">
            <asp:Panel BackColor="#6da9e3" runat="server" Width="600px" Height="40" ForeColor="White">
                <asp:Table runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            No Valid Case Number has been selected.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

             </asp:Panel>   
            </asp:Panel>
        <br />
        <asp:Table runat="server" Width="600px">
            <asp:TableRow>
                <asp:TableCell Width="50px"></asp:TableCell>
                <asp:TableCell>
                    View new Case Number:
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtNewCaseNo" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="5px"></asp:TableCell>
                <asp:TableCell>
                    <asp:Button Text="Search" runat="server" ID="btnSearchNew"/>
                </asp:TableCell>
                <asp:TableCell Width="50px"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Height="10px"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                    <asp:LinkButton runat="server" Text="Return to Finance Help Desk Main Page" href="../../FinanceHelpDesk/financehelpdesk.aspx"></asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

                 
                
        <asp:Label ID="FakeButtonp4" runat="server" />
        <asp:Panel ID="Panelp4" runat="server" Width="233px" BackColor="#6da9e3">
            <asp:Table runat="server" Width="100%" Height="100%">
                <asp:TableRow>
                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="10px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#CBE3FB">
                        <asp:Label ID="explanationlabelp4" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="10px"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Height="20px"></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" VerticalAlign="Middle" HorizontalAlign="Center">
                        <asp:Button ID="OkButtonp4" BorderStyle="Outset" BorderWidth="2px" runat="server" Font-Size="small" Text="OK" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Height="10px"></asp:TableCell></asp:TableRow>
            </asp:Table>
        </asp:Panel>
        <br />
        <cc1:ModalPopupExtender ID="ModalPopupExtenderp4" runat="server"
            TargetControlID="FakeButtonp4"
            PopupControlID="Panelp4"
            DropShadow="true">
        </cc1:ModalPopupExtender>
                </ContentTemplate>
        </asp:UpdatePanel>
       
    </div>
    </form>
</body>
</html>
