Imports System.Data
Partial Class frmStandingPensionerConfirm
    Inherits System.Web.UI.Page

	

	Protected Sub BindGrid(dt As DataTable)

		Try

			Me.gridPensioner.DataSource = dt
			Me.gridPensioner.DataBind()

			If dt.Rows.Count < 10 Then
				pnlValidatdEmail.Height = 400
			Else
				pnlValidatdEmail.Height = Nothing
			End If
			spRowCount.InnerText = dt.Rows.Count & " Row(s) Affected !"
			dvSPRowCount.Visible = True

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub btnConfirmSI_Click(sender As Object, e As EventArgs) Handles btnConfirmSI.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

		Try

			For Each grow As GridViewRow In Me.gridPensioner.Rows
				cb = grow.FindControl("ChkPensionerChecked")

				If cb.Checked = True Then
					cr.PMUpdateNewPensionserList("A", grow.Cells(1).Text, Session("user"), grow.Cells(11).Text)

				End If

			Next

			BindGrid(cr.PMgetPendingNewPensionsers("F", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text)))

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub btnViewRecords_Click(sender As Object, e As EventArgs) Handles btnViewRecords.Click

		Dim cr As New Core, dt As New DataTable
		'dt = cr.PMgetNewPensionsers(Me.txtStartDate.Text, Me.txtEndDate.Text)
		'If IsNothing(dt) = False Then
		'	BindGrid(dt)
		'Else
		dt = cr.PMgetPendingNewPensionsers("F", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))
		ViewState("dt") = dt
		BindGrid(dt)


	End Sub

	Protected Sub gridPensioner_RowDataBound()

	End Sub
	Protected Sub BtnViewDetails_Click()

	End Sub
	Protected Sub BtnCancelApplication_Click()

	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridPensioner.Rows

			grow.FindControl("ChkPensionerChecked")
			cb = grow.FindControl("ChkPensionerChecked")
			cb.Checked = True

		Next

	End Sub

	Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridPensioner.Rows

			grow.FindControl("ChkPensionerChecked")
			cb = grow.FindControl("ChkPensionerChecked")
			cb.Checked = False

		Next

	End Sub

	Protected Sub gridPensioner_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridPensioner.PageIndexChanging


		If IsNothing(ViewState("dt")) = False Then

			Dim dt As New DataTable
			Me.gridPensioner.PageIndex = e.NewPageIndex
			dt = ViewState("dt")
			BindGrid(dt)

		Else
		End If

	End Sub

	Protected Sub gridPensioner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridPensioner.SelectedIndexChanged

		

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

			j = 0
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
		Try

			Dim scriptManagerA, scriptManagerB, scriptManagerC As New ScriptManager, dtusers As New DataTable
			scriptManagerA = ScriptManager.GetCurrent(Me.Page)
			scriptManagerA.RegisterPostBackControl(imgExportExcel)


			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					Dim cr As New Core
					'dtusers = Session("userDetails")
					getUserAccessMenu(Session("user"))
					'populateBank()
					'populateFrequency()
				End If

			Else

				getUserAccessMenu(Session("user"))

			End If



		Catch ex As Exception

		End Try
	End Sub

	Protected Sub gridPensioner_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gridPensioner.SelectedIndexChanging



	End Sub

	Protected Sub imgExportExcel_Click(sender As Object, e As ImageClickEventArgs) Handles imgExportExcel.Click

		Dim dt As New DataTable, cr As New Core


		If IsNothing(ViewState("dt")) = False Then

			dt = ViewState("dt")

			If dt.Rows.Count > 0 Then

				cr.ExtractCSV(dt, "NewPensioners")
			Else

				cr.ExtractCSV(dt, "NewPensioners")

			End If

		Else

		End If

		

	End Sub
End Class
