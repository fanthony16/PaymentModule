<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="DMS.aspx.vb" Inherits="DMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

      <script type = "text/javascript">
           function SetTarget() {
                document.forms[0].target = "_blank";
           }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick ="SetTarget();" />
<asp:Button ID="Button2" runat="server" Text="Get Doc" />
     <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>

