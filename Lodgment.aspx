<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Lodgment.aspx.vb" Inherits="Lodgment" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     b<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="upd" runat="server">
            <ContentTemplate>
            
    <div style ="width:1400px">
    <div id="dvleftBox" class ="dvSideBox">

        <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ;">

            <div id="dvheader" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span></div>
            <div id="dvCrBody" class="dvBoxbody">
                <div id="dvStateDate" class ="dvBoxRows">
                                <div style ="float :left; width:80px"><span>Start Date :</span></div>
                                <div style="width:260px; text-align :right ;"><asp:TextBox ID="txtStartDate" runat="server" Width ="177px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator></div>
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
                                <div style="width:260px;text-align :right ; "><asp:TextBox ID="txtEndDate" runat="server" Width ="177px"></asp:TextBox>
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
                <div id="dvClient"  class ="dvBoxRows">

                    <asp:UpdatePanel ID="updCriteria" runat ="server" >
                                <ContentTemplate>

                                    <div style="padding:5px; width:300px; ">
                                <table style ="margin-bottom :0px;">
                                    <tr>
                                        <td>
                                            <%--<asp:CheckBox ID="chkClient" Text ="Filter By Employer" runat="server" AutoPostBack="True" />--%>
                                            <asp:RadioButton ID="rdClient" runat="server" GroupName="SearchType" Text ="Filter By Employer" AutoPostBack="True" />
                                        </td>
                                        <td align="right" ><asp:TextBox ID="txtClientName" runat="server" Width ="130px" AutoPostBack="True" Enabled ="false" ></asp:TextBox></td>
                                    </tr>
                                </table>
                                                
                            </div>

                                    <div style="padding:5px;">
                        
                                <div style="width:300px; "><asp:DropDownList ID="dcClients" runat="server" Width ="300px"></asp:DropDownList></div>
                            </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                </div>
                <div id="dvAmount" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        <%--<asp:CheckBox ID="chkAmount" Text="Filter By Amount" runat="server" />--%>
                        <asp:RadioButton ID="rdAmount" runat="server" GroupName="SearchType" Text ="Filter By Amount" />
                    </div>
                    <div style ="text-align :right ;"><asp:TextBox ID="txtAmount" Width ="150px" runat="server"></asp:TextBox></div>
                </div>
                <div id="dvlodgmentID" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        <%--<asp:CheckBox ID="chkID" Text="Filter By ID" runat="server" />--%>
                        <asp:RadioButton ID="rdID" runat="server" GroupName="SearchType" Text ="Filter By ID" />
                    </div>
                    <div style ="text-align :right ;"><asp:TextBox ID="txtLodgmentID" Width ="150px" runat="server"></asp:TextBox></div>
                </div>
                <div id="dvNarration" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        <%--<asp:CheckBox ID="chkDesc" Text="Filter By Narration" runat="server" />--%>
                        <asp:RadioButton ID="rdDesc" runat="server" GroupName="SearchType" Text ="Filter By Narration" />
                    </div>
                    <div style ="text-align :right ;"><asp:TextBox ID="txtDesc" Width ="150px" runat="server"></asp:TextBox></div>
                </div>
                <div id="Div1" class ="dvBoxRows" >
                    <div style ="text-align :center ;"><asp:Button ID="btnViewTransaction" runat="server" Text="View Transactions" Visible ="true"  /></div>
                    
                </div>
            </div>

        </div>
        <div id="dvActionBox" style="border-color:#3a4f63; border :2px solid ; margin-top :10px;" runat ="server" visible ="false"  >
            <div id="dvActHeader" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Action Box</strong></span></div>
            <div id="dvActBody" class ="dvBoxbody" style ="float :left;">
                
                <div id="dvImport" class ="dvActionButton"><asp:Button ID="btnImportLodgment" runat="server" Text="Import Lodgment" /></div>
                <div id="dvReciept" class ="dvActionButton"><asp:Button ID="btnPrintReciept" runat="server" Text="Print Reciept" /></div>
                <div id="dvCRUComment" class ="dvActionButton"><asp:Button ID="btnCRUComment" runat="server" Text="CRU's Comment" /></div>
                <div id="dvLodgmentUpdate" class ="dvActionButton"><asp:Button ID="btnSLodgmentUpdate" runat="server" Text="Update" /></div>
                <div id="Div14" class ="dvActionButton"><asp:Button ID="btnULodgmentUpdate" runat="server" Text="Update" /></div>
            </div>
        </div>

    </div>
    <div class="dvMiddleBox" >

        <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
                            <asp:GridView Width="100%" ID="gridContribution" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridContribution_RowDataBound" >
                            <Columns>
                                <%--
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="chKBooking" runat="server" Enabled="true"  AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:CheckBoxField DataField="" HeaderText="Select"  />--%>
                        
                                   <asp:ButtonField CommandName="Select" Text="Select"/>
                                   <asp:BoundField DataField="ID_JournalDetail" HeaderText="Lodgment ID" />
                                   <asp:BoundField DataField="Description" HeaderText="Description" />
                                   <asp:BoundField DataField="LocalAmount" HeaderText="Local Amount" DataFormatString="{0:N}" />
                                   <asp:BoundField DataField="ValueDate" HeaderText="Value Date" DataFormatString="{0:d}" />
                                   <%--<asp:BoundField DataField="Reversals" HeaderText="Reversal" DataFormatString="{0:N}"/>--%>
                                   <%--<asp:BoundField DataField="Refunds" HeaderText="Refund" DataFormatString="{0:N}" />--%>
                                   <%--<asp:BoundField DataField="balance" HeaderText="Balance" DataFormatString="{0:N}"/>--%>
                                   <%--<asp:BoundField DataField="Processed_Fund" HeaderText="Processed" DataFormatString="{0:N}" />--%>
                                   <%--<asp:BoundField DataField="Oustanding" HeaderText="Outstanding" DataFormatString="{0:N}" />--%>
                                   <%--<asp:BoundField DataField="ScheduleType" HeaderText="" ItemStyle-Width="0"/>--%>
                                   <%--<asp:BoundField DataField="CSV_Verification" HeaderText="" ItemStyle-Width="0" />--%>
                        
                        
                     
                  
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
                    <td style ="width :33%;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;margin-bottom :0px;"><span style ="color :#dde4ec;"><strong>Processed Details</strong></span></div>
                            <asp:Panel ID="pnlUploadDetail" Width ="99%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="100px" >
                                <asp:GridView Width="100%" ID="gridUploadDetails" runat="server" Visible="true" AllowPaging="True" PageSize="3" AutoGenerateColumns="False">
                                    <Columns>
                                       <asp:BoundField DataField="Uploaded Amount" HeaderText="Amount" />
                                       <asp:BoundField DataField="Upload Date" HeaderText="Date" DataFormatString="{0:d}" />
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
                    <td style ="width :33%;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :0px;"><span style ="color :#dde4ec;"><strong>Refund Details</strong></span></div>
                            <asp:Panel ID="pnlRefundDetails" Width ="99%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="100px" >
                                <asp:GridView Width="100%" ID="gridRefundDetail" runat="server" Visible="true" AllowPaging="True" PageSize="3" AutoGenerateColumns="False">
                                    <Columns>
                                       <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                       <asp:BoundField DataField="d_ate" HeaderText="Date" DataFormatString="{0:d}" />
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
                    </td >
                    <td style ="width :33%;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :0px;"><span style ="color :#dde4ec;"><strong>Reversal Details</strong></span></div>
                            <asp:Panel ID="pnlReversalDetails" Width ="99%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="100px" >
                                <asp:GridView Width="100%" ID="gridReversalDetail" runat="server" Visible="true" AllowPaging="True" PageSize="3" AutoGenerateColumns="False">
                                    <Columns>
                                       <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                       <asp:BoundField DataField="d_ate" HeaderText="Date" DataFormatString="{0:d}" />
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




    </div>
    <div class="dvLeftBox" >

        <asp:Panel ID="pnlLeft" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="475px" >
                            <asp:GridView Width="100%" ID="gridProperties" runat="server" Visible="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="false" AllowPaging ="false" ShowHeader ="false"  >
                          <Columns>
                                
                        
                                <asp:BoundField DataField="FieldName" HeaderText="Lodgment ID" Visible ="true" ItemStyle-Width="90"/>
                                <asp:BoundField DataField="FieldValue" HeaderText="Narration" ItemStyle-Wrap ="true" ItemStyle-Width="100" DataFormatString="{0}" />
                                
                    </Columns>
                   <%-- <pagersettings mode="NextPreviousFirstLast"
                    firstpagetext="First"
                    lastpagetext="Last"
                    nextpagetext="Next"
                    previouspagetext="Prev"   
                    position="Bottom"/> --%>


            </asp:GridView>
            <div id="dvActionSideBox" style ="width :100%; " runat ="server">
                

                <div id="dvLodgmentImport" runat ="server" style="padding : 5px;">
                   
                    <asp:Button ID="btnLodgmentImport" runat="server" Text="Import Lodgment" Width ="100%"/>

                </div>

                <div id="dvUploadUpdate" runat="server" style="padding : 5px;">
                   
                    <asp:Button ID="btnUploadUpdate" runat="server" Text="Update Upload" Width ="100%"/>

                </div>

                <div id="dvScheduleUpdate" runat="server" style="padding : 5px;">
                   
                    <asp:Button ID="btnScheduleUpdate" runat="server" Text="Update Schedule" Width ="100%"/>
                </div>

                <div id="dvCSVUpdate" runat="server" style="padding : 5px;">
                    
                    <asp:Button ID="btnCSVUpdate" runat="server" Text="Update CSV" Width ="100%"/>

                </div>

                <div id="dvCRUUpdate" runat="server" style ="padding : 5px;">
                    
                    <asp:Button ID="btnCRUUpdate" runat="server" Text="Update CRU's Comment" Width ="100%"/>
                    <%--<div style ="width :100%; float :left ; text-align :left;"><asp:ImageButton ID="btnAddNew" AlternateText ="Update CRU Comment"  runat ="server" ImageUrl="~/images/update.bmp" ToolTip="Update CRU Comment" CausesValidation="true"/></div>--%>
                </div>

                 <div id="dvViewSchedule" runat="server" style="padding : 5px;">
                    
                    <asp:Button ID="btnViewSchedule" runat="server" Text="View Schedule" Width ="100%"/>
                    <%--<div style ="width :100%; float :left ; text-align :left;"><asp:ImageButton ID="btnAddNew" AlternateText ="Update CRU Comment"  runat ="server" ImageUrl="~/images/update.bmp" ToolTip="Update CRU Comment" CausesValidation="true"/></div>--%>
                </div>


                



            </div>
