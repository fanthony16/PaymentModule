Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Partial Class frmApplicationList
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable


	Protected Sub BtnViewInvestigationDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewApplicationLog As New ImageButton, appCode As String
		btnViewApplicationLog = sender
		Dim i As GridViewRow
		i = btnViewApplicationLog.NamingContainer
		appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

		Dim typeID As Integer
		ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		'typeID = (CInt(ApprovalTypeCollection.Item(Me.gridProcessing.Rows(i.RowIndex).Cells(4).Text)))
		typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

		If typeID = 5 Then
			Response.Redirect(String.Format("frmDBAInvestigation.aspx?ApplicationCode={0}&ApplicationTypeID={1}", Server.UrlEncode(appCode), Server.UrlEncode(typeID)))
		Else

		End If



		'  Response.Redirect(String.Format("frmApplicationConfirmation.aspx?ApplicationCode={0}&ReturnPage={1}", Server.UrlEncode(appDetail.ApplicationID), Server.UrlEncode("ApplicationDashBoard")))

	End Sub


     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewApplicationLog As New ImageButton, appCode As String
          btnViewApplicationLog = sender
          Dim i As GridViewRow
          i = btnViewApplicationLog.NamingContainer
          appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

          Dim typeID As Integer
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          'typeID = (CInt(ApprovalTypeCollection.Item(Me.gridProcessing.Rows(i.RowIndex).Cells(4).Text)))
          typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

          Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1, Server.UrlEncode("frmApplicationList")))


          '  Response.Redirect(String.Format("frmApplicationConfirmation.aspx?ApplicationCode={0}&ReturnPage={1}", Server.UrlEncode(appDetail.ApplicationID), Server.UrlEncode("ApplicationDashBoard")))

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

          Dim scriptManagerA As New ScriptManager, dtusers As New DataTable
          scriptManagerA = ScriptManager.GetCurrent(Me.Page)
          scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

          Try

               If IsPostBack = False And Not Context.Request.QueryString("ApplicationID") Is Nothing Then

                    If IsNothing(Session("user")) = True Then
                         Response.Redirect("Login.aspx")
                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then
                         dtusers = Session("userDetails")
                         getUserAccessMenu(Session("user"))
					getApprovalTypes()
					'getCheckLists()
                         PopulateApplicationStatus()
                         getApplicationForDocumentation(0)
                         getApplicationList(CInt(Context.Request.QueryString("ApplicationID")))

                         Dim cr As New Core
					Me.ddApprovalType.SelectedValue = cr.PMgetApprovalTypebyID(CInt(Context.Request.QueryString("ApplicationID")))

					'mpApplicationSummary.Show() PMgetApplicationSummary

					
                    Else
                    End If

               ElseIf IsPostBack = False And Context.Request.QueryString("ApplicationID") Is Nothing Then
                    If IsNothing(Session("user")) = True Then
                         Response.Redirect("Login.aspx")
				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then
					Dim cr As New Core
					dtusers = Session("userDetails")
					getUserAccessMenu(Session("user"))
					getApprovalTypes()
					'getCheckLists()
					PopulateApplicationStatus()
					getApplicationForDocumentation(0)

					Me.gridApplicationSummary.DataSource = cr.PMgetApplicationSummaryByStage("Documentation")
					gridApplicationSummary.DataBind()
					mpApplicationSummary.Show()
                    Else
                    End If
               End If

          Catch ex As Exception

          End Try

     End Sub


	Protected Sub gridProcessing_RowDataBound(sender As Object, e As GridViewRowEventArgs)
		'ViewState("ApplicationList") = dt
		If IsNothing(ViewState("ApplicationList")) = False Then

			Dim dt As DataTable = ViewState("ApplicationList")
			If e.Row.RowType = DataControlRowType.DataRow Then


				If dt.Rows(e.Row.RowIndex).Item("txtLockedBy").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("txtLockedBy").ToString = Session("user").ToString) = False Then
					e.Row.ForeColor = System.Drawing.Color.Blue
					e.Row.Enabled = False
					'isVerified
				Else

				End If



				If dt.Rows(e.Row.RowIndex).Item("fkiAppTypeId").ToString = "5" Then

					Dim imgInvestigation As ImageButton = TryCast(e.Row.FindControl("btnDBAInvestigation"), ImageButton)
					imgInvestigation.Enabled = True

				Else

					Dim imgInvestigation As ImageButton = TryCast(e.Row.FindControl("btnDBAInvestigation"), ImageButton)
					imgInvestigation.Enabled = False

				End If



			End If

		Else
		End If

	End Sub



     Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          If IsNothing(ViewState("Documents")) = False Then

               Dim dt As DataTable = ViewState("Documents")
               If e.Row.RowType = DataControlRowType.DataRow Then

                    '  MsgBox("" & dt.Rows(e.Row.RowIndex).Item("isVerified").ToString)

                    If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

                         e.Row.ForeColor = System.Drawing.Color.Red
                         e.Row.Enabled = False
                         'isVerified
				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Or dt.Rows(e.Row.RowIndex).Item("DocumentID").ToString <> "") And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "True" Then
					e.Row.ForeColor = System.Drawing.Color.Green

                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then
                         e.Row.ForeColor = System.Drawing.Color.Blue
					'e.Row.Enabled = False

                    End If

               End If
          Else
          End If

     End Sub
     'handle the view image button on the submitted document grid on the page
     Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String, dmsDocumentID As String, dmsDocumentExt As String
          btnViewDocumentLog = sender
          Dim i As GridViewRow
          i = btnViewDocumentLog.NamingContainer
          '   appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

          If Not IsNothing(ViewState("Documents")) = True Then

               Dim dt As DataTable = ViewState("Documents")
			'retrieving the location of the scanned document

               documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()
			dmsDocumentID = dt.Rows(i.RowIndex).Item("DocumentID").ToString()
			dmsDocumentExt = dt.Rows(i.RowIndex).Item("DocumentExtension").ToString()

			'testing if the file still exist in the saved file path

			If File.Exists(documentPath) = True Then

				DownLoadDocument(documentPath)

			ElseIf File.Exists(documentPath) = False Then

				Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
				Dim uName As String, uPWD As String, uRI As String

				uName = ConfigurationManager.AppSettings("FileNetUName")
				uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
				uRI = ConfigurationManager.AppSettings("FileNetURI")

				dms.getConnection(uName, uPWD, uRI)
				DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
				DownLoadDocument(DMSDocumentPath)

			End If

          Else

          End If


     End Sub

	Protected Sub gridApplicationSummary_RowDataBound()

	End Sub
     Private Sub DownLoadDocument(path As String)

          If Not File.Exists(path) = False Then

               'If CStr(ViewState("schedulePath")).ToString = "" Then
               '     ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
               'Else
               'End If

               Dim schedulePath As String = path
               Try

                    Dim str() As String = schedulePath.Split("|")
                    Dim FI As FileInfo, fileExt As String, i As Integer = 0

                    Do While i < str.Length

                         FI = New FileInfo(str(i).Trim.ToString)
                         fileExt = LCase(FI.Extension)
                         'MsgBox("" & fileExt)
                         Select Case fileExt

                              Case Is = ".xls"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".xlsx"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
							Response.End()

                              Case Is = ".csv"
                                   'Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
							Response.End()

                              Case Is = ".pdf"
                                   'Process.Start("ACRORD32", str(i).Trim.ToString)

                                   Response.ContentType = "application/pdf"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()


                              Case Is = ".doc"
                                   ' Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".docx"

                                   'Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".jpg"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".png"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Else
                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                         End Select
                         i = i + 1
                    Loop
               Catch ex As Exception
                    '   MsgBox("" & ex.Message)
               End Try

          Else
               ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "File Not Found", True)
          End If


     End Sub





     Protected Sub getApplicationForDocumentation(typeID As Integer)
          Dim cr As New Core, dt As New DataTable

          Try

               dt = cr.PMgetApplicationByTpye(typeID, "Documentation")
               'Me.gridProcessing.DataSource = dt
               'gridProcessing.DataBind()
			'  MsgBox("" & dt.Rows.Count)
			ViewState("ApplicationList") = dt
               loadGrid(dt)


          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try

     End Sub
     'loading datagrid on the page
     Protected Sub loadGrid(dt As DataTable)
          Try
               ' dt = cr.PMgetApplicationByTpye(typeID, "Documentation")
               Me.gridProcessing.DataSource = dt
			gridProcessing.DataBind()

			If dt.Rows.Count > 5 Then
				pnlGrid.Height = Nothing
			Else

			End If

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub


     Protected Sub PopulateApplicationStatus()

          ddApplicationStatusBatch.Items.Add("")
          ddApplicationStatusBatch.Items.Add("Entry")
          ddApplicationStatusBatch.Items.Add("Documentation")
		ddApplicationStatusBatch.Items.Add("Processing")
		ddApplicationStatusBatch.Items.Add("UnFunded")

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

	Protected Sub getCheckLists(AppTypeID As Integer)

		Dim i As Integer = 0, cr As New Core, dt As New DataTable

		If AppTypeID = 5 Then
			dt = cr.PMgetCheckList(AppTypeID)
		Else
			dt = cr.PMgetCheckList(1)
		End If

		'MsgBox("" & dt.Rows.Count)
		Me.cbErrorCheckList.DataSource = dt
		cbErrorCheckList.DataValueField = "intErrorID"
		cbErrorCheckList.DataTextField = "txtDescription"

		cbErrorCheckList.DataBind()


	End Sub

     Protected Sub getApprovalTypes()

          Dim i As Integer = 0
          Dim lstAppTypes As New List(Of String)
          lstAppTypes = getApprovalType()
          ddApprovalType.Items.Clear()
          Do While i < lstAppTypes.Count

               If ddApprovalType.Items.Count = 0 Then
                    ddApprovalType.Items.Add("")
                    ddApprovalType.Items.Add(lstAppTypes.Item(i))
               ElseIf ddApprovalType.Items.Count > 0 Then
                    ddApprovalType.Items.Add(lstAppTypes.Item(i))
               End If
               i = i + 1

          Loop

     End Sub
	Protected Sub populateDocuments(dt As DataTable)

		Try
			ViewState("Documents") = dt
			Me.gridSubmittedDocuments.DataSource = dt
			Me.gridSubmittedDocuments.DataBind()
			If dt.Rows.Count > 0 Then

				Me.pnlDocumentDetails.Height = Nothing
			Else
				Me.pnlDocumentDetails.Height = 100

			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

     Protected Sub populateProperties(lodgmentProperties As List(Of ApplicationProperties))
          Try


               Me.gridProperties.DataSource = lodgmentProperties
               Me.gridProperties.DataBind()

               If lodgmentProperties.Count > 0 Then
                    pnlLeftGrid.Height = Nothing
               Else
                    pnlLeftGrid.Height = 475
               End If


          Catch ex As Exception

          End Try

     End Sub

     Private Sub getApplicationList(AppTypeID As Integer)

          Dim dt As New DataTable
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          getApplicationForDocumentation(AppTypeID)
          refresh()
          populateDocuments(dt)

     End Sub

     Private Sub getApplicationList()

          Dim dt As New DataTable
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          getApplicationForDocumentation(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
          refresh()
          ' populateDocuments(dt)

     End Sub
    
     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

          getApplicationList()
          dvErrorMessage.Visible = False
          refresh()

     End Sub

	Protected Sub refresh()

		ViewState("ApplicationCode") = Nothing
		Dim nw As New List(Of ApplicationProperties), dtDocuments As New DataTable
		populateProperties(nw)
		Me.populateDocuments(dtDocuments)


	End Sub

     Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
          Response.Redirect("frmApplication.aspx")
     End Sub

     Protected Sub gridProcessing_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridProcessing.PageIndexChanging

          If IsNothing(ViewState("ApplicationList")) = False Then

               Dim dt As New DataTable
               Me.gridProcessing.PageIndex = e.NewPageIndex
               dt = ViewState("ApplicationList")
               Me.loadGrid(dt)

          Else
          End If

     End Sub

     Protected Sub gridProcessing_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridProcessing.SelectedIndexChanged

          Try
          
          Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
          Dim selectedRowIndex As Integer

          selectedRowIndex = Me.gridProcessing.SelectedRow.RowIndex

          Dim row As GridViewRow = gridProcessing.Rows(selectedRowIndex)

			'locking the record for review for the user
			'cr.PMLocKRecord(row.Cells(2).Text.ToString(), Session("user"))


			imgPassport.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", row.Cells(4).Text.ToString(), 1, Server.MapPath("~/Logs"))
			imgSignature.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", row.Cells(4).Text.ToString(), 2, Server.MapPath("~/Logs"))


			dt = cr.PMgetApplicationByCode(row.Cells(2).Text.ToString())

			getCheckLists(dt.Rows(0).Item("fkiAppTypeId"))

			'fkiAppTypeId
          'getting submitted documents per application 
			'dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CInt(row.Cells(2).Text.ToString().Split("-")(1)))

          dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CStr(row.Cells(2).Text.ToString()))
          ViewState("ApplicationCode") = row.Cells(2).Text.ToString
          ViewState("PIN") = row.Cells(4).Text.ToString

          'getting customer's personal information details
          dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())


          ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(2).Text.ToString(), row.Cells(4).Text.ToString())

          Session("lodgmentProperties") = ApplicationProperties

          'population the grid to the left for other application information
          populateProperties(ApplicationProperties)

          'population the grid at the bottom for submitted required application documents
          Me.populateDocuments(dtDocuments)




          'Dim dDetails As New List(Of ApplicationDocumentDetail), i As Integer = 0
          'Do While i < dtDocuments.Rows.Count

          '     Dim dDetail As New ApplicationDocumentDetail
          '     dDetail.DocumentTypeName = dt.Rows(i).Item("txtDocumentName")
          '     dDetail.DocumentLocation = dt.Rows(i).Item("DocumentPath").ToString
          '     dDetails.Add(dDetail)
          '     i = i + 1
          'Loop
          'ViewState("DocumentDetails") = dDetails



          If ApplicationProperties.Count < 10 Then
               pnlLeftGrid.Height = 400
          Else
               pnlLeftGrid.Height = Nothing
          End If
          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub

	Protected Sub AddViewIACComment_Click(sender As Object, e As EventArgs) Handles btnShowIACCommentPopup.Click

		Dim btnAddViewIACComment As New ImageButton
		btnAddViewIACComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewIACComment.NamingContainer

		'MsgBox("" & Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text.ToString)

		Me.txtIACApplicationID.Text = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text
		Me.txtApplicationIACComment.Text = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE_IC").Rows(0).Item("txtComment").ToString

		'pops up the comment dialogue
		mpAppIACComments.Show()

	End Sub


     'handles the click event of the comment button on the grid
     Protected Sub AddViewComment_Click(sender As Object, e As EventArgs) Handles btnShowCommentPopup.Click

          Dim btnAddViewComment As New ImageButton, dt As DataTable, j As Integer
          btnAddViewComment = sender
          Dim i As GridViewRow, cr As New Core

          i = btnAddViewComment.NamingContainer
          Me.txtApplicationID.Text = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text
          'logging comments for pre approval benefit application
          'Me.txtApplicationComment.Text = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE")
          dt = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

			lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString & " (" & dt.Rows(j).Item(3).ToString & " )")
               j = j + 1

          Loop

          'pops up the comment dialogue
          mpAppComments.Show()



     End Sub

	Private Function AMsg(appDetails As ApplicationDetail, dtReasons As DataTable) As String

		Dim msg As String = ""
		Dim sb As New StringBuilder

		Try


			sb.Append("<!DOCTYPE html>")
			sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>")

			sb.Append("<title></title>")
			sb.Append("<style type='text/css'>")
			sb.Append(".auto -style2")
			sb.Append("{")
			sb.Append("width: 603px;")
			sb.Append("font-family: 'Trebuchet MS';")
			sb.Append("font-size: 12px;")
			sb.Append("}")

			sb.Append(".auto -style3")
			sb.Append("{")
			sb.Append("width: 307px;")
			sb.Append("font-family: 'Trebuchet MS';")
			sb.Append("font-size: 12px;")
			sb.Append("}")

			sb.Append(".auto -style4")
			sb.Append("{")
			sb.Append("}")
			sb.Append(".auto -style5")
			sb.Append("{")
			sb.Append("width: 219px;")
			sb.Append("font-family: 'Trebuchet MS';")
			sb.Append("font-size: 12px;")
			sb.Append("}")

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
			sb.Append("<body>")


			sb.Append("<br></br>")

			sb.Append("Dear <b>" & appDetails.CreatedBy & "</b>")

			sb.Append("<br></br>")

			'sb.Append("This is to inform you that, <b>[" & appDetails.PIN & "]</b>.  <b>" & appDetails.FullName.Replace("|", "") & "'s</b>  <b>" & appDetails.ApplicationTypeName & "</b> benefit application with the reference code <b>" & appDetails.ApplicationID & "</b> and application date of <b>" & appDetails.ApplicationDate & "</b> has been returned to Entry Stage for further review.")

			sb.Append("This is to inform you that, <b>" & appDetails.FullName.Replace("|", "") & "'s</b>  <b>" & appDetails.ApplicationTypeName & "</b> benefit application with the reference code <b>" & appDetails.ApplicationID & "</b> and application date of <b>" & appDetails.ApplicationDate & "</b> has been returned to Entry Stage for further review.")

			sb.Append("<br></br>")
			sb.Append("The reason(s) for the return is/are listed below;")
			sb.Append("<br></br>")

			Dim i As Integer = 0

			Do While i < dtReasons.Rows.Count
				sb.Append(" <b>" & (i + 1) & "</b>. <b>" & UCase(dtReasons.Rows(i).Item(0).ToString) & "</b>")
				sb.Append("<br></br>")
				i = i + 1
			Loop



			sb.Append("For further enquiries kindly contact us via 8153 or BenefitApp@leadway-pensure.com")


			sb.Append("<br></br>")


			sb.Append("</body>")
			sb.Append("</html>")

			sb.Append("<br></br>")
			sb.Append("<br></br>")
			sb.Append("Yours faithfully,")
			sb.Append("<br></br>")
			sb.Append("<br></br>")
			sb.Append("For: LEADWAY PENSURE PFA LTD.")

			sb.Append("<br></br>")
			sb.Append("<br></br>")


			msg = sb.ToString
		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

		Return msg
	End Function


	Protected Sub SendNotifiCation(appDetail As ApplicationDetail)

		Dim em As New EmailGateway.EmailGateway, dtComments As New DataTable
		Dim lstEmails As New List(Of EmailGateway.EmailAddress)
		Dim lstEmail As New EmailGateway.EmailAddress, cr As New Core

		Try
			'retrieving all the comment on the application
			dtComments = cr.PMgetApplicationComment(appDetail.ApplicationID, "PRE")


			lstEmails = cr.PMgetEscalationEmail(Session("user").ToString, 1)

			If cr.IsValidEmailAddress(appDetail.Email) = True Then

				'builds the email addresses to send the acknowledgment mail to
				If IsNothing(lstEmails) = True Then
					lstEmails = New List(Of EmailGateway.EmailAddress)

				Else
				End If



				lstEmail.EmailAddress = appDetail.Email
				lstEmail.Reciever = True
				lstEmails.Add(lstEmail)

				lstEmail = New EmailGateway.EmailAddress
				lstEmail.EmailAddress = Session("user") & "@leadway-pensure.com"
				lstEmail.Reciever = False
				lstEmails.Add(lstEmail)


				lstEmail = New EmailGateway.EmailAddress
				lstEmail.EmailAddress = "o-taiwo@leadway-pensure.com"
				lstEmail.Reciever = False
				lstEmails.Add(lstEmail)


				'building the e-mail notification to be sent to the application logger

				em.sendMailWithOutAttachmentAddess(Me.AMsg(appDetail, dtComments), "Benefit Application Notification - " & appDetail.PIN, lstEmails)


			Else



			End If
		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message & "Error Send Email Notification")

		End Try


	End Sub


     Protected Sub btnHardShipProcessingBatch_Click(sender As Object, e As EventArgs) Handles btnHardShipProcessingBatch.Click


          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try

			If IsNothing(Session("user")) = True Then
				Response.Redirect("Login.aspx")
			Else
			End If


          For Each grow As GridViewRow In Me.gridProcessing.Rows

				Dim dt As New DataTable, appDetail As New ApplicationDetail
               cb = grow.FindControl("chkProcessing")

               If cb.Checked = True Then


                         If Me.ddApplicationStatusBatch.SelectedItem.Text.ToString <> "" Then

						cr.PMSetApplicationStatus(grow.Cells(2).Text, Me.ddApplicationStatusBatch.SelectedItem.Text, Session("user").ToString)


						'dt = cr.PMgetApplicationByCode(grow.Cells(2).Text)

						'If dt.Rows.Count > 0 And Me.ddApplicationStatusBatch.SelectedItem.Text = "Entry" Then

						'	appDetail.ApplicationID = dt.Rows(0).Item("txtApplicationCode")
						'	appDetail.ApplicationDate = CDate(dt.Rows(0).Item("dteApplicationDate"))
						'	appDetail.FullName = dt.Rows(0).Item("Name")
						'	appDetail.ApplicationTypeName = dt.Rows(0).Item("TypeName")
						'	appDetail.PIN = grow.Cells(4).Text
						'	'appDetail.Title = cr.getPMPersonInformation(grow.Cells(4).Text, Now.Date).Rows(0).Item("Title")
						'	appDetail.CreatedBy = dt.Rows(0).Item("CreatedBy")
						'	appDetail.EmployerName = dt.Rows(0).Item("txtEmployerName")
						'	appDetail.Email = dt.Rows(0).Item("txtCreatedBy").ToString & "@leadway-pensure.com"
						'	SendNotifiCation(appDetail)

						'Else

						'End If

                         Else

					End If

					'                    refresh()

               ElseIf cb.Checked = False Then

               End If

			Next

			refresh()
			getApplicationList()

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try

               'For Each grow As GridViewRow In Me.gridProcessing.Rows

               '     cb = grow.FindControl("chkProcessing")

               '     If cb.Checked = True Then

               '          Response.Redirect("frmEditApplication.aspx?ApplicationCode=" & grow.Cells(2).Text)

               '     ElseIf cb.Checked = False Then

               '     End If

               'Next
               'getApplicationList()
          Catch ex As Exception

          End Try

     End Sub

     Protected Sub btnNew_Click(sender As Object, e As ImageClickEventArgs) Handles btnNew.Click
          Response.Redirect("frmApplication.aspx")
     End Sub

     Protected Sub btnEdit_Click(sender As Object, e As ImageClickEventArgs) Handles btnEdit.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, appCode As String = ""
          dvErrorMessage.Visible = False
          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         chk = chk + 1
                         appCode = grow.Cells(2).Text


                    ElseIf cb.Checked = False Then



                    End If

               Next

               If chk = 1 Then

                    Dim typeID As Integer
                    ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
                    typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

                    Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1))

               Else
                    dvErrorMessage.Visible = True
                    Exit Sub
               End If


               'getApplicationList()
          Catch ex As Exception

          End Try


     End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As ImageClickEventArgs) Handles btnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = True


          Next

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As ImageClickEventArgs) Handles btnUnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = False


          Next

     End Sub

     Protected Sub btnCancel_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancel.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then
					cr.PMDeleteApplication(grow.Cells(2).Text, Session("user"))
                    ElseIf cb.Checked = False Then

                    End If

               Next

               getApplicationList()

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

		Dim cr As New Core

		If Not IsNothing(Session("user")) = True Then

			cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1, cbErrorCheckList.SelectedValue)
			txtApplicationComment.Text = ""
			refreshCommentList(txtApplicationID.Text)

		Else
			Response.Redirect("Login.aspx")
		End If

          

     End Sub

     Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

     End Sub
     Protected Sub refreshCommentList(appCode As String)
          Dim cr As New Core, j As Integer, dt As DataTable
          dt = cr.PMgetApplicationComment(appCode, "PRE")
		lstComments.Items.Clear()

          Do While j < dt.Rows.Count

			lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString & " (" & dt.Rows(j).Item(3).ToString & " )")
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
End Class
