<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApprovalComfirmation.aspx.vb" Inherits="frmApprovalComfirmation" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
                         
               <div class ="bodyMainDiv" style="width:2000px;" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Pencom Approval Comfirmation...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                        <div style ="text-align :left  ; padding : 5px;" ><span >Select The Approval Type: </span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddApplicationType" runat="server" Width ="300px" ValidationGroup="processing"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqApplicationType" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="pencomApproval"></asp:RequiredFieldValidator>

                        </div>

                        <div style ="text-align :right ; margin-top :10px; margin-bottom : 10px; ">
                             <div style ="padding-right :10px;"> <asp:Button ID="btnFind" runat="server" Text="Find" ValidationGroup="RMASComfirmation" /></div>

                        </div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>




                    <div id="dvBankDetails" style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;" runat="server" visible ="false" >
                        <div id="Div2" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Bank Details</strong></span> 
                        </div>                                            

                                          <div id="dvAccountName" class ="dvBoxRows" style="margin-top : 5px; padding-left :7px;">
                                               <div class="dvBoxRowsFieldLabel" style="text-align :left ;">
                                                    <span style ="font-size : medium;">Account Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAccountName" runat="server" Width ="300px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqAccountName" runat ="server" ErrorMessage="*" controlToValidate="txtAccountName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="updateBankDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvAccountNumber" class ="dvBoxRows" style="margin-top : 5px; padding-left :7px;">
                                               <div class="dvBoxRowsFieldLabel" style="text-align :left ;">
                                                    <span style ="font-size : medium; text-align :left ;">Account No :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAccountNumber" runat="server" Width ="300px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqAccountNumber" runat ="server" ErrorMessage="*" controlToValidate="txtAccountNumber" Display="Dynamic" SetFocusOnError="True" ValidationGroup="updateBankDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvBVN" class ="dvBoxRows" style="margin-top : 5px; padding-left :7px;">
                                               <div class="dvBoxRowsFieldLabel" style ="text-align :left ;">
                                                    <span style ="font-size : medium;">BV Number :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtBVN" runat="server" Width ="300px"></asp:TextBox>
                                                    
                                               </div>
                                          </div>
                                          <div id="dvBank" class ="dvBoxRows" style="margin-top : 5px; padding-left :7px;">
                                               <div class="dvBoxRowsFieldLabel" style="text-align :left ;">
                                                    <span style ="font-size : medium;">Bank Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddBankName" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="reqValBank" runat ="server" ErrorMessage="*" controlToValidate="ddBankName" Display="Dynamic"  SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="updateBankDetails" ></asp:RequiredFieldValidator>--%>
                                               </div>
                                               </div>
                                          <div id="dvBankBranch" class ="dvBoxRows" style="margin-top : 5px; padding-left :7px;">
                                               <div class="dvBoxRowsFieldLabel" style="text-align :left ;">
                                                    <span style ="font-size : medium;">Bank Branch :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:DropDownList ID="ddBankBranch" runat="server" Width="300px" ValidationGroup ="updateBankDetails"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqBranch" runat ="server" ErrorMessage="*" controlToValidate="ddBankBranch" Display="Dynamic"  SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="updateBankDetails" ></asp:RequiredFieldValidator>
                                               </div>
                                              
                                          </div>
                                          <div id ="dvUpdateButton" style ="text-align :right ; margin-top :5px; margin-bottom : 10px; ">
                                            <div style ="padding-right :10px;"> <asp:Button ID="btnUpdateBankDetails" runat="server" Text="Update Bank Details" ValidationGroup="updateBankDetails" /></div>

                                       </div>







            <asp:Panel ID="Panel2" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>

                    <div id="dvGridRecievedDocument" runat ="server" visible ="false" class   ="dvBoxRows" style="margin-top : 0px; margin-left :7px;">

                               <div id="Div19" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Submitted Documents</strong></span> 
                        </div>

                                               <asp:Panel ID="pnlUploadDetail" Width ="98%" runat ="server" BorderStyle="Solid" Height ="290px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridRecievedDocument" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False" OnRowDataBound ="gridSubmittedDocuments_RowDataBound">
                                                         <Columns>

                                                              

                                                              <asp:BoundField DataField="txtDocumentName" HeaderText="Desciption" />
                                                              <%--<asp:BoundField DataField="DocumentPath" HeaderText="" HeaderStyle-Width="0" Visible ="true" ItemStyle-Width ="0"  />--%>
                                                              <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="ViewDocumentDetails_Click" ID="btnViewDocument" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Document" OnClientClick="BtnViewDetails_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>

                                                               </asp:TemplateField>
                                                              
                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>
                                             
                                          </div>


                     <div id="dvDetailGrid" class="dvMoreDetails" style=" margin-top :10px; padding  :5px 10px 0px 10px; border-radius :25px 25px 0px 0px; border :2px solid ;" runat ="server" visible ="false"  >

        <asp:Panel ID="pnlLeftGrid" Width ="100%" runat ="server" Height ="475px" >
          <asp:GridView Width="100%" ID="gridProperties" runat="server" Visible="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="false" AllowPaging ="false" ShowHeader ="false"  >
                    <Columns>
                                <asp:BoundField DataField="FieldName" HeaderText="Lodgment ID" Visible ="true" ItemStyle-Width="90"/>
                                <asp:BoundField DataField="FieldValue" HeaderText="Narration" ItemStyle-Wrap ="true" ItemStyle-Width="100" DataFormatString="{0}" />
                    </Columns>
          </asp:GridView>
          
      
