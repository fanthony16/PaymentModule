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

		If Me.chkSourceType.Checked = True Then

			Dim cr As New Core, dt As DataTable
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
			'LoadPIN(str)

			dt = cr.PMgetPencomEnhancementMultiplePIN(CDate(txtRunDate.Text), Server.MapPath("~/Logs"), "IC", str)

			'ViewState("dtEnhancement") = dt

			BindGrid(dt)

		Else

			Dim crr As New Core, dtt As DataTable
			dtt = crr.PMgetPencomEnhancement(CDate(txtRunDate.Text), Server.MapPath("~/Logs"), "IC")
			BindGrid(dtt)

		End If






		






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


			dt = cr.PMgetPencomEnhancement(CDate(txtRunDate.Text), Server.MapPath("~/Logs"), "IC")
			BindGrid(dt)

		Catch ex As Exception

			Dim loger As New Global.Logger.Logger
			loger.FileSource = "Sure Pay Benefit Application "
			loger.FilePath = Server.MapPath("~/Logs")
			loger.Logger(ex.Message & "Core - Error on Internal Control's Review")

		End Try
	End Sub

	Protected Sub btnChecked_Click(sender As Object, e As EventArgs) Handles btnChecked.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, dt As New DataTable

		Try
			If IsNothing(Session("user")) = True Then
				Response.Redirect("Login.aspx")
			Else

				For Each grow As GridViewRow In Me.gridApplications.Rows

					cb = grow.FindControl("ChkPINReviewChecked")

					If cb.Checked = True And cb.Enabled = True Then
						PMUpdateApprovalControlCheck(grow.Cells(2).Text, CDate(Me.txtRunDate.Text), Session("user"))
					Else
					End If

				Next

				dt = cr.PMgetPencomEnhancement(CDate(txtRunDate.Text), Server.MapPath("~/Logs"), "IC")
				BindGrid(dt)

				'Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
				'dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True)
				'ViewState("BatchApprovals") = dt
				'BindGrid(dt)
			End If

		Catch ex As Exception


		End Try

	End Sub

	Protected Sub gridApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.SelectedIndexChanged

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

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim dtusers As New DataTable

		If IsPostBack = False Then



			If IsNothing(Session("user")) = True Then

				'   getApprovalType()
				Response.Redirect("Login.aspx")
			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				dtusers = Session("userDetails")
				getUserAccessMenu(Session("user"))

			End If
		End If



	End Sub

	Protected Sub btnFindPIN_Click(sender As Object, e As EventArgs) Handles btnFindPIN.Click

	End Sub
End Class
