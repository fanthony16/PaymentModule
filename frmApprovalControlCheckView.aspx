<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApprovalControlCheckView.aspx.vb" Inherits="frmApprovalControlCheckView" Theme="blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
                         
               <div class ="bodyMainDiv" style ="width :1750px;" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Pencom Approval Comfirmation...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                        <div style ="text-align :left  ; padding : 5px;" ><span >Select The Approval Type: </span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddApplicationType" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqApplicationType" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="pencomApproval"></asp:RequiredFieldValidator>

                        </div>

                         
                         <%--<div style ="text-align :left  ; padding : 5px;" ><span >Select The Approval Batch: </span></div>
                         <div id="Div2" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddApprovalBatch" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddExportedBatches" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="pencomApproval"></asp:RequiredFieldValidator>

                        </div>--%>


                         <div style ="text-align :left  ; padding : 5px;" ><span >Enter The Approval Batch: </span></div>
                         <div id="Div16" style ="padding :5px; text-align :right ;" >
                             
                              <asp:TextBox ID="txtApprovalBatches" runat="server" Width ="300px" ValidationGroup="ExportedBatches" Text ="PENCOM/TECH/"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqApprovalBatches" runat="server" ErrorMessage="*" ControlToValidate="txtApprovalBatches" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="ExportedBatches"></asp:RequiredFieldValidator>

                        </div>
                         <div style ="text-align :right ; margin-top :10px; margin-bottom : 10px; ">
                             <div style ="padding-right :10px;"> <asp:Button ID="btnViewExportedBatches" runat="server" Text="View Exported Batches" ValidationGroup="ExportedBatches" /></div>

                        </div>
                         
                         <div style ="text-align :left  ; padding : 5px;" ><span >Select The Export Batch: </span></div>
                         <div id="dvExportedBatches" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddExportedBatches" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqExportedBatches" runat="server" ErrorMessage="*" ControlToValidate="ddExportedBatches" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="pencomApproval"></asp:RequiredFieldValidator>

                        </div>


                         


                        <div style ="text-align :right ; margin-top :10px; margin-bottom : 10px; ">
                             <div style ="padding-right :10px;"> <asp:Button ID="btnFind" runat="server" Text="Find" ValidationGroup="RMASComfirmation" /></div>

                        </div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div2" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>New Pension Entrants</strong></span> 
                        </div>
                        
                        
                                                  


                         <div id="dvStateDate" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>Start Date :</span></div>
                                <div style="text-align :right ;"><asp:TextBox ID="txtStartDate" runat="server" Width ="200px"></asp:TextBox></div>
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
                                <div style="text-align :right ; "><asp:TextBox ID="txtEndDate" runat="server" Width ="200px"></asp:TextBox>            
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
                         
                   
                        
                         
                         
                        

                         
                         

                        <div style ="text-align :right ; margin-top :10px; margin-bottom : 10px; ">
                             <div style ="padding-right :10px;"> <asp:Button ID="btnPensionExtract" runat="server" Text="DownLoad New Pension Entrants" ValidationGroup="newEntrants" /></div>

                        </div>

                         <div id="dvExtractError" runat ="server" visible ="false" style="color :red ;" ><span id="spExtractError" runat ="server" ></span></div>

            <asp:Panel ID="Panel1" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>



            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :10px; padding  :5px 10px 20px 10px; width :77%; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="600px"  >
                                  <asp:GridView Width="100%" ID="gridApplications" runat="server" Visible="true" PageSize="60" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridExport_OnRowDataBound" >
                                 
                                 <Columns >
                                        
                                        
                                        <asp:ButtonField CommandName="Select" Text="Select"/>

                                        <asp:TemplateField HeaderText="Checked?">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalChecked" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Verified?">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalVerified" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Authorised?">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalAuthorised" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="txtApplicationCode" HeaderText="Application Code" ItemStyle-Width="150"/>
                                        <asp:BoundField DataField="PIN" HeaderText="PIN" ItemStyle-Width="100"/> 
                                        <asp:BoundField DataField="FullName" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="Sector" HeaderText="Sector"/>
                                        <asp:BoundField DataField="numApproved" HeaderText="Approved Amount" DataFormatString="{0:N}" />
                                      <asp:BoundField DataField="dteValueDate" HeaderText="Value Date" DataFormatString="{0:d}" />
                                      <asp:BoundField DataField="dteStartPeriod" HeaderText="Start Period" DataFormatString="{0:d}" />
                                      <asp:BoundField DataField="dteEndPeriod" HeaderText="End Period" DataFormatString="{0:d}" />

                                      <asp:TemplateField HeaderText="">
                                                   <ItemTemplate>
                                                                      <asp:ImageButton OnClick="btnView_ApprovalComment" ID="btnViewComments" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" ItemStyle-Width ="10px" />
                                        
                                                   </ItemTemplate>
                                                                   
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="">
                                                   <ItemTemplate>
                                                                      <asp:ImageButton OnClick="btnView_ApplicationComment" ID="btnViewApplicationComments" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="View Application Comment(s)" ItemStyle-Width ="10px" />
                                        
                                                   </ItemTemplate>
                                                                   
                                        </asp:TemplateField>
                                        
                                           <asp:TemplateField HeaderText="">
                                                  <ItemTemplate>

                                                       <asp:ImageButton OnClick="Reset_Click" ID="btnResetUnprocessed" runat ="server" ImageUrl="~/images/remove.jpg" ToolTip="Reset To Unprocessed"  ItemStyle-Width ="10px"/>

                                                  </ItemTemplate>
                                             </asp:TemplateField>

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

             <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; margin-bottom :50px; ">
                  <div style="float:left ; padding-right :20px; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
                  
                  <div style="float:left; padding-right :0px;"><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>
                  <div id="Div15" style="float:left; padding-left :20px;" runat ="server" visible="true" ><asp:Button ID="btnSchedule" runat="server" Text="DownLoad Schedule" /></div>
                   <div style="float:left ;padding-left :30px; "><asp:ImageButton ID="imgDownloadSoft" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Download Schedule Soft Copy" Visible="true"/>

             </div>
             </div>
                       

    </div>

             
              
              
          </div>
     </div>

              
          
               <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
   <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
      
   <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div3" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div4" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div5" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Approval Control Comment</strong></span></div>
            <div id="Div6" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="80px" Width="100%" MaxLength="70"></asp:TextBox>
                </div>

                 <div id="Div7"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div8"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/add.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppComments" runat="server" Text="Close" />
    </asp:Panel>






                <asp:Button id="btnShowApplicationCommentPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpApplicationComments" runat="server" PopupControlID="pnlApplicationComments" TargetControlID="btnShowApplicationCommentPopup" CancelControlID="btnMPApplicationComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
      
                  <asp:Panel ID="pnlApplicationComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div9" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div10" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div11" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
            <div id="Div12" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationIDD" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationCommentt" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="80px" Width="100%" MaxLength="70" Enabled ="false" ></asp:TextBox>
                </div>

                 <div id="Div13"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="imgAddComment" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment" Visible ="false"   />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstApplicationComments" runat="server" Width ="100%" Height ="300px" AutoPostBack="True"></asp:ListBox>
                </div>

                 <div id="Div14"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="imgRemoveComment" runat ="server" ImageUrl="~/images/add.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment" Visible ="false"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPApplicationComments" runat="server" Text="Close" />
    </asp:Panel>


          
          </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

