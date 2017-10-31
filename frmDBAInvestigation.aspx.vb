Imports System.Data
Partial Class frmDBAInvestigation
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		If IsPostBack = False Then

			If IsNothing(Session("user")) = True Then
				Response.Redirect("Login.aspx")
			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				If Not Context.Request.QueryString("ApplicationCode") Is Nothing Then

					Dim appCode As String
					appCode = Context.Request.QueryString("ApplicationCode")
					ViewState("appCode") = appCode
					getInvestigationReport(appCode)
				End If

			End If

		Else

		End If

	End Sub

	Protected Sub getInvestigationReport(AppCode As String)

		Dim dt As New DataTable, cr As New Core
		dt = cr.PMgetDBAInvestigationReport(AppCode)

		If dt.Rows.Count > 0 Then

			Me.txtPIN.Text = dt.Rows(0).Item("txtPIN").ToString
			txtFullName.Text = dt.Rows(0).Item("txtFullName").ToString.Replace("|", "")
			Me.txtInvestigatorName.Text = dt.Rows(0).Item("txtInvestigationBy").ToString
			Me.txtLocation.Text = dt.Rows(0).Item("txtLocation").ToString
			Me.txtNOKAddress.Text = dt.Rows(0).Item("txtNOKAddress").ToString
			Me.txtNOKNames.Text = dt.Rows(0).Item("txtNOKName").ToString
			Me.txtNOKTelephone.Text = dt.Rows(0).Item("txtNOKTelephone").ToString

			Me.txtRepnderAddy1.Text = dt.Rows(0).Item("txtResponderAddress1").ToString
			Me.txtRepnderAddy2.Text = dt.Rows(0).Item("txtResponderAddress2").ToString
			Me.txtRepnderAddy3.Text = dt.Rows(0).Item("txtResponderAddress3").ToString

			Me.txtRepnderName.Text = dt.Rows(0).Item("txtResponderName1").ToString
			Me.txtRepnderName2.Text = dt.Rows(0).Item("txtResponderName2").ToString
			Me.txtRepnderName3.Text = dt.Rows(0).Item("txtResponderName3").ToString

			Me.txtSubject.Text = dt.Rows(0).Item("txtPurpose").ToString
			Me.txtNOKTelephone.Text = dt.Rows(0).Item("txtNOKPhone").ToString

			Me.txtInvestigationDate.Text = dt.Rows(0).Item("dteInvestigation").ToString()
			Me.txtReport.Text = dt.Rows(0).Item("txtDescription").ToString()
		Else

			dt = cr.PMgetApplicationByCode(AppCode)
			Me.txtPIN.Text = dt.Rows(0).Item("txtPIN").ToString
			txtFullName.Text = dt.Rows(0).Item("txtFullName").ToString.Replace("|", "")

		End If


		

	End Sub

	Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
		Dim DBInvestigation As New ApplicationDBAInvestigation, cr As New Core

		DBInvestigation.ApplicationCode = ViewState("appCode")
		DBInvestigation.CreatedBy = Session("user")
		DBInvestigation.DateCreated = CDate(Me.txtInvestigationDate.Text)
		DBInvestigation.Description = Me.txtReport.Text

		DBInvestigation.Purpose = Me.txtSubject.Text

		DBInvestigation.InvestigationBy = Me.txtInvestigatorName.Text

		DBInvestigation.Location = Me.txtLocation.Text
		DBInvestigation.NOKAddress = Me.txtNOKAddress.Text
		DBInvestigation.NOKName = Me.txtNOKNames.Text
		DBInvestigation.NOKPhone = Me.txtNOKTelephone.Text

		DBInvestigation.ResponderAddress1 = Me.txtRepnderAddy1.Text
		DBInvestigation.ResponderAddress2 = Me.txtRepnderAddy2.Text
		DBInvestigation.ResponderAddress3 = Me.txtRepnderAddy3.Text

		DBInvestigation.ResponderName1 = Me.txtRepnderName.Text
		DBInvestigation.ResponderName2 = Me.txtRepnderName2.Text
		DBInvestigation.ResponderName3 = Me.txtRepnderName3.Text


		If cr.PMgetDBAInvestigationReport(ViewState("appCode")).Rows.Count > 0 Then

			cr.PMSubmitDBAInvestigationReport(DBInvestigation, "U")
			
		Else
			'MsgBox("" & DateTime.Parse(CDate(Me.txtInvestigationDate.Text).ToString("yyyy-MM-dd")))
			'Exit Sub
			cr.PMSubmitDBAInvestigationReport(DBInvestigation, "I")

		End If





	End Sub

	Protected Sub calInvestigationDate_SelectionChanged(sender As Object, e As EventArgs) Handles calInvestigationDate.SelectionChanged

		Me.calInvestigationDate_PopupControlExtender.Commit(Me.calInvestigationDate.SelectedDate)

	End Sub
End Class
