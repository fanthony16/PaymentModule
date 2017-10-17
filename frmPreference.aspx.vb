Imports System.Data

Partial Class frmPreference
	Inherits System.Web.UI.Page

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
		Dim cr As New Core, intFile As Integer, intDMS As Integer

		If Me.chkFileSystem.Checked = True Then
			intFile = 1
		Else
			intFile = 0
		End If

		If Me.chkDMS.Checked = True Then
			intDMS = 1
		Else
			intDMS = 0
		End If

		cr.PMUpdatePreferences(intFile, intDMS)

	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim dtAppPreference As DataTable, cr As New Core
		Try

			If IsPostBack = False Then


				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")


				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					getUserAccessMenu(Session("user"))
					dtAppPreference = cr.PMgetApplicationPreference
					If dtAppPreference.Rows(0).Item("blnIsDMSFileStorage") = True Then
						Me.chkDMS.Checked = True
					End If
					If dtAppPreference.Rows(0).Item("blnIsFileSystemStorage") = True Then
						Me.chkFileSystem.Checked = True
					End If

				End If

			Else

				getUserAccessMenu(Session("user"))

			End If

		Catch ex As Exception

		End Try


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

End Class
