<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" Theme ="Blue"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
		.logonError
            {
                background: url('images/ERROR_ICON.png') no-repeat left;
                border: 0px solid #ee9b9b;
                color: #f00;
                margin-right: 0px;
   
            }
        .logonAccessNotGranted
            {
                background: url('images/information.png') no-repeat left;
                border: 0px solid #ee9b9b;
                color: #f00;
                margin-right: 0px;
   
            }
        .logonWarning
            {
                background: url('images/warning.png') no-repeat left;
                border: 0px solid #ee9b9b;
                color: #f00;
                margin-right: 0px;
   
            }
        .logonSuccess
            {
                background: url('images/success.png') no-repeat left;
                border: 0px solid #ee9b9b;
                color: #f00;
                margin-right: 0px;
   
            }
	</style>

    </head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 200px 0px 0px 350px;">
        <div style ="overflow :auto ; width :740px">
            <div style ="width :140px; float :left ; height :128px;"><img alt="" src="images/logon_icon.png" style ="height :100%;"  /></div>
            <div style ="width :600px; padding-left : 138px;">
                <div id="Divlogon" style ="width:360px; text-align :left ; padding-left :20px; padding-bottom :10px;" runat ="server" ><span id="lblogonMessage" runat ="server" > Enter Your Login Details</span></div>
                <table border ="0" style ="height :40px;">
                       
                          <tr>
                            <td><div style ="width :150px;"><span >User Name :</span></div></td>
                            <td><div style ="width :200px;"><asp:TextBox ID="txtUserName" runat="server" Width ="180px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqUserName" runat="server" ErrorMessage="*" ControlToValidate="txtUserName" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" Font-Bold="True"></asp:RequiredFieldValidator>
                                </div></td>
                          </tr>
                          <tr>
                            <td><div style ="width :150px;"><span >Password :</span></div></td>
                            <td><div style ="width :200px;"><asp:TextBox ID="txtPassword" runat="server" Width ="180px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage="*" ControlToValidate="txtPassword" Display="Dynamic" Font-Bold="True" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                              </td>
                          </tr>
                     <%--     <tr>
                            <td><div style ="width :150px;">Select Fund</div></td>
                            <td><div style ="width :200px;"><asp:DropDownList ID="cbFund" runat="server" Width="180px">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem Value="RSA">RSA Fund</asp:ListItem>
                                <asp:ListItem Value="Retiree">Retiree Fund</asp:ListItem>
                                <asp:ListItem Value="WAPMC">WAPMC Fund </asp:ListItem>
                                <asp:ListItem Value="Guiness">Guiness Fund</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="cbFund" Display="Dynamic" Font-Bold="True" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div></td>
                          </tr>--%>
                          <tr>
                            <td><div style ="width :150px; text-align:left ; margin-top:25px;"><asp:ImageButton ID="btnlogon" runat ="server" ImageUrl="~/images/login_icon_128.png" AlternateText="Login" ToolTip="Login"  /></div></td>
                            <td><div style ="width :200px; text-align:right ;"><asp:ImageButton ID="btnCancel" runat ="server" ImageUrl="~/images/cancel.png" ToolTip="Cancel" ValidationGroup="Cancel"  /></div></td>
                          </tr>

            </table>

            </div>
        </div>
       
        <div></div>
    </div>
    </form>
</body>
</html>
