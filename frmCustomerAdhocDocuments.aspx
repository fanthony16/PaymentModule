<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmCustomerAdhocDocuments.aspx.vb" Inherits="frmCustomerAdhocDocuments" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>

               <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Adhoc Document List...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ; height : 530px;">
               
               <div id="dvSideBox" style="float:left; width:320px; height :300px;  padding :8px;" >

                    <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                         <div style ="text-align :right ; padding : 5px;" ><span >Enter PIN: </span></div>
                        <div id="dvPIN" style ="padding :5px; text-align :right ;" >
                             <asp:TextBox ID="txtPIN" runat ="server" Width ="270px" ValidationGroup="adhocDoc" ></asp:TextBox>
                             <asp:Button ID="btnFindDocuments" runat ="server" Text ="..." ToolTip ="View Recieved Adhoc Documents" />
                             <asp:RequiredFieldValidator ID="reqPIN" runat="server" ErrorMessage="*" ControlToValidate="txtPIN" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="adhocDoc"></asp:RequiredFieldValidator>

                        </div>



                        <div style ="text-align :right ; padding : 5px;" ><span >Select The Document Type: </span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :right ;" >
                             <asp:DropDownList ID="ddDocumentType" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="reqDocumentType" runat="server" ErrorMessage="*" ControlToValidate="ddDocumentType" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="processing"></asp:RequiredFieldValidator>

                        </div>


                        <div id ="dvUploadOtherDocuments" runat ="server" visible ="false">

                                              <div style ="text-align :right ; padding : 5px;" ><span >Add Description: </span></div>

                                               <div style ="text-align :left ;">

                                                    <asp:TextBox ID="txtAdhocDocDescription" runat ="server" Width="300px" ></asp:TextBox>
                                                    
                                               </div>

                                          </div>


                          <div id="dvApplicationDate " class ="dvBoxRows" style="margin-top : 15px;">
                                               <div style ="text-align :right ; padding : 5px;" ><span >Enter Recieved Date: </span></div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtDocRecievedDate" runat="server" Width="298px" Enabled ="TRUE" ></asp:TextBox><asp:RequiredFieldValidator ID="reqDocRecievedDate" runat ="server" ErrorMessage="*" controlToValidate="txtDocRecievedDate" Display="Dynamic" SetFocusOnError="True" Font-Bold="True" ForeColor="Red" ValidationGroup="SubmittingApplicatiom" ></asp:RequiredFieldValidator>
                                               </div>
                                               
                                                    <asp:PopupControlExtender ID="calDocRecievedDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDocRecievedDate" Position="Bottom" TargetControlID="txtDocRecievedDate"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlDocRecievedDate" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDocRecievedDate" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calDocRecievedDate" runat="server" BackColor="White" 
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


                         <div id="Div2" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div style ="text-align :right ; padding : 5px;" ><span >Enter Retirement Date: </span></div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtDORetirement" runat="server" Width="298px" Enabled ="TRUE" ></asp:TextBox>
                                               </div>
                                               
                                                    <asp:PopupControlExtender ID="PopupControlExtender_calDORetirement" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlDORetirement" Position="Bottom" TargetControlID="txtDORetirement"></asp:PopupControlExtender>
                                                    <asp:Panel ID="pnlDORetirement" runat="server">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                                                 <Triggers>

                                                                      <asp:AsyncPostBackTrigger ControlID="calDORetirement" />

                                                                 </Triggers>
                                                                 <ContentTemplate>
                                                                      <asp:Calendar ID="calDORetirement" runat="server" BackColor="White" 
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


                    <div id="dvUploadDocument" class ="dvBoxRows" style ="margin-top : 1px;">

                                               <div style ="text-align :right ; padding : 5px;" ><span >Select The Document From File: </span></div>

                                               <div style ="text-align :right ; padding-right :1px;">
                                                    <asp:AjaxFileUpload ID="flReqDocUpload" runat="server" OnUploadComplete="AjaxFileDocumentUploadEvent"  ValidationGroup ="valSchedulee" />
                                               </div>

                                                    
                                          </div>


                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnSaveDocument" runat="server" Text="Save Document" ValidationGroup="processing" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>



            
        </div>
               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; height :480px; width :1000px;" >

        <div id="dvtable" style ="margin-top :10px; height :440px;">
                <table border="0" width="100%"  cellpadding="2px" cellspacing="2px">
                  <tr>

                    <td style ="width :90%; border :none;  ">
                        <div style ="border : 2px solid  ;border-radius :25px 25px 0px 0px;">
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; border : 2px solid ; height :25px;margin-bottom :0px;border-radius :25px 25px 0px 0px;"><span style ="color :#dde4ec;"><strong>Documentation Details</strong></span></div>
                            <asp:Panel ID="pnlDocumentDetails" Width ="99%" runat ="server" Height ="400px" >
                                <asp:GridView Width="100%" ID="gridSubmittedDocuments" runat="server" Visible="true" AllowPaging="True" PageSize="30" AutoGenerateColumns="False" OnRowDataBound ="gridSubmittedDocuments_RowDataBound">
                                    <Columns>
                                       <asp:ButtonField CommandName="Select" Text="Select"/>
                                       <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                                       <asp:BoundField DataField="RecievedDate" HeaderText="Recieved Date" DataFormatString="{0:d}" />
                                       <asp:BoundField DataField="RetirementDate" HeaderText="Retirement Date" DataFormatString="{0:d}" />
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


        <div id="dvTag" style ="width :100%; padding : 5px; text-align :right ; height :20px; ">
             
             <div style="float:left ;"><asp:ImageButton ID="btnCancel" runat ="server" ImageUrl="~/images/remove.png" ToolTip="Cancel Application" Visible="true"/>

             </div>
             <div style="float:left ; padding-left :20px; ">
                  <%--<asp:Button ID="btnTagAll" runat="server" Text="Tag All" />--%>
                  <asp:ImageButton ID="btnTagAll" runat ="server" ImageUrl="~/images/success.png" ToolTip="Tag All" Visible ="false" />
             </div>
                          
             <div style="float:left; padding-left :10px;">
                  <%--<asp:Button ID="btnUnTagAll" runat="server" Text="Un-Tag All" />--%>
                  <asp:ImageButton ID="btnUnTagAll" runat ="server" ImageUrl="~/images/error.png" ToolTip="Un-Tag All" Visible ="false" />
             </div>
             
             <div id="dvErrorMessage" style="float:left; color :red ; padding-left :0px; width : 120px;" runat ="server" visible ="false"  ><span>Multiple Selection Not Allowed !!! </span></div>

        </div>
        <div id="dvPriceDate" style ="width :100%; padding: 10px; margin-bottom : 10px;">

             

             
             <%--<div style="float:left "><asp:DropDownList ID="ddApplicationStatusBatch" Width ="200px" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="reqddApplicationStatus" runat="server" ErrorMessage="*" ValidationGroup="ChangeStatus" ControlToValidate ="ddApplicationStatusBatch" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator></div>--%>
             
        </div>
        <div id="dvMainProcessButton" style ="width :100%; padding: 10px; margin-top : 10px; margin-bottom : 10px;">

             <%--<div style="float:left"><asp:Button ID="btnHardShipProcessingBatch" runat="server" Text="Change Application Status" ValidationGroup="ChangeStatus" /></div>--%>
        </div>


    </div>
              
          </div>
     </div>

          </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

