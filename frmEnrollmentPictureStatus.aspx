<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmEnrollmentPictureStatus.aspx.vb" Inherits="frmEnrollmentPictureStatus" Theme="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
     
          

     <div id="dvSideBox" style="float:left; width:320px; height :300px;" >



            <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ;">
                <div id="dvCrHeader" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> </div>
                <div id="dvCrBody" style="height:auto; padding:5px;">
                    <div style="padding:5px;">
                        <div style ="float :left; width:80px"><span>Start Date :</span></div>
                        <div style="width:260px;"><asp:TextBox ID="txtStartDate" runat="server" Width ="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

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
                    <div style="padding:5px;">
                        <div style ="float :left; width:80px"><span>End Date :</span></div>
                        <div style="width:260px; "><asp:TextBox ID="txtEndDate" runat="server" Width ="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
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
                    
                    <div style="padding:0px;">
                       
                        <div style ="text-align:center;"><asp:Button ID="btnGenerate" runat="server" Text="View Results" /></div>

                    </div>
                     <%--<div style ="text-align:center ; height :40px; margin-top:20px; color :red ;"> <span id="spApplicationCount" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span> Record(s) Retrieved !</div>--%>
                    
                </div>
            </div>
        </div>

     <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; margin-bottom :10px; padding  :5px 10px 20px 10px; width :70%; " >

                    <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="870px"  >
                                  <asp:GridView Width="100%" ID="gridApplications" runat="server" Visible="true" PageSize="50" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound = "gridExport_OnRowDataBound" >
                                 
                                 <Columns >
                                        
                                        
                                        <asp:ButtonField CommandName="Select" Text="Select"/>

                                       <%-- <asp:TemplateField HeaderText="Is Biometric Available ?">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalChecked" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>--%>

                                      <%--<asp:TemplateField HeaderText="Verified?">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalVerified" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Authorised?">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPINApprovalAuthorised" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>--%>
                                      
                                      <asp:BoundField DataField="IsEAuditAvailable" HeaderText="Is E-Audit Available?" ItemStyle-Width="100"/>   
                                      <asp:BoundField DataField="IsImageAvailable" HeaderText="Is Image Available?" ItemStyle-Width="100"/>  
                                      <asp:BoundField DataField="PIN" HeaderText="PIN" ItemStyle-Width="100"/> 
                                        <asp:BoundField DataField="FullName" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="PinnedDate" HeaderText="Pinned Date" ItemStyle-Width="100" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="200"/> 

                                      <asp:TemplateField HeaderText="Enpower Image">

                                                   <ItemTemplate>
                                                                      <asp:ImageButton OnClick="ViewPassport_Click" ID="btnViewPassport" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Enpower Passport" ItemStyle-Width ="10px" />
                                        
                                                   </ItemTemplate>
                                                                   
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="PSA Image">
                                                   <ItemTemplate>
                                                                      <asp:ImageButton OnClick="ViewPSAPassport_Click" ID="btnViewPSAPassport" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View PSA Passport" ItemStyle-Width ="10px" />
                                        
                                                   </ItemTemplate>
                                                                   
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Update">
                                                   <ItemTemplate>
                                                                      <asp:ImageButton OnClick="UpdateEnpowerPassport_Click" ID="btnUpdateEnpowerPassport" runat ="server" ImageUrl="~/images/update.bmp" ToolTip="View Passport" ItemStyle-Width ="10px" />
                                        
                                                   </ItemTemplate>
                                                                   
                                        </asp:TemplateField>

                               <%--       <asp:TemplateField HeaderText="">
                                                   <ItemTemplate>
                                                                      <asp:ImageButton OnClick="btnView_ApplicationComment" ID="btnViewApplicationComments" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="View Application Comment(s)" ItemStyle-Width ="10px" />
                                        
                                                   </ItemTemplate>
                                                                   
                                        </asp:TemplateField>--%>

                                 <%--  <asp:TemplateField HeaderText="">
                                   <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>--%>
                                        


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
           
                         <%--<div style="float:left ; padding-right :20px; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>--%>
                  
                  <%--<div style="float:left; padding-right :0px;"><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>--%>
                  <%--<div id="dvChecked" style="float:left; padding-left: 20px;" runat="server"><asp:Button ID="btnChecked" runat="server" Text="IC Checked" /></div>--%>
                  
                   <%--<div id="dvCancelCheck" style="float:left; padding-left: 20px;" runat="server"><asp:Button ID="btnCancelChecked" runat="server" Text="Cancel Confirmation" /></div>--%>
                  <%--<div id="Div15" style="float:left; padding-left :20px;" runat ="server" visible="true" ><asp:Button ID="btnSchedule" runat="server" Text="DownLoad Schedule" /></div>--%>

                  <div style="float:left ;padding-left :30px; "><asp:ImageButton ID="imgDownloadSoft" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Download Soft Copy Schedule" Visible="true"/>
             </div>
                       

    </div>

             
              
              
          </div>


       <asp:Button id="btnShowPassportPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpEnrollmentPassport" runat="server" PopupControlID="pnlEnrollmentPassport" TargetControlID="btnShowPassportPopup" CancelControlID="btnmpEnrollmentPassportClose" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>     
                      
      
     <asp:Panel ID="pnlEnrollmentPassport" runat="server" CssClass="modalPopup" align="center" Height ="300px" style = "display:none" Width ="300px">

    <div id="Div2" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div3" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div4" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Passport Photo</strong></span></div>
            <div id="Div5" class="dvBoxbody">
               
           
       
                  <div id="dvPassport" style="float :left ; width :100%;  ">

                                          <div style="padding: 8px; margin: 0px; border-style: solid; border-width: thin; float: left; width: 80%; height: 200px; border-radius: 25px;">
                                               <div style="height : 180px"><asp:Image ID="imgPassport" runat="server" Width="90%" Height ="165px" ImageUrl="~/Images/untitled.png" /></div>
                                               <div style="float: left; width: 100%;"><span >Passort Photograph</span></div>
                                               
                                          </div>                                        

                                     </div>

            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnmpEnrollmentPassportClose" runat="server" Text="Close" />
    </asp:Panel>


      </ContentTemplate>
     </asp:UpdatePanel>


</asp:Content>

