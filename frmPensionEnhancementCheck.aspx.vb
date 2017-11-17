Imports System.Data

Partial Class frmPensionEnhancementCheck
	Inherits System.Web.UI.Page

	Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click


		For Each grow As GridViewRow In Me.gridApplications.Rows


			Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkPINReviewChecked"), CheckBox)

			If cb.Enabled = True Then
				cb.Checked = True
			Else
			End If

		Next

	End Sub
	Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click



		For Each grow As GridViewRow In Me.gridApplications.Rows


			Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkPINReviewChecked"), CheckBox)

			If cb.Enabled = True Then
				cb.Checked = False
			Else
			End If

		Next

	End Sub

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim cr As New Core, dt As DataTable
		dt = cr.PMgetPencomEnhancement(CDate(txtRunDate.Text))
		BindGrid(dt)

	End Sub
	Protected Sub calRunDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRunDate.SelectionChanged
		Me.calRunDate_PopupControlExtender.Commit(Me.calRunDate.SelectedDate)
	End Sub
	Protected Sub BindGrid(dt As DataTable)

		Me.gridApplications.DataSource = dt
		Me.gridApplications.DataBind()
		If dt.Rows.Count > 10 Then

			pnlGrid.Height = Nothing

		Else

		End If


	End Sub
	Protected Sub gridExport_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
		'Dim dtt As New DataTable
		'If IsNothing(ViewState("BatchApprovals")) = False Then
		'	Dim dt As DataTable = ViewState("BatchApprovals")

		'	If e.Row.RowType = DataControlRowType.DataRow Then

		'		'gridExport_OnRowDataBound

		'		'If ((dt.Rows(e.Row.RowIndex).Item("txtControlCheckedBy"))).ToString.Trim <> "" Then
		'		If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteChecked")) = False Then

		'			Dim cbChecked As CheckBox = TryCast(e.Row.FindControl("ChkPINApprovalChecked"), CheckBox)
		'			cbChecked.Checked = True
		'			cbChecked.Enabled = False
		'		End If
		'		'e.Row.ForeColor = System.Drawing.Color.Blue

		'		' If ((dt.Rows(e.Row.RowIndex).Item("txtControlVerifiedBy"))).ToString.Trim <> "" Then
		'		If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteVerified")) = False Then

		'			'e.Row.ForeColor = System.Drawing.Color.Green
		'			Dim cbVerified As CheckBox = TryCast(e.Row.FindControl("ChkPINApprovalVerified"), CheckBox)
		'			cbVerified.Checked = True
		'			cbVerified.Enabled = False

		'		End If

		'		'If ((dt.Rows(e.Row.RowIndex).Item("txtControlAuthorisedBy"))).ToString.Trim <> "" Then
		'		If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteAuthorised")) = False Then

		'			Dim cbAuthorised As CheckBox = TryCast(e.Row.FindControl("ChkPINApprovalAuthorised"), CheckBox)
		'			cbAuthorised.Checked = True
		'			cbAuthorised.Enabled = False

		'		End If


		'		If CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Rejected" Then
		'			e.Row.ForeColor = System.Drawing.Color.Red

		'		ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Confirmed" Then

		'			e.Row.ForeColor = Drawing.Color.Green

		'		ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Tentative" Then

		'			e.Row.ForeColor = Drawing.Color.BlueViolet

		'		ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Open" Then



		'		Else
		'		End If




		'	End If
		'End If

	End Sub

	Public Sub PMUpdateApprovalControlCheck(pin As String, runDate As Date, uName As String)
		Try

			Dim db As New DbConnection, dt As DataTable, cr As New Core
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblPensionEnhancement set  iscontrolchecked = 1 ,txtControlCheckedBy = '" & uName & "', dteControlChecked = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where dteRunFor = '" & DateTime.Parse(runDate).ToString("yyyy-MM-dd") & "' and txtPIN = '" & pin & "'  "
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()


			dt = cr.PMgetPencomEnhancement(CDate(txtRunDate.Text))
			BindGrid(dt)

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try
	End Sub

	Protected Sub btnChecked_Click(sender As Object, e As EventArgs) Handles btnChecked.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, dt As New DataTable

		Try
			'If IsNothing(Session("user")) = True Then
			'	Response.Redirect("Login.aspx")
			'Else



			For Each grow As GridViewRow In Me.gridApplications.Rows

				cb = grow.FindControl("ChkPINReviewChecked")

				If cb.Checked = True And cb.Enabled = True Then
					PMUpdateApprovalControlCheck(grow.Cells(2).Text, CDate(Me.txtRunDate.Text), "o-taiwo")
				Else
				End If

			Next



			dt = cr.PMgetPencomEnhancement(CDate(txtRunDate.Text))
			BindGrid(dt)


			'Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
			'dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True)
			'ViewState("BatchApprovals") = dt
			'BindGrid(dt)
			'End If

		Catch ex As Exception


		End Try

	End Sub

	Protected Sub gridApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.SelectedIndexChanged

	End Sub
End Class
