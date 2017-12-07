Imports System.Data
Partial Class index
    Inherits System.Web.UI.Page
	Protected Sub gridApplicationSummary_RowDataBound()

	End Sub
     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim dtusers As New DataTable, dtAppPreference As New DataTable, cr As New Core


		Dim appStr = "ANN-003"

		If appStr.Substring(0, 3) = "ANN" Then
			MsgBox("" & appStr.Substring(0, 3))
		ElseIf appStr.Substring(0, 3) = "LPW" Then
			MsgBox("" & appStr.Substring(0, 3))
		End If




          If Page.IsPostBack = False Then

               If IsNothing(Session("user")) = True Then

                    Response.Redirect("Login.aspx")

               ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				getUserAccessMenu(Session("user"))
				'getting the application preference setting form the DB
				dtAppPreference = cr.PMgetApplicationPreference()
				ViewState("dtAppPreference") = dtAppPreference
				SendNotifiCation()

				If IsNothing(Session("RoleID")) = False And CInt(Session("RoleID")) = 3014 Then

					Me.gridApplicationSummary.DataSource = cr.PMgetApplicationSummaryByStage("Documentation")
					gridApplicationSummary.DataBind()
					mpApplicationSummary.Show()

				End If

			Else

			End If

          End If

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


	Protected Sub SendNotifiCation()
		'Protected Sub SendNotifiCation(appDetail As ApplicationDetail)

		Dim em As New EmailGateway.EmailGateway, dtComments As New DataTable, dtPrefernce As New DataTable
		Dim lstEmails As New List(Of EmailGateway.EmailAddress), emialAddys As New List(Of EmailGateway.EmailAddress), emialARLAddys As New List(Of EmailGateway.EmailAddress)
		Dim lstEmail As New EmailGateway.EmailAddress, cr As New Core, datediff As Integer = 0, dayss As Integer

		Try
			'retrieving all the comment on the application
			'dtComments = cr.PMgetApplicationComment(appDetail.ApplicationID, "PRE")

			dtPrefernce = ViewState("dtAppPreference")

			If dtPrefernce.Rows.Count > 0 And dtPrefernce.Rows(0).Item("dteLastNotificationSent").ToString <> "" Then

				Dim lastSentDate As Date = CDate(dtPrefernce.Rows(0).Item("dteLastNotificationSent"))

				Dim aryEmail As Array = dtPrefernce.Rows(0).Item("txtEligibilityNotificationAddresses").ToString.Split(","), i As Integer
				Dim aryARLEmail As Array = dtPrefernce.Rows(0).Item("txtARLNotification").ToString.Split(",")

				Do While i < aryEmail.Length
					Dim emialAddy As New EmailGateway.EmailAddress
					emialAddy.EmailAddress = aryEmail(i)
					emialAddy.Reciever = True
					emialAddys.Add(emialAddy)
					i = i + 1
				Loop


				'building emailAdress for ARL Applications
				i = 0
				Do While i < aryARLEmail.Length
					Dim emialARLAddy As New EmailGateway.EmailAddress
					emialARLAddy.EmailAddress = aryARLEmail(i)
					emialARLAddy.Reciever = True
					emialARLAddys.Add(emialARLAddy)
					i = i + 1
				Loop


				Dim dt1 As DateTime = lastSentDate
				Dim dt2 As DateTime = Now.Date
				Dim ts As TimeSpan = dt2 - dt1
				dayss = ts.Days


				i = 1

				Do While i <= dayss

					Dim dtEligibility As New DataTable

					Dim today As System.DateTime = lastSentDate
					Dim answer As System.DateTime

					'today = System.DateTime.Now
					answer = today.AddDays(i)

					dtEligibility = cr.PMEligibilityList(answer, Server.MapPath("~/Logs"))

					If dtEligibility.Rows.Count > 1 Then

						lstEmails = New List(Of EmailGateway.EmailAddress)
						lstEmails = emialAddys

						lstEmail = New EmailGateway.EmailAddress
						lstEmail.EmailAddress = "o-taiwo@leadway-pensure.com"
						lstEmail.Reciever = False
						lstEmails.Add(lstEmail)

						'building the e-mail notification to be sent to the application logger

						em.sendMailWithOutAttachmentAddess(mailMsg(dtEligibility, answer), "Benefit Application Eligibility Notification", lstEmails)

					Else

					End If

					i = i + 1
					cr.PMUpdatePreference(answer)


					If i = dayss Then


						Dim dtARLNotification As New DataTable
						dtARLNotification = cr.PMAgedARL(Server.MapPath("~/Logs"))

						If dtARLNotification.Rows.Count > 0 Then

							lstEmails = New List(Of EmailGateway.EmailAddress)
							lstEmails = emialARLAddys

							lstEmail = New EmailGateway.EmailAddress
							lstEmail.EmailAddress = "o-taiwo@leadway-pensure.com"
							lstEmail.Reciever = False
							lstEmails.Add(lstEmail)

							'building the e-mail notification to be sent to the application logger

							em.sendMailWithOutAttachmentAddess(mailMsgARL(dtARLNotification), "Benefit Application Aged ARL Acknowledgment Notification", lstEmails)

						Else

						End If

					Else

					End If


				Loop



				



				'aryARLEmail

			Else

			End If


			'	Dim dtARL As New DataTable
			'	dtARL = cr.PMARLNotification(Server.MapPath("~/Logs"))

			'Dim aryEmaill As Array = dtPrefernce.Rows(0).Item("txtARLNotification").ToString.Split(","), j As Integer
			'lstEmails = New List(Of EmailGateway.EmailAddress)

			'Do While i < aryEmail.Length
			'	Dim emialAddy As New EmailGateway.EmailAddress
			'	emialAddy.EmailAddress = aryEmail(i)
			'	emialAddy.Reciever = True
			'	lstEmails.Add(emialAddy)
			'	i = i + 1
			'Loop


			'lstEmail = New EmailGateway.EmailAddress
			'lstEmail.EmailAddress = "o-taiwo@leadway-pensure.com"
			'lstEmail.Reciever = False
			'lstEmails.Add(lstEmail)


			'em.sendMailWithOutAttachmentAddess(mailMsgARL(dtARL), "Benefit Application ARL Notification", lstEmails)


		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message & "Error Sending Eligibility Email Notification")

		End Try


	End Sub

	Private Function mailMsg(ByVal myLists As DataTable, ByVal processDate As Date) As String

		Dim sb As New StringBuilder, i As Integer = 0

		sb.Append("<br></br>")
		sb.Append("<br></br>")
		sb.Append("<head>")
		sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />")
		sb.Append("<title>Eligibility Notification</title>")
		sb.Append("<style type='text/css'>")
		sb.Append(".style5 {")
		sb.Append("font-family: 'Trebuchet MS';")
		sb.Append("font-size: 12px;")
		sb.Append("}")
		sb.Append(".style7 {")
		sb.Append("font-family: 'Trebuchet MS';")
		sb.Append("font-size: 12px;")
		sb.Append("font-weight: bold;")
		sb.Append("color: #FFFFFF;")
		sb.Append("}")
		sb.Append("</style>")
		sb.Append("</head>")

		sb.Append("<table border= '1' align='left' cellpadding='2' cellspacing='4' >")
		sb.Append(" <tr>")
		sb.Append("<th colspan='3' align='center' bordercolor='#FFFFFF' bgcolor='#0000FF' scope='col'><span class='style7'>Eligibility Notification</span> </th>")
		sb.Append("</tr>")
		sb.Append("<tr>")
		sb.Append("<td width='250' align='right'><span class='style5'>Date Run For</span> : </td>")
		sb.Append("<td colspan='2'>" & processDate & "</td>")
		sb.Append(" </tr>")
		sb.Append("<tr>")
		sb.Append("<td width='51'><span class='style5'>PIN</span></td>")
		sb.Append("<td width='200'><span class='style5'>FullName</span></td>")
		sb.Append("<td width='200'><span class='style5'>EmployerName</span></td>")
		sb.Append("<td width='200'><span class='style5'>Sex</span></td>")
		sb.Append("<td width='200'><span class='style5'>Phone No</span></td>")
		sb.Append("<td width='200'><span class='style5'>DOB</span></td>")
		sb.Append("</tr>")


		Do While i < myLists.Rows.Count

			sb.Append("<tr>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("PIN") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("FullName") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("EmployerName") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("Sex") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("Mobile") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("DOB") & "</span> </td>")
		
			sb.Append("</tr>")
			i = i + 1
		
		Loop
		
		sb.Append("</table>")
		sb.Append("</br>")
		sb.Append("<br></br>")

		'i = i + 1
		'Loop


		sb.Append("<br></br>")
		sb.Append("<br></br>")
		sb.Append("<br></br>")
		sb.Append("<br></br>")
		sb.Append("<br></br>")


		sb.Append("<i><br>Note : This is an auto-generated e-mail and not to be replied</br></i>")
		sb.Append("<br></br>")
		sb.Append("<br></br>")

		sb.Append("Regards")

		sb.Append("<br></br>")
		sb.Append("<br></br>")


		Return sb.ToString
	End Function


	Private Function mailMsgARL(ByVal myLists As DataTable) As String

		Dim sb As New StringBuilder, i As Integer = 0

		sb.Append("<br></br>")
		sb.Append("<br></br>")
		sb.Append("<head>")
		sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />")
		sb.Append("<title>Accrued Right Notification</title>")
		sb.Append("<style type='text/css'>")
		sb.Append(".style5 {")
		sb.Append("font-family: 'Trebuchet MS';")
		sb.Append("font-size: 12px;")
		sb.Append("}")
		sb.Append(".style7 {")
		sb.Append("font-family: 'Trebuchet MS';")
		sb.Append("font-size: 12px;")
		sb.Append("font-weight: bold;")
		sb.Append("color: #FFFFFF;")
		sb.Append("}")
		sb.Append("</style>")
		sb.Append("</head>")

		sb.Append("Dear All,")
		sb.Append("<br></br>")

		sb.Append("This is to inform you that the follow cusotmer's Accrued Right Letter's Acknowledgment is five (5) working days or more in the system")
		sb.Append("<br></br>")
		sb.Append("<b>Kindly accept this as a reminder. </b>")

		sb.Append("<br></br>")
		sb.Append("Please find their details below")

		sb.Append("<table border= '1' align='left' cellpadding='2' cellspacing='4' >")
		sb.Append(" <tr>")
		sb.Append("<th colspan='3' align='center' bordercolor='#FFFFFF' bgcolor='#0000FF' scope='col'><span class='style7'>Accrued Right Notification</span> </th>")
		sb.Append("</tr>")
		
		sb.Append("<tr>")

		sb.Append("<td width='51'><span class='style5'>Application Code</span></td>")
		sb.Append("<td width='200'><span class='style5'>PIN</span></td>")
		sb.Append("<td width='200'><span class='style5'>Full Name</span></td>")
		sb.Append("<td width='200'><span class='style5'>Employer Name</span></td>")
		sb.Append("<td width='200'><span class='style5'>Sex</span></td>")
		sb.Append("<td width='200'><span class='style5'>DOB</span></td>")
		sb.Append("<td width='200'><span class='style5'>ApplicationType</span></td>")
		sb.Append("<td width='200'><span class='style5'>Acknowledgment Date</span></td>")

		sb.Append("</tr>")


		Do While i < myLists.Rows.Count

			sb.Append("<tr>")

			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("ApplicationCode") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("PIN") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("FullName") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("EmployerName") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("Sex") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("DOB") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("ApplicationType") & "</span> </td>")
			sb.Append("<td align='right'><span class='style5'>" & myLists.Rows(i).Item("dteAcknowledgment") & "</span> </td>")

			sb.Append("</tr>")
			i = i + 1

		Loop

		sb.Append("</table>")
		sb.Append("</br>")
		sb.Append("<br></br>")

		'i = i + 1
		'Loop


		sb.Append("<br></br>")
		sb.Append("<br></br>")
	


		sb.Append("<i><br>Note : This is an auto-generated e-mail and not to be replied</br></i>")
		sb.Append("<br></br>")
		sb.Append("<br></br>")

		sb.Append("Regards")

		sb.Append("<br></br>")
		sb.Append("<br></br>")


		Return sb.ToString
	End Function

End Class
