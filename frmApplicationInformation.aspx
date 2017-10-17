<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationInformation.aspx.vb" Inherits="frmApplicationInformation" Theme="Blue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="upd" runat="server">
            <ContentTemplate>
            
    <div style ="width:1400px">
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
                        
                        <asp:RadioButton ID="rdApprovalTypes" runat="server" GroupName="SearchType" Text ="Approval Types" AutoPostBack="True" />
                    </div>
                    <div style ="text-align :right ;"><asp:DropDownList ID="ddApprovalType" runat="server" Width ="175px" Enabled="False"></asp:DropDownList></div>
                </div>
                <div id="dvlodgmentID" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        
                        <asp:RadioButton ID="rdPIN" runat="server" GroupName="SearchType" Text ="Filter By PIN" AutoPostBack="True" />
                    </div>
                    <div style ="text-align :right ;"><asp:TextBox ID="txtPIN" Width ="185px" runat="server" Enabled="False"></asp:TextBox></div>
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
    <div class="dvMiddleBox" >

        <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="500px" >
                            <asp:GridView Width="100%" ID="gridApplication" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridApplication_RowDataBound" >
                            <Columns>
                                                        
                                        <%--<asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="chkApplication" runat="server" Enabled="true"  AutoPostBack="true"/>
                                        </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:ButtonField CommandName="Select" Text="Select"/>
                                        <asp:BoundField DataField="txtApplicationCode" HeaderText="Application Code" ItemStyle-Width="70" />
                                        <asp:BoundField DataField="Name" HeaderText="Full Name" ItemStyle-Width="200"/> 
                                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" ItemStyle-Width="100"/>
                                        <asp:BoundField DataField="txtEmployerName" HeaderText="Employer" ItemStyle-Width="250" />
                                        <asp:BoundField DataField="ApprovalType" HeaderText="Approval Type" ItemStyle-Width="150" />
                                 <%--<asp:BoundField DataField="txtStatus" HeaderText="Status" ItemStyle-Width="100" />--%>
                                        <asp:TemplateField HeaderText="">
                                                   <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnViewDetails" runat ="server" ImageUrl="~/images/K view.png" ToolTip="Add/View Comment(s)" OnClientClick="BtnViewDetails_Click" ItemStyle-Width ="10px" />
                                        
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
    </div>  

    


        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

