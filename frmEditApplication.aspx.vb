Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Partial Class frmEditApplication
     Inherits System.Web.UI.Page
     Dim DocumentCollection As New Hashtable
     Dim ApprovalTypeCollection As New Hashtable
     Dim BankTypeCollection As New Hashtable
     Dim BankBranchCollection As New Hashtable
     Dim EmployerHistoryCollection As New Hashtable
     Dim lstRecievedDoc As New ArrayList
     Dim dtDocuments As New DataTable
	Dim dtColumn As New DataColumn
     Dim blnPW As Boolean = False
     Dim blnHardShip As Boolean = False
     Dim blnAnnuity As Boolean = False
     Dim blnDBOk As Boolean = False
	Dim blnNSITF As Boolean = False
	Dim blnAVC As Boolean = False
	Dim blnLegacy As Boolean = False


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

               Do While i < EmployerHistoryCollection.Count

                    Dim newCustomersRow As DataRow
                    newCustomersRow = dtEmployerHistory.NewRow()

                    newCustomersRow("employerName") = EmployerHistoryCollection.Keys(i)
                    newCustomersRow("EmployerID") = EmployerHistoryCollection.Item(EmployerHistoryCollection.Keys(i))
                    newCustomersRow("EmployerCode") = Me.gridCustomerHistory.Rows(i).Cells(3).Text

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

     Protected Sub BtnNewDetails_Click(sender As Object, e As EventArgs) Handles btnShowPopup.Click

          ' ViewState("ID") = 0
          '   mpARLDetail.Show()
          '

     End Sub

     Protected Sub gridCustomerHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)

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


     Protected Sub btnRemoveDocument_Click(sender As Object, e As EventArgs) Handles btnRemoveDocument.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, aryIndex As New ArrayList


          For Each grow As GridViewRow In Me.gridRecievedDocument.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")

               If cb.Checked = True Then

				chk = chk + 1
				aryIndex.Add(grow.RowIndex)

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
                         dtDocuments.Rows(grow.RowIndex).Delete()
                         ViewState("RecievedDocument") = dtDocuments
                         loadGrid(dtDocuments)

                    ElseIf cb.Checked = False Then

                    End If

               Next
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

			Exit Sub
		Else

			dvDocumentError.Visible = False
			Exit Sub
          End If

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

               Dim scriptManagerA As New ScriptManager
               scriptManagerA = ScriptManager.GetCurrent(Me.Page)
               scriptManagerA.RegisterPostBackControl(Me.gridRecievedDocument)

               If IsPostBack = False Then

                    If IsNothing(Session("user")) = True Then
                         Response.Redirect("Login.aspx")
                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

                         getUserAccessMenu(Session("user"))
                         getStates()
                         populateBank()
                         getApprovalTypes()
                         PopulateApplicationStatus()
                         PopulateFundingStatus()
                         PopulateCommentGroup()
                         Me.txtRecievedDate.Text = Now.Date

                         If Not Context.Request.QueryString("ApplicationCode") Is Nothing And Not Context.Request.QueryString("ReadOnly") Is Nothing Then
						Dim myState As New States, myLGA As New LGA, dt As DataTable, dtt As DataTable, dtRDetails As DataTable, dtDBDetails As DataTable, appCode As String, appID As Integer = 0, cr As New Core, editStatus As Integer
						'retrieving parameter values from previous page
                              editStatus = Context.Request.QueryString("ReadOnly")
                              appCode = Context.Request.QueryString("ApplicationCode") 'ApplicationTypeID
                              ViewState("appID") = Context.Request.QueryString("ApplicationTypeID")
						ViewState("ReturnPage") = Context.Request.QueryString("ReturnPage")
						ViewState("PIN") = Context.Request.QueryString("PIN")

                              Me.populateExitReasons(CInt(ViewState("appID")))
                              ViewState("appCode") = appCode

						If editStatus = 1 Then

							imgBack.Visible = True
							Me.btnSubmit.Visible = False
							Me.btnRemoveDocument.Visible = False
							Me.btnEmployerHistory.Visible = False
							btnRemoveRecievedDoc.Visible = False
							Me.ddBankName.Enabled = False
							Me.ddBankBranch.Enabled = False
							Me.ddApplicationType.Enabled = False
							flReqDocUpload.Visible = False
							dvUploadLabel.Visible = False


							spCaption.InnerText = spCaption.InnerText & " ID : " & Context.Request.QueryString("ApplicationCode") & " IN READ ONLY MODE"
						Else

							spCaption.InnerText = spCaption.InnerText & " ID : " & Context.Request.QueryString("ApplicationCode") & " IN EDIT MODE"

						End If


						dt = cr.PMgetApplicationByCode(appCode)
						ViewState("applicationDetails") = dt
						'ViewState("ReturnPage")  ViewState("applicationDetails") = dt
                              dtRDetails = cr.PMGetPWAnnuityDetails(appCode)
                              dtDBDetails = cr.PMGetDeathDetails(appCode)


						imgPassport.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", dt.Rows(0).Item("txtPIN").ToString, 1, Server.MapPath("~/Logs"))

                              imgSignature.ImageUrl = String.Format("ShowPassportImage.ashx?sToolGUID={0}&Gridid={1}&LogLocation={2}", dt.Rows(0).Item("txtPIN").ToString, 2, Server.MapPath("~/Logs"))



                              Me.txtPIN.Text = dt.Rows(0).Item("txtPIN").ToString
                              Session("PIN") = dt.Rows(0).Item("txtPIN").ToString
                              'retrieving application for edit
                              dtt = cr.getPMPersonInformation(Me.txtPIN.Text)

                              'txtTIN

                              If dtDBDetails.Rows.Count > 0 Then

                                   Me.txtDBARetirementDate.Text = Convert.ToDateTime(dtDBDetails.Rows(0).Item("dteRetirement")).ToString("dd/MM/yyyy")
                                   'dd/MM/yyyy
                                   Me.txtDBADeathDate.Text = Convert.ToDateTime(dtDBDetails.Rows(0).Item("dteDeath")).ToString("dd/MM/yyyy")
                                   Me.txtAdminLetterIssuer.Text = dtDBDetails.Rows(0).Item("txtAdminLetterAuthority").ToString()
                                   Me.txtAdminLetterDate.Text = Convert.ToDateTime(dtDBDetails.Rows(0).Item("dteAdminLetter")).ToString("dd/MM/yyyy")
                                   Me.txtDBValueDate.Text = Convert.ToDateTime(dtDBDetails.Rows(0).Item("dtePriceDate")).ToString("dd/MM/yyyy")
                                   Me.txtDBAAdminNOK.Text = dtDBDetails.Rows(0).Item("txtAdminNOK").ToString()
                                   Me.txtInsuranceProceed.Text = dtDBDetails.Rows(0).Item("numInsuranceProceed").ToString()
                                   Me.txtDBAAccruedRight.Text = dtDBDetails.Rows(0).Item("numAccruedRight").ToString()
                                   Me.txtDBAContribution.Text = dtDBDetails.Rows(0).Item("numContribution").ToString()
                                   Me.txtDBAInvestmentIncome.Text = dtDBDetails.Rows(0).Item("numInvestmentIncome").ToString()
                                   Me.txtDBARSABalance.Text = dtDBDetails.Rows(0).Item("numRSABalance").ToString()

                              End If

						'retrieving annuity or programme withdrawal retirement details
						If dtRDetails.Rows.Count > 0 Then
							If CInt(dtRDetails.Rows(0).Item("fkiAppTypeId")) = 3 Then

								Me.txtBasicSalaryPW.Text = dtRDetails.Rows(0).Item("numBasicSalary").ToString
								Me.txtHouseRent.Text = dtRDetails.Rows(0).Item("numHouseRent").ToString
								Me.txtTransport.Text = dtRDetails.Rows(0).Item("numTransport").ToString
								Me.txtUtility.Text = dtRDetails.Rows(0).Item("numUtility").ToString

								Me.txtConsolidatedAllowance.Text = dtRDetails.Rows(0).Item("numConsolidatedAallowance").ToString
								Me.txtConsolidatedSalary.Text = dtRDetails.Rows(0).Item("numConsolidatedSalary").ToString
								Me.txtMonthTotal.Text = dtRDetails.Rows(0).Item("numMonthlyTotal").ToString
								Me.txtAnnualTotalEmolument.Text = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj").ToString

								Me.txtValueDate.Text = CDate(dtRDetails.Rows(0).Item("dtePriceDate")).ToString("yyyy/MM/dd")
								Me.txtAccruedRightPW.Text = dtRDetails.Rows(0).Item("numAccruedRight").ToString
								Me.txtRSABalancePW.Text = dtRDetails.Rows(0).Item("numRSABalance").ToString
								Me.txtRecommendeLumpSum.Text = dtRDetails.Rows(0).Item("numRecommendedLumpSum").ToString
								Me.txtMonthlyDrawDown.Text = dtRDetails.Rows(0).Item("numMonthlyDrowDown").ToString
								Me.txtDORPW.Text = CDate(dt.Rows(0).Item("dteDOR")).ToString("yyyy/MM/dd")

							ElseIf CInt(dtRDetails.Rows(0).Item("fkiAppTypeId")) = 14 Then

								Me.txtBasicSalaryPW.Text = dtRDetails.Rows(0).Item("numBasicSalary").ToString
								Me.txtHouseRent.Text = dtRDetails.Rows(0).Item("numHouseRent").ToString
								Me.txtTransport.Text = dtRDetails.Rows(0).Item("numTransport").ToString
								Me.txtUtility.Text = dtRDetails.Rows(0).Item("numUtility").ToString

								Me.txtConsolidatedAllowance.Text = dtRDetails.Rows(0).Item("numConsolidatedAallowance").ToString
								Me.txtConsolidatedSalary.Text = dtRDetails.Rows(0).Item("numConsolidatedSalary").ToString
								Me.txtMonthTotal.Text = dtRDetails.Rows(0).Item("numMonthlyTotal").ToString
								Me.txtAnnualTotalEmolument.Text = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj").ToString

								Me.txtValueDate.Text = CDate(dtRDetails.Rows(0).Item("dtePriceDate")).ToString("yyyy/MM/dd")
								Me.txtAccruedRightPW.Text = dtRDetails.Rows(0).Item("numAccruedRight").ToString
								Me.txtRSABalancePW.Text = dtRDetails.Rows(0).Item("numRSABalance").ToString
								Me.txtRecommendeLumpSum.Text = dtRDetails.Rows(0).Item("numRecommendedLumpSum").ToString
								Me.txtMonthlyDrawDown.Text = dtRDetails.Rows(0).Item("numMonthlyDrowDown").ToString

							ElseIf CInt(dtRDetails.Rows(0).Item("fkiAppTypeId")) = 4 Then

								Me.txtBasicSalaryAnnuity.Text = dtRDetails.Rows(0).Item("numBasicSalary").ToString
								Me.txtHouseRentAnnuity.Text = dtRDetails.Rows(0).Item("numHouseRent").ToString
								Me.txtTransportAnnuity.Text = dtRDetails.Rows(0).Item("numTransport").ToString
								Me.txtUtilityAnnuity.Text = dtRDetails.Rows(0).Item("numUtility").ToString

								Me.txtConsolidatedAllowanceAnnuity.Text = dtRDetails.Rows(0).Item("numConsolidatedAallowance").ToString
								Me.txtConsolidatedSalaryAnnuity.Text = dtRDetails.Rows(0).Item("numConsolidatedSalary").ToString
								Me.txtMonthTotalAnnuity.Text = dtRDetails.Rows(0).Item("numMonthlyTotal").ToString
								Me.txtAnnualTotalEmolumentAnnuity.Text = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj").ToString
								Me.txtValueDateAnnuity.Text = CDate(dtRDetails.Rows(0).Item("dtePriceDate")).ToString("yyyy/MM/dd")

								Me.txtCommencmentDate.Text = CDate(dtRDetails.Rows(0).Item("dteAnnuityCcommencementDate")).ToString("yyyy/MM/dd")
								Me.txtInsuranceCoy.Text = dtRDetails.Rows(0).Item("txtInsuranceCompanyName").ToString
								Me.txtRSABalanceAnnuity.Text = dtRDetails.Rows(0).Item("numRSABalance").ToString
								Me.txtPremium.Text = dtRDetails.Rows(0).Item("numPremium").ToString

								Me.txtLumpSum.Text = dtRDetails.Rows(0).Item("numLumpSum").ToString
								Me.txtMonthlyAnnuity.Text = dtRDetails.Rows(0).Item("numMonthlyAnnuity").ToString

							ElseIf CInt(dtRDetails.Rows(0).Item("fkiAppTypeId")) = 15 Then

								Me.txtBasicSalaryAnnuity.Text = dtRDetails.Rows(0).Item("numBasicSalary").ToString
								Me.txtHouseRentAnnuity.Text = dtRDetails.Rows(0).Item("numHouseRent").ToString
								Me.txtTransportAnnuity.Text = dtRDetails.Rows(0).Item("numTransport").ToString
								Me.txtUtilityAnnuity.Text = dtRDetails.Rows(0).Item("numUtility").ToString

								Me.txtConsolidatedAllowanceAnnuity.Text = dtRDetails.Rows(0).Item("numConsolidatedAallowance").ToString
								Me.txtConsolidatedSalaryAnnuity.Text = dtRDetails.Rows(0).Item("numConsolidatedSalary").ToString
								Me.txtMonthTotalAnnuity.Text = dtRDetails.Rows(0).Item("numMonthlyTotal").ToString
								Me.txtAnnualTotalEmolumentAnnuity.Text = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj").ToString
								Me.txtValueDateAnnuity.Text = CDate(dtRDetails.Rows(0).Item("dtePriceDate")).ToString("yyyy/MM/dd")

								Me.txtCommencmentDate.Text = CDate(dtRDetails.Rows(0).Item("dteAnnuityCcommencementDate")).ToString("yyyy/MM/dd")
								Me.txtInsuranceCoy.Text = dtRDetails.Rows(0).Item("txtInsuranceCompanyName").ToString
								Me.txtRSABalanceAnnuity.Text = dtRDetails.Rows(0).Item("numRSABalance").ToString
								Me.txtPremium.Text = dtRDetails.Rows(0).Item("numPremium").ToString

								Me.txtLumpSum.Text = dtRDetails.Rows(0).Item("numLumpSum").ToString
								Me.txtMonthlyAnnuity.Text = dtRDetails.Rows(0).Item("numMonthlyAnnuity").ToString

							End If


						Else

						End If





						If dt.Rows.Count > 0 Then
							ViewState("Employerid") = dt.Rows(0).Item("fkiEmployerID").ToString
							ViewState("EmployeeID") = dtt.Rows(0).Item("employeeid").ToString
							ViewState("EmployerCode") = dt.Rows(0).Item("txtEmployerCode").ToString
							ViewState("Sector") = dt.Rows(0).Item("txtSector")

							If CBool(dt.Rows(0).Item("IsPassportConfirmed")) = True Then
								Me.chkConfirmedPassport.Checked = True
							ElseIf CBool(dt.Rows(0).Item("IsPassportConfirmed")) = False Then
								Me.chkConfirmedPassport.Checked = False
							Else
							End If


							If CBool(dt.Rows(0).Item("IsSignatureConfirmed")) = True Then
								Me.chkConfirmedSignature.Checked = True
							ElseIf CBool(dt.Rows(0).Item("IsSignatureConfirmed")) = False Then
								Me.chkConfirmedSignature.Checked = False
							Else
							End If


							Me.txtInitialAmountPaid.Text = dt.Rows(0).Item("numNSITFInitialAmountPaid").ToString
							Me.txtAmountRecievedToRSANSITF.Text = dt.Rows(0).Item("numNSITFRecievedToRSA").ToString
							Me.txtAmountRequestedFromRSANSITF.Text = dt.Rows(0).Item("numNSITFRequestedToRSA").ToString


							Me.txtNOKNo.Text = dtt.Rows(0).Item("NOKPhone").ToString
							Me.txtSurname.Text = dt.Rows(0).Item("txtFullName").ToString.Split("|")(0).Trim
							Me.txtFirstName.Text = dt.Rows(0).Item("txtFullName").ToString.Split("|")(1).Trim
							Me.ddFundingStatus.SelectedValue = dt.Rows(0).Item("txtFundStatus").ToString

							If dt.Rows(0).Item("txtFullName").ToString.Split("|").Count > 2 Then
								Me.txtOtherNames.Text = dt.Rows(0).Item("txtFullName").ToString.Split("|")(2).Trim
							Else
								Me.txtOtherNames.Text = ""
							End If

							Me.txtDOB.Text = CDate(dt.Rows(0).Item("dteDOB"))

							Me.txtAge.Text = DateDiff(DateInterval.Year, dt.Rows(0).Item("dteDOB"), Now.Date)

							If CInt(dt.Rows(0).Item("intDeclaredAge")) <> 0 Then
								Me.txtDelaredAge.Text = dt.Rows(0).Item("intDeclaredAge")
							Else
								Me.txtDelaredAge.Text = DateDiff(DateInterval.Year, dt.Rows(0).Item("dteDOB"), Now.Date)
							End If

							Me.txtEmployer.Text = dt.Rows(0).Item("txtEmployerName")
							Me.txtSex.Text = dt.Rows(0).Item("txtSex").ToString

							Me.txtApplicationDate.Text = CDate(dt.Rows(0).Item("dteApplicationDate"))

							Me.ddApplicationType.SelectedValue = dt.Rows(0).Item("TypeName").ToString
							Me.ddApplicationState.SelectedValue = UCase(dt.Rows(0).Item("txtApplicationState").ToString)

							If (dt.Rows(0).Item("txtApplicationState").ToString = dt.Rows(0).Item("txtApplicationOffice").ToString) = True Then

							Else
								Me.ddPFAOfficeLocation.Items.Add(dt.Rows(0).Item("txtApplicationOffice").ToString)
							End If


							Me.txtNOK.Text = dtt.Rows(0).Item("NOK")
							Me.txtEmail.Text = dtt.Rows(0).Item("email").ToString
							Me.txtPhone.Text = dtt.Rows(0).Item("Phone").ToString
							Me.txtOfficeAddress.Text = dtt.Rows(0).Item("OfficeAddress")


							If Not dtt.Rows(0).Item("OfficeStateID") Is DBNull.Value Then

								Me.txtOfficeState.Text = myState.getStateName(CInt(dtt.Rows(0).Item("OfficeStateID"))).ToString
							Else
								Me.txtOfficeState.Text = myState.getStateName(0).ToString

							End If

							If Not dtt.Rows(0).Item("OfficeLGAID") Is DBNull.Value Then
								txtOfficeLGA.Text = myLGA.getLGAName(CInt(dtt.Rows(0).Item("OfficeLGAID"))).ToString
							Else
								Me.txtResidentialState.Text = ""
							End If

							txtResidentialAddress.Text = dtt.Rows(0).Item("ResidentialAddress")

							If Not dtt.Rows(0).Item("ResidentialStateID") Is DBNull.Value Then
								Me.txtResidentialState.Text = myState.getStateName(CInt(dtt.Rows(0).Item("ResidentialStateID"))).ToString
							Else
								Me.txtResidentialState.Text = ""
							End If


							If Not dtt.Rows(0).Item("ResidentialLGAID") Is DBNull.Value Then
								Me.txtResidentialLGA.Text = myLGA.getLGAName(CInt(dtt.Rows(0).Item("ResidentialLGAID"))).ToString
							Else
								txtResidentialLGA.Text = ""
							End If


							Me.txtPermanentAddress.Text = dtt.Rows(0).Item("ContactAddress")

							If Not dtt.Rows(0).Item("ContactStateID") Is DBNull.Value Then
								Me.txtPermanentState.Text = myState.getStateName(CInt(dtt.Rows(0).Item("ContactStateID"))).ToString
							Else
								Me.txtPermanentState.Text = ""
							End If

							If Not dtt.Rows(0).Item("ContactLGAID") Is DBNull.Value Then
								Me.txtPermanentLGA.Text = myLGA.getLGAName(CInt(dtt.Rows(0).Item("ContactLGAID"))).ToString
							Else
								txtPermanentLGA.Text = ""
							End If

							Me.txtRSABalance.Text = CDbl(dtt.Rows(0).Item("RSABalance"))

							Me.txtMandatory.Text = CDbl(dtt.Rows(0).Item("Mandatory"))
							Me.txtAVC.Text = CDbl(dtt.Rows(0).Item("AVC"))
							Me.txtLegacy.Text = CDbl(dtt.Rows(0).Item("Legacy"))

							Me.txtRFBalance.Text = CDbl(dtt.Rows(0).Item("RFBalance"))
							Me.txtAccruedRight.Text = CDbl(dtt.Rows(0).Item("AccruedRight"))
							Me.txtSector.Text = (dt.Rows(0).Item("txtSector")).ToString
							Me.txtBasicSalary.Text = CDbl(dtt.Rows(0).Item("BasicSalary"))
							Me.txtTransportAllow.Text = CDbl(dtt.Rows(0).Item("Transport"))
							Me.txtHousingAllowance.Text = CDbl(dtt.Rows(0).Item("Housing"))


							Me.txtAccountName.Text = (dt.Rows(0).Item("txtAccountName")).ToString
							Me.txtAccountNumber.Text = (dt.Rows(0).Item("txtAccountNo")).ToString
							Me.txtBVN.Text = dt.Rows(0).Item("txtBVN").ToString

							Me.ddExitReasons.SelectedValue = dt.Rows(0).Item("txtReason").ToString
							Me.txtDesignation.Text = dt.Rows(0).Item("txtDesignation").ToString
							Me.txtPartDepartment.Text = dt.Rows(0).Item("txtDepartment").ToString

							'MsgBox("" & dt.Rows(0).Item("dteDisengagement").ToString.Substring(0, 10))

							If Not dt.Rows(0).Item("dteDisengagement") Is DBNull.Value Then
								'  Me.txtDisengagementDate.Text = dt.Rows(0).Item("dteDisengagement").ToString.Substring(0, 10)
								Me.txtDisengagementDate.Text = CDate(dt.Rows(0).Item("dteDisengagement"))
							Else
							End If




							If Not dt.Rows(0).Item("fkiBankID") Is DBNull.Value Then
								Me.ddBankName.SelectedValue = Me.getBankName(CInt(dt.Rows(0).Item("fkiBankID"))).ToString
							Else
								ddBankName.SelectedItem.Text = ""
							End If

							'Dim cr As New Core
							If Not dt.Rows(0).Item("fkiBranchID") Is DBNull.Value And cr.PMgetBankBranches(CInt(dt.Rows(0).Item("fkiBankID")), CInt(dt.Rows(0).Item("fkiBranchID"))).Rows.Count > 0 Then
								Dim lstBankBranches As New DataTable
								lstBankBranches = cr.PMgetBankBranches(CInt(dt.Rows(0).Item("fkiBankID")), CInt(dt.Rows(0).Item("fkiBranchID")))
								'lstBankBranches = Nothing
								Me.ddBankBranch.Items.Add(lstBankBranches.Rows(0).Item("BranchName") & "                   | " & lstBankBranches.Rows(0).Item("BankBranchID"))
							ElseIf ddBankBranch.Items.Count > 0 Then
								ddBankBranch.SelectedItem.Text = ""
							Else

							End If


							Dim dtDocument As New DataTable, dtDocuments As New DataTable, i As Integer, lstAppDocDetailOdd As New List(Of ApplicationDocumentDetail)

							'retrieving all the submitted documents for the application
							dtDocument = cr.PMgetApplicationDocumentsByCode(appCode)
							ViewState("dtDocument") = dtDocument

							'populating all the required document on dropdown control for the applicationn type on the web page base on the sector
							getApplicationDocuments(CInt(dt.Rows(0).Item("fkiAppTypeId")), dt.Rows(0).Item("txtSector"))

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
									newCustomersRow("DocumentPath") = dtDocument.Rows(i).Item("txtDocumentPath")
								End If

								'1= means the death benefit document is verified while 0 = means not verified
								If dtDocument.Rows(i).Item("IsVerified").ToString = "True" Then
									newCustomersRow("IsVerified") = 1
								Else
									newCustomersRow("IsVerified") = 0
								End If


								dtDocuments.Rows.Add(newCustomersRow)

								i = i + 1
							Loop


							ViewState("RecievedDocument") = dtDocuments
							ViewState("RecievedDocumentOLD") = dtDocument

							loadGrid(dtDocuments)

							If CInt(dt.Rows(0).Item("fkiAppTypeId")) = 5 Then
								Me.dvVerified.Visible = True
							Else
								Me.dvVerified.Visible = False
							End If
						End If

					Else
						'Response.Redirect("frmApplicationList.aspx")
					End If
				End If
			Else
				getUserAccessMenu(Session("user"))
			End If

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

		End Try

	End Sub

	Protected Sub AjaxFileUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)


		'Dim filename As String = System.IO.Path.GetFileName(e.FileName)
		'Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)

		''Dim strUploadPath As String = "~/NewFolder1/" + filename

		'Dim strUploadPath As String = "~/" & Session("user") & "/" + filename
		'Me.FlUploadBankConfirmation.SaveAs(Server.MapPath(strUploadPath) + filename)


	End Sub
	'responds to 
	Protected Sub BtnViewDocuments_Click(sender As Object, e As EventArgs)

		Dim btnViewDocument As New ImageButton
		btnViewDocument = sender
		Dim i As GridViewRow
		i = btnViewDocument.NamingContainer
		Dim tmpPath As String = Server.MapPath("~/FileUploads" + "/" + Session("user") + "/" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		Dim permPath As String = Server.MapPath("~/ApplicationDocuments" + "/" + CStr(ViewState("appCode")).ToString.Replace("-", "_") + "_" + Me.gridRecievedDocument.Rows(i.RowIndex).Cells(3).Text)

		If File.Exists(tmpPath) = True Then

			DownLoadDocument(tmpPath)

		End If


		If File.Exists(permPath) = True Then

			DownLoadDocument(permPath)

		End If


		''''dms integration addition'''''''''''''''''''''''''''''''''''''''''''''''''''''''''


		Dim dtDocs As New DataTable, dmsDocumentID As String, dmsDocumentExt As String
		If IsNothing(ViewState("dtDocument")) = False Then

			dtDocs = ViewState("dtDocument")
			dmsDocumentID = dtDocs.Rows(i.RowIndex).Item("txtDMSDocumentID")
			dmsDocumentExt = dtDocs.Rows(i.RowIndex).Item("txtDMSDocumentExt")

			Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
			Dim uName As String, uPWD As String, uRI As String

			uName = ConfigurationManager.AppSettings("FileNetUName")
			uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
			uRI = ConfigurationManager.AppSettings("FileNetURI")

			dms.getConnection(uName, uPWD, uRI)
			DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
			DownLoadDocument(DMSDocumentPath)

		Else
		End If


		''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



	End Sub
	Protected Sub AjaxFileDocumentUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)

		Try

			Dim filename As String = System.IO.Path.GetFileName(e.FileName)
			Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)
			Dim fileNewName As String = Session("Document").ToString.Replace(" | ", "_")

			fileNewName = fileNewName.Replace(" ", "_")
			fileNewName = fileNewName.Replace("|", "_")
			fileNewName = fileNewName.Replace(" ", "_")
			fileNewName = fileNewName.Replace("(", "_")
			fileNewName = fileNewName.Replace(")", "_")


			' Dim strUploadPath As String = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & Session("Document").ToString & "_" & filename

			'  Dim strUploadPath As String = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & Session("Document").ToString.Replace("|", "").Replace("", "_") & System.IO.Path.GetExtension(fullPath) '& "_" & filename

			Dim strUploadPath As String = "~/FileUploads/" & Session("user") & "/" + Session("PIN").ToString & "_" & fileNewName & System.IO.Path.GetExtension(fullPath) '& "_" & filename


			Session("documentPath") = strUploadPath
			Me.flReqDocUpload.SaveAs(Server.MapPath(strUploadPath))
			flReqDocUpload.Dispose()

			'     File.Delete(fullPath)
			Session("Document") = Nothing

		Catch ex As Exception

			'MsgBox("" & ex.Message)

		End Try







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






	Protected Sub btnOtherDetails_Click(sender As Object, e As EventArgs) Handles btnOtherDetails.Click

		Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)

		Select Case apptypeID

			Case 1

			Case 2
				Me.MPRMASHardShip.Show()
			Case 3

			Case 4

			Case Else

		End Select

	End Sub

	Protected Sub calDisengagementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calDisengagementDate.SelectionChanged

		Me.calDisengagementDate_PopupControlExtender.Commit(Me.calDisengagementDate.SelectedDate)
		Me.MPRMASHardShip.Show()
	End Sub

	Protected Sub calApplicationDate_SelectionChanged(sender As Object, e As EventArgs) Handles calApplicationDate.SelectionChanged

		Me.calApplicationDate_PopupControlExtender.Commit(Me.calApplicationDate.SelectedDate)

	End Sub
	Protected Sub calRecievedDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRecievedDate.SelectionChanged

		Me.calRecievedDate_PopupControlExtender.Commit(Me.calRecievedDate.SelectedDate)

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

	Public Function getDocumentTypes(approvalTypeID As Integer, sector As String) As List(Of String)
		Try

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
		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

		End Try

	End Function

	'population the required documents base on benefit application types
	Private Sub getApplicationDocuments(TypeID As Integer, txtSector As String)
		Try


			Dim lstAppDocs As List(Of String), i As Integer

			lstAppDocs = getDocumentTypes(TypeID, txtSector)
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

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
		End Try

	End Sub




	'population the required documents base on benefit application types
	Private Sub getApplicationDocuments(TypeID As Integer)
		Try


			Dim lstAppDocs As List(Of String), i As Integer

			lstAppDocs = getDocumentTypes(TypeID, "Private")
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

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
		End Try

	End Sub


	Protected Sub ddApplicationType_TextChanged(sender As Object, e As EventArgs) Handles ddApplicationType.TextChanged


		Dim apptype As Integer
		If Not IsNothing(ViewState("ApprovalTypeCollection")) Then
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			getApplicationDocuments(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))
			appType = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text))


			Select Case apptype

				Case Is = 1

					Me.dvVerified.Visible = False
					Dim date2 As Date = Date.Parse(txtDOB.Text)
					Dim date1 As Date = Now
					Dim years As Long = DateDiff(DateInterval.Year, date2, date1)

					If years < 50 Then

						pnlAppTypeVerificationError.Visible = True
						Me.lblAppTypeError.Text = "Application Type Error !. Age Most be More than 50 and above"
						Me.ddRequiredDocuments.Items.Clear()
						Exit Sub
					Else
					End If

				Case Is = 16

					Me.dvVerified.Visible = False
					Dim date2 As Date = Date.Parse(txtDOB.Text)
					Dim date1 As Date = Now
					Dim years As Long = DateDiff(DateInterval.Year, date2, date1)

					If years < 50 Then

						pnlAppTypeVerificationError.Visible = True
						Me.lblAppTypeError.Text = "Application Type Error !. Age Most be More than 50 and above"
						Me.ddRequiredDocuments.Items.Clear()
						Exit Sub
					Else
					End If

				Case Is = 2

					Me.dvVerified.Visible = False
					Dim date2 As Date = Date.Parse(txtDOB.Text)
					Dim date1 As Date = Now
					Dim years As Long = DateDiff(DateInterval.Year, date2, date1)

					If years > 49 Then
						pnlAppTypeVerificationError.Visible = True
						Me.lblAppTypeError.Text = "Application Type Error !. Age Not Less Than 50"
						Me.ddRequiredDocuments.Items.Clear()
						Exit Sub
					Else
					End If

				Case Is = 5

					Me.dvVerified.Visible = True

				Case Else
					Me.dvVerified.Visible = False

			End Select
			populateExitReasons(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

			'lstAppDocs = getDocumentTypes(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "Private")
			'ddRequiredDocuments.DataSource = lstAppDocs


			'ddRequiredDocuments.Items.Clear()
			'Do While i < lstAppDocs.Count

			'     If ddRequiredDocuments.Items.Count = 0 Then
			'          ddRequiredDocuments.Items.Add("")
			'          ddRequiredDocuments.Items.Add(lstAppDocs.Item(i))
			'     ElseIf ddRequiredDocuments.Items.Count > 0 Then
			'          ddRequiredDocuments.Items.Add(lstAppDocs.Item(i))
			'     End If
			'     i = i + 1

			'Loop


		Else

		End If

	End Sub
	'populating recieved document for benefit application
	Protected Sub loadGrid(dt As DataTable)
		Try


			If dt.Rows.Count > 5 Then
				pnlUploadDetail.Height = Nothing
				dvRecievedDocument.Style.Item("height") = Nothing
			Else
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

	'getting the list of bank branches per bank
	Public Function getBankBranchName(id As Integer) As String
		Try


			Dim lstBankBranchName As New List(Of String)
			Dim dc As New BanksDataContext
			Dim querys = From m In dc.BankBranches
					  Where m.BankBranchID = id
					  Select New With {m.BranchName}

			For Each m In querys
				lstBankBranchName.Add(m.BranchName)
			Next



			If (lstBankBranchName.Count > 0) Then
				Return lstBankBranchName(0).ToString
			ElseIf (lstBankBranchName.Count = 0) Then
				pnlError.Visible = True
				Me.lblError.Text = "Error Loading Bank Branches"
				Return Nothing
			End If

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
			pnlError.Visible = True
			Me.lblError.Text = "Error Loading Bank Branches"
		End Try

	End Function

	Public Function getBankBranches(bankID As Integer) As List(Of String)

		Dim dc As New BanksDataContext
		Dim lstBankBranches As New List(Of String)
		Try

			Dim query = From m In dc.Banks Join n In dc.BankBranches On m.BankID Equals n.BankID Where m.BankID = bankID _
					  Select New With {n.BankBranchID, n.BranchName}
			For Each a In query

				lstBankBranches.Add(a.BranchName)
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
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

		End Try

	End Function


	Public Function getBankName(Bankid As Integer) As String
		Try


			'Dim lstBankName As New List(Of String)
			'Dim dc As New BanksDataContext
			'Dim querys = From m In dc.Banks
			'            Where m.BankID = id
			'            Select New With {m.BankName}

			'For Each m In querys
			'     lstBankName.Add(m.BankName)
			'Next

			'If (lstBankName.Count > 0) Then
			'     Return lstBankName(0).ToString
			'Else
			'     Return Nothing
			'     End If

			Dim cr As New Core
			Return cr.PMgetBanks(Bankid).Rows(0).Item(1).ToString

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
		End Try

	End Function



	Protected Sub populateExitReasons(paymentTypeID As Integer)

		Dim myExitReasons As New ExitReasons, i As Integer = 0
		Dim lstReasons As New List(Of String)
		lstReasons = myExitReasons.getExitReasonsTypes(paymentTypeID)

		Me.ddExitReasons.Items.Clear()

		Do While i < lstReasons.Count

			Select Case paymentTypeID

				Case Is = 1
					'populating reason for payment on rmas screen for enbloc payment
					If Me.ddPaymentReasons.Items.Count = 0 Then
						Me.ddPaymentReasons.Items.Add("")
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddPaymentReasons.Items.Count > 0 Then
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 16
					'populating reason for payment on rmas screen for enbloc payment
					If Me.ddPaymentReasons.Items.Count = 0 Then
						Me.ddPaymentReasons.Items.Add("")
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					ElseIf Me.ddPaymentReasons.Items.Count > 0 Then
						Me.ddPaymentReasons.Items.Add(lstReasons.Item(i))
					End If

				Case Is = 2
					'populating reason for payment on rmsa screen for 25%
					If Me.ddExitReasons.Items.Count = 0 Then
						Me.ddExitReasons.Items.Add("")
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

	Protected Sub calDORAnnuity_SelectionChanged(sender As Object, e As EventArgs) Handles calDORAnnuity.SelectionChanged
		Me.calDORAnnuity_PopupControlExtender.Commit(Me.calDORAnnuity.SelectedDate)
		Me.MPRMASAnnuity.Show()
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

     Public Function getBanks() As List(Of String)

          Dim lstBankTypes As New List(Of String)
          Dim dc As New BanksDataContext
          Dim query = From m In dc.Banks
                      Select m

          For Each a As Bank In query
               lstBankTypes.Add(a.BankName)
               BankTypeCollection.Add(a.BankName, a.BankID)
          Next
          ViewState("BankTypeCollection") = BankTypeCollection
          Return lstBankTypes

     End Function

	'    Protected Sub populateBank()

	'         Dim myState As New States, i As Integer = 0
	'         Dim lstBank As New List(Of String)
	'         lstBank = getBanks()
	'         Me.ddBankName.Items.Clear()

	'         Do While i < lstBank.Count

	'              If Me.ddBankName.Items.Count = 0 Then
	'			Me.ddBankName.Items.Add("")
	'			Me.ddBankName.Items.Add(lstBank.Item(i))
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

     Protected Sub btnRemoveRecievedDoc_Click(sender As Object, e As EventArgs) Handles btnRemoveRecievedDoc.Click

          Try
               'mpARLDetail.Show()

               If Me.ddRequiredDocuments.SelectedValue.ToString <> "" Then



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


                    Dim newCustomersRow As DataRow
                    newCustomersRow = dtDocuments.NewRow()


                    newCustomersRow("DocumentName") = Me.ddRequiredDocuments.SelectedValue
                    newCustomersRow("RecievedDate") = Me.txtRecievedDate.Text


				'If Not IsNothing(Session("documentPath")) = True Then

				'	Dim sarrMyString As String() = Session("documentPath").ToString.Split(New String() {"PEN"}, StringSplitOptions.None)
				'	newCustomersRow("DocumentPath") = "PEN" + sarrMyString(1).ToString

				'Else
				'	newCustomersRow("DocumentPath") = ""
				'End If



				If Not IsNothing(Session("documentPath")) = True Then

					Dim sarrMyString As String() = Session("documentPath").ToString.Split(New String() {"PEN"}, StringSplitOptions.None)

					If sarrMyString.Count = 1 Then
						sarrMyString = Session("documentPath").ToString.Split(New String() {"DBA"}, StringSplitOptions.None)
						newCustomersRow("DocumentPath") = "DBA" + sarrMyString(1).ToString
					Else
						newCustomersRow("DocumentPath") = "PEN" + sarrMyString(1).ToString
						'saving the documents in a temp folder to allow auto-retrieval of document should the application was not submitted

					End If

				Else

					newCustomersRow("DocumentPath") = ""

				End If





















                    '''''reviewing the collection of all the available application types from memory
                    If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
                         ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
                    Else
                    End If
                    '''''getting the ID of the user selected application type
                    Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

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

               Else

               End If

          Catch ex As Exception
			'    MsgBox("" & ex.Message)
          End Try

     End Sub

     Protected Sub btnEmployerHistory_Click(sender As Object, e As EventArgs) Handles btnEmployerHistory.Click

     End Sub

     Protected Sub gridCustomerHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridCustomerHistory.SelectedIndexChanged
          Dim selectedRowIndex As Integer
          Dim selectedLodgment As New ArrayList

          selectedRowIndex = gridCustomerHistory.SelectedRow.RowIndex

          Dim row As GridViewRow = gridCustomerHistory.Rows(selectedRowIndex)

          Me.txtEmployer.Text = row.Cells(2).Text.ToString.Replace("&amp;", "&")

          ViewState("EmployerCode") = row.Cells(3).Text.ToString
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

     Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

          If Not IsNothing(ViewState("ApprovalTypeCollection")) = True Then
               ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          Else
          End If

		Dim typeID As Integer = CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text.ToString))

          If IsNothing(ViewState("RMAS")) = True And typeID = 2 Then

               Me.MPRMASHardShip.Show()
			Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 1 Then
               Me.MPRMASEnbloc.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 16 Then
			Me.MPRMASEnbloc.Show()
			Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 3 Then
               Me.MPRMASPW.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 14 Then
			Me.MPRMASPW.Show()
			Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 4 Then
               Me.MPRMASAnnuity.Show()
			Exit Sub

		ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 15 Then
			Me.MPRMASAnnuity.Show()
			Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 8 Then
               Me.MPRMASLegacy.Show()
               Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 5 Then
               Me.mpDeathBenefit.Show()
               Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 7 Then
               Me.mpAVC.Show()
               Exit Sub

          ElseIf IsNothing(ViewState("RMAS")) = True And typeID = 6 Then
               Me.MPNSITF.Show()
			Exit Sub

		ElseIf typeID = 11 Then
			ViewState("RMAS") = True

          Else
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


          Dim appDetail As New ApplicationDetail, NextAppID As Integer, cr As New Core, ApplicationCode As String, sector As String, myID As Integer
		Dim appDocDetails As New List(Of ApplicationDocumentDetail), appAdhocDocDetails As New List(Of AdhocDocuments), appCheckList As New ApplicationCheckList, appCheckListDBA As New ApplicationCheckListDOB

          'NextAppID = cr.PMgetNextApplicationID()
          If Page.IsValid And IsNothing(ViewState("EmployeeID")) = False Then

               If Me.ddRequiredDocuments.Items.Count = 0 Then
                    Exit Sub
               Else
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

               Try

                    If IsNothing(ViewState("RMAS")) = False And typeID = 4 Then

                         Dim rDetails As New RetirementDetails
                         rDetails.ApplicationCode = CStr(ViewState("appCode"))
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
                         rDetails.InsuranceCoy = Me.txtInsuranceCoy.Text
					rDetails.RSABalance = Me.txtRSABalanceAnnuity.Text
                         rDetails.Premium = Me.txtPremium.Text
                         rDetails.AnnuityLumpSum = Me.txtLumpSum.Text
                         rDetails.MonthlyAnnuity = Me.txtMonthlyAnnuity.Text
					rDetails.isAnnuity = True
					rDetails.RetirementDate = CDate(Me.txtDORAnnuity.Text)
                         appDetail.RetirementDetails = rDetails

				End If

				If IsNothing(ViewState("RMAS")) = False And typeID = 15 Then

					Dim rDetails As New RetirementDetails
					rDetails.ApplicationCode = CStr(ViewState("appCode"))
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
					rDetails.InsuranceCoy = Me.txtInsuranceCoy.Text
					rDetails.RSABalance = Me.txtRSABalanceAnnuity.Text
					rDetails.Premium = Me.txtPremium.Text
					rDetails.AnnuityLumpSum = Me.txtLumpSum.Text
					rDetails.MonthlyAnnuity = Me.txtMonthlyAnnuity.Text
					rDetails.isAnnuity = True
					appDetail.RetirementDetails = rDetails

				End If

                    If IsNothing(ViewState("RMAS")) = False And typeID = 3 Then

                         Dim rDetails As New RetirementDetails
                         rDetails.ApplicationCode = CStr(ViewState("appCode"))
                         rDetails.BasicSalary = Me.txtBasicSalaryPW.Text
                         rDetails.HouseRent = Me.txtHouseRent.Text
                         rDetails.Transport = Me.txtTransport.Text
                         rDetails.Utility = Me.txtUtility.Text
                         rDetails.ConsolidatedAallowance = Me.txtConsolidatedAllowance.Text
                         rDetails.ConsolidatedSalary = Me.txtConsolidatedSalary.Text
                         rDetails.MonthlyTotal = Me.txtMonthTotal.Text
					rDetails.AnnualTotalEmolumentAdj = Me.txtAnnualTotalEmolument.Text
					rDetails.RetirementDate = Me.txtDORPW.Text


                         rDetails.PriceDate = Me.txtValueDate.Text
                         rDetails.RSABalance = Me.txtRSABalancePW.Text
                         rDetails.AccruedRight = Me.txtAccruedRightPW.Text
                         rDetails.RecommendedLumpSum = Me.txtRecommendeLumpSum.Text
                         rDetails.MonthlyProgramedDrawndown = Me.txtMonthlyDrawDown.Text
                         rDetails.isProgramWithdrawal = True
                         appDetail.RetirementDetails = rDetails
					appDetail.DOR = Me.txtDORPW.Text
				End If


				If IsNothing(ViewState("RMAS")) = False And typeID = 14 Then

					Dim rDetails As New RetirementDetails
					rDetails.ApplicationCode = CStr(ViewState("appCode"))
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
                         rDetails.ApplicationCode = CStr(ViewState("appCode"))
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
                    appDetail.ApplicationID = CStr(ViewState("appCode"))
               appDetail.Sector = Me.txtSector.Text
              

				If typeID = 5 Then

					appCheckListDBA.ApplicationCode = CStr(ViewState("appCode"))
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

					appCheckList.ApplicationCode = CStr(ViewState("appCode"))
					appCheckList.DataEntryChecked = 1
					appCheckList.ExitDocChecked = 1
					appCheckList.FundingStatusChecked = 1
					appCheckList.LegAVCChecked = 1
					appCheckList.NamesChecked = 1
					appCheckList.ValidDocChecked = 1
					appCheckList.DOBChecked = 1

				End If



                    If IsNothing(ViewState("RMAS")) = False And typeID = 2 Then
                         appDetail.DateDisengagement = DateTime.Parse(Me.txtDisengagementDate.Text).ToString("yyyy-MM-dd")
                         appDetail.IsRetirement = False

				ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 1 Then

					appDetail.DOR = DateTime.Parse(Me.txtRetirementDate.Text).ToString("yyyy-MM-dd")
					appDetail.IsRetirement = True

				ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 16 Then

					appDetail.DOR = DateTime.Parse(Me.txtRetirementDate.Text).ToString("yyyy-MM-dd")
					appDetail.IsRetirement = True

				ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 3 Then

					appDetail.DOR = DateTime.Parse(Me.txtDORPW.Text).ToString("yyyy-MM-dd")
					appDetail.IsRetirement = True

				ElseIf IsNothing(ViewState("RMAS")) = False And typeID = 4 Then

					appDetail.DOR = DateTime.Parse(Me.txtDORAnnuity.Text).ToString("yyyy-MM-dd")
					appDetail.IsRetirement = True

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

				Else

					appDetail.NSITFInitialAmountPaid = "0.00"
					appDetail.NSITFRecievedToRSA = "0.00"
					appDetail.NSITFRequestedToRSA = "0.00"
				End If


               If IsNothing(ViewState("EmployerHistoryCollection")) = True And IsNothing(ViewState("Employerid")) = False Then
                    appDetail.EmployerID = CInt(ViewState("Employerid"))
					appDetail.EmployerName = Me.txtEmployer.Text.Replace("'", "''")
                    appDetail.EmployerCode = ViewState("EmployerCode").ToString
               ElseIf IsNothing(ViewState("EmployerHistoryCollection")) = False Then
                    EmployerHistoryCollection = ViewState("EmployerHistoryCollection")
                    appDetail.EmployerID = CInt(EmployerHistoryCollection.Item(Me.txtEmployer.Text))
                    appDetail.EmployerName = Me.txtEmployer.Text
                    appDetail.EmployerCode = ViewState("EmployerCode").ToString
               End If


               appDetail.PIN = Me.txtPIN.Text
				appDetail.FullName = Me.txtSurname.Text.Trim.Replace("'", "''") & " | " & Me.txtFirstName.Text.Trim.Replace("'", "''") & " | " & Me.txtOtherNames.Text.Trim.Replace("'", "''")

				If Me.txtDOB.Text = "" Then
					appDetail.DOB = Now.Date
				Else
					appDetail.DOB = CDate(Me.txtDOB.Text)
				End If

               appDetail.FundStatus = Me.ddFundingStatus.SelectedItem.Text.ToString

               If typeID = 1 Then
					appDetail.Reason = Me.ddPaymentReasons.SelectedItem.Text.ToString

				ElseIf typeID = 16 Then
					appDetail.Reason = Me.ddPaymentReasons.SelectedItem.Text.ToString
				ElseIf typeID = 3 Then
					appDetail.Reason = Me.ddRetirementGroundPW.SelectedItem.Text.ToString
               ElseIf typeID = 2 Then
                    appDetail.Reason = Me.ddExitReasons.SelectedItem.Text.ToString
                    appDetail.Designation = Me.txtDesignation.Text
                         appDetail.Department = Me.txtPartDepartment.Text
                         appDetail.RSABalance = CDbl(Me.txtMandatory.Text)
                    Else
                         appDetail.RSABalance = CDbl(Me.txtRSABalance.Text)
                    End If


               appDetail.Comment = Me.txtOtherComments.Text
               appDetail.CommentGroup = Me.ddCommentGroup.SelectedItem.Text.ToString
               appDetail.MemberID = CInt(ViewState("EmployeeID"))
               appDetail.ApplicationDate = CDate(Me.txtApplicationDate.Text)
               appDetail.ApplicationState = ddApplicationState.SelectedItem.Text.ToString

                    appDetail.Sex = Me.txtSex.Text
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

				appDetail.AccountName = Me.txtAccountName.Text
               appDetail.AccountNo = Me.txtAccountNumber.Text
               appDetail.BVN = Me.txtBVN.Text
               If Not IsNothing(ViewState("BankTypeCollection")) = True Then
                    BankTypeCollection = ViewState("BankTypeCollection")
                    appDetail.BankID = CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text.ToString))
               Else
               End If

				'           If Not IsNothing(ViewState("BankBranchCollection")) = True Then
				'                BankBranchCollection = ViewState("BankBranchCollection")
				'	'appDetail.BranchID = getBankBranchID(Me.ddBankBranch.SelectedItem.Text.ToString)
				'           Else
				'End If


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

                                   appAdhocDocDetail.ApplicationCode = CStr(ViewState("appCode"))
                                   appAdhocDocDetail.Description = row.Cells(1).Text
                                   appAdhocDocDetail.PIN = LTrim(RTrim(Me.txtPIN.Text))
                                   appAdhocDocDetail.RecievedBy = Session("user")
							appAdhocDocDetail.RecievedDate = Now

							appAdhocDocDetail.DocPath = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

							'appAdhocDocDetail.DocPath = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "\\P-MIDAS2\NPM_Data\ApplicationDocuments\"

                                   appAdhocDocDetail.IsVerified = CInt((row.Cells(5).Text))
                                   appAdhocDocDetails.Add(appAdhocDocDetail)

						Else

							appDocDetail.DocumentTypeID = CInt(DocumentCollection.Item(row.Cells(1).Text))
							appDocDetail.DateReceived = CDate((row.Cells(2).Text))
							appDocDetail.ReceivedBy = Session("user")

							If CStr((row.Cells(3).Text)) = "&nbsp;" Then
								isAllDocumentScanned = False
							Else
								isAllDocumentScanned = True
							End If

							appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + Server.MapPath("~/ApplicationDocuments/")

							'appDocDetail.DocumentLocation = CStr((row.Cells(3).Text)) + "|" + Server.MapPath("~/FileUploads/") + "|" + "\\P-MIDAS2\NPM_Data\ApplicationDocuments\"
							'\\P-MIDAS2\NPM_Data
							appDocDetail.IsVerified = CInt((row.Cells(5).Text))
							appDocDetails.Add(appDocDetail)
                              End If

                              
                         docCount = docCount + 1
                    Loop
               Else
               End If

               appDetail.CommentGroup = Me.ddCommentGroup.Text


				'ViewState("ReturnPage")  ViewState("applicationDetails") = dt

				Dim dt As New DataTable
				dt = ViewState("applicationDetails")

				If CStr(ViewState("ReturnPage")) = "AdvApplicationFind" Then

					'saving back the status of the application as is without checking the status of the submitted document
					appDetail.Status = dt.Rows(0).Item("txtStatus").ToString
					appDetail.DocCompleted = 0
					appDetail.DateDocumentCompleted = Nothing

				Else


					''''''''''''''''''''''''''determining the status of the application base on the documentation status'''''''''''''''''''''''''''''


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


					'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

				End If

				Dim dtRecievedDocumentOLD As DataTable, appDocDetailsOLD As New List(Of ApplicationDocumentDetail)

				If IsNothing(ViewState("RecievedDocumentOLD")) = False Then
					dtRecievedDocumentOLD = ViewState("RecievedDocumentOLD")
					Dim j As Integer = 0

					'building data object of the old documents recieved list
					Do While j < dtRecievedDocumentOLD.Rows.Count

						Dim lstAppDocDetail As New ApplicationDocumentDetail
						lstAppDocDetail.DocumentTypeID = dtRecievedDocumentOLD.Rows(j).Item("fkiDocumentTypeID")
						lstAppDocDetail.DocumentTypeName = dtRecievedDocumentOLD.Rows(j).Item("txtDocumentName")
						lstAppDocDetail.MemberApplicationID = dtRecievedDocumentOLD.Rows(j).Item("fkiMemberApplicationID")
						lstAppDocDetail.DocumentLocation = dtRecievedDocumentOLD.Rows(j).Item("txtDocumentPath").ToString
						''''dms additions'''''''''
						lstAppDocDetail.DMSDocumentID = dtRecievedDocumentOLD.Rows(j).Item("txtDMSDocumentID").ToString
						lstAppDocDetail.DMSDocumentExt = dtRecievedDocumentOLD.Rows(j).Item("txtDMSDocumentExt").ToString
						''''''''''''''''''''''''''
						lstAppDocDetail.ReceivedBy = Session("user")
						appDocDetailsOLD.Add(lstAppDocDetail)
						j = j + 1

					Loop

				Else

				End If


				'relaxing all required documents check for death benefit application only

				'If CStr(ViewState("ReturnPage")) = "AdvApplicationFind" Then


				If appDetail.AppTypeId = 5 And isAllDocumentScanned = True And CStr(ViewState("ReturnPage")) <> "AdvApplicationFind" Then

					appDetail.Status = "Documentation"
					appDetail.DateDocumentCompleted = DateTime.Parse(Now)
					appDetail.DocCompleted = 1

				Else

				End If

				If IsNothing(Session("user")) = True Then
					Response.Redirect("Login.aspx")
				Else
					cr.PMUpdateApplication(appDetail, appDocDetails, appDocDetailsOLD, appAdhocDocDetails, Session("user"), appCheckList, appCheckListDBA, Server.MapPath("~/Logs"))
					cr.PMCleanDuplicateDocument(appDetail.ApplicationID)
				End If

				'   Response.Redirect(String.Format("frmApplicationList.aspx?ApplicationID={0}", Server.UrlEncode(ViewState("appID").ToString)))


				If Not IsNothing(ViewState("ReturnPage")) = True Then

					'MsgBox("" & "" & "frm" & CStr(ViewState("ReturnPage")) & ".aspx?IsReturn=1")

					Response.Redirect("" & "frm" & CStr(ViewState("ReturnPage")) & ".aspx?IsReturn=1")
				Else
					Response.Redirect("Login.aspx")
				End If

			Catch ex As Exception

				Dim logerr As New Global.Logger.Logger
				logerr.FileSource = "Payment Module"
				logerr.FilePath = Server.MapPath("~/Logs")
				logerr.Logger(ex.Message & "Application Edit Submission")

			End Try
          End If


     End Sub

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

     Protected Sub btnHardShipOK_Click(sender As Object, e As EventArgs) Handles btnHardShipOK.Click

          blnHardShip = True
          ViewState("RMAS") = blnHardShip

     End Sub

     Protected Sub ddBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddBankName.SelectedIndexChanged
          Try

          
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

			'     Loop


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

          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = "c:"
               logerr.Logger(ex.Message)
          End Try

     End Sub

     Protected Sub ddBankName_TextChanged(sender As Object, e As EventArgs) Handles ddBankName.TextChanged

     End Sub

     Protected Sub txtDisengagementDate_TextChanged(sender As Object, e As EventArgs) Handles txtDisengagementDate.TextChanged

     End Sub

     Protected Sub ddRequiredDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddRequiredDocuments.SelectedIndexChanged

		'Session("user") = 

		If Me.ddRequiredDocuments.SelectedItem.Text = "Accrued Rights Letter" Then
			dvARL.Visible = True
		Else
			dvARL.Visible = False
		End If

		Session("Document") = Me.ddRequiredDocuments.SelectedItem.Text
          Dim strUploadPath As String = Server.MapPath("~/FileUploads/" & Session("user"))
          MakeDirectoryIfExists(strUploadPath)


          'Session("user") = "o-taiwo"
          'Session("Document") = Me.ddRequiredDocuments.SelectedItem.Text
          'Dim strUploadPath As String = Server.MapPath("~/FileUploads/" & Session("user"))
          'MakeDirectoryIfExists(strUploadPath)


     End Sub

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

     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

     End Sub

     Protected Sub calRetirementDate_SelectionChanged(sender As Object, e As EventArgs) Handles calRetirementDate.SelectionChanged

          Me.calRetirementDate_PopupControlExtender.Commit(Me.calRetirementDate.SelectedDate)
          Me.MPRMASEnbloc.Show()

     End Sub

     Protected Sub btnEnblocOK_Click(sender As Object, e As EventArgs) Handles btnEnblocOK.Click

          blnHardShip = True
          ViewState("RMAS") = blnHardShip

     End Sub

     Protected Sub imgBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack.Click

          If Not IsNothing(ViewState("appID")) = True Then
               'Dim returnPage As String = CStr(ViewState("ReturnPage")) & ".aspx?"

               If CStr(ViewState("ReturnPage")) = "frmApplicationApprovaList" Then
                    Response.Redirect(String.Format("frmApplicationApprovaList.aspx?ApplicationID={0}", Server.UrlEncode(ViewState("appID").ToString)))

               ElseIf CStr(ViewState("ReturnPage")) = "frmApplicationList" Then
                    Response.Redirect(String.Format("frmApplicationList.aspx?ApplicationID={0}", Server.UrlEncode(ViewState("appID").ToString)))

               ElseIf CStr(ViewState("ReturnPage")) = "frmProcessing" Then
                    Response.Redirect(String.Format("frmProcessing.aspx?ApplicationID={0}", Server.UrlEncode(ViewState("appID").ToString)))

               ElseIf CStr(ViewState("ReturnPage")) = "frmConfirmation" Then
				Response.Redirect(String.Format("frmConfirmation.aspx?ApplicationID={0}", Server.UrlEncode(ViewState("appID").ToString)))

			ElseIf CStr(ViewState("ReturnPage")) = "frmApplicationFind" Then

				Response.Redirect(String.Format("frmApplicationFind.aspx?PIN={0}", Server.UrlEncode(ViewState("PIN").ToString)))

               End If

          Else
               Response.Redirect("Login.aspx")
          End If

     End Sub

     Protected Sub btnOKAnnuity_Click(sender As Object, e As EventArgs) Handles btnOKAnnuity.Click

          blnAnnuity = True
          ViewState("RMAS") = blnAnnuity

     End Sub

     Protected Sub btnOKPW_Click(sender As Object, e As EventArgs) Handles btnOKPW.Click

		' blnPW = True
		'ViewState("RMAS") = blnPW
		pnlError.Visible = False

		If (CDbl(txtRecommendeLumpSum.Text) * 2) > CDbl(txtRSABalancePW.Text) Then

			lblError.Text = "Lumpsum Should be Less Than 50% of RSA balance "
			Me.pnlError.Visible = True

		Else
			blnPW = True
			ViewState("RMAS") = blnPW
		End If



     End Sub

     Protected Sub CalValueDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalValueDate.SelectionChanged

          Me.CalValueDate_PopupControlExtender.Commit(Me.CalValueDate.SelectedDate)
          Me.MPRMASPW.Show()

     End Sub

     Protected Sub CalValueDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalValueDate.VisibleMonthChanged

          Me.MPRMASPW.Show()

     End Sub

     Protected Sub btnReValuePW_Click(sender As Object, e As EventArgs) Handles btnReValuePW.Click

          Dim cr As New Core
          Try
               Me.txtRSABalancePW.Text = cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), CDate(Me.txtValueDate.Text), 1)
               Me.MPRMASPW.Show()
          Catch ex As Exception

          End Try

     End Sub

     Protected Sub btnReValueAnnuity_Click(sender As Object, e As EventArgs) Handles btnReValueAnnuity.Click

          Dim cr As New Core
          Try

               Me.txtRSABalanceAnnuity.Text = cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), CDate(Me.txtValueDateAnnuity.Text), 1)
               Me.MPRMASAnnuity.Show()

          Catch ex As Exception

          End Try


     End Sub

     Protected Sub btnDBOk_Click(sender As Object, e As EventArgs) Handles btnDBOk.Click
          blnDBOk = True
          ViewState("RMAS") = blnDBOk
     End Sub

     Protected Sub calDORNSITF_SelectionChanged(sender As Object, e As EventArgs) Handles calDORNSITF.SelectionChanged

          Me.PopupControlExtender_calDORNSITF.Commit(Me.calDORNSITF.SelectedDate)
          Me.MPNSITF.Show()

     End Sub

     Protected Sub btnNSITFOk_Click(sender As Object, e As EventArgs) Handles btnNSITFOk.Click

          blnNSITF = True
          ViewState("RMAS") = blnNSITF

     End Sub

     Protected Sub CalDBValueDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalDBValueDate.SelectionChanged

          Me.PopupControlExtender_CalDBValueDate.Commit(Me.CalDBValueDate.SelectedDate)
          Me.mpDeathBenefit.Show()

     End Sub

     Protected Sub CalDBValueDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalDBValueDate.VisibleMonthChanged
          Me.mpDeathBenefit.Show()
     End Sub

     Protected Sub btnDBReValue_Click(sender As Object, e As EventArgs) Handles btnDBReValue.Click
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

     Protected Sub CalAdminLetterDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalAdminLetterDate.SelectionChanged
          Me.PopupControlExtender_CalAdminLetterDate.Commit(Me.CalAdminLetterDate.SelectedDate)
          Me.mpDeathBenefit.Show()
     End Sub

     Protected Sub CalDBARetirement_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalDBARetirement.VisibleMonthChanged
          Me.mpDeathBenefit.Show()
     End Sub

     Protected Sub CalDeathDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalDeathDate.VisibleMonthChanged
          Me.mpDeathBenefit.Show()
     End Sub

     Protected Sub CalAdminLetterDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalAdminLetterDate.VisibleMonthChanged
          Me.mpDeathBenefit.Show()
     End Sub

	Protected Sub btnAVCOk_Click(sender As Object, e As EventArgs) Handles btnAVCOk.Click

		blnAVC = True
		ViewState("RMAS") = blnAVC


	End Sub

	Protected Sub btnSubmit_Load(sender As Object, e As EventArgs) Handles btnSubmit.Load

	End Sub

	Protected Sub calDORPW_SelectionChanged(sender As Object, e As EventArgs) Handles calDORPW.SelectionChanged

		Me.calDORPW_PopupControlExtender.Commit(Me.calDORPW.SelectedDate)
		Me.MPRMASPW.Show()
	End Sub

	Protected Sub calDORPW_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calDORPW.VisibleMonthChanged
		Me.MPRMASPW.Show()
	End Sub

	Protected Sub btnLegacyOK_Click(sender As Object, e As EventArgs) Handles btnLegacyOK.Click

		blnLegacy = True
		ViewState("RMAS") = blnLegacy


	End Sub
End Class
