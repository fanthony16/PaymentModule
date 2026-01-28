<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmFundReturns.aspx.vb" Inherits="frmFundReturns" Theme ="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
  

     <asp:GridView Width="100%" ID="gridApplicationSummary" runat="server" Visible="true" PageSize="30" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" OnRowDataBound ="gridDashBoard_RowDataBound" >
                    <Columns>
                                               
                        <asp:BoundField DataField="FundName" HeaderText="FundName" Visible ="true" ItemStyle-Width="250"/>
                        <asp:BoundField DataField="Valuedate" HeaderText="Value Date" Visible ="true" ItemStyle-Width="150" DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="OpeningUnitPrice" HeaderText="Opening Unit Price" ItemStyle-Wrap ="false" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="CurUnitPrice" HeaderText="Current Unit Price" ItemStyle-Wrap ="false" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="YearToDate" HeaderText="Year To Date Returns(%)" Visible ="true" ItemStyle-Width="100"/>
                        <asp:BoundField DataField="Annualized" HeaderText="Annualized Returns(%)" Visible ="true" ItemStyle-Width="100"/>
                        
                    </Columns>
            <pagersettings mode="NextPreviousFirstLast"
            firstpagetext="First"
            lastpagetext="Last"
            nextpagetext="Next"
            previouspagetext="Prev"   
            position="Bottom"/> 


    </asp:GridView>
     

     <div id="dvSideBox" style="float:left; width:320px; text-align:left ;" >
     <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ;">
                <div id="dvCrHeader" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px;"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> </div>
                <div id="dvCrBody" style="height:auto; padding:5px;">
                    <div style="padding:5px;">
                        <div style ="float :left; width:80px"><span>Value Date :</span></div>
                        <div style="width:260px;"><asp:TextBox ID="txtValueDate" runat="server" Width ="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValueDate" runat="server" ControlToValidate="txtValueDate" Display="Dynamic" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                <asp:PopupControlExtender ID="calSDate_PopupControlExtender" runat="server" Enabled="True" ExtenderControlID="" PopupControlID="pnlStartDate" Position="Bottom" TargetControlID="txtValueDate"></asp:PopupControlExtender>
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
                    
                    <div style="padding:0px;">
                       
                        <div style ="text-align:center;"><asp:Button ID="btnGenerate" runat="server" Text="View Results" /></div>

                    </div>
                     <%--<div style ="text-align:center ; height :40px; margin-top:20px; color :red ;"> <span id="spApplicationCount" runat ="server"  style ="color :#3a4f63; font-size :18px;"><strong>0</strong></span> Record(s) Retrieved !</div>--%>
                    
                </div>
            </div>
       </div>
     
</asp:Content>

