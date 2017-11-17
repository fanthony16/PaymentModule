Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit

Partial Class frmApplication
	Inherits System.Web.UI.Page
	Dim DocumentCollection As New Hashtable
	Dim ApprovalTypeCollection As New Hashtable
	Dim InsurerTypeCollection As New Hashtable
	Dim BankTypeCollection As New Hashtable
	Dim BankBranchCollection As New Hashtable
	Dim EmployerHistoryCollection As New Hashtable
	Dim lstRecievedDoc As New ArrayList
	Dim dtDocuments As New DataTable
	Dim dtColumn As New DataColumn
	Dim blnHardShip As Boolean = False
	Dim blnEnbloc As Boolean = False
	Dim blnLegacy As Boolean = False
	Dim blnAnnuity As Boolean = False
	Dim blnPW As Boolean = False
	Dim blnAVC As Boolean = False
	Dim blnDB As Boolean = False
	Dim blnNSITF As Boolean = False

	Private Function copyFile(ByVal path As String, ByVal destinationDir As String, ByVal destinationFile As String) As Boolean

		Try

			If File.Exists(path) And Directory.Exists(destinationDir) And Not File.Exists(destinationFile) Then

				'move attached document to the permanent location if it does not already exists
				File.Copy(path, destinationFile)
				'delete document from the temp folder(folder named after the username)
				File.Delete(path)

				Return True

			ElseIf File.Exists(path) And Directory.Exists(destinationDir) And File.Exists(destinationFile) Then

				'delete document from permanent location if it already exists
				File.Delete(destinationFile)
				'move attached document to the permanent location if it does not already exists
				File.Copy(path, destinationFile)
				'delete document from the temp folder(folder named after the username)
				File.Delete(path)
				Return True
			Else

				File.Delete(path)
				Return False

			End If

		Catch ex As Exception

			' MsgBox("" & ex.Message)

		End Try
		Return False
	End Function

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

				If Me.txtAdhocDocDescription.Text <> "" Then
					Dim query = From n In ary
						Where n.Item("DocumentName") = txtAdhocDocDescription.Text
					If query.Count > 0 Then
						Exit Sub
					Else
					End If
				Else
					Dim query = From n In ary
					  Where n.Item("DocumentName") = Me.ddRequiredDocuments.SelectedValue
					If query.Count > 0 Then
						Exit Sub
					Else
					End If
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


			If Me.txtAdhocDocDescription.Text <> "" Then
				newCustomersRow("DocumentName") = Me.txtAdhocDocDescription.Text
			Else
				newCustomersRow("DocumentName") = Me.ddRequiredDocuments.SelectedValue
			End If


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
	Protected Sub BindGridPreviousApps(dt As DataTable)
		Try

			gridPreviousApps.DataSource = dt
			gridPreviousApps.DataBind()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

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

	Protected Sub BtnNewDetails_Click(sender As Object, e As EventArgs) Handles btnShowPopup.Click

		Dim btnViewDocument As New ImageButton
		btnViewDocument = sender
		Dim i As GridViewRow
		i = btnViewDocument.NamingContainer
		'	MsgBox("" & Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

	End Sub


	Protected Sub btnAddRMASDetail_Click(sender As Object, e As EventArgs) Handles btnShowPopup.Click

		' ViewState("ID") = 0
		Me.MPRMASHardShip.Show()

		'btnAddRMASDetail_Click

	End Sub
	Protected Sub PopulateFundingStatus()
		ddFundingStatus.Items.Add("")
		ddFundingStatus.Items.Add("Funded")
		ddFundingStatus.Items.Add("UnFunded")
	End Sub

	Protected Sub PopulateApplicationStatus()

		ddStatus.Items.Add("")
		ddStatus.Items.Add("Documentation")
		ddStatus.Items.Add("Processing")
		ddStatus.Items.Add("Confirmation")
		ddStatus.Items.Add("Sent to Pencom")
		ddStatus.Items.Add("Approved/Processing")
		ddStatus.Items.Add("Paid")
		ddStatus.Items.Add("Terminated")

	End Sub


	Protected Sub PopulateCommentGroup()

		ddCommentGroup.Items.Add("")
		ddCommentGroup.Items.Add("Complete Documents")
		ddCommentGroup.Items.Add("Incomplete Documents")
		ddCommentGroup.Items.Add("Irregular Names")
		ddCommentGroup.Items.Add("Irregular Signature")
		ddCommentGroup.Items.Add("Irregular DOB")
		ddCommentGroup.Items.Add("Irregular DOR")
		ddCommentGroup.Items.Add("Irregular Bank Details")
		ddCommentGroup.Items.Add("Irrelevant Payslip")
		ddCommentGroup.Items.Add("Accrued Right UnFunded")
		ddCommentGroup.Items.Add("Outstanding ARL")

	End Sub

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


	Protected Sub getAnnuityPW(PIN As String)

		Dim i As Integer = 0
		Dim dtAnnPW As New DataTable, cr As New Core
		dtAnnPW = cr.PMgetAnnuityPW(PIN)
		ddAnnRunningPW.Items.Clear()
		Do While i < dtAnnPW.Rows.Count

			If ddAnnRunningPW.Items.Count = 0 Then
				ddAnnRunningPW.Items.Add("")
				ddAnnRunningPW.Items.Add(dtAnnPW.Rows(i).Item(0))
			ElseIf dtAnnPW.Rows.Count > 0 Then
				ddAnnRunningPW.Items.Add(dtAnnPW.Rows(i).Item(0))
			End If
			i = i + 1

		Loop

		If dtAnnPW.Rows.Count > 0 Then

			ddAnnRunningPW.Text = dtAnnPW.Rows(0).Item(0)
			populateRuuningPW()
		Else
		End If

	End Sub


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

	'Populating bank List   getBanks
	'    Protected Sub populateBank()

	'         Dim myState As New States, i As Integer = 0
	'         Dim lstBank As New List(Of String)
	'         lstBank = getBanks()
	'         Me.ddBankName.Items.Clear()

	'         Do While i < lstBank.Count

	'              If Me.ddBankName.Items.Count = 0 Then
	'                   Me.ddBankName.Items.Add("")
	'              ElseIf Me.ddBankName.Items.Count > 0 Then
	'                   Me.ddBankName.Items.Add(lstBank.Item(i))
	'              End If
	'              i = i + 1

	'         Loop

	'End Sub

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

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Try

			Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtusers As New DataTable

			scriptManagerA = ScriptManager.GetCurrent(Me.Page)
			scriptManagerA.RegisterPostBackControl(Me.gridRecievedDocument)

			scriptManagerB = ScriptManager.GetCurrent(Me.Page)
			scriptManagerB.RegisterPostBackControl(Me.btnOtherDetails)

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
					getInsurerTypes()
					PopulateApplicationStatus()
					PopulateFundingStatus()
					PopulateCommentGroup()



				Else
				End If

			Else

			End If

		Catch ex As Exception
			'               MsgBox("" & ex.Message)
		End Try
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


	Public Function getBankBranchName(id As Integer) As String

		Dim lstBankBranchName As New List(Of String)
		Dim dc As New BanksDataContext
		Dim querys = From m In dc.BankBranches
				  Where m.BankBranchID = id
				  Select New With {m.BranchName} Order By 1 Ascending

		For Each m In querys
			lstBankBranchName.Add(m.BranchName)
		Next

		If (lstBankBranchName.Count > 0) Then
			Return lstBankBranchName(0).ToString
		Else
			Return Nothing
		End If

	End Function

	Public Function getBankBranchID(Name As String) As Integer

		Dim lstBankBranchName As New List(Of String)
		Dim dc As New BanksDataContext
		Dim querys = From m In dc.BankBranches
				  Where m.BranchName = Name
				  Select New With {m.BankBranchID}

		For Each m In querys
			lstBankBranchName.Add(m.BankBranchID)
		Next

		If (lstBankBranchName.Count > 0) Then
			Return CInt(lstBankBranchName(0))
		Else
			Return Nothing
		End If

	End Function


	Public Function getBankName(id As Integer) As String

		Dim lstBankName As New List(Of String)
		Dim dc As New BanksDataContext
		Dim querys = From m In dc.Banks
				  Where m.BankID = id
				  Select New With {m.BankName}

		For Each m In querys
			lstBankName.Add(m.BankName)
		Next

		If (lstBankName.Count > 0) Then
			Return lstBankName(0).ToString
		Else
			Return Nothing
		End If

	End Function

	Public Function getBanks() As List(Of String)

		Dim lstBankTypes As New List(Of String)
		Dim dc As New BanksDataContext
		BankTypeCollection.Clear()
		Dim query = From m In dc.Banks
				  Select m Order By m.BankName Ascending

		For Each a As Bank In query

			lstBankTypes.Add(a.BankName)
			BankTypeCollection.Add(a.BankName, a.BankID)

		Next
		ViewState("BankTypeCollection") = BankTypeCollection
		Return lstBankTypes

	End Function
	'getting the ID of a particular approval type
	Public Function getApprovalType(typeName As String) As Integer


		Dim dc As New AppDocumentsDataContext
		Dim query = From m In dc.tblApplicationTypes
				  Where m.txtDescription = typeName
				  Select New With {m.pkiAppTypeId}

		Dim typeID As Integer
		For Each a In query
			typeID = a.pkiAppTypeId
		Next

		Return typeID

	End Function

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




	''' <summary>
	''' geting list of bank branch for a selected bank
	''' </summary>
	''' <param name="bankID"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	''' 
	Public Function getBankBranches(bankID As Integer) As List(Of String)
		Try


			Dim dc As New BanksDataContext
			Dim lstBankBranches As New List(Of String)
			Dim query = From m In dc.Banks Join n In dc.BankBranches On m.BankID Equals n.BankID Where m.BankID = bankID Order By n.BranchName Ascending _
					  Select New With {n.BankBranchID, n.BranchName}
			For Each a In query

				lstBankBranches.Add(a.BranchName)
				'DocumentCollection.Add(a.BranchName, a.BankBranchID)
				If DocumentCollection.ContainsKey(a.BranchName) = True Then

				Else
					DocumentCollection.Add(a.BranchName, a.BankBranchID)

				End If


			Next
			ViewState("BankBranchCollection") = BankBranchCollection
			Return lstBankBranches
		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)
			pnlError.Visible = True
			Me.lblError.Text = "Error Loading Bank Branches"
		End Try
	End Function

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

	Protected Sub ddApplicationType_TextChanged(sender As Object, e As EventArgs) Handles ddApplicationType.TextChanged

		Try
			Dim lstAppDocs As List(Of String), i As Integer, appType As Integer = 0, cr As New Core, dtApplications As New DataTable
			' Me.lblAppTypeError.Text = "Error On Selection"
			pnlAppTypeVerificationError.Visible = False
			ViewState("RMAS") = Nothing

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
							pnlAppTypeVerificationError.Visible = True
							Me.lblAppTypeError.Text = "Application Already Exit for this Type !. "
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

						Me.dvVerified.Visible = False
						Dim date2 As Date = Date.Parse(txtDOB.Text)
						Dim date1 As Date = Now
						Dim years As Long = DateDiff(DateInterval.Year, date2, date1)

						If years < 50 Then

							pnlAppTypeVerificationError.Visible = True
							dvOverrideAge.Visible = True
							Me.lblAppTypeError.Text = "Application Type Error !. Age Most be More than 50 and above"
							ddRequiredDocuments.Enabled = False
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
							ddRequiredDocuments.Enabled = True
							pnlAppTypeVerificationError.Visible = True
							Me.lblAppTypeError.Text = "Application Type Error !. Age Not Less Than 50"
							Me.ddRequiredDocuments.Items.Clear()
							Exit Sub
						Else
							ddRequiredDocuments.Enabled = True
						End If
						Me.dvDownloadCalculator.Visible = False
						divAnnRunningPW.Visible = False

					Case Is = 5

						Me.dvVerified.Visible = True
						dvOverrideAge.Visible = False
						Me.chkOverrideAgeBarrier.Checked = False
						Me.dvDownloadCalculator.Visible = False
						divAnnRunningPW.Visible = False
					Case Is = 3

						Me.dvDownloadCalculator.Visible = True
						divAnnRunningPW.Visible = False

					Case Is = 4

						Me.dvDownloadCalculator.Visible = False
						getAnnuityPW(Me.txtPIN.Text)
						divAnnRunningPW.Visible = True

					Case Else

						Me.dvVerified.Visible = False
						dvOverrideAge.Visible = False
						Me.chkOverrideAgeBarrier.Checked = False
						Me.dvDownloadCalculator.Visible = False
						divAnnRunningPW.Visible = False

				End Select

				populateExitReasons(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

				If Me.txtSector.Text = "Public" Then
					lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), Me.txtSector.Text)
				Else
					lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "Private")
				End If

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

			'retrieving the cached unsaved previously uploaded documents for a customer and a particular application type from the NPM_Doc_Temp folder
			Dim tempFilePath As String = Server.MapPath("~/NPM_Doc_Temp/" & Session("user"))
			'	Dim NewDirectory As String = "~/NPM_Doc_Temp/" & Session("user")
			'If Directory.Exists("C:\NPM_Doc_Temp\" & Session("user")) = True Then
			If Directory.Exists(tempFilePath) = True Then
				Dim lstFiles As String()
				lstFiles = Directory.GetFiles(tempFilePath)
				loadFilesFromTempFolder(lstFiles)
			Else

			End If

			i = 0

		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try

	End Sub

	Private Sub loadFilesFromTempFolder(files As String())

		Dim i As Integer

		If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		Else
		End If

		Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

		dtDocuments = New DataTable
		dtColumn = New DataColumn("DocumentName")
		dtDocuments.Columns.Add(dtColumn)
		dtColumn = New DataColumn("RecievedDate")
		dtDocuments.Columns.Add(dtColumn)
		dtColumn = New DataColumn("DocumentPath")
		dtDocuments.Columns.Add(dtColumn)
		dtColumn = New DataColumn("IsVerified")
		dtDocuments.Columns.Add(dtColumn)

		Do While i < files.Length

			Dim str As String() = files(i).Split("\")(5).Split("_")
			Dim sarrMyString As String() = files(i).Split(New String() {"PEN"}, StringSplitOptions.None)

			If str(0) = typeID And str(2) = Me.txtPIN.Text Then

				Dim newCustomersRow As DataRow, strr As String = ""
				newCustomersRow = dtDocuments.NewRow()
				newCustomersRow("DocumentName") = str(1).Replace(" - ", " | ")
				newCustomersRow("RecievedDate") = Me.txtRecievedDate.Text

				newCustomersRow("DocumentPath") = "PEN" & sarrMyString(1)

				newCustomersRow("IsVerified") = 1

				dtDocuments.Rows.Add(newCustomersRow)

			Else
			End If

			i = i + 1

		Loop


		ViewState("RecievedDocument") = dtDocuments
		loadGrid(dtDocuments)


	End Sub
	''' <summary>
	''' 'removing from the recieved document list
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Protected Sub btnRemoveDocument_Click(sender As Object, e As EventArgs) Handles btnRemoveDocument.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, aryIndex As New ArrayList

		For Each grow As GridViewRow In Me.gridRecievedDocument.Rows

			grow.FindControl("chkSelect")
			cb = grow.FindControl("chkSelect")

			If cb.Checked = True Then

				aryIndex.Add(grow.RowIndex)

				chk = chk + 1

			ElseIf cb.Checked = False Then

			End If

		Next



		If chk = 1 Then


			dvDocumentError.Visible = False
			For Each grow As GridViewRow In Me.gridRecievedDocument.Rows

				grow.FindControl("chkSelect")
				cb = grow.FindControl("chkSelect")

				If cb.Checked = True Then

					dtDocuments = ViewState("RecievedDocument")

					Dim filePath As String = (Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + dtDocuments.Rows(grow.RowIndex).Item("DocumentPath").ToString))

					dtDocuments.Rows(grow.RowIndex).Delete()
					ViewState("RecievedDocument") = dtDocuments
					Session("documentPath") = Nothing
					loadGrid(dtDocuments)


					If File.Exists(filePath) = True Then
						File.Delete(filePath)
					Else

					End If

				ElseIf cb.Checked = False Then

				End If

			Next

			loadGrid(dtDocuments)

		ElseIf chk > 1 Then

			Dim j As Integer
			dtDocuments = ViewState("RecievedDocument")
			'	Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))
			Do While j < aryIndex.Count



				If j = 0 Then

					Dim filePath As String = (Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + dtDocuments.Rows(aryIndex.Item(j)).Item("DocumentPath").ToString))
					'Dim filePath2 As String = (Server.MapPath("~/NPM_Doc_Temp" + "/" + Session("user") + "/" + typeID.ToString + "_" + gridRecievedDocument.Rows(aryIndex.Item(j)).Cells(3).ToString))
					'
					dtDocuments.Rows(aryIndex.Item(j)).Delete()
					If File.Exists(filePath) = True Then
						File.Delete(filePath)
					Else

					End If
				Else

					Dim filePath As String = (Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + dtDocuments.Rows(aryIndex.Item(j) - 1).Item("DocumentPath").ToString))
					dtDocuments.Rows(aryIndex.Item(j) - 1).Delete()
					If File.Exists(filePath) = True Then
						File.Delete(filePath)
					Else

					End If
				End If

				j = j + 1

			Loop
			ViewState("RecievedDocument") = dtDocuments
			loadGrid(dtDocuments)


			Exit Sub
		Else
			dvDocumentError.Visible = False
			Exit Sub
		End If

	End Sub

	Protected Sub calApplicationDate_SelectionChanged(sender As Object, e As EventArgs) Handles calApplicationDate.SelectionChanged

		Me.calApplicationDate_PopupControlExtender.Commit(Me.calApplicationDate.SelectedDate)

	End Sub

	Protected Sub calRecievedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRecievedDate.SelectionChanged

		Me.calRecievedDate_PopupControlExtender.Commit(Me.calRecievedDate.SelectedDate)

	End Sub
	''' <summary>
	''' loading passport, signature and the cusromer other details
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks></remarks>
	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
		Try

			'mpCheckListDBA.Show()
			'Exit Sub

			'this is enforcing that the cut-off date is supplied when the check button is selected
			'If chkCutOff.Checked = True And Me.txtCutOffDate.Text = "" Then
			'	Me.spCutOffError.Visible = True
			'	Exit Sub
			'Else
			'End If

			Dim myState As New States, myLGA As New LGA

			imgPassport.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", txtPIN.Text, 1, Server.MapPath("~/Logs"))
			imgSignature.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", txtPIN.Text, 2, Server.MapPath("~/Logs"))

			Dim cr As New Core, dt As DataTable, dtApplications As DataTable
			ViewState("EmployerHistoryCollection") = Nothing
			dt = cr.getPMPersonInformation(Me.txtPIN.Text)
			dtApplications = cr.PMgetApplicationByPIN(Me.txtPIN.Text, "")
			ViewState("dtApplications") = dtApplications

			Session("PIN") = Me.txtPIN.Text

			If dt.Rows.Count > 0 Then
				ViewState("Employerid") = dt.Rows(0).Item("employerid")
				ViewState("EmployeeID") = dt.Rows(0).Item("employeeid")
				ViewState("EmployerCode") = dt.Rows(0).Item("EmployerCode")
				ViewState("Sector") = dt.Rows(0).Item("Sector")
				lblTitle.Text = dt.Rows(0).Item("Title")
				Me.txtNOKNo.Text = dt.Rows(0).Item("NOKPhone")
				Me.txtSurname.Text = dt.Rows(0).Item("Surname")
				Me.txtFirstName.Text = dt.Rows(0).Item("FirstName")
				Me.txtOtherNames.Text = dt.Rows(0).Item("MiddleName")
				Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")
				Me.txtAge.Text = dt.Rows(0).Item("Age")

				Me.txtDelaredAge.Text = dt.Rows(0).Item("Age")

				Me.txtEmployer.Text = dt.Rows(0).Item("EmployerName").ToString
				Me.txtNOK.Text = dt.Rows(0).Item("NOK").ToString
				Me.txtEmail.Text = dt.Rows(0).Item("email").ToString
				Me.txtSex.Text = dt.Rows(0).Item("sex").ToString

				Me.txtPhone.Text = dt.Rows(0).Item("Phone").ToString

				Me.txtOfficeAddress.Text = dt.Rows(0).Item("OfficeAddress")

				If Not dt.Rows(0).Item("OfficeStateID") Is DBNull.Value Then
					Me.txtOfficeState.Text = myState.getStateName(CInt(dt.Rows(0).Item("OfficeStateID"))).ToString
				Else
					'Me.txtOfficeState.Text = myState.getStateName(0).ToString
					Me.txtOfficeState.Text = ""
				End If

				If Not dt.Rows(0).Item("OfficeLGAID") Is DBNull.Value Then
					txtOfficeLGA.Text = myLGA.getLGAName(CInt(dt.Rows(0).Item("OfficeLGAID"))).ToString
				Else
					'txtOfficeLGA.Text = myLGA.getLGAName(0).ToString
					txtOfficeLGA.Text = ""
				End If

				txtResidentialAddress.Text = dt.Rows(0).Item("ResidentialAddress")

				If Not dt.Rows(0).Item("ResidentialStateID") Is DBNull.Value Then
					Me.txtResidentialState.Text = myState.getStateName(CInt(dt.Rows(0).Item("ResidentialStateID"))).ToString
				Else
					Me.txtResidentialState.Text = ""
				End If


				If Not dt.Rows(0).Item("ResidentialLGAID") Is DBNull.Value Then
					Me.txtResidentialLGA.Text = myLGA.getLGAName(CInt(dt.Rows(0).Item("ResidentialLGAID"))).ToString
				Else
					txtResidentialLGA.Text = ""
				End If


				Me.txtPermanentAddress.Text = dt.Rows(0).Item("ContactAddress")

				If Not dt.Rows(0).Item("ContactStateID") Is DBNull.Value Then
					Me.txtPermanentState.Text = myState.getStateName(CInt(dt.Rows(0).Item("ContactStateID"))).ToString
				Else
					'Me.txtPermanentState.Text = myState.getStateName(0).ToString
					Me.txtPermanentState.Text = ""
				End If


				If Not dt.Rows(0).Item("ContactLGAID") Is DBNull.Value Then
					Me.txtPermanentLGA.Text = myLGA.getLGAName(CInt(dt.Rows(0).Item("ContactLGAID"))).ToString
				Else
					'     txtPermanentLGA.Text = myLGA.getLGAName(0).ToString
					txtPermanentLGA.Text = ""
				End If

				Me.txtRSABalance.Text = CDbl(dt.Rows(0).Item("RSABalance"))
				Me.txtDBARSABalance.Text = CDbl(dt.Rows(0).Item("RSABalance"))
				Me.txtMandatory.Text = CDbl(dt.Rows(0).Item("Mandatory"))
				Me.txtAVC.Text = CDbl(dt.Rows(0).Item("AVC"))
				Me.txtLegacy.Text = CDbl(dt.Rows(0).Item("Legacy"))

				Me.txtRFBalance.Text = CDbl(dt.Rows(0).Item("RFBalance"))

				Me.txtMandatory.Text = CDbl(dt.Rows(0).Item("Mandatory"))
				Me.txtAVC.Text = CDbl(dt.Rows(0).Item("AVC"))
				Me.txtLegacy.Text = CDbl(dt.Rows(0).Item("Legacy"))


				Me.txtAccruedRight.Text = CDbl(dt.Rows(0).Item("AccruedRight"))

				Me.txtDBAAccruedRight.Text = CDbl(dt.Rows(0).Item("AccruedRight"))
				Me.txtSector.Text = (dt.Rows(0).Item("Sector")).ToString
				Me.txtBasicSalary.Text = CDbl(dt.Rows(0).Item("BasicSalary"))
				Me.txtTransportAllow.Text = CDbl(dt.Rows(0).Item("Transport"))
				Me.txtHousingAllowance.Text = CDbl(dt.Rows(0).Item("Housing"))

				''''set values on the Program withdrawal and annuity details and death benefit''''''''''''''''''
				Me.txtValueDate.Text = CDate(dt.Rows(0).Item("RSAPriceDate"))
				Me.txtValueDateAnnuity.Text = CDate(dt.Rows(0).Item("RSAPriceDate"))
				Me.txtRSABalancePW.Text = CDbl(dt.Rows(0).Item("RSABalance"))
				Me.txtRSABalanceAnnuity.Text = CDbl(dt.Rows(0).Item("RSABalance"))
				txtDBValueDate.Text = CDate(dt.Rows(0).Item("RSAPriceDate"))
				'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				''''''setting value for NSITF details------------

				txtInitialAmountPaid.Text = CDbl(dt.Rows(0).Item("TotalRFPayment"))
				txtAmountRecievedToRSANSITF.Text = CDbl(dt.Rows(0).Item("TotalNSITFUpload"))
				txtAmountRequestedFromRSANSITF.Text = CDbl(dt.Rows(0).Item("TotalNSITFValueRSA"))
				'TotalRFPayment



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

				'BranchName 


				' checking if customer has previous applications and displaying to the user 

				If dtApplications.Rows.Count > 0 Then

					ViewState("PreviousApplication") = dtApplications
					ViewState("FileNumber") = CStr(dtApplications.Rows(0).Item("txtFileNo").ToString)

					Me.BindGridPreviousApps(dtApplications)
					Me.MPPreviousApps.Show()
				Else
				End If


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

			Dim dtApplications As New DataTable

			If Not IsNothing(ViewState("dtApplications")) = True Then

				dtApplications = ViewState("dtApplications")
				If dtApplications.Rows.Count > 0 Then

					ViewState("PreviousApplication") = dtApplications
					ViewState("FileNumber") = CStr(dtApplications.Rows(0).Item("txtFileNo").ToString)

					Me.BindGridPreviousApps(dtApplications)
					Me.MPPreviousApps.Show()
				Else
				End If

			Else
			End If

		End Try
	End Sub

	Protected Sub ddBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddBankName.SelectedIndexChanged

		'BankTypeCollection = ViewState("BankTypeCollection")
		'Dim lstBankBranches As New List(Of String), i As Integer = 0
		'lstBankBranches = getBankBranches(CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text)))

		'Me.ddBankBranch.Items.Clear()
		'Do While i < lstBankBranches.Count

		'     If Me.ddBankBranch.Items.Count = 0 Then
		'          Me.ddBankBranch.Items.Add("")
		'          Me.ddBankBranch.Items.Add(lstBankBranches.Item(i))
		'     ElseIf Me.ddBankBranch.Items.Count > 0 Then
		'          Me.ddBankBranch.Items.Add(lstBankBranches.Item(i))
		'     End If

		'     i = i + 1

		'Loop


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

	Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

		Dim btnDetails As New ImageButton
		btnDetails = sender
		Dim i As GridViewRow
		i = btnDetails.NamingContainer
		'	DownLoadDocument(Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text))



		Dim tmpPath As String = Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		'Dim permPath As String = Server.MapPath("~/ApplicationDocuments" + "/" + CStr(ViewState("appCode")).ToString.Replace("-", "_") + "_" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		Dim permPath As String = Server.MapPath("~/ApplicationDocuments" + "/" + Me.ddAnnRunningPW.Text.ToString.Replace("-", "_") + "_" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

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

			If Me.txtAdhocDocDescription.Text <> "" Then

				Session("Document") = Me.txtAdhocDocDescription.Text
				fileNewName = Session("Document").ToString

			Else
				fileNewName = Session("Document").ToString

			End If

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

	Protected Sub AjaxFileBankUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)


		Try

			'Dim filename As String = System.IO.Path.GetFileName(e.FileName)
			'Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)
			''Dim strUploadPath As String = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & Session("Document").ToString & "_" & filename

			'Dim strUploadPath As String = "~/FileUploads/" & Session("user")
			'MakeDirectoryIfExists(strUploadPath)
			'strUploadPath = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_BankConfirmation_" & filename

			'Session("BankdocumentPath") = strUploadPath
			'Me.FlUploadBankConfirmation.SaveAs(Server.MapPath(strUploadPath))
			'FlUploadBankConfirmation.Dispose()
			''     File.Delete(fullPath)
			'Session("BankdocumentPath") = Nothing


		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try








	End Sub

	Protected Sub gridCustomerHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)

	End Sub
	Protected Sub btnViewEmployerHistory_Click(sender As Object, e As EventArgs) Handles btnViewEmployerHistory.Click

		Dim cr As New Core, i As Integer = 0, dtEmployerHistory As New DataTable

		If IsNothing(ViewState("EmployerHistoryCollection")) = False Then

			EmployerHistoryCollection = ViewState("EmployerHistoryCollection")
			dtEmployerHistory = New DataTable

			dtColumn = New DataColumn("EmployerID")
			dtEmployerHistory.Columns.Add(dtColumn)

			dtColumn = New DataColumn("employerName")
			dtEmployerHistory.Columns.Add(dtColumn)

			dtColumn = New DataColumn("EmployerCode")
			dtEmployerHistory.Columns.Add(dtColumn)

			dtColumn = New DataColumn("LastFundDate")
			dtEmployerHistory.Columns.Add(dtColumn)

			Do While i < EmployerHistoryCollection.Count

				Dim newCustomersRow As DataRow
				newCustomersRow = dtEmployerHistory.NewRow()

				newCustomersRow("employerName") = EmployerHistoryCollection.Keys(i)
				newCustomersRow("EmployerID") = EmployerHistoryCollection.Item(EmployerHistoryCollection.Keys(i))
				newCustomersRow("EmployerCode") = Me.gridCustomerHistory.Rows(i).Cells(3).Text
				newCustomersRow("LastFundDate") = Me.gridCustomerHistory.Rows(i).Cells(4).Text

				dtEmployerHistory.Rows.Add(newCustomersRow)

				i = i + 1
			Loop


		Else

			dtEmployerHistory = cr.getEmployerHistory(Me.txtPIN.Text)

			Do While i < dtEmployerHistory.Rows.Count

				EmployerHistoryCollection.Add(dtEmployerHistory.Rows(i).Item("employerName"), dtEmployerHistory.Rows(i).Item("EmployerID"))
				i = i + 1
			Loop
			ViewState("EmployerHistoryCollection") = EmployerHistoryCollection

		End If


		gridCustomerHistory.DataSource = dtEmployerHistory
		gridCustomerHistory.DataBind()

		' EmployerHistoryCollection = New Hashtable

		mpEmployerList.Show()

	End Sub


	Protected Sub gridCustomerHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridCustomerHistory.SelectedIndexChanged

		Dim selectedRowIndex As Integer
		Dim selectedLodgment As New ArrayList

		selectedRowIndex = gridCustomerHistory.SelectedRow.RowIndex

		Dim row As GridViewRow = gridCustomerHistory.Rows(selectedRowIndex)

		Me.txtEmployer.Text = row.Cells(2).Text.ToString.Replace("&amp;", "&")

	End Sub

	Protected Sub btnOtherDetails_Click(sender As Object, e As EventArgs) Handles btnOtherDetails.Click

		Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)

		Select Case apptypeID
			Case 1

			Case 2
				' Me.MPRMASHardShip.Show()
			Case 3

				DownLoadDocument("\\zeus\NPM_Data\ApplicationDocuments\PW_Model_Template_Operators.xlsx")
			Case 4

			Case Else

		End Select

	End Sub

	Protected Sub checkRMASstatus()

		Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

		If IsNothing(ViewState("HardShipRMAS")) = True And typeID = 2 Then

			Me.MPRMASHardShip.Show()

		ElseIf IsNothing(ViewState("HardShipRMAS")) = False And CBool(ViewState("HardShipRMAS")) = False Then

			Me.MPRMASHardShip.Show()

		Else
		End If

	End Sub

	Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click


		
		Dim date2 As Date = Date.Parse(txtDOB.Text)
		Dim date1 As Date = Now
		Dim years As Long = DateDiff(DateInterval.Year, date2, date1)
		Dim cr As New Core



		If years < 50 And Me.chkOverrideAgeBarrier.Checked = True And Me.txtOtherComments.Text = "" Then

			lblError.Text = "Please Enter Comment For Age Barrier Overriden"
			Me.pnlError.Visible = True

			Exit Sub
		Else

		End If


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
				rDetails.RSABalance = Me.txtRSABalanceAnnuity.Text
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


			If IsNothing(ViewState("EmployerHistoryCollection")) = True And IsNothing(ViewState("Employerid")) = False Then
				appDetail.EmployerID = CInt(ViewState("Employerid"))
				appDetail.EmployerName = Me.txtEmployer.Text
				appDetail.EmployerCode = ViewState("EmployerCode").ToString
			ElseIf IsNothing(ViewState("EmployerHistoryCollection")) = False Then
				EmployerHistoryCollection = ViewState("EmployerHistoryCollection")
				appDetail.EmployerID = CInt(EmployerHistoryCollection.Item(Me.txtEmployer.Text))
				appDetail.EmployerName = Me.txtEmployer.Text
			End If

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

			ElseIf typeID = 4 Then

				appDetail.Reason = Me.ddRetirementGroundAnnuity.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			ElseIf typeID = 15 Then

				appDetail.Reason = Me.ddRetirementGroundAnnuity.SelectedItem.Text.ToString
				appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)

			End If

			appDetail.Comment = Me.txtOtherComments.Text
			appDetail.CommentGroup = Me.ddCommentGroup.SelectedItem.Text.ToString
			'               appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
			appDetail.MemberID = CInt(ViewState("EmployeeID"))
			appDetail.ApplicationDate = CDate(Me.txtApplicationDate.Text)
			appDetail.ApplicationState = ddApplicationState.SelectedItem.Text.ToString
			appDetail.Sex = Me.txtSex.Text
			appDetail.AccountName = Me.txtAccountName.Text
			appDetail.AccountNo = Me.txtAccountNumber.Text
			appDetail.BVN = Me.txtBVN.Text
			appDetail.Comment = Me.txtOtherComments.Text

			If Me.chkBankConfirmed.Checked = True Then
				appDetail.IsBankDetailsConfirmed = 1
			Else
				appDetail.IsBankDetailsConfirmed = 0
			End If



			''''''''''''''''''''''''''ARL Notification'''''''''''''''''''''''''''''''''''''

			If Me.rdARLAckRecieved.Checked = True Then

				appDetail.IsARLActRecieved = True
				appDetail.ARLAcknowledgmentDate = CDate(Me.txtApplicationDate.Text)

			ElseIf Me.rdARLRecieved.Checked = False Then

				appDetail.IsARLActRecieved = False

			End If

			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


			If Not IsNothing(ViewState("BankTypeCollection")) = True Then
				BankTypeCollection = ViewState("BankTypeCollection")
				appDetail.BankID = CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text.ToString))
			Else
			End If

			'            If Not IsNothing(ViewState("BankBranchCollection")) = True Then
			'                 BankBranchCollection = ViewState("BankBranchCollection")
			'	appDetail.BranchID = getBankBranchID(Me.ddBankBranch.SelectedItem.Text.ToString)
			'            Else
			'End If


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
				'appDetail.AppTypeId = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))
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

					If CInt(DocumentCollection.Item(row.Cells(1).Text)) = 0 Then

						Dim appAdhocDocDetail As New AdhocDocuments

						If CStr((row.Cells(3).Text)) = "&nbsp;" Then
							isAllDocumentScanned = False
						Else
							isAllDocumentScanned = True
						End If

						appAdhocDocDetail.ApplicationCode = ApplicationCode
						appAdhocDocDetail.Description = row.Cells(1).Text
						appAdhocDocDetail.PIN = LTrim(RTrim(Me.txtPIN.Text))
						appAdhocDocDetail.RecievedBy = Session("user")
						appAdhocDocDetail.RecievedDate = Now
						appAdhocDocDetail.DocPath = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

						'appAdhocDocDetail.DocPath = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "D:\NPM_Data\ApplicationDocuments\"

						appAdhocDocDetail.IsVerified = CInt((row.Cells(5).Text))
						appAdhocDocDetails.Add(appAdhocDocDetail)

					Else

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

					End If
					docCount = docCount + 1
				Loop
			Else
			End If

			appDetail.CommentGroup = Me.ddCommentGroup.Text

			'''''''''''''''''''''
			'''remove this ''''''

			'  Exit Sub


			If ((Me.ddRequiredDocuments.Items.Count - 1) = Me.gridRecievedDocument.Rows.Count) = True And Me.ddStatus.SelectedItem.Text = "" And isAllDocumentScanned = True Then
				appDetail.Status = "Documentation"

				appDetail.DateDocumentCompleted = DateTime.Parse(Now)

				appDetail.DocCompleted = 1
			ElseIf ((Me.ddRequiredDocuments.Items.Count - 1) = Me.gridRecievedDocument.Rows.Count) = True And Me.ddStatus.SelectedItem.Text = "" And isAllDocumentScanned = False Then
				appDetail.Status = "Entry"
				appDetail.DocCompleted = 0
				appDetail.DateDocumentCompleted = Nothing

			ElseIf ((Me.ddRequiredDocuments.Items.Count - 1) = Me.gridRecievedDocument.Rows.Count) = False And Me.ddStatus.SelectedItem.Text = "" Then
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

			Dim chrequests As New List(Of ChangeRequest)

			If Not IsNothing(ViewState("emailAddress")) = True Then
				Dim chrequest As New ChangeRequest
				chrequest.FieldName = CStr(ViewState("emailAddress")).ToString.Split("|")(0)
				chrequest.FieldValue = CStr(ViewState("emailAddress")).ToString.Split("|")(1)
				chrequests.Add(chrequest)
			Else
			End If

			If Not IsNothing(ViewState("Telephone")) = True Then
				Dim chrequest As New ChangeRequest
				chrequest.FieldName = CStr(ViewState("Telephone")).ToString.Split("|")(0)
				chrequest.FieldValue = CStr(ViewState("Telephone")).ToString.Split("|")(1)
				chrequests.Add(chrequest)
			Else
			End If

			If Not IsNothing(ViewState("OfficeAddress")) = True Then
				Dim chrequest As New ChangeRequest
				chrequest.FieldName = CStr(ViewState("OfficeAddress")).ToString.Split("|")(0)
				chrequest.FieldValue = CStr(ViewState("OfficeAddress")).ToString.Split("|")(1)
				chrequests.Add(chrequest)
			Else
			End If

			If Not IsNothing(ViewState("ResidentailAddress")) = True Then
				Dim chrequest As New ChangeRequest
				chrequest.FieldName = CStr(ViewState("ResidentailAddress")).ToString.Split("|")(0)
				chrequest.FieldValue = CStr(ViewState("ResidentailAddress")).ToString.Split("|")(1)
				chrequests.Add(chrequest)
			Else
			End If

			If Not IsNothing(ViewState("PermanentHomeAddress")) = True Then
				Dim chrequest As New ChangeRequest
				chrequest.FieldName = CStr(ViewState("PermanentHomeAddress")).ToString.Split("|")(0)
				chrequest.FieldValue = CStr(ViewState("PermanentHomeAddress")).ToString.Split("|")(1)
				chrequests.Add(chrequest)
			Else
			End If

			If chrequests.Count > 0 Then

				Dim em As New EmailGateway.EmailGateway
				Dim Addy As New EmailGateway.EmailAddress
				Dim Addies As New List(Of EmailGateway.EmailAddress)
				Addy.EmailAddress = "o-taiwo@leadway-pensure.com"
				Addy.Reciever = True
				Addies.Add(Addy)
				'sending change request to CSD
				em.sendMailWithOutAttachmentAddess(AMsg(chrequests, Me.txtPIN.Text, Session("user")), "Data Change Request", Addies)

			Else

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

		End If

	End Sub

	'builds the message to be sent out on data change request complaint Unit in CSD
	Private Function AMsg(lstChangeRequest As List(Of ChangeRequest), pin As String, UName As String) As String

		Dim msg As String = ""
		Dim sb As New StringBuilder
		' MsgBox("Enter")

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


			sb.Append("<br>Dear All</br>")
			sb.Append("<br></br>")
			sb.Append("<br>This is to inform you of the change request. Please find details below as requested</br>")

			sb.Append("<br></br>")

			Dim i As Integer = 0

			sb.Append(" <table width='1000' border= '1' align='left' cellpadding='2' cellspacing='4' >")
			sb.Append("<tr>")


			sb.Append(" <td colspan='7' align='center' bordercolor='#FFFFFF' bgcolor='#0000FF' scope='col'><span class='style7'>" & pin & " : Data Update Request Notification</span></td>")

			sb.Append("</tr>")
			sb.Append("<tr>")

			sb.Append("<td class='auto-style4'>S/N</td>")
			sb.Append("<td class='auto-style5'>Field Name</td>")
			sb.Append("<td class='auto-style5'>New Value</td>")

			sb.Append("</tr>")


			Do While i < lstChangeRequest.Count
				'the block ensure the listed employers in the body of the mail is not more than 10
				If i < 11 Then

					sb.Append("<tr>")
					'''''serial numbers
					sb.Append("<td class='auto-style4'>" & (i + 1).ToString & "</td>")
					'''''listing the fields and new value to update with.
					sb.Append("<td class='auto-style2'>" & lstChangeRequest(i).FieldName & "</td>")
					sb.Append("<td class='auto-style2'>" & lstChangeRequest(i).FieldValue & "</td>")

					sb.Append("</tr>")

				Else

				End If

				i = i + 1

			Loop
			sb.Append("</table>")

			sb.Append("<br></br>")
			sb.Append("<br></br>")
			sb.Append("<br></br>")

			sb.Append("<br>Requester ID : <b>" & UName & "</b></br>")

			sb.Append("<br></br>")

			sb.Append("<br>Thank you.</br>")
			sb.Append("</body>")
			sb.Append("</html>")

			sb.Append("<br></br>")
			sb.Append("<br></br>")
			sb.Append("<br></br>")

			msg = sb.ToString
		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

		Return msg
	End Function




	Protected Sub btnHardShipOK_Click(sender As Object, e As EventArgs) Handles btnHardShipOK.Click


		blnHardShip = True
		ViewState("RMAS") = blnHardShip

	End Sub

	'Protected Sub calRetirementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRetirementDate.SelectionChanged
	'     Me.calRetirementDate_PopupControlExtender.Commit(Me.calRetirementDate.SelectedDate)
	'End Sub

	Protected Sub calDisengagementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calDisengagementDate.SelectionChanged

		Me.calDisengagementDate_PopupControlExtender.Commit(Me.calDisengagementDate.SelectedDate)
		Me.MPRMASHardShip.Show()

	End Sub


	Protected Sub btnEmployerHistory_Click(sender As Object, e As EventArgs) Handles btnEmployerHistory.Click

	End Sub

	Protected Sub imgUpdateEmail_Click(sender As Object, e As ImageClickEventArgs) Handles imgUpdateEmail.Click

		ViewState("emailAddress") = "Email Address : |" & Me.txtEmail.Text
		txtEmail.Enabled = False



	End Sub

	Protected Sub imgUpdatePhone_Click(sender As Object, e As ImageClickEventArgs) Handles imgUpdatePhone.Click

		ViewState("Telephone") = "Telephone No : |" & Me.txtPhone.Text
		txtPhone.Enabled = False

	End Sub

	Protected Sub imgUpdateOffice_Click(sender As Object, e As ImageClickEventArgs) Handles imgUpdateOffice.Click

		ViewState("OfficeAddress") = "Office Address : |" & txtOfficeAddress.Text & " " & Me.txtOfficeLGA.Text & " " & Me.txtOfficeState.Text
		txtOfficeAddress.Enabled = False
		txtOfficeLGA.Enabled = False
		txtOfficeState.Enabled = False


	End Sub

	Protected Sub imgUpdateResid_Click(sender As Object, e As ImageClickEventArgs) Handles imgUpdateResid.Click

		ViewState("ResidentailAddress") = "Residential Address : |" & Me.txtResidentialAddress.Text & " " & Me.txtResidentialLGA.Text & " " & Me.txtResidentialState.Text

		txtResidentialAddress.Enabled = False
		txtResidentialLGA.Enabled = False
		txtResidentialState.Enabled = False

	End Sub

	Protected Sub imgUpdatePermanentAddress_Click(sender As Object, e As ImageClickEventArgs) Handles imgUpdatePermanentAddress.Click

		ViewState("PermanentHomeAddress") = "Residential Address : |" & Me.txtPermanentAddress.Text & " " & Me.txtPermanentState.Text & " " & Me.txtPermanentLGA.Text

		txtPermanentAddress.Enabled = False
		txtPermanentState.Enabled = False
		txtPermanentLGA.Enabled = False

	End Sub

	Protected Sub ddRequiredDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddRequiredDocuments.SelectedIndexChanged

		If Me.ddRequiredDocuments.SelectedItem.Text = "Others" Then

			Me.dvUploadOtherDocuments.Visible = True
			Session("Document") = Me.ddRequiredDocuments.SelectedItem.Text

			'Accrued Rights Letter'
		Else

			If Me.ddRequiredDocuments.SelectedItem.Text = "Accrued Rights Letter" Then
				dvARL.Visible = True
			Else
				dvARL.Visible = False
			End If

			Session("Document") = Me.ddRequiredDocuments.SelectedItem.Text
			Dim strUploadPath As String = Server.MapPath("~/FileUploads/" & Session("user"))
			MakeDirectoryIfExists(strUploadPath)
			Me.txtAdhocDocDescription.Text = ""
			Me.dvUploadOtherDocuments.Visible = False
			Me.dvfileSizeError.Visible = False

		End If

	End Sub


	'Protected Sub btnViewDocument_Click(sender As Object, e As EventArgs) Handles btnViewDocument.Click

	'     Dim cb As CheckBox, chk As Integer = 0, cr As New Core


	'     For Each grow As GridViewRow In Me.gridRecievedDocument.Rows

	'          grow.FindControl("chkSelect")
	'          cb = grow.FindControl("chkSelect")

	'          If cb.Checked = True Then

	'               chk = chk + 1

	'          ElseIf cb.Checked = False Then

	'          End If

	'     Next



	'     If chk = 1 Then


	'          dvDocumentError.Visible = False
	'          For Each grow As GridViewRow In Me.gridRecievedDocument.Rows

	'               grow.FindControl("chkSelect")
	'               cb = grow.FindControl("chkSelect")

	'               If cb.Checked = True Then


	'                    MsgBox("" & gridRecievedDocument.Rows(grow.RowIndex).Cells(4).Text)

	'                    'dtDocuments = ViewState("RecievedDocument")
	'                    'dtDocuments.Rows(grow.RowIndex).Delete()
	'                    'ViewState("RecievedDocument") = dtDocuments
	'                    'loadGrid(dtDocuments)


	'               ElseIf cb.Checked = False Then

	'               End If

	'          Next
	'     ElseIf chk > 1 Then
	'          dvDocumentError.Visible = True
	'          Exit Sub
	'     Else
	'          dvDocumentError.Visible = False
	'          Exit Sub
	'     End If

	'End Sub

	Protected Sub flReqDocUpload_UploadComplete(sender As Object, e As AjaxFileUploadEventArgs) Handles flReqDocUpload.UploadComplete

	End Sub

	Protected Sub txtPIN_TextChanged(sender As Object, e As EventArgs) Handles txtPIN.TextChanged
		Me.txtPIN.Text = UCase(txtPIN.Text)
	End Sub

	Protected Sub btnEnblocOK_Click(sender As Object, e As EventArgs) Handles btnEnblocOK.Click


		blnEnbloc = True
		ViewState("RMAS") = blnEnbloc

	End Sub

	Protected Sub calRetirementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRetirementDate.SelectionChanged

		Me.calRetirementDate_PopupControlExtender.Commit(Me.calRetirementDate.SelectedDate)
		Me.MPRMASEnbloc.Show()

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

	Protected Sub calDisengagementDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calDisengagementDate.VisibleMonthChanged
		MPRMASHardShip.Show()
	End Sub

	Protected Sub btnLegacyOK_Click(sender As Object, e As EventArgs) Handles btnLegacyOK.Click

		blnLegacy = True
		ViewState("RMAS") = blnLegacy

	End Sub

	Protected Sub calLegacyRetirementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calLegacyRetirementDate.SelectionChanged

		Me.calLegacyRetirementDate_PopupControlExtenderLegacy.Commit(Me.calLegacyRetirementDate.SelectedDate)
		Me.MPRMASLegacy.Show()

	End Sub

	Protected Sub btnOKAnnuity_Click(sender As Object, e As EventArgs) Handles btnOKAnnuity.Click

		blnAnnuity = True
		ViewState("RMAS") = blnAnnuity

	End Sub

	Protected Sub CalCommencmentDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalCommencmentDate.SelectionChanged

		Me.CalCommencmentDate_PopupControlExtender.Commit(Me.CalCommencmentDate.SelectedDate)
		Me.MPRMASAnnuity.Show()
	End Sub

	Protected Sub CalValueDateAnnuity_SelectionChanged(sender As Object, e As EventArgs) Handles CalValueDateAnnuity.SelectionChanged

		Me.CalValueDateAnnuity_PopupControlExtender.Commit(Me.CalValueDateAnnuity.SelectedDate)
		Me.MPRMASAnnuity.Show()


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
			' Me.txtRSABalancePW.Text = cr.PMUnitPriceByDate(CDate(Me.txtValueDate.Text), 1)
			Me.txtRSABalancePW.Text = cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), CDate(Me.txtValueDate.Text), 1)
			Me.MPRMASPW.Show()
		Catch ex As Exception

		End Try
	End Sub

	Protected Sub CalCommencmentDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalCommencmentDate.VisibleMonthChanged

		Me.MPRMASAnnuity.Show()

	End Sub

	Protected Sub CalValueDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalValueDate.SelectionChanged

		Me.CalValueDate_PopupControlExtender.Commit(Me.CalValueDate.SelectedDate)
		Me.MPRMASPW.Show()

	End Sub

	Protected Sub CalValueDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalValueDate.VisibleMonthChanged

		Me.MPRMASPW.Show()

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

	Protected Sub btnAVCOk_Click(sender As Object, e As EventArgs) Handles btnAVCOk.Click
		blnAVC = True
		ViewState("RMAS") = blnAVC
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

	Protected Sub btnDBOk_Click(sender As Object, e As EventArgs) Handles btnDBOk.Click

		blnDB = True
		ViewState("RMAS") = blnDB

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

	Protected Sub calDORNSITF_SelectionChanged(sender As Object, e As EventArgs) Handles calDORNSITF.SelectionChanged
		Me.PopupControlExtender_calDORNSITF.Commit(Me.calDORNSITF.SelectedDate)
		Me.MPNSITF.Show()
	End Sub

	Protected Sub btnNSITFOk_Click(sender As Object, e As EventArgs) Handles btnNSITFOk.Click

		blnNSITF = True
		ViewState("RMAS") = blnNSITF

	End Sub

	Protected Sub txtAdhocDocDescription_TextChanged(sender As Object, e As EventArgs) Handles txtAdhocDocDescription.TextChanged



	End Sub

	Protected Sub ddInsuranceCoy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddInsuranceCoy.SelectedIndexChanged

		Me.MPRMASAnnuity.Show()

	End Sub

	Protected Sub btnRemoveAllDocument_Click(sender As Object, e As EventArgs) Handles btnRemoveAllDocument.Click

		Dim chk As Integer = 0, cr As New Core

		Try

			If gridRecievedDocument.Rows.Count > 0 Then

				dvDocumentError.Visible = False
				For Each grow As GridViewRow In Me.gridRecievedDocument.Rows

					dtDocuments = ViewState("RecievedDocument")

					Dim filePath As String = (Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + dtDocuments.Rows(grow.RowIndex).Item("DocumentPath").ToString))

					ViewState("RecievedDocument") = dtDocuments
					Session("documentPath") = Nothing

					If File.Exists(filePath) = True Then
						File.Delete(filePath)
					Else

					End If

				Next
				'dtDocuments.Rows(0).Delete()
				dtDocuments.Rows.Clear()

				If Directory.Exists("c:\NPM_Doc_Temp\" & Session("user")) = True Then

					Directory.Delete("c:\NPM_Doc_Temp\" & Session("user"), True)

				Else

				End If

				loadGrid(dtDocuments)

			Else
			End If

		Catch ex As Exception

		End Try


	End Sub

	'Protected Sub calCutOffDate_SelectionChanged(sender As Object, e As EventArgs) Handles calCutOffDate.SelectionChanged

	'	Me.PopupControlExtender_calCutOffDate.Commit(Me.calCutOffDate.SelectedDate)

	'End Sub

	Protected Sub chkOverrideAgeBarrier_CheckedChanged(sender As Object, e As EventArgs) Handles chkOverrideAgeBarrier.CheckedChanged
		Dim date1 As Date = Now
		Dim years As Long = DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), date1)

		If Me.chkOverrideAgeBarrier.Checked = True Then

			Me.ddRequiredDocuments.Enabled = True

		ElseIf chkOverrideAgeBarrier.Checked = False And years < 50 Then

			ddRequiredDocuments.Items.Clear()

		End If






	End Sub

	Protected Sub ddAnnRunningPW_TextChanged(sender As Object, e As EventArgs) Handles ddAnnRunningPW.TextChanged
		populateRuuningPW()
	End Sub
	Protected Sub populateRuuningPW()
		Dim dtDocument As New DataTable, dtDocuments As New DataTable, lstAppDocDetailOdd As New List(Of ApplicationDocumentDetail), cr As New Core, i As Integer
		Try

			If Me.ddAnnRunningPW.Text <> "" Then

				If Me.txtSector.Text = "Public" Then
					dtDocument = cr.PMgetAnnuityPWSubmittedDoc(Me.ddAnnRunningPW.Text, txtSector.Text)
				Else
					dtDocument = cr.PMgetAnnuityPWSubmittedDoc(Me.ddAnnRunningPW.Text, "Private")
				End If

				dtColumn = New DataColumn("DocumentName")
				dtDocuments.Columns.Add(dtColumn)

				dtColumn = New DataColumn("RecievedDate")
				dtDocuments.Columns.Add(dtColumn)

				dtColumn = New DataColumn("DocumentPath")
				dtDocuments.Columns.Add(dtColumn)

				dtColumn = New DataColumn("IsVerified")
				dtDocuments.Columns.Add(dtColumn)

				'dtDocuments = New DataTable
				Do While i < dtDocument.Rows.Count

					Dim lstAppDocDetail As New ApplicationDocumentDetail
					lstAppDocDetail.DocumentTypeID = dtDocument.Rows(i).Item("fkiDocumentTypeID")
					lstAppDocDetail.DocumentTypeName = dtDocument.Rows(i).Item("txtDocumentName")
					lstAppDocDetail.MemberApplicationID = dtDocument.Rows(i).Item("fkiMemberApplicationID")
					lstAppDocDetail.DocumentLocation = dtDocument.Rows(i).Item("txtDocumentPath").ToString
					lstAppDocDetail.IsVerified = dtDocument.Rows(i).Item("IsVerified").ToString

					lstAppDocDetailOdd.Add(lstAppDocDetail)

					Dim newCustomersRow As DataRow
					newCustomersRow = dtDocuments.NewRow()

					newCustomersRow("DocumentName") = dtDocument.Rows(i).Item("txtDocumentName")
					'newCustomersRow("RecievedDate") = dtDocument.Rows(i).Item("dteReceived").ToString.Substring(0, 10)
					newCustomersRow("RecievedDate") = CDate(dtDocument.Rows(i).Item("dteReceived")).Date

					Dim aryDocumentPath As Array = dtDocument.Rows(i).Item("txtDocumentPath").ToString.Split("\")


					If aryDocumentPath.Length > 1 Then
						Array.Reverse(aryDocumentPath)
						Dim sarrMyString As String() = aryDocumentPath(0).ToString.Split(New String() {"PEN"}, StringSplitOptions.None)
						If sarrMyString.Count = 1 Then
							sarrMyString = aryDocumentPath(0).ToString.Split(New String() {"DBA023"}, StringSplitOptions.None)
							newCustomersRow("DocumentPath") = "DBA023" + sarrMyString(1).ToString
						ElseIf sarrMyString.Count > 1 Then
							newCustomersRow("DocumentPath") = "PEN" + sarrMyString(1).ToString
						Else
							newCustomersRow("DocumentPath") = aryDocumentPath(0)
						End If
					Else
						newCustomersRow("DocumentPath") = ""
					End If

					'1= means the death benefit document is verified while 0 = means not verified
					If dtDocument.Rows(i).Item("IsVerified").ToString = "True" Then
						newCustomersRow("IsVerified") = 1
					Else
						newCustomersRow("IsVerified") = 0
					End If


					dtDocuments.Rows.Add(newCustomersRow)

					If dtDocument.Rows(i).Item("txtDocumentPath").ToString <> "" Then

						Dim strUploadPath As String, fileNewName As String
						fileNewName = dtDocument.Rows(i).Item("txtDocumentName").ToString.Replace(" | ", "_")

						fileNewName = fileNewName.Replace(" ", "_")
						fileNewName = fileNewName.Replace("|", "_")
						fileNewName = fileNewName.Replace(" ", "_")
						fileNewName = fileNewName.Replace("(", "_")
						fileNewName = fileNewName.Replace(")", "_")

						strUploadPath = Server.MapPath("~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName & System.IO.Path.GetExtension(dtDocument.Rows(i).Item("txtDocumentPath").ToString))

						Try
							File.Copy(dtDocument.Rows(i).Item("txtDocumentPath").ToString, strUploadPath, True)
						Catch ex As Exception

						End Try


					Else
					End If

					i = i + 1

				Loop

				'("" & dtDocuments.Rows.Count)

				If dtDocuments.Rows.Count > 0 Then

					ViewState("RecievedDocument") = dtDocuments
					ViewState("RecievedDocumentOLD") = dtDocument
					loadGrid(dtDocuments)

				Else

				End If




			Else

			End If

		Catch ex As Exception

			MsgBox("" & ex.Message)

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
