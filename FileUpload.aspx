<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="FileUpload.aspx.vb" Inherits="FileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:FileUpload id="FileUploadControl" runat="server" AllowMultiple="true" />
    <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />
     <asp:Label ID="listofuploadedfiles" runat="server" /> 
    <asp:Button runat="server" id="UploadButton0" text="Upload" Visible ="false" />
    <asp:Button runat="server" id="UploadButton1" text="Upload" Visible ="false" />
    <br /><br />
    <asp:Label runat="server" id="StatusLabel" text="Upload status: " Visible ="false"  />
     <asp:DropDownList ID="ddBankName" runat="server" AutoPostBack="True" Visible ="false" >
     </asp:DropDownList>
     <asp:DropDownList ID="ddBankBranch" runat="server" AutoPostBack="True" Visible ="false" >
     </asp:DropDownList>
</asp:Content>

