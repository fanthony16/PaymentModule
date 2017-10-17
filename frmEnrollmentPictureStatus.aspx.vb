Imports System.Data
Imports System.Security.Cryptography

Partial Class frmEnrollmentPictureStatus
	Inherits System.Web.UI.Page

	Protected Sub getMissingImageEnrollments(date1 As Date, date2 As Date)

		Dim cr As New Core, dt As New DataTable, lstUpdatedablePINs As New List(Of String), i As New Integer

		Try
			dt = cr.PMEGetEVAuditMissingRecords(date1, date2)

			Do While i < dt.Rows.Count

				Dim imgByte() As Byte, pictureHashValue As String
				imgByte = dt.Rows(i).Item("Picture")
				pictureHashValue = generateHash(imgByte)
				If pictureHashValue = "61I+8PsJHH7KN8B9XXQ0IQ==" Then

					lstUpdatedablePINs.Add(dt.Rows(i).Item("RSAPIN").ToString)

				Else
				End If

				i = i + 1

			Loop

			i = 0
			Do While i < lstUpdatedablePINs.Count

				cr.PMEUpdateEnpowerBiometrics(lstUpdatedablePINs.Item(i), Session("user"))
				i = i + 1

			Loop

			getEnrollments(date1, date2)

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub getEnrollments(date1 As Date, date2 As Date)

		Dim cr As New Core, dt As DataTable
		Dim dtDocuments = New DataTable, i As Integer, dtColumn As DataColumn, lstUpdatedablePINs As New List(Of String)
		Try

			dt = cr.PMEGetEVAuditMissingRecords(date1, date2)
			ViewState("EnrollmentDetail") = dt

			If dt.Rows.Count > 0 Then

				dtColumn = New DataColumn("IsEAuditAvailable")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("IsImageAvailable")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("PIN")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("FullName")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("PinnedDate")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("Comments")
				dtDocuments.Columns.Add(dtColumn)

				Do While i < dt.Rows.Count

					Dim imgByte() As Byte, pictureHashValue As String
					imgByte = dt.Rows(i).Item("Picture")
					pictureHashValue = generateHash(imgByte)

					Dim newCustomersRow As DataRow
					newCustomersRow = dtDocuments.NewRow()

					newCustomersRow("IsEAuditAvailable") = dt.Rows(i).Item("IsRecordAvailable")

					'If pictureHashValue = "Rvj97DUfIEXcPrY4d+om5g==" Then
					'	newCustomersRow("IsImageAvailable") = "False"
					'	newCustomersRow("Comments") = "Missing Image"

					'Else

					If pictureHashValue = "61I+8PsJHH7KN8B9XXQ0IQ==" Then
						newCustomersRow("IsImageAvailable") = "False"
						newCustomersRow("Comments") = "Missing Image"

					Else
						newCustomersRow("IsImageAvailable") = "True"
						newCustomersRow("Comments") = dt.Rows(i).Item("comment").ToString
					End If

					newCustomersRow("PIN") = dt.Rows(i).Item("RSAPIN").ToString
					newCustomersRow("FullName") = dt.Rows(i).Item("FullName").ToString
					newCustomersRow("PinnedDate") = CDate(dt.Rows(i).Item("RSAPINRegistrationDate")).ToString("yyyy-MM-dd")


					dtDocuments.Rows.Add(newCustomersRow)

					

					i = i + 1

				Loop


			Else
			End If
			ViewState("EnrollmentDetail") = dtDocuments
			Me.BindEnrollmentDetail(dtDocuments)

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click


		Try

			'getEnrollments(Me.txtStartDate.Text, Me.txtEndDate.Text)

			getMissingImageEnrollments(Me.txtStartDate.Text, Me.txtEndDate.Text)

		Catch ex As Exception

		End Try

	End Sub


	'handles the click event to view the participant image on PSA
	Protected Sub ViewPSAPassport_Click(sender As Object, e As EventArgs) Handles btnShowPassportPopup.Click
		Try
			Dim btnViewPassport As New ImageButton, pin As String
			btnViewPassport = sender
			Dim i As GridViewRow, cr As New Core

			i = btnViewPassport.NamingContainer

			pin = Me.gridApplications.Rows(i.RowIndex).Cells(3).Text

			imgPassport.ImageUrl = String.Format("PSAImageHandler.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", pin, 1, Server.MapPath("~/Logs"))

			'pops up the comment dialogue
			mpEnrollmentPassport.Show()
		Catch ex As Exception

		End Try




	End Sub

	'handles the click event to update psa image with enpower image of the participant
	Protected Sub UpdateEnpowerPassport_Click(sender As Object, e As EventArgs) Handles btnShowPassportPopup.Click
		Try
			

			Dim btnViewPassport As New ImageButton, pin As String
			btnViewPassport = sender
			Dim i As GridViewRow, cr As New Core

			i = btnViewPassport.NamingContainer
			pin = Me.gridApplications.Rows(i.RowIndex).Cells(3).Text

			If Not IsNothing(Session("user")) = True Then
				cr.PMEUpdateEnpowerBiometrics(pin, Session("user"))
			Else
			End If



			getEnrollments(Me.txtStartDate.Text, Me.txtEndDate.Text)
			'pops up the comment dialogue
			'mpEnrollmentPassport.Show()
		Catch ex As Exception

		End Try

	End Sub

	


	'handles the click event of the comment button on the grid
	Protected Sub ViewPassport_Click(sender As Object, e As EventArgs) Handles btnShowPassportPopup.Click
		Try
			Dim btnViewPassport As New ImageButton, pin As String
			btnViewPassport = sender
			Dim i As GridViewRow, cr As New Core

			i = btnViewPassport.NamingContainer

			pin = Me.gridApplications.Rows(i.RowIndex).Cells(3).Text

			imgPassport.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", pin, 1, Server.MapPath("~/Logs"))

			'pops up the comment dialogue
			mpEnrollmentPassport.Show()
		Catch ex As Exception

		End Try
		



	End Sub

	Protected Sub gridExport_OnRowDataBound()

	End Sub
	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub gridApplications_PageIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.PageIndexChanged



	End Sub

	Protected Sub gridApplications_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridApplications.PageIndexChanging
		Dim dt As New DataTable
		If IsNothing(ViewState("EnrollmentDetail")) = False Then
			dt = ViewState("EnrollmentDetail")
			Me.gridApplications.PageIndex = e.NewPageIndex
			Me.BindEnrollmentDetail(dt)
		Else
		End If

	End Sub
	Protected Sub BindEnrollmentDetail(dt As DataTable)

		


		gridApplications.DataSource = dt
		gridApplications.DataBind()







		If dt.Rows.Count > 10 Then
			Me.pnlGrid.Height = Nothing
		Else

		End If


	End Sub

	Protected Sub imgDownloadSoft_Click(sender As Object, e As ImageClickEventArgs) Handles imgDownloadSoft.Click

		Try

			Dim dt As New DataTable, cr As New Core
			If IsNothing(ViewState("EnrollmentDetail")) = False Then
				dt = ViewState("EnrollmentDetail")
				cr.ExtractCSV(dt, "PinnedEnrollment")
			Else
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

	Public Sub EntryPoint2()

		'get the hash of the default image
		'compare the hash of the default image with the hash of all the saved images
		'where true get image from PSA to update enpower.

		Try

			Dim conn As New SqlClient.SqlConnection, command As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim db As New DbConnection

			conn = db.getConnection("Enpowerv4")

			Dim cmdSelect = New SqlClient.SqlCommand("select imagebyte from LegacyTestImage", conn)
			Dim barrImg As Byte() = cmdSelect.ExecuteScalar()

			daUser.SelectCommand = cmdSelect
			daUser.SelectCommand.CommandType = CommandType.Text
			daUser.Fill(dsUser, "defaultPicture")
			dtUser = dsUser.Tables("defaultPicture")

			conn.Close()

			Dim pictureHashValue As String

			pictureHashValue = generateHash(barrImg)

			'	updateEnpowerBiometrics(getDefaultBiometricPIN(pictureHashValue))

		Catch ex As Exception

		End Try


	End Sub

	Public Function generateHash(pByte() As Byte) As String

		Dim hash As String

		hash = Convert.ToBase64String(MD5.Create.ComputeHash(pByte))

		Return LTrim(RTrim(hash))

	End Function










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

		Else
			getUserAccessMenu(Session("user"))
		End If

	End Sub
End Class
