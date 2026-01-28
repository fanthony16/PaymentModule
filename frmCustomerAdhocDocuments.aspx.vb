Imports AjaxControlToolkit
Imports System.IO
Imports System.Data

Partial Class frmCustomerAdhocDocuments
	Inherits System.Web.UI.Page
	Dim DocumentCollection As New Hashtable

	Protected Sub calDocRecievedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calDocRecievedDate.SelectionChanged

		Me.calDocRecievedDate_PopupControlExtender.Commit(Me.calDocRecievedDate.SelectedDate)

	End Sub

	Protected Sub gridProcessing_RowDataBound()

	End Sub
	Protected Sub AddViewComment_Click()

	End Sub
	Protected Sub BtnViewDetails_Click()

	End Sub
	Protected Sub AjaxFileDocumentUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)

		Try

			Dim filename As String = System.IO.Path.GetFileName(e.FileName)
			Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)
			Dim fileNewName As String

			If Me.txtAdhocDocDescription.Text <> "" Then

				Session("Document") = Me.txtAdhocDocDescription.Text
				fileNewName = Session("Document").ToString

			Else
				fileNewName = Session("Document").ToString

			End If

			fileNewName = fileNewName.Replace(" | ", "_")
			fileNewName = fileNewName.Replace(" ", "_")

			Dim strUploadPath As String

			If (Session("Document").ToString) = "Others" Then

				If Directory.Exists(Server.MapPath("~/FileUploads/" & Session("user"))) = False Then

					Directory.CreateDirectory(Server.MapPath("~/FileUploads/" & Session("user")))

				Else

				End If

				fileNewName = filename
				fileNewName = fileNewName.Replace("|", "")
				fileNewName = fileNewName.Replace(" ", "_")

				'strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName

				strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName
			Else

				'	MsgBox("" & Session("PIN") & " : " & Session("user"))

				strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName & System.IO.Path.GetExtension(fullPath) '& "_" & filename
				'keeping the temporary the uploaded document prior final saving for easy retrieval should there is network cut-off

			End If


			Session("documentPath") = strUploadPath
			Me.flReqDocUpload.SaveAs(Server.MapPath(strUploadPath))
			flReqDocUpload.Dispose()
			Session("Document") = Nothing

		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try

	End Sub
	Protected Sub gridSubmittedDocuments_RowDataBound()

	End Sub
	Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

		Try

			Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String, dmsDocumentID As String, dmsDocumentExt As String
			btnViewDocumentLog = sender
			Dim i As GridViewRow
			i = btnViewDocumentLog.NamingContainer
			'   appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

			If Not IsNothing(ViewState("RecievedDocument")) = True Then

				Dim dt As DataTable = ViewState("RecievedDocument")
				'retrieving the location of the scanned document
				documentPath = dt.Rows(i.RowIndex).Item("txtDocPath").ToString()
				dmsDocumentID = dt.Rows(i.RowIndex).Item("txtDMSDocumentID").ToString()
				dmsDocumentExt = dt.Rows(i.RowIndex).Item("txtDMSDocumentExt").ToString()

				'testing if the file still exist in the saved file path
				If File.Exists(documentPath) = True Then

					DownLoadDocument(documentPath)

				ElseIf File.Exists(documentPath) = False Then

					Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
					Dim uName As String, uPWD As String, uRI As String

					uName = ConfigurationManager.AppSettings("FileNetUName")
					uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
					uRI = ConfigurationManager.AppSettings("FileNetURI")

					dms.getConnection(uName, uPWD, uRI)
					DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
					DownLoadDocument(DMSDocumentPath)

				End If


			Else

			End If

		Catch ex As Exception

		End Try

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

	Public Function getDocumentTypes() As List(Of String)

		Dim dc As New AppDocumentsDataContext
		Dim lstDocument As New List(Of String)
		Dim query = From m In dc.tblDocumentTypes _
				  Select New With {m.pkiDocumentTypeID, m.txtDocumentName}
		For Each a In query

			lstDocument.Add(a.txtDocumentName & " | " & a.pkiDocumentTypeID)
			'DocumentCollection.Add(a.txtDocumentName, a.pkiDocumentTypeID)

		Next
		ViewState("DocumentCollection") = DocumentCollection
		Return lstDocument

	End Function

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

		Try

			'If Page.IsPostBack = False Then
			'	populateDocuments()
			'Else
			'End If

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					populateDocuments()
					getUserAccessMenu(Session("user"))

					'If IsNothing(Session("PIN")) = False Then
					'	Me.txtPIN.Text = Session("PIN")
					'	Me.getDocuments(Me.txtPIN.Text)
					'Else
					'End If

				Else

				End If
			Else
				getUserAccessMenu(Session("user"))

				'If IsNothing(Session("PIN")) = False Then
				'	Me.txtPIN.Text = Session("PIN")
				'	Me.getDocuments(Me.txtPIN.Text)
				'Else
				'End If

			End If


		Catch ex As Exception

		End Try
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

	Protected Sub populateDocuments()
		Dim lstDoc As New List(Of String), i As Integer
		Try
			lstDoc = getDocumentTypes()
			ddDocumentType.Items.Clear()
			Do While i < lstDoc.Count
				If Me.ddDocumentType.Items.Count = 0 Then

					ddDocumentType.Items.Add("")
					ddDocumentType.Items.Add(lstDoc.Item(i))

				Else
					ddDocumentType.Items.Add(lstDoc.Item(i))
				End If
				i = i + 1
			Loop

		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try


	End Sub
	'creating temporary folder to upload scanned document before moving to the permanent location on saving the application
	Private Sub MakeDirectoryIfExists(ByVal NewDirectory As String)

		Try
			' Check if directory exists
			If Not Directory.Exists(NewDirectory) Then
				' Create the directory.
				Directory.CreateDirectory(NewDirectory)
			ElseIf Directory.Exists(NewDirectory) Then

			End If
		Catch _ex As IOException
			Response.Write(_ex.Message)
		End Try

	End Sub
	Protected Sub ddDocumentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddDocumentType.SelectedIndexChanged

		Try

			'If Me.ddDocumentType.SelectedItem.Text.ToString.Split("|")(0).Trim = "Others" Then

			'	dvUploadOtherDocuments.Visible = True
			'	Session("Document") = Me.txtAdhocDocDescription.Text
			'Else
			'	dvUploadOtherDocuments.Visible = False
			'End If



			If Me.ddDocumentType.SelectedItem.Text.ToString.Split("|")(0).Trim = "Others" Then
				Me.dvUploadOtherDocuments.Visible = True
				Session("Document") = Me.ddDocumentType.SelectedItem.Text.ToString.Split("|")(0).Trim
				Session("PIN") = Me.txtPIN.Text
				Dim strUploadPath As String = Server.MapPath("~/FileUploads/" & Session("user"))
				MakeDirectoryIfExists(strUploadPath)

			Else
				Session("Document") = Me.ddDocumentType.SelectedItem.Text
				Dim strUploadPath As String = Server.MapPath("~/FileUploads/" & Session("user"))
				MakeDirectoryIfExists(strUploadPath)
				Session("PIN") = Me.txtPIN.Text
				Me.txtAdhocDocDescription.Text = ""
				Me.dvUploadOtherDocuments.Visible = False

			End If







		Catch ex As Exception

		End Try

	End Sub

	Protected Sub btnSaveDocument_Click(sender As Object, e As EventArgs) Handles btnSaveDocument.Click

		Dim appAdhocDocDetail As New AdhocDocuments, cr As New Core

		If Not IsNothing(Session("documentPath")) = True Then

			'appAdhocDocDetail.ApplicationCode = ApplicationCode
			appAdhocDocDetail.Description = txtAdhocDocDescription.Text
			appAdhocDocDetail.PIN = LTrim(RTrim(Me.txtPIN.Text))
			appAdhocDocDetail.RecievedBy = Session("user")
			appAdhocDocDetail.RecievedDate = Now

			appAdhocDocDetail.DocumentTypeID = CInt(Me.ddDocumentType.SelectedItem.Text.ToString.Split("|")(Me.ddDocumentType.SelectedItem.Text.ToString.Split("|").Length - 1).Trim)
			If Me.txtDORetirement.Text <> "" Then
				appAdhocDocDetail.RetirementDate = CDate(Me.txtDORetirement.Text)

				appAdhocDocDetail.IsRetiree = 1
			Else
				appAdhocDocDetail.IsRetiree = 0
				appAdhocDocDetail.RetirementDate = CDate("1900-01-01")
			End If


			Dim sarrMyString As String() = Session("documentPath").ToString.Split(New String() {"PEN"}, StringSplitOptions.None)

			If sarrMyString.Count = 1 Then

				sarrMyString = Session("documentPath").ToString.Split(New String() {"DBA"}, StringSplitOptions.None)
				'	appAdhocDocDetail.DocPath = "DBA" + sarrMyString(1).ToString + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

				appAdhocDocDetail.DocPath = "DBA" + sarrMyString(1).ToString + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

			Else

				'appAdhocDocDetail.DocPath = "PEN" + sarrMyString(1).ToString + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

				appAdhocDocDetail.DocPath = "PEN" + sarrMyString(1).ToString + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

				'D:\NPM_Data\ApplicationDocuments\
				'"D:\NPM_Data\ApplicationDocuments\"

			End If


			If cr.PMSubmitAdhocDocument(appAdhocDocDetail, Session("user")) = True Then

				'	Response.Redirect("frmCustomerAdhocDocuments.aspx")
				Me.getDocuments(Me.txtPIN.Text)
				Session("PIN") = Me.txtPIN.Text
			Else

			End If

		Else

			'newCustomersRow("DocumentPath") = ""

		End If

	End Sub

	Protected Sub btnFindDocuments_Click(sender As Object, e As EventArgs) Handles btnFindDocuments.Click

		getDocuments(Me.txtPIN.Text)

	End Sub
	Protected Sub getDocuments(PIN As String)

		Dim dtDocument As New DataTable, cr As New Core, dtDocuments As New DataTable, dtColumn As DataColumn, i As Integer

		Try

			'retrieving all the submitted documents for the application
			dtDocument = cr.PMgetSubmitAdhocDocument(PIN)

			dtColumn = New DataColumn("DocumentName")
			dtDocuments.Columns.Add(dtColumn)

			dtColumn = New DataColumn("RecievedDate")
			dtDocuments.Columns.Add(dtColumn)

			dtColumn = New DataColumn("RetirementDate")
			dtDocuments.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DocumentPath")
			dtDocuments.Columns.Add(dtColumn)

			'dtDocuments = New DataTable
			Do While i < dtDocument.Rows.Count

				'Dim lstAppDocDetail As New ApplicationDocumentDetail
				'lstAppDocDetail.DocumentTypeID = dtDocument.Rows(i).Item("fkiDocumentTypeID")
				'lstAppDocDetail.DocumentTypeName = dtDocument.Rows(i).Item("txtDocumentName")
				'lstAppDocDetail.MemberApplicationID = dtDocument.Rows(i).Item("fkiMemberApplicationID")
				'lstAppDocDetail.DocumentLocation = dtDocument.Rows(i).Item("txtDocumentPath").ToString
				'lstAppDocDetail.IsVerified = dtDocument.Rows(i).Item("IsVerified").ToString
				'lstAppDocDetailOdd.Add(lstAppDocDetail)

				'PMGetDocument

				Dim newCustomersRow As DataRow
				newCustomersRow = dtDocuments.NewRow()

				'If CStr(dtDocument.Rows(i).Item("fkiDocumentTypeID")).ToString = "" Then
				'	newCustomersRow("DocumentName") = dtDocument.Rows(i).Item("txtDescription")
				'ElseIf CStr(dtDocument.Rows(i).Item("fkiDocumentTypeID")).ToString <> "" Then
				'	newCustomersRow("DocumentName") = cr.PMGetDocument(CInt(dtDocument.Rows(i).Item("fkiDocumentTypeID")))
				'End If

				If IsDBNull(dtDocument.Rows(i).Item("fkiDocumentTypeID")) = True Then
					newCustomersRow("DocumentName") = dtDocument.Rows(i).Item("txtDescription")
				Else
					newCustomersRow("DocumentName") = cr.PMGetDocument(CInt(dtDocument.Rows(i).Item("fkiDocumentTypeID"))).Rows(0).Item("txtDocumentName").ToString
				End If


				'newCustomersRow("RecievedDate") = dtDocument.Rows(i).Item("dteReceived").ToString.Substring(0, 10)
				'	MsgBox("" & dtDocument.Rows(i).Item("dteRecieved"))

				newCustomersRow("RecievedDate") = CDate(dtDocument.Rows(i).Item("dteRecieved"))




				If CBool(dtDocument.Rows(i).Item("isRetiree")) = False Then
					newCustomersRow("RetirementDate") = ""
				Else
					newCustomersRow("RetirementDate") = CDate(dtDocument.Rows(i).Item("dteDOR"))
				End If


				Dim aryDocumentPath As Array = dtDocument.Rows(i).Item("txtDocPath").ToString.Split("\")

				If aryDocumentPath.Length > 1 Then
					Array.Reverse(aryDocumentPath)
					Dim sarrMyString As String() = aryDocumentPath(0).ToString.Split(New String() {"PEN"}, StringSplitOptions.None)
					If sarrMyString.Count = 1 Then
						sarrMyString = aryDocumentPath(0).ToString.Split(New String() {"DBA023"}, StringSplitOptions.None)
						newCustomersRow("DocumentPath") = "DBA023" + sarrMyString(1).ToString
					ElseIf sarrMyString.Count > 1 Then
						newCustomersRow("DocumentPath") = "PEN" + sarrMyString(1).ToString
					Else
						newCustomersRow("DocumentPath") = aryDocumentPath(0)
					End If
				Else
					newCustomersRow("DocumentPath") = ""
				End If

				newCustomersRow("DocumentPath") = dtDocument.Rows(i).Item("txtDocPath").ToString


				dtDocuments.Rows.Add(newCustomersRow)

				i = i + 1
			Loop

			ViewState("RecievedDocument") = dtDocument

			loadGrid(dtDocuments)

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

		End Try


	End Sub

	Protected Sub loadGrid(dt As DataTable)
		Try

			gridSubmittedDocuments.DataSource = dt
			gridSubmittedDocuments.DataBind()

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
		End Try


	End Sub

	Protected Sub calDORetirement_SelectionChanged(sender As Object, e As EventArgs) Handles calDORetirement.SelectionChanged
		'PopupControlExtender_calDORetirement
		Me.PopupControlExtender_calDORetirement.Commit(Me.calDORetirement.SelectedDate)
	End Sub

	Protected Sub btnCancel_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancel.Click

		'intDocumentID
		Dim selectedRowIndex As Integer = Me.gridSubmittedDocuments.SelectedRow.RowIndex
		Dim dtDocument As DataTable = ViewState("RecievedDocument")
		Dim cr As New Core
		Dim filePath As String = dtDocument.Rows(selectedRowIndex).Item("txtDocPath")

		If File.Exists(filePath) = True Then
			File.Delete(filePath)
			cr.PMDeleteAdhocDocument(dtDocument.Rows(selectedRowIndex).Item("intDocumentID"))
			getDocuments(Me.txtPIN.Text)

		Else
		End If

	End Sub


	Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

	End Sub
End Class
