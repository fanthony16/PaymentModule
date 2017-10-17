<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmPreference.aspx.vb" Inherits="frmPreference" Theme="Blue"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePartialRendering="true" ></asp:ToolkitScriptManager>

     <div class ="bodyMainDiv" >
           <div id="dvMainDvTitle" style ="padding-left :20px;"><h2><span>SurePay General Settings...</span></h2></div>
          <div id="dvSubbodyMainDiv" class ="SubbodyMainDiv" style="text-align:center ; float :left ;">

               <div class="dvMiddleBox" style="border-radius :25px 25px 0px 0px; border :2px solid; margin-top :10px; padding  :5px 10px 0px 10px; " >


                         <div style=" width :100%; padding : 0px; border-color:#3a4f63; border :2px solid ; margin-bottom :20px; border-radius :25px 25px 0px 0px;">
                        <div id="Div1" style ="text-align:center ; background-color:#3a4f63; font-size :14px; height :25px; border-radius :25px 25px 0px 0px;">
                             <span style ="color :#dde4ec;"><strong>Search Criteria</strong></span> 
                        </div>
                        
                        <div style ="text-align :left  ; padding : 5px;" ><asp:CheckBox ID="chkFileSystem" runat="server" TextAlign ="Right" Text ="Drop Application Document on FileSystem" /></div>
                              <div style ="text-align :left  ; padding : 5px;" ><span >Please Specify Documents Folder Location</span></div>
                        <div id="dvApprovalType" style ="padding :5px; text-align :left  ;" >
                             <asp:textbox ID="ddApprovalType" runat="server" Width ="300px" ValidationGroup="processing" AutoPostBack="True" Text ="\\P-MIDAS2\NPM_Data\ApplicationDocuments\" Enabled ="false" ></asp:textbox>
                             

                        </div>


                              <div style ="text-align :left  ; padding : 5px;" ><asp:CheckBox ID="chkDMS" runat="server" TextAlign ="Right" Text ="Drop Application Documents on Document Management System" /></div>
                              <div style ="text-align :left  ; padding : 5px;" ><span >Please Specify DMS Object Store</span></div>
                        <div id="Div2" style ="padding :5px; text-align :left  ;" >
                             <asp:textbox ID="txtObjectStore" runat="server" Width ="300px" Text ="LPPFA" ValidationGroup="processing" AutoPostBack="True" Enabled ="false" ></asp:textbox>
                             

                        </div>

                        <div style ="text-align :right ; padding :5px;"><asp:Button ID="btnFind" runat="server" Text="Update" ValidationGroup="processing" /></div>
            <asp:Panel ID="pnlMessage" runat ="server" Visible="False"><div style ="padding:5PX;"><span id="spnMessage" runat ="server" >.</span></div></asp:Panel>
            
            
                  </div>



               </div>

          </div>
     </div>
</asp:Content>

