Imports System.Data

Partial Class frmGeneralReport
	Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
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
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, scriptManagerC As New ScriptManager, dtusers As New DataTable

		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(ddReportType)

		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(imgExport)

		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(btnFindByDate)



		If IsPostBack = False Then

			If IsNothing(Session("user")) = True Then

				Response.Redirect("Login.aspx")

			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				getApprovalTypes()
				Me.ddReportType.Items.Add("")
				Me.ddReportType.Items.Add("Application Recieved and Processed in Same Month of Report")
				Me.ddReportType.Items.Add("Application Recieved and Processed in previous Month of Report")
				Me.ddReportType.Items.Add("Application Processed SMS Report")
				Me.ddReportType.Items.Add("Periodic Application List for PW-Annuity conversion Report")
				Me.ddReportType.Items.Add("Paid Periodic PW-Annuity Application report")
				Me.ddReportType.Items.Add("List of Retirees on PW (periodic)")
				Me.ddReportType.Items.Add("Summary of Application Recieved")
				Me.ddReportType.Items.Add("Unprocessed Approvals")
				Me.ddReportType.Items.Add("UnRecieved Approvals")
				Me.ddReportType.Items.Add("pencom benefit report")

				getUserAccessMenu(Session("user"))

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
			'aryAccessModule.Add(dtAccessModule.Rows(i).Item(1))
			' MsgBox("" & dtAccessModule.Rows(i).Item(0).ToString)
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


	Protected Sub ddReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddReportType.SelectedIndexChanged

		Try
			Dim dt As New DataTable, cr As New Core, appTypeID As Integer

			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

			appTypeID = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))

			'dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

			'ViewState("ApplicationList") = dt

			'If ddReportType.SelectedIndex = 1 Then
			'	dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 1)
			'	cr.ExtractCSV(dt, "ApplicationProcessedSameMonth")
			'ElseIf ddReportType.SelectedIndex = 2 Then
			'	dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 2)
			'	cr.ExtractCSV(dt, "ApplicationProcessedPreviousMonth")
			'ElseIf ddReportType.SelectedIndex = 3 Then

			'End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub btnFindByDate_Click(sender As Object, e As EventArgs) Handles btnFindByDate.Click
		Dim cr As New Core, dt As New DataTable

		''''generating SMS Sent report by application date

		If ddReportType.SelectedIndex = 3 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 3)
			ViewState("ApplicationList") = dt
			loadGridParticipantApps(dt)

		ElseIf ddReportType.SelectedIndex = 4 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 4)
			ViewState("ApplicationList") = dt
			cr.ExtractCSV(dt, "PW_To_Annuity")
			'loadGridParticipantApps(dt)

		ElseIf ddReportType.SelectedIndex = 5 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 5)
			ViewState("ApplicationList") = dt
			cr.ExtractCSV(dt, "Fresh_Annuity")
			'loadGridParticipantApps(dt)

		ElseIf ddReportType.SelectedIndex = 6 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 6)
			ViewState("ApplicationList") = dt
			'loadGridParticipantApps(dt)
			cr.ExtractCSV(dt, "Retiree on PW")

		ElseIf ddReportType.SelectedIndex = 7 Then

			If ddApplicationType.SelectedItem.Text = "ALL" Then

				dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 7)
				cr.ExtractCSV(dt, "ApplicationSummary")

				'	loadGridParticipantApps(dt)

			ElseIf ddApplicationType.SelectedItem.Text <> "ALL" Then

				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
				dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), (CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)) + 100))
				cr.ExtractCSV(dt, ddApplicationType.SelectedItem.Text)

			End If

		ElseIf ddReportType.SelectedIndex = 10 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 10)
			cr.ExtractCSV(dt, "Benefit Pencom Report")

		ElseIf ddReportType.SelectedIndex = 9 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 9)
			cr.ExtractCSV(dt, "UnRecieved Approvals")

		ElseIf ddReportType.SelectedIndex = 8 Then

			dt = cr.PMgetApplicationSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 8)
			cr.ExtractCSV(dt, "UnProcessed Approvals")

		End If




	End Sub

	Protected Sub loadGridParticipantApps(dt As DataTable)

		Try

			Me.gridParticipantApps.DataSource = dt
			gridParticipantApps.DataBind()

			If dt.Rows.Count > 10 Then
				Me.pnlGridApplication.Height = Nothing
			Else
				Me.pnlGridApplication.Height = 500
			End If

		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try

	End Sub


	Protected Sub imgExport_Click(sender As Object, e As ImageClickEventArgs) Handles imgExport.Click
		Dim cr As New Core, dt As New DataTable
		Try

			dt = ViewState("ApplicationList")

			If dt.Rows.Count > 0 Then
				cr.ExtractCSV(dt, "ApplicationList")
			Else
			End If



		Catch ex As Exception

		End Try
		
	End Sub
End Class
