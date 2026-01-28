<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmEnhancementApproval.aspx.vb" Inherits="frmEnhancementApproval" Theme ="Blue"  %>
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
                 
                 <div id ="Div2"><asp:ListBox ID="lstApprovalPIN" runat="server" Width ="300px" Height ="300px" Visible="False"></asp:ListBox></div></div>
                 <div style =" float :left ; text-align :left ; padding-left :7px; padding-right :41px;"><asp:Button ID="btnAddPIN" runat="server" Text="Add PIN Application" ValidationGroup="RMASComfirmation" Visible="False" /></div>
                 <div style ="padding-right :10px;"> <asp:Button ID="Button1" runat="server" Text="Remove " ValidationGroup="RMASComfirmation" Visible="False" /></div>
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
               <asp:Panel ID="pnlRMASHardShip" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="410px">

          <div class ="bodyMainDiv" style="height:370px; width: 100%" >
          <div id="Div14" style ="padding-left :20px;"><h2><span>Pencom Approval Details</span></h2></div>
          
          <div id="Div15" class ="SubbodyMainDiv" style="height:300px;">
                      
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

               <div id="dvArrearsNo" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">No of Arears(Months) :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNoArears" runat="server" Width="300px" ValidationGroup="HardApprovalDetails"></asp:TextBox>
                         <asp:CompareValidator ID="compNoArears" runat="server" ErrorMessage="*" ControlToValidate="txtTotalApprovedAmount" Display="Dynamic" Font-Bold="True" ForeColor="Red" Type="Double" ValidationGroup="HardApprovalDetails"></asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="reqtxtNoArears" runat ="server" ErrorMessage="*" controlToValidate="txtNoArears" Display="Dynamic" SetFocusOnError="True" ValidationGroup="HardApprovalDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
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

                      
          
          </ContentTemplate>
     </asp:UpdatePanel>


</asp:Content>

