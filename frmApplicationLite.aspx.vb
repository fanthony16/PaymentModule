Imports AjaxControlToolkit
Imports System.Data
Imports System.IO

Partial Class frmApplicationLite
	Inherits System.Web.UI.Page
	Dim DocumentCollection As New Hashtable
	Dim ApprovalTypeCollection As New Hashtable
	Dim InsurerTypeCollection As New Hashtable
	Dim BankTypeCollection As New Hashtable
	Dim BankBranchCollection As New Hashtable

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

		Dim aryDocIDList As New ArrayList
		Dim aryDocNameList As New ArrayList
		Dim aryDroppedDocs As New ArrayList

		Do While docCount < Me.gridRecievedDocument.Rows.Count

			Dim row As GridViewRow = gridRecievedDocument.Rows(docCount)
			aryDocIDList.Add(row.Cells(0).Text)
			aryDocNameList.Add(row.Cells(1).Text)
			docCount = docCount + 1

		Loop

		aryDocIDList.TrimToSize()
		aryDocNameList.TrimToSize()

		docCount = 0

		Do While docCount < uploadedFiles.Count

			Dim userPostedFile As HttpPostedFile = uploadedFiles(docCount)

			fileName = userPostedFile.FileName
			'MsgBox("" & fileName.Split(".")(0))
			If aryDocIDList.Contains(fileName.Split(".")(0)) Then

				Dim fileNewName, strUploadPath As String
				fileNewName = aryDocNameList(aryDocIDList.IndexOf(fileName.Split(".")(0)))
				fileNewName = fileNewName.Replace(" | ", "_")
				fileNewName = fileNewName.Replace(" ", "_")

				strUploadPath = "~/FileUploads/" & Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1)
				strUploadPath = Server.MapPath("FileUploads/" & "o-taiwo" & "/" & Me.txtPIN.Text.Trim & "_" & fileNewName & "." & fileName.Split(".")(1))
				userPostedFile.SaveAs(strUploadPath)

				'keeing the list of dropped application docs
				aryDroppedDocs.Add(fileName.Split(".")(0))
				uploadDocCount = uploadDocCount + 1

			Else

			End If

			docCount = docCount + 1

		Loop
		Session("aryDroppedDocs") = aryDroppedDocs
		Session("TotalFileUploaded") = uploadDocCount

		'Dim filename As String = System.IO.Path.GetFileName(e.FileName)
		'Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)

		'Dim fileNewName As String


		'If Me.txtAdhocDocDescription.Text <> "" Then

		'	Session("Document") = Me.txtAdhocDocDescription.Text
		'	fileNewName = Session("Document").ToString

		'Else
		'	fileNewName = Session("Document").ToString

		'End If


		'fileNewName = fileNewName.Replace(" | ", "_")
		'fileNewName = fileNewName.Replace(" ", "_")
		'Dim strUploadPath As String
		'If (Session("Document").ToString) = "Others" Then
		'	fileNewName = filename
		'	fileNewName = fileNewName.Replace("|", "")
		'	fileNewName = fileNewName.Replace(" ", "_")
		'	strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName
		'Else

		'	strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName & System.IO.Path.GetExtension(fullPath) '& "_" & filename
		'	'keeping the temporary the uploaded document prior final saving for easy retrieval should there is network cut-off

		'End If


		'Session("documentPath") = strUploadPath




		''creating data object for the recieved documents
		'Do While docCount < Me.gridRecievedDocument.Rows.Count
		'	Dim appDocDetail As New ApplicationDocumentDetail

		'	Dim row As GridViewRow = gridRecievedDocument.Rows(docCount)

		'	If CInt(DocumentCollection.Item(row.Cells(1).Text)) = 0 Then

		'		Dim appAdhocDocDetail As New AdhocDocuments

		'		If CStr((row.Cells(3).Text)) = "&nbsp;" Then
		'			isAllDocumentScanned = False
		'		Else
		'			isAllDocumentScanned = True
		'		End If

		'		appAdhocDocDetail.ApplicationCode = ApplicationCode
		'		appAdhocDocDetail.Description = row.Cells(1).Text
		'		appAdhocDocDetail.PIN = LTrim(RTrim(Me.txtPIN.Text))
		'		appAdhocDocDetail.RecievedBy = Session("user")
		'		appAdhocDocDetail.RecievedDate = Now

		'		appAdhocDocDetail.DocPath = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

		'		'appAdhocDocDetail.DocPath = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

		'		appAdhocDocDetail.IsVerified = CInt((row.Cells(5).Text))
		'		appAdhocDocDetails.Add(appAdhocDocDetail)

		'	Else

		'		appDocDetail.DocumentTypeID = CInt(DocumentCollection.Item(row.Cells(1).Text))
		'		appDocDetail.DateReceived = CDate((row.Cells(2).Text))
		'		'checking if all the documents had been scanned 

		'		If CStr((row.Cells(3).Text)) = "&nbsp;" Then
		'			isAllDocumentScanned = False
		'		Else
		'			isAllDocumentScanned = True
		'		End If
		'		appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

		'		'appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

		'		appDocDetail.IsVerified = CInt((row.Cells(5).Text))
		'		appDocDetails.Add(appDocDetail)

		'	End If
		'	docCount = docCount + 1
		'Loop





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

							Me.gridRecievedDocument.DataSource = Nothing

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
		'ViewState("EmployerHistoryCollection") = Nothing
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

	Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

		Dim aryDroppedDocs As New ArrayList
		Dim date2 As Date = Date.Parse(txtDOB.Text)
		Dim date1 As Date = Now
		Dim years As Long = DateDiff(DateInterval.Year, date2, date1)
		Dim cr As New Core

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
			'InsurerTypeCollection = ViewState("InsurerTypeCollection")
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

		Dim appDetail As New ApplicationDetail, NextAppID As Integer, ApplicationCode As String, FileNumber As String, sector As String, myID As Integer
		Dim appDocDetails As New List(Of ApplicationDocumentDetail), appAdhocDocDetails As New List(Of AdhocDocuments)

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
				rDetails.RSABalance = Me.txtRSABalance.Text
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
				rDetails.RSABalance = Me.txtRSABalance.Text
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




			If IsNothing(ViewState("RMAS")) = False And typeID = 2 Then

				appDetail.DateDisengagement = DateTime.Parse(Me.txtDisengagementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = False
				appDetail.RSABalance = CDbl(Me.txtMandatory.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 1 Then

				appDetail.DOR = DateTime.Parse(Me.txtRetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 16 Then

				appDetail.DOR = DateTime.Parse(Me.txtRetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 3 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORPW.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)


			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 14 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORPW.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 4 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORAnnuity.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 15 Then

				appDetail.DOR = DateTime.Parse(Me.txtDORAnnuity.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 8 Then

				appDetail.DOR = DateTime.Parse(Me.txtLegacyRetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 7 Then

				appDetail.IsRetirement = False
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 5 Then

				appDetail.DOR = DateTime.Parse(Me.txtDBARetirementDate.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 11 Then


				appDetail.IsRetirement = False
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
			Else
			End If


			If IsNothing(ViewState("RMAS")) = False And typeID = 6 Then

				appDetail.DOR = DateTime.Parse(Me.txtRetirementDateNSTITF.Text).ToString("yyyy-MM-dd")
				appDetail.IsRetirement = True
				appDetail.NSITFInitialAmountPaid = Me.txtInitialAmountPaid.Text
				appDetail.NSITFRecievedToRSA = Me.txtAmountRecievedToRSANSITF.Text
				appDetail.NSITFRequestedToRSA = Me.txtAmountRequestedFromRSANSITF.Text
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

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
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf typeID = 16 Then

				appDetail.Reason = Me.ddPaymentReasons.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				'25% hardShip Payment
			ElseIf typeID = 2 Then
				appDetail.Reason = Me.ddExitReasons.SelectedItem.Text.ToString
				appDetail.Designation = Me.txtDesignation.Text
				appDetail.Department = Me.txtPartDepartment.Text
				appDetail.RSABalance = CDbl(Me.txtMandatory.Text)
				'lump sum payment
			ElseIf typeID = 3 Then

				appDetail.Reason = Me.ddRetirementGroundPW.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				'additional lump payment
			ElseIf typeID = 14 Then

				appDetail.Reason = Me.ddRetirementGroundPW.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
				'Annuity Payment
			ElseIf typeID = 4 Then

				appDetail.Reason = Me.ddRetirementGroundAnnuity.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf typeID = 15 Then

				appDetail.Reason = Me.ddRetirementGroundAnnuity.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			End If

			appDetail.Comment = Me.txtOtherComments.Text
			appDetail.CommentGroup = Me.ddCommentGroup.SelectedItem.Text.ToString
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
			' Dim row As gridRecievedDocument.
			If Not IsNothing(ViewState("DocumentCollection")) = True Then
				DocumentCollection = ViewState("DocumentCollection")


				'creating data object for the recieved documents
				Do While docCount < Me.gridRecievedDocument.Rows.Count
					Dim appDocDetail As New ApplicationDocumentDetail

					Dim row As GridViewRow = gridRecievedDocument.Rows(docCount)

					If CInt(DocumentCollection.Item(row.Cells(0).Text)) = 0 Then

					Else

						'appDocDetail.DocumentTypeID = CInt(DocumentCollection.Item(row.Cells(1).Text))
						appDocDetail.DocumentTypeID = CInt((row.Cells(0).Text))
						appDocDetail.DateReceived = CDate(Me.txtRecievedDate.Text)

						'checking if all the documents had been scanned 
						'						If CStr((row.Cells(3).Text)) = "&nbsp;" Then
						If aryDroppedDocs.Contains(CStr((row.Cells(0).Text))) Then
							isAllDocumentScanned = True
						Else
							isAllDocumentScanned = False
						End If
						appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

						'appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

						appDocDetail.IsVerified = CInt((row.Cells(5).Text))
						appDocDetails.Add(appDocDetail)

					End If
					docCount = docCount + 1
				Loop
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


			If ((aryDroppedDocs.Count) = Me.gridRecievedDocument.Rows.Count) = True And Me.ddStatus.SelectedItem.Text = "" Then ''And isAllDocumentScanned = True Then
				appDetail.Status = "Documentation"
				appDetail.DateDocumentCompleted = DateTime.Parse(Now)
				appDetail.DocCompleted = 1
				'ElseIf ((tDocs) = Me.gridRecievedDocument.Rows.Count) = True And Me.ddStatus.SelectedItem.Text = "" And isAllDocumentScanned = False Then
				'	appDetail.Status = "Entry"
				'	appDetail.DocCompleted = 0
				'	appDetail.DateDocumentCompleted = Nothing

			ElseIf ((aryDroppedDocs.Count) = Me.gridRecievedDocument.Rows.Count) = False And Me.ddStatus.SelectedItem.Text = "" Then
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

		End If


	End Sub
End Class
