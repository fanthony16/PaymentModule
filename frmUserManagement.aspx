<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmUserManagement.aspx.vb" Inherits="frmUserManagement" Theme="Blue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="updFormPanel" runat ="server" >
          <ContentTemplate>

     <div class =""><asp:Panel ID="pnlError" runat ="server" Visible="false">

          <asp:Image ID="imgError" runat="server" ImageUrl="~/images/ERROR_ICON.png" />
          <asp:Label ID ="lblError" runat ="server" Font-Bold="True" ForeColor="Red"></asp:Label></asp:Panel>

     </div>
          
     <div class ="bodyMainDiv" >
          <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span id="spCaption" runat ="server" >SurePay User Management</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv">
               
               <asp:Accordion ID="UserAccordion"  runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                <Panes>

                    <asp:AccordionPane ID="UserDetail"  runat="server">
                        <Header><a href="#" class="accordionhref">Users</a></Header>
                        <Content>
                            <asp:Panel ID="UserReg" runat="server">
                                    <div id="Div3" style="float :left ; width :99%; height :500px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div id="Div5" style="float :left ; width :99%; height :420px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">User List</span>
                                        
                                     </div>
                                     <div id="dvGridUser">
                                            <asp:Panel ID="pnlUploadDetail" Width ="100%" runat ="server" BorderStyle="Solid" Height ="410px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridUsers" runat="server" Visible="true" AllowPaging="True" PageSize="10" AutoGenerateColumns="False" OnRowDataBound ="gridUsers_RowDataBound">
                                                         <Columns>

                                                              <%--<asp:TemplateField HeaderText="">
                                                                 <ItemTemplate>
                                                                      <asp:CheckBox ID="chkSelect" runat="server" Enabled="true"  AutoPostBack="true" Width ="50" />
                                                                 </ItemTemplate>
                                                              </asp:TemplateField>--%>

                                                              <asp:BoundField DataField="UserID" HeaderText="User ID" />
                                                              <asp:BoundField DataField="FullName" HeaderText="Full Name"  />
                                                              <asp:BoundField DataField="RoleName" HeaderText="Role Name" HeaderStyle-Width="0" Visible ="true"  />
                                                              <asp:TemplateField HeaderText="" >
                                                                  <ItemTemplate>
                                    
                                                                      <%--<asp:ImageButton OnClick="DeactivateUser_Click" ID="btnDeactivateUser" runat ="server" ImageUrl="~/images/error.png" ToolTip="De-activate User" ItemStyle-Width ="10px" />--%>
                                                                       <asp:Button ID="btnActiveUser" OnClick="ActivateUser_Click" runat="server" Text ="Activate User" ToolTip ="Activate User" />
                                        
                                                                  </ItemTemplate>
                                                               </asp:TemplateField>

                                                              <asp:TemplateField HeaderText="">
                                                                  <ItemTemplate>
                                                                 <asp:Button ID="btnDeactiveUser" OnClick="DeactivateUser_Click" runat="server" Text ="De-activate User" ToolTip ="De-activate User" />
                                                                      <%--<asp:ImageButton OnClick="DeactivateUser_Click" ID="btnDeactivateUser" runat ="server" ImageUrl="~/images/error.png" ToolTip="De-activate User" ItemStyle-Width ="10px" />--%>
                                        
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
                                     </div>
                                     <div id="dvSearchFilter" style="margin-top :10px;">
                                          
                                          <div style="float:left ;"><span>Search Filter :</span></div>
                                          <div style="float:left ;"><asp:TextBox ID="txtSearchFilter" runat="server" ></asp:TextBox></div>
                                          <div style="float:left ;"><asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter"/></div>
                                          <div style="float:left ;width:200px; text-align:center ;"><asp:RadioButton ID="rdActive" runat="server" GroupName ="ViewFilter" TextAlign="Left" Text ="Active" Checked ="true"  /></div>
                                          <div style="float:left ;width:200px; text-align:center ;"><asp:RadioButton ID="rdInActive" runat="server" GroupName ="ViewFilter" TextAlign="Left" Text="In-Active" /></div>
                                          <div style="float:left ; width:200px; text-align:center ;"><asp:RadioButton ID="rdAll" runat="server" GroupName ="ViewFilter" TextAlign ="Left" Text ="All"  /></div>
                                     </div>
                                </div>
                                     

                                </div>
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="Modules"  runat="server">
                        <Header><a href="#" class="accordionhref">Application Modules</a></Header>
                        <Content>
                            <asp:Panel ID="Panel1" runat="server">

                               <div id="dvModuleList" style="float :left ; width :30%; height :450px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Application Modules List</span>

                                         

                                               <div id="Div2" class ="dvBoxRows" style="margin-top : 15px; margin-left :7px;">
                                               <asp:Panel ID="panel4" Width ="98%" runat ="server" BorderStyle="Solid" Height ="398px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridModule" runat="server" Visible="true" AllowPaging="True" PageSize="8" AutoGenerateColumns="False">
                                                         <Columns>
                                                              <asp:ButtonField CommandName="Select" Text="Select" ItemStyle-Width ="50px" ControlStyle-ForeColor ="Blue"  />
                                                              <asp:BoundField DataField="ModuleID" HeaderText="Module ID" />
                                                              <asp:BoundField DataField="ModuleDesc" HeaderText="Module Description"/>

                                                              <asp:TemplateField HeaderText="">     
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="DeleteRole_Click" ID="btnDeleteModule" runat ="server" ImageUrl="~/images/error.png" ToolTip="Delete Module" ItemStyle-Width ="10px" Enabled ="false" />
                                        
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
                                              
                                          </div>

                                         
                                         

                                     </div>
                                      
                                </div>


                               <div id="dvModuleRoleList" style="float :left ; width :22%; height :350px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Role List</span>

                                         

                                               <div id="Div12" class ="dvBoxRows" style="margin-top : 15px; margin-left :7px;">
                                               <asp:Panel ID="panelRoleList" Width ="98%" runat ="server" BorderStyle="Solid" Height ="290px" BorderWidth ="2px">
                                               
                                                  <asp:ListBox ID="lstRole" Width ="100%" runat="server" Height ="100%" AutoPostBack="True" CausesValidation="True"></asp:ListBox>

                                               </asp:Panel>
                                              
                                          </div>

                                         
                                         

                                     </div>
                                      
                                     
                                </div>

     <div id="dvAddRemoveModuleRole" style="float :left ; width :45%; height :350px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Add / Remove User From Role</span>

                                         
                                          <div id="Div4" style="margin-top :20px;">
            <div style ="float :left ; width : 43%; ">
                <asp:Panel ID="Panel5" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height="290px">
                <asp:ListBox ID="lstUnAssignedModules" Width ="100%" runat="server" Height ="100%" AutoPostBack="True" CausesValidation="True"></asp:ListBox>
                </asp:Panel>
            </div>
            <div style ="float :left ; width : 13%; text-align : center ;">
                <div id="Div6" style ="padding-bottom:45px; padding-top :20px;"><asp:ImageButton ID="imgAddAllModuleToRole" Enabled ="false" runat ="server" ImageUrl="~/images/gtk-media-forward-ltr.png" ToolTip="Add All Module To Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
                <div id="Div7" style ="padding-bottom:45px;"><asp:ImageButton ID="imgAddModuleToRole" runat ="server" ImageUrl="~/images/gtk-media-play-ltr.png" ToolTip="Add Module To Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
                <div id="Div8" style ="padding-bottom:45px;"><asp:ImageButton ID="imgRemoveModuleFromRole" runat ="server" ImageUrl="~/images/gtk-media-play-rtl.png" ToolTip="Remove Module From Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
                <div id="Div9" style ="padding-bottom:45px;"><asp:ImageButton ID="imgRemoveAllModuleFromRole" Enabled="false"  runat ="server" ImageUrl="~/images/gtk-media-forward-rtl.png" ToolTip="Remove All Module(s) From Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
            </div>
            <div style ="float :left ; width : 43%; border :2px solid ;">
                <asp:Panel ID="Panel6" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height="290px">
                <asp:ListBox ID="lstAssignedModules" Width ="100%" runat="server" Height ="100%"></asp:ListBox>
                </asp:Panel>
            </div>
        </div>


                                          

                                         
                                         

                                     </div>
                                      
                                     
                                </div>
                               
          
                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>                  
                    <asp:AccordionPane ID="Roles"  runat="server">
                        <Header><a href="#" class="accordionhref">User Roles</a> </Header>
                        <Content>
                            <asp:Panel ID="Panel3" runat="server">
                                
                               
                                <div id="dvBankDetails" style="float :left ; width :22%; height :350px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Role Creation</span>

                                          <div id="dvRoleDescription" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Description :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    <asp:TextBox ID="txtRoleDescription" runat="server" Width ="150px" TextMode ="MultiLine" MaxLength ="100" Height ="100px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqRoleDescription" runat ="server" ErrorMessage="*" controlToValidate="txtRoleDescription" Display="Dynamic" SetFocusOnError="True" ValidationGroup="SubmittingRole" Font-Bold="True" ForeColor="Red" ></asp:RequiredFieldValidator>
                                               </div>
                                          </div>

                                          <div id="dvReadOnly" class ="dvBoxRows" style="margin-top : 5px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Read Only :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:RadioButton ID="rdReadOnly" runat="server" GroupName="RoleAccessLevel" />
                                               </div>
                                          </div>

                                          <div id="dvReadWrite" class ="dvBoxRows" style="margin-top : 5px;">
                                               <div class="dvBoxRowsFieldLabel">
                                                    <span style ="font-size : medium;">Read / Write :</span>
                                               </div>
                                               <div style ="text-align :left ;">
                                                    
                                                    <asp:RadioButton ID="rdReadWrite" runat="server" GroupName="RoleAccessLevel" />
                                               </div>
                                          </div>

                                          <div id="dvADD" class ="dvBoxRows" style="margin-top : 15px;">
                                               <div style ="text-align :left ; padding-left:125px;">
                                                    <asp:Button ID="btnAdd" Text ="Add" runat="server" />    
                                               </div>
                                          </div>                                       

                                        
                                     </div>
                                     
                                </div> 
                                <div id="dvRoleList" style="float :left ; width :30%; height :500px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">User Roles List</span>

                                         

                                               <div id="dvGridRole" class ="dvBoxRows" style="margin-top : 15px; margin-left :7px;">
                                               <asp:Panel ID="panelRole" Width ="98%" runat ="server" BorderStyle="Solid" Height ="450px" BorderWidth ="2px">
                                                    <asp:GridView Width="100%" ID="gridRoles" runat="server" Visible="true" AllowPaging="True" PageSize="15" AutoGenerateColumns="False">
                                                         <Columns>
                                                              <asp:ButtonField CommandName="Select" Text="Select" ItemStyle-Width ="50px" ControlStyle-ForeColor ="Blue"  />
                                                              <asp:BoundField DataField="RoleID" HeaderText="RoleID" />
                                                              <asp:BoundField DataField="RoleDesc" HeaderText="Role Description"/>

                                                              <asp:TemplateField HeaderText="">     
                                                                  <ItemTemplate>
                                    
                                                                      <asp:ImageButton OnClick="DeleteRole_Click" ID="btnDeleteRole" runat ="server" ImageUrl="~/images/error.png" ToolTip="Delete Role" ItemStyle-Width ="10px" />
                                        
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
                                              
                                          </div>

                                         
                                         

                                     </div>
                                      
                                     
                                </div>
                                <div id="dvAddRemoveRoleUser" style="float :left ; width :45%; height :350px; border-style: solid; border-width: thin; border-radius: 25px; margin : 5px;">
                                     <div style="width :100%; border-radius :25px 25px 0px 0px; border-style: solid; border-width: thin;height : 30px;  background-color :#465c71; text-align:center; padding-top : 3px; font-size: 16px;">
                                          <span style="color:white;">Add / Remove User From Role</span>

                                         
                                          <div id="dvAllLocation" style="margin-top :20px;">
            <div style ="float :left ; width : 43%; ">
                <asp:Panel ID="pnlUnassigned" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height="290px">
                <asp:ListBox ID="lstUnAssigned" Width ="100%" runat="server" Height ="100%" AutoPostBack="True" CausesValidation="True"></asp:ListBox>
                </asp:Panel>
            </div>
            <div style ="float :left ; width : 13%; text-align : center ;">
                <div id="dvAddAll" style ="padding-bottom:45px; padding-top :20px;"><asp:ImageButton ID="imgAddAllUserToRole" Enabled="false"  runat ="server" ImageUrl="~/images/gtk-media-forward-ltr.png" ToolTip="Add All Users To Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
                <div id="dvAddSelected" style ="padding-bottom:45px;"><asp:ImageButton ID="imgAddUserToRole" runat ="server" ImageUrl="~/images/gtk-media-play-ltr.png" ToolTip="Add User To Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
                <div id="dvRemoveAll" style ="padding-bottom:45px;"><asp:ImageButton ID="imgRemoveUserFromRole" runat ="server" ImageUrl="~/images/gtk-media-play-rtl.png" ToolTip="Remove User From Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
                <div id="dvRemoveSelected" style ="padding-bottom:45px;"><asp:ImageButton ID="imgRemoveAllUserFromRole" Enabled="false"  runat ="server" ImageUrl="~/images/gtk-media-forward-rtl.png" ToolTip="Remove All Users From Role" CausesValidation="true" ValidationGroup  ="valEmail"  /></div>
            </div>
            <div style ="float :left ; width : 43%; border :2px solid ;">
                <asp:Panel ID="Panel2" Width ="100%" runat ="server" BorderStyle="Solid" BorderWidth ="2px" Height="290px">
                <asp:ListBox ID="lstAssigned" Width ="100%" runat="server" Height ="100%"></asp:ListBox>
                </asp:Panel>
            </div>
        </div>


                                          

                                         
                                         

                                     </div>
                                      
                                     
                                </div>

                            </asp:Panel>
                        </Content>
                    </asp:AccordionPane>


                </Panes>
            </asp:Accordion>

               <div id="dvValSummary">
                    <asp:ValidationSummary id="valSum" 
                             DisplayMode="BulletList"
                             EnableClientScript="true"
                             HeaderText="You must enter a value in the following fields:"
                             runat="server"/>
               </div>
               
          </div>
          
     </div>

     
    
     </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>

