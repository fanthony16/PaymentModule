Imports System.Data
Imports MailFilter

Partial Class frmRMAS
	Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable
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

			If IsPostBack = False And Not Context.Request.QueryString("TypeID") Is Nothing Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					getApprovalTypes()
					Dim cr As New Core, ddate As Date
					Me.ddApprovalType.SelectedValue = cr.PMgetApprovalTypebyID(CInt(Context.Request.QueryString("TypeID")))
					ddate = Context.Request.QueryString("DSent")
					Me.txtReportDate.Text = ddate
					populateApplicationList(CInt(Context.Request.QueryString("TypeID")), ddate)
					getUserAccessMenu(Session("user"))

				End If

			ElseIf Context.Request.QueryString("TypeID") Is Nothing And IsPostBack = False Then

				getApprovalTypes()
				getUserAccessMenu(Session("user"))

			ElseIf Context.Request.QueryString("TypeID") Is Nothing And IsPostBack = True Then
				getUserAccessMenu(Session("user"))
			End If

		Catch ex As Exception

		End Try

	End Sub
	Protected Sub BindGrid(dt As DataTable)
		Try

			If dt.Rows.Count > 7 Then
				pnlGrid.Height = Nothing
			Else
				'pnlGrid.Height = 475
			End If

			Me.gridRMAS.DataSource = dt
			Me.gridRMAS.DataBind()

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub BindFieldEnbloc()



		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldNationality As New BoundField()
		bfieldNationality.HeaderText = "Nationality"
		bfieldNationality.DataField = "nationality"
		gridRMAS.Columns.Add(bfieldNationality)

		Dim bfieldGender As New BoundField()
		bfieldGender.HeaderText = "Gender"
		bfieldGender.DataField = "gender"
		gridRMAS.Columns.Add(bfieldGender)

		Dim bfieldDOB As New BoundField()
		bfieldDOB.HeaderText = "Birth Date"
		bfieldDOB.DataField = "birth-date"
		bfieldDOB.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDOB)

		Dim bfieldRetirement As New BoundField()
		bfieldRetirement.HeaderText = "Retirement-date"
		bfieldRetirement.DataField = "retirement-date"
		bfieldRetirement.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirement)

		Dim bfieldreason As New BoundField()
		bfieldreason.HeaderText = "PaymentReason"
		bfieldreason.DataField = "reason-for-payment"
		gridRMAS.Columns.Add(bfieldreason)

		Dim bfieldRSABalance As New BoundField()
		bfieldRSABalance.HeaderText = "RSA Balance"
		bfieldRSABalance.DataField = "rsa-balance"
		bfieldRSABalance.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRSABalance)

		Dim bfieldEnbloc As New BoundField()
		bfieldEnbloc.HeaderText = "Enbloc-payment"
		bfieldEnbloc.DataField = "enbloc-payment"
		bfieldEnbloc.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldEnbloc)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "date-sent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)


	End Sub

	Protected Sub BindFieldPW()

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldBirthDate As New BoundField()
		bfieldBirthDate.HeaderText = "Birth Date"
		bfieldBirthDate.DataField = "birth-date"
		bfieldBirthDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldBirthDate)

		Dim bfieldRetirementDate As New BoundField()
		bfieldRetirementDate.HeaderText = "Retirement Date"
		bfieldRetirementDate.DataField = "retirement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirementDate)

		Dim bfieldAge As New BoundField()
		bfieldAge.HeaderText = "Age"
		bfieldAge.DataField = "retirement-age"
		gridRMAS.Columns.Add(bfieldAge)

		Dim bfieldGender As New BoundField()
		bfieldGender.HeaderText = "Gender"
		bfieldGender.DataField = "gender"
		' bfieldGender.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldGender)

		Dim bfieldRetirementGround As New BoundField()
		bfieldRetirementGround.HeaderText = "Retirement Ground"
		bfieldRetirementGround.DataField = "retirement-ground"
		' bfieldRetirementGround.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRetirementGround)

		Dim bfieldAnnualTotal As New BoundField()
		bfieldAnnualTotal.HeaderText = "Annual Total"
		bfieldAnnualTotal.DataField = "annual-total-emolument"
		' bfieldAnnualTotal.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAnnualTotal)

		Dim bfieldAccruedRight As New BoundField()
		bfieldAccruedRight.HeaderText = "Accrued Right"
		bfieldAccruedRight.DataField = "accrued-right"
		bfieldAccruedRight.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAccruedRight)

		Dim bfieldRSABalance As New BoundField()
		bfieldRSABalance.HeaderText = "RSA Balance"
		bfieldRSABalance.DataField = "rsa-balance"
		bfieldRSABalance.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRSABalance)

		Dim bfieldRecommendedLumpSum As New BoundField()
		bfieldRecommendedLumpSum.HeaderText = "Recommended LumpSum"
		bfieldRecommendedLumpSum.DataField = "recommended-lumpsum"
		bfieldRecommendedLumpSum.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRecommendedLumpSum)

		Dim bfieldMonthDrawnDown As New BoundField()
		bfieldMonthDrawnDown.HeaderText = "Monthly DrawnDow"
		bfieldMonthDrawnDown.DataField = "monthly-programed-drawndown"
		bfieldMonthDrawnDown.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldMonthDrawnDown)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "dateSent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)

	End Sub

	Protected Sub BindFieldNSITF()

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldRetirementDate As New BoundField()
		bfieldRetirementDate.HeaderText = "Retirement Date"
		bfieldRetirementDate.DataField = "retirement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirementDate)

		Dim bfieldInitialAmount As New BoundField()
		bfieldInitialAmount.HeaderText = "Initial Amount Paid Under PRA"
		bfieldInitialAmount.DataField = "initial-amount-paid-under-pra"
		bfieldInitialAmount.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldInitialAmount)

		Dim bfieldAmountRecieved As New BoundField()
		bfieldAmountRecieved.HeaderText = "Amount Recieved Nsitf to RSA"
		bfieldAmountRecieved.DataField = "amount-recieved-nsitf-to-rsa"
		bfieldAmountRecieved.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAmountRecieved)

		Dim bfieldAmountRequested As New BoundField()
		bfieldAmountRequested.HeaderText = "Amount-Requested-Under-Nsitf-From-RSA"
		bfieldAmountRequested.DataField = "amount-requested-under-nsitf-from-rsa"
		bfieldAmountRequested.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAmountRequested)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "dateSent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)

	End Sub



	Protected Sub BindFieldDBA()

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin-dba"
		gridRMAS.Columns.Add(bfieldPIN)


		Dim bfieldName As New BoundField()
		bfieldName.HeaderText = "Name"
		bfieldName.DataField = "name"
		gridRMAS.Columns.Add(bfieldName)



		Dim bfieldGender As New BoundField()
		bfieldGender.HeaderText = "Gender"
		bfieldGender.DataField = "gender"
		gridRMAS.Columns.Add(bfieldGender)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldRetirementDate As New BoundField()
		bfieldRetirementDate.HeaderText = "Retirement Date"
		bfieldRetirementDate.DataField = "retirement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirementDate)

		Dim bfieldDeathDate As New BoundField()
		bfieldDeathDate.HeaderText = "Death Date"
		bfieldDeathDate.DataField = "death-date"
		bfieldDeathDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDeathDate)

		Dim bfieldAdminIssuingAuth As New BoundField()
		bfieldAdminIssuingAuth.HeaderText = "Admin. Issuing Authority"
		bfieldAdminIssuingAuth.DataField = "administration-letter-issuing-authority"
		gridRMAS.Columns.Add(bfieldAdminIssuingAuth)

		Dim bfieldAdminIssueDate As New BoundField()
		bfieldAdminIssueDate.HeaderText = "Admin. Issue Date"
		bfieldAdminIssueDate.DataField = "administration-letter-date"
		bfieldAdminIssueDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldAdminIssueDate)


		Dim bfieldAdminNOK As New BoundField()
		bfieldAdminNOK.HeaderText = "Admin NOK"
		bfieldAdminNOK.DataField = "administration-letter-named-administrator-nok"
		gridRMAS.Columns.Add(bfieldAdminNOK)

		Dim bfieldInsuranceProceed As New BoundField()
		bfieldInsuranceProceed.HeaderText = "Insurance Proceed"
		bfieldInsuranceProceed.DataField = "life-insurance-proceed"
		bfieldInsuranceProceed.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldInsuranceProceed)

		Dim bfieldAccruedRight As New BoundField()
		bfieldAccruedRight.HeaderText = "Accrued Right"
		bfieldAccruedRight.DataField = "accured-right"
		bfieldAccruedRight.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAccruedRight)

		Dim bfieldContribution As New BoundField()
		bfieldContribution.HeaderText = "Contribution"
		bfieldContribution.DataField = "contributions"
		bfieldContribution.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldContribution)

		Dim bfieldInvestmentIncome As New BoundField()
		bfieldInvestmentIncome.HeaderText = "InvestmentIncome"
		bfieldInvestmentIncome.DataField = "investment-income"
		bfieldInvestmentIncome.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldInvestmentIncome)

		Dim bfieldRSABalance As New BoundField()
		bfieldRSABalance.HeaderText = "RSA Balance"
		bfieldRSABalance.DataField = "total-rsa-balance"
		bfieldRSABalance.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRSABalance)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "dateSent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)

	End Sub



	Protected Sub BindFieldAnnuity()

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldBirthDate As New BoundField()
		bfieldBirthDate.HeaderText = "Birth Date"
		bfieldBirthDate.DataField = "birth-date"
		bfieldBirthDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldBirthDate)

		Dim bfieldRetirementDate As New BoundField()
		bfieldRetirementDate.HeaderText = "Retirement Date"
		bfieldRetirementDate.DataField = "retirement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirementDate)


		Dim bfieldInsuranceCoy As New BoundField()
		bfieldInsuranceCoy.HeaderText = "Insurance Company"
		bfieldInsuranceCoy.DataField = "insurance-company-name"
		gridRMAS.Columns.Add(bfieldInsuranceCoy)

		Dim bfieldCommencementDate As New BoundField()
		bfieldCommencementDate.HeaderText = "Commencement Date"
		bfieldCommencementDate.DataField = "annuity-commencement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldCommencementDate)

		Dim bfieldAnnualTotal As New BoundField()
		bfieldAnnualTotal.HeaderText = "Annual Total"
		bfieldAnnualTotal.DataField = "annual-total-emolument"
		bfieldAnnualTotal.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAnnualTotal)

		Dim bfieldRSABalance As New BoundField()
		bfieldRSABalance.HeaderText = "RSA Balance"
		bfieldRSABalance.DataField = "rsa-balance"
		bfieldRSABalance.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRSABalance)

		Dim bfieldPremium As New BoundField()
		bfieldPremium.HeaderText = "Premium"
		bfieldPremium.DataField = "premium"
		bfieldPremium.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldPremium)

		Dim bfieldLumpSum As New BoundField()
		bfieldLumpSum.HeaderText = "LumpSum"
		bfieldLumpSum.DataField = "lumpsum"
		bfieldLumpSum.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldLumpSum)

		Dim bfieldMonthlyAnnuity As New BoundField()
		bfieldMonthlyAnnuity.HeaderText = "Monthly Annuity"
		bfieldMonthlyAnnuity.DataField = "monthly-annuity"
		bfieldMonthlyAnnuity.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldMonthlyAnnuity)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "dateSent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)

	End Sub

	Protected Sub BindFieldAVC()

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldBirthDate As New BoundField()
		bfieldBirthDate.HeaderText = "Birth Date"
		bfieldBirthDate.DataField = "birth-date"
		bfieldBirthDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldBirthDate)

		Dim bfieldRetirementDate As New BoundField()
		bfieldRetirementDate.HeaderText = "Retirement Date"
		bfieldRetirementDate.DataField = "retirement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirementDate)

		Dim bfieldTotalAVC As New BoundField()
		bfieldTotalAVC.HeaderText = "Total AVC"
		bfieldTotalAVC.DataField = "total-voluntary-contribution"
		bfieldTotalAVC.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldTotalAVC)

		Dim bfieldTotalAmount As New BoundField()
		bfieldTotalAmount.HeaderText = "Total Amount"
		bfieldTotalAmount.DataField = "total-amount"
		bfieldTotalAmount.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldTotalAmount)

		Dim bfieldTotalRequested As New BoundField()
		bfieldTotalRequested.HeaderText = "Total Amount"
		bfieldTotalRequested.DataField = "amount-requested"
		bfieldTotalRequested.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldTotalRequested)

		Dim bfieldTotalTax As New BoundField()
		bfieldTotalTax.HeaderText = "Tax-Deducted"
		bfieldTotalTax.DataField = "tax-deducted"
		bfieldTotalTax.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldTotalTax)

		Dim bfieldAVCPayable As New BoundField()
		bfieldAVCPayable.HeaderText = "Net AVC"
		bfieldAVCPayable.DataField = "amount-payable-net-tax"
		bfieldAVCPayable.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldAVCPayable)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "date-sent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)

	End Sub

	Protected Sub BindFieldLegacy()

		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldRetirementDate As New BoundField()
		bfieldRetirementDate.HeaderText = "Retirement Date"
		bfieldRetirementDate.DataField = "retirement-date"
		bfieldRetirementDate.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldRetirementDate)

		Dim bfieldEnblocPayment As New BoundField()
		bfieldEnblocPayment.HeaderText = "Enbloc Payment"
		bfieldEnblocPayment.DataField = "enbloc-payment"
		bfieldEnblocPayment.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldEnblocPayment)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "date-sent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)


	End Sub

	Protected Sub BindFieldHardShip()



		Dim bfieldPIN As New BoundField()
		bfieldPIN.HeaderText = "PIN"
		bfieldPIN.DataField = "pin"
		gridRMAS.Columns.Add(bfieldPIN)

		Dim bfieldEmpCode As New BoundField()
		bfieldEmpCode.HeaderText = "Employer Code"
		bfieldEmpCode.DataField = "employer-code"
		bfieldEmpCode.ItemStyle.Width = 150
		gridRMAS.Columns.Add(bfieldEmpCode)

		Dim bfieldGender As New BoundField()
		bfieldGender.HeaderText = "Gender"
		bfieldGender.DataField = "gender"
		gridRMAS.Columns.Add(bfieldGender)

		Dim bfieldDOB As New BoundField()
		bfieldDOB.HeaderText = "Birth Date"
		bfieldDOB.DataField = "birth-date"
		bfieldDOB.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDOB)

		Dim bfieldDisengagement As New BoundField()
		bfieldDisengagement.HeaderText = "Disengagement Date"
		bfieldDisengagement.DataField = "disengagement-date"
		bfieldDisengagement.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDisengagement)

		Dim bfieldRSABalance As New BoundField()
		bfieldRSABalance.HeaderText = "RSA Balance"
		bfieldRSABalance.DataField = "rsa-balance"
		bfieldRSABalance.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfieldRSABalance)

		Dim bfield25Percent As New BoundField()
		bfield25Percent.HeaderText = "Twenty Percent"
		bfield25Percent.DataField = "twentyfive-percent-rsa-balance"
		bfield25Percent.DataFormatString = "{0:N}"
		gridRMAS.Columns.Add(bfield25Percent)

		Dim bfieldDateSent As New BoundField()
		bfieldDateSent.HeaderText = "Date Sent"
		bfieldDateSent.DataField = "date-sent"
		bfieldDateSent.DataFormatString = "{0:d}"
		gridRMAS.Columns.Add(bfieldDateSent)

		' Dim tfield As New TemplateField()
		'tfield.HeaderText = "Country"
		'gridRMAS.Columns.Add(tfield)

		' tfield = New TemplateField()
		' tfield.HeaderText = "Confirm"
		' gridRMAS.Columns.Add(tfield)
	End Sub

	Protected Sub gridRMAS_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("RMAS")) = False Then

			Dim dt As DataTable = ViewState("RMAS")
			If e.Row.RowType = DataControlRowType.DataRow Then


				If CBool(dt.Rows(e.Row.RowIndex).Item("blnConfirm")) = False Then

					e.Row.ForeColor = System.Drawing.Color.Red

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("blnConfirm")) = True Then

					e.Row.ForeColor = System.Drawing.Color.Green

					Dim cb As CheckBox = TryCast(e.Row.FindControl("ChkRMASApproval"), CheckBox)
					cb.Enabled = False

					'e.Row.Enabled = False

				End If

			End If
		Else
		End If

		'If e.Row.RowType = DataControlRowType.DataRow Then
		'     Dim chk As New CheckBox
		'     chk.ID = "ChkRMASApproval"
		'     'chk.Text = "View"
		'     'AddHandler lnkView.Click, AddressOf ViewDetails
		'     'lnkView.CommandArgument = TryCast(e.Row.DataItem, DataRowView).Row("Id").ToString()
		'     e.Row.Cells(8).Controls.Add(chk)
		'End If


	End Sub

	Protected Sub getApplicationSchedule(typeID As Integer, ddate As Date)
		Dim cr As New Core, dt As New DataTable

		Try

			dt = cr.PMgetRMASScheduleTpye(typeID, ddate)
			Me.BindGrid(dt)
			' Me.gridRMAS.DataSource = dt
			' gridRMAS.DataBind()
		Catch ex As Exception

		End Try

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

	Private Sub populateApplicationList(AppTypeID As Integer, rdate As Date)


		If Me.gridRMAS.Columns.Count = 2 And AppTypeID = 2 Then
			BindFieldHardShip()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 1 Then
			BindFieldEnbloc()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 16 Then
			BindFieldEnbloc()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 8 Then
			BindFieldLegacy()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 7 Then
			BindFieldAVC()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 3 Then
			BindFieldPW()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 14 Then
			BindFieldPW()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 4 Then
			BindFieldAnnuity()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 15 Then
			BindFieldAnnuity()

		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 5 Then
			BindFieldDBA()
		ElseIf Me.gridRMAS.Columns.Count = 2 And AppTypeID = 6 Then
			BindFieldNSITF()

		End If


		Dim cr As New Core, dt As New DataTable
		ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		dt = cr.PMgetRMASData("", CInt(AppTypeID), rdate)
		ViewState("RMAS") = dt
		BindGrid(dt)

	End Sub

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim cr As New Core, dt As New DataTable
		ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		populateApplicationList(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), CDate(Me.txtReportDate.Text))

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

			Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkRMASApproval"), CheckBox)
			cb.Checked = False

		Next

	End Sub

	Protected Sub btnHardShipProcessingBatch_Click(sender As Object, e As EventArgs) Handles btnHardShipProcessingBatch.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable, lstSchedules As New List(Of RMASSchedule)

		Try


			For Each grow As GridViewRow In Me.gridRMAS.Rows

				cb = grow.FindControl("ChkRMASApproval")

				If cb.Checked = True Then

					cr = New Core
					ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
					cr.PMComfirmRMASSchedule(grow.Cells(2).Text.ToString(), CDate(Me.txtReportDate.Text), CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), Session("user"))

				ElseIf cb.Checked = False Then

				End If

			Next

			Refresh()


		Catch ex As Exception

		End Try

	End Sub

	Protected Sub calReportDate_SelectionChanged(sender As Object, e As EventArgs) Handles calReportDate.SelectionChanged

		Me.calReportDate_PopupControlExtender.Commit(Me.calReportDate.SelectedDate)

	End Sub


	Private Sub Refresh()

		If Me.ddApprovalType.SelectedItem.Text <> "" And txtReportDate.Text <> "" Then

			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			populateApplicationList(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), CDate(Me.txtReportDate.Text))


		Else

		End If

	End Sub

	Protected Sub gridRMAS_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridRMAS.PageIndexChanging

		If IsNothing(ViewState("RMAS")) = False Then

			Dim dt As New DataTable
			Me.gridRMAS.PageIndex = e.NewPageIndex
			dt = ViewState("RMAS")
			BindGrid(dt)

		Else
		End If

	End Sub

	Protected Sub gridRMAS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridRMAS.SelectedIndexChanged

		Dim cr As New Core, dt As New DataTable
		ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		populateApplicationList(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), CDate(Me.txtReportDate.Text))

		If CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 2 Then

			Dim selectedRowIndex As Integer
			selectedRowIndex = Me.gridRMAS.SelectedRow.RowIndex
			Dim row As GridViewRow = gridRMAS.Rows(selectedRowIndex)
			Me.txtHardApplicationCode.Text = row.Cells(2).Text.ToString
			Me.txtRSABalance.Text = row.Cells(7).Text.ToString
			Me.txtHardShipAmount.Text = row.Cells(8).Text.ToString

			mpHardShipOverrideExtender.Show()

		Else

		End If


	End Sub




	Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable, lstSchedules As New List(Of RMASSchedule)

		Try


			'For Each grow As GridViewRow In Me.gridRMAS.Rows

			'	cb = grow.FindControl("ChkRMASApproval")

			'	If cb.Checked = True Then

			'		cr = New Core
			'		ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			'		cr.PMReCallRMASSchedule(grow.Cells(1).Text.ToString(), Now.Date, CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), Session("user"))

			'	ElseIf cb.Checked = False Then

			'	End If

			'Next

			Dim selectedRowIndex As Integer
			selectedRowIndex = Me.gridRMAS.SelectedRow.RowIndex
			Dim row As GridViewRow = gridRMAS.Rows(selectedRowIndex)
			cr = New Core

			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			cr.PMReCallRMASSchedule(row.Cells(2).Text.ToString(), Now.Date, CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)), Session("user"))

			Refresh()


		Catch ex As Exception

		End Try

	End Sub

	Protected Sub AddViewIACComment_Click(sender As Object, e As EventArgs) Handles btnShowIACCommentPopup.Click

		Dim btnAddViewIACComment As New ImageButton
		btnAddViewIACComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewIACComment.NamingContainer

		'MsgBox("" & Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text.ToString)

		Me.txtIACApplicationID.Text = Me.gridRMAS.Rows(i.RowIndex).Cells(2).Text
		Me.txtApplicationIACComment.Text = cr.PMgetApplicationComment(Me.gridRMAS.Rows(i.RowIndex).Cells(2).Text, "PRE_IC").Rows(0).Item("txtComment").ToString

		'pops up the comment dialogue
		mpAppIACComments.Show()

	End Sub

	Private Function convertDate(str As String) As Boolean
		Try
			Dim dt As Date

			dt = CDate(str)
			Return True

		Catch ex As Exception

			Return False

		End Try
	End Function

	Protected Sub btnRMASSMS_Click(sender As Object, e As EventArgs) Handles btnRMASSMS.Click

		Dim mf As New MailFliter, lstSub As New List(Of String), dateCount As Integer = 0, cr As New Core
		'Try

		'mf.Usernames = ConfigurationManager.AppSettings("mailfilterUser")
		'mf.Passwords = ConfigurationManager.AppSettings("mailfilterPW")
		'mf.Domains = ConfigurationManager.AppSettings("mailfilterDomains")
		'mf.Urls = ConfigurationManager.AppSettings("mailfilterUrls")
		'mf.subject = ConfigurationManager.AppSettings("mailfiltersubject")
		'mf.texts = ConfigurationManager.AppSettings("mailfiltertexts")
		'mf.day = (-1 * 7)


		mf.Usernames = "rmassms"
		mf.Passwords = "bonus+3aa"
		mf.Domains = "pensure-nigeria.com"
		mf.Urls = "rmassms@leadway-pensure.com"
		mf.subject = "Daily"
		mf.texts = "validated successfully"
		mf.day = (-1 * 7)


		lstSub = mf.getValidationFeedback



		Dim i As Integer = 0

		Try

			Do While i < lstSub.Count
				Dim stringSeparators() As String = {"As and When"}

				Dim results() As String = lstSub(i).Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)
				For Each result In results

					If convertDate(result) = True Then

						If cr.PMIsRMASSMSSent(CDate(result), Server.MapPath("~/Logs")) = False And CDate(result) > CDate("2016-08-19") Then

							cr.PMPaymentModuleRMASSMSInsert(CDate(result), Server.MapPath("~/Logs"))

							dateCount = dateCount + 1
						Else
							' Exit Sub
						End If
					Else
					End If

				Next
				i = i + 1
			Loop

			'PaymentModuleRMASSMSInsert("2017-04-12")

			If dateCount > 0 Then

				Dim logerr As New Global.Logger.Logger
				logerr.FileSource = "Sure Pay RMAS SMS"
				logerr.FilePath = Server.MapPath("~/Logs")
				logerr.Logger("SMS Inserted Successful")
				spnMessage.InnerText = "RMAS SMS Submitted Successfully"
				pnlMessage.Visible = True
			Else

				Dim logerr As New Global.Logger.Logger
				logerr.FileSource = "Sure Pay RMAS SMS"
				logerr.FilePath = Server.MapPath("~/Logs")
				logerr.Logger("No Record Found")
				spnMessage.InnerText = "No Record Found"
				pnlMessage.Visible = True
			End If


		Catch ex As Exception

			Dim loger As New Global.Logger.Logger
			loger.FileSource = "Payment Module RMAS SMS - "
			loger.FilePath = Server.MapPath("~/Logs")
			loger.Logger(ex.StackTrace & " | " & "Location => SurePay RMAS SMS()")

		End Try


	End Sub

	Protected Sub btnOverrideAmount_Click(sender As Object, e As EventArgs) Handles btnOverrideAmount.Click


		Try

			Dim cr As New Core
			cr.PMUpdateHardShipRMASData(Me.txtRSABalance.Text.Replace(",", ""), Me.txtHardShipAmount.Text.Replace(",", ""), Me.txtHardApplicationCode.Text, Session("user"))

		Catch ex As Exception

		End Try
		


	End Sub
End Class
