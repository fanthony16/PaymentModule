<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmBatchApprovalList.aspx.vb" Inherits="frmBatchApprovalList" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     
      <div id="dvSpace" style ="height :20px;"></div>
    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>

          

     <div style="float:left; width:1200px; padding-left :110px;" >

                 <div style ="width :-1px; text-align :right ;"><asp:ImageButton ID="btnSaveSummary" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel"/></div>
                 

                    <div id="dvBottom">
                        <div id="Div3" style ="text-align:left ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Fund Movement - Batch Approval</strong></span> </div>
                    
                     <div id="dvFundDefinition" style =" border :2px solid ;">
                <asp:Panel ID="pnlMovementDetail" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
                    <asp:GridView Width="100%" ID="gridMovementDetails" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridMovementSummary_RowDataBound">
                    <Columns>
                        
                         <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Enabled="true"  AutoPostBack="true" Width ="50" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                        
                        <asp:BoundField DataField="BatchNo" HeaderText="Batch No" Visible ="true" ItemStyle-Width="300" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="HomeUnit" HeaderText="Total Movement Unit" ItemStyle-Wrap ="false" ItemStyle-Width="300"/>
                        <asp:BoundField DataField="HomeValue" HeaderText="Total Movement Value" DataFormatString="{0:N}" Visible ="true" ItemStyle-Width="300"/>

                        <asp:BoundField DataField="MovementDate" HeaderText="Movement Date" Visible ="true" DataFormatString="{0:d}" ItemStyle-Width="300"/>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="50">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton ID="btnEmailList" OnClick="btnEditView_Click" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="Update Preferred Fund" ItemStyle-Width ="10px" />
                                        
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
                        <div id="dvbtnExportExcel" style ="width :-1px; text-align :right; padding :5px;"><asp:ImageButton ID="btnSaveDetails" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel" Visible ="false"     />
                         
                        </div>
                        <div style ="text-align :left; width :100%; float:left; text-align: right   ;">
                         <div style ="text-align :left; width :50%; float:left; text-align: left    ;">
                             
                              <asp:Button ID="btnTagAll" runat="server" Text ="Tag All " />
                              <asp:Button ID="btnUnTagAll" Text ="Un Tag All " runat="server"  />
                              <asp:Button ID="btnApproval" Text ="Approval" runat="server"  />
                              
                         </div>
                         <div style ="text-align :left; width :50%; float:right ; text-align: right   ;">
                              <%--<asp:Button ID="Button4" runat="server" Text="Generate Batch Movement Summary" Visible ="true" style="height: 26px"   />--%>
                         </div>   
                      </div>

                    </div>



           




</div>
       <asp:Button id="btnShowPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpMailList" runat="server" PopupControlID="pnlMailList" TargetControlID="btnShowPopup" CancelControlID="btnMPMailList" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
    <asp:Panel ID="pnlMailList" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="320px" Width ="1020px">
    <div id="dvleftBox" class ="dvSideBox" style ="width:1000px;"> 
        
        <div id="Div2" style="border-color:#3a4f63; border :2px solid ; padding-left :15px;  width :100%; height: 300px;">

            <div id="Div4" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Fund Movement - Batch Detail</strong></span></div>
            <div id="dvCrBody" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :100%;">

              
                       <asp:GridView Width="100%" ID="gridMovementSummary" runat="server" Visible="true" PageSize="10" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridMovementDetails_RowDataBound">                       

                    <Columns>                                           

                        <asp:BoundField DataField="HomeFund" HeaderText="Home Fund" Visible ="true" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="HomeUnit" HeaderText=" Total Home Unit" ItemStyle-Wrap ="false" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="HomePrice" HeaderText="Total Home Price" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="HomeValue" HeaderText="Total Home Value" Visible ="true" DataFormatString="{0:N}" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="EndFund" HeaderText="End Fund" Visible ="true" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="EndUnit" HeaderText="Total End Unit" ItemStyle-Wrap ="false" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="EndPrice" HeaderText="Total End Price" ItemStyle-Wrap ="false" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="EndValue" HeaderText="Total End Value" Visible ="true" DataFormatString="{0:N}" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="BatchNo" HeaderText="" Visible ="true" ItemStyle-Width="0"/>
                                          
                    </Columns>
            <pagersettings mode="NextPreviousFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"   
            position="Bottom"/> 


    </asp:GridView>
              

              

              

              


              


              

              


                  
                                      
              
            </div>
    
    </div>
    
    </div>
        </div>
        <br />
             <asp:Button ID="btnMPMailList" runat="server" Text="Close" />
        </asp:Panel>

               </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

