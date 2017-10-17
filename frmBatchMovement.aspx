<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmBatchMovement.aspx.vb" Inherits="frmBatchMovement" Theme ="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" Runat="Server">
     <%--<script src="Scripts/Validation.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="updcontent" runat ="server" >

        <ContentTemplate>
   <div id="dvSpace" style ="height :20px;"></div>
                  

             <div id="dvSideBox" style="float:left; width:320px; height :70px;" >


                <div id="Div5" style="border-color:#3a4f63; border :2px solid ;">

            <div id="Div1" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span></div>
            <div id="Div14" class="dvBoxbody">
               
                <div id="dvClient"  class ="dvBoxRows">

                    

                                    <div style="padding:5px; width:300px; ">
                                <table style ="margin-bottom :0px;">
                                    <tr>
                                        <td>
                                            <%--<asp:CheckBox ID="chkClient" Text ="Filter By Employer" runat="server" AutoPostBack="True" />--%>
                                           <span>Filter By Batch No :</span>
                                        </td>
                                        <td align="right" ><asp:TextBox ID="txtBatchRef" runat="server" Width ="130px" AutoPostBack="True" Enabled ="true" ></asp:TextBox></td>
                                    </tr>
                                </table>
                                                
                            </div>

                                 
                              

                </div>
                
                
          
                <div id="Div15" class ="dvBoxRows" >
                    <div style ="text-align :center ;"><asp:Button ID="btnViewTransaction" runat="server" Text="View Transactions" Visible ="true"  /></div>
                    
                </div>
            </div>

        </div>

        </div>


    
             <div style="float:left; width:1000px; padding-left :20px;" >

                 <div style ="width :-1px; text-align :right ;"><asp:ImageButton ID="btnSaveSummary" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel"    /></div>
                    <div id="dvFundMovementSummary" style =" border :2px solid ;">
                        <div id="Div2" style ="text-align:left ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Fund Movement Summary</strong></span> </div>
                <asp:Panel ID="pnlFundMovementSummary" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
             
                       <div id="dvCrBody" class="dvBoxbody">

                    <asp:GridView Width="100%" ID="gridMovementSummary" runat="server" Visible="true" PageSize="10" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridMovementSummary_RowDataBound">                       

                    <Columns>
                        <asp:ButtonField CommandName="Select" Text="Select"/>                                               

                         <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Enabled="true"  AutoPostBack="true" Width ="50" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                        <asp:BoundField DataField="HomeFund" HeaderText="Home Fund" Visible ="true" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="HomeUnit" HeaderText=" Total Home Unit" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="HomePrice" HeaderText="Total Home Price" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="HomeValue" HeaderText="Total Home Value" Visible ="true" DataFormatString="{0:N}" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="EndFund" HeaderText="End Fund" Visible ="true" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="EndUnit" HeaderText="Total End Unit" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="EndPrice" HeaderText="Total End Price" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="EndValue" HeaderText="Total End Value" Visible ="true" DataFormatString="{0:N}" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="BatchNo" HeaderText="" Visible ="true" ItemStyle-Width="0"/>
                        <%--<asp:BoundField DataField="txtPrice" HeaderText="Unit Price" Visible ="true" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="txtValue" HeaderText="Total Amount" Visible ="true" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="txtTotalAmount" HeaderText="Total Amount" Visible ="true" ItemStyle-Width="200"/>
                        <asp:ButtonField CommandName="Select" Text="Update Preferred Fund"/>--%>
                                          
                    </Columns>
            <pagersettings mode="NextPreviousFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"   
            position="Bottom"/> 


    </asp:GridView>


               
            
            </div>
                         
                </asp:Panel>
            </div>
                     <div id="Div4" class ="dvBoxRows" style ="margin-top : 15px;" >

                    <div style ="text-align :left; width :100%; float:left; text-align: right   ;">
                         <div style ="text-align :left; width :50%; float:left; text-align: left    ;">
                             
                              <asp:Button ID="btnTag" runat="server" Text ="Tag All " Visible ="false" />
                              <asp:Button ID="btnUnTag" Text ="Un Tag All " runat="server" Visible ="false"  />
                              <asp:Button ID="btnApprove" Text ="Send For Approval" runat="server" Visible ="false"   />
                              
                         </div>
                         <div style ="text-align :left; width :50%; float:right ; text-align: right   ;">
                              <asp:Button ID="btnGenerateSummary" runat="server" Text="Generate Batch Movement Summary" Visible ="true" style="height: 26px"   />
                         </div>
                         
                         

                    </div>
                   
                    
                </div>

                    <br />
                 <br />
                 

                    <div id="dvBottom">
                        <div id="Div3" style ="text-align:left ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Fund Movement Details</strong></span> </div>
                    
                     <div id="dvFundDefinition" style =" border :2px solid ;">
                <asp:Panel ID="pnlMovementDetail" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
                    <asp:GridView Width="100%" ID="gridMovementDetails" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridMovementSummary_RowDataBound">
                    <Columns>
                                               
                        <asp:BoundField DataField="PIN" HeaderText="PIN" Visible ="true" ItemStyle-Width="150"/>                                     
                        <asp:BoundField DataField="HomeFund" HeaderText="Home Fund" Visible ="true" ItemStyle-Width="70" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="HomeUnit" HeaderText=" Home Unit" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="HomePrice" HeaderText="Home Price" ItemStyle-Wrap ="false" ItemStyle-Width="50"/>
                        <asp:BoundField DataField="HomeValue" HeaderText="Home Value" DataFormatString="{0:N}" Visible ="true" ItemStyle-Width="180"/>
                        <asp:BoundField DataField="EndFund" HeaderText="End Fund" Visible ="true" ItemStyle-Width="70" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="EndUnit" HeaderText="End Unit" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="EndPrice" HeaderText="End Price" ItemStyle-Wrap ="false" ItemStyle-Width="50"/>
                        <asp:BoundField DataField="EndValue" HeaderText="End Value" DataFormatString="{0:N}" Visible ="true" ItemStyle-Width="180"/>
                        <%--<asp:TemplateField HeaderText="" ItemStyle-Width="50">
                                    <ItemTemplate>
                                    
                                    <asp:ImageButton OnClick="BtnViewDetails_Click" ID="btnEmailList" runat ="server" ImageUrl="~/images/edit (1).png" ToolTip="Update Preferred Fund" ItemStyle-Width ="10px" />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                        <%--<asp:BoundField DataField="txtPrice" HeaderText="Unit Price" Visible ="true" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="txtValue" HeaderText="Total Amount" Visible ="true" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="txtTotalAmount" HeaderText="Total Amount" Visible ="true" ItemStyle-Width="200"/>
                        <asp:ButtonField CommandName="Select" Text="Update Preferred Fund"/>--%>
                                          
                    </Columns>
            <pagersettings mode="NextPreviousFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"   
            position="Bottom"/> 


    </asp:GridView>

                   <%-- <div id="dvbtnExport" style="text-align :right;"><asp:Button ID="Button1" runat="server" Text="Generate Schedule For Enpower" /></div>--%>
                </asp:Panel>
                         
            </div>
                        <div style ="width :-1px; text-align :right; padding :5px;"><asp:ImageButton ID="btnSaveDetails" runat ="server" ImageUrl="~/images/xls.png" ToolTip="Export To File" ValidationGroup="Cancel" Visible ="false"     />
                         
                        </div>


                          <div style ="text-align :left; width :100%; float:left; text-align: right   ;">
                         <div style ="text-align :left; width :50%; float:left; text-align: left    ;">
                             
                              <asp:Button ID="btnBTag" runat="server" Text ="Tag All " />
                              <asp:Button ID="btnBUnTag" Text ="Un Tag All " runat="server"  />
                              <asp:Button ID="btnBApproval" Text ="Send For Approval" runat="server" Visible ="false"   />
                              
                         </div>
                         <div style ="text-align :left; width :50%; float:right ; text-align: right   ;">
                              <%--<asp:Button ID="Button4" runat="server" Text="Generate Batch Movement Summary" Visible ="true" style="height: 26px"   />--%>
                         </div>   
                      </div>


                    
                    </div>
