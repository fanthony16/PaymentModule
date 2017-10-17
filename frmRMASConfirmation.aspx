<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmRMASConfirmation.aspx.vb" Inherits="frmRMASConfirmation" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <div class ="bodyMainDiv">
          <div id="dvleftBox" class ="dvSideBox" style ="margin  :20px; padding :10px; background-color :white ; border-radius :20px;">
               <div id="dvCriteria" style="border-color:#3a4f63; border :2px solid ; border-radius :20px;">

            <div id="dvheader" class ="dvBoxHeader" style="border-radius :20px 20px 0px 0px;"><span style ="color :#dde4ec;"><strong>Search Criteria</strong></span></div>
            <div id="dvCrBody" class="dvBoxbody">
             
                <div id="dvFund" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        <%--<asp:CheckBox ID="chkAmount" Text="Filter By Amount" runat="server" />--%>
                        <span>Report Code</span>
                    </div>
                    <div style ="text-align :right ;">
                         <asp:DropDownList ID="DropDownList1" runat="server" Width="190px"></asp:DropDownList>
                    </div>
                </div>
                
                <div id="dvApplicationType" class ="dvBoxRows" >
                    <div style ="float :left; padding :0px 5px 0px 0px;">
                        <%--<asp:CheckBox ID="chkAmount" Text="Filter By Amount" runat="server" />--%>
                        <span>Report Date</span>
                    </div>
                    <div style ="text-align :right ;">
                         <asp:TextBox ID="TextBox1" runat="server" Width="182px"></asp:TextBox>
                    </div>
                </div>
                
                <div id="dvViewTransaction" class ="dvBoxRows" >
                    <div style ="text-align :center ;"><asp:Button ID="btnViewTransaction" runat="server" Text="View Application" Visible ="true"  /></div>   
                </div>
            </div>

        </div>
              

          </div>
          <div style ="width :700px; float :left;"><h2 id="HDer" runat ="server">RMAS Details Comfirmation Stage</h2></div>
          <div id="dvRightBox" style="float:left; width:1030px; padding :13px; margin  :21px 0px 21px 0px; background-color :white ; border-radius :20px;" >
               

               <div id="dvData" style =" border :2px solid ;">
                        <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height="300px" >
                            
                        </asp:Panel>
               </div>
               <div style="margin  :10px 0px 0px 0px;">
                    <div style="float:left; padding :10px "><asp:Button ID="btnTagAll" runat="server" Text="Tag All" /></div>
                    <div style="float:left; padding :10px "><asp:Button ID="btnUnTagAll" runat="server" Text="Un Tag All" /></div>
                    <div style="float:left; padding :10px"><asp:Button ID="btnReject" runat="server" Text="Reject To Processing" /></div>
                    <div style="float:left; padding :10px "><asp:Button ID="btnAccept" runat="server" Text="Approved For RMAS Submission" /></div>

               </div>
          </div>
     </div>
</asp:Content>

