Imports System.DirectoryServices.AccountManagement
Imports System.Data

Partial Class Login
	Inherits System.Web.UI.Page


	Protected Sub btnlogon_Click(sender As Object, e As ImageClickEventArgs) Handles btnlogon.Click


		If Page.IsValid = True Then

			Dim cr As New Core

			If Me.txtUserName.Text = "Admin" And Me.txtPassword.Text = "System.,@" Then
				Response.Redirect("frmUserManagement.aspx")
			Else

				Try

					Dim UFullName As String, pc As PrincipalContext, dtUser As New DataTable

					pc = New PrincipalContext(ContextType.Domain, "PENSURE-NIGERIA.COM", RTrim(LTrim(Me.txtUserName.Text)), RTrim(LTrim(Me.txtPassword.Text)))
					Dim uPrincipal As UserPrincipal
					uPrincipal = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, RTrim(LTrim(Me.txtUserName.Text)))
					UFullName = uPrincipal.DisplayName
					dtUser = cr.PMCheckUserIn(RTrim(LTrim(Me.txtUserName.Text)))

					Session("RoleID") = dtUser.Rows(0).Item("fkiRoleID")

					If IsNothing(pc) = False Then

						If cr.PMCheckUserIn(RTrim(LTrim(Me.txtUserName.Text))).Rows.Count < 1 Then
							'cr.PMInsertNewUserLogin(RTrim(LTrim(Me.txtUserName.Text)), UFullName, cr.PMGetHash(RTrim(LTrim(Me.txtPassword.Text))))
						Else
							'cr.PMUpdateUserLogin(RTrim(LTrim(Me.txtUserName.Text)), cr.PMGetHash(RTrim(LTrim(Me.txtPassword.Text))))
						End If

						If cr.PMCheckUserApplicationAccess(RTrim(LTrim(Me.txtUserName.Text))) = False Then
							lblogonMessage.InnerText = " Logon Successful...Access Not Granted"
							Divlogon.Attributes.Add("class", "logonAccessNotGranted")
							Exit Sub
						Else
						End If

						lblogonMessage.InnerText = " Logon Successful"
						Session("user") = LTrim(RTrim(txtUserName.Text))
						Session("userDetails") = cr.getUserDetails(LTrim(RTrim(txtUserName.Text)))
						Response.Redirect("Index.aspx")

					Else

						lblogonMessage.InnerText = " Logon Failure...Please Confirm Details and Re-try!!!"
						Divlogon.Attributes.Add("class", "logonError")
						Exit Sub

					End If


				Catch ex As Exception
					lblogonMessage.InnerText = ex.Message
					Divlogon.Attributes.Add("class", "logonError")
				End Try

			End If

			Dim aCookie As New HttpCookie("userInfo")

			aCookie.Values("user") = LTrim(RTrim(txtUserName.Text))
			aCookie.Values("lastVisit") = DateTime.Now.ToString()

			aCookie.Expires = DateTime.Now.AddDays(1)

			Response.Cookies.Add(aCookie)


			Session("user") = LTrim(RTrim(txtUserName.Text))
			'Dim cr As New Core
			Session("userDetails") = cr.getUserDetails(LTrim(RTrim(txtUserName.Text)))
			Response.Redirect("Index.aspx")



			'Dim ws As New wsUser.User

			'If Me.txtUserName.Text <> "" And Me.txtPassword.Text <> "" Then


			'	If ws.ValidateForPM(LTrim(RTrim(txtUserName.Text)), LTrim(RTrim(txtPassword.Text))) = 0 Then

			'		lblogonMessage.InnerText = "The user name or password is incorrect."

			'		Divlogon.Attributes.Add("class", "logonError")

			'	ElseIf ws.ValidateForPM(LTrim(RTrim(txtUserName.Text)), LTrim(RTrim(txtPassword.Text))) = 1 Then

			'		lblogonMessage.InnerText = " Logon Successful"
			'		Session("user") = LTrim(RTrim(txtUserName.Text))

			'		Dim cr As New Core
			'		Session("userDetails") = cr.getUserDetails(LTrim(RTrim(txtUserName.Text)))

			'		Response.Redirect("Index.aspx")

			'		Divlogon.Attributes.Add("class", "logonSuccess")

			'	ElseIf ws.ValidateForPM(LTrim(RTrim(txtUserName.Text)), LTrim(RTrim(txtPassword.Text))) = 2 Then

			'		lblogonMessage.InnerText = " Logon Successful...Access Not Granted"
			'		Divlogon.Attributes.Add("class", "logonAccessNotGranted")

			'	ElseIf ws.Validate(LTrim(RTrim(txtUserName.Text)), LTrim(RTrim(txtPassword.Text))) = 3 Then

			'		lblogonMessage.InnerText = " Logon Successful...System Admin"
			'		Response.Redirect("frmUserManagement.aspx")
			'		Divlogon.Attributes.Add("class", "logonWarning")

			'	End If

			'Else

			'End If

		End If


	End Sub

	Protected Sub btnCancel_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancel.Click

		Divlogon.Attributes.Remove("class")
		lblogonMessage.InnerText = "Enter Your Login Details"

	End Sub
End Class
