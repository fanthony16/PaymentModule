<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmProcessing.aspx.vb" Inherits="frmProcessing" Theme="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

     
     <asp:UpdatePanel ID ="updFormPanel" runat ="server">

          <ContentTemplate>

     <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Benefit Application Processing...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                        <div style ="text-align :right ; padding : 5px;" ><span >Select The Approval Type: </span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddApprovalType" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqApprovalType" runat="server" ErrorMessage="*" ControlToValidate="ddApprovalType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="processing"></asp:RequiredFieldValidator>

                        </div>


                         <div id="dvFundType" style ="padding :5px; float:left;width :100%; " >
                             <div style="float:left ; width :50%;"><asp:RadioButton ID="rdRSA" Text ="RSA" GroupName ="ProcessingPlatForm" runat ="server" Checked="true"   /></div>
                             <div style="float:left ; width :50%;"><asp:RadioButton ID="rdRF" Text ="Retiree" GroupName ="ProcessingPlatForm" runat ="server"  />

                             </div>

                        </div>

                        <div style ="text-align :right ; padding : 5px;" ><span >Select The Processing Fund Type: </span></div>
                        <div id="Div21" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddProcessingFundType" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True">
                                  <asp:ListItem Selected="True"></asp:ListItem>
                                  <asp:ListItem>Fund I</asp:ListItem>
                                  <asp:ListItem>Fund II</asp:ListItem>
                                  <asp:ListItem>Fund III</asp:ListItem>
                                  <asp:ListItem>Fund IV</asp:ListItem>
                             </asp:DropDownList>
                             <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddApprovalType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="processing"></asp:RequiredFieldValidator>--%>

                        </div>


                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFind" runat="server" Text="Find" ValidationGroup="processing" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>



            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridProcessing" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" >
                                 <Columns>
                                        <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="chkProcessing" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        <asp:BoundField DataField="txtApplicationCode" HeaderText="Application Code" ItemStyle-Width="70" />
                                        <asp:BoundField DataField="Name" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" ItemStyle-Width="150"/>
                                        <asp:BoundField DataField="txtEmployerName" HeaderText="Employer" ItemStyle-Width="250" />

                                        <asp:TemplateField HeaderText="">

                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                                        </asp:TemplateField>





                                      <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                    
                                                  <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" />
                                        
                                             </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewIACComment_Click" ID="btnAddViewIACComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="View IAC Comment(s)" OnClientClick="AddViewIACComment_Click" ItemStyle-Width ="10px" />
                                        
                                             </ItemTemplate>
                                                                   
                                     </asp:TemplateField>

                  
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

        <div id="dvtable" style ="margin-top :10px;">
                <table border="0" width="100%" cellpadding="2px" cellspacing="2px">
                  <tr>

                    <td style ="width :90%; border :none ">
                        <div style ="border : 2px solid ; ;border-radius :25px 25px 0px 0px;">
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; border : 2px solid ; height :25px;margin-bottom :0px;border-radius :25px 25px 0px 0px;"><span style ="color :#dde4ec;"><strong>Documentation Details</strong></span></div>
                            <asp:Panel ID="pnlDocumentDetails" Width ="99%" runat ="server" Height ="100px" >
                                <asp:GridView Width="100%" ID="gridSubmittedDocuments" runat="server" Visible="true" AllowPaging="True" PageSize="30" AutoGenerateColumns="False" OnRowDataBound ="gridSubmittedDocuments_RowDataBound">
                                    <Columns>
                                       <asp:BoundField DataField="txtDocumentName" HeaderText="Document Name" />
                                       <asp:BoundField DataField="DateRecived" HeaderText="Recieved Date" DataFormatString="{0:d}" />

                                          <asp:TemplateField HeaderText="">
                                         <ItemTemplate>
                                    
                                         <asp:ImageButton OnClick="ViewDocumentDetails_Click" ID="btnViewDocumentLog" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Document" ItemStyle-Width ="10px" />
                                        
                                         </ItemTemplate>
                                       </asp:TemplateField>


                                    </Columns>

                                    <pagersettings mode="NextPreviousFirstLast"
                                    firstpagetext="First"
                                    lastpagetext="Last"
                                    nextpagetext="Next"
                                    previouspagetext="Prev"   
                                    position="Bottom"/> 

                                </asp:GridView>
                            </asp:Panel>
                            
                        </div>
                    </td>
                    
                    
                    
                  </tr>

                </table>
            </div>


        <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; ">
             <div style="float:left ; ">
                  <%--<asp:Button ID="btnTagAll" runat="server" Text="Tag All" />--%>
                  <asp:ImageButton ID="btnTagAll" runat ="server" ImageUrl="~/images/success.png" ToolTip="Tag All"/>
             </div>
             <div style="float:left; ">
                  <%--<asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" />--%>
                  <asp:ImageButton ID="btnUnTagAll" runat ="server" ImageUrl="~/images/error.png" ToolTip="Un-Tag All"/>
                  
             </div>

             <div style="float:left; ">
                  
                  <asp:ImageButton ID="btnSNR" runat ="server" ImageUrl="~/images/pdf.png" ToolTip="DownLoad SNR Form(s)" CausesValidation="False"/>
             </div>
             
        </div>
        <div id="dvPriceDate" style ="width :100%; padding: 0px; margin-top : 0px; margin-bottom : 0px;">

             <div style="float:left; padding-right : 5px; padding-left : 150px; "><span>Set Price Date :</span> </div>
             <div style="float:left; padding-right : 20px; ">
                  <asp:CheckBox id= "chkCurrentDate" runat ="server" Text ="Check To Use Current Date" AutoPostBack="True" CausesValidation="True" />
                  <asp:TextBox ID="txtPriceDateBatch" runat="server" ValidationGroup ="Processing"></asp:TextBox>

                  <asp:RequiredFieldValidator ID="reqPriceDateBatch" runat ="server" ErrorMessage="*" controlToValidate="txtPriceDateBatch" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="Processing" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calPriceDateBatch_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlPriceDateBatch" Position="Bottom" TargetControlID="txtPriceDateBatch"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlPriceDateBatch" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                  <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calPriceDateBatch" />

                                                                     </Triggers>
                                                                 <ContentTemplate>


                                                                      <asp:Calendar ID="calPriceDateBatch" runat="server" BackColor="White" 
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

             <div style="float:left; padding-right : 20px;" >
                  <%--<asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Process" ValidationGroup ="Processing"/>--%>
                  <asp:ImageButton ID="btnPreviewProcessing" runat ="server" ImageUrl="~/images/icon-gadgets.png" ToolTip="Preview" ValidationGroup ="Processing"/>
             </div>


             <div style="float:left; padding-right : 20px;" >
                  <%--<asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Process" ValidationGroup ="Processing"/>--%>
                  <asp:ImageButton ID="btnHardShipProcessingBatch" runat ="server" ImageUrl="~/images/approve.png" ToolTip="Save and Send For Confirmation" ValidationGroup ="Processing"/>
             </div>
             <div style="float:left;">

                  <%--<asp:Button ID="btnSendBack" runat="server" Text="Reject" />--%>
                  <asp:ImageButton ID="btnSendBack" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Reject"/>
             </div>
             
             
             <div style="float:left "><asp:DropDownList ID="ddApplicationStatusBatch" Width ="20px" runat="server" Visible ="false" ></asp:DropDownList>

             </div>
             
        </div>

        <div id="dvMainProcessButton" style ="width :100%; padding: 10px; margin-top : 10px; margin-bottom : 10px;">

             <%--<div style="float:left"><asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Process" /></div>--%>
        </div>


    </div>
               <div class="dvLeftBox" style=" margin-top :10px; padding  :5px 10px 0px 10px; border-radius :25px 25px 0px 0px; border :2px solid ;">

        <asp:Panel ID="pnlLeftGrid" Width ="100%" runat ="server" Height ="475px" >
          <asp:GridView Width="100%" ID="gridProperties" runat="server" Visible="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="false" AllowPaging ="false" ShowHeader ="false"  >
                    <Columns>
                                <asp:BoundField DataField="FieldName" HeaderText="Lodgment ID" Visible ="true" ItemStyle-Width="90"/>
                                <asp:BoundField DataField="FieldValue" HeaderText="Narration" ItemStyle-Wrap ="true" ItemStyle-Width="100" DataFormatString="{0}" />
                    </Columns>
          </asp:GridView>
          
          <div id="dvActionHardShip" style ="width :100%; " runat ="server" visible ="false" >
                
               <div  id="dvSetDate" runat ="server" visible="false" style="padding-top: 10px; padding-bottom :10px;">
                     <div style ="float :left; margin-right : 5px;" ><span >Set Price Date :</span></div>
                     <div style ="float :left  ;"><asp:TextBox ID="txtPriceDateSingle" Width ="170px" runat="server"></asp:TextBox></div>
                </div>
               <div id="dvSetStatus" runat ="server" style="padding-top : 30px; padding-bottom :30px; visibility :hidden ;">
                     <div style ="float :left; margin-right : 5px;"><span >Set Status :</span></div>
                     <div style ="float :left  ;"><asp:DropDownList ID="ddProcessingStatusSingle" runat="server" Width ="200px"></asp:DropDownList></div>
                </div>
               <div id="dvHardShipUpdatebtn" runat ="server" style="padding : 5px; margin :5px;">
                   
                    <asp:Button ID="btnHardShipProcessingSingle" runat="server" Text="Process" Width ="100%" Visible="false"/>

                </div>

               <div id="dvHardShipSNR" runat="server" style="padding : 5px;">
                   
                    <asp:Button ID="btnPrintSNR" runat="server" Text="Print SNR Form" Width ="100%" Visible="False"/>

                </div>


           </div>
