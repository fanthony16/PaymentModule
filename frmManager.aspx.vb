Imports System.Data

Partial Class frmManager
	Inherits System.Web.UI.Page

	Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

		Try
			Dim dt As New DataTable, dtAVGAge As New DataTable, dtAppAge As New DataTable, dtTopApps As New DataTable, dtApps As New DataTable, dtPaidApps As New DataTable, dtAppSource As New DataTable, dtLocationSummary As New DataTable, cr As New Core

			dt = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 1)

			If dt.Rows.Count > 0 Then

				'  spApplicationCount.InnerText = dt.Rows(0).Item("All")

				Me.lblTotalAppCount.InnerText = CInt(dt.Rows(0).Item("TotalApp"))
				Me.lblPercentTotalApp.InnerText = Math.Round((CInt(dt.Rows(0).Item("TotalApp")) / CInt(dt.Rows(0).Item("TotalApp"))) * 100, 1).ToString & "%"

				Me.lblTotalAppSentCount.InnerText = CInt(dt.Rows(0).Item("TotalSenToPencom"))
				Me.lblPercentTotalAppSent.InnerText = Math.Round((CInt(dt.Rows(0).Item("TotalSenToPencom")) / CInt(dt.Rows(0).Item("TotalApp"))) * 100, 1).ToString & "%"

				Me.lblTotalAppApproved.InnerText = CInt(dt.Rows(0).Item("TotalApproved"))
				Me.lblPercentTotalAppApproved.InnerText = Math.Round((CInt(dt.Rows(0).Item("TotalApproved")) / CInt(dt.Rows(0).Item("TotalApp"))) * 100, 1).ToString & "%"


				Me.lblTotalAppPaid.InnerText = CInt(dt.Rows(0).Item("Paid"))
				Me.lblPercentTotalAppPaid.InnerText = Math.Round((CInt(dt.Rows(0).Item("Paid")) / CInt(dt.Rows(0).Item("TotalApp"))) * 100, 1).ToString & "%"

			Else
			End If

			dtAVGAge = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 2)
			BindGridAVGAge(dtAVGAge)

			dtAppAge = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 3)
			ViewState("dtAppAge") = dtAppAge
			BindGridAgeApp(dtAppAge)


			dtTopApps = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 5)


			dtPaidApps = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 4)
			ViewState("dtPaidApps") = dtPaidApps
			BindChartPaid(dtPaidApps)


			dtAppSource = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 6)
			ViewState("dtAppSource") = dtAppSource
			BindChartAppSource(dtAppSource)


			'ViewState("dtPaidApps") = dtPaidApps
			'BindChartPaid(dtPaidApps)


			dtApps = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 7)
			BindGridAppSummary(dtApps)


			dtLocationSummary = cr.PMGetManagerReport(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), 8)
			ViewState("ApplicationLocationSummary") = dtLocationSummary
			BindGridLocationSummary(dtLocationSummary)


		Catch ex As Exception

		End Try

	End Sub

	Protected Sub BindGridAVGAge(dt As DataTable)

		Try

			Me.gridApplicationAvgAge.DataSource = dt
			Me.gridApplicationAvgAge.DataBind()

			If dt.Rows.Count < 10 Then
				pnlValidatdEmail.Height = 400
			Else
				pnlValidatdEmail.Height = Nothing
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub BindGridAgeApp(dt As DataTable)

		Try

			Me.gridAgeApps.DataSource = dt
			Me.gridAgeApps.DataBind()

			If dt.Rows.Count < 10 Then
				pnlValidatdEmail.Height = 400
			Else
				pnlValidatdEmail.Height = Nothing
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	'gridLocationSummary

	Protected Sub BindGridLocationSummary(dt As DataTable)

		Try

			Me.gridLocationSummary.DataSource = dt
			Me.gridLocationSummary.DataBind()

			If dt.Rows.Count < 10 Then
				pnlLocationSummary.Height = 400
			Else
				pnlLocationSummary.Height = Nothing
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub


	Protected Sub BindGridAppSummary(dt As DataTable)

		Try

			Me.gridRecievedApps.DataSource = dt
			Me.gridRecievedApps.DataBind()

			If dt.Rows.Count < 10 Then
				pnlValidatdEmail.Height = 400
			Else
				pnlValidatdEmail.Height = Nothing
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub BindChartPaid(dt As DataTable)

		Try

			Me.chartApplicationPaid.Series("SeriesAppPaid").XValueMember = "AppType"
			Me.chartApplicationPaid.Series("SeriesAppPaid").YValueMembers = "AppTotal"

			Me.chartApplicationPaid.DataSource = dt
			Me.chartApplicationPaid.DataBind()

			'If dt.Rows.Count < 10 Then
			'	pnlValidatdEmail.Height = 400
			'Else
			'	pnlValidatdEmail.Height = Nothing
			'End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub BindChartAppSource(dt As DataTable)

		Try

			Me.ChartAppSource.Series("SeriesAppSource").XValueMember = "Source"
			Me.ChartAppSource.Series("SeriesAppSource").YValueMembers = "AppCount"
			Me.ChartAppSource.Series("SeriesAppSource").IsValueShownAsLabel = True
			Me.ChartAppSource.Series("SeriesAppSource").IsVisibleInLegend = True




			Me.ChartAppSource.DataSource = dt
			Me.ChartAppSource.DataBind()

			'If dt.Rows.Count < 10 Then
			'	pnlValidatdEmail.Height = 400
			'Else
			'	pnlValidatdEmail.Height = Nothing
			'End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub


	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged

		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)

	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged

		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)

	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

	End Sub



	Protected Sub gridManagerBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("dtAppAge")) = False Then

			Dim dt As DataTable = ViewState("dtAppAge")
			If e.Row.RowType = DataControlRowType.DataRow Then
				

				If dt.Rows(e.Row.RowIndex).Item("txtStatus").ToString = "Entry" Then

					e.Row.ForeColor = System.Drawing.Color.Red

				ElseIf dt.Rows(e.Row.RowIndex).Item("txtStatus").ToString = "Documentation" Then

					e.Row.ForeColor = System.Drawing.Color.BlueViolet

				Else

					e.Row.ForeColor = System.Drawing.Color.Green

				End If

			End If
		Else
		End If

	End Sub



	Protected Sub gridLocationSummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridLocationSummary.PageIndexChanging

		If IsNothing(ViewState("ApplicationLocationSummary")) = False Then

			Dim dt, dtAppSource, dtPaidApps As New DataTable
			Me.gridLocationSummary.PageIndex = e.NewPageIndex
			dt = ViewState("ApplicationLocationSummary")
			BindGridLocationSummary(dt)


			dtAppSource = ViewState("dtAppSource")
			BindChartAppSource(dtAppSource)


			dtPaidApps = ViewState("dtPaidApps")
			BindChartPaid(dtPaidApps)


		Else
		End If

	End Sub
End Class