</div>
                 <asp:Button id="btnShowPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpMailList" runat="server" PopupControlID="pnlMailList" TargetControlID="btnShowPopup" CancelControlID="btnMPMailList" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
    <asp:Panel ID="pnlMailList" runat="server" CssClass="modalPopup" align="center" style = "display:none" Height ="280px" Width ="420px">

    <div id="dvleftBox" class ="dvSideBox" style ="width:420px;"> 
        
        <div id="Div6" style="border-color:#3a4f63; border :2px solid ; width : 100%;">

            <div id="Div7" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Broker Mailing List</strong></span></div>
            <div id="Div8" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :100%;">
                  <div>
                   <div id="dvPIN" style ="float :left; width :20%;">PIN : </div>
                   <div style ="float :left; width :80%;"> <asp:TextBox ID="txtEmailAddress" runat ="server" Width ="100%"></asp:TextBox> </div>
                  </div>

                   <div>
                   <div id="Div9" style ="float :left; width :20%;">Full Name : </div>
                   <div style ="float :left; width :80%;"> <asp:TextBox ID="TextBox2" runat ="server" Width ="100%"></asp:TextBox> </div>
                  </div>

                   <div>
                   <div id="Div10" style ="float :left; width :20%;">Current Fund : </div>
                   <div style ="float :left; width :80%;"> <asp:TextBox ID="TextBox1" runat ="server" Width ="100%"></asp:TextBox> </div>
                  </div>

                   <div>
                   <div id="Div11" style ="float :left; width :20%;">Recommended Fund : </div>
                   <div style ="float :left; width :80%;"> <asp:TextBox ID="TextBox3" runat ="server" Width ="100%"></asp:TextBox> </div>
                  </div>

                   <div>
                   <div id="Div12" style ="float :left; width :20%;">Preferred Fund : </div>
                   <div style ="float :left; width :80%;"> <asp:DropDownList ID="DropDownList1" runat="server" Width ="100%"></asp:DropDownList></div>
                  </div>

                  <div>
                   <div id="Div13" style ="float :left; width :50%; text-align :right ;"><asp:Button ID="btnApply" runat="server" Text="Apply" /></div>
                   <div style ="float :left; width :50%;"> <asp:Button ID="btnCancel" runat="server" Text="Cancel" /> </div>
                  </div>

                </div>


            </div>
    
    </div>

        </div>
        <br />

    <asp:Button ID="btnMPMailList" runat="server" Text="Close" />
        </asp:Panel>


                </div>

        </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

