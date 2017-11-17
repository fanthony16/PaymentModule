<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmEnhancementApplication.aspx.vb" Inherits="frmEnhancementApplication" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>     

     <asp:UpdatePanel ID="upFormPanel" runat ="server" >
          <ContentTemplate>

    <%-- <div><asp:Panel ID="pnlError" runat ="server" Visible="false">
          <asp:Image ID="imgError" runat="server" ImageUrl="~/images/ERROR_ICON.png" />
          <asp:Label ID ="lblError" runat ="server" Font-Bold="True" ForeColor="Red"></asp:Label></asp:Panel>
     </div>--%>
     <div class ="bodyMainDiv">

          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Benefit Payment Application</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv">
               
               <asp:Accordion ID="UserAccordion"  runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                
                <Panes>

                    <asp:AccordionPane ID="PersonalDetails"  runat="server">
                        <Header><a href="#" class="accordionhref">Personal Details</a></Header>
                        <Content>
                            <asp:Panel ID="UserReg" runat="server">
                                <div id="dvPersonalDetails" style="width :100%">
                                     <div id="dvPersonalfor" style="float :left ; width :70%; ">

                                          <div id="dvPIN" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Pencom PIN :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtPIN" Width ="300px" runat="server" ValidationGroup="FindPersonDetails"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqPIN" runat ="server" ErrorMessage="*" ControlToValidate="txtPIN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="FindPersonDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnFind" runat="server" Text="..." ValidationGroup="FindPersonDetails" />
                                                    <asp:Label ID="lblTitle" runat="server" Visible ="false" ></asp:Label>
                                               </div>
                                          </div>

                                          <div id="dvSurName" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Surname :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSurname" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqSurname" runat ="server" ErrorMessage="*" ControlToValidate="txtSurname" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvFirstName" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">FirstName :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtFirstName" Width ="300px" runat="server" ReadOnly ="true"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqFirstName" runat ="server" ErrorMessage="*" ControlToValidate="txtFirstName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvOthernames" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Othernames :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtOtherNames" Width ="300px" runat="server" ReadOnly ="true"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqOthernames" runat ="server" ErrorMessage="*" ControlToValidate="txtOtherNames" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvDOB" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Date Of Birth :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtDOB" Width ="300px" runat="server" ReadOnly ="true"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqDOB" runat ="server" ErrorMessage="*" ControlToValidate="txtDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvSex" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Sex :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSex" Width ="300px" runat="server" ReadOnly ="true"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqSex" runat ="server" ErrorMessage="*" ControlToValidate="txtSex" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvAge" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Age :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAge" Width ="300px" runat="server" ReadOnly ="true"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqAge" runat ="server" ErrorMessage="*" ControlToValidate="txtAge" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvDeclaredAge" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Declared Age :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtDelaredAge" Width ="300px" runat="server" ReadOnly ="false"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqDeclaredAge" runat ="server" ErrorMessage="*" ControlToValidate="txtDelaredAge" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvEmployer" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Employer :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtEmployer" Width ="300px" runat="server" Enabled="false" ReadOnly ="true" TextMode ="MultiLine" Height ="50px"   ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqEmployer" runat ="server" ErrorMessage="*" ControlToValidate="txtEmployer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>


                                          <div id="dvEmailAddress" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Email Address :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtEmail" Width ="300px" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgUpdateEmail" runat="server" ImageUrl="~/images/update.bmp" ToolTip="Update Email Address" ValidationGroup ="updateEmail" />
                                                    <asp:RequiredFieldValidator ID="reqEmail" runat ="server" ErrorMessage="*" controlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>                            


                                          <div id="dvTelephone" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Telephone No :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtPhone" Width ="300px" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgUpdatePhone" runat="server" ImageUrl="~/images/update.bmp" ToolTip="Update Telephone Number" />
                               
                                               </div>
                                          </div>                            


                                     </div>

                                     <div id="dvPersonalPassport" style="float :left ; width :30%;  ">
                                          <div style="padding: 8px; margin: 0px; border-style: solid; border-width: thin; float: left; width: 80%; height: 200px; border-radius: 25px;">
                                               <div style="height : 180px"><asp:Image ID="imgPassport" runat="server" Width="90%" Height ="165px" ImageUrl="~/Images/untitled.png" /></div>
                                               <div style="float: left; width: 100%;"><span >Passort Photograph</span></div>
                                               
                                          </div>
                                          <div style ="float :left ;width :100%;" ><asp:CheckBox ID="chkConfirmedPassport" Text =" Is Passport Comfirmed?" TextAlign ="Right"  runat="server" /></div>
                                          <div style="text-align :center; width :90%"><asp:Image ID="imgSignature" runat="server" ImageUrl="~/Images/signature.jpg" /></div>
                                          <div style ="float :left ;width :100%;" ><asp:CheckBox ID="chkConfirmedSignature" Text =" Is Signature Comfirmed?" TextAlign ="Right"  runat="server" /></div>
                                     </div>

                                </div>
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>

                    <asp:AccordionPane ID="FundingDetails"  runat="server">
                        <Header><a href="#" class="accordionhref">Funding Details</a></Header>
                        <Content>
                            <asp:Panel ID="Panel3" runat="server">
                                
                                <div id="Div8" style="float :left ; width :25%; height :400px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px; background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Funding Information</span>
                                      
                                              <div id="Div36" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Current Fund :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <span id="spFundName" runat ="server" >RSA Fund</span>
                                               </div>
                                          </div>
                                          
                                              <div id="dvRSABalance" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Current Balance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRSABalance" Width ="180px" runat="server" Enabled="false" Text ="0.00" ></asp:TextBox>
                                               </div>

                                          </div>                                        

                                              <div id="dvOldPensionAmount" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Old Pension Amount :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtOldPension" Width ="180px" runat="server" Enabled="false" Text ="0.00" ></asp:TextBox>
                                               </div>

                                          </div>                                        

                                              <div id="dvNewPensionAmount" class ="dvBoxRows" style="margin-top : 15px;">
                                               
                                                    <div class="dvBoxRowsFieldLabel">
                                                         <span style ="font-size : medium;">Recommended Amount :</span>
                                                    </div>

                                                    <div style ="text-align :left ;">
                                                         <asp:TextBox ID="txtRecommendedAmount" Width ="180px" runat="server" Enabled="false" Text ="0.00" ></asp:TextBox>
                                                    </div>

                                              </div>                                        

                                              <div id="dvEnhancementReference" class ="dvBoxRows" style="margin-top : 15px;">
                                               
                                                    <div class="dvBoxRowsFieldLabel">
                                                         <span style ="font-size : medium;">Reference Number :</span>
                                                    </div>

                                                    <div style ="text-align :left ;">
                                                         <asp:TextBox ID="txtReferenceNo" Width ="180px" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>

                                              </div>                                        

                                     </div>
                                </div> 
                                

                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>

                    <asp:AccordionPane ID="AppInfo"  runat="server">
                        <Header><a href="#" class="accordionhref">Other Payment Application Information</a></Header>
                        <Content>

                            <asp:Panel ID="Panel2" runat="server">
                               
                                <div id="dvApplicationLocation"  style="float :left ; width :28%;height :380px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Application Location</span>

                                          <div id="dvApplicationState" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">State :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddApplicationState" runat="server" Width="250px" CausesValidation="False" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqApplicationState" runat ="server" ErrorMessage="*" controlToValidate="ddApplicationState" Display="Dynamic" SetFocusOnError="True" ValidationGroup="SubmittingApplicatiom" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvOfficeLocation" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">LPPFA Office :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddPFAOfficeLocation" runat="server" Width="250px"></asp:DropDownList>
                                               </div>
                                          </div>

                                          <div id="dvApplicationDate " class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Application Date :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtApplicationDate" runat="server" Width="245px" Enabled ="false" ></asp:TextBox>
                                               </div>
                                               <asp:RequiredFieldValidator ID="reqApplicationDate" runat ="server" ErrorMessage="*" controlToValidate="txtApplicationDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="SubmittingApplicatiom" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calApplicationDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlApplicationDate" Position="Bottom" TargetControlID="txtApplicationDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlApplicationDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calApplicationDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calApplicationDate" runat="server" BackColor="White" 
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

                                          <div id="dvApplicationType" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Application Type :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddApplicationType" runat="server" Width="250px" AutoPostBack="True"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqApplicationType" runat ="server" ErrorMessage="*" controlToValidate="ddApplicationType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="SubmittingApplicatiom" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>


                                     </div>
                                </div>         
                                  
                                <div id="dvRequiredDocuments" style="float :left ; width :29%;height :380px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px; background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Required Documents</span>
                                          <div id="dvSelectDoc" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Select Doc. :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                    <asp:DropDownList ID="ddRequiredDocuments" runat="server" Width="250px" AutoPostBack="True" ValidationGroup ="valSchedule"></asp:DropDownList>
                                                    
                                               </div>

                                          </div>

                                          <div id="dvRecievedDate" class ="dvBoxRows" style="margin-top : 10px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Recieved Date :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRecievedDate" Width ="250px" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqRecievedDate" runat ="server" ErrorMessage="*" controlToValidate="txtRecievedDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calRecievedDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlRecievedDate" Position="Bottom" TargetControlID="txtRecievedDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlRecievedDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                  <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calRecievedDate" />

                                                                     </Triggers>
                                                                 <ContentTemplate>


                                                                      <asp:Calendar ID="calRecievedDate" runat="server" BackColor="White" 
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
                                          </div>

                                          <div id="dvUploadDocument" class ="dvBoxRows" style ="margin-top : 1px;">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Upload Document :</span>
                                               </div>

                                               <div style ="text-align :right ; padding-right :25px;">
                                                    <asp:AjaxFileUpload ID="flReqDocUpload" runat="server" OnUploadComplete="AjaxFileDocumentUploadEvent"  ValidationGroup ="valSchedule" />
                                                    
                                               </div>

                                                    
                                          </div>

                                          <div id ="dvVerified" runat ="server" visible ="false">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Is Verified? :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                    <asp:CheckBox ID="chkDocVerified" runat="server" Checked ="false"  />
                                                    
                                               </div>

                                          </div>

                                          <div id="dvBtnRecievedDoc" class ="dvBoxRows" style="margin-top : 10px;">

                                               <div class="dvBoxRowsFieldLabel" style="width :48%">
                                                    <span>Max.Size of File is 1MB</span>
                                               </div>
                                               <div style ="text-align :right ; padding-right :25px;">
                                                    <asp:Button ID="btnAddRecievedDoc" runat="server" Text="Add to Recieved Document" ValidationGroup ="valSchedule" />
                                               </div>

                                          </div>

                                           <div id="dvfileSizeError" runat ="server" visible ="false"  class ="dvBoxRows" style="margin-top : 15px;">
                                                <%--<asp:Panel ID="pnlFileSizeError" runat ="server" Visible="true">--%>
                                                       
                                                     <asp:Image ID="imgFileSizeError" runat="server" ImageUrl="~/images/ERROR_ICON.png" />
                                                     <asp:Label ID ="lblFileSizeError" runat ="server" Font-Bold="false" ForeColor="Red"></asp:Label>

                                                <%--</asp:Panel>--%>
                                          </div>



                                          <div id="dvCommentGroup" runat ="server" visible ="false"  class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Comment Groups :</span>
                                               </div>
                                               <div style ="text-align :left ;">

                                                    <asp:DropDownList ID="ddCommentGroup" runat="server" Width="250px"></asp:DropDownList>
                                                    
                                                    
                                               </div>
                                          </div>

                                     </div>
                                </div> 

                                <div id="dvRecievedDocument" runat="server"  style="float :left ; width :40%; height :380px;  border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Submitted Documents</span>
                                                                             
                                          <div id="dvGridRecievedDocument" class ="dvBoxRows" style="margin-top : 15px; margin-left :7px;">
                                               <asp:Panel ID="pnlUploadDetail" Width ="98%" runat ="server" BorderStyle="Solid" Height ="290px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridRecievedDocument" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False">
                                                         <Columns>
                                                              <asp:TemplateField HeaderText="">
                                                                 <ItemTemplate>

                                                                      <asp:CheckBox ID="chkSelect" runat="server" Enabled="true" Width ="10" />
                                                                 </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:BoundField DataField="DocumentName" HeaderText="Desciption" />
                                                              <asp:BoundField DataField="RecievedDate" HeaderText="Date" DataFormatString="{0:d}" />
                                                              <asp:BoundField DataField="DocumentPath" HeaderText="" HeaderStyle-Width="0" Visible ="true"  />
                                                              <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewDocument" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Document" OnClientClick="BtnViewDetails_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                                                               </asp:TemplateField>
                                                              <asp:BoundField DataField="IsVerified" HeaderText="" HeaderStyle-Width="0" Visible ="true"  />
                                                              

                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>
                                               <div id="dvButtonRemoveDoc" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel" style="width :48%">
                                                    <span id="dvDocumentError" style="color:red ;" runat="server" visible ="false" > Multiple Selection Not allowed!</span>
                                               </div>

                                               <div style ="padding-right :8px; float :left ;"> 

                                                    <asp:Button ID="btnRemoveDocument" runat="server" Text="Delete From Recived Documents" ValidationGroup="RemoveDocument" />
                                               </div>

                                               <div style ="padding-right :8px; float :right  ;"> 

                                                    <asp:Button ID="btnRemoveAllDocument" runat="server" Text="Delete All Recieved Documents" ValidationGroup="RemoveDocument" Visible ="false"  />
                                               </div>

                                              </div>
                                          </div>
                                          

                                     </div>
                                </div> 

                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>

                    <asp:AccordionPane ID="BankDetails"  runat="server">
                        <Header><a href="#" class="accordionhref">Bank Details / Other Comments</a></Header>
                        <Content>
                            <asp:Panel ID="Panel4" runat="server">
                               

                                <div id="dvBankDetails" style="float :left ; width :33%; height :350px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 40px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Bank Details</span>

                                          <div id="dvAccountName" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Account Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAccountName" runat="server" Width ="250px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqAccountName" runat ="server" ErrorMessage="*" controlToValidate="txtAccountName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="SubmittingApplicatiom" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvAccountNumber" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Account No :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAccountNumber" runat="server" Width ="250px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqAccountNumber" runat ="server" ErrorMessage="*" controlToValidate="txtAccountNumber" Display="Dynamic" SetFocusOnError="True" ValidationGroup="SubmittingApplicatiom" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvBVN" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">BV Number :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtBVN" runat="server" Width ="250px"></asp:TextBox>
                                                    
                                               </div>
                                          </div>
                                          <div id="dvBank" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Bank :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddBankName" runat="server" Width="250px" AutoPostBack="True"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="reqValBank" runat ="server" ErrorMessage="*" controlToValidate="ddBankName" Display="Dynamic"  SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="SubmittingApplicatiom" ></asp:RequiredFieldValidator>--%>
                                               </div>
                                               </div>
                                          <div id="dvBankBranch" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Bank Branch :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:DropDownList ID="ddBankBranch" runat="server" Width="250px" ValidationGroup ="SubmittingApplicatiom"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqBranch" runat ="server" ErrorMessage="*" controlToValidate="ddBankBranch" Display="Dynamic"  SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="SubmittingApplicatiom" ></asp:RequiredFieldValidator>
                                               </div>
                                              
                                          </div>
                                          <div id="dvAccountConfirmed" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;"></span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:CheckBox ID="chkBankConfirmed" runat="server" Text ="Bank Details Confirmed ? "  />
                                               </div>
                                          </div>

                                     </div>
                                     
                                </div> 
                                
                                <div id="dvOtherComments" style="float :left ; width :33%; height :350px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Other Comments</span>

                                         <div id="dvApplicationStatus" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Status :</span>
                                               </div>
                                               <div style ="text-align :left ;">

                                                    <asp:DropDownList ID="ddStatus" runat="server" Width="300px" Visible ="false" ></asp:DropDownList>
                                                    
                                               </div>
                                          </div>

                                         <div id="dvOtherApplicationComment" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Other Comments :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtOtherComments" runat="server" MaxLength ="1000" Width ="300px" TextMode ="MultiLine" Height ="150px"></asp:TextBox>
                                               </div>
                                          </div>

                                         <div id="dvSubmit" class ="dvBoxRows" >

                                             <asp:Button ID="btnSubmit" runat="server" Text="Submit Application" ValidationGroup ="SubmittingApplicatiom" />

                                         </div>


                                        <div><asp:Panel ID="pnlError" runat ="server" Visible="false">
                                             <asp:Image ID="imgError" runat="server" ImageUrl="~/images/ERROR_ICON.png" />
                                             <asp:Label ID ="lblError" runat ="server" Font-Bold="false" ForeColor="Red"></asp:Label></asp:Panel>
                                        </div>


                                     </div>
                                     
                                </div> 
                               
                              


                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>

                </Panes>
            
               </asp:Accordion>

               <div id="dvValSummary">
                    <asp:ValidationSummary id="valSum" 
                             DisplayMode="BulletList"
                             EnableClientScript="true"
                             HeaderText="You must enter a value in the following fields:"
                             runat="server"/>
               </div>
               
          </div>
          
     </div>

</ContentTemplate>
     </asp:UpdatePanel>



</asp:Content>

