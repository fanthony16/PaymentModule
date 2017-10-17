Imports System.Data
Imports System.IO

Partial Class frmApplicationFind
	Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable

	Protected Sub AddViewIACComment_Click(sender As Object, e As EventArgs) Handles btnShowIACCommentPopup.Click

		Dim btnAddViewIACComment As New ImageButton
		btnAddViewIACComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewIACComment.NamingContainer

		'MsgBox("" & Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text.ToString)

		Me.txtIACApplicationID.Text = Me.gridParticipantApps.Rows(i.RowIndex).Cells(1).Text
		Me.txtApplicationIACComment.Text = cr.PMgetApplicationComment(Me.gridParticipantApps.Rows(i.RowIndex).Cells(1).Text, "PRE_IC").Rows(0).Item("txtComment").ToString

		'pops up the comment dialogue
		mpAppIACComments.Show()

	End Sub


	Protected Sub gridProcessing_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		'If IsNothing(ViewState("ApplicationList")) = False Then

		'	Dim dt As DataTable = ViewState("ApplicationList")
		'	If e.Row.RowType = DataControlRowType.DataRow Then


		'		If dt.Rows(e.Row.RowIndex).Item("txtLockedBy").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("txtLockedBy").ToString = Session("user").ToString) = False Then
		'			e.Row.ForeColor = System.Drawing.Color.Blue
		'			e.Row.Enabled = False
		'			'isVerified
		'		Else

		'		End If

		'	End If
		'Else
		'End If


		If IsNothing(ViewState("ApplicationList")) = False Then

			Dim dt As DataTable = ViewState("ApplicationList")
			If e.Row.RowType = DataControlRowType.DataRow Then

				If CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Rejected" Then

					'Dim cb As CheckBox
					'cb = e.Row.FindControl("chkApplication")
					'cb.Checked = True
					'cb.Enabled = False
					e.Row.ForeColor = System.Drawing.Color.Red

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Confirmed" Then

					'Dim cb As CheckBox
					'cb = e.Row.FindControl("chkApplication")
					'cb.Checked = True
					'cb.Enabled = False
					e.Row.ForeColor = System.Drawing.Color.Green

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True And CStr(dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString) = "Tentative" Then

					'Dim cb As CheckBox
					'cb = e.Row.FindControl("chkApplication")
					'cb.Checked = True
					'cb.Enabled = False
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

	'handle the view image button on the submitted document grid on the page
	Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
		btnViewDocumentLog = sender
		Dim i As GridViewRow
		i = btnViewDocumentLog.NamingContainer
		'   appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

		If Not IsNothing(ViewState("Documents")) = True Then

			Dim dt As DataTable = ViewState("Documents")
			'retrieving the location of the scanned document
			documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()

			'testing if the file still exist in the saved file path
			If File.Exists(documentPath) = True Then

				DownLoadDocument(documentPath)

			ElseIf File.Exists(documentPath) = False Then

				DownLoadDocument(documentPath)

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


	Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("Documents")) = False Then

			Dim dt As DataTable = ViewState("Documents")
			If e.Row.RowType = DataControlRowType.DataRow Then

				'  MsgBox("" & dt.Rows(e.Row.RowIndex).Item("isVerified").ToString)

				If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

					e.Row.ForeColor = System.Drawing.Color.Red
					e.Row.Enabled = False
					'isVerified
				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Or dt.Rows(e.Row.RowIndex).Item("DocumentID").ToString <> "") And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "True" Then
					e.Row.ForeColor = System.Drawing.Color.Green

				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Or dt.Rows(e.Row.RowIndex).Item("DocumentID").ToString <> "") And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then
					e.Row.ForeColor = System.Drawing.Color.Blue
					'e.Row.Enabled = False

				End If

			End If
		Else
		End If

	End Sub

	Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewApplicationLog As New ImageButton, appCode As String, cr As New Core
		btnViewApplicationLog = sender
		Dim i As GridViewRow
		i = btnViewApplicationLog.NamingContainer
		appCode = Me.gridParticipantApps.Rows(i.RowIndex).Cells(1).Text
		ViewState("PIN") = Me.gridParticipantApps.Rows(i.RowIndex).Cells(5).Text
		Dim typeID As Integer

		If Not IsNothing(ViewState("PIN")) = True Then

			typeID = cr.PMgetApprovalTypeByCode(appCode.Split("-")(0)).Rows(0).Item(0)

			Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}&PIN={4}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1, Server.UrlEncode("ApplicationFind"), Server.UrlEncode(ViewState("PIN"))))

		Else

		End If

		



	End Sub

	Protected Sub AddViewComment_Click(sender As Object, e As EventArgs) Handles btnShowCommentPopup.Click

		Dim btnAddViewComment As New ImageButton, dt As DataTable, j As Integer
		btnAddViewComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewComment.NamingContainer
		Me.txtApplicationID.Text = Me.gridParticipantApps.Rows(i.RowIndex).Cells(2).Text
		'logging comments for pre approval benefit application
		'Me.txtApplicationComment.Text = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE")
		dt = cr.PMgetApplicationComment(Me.gridParticipantApps.Rows(i.RowIndex).Cells(1).Text, "PRE")
		lstComments.Items.Clear()
		Do While j < dt.Rows.Count

			lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
			j = j + 1

		Loop

		'pops up the comment dialogue
		mpAppComments.Show()



	End Sub

	Protected Sub btnFindPIN_Click(sender As Object, e As EventArgs) Handles btnFindPIN.Click


		'Response.Write("<script language=javascript>alert('Please Note That Legacy Contribution Exists for teh Customer')</script>")


		Dim iCount As Integer, str As String = ""
		Dim sarrMyString As String() = UCase(Me.txtFindPIN.Text).ToString.Split(New String() {"PEN"}, StringSplitOptions.None)

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

		'LoadPIN(Me.txtFindPIN.Text)
		LoadPIN(str)


	End Sub
	Protected Sub LoadPIN(PIN As String)
		Try
			Dim cr As New Core, dt As DataTable, dtApplications As New DataTable, dtDocuments As New DataTable, ApplicationProperties As New List(Of ApplicationProperties)

			dt = cr.getPMPersonInformation(PIN, True)
			ViewState("PIN") = PIN
			loadGridParticipant(dt)
			loadGridParticipantApps(dtApplications)
			Me.populateDocuments(dtDocuments)
			populateProperties(ApplicationProperties)

		Catch ex As Exception

		End Try
	End Sub

	Protected Sub loadGridParticipantApps(dt As DataTable)

		Try

			ViewState("ApplicationList") = dt
			Me.gridParticipantApps.DataSource = dt
			gridParticipantApps.DataBind()

			spRowCount.Visible = True
			spRowCount.InnerText = "" & dt.Rows.Count & " Row(s) Affected"

			If dt.Rows.Count > 10 Then
				Me.pnlGridApplication.Height = Nothing
			Else
				Me.pnlGridApplication.Height = 500
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub


	Protected Sub loadGridParticipant(dt As DataTable)

		Try


			Me.gridParticipant.DataSource = dt
			gridParticipant.DataBind()

			'If dt.Rows.Count > 10 Then
			'	Me.pnlGrid.Height = Nothing
			'Else
			'	Me.pnlGrid.Height = 500
			'End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub gridParticipant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridParticipant.SelectedIndexChanged

		Try

			Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtApplications As New DataTable
			Dim selectedRowIndex As Integer

			selectedRowIndex = Me.gridParticipant.SelectedRow.RowIndex

			Dim row As GridViewRow = gridParticipant.Rows(selectedRowIndex)

			'locking the record for review for the user
			'cr.PMLocKRecord(row.Cells(2).Text.ToString(), Session("user"))
			dtApplications = cr.PMgetApplicationByPIN(row.Cells(1).Text.ToString(), "")
			ViewState("ApplicationList") = dtApplications
			loadGridParticipantApps(dtApplications)

			'dt = cr.PMgetApplicationByCode(row.Cells(2).Text.ToString())



			'getting submitted documents per application 
			' dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CInt(row.Cells(2).Text.ToString().Split("-")(1)))
			'	dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CStr(row.Cells(2).Text.ToString()))

			'ViewState("ApplicationCode") = row.Cells(2).Text.ToString
			'ViewState("PIN") = row.Cells(4).Text.ToString

			'getting customer's personal information details
			'dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())


			'ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(2).Text.ToString(), row.Cells(4).Text.ToString())

			'Session("lodgmentProperties") = ApplicationProperties

			'population the grid to the left for other application information
			'populateProperties(ApplicationProperties)

			'population the grid at the bottom for submitted required application documents
			'Me.populateDocuments(dtDocuments)




			'Dim dDetails As New List(Of ApplicationDocumentDetail), i As Integer = 0
			'Do While i < dtDocuments.Rows.Count

			'     Dim dDetail As New ApplicationDocumentDetail
			'     dDetail.DocumentTypeName = dt.Rows(i).Item("txtDocumentName")
			'     dDetail.DocumentLocation = dt.Rows(i).Item("DocumentPath").ToString
			'     dDetails.Add(dDetail)
			'     i = i + 1
			'Loop
			'ViewState("DocumentDetails") = dDetails



			'If ApplicationProperties.Count < 10 Then
			'	pnlLeftGrid.Height = 400
			'Else
			'	pnlLeftGrid.Height = Nothing
			'End If
		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try


	End Sub

	Protected Sub gridParticipantApps_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridParticipantApps.PageIndexChanging

		Dim dtApproval As New DataTable
		Try

			gridParticipantApps.PageIndex = e.NewPageIndex
			dtApproval = ViewState("ApplicationList")

			If IsNothing(dtApproval) = False Then
				loadGridParticipantApps(dtApproval)
			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub gridParticipantApps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridParticipantApps.SelectedIndexChanged


		Try

			Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
			Dim selectedRowIndex As Integer

			selectedRowIndex = Me.gridParticipantApps.SelectedRow.RowIndex

			Dim row As GridViewRow = gridParticipantApps.Rows(selectedRowIndex)

			'getting submitted documents per application 
			dtDocuments = cr.PMgetSubmittedDocument(CStr(row.Cells(5).Text.ToString()), CStr(row.Cells(1).Text.ToString()))

			ViewState("ApplicationCode") = row.Cells(2).Text.ToString
			ViewState("PIN") = CStr(row.Cells(5).Text.ToString())

			'getting customer's personal information details
			dtPDetails = cr.getPMPersonInformation(CStr(row.Cells(5).Text.ToString()))

			dt = cr.PMgetApplicationByCode(row.Cells(1).Text.ToString())

			ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(1).Text.ToString(), CStr(row.Cells(5).Text.ToString()))

			Dim aDetails As New ApplicationProperties

			
			Session("lodgmentProperties") = ApplicationProperties

			'population the grid to the left for other application information
			populateProperties(ApplicationProperties)

			'population the grid at the bottom for submitted required application documents
			Me.populateDocuments(dtDocuments)

			If ApplicationProperties.Count < 10 Then
				pnlLeftGrid.Height = 400
			Else
				pnlLeftGrid.Height = Nothing
			End If
		Catch ex As Exception
			MsgBox("" & ex.Message)
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

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub getApplicationStatus()

		ddApplicationStatus.Items.Add("")
		ddApplicationStatus.Items.Add("ALL")
		ddApplicationStatus.Items.Add("Entry")
		ddApplicationStatus.Items.Add("Documentation")
		ddApplicationStatus.Items.Add("Processing")
		ddApplicationStatus.Items.Add("Confirmation")
		ddApplicationStatus.Items.Add("Send To Pencom")
		ddApplicationStatus.Items.Add("Sent To Pencom")
		ddApplicationStatus.Items.Add("Approved/Processing")
		ddApplicationStatus.Items.Add("Paid")


	End Sub

	Protected Sub getApprovalTypes()

		Dim i As Integer = 0
		Dim lstAppTypes As New List(Of String)
		lstAppTypes = getApprovalType()
		ddApplicationType.Items.Clear()
		Do While i < lstAppTypes.Count

			If ddApplicationType.Items.Count = 0 Then
				ddApplicationType.Items.Add("")
				ddApplicationType.Items.Add("ALL")
				ddApplicationType.Items.Add(lstAppTypes.Item(i))
			ElseIf ddApplicationType.Items.Count > 0 Then
				ddApplicationType.Items.Add(lstAppTypes.Item(i))
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
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtusers As New DataTable

		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(imgExport)

		If IsPostBack = False Then

			If IsNothing(Session("user")) = True Then

				Response.Redirect("Login.aspx")

			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				getApprovalTypes()
				getApplicationStatus()
				getUserAccessMenu(Session("user"))

				If Not Context.Request.QueryString("PIN") Is Nothing Then

					Me.txtFindPIN.Text = Context.Request.QueryString("PIN")
					LoadPIN(Me.txtFindPIN.Text)
				Else

				End If

			Else

			End If

		End If


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


	Private Sub getApplicationList()

		spRowCount.Visible = False

		Try
			Dim dt As New DataTable, cr As New Core

			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

			If Me.chkSentToPencomDate.Checked = True Then

				dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), ddApplicationStatus.SelectedItem.Text, 1, 0)

			Else

				dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), ddApplicationStatus.SelectedItem.Text, 0, 0)

			End If


			'If chkProcessedWithin.Checked = True And Me.chkPrevProcessedWithin.Checked = True Then

			'	dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), ddApplicationStatus.SelectedItem.Text, 1, 1)

			'ElseIf chkProcessedWithin.Checked = False And Me.chkPrevProcessedWithin.Checked = True Then

			'	dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), ddApplicationStatus.SelectedItem.Text, 0, 1)

			'ElseIf chkProcessedWithin.Checked = False And Me.chkPrevProcessedWithin.Checked = False Then

			'	dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), ddApplicationStatus.SelectedItem.Text, 0, 0)

			'ElseIf chkProcessedWithin.Checked = True And Me.chkPrevProcessedWithin.Checked = False Then

			'	dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), ddApplicationStatus.SelectedItem.Text, 1, 0)

			'End If

			ViewState("ApplicationList") = dt

			If IsNothing(dt) = False Then

				loadGridParticipantApps(dt)

			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub loadGrid(dt As DataTable)

		Try



			gridParticipantApps.DataSource = dt
			gridParticipantApps.DataBind()

			'If dt.Rows.Count > 10 Then
			'	Me.pnlGrid.Height = Nothing
			'Else
			'	Me.pnlGrid.Height = 500
			'End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	
	Protected Sub btnFindByDate_Click(sender As Object, e As EventArgs) Handles btnFindByDate.Click
		getApplicationList()
	End Sub

	Protected Sub gridParticipantApps_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gridParticipantApps.SelectedIndexChanging

		

	End Sub

	Protected Sub imgExport_Click(sender As Object, e As ImageClickEventArgs) Handles imgExport.Click

		Try

			Dim dt As New DataTable, cr As New Core

			If IsNothing(ViewState("ApplicationList")) = False Then

				dt = ViewState("ApplicationList")

				'Dim dtDocuments = New DataTable, i As Integer, dtColumn As DataColumn
				'dtColumn = New DataColumn("ApplicationCode")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("FullName")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("PIN")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("EmployerName")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("ApplicationType")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("ApplicationDate")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("Status")
				'dtDocuments.Columns.Add(dtColumn)
				'dtColumn = New DataColumn("DocumentSource")
				'dtDocuments.Columns.Add(dtColumn)


				'Do While i < dt.Rows.Count

				'	Dim newCustomersRow As DataRow
				'	newCustomersRow = dtDocuments.NewRow()
				'	newCustomersRow("ApplicationCode") = dt.Rows(i).Item("txtApplicationCode")
				'	newCustomersRow("FullName") = dt.Rows(i).Item("txtFullName").ToString.Replace("|", "")
				'	newCustomersRow("PIN") = dt.Rows(i).Item("txtPIN").ToString
				'	newCustomersRow("EmployerName") = dt.Rows(i).Item("txtEmployerName").ToString
				'	newCustomersRow("ApplicationType") = dt.Rows(i).Item("ApprovalType").ToString
				'	newCustomersRow("ApplicationDate") = dt.Rows(i).Item("dteApplicationDate")
				'	newCustomersRow("Status") = dt.Rows(i).Item("txtStatus")
				'	newCustomersRow("DocumentSource") = dt.Rows(i).Item("txtdocumentSource")

				'	dtDocuments.Rows.Add(newCustomersRow)
				'	i = i + 1
				'Loop

				cr.ExtractCSV(dt, "ApplicationList")

			Else

			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

	End Sub
End Class
