Imports System.Data
Partial Class frmRMU

	Inherits System.Web.UI.Page


	Protected Sub populatedData()

		Me.ddSector.Items.Add("")
		Me.ddSector.Items.Add("Private")
		Me.ddSector.Items.Add("Public")

		Me.ddAge.Items.Add("")
		Me.ddAge.Items.Add("20 & Above")
		Me.ddAge.Items.Add("30 & Above")
		Me.ddAge.Items.Add("40 & Above")
		Me.ddAge.Items.Add("50 & Above")
		Me.ddAge.Items.Add("60")
		Me.ddAge.Items.Add("65")
		Me.ddAge.Items.Add("60 & Above")
		Me.ddAge.Items.Add("70")
		Me.ddAge.Items.Add("70 & Above")
		Me.ddAge.Items.Add("80 & Above")
		Me.ddAge.Items.Add("All")

		Me.ddYearsOfService.Items.Add("")
		Me.ddYearsOfService.Items.Add("20 & Above")
		Me.ddYearsOfService.Items.Add("30 & Above")
		Me.ddYearsOfService.Items.Add("33")
		Me.ddYearsOfService.Items.Add("34")
		Me.ddYearsOfService.Items.Add("35")
		Me.ddYearsOfService.Items.Add("40 & Above")
		Me.ddYearsOfService.Items.Add("50 & Above")
		Me.ddYearsOfService.Items.Add("All")

		Dim cr As New Core, i As Integer, dt As New DataTable, dtEligibility As New DataTable

		dt = cr.PMgetTitles()
		Do While i < dt.Rows.Count

			'TitleID,Value

			If Me.ddTitle.Items.Count = 0 Then
				Me.ddTitle.Items.Add("")
				Me.ddTitle.Items.Add(dt.Rows(i).Item("Value") & "			       	      |" & dt.Rows(i).Item("TitleID"))
			Else
				Me.ddTitle.Items.Add(dt.Rows(i).Item("Value") & "			       	      |" & dt.Rows(i).Item("TitleID"))
			End If

			i = i + 1

		Loop

		'dtEligibility = cr.PMEligibilityList()
		'ViewState("Eligibility") = dtEligibility

		If dtEligibility.Rows.Count > 0 Then
			mpEligibityPopup.Show()
		Else
		End If


	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtusers As New DataTable
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(imgExportExcel)

		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(btnDownLoadList)

		'btnDownLoadList

		Try

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					Dim cr As New Core
					'dtusers = Session("userDetails")
					getUserAccessMenu(Session("user"))

					populatedData()
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



	Protected Sub gridSoonToRetiree_RowDataBound()

	End Sub

	Protected Sub BtnViewSI_Click()

	End Sub

	Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
		Dim str As String = ""
		'where isretired = 0 and firstemploymentdate is not null 

		'"& CDate(Me.txtDate.Text) &"'

		'MsgBox("" & DateTime.Parse(txtDate.Text).ToString("yyyy-MM-dd"))
		'Exit Sub


		If Me.ddAge.Text > "" And str = "" Then

			If ddAge.Text.Split("&").Length > 1 Then
				'str = str & " where datediff(year,dateofbirth,getdate()) >= '" & CInt(Me.ddAge.Text.Split("&")(0).Trim.ToString) & "'" & " and isretired = 0"
				str = str & " where datediff(month,dateofbirth,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 >= '" & CInt(Me.ddAge.Text.Split("&")(0).Trim.ToString) & "'" & " and isretired = 0"
			Else
				str = str & " where datediff(month,dateofbirth,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 = '" & CInt(Me.ddAge.Text.Trim.ToString) & "'" & " and isretired = 0"

				'str = str & " where datediff(year,dateofbirth, '"" & DateTime.Parse('" & Me.txtDate.Text & "') & ""') = '" & CInt(Me.ddAge.Text.Trim.ToString) & "'" & " and isretired = 0"
			End If


		ElseIf Me.ddAge.Text > "" And str <> "" Then

			If ddAge.Text.Split("&").Length > 1 Then
				str = str & " or datediff(month,dateofbirth,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 >= '" & CInt(Me.ddAge.Text.Split("&")(0).Trim.ToString) & "'"
			Else
				str = str & " or datediff(month,dateofbirth,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 = '" & CInt(Me.ddAge.Text.Trim.ToString) & "'"
			End If


		Else

		End If


		If Me.ddTitle.Text > "" And str = "" Then

			str = str & " where (select value from titles where titleID = a.titleID) = '" & Me.ddTitle.Text.Split("|")(0).Trim.ToString & "' " & " and isretired = 0"
		ElseIf Me.ddTitle.Text > "" And str <> "" Then

			str = str & " or (select value from titles where titleID = a.titleID) = '" & Me.ddTitle.Text.Split("|")(0).Trim.ToString & "'"
		Else

		End If


		If Me.ddYearsOfService.Text > "" And str = "" Then

			If ddYearsOfService.Text.Split("&").Length > 1 Then
				str = str & " where datediff(month,firstemploymentdate,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 >= '" & Me.ddYearsOfService.Text.Split("&")(0).Trim.ToString & "'" & " and isretired = 0"
			Else
				str = str & " where datediff(month,firstemploymentdate,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 = '" & Me.ddYearsOfService.Text.Trim.ToString & "'" & " and isretired = 0"
			End If


		ElseIf Me.ddYearsOfService.Text > "" And str <> "" Then

			If ddYearsOfService.Text.Split("&").Length > 1 Then
				str = str & " or datediff(month,firstemploymentdate,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 >= '" & Me.ddYearsOfService.Text.Split("&")(0).Trim.ToString & "'" & " and firstemploymentdate is not null "
			Else
				str = str & " or datediff(month,firstemploymentdate,'" & DateTime.Parse(Me.txtDate.Text).ToString("yyyy-MM-dd") & "')/12 = '" & Me.ddYearsOfService.Text.Trim.ToString & "'" & " and firstemploymentdate is not null "
			End If

		Else

		End If


		If Me.ddSector.Text > "" And str = "" Then

			str = str & " where (select  case when substring(EmployerCode,1,2) = 'PU' then 'Public' when substring(EmployerCode,1,2) = 'PR' then 'Private' else 'Others' end from employer where employerid = a.employerid) = '" & Me.ddSector.Text.ToString & "'" & " and isretired = 0"
		ElseIf Me.ddSector.Text > "" And str <> "" Then

			str = str & " or (select  case when substring(EmployerCode,1,2) = 'PU' then 'Public' when substring(EmployerCode,1,2) = 'PR' then 'Private' else 'Others' end from employer where employerid = a.employerid) = '" & Me.ddSector.Text.ToString & "'"

		Else

		End If

		Dim cr As New Core
		If str <> "" Then
			Dim dt As New DataTable
			dt = cr.PMgetSOONToRetiree(str)
			ViewState("dt") = dt
			loadGrid(dt)
		Else

		End If


	End Sub

	Protected Sub loadGrid(dt As DataTable)


		Try


			If dt.Rows.Count > 10 Then

				Me.pnlSoonToRetiree.Height = Nothing
			Else
				Me.pnlSoonToRetiree.Height = 300
			End If
			'pnlGrid

			Me.gridSoonToRetiree.DataSource = dt
			gridSoonToRetiree.DataBind()
		Catch ex As Exception

		End Try

	End Sub

	Protected Sub gridSoonToRetiree_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridSoonToRetiree.PageIndexChanging

		If IsNothing(ViewState("dt")) = False Then

			Dim dt As New DataTable
			Me.gridSoonToRetiree.PageIndex = e.NewPageIndex
			dt = ViewState("dt")
			loadGrid(dt)

		Else
		End If

	End Sub


	Protected Sub gridSoonToRetiree_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSoonToRetiree.SelectedIndexChanged

	End Sub

	Protected Sub imgExportExcel_Click(sender As Object, e As ImageClickEventArgs) Handles imgExportExcel.Click

		Dim dt As New DataTable, cr As New Core

		If IsNothing(ViewState("dt")) = False Then

			dt = ViewState("dt")

			If dt.Rows.Count > 0 Then

				cr.ExtractCSV(dt, "SoonToRetiree")
			Else

				cr.ExtractCSV(dt, "SoonToRetiree")

			End If

		Else

		End If

	End Sub


	Protected Sub btnDownLoadList_Click(sender As Object, e As EventArgs) Handles btnDownLoadList.Click
		'btnDownLoadList
		'Dim dt As New DataTable, cr As New Core
		'dt = ViewState("Eligibility")
		'cr.ExtractCSV(dt, "Eligiblility_List")

		SendNotifiCation()

	End Sub

	Protected Sub SendNotifiCation()
		'Protected Sub SendNotifiCation(appDetail As ApplicationDetail)

		Dim em As New EmailGateway.EmailGateway, dtComments As New DataTable
		Dim lstEmails As New List(Of EmailGateway.EmailAddress)
		Dim lstEmail As New EmailGateway.EmailAddress, cr As New Core

		Try
			'retrieving all the comment on the application
			'dtComments = cr.PMgetApplicationComment(appDetail.ApplicationID, "PRE")


			lstEmails = cr.PMgetEscalationEmail(Session("user").ToString, 1)

			'If cr.IsValidEmailAddress(appDetail.Email) = True Then

			'builds the email addresses to send the acknowledgment mail to
			'If IsNothing(lstEmails) = True Then
			'lstEmails = New List(Of EmailGateway.EmailAddress)

			'Else
			'End If

			'lstEmail.EmailAddress = appDetail.Email
			'lstEmail.Reciever = "o-taiwo@leadway-pensure.com"
			'lstEmails.Add(lstEmail)


			lstEmail = New EmailGateway.EmailAddress
			'lstEmail.EmailAddress = Session("user") & "@leadway-pensure.com"
			lstEmail.EmailAddress = "o-taiwo@leadway-pensure.com"
			lstEmail.Reciever = False
			lstEmails.Add(lstEmail)


			lstEmail = New EmailGateway.EmailAddress
			lstEmail.EmailAddress = "o-taiwo@leadway-pensure.com"
			lstEmail.Reciever = False
			lstEmails.Add(lstEmail)


			'building the e-mail notification to be sent to the application logger

			'em.sendMailWithOutAttachmentAddess("Test", "Benefit Application Notification - " & appDetail.PIN, lstEmails)
			em.sendMailWithOutAttachmentAddess("Test", "Benefit Application Notification - ", lstEmails)

			'Else
			'End If
		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message & "Error Send Email Notification")

		End Try


	End Sub

End Class
