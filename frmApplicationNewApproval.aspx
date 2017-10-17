<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmApplicationNewApproval.aspx.vb" Inherits="frmApplicationNewApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridRMAS" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" onrowediting="EditCustomer" onrowupdating="UpdateCustomer" onrowcancelingedit="CancelEdit" OnRowDataBound = "gridRMAS_OnRowDataBound"  >
                                 
                                 <Columns >
                                      <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                  <asp:CheckBox ID="ChkRMASApproval" runat="server" Enabled="true"  AutoPostBack="true"/>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                 </Columns>

                                        <pagersettings mode="NextPreviousFirstLast"
                                        firstpagetext="First"
                                        lastpagetext="Last"
                                        nextpagetext="Next"
                                        previouspagetext="Prev"   
                                        position="Bottom"/> 
                              </asp:GridView>
                        </asp:Panel>

</asp:Content>

