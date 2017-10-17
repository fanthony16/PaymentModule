<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmConfirmation.aspx.vb" Inherits="frmConfirmation" Theme ="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat ="server" >
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
                                <asp:GridView Width="100%" ID="gridSubmittedDocuments" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False" OnRowDataBound ="gridSubmittedDocuments_RowDataBound">
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
              <div style="float:left; padding-right :560px; ">
                  <%--<asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" />--%>
                   <asp:ImageButton ID="btnSNR" runat ="server" ImageUrl="~/images/pdf.png" ToolTip="DownLoad SNR Form(s)" CausesValidation="False"/>
             </div>
             <div style="float:left">
                  <%--<asp:Button ID="btnReject" runat="server" Text="Reject" />--%>
                  <asp:ImageButton ID="btnReject" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Reject"/>
             </div>
             <div>
                  <%--<asp:Button ID="btnComfirmProcessing" runat ="server" Text ="Confirm" />--%>
                  <asp:ImageButton ID="btnComfirmProcessing" runat ="server" ImageUrl="~/images/approve.png" ToolTip="Confirm"/>
             </div>

             
             
        </div>


        <div id="dvPriceDate" style ="width :100%; padding: 10px; margin-top : 10px; margin-bottom : 10px;">

             <%--<div style="float:left;margin-right :5px "><span>Set Application Status :</span> </div>--%>
             <%--<div style="float:left "><asp:DropDownList ID="ddApplicationStatusBatch" Width ="200px" runat="server" Visible ="false" ></asp:DropDownList><asp:Button ID="btnSetStatus" runat="server" Text="Reject" /></div>--%>
             
        </div>
        <div id="dvMainProcessButton" style ="width :100%; padding: 10px; margin-top : 10px; margin-bottom : 10px;">

             <%--<div style="float:left"><asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Confirm" /></div>--%>
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
                                                              
                                                             <%-- <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="UpdatePrice_Click" ID="btnUpdatePrice" runat ="server" ImageUrl="~/images/K view.png" ToolTip="Update Enpower with Price" OnClientClick="UpdatePrice_Click" ItemStyle-Width ="5px" />
                                        
                                                                  </ItemTemplate>

                                                               </asp:TemplateField>--%>
                                                              
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
          
          
          
          </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

