<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationDashBoard.aspx.vb" Inherits="frmApplicationDashBoard" Theme ="Blue" %>

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
    

<%--    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="updcontent" runat="server">
            <ProgressTemplate>           
            <img alt="progress" src="images/loading.png"/>
               Processing...           
            </ProgressTemplate>
        </asp:UpdateProgress>--%>


    <%--<asp:UpdatePanel ID="updcontent" runat ="server" >
        <ContentTemplate>--%>
            <div style="float:left; width :1600px; ">
                <div id="dvSpace" style ="height :30px;"></div>

                <div id="dvSideBox" style="float:left; width:320px; height :300px;" >


                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px;" runat ="server" visible ="false"   >
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Email Validation</strong></span> </div>
            <div style ="text-align :right ; padding : 5px;" ><span >Enter The Nos Of Email to Validate: </span></div>
            <div id="divEmailAddy" style ="padding :5px; text-align :right ;" ><asp:TextBox ID="txtEmailCount" Width ="285px" runat ="server"></asp:TextBox><asp:RequiredFieldValidator ID="reqEmailCount" runat="server" ErrorMessage="*" ControlToValidate="txtEmailCount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EmailCount" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><asp:CompareValidator ID="compareEmailCount" runat="server" ErrorMessage="Please Enter Number Only" Display="Dynamic" ValidationGroup="EmailCount" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtEmailCount" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:CompareValidator></div>
            <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnValidate" runat="server" Text="Validate" ValidationGroup="EmailCount" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
        </div>



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

                <div style="float:left; width:1250px; padding-left :20px;" >
                <div id="dvtable" style ="margin-top :0px;"><table border="0" width="1000px" cellpadding="2px" cellspacing="2px">
                  <tr>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Total Benefit Application(s)</strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotalUndocumentedCount" runat ="server"  style ="color :#3a4f63; font-size :18px; margin-top:20px;" ><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top:20px;"><span id="lblPercentTotalUndocumented" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style =" float :left ; margin-top:20px;">
                                 <div style="float :left ; padding-right :250px;padding-left :10px;text-align:left  ; height :40px; ">
<asp:ImageButton ID="btnNewApplication" runat ="server" ImageUrl="~/images/add.png" ToolTip="Log New Application" ValidationGroup="new" Visible ="false"   />
                                 </div>
                                 <div style="float :left ; padding-right :0px;text-align:left  ; height :40px; ">
<asp:ImageButton ID="btnViewNewApplication" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View All Pending Logs" ValidationGroup="Cancel" Enabled ="true" />
                                 </div>
                                 
                                 
                                 

                            </div>
                        </div>
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>&nbsp;All Pending Application(s)</strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotaldocumentedCount" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><span id="lblPercentTotaldocumented" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><asp:ImageButton ID="btnViewdocumentedApplication" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Good Validated Email Addresses" ValidationGroup="Cancel" Enabled ="true" /></div>
                        </div>
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>In-Completed Documented Application(s)</strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotalProcessingCount" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><span id="lblPercentTotalProcessing" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><asp:ImageButton ID="btnViewProcessedApplication" runat ="server" ImageUrl="~/images/K view.png" ToolTip="View Bad Validated Email Addresses" ValidationGroup="Cancel"/></div>
                        </div>
                    </td>
                    
                  </tr>

                </table></div>

                    <div style ="width :-1px; text-align :right ; padding :5px;">

                         <asp:DropDownList ID="ddUsers" runat ="server" Visible ="true" Width ="300px" AutoPostBack="True" ></asp:DropDownList>
                         <asp:CheckBox ID="chkFilterByUser" Text ="Filter By User" runat="server" AutoPostBack="True" />
                         <asp:CheckBox ID="chkShowMyApplications" Text ="Show My Applications" runat="server" AutoPostBack="True" />
                         <asp:CheckBox ID="chkShowAll" Text ="Show All" Checked ="true"  runat="server" AutoPostBack="True" />
                         <asp:ImageButton ID="btnExport" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel"    />

                    </div>

                <div id="dvValidatdEmail" style =" border :2px solid ;">
                <asp:Panel ID="pnlValidatdEmail" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="500px" >
                    <asp:GridView Width="100%" ID="gridApplicationSummary" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridDashBoard_RowDataBound">
                    <Columns>
                                               
                        <asp:BoundField DataField="txtApplicationCode" HeaderText="Payment Code" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="txtFullName" HeaderText="Full Name" Visible ="true" ItemStyle-Width="500"/>
                        <asp:BoundField DataField="txtEmployerName" HeaderText="Employer Name" Visible ="true" ItemStyle-Width="500"/>
                        <asp:BoundField DataField="ApplicationTypes" HeaderText="Application Types" Visible ="true" ItemStyle-Width="500"/>
                        <asp:BoundField DataField="txtStatus" HeaderText="Application Status" Visible ="true" ItemStyle-Width="150"/>
                        <%--<asp:BoundField DataField="txtStatus" HeaderText="Retirement Date" Visible ="true" ItemStyle-Width="150"/>--%>
                        <asp:BoundField DataField="txtCreatedBy" HeaderText="Logged By" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="txtApplicationOffice" HeaderText="Location" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="dteApplicationDate" HeaderText="Logged Date" Visible ="true" ItemStyle-Width="150" DataFormatString="{0:d}"/>
                        <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" OnClientClick ="SetTarget();" />
                                         
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnCancelApplication_Click" ID="btnCancelApplication" runat ="server" ImageUrl="~/images/remove.png" ToolTip="Delete Application" OnClientClick="BtnCancelApplication_Click" ItemStyle-Width ="10px" />
                                        
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
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPAppComments" runat="server" Text="Close" />
    </asp:Panel>
      
             
             <asp:Button id="btnShowApplicationSummary" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpApplicationSummary" runat="server" PopupControlID="pnlAppSummary" TargetControlID="btnShowApplicationSummary" CancelControlID="btnCloseApplicationSummary" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
                  <asp:Panel ID="pnlAppSummary" runat="server" CssClass="modalPopup" align="center" Height ="700px" style = "display:none" Width ="600px">

    <div id="Div8" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div9" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div10" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Pending Benefit Application User Summary</strong></span></div>
            <div id="Div11" class="dvBoxbody">
                           

                        <asp:Panel ID="pnlUploadDetail" Width ="98%" runat ="server" BorderStyle="Solid" Height ="600px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridApplicationUserSummary" runat="server" Visible="true" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" OnRowDataBound ="gridApplicationSummary_RowDataBound">
                                                         <Columns>

                                                              <asp:BoundField DataField="ApplicationName" HeaderText="Application Name" />
                                                              <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                              <asp:BoundField DataField="ApplicationCount" HeaderText="Application Count" />
                                                              
                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>

                 <div id="dvAction">
                      <asp:ImageButton ID="imgUserSummary" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup ="AppSummary" />
                 </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnCloseApplicationSummary" runat="server" Text="Close" ValidationGroup ="AppSummary" />
    </asp:Panel>



        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

