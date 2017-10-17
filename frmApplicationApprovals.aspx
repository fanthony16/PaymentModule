<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationApprovals.aspx.vb" Inherits="frmApplicationApprovals" Theme ="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
                         
               <div class ="bodyMainDiv" style ="width :2000px;" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Comfirmed Benefit Application(s)...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                        <div style ="text-align :left  ; padding : 5px;" ><span >Select The Approval Type: </span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddApplicationType" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqApplicationType" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="pencomApproval"></asp:RequiredFieldValidator>

                        </div>

                        <div style ="text-align :right ; padding :5px; margin-top:10px; margin-bottom :10px;">
                             <div style ="float :left ;">Batch No: </div>
                             <asp:DropDownList ID="ddApplicationBatchNumber" runat="server" Width ="200px" ValidationGroup="pencomApproval" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqSPBatchNo" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationBatchNumber" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="pencomApproval"></asp:RequiredFieldValidator>
                             <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="pencomApproval" />

                        </div>
                         <div id ="dvSelectedBatch"><asp:ListBox ID="lstBatches" runat="server" Width ="300px" Height ="100px"></asp:ListBox></div>

                        <div style ="text-align :right ; margin-top :10px; margin-bottom : 10px; ">
                             <div style =" float :left ; text-align :left ; padding-left :7px;"><asp:Button ID="btnLoadPIN" runat="server" Text="Load Batch PIN(s)" ValidationGroup="RMASComfirmation" /></div>
                             <div style ="padding-right :10px;"> <asp:Button ID="btnRemove" runat="server" Text="Remove" ValidationGroup="RMASComfirmation" /></div>

                        </div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="true">
                 <div style ="padding:5PX;">
                 
                 <div id ="Div2"><asp:ListBox ID="lstApprovalPIN" runat="server" Width ="300px" Height ="300px"></asp:ListBox></div></div>
                 <div style =" float :left ; text-align :left ; padding-left :7px; padding-right :41px;"><asp:Button ID="btnAddPIN" runat="server" Text="Add PIN Application" ValidationGroup="RMASComfirmation" /></div>
                 <div style ="padding-right :10px;"> <asp:Button ID="Button1" runat="server" Text="Remove " ValidationGroup="RMASComfirmation" /></div>
            </asp:Panel>
            
            
                  </div>



            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :10px; padding  :5px 10px 0px 10px; width :80%; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridRMAS" runat="server" Visible="true" PageSize="50" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridRMAS_OnRowDataBound" >
                                 
                                 <Columns >
                                      <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkRMASApproval" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                      <%--<asp:TemplateField HeaderText="Edit Amount">
                                             <ItemTemplate>
                                                  <asp:Button ID="btnAdd" runat="server" OnClick ="EditApprovedAmount" Text="Edit Amount" />
                                             </ItemTemplate>
                                        </asp:TemplateField>--%>

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

             <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; ">
             <div style="float:left ; padding-right :20px; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
             <div style="float:left; padding-right :20px;"><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>
                  <div style="float:left; "><asp:Button ID="btnUpdateApprovedAmount" runat="server" Text="Edit Approved Amount" Visible="true" /></div>
             <div style="float:left; padding-left: 20px; "><asp:Button ID="btnUpdateApprovalDetails" runat="server" Text="Update Approval Details" /></div>
             <div style="float:left; padding-left: 20px;"><asp:Button ID="btnApprovalSave" runat="server" Text="Save Approval" /></div>
        </div>
                

        <div id="dvMainProcessButton" style ="width :100%; padding: 00px; margin-top : 30px; margin-bottom : 30px; padding :4px;">

             

        </div>


    </div>
              
              
          </div>
     </div>

               <asp:Button id="btnPopUpHardShipApproval" runat="server" style="display:none" />
               <asp:Button id="btnEditApprovedAmount" runat="server" style="display:none" />
               <asp:Button id="btnEditPWApproval" runat="server" style="display:none" />
               <asp:Button id="btnEditAnnApproval" runat="server" style="display:none" />
               <asp:Button id="btnEditEnblocApproval" runat="server" style="display:none" />
               <asp:Button id="btnEditAVCApproval" runat="server" style="display:none" />
               

               <asp:Button id="btnPopUpPWApproval" runat="server" style="display:none" />
               <asp:Button id="btnPopUpAnnApproval" runat="server" style="display:none" />
               <asp:Button id="btnPopUpAVCApproval" runat="server" style="display:none" />
               
               
         
               <asp:ModalPopupExtender ID="MPApprovalHardShip" runat="server" PopupControlID="pnlRMASHardShip" TargetControlID="btnPopUpHardShipApproval" CancelControlID="btnMPRMASHardShip" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlRMASHardShip" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div14" style ="padding-left :20px;"><h2><span>Pencom Approval Details</span></h2></div>
          
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


               <asp:ModalPopupExtender ID="mpApprovalPW" runat="server" PopupControlID="pnlApprovalPW" TargetControlID="btnPopUpPWApproval" CancelControlID="btnCancelPWApproval" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlApprovalPW" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="350px">

          <div class ="bodyMainDiv" style="height:320px; width: 100%" >
          <div id="Div7" style ="padding-left :20px;"><h2><span>Pencom Approval Details</span></h2></div>
          
          <div id="Div8" class ="SubbodyMainDiv" style="height:260px;">
                      
              
               <div id="dvPWApprovalBatchRef" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Batch Ref :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWApprovalBatchRef" runat="server" Width =" 300px" ValidationGroup="PWApprovalDetails"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqPWApprovalBatchRef" runat ="server" ErrorMessage="*" controlToValidate="txtPWApprovalBatchRef" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvPWApprovalLumpSum" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total LumpSum Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPWApprovalLumpSum" runat="server" Width="300px" ValidationGroup="PWApprovalDetails"></asp:TextBox>
                         <asp:CompareValidator ID="compPWApprovalLumpSum" runat="server" ErrorMessage="*" ControlToValidate="txtPWApprovalLumpSum" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Double" ValidationGroup="PWApprovalDetails"></asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="reqPWApprovalLumpSum" runat ="server" ErrorMessage="*" controlToValidate="txtPWApprovalLumpSum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvPWApprovalPension" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total Pension Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPWApprovalPension" runat="server" Width="300px" ValidationGroup="PWApprovalDetails"></asp:TextBox>
                         <asp:CompareValidator ID="compPWApprovalPension" runat="server" ErrorMessage="*" ControlToValidate="txtPWApprovalPension" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Double" ValidationGroup="PWApprovalDetails"></asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="reqPWApprovalPension" runat ="server" ErrorMessage="*" controlToValidate="txtPWApprovalPension" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvPWApprovalApprovedDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtPWApprovedDate" runat="server" Width="300px" ValidationGroup="PWApprovalDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqPWApprovedDate" runat ="server" ErrorMessage="*" controlToValidate="txtPWApprovedDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="PWApprovalDetails" ></asp:RequiredFieldValidator>

                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_calPWApprovedDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlPWApprovedDate" Position="Bottom" TargetControlID="txtPWApprovedDate"></asp:PopupControlExtender>

                    <asp:Panel ID="pnlPWApprovedDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calPWApprovedDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calPWApprovedDate" runat="server" BackColor="White" 
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

               <div id="dvPWApprovalAcknowledgmentDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Acknowledgment Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPWAcknowledgmentDate" runat="server" Width="300px" ValidationGroup="PWApprovalDetails"></asp:TextBox>
                         
                         <asp:RequiredFieldValidator ID="reqPWAcknowledgmentDate" runat ="server" ErrorMessage="*" controlToValidate="txtPWAcknowledgmentDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="PWApprovalDetails" ></asp:RequiredFieldValidator>
                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_calPWAcknowledgmentDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlPWAcknowledgmentDate" Position="Bottom" TargetControlID="txtPWAcknowledgmentDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlPWAcknowledgmentDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calPWAcknowledgmentDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calPWAcknowledgmentDate" runat="server" BackColor="White" 
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
                    
                    <div id="Div18" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnPWApprovalDetailOK" runat="server" Text="Ok" ValidationGroup="PWApprovalDetails" CausesValidation ="true"   />
               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnCancelPWApproval" runat="server" Text="Cancel" />
    </asp:Panel>


               <asp:ModalPopupExtender ID="mpApprovalAnn" runat="server" PopupControlID="pnlApprovalAnn" TargetControlID="btnPopUpAnnApproval" CancelControlID="btnCancelAnnApproval" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlApprovalAnn" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="350px">

          <div class ="bodyMainDiv" style="height:320px; width: 100%" >
          <div id="Div11" style ="padding-left :20px;"><h2><span>Pencom Approval Details</span></h2></div>
          
          <div id="Div12" class ="SubbodyMainDiv" style="height:260px;">
                      
              
               <div id="dvAnnApprovalBatchRef" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Batch Ref :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAnnApprovalBatchRef" runat="server" Width =" 300px" ValidationGroup="AnnApprovalDetails"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAnnApprovalBatchRef" runat ="server" ErrorMessage="*" controlToValidate="txtAnnApprovalBatchRef" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvAnnApprovalLumpSum" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total LumpSum Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAnnApprovalLumpSum" runat="server" Width="300px" ValidationGroup="AnnApprovalDetails"></asp:TextBox>
                         <asp:CompareValidator ID="compAnnApprovalLumpSum" runat="server" ErrorMessage="*" ControlToValidate="txtAnnApprovalLumpSum" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Double" ValidationGroup="AnnApprovalDetails"></asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="reqAnnApprovalLumpSum" runat ="server" ErrorMessage="*" controlToValidate="txtAnnApprovalLumpSum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvAnnApprovalAnnuity" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total Annuity Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAnnApprovalAnnuity" runat="server" Width="300px" ValidationGroup="AnnApprovalDetails"></asp:TextBox>
                         <asp:CompareValidator ID="compAnnApprovalAnnuity" runat="server" ErrorMessage="*" ControlToValidate="txtAnnApprovalAnnuity" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Double" ValidationGroup="AnnApprovalDetails"></asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="reqAnnApprovalAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtAnnApprovalAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvAnnApprovedDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtAnnApprovedDate" runat="server" Width="300px" ValidationGroup="AnnApprovalDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAnnApprovedDate" runat ="server" ErrorMessage="*" controlToValidate="txtAnnApprovedDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="AnnApprovalDetails" ></asp:RequiredFieldValidator>

                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_calAnnApprovedDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlAnnApprovedDate" Position="Bottom" TargetControlID="txtAnnApprovedDate"></asp:PopupControlExtender>

                    <asp:Panel ID="pnlAnnApprovedDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calAnnApprovedDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calAnnApprovedDate" runat="server" BackColor="White" 
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

               <div id="dvAnnAcknowledgmentDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Acknowledgment Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAnnAcknowledgmentDate" runat="server" Width="300px" ValidationGroup="AnnApprovalDetails"></asp:TextBox>
                         
                         <asp:RequiredFieldValidator ID="reqAnnAcknowledgmentDate" runat ="server" ErrorMessage="*" controlToValidate="txtAnnAcknowledgmentDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="AnnApprovalDetails" ></asp:RequiredFieldValidator>
                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_calAnnAcknowledgmentDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlAnnAcknowledgmentDate" Position="Bottom" TargetControlID="txtAnnAcknowledgmentDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlAnnAcknowledgmentDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calAnnAcknowledgmentDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calAnnAcknowledgmentDate" runat="server" BackColor="White" 
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
                    
                    <div id="Div22" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnAnnApprovalDetailOK" runat="server" Text="Ok" ValidationGroup="AnnApprovalDetails" CausesValidation ="true"   />
               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnCancelAnnApproval" runat="server" Text="Cancel" />
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


               <asp:ModalPopupExtender ID="mpEditPWApproval" runat="server" PopupControlID="pnlEditPWApproval" TargetControlID="btnEditPWApproval" CancelControlID="btnCancelMPEditPWApproval" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlEditPWApproval" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="400px">

          <div class ="bodyMainDiv" style="height:370px; width: 100%" >
          <div id="Div5" style ="padding-left :20px;"><h2><span>Edit Pension Approved Amount</span></h2></div>
          
          <div id="Div6" class ="SubbodyMainDiv" style="height:300px;">
                   
               
                <div id="dvPWApplicationCode" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWApplicationCode" runat="server" Width =" 300px" ValidationGroup="EditApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqPWApplicationCode" runat ="server" ErrorMessage="*" controlToValidate="txtPWApplicationCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditPWApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               
                <div id="dvPWApprovedLumpSumAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Lump Sum Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWApprovedLumpSumAmount" runat="server" Width =" 300px" ValidationGroup="EditPWApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqPWApprovedLumpSumAmount" runat ="server" ErrorMessage="*" controlToValidate="txtPWApprovedLumpSumAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditPWApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

                <div id="dvPWApprovedPensionAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Pension Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWApprovedPensionAmount" runat="server" Width =" 300px" ValidationGroup="EditPWApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqApprovedPWPensionAmount" runat ="server" ErrorMessage="*" controlToValidate="txtPWApprovedPensionAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditPWApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>              
               
                <div id="dvPWLumpSum" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Lump Sum AmountTo Pay :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWLumpSumAmount" runat="server" Width =" 300px" ValidationGroup="EditPWApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqLumpSumAmount" runat ="server" ErrorMessage="*" controlToValidate="txtPWLumpSumAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditPWApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

                <div id="dvPWPension" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Pension AmountTo Pay :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWPensionAmount" runat="server" Width =" 300px" ValidationGroup="EditPWApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqPWPensionAmount" runat ="server" ErrorMessage="*" controlToValidate="txtPWPensionAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditPWApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

                <div id="dvPWArears" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Arrears AmountTo Pay :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtPWArrears" runat="server" Width =" 300px" ValidationGroup="EditPWApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqPWArrears" runat ="server" ErrorMessage="*" controlToValidate="txtPWArrears" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditPWApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
                          
              
               <div style ="width :100%; float :left ;">
                    
                    <div id="Div10" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <asp:Button ID="btnEditPWApprovalOK" runat="server" Text="Ok" ValidationGroup="EditPWApprovedAmount" CausesValidation ="true"   />

               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnCancelMPEditPWApproval" runat="server" Text="Cancel" />
    </asp:Panel>


               <asp:ModalPopupExtender ID="mpEditAnnApproval" runat="server" PopupControlID="pnlEditAnnApproval" TargetControlID="btnEditAnnApproval" CancelControlID="btnCancelMPEditAnnApproval" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlEditAnnApproval" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="400px">

          <div class ="bodyMainDiv" style="height:370px; width: 100%" >
          <div id="Div13" style ="padding-left :20px;"><h2><span>Edit Pension Approved Amount</span></h2></div>
          
          <div id="Div16" class ="SubbodyMainDiv" style="height:300px;">    
               
               <div id="dvPAnnApplicationCode" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAnnApplicationCode" runat="server" Width =" 300px" ValidationGroup="EditAnnApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqPAnnApplicationCode" runat ="server" ErrorMessage="*" controlToValidate="txtAnnApplicationCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAnnApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               
               <div id="dvAnnLumpSumApprovedAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Lump Sum Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAnnLumpSumApprovedAmount" runat="server" Width =" 300px" ValidationGroup="EditAnnApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAnnLumpSumApprovedAmount" runat ="server" ErrorMessage="*" controlToValidate="txtAnnLumpSumApprovedAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAnnApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvAnnAnnuityApprovedAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Annuity Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAnnAnnuityApprovedAmount" runat="server" Width =" 300px" ValidationGroup="EditAnnApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAnnAnnuityApprovedAmount" runat ="server" ErrorMessage="*" controlToValidate="txtAnnAnnuityApprovedAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAnnApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
                  
               <div id="dvAnnLumpSumAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Lump Sum Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAnnLumpSumAmount" runat="server" Width =" 300px" ValidationGroup="EditAnnApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAnnLumpSumAmount" runat ="server" ErrorMessage="*" controlToValidate="txtAnnLumpSumAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAnnApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvAnnAnnuityAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Annuity Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAnnAnnuityAmount" runat="server" Width =" 300px" ValidationGroup="EditAnnApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAnnAnnuityAmount" runat ="server" ErrorMessage="*" controlToValidate="txtAnnAnnuityAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAnnApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
              
               <div style ="width :100%; float :left ;">
                    
                    <div id="Div25" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <asp:Button ID="btnEditAnnApprovalOK" runat="server" Text="Ok" ValidationGroup="EditAnnApprovedAmount" CausesValidation ="true"   />
                    <asp:Button ID="btnCancelMPEditAnnApproval" runat="server" Text="Cancel" />
               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    
    </asp:Panel>


               <asp:ModalPopupExtender ID="mpEditEnblocApproval" runat="server" PopupControlID="pnlEnblocApproval" TargetControlID="btnEditEnblocApproval" CancelControlID="btnCancelMPEditEnblocApproval" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlEnblocApproval" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div19" style ="padding-left :20px;"><h2><span>Edit Pension Approved Amount</span></h2></div>
          
          <div id="Div20" class ="SubbodyMainDiv" style="height:200px;">
                   
               
               <div id="dvEnblocApplicationCode" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtEnblocApplicationCode" runat="server" Width =" 300px" ValidationGroup="EditEnblocApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqEnblocApplicationCode" runat ="server" ErrorMessage="*" controlToValidate="txtEnblocApplicationCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditEnblocApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
                  
               <div id="dvEnblocApprovedAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtEnblocApprovedAmount" runat="server" Width =" 300px" ValidationGroup="EditEnblocApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqEnblocApprovedAmount" runat ="server" ErrorMessage="*" controlToValidate="txtEnblocApprovedAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditEnblocApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvEnblocAmountToPay" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Amount ToPay :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtEnblocAmountToPay" runat="server" Width =" 300px" ValidationGroup="EditEnblocApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqEnblocAmountToPay" runat ="server" ErrorMessage="*" controlToValidate="txtEnblocAmountToPay" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditEnblocApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
              
               <div style ="width :100%; float :left ;">
                    
                    <div id="Div26" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <asp:Button ID="btnEditEnblocApprovedAmountOk" runat="server" Text="Ok" ValidationGroup="EditEnblocApprovedAmount" CausesValidation ="true"   />

               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnCancelMPEditEnblocApproval" runat="server" Text="Cancel" />
    </asp:Panel>

               
                <asp:ModalPopupExtender ID="mpEditAVCApproval" runat="server" PopupControlID="pnlAVCApproval" TargetControlID="btnEditAVCApproval" CancelControlID="btnCancelMPEditEnblocApproval" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
               <asp:Panel ID="pnlAVCApproval" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div21" style ="padding-left :20px;"><h2><span>Edit AVC Approved Amount</span></h2></div>
          
          <div id="Div23" class ="SubbodyMainDiv" style="height:200px;">
                   
               
               <div id="Div24" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAVCApplicationCode" runat="server" Width =" 300px" ValidationGroup="EditAVCApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAVCApplicationCode" runat ="server" ErrorMessage="*" controlToValidate="txtAVCApplicationCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAVCApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
                  
               <div id="Div27" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Approved Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAVCApprovedAmount" runat="server" Width =" 300px" ValidationGroup="EditAVCApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAVCApprovedAmount" runat ="server" ErrorMessage="*" controlToValidate="txtAVCApprovedAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAVCApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="Div28" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Amount ToPay :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAVCAmountToPay" runat="server" Width =" 300px" ValidationGroup="EditAVCApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAVCAmountToPay" runat ="server" ErrorMessage="*" controlToValidate="txtAVCAmountToPay" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAVCApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="Div30" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Accrued Interest :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:textbox ID="txtAVCInterestAmount" runat="server" Width =" 300px" ValidationGroup="EditAVCApprovedAmount"></asp:textbox>
                         <asp:RequiredFieldValidator ID="reqAVCInterestAmount" runat ="server" ErrorMessage="*" controlToValidate="txtAVCInterestAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EditAVCApprovedAmount" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
              
               <div style ="width :100%; float :left ;">
                    
                    <div id="Div29" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <asp:Button ID="btnEditAVCApprovedAmount" runat="server" Text="Ok" ValidationGroup="EditAVCApprovedAmount" CausesValidation ="true"   />

               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="Button4" runat="server" Text="Cancel" />
    </asp:Panel>













          
          
          </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

