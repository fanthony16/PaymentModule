<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationPaymentReport.aspx.vb" Inherits="frmApplicationPaymentReport" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="upd" runat="server">
            <ContentTemplate>
            
    <div style ="width:1600px">
    <div id="dvleftBox" class ="dvSideBox">

        <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ;">

            <div id="dvheader" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span></div>
            <div id="dvCrBody" class="dvBoxbody">

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
                
                 
                 <div id="dvAmount" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        
                       <%-- <asp:RadioButton ID="rdApprovalTypes" runat="server" GroupName="SearchType" Text ="Approval Types" AutoPostBack="True" />--%>
                        <div style ="float :left; width:80px"><span>Application Type :</span></div>
                    </div>
                    <div style ="text-align :right ;"><asp:DropDownList ID="ddApprovalType" runat="server" Width ="175px" Enabled="true"></asp:DropDownList></div>
                </div>

                  <div id="dvPaymentStatus" class ="dvBoxRows" style="float :left" >
                    <div style ="float :left; width:120px"><span>Status :</span></div>
                    <div style ="text-align :right ;"><asp:DropDownList ID="ddPaymentStatus" runat="server" Width ="175px" Enabled="False"></asp:DropDownList></div>
                </div>



              <%--  <div id="dvNarration" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                       
                        <asp:RadioButton ID="rdDesc" runat="server" GroupName="SearchType" Text ="Filter By Narration" />
                    </div>
                    <div style ="text-align :right ;"><asp:TextBox ID="txtDesc" Width ="150px" runat="server"></asp:TextBox></div>
                </div>--%>
                <div id="Div1" class ="dvBoxRows" >

                    <div style ="text-align :center ;"><asp:Button ID="btnViewTransaction" runat="server" Text="View Transactions" Visible ="true"  /></div>
                    
                </div>
            </div>

        </div>

    </div>
    <div class="dvMiddleBox" style ="width :800px;" >

        <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="500px" >
                            <asp:GridView Width="100%" ID="gridApplication" runat="server" Visible="true" PageSize="50" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridApplication_RowDataBound">
                            <Columns>
                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        
                                        <asp:BoundField DataField="txtapplicationcode" HeaderText="Application Code" ItemStyle-Width="70" />
                                        <asp:BoundField DataField="txtfullname" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="pin" HeaderText="PIN" ItemStyle-Width="100"/>
                                        <asp:BoundField DataField="txtEmployerName" HeaderText="Employer" ItemStyle-Width="250" />
                                        <asp:BoundField DataField="ApprovalType" HeaderText="Approval Type" ItemStyle-Width="150" />
                                        <asp:BoundField DataField="txtstatus" HeaderText="Approval Status" ItemStyle-Width="150" />
                  
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
                                <asp:GridView Width="100%" ID="gridSubmittedDocuments" runat="server" Visible="true" AllowPaging="True" PageSize="10" AutoGenerateColumns="False" OnRowDataBound ="gridSubmittedDocuments_RowDataBound">
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

             <div style="float:left; ">
                  
                  <asp:ImageButton ID="btnConfirmApplication" runat ="server" ImageUrl="~/images/approve.png" ToolTip="Confirm Documentation Okay!!!" CausesValidation="False"/>
                  
             </div>

            
             <div style="float:left; padding-left :20px; ">

<asp:ImageButton ID="imgDownloadSoft" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Download Soft Copy Schedule" Visible="true"/>

             </div>




    </div>
   
    </div>  


    <div class="dvLeftBox" style=" margin-top :10px; padding  :5px 10px 0px 10px; border-radius :25px 25px 0px 0px; border :2px solid ;" >

        
          <asp:Panel ID="pnlLeftGrid" Width ="100%" runat ="server" Height ="475px" >
          <asp:GridView Width="100%" ID="gridProperties" runat="server" Visible="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="false" AllowPaging ="false" ShowHeader ="false"  >
                    <Columns>
                                <asp:BoundField DataField="FieldName" HeaderText="Lodgment ID" Visible ="true" ItemStyle-Width="90"/>
                                <asp:BoundField DataField="FieldValue" HeaderText="Narration" ItemStyle-Wrap ="true" ItemStyle-Width="100" DataFormatString="{0}" />
                    </Columns>
          </asp:GridView>
          
      
</asp:Panel>




    </div>




    
   <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
   <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
      
   <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="530px">

    <div id="Div2" class ="dvSideBox"> 
        
        <div id="Div3" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div4" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Internal Control Application Comment</strong></span></div>
            <div id="Div5" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="400px" Width="95%"></asp:TextBox>
                </div>

                 <div id="dvScheduleButton"  class="dvBoxRows" style =" width :300px; float :left ; padding :10px;">
                   <div style ="width :150px; float :left ; text-align :right  ;"><asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  /></div>
                    
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

