<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmforSubmission.aspx.vb" Inherits="frmforSubmission" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

     <asp:UpdatePanel ID="updFormPanle" runat="server" >
          <ContentTemplate>

          

     <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Comfirmed Benefit Application(s)...</span></h2></div>
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
                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFind" runat="server" Text="Find" ValidationGroup="processing" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>



            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" >
                            <asp:GridView Width="100%" ID="gridProcessing" runat="server" Visible="true" PageSize="50" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" >
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
                                        <asp:BoundField DataField="RetirementDate" HeaderText="Retirement Date" DataFormatString="{0:d}" ItemStyle-Width="250" />
                                       <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
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

             <div style="float:left; margin-right :5px; padding-left :20px;  "><span>Set Submission Date :</span> </div>
             <div style="float:left;margin-right :5px; height: 258px;">

                  <asp:TextBox ID="txtDateSubmission" runat="server" Width ="100px"></asp:TextBox>

                  <asp:RequiredFieldValidator ID="reqDateSubmission" runat ="server" ErrorMessage="*" controlToValidate="txtDateSubmission" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup ="Submission" ></asp:RequiredFieldValidator>
                                                    <asp:PopupControlExtender ID="calDateSubmission_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDateSubmission" Position="Bottom" TargetControlID="txtDateSubmission"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlDateSubmission" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                  <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDateSubmission" />

                                                                     </Triggers>
                                                                 <ContentTemplate>


                                                                      <asp:Calendar ID="calDateSubmission" runat="server" BackColor="White" 
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
             <div style="float:left"><asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Generate Submission Schedule" ValidationGroup ="Submission" /></div>
              <div style="float:left"><asp:Button ID="btnNonSubmissionSchedule" runat="server" Text="Generate Non-Submission Schedule" ValidationGroup ="Submission" /></div>
        </div>
        <div id="dvSNRForm">

             <div style="float:left; text-align :left  "><asp:ImageButton ID="btnSNR" runat ="server" ImageUrl="~/images/pdf.png" ToolTip="DownLoad SNR Form(s)" CausesValidation="False"/>

             </div>

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
          
      
</asp:Panel>

    </div>
              
          </div>
     </div>


                <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
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



               </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

