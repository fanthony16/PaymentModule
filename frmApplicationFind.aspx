<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationFind.aspx.vb" Inherits="frmApplicationFind" Theme ="Blue"  %>
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
     <%--<asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>--%>


                 <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Benefit Application List...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >

                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Find By PIN(s)</strong></span> 
                        </div>
                        
                       
                        <div id="dvFindPIN" style ="padding :5px; text-align :right ;" >
                            
                             <asp:TextBox ID="txtFindPIN" runat ="server" Width ="300px" TextMode="MultiLine" Height ="70px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqPIN" runat="server" ErrorMessage="*" ControlToValidate="txtFindPIN" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindPIN"></asp:RequiredFieldValidator>

                        </div>
                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFindPIN" runat="server" Text="Find" ValidationGroup="FindPIN" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>


                     <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div8" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Find By Payment Type</strong></span> 
                        </div>
                        

                <div id="dvStateDate" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>Start Date :</span></div>
                                <div style="text-align :right ;"><asp:TextBox ID="txtStartDate" runat="server" Width ="200px" ValidationGroup="FindType"></asp:TextBox></div>
                                <asp:PopupControlExtender ID="calSDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlStartDate" Position="Bottom" TargetControlID="txtStartDate"></asp:PopupControlExtender>
                                <asp:Panel ID="pnlStartDate" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <asp:Calendar ID="calSDate" runat="server" BackColor="White" 
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
                <div id="dvEndDate" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>End Date :</span></div>
                                <div style="text-align :right ; "><asp:TextBox ID="txtEndDate" runat="server" Width ="200px" ValidationGroup="FindType"></asp:TextBox>            
                                </div>
                                        <asp:PopupControlExtender ID="calEDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlEndDate" Position="Bottom" TargetControlID="txtEndDate"></asp:PopupControlExtender>
                        <asp:Panel ID="pnlEndDate" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="calEDate" runat="server" BackColor="White" 
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


                          <div id="Div9" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>App. Type :</span></div>
                                <div style="text-align :right ; "><asp:DropDownList ID="ddApplicationType" runat="server" Width ="200px" ValidationGroup="FindType"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqddApplicationType" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindType"></asp:RequiredFieldValidator>
                         </div>
</div> 

                          <div id="dvAppStatus" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>App. Stage :</span></div>
                                <div style="text-align :right ; "><asp:DropDownList ID="ddApplicationStatus" runat="server" Width ="200px" ValidationGroup="FindType"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqddApplicationStatus" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationStatus" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindType"></asp:RequiredFieldValidator>
                         </div>
</div> 

                          <div id="dvCheckProcessedWithin" class ="dvBoxRows">
                                <%--<div style ="float :left; width:80px"></div>--%>
                                <div style="text-align :right ; ">
                                     <asp:CheckBox ID="chkSentToPencomDate" runat="server" Text ="Search Application By Sent To Pencom Date" Checked ="false" TextAlign ="Left" Visible="true"   />
                                     <asp:CheckBox ID="chkProcessedWithin" runat="server" Text ="Recieved and Processed Within Report Period" Checked ="false" TextAlign ="Left" Visible="False"   />
                                     <asp:CheckBox ID="chkPrevProcessedWithin" runat="server" Text ="Prev. Month & Processed Within Report Period" Checked ="false" TextAlign ="Left" Visible="False"   />
                                </div>
</div> 




                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFindByDate" runat="server" Text="Find" ValidationGroup="FindType" /></div>
            <asp:Panel ID="Panel1" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span></div></asp:Panel>
            
                  </div>

            
        </div>


         






               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >
                    <div id="dvPartDetails" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Participant's Details</strong></span> 
                        </div>

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
                    <asp:Panel ID="pnlGridApplication" Width ="100%" runat ="server" Height ="600px"  >

                         <div id="dvApplicationDetails" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Benefit Application's Details</strong></span> 
                        </div>

                            <asp:GridView Width="100%" ID="gridParticipantApps" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridProcessing_RowDataBound">
                                 <Columns>

                                        
                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        <asp:BoundField DataField="Application No" HeaderText="Application Code" ItemStyle-Width="70" />
                                        <asp:BoundField DataField="ApprovalType" HeaderText="Application Type" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="dteApplicationDate" HeaderText="Application Date" ItemStyle-Width="150" DataFormatString="{0:d}"/>
                                        <asp:BoundField DataField="txtStatus" HeaderText="Status" ItemStyle-Width="250" />
                                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" ItemStyle-Width="250" />

                                        <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                                        </asp:TemplateField>


                                       <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" OnClientClick="SetTarget();" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>


                                      <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewIACComment_Click" ID="btnAddViewIACComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="View IAC Comment(s)" OnClientClick="AddViewIACComment_Click" ItemStyle-Width ="10px" />
                                        
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
       
        <div style="float:left ;padding-left :30px; ">
             <div style ="float :left;">
             <asp:ImageButton ID="imgExport" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To Excel" Visible="true"/>
             </div>
             <div style ="padding-left :100px;"><span id="spRowCount" runat ="server" visible ="false" style ="color :red;" ></span></div>
             </div>

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
             <div style="float:left ;padding-left :30px; "><asp:ImageButton ID="btnCancel" runat ="server" ImageUrl="~/images/remove.png" ToolTip="Cancel Application" Visible="false"/>

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



        <%--  </ContentTemplate>

          </asp:UpdatePanel>--%>

</asp:Content>

