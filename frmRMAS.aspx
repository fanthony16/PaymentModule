<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmRMAS.aspx.vb" Inherits="frmRMAS" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
                         
     <div class ="bodyMainDiv" style="width :1700px;" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Benefit Application(s) RMAS Submission...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                        <div style ="text-align :right ; padding : 5px;" ><span >Select The Approval Type: </span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddApprovalType" runat="server" Width ="300px" ValidationGroup="processing"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqApprovalType" runat="server" ErrorMessage="*" ControlToValidate="ddApprovalType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="RMASComfirmation"></asp:RequiredFieldValidator>

                        </div>

                         <div style ="text-align :right ; padding : 5px;" ><span >Select Report Date: </span></div>
                        <div id="Div2" style ="padding :5px; text-align :right ;" >
                             <asp:TextBox ID="txtReportDate" runat="server" Width ="300px"></asp:TextBox>
                             
                             <asp:RequiredFieldValidator ID="reqReportDate" runat ="server" ErrorMessage="*" controlToValidate="txtReportDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="Processing" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calReportDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlReportDate" Position="Bottom" TargetControlID="txtReportDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlReportDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                  <Triggers>

                                                                      <%--<asp:AsyncPostBackTrigger ControlID="calReportDate" />--%>

                                                                     </Triggers>
                                                                 <ContentTemplate>


                                                                      <asp:Calendar ID="calReportDate" runat="server" BackColor="White" 
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

                        <div style ="text-align :right ; padding :5px;">
                             <asp:Button ID="btnRMASSMS" runat="server" Text="Push RMAS SMS" ValidationGroup="RMASComfirmation" CausesValidation="False" ToolTip ="Note That This Process is Not Date Dependent" />

                             <asp:Button ID="btnFind" runat="server" Text="Find" ValidationGroup="RMASComfirmation" CausesValidation="False" />


                        </div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" style="color: #FF0000" >.</span></div></asp:Panel>
            
            
                  </div>



            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :10px 0px 0px 0px; padding  :5px 10px 0px 10px; width :75%; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridRMAS" runat="server" Visible="true" PageSize="40" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridRMAS_OnRowDataBound" >
                                 
                                 <Columns >
                                      
                                      <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkRMASApproval" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField CommandName="Select" Text="Select"/>

                                       <%--<asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewIACComment_Click" ID="btnAddViewIACComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="View IAC Comment(s)" OnClientClick="AddViewIACComment_Click" ItemStyle-Width ="10px" />
                                        
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
             <div style="float:left ; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
             <div style="float:left; "><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>
             
        </div>
                

        <div id="dvMainProcessButton" style ="width :100%; padding: 00px; margin-top : 30px; margin-bottom : 30px; padding :4px;">

             <div style="float:left"><asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Comfirm Schedule" /></div>

             <div style="float:left; padding-left :20px;"><asp:Button ID="btnReset" runat="server" Text="Reset To Un-Processed Application" /></div>

        </div>


    </div>
              
              
          </div>
     </div>

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




               <asp:Button id="btnShowHardShipOverridePopup" runat="server" style="display:none" />
   <asp:ModalPopupExtender ID="mpHardShipOverrideExtender" runat="server" PopupControlID="pnlHardShipOverride" TargetControlID="btnShowHardShipOverridePopup" CancelControlID="btnMPHardShipOverride" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>                
   <asp:Panel ID="pnlHardShipOverride" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="220px">

    <div id="Div3" class ="dvSideBox"> 
        
        <div id="Div4" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div5" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Override HardShip Benefit Calculation</strong></span></div>
            <div id="Div6" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
                    <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left; margin-left :25px; "><asp:TextBox ID="txtHardApplicationCode" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
               </div>


                 <div class="dvBoxRows" style =" width :300px;">
                   
                    <div style="padding-top :5px;padding-bottom  :5px;  margin-bottom  :15px;">
                    <div style ="float :left "><span>RSA Balance :</span></div>
                    <div style ="float :left; margin-left :35px; "><asp:TextBox ID="txtRSABalance" runat="server" Width ="150px" Enabled="true"></asp:TextBox></div>
               </div>

                <div class="dvBoxRows" style =" width :300px;">
                   
                    <div style="padding-top :5px; padding-bottom  :5px; margin-bottom  :15px; ">
                    <div style ="float :left "><span>HardShip Amount :</span></div>
                    <div style ="float :left; margin-left :5px "><asp:TextBox ID="txtHardShipAmount" runat="server" Width ="150px" Enabled="true"></asp:TextBox></div>
                    </div>

                   
               </div>

                <div class="dvBoxRows" style =" width :300px;">
                   
                    <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><asp:Button ID="btnOverrideAmount" runat="server" Text="Override Amounts" /></div>
                    
                    </div>

                   
               </div>

                

            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPHardShipOverride" runat="server" Text="Close" />
    </asp:Panel>



         </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

