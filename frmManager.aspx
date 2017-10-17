<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmManager.aspx.vb" Inherits="frmManager" Theme ="Blue" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp"  %>

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
                <div id="dvtable" style ="margin-top :0px;">
                     
                     <table border="0" width="1000px" cellpadding="2px" cellspacing="2px">
                  <tr>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Total Benefit Application(s)</strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotalAppCount" runat ="server"  style ="color :#3a4f63; font-size :18px; margin-top:20px;" ><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top:20px;"><span id="lblPercentTotalApp" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style =" float :left ; margin-top:20px;">
                                 <div style="float :left ; padding-right :250px;padding-left :10px;text-align:left  ; height :40px; ">
                                 </div>
                                 
                                 
                                 

                            </div>
                        </div>
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>&nbsp;Total Sent to Pencom</strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotalAppSentCount" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><span id="lblPercentTotalAppSent" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"></div>
                        </div>
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Total Approved </strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotalAppApproved" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><span id="lblPercentTotalAppApproved" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"></div>
                        </div>
                    </td>

                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Total Paid </strong></span></div>
                            <div style ="text-align:center ;"><span id="lblTotalAppPaid" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"><span id="lblPercentTotalAppPaid" runat ="server"  style ="color :#3a4f63; font-size :30px;"><strong>0%</strong></span></div>
                            <div style ="text-align:center ; height :40px; margin-top :20px;"></div>
                        </div>
                    </td>
                    
                  </tr>

                </table>

                </div>

                    <div style ="width :-1px; text-align :right ; padding :5px;">

                    </div>

                <div id="dvValidatdEmail" style =" border :2px solid ;">


                     <table border="0" width="1000px" cellpadding="2px" cellspacing="2px">
                  <tr>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Total AVG. Age Application Per Source</strong></span></div>
                            <div style ="text-align:center ;">

                                 <asp:Panel ID="pnlValidatdEmail" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
                    <asp:GridView Width="100%" ID="gridApplicationAvgAge" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" AlternatingRowStyle-BackColor="#9999ff" AlternatingRowStyle-ForeColor ="#0000cc">
                    <Columns>
                                               
                        <asp:BoundField DataField="AppType" HeaderText="App. Type" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="AvgAge" HeaderText="Avg. Age" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                       
                       
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
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>&nbsp;Top 10 Aged Pending Applications</strong></span></div>
                            <div style ="text-align:center ;">

                                    <asp:Panel ID="Panel1" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
       
                                                       <asp:GridView Width="100%" ID="gridAgeApps" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridManagerBoard_RowDataBound">
                    <Columns>
                                               
                        <asp:BoundField DataField="txtPIN" HeaderText="PIN" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="txtStatus" HeaderText="Status" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                         <asp:BoundField DataField="AppType" HeaderText="App Type" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                         <asp:BoundField DataField="Age" HeaderText="Age" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                       
                       
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
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Benefit Applications Summary</strong></span></div>
                            <div style ="text-align:center ;">

                                    <asp:Panel ID="Panel2" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
                    <asp:GridView Width="100%" ID="gridRecievedApps" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" AlternatingRowStyle-BackColor="#eaeef5">
                    <Columns>
                                               
                        <asp:BoundField DataField="AppType" HeaderText="App. Type Code" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="AppTotal" HeaderText="App. Count" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                       
                       
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
                    </td>

                    
                    
                  </tr>

                </table>




            </div>


                        <div id="Div1" style =" border :2px solid ;">


                     <table border="0" width="1000px" cellpadding="2px" cellspacing="2px">
                  <tr>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Summary of All Paid Application</strong></span></div>
                            <div style ="text-align:center ;">

                                 <asp:Panel ID="Panel3" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >
                                      <asp:Chart ID="chartApplicationPaid" runat="server">
                                           <Titles >
                                                <asp:Title Text ="Total Paid Application Per Type"></asp:Title>
                                           </Titles>
                                           <Series>
                                                <asp:Series Name="SeriesAppPaid" ChartArea ="ChartAreaAppPaid"></asp:Series>
                                           </Series>
                                           <ChartAreas>
                                                <asp:ChartArea Name="ChartAreaAppPaid">

                                                     <AxisX Title ="Application Type"></AxisX>
                                                     <AxisY Title ="No of Application"></AxisY>

                                                </asp:ChartArea>
                                           </ChartAreas>
                                      </asp:Chart>
                </asp:Panel>


                            </div>
                            
                        </div>
                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>&nbsp;Pending Applications Per Location</strong></span></div>
                            <div style ="text-align:center ;">

                                    <asp:Panel ID="pnlLocationSummary" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px">

                                             <asp:GridView Width="100%" ID="gridLocationSummary" runat="server" Visible="true" PageSize="10" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" AlternatingRowStyle-BackColor="#fcb441" AlternatingRowStyle-ForeColor="Red">
                    <Columns>
                                               
                        <asp:BoundField DataField="AppType" HeaderText="Application Type" Visible ="true" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="txtApplicationOffice" HeaderText="Location" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="AppTotal" HeaderText="Application Count" ItemStyle-Wrap ="false" ItemStyle-Width="100"/>
                       
                       
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

                    </td>
                    <td style ="width :330px;">
                        <div>
                            <div style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; margin-bottom :20px;"><span style ="color :#dde4ec;"><strong>Benefit Applications Per Source </strong></span></div>
                            <div style ="text-align:center ;">

                                    <asp:Panel ID="Panel5" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height ="300px" >

                                         <asp:Chart ID="ChartAppSource" runat="server">
                                              <Titles >
                                                <asp:Title Text ="Total Application Recieved Per Source"></asp:Title>
                                              </Titles>
                                              <Series>
                                                   <asp:Series Name="SeriesAppSource" ChartArea ="ChartAreaAppSource" ChartType="Pie"></asp:Series>
                                              </Series>
                                              <ChartAreas>

                                                   <asp:ChartArea Name="ChartAreaAppSource">
                                                        <AxisX Title ="Application Source"></AxisX>
                                                        <AxisY Title ="No of Application"></AxisY>
                                                   </asp:ChartArea>

                                              </ChartAreas>

                                               <Legends>
                                                   <asp:Legend>
                                                   </asp:Legend>
                                               </Legends>

                                         </asp:Chart>

                </asp:Panel>

                            </div>
                           
                            
                        </div>
                    </td>

                    
                    
                  </tr>

                </table>




            </div>

                </div>


            </div>



              <asp:Button id="btnShowCommentPopup" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpAppComments" runat="server" PopupControlID="pnlAppComments" TargetControlID="btnShowCommentPopup" CancelControlID="btnMPAppComments" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            
                        <asp:Panel ID="pnlAppComments" runat="server" CssClass="modalPopup" align="center" Height ="600px" style = "display:none" Width ="600px">

    <div id="Div3" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div4" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div5" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
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

