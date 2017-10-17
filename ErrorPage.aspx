<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ErrorPage.aspx.vb" Inherits="ErrorPage" Theme="Blue"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class ="bodyMainDiv" style="background-color:white ;">
          <div style="float:left; padding: 20px;">
               <asp:Image ID="Image1" ImageUrl="~/images/cancel.png" runat="server" />
          </div>
          <div style="padding-top:30px; color:red;">
               <h2 > An Error Occurred. Please Contact the System Administrator or Send an E-mail to ithelpdesk@leadway-pensure.com</h2>
          </div>
 
          
          
     </div>
     
</asp:Content>

