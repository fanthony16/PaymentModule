<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmPencomTemplate.aspx.vb" Inherits="frmPencomTemplate" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

     <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Enhancement Monthly Pension Template...</span></h2></div>
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
                                                    <asp:TextBox ID="txtPIN" Width ="300px" runat="server" Enabled="true" ></asp:TextBox>
                                                   <asp:Button ID="btnFind" runat="server" Text="..." Visible ="true" ValidationGroup="FindPersonDetails" />
                                               </div>
                              
                                          </div>

                         <div id="dvSex" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Sex :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtSex" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                                   
                                               </div>
                                          </div>

                         <div id="dvCurrentPension" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Pension :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtMonthPencom" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>

                         <div id="dvRSA" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">RSA Balance :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRSABalance" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>                                   

                      <div id="dvAge" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Age :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtAge" Width ="300px" runat="server" Enabled="true" AutoPostBack ="true"  ></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="reqAge" runat ="server" ErrorMessage="*" ControlToValidate="txtAge" Display="Dynamic" SetFocusOnError="True"  Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>

                                                    <asp:CompareValidator ID="compAge" runat="server" ErrorMessage="Bad Input" ControlToValidate="txtAge" Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ForeColor="Red" ></asp:CompareValidator>

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
                                                    <asp:TextBox ID="txtInterest" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
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

                    <div id="Div1" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Calc. Amt. :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRecommendedAmount" Width ="300px" runat="server" Enabled="false" Text ="0" ></asp:TextBox>
                                               </div>
                        </div>



                     <div id="dvFrequency" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Frequency :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtFreq" Width ="300px" runat="server" Enabled="false" Text ="12" ></asp:TextBox>
                                               </div>
                        </div>

                    


                     <div id="dvVariance" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Pension Diff :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtVariance" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>


                    <div id="dvEnhancement" class ="dvBoxRows">
                                               <div class="dvBoxRowsFieldLabel" >
                                                    <span style ="font-size : medium;">Status :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtStatus" Width ="300px" runat="server" Enabled="false" ></asp:TextBox>
                                               </div>
                        </div>


                         









        <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; ">
             <div style="float:left ; ">
                  <%--<asp:Button ID="btnTagAll" runat="server" Text="Tag All" />--%>
             </div>
             <div style="float:left; ">
                  <%--<asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" />--%>
             </div>
              <div style="float:left; padding-right :560px; ">
                  <%--<asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" />--%>
                   <asp:ImageButton ID="btnSNR" runat ="server" ImageUrl="~/images/pdf.png" ToolTip="DownLoad Request Form" CausesValidation="False"/>
             </div>
             <div>
                  <%--<asp:Button ID="btnComfirmProcessing" runat ="server" Text ="Confirm" />--%>
             </div>

             
             
        </div>


    </div>
              
          </div>
     </div>

</asp:Content>

