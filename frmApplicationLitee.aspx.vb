Imports AjaxControlToolkit
Imports System.Data
Imports System.IO
Partial Class frmApplicationLitee
	Inherits System.Web.UI.Page
	Dim DocumentCollection As New Hashtable
	Dim ApprovalTypeCollection As New Hashtable
	Dim InsurerTypeCollection As New Hashtable
	Dim BankTypeCollection As New Hashtable
	Dim BankBranchCollection As New Hashtable
	Dim blnHardShip As Boolean = False
	Dim blnEnbloc As Boolean = False
	Dim blnLegacy As Boolean = False
	Dim blnAnnuity As Boolean = False
	Dim blnPW As Boolean = False
	Dim blnAVC As Boolean = False
	Dim blnDB As Boolean = False
	Dim blnNSITF As Boolean = False

	

	Protected Sub AjaxFileDocumentUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)

	End Sub
	Protected Sub BtnViewDetails_Click()

	End Sub
	Protected Sub gridCustomerHistory_RowDataBound()

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

	'populating the list of insurance Coy from enpower in a list object
	Public Function getInsurer() As List(Of String)

		Dim lstInsurer As New List(Of String)
		Dim dc As New InsurerDataContext
		Dim query = From m In dc.Insurers
				  Select m
		For Each a As Insurer In query
			lstInsurer.Add(a.InsurerName)
			InsurerTypeCollection.Add(a.InsurerName, a.InsurerID)
		Next
		ViewState("InsurerTypeCollection") = InsurerTypeCollection
		Return lstInsurer

	End Function

	Protected Sub getInsurerTypes()

		Dim i As Integer = 0
		Dim lstInsurerTypes As New List(Of String)
		lstInsurerTypes = getInsurer()
		ddInsuranceCoy.Items.Clear()
		Do While i < lstInsurerTypes.Count

			If ddInsuranceCoy.Items.Count = 0 Then
				ddInsuranceCoy.Items.Add("")
				ddInsuranceCoy.Items.Add(lstInsurerTypes.Item(i))
			ElseIf ddInsuranceCoy.Items.Count > 0 Then
				ddInsuranceCoy.Items.Add(lstInsurerTypes.Item(i))
			End If
			i = i + 1

		Loop

	End Sub
	Protected Sub PopulateFundingStatus()
		ddFundingStatus.Items.Add("")
		ddFundingStatus.Items.Add("Funded")
		ddFundingStatus.Items.Add("UnFunded")
	End Sub
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		If IsPostBack = False Then

			'If Not Request.Cookies("userInfo") Is Nothing Then
			'Else
			'Session("user") = "a-ajanaku"

			Me.txtRecievedDate.Text = Now.Date
			Me.txtApplicationDate.Text = Now.Date
			txtRecievedDate.Enabled = False
			getStates()
			populateBank()
			getApprovalTypes()
			getInsurerTypes()
			'PopulateApplicationStatus()
			PopulateFundingStatus()


		Else
			'End If

		End If



		'If Not Request.Cookies("userInfo") Is Nothing Then
		'Label1.Text = _
		'    Server.HtmlEncode(Request.Cookies("userInfo")("user"))
		'Label2.Text = _
		'    Server.HtmlEncode(Request.Cookies("userInfo")("lastVisit"))
		'End If

	End Sub

	Protected Sub btnAddRecievedDoc_Click(sender As Object, e As EventArgs) Handles btnAddRecievedDoc.Click

		Dim docCount As Integer, fileName As String, uploadDocCount As Integer = 0
		Dim uploadedFiles As HttpFileCollection = Request.Files

		'MsgBox("" & uploadedFiles.Count)
		'Session("Document")
		'.MapPath("FileUploads/" & Session("user") 

		If Directory.Exists(Server.MapPath("FileUploads/" & Session("user"))) = True Then
		Else
			Directory.CreateDirectory(Server.MapPath("FileUploads/" & Session("user")))
		End If

		Dim aryDocIDList As New ArrayList
		Dim aryDocNameList As New ArrayList
		Dim aryDroppedDocs As New ArrayList

		Try

			Do While docCount < Me.gridRecievedDocument.Rows.Count

				Dim row As GridViewRow = gridRecievedDocument.Rows(docCount)
				aryDocIDList.Add(row.Cells(0).Text)
				aryDocNameList.Add(row.Cells(1).Text)
				docCount = docCount + 1

			Loop

			aryDocIDList.TrimToSize()
			aryDocNameList.TrimToSize()

			docCount = 0
			Dim dtRecievedDocument = New DataTable, dtColumn As DataColumn

			dtColumn = New DataColumn("DocumentID")
			dtRecievedDocument.Columns.Add(dtColumn)
			dtColumn = New DataColumn("FileName")
			dtRecievedDocument.Columns.Add(dtColumn)

			Do While docCount < uploadedFiles.Count

				Dim userPostedFile As HttpPostedFile = uploadedFiles(docCount)

				fileName = userPostedFile.FileName



				'MsgBox("" & fileName.Split(".")(0))
				'If (fileName.Split(".")(0).Split("\").Length > 1) Then
				'	MsgBox("" & fileName.Split(".")(0).Split("\")(fileName.Split(".")(0).Split("\").Length - 1))
				'	Exit Sub
				'Else
				'	Exit Sub
				'End If


				If aryDocIDList.Contains(fileName.Split(".")(0)) Then

					Dim fileNewName, strUploadPath As String
					fileNewName = aryDocNameList(aryDocIDList.IndexOf(fileName.Split(".")(0)))
					fileNewName = fileNewName.Replace(" | ", "_")
					fileNewName = fileNewName.Replace("|", "_")
					fileNewName = fileNewName.Replace(" ", "_")
					fileNewName = fileNewName.Replace("(", "_")
					fileNewName = fileNewName.Replace(")", "_")
					'Try
					strUploadPath = "~/FileUploads/" & Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1)
					strUploadPath = Server.MapPath("FileUploads/" & Session("user") & "/" & Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1))

					'Catch ex As Exception

					'Me.lblFileSizeError.Text = "Bad Application Document Type File Name"
					'lblFileSizeError.Visible = True
					'Exit Sub
					'End Try

					userPostedFile.SaveAs(strUploadPath)

					'keeing the list of dropped application docs
					aryDroppedDocs.Add(fileName.Split(".")(0))

					Dim newCustomersRow As DataRow
					newCustomersRow = dtRecievedDocument.NewRow()

					newCustomersRow("DocumentID") = fileName.Split(".")(0)
					newCustomersRow("FileName") = Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1)

					dtRecievedDocument.Rows.Add(newCustomersRow)



					uploadDocCount = uploadDocCount + 1

				ElseIf aryDocIDList.Contains(fileName.Split(".")(0).Split("\")(fileName.Split(".")(0).Split("\").Length - 1)) Then


					'If (fileName.Split(".")(0).Split("\").Length > 1) Then
					'	MsgBox("" & fileName.Split(".")(0).Split("\")(fileName.Split(".")(0).Split("\").Length - 1))
					'	Exit Sub
					'Else
					'	Exit Sub
					'End If




					Dim fileNewName, strUploadPath As String
					fileNewName = aryDocNameList(aryDocIDList.IndexOf(fileName.Split(".")(0).Split("\")(fileName.Split(".")(0).Split("\").Length - 1)))
					fileNewName = fileNewName.Replace(" | ", "_")
					fileNewName = fileNewName.Replace("|", "_")
					fileNewName = fileNewName.Replace(" ", "_")
					fileNewName = fileNewName.Replace("(", "_")
					fileNewName = fileNewName.Replace(")", "_")
					'Try
					strUploadPath = "~/FileUploads/" & Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1)
					strUploadPath = Server.MapPath("FileUploads/" & Session("user") & "/" & Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1))

					'Catch ex As Exception

					'Me.lblFileSizeError.Text = "Bad Application Document Type File Name"
					'lblFileSizeError.Visible = True
					'Exit Sub
					'End Try

					userPostedFile.SaveAs(strUploadPath)

					'keeing the list of dropped application docs
					'aryDroppedDocs.Add(fileName.Split(".")(0))
					aryDroppedDocs.Add(fileName.Split(".")(0).Split("\")(fileName.Split(".")(0).Split("\").Length - 1))

					Dim newCustomersRow As DataRow
					newCustomersRow = dtRecievedDocument.NewRow()

					newCustomersRow("DocumentID") = fileName.Split(".")(0).Split("\")(fileName.Split(".")(0).Split("\").Length - 1)
					newCustomersRow("FileName") = Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1)

					dtRecievedDocument.Rows.Add(newCustomersRow)



					uploadDocCount = uploadDocCount + 1


				Else


				End If

				docCount = docCount + 1

			Loop
			Session("aryDroppedDocs") = aryDroppedDocs
			Session("TotalFileUploaded") = uploadDocCount
			Session("dtRecievedDocument") = dtRecievedDocument


		Catch ex As Exception

		End Try



	End Sub

	Protected Sub ddApplicationType_TextChanged(sender As Object, e As EventArgs) Handles ddApplicationType.TextChanged

		Try
			Dim lstAppDocs As List(Of String), i As Integer, appType As Integer = 0, cr As New Core, dtApplications As New DataTable

			If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then

				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
				appType = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))

				If Not IsNothing(ViewState("PreviousApplication")) = True Then

					dtApplications = ViewState("PreviousApplication")

					Dim n As Integer = 0, isAppMustUnique As Boolean
					isAppMustUnique = cr.PMIsApplicationTypeUnique(appType)

					Do While n < dtApplications.Rows.Count

						If appType = dtApplications.Rows(n).Item("fkiAppTypeId") And isAppMustUnique = True Then
							pnlAppTypeVerificationError.Visible = True
							Me.lblAppTypeError.Text = "Application Already Exit for this Type !. "

							Exit Sub
						Else

						End If
						n = n + 1
					Loop

				Else

				End If

				Select Case appType

					Case Is = 1

						Me.dvVerified.Visible = False
						Dim date2 As Date = Date.Parse(txtDOB.Text)
						Dim date1 As Date = Now
						Dim years As Long = DateDiff(DateInterval.Year, date2, date1)

						If years < 50 Then

							pnlAppTypeVerificationError.Visible = True
							dvOverrideAge.Visible = True
							Me.lblAppTypeError.Text = "Application Type Error !. Age Most be More than 50 and above"

							Dim dt As New DataTable
							Me.gridRecievedDocument.DataSource = dt
							gridRecievedDocument.DataBind()

						Else

							Me.chkOverrideAgeBarrier.Checked = False
							dvOverrideAge.Visible = False

						End If

						Me.dvDownloadCalculator.Visible = False
						divAnnRunningPW.Visible = False

					Case Is = 2

						Me.dvVerified.Visible = False
						dvOverrideAge.Visible = False
						Dim date2 As Date = Date.Parse(txtDOB.Text)
						Dim date1 As Date = Now
						Dim years As Long = DateDiff(DateInterval.Year, date2, date1)

						If years > 50 Then

							pnlAppTypeVerificationError.Visible = True
							Me.lblAppTypeError.Text = "Application Type Error !. Age Not Less Than 50"

							Dim dt As New DataTable
							Me.gridRecievedDocument.DataSource = dt
							gridRecievedDocument.DataBind()

							Exit Sub
						Else

						End If
						Me.dvDownloadCalculator.Visible = False
						divAnnRunningPW.Visible = False

					Case Is = 5

						Me.dvVerified.Visible = True
						dvOverrideAge.Visible = False
						Me.chkOverrideAgeBarrier.Checked = False
						divAnnRunningPW.Visible = False
					Case Is = 3


						divAnnRunningPW.Visible = False

					Case Is = 4

						Me.dvDownloadCalculator.Visible = False
						'getAnnuityPW(Me.txtPIN.Text)
						divAnnRunningPW.Visible = True

					Case Else

						Me.dvVerified.Visible = False
						dvOverrideAge.Visible = False
						Me.chkOverrideAgeBarrier.Checked = False

						divAnnRunningPW.Visible = False

				End Select

				populateExitReasons(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

				If Me.txtSector.Text = "Public" Then
					lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), Me.txtSector.Text)
				Else
					lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "Private")
				End If


				'DocumentCollection.Add(a.txtDocumentName, a.pkiDocumentTypeID)


				DocumentCollection = ViewState("DocumentCollection")

				Dim dtDocuments As DataTable, dtColumn As DataColumn
				dtDocuments = New DataTable

				dtColumn = New DataColumn("DocumentID")
				dtDocuments.Columns.Add(dtColumn)

				dtColumn = New DataColumn("DocumentName")
				dtDocuments.Columns.Add(dtColumn)

				i = 0

				Do While i < DocumentCollection.Count

					Dim newCustomersRow As DataRow
					newCustomersRow = dtDocuments.NewRow()

					newCustomersRow("DocumentID") = DocumentCollection.Item(DocumentCollection.Keys(i))

					newCustomersRow("DocumentName") = DocumentCollection.Keys(i)

					dtDocuments.Rows.Add(newCustomersRow)
					i = i + 1

				Loop

				ViewState("RecievedDocument") = dtDocuments

				loadGrid(dtDocuments)


			End If


			i = 0

		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try

	End Sub





	Protected Sub CalAdminLetterDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalAdminLetterDate.SelectionChanged

		Me.PopupControlExtender_CalAdminLetterDate.Commit(Me.CalAdminLetterDate.SelectedDate)
		Me.mpDeathBenefit.Show()


	End Sub

	Protected Sub CalDBARetirement_SelectionChanged(sender As Object, e As EventArgs) Handles CalDBARetirement.SelectionChanged

		Me.PopupControlExtender_CalDBARetirement.Commit(Me.CalDBARetirement.SelectedDate)
		Me.mpDeathBenefit.Show()

	End Sub

	Protected Sub CalDeathDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalDeathDate.SelectionChanged
		Me.PopupControlExtender_CalDeathDate.Commit(Me.CalDeathDate.SelectedDate)
		Me.mpDeathBenefit.Show()
	End Sub

	Protected Sub CalDBValueDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalDBValueDate.SelectionChanged
		Me.PopupControlExtender_CalDBValueDate.Commit(Me.CalDBValueDate.SelectedDate)
		Me.mpDeathBenefit.Show()
	End Sub

	Protected Sub btnDBReValue_Click(sender As Object, e As EventArgs) Handles btnDBReValue.Click

		Dim cr As New Core
		Try

			Me.txtDBARSABalance.Text = cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), CDate(Me.txtDBValueDate.Text), 1)
			Me.mpDeathBenefit.Show()

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


	Public Function getDocumentTypes(approvalTypeID As Integer, sector As String) As List(Of String)

		Dim dc As New AppDocumentsDataContext
		Dim lstDocument As New List(Of String)
		Dim query = From m In dc.tblApplicationTypes Join n In dc.tblAppDocumentTypes On m.pkiAppTypeId Equals n.appTypeID Join o In dc.tblDocumentTypes On o.pkiDocumentTypeID Equals n.fkiDocumentTypeID Where m.pkiAppTypeId = approvalTypeID And n.txtSector = sector _
				  Select New With {o.pkiDocumentTypeID, o.txtDocumentName}
		For Each a In query

			lstDocument.Add(a.txtDocumentName)
			DocumentCollection.Add(a.txtDocumentName, a.pkiDocumentTypeID)

		Next
		ViewState("DocumentCollection") = DocumentCollection
		Return lstDocument

	End Function

	'populating reasons for exit payment per type 
	Protected Sub populateExitReasons(paymentTypeID As Integer)

		Dim myExitReasons As New ExitReasons, i As Integer = 0
		Dim lstReasons As New List(Of String)
		lstReasons = myExitReasons.getExitReasonsTypes(paymentTypeID)

		Me.ddExitReasons.Items.Clear()
		Me.ddPaymentReasons.Items.Clear()

		Do While i < lstReasons.Count

			Select Case paymentTypeID

				Case Is = 1
					'populating rmas screen with the enbloc reasons for application
					If Me.ddPaymentReasons.Items.Count = 0 Then
						Me.ddPaymentReasons.Items.Add("")
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddPaymentReasons.Items.Count > 0 Then
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 16
					'populating rmas screen with the enbloc reasons for application
					If Me.ddPaymentReasons.Items.Count = 0 Then
						Me.ddPaymentReasons.Items.Add("")
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddPaymentReasons.Items.Count > 0 Then
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 2
					'populating rmas screen with the 25% reasons for application
					If Me.ddExitReasons.Items.Count = 0 Then
						Me.ddExitReasons.Items.Add("")
						Me.ddExitReasons.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddExitReasons.Items.Count > 0 Then
						Me.ddExitReasons.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 3
					'populating rmas screen with the programmed withdrawal
					If Me.ddRetirementGroundPW.Items.Count = 0 Then
						Me.ddRetirementGroundPW.Items.Add("")
						Me.ddRetirementGroundPW.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddRetirementGroundPW.Items.Count > 0 Then
						Me.ddRetirementGroundPW.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 14
					'populating rmas screen with the programmed withdrawal
					If Me.ddRetirementGroundPW.Items.Count = 0 Then
						Me.ddRetirementGroundPW.Items.Add("")
						Me.ddRetirementGroundPW.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddRetirementGroundPW.Items.Count > 0 Then
						Me.ddRetirementGroundPW.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 4
					'populating rmas screen with the annuity withdrawal
					If Me.ddRetirementGroundAnnuity.Items.Count = 0 Then
						Me.ddRetirementGroundAnnuity.Items.Add("")
						Me.ddRetirementGroundAnnuity.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddRetirementGroundAnnuity.Items.Count > 0 Then
						Me.ddRetirementGroundAnnuity.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 15
					'populating rmas screen with the annuity withdrawal
					If Me.ddRetirementGroundAnnuity.Items.Count = 0 Then
						Me.ddRetirementGroundAnnuity.Items.Add("")
						Me.ddRetirementGroundAnnuity.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddRetirementGroundAnnuity.Items.Count > 0 Then
						Me.ddRetirementGroundAnnuity.Items.Add(lstReasons.Item(i))
					End If

				Case Else

			End Select


			i = i + 1

		Loop

	End Sub

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim cr As New Core, dt As DataTable


		'retrieving the details of the customer from enpower_midas db
		dt = cr.getPMPersonInformationLite(Me.txtPIN.Text)


		Session("PIN") = Me.txtPIN.Text

		If dt.Rows.Count > 0 Then

			ViewState("Employerid") = dt.Rows(0).Item("employerid")
			ViewState("EmployeeID") = dt.Rows(0).Item("employeeid")
			ViewState("EmployerCode") = dt.Rows(0).Item("EmployerCode")
			ViewState("Sector") = dt.Rows(0).Item("Sector")
			lblTitle.Text = dt.Rows(0).Item("Title")
			'Me.txtNOKNo.Text = dt.Rows(0).Item("NOKPhone")
			Me.txtSurname.Text = dt.Rows(0).Item("Surname")
			Me.txtFirstName.Text = dt.Rows(0).Item("FirstName")
			Me.txtOtherNames.Text = dt.Rows(0).Item("MiddleName")
			Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")
			Me.txtAge.Text = dt.Rows(0).Item("Age")

			Me.txtDelaredAge.Text = dt.Rows(0).Item("Age")

			Me.txtEmployer.Text = dt.Rows(0).Item("EmployerName").ToString
			'Me.txtNOK.Text = dt.Rows(0).Item("NOK").ToString
			Me.txtEmail.Text = dt.Rows(0).Item("email").ToString
			Me.txtSex.Text = dt.Rows(0).Item("sex").ToString

			Me.txtPhone.Text = dt.Rows(0).Item("Phone").ToString
			txtResidentialAddress.Text = dt.Rows(0).Item("FullAddress").ToString
			Me.txtSector.Text = dt.Rows(0).Item("Sector")
			'Me.txtOfficeAddress.Text = dt.Rows(0).Item("OfficeAddress")
		End If

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



	Protected Sub txtDisengagementDate_TextChanged(sender As Object, e As EventArgs) Handles txtDisengagementDate.TextChanged

		Dim date2 As Date = Date.Parse(txtDisengagementDate.Text)
		Dim date1 As Date = Now
		Dim months As Long = DateDiff(DateInterval.Month, date2, date1)
		If months < 4 Then
			spDateError.Visible = True
			Me.btnSubmit.Enabled = False
			MPRMASHardShip.Show()
		Else
			spDateError.Visible = False
			Me.btnSubmit.Enabled = True
		End If

	End Sub


	Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

		Dim aryDroppedDocs As New ArrayList
		Dim date2 As Date = Date.Parse(txtDOB.Text)
		Dim date1 As Date = Now
		Dim years As Long = DateDiff(DateInterval.Year, date2, date1)
		Dim cr As New Core, dt As New DataTable

		If IsNothing(Session("aryDroppedDocs")) = False Then
			aryDroppedDocs = Session("aryDroppedDocs")
		Else

		End If


		If years < 50 And Me.chkOverrideAgeBarrier.Checked = True And Me.txtOtherComments.Text = "" Then

			lblError.Text = "Please Enter Comment For Age Barrier Overriden"
			Me.pnlError.Visible = True

			Exit Sub
		Else
		End If

		'Session("user")
		If Not IsNothing(Session("user")) = True Then
			'retrieving the account detail of the customer
			dt = cr.getPMPersonInformationLitea(Me.txtPIN.Text)
		Else
			Response.Redirect("login.aspx")
		End If

		If Not IsNothing(ViewState("InsurerTypeCollection")) = True Then
			InsurerTypeCollection = ViewState("InsurerTypeCollection")
		Else
		End If


		If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		Else
		End If


		Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))
		Dim isAppExisting As Boolean = cr.PMIsApplicationAlreadyExist(Me.txtPIN.Text, typeID)

		If isAppExisting = True Then
			lblError.Text = "Application Already Exists"
			Me.pnlError.Visible = True
			Exit Sub
		End If

		If IsNothing(ViewState("RMAS")) = True And typeID = 2 Then

			Me.MPRMASHardShip.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 1 Then
			Me.MPRMASEnbloc.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 16 Then
			Me.MPRMASEnbloc.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 8 Then
			Me.MPRMASLegacy.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 3 Then
			Me.MPRMASPW.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 4 Then
			Me.MPRMASAnnuity.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 15 Then
			Me.MPRMASAnnuity.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 7 Then
			Me.mpAVC.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 5 Then
			Me.mpDeathBenefit.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 6 Then
			Me.MPNSITF.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 14 Then
			Me.MPRMASPW.Show()
			Exit Sub

		ElseIf typeID = 11 Then
			ViewState("RMAS") = True
		End If


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



		If typeID = 5 Then

			If IsNothing(ViewState("CheckListChecked")) = True Then '' And CBool(ViewState("CheckListChecked")) = False Then
				mpCheckListDBA.Show()
				Exit Sub
			Else
			End If

		Else

			If IsNothing(ViewState("CheckListChecked")) = True Then '' And CBool(ViewState("CheckListChecked")) = False Then
				Me.mpCheckList.Show()
				Exit Sub
			Else
			End If

		End If



		Dim appDetail As New ApplicationDetail, NextAppID As Integer, ApplicationCode As String, FileNumber As String, sector As String, myID As Integer
		Dim appDocDetails As New List(Of ApplicationDocumentDetail), appAdhocDocDetails As New List(Of AdhocDocuments), appCheckList As New ApplicationCheckList, appCheckListDBA As New ApplicationCheckListDOB

		NextAppID = cr.PMgetNextApplicationID()

		'generate sp log batch no
		'Dim SPLodID As String = cr.PMgetNextSPLogID(CInt(typeID), "A")

		If Page.IsValid And IsNothing(ViewState("EmployeeID")) = False Then

			'If Me.ddRequiredDocuments.Items.Count = 0 Then
			'	Exit Sub
			'Else
			'End If

			If Me.gridRecievedDocument.Rows.Count = 0 Then
				pnlError.Visible = True
				Me.lblError.Visible = True
				Me.lblError.Text = "No Required Document Populated"
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



			If typeID = 5 Then

				appCheckListDBA.ApplicationCode = ApplicationCode
				appCheckListDBA.LOAAffidavitChecked = 1
				appCheckListDBA.LOAEmployerIntroLetter = 1
				appCheckListDBA.LOAIntroLetter = 1
				appCheckListDBA.LOANumbersChecked = 1
				appCheckListDBA.LOASignatories = 1
				appCheckListDBA.MinorBirthCert = 1
				appCheckListDBA.MOIDocs = 1
				appCheckListDBA.NamesDOB = 1
				appCheckListDBA.NOKMOIs = 1
				appCheckListDBA.POA = 1
				appCheckListDBA.DODName = 1

			Else

				appCheckList.ApplicationCode = ApplicationCode
				appCheckList.DataEntryChecked = 1
				appCheckList.ExitDocChecked = 1
				appCheckList.FundingStatusChecked = 1
				appCheckList.LegAVCChecked = 1
				appCheckList.NamesChecked = 1
				appCheckList.ValidDocChecked = 1
				appCheckList.DOBChecked = 1

			End If




			If chkOverrideAgeBarrier.Checked = True Then
				appDetail.AgeConstrainstOverwitten = 1
			Else
				appDetail.AgeConstrainstOverwitten = 0
			End If



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
			If IsNothing(ViewState("RMAS")) = False And typeID = 4 Then

				Dim rDetails As New RetirementDetails
				rDetails.ApplicationCode = ApplicationCode
				rDetails.BasicSalary = Me.txtBasicSalaryAnnuity.Text
				rDetails.HouseRent = Me.txtHouseRentAnnuity.Text
				rDetails.Transport = Me.txtTransportAnnuity.Text
				rDetails.Utility = Me.txtUtilityAnnuity.Text
				rDetails.ConsolidatedAallowance = Me.txtConsolidatedAllowanceAnnuity.Text
				rDetails.ConsolidatedSalary = Me.txtConsolidatedSalaryAnnuity.Text
				rDetails.MonthlyTotal = Me.txtMonthTotalAnnuity.Text
				rDetails.AnnualTotalEmolumentAdj = Me.txtAnnualTotalEmolumentAnnuity.Text
				rDetails.PriceDate = Me.txtValueDateAnnuity.Text
				rDetails.AnnuityCommencement = Me.txtCommencmentDate.Text
				'rDetails.InsuranceCoy = Me.txtInsuranceCoy.Text
				rDetails.InsuranceCoy = CInt(InsurerTypeCollection.Item(Me.ddInsuranceCoy.SelectedItem.Text.ToString))
				'rDetails.RSABalance = Me.txtRSABalance.Text
				rDetails.RSABalance = dt.Rows(0).Item("RSABalance")

				rDetails.Premium = Me.txtPremium.Text
				rDetails.AnnuityLumpSum = Me.txtLumpSum.Text
				rDetails.MonthlyAnnuity = Me.txtMonthlyAnnuity.Text
				rDetails.isAnnuity = True
				appDetail.RetirementDetails = rDetails
				appDetail.ReferenceApplicationCode = Me.ddAnnRunningPW.Text
				'ReferenceApplicationCode
			End If


			If IsNothing(ViewState("RMAS")) = False And typeID = 15 Then

				Dim rDetails As New RetirementDetails
				rDetails.ApplicationCode = ApplicationCode
				rDetails.BasicSalary = Me.txtBasicSalaryAnnuity.Text
				rDetails.HouseRent = Me.txtHouseRentAnnuity.Text
				rDetails.Transport = Me.txtTransportAnnuity.Text
				rDetails.Utility = Me.txtUtilityAnnuity.Text
				rDetails.ConsolidatedAallowance = Me.txtConsolidatedAllowanceAnnuity.Text
				rDetails.ConsolidatedSalary = Me.txtConsolidatedSalaryAnnuity.Text
				rDetails.MonthlyTotal = Me.txtMonthTotalAnnuity.Text
				rDetails.AnnualTotalEmolumentAdj = Me.txtAnnualTotalEmolumentAnnuity.Text
				rDetails.PriceDate = Me.txtValueDateAnnuity.Text
				rDetails.AnnuityCommencement = Me.txtCommencmentDate.Text
				'rDetails.InsuranceCoy = Me.txtInsuranceCoy.Text
				rDetails.InsuranceCoy = CInt(InsurerTypeCollection.Item(Me.ddInsuranceCoy.SelectedItem.Text.ToString))
				'				rDetails.RSABalance = Me.txtRSABalance.Text
				rDetails.RSABalance = dt.Rows(0).Item("RSABalance")
				rDetails.Premium = Me.txtPremium.Text
				rDetails.AnnuityLumpSum = Me.txtLumpSum.Text
				rDetails.MonthlyAnnuity = Me.txtMonthlyAnnuity.Text
				rDetails.isAnnuity = True
				appDetail.RetirementDetails = rDetails

			End If


			If IsNothing(ViewState("RMAS")) = False And typeID = 3 Then

				Dim rDetails As New RetirementDetails
				rDetails.ApplicationCode = ApplicationCode
				rDetails.BasicSalary = Me.txtBasicSalaryPW.Text
				rDetails.HouseRent = Me.txtHouseRent.Text
				rDetails.Transport = Me.txtTransport.Text
				rDetails.Utility = Me.txtUtility.Text
				rDetails.ConsolidatedAallowance = Me.txtConsolidatedAllowance.Text
				rDetails.ConsolidatedSalary = Me.txtConsolidatedSalary.Text
				rDetails.MonthlyTotal = Me.txtMonthTotal.Text
				rDetails.AnnualTotalEmolumentAdj = Me.txtAnnualTotalEmolument.Text
				rDetails.PriceDate = Me.txtValueDate.Text
				rDetails.RSABalance = Me.txtRSABalancePW.Text
				rDetails.AccruedRight = Me.txtAccruedRightPW.Text
				rDetails.RecommendedLumpSum = Me.txtRecommendeLumpSum.Text
				rDetails.MonthlyProgramedDrawndown = Me.txtMonthlyDrawDown.Text
				rDetails.isProgramWithdrawal = True
				appDetail.RetirementDetails = rDetails

			End If



			If IsNothing(ViewState("RMAS")) = False And typeID = 14 Then

				Dim rDetails As New RetirementDetails
				rDetails.ApplicationCode = ApplicationCode
				rDetails.BasicSalary = Me.txtBasicSalaryPW.Text
				rDetails.HouseRent = Me.txtHouseRent.Text
				rDetails.Transport = Me.txtTransport.Text
				rDetails.Utility = Me.txtUtility.Text
				rDetails.ConsolidatedAallowance = Me.txtConsolidatedAllowance.Text
				rDetails.ConsolidatedSalary = Me.txtConsolidatedSalary.Text
				rDetails.MonthlyTotal = Me.txtMonthTotal.Text
				rDetails.AnnualTotalEmolumentAdj = Me.txtAnnualTotalEmolument.Text
				rDetails.PriceDate = Me.txtValueDate.Text
				rDetails.RSABalance = Me.txtRSABalancePW.Text
				rDetails.AccruedRight = Me.txtAccruedRightPW.Text
				rDetails.RecommendedLumpSum = Me.txtRecommendeLumpSum.Text
				rDetails.MonthlyProgramedDrawndown = Me.txtMonthlyDrawDown.Text
				rDetails.isProgramWithdrawal = True
				appDetail.RetirementDetails = rDetails

			End If


			If IsNothing(ViewState("RMAS")) = False And typeID = 5 Then

				Dim rDetails As New RetirementDetails
				rDetails.ApplicationCode = ApplicationCode
				rDetails.RetirementDate = CDate(Me.txtDBARetirementDate.Text)
				rDetails.DeathDate = CDate(Me.txtDBADeathDate.Text)
				rDetails.AdminIssuingAuthority = Me.txtAdminLetterIssuer.Text
				rDetails.AdminIssuingDate = CDate(Me.txtAdminLetterDate.Text)
				rDetails.AdminNOK = Me.txtDBAAdminNOK.Text
				rDetails.InsuranceProceed = Me.txtInsuranceProceed.Text
				rDetails.AccruedRight = Me.txtDBAAccruedRight.Text
				rDetails.Contribution = CDbl(Me.txtDBAContribution.Text)
				rDetails.InvestmentIncome = Me.txtDBAInvestmentIncome.Text
				rDetails.RSABalance = Me.txtDBARSABalance.Text
				rDetails.PriceDate = CDate(Me.txtDBValueDate.Text)
				rDetails.IsDeathBenefit = True
				appDetail.RetirementDetails = rDetails

			End If


			appDetail.TIN = Me.txtTIN.Text
			appDetail.FileNumber = FileNumber
			appDetail.Title = Me.lblTitle.Text
			appDetail.ApplicationID = ApplicationCode
			appDetail.Sector = sector



			'Mandatory
			If IsNothing(ViewState("RMAS")) = False And typeID = 2 Then

				appDetail.DateDisengagement = DateTime.Parse(Me.txtDisengagementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = False
				'appDetail.RSABalance = CDbl(Me.txtMandatory.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("Mandatory"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 1 Then

				appDetail.DOR = DateTime.Parse(Me.txtRetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 16 Then

				appDetail.DOR = DateTime.Parse(Me.txtRetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 3 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORPW.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))


			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 14 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORPW.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 4 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORAnnuity.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 15 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORAnnuity.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 8 Then

				appDetail.DOR = DateTime.Parse(Me.txtLegacyRetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 7 Then

				appDetail.IsRetirement = False
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 5 Then

				appDetail.DOR = DateTime.Parse(Me.txtDBARetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 11 Then


				appDetail.IsRetirement = False
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			Else
			End If


			If IsNothing(ViewState("RMAS")) = False And typeID = 6 Then

				appDetail.DOR = DateTime.Parse(Me.txtRetirementDateNSTITF.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.NSITFInitialAmountPaid = Me.txtInitialAmountPaid.Text
				appDetail.NSITFRecievedToRSA = Me.txtAmountRecievedToRSANSITF.Text
				appDetail.NSITFRequestedToRSA = Me.txtAmountRequestedFromRSANSITF.Text
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			Else

				appDetail.NSITFInitialAmountPaid = "0.00"
				appDetail.NSITFRecievedToRSA = "0.00"
				appDetail.NSITFRequestedToRSA = "0.00"
			End If


			'If IsNothing(ViewState("EmployerHistoryCollection")) = True And IsNothing(ViewState("Employerid")) = False Then

			appDetail.EmployerID = CInt(ViewState("Employerid"))
			appDetail.EmployerName = Me.txtEmployer.Text
			appDetail.EmployerCode = ViewState("EmployerCode").ToString

			'ElseIf IsNothing(ViewState("EmployerHistoryCollection")) = False Then
			'	EmployerHistoryCollection = ViewState("EmployerHistoryCollection")
			'	appDetail.EmployerID = CInt(EmployerHistoryCollection.Item(Me.txtEmployer.Text))
			'	appDetail.EmployerName = Me.txtEmployer.Text
			'End If

			appDetail.CreatedBy = CStr(Session("user"))
			appDetail.PIN = Me.txtPIN.Text
			appDetail.Email = LTrim(RTrim(Me.txtEmail.Text))
			appDetail.FullName = Me.txtSurname.Text & " | " & Me.txtFirstName.Text & " | " & Me.txtOtherNames.Text

			If Me.txtDOB.Text = "" Then
				appDetail.DOB = Now.Date
			Else
				appDetail.DOB = CDate(Me.txtDOB.Text)
			End If

			appDetail.FundStatus = Me.ddFundingStatus.SelectedItem.Text.ToString
			'enbloc payment
			If typeID = 1 Then
				appDetail.Reason = Me.ddPaymentReasons.SelectedItem.Text.ToString
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf typeID = 16 Then

				appDetail.Reason = Me.ddPaymentReasons.SelectedItem.Text.ToString
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))
				'25% hardShip Payment
			ElseIf typeID = 2 Then
				appDetail.Reason = Me.ddExitReasons.SelectedItem.Text.ToString
				appDetail.Designation = Me.txtDesignation.Text
				appDetail.Department = Me.txtPartDepartment.Text
				'appDetail.RSABalance = CDbl(Me.txtMandatory.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("Mandatory"))
				'lump sum payment
			ElseIf typeID = 3 Then

				appDetail.Reason = Me.ddRetirementGroundPW.SelectedItem.Text.ToString
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))
				'additional lump payment
			ElseIf typeID = 14 Then

				appDetail.Reason = Me.ddRetirementGroundPW.SelectedItem.Text.ToString
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))
				'Annuity Payment
			ElseIf typeID = 4 Then

				appDetail.Reason = Me.ddRetirementGroundAnnuity.SelectedItem.Text.ToString
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))

			ElseIf typeID = 15 Then

				appDetail.Reason = Me.ddRetirementGroundAnnuity.SelectedItem.Text.ToString
				'appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				appDetail.RSABalance = CDbl(dt.Rows(0).Item("RSABalance"))
			End If

			appDetail.Comment = Me.txtOtherComments.Text
			appDetail.CommentGroup = ""
			appDetail.MemberID = CInt(ViewState("EmployeeID"))
			appDetail.ApplicationDate = CDate(Me.txtApplicationDate.Text)
			appDetail.ApplicationState = ddApplicationState.SelectedItem.Text.ToString
			appDetail.Sex = Me.txtSex.Text
			appDetail.AccountName = Me.txtAccountName.Text
			appDetail.AccountNo = Me.txtAccountNumber.Text
			appDetail.BVN = Me.txtBVN.Text
			appDetail.Comment = Me.txtOtherComments.Text


			If Not IsNothing(ViewState("BankTypeCollection")) = True Then
				BankTypeCollection = ViewState("BankTypeCollection")
				appDetail.BankID = CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text.ToString))
			Else
			End If

			If Me.ddBankBranch.SelectedItem.Text.ToString <> "" Then
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
				appDetail.AppTypeId = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))
			Else

			End If


			appDetail.ApplicationTypeName = Me.ddApplicationType.SelectedItem.Text.ToString

			Dim docCount As Integer, isAllDocumentScanned As Boolean = True

			Dim dtRecievedDocument As New DataTable
			If Not IsNothing(ViewState("DocumentCollection")) = True Then
				DocumentCollection = Session("DocumentCollection")


				If Not IsNothing(Session("dtRecievedDocument")) = True Then

					dtRecievedDocument = Session("dtRecievedDocument")
					'creating data object for the uploaded documents
					Do While docCount < dtRecievedDocument.Rows.Count
						Dim appDocDetail As New ApplicationDocumentDetail


						If CInt((dtRecievedDocument.Rows(docCount).Item(0))) = 0 Then

						Else

							'appDocDetail.DocumentTypeID = CInt(DocumentCollection.Item(row.Cells(1).Text))
							appDocDetail.DocumentTypeID = CInt(dtRecievedDocument.Rows(docCount).Item(0))
							appDocDetail.DateReceived = CDate(Me.txtRecievedDate.Text)


							appDocDetail.DocumentLocation = CStr(dtRecievedDocument.Rows(docCount).Item(1)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

							'appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

							appDocDetail.IsVerified = 1
							appDocDetails.Add(appDocDetail)

						End If
						docCount = docCount + 1
					Loop


				Else

				End If
				
			Else
			End If

			appDetail.CommentGroup = Me.ddCommentGroup.Text

			'''''''''''''''''''''
			'''remove this ''''''

			'  Exit Sub

			'checking if the total uplaoded document is same as the total required document
			Dim tDocs As Integer

			If IsNothing(Session("TotalFileUploaded")) = False Then

				tDocs = CInt(Session("TotalFileUploaded"))

			Else

				tDocs = 0

			End If


			If ((dtRecievedDocument.Rows.Count) = Me.gridRecievedDocument.Rows.Count) = True And Me.gridRecievedDocument.Rows.Count > 0 Then 'And Me.ddStatus.SelectedItem.Text = "" Then ''And isAllDocumentScanned = True Then

				appDetail.Status = "Documentation"
				appDetail.DateDocumentCompleted = DateTime.Parse(Now)
				appDetail.DocCompleted = 1

				'ElseIf ((tDocs) = Me.gridRecievedDocument.Rows.Count) = True And Me.ddStatus.SelectedItem.Text = "" And isAllDocumentScanned = False Then
				'	appDetail.Status = "Entry"
				'	appDetail.DocCompleted = 0
				'	appDetail.DateDocumentCompleted = Nothing

			ElseIf ((dtRecievedDocument.Rows.Count) = Me.gridRecievedDocument.Rows.Count) = False Then 'And Me.ddStatus.SelectedItem.Text = "" Then

				appDetail.Status = "Entry"
				appDetail.DocCompleted = 0
				appDetail.DateDocumentCompleted = Nothing

			Else
				'appDetail.Status = ddStatus.SelectedItem.Text
				'Dim selStat As String = Me.ddStatus.SelectedItem.Text

				'Select Case selStat
				'	Case Is = "Documentation"
				'		appDetail.DateDocumentCompleted = Nothing
				'		appDetail.DocCompleted = 1
				'	Case Else
				'		appDetail.DocCompleted = 0
				'End Select

			End If

			'relaxing all required documents check for death benefit application only
			If appDetail.AppTypeId = 5 And isAllDocumentScanned = True Then
				appDetail.Status = "Documentation"
				appDetail.DateDocumentCompleted = DateTime.Parse(Now)
				appDetail.DocCompleted = 1
			Else
			End If




			Dim boolSubmitStatus As Boolean
			Try
				If Not IsNothing(Session("user")) = True Then

					'boolSubmitStatus = cr.PMSubmitApplication(appDetail, appDocDetails, appAdhocDocDetails, Session("user"), Server.MapPath("~/Logs"))

					boolSubmitStatus = cr.PMSubmitApplication(appDetail, appDocDetails, appAdhocDocDetails, Session("user"), Server.MapPath("~/Logs"), appCheckList, appCheckListDBA)


					If boolSubmitStatus = True Then
						'deleting possible duplicate document created

						cr.PMCleanDuplicateDocument(appDetail.ApplicationID)

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
				logerr.Logger(ex.StackTrace & "Application Logging")


			Finally



			End Try

		End If

	End Sub


	Protected Sub CalValueDateAnnuity_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalValueDateAnnuity.VisibleMonthChanged
		Me.MPRMASAnnuity.Show()
	End Sub

	Protected Sub calDORPW_SelectionChanged(sender As Object, e As EventArgs) Handles calDORPW.SelectionChanged
		Me.calDORPW_PopupControlExtender.Commit(Me.calDORPW.SelectedDate)
		Me.MPRMASPW.Show()
	End Sub

	Protected Sub calDORPW_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calDORPW.VisibleMonthChanged
		Me.MPRMASPW.Show()
	End Sub

	Protected Sub calDORAnnuity_SelectionChanged(sender As Object, e As EventArgs) Handles calDORAnnuity.SelectionChanged
		Me.calDORAnnuity_PopupControlExtender.Commit(Me.calDORAnnuity.SelectedDate)
		Me.MPRMASAnnuity.Show()
	End Sub

	Protected Sub calDORAnnuity_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calDORAnnuity.VisibleMonthChanged
		Me.MPRMASAnnuity.Show()
	End Sub

	Protected Sub CalValueDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalValueDate.SelectionChanged

		Me.CalValueDate_PopupControlExtender.Commit(Me.CalValueDate.SelectedDate)
		Me.MPRMASPW.Show()

	End Sub

	Protected Sub CalValueDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalValueDate.VisibleMonthChanged

		Me.MPRMASPW.Show()

	End Sub

	Protected Sub CalCommencmentDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalCommencmentDate.SelectionChanged

		Me.CalCommencmentDate_PopupControlExtender.Commit(Me.CalCommencmentDate.SelectedDate)
		Me.MPRMASAnnuity.Show()
	End Sub

	Protected Sub CalValueDateAnnuity_SelectionChanged(sender As Object, e As EventArgs) Handles CalValueDateAnnuity.SelectionChanged

		Me.CalValueDateAnnuity_PopupControlExtender.Commit(Me.CalValueDateAnnuity.SelectedDate)
		Me.MPRMASAnnuity.Show()

	End Sub

	Protected Sub calLegacyRetirementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calLegacyRetirementDate.SelectionChanged

		Me.calLegacyRetirementDate_PopupControlExtenderLegacy.Commit(Me.calLegacyRetirementDate.SelectedDate)
		Me.MPRMASLegacy.Show()

	End Sub

	Protected Sub calRetirementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRetirementDate.SelectionChanged

		Me.calRetirementDate_PopupControlExtender.Commit(Me.calRetirementDate.SelectedDate)
		Me.MPRMASEnbloc.Show()

	End Sub

	Protected Sub calDisengagementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calDisengagementDate.SelectionChanged

		Me.calDisengagementDate_PopupControlExtender.Commit(Me.calDisengagementDate.SelectedDate)
		Me.MPRMASHardShip.Show()

	End Sub

	Protected Sub btnNSITFOk_Click(sender As Object, e As EventArgs) Handles btnNSITFOk.Click

		blnNSITF = True
		ViewState("RMAS") = blnNSITF

	End Sub

	Protected Sub btnDBOk_Click(sender As Object, e As EventArgs) Handles btnDBOk.Click

		blnDB = True
		ViewState("RMAS") = blnDB

	End Sub

	Protected Sub btnOKPW_Click(sender As Object, e As EventArgs) Handles btnOKPW.Click
		pnlError.Visible = False
		If (CDbl(txtRecommendeLumpSum.Text) * 2) > CDbl(txtRSABalancePW.Text) Then

			lblError.Text = "Lumpsum Should be Less Than 50% of RSA balance "
			Me.pnlError.Visible = True

		Else
			blnPW = True
			ViewState("RMAS") = blnPW
		End If

	End Sub

	Protected Sub btnOKAnnuity_Click(sender As Object, e As EventArgs) Handles btnOKAnnuity.Click

		blnAnnuity = True
		ViewState("RMAS") = blnAnnuity

	End Sub

	Protected Sub btnLegacyOK_Click(sender As Object, e As EventArgs) Handles btnLegacyOK.Click

		blnLegacy = True
		ViewState("RMAS") = blnLegacy

	End Sub

	Protected Sub btnEnblocOK_Click(sender As Object, e As EventArgs) Handles btnEnblocOK.Click


		blnEnbloc = True
		ViewState("RMAS") = blnEnbloc

	End Sub





	Protected Sub btnHardShipOK_Click(sender As Object, e As EventArgs) Handles btnHardShipOK.Click


		blnHardShip = True
		ViewState("RMAS") = blnHardShip

	End Sub

	Protected Sub btnAVCOk_Click(sender As Object, e As EventArgs) Handles btnAVCOk.Click

		blnAVC = True
		ViewState("RMAS") = blnAVC

	End Sub

	Protected Sub ddApplicationState_TextChanged(sender As Object, e As EventArgs) Handles ddApplicationState.TextChanged

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

	Protected Sub btnShowPicture_Click(sender As Object, e As EventArgs) Handles btnShowPicture.Click

		imgPassport.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", txtPIN.Text, 1, Server.MapPath("~/Logs"))

	End Sub

	Protected Sub btnShowSignature_Click(sender As Object, e As EventArgs) Handles btnShowSignature.Click

		imgSignature.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", txtPIN.Text, 2, Server.MapPath("~/Logs"))

	End Sub

	Protected Sub ddInsuranceCoy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddInsuranceCoy.SelectedIndexChanged

		Me.MPRMASAnnuity.Show()

	End Sub


	Protected Sub btnReValueAnnuity_Click(sender As Object, e As EventArgs) Handles btnReValueAnnuity.Click

		Dim cr As New Core

		Try

			Me.txtRSABalanceAnnuity.Text = cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), CDate(Me.txtValueDateAnnuity.Text), 1)
			Me.MPRMASAnnuity.Show()

		Catch ex As Exception

		End Try


	End Sub

	Protected Sub btnReValuePW_Click(sender As Object, e As EventArgs) Handles btnReValuePW.Click
		Dim cr As New Core
		Try

			Me.txtRSABalancePW.Text = cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), CDate(Me.txtValueDate.Text), 1)
			Me.MPRMASPW.Show()

		Catch ex As Exception

		End Try
	End Sub


	Protected Sub btnViewFundingDetails_Click(sender As Object, e As EventArgs) Handles btnViewFundingDetails.Click

		Dim cr As New Core, dt As New DataTable

		Try

			dt = cr.getPMPersonInformationLitea(Me.txtPIN.Text)

			If dt.Rows.Count > 0 Then

				txtRSABalance.Text = dt.Rows(0).Item("RSABalance")
				txtMandatory.Text = dt.Rows(0).Item("Mandatory")
				txtAVC.Text = dt.Rows(0).Item("AVC")
				txtLegacy.Text = dt.Rows(0).Item("Legacy")
				txtRFBalance.Text = dt.Rows(0).Item("RFBalance")
				txtAccruedRight.Text = dt.Rows(0).Item("AccruedRight")

			Else
			End If

		Catch ex As Exception

		End Try
		
		


	End Sub


	Protected Sub btnCheckAll_Click(sender As Object, e As EventArgs) Handles btnCheckAll.Click

		Me.chkDataEntry.Checked = True
		Me.chkLegAVC.Checked = True
		Me.chkNames.Checked = True
		Me.chkValidDoc.Checked = True
		Me.chkExitDoc.Checked = True
		chkDOB.Checked = True
		chkFundingStatus.Checked = True

		Me.mpCheckList.Show()

	End Sub

	Protected Sub btnCheckOkay_Click(sender As Object, e As EventArgs) Handles btnCheckOkay.Click

		If chkDataEntry.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		If chkLegAVC.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		If chkNames.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		If chkValidDoc.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		If chkExitDoc.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		If chkDOB.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		If chkFundingStatus.Checked = False Then
			Me.mpCheckList.Show()
		Else
		End If

		ViewState("CheckListChecked") = True


	End Sub

	Protected Sub btnCheckOkayDBA_Click(sender As Object, e As EventArgs) Handles btnCheckOkayDBA.Click

		If chkLOAAffidavit.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkLOANumbers.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkLOAIntroLetter.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkLOAEmployerIntroLetter.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkLOASignatories.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkPOA.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkMinorBirthCert.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkNOKMOIs.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkMOIDocs.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkNamesDOB.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		If chkDODName.Checked = False Then
			Me.mpCheckListDBA.Show()
		Else
		End If

		ViewState("CheckListChecked") = True


	End Sub

	Protected Sub btnCheckAllDBA_Click(sender As Object, e As EventArgs) Handles btnCheckAllDBA.Click

		Me.chkLOAAffidavit.Checked = True
		Me.chkLOAEmployerIntroLetter.Checked = True
		Me.chkLOAIntroLetter.Checked = True
		Me.chkLOANumbers.Checked = True
		Me.chkLOASignatories.Checked = True
		Me.chkMinorBirthCert.Checked = True
		Me.chkMOIDocs.Checked = True
		'Me.chk.Checked = True
		Me.chkNamesDOB.Checked = True
		Me.chkNOKMOIs.Checked = True
		Me.chkPOA.Checked = True
		Me.chkDODName.Checked = True

		Me.mpCheckListDBA.Show()

	End Sub


End Class
