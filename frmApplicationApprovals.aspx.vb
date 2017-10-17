Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Partial Class frmApplicationApprovals
    Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable
     Dim blnHardShipApproval As Boolean = False
     Dim blnPWApproval As Boolean = False
     Dim blnAnnApproval As Boolean = False
     Dim blnEnblocApproval As Boolean = False


     Dim lstPINs As New ArrayList
     Protected Sub EditApprovedAmount()
          MsgBox("Click Me")
     End Sub
     Protected Sub gridRMAS_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)

          If IsNothing(ViewState("Applications")) = False Then

               Dim dt As DataTable = ViewState("Applications")
               If e.Row.RowType = DataControlRowType.DataRow Then

                    'Dim txtAmount As New TextBox()
                    'txtAmount.ID = "txtAmount"
                    'txtAmount.Width = 120
                    'txtAmount.Text = TryCast(e.Row.DataItem, DataRowView).Row("Amount").ToString()
                    'e.Row.Cells(5).Controls.Add(txtAmount)

                    'Dim cmd As New Button()
                    'cmd.ID = "cmd"
                    'cmd.Text = "Edit Amount"
                    'AddHandler cmd.Click, AddressOf Me.EditApprovedAmount
                    'e.Row.Cells(11).Controls.Add(cmd)
                    'container.Controls.Add(cmd)



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
     'Public Function getApprovalType() As List(Of String)

     '     Dim lstAppTypes As New List(Of String)
     '     Dim dc As New AppDocumentsDataContext
     '     Dim query = From m In dc.tblApplicationTypes
     '                 Select m

     '     For Each a As tblApplicationType In query
     '          lstAppTypes.Add(a.txtDescription)
     '          ApprovalTypeCollection.Add(a.txtDescription, a.pkiAppTypeId)
     '     Next
     '     ViewState("ApprovalTypeCollection") = ApprovalTypeCollection
     '     Return lstAppTypes

     'End Function
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

     Protected Sub btnUpdateApprovalDetails_Click(sender As Object, e As EventArgs) Handles btnUpdateApprovalDetails.Click
          'getting the ID of the user selected application type

          Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail)

          For Each grow As GridViewRow In Me.gridRMAS.Rows

               cb = grow.FindControl("ChkRMASApproval")

               If cb.Checked = True Then
                    chk = chk + 1

               ElseIf cb.Checked = False Then
               End If

          Next



          'displays the required approval details to be saved based on the type of approval selected

          If chk > 0 Then

               Select Case apptypeID

                    Case 1
					Me.MPApprovalHardShip.Show()
				Case 16
					Me.MPApprovalHardShip.Show()
				Case 11
					Me.MPApprovalHardShip.Show()
                    Case 2
                         Me.MPApprovalHardShip.Show()
                    Case 3
                         Me.mpApprovalPW.Show()
                    Case 4
                         Me.mpApprovalAnn.Show()
                    Case 7
                         Me.MPApprovalHardShip.Show()
                    Case 8
                         Me.MPApprovalHardShip.Show()
                    Case 5
                         Me.MPApprovalHardShip.Show()
                    Case 6
                         Me.MPApprovalHardShip.Show()
                    Case Else

               End Select

          Else
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
     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          Try

          
               If IsPostBack = False Then

                    If IsNothing(Session("user")) = True Then
                         Response.Redirect("Login.aspx")
                    Else
                         getApprovalTypes()
                         getUserAccessMenu(Session("user"))
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

     Protected Sub calAcknowledgmentDate_SelectionChanged(sender As Object, e As EventArgs) Handles calAcknowledgmentDate.SelectionChanged

          Me.calAcknowledgmentDate_PopupControlExtender.Commit(Me.calAcknowledgmentDate.SelectedDate)
          Me.MPApprovalHardShip.Show()

     End Sub

     Protected Sub calApprovedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calApprovedDate.SelectionChanged
          Me.calApprovedDate_PopupControlExtender.Commit(Me.calApprovedDate.SelectedDate)
          Me.MPApprovalHardShip.Show()
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

	Protected Sub BindFieldEmployee()

		Dim bfieldAppCode As New BoundField()
		bfieldAppCode.HeaderText = "Application Code"
		bfieldAppCode.DataField = "ApplicationCode"
		gridRMAS.Columns.Add(bfieldAppCode)

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "PIN"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldName As New BoundField()

		bfieldName.HeaderText = "Name"
		bfieldName.DataField = "Name"
		bfieldName.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldName)

		Dim bfieldRDate As New BoundField()
		bfieldRDate.HeaderText = "Retirement Date"
		bfieldRDate.DataField = "RetirementDate"
		bfieldRDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRDate)

		Dim bfieldAmount As New BoundField()
		bfieldAmount.HeaderText = "Approved Amount"
		'bfieldAmount.DataField = "Amount"
		bfieldAmount.DataField = "ApprovedAmount"
		bfieldAmount.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAmount)

		Dim bfieldAmountToPay As New BoundField()
		bfieldAmountToPay.HeaderText = "Amount ToPay"
		'bfieldAmount.DataField = "Amount"
		bfieldAmountToPay.DataField = "AmountToPay"
		bfieldAmountToPay.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAmountToPay)

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

     Protected Sub BindFieldNSITF()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "ApplicationCode"
          gridRMAS.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "PIN"
          gridRMAS.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()

          bfieldName.HeaderText = "Name"
          bfieldName.DataField = "Name"
          bfieldName.ItemStyle.Width = 150
          gridRMAS.Columns.Add(bfieldName)

          Dim bfieldRDate As New BoundField()
          bfieldRDate.HeaderText = "Retirement Date"
          bfieldRDate.DataField = "RetirementDate"
          bfieldRDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldAmount As New BoundField()
          bfieldAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldAmount.DataField = "ApprovedAmount"
          bfieldAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmountToPay)

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


     Protected Sub BindFieldDBA()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "ApplicationCode"
          gridRMAS.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "PIN"
          gridRMAS.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()

          bfieldName.HeaderText = "Name"
          bfieldName.DataField = "Name"
          bfieldName.ItemStyle.Width = 150
          gridRMAS.Columns.Add(bfieldName)

          Dim bfieldRDate As New BoundField()
          bfieldRDate.HeaderText = "Retirement Date"
          bfieldRDate.DataField = "RetirementDate"
          bfieldRDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldAmount As New BoundField()
          bfieldAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldAmount.DataField = "ApprovedAmount"
          bfieldAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmountToPay)

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


     Protected Sub BindFieldAnnuity()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "ApplicationCode"
          gridRMAS.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "PIN"
          gridRMAS.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()

          bfieldName.HeaderText = "Name"
          bfieldName.DataField = "Name"
          bfieldName.ItemStyle.Width = 150
          gridRMAS.Columns.Add(bfieldName)

          Dim bfieldRDate As New BoundField()
          bfieldRDate.HeaderText = "Retirement Date"
          bfieldRDate.DataField = "RetirementDate"
          bfieldRDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldAmount As New BoundField()
          bfieldAmount.HeaderText = "LumpSum"
          'bfieldAmount.DataField = "Amount"
          bfieldAmount.DataField = "LumpSum"
          bfieldAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmount)

          Dim bfieldLumpSumToPay As New BoundField()
          bfieldLumpSumToPay.HeaderText = "LumpSum ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldLumpSumToPay.DataField = "LumpSumToPay"
          bfieldLumpSumToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldLumpSumToPay)

          Dim bfieldMonthlyPremium As New BoundField()
          bfieldMonthlyPremium.HeaderText = "Annuity Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldMonthlyPremium.DataField = "monthly-annuity"
          bfieldMonthlyPremium.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldMonthlyPremium)


          Dim bfieldMonthlyAnnuityToPay As New BoundField()
          bfieldMonthlyAnnuityToPay.HeaderText = "Annuity ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldMonthlyAnnuityToPay.DataField = "AnnuityToPay"
          bfieldMonthlyAnnuityToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldMonthlyAnnuityToPay)


          Dim bfieldInsuranceCoy As New BoundField()
          bfieldInsuranceCoy.HeaderText = "Insurance Company"
          bfieldInsuranceCoy.DataField = "insurance-company-name"
          gridRMAS.Columns.Add(bfieldInsuranceCoy)


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


     Protected Sub BindFieldPW()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "ApplicationCode"
          gridRMAS.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "PIN"
          gridRMAS.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()

          bfieldName.HeaderText = "Name"
          bfieldName.DataField = "Name"
          bfieldName.ItemStyle.Width = 150
          gridRMAS.Columns.Add(bfieldName)

          Dim bfieldRDate As New BoundField()
          bfieldRDate.HeaderText = "Retirement Date"
          bfieldRDate.DataField = "RetirementDate"
          bfieldRDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldLumpSum As New BoundField()
          bfieldLumpSum.HeaderText = "Lump Sum"
          bfieldLumpSum.DataField = "LumpSum"
          bfieldLumpSum.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldLumpSum)

          Dim bfieldLumpSumToPay As New BoundField()
          bfieldLumpSumToPay.HeaderText = "LumpSum ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldLumpSumToPay.DataField = "LumpSumToPay"
          bfieldLumpSumToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldLumpSumToPay)


          Dim bfieldMonthlyDrawndown As New BoundField()
          bfieldMonthlyDrawndown.HeaderText = "Pension"
          bfieldMonthlyDrawndown.DataField = "monthly-programed-drawndown"
          bfieldMonthlyDrawndown.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldMonthlyDrawndown)


          Dim bfieldMonthlyDrawndownToPay As New BoundField()
          bfieldMonthlyDrawndownToPay.HeaderText = "Pension ToPay"
          bfieldMonthlyDrawndownToPay.DataField = "MonthlyDrawndownToPay"
          bfieldMonthlyDrawndownToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldMonthlyDrawndownToPay)


          Dim bfieldArrears As New BoundField()
          bfieldArrears.HeaderText = "Arrears"
          bfieldArrears.DataField = "Arrears"
          bfieldArrears.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldArrears)


          Dim bfieldArrearsToPay As New BoundField()
          bfieldArrearsToPay.HeaderText = "Arrears ToPay"
          bfieldArrearsToPay.DataField = "ArearsToPay"
          bfieldArrearsToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldArrearsToPay)


          Dim bfieldAccName As New BoundField()
          bfieldAccName.HeaderText = "Account Name"
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


     Protected Sub BindFieldAVC()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "ApplicationCode"
          gridRMAS.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "PIN"
          gridRMAS.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()

          bfieldName.HeaderText = "Name"
          bfieldName.DataField = "Name"
          bfieldName.ItemStyle.Width = 150
          gridRMAS.Columns.Add(bfieldName)

		Dim bfieldRDate As New BoundField()
		bfieldRDate.HeaderText = "Transaction Date"
		bfieldRDate.DataField = "RetirementDate"
		bfieldRDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldAmount As New BoundField()
          bfieldAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldAmount.DataField = "ApprovedAmount"
          bfieldAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAmountToPay)

		Dim bfieldAccruedInterest As New BoundField()
		bfieldAccruedInterest.HeaderText = "Interest Amount"
		'bfieldAmount.DataField = "Amount"
		bfieldAccruedInterest.DataField = "InterestAmount"
		bfieldAccruedInterest.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAccruedInterest)

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


     Protected Sub BindFieldEnloc()

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

          Dim bfieldRDate As New BoundField()
          bfieldRDate.HeaderText = "Retirement Date"
          bfieldRDate.DataField = "RetirementDate"  'RetirementDate
          bfieldRDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldAmount As New BoundField()
          bfieldAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldAmount.DataField = "ApprovedAmount"
          bfieldAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmountToPay)

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


     Protected Sub BindFieldLegacy()

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

          Dim bfieldRDate As New BoundField()
          bfieldRDate.HeaderText = "Retirement Date"
          bfieldRDate.DataField = "RetirementDate"  'RetirementDate
          bfieldRDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldRDate)

          Dim bfieldAmount As New BoundField()
          bfieldAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldAmount.DataField = "ApprovedAmount"
          bfieldAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmountToPay)

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


     Protected Sub BindFieldHardShip()

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

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "ValueDate"
          'bfieldDDate.DataField = "dteDisengagement"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldValueDate)

          Dim bfieldDDate As New BoundField()
          bfieldDDate.HeaderText = "Disengagement Date"
          'bfieldDDate.DataField = "dteDisengagement"
          bfieldDDate.DataField = "Disengagement"
          bfieldDDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldDDate)

          Dim bfieldApprovedAmount As New BoundField()
          bfieldApprovedAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          bfieldApprovedAmount.DataField = "ApprovedAmount"
          bfieldApprovedAmount.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldApprovedAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          'bfieldAmount.DataField = "Amount"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridRMAS.Columns.Add(bfieldAmountToPay)


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

     Private Sub getUnApprovedBatches()

          Try

               If gridRMAS.Columns.Count > 1 Then
                    Response.Redirect("frmApplicationApprovals.aspx")
               Else
               End If

               ddApplicationBatchNumber.Items.Clear()
               Me.lstBatches.Items.Clear()

               '  gridRMAS.Columns.Clear()

               'Dim rCount As Integer = gridRMAS.Columns.Count
               'Do While rCount > 1
               '     gridRMAS.Columns.RemoveAt(gridRMAS.Columns.Count - 1)
               '     rCount = gridRMAS.Columns.Count
               'Loop

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

     

     Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

          addBatch(ddApplicationBatchNumber.SelectedItem.Text)
          ddApplicationBatchNumber.Text = ""

     End Sub

     Private Sub addBatch(ByVal item As String)
          Dim items As ListItem
          items = New ListItem(RTrim(LTrim(item)))

          If Me.lstBatches.Items.Contains(items) Then

               ' If lstSchedule.Items.FindByText(item).Value.Count > 0 Then

          Else

               Me.lstBatches.Items.Add(item)

          End If

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

						Case Is = 1
							BindFieldEnloc()

						Case Is = 16
							BindFieldEnloc()

						Case Is = 2
							BindFieldHardShip()

						Case Is = 8
							BindFieldLegacy()

						Case Is = 7
							BindFieldAVC()

						Case Is = 3
							BindFieldPW()

						Case Is = 14
							BindFieldPW()

						Case Is = 4
							BindFieldAnnuity()

						Case Is = 15
							BindFieldAnnuity()

						Case Is = 5
							BindFieldDBA()

						Case Is = 6
							BindFieldNSITF()

						Case Is = 11
							BindFieldEmployee()

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
					If AppType = 2 Then

						lstApprovalPerson.Disengagement = dt.Rows(j).Item("dteDisengagement")

					ElseIf AppType = 8 Then
						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")

					ElseIf AppType = 1 Then
						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")

					ElseIf AppType = 7 Then
						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")

					ElseIf AppType = 3 Then

						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
						lstApprovalPerson.MonthlyDrawndown = dt.Rows(j).Item("MonthPension")
						lstApprovalPerson.Arears = CInt(dt.Rows(j).Item("ArearsMonths")) * lstApprovalPerson.MonthlyDrawndown
						lstApprovalPerson.LumpSum = dt.Rows(j).Item("LumpSum")
						lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
						lstApprovalPerson.MonthlyDrawndownToPay = dt.Rows(j).Item("PayingPension")
						lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingArrears")

						Dim arDate As Date, dor As Date, standMonth As Integer, intAge As Integer
						arDate = dt.Rows(j).Item("dteDOR")
						dor = dt.Rows(j).Item("dteDOR")
						intAge = DateDiff(DateInterval.Year, dt.Rows(j).Item("dteDOB"), Now.Date)

						Select Case Day(arDate)

							Case Day(arDate) > 10 And intAge > 50

								dor = DateAdd(DateInterval.Month, 1, dor)
								standMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * standMonth

							Case Day(arDate) <= 10 And intAge > 50

								standMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * standMonth

							Case Else

								If intAge < 50 Then

									dor = DateAdd(DateInterval.Year, 50, dt.Rows(j).Item("dteDOB"))
									standMonth = DateDiff(DateInterval.Month, dor, Now.Date)
									lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * standMonth

								Else

								End If

						End Select



					ElseIf AppType = 14 Then

						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
						lstApprovalPerson.MonthlyDrawndown = dt.Rows(j).Item("MonthPension")
						lstApprovalPerson.Arears = CInt(dt.Rows(j).Item("ArearsMonths")) * lstApprovalPerson.MonthlyDrawndown
						lstApprovalPerson.LumpSum = dt.Rows(j).Item("LumpSum")
						lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
						lstApprovalPerson.MonthlyDrawndownToPay = dt.Rows(j).Item("PayingPension")
						lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingArrears")

						Dim arDate As Date, dor As Date, standMonth As Integer, intAge As Integer
						arDate = dt.Rows(j).Item("dteDOR")
						dor = dt.Rows(j).Item("dteDOR")
						intAge = DateDiff(DateInterval.Year, dt.Rows(j).Item("dteDOB"), Now.Date)

						Select Case Day(arDate)

							Case Day(arDate) > 10 And intAge > 50

								dor = DateAdd(DateInterval.Month, 1, dor)
								standMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * standMonth

							Case Day(arDate) <= 10 And intAge > 50

								standMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * standMonth

							Case Else

								If intAge < 50 Then

									dor = DateAdd(DateInterval.Year, 50, dt.Rows(j).Item("dteDOB"))
									standMonth = DateDiff(DateInterval.Month, dor, Now.Date)
									lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * standMonth

								Else

								End If

						End Select



					ElseIf AppType = 4 Then

						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
						lstApprovalPerson.InsuranceCompanyName = dt.Rows(j).Item("insurance-company-name").ToString
						lstApprovalPerson.MonthlyAnnuity = dt.Rows(j).Item("monthly-annuity")

					ElseIf AppType = 5 Then
						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")

					ElseIf AppType = 6 Then
						lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")


					End If

					lstApprovalPerson.ApprovedAmount = dt.Rows(j).Item("ApprovedAmount")
					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.AccountName = dt.Rows(j).Item("txtAccountName")
					lstApprovalPerson.AccountNo = dt.Rows(j).Item("txtAccountNo")
					lstApprovalPerson.BankName = dt.Rows(j).Item("fkiBankID")
					lstApprovalPerson.BankBranch = dt.Rows(j).Item("fkiBranchID")
					lstApprovalPerson.PencomBatch = dt.Rows(j).Item("txtPencomBatch").ToString
					lstApprovalPerson.DateApprovalConfirmed = dt.Rows(j).Item("dteApprovalConfirmed").ToString

					'dteApprovalConfirmed

					lstApprovalPeople.Add(lstApprovalPerson)
					'fkiBranchID
					'txtAccountNo

					'Amount
					j = j + 1
				Loop


				ViewState("ApprovedPeople") = dt
				'ViewState("ApprovedPeople") = lstApprovalPeople
				Dim l As Integer = 0
				Do While l < lstApprovalPeople.Count
					lstApprovalPIN.Items.Add(lstApprovalPeople(l).PIN)
					l = l + 1
				Loop


				'BindGrid(lstApprovalPeople, AppType)

			Else

			End If




		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

     End Sub
     Private Function convertDToList(dt As DataTable, Apptype As Integer) As List(Of PencomApprovalPeople)


          Dim lstApprovalPeople As New List(Of PencomApprovalPeople)
          Dim j As Integer = 0
          Try

               '      MsgBox("" & dt.Rows.Count)

               Do While j < dt.Rows.Count
                    Dim lstApprovalPerson As New PencomApprovalPeople
                    lstApprovalPerson.ApplicationCode = dt.Rows(j).Item("ApplicationCode").ToString
                    lstApprovalPerson.PIN = dt.Rows(j).Item("PIN").ToString
                    lstApprovalPerson.Name = dt.Rows(j).Item("Name").ToString.Replace("|", "")
                    If Apptype = 2 Then
                         lstApprovalPerson.Disengagement = dt.Rows(j).Item("Disengagement")
                    ElseIf Apptype = 8 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("RetirementDate")
                    ElseIf Apptype = 7 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("RetirementDate")

                    ElseIf Apptype = 3 Then

                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("RetirementDate")
                         lstApprovalPerson.MonthlyDrawndown = dt.Rows(j).Item("MonthPension")
                         lstApprovalPerson.LumpSum = dt.Rows(j).Item("LumpSum")
                         lstApprovalPerson.Arears = CInt(dt.Rows(j).Item("ArearsMonths")) * lstApprovalPerson.MonthlyDrawndown

                         lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
                         lstApprovalPerson.MonthlyDrawndownToPay = dt.Rows(j).Item("PayingPension")
                         lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingArrears")

                    ElseIf Apptype = 4 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("RetirementDate")
                         lstApprovalPerson.MonthlyAnnuity = dt.Rows(j).Item("monthly-annuity")
                         lstApprovalPerson.InsuranceCompanyName = dt.Rows(j).Item("insurance-company-name")

                    ElseIf Apptype = 1 Then

                    End If

                    lstApprovalPerson.ApprovedAmount = dt.Rows(j).Item("ApprovedAmount")
                    lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
                    lstApprovalPerson.AccountName = dt.Rows(j).Item("AccountName")
                    lstApprovalPerson.AccountNo = dt.Rows(j).Item("AccountNo")
                    lstApprovalPerson.BankName = dt.Rows(j).Item("BankName")
                    lstApprovalPerson.BankBranch = dt.Rows(j).Item("BankBranch")
                    lstApprovalPerson.PencomBatch = dt.Rows(j).Item("PencomBatch").ToString
                    lstApprovalPerson.DateApprovalConfirmed = dt.Rows(j).Item("DateApprovalConfirmed").ToString
                    lstApprovalPeople.Add(lstApprovalPerson)
                    j = j + 1

               Loop

               Return lstApprovalPeople
          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Function
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

               If AppType = 2 Then

                    dtColumn = New DataColumn("Disengagement")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AmountToPay")
                         dtApproval.Columns.Add(dtColumn)


               ElseIf AppType = 1 Then
                    dtColumn = New DataColumn("RetirementDate")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AmountToPay")
					dtApproval.Columns.Add(dtColumn)

				ElseIf AppType = 16 Then
					dtColumn = New DataColumn("RetirementDate")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("AmountToPay")
					dtApproval.Columns.Add(dtColumn)

               ElseIf AppType = 8 Then
                    dtColumn = New DataColumn("RetirementDate")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AmountToPay")
                         dtApproval.Columns.Add(dtColumn)

               ElseIf AppType = 7 Then


					dtColumn = New DataColumn("RetirementDate")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AmountToPay")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("InterestAmount")
					dtApproval.Columns.Add(dtColumn)


               ElseIf AppType = 3 Then

					dtColumn = New DataColumn("RetirementDate")
					dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("monthly-programed-drawndown")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("LumpSum")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("Arrears")
                         dtApproval.Columns.Add(dtColumn)


                         dtColumn = New DataColumn("MonthlyDrawndownToPay")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("LumpSumToPay")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("ArearsToPay")
					dtApproval.Columns.Add(dtColumn)


				ElseIf AppType = 14 Then

					dtColumn = New DataColumn("RetirementDate")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("monthly-programed-drawndown")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("LumpSum")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("Arrears")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("MonthlyDrawndownToPay")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("LumpSumToPay")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("ArearsToPay")
					dtApproval.Columns.Add(dtColumn)



                    ElseIf AppType = 4 Then


                         dtColumn = New DataColumn("RetirementDate")
                         dtApproval.Columns.Add(dtColumn)
                         '
                         dtColumn = New DataColumn("LumpSum")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("monthly-annuity")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("insurance-company-name")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("LumpSumToPay")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AnnuityToPay")
					dtApproval.Columns.Add(dtColumn)

				ElseIf AppType = 15 Then


					dtColumn = New DataColumn("RetirementDate")
					dtApproval.Columns.Add(dtColumn)
					'
					dtColumn = New DataColumn("LumpSum")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("monthly-annuity")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("insurance-company-name")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("LumpSumToPay")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("AnnuityToPay")
					dtApproval.Columns.Add(dtColumn)

                    ElseIf AppType = 5 Then
                         dtColumn = New DataColumn("RetirementDate")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AmountToPay")
                         dtApproval.Columns.Add(dtColumn)

                    ElseIf AppType = 6 Then
                         dtColumn = New DataColumn("RetirementDate")
                         dtApproval.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AmountToPay")
					dtApproval.Columns.Add(dtColumn)

				ElseIf AppType = 11 Then
					dtColumn = New DataColumn("RetirementDate")
					dtApproval.Columns.Add(dtColumn)

					dtColumn = New DataColumn("AmountToPay")
					dtApproval.Columns.Add(dtColumn)

               End If

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
                    If AppType = 2 Then
                         newCustomersRow("Disengagement") = dt(i).Disengagement.ToString("yyyy-MM-dd")
                         'newCustomersRow("ApprovedAmount") = dt(i).ApprovedAmount
                         newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")
                    ElseIf AppType = 8 Then
                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
                         newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")
                    ElseIf AppType = 7 Then
                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
					newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")
					newCustomersRow("InterestAmount") = dt(i).InterestAmount.ToString("0.#0")

                    ElseIf AppType = 3 Then

                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
                         newCustomersRow("monthly-programed-drawndown") = dt(i).MonthlyDrawndown
                         newCustomersRow("Arrears") = dt(i).Arears
                         newCustomersRow("LumpSum") = dt(i).LumpSum

                         newCustomersRow("MonthlyDrawndownToPay") = dt(i).MonthlyDrawndownToPay
                         newCustomersRow("ArearsToPay") = dt(i).ArearsToPay
					newCustomersRow("LumpSumToPay") = dt(i).LumpSumToPay


				ElseIf AppType = 14 Then

					newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
					newCustomersRow("monthly-programed-drawndown") = dt(i).MonthlyDrawndown
					newCustomersRow("Arrears") = dt(i).Arears
					newCustomersRow("LumpSum") = dt(i).LumpSum

					newCustomersRow("MonthlyDrawndownToPay") = dt(i).MonthlyDrawndownToPay
					newCustomersRow("ArearsToPay") = dt(i).ArearsToPay
					newCustomersRow("LumpSumToPay") = dt(i).LumpSumToPay


                    ElseIf AppType = 4 Then
                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")

                         newCustomersRow("monthly-annuity") = dt(i).MonthlyAnnuity
                         newCustomersRow("insurance-company-name") = dt(i).InsuranceCompanyName
                         newCustomersRow("LumpSum") = dt(i).LumpSum
                         newCustomersRow("AnnuityToPay") = dt(i).AnnuityToPay
					newCustomersRow("LumpSumToPay") = dt(i).LumpSumToPay

				ElseIf AppType = 15 Then
					newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")

					newCustomersRow("monthly-annuity") = dt(i).MonthlyAnnuity
					newCustomersRow("insurance-company-name") = dt(i).InsuranceCompanyName
					newCustomersRow("LumpSum") = dt(i).LumpSum
					newCustomersRow("AnnuityToPay") = dt(i).AnnuityToPay
					newCustomersRow("LumpSumToPay") = dt(i).LumpSumToPay

                    ElseIf AppType = 1 Then
                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
					newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")

				ElseIf AppType = 16 Then

					newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
					newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")

                    ElseIf AppType = 5 Then

                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
                         newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")

                    ElseIf AppType = 6 Then
                         newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
					newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")
				ElseIf AppType = 11 Then
					newCustomersRow("RetirementDate") = dt(i).RetirementDate.ToString("yyyy-MM-dd")
					newCustomersRow("AmountToPay") = dt(i).AmountToPay.ToString("0.#0")

                    End If

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

     Protected Sub btnApprovalSave_Click(sender As Object, e As EventArgs) Handles btnApprovalSave.Click

          Try

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

                    If IsNothing(ViewState("ApprovalDetails")) = True And typeID = 2 Then
                         Me.MPApprovalHardShip.Show()
                    ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 8 Then
					Me.MPApprovalHardShip.Show()

				ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 11 Then
					Me.MPApprovalHardShip.Show()

                    ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 1 Then
					Me.MPApprovalHardShip.Show()

				ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 16 Then
					Me.MPApprovalHardShip.Show()
				ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 7 Then

					Me.MPApprovalHardShip.Show()

                    ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 3 Then
                         Me.mpApprovalPW.Show()
                         'Me.MPApprovalHardShip.Show()
                    ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 4 Then
					Me.mpApprovalAnn.Show()
				ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 15 Then
					Me.mpApprovalAnn.Show()
                    ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 5 Then
                         Me.MPApprovalHardShip.Show()
                    ElseIf IsNothing(ViewState("ApprovalDetails")) = True And typeID = 6 Then
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


     Private Sub postApprovalEntries(TypeID As Integer, UName As String)

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail)

          Try

               For Each grow As GridViewRow In Me.gridRMAS.Rows
                    'MsgBox("" & grow.RowIndex)

                    cb = grow.FindControl("ChkRMASApproval")

                    If cb.Checked = True And TypeID = 3 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtPWApprovalBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
                         lstApprovedApp.AmountToPay = CDbl(grow.Cells(7).Text.ToString())
                         lstApprovedApp.PayingLumpSum = CDbl(grow.Cells(6).Text.ToString())
					lstApprovedApp.PayingPension = CDbl(grow.Cells(8).Text.ToString())
					'the keeps the calculated arrears pension
					lstApprovedApp.PayingAnnuity = CDbl(grow.Cells(9).Text.ToString())

                         lstApprovedApp.PayingArrears = CDbl(grow.Cells(10).Text.ToString())
                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)

                         lstApprovedApps.Add(lstApprovedApp)

                    ElseIf cb.Checked = True And TypeID = 4 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtAnnApprovalBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
                         lstApprovedApp.Annuity = CDbl(grow.Cells(7).Text.ToString())
                         lstApprovedApp.PayingLumpSum = CDbl(grow.Cells(6).Text.ToString())
                         lstApprovedApp.PayingAnnuity = CDbl(grow.Cells(8).Text.ToString())
                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)

				ElseIf cb.Checked = True And TypeID = 15 Then

					Dim lstApprovedApp As New ApplicationDetail
					lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
					lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
					lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
					lstApprovedApp.PencomBatch = Me.txtAnnApprovalBatchRef.Text
					lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
					lstApprovedApp.Annuity = CDbl(grow.Cells(7).Text.ToString())
					lstApprovedApp.PayingLumpSum = CDbl(grow.Cells(6).Text.ToString())
					lstApprovedApp.PayingAnnuity = CDbl(grow.Cells(8).Text.ToString())
					lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)


                    ElseIf cb.Checked = True And TypeID = 2 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(6).Text.ToString())
                         lstApprovedApp.AmountToPay = CDbl(grow.Cells(7).Text.ToString())

                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
                         lstApprovedApps.Add(lstApprovedApp)

                    ElseIf cb.Checked = True And TypeID = 1 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
                         lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())

                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)

				ElseIf cb.Checked = True And TypeID = 16 Then

					Dim lstApprovedApp As New ApplicationDetail
					lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
					lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
					lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
					lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
					lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
					lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())

					lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)


                    ElseIf cb.Checked = True And TypeID = 5 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
                         lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())

                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
                         lstApprovedApps.Add(lstApprovedApp)

                    ElseIf cb.Checked = True And TypeID = 6 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
                         lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())

                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
                         lstApprovedApps.Add(lstApprovedApp)


                    ElseIf cb.Checked = True And TypeID = 7 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
					lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())
					lstApprovedApp.InterestAmount = CDbl(grow.Cells(7).Text.ToString())
                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
                         lstApprovedApps.Add(lstApprovedApp)


                    ElseIf cb.Checked = True And TypeID = 8 Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
                         lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
                         lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())

                         lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)

				ElseIf cb.Checked = True And TypeID = 11 Then

					Dim lstApprovedApp As New ApplicationDetail
					lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
					lstApprovedApp.PIN = grow.Cells(2).Text.ToString()
					lstApprovedApp.AppTypeId = getApprovalType(Me.ddApplicationType.SelectedValue)
					lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
					lstApprovedApp.ApprovedAmount = CDbl(grow.Cells(5).Text.ToString())
					lstApprovedApp.AmountToPay = CDbl(grow.Cells(6).Text.ToString())
					lstApprovedApp.ApprovalOrderID = (grow.RowIndex + 1)
					lstApprovedApps.Add(lstApprovedApp)

					'txtEditApprovedAmount

                    ElseIf cb.Checked = False Then

                    End If

               Next

               If TypeID = 3 Then

                    ApprovalDetails.AcknowledgmentDate = CDate(Me.txtPWAcknowledgmentDate.Text)
                    ApprovalDetails.ApprovalDate = CDate(Me.txtPWApprovedDate.Text)
                    ApprovalDetails.TotalLumpSumAmount = CDbl(Me.txtPWApprovalLumpSum.Text)
                    ApprovalDetails.TotalPensionAmount = CDbl(Me.txtPWApprovalPension.Text)
                    ApprovalDetails.PencomBatch = Me.txtPWApprovalBatchRef.Text
                    ApprovalDetails.AppType = TypeID
                    ApprovalDetails.CreatedBy = UName

               ElseIf TypeID = 4 Then

                    ApprovalDetails.AcknowledgmentDate = CDate(Me.txtAnnAcknowledgmentDate.Text)
                    ApprovalDetails.ApprovalDate = CDate(Me.txtAnnApprovedDate.Text)
                    ApprovalDetails.TotalLumpSumAmount = CDbl(Me.txtAnnApprovalLumpSum.Text)
                    ApprovalDetails.TotalAnnuityAmount = CDbl(Me.txtAnnApprovalAnnuity.Text)
                    ApprovalDetails.PencomBatch = Me.txtAnnApprovalBatchRef.Text
                    ApprovalDetails.AppType = TypeID
				ApprovalDetails.CreatedBy = UName

			ElseIf TypeID = 15 Then

				ApprovalDetails.AcknowledgmentDate = CDate(Me.txtAnnAcknowledgmentDate.Text)
				ApprovalDetails.ApprovalDate = CDate(Me.txtAnnApprovedDate.Text)
				ApprovalDetails.TotalLumpSumAmount = CDbl(Me.txtAnnApprovalLumpSum.Text)
				ApprovalDetails.TotalAnnuityAmount = CDbl(Me.txtAnnApprovalAnnuity.Text)
				ApprovalDetails.PencomBatch = Me.txtAnnApprovalBatchRef.Text
				ApprovalDetails.AppType = TypeID
				ApprovalDetails.CreatedBy = UName

               ElseIf TypeID = 2 Then

                    ApprovalDetails.AcknowledgmentDate = CDate(Me.txtAcknowledgmentDate.Text)
                    ApprovalDetails.ApprovalDate = CDate(Me.txtApprovedDate.Text)
                    ApprovalDetails.TotalApprovalAmount = CDbl(Me.txtTotalApprovedAmount.Text)
                    ApprovalDetails.PencomBatch = Me.txtBatchRef.Text
                    ApprovalDetails.AppType = TypeID
                    ApprovalDetails.CreatedBy = UName
               Else
                    ApprovalDetails.AcknowledgmentDate = CDate(Me.txtAcknowledgmentDate.Text)
                    ApprovalDetails.ApprovalDate = CDate(Me.txtApprovedDate.Text)
                    ApprovalDetails.TotalApprovalAmount = CDbl(Me.txtTotalApprovedAmount.Text)
                    ApprovalDetails.PencomBatch = Me.txtBatchRef.Text
                    ApprovalDetails.AppType = TypeID
                    ApprovalDetails.CreatedBy = UName
			End If

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

			' Me.gridRMAS.DataSource = DBNull.Value
			'   Dim clrGrid As New List(Of PencomApprovalPeople)
			'  BindGrid(clrGrid, 0)

			'   Me.gridRMAS.Columns.Clear()

			'Refresh()

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
     Private Sub Refresh()
          Dim str As String, cr As New Core




          If IsNothing(ViewState("ApprovalTypeCollection")) = False And IsNothing(ViewState("str")) = False Then
               ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
               str = ViewState("str")
               Dim dt As DataTable, j As Integer, lstApprovalPeople As New List(Of PencomApprovalPeople)
               Dim Apptype As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))
               dt = cr.PMgetPaymentData(str, CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), Now.Date)

               Do While j < dt.Rows.Count
                    Dim lstApprovalPerson As New PencomApprovalPeople

                    lstApprovalPerson.ApplicationCode = dt.Rows(j).Item("txtApplicationCode").ToString
                    lstApprovalPerson.PIN = dt.Rows(j).Item("txtPIN").ToString
                    lstApprovalPerson.Name = dt.Rows(j).Item("txtFullName").ToString
                    If Apptype = 2 Then
                         lstApprovalPerson.Disengagement = dt.Rows(j).Item("dteDisengagement")
                    ElseIf Apptype = 8 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")

                    ElseIf Apptype = 7 Then
                         lstApprovalPerson.RetirementDate = Now.Date.ToString("yyyy/MM/dd")

                    ElseIf Apptype = 1 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
                    ElseIf Apptype = 3 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
                         lstApprovalPerson.MonthlyDrawndown = dt.Rows(j).Item("monthly-programed-drawndown")
                    ElseIf Apptype = 4 Then
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
                         lstApprovalPerson.InsuranceCompanyName = dt.Rows(j).Item("insurance-company-name")
                         lstApprovalPerson.MonthlyAnnuity = dt.Rows(j).Item("monthly-annuity")
                    End If

                    lstApprovalPerson.ApprovedAmount = dt.Rows(j).Item("ApprovedAmount")
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

               BindGrid(lstApprovalPeople, Apptype)

          Else

          End If



     End Sub

     Protected Sub btnHardApprovalOK_Click(sender As Object, e As EventArgs) Handles btnHardApprovalOK.Click

          blnHardShipApproval = True
          ViewState("ApprovalDetails") = blnHardShipApproval

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

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

          For Each grow As GridViewRow In Me.gridRMAS.Rows
               If grow.Enabled = True Then
                    Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkRMASApproval"), CheckBox)
                    cb.Checked = False
               Else
               End If



          Next

     End Sub

     Protected Sub btnUpdateApprovedAmount_Click(sender As Object, e As EventArgs) Handles btnUpdateApprovedAmount.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail)
          Dim typeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
          Try

               For Each grow As GridViewRow In Me.gridRMAS.Rows

                    cb = grow.FindControl("ChkRMASApproval")

                    If cb.Checked = True And typeID = 3 Then

                         Me.txtPWApplicationCode.Text = grow.Cells(1).Text
                         Me.txtPWLumpSumAmount.Text = grow.Cells(6).Text
                         Me.txtPWPensionAmount.Text = grow.Cells(8).Text
                         Me.txtPWArrears.Text = grow.Cells(10).Text
                         Me.txtPWApprovedLumpSumAmount.Text = grow.Cells(5).Text
                         Me.txtPWApprovedPensionAmount.Text = grow.Cells(7).Text

                         chk = chk + 1

                    ElseIf cb.Checked = True And typeID = 4 Then

                         Me.txtAnnApplicationCode.Text = grow.Cells(1).Text
                         Me.txtAnnLumpSumAmount.Text = grow.Cells(6).Text
                         Me.txtAnnAnnuityAmount.Text = grow.Cells(8).Text
                         txtAnnLumpSumApprovedAmount.Text = grow.Cells(5).Text
                         txtAnnAnnuityApprovedAmount.Text = grow.Cells(7).Text

					chk = chk + 1
					'additional annuity application
				ElseIf cb.Checked = True And typeID = 15 Then

					Me.txtAnnApplicationCode.Text = grow.Cells(1).Text
					Me.txtAnnLumpSumAmount.Text = grow.Cells(6).Text
					Me.txtAnnAnnuityAmount.Text = grow.Cells(8).Text
					txtAnnLumpSumApprovedAmount.Text = grow.Cells(5).Text
					txtAnnAnnuityApprovedAmount.Text = grow.Cells(7).Text

					chk = chk + 1

                    ElseIf cb.Checked = True And typeID = 1 Then

                         Me.txtEnblocApplicationCode.Text = grow.Cells(1).Text
                         Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
                         Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text

					chk = chk + 1
					'additional enbloc application
				ElseIf cb.Checked = True And typeID = 16 Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(1).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text

					chk = chk + 1

					'using the pop screen for enbloc to modify the approved and amountTo Pay for DBA payment by setting values on the text fields
                    ElseIf cb.Checked = True And typeID = 5 Then

                         Me.txtEnblocApplicationCode.Text = grow.Cells(1).Text
                         Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
                         Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text

                         chk = chk + 1


                         'using the pop screen for enbloc to modify the approved and amountTo Pay for NSITF payment by setting values on the text fields
                    ElseIf cb.Checked = True And typeID = 6 Then

                         Me.txtEnblocApplicationCode.Text = grow.Cells(1).Text
                         Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
                         Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text

                         chk = chk + 1

                         'using the pop screen for AVC to modify the approved and amountTo Pay for NSITF payment by setting values on the text fields
                    ElseIf cb.Checked = True And typeID = 7 Then

					Me.txtAVCApplicationCode.Text = grow.Cells(1).Text
					Me.txtAVCApprovedAmount.Text = grow.Cells(5).Text
					Me.txtAVCAmountToPay.Text = grow.Cells(6).Text

					'mpEditAVCApproval

                         chk = chk + 1
                         'using the pop screen for Legacy to modify the approved and amountTo Pay for NSITF payment by setting values on the text fields
                    ElseIf cb.Checked = True And typeID = 8 Then

                         Me.txtEnblocApplicationCode.Text = grow.Cells(1).Text
                         Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
                         Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text

                         chk = chk + 1

					'using the pop screen for employee portion to modify the approved and amountTo Pay for Employee portion payment by setting values on the text fields
				ElseIf cb.Checked = True And typeID = 11 Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(1).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text

					chk = chk + 1


                    ElseIf cb.Checked = True And typeID <> 3 Then

                         Me.txtApplicationCode.Text = grow.Cells(1).Text
                         txtEditApprovedAmount.Text = grow.Cells(6).Text

                         chk = chk + 1

                    ElseIf cb.Checked = False Then

                    End If

               Next

               If chk = 1 And typeID = 3 Then
                    mpEditPWApproval.Show()
               ElseIf chk = 1 And typeID = 4 Then
				mpEditAnnApproval.Show()
			ElseIf chk = 1 And typeID = 15 Then
				mpEditAnnApproval.Show()
               ElseIf chk = 1 And typeID = 1 Then
				mpEditEnblocApproval.Show()
			ElseIf chk = 1 And typeID = 16 Then
				mpEditEnblocApproval.Show()
				'using the pop screen for enbloc to modify the approved and amountTo Pay for DBA payment
               ElseIf chk = 1 And typeID = 5 Then
                    mpEditEnblocApproval.Show()
                    'using the pop screen for enbloc to modify the approved and amountTo Pay for NSITF payment
               ElseIf chk = 1 And typeID = 6 Then
                    mpEditEnblocApproval.Show()
                    'using the pop screen for enbloc to modify the approved and amountTo Pay for AVC payment
               ElseIf chk = 1 And typeID = 7 Then
				'mpEditEnblocApproval.Show()
				mpEditAVCApproval.Show()

                    'using the pop screen for Legacy to modify the approved and amountTo Pay for NSITF payment
               ElseIf chk = 1 And typeID = 8 Then
				mpEditEnblocApproval.Show()
			ElseIf chk = 1 And typeID = 11 Then
				mpEditEnblocApproval.Show()
               ElseIf chk = 1 Then
                    mpEditApprovedAmount.Show()
               End If

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub btnEditApprovedAmount_Click(sender As Object, e As EventArgs) Handles btnEditApprovedAmount.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail)

          Try

               For Each grow As GridViewRow In Me.gridRMAS.Rows

                    cb = grow.FindControl("ChkRMASApproval")

                    If cb.Checked = True Then

                         Dim lstApprovedApp As New ApplicationDetail
                         lstApprovedApp.ApplicationID = grow.Cells(1).Text.ToString()
                         lstApprovedApp.PencomBatch = Me.txtBatchRef.Text
                         lstApprovedApps.Add(lstApprovedApp)

                    ElseIf cb.Checked = False Then

                    End If

               Next

               ApprovalDetails.AcknowledgmentDate = CDate(Me.txtAcknowledgmentDate.Text)
               ApprovalDetails.ApprovalDate = CDate(Me.txtApprovedDate.Text)
               ApprovalDetails.TotalApprovalAmount = CDbl(Me.txtTotalApprovedAmount.Text)
               ApprovalDetails.PencomBatch = Me.txtBatchRef.Text
               ApprovalDetails.CreatedBy = Session("user")

			cr.PMInsertPencomApproval(ApprovalDetails, lstApprovedApps, 0, 1, Server.MapPath("~/Logs"))
               'Refresh()


          Catch ex As Exception

          End Try


	End Sub

	Protected Sub gridRMAS_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridRMAS.PageIndexChanging

		Dim dtApproval As New DataTable
		Try

			gridRMAS.PageIndex = e.NewPageIndex
			dtApproval = ViewState("Applications")

			Me.gridRMAS.DataSource = dtApproval
			Me.gridRMAS.DataBind()

			If dtApproval.Rows.Count > 5 Then
				pnlGrid.Height = Nothing
			Else
			End If

		Catch ex As Exception

		End Try
		

	End Sub

     Protected Sub gridRMAS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridRMAS.SelectedIndexChanged

     End Sub


     Protected Sub ddApplicationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddApplicationType.SelectedIndexChanged

          getUnApprovedBatches()

     End Sub

     Protected Sub btnSaveApprovedAmount_Click(sender As Object, e As EventArgs) Handles btnSaveApprovedAmount.Click
          refreshApprovalGrid()
     End Sub
     Protected Sub refreshApprovalGrid()
          Dim chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovalPeople As New List(Of PencomApprovalPeople)

          Try

               ''''''''''''''''''''''''''''''''''''Before the arrangement of PIN on approval letter'''''''''''''''''''''''''''''''''''''''''''''''
               'If IsNothing(ViewState("Applications")) = False And IsNothing(ViewState("AppType")) = False Then
               '     dt = ViewState("Applications")
               '     lstApprovalPeople = convertDToList(dt, CInt(ViewState("AppType")))

               '     For Each i As PencomApprovalPeople In lstApprovalPeople

               '          If i.ApplicationCode = txtApplicationCode.Text Then
               '               i.ApprovedAmount = txtEditApprovedAmount.Text
               '          Else

               '          End If
               '     Next

               '     BindGrid(lstApprovalPeople, CInt(ViewState("AppType")))
               'Else

               'End If
               ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

               Dim cb As New CheckBox
               For Each grow As GridViewRow In Me.gridRMAS.Rows

                    Dim lstApprovalPerson As New PencomApprovalPeople

                    cb = grow.FindControl("ChkRMASApproval")

                    If cb.Checked = True And CInt(ViewState("AppType")) = 3 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         'lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.LumpSum = Me.txtPWApprovedLumpSumAmount.Text
                         lstApprovalPerson.MonthlyDrawndown = Me.txtPWApprovedPensionAmount.Text
                         lstApprovalPerson.Arears = grow.Cells(9).Text
                         lstApprovalPerson.AccountName = grow.Cells(11).Text
                         lstApprovalPerson.AccountNo = grow.Cells(12).Text
                         lstApprovalPerson.BankName = grow.Cells(13).Text
                         lstApprovalPerson.BankBranch = grow.Cells(14).Text

                         lstApprovalPerson.LumpSumToPay = Me.txtPWLumpSumAmount.Text
                         lstApprovalPerson.MonthlyDrawndownToPay = Me.txtPWPensionAmount.Text
                         lstApprovalPerson.ArearsToPay = Me.txtPWArrears.Text

                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 3 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.LumpSum = grow.Cells(5).Text
                         lstApprovalPerson.MonthlyDrawndown = grow.Cells(7).Text
                         lstApprovalPerson.Arears = grow.Cells(9).Text
                         lstApprovalPerson.AccountName = grow.Cells(11).Text
                         lstApprovalPerson.AccountNo = grow.Cells(12).Text
                         lstApprovalPerson.BankName = grow.Cells(13).Text
                         lstApprovalPerson.BankBranch = grow.Cells(14).Text
                         lstApprovalPerson.LumpSumToPay = grow.Cells(6).Text
                         lstApprovalPerson.MonthlyDrawndownToPay = grow.Cells(8).Text
                         lstApprovalPerson.ArearsToPay = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    End If

                    If cb.Checked = True And CInt(ViewState("AppType")) = 4 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         'lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.LumpSum = txtAnnLumpSumApprovedAmount.Text
                         lstApprovalPerson.MonthlyAnnuity = Me.txtAnnAnnuityApprovedAmount.Text
                         lstApprovalPerson.InsuranceCompanyName = grow.Cells(9).Text
                         lstApprovalPerson.AccountName = grow.Cells(10).Text
                         lstApprovalPerson.AccountNo = grow.Cells(11).Text
                         lstApprovalPerson.BankName = grow.Cells(12).Text
                         lstApprovalPerson.BankBranch = grow.Cells(13).Text
                         lstApprovalPerson.LumpSumToPay = Me.txtAnnLumpSumAmount.Text
                         lstApprovalPerson.AnnuityToPay = Me.txtAnnAnnuityAmount.Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 4 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         'lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.LumpSum = grow.Cells(5).Text
                         lstApprovalPerson.MonthlyAnnuity = grow.Cells(7).Text
                         lstApprovalPerson.InsuranceCompanyName = grow.Cells(9).Text
                         lstApprovalPerson.AccountName = grow.Cells(10).Text
                         lstApprovalPerson.AccountNo = grow.Cells(11).Text
                         lstApprovalPerson.BankName = grow.Cells(12).Text
                         lstApprovalPerson.BankBranch = grow.Cells(13).Text
                         lstApprovalPerson.LumpSumToPay = grow.Cells(6).Text
                         lstApprovalPerson.AnnuityToPay = grow.Cells(8).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

				End If


				If cb.Checked = True And CInt(ViewState("AppType")) = 15 Then

					lstApprovalPerson = New PencomApprovalPeople
					'lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
					lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
					lstApprovalPerson.PIN = grow.Cells(2).Text
					lstApprovalPerson.Name = grow.Cells(3).Text
					lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.LumpSum = txtAnnLumpSumApprovedAmount.Text
					lstApprovalPerson.MonthlyAnnuity = Me.txtAnnAnnuityApprovedAmount.Text
					lstApprovalPerson.InsuranceCompanyName = grow.Cells(9).Text
					lstApprovalPerson.AccountName = grow.Cells(10).Text
					lstApprovalPerson.AccountNo = grow.Cells(11).Text
					lstApprovalPerson.BankName = grow.Cells(12).Text
					lstApprovalPerson.BankBranch = grow.Cells(13).Text
					lstApprovalPerson.LumpSumToPay = Me.txtAnnLumpSumAmount.Text
					lstApprovalPerson.AnnuityToPay = Me.txtAnnAnnuityAmount.Text
					lstApprovalPeople.Add(lstApprovalPerson)

				ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 15 Then

					lstApprovalPerson = New PencomApprovalPeople
					'lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
					lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
					lstApprovalPerson.PIN = grow.Cells(2).Text
					lstApprovalPerson.Name = grow.Cells(3).Text
					lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.LumpSum = grow.Cells(5).Text
					lstApprovalPerson.MonthlyAnnuity = grow.Cells(7).Text
					lstApprovalPerson.InsuranceCompanyName = grow.Cells(9).Text
					lstApprovalPerson.AccountName = grow.Cells(10).Text
					lstApprovalPerson.AccountNo = grow.Cells(11).Text
					lstApprovalPerson.BankName = grow.Cells(12).Text
					lstApprovalPerson.BankBranch = grow.Cells(13).Text
					lstApprovalPerson.LumpSumToPay = grow.Cells(6).Text
					lstApprovalPerson.AnnuityToPay = grow.Cells(8).Text
					lstApprovalPeople.Add(lstApprovalPerson)

				End If




                    'ElseIf cb.Checked = False And CInt(ViewState("AppType")) <> 3 Then

                    '     lstApprovalPerson = New PencomApprovalPeople
                    '     ' lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
                    '     lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                    '     lstApprovalPerson.PIN = grow.Cells(2).Text
                    '     lstApprovalPerson.Name = grow.Cells(3).Text
                    '     lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                    '     lstApprovalPerson.LumpSum = grow.Cells(5).Text
                    '     lstApprovalPerson.MonthlyDrawndown = grow.Cells(6).Text
                    '     lstApprovalPerson.Arears = grow.Cells(7).Text
                    '     lstApprovalPerson.AccountName = grow.Cells(8).Text
                    '     lstApprovalPerson.AccountNo = grow.Cells(9).Text
                    '     lstApprovalPerson.BankName = grow.Cells(10).Text
                    '     lstApprovalPerson.BankBranch = grow.Cells(11).Text
                    '     lstApprovalPerson.LumpSumToPay = grow.Cells(12).Text
                    '     lstApprovalPerson.MonthlyDrawndownToPay = grow.Cells(13).Text
                    '     lstApprovalPerson.ArearsToPay = grow.Cells(14).Text

                    '     lstApprovalPeople.Add(lstApprovalPerson)

                    'ElseIf cb.Checked = True And CInt(ViewState("AppType")) <> 3 Then

                    '     lstApprovalPerson = New PencomApprovalPeople
                    '     lstApprovalPerson.ApprovedAmount = txtEditApprovedAmount.Text
                    '     'lstApprovalPerson.MonthlyDrawndown = grow.Cells(6).Text
                    '     lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                    '     lstApprovalPerson.PIN = grow.Cells(2).Text
                    '     lstApprovalPerson.Name = grow.Cells(3).Text
                    '     lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                    '     lstApprovalPerson.AccountName = grow.Cells(6).Text
                    '     lstApprovalPerson.AccountNo = grow.Cells(7).Text
                    '     lstApprovalPerson.BankName = grow.Cells(8).Text
                    '     lstApprovalPerson.BankBranch = grow.Cells(9).Text
                    '     lstApprovalPeople.Add(lstApprovalPerson)

                    If cb.Checked = True And CInt(ViewState("AppType")) = 2 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         '                         lstApprovalPerson.ApprovedAmount = grow.Cells(6).Text
                         lstApprovalPerson.ApprovedAmount = Me.txtEditApprovedAmount.Text
                         lstApprovalPerson.AmountToPay = Me.txtEditApprovedAmount.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.Disengagement = grow.Cells(5).Text
                         lstApprovalPerson.AccountName = grow.Cells(8).Text
                         lstApprovalPerson.AccountNo = grow.Cells(9).Text
                         lstApprovalPerson.BankName = grow.Cells(10).Text
                         lstApprovalPerson.BankBranch = grow.Cells(11).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 2 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = grow.Cells(6).Text
                         lstApprovalPerson.AmountToPay = grow.Cells(7).Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.Disengagement = grow.Cells(5).Text
                         lstApprovalPerson.AccountName = grow.Cells(8).Text
                         lstApprovalPerson.AccountNo = grow.Cells(9).Text
                         lstApprovalPerson.BankName = grow.Cells(10).Text
                         lstApprovalPerson.BankBranch = grow.Cells(11).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    End If

                    If cb.Checked = True And CInt(ViewState("AppType")) = 1 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
                         lstApprovalPerson.AmountToPay = Me.txtEnblocAmountToPay.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 1 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
                         lstApprovalPerson.AmountToPay = grow.Cells(6).Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

				End If


				If cb.Checked = True And CInt(ViewState("AppType")) = 16 Then

					lstApprovalPerson = New PencomApprovalPeople
					lstApprovalPerson.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
					lstApprovalPerson.AmountToPay = Me.txtEnblocAmountToPay.Text
					lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
					lstApprovalPerson.PIN = grow.Cells(2).Text
					lstApprovalPerson.Name = grow.Cells(3).Text
					'  lstApprovalPerson.ValueDate = grow.Cells(4).Text
					lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.AccountName = grow.Cells(7).Text
					lstApprovalPerson.AccountNo = grow.Cells(8).Text
					lstApprovalPerson.BankName = grow.Cells(9).Text
					lstApprovalPerson.BankBranch = grow.Cells(10).Text
					lstApprovalPeople.Add(lstApprovalPerson)

				ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 16 Then

					lstApprovalPerson = New PencomApprovalPeople
					lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
					lstApprovalPerson.AmountToPay = grow.Cells(6).Text
					lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
					lstApprovalPerson.PIN = grow.Cells(2).Text
					lstApprovalPerson.Name = grow.Cells(3).Text
					'  lstApprovalPerson.ValueDate = grow.Cells(4).Text
					lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.AccountName = grow.Cells(7).Text
					lstApprovalPerson.AccountNo = grow.Cells(8).Text
					lstApprovalPerson.BankName = grow.Cells(9).Text
					lstApprovalPerson.BankBranch = grow.Cells(10).Text
					lstApprovalPeople.Add(lstApprovalPerson)

				End If


                    If cb.Checked = True And CInt(ViewState("AppType")) = 5 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
                         lstApprovalPerson.AmountToPay = Me.txtEnblocAmountToPay.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 5 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
                         lstApprovalPerson.AmountToPay = grow.Cells(6).Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    End If


                    If cb.Checked = True And CInt(ViewState("AppType")) = 6 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
                         lstApprovalPerson.AmountToPay = Me.txtEnblocAmountToPay.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 6 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
                         lstApprovalPerson.AmountToPay = grow.Cells(6).Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    End If


                    If cb.Checked = True And CInt(ViewState("AppType")) = 7 Then

                         lstApprovalPerson = New PencomApprovalPeople
					lstApprovalPerson.ApprovedAmount = Me.txtAVCApprovedAmount.Text
					lstApprovalPerson.AmountToPay = Me.txtAVCAmountToPay.Text
					lstApprovalPerson.InterestAmount = Me.txtAVCInterestAmount.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.AccountName = grow.Cells(8).Text
					lstApprovalPerson.AccountNo = grow.Cells(9).Text
					lstApprovalPerson.BankName = grow.Cells(10).Text
					lstApprovalPerson.BankBranch = grow.Cells(11).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 7 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
					lstApprovalPerson.AmountToPay = grow.Cells(6).Text
					lstApprovalPerson.InterestAmount = grow.Cells(7).Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.AccountName = grow.Cells(8).Text
					lstApprovalPerson.AccountNo = grow.Cells(9).Text
					lstApprovalPerson.BankName = grow.Cells(10).Text
					lstApprovalPerson.BankBranch = grow.Cells(11).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    End If

                    If cb.Checked = True And CInt(ViewState("AppType")) = 8 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
                         lstApprovalPerson.AmountToPay = Me.txtEnblocAmountToPay.Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

                    ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 8 Then

                         lstApprovalPerson = New PencomApprovalPeople
                         lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
                         lstApprovalPerson.AmountToPay = grow.Cells(6).Text
                         lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
                         lstApprovalPerson.PIN = grow.Cells(2).Text
                         lstApprovalPerson.Name = grow.Cells(3).Text
                         '  lstApprovalPerson.ValueDate = grow.Cells(4).Text
                         lstApprovalPerson.RetirementDate = grow.Cells(4).Text
                         lstApprovalPerson.AccountName = grow.Cells(7).Text
                         lstApprovalPerson.AccountNo = grow.Cells(8).Text
                         lstApprovalPerson.BankName = grow.Cells(9).Text
                         lstApprovalPerson.BankBranch = grow.Cells(10).Text
                         lstApprovalPeople.Add(lstApprovalPerson)

				End If


				If cb.Checked = True And CInt(ViewState("AppType")) = 11 Then

					lstApprovalPerson = New PencomApprovalPeople
					lstApprovalPerson.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
					lstApprovalPerson.AmountToPay = Me.txtEnblocAmountToPay.Text
					lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
					lstApprovalPerson.PIN = grow.Cells(2).Text
					lstApprovalPerson.Name = grow.Cells(3).Text
					'  lstApprovalPerson.ValueDate = grow.Cells(4).Text
					lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.AccountName = grow.Cells(7).Text
					lstApprovalPerson.AccountNo = grow.Cells(8).Text
					lstApprovalPerson.BankName = grow.Cells(9).Text
					lstApprovalPerson.BankBranch = grow.Cells(10).Text
					lstApprovalPeople.Add(lstApprovalPerson)

				ElseIf cb.Checked = False And CInt(ViewState("AppType")) = 11 Then

					lstApprovalPerson = New PencomApprovalPeople
					lstApprovalPerson.ApprovedAmount = grow.Cells(5).Text
					lstApprovalPerson.AmountToPay = grow.Cells(6).Text
					lstApprovalPerson.ApplicationCode = grow.Cells(1).Text
					lstApprovalPerson.PIN = grow.Cells(2).Text
					lstApprovalPerson.Name = grow.Cells(3).Text
					'  lstApprovalPerson.ValueDate = grow.Cells(4).Text
					lstApprovalPerson.RetirementDate = grow.Cells(4).Text
					lstApprovalPerson.AccountName = grow.Cells(7).Text
					lstApprovalPerson.AccountNo = grow.Cells(8).Text
					lstApprovalPerson.BankName = grow.Cells(9).Text
					lstApprovalPerson.BankBranch = grow.Cells(10).Text
					lstApprovalPeople.Add(lstApprovalPerson)

				End If


               Next


               Dim dtApproval As New DataTable
               dtApproval = ViewState("Applications")
               For Each grow As GridViewRow In Me.gridRMAS.Rows
                    dtApproval.Rows(0).Delete()
               Next

               Me.gridRMAS.DataSource = dtApproval
               Me.gridRMAS.DataBind()


               BindGrid(lstApprovalPeople, CInt(ViewState("AppType")))

          Catch ex As Exception

          End Try


     End Sub

     Private Sub BindGrid(lstApprovalPeople As List(Of PencomApprovalPeople))
          Throw New NotImplementedException
     End Sub

     Protected Sub btnAddPIN_Click(sender As Object, e As EventArgs) Handles btnAddPIN.Click
          Try

               If IsNothing(ViewState("lstPINs")) = False Then
                    lstPINs = ViewState("lstPINs")
               Else
               End If

               If Me.lstApprovalPIN.SelectedItem.Text <> "" Then
                    Me.lstPINs.Add(lstApprovalPIN.SelectedItem.Text)
			End If

               LoadApplicationGrid(Me.lstApprovalPIN.SelectedItem.Text, "N")

               ViewState("lstPINs") = lstPINs

          Catch ex As Exception

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

                    If AppType = 2 Then
                         lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
                         lstApprovalPerson.Disengagement = dt.Rows(j).Item("dteDisengagement")
                         lstApprovalPerson.ApprovedAmount = CDbl(dt.Rows(j).Item("AmountToPay"))
                         lstApprovalPerson.AmountToPay = CDbl(dt.Rows(j).Item("AmountToPay"))

                    ElseIf AppType = 8 Then
                         lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
                         lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
                         lstApprovalPerson.AmountToPay = CDbl(dt.Rows(j).Item("AmountToPay"))

				ElseIf AppType = 1 Then

					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.ApprovedAmount = CDbl(dt.Rows(j).Item("ApprovedAmount"))

					' calculating the current value of the participant on the fund platform
					If CDbl(dt.Rows(j).Item("AmountToPay")) <> 0 Then
						lstApprovalPerson.AmountToPay = CDbl(dt.Rows(j).Item("AmountToPay"))
					Else
						Dim vDate As Date = cr.PMgetCurrentValueDate(CInt(dt.Rows(j).Item("intFundPlatFormID")))
						Dim amtPaid As Double = cr.PMValueByDate(PIN, vDate, CInt(dt.Rows(j).Item("intFundPlatFormID")))
						lstApprovalPerson.AmountToPay = amtPaid
						lstApprovalPerson.ValueDate = vDate
					End If



				ElseIf AppType = 16 Then

					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.ApprovedAmount = CDbl(dt.Rows(j).Item("ApprovedAmount"))

					' calculating the current value of the participant on the fund platform
					If CDbl(dt.Rows(j).Item("AmountToPay")) <> 0 Then
						lstApprovalPerson.AmountToPay = CDbl(dt.Rows(j).Item("AmountToPay"))
					Else
						Dim vDate As Date = cr.PMgetCurrentValueDate(CInt(dt.Rows(j).Item("intFundPlatFormID")))
						Dim amtPaid As Double = cr.PMValueByDate(PIN, vDate, CInt(dt.Rows(j).Item("intFundPlatFormID")))
						lstApprovalPerson.AmountToPay = amtPaid
						lstApprovalPerson.ValueDate = vDate
					End If

                         


				ElseIf AppType = 7 Then

					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.ApprovedAmount = CDbl(dt.Rows(j).Item("ApprovedAmount"))
					lstApprovalPerson.AmountToPay = CDbl(dt.Rows(j).Item("AmountToPay"))
					lstApprovalPerson.InterestAmount = CDbl(dt.Rows(j).Item("numInterestAtPayment"))

				ElseIf AppType = 3 Then

					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.MonthlyDrawndown = dt.Rows(j).Item("MonthPension")
					lstApprovalPerson.LumpSum = dt.Rows(j).Item("LumpSum")

					lstApprovalPerson.Arears = CDbl(dt.Rows(j).Item("ArearsMonths")) * lstApprovalPerson.MonthlyDrawndown

					lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
					lstApprovalPerson.MonthlyDrawndownToPay = dt.Rows(j).Item("PayingPension")

					'lstApprovalPerson.ArearsToPay = CDbl(dt.Rows(j).Item("PayingArrears"))

					Dim arDate As Date, dor As Date, outstandingMonth As Integer, intAgeAtRetirement As Integer
					arDate = dt.Rows(j).Item("dteDOR")
					dor = dt.Rows(j).Item("dteDOR")
					intAgeAtRetirement = DateDiff(DateInterval.Year, dt.Rows(j).Item("dteDOB"), dor)
					'	MsgBox("" & Day(arDate))

					'If CInt(dt.Rows(j).Item("numApprovedArrears")) = 0 Then

					Select Case Day(arDate)

						Case Is > 10


							If intAgeAtRetirement > 49 Then

								dor = DateAdd(DateInterval.Month, 1, dor)
								outstandingMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							ElseIf intAgeAtRetirement < 50 Then

								Dim offSetPeriod As Integer = (50 - intAgeAtRetirement)
								Dim newdor As Date = DateAdd(DateInterval.Year, offSetPeriod, dor)
								'dor = DateAdd(DateInterval.Year, 50, dt.Rows(j).Item("dteDOB"))
								outstandingMonth = DateDiff(DateInterval.Month, newdor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							Else

							End If


						Case Is < 10

							If intAgeAtRetirement > 49 Then

								'	dor = DateAdd(DateInterval.Month, 1, dor)
								outstandingMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							ElseIf intAgeAtRetirement < 50 Then

								Dim offSetPeriod As Integer = (50 - intAgeAtRetirement)
								Dim newdor As Date = DateAdd(DateInterval.Year, offSetPeriod, dor)
								'dor = DateAdd(DateInterval.Year, 50, dt.Rows(j).Item("dteDOB"))
								outstandingMonth = DateDiff(DateInterval.Month, newdor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							Else

							End If

						Case Else

					End Select
					lstApprovalPerson.Arears = lstApprovalPerson.ArearsToPay
					'Else
					If CInt(dt.Rows(j).Item("numApprovedArrears")) > 0 Then
						lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("numApprovedArrears")
					Else
					End If



				ElseIf AppType = 14 Then

					'MsgBox("" & dt.Rows(j).Item("LumpSum"))

					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.MonthlyDrawndown = dt.Rows(j).Item("MonthPension")
					lstApprovalPerson.LumpSum = dt.Rows(j).Item("LumpSum")

					lstApprovalPerson.Arears = CDbl(dt.Rows(j).Item("ArearsMonths")) * lstApprovalPerson.MonthlyDrawndown

					lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
					lstApprovalPerson.MonthlyDrawndownToPay = dt.Rows(j).Item("PayingPension")

					'lstApprovalPerson.ArearsToPay = CDbl(dt.Rows(j).Item("PayingArrears"))

					Dim arDate As Date, dor As Date, outstandingMonth As Integer, intAgeAtRetirement As Integer
					arDate = dt.Rows(j).Item("dteDOR")
					dor = dt.Rows(j).Item("dteDOR")
					intAgeAtRetirement = DateDiff(DateInterval.Year, dt.Rows(j).Item("dteDOB"), dor)
					'	MsgBox("" & Day(arDate))

					'If CInt(dt.Rows(j).Item("numApprovedArrears")) = 0 Then

					Select Case Day(arDate)

						Case Is > 10


							If intAgeAtRetirement > 49 Then

								dor = DateAdd(DateInterval.Month, 1, dor)
								outstandingMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							ElseIf intAgeAtRetirement < 50 Then

								Dim offSetPeriod As Integer = (50 - intAgeAtRetirement)
								Dim newdor As Date = DateAdd(DateInterval.Year, offSetPeriod, dor)
								'dor = DateAdd(DateInterval.Year, 50, dt.Rows(j).Item("dteDOB"))
								outstandingMonth = DateDiff(DateInterval.Month, newdor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							Else

							End If


						Case Is < 10

							If intAgeAtRetirement > 49 Then

								'	dor = DateAdd(DateInterval.Month, 1, dor)
								outstandingMonth = DateDiff(DateInterval.Month, dor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							ElseIf intAgeAtRetirement < 50 Then

								Dim offSetPeriod As Integer = (50 - intAgeAtRetirement)
								Dim newdor As Date = DateAdd(DateInterval.Year, offSetPeriod, dor)
								'dor = DateAdd(DateInterval.Year, 50, dt.Rows(j).Item("dteDOB"))
								outstandingMonth = DateDiff(DateInterval.Month, newdor, Now.Date)
								lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("PayingPension") * outstandingMonth

							Else

							End If

						Case Else

					End Select
					lstApprovalPerson.Arears = lstApprovalPerson.ArearsToPay
					'Else
					If CInt(dt.Rows(j).Item("numApprovedArrears")) > 0 Then
						lstApprovalPerson.ArearsToPay = dt.Rows(j).Item("numApprovedArrears")
					Else
					End If


					'End If

				ElseIf AppType = 4 Then

					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.InsuranceCompanyName = dt.Rows(j).Item("insurance-company-name").ToString

					lstApprovalPerson.MonthlyAnnuity = dt.Rows(j).Item("monthly-annuity")
					lstApprovalPerson.LumpSum = dt.Rows(j).Item("lumpsum")

					lstApprovalPerson.AnnuityToPay = dt.Rows(j).Item("PayingAnnuity")
					lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString


				ElseIf AppType = 15 Then

					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.InsuranceCompanyName = dt.Rows(j).Item("insurance-company-name").ToString

					lstApprovalPerson.MonthlyAnnuity = dt.Rows(j).Item("monthly-annuity")
					lstApprovalPerson.LumpSum = dt.Rows(j).Item("lumpsum")


					lstApprovalPerson.AnnuityToPay = dt.Rows(j).Item("PayingAnnuity")
					lstApprovalPerson.LumpSumToPay = dt.Rows(j).Item("PayingLumpSum")
					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString


				ElseIf AppType = 5 Then
					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.AmountToPay = dt.Rows(j).Item("AmountToPay").ToString
				ElseIf AppType = 6 Then

					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.AmountToPay = dt.Rows(j).Item("AmountToPay").ToString
				ElseIf AppType = 11 Then

					lstApprovalPerson.ValueDate = dt.Rows(j).Item("ValueDate").ToString
					lstApprovalPerson.RetirementDate = dt.Rows(j).Item("dteDOR")
					lstApprovalPerson.ApprovedAmount = CDbl(dt.Rows(j).Item("ApprovedAmount"))
					lstApprovalPerson.AmountToPay = CDbl(dt.Rows(j).Item("AmountToPay"))
				End If

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

   

     Protected Sub btnPWApprovalDetailOK_Click(sender As Object, e As EventArgs) Handles btnPWApprovalDetailOK.Click

          blnPWApproval = True
          ViewState("ApprovalDetails") = blnPWApproval

     End Sub

     Protected Sub calPWAcknowledgmentDate_SelectionChanged(sender As Object, e As EventArgs) Handles calPWAcknowledgmentDate.SelectionChanged

          Me.PopupControlExtender_calPWAcknowledgmentDate.Commit(Me.calPWAcknowledgmentDate.SelectedDate)
          Me.mpApprovalPW.Show()

     End Sub

     Protected Sub calPWApprovedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calPWApprovedDate.SelectionChanged

          Me.PopupControlExtender_calPWApprovedDate.Commit(Me.calPWApprovedDate.SelectedDate)
          Me.mpApprovalPW.Show()

     End Sub

     


     Protected Sub btnEditPWApprovalOK_Click(sender As Object, e As EventArgs) Handles btnEditPWApprovalOK.Click
          'btnEditPWApprovalOK
          refreshApprovalGrid()
     End Sub

     Protected Sub btnEditAnnApprovalOK_Click(sender As Object, e As EventArgs) Handles btnEditAnnApprovalOK.Click
          refreshApprovalGrid()
     End Sub

     Protected Sub calAnnAcknowledgmentDate_SelectionChanged(sender As Object, e As EventArgs) Handles calAnnAcknowledgmentDate.SelectionChanged

          Me.PopupControlExtender_calAnnAcknowledgmentDate.Commit(Me.calAnnAcknowledgmentDate.SelectedDate)
          mpApprovalAnn.Show()

          'calAnnAcknowledgmentDate
     End Sub

     Protected Sub calAnnApprovedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calAnnApprovedDate.SelectionChanged

          Me.PopupControlExtender_calAnnApprovedDate.Commit(Me.calAnnApprovedDate.SelectedDate)
          mpApprovalAnn.Show()

     End Sub

     Protected Sub btnAnnApprovalDetailOK_Click(sender As Object, e As EventArgs) Handles btnAnnApprovalDetailOK.Click

          blnAnnApproval = True
          ViewState("ApprovalDetails") = blnAnnApproval

     End Sub

     Protected Sub btnEditEnblocApprovedAmountOk_Click(sender As Object, e As EventArgs) Handles btnEditEnblocApprovedAmountOk.Click

          refreshApprovalGrid()
          

     End Sub

	

	Protected Sub btnEditAVCApprovedAmount_Click(sender As Object, e As EventArgs) Handles btnEditAVCApprovedAmount.Click
		refreshApprovalGrid()
	End Sub

	Protected Sub lstApprovalPIN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstApprovalPIN.SelectedIndexChanged

	End Sub
End Class
