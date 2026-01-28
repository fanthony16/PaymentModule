<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmPWCalculatorNew.aspx.vb" Inherits="frmPWCalculatorNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

          <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

   
          <asp:UpdatePanel ID="updFormPanel" runat="server">
        <ContentTemplate>


          <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Programmed Withdrawal Pension Calculator...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
          <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >
            
          </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >


                         <div id="Div2" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;" runat ="server" id="spControl" visible ="false" >.</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <span style="color: #FF0000; font-weight: bold;" runat ="server" id="spError" visible ="false"  >Error !!!. Retirement Date is Missing For the Customer</span>
                                                   
                                               </div>
                                          </div>

                    <div id="dvIsAnnuity" class ="dvBoxRows">
                                               
                                               <div style ="text-align :left ;">
                                                    <asp:CheckBox ID="chkIsAnnuity" runat="server" Width ="580px" Text ="Please Check if Application is Annuity" />
                                                    
                                               </div>
                                          </div>           

                            
                    <div id="Div1" >
                         <div style="float :left;   "> 
                              <span style ="font-size : medium;">Salary Structure :</span>                                               
                         </div>
                         <div >   
                              <asp:DropDownList ID="ddSS" runat="server" Width ="300px">
                                   <asp:ListItem></asp:ListItem>
                                   <asp:ListItem>CONHESS</asp:ListItem>
                                   <asp:ListItem>CONMESS</asp:ListItem>
                                   <asp:ListItem>CONPASS</asp:ListItem>
                                   <asp:ListItem>CONPCASS</asp:ListItem>
                                   <asp:ListItem>CONPOSS</asp:ListItem>
                                   <asp:ListItem>CONPSS</asp:ListItem>
                                   <asp:ListItem>CONRAISS</asp:ListItem>
                                   <asp:ListItem>CONTEDISS</asp:ListItem>
                                   <asp:ListItem>CONTISS</asp:ListItem>
                                   <asp:ListItem>CONUASS</asp:ListItem>
                                   <asp:ListItem>TOPSAL</asp:ListItem>
                              </asp:DropDownList>                                             
                         </div>

                    </div>



                         <div id="dvPIN" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">PIN :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtPIN" Width ="300px" runat="server" Enabled="true" ></asp:TextBox><asp:RequiredFieldValidator ID="reqPIN" ControlToValidate="txtPIN" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup ="CalculatePW"></asp:RequiredFieldValidator>
                                                   <asp:Button ID="btnFind" runat="server" Text="..." Visible ="true" ValidationGroup ="CalculatePW" />
                                               </div>
                              
                                          </div>                

                         <div id="dvGL" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Grade Level :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtGradeLevel" Width ="300px" runat="server" ></asp:TextBox>
                                                   
                                               </div>
                                          </div>

                         <div id="dvStep" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Step :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtStep" Width ="300px" runat="server" ></asp:TextBox>
                                                   
                                               </div>
                                          </div>

                         <div id="dvSex" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Sex :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSex" Width ="300px" runat="server" Enabled ="false"  ></asp:TextBox>
                                                   
                                               </div>
                                          </div>
           
                         <div id="dvRSA" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">RSA Balance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRSABalance" Width ="300px" runat="server" >0</asp:TextBox>
                                               </div>
                        </div> 

                         <div id="dvFinalSalary" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Final Salary :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtFinalSalary" Width ="300px" runat="server" Enabled="true" >0</asp:TextBox><asp:RequiredFieldValidator ID="reqFinalSalary" ControlToValidate="txtFinalSalary" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup ="CalculatePW"></asp:RequiredFieldValidator>
                                               </div>
                        </div>

                         <div id="dvValidatedSalary" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Val. Salary:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtValidatedSalary" Width ="300px" runat="server" Enabled="false" >0</asp:TextBox><asp:RequiredFieldValidator ID="reqtxtValidatedSalary" ControlToValidate="txtValidatedSalary" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup ="CalculatePW"></asp:RequiredFieldValidator>
                                               </div>
                        </div>
                         
                         <div id="dvAgee" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Current Age  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtAgee" Width ="300px" runat="server" Enabled="False"  ReadOnly ="true"  ></asp:TextBox>

                                               </div>
                              
                                   </div>

                         <div id="dvRAge" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Retirement Age  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtRAge" Width ="300px" runat="server" Enabled="False"  ReadOnly ="true"  ></asp:TextBox>

                                               </div>
                              
                                   </div>

                         <div id="dvDOB" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">DOB  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtDOB" Width ="300px" runat="server" Enabled="False" ReadOnly ="true"  ></asp:TextBox>

                                               </div>
                              
                                   </div>

                         <div id="dvDOR" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">DOR  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtDOR" Width ="300px" runat="server"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="reqDOR" runat ="server" ErrorMessage="*" controlToValidate="txtDOR" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="CalculatePW" ></asp:RequiredFieldValidator>

                                                   <%-- <asp:PopupControlExtender ID="calDOR_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDOR" Position="Bottom" TargetControlID="txtDOR"></asp:PopupControlExtender>

                                                    <asp:Panel ID="pnlDOR" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                  
                                                                  <Triggers>
                                                                      <asp:AsyncPostBackTrigger ControlID="calDOR" />
                                                                  </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="calDOR" runat="server" BackColor="White" 
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
                                                    </asp:Panel>--%>

                                               </div>
                              
                                   </div>

                         <div id="dvDOP" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">DOP  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtDOP" Width ="300px" runat="server"></asp:TextBox>


                                                    <asp:RequiredFieldValidator ID="reqDOP" runat ="server" ErrorMessage="*" controlToValidate="txtDOP" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="CalculatePW" ></asp:RequiredFieldValidator>
                                                    
                                                    <%--<asp:PopupControlExtender ID="calDOP_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDOP" Position="Bottom" TargetControlID="txtDOP"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlDOP" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                  <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDOP" />

                                                                     </Triggers>

                                                                 <ContentTemplate>

                                                                      <asp:Calendar ID="calDOP" runat="server" BackColor="White" 
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
                                                    </asp:Panel>--%>




                                               </div>
                              
                                   </div>

                         <div id="dvArears" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">No. Of Arrears :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtArrears" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="dvMinLump" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Min. Lump Sum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMinLumpSum" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="dv25%LumpSum" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Reg. LumpSum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txt25LumpSum" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="dvMaxLumpSum" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Max. LumpSum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMaxLumpSum" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="divRecommendedLumpSum" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Recom. LumpSum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRecommendedLumpSum" Width ="300px" runat="server" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="dvAdminCharges" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Administrative. Charges :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAdministrativeCharges" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                          

                         

                         

                                                           

                      

                    <div id="dvMgtCharges" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Mgt. Charges :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtManagement" Width ="300px" runat="server" Enabled="false" Text ="5.00%" ></asp:TextBox>
                                               </div>
                        </div>


                     <div id="dvRegulator" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Reg. Charges :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRegulator" Width ="300px" runat="server" Enabled="false" Text ="0.30%" ></asp:TextBox>
                                               </div>
                        </div>


                     <div id="dvInterest" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Int. Rate :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtInterest" Width ="300px" runat="server" Enabled="false" Text ="8.00%" ></asp:TextBox>
                                               </div>
                        </div>


                    <div id="dvNetInterest" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Net Interest :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtNetInterest" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>

                     <div id="dvNxDx" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Nx/Dx :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtNxDx" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>


                    <div id="dvNc" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Nc :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtNc" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>

                   
                     <div id="dvFrequency" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Frequency :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtFreq" Width ="300px" runat="server" Text ="12" ></asp:TextBox>
                                               </div>
                        </div>

                    
                     <div id="dvMinDraw" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Min. Monthly:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMinMonthlyDraw" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                     <div id="dvMaxDraw" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Reg. Monthly:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMaxMonthlyDraw" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                    <div id="dvRecommendMonthly" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Max. Monthly :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRecommendedMonthly" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>


                            <div id="dvAgreedPension" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Agreed Monthly :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAgreedPension" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>
                     
                    
                    
                    <div id="dvUpfront" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Total Upfront Payment :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtUpfrontPayment" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                    


                  


                         









        <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; ">
             <div style="float:left ; ">
                  <%--<asp:Button ID="btnTagAll" runat="server" Text="Tag All" />--%>
             </div>
             <div style="float:left; ">
                  <%--<asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" />--%>
             </div>
              <div style="float:left; padding-right :560px; width : 300px; ">
                  
                   <asp:ImageButton ID="btnSNR" runat ="server" ImageUrl="~/images/pdf.png" ToolTip="DownLoad Request Form" CausesValidation="False"/>
                   <asp:Button ID="btnUpDateEligibility" runat="server" Text="Calculate" Visible ="false"  />
                   <span id="spUpdateStatus" runat ="server" style ="color :red ;" visible ="false"  >lblUpdateStatus</span>
                   
             </div>
             <div>
                  <%--<asp:Button ID="btnComfirmProcessing" runat ="server" Text ="Confirm" />--%>
             </div>

             
             
        </div>


    </div>
              
          </div>
     </div>


             <asp:Button id="btnShowPopup" runat="server" style="display:none" />

             <asp:ModalPopupExtender ID="mpARLDetail" runat="server" PopupControlID="pnlARL" TargetControlID="btnShowPopup" CancelControlID="btnMPMailList" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
     <asp:Panel ID="pnlARL" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="180px" Width ="600px">

          <div class ="bodyMainDiv" style="height:180px; width: 100%" >
          <div id="Div6" style ="padding-left :20px;"><h2><span>Other Annuity Details</span></h2></div>
          
          <div id="Div7" class ="SubbodyMainDiv">            

               <div id="dv1MonthAgreedPension" class ="dvBoxRows" style="margin-top : 10px; ">
                    
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">1 Month Agreed Pension :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         
                         <asp:TextBox ID="txtOneMonthPension" runat="server" Width ="300px" Text ="0"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqOneMonthPension" runat ="server" ErrorMessage="*" controlToValidate="txtOneMonthPension" Display="Dynamic" SetFocusOnError="True" ValidationGroup="OtherAnnDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                    </div>

               </div>


               <div id="dvPremiumAmount" class ="dvBoxRows" style="margin-top : 10px; ">
                    <div class="dvBoxRowsFieldLabel" style="width:200px;">
                         <span style ="font-size : medium;">Premium Value Amount :</span>
                    </div>
                    <div style ="text-align :left ; padding-left : 250px;">
                         <asp:TextBox ID="txtPremiumValue" runat="server" Width ="300px" Text ="0"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqPremiumValue" runat ="server" ErrorMessage="*" controlToValidate="txtPremiumValue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="OtherAnnDetails" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </div>
               </div>

          </div>

     </div>
          <br />
          <asp:Button ID="btnOkay" runat="server" Text="Okay" ValidationGroup="OtherAnnDetails" />
    <asp:Button ID="btnMPMailList" runat="server" Text="Close" />
     </asp:Panel>

          </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

