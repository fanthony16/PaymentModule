Imports MailFilter

Partial Class frmRMASSMS
	Inherits System.Web.UI.Page

	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Dim mf As New MailFliter, lstSub As New List(Of String), dateCount As Integer = 0
		mf.Usernames = "rmassms"
		mf.Passwords = "bonus+3aa"
		mf.Domains = "pensure-nigeria.com"
		mf.Urls = "rmassms@leadway-pensure.com"
		mf.subject = "Daily"
		mf.texts = "validated successfully"
		mf.day = (-1 * 7)
		lstSub = mf.getValidationFeedback
		Me.Label1.Text = lstSub.Count

	End Sub
End Class
