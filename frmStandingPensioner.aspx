<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmStandingPensioner.aspx.vb" Inherits="frmStandingPensioner" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
    

   <%-- <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="updcontent" runat="server">
            <ProgressTemplate>           
            <img alt="progress" src="images/loading.png"/>
               Processing...           
            </ProgressTemplate>
        </asp:UpdateProgress>--%>

    <%--<asp:UpdatePanel ID="updcontent" runat ="server" >
        <ContentTemplate>--%>

            <div style="float:left; width :1600px; ">
                <div id="dvSpace" style ="height :30px;"></div>

                <div id="dvSideBox" style="float:left; width:320px; height :300px;" >


                    <div id="Div1" style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px;" runat ="server" visible ="false"   >
                        <div id="Div2" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Email Validation</strong></span> </div>
                        

            <div style ="text-align :right ; padding : 5px;" ><span >Enter The Nos Of Email to Validate: </span></div>
            <div id="divEmailAddy" style ="padding :5px; text-align :right ;" ><asp:TextBox ID="txtEmailCount" Width ="285px" runat ="server"></asp:TextBox><asp:RequiredFieldValidator ID="reqEmailCount" runat="server" ErrorMessage="*" ControlToValidate="txtEmailCount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EmailCount" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><asp:CompareValidator ID="compareEmailCount" runat="server" ErrorMessage="Please Enter Number Only" Display="Dynamic" ValidationGroup="EmailCount" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtEmailCount" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:CompareValidator></div>
            <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnValidate" runat="server" Text="Validate" ValidationGroup="EmailCount" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
        </div>



            <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ;">
                <div id="dvCrHeader" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Import New Pension Entrants</strong></span> </div>
                <div id="dvCrBody" style="height:auto; padding:5px;">
                    <div style="padding:5px;">
                        <div style ="float :left; width:80px"><span>Start Date :</span></div>
                        <div style="width:260px;"><asp:TextBox ID="txtStartDate" runat="server" Width ="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

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
                    <div style="padding:5px;">
                        <div style ="float :left; width:80px"><span>End Date :</span></div>
                        <div style="width:260px; "><asp:TextBox ID="txtEndDate" runat="server" Width ="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtEndDate" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
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

                    <div style="padding:5px;">
                        
                        <div style="width:260px; ">
                             <asp:CheckBox ID="chkUpload" runat ="server" Text ="Upload File" />
                          <asp:FileUpload ID ="FileUploadControl" runat ="server" Width ="300px"  />
                            
                        </div>
                          
                    </div>


                    <div id="dvSpan" style="padding:5px;" runat ="server" >
                        
                        <div style="width:260px; ">
                             <span id="spUploadFeedback" runat ="server" visible ="false"  ></span>
                            
                        </div>
                          
                    </div>


                    <div style="padding:5px; width :100%;">
                        
                        <div style="width:50%; float :left ;">
                             <asp:Button ID="btnGenerate" runat="server" Text="Import Records" OnClick ="UploadButton_Click" />
                          <%--<asp:FileUpload ID ="FileUpload1" runat ="server" Width ="300px"  />--%>
                            
                        </div>
                         <div> <asp:Button ID="btnViewRecord" runat="server" Text="View Records" /> </div>
                          
                    </div>

                     
                    <div style="padding:0px; float :left ; width :300px;">
                       
                        <div style ="text-align:center; float :left ;"></div>
                          <div style ="text-align:right;"></div>

                    </div>


                     <%--<asp:Panel ID="Panel1" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span>
                          </asp:Panel>--%>
                     <%--<div style ="text-align:center ; height :40px; margin-top:20px; color :red ;"> <span id="spApplicationCount" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span> Record(s) Retrieved !</div>--%>
                    
                </div>
            </div>
        </div>

                <div style="float:left; width:1250px; padding-left :20px;" >

                    <%--<div style ="width :-1px; text-align :right ; padding :5px;">

                         <asp:DropDownList ID="ddUsers" runat ="server" Visible ="true" Width ="300px" AutoPostBack="True" ></asp:DropDownList>
                         <asp:CheckBox ID="chkFilterByUser" Text ="Filter By User" runat="server" AutoPostBack="True" />
                         <asp:CheckBox ID="chkShowMyApplications" Text ="Show My Applications" runat="server" AutoPostBack="True" />
                         <asp:CheckBox ID="chkShowAll" Text ="Show All" Checked ="true"  runat="server" AutoPostBack="True" />
                         <asp:ImageButton ID="btnExport" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel"    />

                    </div>--%>

                <div id="dvValidatdEmail" style =" border :2px solid ;">
                    <asp:Panel ID="pnlValidatdEmail" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="500px" >
                    <asp:GridView Width="100%" ID="gridPensioner" runat="server" Visible="true" PageSize="70" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridPensioner_RowDataBound">
                    <Columns>

                        <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPensionerChecked" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="pkiStandingPensioneer" HeaderText="SI_ID" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="txtFullName" HeaderText="Full Name" Visible ="true" ItemStyle-Width="250"/>
                        <asp:BoundField DataField="numPWAmount" HeaderText="PW Amount" DataFormatString="{0:N}" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="numPension" HeaderText="Pension Amount" DataFormatString="{0:N}" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="Frequency" HeaderText="Pension Frequency" Visible ="true" ItemStyle-Width="70"/>
                        <asp:BoundField DataField="dteAnniversary" HeaderText="Anniversary Date" DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="BankName" HeaderText="Bank Name" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="BankBranch" HeaderText="Bank Branch" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="txtBankAccount" HeaderText="Account Number" Visible ="true" ItemStyle-Width="100"/>
                        
                        
                        <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewPensionerDetail" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="Edit Pension Details" ItemStyle-Width ="10px" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>

                         <%--<asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                        </asp:TemplateField>--%>


                          <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnCancelApplication_Click" ID="btnCancelApplication" runat ="server" ImageUrl="~/images/remove.png" ToolTip="De-activate Pensioner" OnClientClick="BtnCancelApplication_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnActivateApplication_Click" ID="btnActivateApplication" runat ="server" ImageUrl="~/images/add.jpg" ToolTip="Activate Pensioner" OnClientClick="BtnActivateApplication_Click" ItemStyle-Width ="10px" />
                                        
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
                     <div><hr /></div>
                <div style =" border :2px solid; height :20px; padding :20px;">
                     <div style="float:left ; padding-right :20px; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
                     <div style="float:left; padding-right :0px;"><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>
                     <div id="Div11" style="float:left; padding-left :20px;" runat ="server" visible="true" ><asp:Button ID="btnSIForApproval" runat="server" Text="Send For Approval" /></div>
                     <div style="float:left ;padding-left :30px; "><asp:ImageButton ID="imgExportExcel" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Download Schedule Soft Copy" Visible="true"/>
                     <div id="dvSPRowCount" style="float:right; padding-left :20px;" runat ="server" visible="false" ><span id="spRowCount" runat ="server" style ="color :red ; font-weight :200 ;"></span></div>
                </div>

                </div>


            </div>

                 </div>
              <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
                        <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div3" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div4" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div5" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
            <div id="Div6" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="80px" Width="100%" MaxLength="70"></asp:TextBox>
                </div>

                 <div id="Div7"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div8"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppComments" runat="server" Text="Close" />
    </asp:Panel>



                 <asp:Button id="btnPopupPension" runat="server" style="display:none" />
                 <asp:ModalPopupExtender ID="MPPensioner" runat="server" PopupControlID="pnlPensioner" TargetControlID="btnPopupPension" CancelControlID="btnCancelPensioner" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlPensioner" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:250px; width: 100%" >
          <div id="Div33" style ="padding-left :20px;"><h2><span>Edit Pensioner Details</span></h2></div>
          
          <div id="Div35" class ="SubbodyMainDiv" style="height:250px;">

               <div id="dvPensionerID" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Pensioner ID :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPensionerID" runat="server" Width="300px" ValidationGroup="NSITFDetails" ReadOnly ="true" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqtxtPensionerID" runat ="server" ErrorMessage="*" controlToValidate="txtPensionerID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         

                    </div>

               </div>

               <div id="dvPensionAmount" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Pensioner ID :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPensionAmount" runat="server" Width="300px" Text ="0.00" ValidationGroup="PensionerDetails" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqtxtPensionAmount" runat ="server" ErrorMessage="*" controlToValidate="txtPensionAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="nPensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compPensionAmount" runat="server" ErrorMessage="*" ControlToValidate="txtPensionAmount" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ForeColor="Red" ValidationGroup="nPensionerDetails"></asp:CompareValidator>

                         

                    </div>

               </div>

               <div id="dvPaymentFrequency" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Payment Frequency :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         
                         <asp:DropDownList ID ="ddPaymentFrequency" runat ="server" Width ="300px" ValidationGroup="nPensionerDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqPaymentFrequency" runat ="server" ErrorMessage="*" controlToValidate="ddPaymentFrequency" Display="Dynamic" SetFocusOnError="True" ValidationGroup="nPensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>

               </div>

               <div id="dvAccountNumber" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Account No :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAccountNumber" runat="server" Width="300px" ValidationGroup="nPensionerDetails" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAccountNumber" runat ="server" ErrorMessage="*" controlToValidate="txtAccountNumber" Display="Dynamic" SetFocusOnError="True" ValidationGroup="nPensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>


                    </div>

               </div>

               <div id="dvPensionerBanks" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Banks :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         
                         <asp:DropDownList ID ="ddPensionerBanks" runat ="server" Width ="300px" ValidationGroup="nPensionerDetails" AutoPostBack="True"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqddPensionerBanks" runat ="server" ErrorMessage="*" controlToValidate="ddPensionerBanks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="nPensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>

               </div>

               <div id="dvPensionerBankBranch" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Bank Branches :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         
                         <asp:DropDownList ID ="ddBankBranches" runat ="server" Width ="300px" ValidationGroup="nPensionerDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ErrorMessage="*" controlToValidate="ddBankBranches" Display="Dynamic" SetFocusOnError="True" ValidationGroup="nPensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>

               </div>
  
               <div id="dvActionPensioner" style ="width :100%; float :left ;">
                    
                    <div id="Div56" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         
                         <asp:Button ID="btnPensionerOk" runat="server" Text="Ok" ValidationGroup="nPensionerDetails" CausesValidation ="true"   />
                         <asp:Button ID="btnCancelPensioner" runat="server" Text="Cancel" />
               
                    </div>
                         
               </div>



          </div>

     </div>
          <br />
          <div></div>
    
    </asp:Panel>



                 <asp:Button id="btnPopupDeactivationReasion" runat="server" style="display:none" />
                 <asp:ModalPopupExtender ID="mpDeativationReason" runat="server" PopupControlID="pnlPensionerDeactivationReason" TargetControlID="btnPopupDeactivationReasion" CancelControlID="btnDeactivationCancel" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlPensionerDeactivationReason" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:250px; width: 100%" >
          <div id="Div9" style ="padding-left :20px;"><h2><span>Pensioner Deactivation Details</span></h2></div>
          
          <div id="Div10" class ="SubbodyMainDiv" style="height:250px;">

               <div id="dvDeactivationPensioner" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Pensioner ID :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDPensionerID" runat="server" Width="300px" ValidationGroup="PensionerDDetails" ReadOnly ="true" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDPensionerID" runat ="server" ErrorMessage="*" controlToValidate="txtDPensionerID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PensionerDDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         

                    </div>

               </div>

               <div id="dvDeactivationReason" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Deactivation Reason :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         
                         <asp:DropDownList ID ="ddDeactivationReason" runat ="server" Width ="300px" ValidationGroup="PensionerDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="ReqDeactivationReason" runat ="server" ErrorMessage="*" controlToValidate="ddDeactivationReason" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PensionerDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>

               </div>

               <div id="dvDeactivationComment" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Deactivation Comment :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDeactivationComment" runat="server" Width="300px" TextMode ="MultiLine" Height ="100px"  ValidationGroup="PensionerDeativation"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDeactivationComment" runat ="server" ErrorMessage="*" controlToValidate="txtDeactivationComment" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PensionerDeativation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>

               </div>
  
               <div id="dvDeactivationAction" style ="width :100%; float :left ;">
                    
                    <div id="Div20" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         
                         <asp:Button ID="btnDeactivationOk" runat="server" Text="Ok" ValidationGroup="PensionerDeativation" CausesValidation ="true"   />
                         <asp:Button ID="btnDeactivationCancel" runat="server" Text="Cancel" />
               
                    </div>
                         
               </div>



          </div>

     </div>
          <br />
          <div></div>
    
    </asp:Panel>
















       <%-- </ContentTemplate>

    </asp:UpdatePanel>--%>

</asp:Content>

