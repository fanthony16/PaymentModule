<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Progress2.aspx.vb" Inherits="Progress2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <%--   <script type="text/javascript">

        function onUpdating() {
            // get the divImage
            var panelProg = $get('divImage');
            // set it to visible
            panelProg.style.display = '';

            // hide label if visible     
            var lbl = $get('<%= Me.lblText.ClientID%>');
            lbl.innerHTML = '';
        }

        function onUpdated() {
            // get the divImage
            var panelProg = $get('divImage');
            // set it to invisible
            panelProg.style.display = 'none';
        }


    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>           
            <img alt="progress" src="images/loading.png"/>
               Processing...           
            </ProgressTemplate>
        </asp:UpdateProgress>
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="btnInvoke" runat="server" Text="Click" onclick="btnInvoke_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>        
    </div>

</asp:Content>

