Imports System.Data

Partial Class frmApplicationPaymentReport
	Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable
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

	Protected Sub gridApplication_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("ApplicationList")) = False Then

			Dim dt As DataTable = ViewState("ApplicationList")
			If e.Row.RowType = DataControlRowType.DataRow Then

				' rowIndex = Convert.ToInt32(gridApplication.DataKeys(e.Row.RowIndex).Value)
				'objAL.Add(rowIndex)

				If CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = True Then

					Dim cb As CheckBox
					cb = e.Row.FindControl("chkApplication")
					cb.Checked = True
					e.Row.ForeColor = System.Drawing.Color.Green
					e.Row.Enabled = True

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsControlChecked")) = False Then


				End If

			End If
			'ViewState("SELECTED_ROWS") = objAL
		Else
		End If

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
	Protected Sub ViewDocumentDetails_Click()

	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtUsers As New DataTable
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(imgDownloadSoft)
		Try

			If IsPostBack = False Then

				'	If IsNothing(Session("user")) = True Then

				'		Response.Redirect("Login.aspx")

				'	ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				getApprovalTypes()
				'dtUsers = Session("userDetails")
				'getUserAccessMenu(Session("user"))

				'	End If

			Else
				'getUserAccessMenu(Session("user"))

			End If





		Catch ex As Exception

		End Try

	End Sub
	Protected Sub populateViewType()

		ddPaymentStatus.Items.Add("")
		ddPaymentStatus.Items.Add("Confirmation")
		ddPaymentStatus.Items.Add("Send To Pencom")
		ddPaymentStatus.Items.Add("Approved/Processing")
		ddPaymentStatus.Items.Add("Sent To Pencom")
		ddPaymentStatus.Items.Add("Entry")
		ddPaymentStatus.Items.Add("Documentation")
		ddPaymentStatus.Items.Add("Processing")

	End Sub

	Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click

		getApplicationList()

	End Sub


	Private Sub getApplicationList()

		Try
			Dim dt As New DataTable, cr As New Core

			If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

				dt = cr.PMgetPaidApplication(Me.txtStartDate.Text, Me.txtEndDate.Text, (ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), 2, Me.ddPaymentStatus.SelectedItem.Text)

			Else
			End If

			If IsNothing(dt) = False Then
				Me.loadGrid(dt)
			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub loadGrid(dt As DataTable)

		Try


			gridApplication.DataSource = dt
			gridApplication.DataBind()

			If dt.Rows.Count > 10 Then

				Me.pnlGrid.Height = Nothing

			Else
				Me.pnlGrid.Height = 500
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub gridApplication_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplication.SelectedIndexChanged


		Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
		Dim selectedRowIndex As Integer
		txtApplicationComment.Text = ""

		selectedRowIndex = Me.gridApplication.SelectedRow.RowIndex

		Dim row As GridViewRow = gridApplication.Rows(selectedRowIndex)

		dt = cr.PMgetApplicationByCode(row.Cells(1).Text.ToString())

		'getting submitted documents per application 
		'dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CInt(row.Cells(2).Text.ToString().Split("-")(1)))
		dtDocuments = cr.PMgetSubmittedDocument(row.Cells(3).Text.ToString(), CStr(row.Cells(1).Text.ToString()))

		ViewState("ApplicationCode") = row.Cells(1).Text.ToString
		ViewState("PIN") = row.Cells(3).Text.ToString

		'getting customer's personal information details
		dtPDetails = cr.getPMPersonInformation(row.Cells(3).Text.ToString())

		ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(1).Text.ToString(), row.Cells(3).Text.ToString())

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

End Class
