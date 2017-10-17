<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmSIChecker2.aspx.vb" Inherits="frmSIChecker2" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
    

    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="updcontent" runat="server">
            <ProgressTemplate>           
            <img alt="progress" src="images/loading.png"/>
               Processing...           
            </ProgressTemplate>
        </asp:UpdateProgress>


    <asp:UpdatePanel ID="updcontent" runat ="server" >
        <ContentTemplate>
            <div style="float:left; width :1600px; ">
                <div id="dvSpace" style ="height :30px;"></div>

                <div id="dvSideBox" style="float:left; width:320px; height :300px;" >


                    <div id="Div1" style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px;" runat ="server" visible ="false"   >
                        <div id="Div2" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Email Validation</strong></span> </div>
                        

            <div style ="text-align :right ; padding : 5px;" ><span >Enter The Nos Of Email to Validate: </span></div>
            <div id="divEmailAddy" style ="padding :5px; text-align :right ;" ><asp:TextBox ID="txtEmailCount" Width ="285px" runat ="server"></asp:TextBox><asp:RequiredFieldValidator ID="reqEmailCount" runat="server" ErrorMessage="*" ControlToValidate="txtEmailCount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="EmailCount" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><asp:CompareValidator ID="compareEmailCount" runat="server" ErrorMessage="Please Enter Number Only" Display="Dynamic" ValidationGroup="EmailCount" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtEmailCount" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:CompareValidator></div>
            <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnValidate" runat="server" Text="Validate" ValidationGroup="EmailCount" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
        </div>



      <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div10" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Generate Standing Instruction(s)</strong></span> 
                        </div>
                        

              <%--  <div id="dvStateDate" class ="dvBoxRows">
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

                            </div>--%>


                          <div id="Div11" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>Run Type :</span></div>
                                <div style="text-align :right ; "><asp:DropDownList ID="ddRunType" runat="server" Width ="200px" ValidationGroup="FindType"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqddRunType" runat="server" ErrorMessage="*" ControlToValidate="ddRunType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindType"></asp:RequiredFieldValidator>
                         </div>
</div> 

                          <div id="dvMonthRunFor" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>Ann. Month:</span></div>
                                <div style="text-align :right ; "><asp:DropDownList ID="ddAnnMonth" runat="server" Width ="200px" ValidationGroup="SI"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqddAnnMonth" runat="server" ErrorMessage="*" ControlToValidate="ddAnnMonth" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="SI"></asp:RequiredFieldValidator>
                         </div>
</div> 

                          <div id="Div12" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>Ann. Year:</span></div>
                                <div style="text-align :right ; "><asp:DropDownList ID="ddAnnYear" runat="server" Width ="200px" ValidationGroup="SI"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqddAnnYear" runat="server" ErrorMessage="*" ControlToValidate="ddAnnYear" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="SI"></asp:RequiredFieldValidator>
                         </div>
</div> 

                       <%--   <div id="dvAppStatus" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>App. Stage :</span></div>
                                <div style="text-align :right ; "><asp:DropDownList ID="ddApplicationStatus" runat="server" Width ="200px" ValidationGroup="FindType"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="reqddApplicationStatus" runat="server" ErrorMessage="*" ControlToValidate="ddApplicationStatus" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="FindType"></asp:RequiredFieldValidator>
                         </div>
</div> --%>

                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnGenerate" runat="server" Text="Generate" ValidationGroup="FindType" /></div>
            <asp:Panel ID="Panel1" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="Span1" runat ="server" >.</span></div></asp:Panel>
            
                  </div>




        </div>

                <div style="float:left; width:1250px; padding-left :20px;" >

                    <%--<div style ="width :-1px; text-align :right ; padding :5px;">

                         <asp:DropDownList ID="ddUsers" runat ="server" Visible ="true" Width ="300px" AutoPostBack="True" ></asp:DropDownList>
                         <asp:CheckBox ID="chkFilterByUser" Text ="Filter By User" runat="server" AutoPostBack="True" />
                         <asp:CheckBox ID="chkShowMyApplications" Text ="Show My Applications" runat="server" AutoPostBack="True" />
                         <asp:CheckBox ID="chkShowAll" Text ="Show All" Checked ="true"  runat="server" AutoPostBack="True" />
                         <asp:ImageButton ID="btnExport" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel"    />

                    </div>--%>

                <div id="dvValidatdEmail" style =" border :2px solid ;">
                    <asp:Panel ID="pnlValidatdEmail" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="500px" >
                    <asp:GridView Width="100%" ID="gridStandingOrder" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridSIRun_RowDataBound">
                    <Columns>

                        <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkPensionerChecked" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="pkiSIApproval" HeaderText="Standing Order ID" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>                       
                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="txtFullName" HeaderText="Full Name" Visible ="true" ItemStyle-Width="250"/>
                        <asp:BoundField DataField="numPension" HeaderText="Pension Amount" DataFormatString="{0:N}" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="txtBankAccount" HeaderText="Account Number" Visible ="true" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="BankName" HeaderText="Bank Name" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="BankBranch" HeaderText="Bank Branch" Visible ="true" ItemStyle-Width="150"/>
                        
                        
                        <%--<asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewApplicationLog" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="View Application" ItemStyle-Width ="10px" />
                                        
                                    </ItemTemplate>
                        </asp:TemplateField>--%>

                         <%--<asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="AddViewComment_Click" ID="btnAddViewComment" runat ="server" ImageUrl="~/images/comment_bubble2.png" ToolTip="Add/View Comment(s)" OnClientClick="AddViewComment_Click" ItemStyle-Width ="10px" />
                                        
                                                                  </ItemTemplate>
                                                                   
                        </asp:TemplateField>--%>


                          <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnViewSI_Click" ID="btnViewSI" runat ="server" ImageUrl="~/images/folder-open.png" ToolTip="View Standing Instruction" OnClientClick="BtnViewSI_Click" ItemStyle-Width ="10px" />
                                        
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
                     <div><hr /></div>
                <div style =" border :2px solid; height :20px; padding :20px;">
                     <div style="float:left ; padding-right :20px; "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
                     <div style="float:left; padding-right :0px;"><asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" /></div>
                     <div id="Div3" style="float:left; padding-left :20px;" runat ="server" visible="true" ><asp:Button ID="btnConfirmCheck" runat="server" Text="Confirm Check" /></div>
                     <div id="dvSPRowCount" style="float:right; padding-left :20px;" runat ="server" visible="false" ><span id="spRowCount" runat ="server" style ="color :red ; font-weight :200 ;"></span></div>
                </div>

                </div>


            </div>



              <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
                        <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div4" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div5" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div6" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
            <div id="Div7" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="txtApplicationComment" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="80px" Width="100%" MaxLength="70"></asp:TextBox>
                </div>

                 <div id="Div8"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentAdd" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div9"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="btnAppCommentRemove" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Remove Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
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

