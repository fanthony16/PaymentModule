<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationLitee.aspx.vb" Inherits="frmApplicationLitee" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

   

          
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
                                                    <%--<asp:Button ID="btnEmployerHistory" runat="server" Text="View Employer History" ValidationGroup ="viewHistory" Visible ="true" OnClick="btnViewEmployerHistory_Click"  />--%>
                                                    <asp:RequiredFieldValidator ID="reqEmployer" runat ="server" ErrorMessage="*" ControlToValidate="txtEmployer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                        
                                          <div id="dvSector" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Sector :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSector" Width ="180px" runat="server" Enabled="false"></asp:TextBox>
                                               </div>
                                          </div>

                                     </div>

                                     <div id="dvPersonalPassport" style="float :left ; width :30%;  ">
                                          <div style="padding: 8px; margin: 0px; border-style: solid; border-width: thin; float: left; width: 80%; height: 200px; border-radius: 25px;">
                                               <div style="height : 180px"><asp:Image ID="imgPassport" runat="server" Width="90%" Height ="165px" ImageUrl="~/Images/untitled.png" /></div>
                                               <div style="float: left; width: 100%;"><span >Passort Photograph</span></div>
                                               
                                          </div>
                                          <div style ="float :left ;width :100%;" ><asp:CheckBox ID="chkConfirmedPassport" Text =" Is Passport Comfirmed?" TextAlign ="Right"  runat="server" /><asp:Button ID="btnShowPicture" runat="server" Text="Show Picture" ValidationGroup="personDetails"  /></div>
                                          <div style="text-align :center; width :90%"><asp:Image ID="imgSignature" runat="server" ImageUrl="~/Images/signature.jpg" /></div>
                                          <div style ="float :left ;width :100%;" ><asp:CheckBox ID="chkConfirmedSignature" Text =" Is Signature Comfirmed?" TextAlign ="Right"  runat="server" /><asp:Button ID="btnShowSignature" runat="server" Text="Show Picture" ValidationGroup="personDetails"  /></div>
                                     </div>

                                </div>
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>

                    <asp:AccordionPane ID="ContactAddress"  runat="server">
                        <Header><a href="#" class="accordionhref">Contact Address</a></Header>
                        <Content>
                            <asp:Panel ID="Panel1" runat="server">

                                <div id="dvEmailAddress" class ="dvBoxRows">
                                     <div class="dvBoxRowsFieldLabel">
                                          <span style ="font-size : medium;">Email Address :</span>
                                     </div>
                                     <div style ="text-align :left ;">
                                          <asp:TextBox ID="txtEmail" Width ="300px" runat="server"></asp:TextBox>
                                     </div>
                                </div>                            


                                <div id="dvTelephone" class ="dvBoxRows">
                                     <div class="dvBoxRowsFieldLabel">
                                          <span style ="font-size : medium;">Telephone No :</span>
                                     </div>
                                     <div style ="text-align :left ;">
                                          <asp:TextBox ID="txtPhone" Width ="300px" runat="server"></asp:TextBox>
                                         
                                     </div>
                                </div>                            

                                
                                <div id="dvAddress2" style="float :left ; width :33%; height :260px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px; background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Residential Address</span>
                                          <div id="dvResidentialAddress" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Address :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtResidentialAddress" Width ="500px" Height ="150px" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                    
                                               </div>
                                          </div>
                       

                           

                           

                                     </div>
                                </div> 

                                
                                 
          
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane> 
                                      
                    <asp:AccordionPane ID="FundingDetails"  runat="server">
                        <Header><a href="#" class="accordionhref">Funding Details</a></Header>
                        <Content>
                            
                                      <asp:Panel ID="Panel3" runat="server">
                                
                                <div id="Div8" style="float :left ; width :25%; height :380px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px; background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Funding Information</span>
                                          <div id="dvRSABalance" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Current RSA Balance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRSABalance" Width ="180px" runat="server" Enabled="false" Text ="0.00" ></asp:TextBox>
                                               </div>
                                          </div>
                                          <div id="dvMandatory" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Mandatory :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMandatory" Width ="180px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqUtility" runat ="server" ErrorMessage="*" controlToValidate="txtMandatory" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvAVC" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">AVC :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAVC" Width ="180px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqConsolidatedAllowance" runat ="server" ErrorMessage="*" controlToValidate="txtAVC" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvLegacy" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Legacy :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtLegacy" Width ="180px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqLegacy" runat ="server" ErrorMessage="*" controlToValidate="txtLegacy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          
                                          <div id="dvFundedStatus" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Funding Status :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddFundingStatus" runat="server" Width ="180px"></asp:DropDownList>
                                               </div>
                                          </div>

                                          <div id="dvGetFundingInfo" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <%--<span style ="font-size : medium;">View Funding Details :</span>--%>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:Button ID="btnViewFundingDetails" runat="server" Text="Show Funding Details" />
                                               </div>
                                          </div>

                                     </div>
                                </div> 
                                <div id="Div4" style="float :left ; width :28%; height :380px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div id="Div12" style="float :left ; width :100%; height :370px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">

                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Salary / Retirement Information</span>

                                          <div id="dvRFBalance" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Current RF Balance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRFBalance" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                               </div>
                                          </div>
                                          <div id="dvAccruedRight" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Accrued Right :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAccruedRight" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                               </div>
                                          </div>
                                          <div id="dvOutstandingCont" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Outstanding Contribution :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtOutStandingContribution" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                               </div>
                                          </div>
                                          <div id="dvBasicSalary" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Basic Salary :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtBasicSalary" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqBasicSalary" runat ="server" ErrorMessage="*" controlToValidate="txtBasicSalary" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvMonthlyTotal" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Monthly Total :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMonthlyTotal" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqMonthlyTotal" runat ="server" ErrorMessage="*" controlToValidate="txtMonthlyTotal" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          

                                     </div>


                                </div>
                      <%--               <div id="Div5" style="float :left ; width :48%; height :370px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Salary / Retirement Information</span>
                                          
                                          <div id="divTransportAllow" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Transaport Allowance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtTransportAllow" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqTransportAllow" runat ="server" ErrorMessage="*" controlToValidate="txtTransportAllow" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvConsolidatedSalary" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Consolidated Salary :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtConsildatedSalary" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqConsildatedSalary" runat ="server" ErrorMessage="*" controlToValidate="txtConsildatedSalary" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvPensionContribution" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Pension Contribution :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtPensionContribution" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqPensionContribution" runat ="server" ErrorMessage="*" controlToValidate="txtPensionContribution" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          
                                          
                                          <div id="dvHousingAllowance" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Housing Allowance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtHousingAllowance" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqHousingAllowance" runat ="server" ErrorMessage="*" controlToValidate="txtHousingAllowance" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>
                                          <div id="dvAnnualEmolument" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Annual Emolument Adjusted:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAnnualEmolument" Width ="200px" runat="server" Enabled="false" Text ="0.00"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqAnnualEmolument" runat ="server" ErrorMessage="*" controlToValidate="txtAnnualEmolument" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                     </div>
                                </div>--%>

                                </div>
                                

                            </asp:Panel>

                        </Content>
                    </asp:AccordionPane>

                    <asp:AccordionPane ID="AppInfo"  runat="server">
                        <Header><a href="#" class="accordionhref">Other Payment Application Information</a></Header>
                        <Content>

                            <asp:Panel ID="Panel2" runat="server">
                               
                                <asp:UpdatePanel ID="updAppInfo" runat ="server" >
                                     <ContentTemplate>

                                     

                                <div id="dvApplicationLocation"  style="float :left ; width :28%;height :380px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Application Location</span>


                                          <asp:UpdatePanel ID ="updLocation" runat ="server"  >
                                              <ContentTemplate >
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

                                              </ContentTemplate>
                                        </asp:UpdatePanel> 

                                         

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

                                          <div id="dvDownloadCalculator" class ="dvBoxRows" style="margin-top : 15px; text-align :right  " runat ="server" Visible ="false" >
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;"></span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:Button ID="btnOtherDetails" runat="server" Text="Down Pencom Calculator" ValidationGroup="SubmittingApplicatiomCalculator"   />
                                               </div>
                                          </div>


                                          <div id="divAnnRunningPW" class ="dvBoxRows" style="margin-top : 15px;" runat ="server" visible ="false"  >

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Select Running PW :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:DropDownList ID="ddAnnRunningPW" runat="server" Width="250px" AutoPostBack="True"></asp:DropDownList>
                                               </div>

                                          </div>


                                          <div>
                                               <asp:Panel ID="pnlAppTypeVerificationError" runat ="server" Visible="false">
                                                    <asp:Image ID="imgAppTypeError" runat="server" ImageUrl="~/images/ERROR_ICON.png" />
                                                    <asp:Label ID ="lblAppTypeError" runat ="server" Font-Bold="false" ForeColor="Red" Visible ="true" Text ="Error" ></asp:Label>
                                               </asp:Panel>
                                        </div>

                                        <div id="dvOverrideAge" class="dvBoxRowsFieldLabel" runat="server" visible ="false" style="width :400px; text-align :center  ;"  >
                                                    <asp:CheckBox ID="chkOverrideAgeBarrier" Text ="Override Age Restriction With Comment" runat="server" AutoPostBack="True" />
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
                                                              <%--<asp:TemplateField HeaderText="">
                                                                 <ItemTemplate>

                                                                      <asp:CheckBox ID="chkSelect" runat="server" Enabled="true" Width ="10" />
                                                                 </ItemTemplate>
                                                              </asp:TemplateField>--%>
                                                              <asp:BoundField DataField="DocumentID" HeaderText="Document ID"  />
                                                              <asp:BoundField DataField="DocumentName" HeaderText="Desciption" />
                                                              
                                                              <%--<asp:BoundField DataField="DocumentPath" HeaderText="" HeaderStyle-Width="0" Visible ="true"  />--%>
                                                             <%-- <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewDocument" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Document" OnClientClick="BtnViewDetails_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                                                               </asp:TemplateField>
                                                              <asp:BoundField DataField="IsVerified" HeaderText="" HeaderStyle-Width="0" Visible ="true"  />--%>
                                                              

                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>
                                               <div id="dvButtonRemoveDoc" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel" style="width :48%">
                                                    <span id="dvDocumentError" style="color:red ;" runat="server" visible ="false" > Multiple Selection Not allowed!</span>
                                               </div>

                                               <div style ="padding-right :8px; float :left ;"> 

                                                    <%--<asp:Button ID="btnRemoveDocument" runat="server" Text="Delete From Recived Documents" ValidationGroup="RemoveDocument" />--%>
                                               </div>

                                               <div style ="padding-right :8px; float :right  ;"> 

                                                    <asp:Button ID="btnRemoveAllDocument" runat="server" Text="Delete All Recieved Documents" ValidationGroup="RemoveDocument" Visible ="false"  />
                                               </div>

                                              </div>

                                          </div>
                                          

                                     </div>
                                </div> 


                                     </ContentTemplate>
                                </asp:UpdatePanel>

                                <div id="dvRequiredDocuments" style="float :left ; width :29%;height :380px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px; background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Required Documents</span>
                                          <div id="dvSelectDoc" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Select Doc. :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Multiple="Multiple" />
                                                    <%--<asp:DropDownList ID="ddRequiredDocuments" runat="server" Width="250px" AutoPostBack="True" ValidationGroup ="valSchedule"></asp:DropDownList>--%>
                                                    
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
                                         
                                          <asp:UpdatePanel ID="updPanalBank" runat ="server"  >
                                               <ContentTemplate>

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
                                       
                                               </ContentTemplate>
                                          </asp:UpdatePanel>

                                          <div id="dvAccountConfirmed" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;"></span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:CheckBox ID="chkBankConfirmed" runat="server" Text ="Bank Details Confirmed ? "  />
                                               </div>
                                          </div>

                                          <%--<div style ="text-align :left ;">
                                                    <asp:AjaxFileUpload ID="FlUploadBankConfirmation" runat="server" OnUploadComplete="AjaxFileBankUploadEvent"  ValidationGroup ="valSchedule" />
                                          </div>--%>
                                     
                                         <%-- <div id="dvSave" class ="dvBoxRows" >

                                             <asp:Button ID="btnSubmit" runat="server" Text="Submit Application" ValidationGroup ="SubmittingApplicatiom" />

                                         </div>--%>

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
               <%--<div id ="dvTTIN" runat ="server" visible ="false" >

               </div>--%>
          </div>
          
     </div>

         

     <asp:Button id="btnShowPopup" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASHardShip" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASEnbloc" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASLegacy" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASPW" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASAnnuity" runat="server" style="display:none" />
     <asp:Button id="btnViewEmployerHistory" runat="server" style="display:none" />
     <asp:Button id="btnViewPreviousApps" runat="server" style="display:none" />
     <asp:Button id="btnPopuAVCTIN" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASDB" runat="server" style="display:none" />
     <asp:Button id="btnPopupRMASNSITF" runat="server" style="display:none" />
               

     <asp:ModalPopupExtender ID="mpARLDetail" runat="server" PopupControlID="pnlARL" TargetControlID="btnShowPopup" CancelControlID="btnMPMailList" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlARL" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="480px">

          <div class ="bodyMainDiv" style="height:460px; width: 100%" >
          <div id="Div6" style ="padding-left :20px;"><h2><span>Accrued Right Letter Details</span></h2></div>
          
          <div id="Div7" class ="SubbodyMainDiv">
           
               <div id="divARLCustomerName" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Name Of Customer :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <span id="spNameOfCustomer" runat="server" style ="font-size : medium;">Reason  For  Exit</span>
                    </div>
               </div>
               <div id="divARLPIN" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">PENCOM Pin :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <span id="spPENCOMPin" runat="server" style ="font-size : medium;">Reason  For  Exit</span>
                    </div>
               </div>

               <div id="divARLEmployerName" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Employer Name :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <span id="spEmployerName" runat="server" style ="font-size : medium;">Employer Name  </span>
                    </div>
               </div>

               <div id="divARLEmployerAddress" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Employer Address :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <span id="Span1" runat="server" style ="font-size : medium;">Employer Address  </span>
                    </div>
               </div>
               <div id="dvRecievingCSD" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">CSD Recieving Officer :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddCSDRecievingOfficer" runat="server" Width =" 300px"></asp:DropDownList>
                    </div>
               </div>
               <div id="dvIssuingCSD" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">CSD Issuing Officer :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddCSDIssuingOfficer" runat="server" Width =" 300px"></asp:DropDownList>
                    </div>
               </div>
               <div id="dvCRMTeam" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">CRM Team :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddCRMTeam" runat="server" Width =" 300px"></asp:DropDownList>
                    </div>
               </div>
               <div style ="width :100%; float :left ;">
                    <div id="dvchkRecieved" style="float :left ; width :14%;  border-style: none ; border-width: thin;  margin  : 10px 0px 0px 10px; text-align :right ;">
                    <asp:CheckBox ID="chkARLRecieved" runat="server" Text="Is ARL Recieved? :" TextAlign ="Left" />
               </div>
                    <div id="dvRecievedLabel" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <span >Date Recieved :</span>
               </div>
                    <div id="dvRecivedDate" style="float :left ;  border-style: none ; border-width: thin; margin : 10px 0px 0px 10px;">
                    <asp:TextBox ID="TextBox1" runat="server" Width ="195px"></asp:TextBox>
               </div>
               </div>
               <div style ="width :100%; float :left ;">
                    <div id="dvARLDispatched" style="float :left ; width :14%;  border-style: none ; border-width: thin;  margin  : 10px 0px 0px 10px; text-align :right ;">
                    <asp:CheckBox ID="chkDispatched" runat="server" Text="Is ARL Dispatched? :" TextAlign ="Left" />
               </div>
                    <div id="dvDateDispatchedLabel" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <span >Date Dispatched :</span>
               </div>
                    <div id="dvDateDispatched" style="float :left ;  border-style: none ; border-width: thin; margin : 10px 0px 0px 10px;">
                    <asp:TextBox ID="txtDispatchedDate" runat="server" Width ="183px"></asp:TextBox>
               </div>
               </div>
               <div style ="width :100%; float :left ;">
                    <div id="Div9" style="float :left ; width :14%;  border-style: none ; border-width: thin;  margin  : 10px 0px 0px 10px; text-align :right ;">
                    <asp:CheckBox ID="chkAcknowledged" runat="server" Text="Is Request Acknowledged? :" TextAlign ="Left" />
               </div>
                    <div id="Div10" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                    <span >Date Acknowledged :</span>
               </div>
                    <div id="Div11" style="float :left ;  border-style: none ; border-width: thin; margin : 10px 0px 0px 10px;">
                    <asp:TextBox ID="txtDateAcknowledge" runat="server" Width ="160px"></asp:TextBox>
               </div>
               </div>

          </div>

     </div>
          <br />

    <asp:Button ID="btnMPMailList" runat="server" Text="Close" />
     </asp:Panel>


                    <asp:ModalPopupExtender ID="mpAVC" runat="server" PopupControlID="pnlAVCTIN" TargetControlID="btnPopuAVCTIN" CancelControlID="btnCancelMPAVC" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlAVCTIN" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div28" style ="padding-left :20px;"><h2><span>Tax Identification Number</span></h2></div>
          
          <div id="Div29" class ="SubbodyMainDiv" style="height:200px;">
                      
               <div id="dvTIN" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Enter TIN :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtTIN" runat="server" Width="300px" ValidationGroup="AVCDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqTIN" runat ="server" ErrorMessage="*" controlToValidate="txtTIN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AVCDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div style ="width :100%; float :left ;">
                    
                    <div id="Div37" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnAVCOk" runat="server" Text="Ok" ValidationGroup="AVCDetails" CausesValidation ="true"   />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
    <asp:Button ID="btnCancelMPAVC" runat="server" Text="Cancel" />
    </asp:Panel>


     <asp:ModalPopupExtender ID="MPRMASHardShip" runat="server" PopupControlID="pnlRMASHardShip" TargetControlID="btnPopupRMASHardShip" CancelControlID="btnMPRMASHardShip" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlRMASHardShip" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div14" style ="padding-left :20px;"><h2><span>HardShip RMAS Details</span></h2></div>
          
          <div id="Div15" class ="SubbodyMainDiv" style="height:200px;">
                      
               <div id="dvReason" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Reason  For  Exit :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddExitReasons" runat="server" Width =" 300px" ValidationGroup="HardShipDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqReason" runat ="server" ErrorMessage="*" controlToValidate="ddExitReasons" Display="Dynamic" SetFocusOnError="True" ValidationGroup="HardShipDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               <div id="dvDepartment" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Participant Department :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPartDepartment" runat="server" Width="300px" ValidationGroup="HardShipDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDepartment" runat ="server" ErrorMessage="*" controlToValidate="txtPartDepartment" Display="Dynamic" SetFocusOnError="True" ValidationGroup="HardShipDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               <div id="dvDesignation" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Participant Designation :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDesignation" runat="server" Width="300px" ValidationGroup="HardShipDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDesignation" runat ="server" ErrorMessage="*" controlToValidate="txtDesignation" Display="Dynamic" SetFocusOnError="True" ValidationGroup="HardShipDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>
               <div id="dvDisengagementDate" class ="dvBoxRows" style="margin-top : 10px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Disengagement Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDisengagementDate" runat="server" Width="300px" ValidationGroup="HardShipDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDisengagementDate" runat ="server" ErrorMessage="*" controlToValidate="txtDisengagementDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="HardShipDetails" ></asp:RequiredFieldValidator>
                    </div>
                    <asp:PopupControlExtender ID="calDisengagementDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDisengagementDate" Position="Bottom" TargetControlID="txtDisengagementDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDisengagementDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDisengagementDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calDisengagementDate" runat="server" BackColor="White" 
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

               <div id="dvError"><span id="spDateError" style="color :red" runat ="server" visible ="false"  >Four(4) Months Window From Date of Disengagement is Required</span></div>

               <div style ="width :100%; float :left ;">
                    
                    <div id="Div17" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnHardShipOK" runat="server" Text="Ok" ValidationGroup="HardShipDetails" CausesValidation ="true"   />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
    <asp:Button ID="btnMPRMASHardShip" runat="server" Text="Cancel" />
    </asp:Panel>


    <asp:ModalPopupExtender ID="MPRMASEnbloc" runat="server" PopupControlID="pnlEnbloc" TargetControlID="btnPopupRMASEnbloc" CancelControlID="btnMPRMASEnbloc" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlEnbloc" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div1" style ="padding-left :20px;"><h2><span>Enbloc RMAS Details</span></h2></div>
          
          <div id="Div2" class ="SubbodyMainDiv" style="height:200px;">
                      
               <div id="Div3" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Reason  For  Payment :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddPaymentReasons" runat="server" Width =" 300px" ValidationGroup="HardShipDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqPaymentReasons" runat ="server" ErrorMessage="*" controlToValidate="ddPaymentReasons" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EnblocDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvRetirementDate" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel" style="width:200px;">
                                                    <span style ="font-size : medium;">Retirement Date :</span>
                                               </div>
                                               <div style ="text-align :left ; padding-left : 250px;">
                                                    <asp:TextBox ID="txtRetirementDate" runat="server" Width="300px"></asp:TextBox>
                                               </div>
                                               <asp:RequiredFieldValidator ID="reqRetirementDate" runat ="server" ErrorMessage="*" controlToValidate="txtRetirementDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="EnblocDetails" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calRetirementDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlRetirementDate" Position="Bottom" TargetControlID="txtRetirementDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlRetirementDate" runat="server">
                                                            <asp:UpdatePanel ID="updRetirementDate" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calRetirementDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calRetirementDate" runat="server" BackColor="White" 
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
                    
                    <div id="Div21" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnEnblocOK" runat="server" Text="Ok" ValidationGroup="EnblocDetails" CausesValidation ="true"   />
               </div>
                    
               </div>
               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnMPRMASEnbloc" runat="server" Text="Cancel" />
    </asp:Panel>


               <asp:ModalPopupExtender ID="MPRMASLegacy" runat="server" PopupControlID="pnlLegacy" TargetControlID="btnPopupRMASLegacy" CancelControlID="btnMPRMASLegacy" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlLegacy" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:270px; width: 100%" >
          <div id="Div13" style ="padding-left :20px;"><h2><span>Legacy RMAS Details</span></h2></div>
          
          <div id="Div16" class ="SubbodyMainDiv" style="height:200px;">
                      
              

               <div id="Div22" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel" style="width:200px;">
                                                    <span style ="font-size : medium;">Retirement Date :</span>
                                               </div>
                                               <div style ="text-align :left ; padding-left : 250px;">
                                                    <asp:TextBox ID="txtLegacyRetirementDate" runat="server" Width="300px"></asp:TextBox>
                                               </div>
                                               <asp:RequiredFieldValidator ID="reqLegacyDOR" runat ="server" ErrorMessage="*" controlToValidate="txtLegacyRetirementDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="LegacyDetails" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calLegacyRetirementDate_PopupControlExtenderLegacy" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlLegacyRetirementDate" Position="Bottom" TargetControlID="txtLegacyRetirementDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlLegacyRetirementDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calLegacyRetirementDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calLegacyRetirementDate" runat="server" BackColor="White" 
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
                    
                    <div id="Div23" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnLegacyOK" runat="server" Text="Ok" ValidationGroup="LegacyDetails" CausesValidation ="true"   />
               </div>
                    
               </div>               
               

          </div>

     </div>
          <br />
    <asp:Button ID="btnMPRMASLegacy" runat="server" Text="Cancel" />
    </asp:Panel>


     <asp:ModalPopupExtender ID="mpEmployerList" runat="server" PopupControlID="pnlEmployerList" TargetControlID="btnViewEmployerHistory" CancelControlID="btnMPMailList" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
    <asp:Panel ID="pnlEmployerList" runat="server" CssClass="modalPopup" align="center" style = "display:none; padding-left : 20px;"  Width ="920px" >
    <div id="dvleftBox" class ="bodyMainDiv" style ="width:900px;"> 
        
        <div id="Div18" style="border-color:#3a4f63; border :2px solid ; padding-left :5px; padding-right  :5px;  width :95%; ">

            <div id="Div19" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Customer - Employer History</strong></span></div>
            <div id="dvCrBody" class="SubbodyMainDiv" >
               
               <div class="dvBoxRows" >

              
                       <asp:GridView Width="800px" ID="gridCustomerHistory" runat="server" Visible="true" PageSize="10" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridCustomerHistory_RowDataBound">                       

                           <Columns>                                           
                                
                                <asp:ButtonField CommandName="Select" Text="Select" ItemStyle-Width ="50px" />
                                <asp:BoundField DataField="EmployerID" HeaderText="Employer ID" Visible ="true" ItemStyle-Width="100"/>
                                <asp:BoundField DataField="employerName" HeaderText="Employer Name" ItemStyle-Wrap ="false" ItemStyle-Width="150"/>
                                <asp:BoundField DataField="EmployerCode" HeaderText="Employer Name" ItemStyle-Wrap ="false" ItemStyle-Width="150"/>
                                <asp:BoundField DataField="LastFundDate" HeaderText="Last Payment Date" ItemStyle-Wrap ="false" ItemStyle-Width="150" DataFormatString="{0:d}"/>
                                          
                         </Columns>
                         <pagersettings mode="NextPreviousFirstLast" firstpagetext="First" lastpagetext="Last" nextpagetext="Next" previouspagetext="Prev"   position="Bottom"/> 

                   </asp:GridView>                   
              
            </div>
    
    </div>
    
    </div>
        </div>
        <br />
             <asp:Button ID="btnCloseEmployerHistory" runat="server" Text="Close" ValidationGroup ="closeHistory"/>
        </asp:Panel>

               <asp:ModalPopupExtender ID="mpDeathBenefit" runat="server" PopupControlID="pnlRMASDeathBenefit" TargetControlID="btnPopupRMASDB" CancelControlID="btnMPRMASDB" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlRMASDeathBenefit" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="600px">

          <div class ="bodyMainDiv" style="height:540px; width: 100%" >
          <div id="Div31" style ="padding-left :20px;"><h2><span>Death Benefit Details</span></h2></div>
          
          <div id="Div32" class ="SubbodyMainDiv" style="height:540px;">       

               <div id="dvDBARetirementDate" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Retirement Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDBARetirementDate" runat="server" Width="300px" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDBARetirementDate" runat ="server" ErrorMessage="*" controlToValidate="txtDBARetirementDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="DBADetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compDBARetirementDate" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBARetirementDate" Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_CalDBARetirement" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDBARetirement" Position="Bottom" TargetControlID="txtDBARetirementDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDBARetirement" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalDBARetirement" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="CalDBARetirement" runat="server" BackColor="White" 
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

               <div id="dvDeathDate" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Death Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDBADeathDate" runat="server" Width="300px" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDBADeathDate" runat ="server" ErrorMessage="*" controlToValidate="txtDBADeathDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="DBADetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compDBADeathDate" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBADeathDate" Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_CalDeathDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDBADeathDate" Position="Bottom" TargetControlID="txtDBADeathDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDBADeathDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalDeathDate" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="CalDeathDate" runat="server" BackColor="White" 
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

               <div id="dvAdminLetterIssuer" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Admin Letter Issuer :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAdminLetterIssuer" runat="server" Width="300px" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAdminLetterIssuer" runat ="server" ErrorMessage="*" controlToValidate="txtAdminLetterIssuer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>
               </div>

               <div id="dvAdminLetterDate" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Admin. Letter Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAdminLetterDate" runat="server" Width="300px" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAdminLetterDate" runat ="server" ErrorMessage="*" controlToValidate="txtAdminLetterDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="DBADetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compAdminLetterDate" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAdminLetterDate" Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                         
                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_CalAdminLetterDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlAdminLetterDate" Position="Bottom" TargetControlID="txtAdminLetterDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlAdminLetterDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalAdminLetterDate" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="CalAdminLetterDate" runat="server" BackColor="White" 
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

               <div id="dvDBValueDate" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Value Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDBValueDate" runat="server" Width="300px" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDBValueDate" runat ="server" ErrorMessage="*" controlToValidate="txtDBValueDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="DBADetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compDBValueDate" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBValueDate" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="DBADetails"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_CalDBValueDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDBValueDate" Position="Bottom" TargetControlID="txtDBValueDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDBValueDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalDBValueDate" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="CalDBValueDate" runat="server" BackColor="White" 
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

               <div id="dvDBAAdminNOK" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Named Admin. NOK :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDBAAdminNOK" runat="server" Width="300px" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDBAAdminNOK" runat ="server" ErrorMessage="*" controlToValidate="txtDBAAdminNOK" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>
               </div>

               <div id="dvDBAInsuranceProceed" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Life Insurance Proceed :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtInsuranceProceed" runat="server" Width="300px" Text ="0.00" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqInsuranceProceed" runat ="server" ErrorMessage="*" controlToValidate="txtInsuranceProceed" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="comInsuranceProceed" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtInsuranceProceed" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>
               </div>

               <div id="dvDBAAccruedRight" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Accrued Right :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtDBAAccruedRight" runat="server" Width="300px" Text ="0.00" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDBAAccruedRight" runat ="server" ErrorMessage="*" controlToValidate="txtDBAAccruedRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compDBAAccruedRight" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBAAccruedRight" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>
               </div>

               <div id="dvDBAContribution" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Contribution :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtDBAContribution" runat="server" Width="300px" Text ="0.00" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqtxtDBAContribution" runat ="server" ErrorMessage="*" controlToValidate="txtDBAContribution" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compDBAContribution" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBAContribution" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="DBADetails"></asp:CompareValidator>

                    </div>
               </div>

               <div id="dvDBAInvestmentIncome" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Investment Income :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtDBAInvestmentIncome" runat="server" Width="300px" Text ="0.00" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqInvestmentIncome" runat ="server" ErrorMessage="*" controlToValidate="txtDBAInvestmentIncome" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compInvestmentIncome" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBAInvestmentIncome" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="DBADetails"></asp:CompareValidator>

                    </div>
               </div>

               <div id="dvDBARSABalance" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">RSA Balance :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtDBARSABalance" runat="server" Width="300px" Text ="0.00" ValidationGroup="DBADetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDBARSABalance" runat ="server" ErrorMessage="*" controlToValidate="txtDBARSABalance" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBADetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compDBARSABalance" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDBARSABalance" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="DBADetails"></asp:CompareValidator>

                    </div>
               </div>
               
               <div id="Div51" style ="width :100%; float :left ;">
                    
                    <div id="Div52" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnDBOk" runat="server" Text="Ok" ValidationGroup="DBADetails" CausesValidation ="true"   />
                         <asp:Button ID="btnDBReValue" runat="server" Text="Re-Value" ValidationGroup="DBADetails" CausesValidation ="true"   />
                         <asp:Button ID="btnMPRMASDB" runat="server" Text="Cancel" />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
          <div></div>
    
    </asp:Panel>
               

               <asp:ModalPopupExtender ID="MPRMASPW" runat="server" PopupControlID="pnlRMASPW" TargetControlID="btnPopupRMASPW" CancelControlID="btnMPRMASPW" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlRMASPW" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="740px">

          <div class ="bodyMainDiv" style="height:690px; width: 100%" >
          <div id="Div20" style ="padding-left :20px;"><h2><span>Program Withdrawal RMAS Details</span></h2></div>
          
          <div id="Div24" class ="SubbodyMainDiv" style="height:690px;">

               <div id="dvBasicSalaryPW" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Basic Salary :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtBasicSalaryPW" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails" ControlToValidate="txtBasicSalaryPW"></asp:TextBox>

                         <asp:CompareValidator ID="compBasicSalaryPW" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtBasicSalaryPW" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                         <asp:RequiredFieldValidator ID="reqtxtBasicSalary" runat ="server" ErrorMessage="*" controlToValidate="txtBasicSalaryPW" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>

               </div>
               <div id="dvHouseRent" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">House Rent :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtHouseRent" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqHouseRent" runat ="server" ErrorMessage="*" controlToValidate="txtHouseRent" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compHouseRent" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtHouseRent" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>


                    </div>

               </div>
               <div id="dvTransport" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Trasport :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtTransport" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqTransport" runat ="server" ErrorMessage="*" controlToValidate="txtTransport" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compTransport" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtTransport" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </div>

               </div>
               <div id="dvUtility" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Utility :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtUtility" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqUtilityPW" runat ="server" ErrorMessage="*" controlToValidate="txtUtility" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compUtility" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtUtility" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>
               <div id="dvConsolidatedAllowance" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Consolidated Allowance :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtConsolidatedAllowance" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqConsolidatedAllowancePW" runat ="server" ErrorMessage="*" controlToValidate="txtConsolidatedAllowance" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compConsolidatedAallowance" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtConsolidatedAllowance" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>
               <div id="dvConsolidatedSalaryPW" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Consolidated Salary :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtConsolidatedSalary" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqConsolidatedSalary" runat ="server" ErrorMessage="*" controlToValidate="txtConsolidatedSalary" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compConsolidatedSalary" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtConsolidatedSalary" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>


                    </div>

               </div>
               <div id="dvMonthlyTotalPW" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Monthly Total :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtMonthTotal" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqMonthTotal" runat ="server" ErrorMessage="*" controlToValidate="txtMonthTotal" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compMonthTotal" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtMonthTotal" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>
               <div id="dvAnnualTotalEmolument" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Annual Total Emolument :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAnnualTotalEmolument" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAnnualTotalEmolument" runat ="server" ErrorMessage="*" controlToValidate="txtAnnualTotalEmolument" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compAnnualTotalEmolument" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAnnualTotalEmolument" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>

               <div id="dvRetirementDatePW" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Retirement Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDORPW" runat="server" Width="300px" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDORPW" runat ="server" ErrorMessage="*" controlToValidate="txtDORPW" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="PWDetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compDORPW" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDORPW" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="PWDetails"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="calDORPW_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDORPW" Position="Bottom" TargetControlID="txtDORPW"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDORPW" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDORPW" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="calDORPW" runat="server" BackColor="White" 
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

               <div id="dvRetirementGround" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Retirement Ground :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddRetirementGroundPW" runat="server" Width =" 300px" ValidationGroup="PWDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqRetirementGround" runat ="server" ErrorMessage="*" controlToValidate="ddRetirementGroundPW" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvValueDate" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Value Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtValueDate" runat="server" Width="300px" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqValueDate" runat ="server" ErrorMessage="*" controlToValidate="txtValueDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="PWDetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compValueDate" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtValueDate" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="CalValueDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlValueDate" Position="Bottom" TargetControlID="txtValueDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlValueDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalValueDate" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="CalValueDate" runat="server" BackColor="White" 
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
               <div id="dvAcruedRight" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Accrued Right :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAccruedRightPW" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAccruedRight" runat ="server" ErrorMessage="*" controlToValidate="txtAccruedRightPW" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compAccruedRightPW" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAccruedRightPW" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>
               </div>
               <div id="dvRSABalancPW" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">RSA Balance :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtRSABalancePW" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqRSABalance" runat ="server" ErrorMessage="*" controlToValidate="txtRSABalancePW" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compRSABalancePW" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtRSABalancePW" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>
               </div>
               <div id="dvRecommendedLumpSum" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Recommended LumpSum :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtRecommendeLumpSum" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqRecommendeLumpSum" runat ="server" ErrorMessage="*" controlToValidate="txtRecommendeLumpSum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compRecommendeLumpSum" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtRecommendeLumpSum" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>
               </div>
               <div id="dvMonthlyDrawDown" class ="dvBoxRows" style="margin-top : 2px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Monthly Drawn Down :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtMonthlyDrawDown" runat="server" Width="300px" Text ="0.00" ValidationGroup="PWDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqMonthlyDrawDown" runat ="server" ErrorMessage="*" controlToValidate="txtMonthlyDrawDown" Display="Dynamic" SetFocusOnError="True" ValidationGroup="PWDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compMonthlyDrawDown" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtMonthlyDrawDown" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>
               </div>



               <div id="dvActions" style ="width :100%; float :left ;">
                    
                    <div id="Div30" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnOKPW" runat="server" Text="Ok" ValidationGroup="PWDetails" CausesValidation ="true"   />
                         <asp:Button ID="btnReValuePW" runat="server" Text="Re-Value" ValidationGroup="PWDetails" CausesValidation ="true"   />
                         <asp:Button ID="btnMPRMASPW" runat="server" Text="Cancel" />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
          <div></div>
    
    </asp:Panel>


               <asp:ModalPopupExtender ID="MPRMASAnnuity" runat="server" PopupControlID="pnlRMASAnnuity" TargetControlID="btnPopupRMASAnnuity" CancelControlID="btnMPRMASAnnuity" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlRMASAnnuity" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="780px">
          
          <div class ="bodyMainDiv" style="height:750px; width: 100%" >
          <%--<div id="Div25" style ="padding-left :20px;"><h2><span>Annuity RMAS Details</span></h2></div>--%>
          
          <div id="Div26" class ="SubbodyMainDiv" style="height:680px;">

               <div id="dvBasicSalaryAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Basic Salary :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtBasicSalaryAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails" ControlToValidate="txtBasicSalaryPW"></asp:TextBox>

                         <asp:CompareValidator ID="compBasicSalaryAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtBasicSalaryAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                         <asp:RequiredFieldValidator ID="reqBasicSalaryAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtBasicSalaryAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>

               </div>

               <div id="dvHouseRentAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">House Rent :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtHouseRentAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqHouseRentAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtHouseRentAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compHouseRentAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtHouseRentAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>


                    </div>

               </div>

               <div id="dvTransportAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Trasport :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtTransportAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqTransportAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtTransportAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compTransportAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtTransportAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>
                    </div>

               </div>

               <div id="dvUtilityAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Utility :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtUtilityAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqUtilityAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtUtilityAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compUtilityAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtUtilityAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>

               </div>

               <div id="dvConsolidatedAllowanceAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Consolidated Allowance :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtConsolidatedAllowanceAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqConsolidatedAllowanceAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtConsolidatedAllowanceAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compConsolidatedAllowanceAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtConsolidatedAllowanceAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>

               </div>

               <div id="dvConsolidatedSalaryAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Consolidated Salary :</span>
                    </div>

                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtConsolidatedSalaryAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqConsolidatedSalaryAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtConsolidatedSalaryAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compConsolidatedSalaryAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtConsolidatedSalaryAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>
                    </div>

               </div>

               <div id="dvMonthTotalAnnuity" class ="dvBoxRows" style="margin-top :1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Monthly Total :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">

                         <asp:TextBox ID="txtMonthTotalAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqMonthTotalAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtMonthTotalAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compMonthTotalAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtMonthTotalAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>

               </div>

               <div id="dvAnnualTotalEmolumentAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Annual Total Emolument :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAnnualTotalEmolumentAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAnnualTotalEmolumentAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtAnnualTotalEmolumentAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compAnnualTotalEmolumentAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAnnualTotalEmolumentAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>

               </div>

               <div id="dvRetirementGroundAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Retirement Ground :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:DropDownList ID="ddRetirementGroundAnnuity" runat="server" Width =" 300px" ValidationGroup="AnnuityDetails"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqRetirementGroundAnnuity" runat ="server" ErrorMessage="*" controlToValidate="ddRetirementGroundAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

               <div id="dvRetirementDateAnnuity" class ="dvBoxRows" style="margin-top : 1px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Retirement Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtDORAnnuity" runat="server" Width="300px" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDORAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtDORAnnuity" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="AnnuityDetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compDORAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtDORAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="AnnuityDetails"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="calDORAnnuity_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDORAnnuity" Position="Bottom" TargetControlID="txtDORAnnuity"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDORAnnuity" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDORAnnuity" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="calDORAnnuity" runat="server" BackColor="White" 
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

               <div id="dvValueDateAnnuity" class ="dvBoxRows" style="margin-top : 1px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Value Date :</span>
                    </div>
                    
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtValueDateAnnuity" runat="server" Width="300px" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqValueDateAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtValueDateAnnuity" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="AnnuityDetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compValueDateAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtValueDateAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup ="AnnuityDetails"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="CalValueDateAnnuity_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlValueDateAnnuity" Position="Bottom" TargetControlID="txtValueDateAnnuity"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlValueDateAnnuity" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalValueDateAnnuity" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="CalValueDateAnnuity" runat="server" BackColor="White" 
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

               <div id="dvCommencmentDate" class ="dvBoxRows" style="margin-top : 1px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Annuity Commencment Date :</span>
                    </div>
                    
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtCommencmentDate" runat="server" Width="300px" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqCommencmentDate" runat ="server" ErrorMessage="*" controlToValidate="txtCommencmentDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="AnnuityDetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compCommencmentDate" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtCommencmentDate" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup ="AnnuityDetails"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="CalCommencmentDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlCommencmentDate" Position="Bottom" TargetControlID="txtCommencmentDate"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlCommencmentDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="CalCommencmentDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="CalCommencmentDate" runat="server" BackColor="White" 
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

               <div id="dvInsuranceCoy" class ="dvBoxRows" style="margin-top : 1px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Insurance Company Name :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <%--<asp:TextBox ID="txtInsuranceCoy" runat="server" Width="300px" ValidationGroup="AnnuityDetails"></asp:TextBox>--%>
                         <asp:DropDownList ID="ddInsuranceCoy" runat="server" Width ="300px" ValidationGroup="AnnuityDetails" AutoPostBack="True"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="reqInsuranceCoy" runat ="server" ErrorMessage="*" controlToValidate="ddInsuranceCoy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>
               </div>

               <div id="dvRSABalanceAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">RSA Balance :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtRSABalanceAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails" Enabled ="true" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqRSABalanceAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtRSABalanceAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compRSABalanceAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtRSABalanceAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>
               </div>

               <div id="dvPremiumAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Premium :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPremium" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqPremium" runat ="server" ErrorMessage="*" controlToValidate="txtPremium" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compPremium" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtPremium" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>
               </div>
               
               <div id="dvlumpsum" class ="dvBoxRows" style="margin-top : 1px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">LumpSum :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtLumpSum" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqLumpSum" runat ="server" ErrorMessage="*" controlToValidate="txtLumpSum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compLumpSum" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtLumpSum" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>
               </div>

               <div id="dvMonthlyAnnuity" class ="dvBoxRows" style="margin-top : 1px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Monthly Annuity :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtMonthlyAnnuity" runat="server" Width="300px" Text ="0.00" ValidationGroup="AnnuityDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqMonthlyAnnuity" runat ="server" ErrorMessage="*" controlToValidate="txtMonthlyAnnuity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AnnuityDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compMonthlyAnnuity" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtMonthlyAnnuity" Display="Dynamic" Operator="DataTypeCheck" Type="Double" ValidationGroup="AnnuityDetails"></asp:CompareValidator>

                    </div>
               </div>            

               <div style ="width :100%; float :left ;">
                    
                    <div id="Div41" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnOKAnnuity" runat="server" Text="Ok" ValidationGroup="AnnuityDetails" CausesValidation ="true"   />
                         <asp:Button ID="btnReValueAnnuity" runat="server" Text="Re-Value" ValidationGroup="AnnuityDetails" CausesValidation ="true"   />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
    <asp:Button ID="btnMPRMASAnnuity" runat="server" Text="Cancel" />
    </asp:Panel>


               <asp:ModalPopupExtender ID="MPPreviousApps" runat="server" PopupControlID="pnlPreviousApps" TargetControlID="btnViewPreviousApps" CancelControlID="btnCancelMPPreviousApps" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlPreviousApps" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="400px" Width ="600px">

          <div class ="bodyMainDiv" style="height:400px; width: 100%" >
          <div id="Div25" style ="padding-left :20px;"><h2><span>Customer's Previous Application(s)</span></h2></div>
          
          <div id="Div27" class ="SubbodyMainDiv" style="height:320px;">
                      
              

                      <asp:Panel ID="pnlGridPreviousApps" Width ="98%" runat ="server" BorderStyle="Solid" Height ="290px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridPreviousApps" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False">
                                                         <Columns>
                                                              

                                                              <asp:BoundField DataField="ApprovalType" HeaderText="Application Type" />
                                                              <asp:BoundField DataField="txtStatus" HeaderText="Status" />
                                                              <asp:BoundField DataField="dteApplicationDate" HeaderText="Application Date" DataFormatString="{0:d}" />

                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>


               <div style ="width :100%; float :left ;">
                    
                    <div id="Div34" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnPreviousAppsOK" runat="server" Text="Ok" CausesValidation ="true"   />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
    <asp:Button ID="btnCancelMPPreviousApps" runat="server" Text="Cancel" />
    </asp:Panel>



               <asp:ModalPopupExtender ID="MPNSITF" runat="server" PopupControlID="pnlRMASNSITF" TargetControlID="btnPopupRMASNSITF" CancelControlID="btnCancelNSITF" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlRMASNSITF" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="300px">

          <div class ="bodyMainDiv" style="height:250px; width: 100%" >
          <div id="Div33" style ="padding-left :20px;"><h2><span>NSITF Application Details</span></h2></div>
          
          <div id="Div35" class ="SubbodyMainDiv" style="height:250px;">

               
               <div id="dvRetirementNSITF" class ="dvBoxRows" style="margin-top : 2px;">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Retirement Date :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtRetirementDateNSTITF" runat="server" Width="300px" ValidationGroup="NSITFDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqRetirementDateNSTITF" runat ="server" ErrorMessage="*" controlToValidate="txtRetirementDateNSTITF" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="NSITFDetails" ></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="compRetirementDateNSTITF" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtRetirementDateNSTITF" Display="Dynamic" Operator="DataTypeCheck" Type="Date" ValidationGroup="NSITFDetails"></asp:CompareValidator>
                         

                    </div>
                    <asp:PopupControlExtender ID="PopupControlExtender_calDORNSITF" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDORNSITF" Position="Bottom" TargetControlID="txtRetirementDateNSTITF"></asp:PopupControlExtender>
                    <asp:Panel ID="pnlDORNSITF" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDORNSITF" />

                                                                 </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="calDORNSITF" runat="server" BackColor="White" 
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

               <div id="dvInitialAmountPaidNSITF" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Initial Amount Paid :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtInitialAmountPaid" runat="server" Width="300px" Text ="0.00" ValidationGroup="NSITFDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqInitialAmountPaid" runat ="server" ErrorMessage="*" controlToValidate="txtInitialAmountPaid" Display="Dynamic" SetFocusOnError="True" ValidationGroup="NSITFDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compInitialAmountPaid" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtInitialAmountPaid" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>

               <div id="dvAmountRecievedNSITF" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Amount Recieved to RSA :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAmountRecievedToRSANSITF" runat="server" Width="300px" Text ="0.00" ValidationGroup="NSITFDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAmountRecievedToRSANSITF" runat ="server" ErrorMessage="*" controlToValidate="txtAmountRecievedToRSANSITF" Display="Dynamic" SetFocusOnError="True" ValidationGroup="NSITFDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compAmountRecievedToRSANSITF" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAmountRecievedToRSANSITF" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>


               <div id="dvAmountRequestedNSITF" class ="dvBoxRows" style="margin-top : 2px; ">

                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Amount Requested to RSA :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtAmountRequestedFromRSANSITF" runat="server" Width="300px" Text ="0.00" ValidationGroup="NSITFDetails"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqAmountRequestedFromRSANSITF" runat ="server" ErrorMessage="*" controlToValidate="txtAmountRequestedFromRSANSITF" Display="Dynamic" SetFocusOnError="True" ValidationGroup="NSITFDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                         <asp:CompareValidator ID="compAmountRequestedFromRSANSITF" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAmountRequestedFromRSANSITF" Display="Dynamic" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>

                    </div>

               </div>
  

               <div id="dvActionNSITF" style ="width :100%; float :left ;">
                    
                    <div id="Div56" style="float :left ;   border-style: none ; border-width: thin; margin : 10px 0px 0px 10px; padding-left :30px;">
                         <asp:Button ID="btnNSITFOk" runat="server" Text="Ok" ValidationGroup="NSITFDetails" CausesValidation ="true"   />
                         <asp:Button ID="btnCancelNSITF" runat="server" Text="Cancel" />
               </div>
                    
               </div>

          </div>

     </div>
          <br />
          <div></div>
    
    </asp:Panel>



</asp:Content>

