Imports System.IO
Imports AjaxControlToolkit
Imports System.Data

Partial Class frmEnhancementApplication
	Inherits System.Web.UI.Page

	Dim DocumentCollection As New Hashtable
	Dim BankTypeCollection As New Hashtable
	Dim ApprovalTypeCollection As New Hashtable
	Dim dtDocuments As New DataTable
	Dim dtColumn As New DataColumn
	Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

		Dim btnDetails As New ImageButton
		btnDetails = sender
		Dim i As GridViewRow
		i = btnDetails.NamingContainer
		'	DownLoadDocument(Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text))

		Dim tmpPath As String = Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		Dim permPath As String = Server.MapPath("~/ApplicationDocuments" + "/" + CStr(ViewState("appCode")).ToString.Replace("-", "_") + "_" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		'	Dim permPath As String = Server.MapPath("~/ApplicationDocuments" + "/" + Me.ddAnnRunningPW.Text.ToString.Replace("-", "_") + "_" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		If File.Exists(tmpPath) = True Then

			DownLoadDocument(tmpPath)
			Exit Sub

		End If


		If File.Exists(permPath) = True Then

			DownLoadDocument(permPath)

		End If


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

	Protected Sub AjaxFileDocumentUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)

		Try

			Dim filename As String = System.IO.Path.GetFileName(e.FileName)
			Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)

			'Dim FI As System.IO.FileInfo
			'FI.le()

			Dim fileNewName As String
			'Me.ddRequiredDocuments.SelectedItem.Text

			' If Me.ddRequiredDocuments.SelectedValue.Text = "Others" Then

			'   MsgBox("" & txtAdhocDocDescription.Text)

			'If Me.txtAdhocDocDescription.Text <> "" Then

			'	Session("Document") = Me.txtAdhocDocDescription.Text
			'	fileNewName = Session("Document").ToString

			'Else
			fileNewName = Session("Document").ToString

			'End If

			'Session("Document")


			fileNewName = fileNewName.Replace(" | ", "_")
			fileNewName = fileNewName.Replace(" ", "_")
			fileNewName = fileNewName.Replace("|", "_")
			fileNewName = fileNewName.Replace(" ", "_")
			fileNewName = fileNewName.Replace("(", "_")
			fileNewName = fileNewName.Replace(")", "_")


			Dim strUploadPath As String
			If (Session("Document").ToString) = "Others" Then
				fileNewName = filename

				fileNewName = fileNewName.Replace(" | ", "_")
				fileNewName = fileNewName.Replace("|", "")
				fileNewName = fileNewName.Replace(" ", "_")
				fileNewName = fileNewName.Replace("(", "_")
				fileNewName = fileNewName.Replace(")", "_")


				strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName
			Else

				strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName & System.IO.Path.GetExtension(fullPath) '& "_" & filename
				'keeping the temporary the uploaded document prior final saving for easy retrieval should there is network cut-off

			End If


			Session("documentPath") = strUploadPath


			'Dim infoReader As System.IO.FileInfo
			'infoReader = My.Computer.FileSystem.GetFileInfo(e.FileSize)
			'MsgBox("File is " & infoReader.Length & " bytes.")
			'Session("FileSize") = infoReader.Length

			'MsgBox("" & e.FileSize)
			'If e.FileSize > 1000000 Then

			'	lblFileSizeError.Text = "Max. File Size Exceeded"
			'	Me.dvfileSizeError.Visible = True

			'	pnlAppTypeVerificationError.Visible = True
			'	Me.lblAppTypeError.Text = "Max. File Size Exceeded"

			'	Exit Sub
			'Else

			'End If

			'Uplaod Document should be <= 2MB
			If e.FileSize > 2000000 Then
				Session("FileSizeExceeded") = True
				Session("Document") = Nothing
			Else
				Session("FileSizeExceeded") = False
				Me.flReqDocUpload.SaveAs(Server.MapPath(strUploadPath))
				flReqDocUpload.Dispose()
				Session("Document") = Nothing
			End If


		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try

	End Sub

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Try

			Dim myState As New States, myLGA As New LGA

			imgPassport.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", txtPIN.Text, 1, Server.MapPath("~/Logs"))
			imgSignature.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", txtPIN.Text, 2, Server.MapPath("~/Logs"))

			Dim cr As New Core, dt As DataTable ', dtApplications As DataTable
			ViewState("EmployerHistoryCollection") = Nothing
			dt = cr.getPMEnhancementPersonInformation(Me.txtPIN.Text)
			'dtApplications = cr.PMgetApplicationByPIN(Me.txtPIN.Text, "")
			'ViewState("dtApplications") = dtApplications

			Session("PIN") = Me.txtPIN.Text

			If dt.Rows.Count > 0 Then
				ViewState("Employerid") = dt.Rows(0).Item("employerid")
				ViewState("EmployeeID") = dt.Rows(0).Item("employeeid")
				ViewState("EmployerCode") = dt.Rows(0).Item("EmployerCode")
				ViewState("Sector") = dt.Rows(0).Item("Sector")
				lblTitle.Text = dt.Rows(0).Item("Title")
				Me.txtSurname.Text = dt.Rows(0).Item("Surname")
				Me.txtFirstName.Text = dt.Rows(0).Item("FirstName")
				Me.txtOtherNames.Text = dt.Rows(0).Item("MiddleName")
				Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")
				Me.txtAge.Text = dt.Rows(0).Item("Age")

				Me.txtDelaredAge.Text = dt.Rows(0).Item("Age")

				Me.txtEmployer.Text = dt.Rows(0).Item("EmployerName").ToString

				Me.txtEmail.Text = dt.Rows(0).Item("email").ToString
				Me.txtSex.Text = dt.Rows(0).Item("sex").ToString

				Me.txtPhone.Text = dt.Rows(0).Item("Phone").ToString

				Me.txtRSABalance.Text = CDbl(dt.Rows(0).Item("numCurrentRSABalance"))

				Me.txtOldPension.Text = CDbl(dt.Rows(0).Item("numMonthPension"))
				Me.txtRecommendedAmount.Text = CDbl(dt.Rows(0).Item("numNewPension"))

				txtReferenceNo.Text = dt.Rows(0).Item("pkiEnhancementID").ToString

				'''''''''''''''''''''''''''''''''''''''''''''''''''''''''

				Me.txtAccountName.Text = (dt.Rows(0).Item("AccountName")).ToString
				Me.txtAccountNumber.Text = (dt.Rows(0).Item("AccountName")).ToString


				'           If Not dt.Rows(0).Item("BankID") Is DBNull.Value Then
				'                Me.ddBankName.SelectedItem.Text = Me.getBankName(CInt(dt.Rows(0).Item("BankID"))).ToString
				'           Else
				'                ddBankName.SelectedItem.Text = ""
				'End If

				'PMgetBanks

				If Not dt.Rows(0).Item("BankID") Is DBNull.Value Then

					Me.ddBankName.SelectedItem.Text = cr.PMgetBanks(CInt(dt.Rows(0).Item("BankID"))).Rows(0).Item("bankname")
				Else
					ddBankName.SelectedItem.Text = ""
				End If



				'           If Not dt.Rows(0).Item("BankBranchID") Is DBNull.Value Then
				'                Dim lstBankBranches As New List(Of String)
				'	lstBankBranches = getBankBranches(CInt(dt.Rows(0).Item("BankID")))
				'                lstBankBranches = Nothing
				'                Me.ddBankBranch.Items.Add(Me.getBankBranchName(CInt(dt.Rows(0).Item("BankBranchID"))).ToString)
				'                'Me.ddBankBranch.SelectedItem.Text = Me.getBankBranchName(CInt(dt.Rows(0).Item("BankBranchID"))).ToString
				'           ElseIf ddBankBranch.Items.Count > 0 Then
				'                'txtPermanentLGA.Text = myLGA.getLGAName(0).ToString
				'                ddBankBranch.SelectedItem.Text = ""
				'           Else

				'End If

				'	MsgBox("Branch :" & dt.Rows(0).Item("BankBranchID").ToString & " Bank :" & dt.Rows(0).Item("BankID").ToString)


				If Not dt.Rows(0).Item("BankBranchID") Is DBNull.Value And Not dt.Rows(0).Item("BankID") Is DBNull.Value Then
					'
					' And 

					If cr.PMgetBankBranches(CInt(dt.Rows(0).Item("BankID")), CInt(dt.Rows(0).Item("BankBranchID"))).Rows.Count > 0 Then
						Dim lstBankBranches As New DataTable
						lstBankBranches = cr.PMgetBankBranches(CInt(dt.Rows(0).Item("BankID")), CInt(dt.Rows(0).Item("BankBranchID")))

						Me.ddBankBranch.Items.Add(lstBankBranches.Rows(0).Item("BranchName") & "                   | " & lstBankBranches.Rows(0).Item("BankBranchID"))
					Else

					End If

				ElseIf ddBankBranch.Items.Count > 0 Then
					'txtPermanentLGA.Text = myLGA.getLGAName(0).ToString
					ddBankBranch.SelectedItem.Text = ""
				Else


				End If


				' checking if customer has previous applications and displaying to the user 

				'If dtApplications.Rows.Count > 0 Then

				'	ViewState("PreviousApplication") = dtApplications
				'	ViewState("FileNumber") = CStr(dtApplications.Rows(0).Item("txtFileNo").ToString)

				'	Me.BindGridPreviousApps(dtApplications)
				'	Me.MPPreviousApps.Show()
				'Else
				'End If


				''''''''''''''''''''''''''''''''''''''''''''


			Else
				Response.Redirect("frmApplication.aspx")

			End If
		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message & "New Application Participant Find")
			pnlError.Visible = True
			'  Me.lblError.Text = "Error Loading Bank Branches"

		Finally

			'Dim dtApplications As New DataTable
			'If Not IsNothing(ViewState("dtApplications")) = True Then

			'	dtApplications = ViewState("dtApplications")
			'	If dtApplications.Rows.Count > 0 Then

			'		ViewState("PreviousApplication") = dtApplications
			'		ViewState("FileNumber") = CStr(dtApplications.Rows(0).Item("txtFileNo").ToString)

			'		Me.BindGridPreviousApps(dtApplications)
			'		Me.MPPreviousApps.Show()
			'	Else
			'	End If

			'Else
			'End If

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

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Try

			Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtusers As New DataTable

			scriptManagerA = ScriptManager.GetCurrent(Me.Page)
			scriptManagerA.RegisterPostBackControl(Me.gridRecievedDocument)

			'scriptManagerB = ScriptManager.GetCurrent(Me.Page)
			'scriptManagerB.RegisterPostBackControl(Me.btnOtherDetails)

			'btnOtherDetails

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					'   getApprovalType()
					Response.Redirect("Login.aspx")
				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then


					dtusers = Session("userDetails")
					getUserAccessMenu(Session("user"))

					ViewState("emailAddress") = Nothing
					ViewState("Telephone") = Nothing
					ViewState("OfficeAddress") = Nothing
					ViewState("ResidentailAddress") = Nothing
					ViewState("PermanentHomeAddress") = Nothing

					Me.txtRecievedDate.Text = Now.Date
					Me.txtApplicationDate.Text = Now.Date

					txtRecievedDate.Enabled = False

					getStates()
					populateBank()
					getApprovalTypes()

					'getInsurerTypes()
					'PopulateApplicationStatus()
					'PopulateFundingStatus()
					'PopulateCommentGroup()

				Else
				End If

			Else

			End If

		Catch ex As Exception
			'               MsgBox("" & ex.Message)
		End Try

	End Sub
	'populating the list of approval type in a list object
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
	Protected Sub getApprovalTypes()

		Dim i As Integer = 0
		Dim lstAppTypes As New List(Of String)
		lstAppTypes = getApprovalType()
		ddApplicationType.Items.Clear()
		Do While i < lstAppTypes.Count

			If ddApplicationType.Items.Count = 0 Then
				ddApplicationType.Items.Add("")
				ddApplicationType.Items.Add(lstAppTypes.Item(i))
			ElseIf ddApplicationType.Items.Count > 0 Then
				ddApplicationType.Items.Add(lstAppTypes.Item(i))
			End If
			i = i + 1

		Loop

	End Sub

	Protected Sub populateBank()

		Dim myState As New States, i As Integer = 0, cr As New Core
		Dim lstBank As New DataTable
		lstBank = cr.PMgetBanks
		Me.ddBankName.Items.Clear()

		Do While i < lstBank.Rows.Count

			If Me.ddBankName.Items.Count = 0 Then
				Me.ddBankName.Items.Add("")
				Me.ddBankName.Items.Add(lstBank.Rows(i).Item("bankname"))
				BankTypeCollection.Add(lstBank.Rows(i).Item("bankname"), lstBank.Rows(i).Item("BankID"))

			ElseIf Me.ddBankName.Items.Count > 0 Then
				Me.ddBankName.Items.Add(lstBank.Rows(i).Item("bankname"))
				BankTypeCollection.Add(lstBank.Rows(i).Item("bankname"), lstBank.Rows(i).Item("BankID"))

			End If
			i = i + 1

		Loop
		ViewState("BankTypeCollection") = BankTypeCollection

	End Sub

	Protected Sub getStates()

		Dim myState As New States, i As Integer = 0
		Dim lstState As New List(Of String)
		lstState = myState.getStates
		ddApplicationState.Items.Clear()
		Do While i < lstState.Count

			If ddApplicationState.Items.Count = 0 Then
				ddApplicationState.Items.Add("")
				ddApplicationState.Items.Add(lstState.Item(i))
			ElseIf ddApplicationState.Items.Count > 0 Then
				ddApplicationState.Items.Add(lstState.Item(i))
			End If
			i = i + 1

		Loop

	End Sub

	Protected Sub ddApplicationState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddApplicationState.TextChanged

		Dim myState As New States, myStateID As Integer
		myStateID = myState.getStateID(Me.ddApplicationState.SelectedValue)

		Dim myOfficeLocation As New OfficeLocation, i As Integer = 0

		Dim lstOfficeLocation As New List(Of String)
		lstOfficeLocation = myOfficeLocation.getStateOfficeLocation(myStateID)
		ddPFAOfficeLocation.Items.Clear()

		Do While i < lstOfficeLocation.Count

			If ddPFAOfficeLocation.Items.Count = 0 Then
				ddPFAOfficeLocation.Items.Add("")
				ddPFAOfficeLocation.Items.Add(lstOfficeLocation.Item(i))
			ElseIf ddPFAOfficeLocation.Items.Count > 0 Then
				ddPFAOfficeLocation.Items.Add(lstOfficeLocation.Item(i))
			End If
			i = i + 1

		Loop

	End Sub

	Protected Sub ddBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddBankName.SelectedIndexChanged

		BankTypeCollection = ViewState("BankTypeCollection")
		Dim lstBankBranches As New DataTable, i As Integer = 0, cr As New Core
		lstBankBranches = cr.PMgetBankBranches(CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text)))

		Me.ddBankBranch.Items.Clear()
		Do While i < lstBankBranches.Rows.Count

			If Me.ddBankBranch.Items.Count = 0 Then
				Me.ddBankBranch.Items.Add("")
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
			ElseIf Me.ddBankBranch.Items.Count > 0 Then
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
			End If

			i = i + 1

		Loop

	End Sub

	Protected Sub ddApplicationType_TextChanged(sender As Object, e As EventArgs) Handles ddApplicationType.TextChanged


		Try
			Dim lstAppDocs As List(Of String), i As Integer, appType As Integer = 0, cr As New Core, dtApplications As New DataTable
			' Me.lblAppTypeError.Text = "Error On Selection"
			'	pnlAppTypeVerificationError.Visible = False
			'ViewState("RMAS") = Nothing

			'fkiAppTypeId()
			'Testing if application type should be unique

			If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then

				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
				appType = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))

				If Not IsNothing(ViewState("PreviousApplication")) = True Then

					dtApplications = ViewState("PreviousApplication")

					Dim n As Integer = 0, isAppMustUnique As Boolean
					isAppMustUnique = cr.PMIsApplicationTypeUnique(appType)

					Do While n < dtApplications.Rows.Count

						If appType = dtApplications.Rows(n).Item("fkiAppTypeId") And isAppMustUnique = True Then
							'pnlAppTypeVerificationError.Visible = True
							'Me.lblAppTypeError.Text = "Application Already Exit for this Type !. "
							Me.ddRequiredDocuments.Items.Clear()
							Exit Sub
						Else

						End If
						n = n + 1
					Loop

				Else

				End If

				Select Case appType

					Case Is = 1

						

					Case Is = 2


					Case Is = 5


					Case Is = 3


					Case Is = 4


					Case Else


				End Select

				'populateExitReasons(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

				'If Me.txtSector.Text = "Public" Then
				'	lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), Me.txtSector.Text)
				'Else
				lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "Private")
				'End If

				ddRequiredDocuments.DataSource = lstAppDocs

				ddRequiredDocuments.Items.Clear()

				Do While i < lstAppDocs.Count

					If ddRequiredDocuments.Items.Count = 0 Then
						ddRequiredDocuments.Items.Add("")
						ddRequiredDocuments.Items.Add(lstAppDocs.Item(i))
					ElseIf ddRequiredDocuments.Items.Count > 0 Then
						ddRequiredDocuments.Items.Add(lstAppDocs.Item(i))
					End If
					i = i + 1

				Loop

			Else

			End If

			''retrieving the cached unsaved previously uploaded documents for a customer and a particular application type from the NPM_Doc_Temp folder
			'Dim tempFilePath As String = Server.MapPath("~/NPM_Doc_Temp/" & Session("user"))
			''	Dim NewDirectory As String = "~/NPM_Doc_Temp/" & Session("user")
			''If Directory.Exists("C:\NPM_Doc_Temp\" & Session("user")) = True Then
			'If Directory.Exists(tempFilePath) = True Then
			'	Dim lstFiles As String()
			'	lstFiles = Directory.GetFiles(tempFilePath)
			'	loadFilesFromTempFolder(lstFiles)
			'Else

			'End If

			'i = 0

		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try

	End Sub

	Public Function getDocumentTypes(approvalTypeID As Integer, sector As String) As List(Of String)

		Dim dc As New AppDocumentsDataContext
		Dim lstDocument As New List(Of String)
		Dim query = From m In dc.tblApplicationTypes Join n In dc.tblAppDocumentTypes On m.pkiAppTypeId Equals n.appTypeID Join o In dc.tblDocumentTypes On o.pkiDocumentTypeID Equals n.fkiDocumentTypeID Where m.pkiAppTypeId = approvalTypeID And n.txtSector = sector _
				  Select New With {o.pkiDocumentTypeID, o.txtDocumentName}
		For Each a In query

			If lstDocument.Contains(a.txtDocumentName) = False Then

				lstDocument.Add(a.txtDocumentName)
				DocumentCollection.Add(a.txtDocumentName, a.pkiDocumentTypeID)

			Else

			End If

		Next
		ViewState("DocumentCollection") = DocumentCollection
		Return lstDocument

	End Function

	Protected Sub ddRequiredDocuments_TextChanged(sender As Object, e As EventArgs) Handles ddRequiredDocuments.SelectedIndexChanged

		'If Me.ddRequiredDocuments.SelectedItem.Text = "Others" Then

		'	'Me.dvUploadOtherDocuments.Visible = True
		'	Session("Document") = Me.ddRequiredDocuments.SelectedItem.Text

		'	'Accrued Rights Letter'
		'Else



		Session("Document") = Me.ddRequiredDocuments.SelectedItem.Text
		Dim strUploadPath As String = Server.MapPath("~/FileUploads/" & Session("user"))
		MakeDirectoryIfExists(strUploadPath)


		'End If

	End Sub

	Private Sub DeleteDir(ByVal DirPath As String)

		Try
			If Directory.Exists(DirPath) Then
				'File.Delete(DirPath)
				Directory.Delete("", True)
			Else
			End If
		Catch ex As Exception

		End Try

	End Sub

	'creating temporary folder to upload scanned document before moving to the permanent location on saving the application
	Private Sub MakeDirectoryIfExists(ByVal NewDirectory As String)

		Try
			' Check if directory exists
			If Not Directory.Exists(NewDirectory) Then
				' Create the directory.
				Directory.CreateDirectory(NewDirectory)
			ElseIf Directory.Exists(NewDirectory) Then
				DeleteDir(NewDirectory)
				Directory.CreateDirectory(NewDirectory)
			End If
		Catch _ex As IOException
			Response.Write(_ex.Message)
		End Try

	End Sub

	Protected Sub btnAddRecievedDoc_Click(sender As Object, e As EventArgs) Handles btnAddRecievedDoc.Click

		Try


			If Not IsNothing(Session("FileSizeExceeded")) = True And Session("FileSizeExceeded") = True Then

				lblFileSizeError.Text = "Max. File Size Exceeded"
				Me.dvfileSizeError.Visible = True
				Exit Sub

			Else

			End If



			Dim ary As IList(Of DataRow)

			If Not IsNothing(ViewState("RecievedDocument")) Then

				dtDocuments = ViewState("RecievedDocument")
				ary = dtDocuments.Select()

				
					Dim query = From n In ary
					  Where n.Item("DocumentName") = Me.ddRequiredDocuments.SelectedValue
					If query.Count > 0 Then
						Exit Sub
					Else
					End If


			Else

				dtDocuments = New DataTable
				dtColumn = New DataColumn("DocumentName")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("RecievedDate")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("DocumentPath")
				dtDocuments.Columns.Add(dtColumn)
				dtColumn = New DataColumn("IsVerified")
				dtDocuments.Columns.Add(dtColumn)


			End If

			'Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

			Dim newCustomersRow As DataRow
			newCustomersRow = dtDocuments.NewRow()


			'If Me.txtAdhocDocDescription.Text <> "" Then
			'	newCustomersRow("DocumentName") = Me.txtAdhocDocDescription.Text
			'Else
			newCustomersRow("DocumentName") = Me.ddRequiredDocuments.SelectedValue
			'End If


			newCustomersRow("RecievedDate") = Me.txtRecievedDate.Text

			'''''getting the ID of the user selected application type

			'''''reviewing the collection of all the available application types from memory
			If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			Else
			End If
			Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

			If Not IsNothing(Session("documentPath")) = True Then

				'KeepTempAppDocument(Session("documentPath").ToString)

				Dim sarrMyString As String() = Session("documentPath").ToString.Split(New String() {"PEN"}, StringSplitOptions.None)

				If sarrMyString.Count = 1 Then
					sarrMyString = Session("documentPath").ToString.Split(New String() {"DBA"}, StringSplitOptions.None)
					newCustomersRow("DocumentPath") = "DBA" + sarrMyString(1).ToString
				Else
					newCustomersRow("DocumentPath") = "PEN" + sarrMyString(1).ToString
					'saving the documents in a temp folder to allow auto-retrieval of document should the application was not submitted
					KeepTempAppDocument(Session("documentPath").ToString, "PEN" + sarrMyString(1).ToString, typeID, Me.ddRequiredDocuments.SelectedItem.Text.ToString)

				End If

			Else

				newCustomersRow("DocumentPath") = ""

			End If



			'''''getting the ID of the user selected application type
			'Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

			'checking if the verified document checkbox was checked by the user for dead benefit and set as verified by default for all other application type

			If chkDocVerified.Checked = True And typeID = 5 Then
				newCustomersRow("IsVerified") = "1"
			ElseIf chkDocVerified.Checked = False And typeID = 5 Then
				newCustomersRow("IsVerified") = "0"
			Else
				newCustomersRow("IsVerified") = "1"
			End If

			dtDocuments.Rows.Add(newCustomersRow)

			ViewState("RecievedDocument") = dtDocuments
			Session("documentPath") = Nothing
			loadGrid(dtDocuments)
			chkDocVerified.Checked = False

			'isVerified

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)
			Me.lblError.Text = "Error Adding Documents"

		End Try

	End Sub

	Private Sub KeepTempAppDocument(docPath As String, docNewName As String, typeID As Integer, RequiredName As String)

		'Dim NewDirectory As String = "c:\NPM_Doc_Temp\" & Session("user")

		Dim NewDirectory As String = Server.MapPath("~/NPM_Doc_Temp/" & Session("user"))

		Try

			If Not Directory.Exists(NewDirectory) Then
				' Create the directory.
				Directory.CreateDirectory(NewDirectory)

				'NewDirectory = NewDirectory & "\" & typeID & "_" & RequiredName.Replace("|", "_") & "_" & docNewName
				NewDirectory = NewDirectory & "\" & typeID & "_" & RequiredName.Replace("|", "-") & "_" & docNewName
				'NewDirectory = NewDirectory & "\" & typeID & "_" & RequiredName & "_" & docNewName

				Dim fpath As String
				fpath = Server.MapPath("~/FileUploads/" & Session("user") & "/" & docNewName)
				File.Copy(fpath, NewDirectory)

			ElseIf Directory.Exists(NewDirectory) Then

				'NewDirectory = NewDirectory & "\" & typeID & "_" & RequiredName.Replace("|", "_") & "_" & docNewName
				NewDirectory = NewDirectory & "\" & typeID & "_" & RequiredName.Replace("|", "-") & "_" & docNewName
				'NewDirectory = NewDirectory & "\" & typeID & "_" & RequiredName & "_" & docNewName

				Dim fpath As String
				fpath = Server.MapPath("~/FileUploads/" & Session("user") & "/" & docNewName)
				File.Copy(fpath, NewDirectory)

			End If

		Catch ex As Exception

		End Try

	End Sub



	'populating recieved document for benefit application
	Protected Sub loadGrid(dt As DataTable)
		Try

			If dt.Rows.Count > 5 Then

				pnlUploadDetail.Height = Nothing
				dvRecievedDocument.Style.Item("height") = Nothing

			End If

			gridRecievedDocument.DataSource = dt
			gridRecievedDocument.DataBind()

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
		End Try
	End Sub

	Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click


		Dim date2 As Date = Date.Parse(txtDOB.Text)
		Dim date1 As Date = Now
		Dim years As Long = DateDiff(DateInterval.Year, date2, date1)
		Dim cr As New Core

		If Not IsNothing(Session("user")) = True Then
			'InsurerTypeCollection = ViewState("InsurerTypeCollection")
		Else
			Response.Redirect("login.aspx")
		End If

		

		If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		Else
		End If


		Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))
		'Dim isAppExisting As Boolean = cr.PMIsApplicationAlreadyExist(Me.txtPIN.Text, typeID)

		'If isAppExisting = True Then

		'	lblError.Text = "Application Already Exists"
		'	Me.pnlError.Visible = True
		'	Exit Sub

		'End If

		If Me.chkConfirmedPassport.Checked = False Then
			Me.lblError.Visible = True
			pnlError.Visible = True
			Me.lblError.Text = "Please Check to Confirm Picture and Signature are Okay"
			Exit Sub
		ElseIf Me.chkConfirmedSignature.Checked = False Then
			pnlError.Visible = True
			Me.lblError.Visible = True
			Me.lblError.Text = "Please Check to Confirm Picture and Signature are Okay"
			Exit Sub
		End If


		Dim appDetail As New ApplicationDetail, NextAppID As Integer, ApplicationCode As String, FileNumber As String, sector As String, myID As Integer
		Dim appDocDetails As New List(Of ApplicationDocumentDetail), appAdhocDocDetails As New List(Of AdhocDocuments), appCheckList As New ApplicationCheckList, appCheckListDBA As New ApplicationCheckListDOB

		NextAppID = cr.PMgetNextApplicationID()

		If Page.IsValid And IsNothing(ViewState("EmployeeID")) = False Then

			If Me.ddRequiredDocuments.Items.Count = 0 Then
				Exit Sub
			Else
			End If
			sector = ViewState("Sector")

			If IsNothing(ViewState("FileNumber")) = False Then

				FileNumber = CStr(ViewState("FileNumber"))

			Else

				Select Case sector

					Case Is = "Private"
						FileNumber = "PR-" & NextAppID.ToString
					Case Is = "Public"
						FileNumber = "PU-" & NextAppID.ToString
					Case Else
						FileNumber = "SF-" & NextAppID.ToString

				End Select

			End If

			'generating application code
			ApplicationCode = cr.PMgetNextSPLogID(CInt(typeID), "A")


			If Me.chkConfirmedPassport.Checked = True Then
				appDetail.IsPassportConfirmed = 1
			Else
				appDetail.IsPassportConfirmed = 0
			End If


			If Me.chkConfirmedSignature.Checked = True Then
				appDetail.isSignatureConfirmed = 1
			Else
				appDetail.isSignatureConfirmed = 0
			End If

			appDetail.ReferenceApplicationCode = ""
			
			appDetail.FileNumber = FileNumber
			appDetail.Title = Me.lblTitle.Text
			appDetail.ApplicationID = ApplicationCode
			appDetail.Sector = sector

			appDetail.EmployerID = CInt(ViewState("Employerid"))
			appDetail.EmployerName = Me.txtEmployer.Text
			appDetail.EmployerCode = ViewState("EmployerCode").ToString

			appDetail.CreatedBy = CStr(Session("user"))
			appDetail.PIN = Me.txtPIN.Text
			appDetail.Email = LTrim(RTrim(Me.txtEmail.Text))
			appDetail.FullName = Me.txtSurname.Text & " | " & Me.txtFirstName.Text & " | " & Me.txtOtherNames.Text

			If Me.txtDOB.Text = "" Then
				appDetail.DOB = Now.Date
			Else
				appDetail.DOB = CDate(Me.txtDOB.Text)
			End If

		End If

		appDetail.MemberID = CInt(ViewState("EmployeeID"))
		appDetail.ApplicationDate = CDate(Me.txtApplicationDate.Text)
		appDetail.ApplicationState = ddApplicationState.SelectedItem.Text.ToString
		appDetail.Sex = Me.txtSex.Text
		appDetail.AccountName = Me.txtAccountName.Text
		appDetail.AccountNo = Me.txtAccountNumber.Text
		appDetail.BVN = Me.txtBVN.Text
		appDetail.Comment = Me.txtOtherComments.Text
		appDetail.ApprovedAmount = txtRecommendedAmount.Text
		appDetail.RSABalance = txtRSABalance.Text
		appDetail.IsRetirement = True
		appDetail.ReferenceApplicationCode = txtReferenceNo.Text
		'" & AppDetail.ApprovedAmount & "','" & AppDetail.RSABalance & "'


		If Me.chkBankConfirmed.Checked = True Then
			appDetail.IsBankDetailsConfirmed = 1
		Else
			appDetail.IsBankDetailsConfirmed = 0
		End If


		If Not IsNothing(ViewState("BankTypeCollection")) = True Then
			BankTypeCollection = ViewState("BankTypeCollection")
			appDetail.BankID = CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text.ToString))
		Else
		End If


		If Me.ddBankBranch.SelectedItem.Text.ToString <> "" Then
			'BankBranchCollection = ViewState("BankBranchCollection")
			appDetail.BranchID = CInt(Me.ddBankBranch.SelectedItem.Text.ToString.Split("|")(1))
		Else
		End If


		If Me.ddPFAOfficeLocation.SelectedValue.ToString = "" Then
			appDetail.ApplicationOffice = ddApplicationState.SelectedItem.Text.ToString
		Else
			appDetail.ApplicationOffice = ddPFAOfficeLocation.SelectedItem.Text.ToString
		End If

		If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			appDetail.AppTypeId = typeID
		Else

		End If


		appDetail.ApplicationTypeName = Me.ddApplicationType.SelectedItem.Text.ToString

		Dim docCount As Integer, isAllDocumentScanned As Boolean = True
		' Dim row As gridRecievedDocument.
		If Not IsNothing(ViewState("DocumentCollection")) = True Then
			DocumentCollection = ViewState("DocumentCollection")

			'creating data object for the recieved documents
			Do While docCount < Me.gridRecievedDocument.Rows.Count
				Dim appDocDetail As New ApplicationDocumentDetail

				Dim row As GridViewRow = gridRecievedDocument.Rows(docCount)

				

					appDocDetail.DocumentTypeID = CInt(DocumentCollection.Item(row.Cells(1).Text))
					appDocDetail.DateReceived = CDate((row.Cells(2).Text))
					'checking if all the documents had been scanned 

					If CStr((row.Cells(3).Text)) = "&nbsp;" Then
						isAllDocumentScanned = False
					Else
						isAllDocumentScanned = True
					End If
					appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

					'appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

					appDocDetail.IsVerified = CInt((row.Cells(5).Text))
					appDocDetails.Add(appDocDetail)


					docCount = docCount + 1
			Loop
		Else
		End If

		appDetail.CommentGroup = Me.ddCommentGroup.Text

		'''''''''''''''''''''
		'''remove this ''''''

		'  Exit Sub

		'MsgBox("" & Me.ddRequiredDocuments.Items.Count - 1)
		'MsgBox("" & Me.gridRecievedDocument.Rows.Count)
		'MsgBox("" & Me.ddStatus.SelectedItem.Text)


		If ((Me.ddRequiredDocuments.Items.Count - 1) = Me.gridRecievedDocument.Rows.Count) = True And isAllDocumentScanned = True Then
			appDetail.Status = "Documentation"

			appDetail.DateDocumentCompleted = DateTime.Parse(Now)

			appDetail.DocCompleted = 1
		ElseIf ((Me.ddRequiredDocuments.Items.Count - 1) = Me.gridRecievedDocument.Rows.Count) = True And isAllDocumentScanned = False Then
			appDetail.Status = "Entry"
			appDetail.DocCompleted = 0
			appDetail.DateDocumentCompleted = Nothing

		ElseIf ((Me.ddRequiredDocuments.Items.Count - 1) = Me.gridRecievedDocument.Rows.Count) = False Then
			appDetail.Status = "Entry"
			appDetail.DocCompleted = 0
			appDetail.DateDocumentCompleted = Nothing
		Else
			appDetail.Status = ddStatus.SelectedItem.Text
			Dim selStat As String = Me.ddStatus.SelectedItem.Text

			Select Case selStat
				Case Is = "Documentation"
					appDetail.DateDocumentCompleted = Nothing
					appDetail.DocCompleted = 1
				Case Else
					appDetail.DocCompleted = 0
			End Select

		End If

		Dim boolSubmitStatus As Boolean
		Try
			If Not IsNothing(Session("user")) = True Then

				boolSubmitStatus = cr.PMSubmitApplication(appDetail, appDocDetails, appAdhocDocDetails, Session("user"), Server.MapPath("~/Logs"), appCheckList, appCheckListDBA)


				If boolSubmitStatus = True Then
					'deleting possible duplicate document created
					cr.PMCleanDuplicateDocument(appDetail.ApplicationID)
					Dim NewDirectory As String = Server.MapPath("~/NPM_Doc_Temp/" & Session("user"))

					If Directory.Exists(NewDirectory) = True Then
						Directory.Delete(NewDirectory, True)
					Else
					End If


					Session("appDetail") = appDetail
					Response.Redirect(String.Format("frmApplicationConfirmation.aspx?ApplicationCode={0}&ReturnPage={1}", Server.UrlEncode(appDetail.ApplicationID), Server.UrlEncode("ApplicationDashBoard")))

				Else

					lblError.Text = "An Error Occur Creating Application !!!"

				End If
			Else
				Response.Redirect("Login.aspx")
			End If

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message & "Application Logging")


		Finally



		End Try




	End Sub
End Class
