<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="_Default2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="tel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>     
     <asp:Button ID="Button1" runat="server" Text="Button" />
     






            <%-- <asp:Panel ID="pnlGrid" Width ="100%" runat ="server" Height ="300px"  >
                            <asp:GridView Width="100%" ID="gridApprovalBatch" runat="server" Visible="true" PageSize="20" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging ="true" >
                                 
                                 <Columns >
                                      
                                        
                                       
                                        <asp:BoundField DataField="PriceDate" HeaderText="Price Date" ItemStyle-Width="200" DataFormatString="{0:d}"/> 
                                        <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" ItemStyle-Width="150" DataFormatString="{0:N}"/>
                                        
                                 </Columns>

                                        <pagersettings mode="NextPreviousFirstLast"
                                        firstpagetext="First"
                                        lastpagetext="Last"
                                        nextpagetext="Next"
                                        previouspagetext="Prev"   
                                        position="Bottom"/> 
                              </asp:GridView>
                        </asp:Panel>--%>




     <asp:Button ID="Button3" runat="server" Text="Button" />
     






       <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Height ="600px" Width ="600px">

    <div id="Div1" class ="dvSideBox" style="width :98%"> 
        
        <div id="Div2" style="border-color:#3a4f63; border :2px solid ; width :100%;">

            <div id="Div3" class ="dvBoxHeader"><span style ="color :#dde4ec;"><strong>Benefit Application Comment</strong></span></div>
            <div id="Div4" class="dvBoxbody">
               
               <div class="dvBoxRows" style =" width :300px;">
                   
               <div style="padding-top :5px; margin-bottom  :15px;">
                    <div style ="float :left "><span>Application ID :</span></div>
                    <div style ="float :left "><asp:TextBox ID="txtApplicationID" runat="server" Width ="150px" Enabled="false"></asp:TextBox></div>
               </div>
                   
                </div>
                
                <div class="dvBoxRows" style =" width :300px; padding-top :10px; ">
                    
                     <asp:TextBox id="TextBox2" runat ="server" TextMode ="MultiLine" ValidationGroup  ="AppComment" Height="80px" Width="100%" MaxLength="50"></asp:TextBox>
                </div>

                 <div id="Div5"  class="dvBoxRows" style =" width :300px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="ImageButton1" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>


                  <div class="dvBoxRows" style =" width :570px; padding-top :10px; ">
                    
                       <asp:ListBox ID="lstComments" runat="server" Width ="100%" Height ="300px"></asp:ListBox>
                </div>

                 <div id="Div6"  class="dvBoxRows" style =" width :560px; float :left  ;text-align :right ; padding :10px;">
                   <asp:ImageButton ID="ImageButton2" runat ="server" ImageUrl="~/images/add.png" ToolTip="Add To Comment" CausesValidation="true" ValidationGroup  ="AppComment"  />
                     
                    
                </div>
                 
            </div>
    
    </div>
    
    </div>
        
        <br />

    <asp:Button ID="Button2" runat="server" Text="Close" />
    </asp:Panel>

    
  

</asp:Content>

