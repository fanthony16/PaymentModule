<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmAdvApplicationFind.aspx.vb" Inherits="frmAdvApplicationFind" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

     <script type = "text/javascript">
          function SetTarget() {
               document.forms[0].target = "_blank";
          }
        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

    <%-- <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>--%>

                 <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Benefit Application List...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >

                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Find By PIN</strong></span> 
                        </div>
                        
                       
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                            
                             <asp:TextBox ID="txtFindPIN" runat ="server" Width ="300px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqPIN" runat="server" ErrorMessage="*" ControlToValidate="txtFindPIN" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindPIN"></asp:RequiredFieldValidator>

                        </div>
                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFindPIN" runat="server" Text="Find" ValidationGroup="FindPIN" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>


                    <div id="dvShelveNumber" style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;" runat ="server" >
                        <div id="Div8" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Enter Shelve Number</strong></span> 
                        </div>
                        
                       
                        <div id="Div9" style ="padding :5px; text-align :right ;" >
                            
                             <asp:TextBox ID="txtShelveNo" runat ="server" Width ="300px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqShelveNo" runat="server" ErrorMessage="*" ControlToValidate="txtShelveNo" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="UpdateShelve"></asp:RequiredFieldValidator>

                        </div>

                        <div id="Div10" style ="padding :5px; text-align :right ;" >
                            
                             <asp:CheckBox ID="chkIsDocRecieved" runat ="server" Text ="Is Hard Copy Recieved?" TextAlign ="Left"  />

                        </div>

                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnUpdateShelve" runat="server" Text="Save" ValidationGroup="UpdateShelve" /></div>
            <asp:Panel ID="Panel1" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>





                    <div id="Div11" style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;" runat ="server" >
                        <div id="Div12" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Update Fund Platform</strong></span> 
                        </div>
                        
                       
                        <div id="Div13" style ="padding :5px; text-align :right ;" >
                            
                             
                             <asp:DropDownList ID="ddFundPlatform" runat="server" Width ="300px"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqddFundPlatform" runat="server" ErrorMessage="*" ControlToValidate="ddFundPlatform" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="UpdateFund"></asp:RequiredFieldValidator>

                        </div>

                        <div id="Div14" style ="padding :5px; text-align :right ;" >
                            
                        </div>

                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnUpdateFundPlatform" runat="server" Text="Update Fund Platform" ValidationGroup="UpdateFund" /></div>
            <asp:Panel ID="Panel2" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span2" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>



