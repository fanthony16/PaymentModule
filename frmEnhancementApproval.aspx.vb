Imports System.Data

Partial Class frmEnhancementApproval
	Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable
	Dim blnHardShipApproval As Boolean = False
	Dim lstPINs As New ArrayList

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

	'getting all the types of benefit application types
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

	Protected Function getApplicationBatches(AppTypeID As Integer) As List(Of String)
		Dim lstBatches As New List(Of String)

		Dim dc As New AppDocumentsDataContext, cr As New Core

		Try

			Dim lstBatch As New List(Of String)
			Dim query = From m In dc.tblSPLogs Join n In dc.tblMemberApplications On m.txtBatchNo Equals n.txtSPBatchNo Join o In dc.tblApplicationTypes On o.pkiAppTypeId Equals n.fkiAppTypeId Where o.pkiAppTypeId = AppTypeID _
					  Select New With {m.txtBatchNo}
			' MsgBox("" & query.Count)
			For Each a In query

				If lstBatches.Contains(a.txtBatchNo) = False Then

					If cr.PMCheckSPBatchApplications(a.txtBatchNo) = True Then
						lstBatches.Add(a.txtBatchNo)
					Else
					End If


				Else

				End If


			Next

			Return lstBatches

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

		End Try

		Return Nothing

	End Function

	Private Sub getUnApprovedBatches()

		Try

			If gridRMAS.Columns.Count > 1 Then
				Response.Redirect("frmApplicationApprovals.aspx")
			Else
			End If

			ddApplicationBatchNumber.Items.Clear()
			Me.lstBatches.Items.Clear()


			Dim cr As New Core, dt As New DataTable, lstBatches As List(Of String), i As Integer
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

			'returning the list of batches created per approval types
			lstBatches = getApplicationBatches(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

			'populating the batches with on the UI
			Do While i < lstBatches.Count

				If ddApplicationBatchNumber.Items.Count = 0 Then
					ddApplicationBatchNumber.Items.Add("")
					ddApplicationBatchNumber.Items.Add(lstBatches.Item(i))
				ElseIf ddApplicationBatchNumber.Items.Count > 0 Then
					ddApplicationBatchNumber.Items.Add(lstBatches.Item(i))
				End If
				i = i + 1

			Loop

		Catch ex As Exception
			' MsgBox("" & ex.Message)
		End Try

	End Sub


	Protected Sub gridRMAS_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("Applications")) = False Then

			Dim dt As DataTable = ViewState("Applications")
			If e.Row.RowType = DataControlRowType.DataRow Then

				If (dt.Rows(e.Row.RowIndex).Item("PencomBatch")).ToString = "" Then

					'e.Row.ForeColor = System.Drawing.Color.Red

				ElseIf (dt.Rows(e.Row.RowIndex).Item("PencomBatch")).ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DateApprovalConfirmed")) = "" Then

					e.Row.ForeColor = System.Drawing.Color.Red
					e.Row.Enabled = False

				ElseIf (dt.Rows(e.Row.RowIndex).Item("PencomBatch")).ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DateApprovalConfirmed")) <> "" Then

					e.Row.ForeColor = System.Drawing.Color.Green
					e.Row.Enabled = False

				End If

			End If
		Else
		End If




	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Try


			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then
					Response.Redirect("Login.aspx")
				Else
					getApprovalTypes()
					'getUserAccessMenu(Session("user"))
				End If

			ElseIf IsNothing(ViewState("ApplicationList")) = False Then

				'Dim dt As New DataTable
				'dt = ViewState("ApplicationList")
				'BindGrid(dt)
				getUserAccessMenu(Session("user"))

			End If



		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub ddApplicationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddApplicationType.SelectedIndexChanged

		getUnApprovedBatches()

	End Sub

	Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
		addBatch(ddApplicationBatchNumber.SelectedItem.Text)
		ddApplicationBatchNumber.Text = ""
	End Sub

	Private Sub addBatch(ByVal item As String)
		Dim items As ListItem
		items = New ListItem(RTrim(LTrim(item)))

		If Me.lstBatches.Items.Contains(items) Then

		Else

			Me.lstBatches.Items.Add(item)

		End If

	End Sub

	Protected Sub BindFieldEnhanced()

		Dim bfieldAppCode As New BoundField()
		bfieldAppCode.HeaderText = "Application Code"
		'bfieldAppCode.DataField = "txtApplicationCode"
		bfieldAppCode.DataField = "ApplicationCode"
		gridRMAS.Columns.Add(bfieldAppCode)

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		'bfieldPIN.DataField = "txtPIN"
		bfieldPIN.DataField = "PIN"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldName As New BoundField()

		bfieldName.HeaderText = "Name"
		'bfieldName.DataField = "txtFullName"
		bfieldName.DataField = "Name"
		bfieldName.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldName)

		'Dim bfieldRDate As New BoundField()
		'bfieldRDate.HeaderText = "Retirement Date"
		'bfieldRDate.DataField = "RetirementDate"  'RetirementDate
		'bfieldRDate.DataFormatString = "{0:d}"
		'gridRMAS.Columns.Add(bfieldRDate)

		Dim bfieldAmount As New BoundField()
		bfieldAmount.HeaderText = "Approved Amount"
		'bfieldAmount.DataField = "Amount"
		bfieldAmount.DataField = "ApprovedAmount"
		bfieldAmount.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAmount)

		Dim bfieldAccName As New BoundField()
		bfieldAccName.HeaderText = "Account Name"
		'bfieldAccName.DataField = "txtAccountName"
		bfieldAccName.DataField = "AccountName"
		gridRMAS.Columns.Add(bfieldAccName)

		Dim bfieldAccNo As New BoundField()
		bfieldAccNo.HeaderText = "AccountNo"
		'bfieldAccNo.DataField = "txtAccountNo"
		bfieldAccNo.DataField = "AccountNo"
		gridRMAS.Columns.Add(bfieldAccNo)

		Dim bfieldBankName As New BoundField()
		bfieldBankName.HeaderText = "Bank Name"
		'bfieldBankName.DataField = "fkiBankID"
		bfieldBankName.DataField = "BankName"
		gridRMAS.Columns.Add(bfieldBankName)

		Dim bfieldBranchName As New BoundField()
		bfieldBranchName.HeaderText = "Branch Name"
		bfieldBranchName.DataField = "BankBranch"
		'bfieldBranchName.DataField = "fkiBranchID"
		gridRMAS.Columns.Add(bfieldBranchName)


	End Sub

	Protected Sub btnLoadPIN_Click(sender As Object, e As EventArgs) Handles btnLoadPIN.Click

		Dim i As Integer = 0, str As String = "", cr As New Core
		Try

			If lstApprovalPIN.Items.Count > 0 Then
				Exit Sub
			Else
			End If

			Me.txtAcknowledgmentDate.Text = ""
			Me.txtApprovedDate.Text = ""
			Me.txtBatchRef.Text = ""
			Me.txtTotalApprovedAmount.Text = ""

			If IsNothing(ViewState("ApprovalTypeCollection")) = False Then

				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
				If Me.gridRMAS.Columns.Count = 1 Then

					Select Case CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))


						Case Is = 17
							BindFieldEnhanced()

						Case Else

					End Select
				Else
				End If


				Do While i < Me.lstBatches.Items.Count

					If i = 0 Then
						str = str & "'" & lstBatches.Items(i).ToString & "'"
					Else
						str = str & ",'" & lstBatches.Items(i).ToString & "'"
					End If

					i = i + 1

					If i = Me.lstBatches.Items.Count Then
						str = "(" & str & ")"
					End If

				Loop

				ViewState("str") = str
				Dim dt As New DataTable, j As Integer = 0, lstApprovalPeople As New List(Of PencomApprovalPeople)
				Dim AppType As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))
				dt = cr.PMgetPaymentData(str, CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), Now.Date)


				Do While j < dt.Rows.Count
					Dim lstApprovalPerson As New PencomApprovalPeople

					lstApprovalPerson.ApplicationCode = dt.Rows(j).Item("txtApplicationCode").ToString
					lstApprovalPerson.PIN = dt.Rows(j).Item("txtPIN").ToString
					lstApprovalPerson.Name = dt.Rows(j).Item("txtFullName").ToString.Replace("|", "")

					If AppType = 17 Then

						lstApprovalPerson.ApprovedAmount = dt.Rows(j).Item("ApprovedAmount")

					End If

					'lstApprovalPerson.ApprovedAmount = dt.Rows(j).Item("ApprovedAmount")
					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.AccountName = dt.Rows(j).Item("txtAccountName")
					lstApprovalPerson.AccountNo = dt.Rows(j).Item("txtAccountNo")
					lstApprovalPerson.BankName = dt.Rows(j).Item("fkiBankID")
					lstApprovalPerson.BankBranch = dt.Rows(j).Item("fkiBranchID")
					lstApprovalPerson.PencomBatch = dt.Rows(j).Item("txtPencomBatch").ToString
					lstApprovalPerson.DateApprovalConfirmed = dt.Rows(j).Item("dteApprovalConfirmed").ToString



					lstApprovalPeople.Add(lstApprovalPerson)
					
					j = j + 1
				Loop


				ViewState("ApprovedPeople") = dt
				'ViewState("ApprovedPeople") = lstApprovalPeople
				Dim l As Integer = 0
				Do While l < lstApprovalPeople.Count
					lstApprovalPIN.Items.Add(lstApprovalPeople(l).PIN)
					l = l + 1
				Loop

				BindGrid(lstApprovalPeople, AppType)

			Else

			End If




		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try


	End Sub


	Protected Sub BindGrid(dt As List(Of PencomApprovalPeople), AppType As Integer)
		'Protected Sub BindGrid(dt As DataTable)
		Dim dtApproval As New DataTable, dtColumn As New DataColumn, i As Integer

		Try
			If IsNothing(ViewState("Applications")) = True Then

				dtColumn = New DataColumn("ApplicationCode")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("PIN")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("Name")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("ValueDate")
				dtApproval.Columns.Add(dtColumn)


				dtColumn = New DataColumn("ApprovedAmount")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("AccountNo")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("AccountName")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("BankBranch")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("BankName")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("PencomBatch")
				dtApproval.Columns.Add(dtColumn)

				dtColumn = New DataColumn("DateApprovalConfirmed")
				dtApproval.Columns.Add(dtColumn)
			Else

				dtApproval = ViewState("Applications")

			End If

			Do While i < dt.Count

				Dim newCustomersRow As DataRow
				newCustomersRow = dtApproval.NewRow()

				newCustomersRow("ApplicationCode") = dt(i).ApplicationCode
				newCustomersRow("PIN") = dt(i).PIN
				newCustomersRow("Name") = dt(i).Name
				newCustomersRow("ValueDate") = dt(i).ValueDate
				newCustomersRow("ApprovedAmount") = dt(i).ApprovedAmount
				newCustomersRow("AccountNo") = dt(i).AccountNo
				newCustomersRow("AccountName") = dt(i).AccountName
				newCustomersRow("BankBranch") = dt(i).BankBranch
				newCustomersRow("BankName") = dt(i).BankName
				newCustomersRow("PencomBatch") = dt(i).PencomBatch
				newCustomersRow("DateApprovalConfirmed") = dt(i).DateApprovalConfirmed

				dtApproval.Rows.Add(newCustomersRow)

				i = i + 1

			Loop

			ViewState("Applications") = dtApproval
			ViewState("AppType") = AppType


			Me.gridRMAS.DataSource = dtApproval
			Me.gridRMAS.DataBind()

			If dtApproval.Rows.Count > 5 Then
				pnlGrid.Height = Nothing
			Else
			End If

		Catch ex As Exception

			Dim loger As New Global.Logger.Logger
			loger.FileSource = "Payment Module "
			loger.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			loger.Logger(ex.Message & " | " & "Location => PaymentModule_GetApplicationBatch()")
			'AppDomain.CurrentDomain.BaseDirectory & "\Logs"

		End Try


	End Sub

	Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

		For Each grow As GridViewRow In Me.gridRMAS.Rows
			If grow.Enabled = True Then
				Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkRMASApproval"), CheckBox)
				cb.Checked = True
			Else
			End If
		Next

	End Sub

	Protected Sub gridRMAS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridRMAS.SelectedIndexChanged

		

	End Sub

	Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

		For Each grow As GridViewRow In Me.gridRMAS.Rows

			If grow.Enabled = True Then
				Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkRMASApproval"), CheckBox)
				cb.Checked = False
			Else
			End If

		Next

	End Sub

	Protected Sub btnApprovalSave_Click(sender As Object, e As EventArgs) Handles btnApprovalSave.Click

		Try
			'Session("user") = "o-taiwo"

			Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail), chkStatus As Integer

			If IsNothing(Session("user")) = True Then

				Response.Redirect("login.aspx")

			Else

			End If

			For Each grow As GridViewRow In Me.gridRMAS.Rows

				cb = grow.FindControl("ChkRMASApproval")

				If cb.Checked = True Then

					chkStatus = chkStatus + 1

				ElseIf cb.Checked = False Then

				End If

			Next

			If chkStatus > 0 Then

				Dim typeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)

				If IsNothing(ViewState("ApprovalDetails")) = True And typeID = 17 Then

					Me.MPApprovalHardShip.Show()

				ElseIf IsNothing(ViewState("ApprovalDetails")) = False Then

					postApprovalEntries(typeID, Session("user"))

				End If

			Else

			End If

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)

		End Try

	End Sub

	Protected Sub calAcknowledgmentDate_SelectionChanged(sender As Object, e As EventArgs) Handles calAcknowledgmentDate.SelectionChanged

		Me.calAcknowledgmentDate_PopupControlExtender.Commit(Me.calAcknowledgmentDate.SelectedDate)
		Me.MPApprovalHardShip.Show()

	End Sub

	Protected Sub calApprovedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calApprovedDate.SelectionChanged

		Me.calApprovedDate_PopupControlExtender.Commit(Me.calApprovedDate.SelectedDate)
		Me.MPApprovalHardShip.Show()

	End Sub

	Private Sub postApprovalEntries(TypeID As Integer, UName As String)

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail)

		Try

			For Each grow As GridViewRow In Me.gridRMAS.Rows
				'MsgBox("" & grow.RowIndex)

				cb = grow.FindControl("ChkRMASApproval")

				If cb.Checked = True And TypeID = 17 Then

					Dim lstApprovedApp As New ApplicationDetail
					lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
					lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
					lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
					lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
					lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(4).Text.ToString())
					lstApprovedApp.NumberOfArrears = Me.txtNoArears.Text
					lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)

					'txtEditApprovedAmount

				ElseIf cb.Checked = False Then

				End If

			Next

				ApprovalDetails.AcknowledgmentDate = CDate(Me.txtAcknowledgmentDate.Text)
				ApprovalDetails.ApprovalDate = CDate(Me.txtApprovedDate.Text)
				ApprovalDetails.TotalApprovalAmount = CDbl(Me.txtTotalApprovedAmount.Text)
				ApprovalDetails.PencomBatch = Me.txtBatchRef.Text
				ApprovalDetails.AppType = TypeID
				ApprovalDetails.CreatedBy = UName


			If cr.PMIsPencomApprovalExisting(Me.txtBatchRef.Text, Server.MapPath("~/Logs")) = True Then


			Else

				If IsNothing(ViewState("ExistingBatch")) = True Then

					cr.PMInsertPencomApproval(ApprovalDetails, lstApprovedApps, 0, 1, Server.MapPath("~/Logs"))
					ViewState("ExistingBatch") = Me.txtBatchRef.Text

				ElseIf IsNothing(ViewState("ExistingBatch")) = False Then

					cr.PMInsertPencomApproval(ApprovalDetails, lstApprovedApps, 1, 1, Server.MapPath("~/Logs"))

				Else

				End If

			End If


			Dim dtApproval As New DataTable
			dtApproval = ViewState("Applications")
			For Each grow As GridViewRow In Me.gridRMAS.Rows
				dtApproval.Rows(0).Delete()
			Next

			Me.gridRMAS.DataSource = dtApproval
			Me.gridRMAS.DataBind()


			lstPINs = ViewState("lstPINs")
			Dim k As Integer

			Do While k < lstPINs.Count

				LoadApplicationGrid(lstPINs.Item(k), "R")
				k = k + 1

			Loop


		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)

		End Try

	End Sub

	Private Sub LoadApplicationGrid(PIN As String, loadType As Char)

		Dim lstApprovalPeople As New List(Of PencomApprovalPeople)

		Dim dtApprovedPINs As New DataTable, dtColumn As New DataColumn
		Try

			''''concerting datatable row to a list of approvedPINs
			Dim dt As New DataTable, j As Integer, AppType As Integer, lstApprovalPerson As New PencomApprovalPeople, cr As New Core, Str As String

			If loadType = "R" And IsNothing(ViewState("str")) = False Then

				Str = ViewState("str")
				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
				dt = cr.PMgetPaymentData(Str, CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), Now.Date)

				'  Dim pAmount As Double
				'  pAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)

			ElseIf loadType = "N" Then

				dt = ViewState("ApprovedPeople")

			End If

			Do While j < dt.Rows.Count

				ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
				AppType = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))
				lstApprovalPerson = New PencomApprovalPeople
				lstApprovalPerson.ApplicationCode = dt.Rows(j).Item("txtApplicationCode").ToString
				lstApprovalPerson.PIN = dt.Rows(j).Item("txtPIN").ToString
				lstApprovalPerson.Name = dt.Rows(j).Item("txtFullName").ToString.Replace("|", "")

				lstApprovalPerson.ApprovedAmount = dt.Rows(j).Item("ApprovedAmount")

				lstApprovalPerson.AccountName = dt.Rows(j).Item("txtAccountName")
				lstApprovalPerson.AccountNo = dt.Rows(j).Item("txtAccountNo")
				lstApprovalPerson.BankName = dt.Rows(j).Item("fkiBankID")
				lstApprovalPerson.BankBranch = dt.Rows(j).Item("fkiBranchID")
				lstApprovalPerson.PencomBatch = dt.Rows(j).Item("txtPencomBatch").ToString
				lstApprovalPerson.DateApprovalConfirmed = dt.Rows(j).Item("dteApprovalConfirmed").ToString

				lstApprovalPeople.Add(lstApprovalPerson)

				j = j + 1
			Loop
			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

			Dim newApprovalList As New List(Of PencomApprovalPeople), lstApprovalPersonSorted As New PencomApprovalPeople
			Dim newCustomersRow As DataRow
			newCustomersRow = dtApprovedPINs.NewRow()


			''''''selecting from the batch of PINs for the selected PIN on the User interface
			Dim query = From n In lstApprovalPeople
					   Where n.PIN = PIN

			For Each n As PencomApprovalPeople In query


				lstApprovalPersonSorted.ApplicationCode = n.ApplicationCode
				lstApprovalPersonSorted.PIN = n.PIN
				lstApprovalPersonSorted.Name = n.Name
				lstApprovalPersonSorted.Disengagement = n.Disengagement
				lstApprovalPersonSorted.RetirementDate = n.RetirementDate
				lstApprovalPersonSorted.MonthlyDrawndown = n.MonthlyDrawndown
				lstApprovalPersonSorted.Arears = n.Arears
				lstApprovalPersonSorted.LumpSum = n.LumpSum
				lstApprovalPersonSorted.InsuranceCompanyName = n.InsuranceCompanyName
				lstApprovalPersonSorted.MonthlyAnnuity = n.MonthlyAnnuity
				lstApprovalPersonSorted.ApprovedAmount = n.ApprovedAmount
				lstApprovalPersonSorted.ValueDate = n.ValueDate
				lstApprovalPersonSorted.AccountName = n.AccountName
				lstApprovalPersonSorted.AccountNo = n.AccountNo
				lstApprovalPersonSorted.BankName = n.BankName
				lstApprovalPersonSorted.BankBranch = n.BankBranch
				lstApprovalPersonSorted.PencomBatch = n.PencomBatch
				lstApprovalPersonSorted.DateApprovalConfirmed = n.DateApprovalConfirmed

				lstApprovalPersonSorted.LumpSumToPay = n.LumpSumToPay
				lstApprovalPersonSorted.MonthlyDrawndownToPay = n.MonthlyDrawndownToPay
				lstApprovalPersonSorted.ArearsToPay = n.ArearsToPay
				lstApprovalPersonSorted.AnnuityToPay = n.AnnuityToPay

				lstApprovalPersonSorted.AmountToPay = n.AmountToPay
				lstApprovalPersonSorted.ApprovedAmount = n.ApprovedAmount
				lstApprovalPersonSorted.InterestAmount = n.InterestAmount
				newApprovalList.Add(lstApprovalPersonSorted)


			Next
			'adding the selected pin in order of selection to the grid for further processing
			BindGrid(newApprovalList, AppType)

			'removing the added PIN from the list to avoid duplicates
			Me.lstApprovalPIN.Items.Remove(PIN)

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)

		End Try

	End Sub

	Protected Sub btnHardApprovalOK_Click(sender As Object, e As EventArgs) Handles btnHardApprovalOK.Click

		blnHardShipApproval = True
		ViewState("ApprovalDetails") = blnHardShipApproval

	End Sub

	Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click


		Try

			Me.lstBatches.Items.RemoveAt(Me.lstBatches.SelectedIndex)

		Catch ex As Exception

			Dim loger As New Global.Logger.Logger
			'getting the application path to keet the dialy log file
			loger.FileSource = "Payment Module "
			loger.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			loger.Logger(ex.Message & " | " & "Location => PaymentModule_Approval()")

		End Try

	End Sub
End Class
