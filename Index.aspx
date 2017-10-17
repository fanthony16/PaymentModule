<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Index.aspx.vb" Inherits="Index" Theme="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>


      <asp:UpdatePanel ID="updFormPanel" runat="server">
          <ContentTemplate>
                 <div style ="height : 350px; margin-top : 50px;padding-top :50px;text-align :center ;">
          <img alt="" src="images/background.jpg" style="  height :100%; "/>
     </div>


               <asp:Button id="btnShowApplicationSummary" runat="server" style="display:none" />
       <asp:ModalPopupExtender ID="mpApplicationSummary" runat="server" PopupControlID="pnlAppSummary" TargetControlID="btnShowApplicationSummary" CancelControlID="btnCloseApplicationSummary" BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>            

                  <asp:Panel ID="pnlAppSummary" runat="server" CssClass="modalPopup" align="center" Height ="450px" style = "display:none" Width ="600px">

    <div id="Div8" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div9" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div10" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Summary - Awaiting Review </strong></span></div>
            <div id="Div11" class="dvBoxbody">

                        <asp:Panel ID="pnlUploadDetail" Width ="98%" runat ="server" BorderStyle="Solid" Height ="400px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridApplicationSummary" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False" OnRowDataBound ="gridApplicationSummary_RowDataBound">
                                                         <Columns>

                                                              <asp:BoundField DataField="txtStatus" HeaderText="Application Type" />
                                                              <asp:BoundField DataField="ApplicationCount" HeaderText="Application Count" />
                                                              
                                                             <%-- <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="UpdatePrice_Click" ID="btnUpdatePrice" runat ="server" ImageUrl="~/images/K view.png" ToolTip="Update Enpower with Price" OnClientClick="UpdatePrice_Click" ItemStyle-Width ="5px" />
                                        
                                                                  </ItemTemplate>

                                                               </asp:TemplateField>--%>
                                                              
                                                         </Columns>

                                                    </asp:GridView>
                                               </asp:Panel>
                
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="btnCloseApplicationSummary" runat="server" Text="Close" />
    </asp:Panel>


          </ContentTemplate>

      </asp:UpdatePanel>

</asp:Content>