<%--                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div8" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Find By Surname</strong></span> 
                        </div>
                        
                       
                        <div id="dvFindSurname" style ="padding :5px; text-align :right ;" >
                            
                             <asp:TextBox ID="txtSurname" runat ="server" Width ="300px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqSurname" runat="server" ErrorMessage="*" ControlToValidate="txtSurname" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindSurname"></asp:RequiredFieldValidator>

                        </div>
                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFindSurname" runat="server" Text="Find" ValidationGroup="FindSurname" /></div>
            <asp:Panel ID="Panel1" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>--%>



            
        </div>


         






               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >
                    <asp:Panel ID="pnlParticipants" Width ="100%" runat ="server" Height ="300px"  >

                                             <asp:GridView Width="100%" ID="gridParticipant" runat="server" Visible="true" PageSize="5" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridProcessing_RowDataBound">
                                 <Columns>

                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        <asp:BoundField DataField="rsapin" HeaderText="PIN" ItemStyle-Width="150"/>
                                        <asp:BoundField DataField="Surname" HeaderText="Surname" ItemStyle-Width="70" />
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="MiddleName" HeaderText="Other Names" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="EmployerName" HeaderText="Employer Name" ItemStyle-Width="250" />
                                        <asp:BoundField DataField="Phone" HeaderText="Phone No" ItemStyle-Width="250" />
                                        <asp:BoundField DataField="dateofbirth" HeaderText="Date Of Birth" ItemStyle-Width="250" DataFormatString="{0:d}" />

                                 </Columns>
                    
                                        <pagersettings mode="NextPreviousFirstLast"
                                        firstpagetext="First"
                                        lastpagetext="Last"
                                        nextpagetext="Next"
                                        previouspagetext="Prev"   
                                        position="Bottom"/> 
                              </asp:GridView>

                    </asp:Panel>
                    <hr />
                    <asp:Panel ID="pnlGridApplication" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridParticipantApps" runat="server" Visible="true" PageSize="25" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridProcessing_RowDataBound">
                                 <Columns>

                                        
                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        <asp:BoundField DataField="txtApplicationCode" HeaderText="Application Code" ItemStyle-Width="70" />
                                        <asp:BoundField DataField="ApprovalType" HeaderText="Application Type" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="dteApplicationDate" HeaderText="Application Date" ItemStyle-Width="150" DataFormatString="{0:d}"/>
                                        <asp:BoundField DataField="txtStatus" HeaderText="Status" ItemStyle-Width="250" />

                                        <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                                        </asp:TemplateField>


                                       <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" OnClientClick="SetTarget();" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>


                                        <%--<asp:BoundField DataField="ExitDate" HeaderText="Exit Date" DataFormatString="{0:d}" />--%>
                                        
                                        <%--<asp:BoundField DataField="PIN" HeaderText="Pencom PIN" DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="ValueDate" HeaderText="Value Date" DataFormatString="{0:d}" />--%>
                  
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
                                <asp:GridView Width="100%" ID="gridSubmittedDocuments" runat="server" Visible="true" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" OnRowDataBound ="gridSubmittedDocuments_RowDataBound">
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


        <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; height :0px; ">
             <%--<div style="float:left ; "><asp:ImageButton ID="btnNew" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add New Application" Visible="False"/></div>--%>
             <%--<div style="float:left ;padding-left :30px; "><asp:ImageButton ID="btnEdit" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="Edit Application"/>     </div>--%>
             <div style="float:left ;padding-left :30px; "><asp:ImageButton ID="btnCancel" runat ="server" ImageUrl="~/images/remove.png" ToolTip="Cancel Application" Visible="False"/>

             </div>
             
             <div id="dvErrorMessage" style="float:left; color :red ; padding-left :0px; width : 120px;" runat ="server" visible ="false"  ><span>Multiple Selection Not Allowed !!! </span></div>

             <%--<div style="float:left ; padding-left :350px; ">
                  
                  <asp:ImageButton ID="btnTagAll" runat ="server" ImageUrl="~/images/success.png" ToolTip="Tag All"/>
             </div>
             <div style="float:left; padding-left :10px;">
                  
                  <asp:ImageButton ID="btnUnTagAll" runat ="server" ImageUrl="~/images/error.png" ToolTip="Un-Tag All"/>
             </div>--%>
             
        </div>
        <div id="dvPriceDate" style ="width :100%; padding: 10px; margin-bottom : 10px;">

             

<%--             
             <div style="float:left "><asp:DropDownList ID="ddApplicationStatusBatch" Width ="200px" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="reqddApplicationStatus" runat="server" ErrorMessage="*" ValidationGroup="ChangeStatus" ControlToValidate ="ddApplicationStatusBatch" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator></div>--%>
             
        </div>
        <div id="dvMainProcessButton" style ="width :100%; padding: 10px; margin-top : 10px; margin-bottom : 10px;">

             <%--<div style="float:left"><asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Change Application Status" ValidationGroup="ChangeStatus" /></div>--%>
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
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="ApplicationComment" Height="80px" Width="100%" MaxLength="70"></asp:TextBox><asp:RequiredFieldValidator ID="reqApplicationComment" runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ControlToValidate="txtApplicationComment" ValidationGroup="ApplicationComment"></asp:RequiredFieldValidator>
                </div>

                 <div id="Div6"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="ApplicationComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div7"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppComments" runat="server" Text="Close" />
    </asp:Panel>


         <%-- </ContentTemplate>

          </asp:UpdatePanel>--%>

</asp:Content>

