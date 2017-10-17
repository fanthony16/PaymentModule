<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApprovalScheduleExtract.aspx.vb" Inherits="frmApprovalScheduleExtract" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

         <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
                         
               <div class ="bodyMainDiv" style ="width:2000px;" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Pencom Approval List...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               
               <div id="dvleftBox" class ="dvSideBox">

        <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ;">

            <div id="dvheader" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span></div>
            <div id="dvCrBody" class="dvBoxbody">

                  <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                       <div style ="float :left; width:85px"><span>Approval Type :</span></div>
                             <asp:DropDownList ID="ddApplicationType" runat="server" Width ="190px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqApplicationType" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>



                <div id="dvStateDate" class ="dvBoxRows">
                                <div style ="float :left; width:90px"><span>Start Period :</span></div>
                                <div style="width:200px; text-align :right ; float:left "><asp:TextBox ID="txtStartDate" runat="server" Width ="177px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator></div>
                                <asp:PopupControlExtender ID="calSDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlStartDate" Position="Bottom" TargetControlID="txtStartDate"></asp:PopupControlExtender>
                                <asp:Panel ID="pnlStartDate" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <asp:Calendar ID="calSDate" runat="server" BackColor="White" 
                                        BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                        ForeColor="#003399" Height="200px" Width="220px">
                                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                        <WeekendDayStyle BackColor="#CCCCFF" />
                                    </asp:Calendar>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>


                            </div>
                <div id="dvEndDate" class ="dvBoxRows">
                                <div style ="float :left; width:90px"><span>End Period :</span></div>
                                <div style="width:200px;text-align :right ; float :left ; "><asp:TextBox ID="txtEndDate" runat="server" Width ="177px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>                          
                                </div>

                                        <asp:PopupControlExtender ID="calEDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlEndDate" Position="Bottom" TargetControlID="txtEndDate"></asp:PopupControlExtender>
                        <asp:Panel ID="pnlEndDate" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="calEDate" runat="server" BackColor="White" 
                                        BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                        ForeColor="#003399" Height="200px" Width="220px">
                                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                        <WeekendDayStyle BackColor="#CCCCFF" />
                                    </asp:Calendar>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>

                            </div>
                <div id="dvNarration" class ="dvBoxRows" >
                    <%--<div style ="float :left; padding :0px 0px 0px 0px;"><span > Batch Number</span></div>--%>

                     <div style ="float :left; width:100px"><span>Batch Number :</span></div>

                    <div style ="text-align :left; padding-left :107px;"><asp:TextBox ID="txtPencomBatch" Width ="176px" runat="server"></asp:TextBox></div>
                </div>
                <div id="Div1" class ="dvBoxRows" >
                    <div style ="text-align :center ;"><asp:Button ID="btnViewTransaction" runat="server" Text="View Transactions" Visible ="true"  /></div>
                    
                </div>
            </div>

        </div>

    </div>



               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :10px; padding  :5px 10px 20px 10px; width :81%; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="300px"  >
                         <asp:GridView Width="100%" ID="gridApplications" runat="server" Visible="true" PageSize="70" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridApplication_OnRowDataBound">
                                 
                                 <Columns >
                                        
                                      <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkApprovalConfirm" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                        <asp:ButtonField CommandName="Select" Text="View"/>
                                        <%--<asp:BoundField DataField="txtApplicationCode" HeaderText="Application Code" ItemStyle-Width="150"/>
                                        <asp:BoundField DataField="txtPIN" HeaderText="Full Name" ItemStyle-Width="100"/> 
                                        <asp:BoundField DataField="txtFullName" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="txtEmployerName" HeaderText="Employer Name"/>
                                        <asp:BoundField DataField="ValueDate" HeaderText="Value Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="numApproved" HeaderText="Amount" DataFormatString="{0:N}" />--%>

                                      
                                        
                                     

                                        <%--<asp:BoundField DataField="PIN" HeaderText="Pencom PIN" DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="ValueDate" HeaderText="Value Date" DataFormatString="{0:d}" />--%>
                                 </Columns>

                                        <pagersettings mode="NextPreviousFirstLast"
                                        firstpagetext="First"
                                        lastpagetext="Last"
                                        nextpagetext="Next"
                                        previouspagetext="Prev"   
                                        position="Bottom"/> 
                              </asp:GridView>        
                    </asp:Panel>
       
       
        <div><hr /></div>

             <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; margin-bottom :50px; ">

                  <div style="float:left ; padding-right :20px; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
                  <div style="float:left; padding-right :20px;"><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>
                  <div style="float:left; padding-left: 20px;"><asp:Button ID="btnExportSchedule" runat="server" Text="Export Schedule" /></div>
                  <div style="float:left; padding-left: 20px;"><asp:Button ID="btnGenerateSchedule" runat="server" Text="Generate Schedule" Visible="False" /></div>
                  

             </div>
                

       



                    



    </div>


               
              
              
          </div>
     </div>

               <asp:Button id="btnPopUpHardShipApproval" runat="server" style="display:none" />
               <asp:Button id="btnEditApprovedAmount" runat="server" style="display:none" />
         
               <asp:ModalPopupExtender ID="MPApprovalHardShip" runat="server" PopupControlID="pnlRMASHardShip" TargetControlID="btnPopUpHardShipApproval" CancelControlID="btnMPRMASHardShip" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlRMASHardShip" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div14" style ="padding-left :20px;"><h2><span>HardShip Approval Details</span></h2></div>
          
          <div id="Div15" class ="SubbodyMainDiv" style="height:200px;">
                      
               <div id="dvBatchRef" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Batch Ref :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtBatchRef" runat="server" Width =" 300px" ValidationGroup="HardApprovalDetails"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqBatchRef" runat ="server" ErrorMessage="*" controlToValidate="txtBatchRef" Display="Dynamic" SetFocusOnError="True" ValidationGroup="HardApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               <div id="dvApprovedAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total Approved Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtTotalApprovedAmount" runat="server" Width="300px" ValidationGroup="HardApprovalDetails"></asp:TextBox>
                         <asp:CompareValidator ID="comApprovedAmount" runat="server" ErrorMessage="*" ControlToValidate="txtTotalApprovedAmount" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Double" ValidationGroup="HardApprovalDetails"></asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="reqApprovedAmount" runat ="server" ErrorMessage="*" controlToValidate="txtTotalApprovedAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="HardApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               

               <div id="dvApprovedDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtApprovedDate" runat="server" Width="300px" ValidationGroup="HardApprovalDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqApprovedDate" runat ="server" ErrorMessage="*" controlToValidate="txtApprovedDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="HardApprovalDetails" ></asp:RequiredFieldValidator>

                    </div>
                    <asp:PopupControlExtender ID="calApprovedDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlApprovedDate" Position="Bottom" TargetControlID="txtApprovedDate"></asp:PopupControlExtender>

                    <asp:Panel ID="pnlApprovedDate" runat="server">
                                                            <asp:UpdatePanel ID="updApprovalDate" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calApprovedDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calApprovedDate" runat="server" BackColor="White" 
                                                                           BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                                                                           DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                                                           ForeColor="#003399" Height="200px" Width="220px">
                                                                           <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                                           <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                                           <OtherMonthDayStyle ForeColor="#999999" />
                                                                           <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                           <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                                           <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                                                                               Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                                           <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                                           <WeekendDayStyle BackColor="#CCCCFF" />
                                                                      </asp:Calendar>
                                                                 </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                    </asp:Panel>

               </div>


               <div id="dvAcknowledgmentDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Acknowledgment Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAcknowledgmentDate" runat="server" Width="300px" ValidationGroup="HardApprovalDetails"></asp:TextBox>
                         <%--<asp:CompareValidator ID="comAcknowledgmentDate" runat="server" ErrorMessage="*" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Date" ValidationGroup="HardApprovalDetails" ControlToValidate="txtAcknowledgmentDate"></asp:CompareValidator>--%>
                         <asp:RequiredFieldValidator ID="reqAcknowledgmentDate" runat ="server" ErrorMessage="*" controlToValidate="txtAcknowledgmentDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="HardApprovalDetails" ></asp:RequiredFieldValidator>
                    </div>
                    <asp:PopupControlExtender ID="calAcknowledgmentDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlAcknowledgmentDate" Position="Bottom" TargetControlID="txtAcknowledgmentDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlAcknowledgmentDate" runat="server">
                                                            <asp:UpdatePanel ID="updAcknowledgmentDate" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calAcknowledgmentDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calAcknowledgmentDate" runat="server" BackColor="White" 
                                                                           BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                                                                           DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                                                           ForeColor="#003399" Height="200px" Width="220px">
                                                                           <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                                           <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                                           <OtherMonthDayStyle ForeColor="#999999" />
                                                                           <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                           <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                                           <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                                                                               Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                                           <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                                           <WeekendDayStyle BackColor="#CCCCFF" />
                                                                      </asp:Calendar>
                                                                 </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                    </asp:Panel>
               </div>

              


               <div style ="width :100%; float :left ;">
                    
                    <div id="Div17" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnHardApprovalOK" runat="server" Text="Ok" ValidationGroup="HardApprovalDetails" CausesValidation ="true"   />
               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnMPRMASHardShip" runat="server" Text="Cancel" />
    </asp:Panel>





               <asp:ModalPopupExtender ID="mpEditApprovedAmount" runat="server" PopupControlID="pnlEditApprovedAmount" TargetControlID="btnEditApprovedAmount" CancelControlID="btnMPRMASHardShip" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlEditApprovedAmount" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div3" style ="padding-left :20px;"><h2><span>Edit Approved Amount</span></h2></div>
          
          <div id="Div4" class ="SubbodyMainDiv" style="height:200px;">
                   
               
               <div id="dvApplicationCode" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtApplicationCode" runat="server" Width =" 300px" ValidationGroup="EditApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqApplicationCode" runat ="server" ErrorMessage="*" controlToValidate="txtApplicationCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               
               
               
                  
               <div id="dvEditApprovedAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtEditApprovedAmount" runat="server" Width =" 300px" ValidationGroup="EditApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqEditApprovedAmount" runat ="server" ErrorMessage="*" controlToValidate="txtEditApprovedAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
                          
              
               <div style ="width :100%; float :left ;">
                    
                    <div id="Div9" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnSaveApprovedAmount" runat="server" Text="Ok" ValidationGroup="EditApprovedAmount" CausesValidation ="true"   />
               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="Button2" runat="server" Text="Cancel" />
    </asp:Panel>
          
          
          </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

