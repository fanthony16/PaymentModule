<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmPWCalculator.aspx.vb" Inherits="frmPWCalculator" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

     <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Programmed Withdrawal Pension Calculator...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >
            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >


                         <div id="Div2" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                   <%-- <span style ="font-size : medium;">Sex :</span>--%>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <span style="color: #FF0000; font-weight: bold;" runat ="server" id="spError" visible ="false"  >Error !!!. Retirement Date is Missing For the Customer</span>
                                                   
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

                         <div id="dvSex" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Sex :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSex" Width ="300px" runat="server" ></asp:TextBox>
                                                   
                                               </div>
                                          </div>
           
                         <div id="dvRSA" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">RSA Balance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRSABalance" Width ="300px" runat="server" ></asp:TextBox>
                                               </div>
                        </div> 

                         <div id="dvFinalSalary" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Final Salary :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtFinalSalary" Width ="300px" runat="server" Enabled="true" ></asp:TextBox><asp:RequiredFieldValidator ID="reqFinalSalary" ControlToValidate="txtFinalSalary" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup ="CalculatePW"></asp:RequiredFieldValidator>
                                               </div>
                        </div>
                         
                         <div id="dvAgee" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Age  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtAgee" Width ="300px" runat="server" Enabled="true"  ></asp:TextBox>

                                               </div>
                              
                                   </div>

                         <div id="dvDOB" class ="dvBoxRows">

                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">DOB  :</span>
                                               </div>

                                               <div style ="text-align :left ;">

                                                   <asp:TextBox ID="txtDOB" Width ="300px" runat="server" Enabled="true" Text="2016-12-31"  ></asp:TextBox>

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
                                                    <span style ="font-size : medium;">25% LumpSum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txt25LumpSum" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="dvMaxLumpSum" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Max LumpSum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMaxLumpSum" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="divRecommendedLumpSum" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Recomm. LumpSum :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRecommendedLumpSum" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
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
                                                    <span style ="font-size : medium;">Min. Monthly Drawdown:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMinMonthlyDraw" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                     <div id="dvMaxDraw" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Max. Monthly Drawdown:</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMaxMonthlyDraw" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>

                    <div id="dvRecommendMonthly" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Recomm. Monthly :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRecommendedMonthly" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
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

</asp:Content>

