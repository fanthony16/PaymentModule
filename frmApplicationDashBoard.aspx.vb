Imports System.Net
Imports System.Data


Partial Class frmApplicationDashBoard
     Inherits System.Web.UI.Page
     Dim myThread As System.Threading.Thread
     Dim ApprovalTypeCollection As New Hashtable

     Private Function ValidationSummary(date1 As Date, date2 As Date, status As Integer) As DataTable

          Const cont_string As String = "data Source=p-enpower;Initial Catalog=Enpower_midas;User ID=ibs;Pwd=vaug;Connect Timeout= 720"
          Dim command As New SqlClient.SqlCommand
          Dim myConnection As SqlClient.SqlConnection = New SqlClient.SqlConnection(cont_string)
          Dim dsUser As New DataSet
          Dim dt As New DataTable
          Dim sql As String = ""

          Try
               myConnection.Open()

               If status = 2 Then

                    sql = "select txtIDNo,txtEmail,isValidated,txtDescription from tblEmailValidation where dteValidated between @date1 and @date2"
               ElseIf status < 2 Then
                    sql = "select txtIDNo,txtEmail,isValidated,txtDescription from tblEmailValidation where dteValidated between @date1 and @date2 and isValidated = @status"

                    'ElseIf status = 3 Then
                    '   sql = "select * from tblEmailValidation where dteValidated between @date1 and @date2 and isValidated = @status"
               End If

               Dim MyDataAdapter As SqlClient.SqlDataAdapter
               MyDataAdapter = New SqlClient.SqlDataAdapter(sql, myConnection)
               MyDataAdapter.SelectCommand.CommandType = CommandType.Text

               MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
                    SqlDbType.DateTime))
               MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

               MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
                   SqlDbType.DateTime))
               MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

               If status < 2 Then

                    MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@status", _
                    SqlDbType.Int))
                    MyDataAdapter.SelectCommand.Parameters("@status").Value = status

               End If

               dsUser = New DataSet()
               MyDataAdapter.Fill(dsUser, "ValidationSummary")
               dt = dsUser.Tables("ValidationSummary")
               ViewState("Emails") = dt
               Return dt

          Catch ex As Exception
          Finally
               myConnection.Close()
          End Try
          Return Nothing
     End Function
     Private Sub ValidationLog(txtIDno As String, txtEmail As String, status As Integer, statusCode As Integer, desc As String)

          Const cont_string As String = "data Source=p-enpower;Initial Catalog=Enpower_midas;User ID=ibs;Pwd=vaug;Connect Timeout= 720"
          Dim command As New SqlClient.SqlCommand
          Dim myConnection As SqlClient.SqlConnection = New SqlClient.SqlConnection(cont_string)


          Try
               myConnection.Open()
               Dim sql As String = "insert into tblEmailValidation (txtIDNo,txtEmail,isValidated,intReturnCode,dteValidated,txtValidatedBy,txtDescription) values ('" & txtIDno & "', '" & txtEmail.Replace("'", "''") & "','" & status & "','" & statusCode & "','" & DateTime.Parse(Now.Date).ToString("yyyy-MM-dd") & "', '" & Session("user") & "','" & desc & "')"



               Dim dtUser As New DataTable
               Dim MyDataAdapter As SqlClient.SqlDataAdapter
               MyDataAdapter = New SqlClient.SqlDataAdapter(sql, myConnection)
               MyDataAdapter.SelectCommand.CommandType = CommandType.Text
               MyDataAdapter.SelectCommand.ExecuteNonQuery()
               myConnection.Close()
          Catch ex As Exception
               MsgBox("" & ex.Message)
          Finally
               myConnection.Close()
          End Try

     End Sub
     Private Sub ValidateEmail(ByVal emailAddress As String, txtIDNo As String)
          'Dim apikey As String = "ev-e99a1ad0248a73cf3043a6957923f258"
          Dim apikey As String = "ev-a379c6aee3878573ca996086011caa75"

          Const APIURL As String = "http://api.email-validator.net/api/verify"

          Dim nc As New NetworkCredential
          nc.Domain = "pensure-nigeria.com"
          nc.UserName = "o-taiwo"
          nc.Password = "fanthony16,..,"

          Dim prxy As New WebProxy("172.16.0.8:8080", True)
          prxy.Credentials = nc

          Using client As New Net.WebClient

               client.Proxy = prxy
               Dim postData As New Specialized.NameValueCollection

               postData.Add("EmailAddress", emailAddress)
               postData.Add("APIKey", apikey)
               Dim reply = client.UploadValues(APIURL, "POST", postData)
               Dim data As String = (New System.Text.UTF8Encoding).GetString(reply)
               Dim res = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(Of APIResult)(data)
               Select Case (res.status)
                    Case 200, 207, 215
                         ' Address is valid
                         'spnMessage.InnerText = "Email Address Validation Successful"
                         ValidationLog(txtIDNo, emailAddress, 1, res.status, res.info)

                         'Case 114, 118
                         ' Retry
                    Case Else
                         ValidationLog(txtIDNo, emailAddress, 0, res.status, res.info)
                         'spnMessage.InnerText = "Email Address Not Validated Successfully : " & res.status.ToString
               End Select
          End Using
     End Sub

     Public Class APIResult

          Public status As String
          Public info As String
          Public details As String

     End Class

     Protected Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click


          Dim dtEmails As New DataTable, i As Integer = 0
          dtEmails = getEmails(CInt(Me.txtEmailCount.Text))
          'dtEmails = getEmails(EmailCount)

          Do While i < dtEmails.Rows.Count
               'ValidateEmail(dtEmails.Rows(i).Item("txtEMailAddress"), dtEmails.Rows(i).Item("txtIDno"))
               ValidateEmail(dtEmails.Rows(i).Item(2), dtEmails.Rows(i).Item(1))
               i = i + 1
          Loop

          Me.pnlMessage.Visible = True
          spnMessage.InnerText = "Email Validation Completed."


     End Sub

     Private Function getEmails(recordCount As Integer) As DataTable
          Const cont_string As String = "data Source=p-enpower;Initial Catalog=Enpower_midas;User ID=ibs;Pwd=vaug;Connect Timeout= 720"
          Dim command As New SqlClient.SqlCommand
          Dim dsUser As DataSet
          Try



               ' Dim sql As String = "select top " & recordCount & "pkipersonID,txtIDno,  txtEMailAddress from tblpeople a where isnull(txtEMailAddress,'')<>'' and txtEMailAddress like '%__@%' and txtidno like '%PEN____________' and not exists (select * from tblEmailValidation where txtIDNo = a.txtIDno)"

               'Dim sql As String = "select top " & recordCount & " 0,'',  txtEmail from tblEmailValidationtemp a where isnull(a.txtEmail,'')<>'' and a.txtEmail like '%__@%' and not exists (select * from tblEmailValidation where txtEmail = a.txtEmail)"

               Dim sql As String = "select top " & recordCount & " 0,a.pkiNyscCaptureID,  txtEmailAddress from tblNyscCapture a where isnull(a.txtEmailAddress,'')<>'' and a.txtEmailAddress like '%__@%' and not exists (select * from tblEmailValidation where txtIDNo = CONVERT(varchar(10), a.pkiNyscCaptureID)) order by a.pkiNyscCaptureID asc"


               'select top 10 txtEmailAddress, * from dbo.tblNyscCapture where isnull(txtEmailAddress,'') <> '' and pkiNyscCaptureID

               Dim myConnection As SqlClient.SqlConnection = New SqlClient.SqlConnection(cont_string)
               myConnection.Open()

               Dim dtUser As New DataTable
               command.Connection = myConnection
               Dim MyDataAdapter As SqlClient.SqlDataAdapter
               MyDataAdapter = New SqlClient.SqlDataAdapter(sql, myConnection)
               MyDataAdapter.SelectCommand.CommandType = CommandType.Text
               dsUser = New DataSet()
               MyDataAdapter.Fill(dsUser, "EmailValidation")
               dtUser = dsUser.Tables("EmailValidation")
               Return dtUser
               myConnection.Close()
          Catch ex As Exception
               MsgBox("" & ex.Message)
          Finally
               '   myConnection.close()
          End Try

          Return Nothing
	End Function

	Protected Sub populateUsers(dt As DataTable)

		Try
			Dim i As Integer
			Do While i < dt.Rows.Count
				If Me.ddUsers.Items.Count = 0 Then
					ddUsers.Items.Add("")
					ddUsers.Items.Add(dt.Rows(i).Item("UserID"))
				Else
					ddUsers.Items.Add(dt.Rows(i).Item("UserID"))
				End If
				i = i + 1
			Loop
		Catch ex As Exception

		End Try
	End Sub
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim scriptManagerEmail As New ScriptManager, scriptManagerSummary As New ScriptManager, dtusers As New DataTable, dtAllUsers As New DataTable, cr As New Core, dtPendingAppSummary As New DataTable

		scriptManagerEmail = ScriptManager.GetCurrent(Me.Page)
		scriptManagerEmail.RegisterPostBackControl(Me.btnExport)

		scriptManagerSummary = ScriptManager.GetCurrent(Me.Page)
		scriptManagerSummary.RegisterPostBackControl(imgUserSummary)


		Me.pnlMessage.Visible = False

		If Page.IsPostBack = False Then

			If IsNothing(Session("user")) = True Then

				'   getApprovalType()
				Response.Redirect("Login.aspx")
			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then


				populateUsers(getUserList("A", ""))
				getApprovalType()
				dtusers = Session("userDetails")
				getUserAccessMenu(Session("user"))
				pnlMessage.Visible = False

				dtPendingAppSummary = cr.PMgetApplicationSummaryByStage()
				ViewState("dtPendingAppSummary") = dtPendingAppSummary
				Me.gridApplicationUserSummary.DataSource = dtPendingAppSummary
				gridApplicationUserSummary.DataBind()
				mpApplicationSummary.Show()

				If Not Context.Request.QueryString("IsReturn") Is Nothing Then

					Me.loadAllLoggedApplication(Now.Date, Now.Date)
					Me.txtStartDate.Text = Now.Date
					Me.txtEndDate.Text = Now.Date

				Else

				End If

				'      BindGrid(ValidationSummary(Now.Date.AddDays(1), Now.Date.AddDays(1), 2))

			Else
			End If

		End If



	End Sub

	Protected Sub gridApplicationSummary_RowDataBound()

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

     Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
          Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
     End Sub

     Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
          Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
     End Sub
	Protected Sub generateSummary(sdate As Date, edate As Date, UName As String)

		Try
			Dim dt, dt2 As New DataTable, cr As New Core
			' dt = ValidationSummary(Me.txtStartDate.Text, Me.txtEndDate.Text, 2)
			If UName = "" Then
				dt = cr.PMgetApplicationByDate(sdate, edate, True, 0)
			Else
				dt = cr.PMgetApplicationByDate(sdate, edate, True, 0, UName)
			End If

			If UName = "" Then
				dt2 = cr.PMgetApplicationByStatus(sdate, edate, True, "Entry")
			Else
				dt2 = cr.PMgetApplicationByStatus(sdate, edate, True, "Entry", UName)
			End If



			If dt.Rows.Count > 0 Then

				'  spApplicationCount.InnerText = dt.Rows(0).Item("All")

				Me.lblTotalUndocumentedCount.InnerText = CInt(dt.Rows(0).Item("All"))
				Me.lblPercentTotalUndocumented.InnerText = Math.Round((CInt(dt.Rows(0).Item("All")) / CInt(dt.Rows(0).Item("All"))) * 100, 1).ToString & "%"

				Me.lblTotaldocumentedCount.InnerText = CInt(dt2.Rows(0).Item("StatusApplications"))
				Me.lblPercentTotaldocumented.InnerText = Math.Round((CInt(dt2.Rows(0).Item("StatusApplications")) / CInt(dt2.Rows(0).Item("All"))) * 100, 1).ToString & "%"

				Me.lblTotalProcessingCount.InnerText = CInt(dt.Rows(0).Item("UncompleteDocument"))
				Me.lblPercentTotalProcessing.InnerText = Math.Round((CInt(dt.Rows(0).Item("UncompleteDocument")) / CInt(dt.Rows(0).Item("All"))) * 100, 1).ToString & "%"

			Else
			End If


		Catch ex As Exception

		End Try

	End Sub

	Public Function getUserList(filterType As String, filterBy As String) As DataTable
		Try

			Dim dc As New UsersDataContext, dtUser As New DataTable, dtColumn As New DataColumn

			dtUser = New DataTable
			dtColumn = New DataColumn("UserID")
			dtUser.Columns.Add(dtColumn)
			dtColumn = New DataColumn("FullName")
			dtUser.Columns.Add(dtColumn)
			dtColumn = New DataColumn("RoleName")
			dtUser.Columns.Add(dtColumn)
			dtColumn = New DataColumn("IsActive")
			dtUser.Columns.Add(dtColumn)


			Dim lstUsers As New List(Of String)

			If filterType = "Z" Then
				Dim query = From m In dc.tblUsers Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Where m.UserName.Contains(filterBy) _
					  Select New With {m.UserName, m.FullName, n.txtRole, m.IsActive}
				For Each a In query

					Dim newCustomersRow As DataRow
					newCustomersRow = dtUser.NewRow()

					newCustomersRow("UserID") = a.UserName
					newCustomersRow("FullName") = a.FullName
					newCustomersRow("RoleName") = a.txtRole
					newCustomersRow("IsActive") = a.IsActive

					dtUser.Rows.Add(newCustomersRow)

				Next
			ElseIf filterType = "A" Then
				Dim query = From m In dc.tblUsers Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Where m.IsActive = True And m.UserName.Contains(filterBy) _
					  Select New With {m.UserName, m.FullName, n.txtRole, m.IsActive}
				For Each a In query

					Dim newCustomersRow As DataRow
					newCustomersRow = dtUser.NewRow()

					newCustomersRow("UserID") = a.UserName
					newCustomersRow("FullName") = a.FullName
					newCustomersRow("RoleName") = a.txtRole
					newCustomersRow("IsActive") = a.IsActive

					dtUser.Rows.Add(newCustomersRow)

				Next
			ElseIf filterType = "D" Then
				Dim query = From m In dc.tblUsers Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Where m.IsActive = False And m.UserName.Contains(filterBy) _
					  Select New With {m.UserName, m.FullName, n.txtRole, m.IsActive}
				For Each a In query

					Dim newCustomersRow As DataRow
					newCustomersRow = dtUser.NewRow()

					newCustomersRow("UserID") = a.UserName
					newCustomersRow("FullName") = a.FullName
					newCustomersRow("RoleName") = a.txtRole
					newCustomersRow("IsActive") = a.IsActive

					dtUser.Rows.Add(newCustomersRow)

				Next
			End If

			Return dtUser
		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)
		
		End Try
		Return Nothing
	End Function

     Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
		Try
			generateSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), "")
			Me.chkShowAll.Checked = True
			Me.chkShowMyApplications.Checked = False

		Catch ex As Exception

		End Try


     End Sub

     Protected Sub BindGrid(dt As DataTable)

          Try

               Me.gridApplicationSummary.DataSource = dt
               Me.gridApplicationSummary.DataBind()

               If dt.Rows.Count < 10 Then
                    pnlValidatdEmail.Height = 400
               Else
                    pnlValidatdEmail.Height = Nothing
               End If

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try

     End Sub

     Protected Sub btnExport_Click(sender As Object, e As ImageClickEventArgs) Handles btnExport.Click

          'ApplicationList

          Dim dt As DataTable
          dt = ViewState("ApplicationList")
          If IsNothing(ViewState("ApplicationList")) = False Then

               Dim cr As New Core
               cr.ExtractCSV(ViewState("ApplicationList"), "ApplicationList")

          Else
          End If
     End Sub



     Protected Sub gridApplicationSummary_SelectedIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridApplicationSummary.PageIndexChanging

          If IsNothing(ViewState("ApplicationList")) = False Then

               Dim dt As New DataTable
               Me.gridApplicationSummary.PageIndex = e.NewPageIndex
               dt = ViewState("ApplicationList")
               BindGrid(dt)

          Else
          End If
         
     End Sub

     
     Public Function getApprovalType() As List(Of String)

          Dim lstAppTypes As New List(Of String)
          Dim dc As New AppDocumentsDataContext
          Dim query = From m In dc.tblApplicationTypes
                      Select m

          For Each a As tblApplicationType In query
               lstAppTypes.Add(a.txtDescription)
               ApprovalTypeCollection.Add(a.txtDescription, a.pkiAppTypeId)
          Next
          ViewState("ApprovalTypeCollection") = ApprovalTypeCollection
          Return lstAppTypes

     End Function

	Protected Sub BtnCancelApplication_Click(sender As Object, e As EventArgs)

		Dim btnCancelApplication As New ImageButton, appCode As String
		btnCancelApplication = sender
		Dim i As GridViewRow
		i = btnCancelApplication.NamingContainer
		appCode = Me.gridApplicationSummary.Rows(i.RowIndex).Cells(0).Text

		Dim cr As New Core
		cr.PMDeleteApplication(appCode, Session("user"))
		generateSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), "")
		loadAllLoggedApplication(Me.txtStartDate.Text, Me.txtEndDate.Text)
		'Dim typeID As Integer
		'ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		'typeID = (CInt(ApprovalTypeCollection.Item(Me.gridApplicationSummary.Rows(i.RowIndex).Cells(4).Text)))

		'Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 0, Server.UrlEncode("ApplicationDashBoard")))



	End Sub

	Protected Sub BtnViewDetails_Click2(sender As Object, e As EventArgs)
		Response.Redirect("Default.aspx")
	End Sub

     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewApplicationLog As New ImageButton, appCode As String
          btnViewApplicationLog = sender
          Dim i As GridViewRow
          i = btnViewApplicationLog.NamingContainer
          appCode = Me.gridApplicationSummary.Rows(i.RowIndex).Cells(0).Text

          Dim typeID As Integer
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          typeID = (CInt(ApprovalTypeCollection.Item(Me.gridApplicationSummary.Rows(i.RowIndex).Cells(4).Text)))

          Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 0, Server.UrlEncode("ApplicationDashBoard")))



          '  Response.Redirect(String.Format("frmApplicationConfirmation.aspx?ApplicationCode={0}&ReturnPage={1}", Server.UrlEncode(appDetail.ApplicationID), Server.UrlEncode("ApplicationDashBoard")))

     End Sub


     'handles the click event of the comment button on the grid
     Protected Sub AddViewComment_Click(sender As Object, e As EventArgs) Handles btnShowCommentPopup.Click

          Dim btnAddViewComment As New ImageButton, dt As DataTable, j As Integer
          btnAddViewComment = sender
          Dim i As GridViewRow, cr As New Core

          i = btnAddViewComment.NamingContainer
          Me.txtApplicationID.Text = Me.gridApplicationSummary.Rows(i.RowIndex).Cells(0).Text
          'logging comments for pre approval benefit application

          dt = cr.PMgetApplicationComment(Me.gridApplicationSummary.Rows(i.RowIndex).Cells(0).Text, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop

          'pops up the comment dialogue
          mpAppComments.Show()



     End Sub

     Protected Sub gridDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          If IsNothing(ViewState("ApplicationList")) = False Then

               Dim dt As DataTable = ViewState("ApplicationList")
               If e.Row.RowType = DataControlRowType.DataRow Then
                    ' Dim btn As New ImageButton
                    'btn = e.Row.FindControl("btnViewApplicationLog")

                    If dt.Rows(e.Row.RowIndex).Item("txtStatus").ToString = "Entry" Then

                         e.Row.ForeColor = System.Drawing.Color.Red
                         e.Row.Enabled = True

                    ElseIf dt.Rows(e.Row.RowIndex).Item("txtStatus").ToString <> "Entry" Then

                         e.Row.ForeColor = System.Drawing.Color.Green
                         e.Row.Enabled = False

                    End If

               End If
          Else
          End If

     End Sub



     Protected Sub btnViewdocumentedApplication_Click(sender As Object, e As ImageClickEventArgs) Handles btnViewdocumentedApplication.Click



		Dim cr As New Core, dt As New DataTable
		' dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, "Entry")
		' ViewState("ApplicationList") = dt
		'	BindGrid(dt)

		If Me.chkShowMyApplications.Checked = True And Not IsNothing(Session("user")) = True Then
			dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, "Entry", Session("user"))
		Else
			dt = cr.PMgetApplicationByStatus(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, "Entry")
		End If

		ViewState("ApplicationList") = dt
		BindGrid(dt)



     End Sub

     Protected Sub btnViewProcessedApplication_Click(sender As Object, e As ImageClickEventArgs) Handles btnViewProcessedApplication.Click

         

		Dim cr As New Core, dt As New DataTable

		If Me.chkShowMyApplications.Checked = True And Not IsNothing(Session("user")) = True Then

			dt = cr.PMgetApplicationSummaryDetails(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), "P", Session("user"))
		Else

			dt = cr.PMgetApplicationSummaryDetails(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), "P")
		End If

          ViewState("ApplicationList") = dt
          BindGrid(dt)

     End Sub

     Protected Sub btnViewNewApplication_Click(sender As Object, e As ImageClickEventArgs) Handles btnViewNewApplication.Click


		If Me.chkShowMyApplications.Checked = True And Not IsNothing(Session("user")) = True Then
			loadAllLoggedApplication(Me.txtStartDate.Text, Me.txtEndDate.Text, Session("user"))
		Else
			loadAllLoggedApplication(Me.txtStartDate.Text, Me.txtEndDate.Text)
		End If


     End Sub
	Protected Sub loadAllLoggedApplication(date1 As Date, date2 As Date, UName As String)

		Dim cr As New Core, dt As New DataTable
		dt = cr.PMgetApplicationSummaryDetails(CDate(date1), CDate(date2), "N", UName)
		ViewState("ApplicationList") = dt
		BindGrid(dt)

	End Sub

	Protected Sub loadAllLoggedApplication(date1 As Date, date2 As Date)

		Dim cr As New Core, dt As New DataTable
		dt = cr.PMgetApplicationSummaryDetails(CDate(date1), CDate(date2), "N")
		ViewState("ApplicationList") = dt
		BindGrid(dt)

	End Sub

     Protected Sub btnNewApplication_Click(sender As Object, e As ImageClickEventArgs) Handles btnNewApplication.Click
          Response.Redirect("frmApplication.aspx")
     End Sub

     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

		Dim cr As New Core
		'the first 1 indicate pre-approval comment while the  second 1 indicate a default checklist code
		cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1, 1)
          txtApplicationComment.Text = ""
          refreshCommentList(txtApplicationID.Text)
          Me.mpAppComments.Show()

     End Sub
     'refreshing the pop up comment list on an application
     Protected Sub refreshCommentList(appCode As String)
          Dim cr As New Core, j As Integer, dt As DataTable
          dt = cr.PMgetApplicationComment(appCode, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop
          mpAppComments.Show()

     End Sub

     Protected Sub btnAppCommentRemove_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentRemove.Click

          Dim cr As New Core
          Try
               If IsNothing(Session("user")) = False Then
                    Dim UName As String = CStr(Session("user"))
                    Dim str() As String = Me.lstComments.SelectedItem.Text.Split(":")
                    If UName = LTrim(RTrim(CStr(str(1)))) Then

                         cr.PMRemoveComment(CInt(str(0)))
                         refreshCommentList(txtApplicationID.Text)

                    Else
                         Me.mpAppComments.Show()
                    End If
               Else
                    Me.mpAppComments.Show()
               End If

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub gridApplicationSummary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplicationSummary.SelectedIndexChanged

     End Sub

	Protected Sub chkShowMyApplications_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowMyApplications.CheckedChanged

		Me.chkShowAll.Checked = False
		Try

			If Not IsNothing(Session("user")) = True Then
				generateSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), Session("user"))
				loadAllLoggedApplication(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), Session("user"))
			Else
			End If

		Catch ex As Exception

		End Try
		



	End Sub

	Protected Sub chkShowAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowAll.CheckedChanged

		Me.chkShowMyApplications.Checked = False

		'If Not IsNothing(Session("user")) = True Then
		generateSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), "")
		loadAllLoggedApplication(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))
		'	Else
		'		End If

	End Sub

	Protected Sub ddUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddUsers.SelectedIndexChanged


		Try

			If Not IsNothing(Session("user")) = True Then
				generateSummary(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), ddUsers.SelectedItem.Text)
				loadAllLoggedApplication(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), ddUsers.SelectedItem.Text)
			Else
			End If

		Catch ex As Exception

		End Try


	End Sub

	Protected Sub imgUserSummary_Click(sender As Object, e As ImageClickEventArgs) Handles imgUserSummary.Click

		Dim dt As New DataTable
		'dt = ViewState("dtPendingAppSummary")

		If IsNothing(ViewState("dtPendingAppSummary")) = False Then

			Dim cr As New Core
			cr.ExtractCSV(ViewState("dtPendingAppSummary"), "Pending ApplicationSummary")

		Else
		End If

	End Sub
End Class
