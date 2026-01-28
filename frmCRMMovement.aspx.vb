Imports System.IO
Imports System.Data

Partial Class frmCRMMovement
	Inherits System.Web.UI.Page
	Dim StateCollection As New Hashtable
	Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
		Dim _tempByte() As Byte = Nothing
		If String.IsNullOrEmpty(ImageFilePath) = True Then
			Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
			Return Nothing
		End If
		Try
			Dim _fileInfo As New IO.FileInfo(ImageFilePath)
			Dim _NumBytes As Long = _fileInfo.Length
			Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
			Dim _BinaryReader As New IO.BinaryReader(_FStream)
			_tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
			_fileInfo = Nothing
			_NumBytes = 0
			_FStream.Close()
			_FStream.Dispose()
			_BinaryReader.Close()

			If File.Exists(ImageFilePath) Then
				'File.Delete(ImageFilePath)
			Else
			End If

			Return _tempByte

			'Dim final As String
			'final = Convert.ToBase64String(_tempByte)
			'Return final

		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	Private Function MoveToCRM(appDetails As ApplicationDetail, appDocDetail As List(Of ApplicationDocumentDetail)) As String

		Try

			Dim crm As New CRM.ApplicationDocumentDetail
			Dim crmm As New CRM.BankCls


			Dim _CRMRequest As New CRM.LPPFARequest
			Dim _CRMResponse As New CRM.BenefitApplicationResponse
			Dim _AppDetail As New CRM.NPMOnlineApplicationDetail

			'	Dim _AppDocDetail As New CRM.ApplicationDocumentDetail

			Dim appDocs(appDocDetail.Count) As CRM.ApplicationDocumentDetail


			Dim i As Integer

			Do While i < appDocDetail.Count

				Dim appDoc As New CRM.ApplicationDocumentDetail
				appDoc.dateReceived = appDocDetail(i).DateReceived
				appDoc.documentLocation = appDocDetail(i).DocumentLocation
				appDoc.documentTypeID = appDocDetail(i).DocumentTypeID
				appDoc.documentTypeName = appDocDetail(i).DocumentTypeName
				appDoc.isVerified = 1
				appDoc.ext = appDocDetail(i).DMSDocumentExt


				'appDoc.documentHashValue = ConvertImageFiletoBytes(appDocDetail(i).DocumentLocation.ToString.Split("|")(1) + "CRMImages" + "\" + appDocDetail(i).DocumentLocation.ToString.Split("|")(0))

				appDoc.documentHashValue = ConvertImageFiletoBytes(appDocDetail(i).DocumentLocation)

				appDocs(i) = appDoc
				i = i + 1

			Loop


			_AppDetail.accountName = appDetails.AccountName
			_AppDetail.accountNumber = appDetails.AccountNo
			_AppDetail.applicationDocuments = appDocs
			_AppDetail.applicationTypeName = appDetails.ApplicationTypeName
			_AppDetail.appTypeId = appDetails.AppTypeId
			_AppDetail.aVCApplicationAmount = appDetails.AmountToPay
			_AppDetail.createdBy = appDetails.CreatedBy
			_AppDetail.customerBankBranchID = appDetails.BranchID
			_AppDetail.customerBankID = appDetails.BankID

			_AppDetail.pIN = appDetails.PIN


			'enbloc payment
			If appDetails.AppTypeId = 1 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.dateRetirement = appDetails.DOR

			ElseIf appDetails.AppTypeId = 16 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.dateRetirement = appDetails.DOR

			ElseIf appDetails.AppTypeId = 2 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.designation = appDetails.Designation
				_AppDetail.department = appDetails.Department
				_AppDetail.dateDisengagement = appDetails.DateDisengagement

				'lump sum payment
			ElseIf appDetails.AppTypeId = 3 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.dateRetirement = appDetails.DOR

				'additional lump payment
			ElseIf appDetails.AppTypeId = 14 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.dateRetirement = appDetails.DOR

			ElseIf appDetails.AppTypeId = 4 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.dateRetirement = appDetails.DOR

			ElseIf appDetails.AppTypeId = 15 Then

				_AppDetail.reason = appDetails.Reason
				_AppDetail.dateRetirement = appDetails.DOR

			ElseIf appDetails.AppTypeId = 8 Then

				_AppDetail.dateRetirement = appDetails.DOR

			ElseIf appDetails.AppTypeId = 5 Then

				_AppDetail.dateRetirement = appDetails.DOR

			End If


			_AppDetail.sourceId = 1

			'appDetails.ApplicationState
			StateCollection = ViewState("StateCollection")
			'_AppDetail.stateId = appDetails.ApplicationState
			'MsgBox("" & CInt(StateCollection.Item(appDetails.ApplicationState)).ToString)

			'Return ""
			MsgBox("" & CInt(StateCollection.Item(appDetails.ApplicationState)).ToString)




			_AppDetail.stateId = CInt(StateCollection.Item(appDetails.ApplicationState)).ToString

			_AppDetail.tIN = appDetails.PIN

			_CRMResponse = _CRMRequest.DropBenefitApplication(_AppDetail)

			'Dim ___CRMResponse As String
			'___CRMResponse = _CRMRequest.DropBenefitApplicationSurepay(_AppDetail)

			'Return ___CRMResponse

			If _CRMResponse.Status Then
				Return _CRMResponse.benefitapplicationid
			Else
				Return _CRMResponse.Message
			End If


		Catch ex As Exception

			Return "Error on SurePay"

		End Try

	End Function
	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



		Dim cr As New Core, dt As New DataTable, dtocs As New DataTable, i As Integer

		dt = cr.PMgetApplicationForCRM

		Do While i < dt.Rows.Count
			Dim appDetail As New ApplicationDetail
			Dim appDocDetails As New List(Of ApplicationDocumentDetail)

			If dt.Rows(i).Item("fkiAppTypeId") = 1 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 16 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 2 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.Designation = dt.Rows(i).Item("txtDesignation")
				appDetail.Department = dt.Rows(i).Item("txtDepartment")
				appDetail.DateDisengagement = dt.Rows(i).Item("dteDisengagement")

				'lump sum payment
			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 3 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.DOR = dt.Rows(i).Item("dteDOR")

				'additional lump payment
			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 14 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 4 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 15 Then

				appDetail.Reason = dt.Rows(i).Item("txtReason")
				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 8 Then

				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			ElseIf dt.Rows(i).Item("fkiAppTypeId") = 5 Then

				appDetail.DOR = dt.Rows(i).Item("dteDOR")

			End If


			appDetail.ApplicationID = dt.Rows(i).Item("txtApplicationCode")
			appDetail.AccountName = dt.Rows(i).Item("txtAccountName")
			appDetail.AccountNo = dt.Rows(i).Item("txtAccountNo")
			'_AppDetail.applicationDocuments = appDocs
			appDetail.ApplicationTypeName = dt.Rows(i).Item("applicationTypeName")
			appDetail.AppTypeId = dt.Rows(i).Item("fkiAppTypeId")
			appDetail.AmountToPay = 0

			appDetail.CreatedBy = dt.Rows(i).Item("txtCreatedBy")
			appDetail.BranchID = dt.Rows(i).Item("fkiBranchID")
			appDetail.BankID = dt.Rows(i).Item("fkiBankID")
			appDetail.PIN = dt.Rows(i).Item("txtPIN")
			appDetail.ApplicationState = dt.Rows(i).Item("txtApplicationState")

			dtocs = cr.PMgetApplicationDocsForCRM(dt.Rows(i).Item("txtApplicationCode"))

			Dim j As Integer

			Do While j < dtocs.Rows.Count

				Dim appDoc As New ApplicationDocumentDetail
				appDoc.DateReceived = dtocs.Rows(j).Item("dteReceived")
				appDoc.DocumentLocation = dtocs.Rows(j).Item("txtDocumentPath")
				appDoc.DocumentTypeID = dtocs.Rows(j).Item("fkiDocumentTypeID")
				appDoc.DocumentTypeName = dtocs.Rows(j).Item("documentName")
				'appDoc.DMSDocumentExt = "." & dtocs.Rows(j).Item("txtDMSDocumentExt")
				appDocDetails.Add(appDoc)
				j = j + 1
			Loop

			Dim CRMResponse As String = MoveToCRM(appDetail, appDocDetails)


			If CRMResponse <> "Error on SurePay" Then

				Dim logerr As New Global.Logger.Logger
				logerr.FileSource = "LPPFACRMService"
				logerr.FilePath = Server.MapPath("~/Logs")
				logerr.Logger("Feedback FROM CRM : " & CRMResponse)

			Else

				Dim logerr As New Global.Logger.Logger
				logerr.FileSource = "LPPFACRMService"
				logerr.FilePath = Server.MapPath("~/Logs")
				logerr.Logger("Application Submission Declined")

			End If


			i = i + 1
		Loop


		Try


			



		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "LPPFACRMService"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message & "LPPFACRMService")
		End Try

	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		If IsPostBack = False Then
			getCRMState()
		Else

		End If


	End Sub

	Protected Sub getCRMState()
		Dim lstState As New List(Of States), i As Integer
		Dim st As New States

		Try

			lstState = st.PopulateStates()
			Do While i < lstState.Count

				StateCollection.Add(lstState(i).StateName, lstState(i).StateID)
				i = i + 1

			Loop
			ViewState("StateCollection") = StateCollection
		Catch ex As Exception

		End Try




	End Sub
End Class
