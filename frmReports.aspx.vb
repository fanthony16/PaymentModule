Imports System.Data

Partial Class frmReports
	Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtusers As New DataTable

		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(imgExport)

		If IsPostBack = False Then

			If IsNothing(Session("user")) = True Then

				Response.Redirect("Login.aspx")

			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				getApprovalTypes()

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

		Try
			Dim dt As New DataTable, cr As New Core

			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			dt = cr.PMgetApplicationLifeCycle(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

			ViewState("ApplicationList") = dt

			If IsNothing(dt) = False Then

				loadGridParticipantApps(dt)

			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub loadGridParticipantApps(dt As DataTable)

		Try

			'	MsgBox("" & dt.Rows.Count)

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


	Protected Sub getApprovalTypes()

		Dim i As Integer = 0
		Dim lstAppTypes As New List(Of String)
		lstAppTypes = getApprovalType()
		ddApplicationType.Items.Clear()
		Do While i < lstAppTypes.Count

			If ddApplicationType.Items.Count = 0 Then
				ddApplicationType.Items.Add("")
				'ddApplicationType.Items.Add("ALL")
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

	Protected Sub btnFindByDate_Click(sender As Object, e As EventArgs) Handles btnFindByDate.Click

		getApplicationList()

	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub imgExport_Click(sender As Object, e As ImageClickEventArgs) Handles imgExport.Click

		Dim cr As New Core, dt As New DataTable
		dt = ViewState("ApplicationList")
		cr.ExtractCSV(dt, "ApplicationList")

	End Sub
End Class
