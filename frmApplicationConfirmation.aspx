<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationConfirmation.aspx.vb" Inherits="frmApplicationConfirmation" Theme="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>Application Successfully Submitted. Application Code :</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
               <div style="float:left ; text-align:center ;font-size :medium; padding-left:20px;">
                    <h2 style="color:red ;font-style :italic "><span id="spApplicationID" runat="server">XXXXX</span></h2>
               </div>
               <div>
                    

                    <asp:ImageButton ID="imgBack" runat="server" ImageUrl="~/images/redbackbutton.png" ToolTip ="Back To DashBoard" />

               </div>
               
          </div>
          <div class ="SubbodyMainDiv" style="text-align:center ; float :left ;">
                    <div style ="float :left ; padding-right :20px;"><asp:Button ID ="btnPrintReciept" runat ="server" Text ="Print Acknowledgment" /></div>
                    <div style ="float :left ;"><asp:Button ID ="btnEmail" runat ="server" Text ="E-mail Acknowledgment" /></div>

               <div id="dvEmailStatus" style ="float :left ; padding-left :50px; color :red;" runat ="server" visible ="false" ><span id="spEmailError" runat="server">Invalid Email Address</span></div>

               <%--<div>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/frmApplicationList.aspx">Back To Application</asp:HyperLink>
               </div>--%>

          </div>
     </div>
</asp:Content>

