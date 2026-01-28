Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Imports System.Security.Cryptography

Partial Class frmApplicationControlCheck
	Inherits System.Web.UI.Page
	Dim objAL As New ArrayList()
	Dim ApprovalTypeCollection As New Hashtable
	Dim rowIndex As Integer

	Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)

		gridApplication.PageIndex = e.NewPageIndex
		gridApplication.DataBind()
		If ViewState("CheckBoxArray") IsNot Nothing Then
			Dim CheckBoxArray As ArrayList = _
			DirectCast(ViewState("CheckBoxArray"), ArrayList)
			Dim checkAllIndex As String = "chkAll-" & gridApplication.PageIndex

			'If CheckBoxArray.IndexOf(checkAllIndex) <> -1 Then
			'     Dim chkAll As CheckBox = _
			'     DirectCast(gridApplication.HeaderRow.Cells(0) _
			'     .FindControl("chkAll"), CheckBox)
			'     chkAll.Checked = True
			'End If

			For i As Integer = 0 To gridApplication.Rows.Count - 1
				If gridApplication.Rows(i).RowType = DataControlRowType.DataRow Then
					If CheckBoxArray.IndexOf(checkAllIndex) <> -1 Then
						Dim chk As CheckBox = _
						DirectCast(gridApplication.Rows(i).Cells(0) _
						.FindControl("chkApplication"), CheckBox)
						chk.Checked = True
						gridApplication.Rows(i).Attributes.Add("style", "background-color:aqua")
					Else
						Dim CheckBoxIndex As Integer = gridApplication.PageSize * (gridApplication.PageIndex) + (i + 1)
						If CheckBoxArray.IndexOf(CheckBoxIndex) <> -1 Then
							Dim chk As CheckBox = _
							DirectCast(gridApplication.Rows(i).Cells(0) _
							.FindControl("chkApplication"), CheckBox)
							chk.Checked = True
							gridApplication.Rows(i).Attributes.Add("style", "background-color:aqua")
						End If
					End If
				End If
			Next
		End If
	End Sub

	Protected Sub gridApplication_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("ApplicationList")) = False Then

			Dim dt As DataTable = ViewState("ApplicationList")
			If e.Row.RowType = DataControlRowType.DataRow Then

				If CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Rejected" Then

					Dim cb As CheckBox
					cb = e.Row.FindControl("chkApplication")
					cb.Checked = True
					cb.Enabled = False
					e.Row.ForeColor = System.Drawing.Color.Red

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Confirmed" Then

					Dim cb As CheckBox
					cb = e.Row.FindControl("chkApplication")
					cb.Checked = True
					cb.Enabled = False
					e.Row.ForeColor = System.Drawing.Color.Green

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Tentative" Then

					Dim cb As CheckBox
					cb = e.Row.FindControl("chkApplication")
					cb.Checked = True
					cb.Enabled = False
					e.Row.ForeColor = System.Drawing.Color.BlueViolet

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Open" Then

					'Dim cb As CheckBox
					'cb = e.Row.FindControl("chkApplication")
					'cb.Checked = True
					'cb.Enabled = False
					'e.Row.ForeColor = System.Drawing.Color.BlueViolet

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = False Then


				End If

			End If


		Else
		End If

	End Sub

	Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)


		Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
		btnViewDocumentLog = sender
		Dim i As GridViewRow
		i = btnViewDocumentLog.NamingContainer


		If Not IsNothing(ViewState("Documents")) = True Then

			Dim dt As DataTable = ViewState("Documents"), dmsDocumentID As String, dmsDocumentExt As String
			'retrieving the location of the scanned document
			documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()

			'testing if the file still exist in the saved file path
			If File.Exists(documentPath) = True Then

				DownLoadDocument(documentPath)

			ElseIf File.Exists(documentPath) = False Then

				'DownLoadDocument(documentPath)
				documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()
				dmsDocumentID = dt.Rows(i.RowIndex).Item("DocumentID").ToString()
				dmsDocumentExt = dt.Rows(i.RowIndex).Item("DocumentExtension").ToString()

				Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
				Dim uName As String, uPWD As String, uRI As String

				uName = ConfigurationManager.AppSettings("FileNetUName")
				uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
				uRI = ConfigurationManager.AppSettings("FileNetURI")

				dms.getConnection(uName, uPWD, uRI)
				DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA_BPD", "." & dmsDocumentExt)
				DownLoadDocument(DMSDocumentPath)


			End If



			''''dms integration addition'''''''''''''''''''''''''''''''''''''''''''''''''''''''''


			'Dim dtDocs As New DataTable, dmsDocumentID As String, dmsDocumentExt As String
			'If IsNothing(ViewState("Documents")) = False Then

			'	dtDocs = ViewState("Documents")
			'	dmsDocumentID = dtDocs.Rows(i.RowIndex).Item("DocumentID").ToString
			'	dmsDocumentExt = dtDocs.Rows(i.RowIndex).Item("DocumentExtension").ToString

			'	Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
			'	Dim uName As String, uPWD As String, uRI As String

			'	uName = ConfigurationManager.AppSettings("FileNetUName")
			'	uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
			'	uRI = ConfigurationManager.AppSettings("FileNetURI")
			'	dms.getConnection(uName, uPWD, uRI)
			'	DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
			'	DownLoadDocument(DMSDocumentPath)
			'Else
			'End If


			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''











		Else

		End If







		'Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
		'btnViewDocumentLog = sender
		'Dim i As GridViewRow
		'i = btnViewDocumentLog.NamingContainer

		''appCode = Me.gridApplication.Rows(i.RowIndex).Cells(2).Text
		'appCode = ViewState("ApplicationCode")

		'If Not IsNothing(ViewState("Documents")) = True Then

		'	Dim dt As DataTable = ViewState("Documents")
		'	'retrieving the location of the scanned document
		'	documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()

		'	'testing if the file still exist in the saved file path
		'	If File.Exists(documentPath) = True Then

		'		DownLoadDocument(documentPath)

		'	ElseIf File.Exists(documentPath) = False Then

		'		DownLoadDocument(documentPath)

		'	End If


		'	''''dms integration addition'''''''''''''''''''''''''''''''''''''''''''''''''''''''''


		'	Dim dtDocs As New DataTable, dmsDocumentID As String, dmsDocumentExt As String
		'	If IsNothing(ViewState("Documents")) = False Then

		'		dtDocs = ViewState("Documents")
		'		dmsDocumentID = dtDocs.Rows(i.RowIndex).Item("DocumentID")
		'		dmsDocumentExt = dtDocs.Rows(i.RowIndex).Item("DocumentExtension")

		'		Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
		'		Dim uName As String, uPWD As String, uRI As String

		'		uName = ConfigurationManager.AppSettings("FileNetUName")
		'		uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
		'		uRI = ConfigurationManager.AppSettings("FileNetURI")

		'		dms.getConnection(uName, uPWD, uRI)
		'		DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
		'		DownLoadDocument(DMSDocumentPath)

		'	Else
		'	End If


		'	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


		'Else

		'End If


	End Sub
	Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

		Dim cr As New Core
		Try

			cr.PMUpdateApplicationControlCheck(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1, "PRE", Me.ddReviewStaus.SelectedItem.Text)

		Catch ex As Exception

		End Try


	End Sub



	Private Sub DownLoadDocument(path As String)

		If Not File.Exists(path) = False Then

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


	Protected Sub btnViewApplication_Comment(sender As Object, e As EventArgs)

		Dim btnViewApplicationComments As New ImageButton
		btnViewApplicationComments = sender
		Dim i As GridViewRow, cr As New Core, dt As New DataTable, j As Integer

		i = btnViewApplicationComments.NamingContainer
		Me.txtApplicationID.Text = Me.gridApplication.Rows(i.RowIndex).Cells(2).Text

		dt = cr.PMgetApplicationComment(Me.gridApplication.Rows(i.RowIndex).Cells(2).Text, "PRE")
		lstApplicationComments.Items.Clear()
		Do While j < dt.Rows.Count

			lstApplicationComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
			j = j + 1

		Loop

		mpApplicationComments.Show()

	End Sub

	Protected Sub btnView_Comment(sender As Object, e As EventArgs)


		Dim btnAddViewComment As New ImageButton
		btnAddViewComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewComment.NamingContainer
		Me.txtApplicationID.Text = Me.gridApplication.Rows(i.RowIndex).Cells(2).Text
		Me.txtApplicationComment.Text = cr.PMgetApplicationComment(Me.gridApplication.Rows(i.RowIndex).Cells(2).Text, "PRE_IC").Rows(0).Item("txtComment").ToString

		Me.ddReviewStaus.Text = cr.PMgetApplicationByCode(Me.gridApplication.Rows(i.RowIndex).Cells(2).Text).Rows(0).Item("txtControlCheckedStatus").ToString
		'pops up the comment dialogue
		mpAppComments.Show()


	End Sub

	Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("Documents")) = False Then

			Dim dt As DataTable = ViewState("Documents")
			If e.Row.RowType = DataControlRowType.DataRow Then

				If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

					e.Row.ForeColor = System.Drawing.Color.Red
					e.Row.Enabled = False

				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Then
					e.Row.ForeColor = System.Drawing.Color.Green
				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Then
					e.Row.ForeColor = System.Drawing.Color.OrangeRed
					e.Row.Enabled = True
				End If

			End If
		Else
		End If

	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub getAppReviewComment()

		ddReviewStaus.Items.Add("")
		ddReviewStaus.Items.Add("Open")
		ddReviewStaus.Items.Add("Confirmed")
		ddReviewStaus.Items.Add("Rejected")
		ddReviewStaus.Items.Add("Tentative")

	End Sub

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


		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtUsers As New DataTable
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(imgDownloadSoft)

		Try

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					getApprovalTypes()
					getAppReviewComment()
					dtUsers = Session("userDetails")
					getUserAccessMenu(Session("user"))

				End If

			Else
				getUserAccessMenu(Session("user"))

			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub rdApprovalTypes_CheckedChanged(sender As Object, e As EventArgs) Handles rdApprovalTypes.CheckedChanged
		If Me.rdApprovalTypes.Checked = True Then

			Me.txtPIN.Text = ""
			Me.txtPIN.Enabled = False
			ddApprovalType.Enabled = True

		Else

		End If
	End Sub

	Protected Sub rdPIN_CheckedChanged(sender As Object, e As EventArgs) Handles rdPIN.CheckedChanged

		If Me.rdPIN.Checked = True Then

			Me.ddApprovalType.SelectedIndex = 0
			Me.txtPIN.Enabled = True
			ddApprovalType.Enabled = False

		Else

		End If

	End Sub

	Protected Sub refresh()
		ViewState("ApplicationCode") = Nothing
		Dim nw As New List(Of ApplicationProperties)
		populateProperties(nw)
		Dim dt As New DataTable
		populateDocuments(dt)

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
			MsgBox("" & ex.Message)
		End Try

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
	Private Sub getApplicationList()

		Try
			Dim dt As New DataTable, cr As New Core
			If Me.rdPIN.Checked = True Then


				Dim iCount As Integer, str As String = ""
				Dim sarrMyString As String() = UCase(Me.txtPIN.Text).ToString.Split(New String() {"PEN"}, StringSplitOptions.None)


				If sarrMyString.Length > 1 Then

					Do While iCount < sarrMyString.Length

						If sarrMyString(iCount).ToString <> "" Then

							If iCount = 0 And sarrMyString(iCount).ToString.Trim <> "" Then

								str = "'PEN" & "" & sarrMyString(iCount).Trim & "'"

							ElseIf iCount < sarrMyString.Length And str = "" Then

								str = "'PEN" & "" & sarrMyString(iCount).Trim & "'"

							ElseIf iCount < sarrMyString.Length And str <> "" Then

								str = str & "," & "'PEN" & "" & sarrMyString(iCount).Trim & "'"

							End If

						Else

						End If

						
						iCount = iCount + 1
					Loop


				ElseIf sarrMyString.Length = 1 Then

					str = "'PEN" & "" & sarrMyString(0) & "'"

				End If
				str = "(" & str & ")"
				'MsgBox("" & str)

				'Exit Sub

				dt = cr.PMgetApplicationByPIN(RTrim(LTrim(str)))
				ViewState("ApplicationList") = dt
			ElseIf rdApprovalTypes.Checked = True Then

				If Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" And Me.ddApprovalType.SelectedItem.Text = "" Then
					dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, 0)
					ViewState("ApplicationList") = dt
					'querying by date and application type
				ElseIf Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" And Me.ddApprovalType.SelectedItem.Text <> "" And Me.txtPIN.Text = "" Then

					If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
						ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
						dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
						ViewState("ApplicationList") = dt
					Else
						Response.Redirect("frmApplicationInformation.aspx")

					End If

				End If

			ElseIf Me.rdSPBatches.Checked = True Then

				If Me.ddSPBatches.SelectedItem.Text <> "" Then
					dt = cr.PMgetApplicationByDate(Me.ddSPBatches.SelectedItem.Text)
					ViewState("ApplicationList") = dt
				End If


			Else

				If Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" Then
					dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, 0)
					ViewState("ApplicationList") = dt
				Else
					dt = cr.PMgetApplicationByDate(CDate(Now.Date), CDate(Now.Date), False, 0)
					ViewState("ApplicationList") = dt
				End If

			End If
			'MsgBox("Got dere")
			'  ViewState("ApplicationList") = dt
			If IsNothing(dt) = False Then
				Me.loadGrid(dt)
			End If

		Catch ex As Exception

		End Try

	End Sub
	Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click

		refresh()
		getApplicationList()

	End Sub

	Protected Sub loadGrid(dt As DataTable)

		Try

			gridApplication.DataSource = dt
			gridApplication.DataBind()

			If dt.Rows.Count > 5 Then
				Me.pnlGrid.Height = Nothing
			Else
				Me.pnlGrid.Height = 500
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub gridApplication_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridApplication.PageIndexChanging

		Dim dt As New DataTable
		gridApplication.PageIndex = e.NewPageIndex
		dt = ViewState("ApplicationList")
		Me.loadGrid(dt)


	End Sub

	




	Protected Sub gridApplication_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplication.SelectedIndexChanged

		Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
		Dim selectedRowIndex As Integer
		txtApplicationComment.Text = ""

		selectedRowIndex = Me.gridApplication.SelectedRow.RowIndex

		Dim row As GridViewRow = gridApplication.Rows(selectedRowIndex)

		dt = cr.PMgetApplicationByCode(row.Cells(2).Text.ToString())

		'getting submitted documents per application 
		'dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CInt(row.Cells(2).Text.ToString().Split("-")(1)))
		dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CStr(row.Cells(2).Text.ToString()))

		ViewState("ApplicationCode") = row.Cells(2).Text.ToString
		ViewState("PIN") = row.Cells(4).Text.ToString

		'getting customer's personal information details
		dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())


		ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(2).Text.ToString(), row.Cells(4).Text.ToString())

		Session("lodgmentProperties") = ApplicationProperties

		'population the grid to the left for other application information
		populateProperties(ApplicationProperties)


		txtApplicationComment.Text = dt.Rows(0).Item("txtControlCheckComment").ToString



		'population the grid at the bottom for submitted required application documents
		Me.populateDocuments(dtDocuments)

		If ApplicationProperties.Count < 10 Then
			pnlLeftGrid.Height = 400
		Else
			pnlLeftGrid.Height = Nothing
		End If

	End Sub

	Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

	End Sub


	Protected Sub btnConfirmApplication_Click(sender As Object, e As ImageClickEventArgs) Handles btnConfirmApplication.Click


		Dim cb As CheckBox, chk As Integer = 0, cr As New Core

		Try



			For Each grow As GridViewRow In Me.gridApplication.Rows

				cb = grow.FindControl("chkApplication")

				If cb.Checked = True Then
					cr.PMUpdateApplicationControlCheck("", grow.Cells(2).Text, Session("user"), 1, "PRE", ddReviewStaus.SelectedItem.Text)
				ElseIf cb.Checked = False Then

				End If

			Next

			'    getApplicationList()

			refresh()
			getApplicationList()

		Catch ex As Exception


		End Try


	End Sub

	Protected Sub btnTagAll_Click(sender As Object, e As ImageClickEventArgs) Handles btnTagAll.Click

		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridApplication.Rows
			cb = grow.FindControl("chkApplication")
			cb.Checked = True
		Next

	End Sub

	Protected Sub btnUnTagAll_Click(sender As Object, e As ImageClickEventArgs) Handles btnUnTagAll.Click

		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridApplication.Rows
			cb = grow.FindControl("chkApplication")
			cb.Checked = False
		Next

	End Sub

	'If IsNothing(ViewState("ApplicationList")) = False Then

	'Dim dt As New DataTable
	'          Me.gridApplication.PageIndex = e.NewPageIndex
	'          dt = ViewState("ApplicationList")
	'          Me.loadGrid(dt)
	'' getApplicationList()
	'     Else
	'     End If

	'End Sub


	''Call on gridview page rowIndex change
	'Protected Sub gridApplication_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridApplication.PageIndexChanging
	'     'Save checked rows before page change
	'     Dim dt As New DataTable
	'     SaveCheckedStates()
	'     gridApplication.PageIndex = e.NewPageIndex

	'     dt = ViewState("ApplicationList")
	'     Me.loadGrid(dt)

	'     'LoadResultData()
	'     'Populate cheked items with its checked status
	'     PopulateCheckedStates()
	'End Sub
	'Save the state of row checkboxes
	Private Sub SaveCheckedStates()
		Dim objAL As New ArrayList()
		Dim rowIndex As Integer = -1
		For Each row As GridViewRow In Me.gridApplication.Rows
			rowIndex = Convert.ToInt32(gridApplication.DataKeys(row.RowIndex).Value)
			'rowIndex = Convert.ToInt32((row.RowIndex))
			Dim isSelected As Boolean = CType(row.FindControl("chkApplication"), CheckBox).Checked
			If ViewState("SELECTED_ROWS") IsNot Nothing Then
				objAL = CType(ViewState("SELECTED_ROWS"), ArrayList)
			End If
			If isSelected Then
				If Not objAL.Contains(rowIndex) Then
					objAL.Add(rowIndex)
				End If
			Else
				objAL.Remove(rowIndex)
			End If
		Next row
		If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
			ViewState("SELECTED_ROWS") = objAL
		End If
	End Sub

	'Populate the saved checked checkbox status
	Private Sub PopulateCheckedStates()
		Dim objAL As ArrayList = CType(ViewState("SELECTED_ROWS"), ArrayList)
		If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
			For Each row As GridViewRow In gridApplication.Rows

				Dim rowIndex As Integer = Convert.ToInt32(gridApplication.DataKeys(row.RowIndex).Value)
				'Dim rowIndex As Integer = Convert.ToInt32(row.RowIndex)
				If objAL.Contains(rowIndex) Then
					Dim chkSelect As CheckBox = CType(row.FindControl("chkApplication"), CheckBox)
					chkSelect.Checked = True
					' row.Attributes.Add("class", "selected")
				End If
			Next row
		End If
	End Sub



	Protected Sub lstApplicationComments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstApplicationComments.SelectedIndexChanged
		Me.mpApplicationComments.Show()
	End Sub

	Protected Sub rdSPBatches_CheckedChanged(sender As Object, e As EventArgs) Handles rdSPBatches.CheckedChanged
		Try
			If Me.rdSPBatches.Checked = True Then
				ddSPBatches.Items.Clear()
				Me.txtPIN.Text = ""
				Me.txtPIN.Enabled = False
				ddSPBatches.Enabled = True
				ddApprovalType.Enabled = False

				Dim cr As New Core, dt As New DataTable, i As Integer

				dt = cr.getApplicationBatches(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))
				ddSPBatches.Items.Clear()
				Do While i < dt.Rows.Count

					If Me.ddSPBatches.Items.Count = 0 Then
						ddSPBatches.Items.Add("")
						ddSPBatches.Items.Add(dt.Rows(i).Item("txtBatchNo"))
					Else
						ddSPBatches.Items.Add(dt.Rows(i).Item("txtBatchNo"))
					End If
					i = i + 1
				Loop
			Else

			End If
		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try


	End Sub

	Protected Sub imgDownloadSoft_Click(sender As Object, e As ImageClickEventArgs) Handles imgDownloadSoft.Click

		Try

			Dim dt As New DataTable, cr As New Core

			If IsNothing(ViewState("ApplicationList")) = False Then

				dt = ViewState("ApplicationList")

				Dim dtDocuments = New DataTable, i As Integer, dtColumn As DataColumn
				dtColumn = New DataColumn("ApplicationCode")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("FullName")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("PIN")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("EmployerName")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("ApplicationType")
				dtDocuments.Columns.Add(dtColumn)

				Do While i < dt.Rows.Count

					Dim newCustomersRow As DataRow
					newCustomersRow = dtDocuments.NewRow()
					newCustomersRow("ApplicationCode") = dt.Rows(i).Item("txtApplicationCode")
					newCustomersRow("FullName") = dt.Rows(i).Item("txtFullName").ToString.Replace("|", "")
					newCustomersRow("PIN") = dt.Rows(i).Item("txtPIN").ToString
					newCustomersRow("EmployerName") = dt.Rows(i).Item("txtEmployerName").ToString
					newCustomersRow("ApplicationType") = dt.Rows(i).Item("ApprovalType").ToString

					dtDocuments.Rows.Add(newCustomersRow)
					i = i + 1
				Loop

				cr.ExtractCSV(dtDocuments, "ApplicationList")

			Else

			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub calSDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calSDate.VisibleMonthChanged

	End Sub
End Class