</asp:Panel>

    </div>
    </div>  
    <asp:ModalPopupExtender ID="mpSchedule" runat="server" PopupControlID="pnlSchedule" TargetControlID="btnScheduleUpdate" CancelControlID="btnMPSchedule" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
    <asp:Panel ID="pnlSchedule" runat="server" CssClass="modalPopupSchedule" align="center" style = "display:none">
   
         <div id="Div6" class ="dvSideBox" style ="text-align :left;"> 
        
        <div id="Div7" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div8" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Schedule Update</strong></span></div>
            <div id="Div9" class="dvBoxbody">
                <div id="Div10" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:80px"><span>ID :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtSlodgmentID" runat="server" Width ="224px" Enabled ="false" ></asp:TextBox></div>

                </div>
                <div id="dvScheduleType" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:100px"><span>Schedule Type :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:DropDownList ID="dcScheduleType" runat="server" Width ="200px">
                                    <asp:ListItem Text ="--Select Type--" Value ="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text ="Soft Copy" Value ="0"></asp:ListItem>
                                    <asp:ListItem Text ="Hard Copy" Value ="1"></asp:ListItem>
                                    <asp:ListItem Text ="Scanned Copy" Value ="2"></asp:ListItem></asp:DropDownList></div>
                </div>
                <div id="Div11" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:70px"><span>Amount :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtSLAmount" runat="server" Width ="230px" Enabled ="false" ></asp:TextBox></div>
                </div>
                <div id="Div12" class ="dvBoxRows"  style =" width :300px"><asp:TextBox ID="txtSNarration" runat="server" Width ="100%"></asp:TextBox></div> 
                <div  class ="dvBoxRows" style =" width :300px">
                                <table style ="margin-bottom :0px; width : 100%">
                                    <tr>
                                        <td><asp:CheckBox ID="chkEmployer" Text ="Filter By Employer" runat="server" AutoPostBack="True" /></td>
                                        <td style ="text-align :right ;" ><asp:TextBox ID="txtEmployer" runat="server" Width ="150px" AutoPostBack="True" Enabled ="false" ></asp:TextBox></td>
                                    </tr>
                                </table>
                                                
                            </div>
                <div  class="dvBoxRows" style =" width :300px"><asp:DropDownList ID="dcSClientName" runat="server" Width ="300px" AutoPostBack="True"></asp:DropDownList></div> 

                <div id="dvSStateDate" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:90px"><span>Start Period :</span></div>
                                <div style="width:303px; text-align :right ;"><asp:TextBox ID="txtSStartDate" runat="server" Width ="180px" ValidationGroup ="valSchedule"></asp:TextBox><asp:RequiredFieldValidator ID="reqSStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valSchedule"></asp:RequiredFieldValidator></div>
                                <asp:PopupControlExtender ID="calSStartDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlSStartDate" Position="Bottom" TargetControlID="txtSStartDate"></asp:PopupControlExtender>
                                <asp:Panel ID="pnlSStartDate" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                        <asp:Calendar ID="CalSStartDate" runat="server" BackColor="White" 
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
                <div id="dvSEndDate" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:90px"><span>End Period :</span></div>
                                <div style="width:303px;text-align :right ; "><asp:TextBox ID="txtSEndDate" runat="server" Width ="180px" ValidationGroup ="valSchedule"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqSEndDate" runat="server" ControlToValidate="txtSEndDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valSchedule"></asp:RequiredFieldValidator>
                            
                                </div>

                                        <asp:PopupControlExtender ID="calSEndDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlSEndDate" Position="Bottom" TargetControlID="txtSEndDate"></asp:PopupControlExtender>
                        <asp:Panel ID="pnlSEndDate" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="CalSEndDate" runat="server" BackColor="White" 
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
                <div id="dvScheduleButton"  class="dvBoxRows" style =" width :300px; float :left ;">
                    <div style ="width :150px; float :left ; text-align :left ;"><asp:ImageButton ID="btnSPRemove" runat ="server" ImageUrl="~/images/remove.jpg" ToolTip="Remove From Period List" CausesValidation="False" /></div>
                    <div style ="width :150px; float :left ; text-align :right  ;"><asp:ImageButton ID="btnSPAdd"  runat ="server" ImageUrl="~/images/add.jpg" ToolTip="Add To Period List" CausesValidation="False" /></div>
                    <div style ="width :150px; text-align :right ;"></div>
                </div>
                <div class="dvBoxRows" style =" width :300px;">
                    <asp:ListBox ID="lstSchedule" runat="server" Width ="100%"></asp:ListBox>
                </div>

                <div id="dvScheduleDate" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:98px"><span>Schedule Date :</span></div>
                                <div style="width:302px;text-align :right ; "><asp:TextBox ID="txtScheduleDate" runat="server" Width ="180px" ValidationGroup ="valSchedule"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqScheduleDate" runat="server" ControlToValidate="txtScheduleDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valSchedule"></asp:RequiredFieldValidator>
                            
                                </div>

                                        <asp:PopupControlExtender ID="calScheduleDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlScheduleDate" Position="Bottom" TargetControlID="txtScheduleDate"></asp:PopupControlExtender>
                        <asp:Panel ID="pnlScheduleDate" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="calScheduleDate" runat="server" BackColor="White" 
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

                 <div id="dvSPartCount" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:80px"><span>PIN Count :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtSPartCount" runat="server" Width ="224px"></asp:TextBox></div>

                </div>

                 <div id="dvSLocation" class ="dvBoxRows" style =" width :300px">
                                
                                <div style ="float :left; width:80px"><span>Location :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtSLocation" runat="server" Width ="224px"></asp:TextBox></div>

                </div>

                 <div id="dvSComments" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:80px"><span>Comments :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtSComments" runat="server" Width ="224px"></asp:TextBox></div>

                </div>
                <div id="Div13" class ="dvBoxRows" style =" width :300px"><asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" OnUploadComplete="AjaxFileUploadEvent"  ValidationGroup ="valSchedule" /></div>
                <div class="dvBoxRows"style =" width :300px; text-align :center ;"><asp:Button ID="btnSUpdateComment" runat="server" Text="Update Comment" ValidationGroup ="valSchedule" /></div>
        

            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPSchedule" runat="server" Text="Close" />
    </asp:Panel>


    <asp:ModalPopupExtender ID="mpCRUComment" runat="server" PopupControlID="pnlCRUComment" TargetControlID="btnCRUUpdate" CancelControlID="btnMPCRUComment" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
    <%--<asp:ModalPopupExtender ID="mpMailList" runat="server" PopupControlID="pnlMailList" TargetControlID="btnShowPopup" CancelControlID="btnMPMailList" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>--%>
    <asp:Panel ID="pnlCRUComment" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="230px">
        

    <div id="Div2" class ="dvSideBox"> 
        
        <div id="Div3" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div4" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>CRU's Comment</strong></span></div>
            <div id="Div5" class="dvBoxbody" style ="height :100%;">
               
               
               
                <div class="dvBoxRows" style =" width :300px;">
                    
                    <asp:TextBox ID="txtCRUComments"  runat="server" TextMode="MultiLine" Width ="100%" Height ="150px"></asp:TextBox>
                </div>

                <div class="dvBoxRows" style =" width :300px; height :100%;">
                    
                    <asp:Button ID="btnUpdateCRUComment" runat="server" Text="Update" />
                </div>

            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnMPCRUComment" runat="server" Text="Close" />
    </asp:Panel>

    <asp:ModalPopupExtender ID="mpUploadUpdate" runat="server" PopupControlID="pnlUploadUpdate" TargetControlID="btnUploadUpdate" CancelControlID="btnMPCRUComment" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
    <asp:Panel ID="pnlUploadUpdate" runat="server" CssClass="modalPopupSchedule" align="center" style = "display:none" >
   
         <div id="Div15" class ="dvSideBox" style ="text-align :left;"> 
        
        <div id="Div16" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div17" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Contribution Update</strong></span></div>
            <div id="Div18" class="dvBoxbody">

                <div id="Div19" class ="dvBoxRows" style =" width :300px; padding-bottom : 25px;">

                                <div style ="float :left; width:50%"><asp:RadioButton ID="rdUNormal" Text ="Normal Contribution" runat="server" Checked ="true" GroupName ="CheckUpdateType" CausesValidation="False"  AutoPostBack="True" /></div>
                                <div style ="float :right; text-align :right ; width:50%"><asp:RadioButton ID="rdUPenalty" Text="Penalty" runat="server" TextAlign ="Left" GroupName ="CheckUpdateType" AutoPostBack="True" /></div>
                                
                </div>

                <div id="dvUID" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:80px"><span>ID :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtULodgmentID" runat="server" Width ="224px" Enabled ="false" ></asp:TextBox></div>

                </div>
               
                <div id="dvUAmount" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:70px"><span>Amount :</span></div>
                                <div style="width:307px; text-align :right ;"><asp:TextBox ID="txtUAmount" runat="server" Width ="225px" Enabled ="false" ></asp:TextBox></div>
                </div>
                <div id="dvDescription" class ="dvBoxRows"  style =" width :300px"><asp:TextBox ID="txtUDescription" runat="server" Width ="100%" Enabled ="false" ></asp:TextBox></div> 
                <div  class ="dvBoxRows" style =" width :300px">
                                <table style ="margin-bottom :0px; width : 100%">
                                    <tr>
                                        <td><asp:CheckBox ID="chkUFilterEmployer" Text ="Filter By Employer" runat="server" AutoPostBack="True" /></td>
                                        <td style ="text-align :right ;" ><asp:TextBox ID="txtUEmployerFilter" runat="server" Width ="150px" Enabled ="false" AutoPostBack="True" ></asp:TextBox></td>
                                    </tr>
                                </table>
                                                
                            </div>
                <div  class="dvBoxRows" style =" width :300px"><asp:DropDownList ID="dcUClientName" runat="server" Width ="300px" AutoPostBack="True"></asp:DropDownList></div> 
                  <asp:RequiredFieldValidator ID="reqUClientName" runat="server" ErrorMessage="*" ControlToValidate="dcUClientName" Display="Dynamic" ValidationGroup ="valUUpdate" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <div id="dvBalance" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:120px"><span>Balance :</span></div>
                                <div style="width:303px; text-align :right ;"><asp:TextBox ID="txtUBalance" Enabled ="false"  runat="server" Width ="180px" ValidationGroup ="valSchedule"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valSchedule"></asp:RequiredFieldValidator>--%>

                                </div>
                            </div>

                <div id="dvProcessedDate" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:98px"><span>Process Date :</span></div>
                                <div style="width:302px;text-align :right ; "><asp:TextBox ID="txtUProcessDate" runat="server" Width ="180px" ValidationGroup ="valUUpdate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqProcessDate" runat="server" ControlToValidate="txtUprocessDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valUUpdate"></asp:RequiredFieldValidator>
                            
                                </div>

                        <asp:PopupControlExtender ID="calProcessDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlProcessDate" Position="Bottom" TargetControlID="txtUProcessDate"></asp:PopupControlExtender>
                        <asp:Panel ID="pnlProcessDate" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="calProcessDate" runat="server" BackColor="White" 
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

                <div id="dvProcessed" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:120px"><span>Processed :</span>
                                    <div style ="float :right "><asp:RadioButton ID="rdProcessed" GroupName ="UpdateType" runat="server" AutoPostBack="True" /></div>
                                </div>
                                <div style="width:303px; text-align :right ;"><asp:TextBox ID="txtUProcessed" runat="server" Width ="180px" ValidationGroup ="valUUpdate" AutoPostBack="True"></asp:TextBox>
                                    <asp:CompareValidator ID="comUProcessed" ControlToValidate="txtUProcessed" runat="server" ErrorMessage="*" Display="Dynamic" Type="Double" ValidationGroup ="valUUpdate"></asp:CompareValidator>
                                    <%--<asp:RequiredFieldValidator ID="reqSStartDate" runat="server" ControlToValidate="txtsStartDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valSchedule"></asp:RequiredFieldValidator>--%>


                                </div>
                            </div>

                <div id="dvReversed" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:120px"><span>Reversed :</span>
                                    <div style ="float :right "><asp:RadioButton ID="rdReversed" GroupName ="UpdateType" runat="server" AutoPostBack="True" /></div>
                                </div>
                                <div style="width:303px;text-align :right ; "><asp:TextBox ID="txtUReversed" runat="server" Enabled ="false"  Width ="180px" ValidationGroup ="valUUpdate" AutoPostBack="True"></asp:TextBox>
                                    <asp:CompareValidator ID="comUReversed" ControlToValidate="txtUReversed" runat="server" ErrorMessage="*" Display="Dynamic" Type="Double" ValidationGroup ="valUUpdate"></asp:CompareValidator>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUReversed" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup ="valSchedule"></asp:RequiredFieldValidator>--%>
                            
                                </div>

                                       
                            </div>


                 


                 <div id="dvUpdateRemarks" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:120px"><span>Update Remarks :</span></div>
                                <div style="width:303px; text-align :right ;"><asp:TextBox ID="txtUUpdateRemarks" runat="server" Width ="180px" ValidationGroup ="valUUpdate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqUUpdateRemarks" runat="server" ErrorMessage="*" ControlToValidate="txtUUpdateRemarks" Display="Dynamic" ValidationGroup ="valUUpdate" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                <div id="dvOutstanding" class ="dvBoxRows" style =" width :300px">
                                <div style ="float :left; width:120px"><span>Outstanding :</span></div>
                                <div style="width:303px; text-align :right ;"><asp:TextBox ID="txtUOutstanding" runat="server" Width ="180px" Enabled ="false"  ValidationGroup ="valUUpdate"></asp:TextBox>
                                

                                </div>
                            </div>

              

                <div id="dvUCommentBox" class="dvBoxRows" style =" width :300px; padding-bottom : 10px;" runat ="server" >
                    <%--<asp:ListBox ID="ListBox1" runat="server" Width ="100%"></asp:ListBox>--%>
                    <asp:TextBox ID="txtUUploadComment" runat="server" Width ="100%" TextMode ="MultiLine" ></asp:TextBox>
                </div>

                <div id="dvUpdateOption" class ="dvBoxRows" style =" width :300px;padding-bottom : 15px;">
                                <div style ="float :left; width:50%"><asp:RadioButton ID="rdUUpload" Text ="Contribution Update" runat="server" Checked ="true" GroupName ="updateType2" AutoPostBack="True"  /></div>
                                <div style ="float :right; text-align :right ; width:50%"><asp:RadioButton ID="rdUComment" Text="Comment Update" runat="server" TextAlign ="Left" GroupName ="updateType2" AutoPostBack="True" /></div>
                                
                </div>
               
                <div id="Div25" class ="dvBoxRows" style =" width :300px"></div>
                <div class="dvBoxRows"style =" width :300px; text-align :center ;"><asp:Button ID="btnUUpdateComment" runat="server"  Text="Update Comment" ValidationGroup ="valUUpdate" /></div>
        

            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="Button2" runat="server" Text="Close" />
    </asp:Panel>

        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

