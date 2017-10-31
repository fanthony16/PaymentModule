<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmDBAInvestigation.aspx.vb" Inherits="frmDBAInvestigation" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="tel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

      <script type = "text/javascript">
           function SetTarget() {
                document.forms[0].target = "_blank";
           }
        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager> 
     <div class ="bodyMainDiv" style ="width :1000px;">
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>DEATH BENEFIT INVESTIGATION REPORT FORM</span></h2></div>
           <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style ="padding-left:0px;">

                <asp:Panel ID="UserReg" runat="server">
                                <div id="dvPersonalDetails" style="width :100%">
                                     <div id="dvPersonalfor" style="float :left ; width :70%; ">


                                          <div id="dvSetCutOff" class ="dvBoxRows">
                                               <%--<div class="dvBoxRowsFieldLabel">
                                                    <asp:CheckBox ID="chkCutOff" Text ="Cut-Off Date" runat="server" />
                                               </div>--%>
                                               <%--<asp:Panel id="pnlCutOffDateMain" runat ="server" Width ="500px">--%>

                                               
                                             <%--  <div id="dvCutOffDate" style ="text-align :left ;" runat ="server" visible ="true"  >

                                                    <asp:TextBox ID="txtCutOffDate" Width ="300px" runat="server" ></asp:TextBox>
                                                    <span id ="spCutOffError" runat ="server" visible ="false" style ="color :red "   >*</span>
                                                    <asp:RequiredFieldValidator ID="reqCutOffDate" runat ="server" ErrorMessage="*" controlToValidate="txtCutOffDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="PopupControlExtender_calCutOffDate" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlCutOffDate" Position="Bottom" TargetControlID="txtCutOffDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlCutOffDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calCutOffDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calCutOffDate" runat="server" BackColor="White" 
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
                                                    
                                               </div>--%>
                                               <%--</asp:Panel>--%>
                                          </div>


                                          <div id="dvPIN" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Pencom PIN :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtPIN" Width ="300px" runat="server" ValidationGroup="FindPersonDetails"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqPIN" runat ="server" ErrorMessage="*" ControlToValidate="txtPIN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnFind" runat="server" Text="..." Visible ="false"  ValidationGroup="personInvestigation" />
                                                    <asp:Label ID="lblTitle" runat="server" Visible ="false" ></asp:Label>
                                               </div>
                                          </div>

                                          <div id="dvSurName" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Full Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtFullName" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqSurname" runat ="server" ErrorMessage="*" ControlToValidate="txtFullName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvFirstName" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" style ="width :100%; text-align :left ">
                                                    <span style ="font-size : medium;"><strong>NOK DETAILS (IF AVAILABLE) :</strong></span>
                                               </div>
                                              
                                          </div>

                                          <div id="dvOthernames" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    N<span style="font-size : medium;">ame :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtNOKNames" Width ="300px" runat="server" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqNOKNames" runat ="server" ErrorMessage="*" ControlToValidate="txtNOKNames" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvDOB" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    Address<span style="font-size : medium;"> :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtNOKAddress" Width ="300px" runat="server"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqNOKAddress" runat ="server" ErrorMessage="*" ControlToValidate="txtNOKAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvSex" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    Telephone<span style="font-size : medium;"> :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtNOKTelephone" Width ="300px" runat="server"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqNOKTelephone" runat ="server" ErrorMessage="*" ControlToValidate="txtNOKTelephone" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvAge" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    Subject/Purpose:</div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSubject" Width ="300px" runat="server"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqSubject" runat ="server" ErrorMessage="*" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvDeclaredAge" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    Investigation Location:</div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtLocation" Width ="300px" runat="server"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqLocation" runat ="server" ErrorMessage="*" ControlToValidate="txtLocation" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dv" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" style ="width :100%; text-align :left ">
                                                    <span style ="font-size : medium;"><strong>REPORT OF INVESTIGATION (Document all observations)</strong></span>
                                               </div>
                                              
                                          </div>


                                          <div id="dvEmployer" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    Report of Investigation<span style="font-size : medium;"> :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtReport" Width ="540px" runat="server" TextMode ="MultiLine" Height ="100px"   ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqReport" runat ="server" ErrorMessage="*" ControlToValidate="txtReport" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>


                                          <div id="dvResponder1" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" style ="width :100%; text-align :left ">
                                                    <span style ="font-size : medium;"><strong> RESPONDENT 1 (Family member to confirm Administrators) tick Yes/No</strong></span>
                                               </div>
                                              
                                          </div>


                                          <div id="dvNextofKIN" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRepnderName" Width ="300px" runat="server" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqRepnderName" runat ="server" ErrorMessage="*" ControlToValidate="txtRepnderName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvNextofKINTelephone" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Address:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:TextBox ID="txtRepnderAddy1" Width ="300px" runat="server" TextMode ="MultiLine" ></asp:TextBox>
                                                    
                                               </div>
                                          </div>




                                          
                                          <div id="dvResponder2" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" style ="width :100%; text-align :left ">
                                                    <span style ="font-size : medium;"><strong> RESPONDENT 2 </strong></span>
                                               </div>
                                              
                                          </div>


                                          <div id="Div2" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRepnderName2" Width ="300px" runat="server" ></asp:TextBox>
                                                    
                                               </div>
                                          </div>

                                          <div id="Div3" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Address:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:TextBox ID="txtRepnderAddy2" Width ="300px" runat="server" TextMode ="MultiLine" ></asp:TextBox>
                                                    
                                               </div>
                                          </div>



                                          
                                          <div id="dvResponder3" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" style ="width :100%; text-align :left ">
                                                    <span style ="font-size : medium;"><strong> RESPONDENT 3 </strong></span>
                                               </div>
                                              
                                          </div>

                                          <div id="Div5" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRepnderName3" Width ="300px" runat="server" ></asp:TextBox>
                                                   
                                               </div>
                                          </div>

                                          <div id="Div6" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Address:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:TextBox ID="txtRepnderAddy3" Width ="300px" runat="server" ></asp:TextBox>
                                                    
                                               </div>
                                          </div>

                                          <div id="Div1" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" style ="width :100%; text-align :left ">
                                                    <span style ="font-size : medium;"><strong> INVESTIGATOR'S INFORMATION </strong></span>
                                               </div>
                                              
                                          </div>

                                          <div id="Div4" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Name :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtInvestigatorName" Width ="300px" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqInvestigatorName" runat ="server" ErrorMessage="*" ControlToValidate="txtInvestigatorName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="Div7" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Date:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:TextBox ID="txtInvestigationDate" Width ="300px" runat="server"  ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqInvestigationDate" runat ="server" ErrorMessage="*" ControlToValidate="txtInvestigationDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="personInvestigation" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                                                    <asp:PopupControlExtender ID="calInvestigationDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlInvestigationDate" Position="Bottom" TargetControlID="txtInvestigationDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlInvestigationDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calInvestigationDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calInvestigationDate" runat="server" BackColor="White" 
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


                                          <div id="dvSave" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;"></span>
                                               </div>

                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:Button ID="btnSave" Text ="Submit" runat ="server" ValidationGroup="personInvestigation" />

                                               </div>

                                          </div>

                                

                                     </div>

                                </div>
                            </asp:Panel>

           </div>
     </div>
</asp:Content>

