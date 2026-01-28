Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Partial Class frmPWCalculatorNew
	Inherits System.Web.UI.Page
	Dim numRSABalance As Double = 0
	Private Function calInterate() As Double

		Dim C12 As Double
		Dim C13 As Double
		Dim C14 As Double


		C12 = FormatNumber(CDbl(Me.txtManagement.Text.Split("%")(0)) / 100, 5)
		C13 = FormatNumber(CDbl(Me.txtRegulator.Text.Split("%")(0)) / 100, 5)
		C14 = FormatNumber(CDbl(Me.txtInterest.Text.Split("%")(0)) / 100, 5)

		Return C14 * (1 - (C12 + C13))

	End Function

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		If Me.chkIsAnnuity.Checked = True And IsNothing(ViewState("ANNDetails")) = True Then
			Me.mpARLDetail.Show()
			Exit Sub
		Else
		End If



		If IsNothing(Session("user")) = True Then
			Response.Redirect("login.aspx")
		Else

		End If

		Me.spUpdateStatus.InnerText = ""
		Try


			Dim excel = New Microsoft.Office.Interop.Excel.Application()
			Dim wsf As Microsoft.Office.Interop.Excel.WorksheetFunction = excel.WorksheetFunction

			Dim dt As New DataTable, cr As New Core, k As Integer

			dt = cr.getPMPersonInformation(Me.txtPIN.Text)

			If txtRSABalance.Text = "" Then
				txtRSABalance.Text = 0
			Else

			End If

			If CDbl(Me.txtRSABalance.Text) > 0 Then
				numRSABalance = CDbl(Me.txtRSABalance.Text)
				ViewState("numRSABalance") = numRSABalance
			Else
				numRSABalance = dt.Rows(0).Item("RFBalance")
				Me.txtRSABalance.Text = dt.Rows(0).Item("RFBalance")
				ViewState("numRSABalance") = numRSABalance
			End If


			ViewState("Retiree") = dt


			If Me.ddSS.SelectedItem.Text <> "" And Me.txtGradeLevel.Text <> "" And Me.txtStep.Text <> "" Then

				Me.txtValidatedSalary.Text = cr.getSalaryStructure(Me.ddSS.SelectedItem.Text, Me.txtGradeLevel.Text, Me.txtStep.Text)
			Else

				Me.txtValidatedSalary.Text = txtFinalSalary.Text

			End If

			Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")
			Dim age As Integer, ageR As Integer, arears As Integer

			age = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(txtDOP.Text)))
			ageR = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtDOR.Text)))
			arears = (DateDiff(DateInterval.Month, CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text)))

			'	wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1)

			'txtArrears.Text = arears


			txtArrears.Text = CInt(CInt(wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) * (CInt(Me.txtFreq.Text) / 12))

			'txtArrears.Text = CInt(((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12)
			Me.spControl.InnerText = CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)

			'Me.spControl.InnerText = CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)
			If (Month(CDate(Me.txtDOP.Text)) < CDate(Me.txtDOB.Text).Month) Then
				age = age - 1
			ElseIf (Month(CDate(Me.txtDOP.Text)) = CDate(Me.txtDOB.Text).Month) And (Day(CDate(Me.txtDOP.Text))) < CDate(Me.txtDOB.Text).Day Then
				age = age - 1
			ElseIf (Month(CDate(Me.txtDOP.Text)) > CDate(Me.txtDOB.Text).Month) Then
				age = age
			ElseIf (Month(CDate(Me.txtDOP.Text)) = CDate(Me.txtDOB.Text).Month) And (Day(CDate(Me.txtDOP.Text))) > CDate(Me.txtDOB.Text).Day Then
				age = age
			Else
			End If


			If (Month(CDate(Me.txtDOR.Text)) < CDate(Me.txtDOB.Text).Month) Then
				ageR = ageR - 1
			ElseIf (Month(CDate(Me.txtDOR.Text)) = CDate(Me.txtDOB.Text).Month) And (Day(CDate(Me.txtDOR.Text))) < CDate(Me.txtDOB.Text).Day Then
				ageR = ageR - 1
			ElseIf (Month(CDate(Me.txtDOR.Text)) > CDate(Me.txtDOB.Text).Month) Then
				ageR = ageR
			ElseIf (Month(CDate(Me.txtDOR.Text)) = CDate(Me.txtDOB.Text).Month) And (Day(CDate(Me.txtDOR.Text))) > CDate(Me.txtDOB.Text).Day Then
				ageR = ageR
			Else
			End If


			txtAgee.Text = age
			txtRAge.Text = ageR

			'Me.txtRAge.Text = (DateDiff(DateInterval.Year, CDate(Me.txtDOP.Text), CDate(Now.Date)))


			Me.txtSex.Text = dt.Rows(0).Item("sex").ToString
			'Me.txtRSABalance.Text = FormatNumber(CDbl(dt.Rows(0).Item("RFBalance")), 2)

			Me.txt25LumpSum.Text = FormatNumber(CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)) * 0.2, 2)

			'Me.txt25LumpSum.Text = FormatNumber(CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)) * 0.2, 2)


			Me.txtMinMonthlyDraw.Text = FormatNumber((CDbl((Me.txtValidatedSalary.Text)) / CInt(Me.txtFreq.Text)) * 0.5, 2)


			txtNetInterest.Text = FormatNumber(calInterate() * 100, 2)


			If Me.txtSex.Text = "M" Then

				'Me.txtNxDx.Text = FormatNumber(cr.getNxDxPW(1, CInt(age)), 8)

				Me.txtNxDx.Text = FormatNumber(cr.getNxDxPW(1, CInt(Me.txtRAge.Text), CInt(Me.txtFreq.Text)), 8)
			Else
				'Me.txtNxDx.Text = FormatNumber(cr.getNxDxPW(0, CInt(age)), 8)

				Me.txtNxDx.Text = FormatNumber(cr.getNxDxPW(0, CInt(Me.txtRAge.Text), CInt(Me.txtFreq.Text)), 8)

			End If




			Me.txtNc.Text = FormatNumber(Me.txtNxDx.Text - (11 / 24), 6)

			Dim lstNumbers As New List(Of Double), lstNumbers2 As New List(Of Double), recommendedAmount As Double, para1 As Double, para2 As Double

			''''''''''''''''''''''''''''''''''calculation of maximum lumpSum'''''''''''''''''''''''''''''''''''''''''''''''''''''
			'para1 = (CDbl(Me.txtRSABalance.Text) - CDbl(Me.txtAdministrativeCharges.Text))




			para1 = CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12))

			'para1 = CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12))

			para2 = Financial.PV(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtMinMonthlyDraw.Text, 0, 1)

			recommendedAmount = para1 + para2

			lstNumbers.Add(recommendedAmount)

			lstNumbers.Add(0)

			Me.txtMaxLumpSum.Text = FormatNumber(CDbl(lstNumbers.Max), 2)


			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

			If txtRecommendedLumpSum.Text = "" Then
				txtRecommendedLumpSum.Text = 0
			Else
			End If

			If CDbl(txtRecommendedLumpSum.Text.Replace(",", "")) > 0 Then

			Else

				If CDbl(lstNumbers.Max) > CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)) Then

					Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)), 2)

				ElseIf CDbl(lstNumbers.Max) > CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)) * 0.2 Then

					Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl(lstNumbers.Max), 2)

				ElseIf CDbl(lstNumbers.Max) < CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)) * 0.2 Then

					Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl(CDbl(numRSABalance) * (1.08) ^ -((wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) / 12)) * 0.2, 2)

				End If


			End If








			'If CDbl(lstNumbers.Max) > CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)) Then

			'	Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)), 2)

			'ElseIf CDbl(lstNumbers.Max) > CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)) * 0.2 Then

			'	Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl(lstNumbers.Max), 2)

			'ElseIf CDbl(lstNumbers.Max) < CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)) * 0.2 Then

			'	Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl(CDbl(dt.Rows(0).Item("RFBalance")) * (1.08) ^ -((((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) / 12)) * 0.2, 2)

			'End If





			''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



			'If Me.txtMaxLumpSum.Text > (Me.txtRSABalance.Text * 0.5) Then
			'	Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl((Me.txtRSABalance.Text * 0.5)), 2)
			'ElseIf (txtMaxLumpSum.Text < (Me.txtRSABalance.Text * 0.5)) = True And (txtMaxLumpSum.Text > Me.txt25LumpSum.Text) = True Then
			'	txtRecommendedLumpSum.Text = FormatNumber(CDbl(txtMaxLumpSum.Text), 2)
			'ElseIf txtMaxLumpSum.Text < Me.txt25LumpSum.Text Then
			'	txtRecommendedLumpSum.Text = FormatNumber(CDbl(Me.txt25LumpSum.Text), 2)
			'End If

			'txtAdministrativeCharges
			'txtMaxMonthlyDraw


			Me.txtMaxMonthlyDraw.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, (para1 - (para1 * 0.2)), 0, 1)), 2)

			'(para1 * 0.2)

			Me.txtRecommendedMonthly.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, (para1 - txtMinLumpSum.Text), 0, 1)), 2)


			'txtUpfrontPayment.Text = FormatNumber(txtRecommendedLumpSum.Text + (txtMinMonthlyDraw.Text * CInt(Me.txtArrears.Text)), 2)


			'txtUpfrontPayment.Text = FormatNumber(txtRecommendedLumpSum.Text + (txtMinMonthlyDraw.Text * (((CDate(Me.txtDOP.Text) - CDate(Me.txtDOR.Text)).Days / 365) * 12) * (CInt(Me.txtFreq.Text) / 12)), 2)


			'txtUpfrontPayment.Text = FormatNumber(txtRecommendedLumpSum.Text, 2) + (txtMaxMonthlyDraw.Text * CInt(txtArrears.Text))


			'MsgBox("" & (txtMinMonthlyDraw.Text * (wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) * (CInt(Me.txtFreq.Text) / 12)))


			If CDbl(Me.txtRecommendedLumpSum.Text.Replace(",", "")) < 0 Then
				'Error
				spUpdateStatus.InnerText = "Error Calculating Template"
			ElseIf (CDbl(Me.txtMaxLumpSum.Text.Replace(",", "")) > CDbl(Me.txt25LumpSum.Text.Replace(",", ""))) = True And (Me.txtRecommendedLumpSum.Text.Replace(",", "") > Me.txtMaxLumpSum.Text.Replace(",", "")) = True Then
				'Error
				spUpdateStatus.InnerText = "Error Calculating Template"
			ElseIf (CDbl(Me.txtMaxLumpSum.Text.Replace(",", "")) < CDbl(Me.txt25LumpSum.Text.Replace(",", ""))) = True And (CDbl(Me.txtRecommendedLumpSum.Text.Replace(",", "")) > CDbl(Me.txt25LumpSum.Text.Replace(",", ""))) = True Then
				'Error
				spUpdateStatus.InnerText = "Error Calculating Template"
			Else

				Me.txtAgreedPension.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, (para1 - CDbl(Me.txtRecommendedLumpSum.Text.Replace(",", ""))), 0, 1)), 2)

			End If


			txtUpfrontPayment.Text = FormatNumber(txtRecommendedLumpSum.Text + (CDbl(txtAgreedPension.Text.Replace(",", "")) * (wsf.YearFrac(CDate(Me.txtDOR.Text), CDate(Me.txtDOP.Text), 1) * 12) * (CInt(Me.txtFreq.Text) / 12)), 2)


			'		Exit Sub



			Dim pwTemplate As New CalulatedPWTemplate

			pwTemplate.CreatedUser = CStr(Session("user"))
			pwTemplate.PIN = Me.txtPIN.Text
			pwTemplate.AdminCharges = Me.txtAdministrativeCharges.Text
			pwTemplate.Arrears = Me.txtArrears.Text
			pwTemplate.CurAge = Me.txtAgee.Text

			pwTemplate.DOB = CDate(Me.txtDOB.Text)
			pwTemplate.DOP = CDate(Me.txtDOP.Text)
			pwTemplate.DOR = CDate(Me.txtDOR.Text)

			pwTemplate.FinalSalary = CDbl(Me.txtFinalSalary.Text)
			pwTemplate.Frequency = CInt(Me.txtFreq.Text)

			pwTemplate.GradeLevel = Me.txtGradeLevel.Text
			pwTemplate.MaxLumpSum = CDbl(Me.txtMaxLumpSum.Text)
			pwTemplate.MaxMonthly = CDbl(Me.txtRecommendedMonthly.Text)

			pwTemplate.MgtCharges = CDbl(Me.txtManagement.Text.Split("%")(0))
			pwTemplate.MinLumpSum = CDbl(Me.txtMinLumpSum.Text)
			pwTemplate.MinMonthly = CDbl(Me.txtMinMonthlyDraw.Text)
			pwTemplate.Nc = Me.txtNc.Text
			pwTemplate.NxDx = Me.txtNxDx.Text

			pwTemplate.Rate = CDbl(Me.txtInterest.Text.Split("%")(0))
			pwTemplate.RegCharges = CDbl(Me.txtRegulator.Text.Split("%")(0))
			pwTemplate.RegLumpSum = CDbl(Me.txt25LumpSum.Text)
			pwTemplate.RegMonthly = CDbl(Me.txtMaxMonthlyDraw.Text)
			pwTemplate.RetirementAge = CInt(Me.txtRAge.Text)

			pwTemplate.RSABalance = CDbl(Me.txtRSABalance.Text)
			pwTemplate.SalaryStructure = Me.ddSS.SelectedItem.Text
			pwTemplate.Sex = Me.txtSex.Text
			pwTemplate.TotalUpfront = CDbl(Me.txtUpfrontPayment.Text)

			pwTemplate.SStep = Me.txtStep.Text

			pwTemplate.AgreedPension = CDbl(Me.txtAgreedPension.Text)
			pwTemplate.MonthBuffer = CDbl(Me.txtOneMonthPension.Text)


			Dim crr As New Core


			If crr.PMIsPWTemplateSaved(Me.txtPIN.Text) = True Then

				crr.PMSavePWTemplate(pwTemplate, True, Server.MapPath("~/Logs/"))

			Else

				crr.PMSavePWTemplate(pwTemplate, False, Server.MapPath("~/Logs/"))

			End If


		Catch ex As Exception

			spUpdateStatus.InnerText = "" & ex.Message
			spUpdateStatus.Visible = True



		End Try



	End Sub

	'Protected Sub calDOR_SelectionChanged(sender As Object, e As EventArgs) Handles calDOR.SelectionChanged

	'Me.calDOR_PopupControlExtender.Commit(Me.calDOR.SelectedDate)

	'End Sub

	'Protected Sub calDOP_SelectionChanged(sender As Object, e As EventArgs) Handles calDOP.SelectionChanged

	'Me.calDOP_PopupControlExtender.Commit(Me.calDOP.SelectedDate)

	'End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.btnSNR)

		If Page.IsPostBack = True Then
			'Exit Sub
		Else

		End If

	End Sub

	Private Function populateconsentForm(pin As String) As DataSet

		Dim cr As New Core, dtApplication As New DataTable, i As Integer = 0



		Dim ds As New PWCalculator
		Dim newPWRow As DataRow
		Dim dt As New DataTable


		Try
			If IsNothing(Session("UserFullName")) = True Then
				Response.Redirect("login.aspx")

			Else

			End If

			dt = ViewState("Retiree")


			newPWRow = ds.Tables(0).NewRow

			newPWRow("intAgreedMonths") = Me.txtOneMonthPension.Text.ToString
			newPWRow("numAnnuityValue") = CDbl(Me.txtPremiumValue.Text)
			newPWRow("txtFinalSalary") = Me.txtFinalSalary.Text.ToString

			newPWRow("txtRecievingOfficer") = CStr(Session("UserFullName")).ToString
			newPWRow("txtAddress") = dt.Rows(0).Item("ResidentialAddress").ToString
			newPWRow("dteDOP") = CDate(Me.txtDOP.Text)
			newPWRow("dteDOR") = CDate(Me.txtDOR.Text)
			newPWRow("dteDOB") = CDate(dt.Rows(0).Item("dateofbirth"))

			newPWRow("txtPIN") = txtPIN.Text
			newPWRow("txtFullName") = dt.Rows(0).Item("Surname").ToString & " " & dt.Rows(0).Item("FirstName").ToString & " " & dt.Rows(0).Item("MiddleName").ToString
			If Me.txtSex.Text = "M" Then
				newPWRow("txtSex") = "Male"
			Else
				newPWRow("txtSex") = "Female"
			End If

			newPWRow("txtRSABalance") = CDbl(ViewState("numRSABalance"))

			newPWRow("intAge") = Me.txtRAge.Text.Replace(",", "")

			newPWRow("numRecommendedLumpSum") = Me.txtUpfrontPayment.Text.Replace(",", "")
			newPWRow("numRecommendedMonthly") = Me.txtAgreedPension.Text.Replace(",", "")

			newPWRow("intMonthArrears") = Me.txtArrears.Text.Replace(",", "")
			newPWRow("numArrears") = CDbl(Me.txtUpfrontPayment.Text.Replace(",", "")) - CDbl(Me.txtRecommendedLumpSum.Text.Replace(",", ""))



			ds.Tables(0).Rows.Add(newPWRow)
			Return ds



		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Download Retirement Calculator"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)

			'	MsgBox("" & ex.Message)

		End Try


		'          Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & Year(Me.txtStartDate.Text) & "" & Month(Me.txtStartDate.Text) & "" & Day(Me.txtStartDate.Text) & "_" & Me.dcFund.SelectedValue & "_" & brokers.Item(i) & ".pdf"



	End Function


	Private Sub generateFiles(pin As String, path As String)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		If Me.chkIsAnnuity.Checked = True Then
			rdoc.Load(Server.MapPath("~/Report/ANConsent.rpt"))
		Else
			rdoc.Load(Server.MapPath("~/Report/PWConsent.rpt"))
		End If


		'If Not Directory.Exists(path) = True Then
		'    Directory.CreateDirectory(path)
		'End If

		Dim ds As DataSet

		ds = populateconsentForm(pin)

		rdoc.SetDataSource(ds.Tables(0))

		'crDiskFileDestinationOptions.DiskFileName = path & "MyTest.PDF"
		crDiskFileDestinationOptions.DiskFileName = path
		crExportOptions = rdoc.ExportOptions

		With crExportOptions

			.ExportDestinationType = ExportDestinationType.DiskFile
			.ExportFormatType = ExportFormatType.PortableDocFormat
			.ExportDestinationOptions = crDiskFileDestinationOptions
			.ExportFormatOptions = crFormatypeOption

		End With

		rdoc.Export()


	End Sub

	Private Sub DownLoadSNR()

		If IsNothing(ViewState("schedulePath")) = False Then

			If CStr(ViewState("schedulePath")).ToString = "" Then
				ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "File Not Available", True)
			Else
			End If


			Dim schedulePath As String = ViewState("schedulePath")
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
				'	MsgBox("" & ex.Message)
			End Try

		Else
			ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
		End If


	End Sub

	Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click

		Try

			If Me.chkIsAnnuity.Checked = True Then
				'Me.mpARLDetail.Show()
				Dim filePath As String = Server.MapPath("~/FileDownLoads/PW_Consent_" & Me.txtPIN.Text & ".pdf")

				generateFiles(txtPIN.Text, filePath)
				ViewState("schedulePath") = filePath
				DownLoadSNR()
				Exit Sub

			Else
				Dim filePath As String = Server.MapPath("~/FileDownLoads/PW_Consent_" & Me.txtPIN.Text & ".pdf")

				generateFiles(txtPIN.Text, filePath)
				ViewState("schedulePath") = filePath
				DownLoadSNR()

			End If

			


		Catch ex As Exception

		Finally

			GC.Collect()

		End Try

	End Sub

	Protected Sub btnOkay_Click(sender As Object, e As EventArgs) Handles btnOkay.Click

		ViewState("ANNDetails") = True

	End Sub

	Protected Sub btnUpDateEligibility_Click(sender As Object, e As EventArgs) Handles btnUpDateEligibility.Click

	End Sub
End Class
