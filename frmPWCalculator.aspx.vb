Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Partial Class frmPWCalculator
	Inherits System.Web.UI.Page


	Protected Sub autoCalculate(pin As String, lastSalary As Double, rsabalance As Double)


		Dim dt As New DataTable, cr As New Core
		Dim txtDOB, txtAgee, txtSex As String
		dt = cr.getPMPersonInformation(pin)

		ViewState("Retiree") = dt

		'dt.Rows(0).Item("dateofbirth")
		If dt.Rows.Count = 0 Then

			Exit Sub

		Else

			If IsDBNull(dt.Rows(0).Item("dateofbirth")) = True Then
				Exit Sub
			Else
			End If

		End If
		
		txtDOB = dt.Rows(0).Item("dateofbirth")



		Dim age As Integer



		age = (DateDiff(DateInterval.Year, CDate(txtDOB), CDate(Now.Date)))

		If age < 50 Then

			txtAgee = 50
			age = 50

		ElseIf age > 65 Then

			txtAgee = 65
			age = 65

		Else

			txtAgee = (DateDiff(DateInterval.Year, CDate(txtDOB), CDate(Now.Date)))

		End If


		txtSex = dt.Rows(0).Item("sex").ToString
		Dim txtRSABalance, txt25LumpSum, txtMinMonthlyDraw, txtNetInterest As Double

		'Balance Before Lumpsum

		'txtRSABalance = CDbl(dt.Rows(0).Item("RSABalance"))
		txtRSABalance = CDbl(rsabalance)
		txt25LumpSum = CDbl(txtRSABalance) * 0.25

		txtMinMonthlyDraw = (CDbl((Me.txtFinalSalary.Text)) / 12) * 0.5


		txtNetInterest = calInterate() * 100

		Dim txtNxDx, txtNc As String
		If txtSex = "M" Then

			txtNxDx = FormatNumber(cr.getNxDxPW(1, CInt(age), Me.txtFreq.Text), 8).ToString
		Else
			txtNxDx = FormatNumber(cr.getNxDxPW(0, CInt(age), Me.txtFreq.Text), 8).ToString

		End If

		txtNc = FormatNumber(txtNxDx - (11 / 24), 6).ToString


		cr.PMUpdateParticipantForFIN(pin, txtNxDx, txtNc)


		'Dim lstNumbers As New List(Of Double), lstNumbers2 As New List(Of Double), recommendedAmount As Double, para1 As Double, para2 As Double

		'para1 = (CDbl(txtRSABalance) - CDbl(Me.txtAdministrativeCharges.Text))
		'para2 = Financial.PV(calInterate() / 12, 2 * txtNc * Me.txtFreq.Text, Me.txtMinMonthlyDraw.Text, 0, 1)



		'recommendedAmount = para1 + para2

		'lstNumbers.Add(recommendedAmount)


		'lstNumbers.Add(0)
		'Me.txtMaxLumpSum.Text = FormatNumber(CDbl(lstNumbers.Max), 2)

		'If Me.txtMaxLumpSum.Text > (Me.txtRSABalance.Text * 0.5) Then
		'	Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl((Me.txtRSABalance.Text * 0.5)), 2)
		'ElseIf (txtMaxLumpSum.Text < (Me.txtRSABalance.Text * 0.5)) = True And (txtMaxLumpSum.Text > Me.txt25LumpSum.Text) = True Then
		'	txtRecommendedLumpSum.Text = FormatNumber(CDbl(txtMaxLumpSum.Text), 2)
		'ElseIf txtMaxLumpSum.Text < Me.txt25LumpSum.Text Then
		'	txtRecommendedLumpSum.Text = FormatNumber(CDbl(Me.txt25LumpSum.Text), 2)
		'End If


		'Me.txtMaxMonthlyDraw.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, (Me.txtRSABalance.Text - Me.txtMinLumpSum.Text - Me.txtAdministrativeCharges.Text), 0, 1)), 2)

		'Me.txtRecommendedMonthly.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtRSABalance.Text - Me.txtRecommendedLumpSum.Text - Me.txtAdministrativeCharges.Text, 0, 1)), 2)

	End Sub

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click



		Dim dt As New DataTable, cr As New Core, k As Integer

		'dt = cr.PMgetParticipantForFIN()

		'Do While k < dt.Rows.Count

		'	autoCalculate(dt.Rows(k).Item("rsapin"), dt.Rows(k).Item("LastSalary"), dt.Rows(k).Item("Balance Before Lumpsum"))
		'	k = k + 1

		'Loop

		'Exit Sub

		'dt = cr.getPMPersonInformation(Me.txtPIN.Text)

		'ViewState("Retiree") = dt

		'Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")



		Dim age As Integer



		age = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Now.Date)))

		'If age < 50 Then

		'	Me.txtAgee.Text = 50
		'	age = 50

		'ElseIf age > 65 Then

		'	Me.txtAgee.Text = 65
		'	age = 65

		'Else

		'	Me.txtAgee.Text = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Now.Date)))

		'End If

		age = txtAgee.Text

		'Me.txtSex.Text = dt.Rows(0).Item("sex").ToString
		'Me.txtRSABalance.Text = FormatNumber(CDbl(dt.Rows(0).Item("RSABalance")), 2)

		Me.txt25LumpSum.Text = FormatNumber(CDbl((Me.txtRSABalance.Text)) * 0.25, 2)

		Me.txtMinMonthlyDraw.Text = FormatNumber((CDbl((Me.txtFinalSalary.Text)) / 12) * 0.5, 2)


		txtNetInterest.Text = FormatNumber(calInterate() * 100, 2)


		If Me.txtSex.Text = "M" Then

			Me.txtNxDx.Text = FormatNumber(cr.getNxDxPW(1, CInt(age), CInt(Me.txtFreq.Text)), 8)
		Else
			Me.txtNxDx.Text = FormatNumber(cr.getNxDxPW(0, CInt(age), CInt(Me.txtFreq.Text)), 8)

		End If

		Me.txtNc.Text = FormatNumber(Me.txtNxDx.Text - (11 / 24), 6)


		Dim lstNumbers As New List(Of Double), lstNumbers2 As New List(Of Double), recommendedAmount As Double, para1 As Double, para2 As Double

		para1 = (CDbl(Me.txtRSABalance.Text) - CDbl(Me.txtAdministrativeCharges.Text))
		para2 = Financial.PV(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtMinMonthlyDraw.Text, 0, 1)

		'	recommendedAmount = FormatNumber(((CDbl(Me.txtRSABalance.Text) - CDbl(Me.txtAdministrativeCharges.Text)) + Financial.PV(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtMinMonthlyDraw.Text, 0, 1)), 2)

		recommendedAmount = para1 + para2

		lstNumbers.Add(recommendedAmount)


		lstNumbers.Add(0)
		Me.txtMaxLumpSum.Text = FormatNumber(CDbl(lstNumbers.Max), 2)

		If Me.txtMaxLumpSum.Text > (Me.txtRSABalance.Text * 0.5) Then
			Me.txtRecommendedLumpSum.Text = FormatNumber(CDbl((Me.txtRSABalance.Text * 0.5)), 2)
		ElseIf (txtMaxLumpSum.Text < (Me.txtRSABalance.Text * 0.5)) = True And (txtMaxLumpSum.Text > Me.txt25LumpSum.Text) = True Then
			txtRecommendedLumpSum.Text = FormatNumber(CDbl(txtMaxLumpSum.Text), 2)
		ElseIf txtMaxLumpSum.Text < Me.txt25LumpSum.Text Then
			txtRecommendedLumpSum.Text = FormatNumber(CDbl(Me.txt25LumpSum.Text), 2)
		End If


		Me.txtMaxMonthlyDraw.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, (Me.txtRSABalance.Text - Me.txtMinLumpSum.Text - Me.txtAdministrativeCharges.Text), 0, 1)), 2)

		Me.txtRecommendedMonthly.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtRSABalance.Text - Me.txtRecommendedLumpSum.Text - Me.txtAdministrativeCharges.Text, 0, 1)), 2)




	End Sub

	Private Function calInterate() As Double

		Dim C12 As Double
		Dim C13 As Double
		Dim C14 As Double


		C12 = FormatNumber(CDbl(Me.txtManagement.Text.Split("%")(0)) / 100, 5)
		C13 = FormatNumber(CDbl(Me.txtRegulator.Text.Split("%")(0)) / 100, 5)
		C14 = FormatNumber(CDbl(Me.txtInterest.Text.Split("%")(0)) / 100, 5)

		Return C14 * (1 - (C12 + C13))

	End Function

	Private Sub DownLoadSNR()

		If IsNothing(ViewState("schedulePath")) = False Then

			If CStr(ViewState("schedulePath")).ToString = "" Then
				ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
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
				'  MsgBox("" & ex.Message)
			End Try

		Else
			ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
		End If


	End Sub


	Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click

		Try


			Dim filePath As String = Server.MapPath("~/FileDownLoads/PW_Calculator_" & Me.txtPIN.Text & ".pdf")

			generateFiles(txtPIN.Text, filePath)
			ViewState("schedulePath") = filePath
			DownLoadSNR()


		Catch ex As Exception

		Finally

			GC.Collect()

		End Try

	End Sub

	Private Function populateconsentForm(pin As String) As DataSet

		Dim cr As New Core, dtApplication As New DataTable, i As Integer = 0

		

		Dim ds As New PWCalculator
		Dim newPWRow As DataRow
		Dim dt As New DataTable


		Try


			dt = ViewState("Retiree")

			newPWRow = ds.Tables(0).NewRow

			newPWRow("txtPIN") = txtPIN.Text
			newPWRow("txtFullName") = dt.Rows(0).Item("Surname").ToString & " " & dt.Rows(0).Item("FirstName").ToString & " " & dt.Rows(0).Item("MiddleName").ToString
			If Me.txtSex.Text = "M" Then
				newPWRow("txtSex") = 1
			Else
				newPWRow("txtSex") = 0
			End If

			newPWRow("txtRSABalance") = CDbl(dt.Rows(0).Item("RSABalance"))

			newPWRow("txtFinalSalary") = Me.txtFinalSalary.Text.Replace(",", "")
			newPWRow("intAge") = Me.txtAgee.Text.Replace(",", "")
			newPWRow("numMinLumpSum") = Me.txtMinLumpSum.Text.Replace(",", "")

			newPWRow("num25PercentLumpSum") = Me.txt25LumpSum.Text.Replace(",", "")

			newPWRow("numMaxStatutory") = Me.txtMaxLumpSum.Text.Replace(",", "")
			newPWRow("numRecommendedLumpSum") = Me.txtRecommendedLumpSum.Text.Replace(",", "")
			newPWRow("numAdminCharges") = Me.txtAdministrativeCharges.Text

			newPWRow("numManagementCharges") = Me.txtManagement.Text
			newPWRow("numRegulatoryCharges") = Me.txtRegulator.Text
			newPWRow("numInterestRate") = Me.txtInterest.Text

			newPWRow("numInterestRateNet") = Me.txtNetInterest.Text

			newPWRow("numNxDx") = Me.txtNxDx.Text
			newPWRow("numNc") = Me.txtNc.Text
			newPWRow("intFreq") = Me.txtFreq.Text

			newPWRow("numMinMonthStatutory") = Me.txtMinMonthlyDraw.Text.Replace(",", "")
			newPWRow("numMaxMonthly") = Me.txtMaxMonthlyDraw.Text.Replace(",", "")
			newPWRow("numRecommendedMonthly") = Me.txtRecommendedMonthly.Text.Replace(",", "")


			ds.Tables(0).Rows.Add(newPWRow)
			Return ds



		Catch ex As Exception

			MsgBox("" & ex.Message)

		End Try


		'          Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & Year(Me.txtStartDate.Text) & "" & Month(Me.txtStartDate.Text) & "" & Day(Me.txtStartDate.Text) & "_" & Me.dcFund.SelectedValue & "_" & brokers.Item(i) & ".pdf"



	End Function

	Private Sub generateFiles(pin As String, path As String)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		rdoc.Load(Server.MapPath("~/Report/PWCalculator.rpt"))

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

		Dim dtusers As New DataTable

		If IsPostBack = False Then



			If IsNothing(Session("user")) = True Then


				'	Response.Redirect("Login.aspx")

			ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

				'dtusers = Session("userDetails")
				'getUserAccessMenu(Session("user"))

			End If
		End If


	End Sub

	Protected Sub btnUpDateEligibility_Click(sender As Object, e As EventArgs) Handles btnUpDateEligibility.Click

	End Sub
End Class