</asp:Panel>

    </div>
              
          </div>
     </div>

               <asp:Button id="btnShowAVCDetailsPopup" runat="server" style="display:none" />
                 <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
               <asp:Button id="btnShowNSITFDetailsPopup" runat="server" style="display:none" />

               

       <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
      
                  <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div2" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div3" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div4" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
            <div id="Div5" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="80px" Width="100%" MaxLength="70"></asp:TextBox>
                </div>

                 <div id="Div6"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div7"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/add.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppComments" runat="server" Text="Close" />
    </asp:Panel>


                 <asp:ModalPopupExtender ID="mpAVCDetail" runat="server" PopupControlID="pnlAVC" TargetControlID="btnShowAVCDetailsPopup" CancelControlID="btnAVCDetails" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlAVC" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="680px">

          <div class ="bodyMainDiv" style="height:660px; width: 98%" >
          <div id="Div8" style ="padding-left :20px;"><h2><span>AVC Details</span></h2></div>
          
          <div id="Div9" class ="SubbodyMainDiv">
           
               <div id="divApplicationCode" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtApplicationCode" runat="server" Width ="250px" Enabled ="false" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqApplicationCode" runat="server" ErrorMessage="*" ControlToValidate="txtApplicationCode" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AVCDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="divAVCAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total AVC Processed (Taxable) :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAVCAmount" runat="server" Width ="250px" Text ="0.00"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAVCAmTTax" runat="server" ErrorMessage="*" ControlToValidate="txtAVCAmount" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AVCDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compAVCAmtTax" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAVCAmount" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="AVCDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="divAVCAmountNOTax" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total AVC Processed (Non-Taxable):</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAVCAmountNoTax" runat="server" Width ="250px" Text ="0.00"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAVCAmTNoTax" runat="server" ErrorMessage="*" ControlToValidate="txtAVCAmountNoTax" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AVCDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compAVCAmtNoTax" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAVCAmountNoTax" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="AVCDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="divAVCUnits" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total AVC Units (Taxable) :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAVCUnits" runat="server" Width ="250px" Text ="0.0000"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqTotalAVCUnit" runat="server" ErrorMessage="*" ControlToValidate="txtAVCUnits" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AVCDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compTotalAVCUnit" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAVCUnits" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="AVCDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="div10" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Total AVC Units (Non-Taxable) :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNoTAXAVCUnits" runat="server" Width ="250px" Text ="0.0000"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqTAXAVCUnits" runat="server" ErrorMessage="*" ControlToValidate="txtNoTAXAVCUnits" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AVCDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compTAXAVCUnits" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtNoTAXAVCUnits" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="AVCDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="divAVCPrice" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Average Price :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAVGPrice" runat="server" Width ="250px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAVGPrice" runat="server" ErrorMessage="*" ControlToValidate="txtAVGPrice" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AVCDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compAVGPrice" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAVGPrice" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="AVCDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="divPaymentPriceDate" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Payment Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPaymentPriceDate" runat="server" Width ="250px" ReadOnly ="true" ></asp:TextBox>
                    </div>
               </div>

               <div id="dvPayingPrice" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Paying Price :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPayingPrice" runat="server" Width ="250px" ReadOnly ="true" Text ="0.0000" ></asp:TextBox>
                    </div>
               </div>

               <div id="dvCurrentAVCValue" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Current Value :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtCurrentValue" runat="server" Width ="250px" ReadOnly ="true" Text ="0.00" ></asp:TextBox>
                    </div>
               </div>

               <div id="dvTaxDeduction" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Tax Deduction :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtTaxDeduction" runat="server" Width ="250px" ReadOnly ="true" Text ="0.00" ></asp:TextBox>
                    </div>
               </div>

               <div id="dvNetPayable" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Net Payable :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNetPayable" runat="server" Width ="250px" Text ="0.00" ></asp:TextBox>
                    </div>
               </div>

               <div id="dvApply" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">    </span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:Button ID ="btnCalculate" runat="server" Text ="Calculate" ValidationGroup="AVCDetails" />
                         <asp:Button ID ="btnApply" runat="server" Text ="Apply" ValidationGroup="AVCDetails" />
                         
                    </div>
               </div>

              

          </div>

     </div>
          <br />

    <asp:Button ID="btnAVCDetails" runat="server" Text="Close" />
     </asp:Panel>



                                <asp:ModalPopupExtender ID="mpNSITFDetail" runat="server" PopupControlID="pnlNSITF" TargetControlID="btnShowNSITFDetailsPopup" CancelControlID="btnNSITFCloseDetails" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlNSITF" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="330px">

          <div class ="bodyMainDiv" style="height:310px; width: 98%" >
          <div id="Div11" style ="padding-left :20px;"><h2><span>NSITF Details</span></h2></div>
          
          <div id="Div12" class ="SubbodyMainDiv">
           
               <div id="dvNSITFApplicationCode" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Application Code :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNSITFApplicationCode" runat="server" Width ="250px" Enabled ="false" ValidationGroup="NSITFDetails" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqNSITFApplicationCode" runat="server" ErrorMessage="*" ControlToValidate="txtNSITFApplicationCode" Font-Bold="True" SetFocusOnError="True" ValidationGroup="NSITFDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
               </div>


               <div id="dvNSITFAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Initial Amount Paid :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNSITFAmount" runat="server" Width ="250px" Text ="0.00" Enabled ="true" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqNSITFAmount" runat="server" ErrorMessage="*" ControlToValidate="txtNSITFAmount" Font-Bold="True" SetFocusOnError="True" ValidationGroup="NSITFDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compNSITFAmount" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtNSITFAmount" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="NSITFDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="dvNSITFAmountRecieved" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Amount Recieved into RSA:</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNSITFAmountRecieved" runat="server" Width ="250px" Text ="0.00" Enabled ="true" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqNSITFAmountRecieved" runat="server" ErrorMessage="*" ControlToValidate="txtNSITFAmountRecieved" Font-Bold="True" SetFocusOnError="True" ValidationGroup="NSITFDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compNSITFAmountRecieved" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtNSITFAmountRecieved" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="NSITFDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="dvNSITFAmountRequested" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">NSITF Request Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtNSITFAmountRequested" runat="server" Width ="250px" Text ="0.0000"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqNSITFAmountRequested" runat="server" ErrorMessage="*" ControlToValidate="txtNSITFAmountRequested" Font-Bold="True" SetFocusOnError="True" ValidationGroup="NSITFDetails" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compNSITFAmountRequested" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtNSITFAmountRequested" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="NSITFDetails"></asp:CompareValidator>
                    </div>
               </div>

               <div id="Div24" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">    </span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                        
                         <asp:Button ID ="btnNSITFApply" runat="server" Text ="Apply" ValidationGroup="NSITFDetails" />
                         <asp:Button ID="btnNSITFCloseDetails" runat="server" Text="Close" />
                    </div>
               </div>

              

          </div>

     </div>
          <br />

    
     </asp:Panel>



               <asp:Button id="btnShowApplicationSummary" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpApplicationSummary" runat="server" PopupControlID="pnlAppSummary" TargetControlID="btnShowApplicationSummary" CancelControlID="btnCloseApplicationSummary" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
                  <asp:Panel ID="pnlAppSummary" runat="server" CssClass="modalPopup" align="center" Height ="300px" style = "display:none" Width ="600px">

    <div id="Div13" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div14" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div15" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Summary</strong></span></div>
            <div id="Div16" class="dvBoxbody">
               
             

                        <asp:Panel ID="pnlUploadDetail" Width ="98%" runat ="server" BorderStyle="Solid" Height ="220px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridApplicationSummary" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False">
                                                         <Columns>

                                                              <asp:BoundField DataField="txtStatus" HeaderText="Application Type" />
                                                              <asp:BoundField DataField="ApplicationCount" HeaderText="Application Count" />
                                                              
                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnCloseApplicationSummary" runat="server" Text="Close" />
    </asp:Panel>


               <asp:Button id="btnShowIACCommentPopup" runat="server" style="display:none" />
   <asp:ModalPopupExtender ID="mpAppIACComments" runat="server" PopupControlID="pnlAppIACComments" TargetControlID="btnShowIACCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>                
   <asp:Panel ID="pnlAppIACComments" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="530px">

    <div id="Div17" class ="dvSideBox"> 
        
        <div id="Div18" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div19" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Internal Control Application Comment</strong></span></div>
            <div id="Div20" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
                    <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtIACApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
               </div>

                
               <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationIACComment" runat ="server" ReadOnly ="true"  TextMode ="MultiLine" ValidationGroup  ="AppCommentStatus" Height="400px" Width="95%"></asp:TextBox>

                </div>

            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppIACComments" runat="server" Text="Close" />
    </asp:Panel>


   <asp:Button id="btnShowLagacyContPopup" runat="server" style="display:none" />
   <asp:ModalPopupExtender ID="mpLagacyPopupExtender" runat="server" PopupControlID="pnlLagacyCont" TargetControlID="btnShowLagacyContPopup" CancelControlID="btnMPAppLagcy" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>                
   <asp:Panel ID="pnlLagacyCont" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="130px">

    <div id="Div22" class ="dvSideBox"> 
        
        <div id="Div23" style="border-color:#3a4f63; border :2px solid ; height :100PX;">

            <div id="Div25" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Legacy Contribution Notifcation</strong></span></div>
            <div id="Div26" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
                    <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Please be Informed That Lagacy Contribution ExistS for this customer :</span></div>
                   
               </div>
                   
               </div>
              
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppLagcy" runat="server" Text="Close" />
    </asp:Panel>


               </ContentTemplate>
     </asp:UpdatePanel>



</asp:Content>

