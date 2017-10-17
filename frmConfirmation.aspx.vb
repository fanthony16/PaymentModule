Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Partial Class frmConfirmation
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable

     Protected Sub getApplicationForProcessing(typeID As Integer)
          Dim cr As New Core, dt As New DataTable

          Try

			dt = cr.PMgetApplicationByTpye(typeID, "Confirmation")
			If dt.Rows.Count > 10 Then
				Me.pnlGrid.Height = Nothing
			Else

			End If
			'pnlGrid

               Me.gridProcessing.DataSource = dt
               gridProcessing.DataBind()
          Catch ex As Exception

          End Try

     End Sub

     'handles the click event of the comment button on the grid
     Protected Sub AddViewComment_Click(sender As Object, e As EventArgs) Handles btnShowCommentPopup.Click

          Dim btnAddViewComment As New ImageButton, dt As DataTable, j As Integer
          btnAddViewComment = sender
          Dim i As GridViewRow, cr As New Core

          i = btnAddViewComment.NamingContainer
          Me.txtApplicationID.Text = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text
          'logging comments for pre approval benefit application

          dt = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop

          'pops up the comment dialogue
          mpAppComments.Show()



     End Sub

     Private Sub DownLoadDocument(path As String)

          If Not File.Exists(path) = False Then

               'If CStr(ViewState("schedulePath")).ToString = "" Then
               '     ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
               'Else
               'End If

               Dim schedulePath As String = path
               Try

                    Dim str() As String = schedulePath.Split("|")
                    Dim FI As FileInfo, fileExt As String, i As Integer = 0

                    Do While i < str.Length

                         FI = New FileInfo(str(i).Trim.ToString)
                         fileExt = LCase(FI.Extension)
                         'MsgBox("" & fileExt)
                         Select Case fileExt

                              Case Is = ".xls"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".xlsx"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Is = ".csv"
                                   'Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Is = ".pdf"
                                   'Process.Start("ACRORD32", str(i).Trim.ToString)

                                   Response.ContentType = "application/pdf"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()


                              Case Is = ".doc"
                                   ' Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".docx"

                                   'Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".jpg"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".png"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Else
                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                         End Select
                         i = i + 1
                    Loop
               Catch ex As Exception
                    '   MsgBox("" & ex.Message)
               End Try

          Else
               ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "File Not Found", True)
          End If


     End Sub

     Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
          btnViewDocumentLog = sender
          Dim i As GridViewRow
          i = btnViewDocumentLog.NamingContainer
          '       appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(0).Text

          If Not IsNothing(ViewState("Documents")) = True Then

               Dim dt As DataTable = ViewState("Documents")
               'retrieving the location of the scanned document
               documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()

               'testing if the file still exist in the saved file path
               If File.Exists(documentPath) = True Then

                    DownLoadDocument(documentPath)

               ElseIf File.Exists(documentPath) = False Then

                    '            DownLoadDocument(documentPath)

			End If



			''''dms integration addition'''''''''''''''''''''''''''''''''''''''''''''''''''''''''


			Dim dtDocs As New DataTable, dmsDocumentID As String, dmsDocumentExt As String
			If IsNothing(ViewState("Documents")) = False Then

				dtDocs = ViewState("Documents")
				dmsDocumentID = dtDocs.Rows(i.RowIndex).Item("DocumentID")
				dmsDocumentExt = dtDocs.Rows(i.RowIndex).Item("DocumentExtension")

				Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
				Dim uName As String, uPWD As String, uRI As String

				uName = ConfigurationManager.AppSettings("FileNetUName")
				uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
				uRI = ConfigurationManager.AppSettings("FileNetURI")

				dms.getConnection(uName, uPWD, uRI)
				DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
				DownLoadDocument(DMSDocumentPath)

			Else
			End If


			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''








          Else

          End If


     End Sub


     Public Function getApprovalType() As List(Of String)

          Dim lstAppTypes As New List(Of String)
          Dim dc As New AppDocumentsDataContext
          Dim query = From m In dc.tblApplicationTypes
                      Select m

          For Each a As tblApplicationType In query
               lstAppTypes.Add(a.txtDescription)
               ApprovalTypeCollection.Add(a.txtDescription, a.pkiAppTypeId)
          Next
          ViewState("ApprovalTypeCollection") = ApprovalTypeCollection
          Return lstAppTypes

     End Function

     Protected Sub getApprovalTypes()

          Dim i As Integer = 0
          Dim lstAppTypes As New List(Of String)
          lstAppTypes = getApprovalType()
          ddApprovalType.Items.Clear()
          Do While i < lstAppTypes.Count

               If ddApprovalType.Items.Count = 0 Then
                    ddApprovalType.Items.Add("")
                    ddApprovalType.Items.Add(lstAppTypes.Item(i))
               ElseIf ddApprovalType.Items.Count > 0 Then
                    ddApprovalType.Items.Add(lstAppTypes.Item(i))
               End If
               i = i + 1

          Loop

     End Sub

     Protected Sub PopulateApplicationStatus()

     End Sub
     Public Sub getUserAccessMenu(uName As String)

          Dim cr As New Core
          Dim dtAccessModule As New DataTable
          Dim aryAccessModule As New ArrayList
          Dim i As Integer, j As Integer, k As Integer
          dtAccessModule = cr.getAccessModule(Session("User"))

          Do While i < dtAccessModule.Rows.Count
             
               aryAccessModule.Add(dtAccessModule.Rows(i).Item(1))
               i = i + 1
          Loop
          i = 0
          j = 0
          k = 0
          Dim M As New System.Web.UI.WebControls.Menu
          Dim n As New System.Web.UI.WebControls.MenuItem
          M = Master.FindControl("NavigationMenu")

          Do While i < M.Items.Count


               ''''main menu''''
               If aryAccessModule.Contains(M.Items(i).Value) = False Then

                    M.Items.RemoveAt(i)

               Else
                    ''''sub menu''''
                    Do While j < M.Items(i).ChildItems.Count

                         If aryAccessModule.Contains(M.Items(i).ChildItems.Item(j).Value) = False Then
                              M.Items(i).ChildItems.RemoveAt(j)
                         Else

                              ''''sub---sub menu''''
                              Do While k < M.Items(i).ChildItems(j).ChildItems.Count

                                   If aryAccessModule.Contains(M.Items(i).ChildItems(j).ChildItems.Item(k).Value) = False Then
                                        M.Items(i).ChildItems(j).ChildItems.RemoveAt(k)
                                   Else
                                        k = k + 1

                                   End If

                              Loop

                              j = j + 1
                         End If

                    Loop
                    i = i + 1
               End If


          Loop

          If aryAccessModule.Count = 0 Then
               Response.Redirect("default.aspx")
          Else
          End If

     End Sub



     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          Dim scriptManagerA As New ScriptManager, dtusers As New DataTable, scriptManagerb As New ScriptManager
          scriptManagerA = ScriptManager.GetCurrent(Me.Page)
          scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

          scriptManagerb = ScriptManager.GetCurrent(Me.Page)
          scriptManagerb.RegisterPostBackControl(Me.btnSNR)


          Try

               If IsPostBack = False Then


                    If IsNothing(Session("user")) = True Then

                         '   getApprovalType()
                         Response.Redirect("Login.aspx")
                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					Dim cr As New Core
                         dtusers = Session("userDetails")
                         getUserAccessMenu(Session("user"))
                         getApprovalTypes()
                         PopulateApplicationStatus()
					getApplicationForProcessing(0)


					Me.gridApplicationSummary.DataSource = cr.PMgetApplicationSummaryByStage("Confirmation")
					gridApplicationSummary.DataBind()
					mpApplicationSummary.Show()

                    End If

			Else
				getUserAccessMenu(Session("user"))
               End If

          Catch ex As Exception

          End Try

     End Sub
     Protected Sub populateDocuments(dt As DataTable)
          Try
               ViewState("Documents") = dt
               Me.gridSubmittedDocuments.DataSource = dt
               Me.gridSubmittedDocuments.DataBind()

               If dt.Rows.Count > 0 Then

                    Me.pnlDocumentDetails.Height = Nothing
               Else
                    Me.pnlDocumentDetails.Height = 100
               End If

          Catch ex As Exception

          End Try

     End Sub
     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

          populateApplicationList()

     End Sub

     Private Sub populateApplicationList()
          Dim dt As New DataTable
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
          refresh()
          populateDocuments(dt)
     End Sub


     Protected Sub refresh()
          ViewState("ApplicationCode") = Nothing
          Dim nw As New List(Of ApplicationProperties)
          populateProperties(nw)

     End Sub
     Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          'If IsNothing(ViewState("Documents")) = False Then

          '     Dim dt As DataTable = ViewState("Documents")
          '     If e.Row.RowType = DataControlRowType.DataRow Then

          '          If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

          '               e.Row.ForeColor = System.Drawing.Color.Red

          '          ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" Then

          '               e.Row.ForeColor = System.Drawing.Color.Green

          '          End If

          '     End If
          'Else
          'End If


          If IsNothing(ViewState("Documents")) = False Then

               Dim dt As DataTable = ViewState("Documents")
               If e.Row.RowType = DataControlRowType.DataRow Then

                    If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

                         e.Row.ForeColor = System.Drawing.Color.Red
                         e.Row.Enabled = False

                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "True" Then
                         e.Row.ForeColor = System.Drawing.Color.Green
                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then
                         e.Row.ForeColor = System.Drawing.Color.Blue
					e.Row.Enabled = True
                    End If

               End If
          Else
          End If



     End Sub
     Protected Sub populateProperties(lodgmentProperties As List(Of ApplicationProperties))
          Try


               Me.gridProperties.DataSource = lodgmentProperties
               Me.gridProperties.DataBind()

               If lodgmentProperties.Count > 0 Then
                    pnlLeftGrid.Height = Nothing
               Else
                    pnlLeftGrid.Height = 475
               End If


          Catch ex As Exception

          End Try

     End Sub


     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewApplicationLog As New ImageButton, appCode As String
          btnViewApplicationLog = sender
          Dim i As GridViewRow
          i = btnViewApplicationLog.NamingContainer
          appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

          Dim typeID As Integer
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          'typeID = (CInt(ApprovalTypeCollection.Item(Me.gridProcessing.Rows(i.RowIndex).Cells(4).Text)))
          typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

          Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1, Server.UrlEncode("frmConfirmation")))


          '  Response.Redirect(String.Format("frmApplicationConfirmation.aspx?ApplicationCode={0}&ReturnPage={1}", Server.UrlEncode(appDetail.ApplicationID), Server.UrlEncode("ApplicationDashBoard")))

	End Sub

	Protected Sub gridProcessing_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridProcessing.PageIndexChanging

	End Sub


     Protected Sub gridProcessing_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridProcessing.SelectedIndexChanged


          Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
          Dim selectedRowIndex As Integer

          selectedRowIndex = Me.gridProcessing.SelectedRow.RowIndex

          Dim row As GridViewRow = gridProcessing.Rows(selectedRowIndex)

          dt = cr.PMgetApplicationByCode(row.Cells(2).Text.ToString())
          ' getting the submitted application docunments
          'dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CInt(row.Cells(2).Text.ToString().Split("-")(1)))
          dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CStr(row.Cells(2).Text.ToString()))

          ViewState("ApplicationCode") = row.Cells(2).Text.ToString
          ViewState("PIN") = row.Cells(4).Text.ToString

          dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())

          ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(2).Text.ToString(), row.Cells(4).Text.ToString())

          Session("lodgmentProperties") = ApplicationProperties

          populateProperties(ApplicationProperties)
          Me.populateDocuments(dtDocuments)

          If ApplicationProperties.Count < 10 Then
               pnlLeftGrid.Height = 400
          Else
               pnlLeftGrid.Height = Nothing
          End If

          

     End Sub

     Protected Sub btnComfirmProcessing_Click(sender As Object, e As EventArgs) Handles btnComfirmProcessing.Click


          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))

                         refresh()

                         ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
                         getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

                         populateDocuments(dt)


                    ElseIf cb.Checked = False Then

                    End If

               Next

          Catch ex As Exception
               '   MsgBox("" & ex.Message)
          Finally

               GC.Collect()


          End Try




     End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = True


          Next

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click
          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = False


          Next
     End Sub

    

     
     Protected Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click


          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try


               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         'If Me.ddApplicationStatusBatch.SelectedItem.Text.ToString <> "" Then
                         cr.PMSetApplicationStatus(grow.Cells(2).Text, "Processing", 1)
                         populateApplicationList()
                         refresh()

                    ElseIf cb.Checked = False Then

                    End If

               Next
               'getApplicationList()
          Catch ex As Exception

          End Try


     End Sub

     Protected Sub btnComfirmProcessing_Click(sender As Object, e As ImageClickEventArgs) Handles btnComfirmProcessing.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         cr.PMSetApplicationStatus(grow.Cells(2).Text, "Send to Pencom", Session("user"))

                         refresh()

                         ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
                         getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

                         populateDocuments(dt)


                    ElseIf cb.Checked = False Then

                    End If

               Next

          Catch ex As Exception

          Finally

               GC.Collect()


          End Try

     End Sub

     Protected Sub btnReject_Click(sender As Object, e As ImageClickEventArgs) Handles btnReject.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try


               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         'If Me.ddApplicationStatusBatch.SelectedItem.Text.ToString <> "" Then
                         cr.PMSetApplicationStatus(grow.Cells(2).Text, "Processing", 1)
                         populateApplicationList()
                         refresh()

                    ElseIf cb.Checked = False Then

                    End If

               Next
               'getApplicationList()
          Catch ex As Exception

          End Try

     End Sub
     'refreshing the pop up comment list on an application
     Protected Sub refreshCommentList(appCode As String)
          Dim cr As New Core, j As Integer, dt As DataTable
          dt = cr.PMgetApplicationComment(appCode, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop
          mpAppComments.Show()

     End Sub
     Protected Sub btnAppCommentRemove_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentRemove.Click


          Dim cr As New Core
          Try
               If IsNothing(Session("UserName")) = False Then
                    Dim UName As String = CStr(Session("UserName"))
                    Dim str() As String = Me.lstComments.SelectedItem.Text.Split(":")
                    If UName = LTrim(RTrim(CStr(str(1)))) Then

                         cr.PMRemoveComment(CInt(str(0)))
                         refreshCommentList(txtApplicationID.Text)

                    Else
                         Me.mpAppComments.Show()
                    End If
               Else

               End If

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

          Dim cr As New Core
          cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1)
          txtApplicationComment.Text = ""
          refreshCommentList(txtApplicationID.Text)
          Me.mpAppComments.Show()

     End Sub

     Private Sub DownLoadSNR()

          If IsNothing(ViewState("schedulePath")) = False Then

               If CStr(ViewState("schedulePath")).ToString = "" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
               Else
               End If


               Dim schedulePath As String = ViewState("schedulePath")
               Try


                    Dim str() As String = schedulePath.Split("|")
                    Dim FI As FileInfo, fileExt As String, i As Integer = 0

                    Do While i < str.Length

                         FI = New FileInfo(str(i).Trim.ToString)
                         fileExt = LCase(FI.Extension)
                         'MsgBox("" & fileExt)
                         Select Case fileExt

                              Case Is = ".xls"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".xlsx"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Is = ".csv"
                                   'Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Is = ".pdf"
                                   'Process.Start("ACRORD32", str(i).Trim.ToString)

                                   Response.ContentType = "application/pdf"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()


                              Case Is = ".doc"
                                   ' Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".docx"

                                   'Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".jpg"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".png"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Else
                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                         End Select
                         i = i + 1
                    Loop
               Catch ex As Exception
                    '  MsgBox("" & ex.Message)
               End Try

          Else
               ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
          End If


     End Sub

     Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True And chk < 1 Then

                         ' Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & grow.Cells(4).Text & ".pdf"
                         Dim filePath As String = Server.MapPath("~/FileDownLoads/" & grow.Cells(4).Text & ".pdf")

                         generateFiles(grow.Cells(4).Text, grow.Cells(2).Text, filePath)
                         ViewState("schedulePath") = filePath
                         DownLoadSNR()
                         chk = chk + 1
                    ElseIf cb.Checked = False Then
                    End If

               Next

          Catch ex As Exception

          Finally

               GC.Collect()

          End Try

     End Sub

     Private Sub generateFiles(pin As String, appCode As String, path As String)

          Dim crExportOptions As New ExportOptions
          Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
          Dim crFormatypeOption As New PdfRtfWordFormatOptions
          Dim rdoc As New ReportDocument
          Dim rsource As New CrystalDecisions.Web.CrystalReportSource

          rdoc.Load(Server.MapPath("~/Report/SNR.rpt"))

          'If Not Directory.Exists(path) = True Then
          '    Directory.CreateDirectory(path)
          'End If

          Dim ds As DataSet
          ds = populateSNR(pin, appCode)
          rdoc.SetDataSource(ds.Tables(0))

          'crDiskFileDestinationOptions.DiskFileName = path & "MyTest.PDF"
          crDiskFileDestinationOptions.DiskFileName = path
          crExportOptions = rdoc.ExportOptions

          With crExportOptions

               .ExportDestinationType = ExportDestinationType.DiskFile
               .ExportFormatType = ExportFormatType.PortableDocFormat
               .ExportDestinationOptions = crDiskFileDestinationOptions
               .ExportFormatOptions = crFormatypeOption

          End With

          rdoc.Export()
          'rsource.Export()

     End Sub

     Private Function populateSNR(pin As String, appCode As String) As DataSet

          Dim cr As New Core, dtApplication As New DataTable, i As Integer = 0
          'dtApplication = cr.PMgetApplicationByPIN("PEN100000189215", 2)
          dtApplication = cr.PMgetApplicationByPIN(pin, appCode)
          Dim ds As New dsSNR
          Dim newSNRow As DataRow

          newSNRow = ds.Tables(0).NewRow

          newSNRow("txtPIN") = dtApplication.Rows(0).Item("rsapin")
          newSNRow("txtSurname") = dtApplication.Rows(0).Item("Surname")
          newSNRow("txtFirstName") = dtApplication.Rows(0).Item("FirstName")
          newSNRow("txtOtherName") = dtApplication.Rows(0).Item("MiddleName")
          newSNRow("dteDOB") = dtApplication.Rows(0).Item("dateofbirth")
          newSNRow("dteDOR") = dtApplication.Rows(0).Item("DOR")
          newSNRow("txtPermAddress1") = dtApplication.Rows(0).Item("ResidentialAddress")
          newSNRow("txtPermAddress2") = ""
          newSNRow("txtContactAddress1") = dtApplication.Rows(0).Item("ContactAddress")
          newSNRow("txtContactAddress2") = ""
          newSNRow("txtTelephone1") = dtApplication.Rows(0).Item("Phone")
          newSNRow("txtTelephone2") = ""
          newSNRow("txtEmployerName") = dtApplication.Rows(0).Item("EmployerName")
          newSNRow("txtEmpoyerAddress") = dtApplication.Rows(0).Item("OfficeAddress")
          newSNRow("txtEmployerCode") = dtApplication.Rows(0).Item("EmployerCode")
          newSNRow("numTotalRemuneration") = "0.00"
          newSNRow("numTotalContribution") = dtApplication.Rows(0).Item("numRSABalance")
          newSNRow("txtPFACode") = "0023"
          newSNRow("txtSignatory") = "Tade Gbadebo"
          newSNRow("txtSignatoryDesignation") = "Ag. Head Benefit Processing"
          newSNRow("txtSex") = dtApplication.Rows(0).Item("sex")
          newSNRow("txtRetirementReason") = dtApplication.Rows(0).Item("txtReason")
          newSNRow("txtMaritalStatus") = dtApplication.Rows(0).Item("MaritalStatus")

          ds.Tables(0).Rows.Add(newSNRow)
          Return ds
          'MsgBox("" & ds.Tables(0).Rows.Count)

          '          Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & Year(Me.txtStartDate.Text) & "" & Month(Me.txtStartDate.Text) & "" & Day(Me.txtStartDate.Text) & "_" & Me.dcFund.SelectedValue & "_" & brokers.Item(i) & ".pdf"





     End Function

	Protected Sub lstComments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstComments.SelectedIndexChanged

		Me.txtApplicationComment.Text = Me.lstComments.SelectedValue.ToString

	End Sub


	Protected Sub AddViewIACComment_Click(sender As Object, e As EventArgs) Handles btnShowIACCommentPopup.Click

		Dim btnAddViewIACComment As New ImageButton
		btnAddViewIACComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewIACComment.NamingContainer

		'MsgBox("" & Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text.ToString)

		Me.txtIACApplicationID.Text = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text
		Me.txtApplicationIACComment.Text = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE_IC").Rows(0).Item("txtComment").ToString

		'pops up the comment dialogue
		mpAppIACComments.Show()

	End Sub
End Class
