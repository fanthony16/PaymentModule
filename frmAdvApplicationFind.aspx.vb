Imports System.Data
Imports System.IO

Partial Class frmAdvApplicationFind
	Inherits System.Web.UI.Page
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

				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then
					e.Row.ForeColor = System.Drawing.Color.Blue
					'e.Row.Enabled = False

				End If

			End If
		Else
		End If

	End Sub

	Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewApplicationLog As New ImageButton, appCode As String, cr As New Core, appName As String
		btnViewApplicationLog = sender
		Dim i As GridViewRow
		i = btnViewApplicationLog.NamingContainer
		appCode = Me.gridParticipantApps.Rows(i.RowIndex).Cells(1).Text
		appName = Me.gridParticipantApps.Rows(i.RowIndex).Cells(2).Text

		Dim typeID As Integer

		If Not IsNothing(ViewState("PIN")) = True Then

			If appCode.Split("-").Count > 1 Then

				typeID = cr.PMgetApprovalTypeByCode(appCode.Split("-")(0)).Rows(0).Item(0)

			Else
				Select Case appName
					Case Is = "25% Lumpsum Withdrawal"
						typeID = 2
					Case Else

				End Select

			End If



			Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}&PIN={4}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 0, Server.UrlEncode("AdvApplicationFind"), Server.UrlEncode(ViewState("PIN"))))

			'	Context.Response.Write("<script language='javascript'>window.open('frmEditApplication.aspx?ApplicationCode=" & ViewState("PIN") & "','_blank');</script>")

			'Page.ClientScript.RegisterStartupScript(Me.GetType, "OpenWindow", "window.open('frmApplication.aspx','_newtab')")

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

		LoadPIN(Me.txtFindPIN.Text)

	End Sub
	Protected Sub LoadPIN(PIN As String)
		Try
			Dim cr As New Core, dt As DataTable, dtApplications As New DataTable, dtDocuments As New DataTable, ApplicationProperties As New List(Of ApplicationProperties)

			dt = cr.getPMPersonInformation(PIN)
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


			Me.gridParticipantApps.DataSource = dt
			gridParticipantApps.DataBind()

			If dt.Rows.Count > 5 Then
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
			dtDocuments = cr.PMgetSubmittedDocument(Me.txtFindPIN.Text, CStr(row.Cells(1).Text.ToString()))

			ViewState("ApplicationCode") = row.Cells(1).Text.ToString
			ViewState("PIN") = Me.txtFindPIN.Text

			'getting customer's personal information details
			dtPDetails = cr.getPMPersonInformation(txtFindPIN.Text)

			dt = cr.PMgetApplicationByCode(row.Cells(1).Text.ToString())
			ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(1).Text.ToString(), txtFindPIN.Text)
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

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager, scriptManagerb As New ScriptManager, dtusers As New DataTable
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

		scriptManagerb = ScriptManager.GetCurrent(Me.Page)
		scriptManagerb.RegisterPostBackControl(Me.gridParticipantApps)

		If IsPostBack = False Then

			If IsNothing(Session("user")) = True Then

				Response.Redirect("Login.aspx")

			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				getUserAccessMenu(Session("user"))
				Me.ddFundPlatform.Items.Clear()
				Me.ddFundPlatform.Items.Add("")
				Me.ddFundPlatform.Items.Add("RSA")
				Me.ddFundPlatform.Items.Add("RETIREE")

				If Not Context.Request.QueryString("PIN") Is Nothing Then

					Me.txtFindPIN.Text = Context.Request.QueryString("PIN")
					LoadPIN(Me.txtFindPIN.Text)
				Else

				End If

			Else

				Me.ddFundPlatform.Items.Clear()
				Me.ddFundPlatform.Items.Add("")
				Me.ddFundPlatform.Items.Add("RSA")
				Me.ddFundPlatform.Items.Add("RETIREE")

			End If
		Else

			

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

	Protected Sub btnUpdateShelve_Click(sender As Object, e As EventArgs) Handles btnUpdateShelve.Click


		Try
			If Not IsNothing(ViewState("ApplicationCode")) = True And Not IsNothing(Session("user")) = True Then
				Dim cr As New Core

				If Me.chkIsDocRecieved.Checked = True Then
					cr.PMUpdateApplicationShelveNo(CStr(ViewState("ApplicationCode")), Session("user"), UCase(Me.txtShelveNo.Text), 1)
				ElseIf Me.chkIsDocRecieved.Checked = False Then
					cr.PMUpdateApplicationShelveNo(CStr(ViewState("ApplicationCode")), Session("user"), UCase(Me.txtShelveNo.Text), 0)
				End If

				Me.txtShelveNo.Text = ""
				Me.chkIsDocRecieved.Checked = False
			Else

			End If
		Catch ex As Exception

		End Try



	End Sub

	Protected Sub btnUpdateFundPlatform_Click(sender As Object, e As EventArgs) Handles btnUpdateFundPlatform.Click
		Dim cr As New Core, fundType As Integer


		If Not IsNothing(ViewState("ApplicationCode")) = True Then

			If Me.ddFundPlatform.SelectedItem.ToString = "RSA" Then
				fundType = 1
				cr.PMUpdateFundPlatform(CStr(ViewState("ApplicationCode")), fundType)

			ElseIf Me.ddFundPlatform.SelectedItem.ToString = "RETIREE" Then
				fundType = 2
				cr.PMUpdateFundPlatform(ViewState("ApplicationCode"), fundType)

			End If


			Dim dtDocuments, dtPDetails, dt As New DataTable, ApplicationProperties As New List(Of ApplicationProperties)
			'getting submitted documents per application 
			dtDocuments = cr.PMgetSubmittedDocument(Me.txtFindPIN.Text, CStr(ViewState("ApplicationCode")))

			'getting customer's personal information details
			dtPDetails = cr.getPMPersonInformation(txtFindPIN.Text)

			dt = cr.PMgetApplicationByCode(CStr(ViewState("ApplicationCode")))
			ApplicationProperties = cr.PMgetApplicationDetails(CStr(ViewState("ApplicationCode")), txtFindPIN.Text)
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














		Else

		End If

		



	End Sub
End Class