</asp:Panel>

    </div>

            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :10px; padding  :5px 10px 20px 10px; width :80%; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="500px"  >
                            <asp:GridView Width="100%" ID="gridApprovalBatch" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridRMAS_OnRowDataBound" >
                                 
                                 <Columns >
                                        <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkApprovalConfirm" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        <asp:BoundField DataField="txtRefNo" HeaderText="Pencom Batch" ItemStyle-Width="250"/>
                                        <asp:BoundField DataField="dteApproval" HeaderText="Approval Date" ItemStyle-Width="200" DataFormatString="{0:d}"/> 
                                        <asp:BoundField DataField="dteAcknowledgment" HeaderText="Acknowledgment Date" ItemStyle-Width="200" DataFormatString="{0:d}"/> 
                                        <asp:BoundField DataField="numApprovalAmount" HeaderText="Approval Amount" ItemStyle-Width="150" DataFormatString="{0:N}"/>
                                        
                                        <%--<asp:BoundField DataField="ExitDate" HeaderText="Exit Date" DataFormatString="{0:d}" />--%>
                                        
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
                  <div style="float:left; padding-left: 20px;"><asp:Button ID="btnApprovalSave" runat="server" Text="Save Approval" /></div>
             </div>
                

       



                    <div style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :0px; padding  :5px 10px 0px 10px; width :98%; " >

                    <asp:Panel ID="Panel1" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridApplications" runat="server" Visible="true" PageSize="50" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridRMAS_OnRowDataBound" >
                                 
                                 <Columns >
                                        
                                        
                                        <asp:ButtonField CommandName="Select" Text="View"/>

                                        <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalConfirm" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>



                                      <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                        </asp:TemplateField>


                                        <%--<asp:BoundField DataField="txtApplicationCode" HeaderText="Application Code" ItemStyle-Width="150"/>
                                        <asp:BoundField DataField="txtPIN" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="txtFullName" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="txtEmployerName" HeaderText="Employer Name"/>
                                        <asp:BoundField DataField="dteApplicationDate" HeaderText="Application Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="numApproved" HeaderText="Approved Amount" DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="numPayingAmount" HeaderText="Amount To Pay" DataFormatString="{0:N}" />--%>
                                 </Columns>

                                        <pagersettings mode="NextPreviousFirstLast"
                                        firstpagetext="First"
                                        lastpagetext="Last"
                                        nextpagetext="Next"
                                        previouspagetext="Prev"   
                                        position="Bottom"/> 
                              </asp:GridView>
                        </asp:Panel>
       
       
             <%--<div><hr /></div>--%>

             <%--<div id="Div2" style ="width :100%; padding-top  : 0px; padding-bottom :0px; text-align :right ; height :20px; ">
                  <div style="float:left ; padding-right :20px; "><asp:Button ID="Button1" runat="server" Text="Tag All" /></div>
                  <div style="float:left; padding-right :20px;"><asp:Button ID="Button3" runat="server" Text="Un-Tag All" /></div>
                  <div style="float:left; padding-left: 20px;"><asp:Button ID="Button4" runat="server" Text="Save Approval" /></div>
             </div>--%>
                

       


    </div>







    </div>


               
              
              
          </div>
     </div>

               <asp:Button id="btnPopUpHardShipApproval" runat="server" style="display:none" />
               <asp:Button id="btnEditApprovedAmount" runat="server" style="display:none" />
               <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />

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


               <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
               <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div5" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div6" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div7" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
            <div id="Div8" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="ApplicationComment" Height="80px" Width="100%" MaxLength="70"></asp:TextBox><asp:RequiredFieldValidator ID="reqApplicationComment" runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ControlToValidate="txtApplicationComment" ValidationGroup="ApplicationComment"></asp:RequiredFieldValidator>
                </div>

                 <div id="Div10"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="ApplicationComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div11"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppComments" runat="server" Text="Close" />
    </asp:Panel>
          
          
          </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

