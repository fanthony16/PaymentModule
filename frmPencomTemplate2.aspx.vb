Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Partial Class frmPencomTemplate2
	Inherits System.Web.UI.Page

	Protected Sub BatchProcessing()
		Dim dt As New DataTable, cr As New Core, i As Integer, txtMonthPencomm As Double, txtSexx As String, txtRSABalancee As Double, txtAgee As Double, txtNetInterestt As String, txtNxDxx As Double, txtNcc As Decimal, txtRecommendedAmountt As Double, txtVariancee As String, txtFreqq As String, ds As New dsBenefitEnhancement, txtYearNumber As String, txtSurplus As Double, txtDOB As Date, txtRetirementDate As String, txtRSABalance As Double, txtRSABalanceIFRS As Double, txtNetInterest As Double, txtReservee As Double, txtMaxEnhancedd As Double

		Dim mycon As New SqlClient.SqlConnection, db As New DbConnection, dtEnhancement As DataTable
		mycon = db.getConnection("PaymentModule")

		'txtFreqq = 12

		dtEnhancement = cr.PMgetRetireeForEnhencement()

		Dim txtReviewDate As Date, txtFreq As Double = 12
		'txtReviewDate = CDate("2016-12-31") 'first enhancement window

		txtReviewDate = CDate("2019-10-31") 'second enhancement window




		Do While i < dtEnhancement.Rows.Count


			dt = cr.getPMPersonInformation(dtEnhancement.Rows(i).Item("PIN"))

			If dt.Rows(0).Item("AgeAtRetirement") = 0 Then
				
			Else
				ViewState("Retiree") = dt
			End If

			txtDOB = dt.Rows(0).Item("dateofbirth")

			txtRetirementDate = dtEnhancement.Rows(i).Item("Date Of Retirement").ToString

			Dim dd As Date, age As Integer

			dd = DateAdd(DateInterval.Year, (DateDiff(DateInterval.Year, CDate(dt.Rows(0).Item("dateofbirth")), CDate(txtReviewDate))), CDate(dt.Rows(0).Item("dateofbirth")))


			'Me.txtAge.Text = (DateDiff(DateInterval.Year, CDate(dt.Rows(0).Item("dateofbirth")), CDate(txtReviewDate)))
			age = (DateDiff(DateInterval.Year, CDate(dt.Rows(0).Item("dateofbirth")), CDate(txtReviewDate)))


			''''''''''''''Anniversary Calculation''''''''''''''''''

			If Month(CDate(txtDOB)) > Month(CDate(txtReviewDate)) Then
				age = age - 1
			Else
			End If
			'''''''''''''''''''''''''''''''''''''''''''''''''''''''


			''''''''''''''quarterly''''''''''''''''''''


			If dt.Rows(0).Item("Frequency") = 1 Then

				txtFreq = "12"
				txtMonthPencomm = dt.Rows(0).Item("LastPensionAmount")

			ElseIf dt.Rows(0).Item("Frequency") = 3 Then

				txtMonthPencomm = FormatNumber(CDbl(dt.Rows(0).Item("LastPensionAmount")) / 3, 2)
				txtFreq = "12"

			Else

				txtFreq = "12"
				txtMonthPencomm = dt.Rows(0).Item("LastPensionAmount")

			End If


			'''''''''''''''''''''''''''''''''''''''''''



			'txtMonthPencomm = dt.Rows(0).Item("LastPensionAmount")
			txtSexx = dt.Rows(0).Item("sex").ToString
			'''''''''''''
			'IFRS Version
			'''''''''

			'MsgBox("" & cr.getPMIFRSBalance(dtEnhancement.Rows(i).Item("EmployeeID")))

			'MsgBox("" & CDbl(cr.getPMIFRSBalance(dtEnhancement.Rows(i).Item("EmployeeID")) * 2.2104))

			'Exit Sub

			'txtRSABalance = FormatNumber(CDbl(cr.getPMIFRSBalance(dtEnhancement.Rows(i).Item("EmployeeID")) * 2.2104), 2)

			txtRSABalance = FormatNumber(CDbl(dt.Rows(0).Item("YearEndRFBalance")), 2)

			txtNetInterest = FormatNumber(calInterate() * 100, 2)

			If txtSexx = "M" Then
				txtNxDxx = FormatNumber(cr.getNxDx(1, CInt(age), txtFreq), 8)
			Else
				txtNxDxx = FormatNumber(cr.getNxDx(0, CInt(age), txtFreq), 8)
			End If

			txtNcc = FormatNumber(txtNxDxx - (11 / 24), 6)
			Dim lstNumbers As New List(Of Double), lstNumbers2 As New List(Of Double), recommendedAmount As Double

			recommendedAmount = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * txtNcc * txtFreq, txtRSABalance, 0, 1)), 2)

			txtMaxEnhancedd = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * txtNcc * txtFreq, txtRSABalance, 0, 1)), 2)

			lstNumbers.Add(recommendedAmount)

			lstNumbers.Add(txtMonthPencomm * (1.5))

			lstNumbers2.Add(txtMonthPencomm)
			lstNumbers2.Add(lstNumbers.Min())

			txtRecommendedAmountt = lstNumbers2.Max().ToString("#,##.00")

			lstNumbers = New List(Of Double)
			lstNumbers.Clear()

			lstNumbers.Add(0)
			lstNumbers.Add(CDbl(txtRSABalance) + Financial.PV(calInterate() / 12, 2 * txtNcc * CInt(12), CDbl(txtMonthPencomm), 0, 1))

			txtReservee = lstNumbers.Max().ToString("#,##.00")

			txtSurplus = (CDbl(txtRSABalance) + Financial.PV(calInterate() / 12, 2 * txtNcc * CInt(12), CDbl(txtMonthPencomm), 0, 1)).ToString("#,##.00")


			Dim ddate As Date = CDate("2019-10-31")

			Try
				'NGAP  tblForTad

				Dim MyDataAdapter As SqlClient.SqlDataAdapter
				MyDataAdapter = New SqlClient.SqlDataAdapter("INSERT INTO [dbo].[tblPensionEnhancement]([txtSurname],[txtFirstName],[txtMiddleName],[txtPIN],[txtGender],[dteDOB] ,[dteDOR],[txtEmployerName],[txtEmployerCode],[numRSABalance],[numMonthPension],[intAge],[Nc],[NxDxNc],[numEnhancement],[numMaxEnhancement],[numReserve],[numSurplus],dterunfor)	VALUES('" & dt.Rows(0).Item("Surname").ToString.Replace("'", "''") & "','" & dt.Rows(0).Item("FirstName").ToString.Replace("'", "''") & "','" & dt.Rows(0).Item("MiddleName").ToString.Replace("'", "''") & "','" & dt.Rows(0).Item("rsapin") & "','" & dt.Rows(0).Item("sex") & "','" & dt.Rows(0).Item("dateofbirth").ToString & "','" & dt.Rows(0).Item("DateOfResignation").ToString & "','" & dt.Rows(0).Item("EmployerName").ToString.Replace("'", "''") & "','" & dt.Rows(0).Item("EmployerCode") & "','" & dt.Rows(0).Item("YearEndRFBalance") & "','" & txtMonthPencomm & "','" & age & "','" & txtNcc & "','" & txtNxDxx & "','" & txtRecommendedAmountt & "','" & txtMaxEnhancedd & "','" & txtReservee & "','" & txtSurplus & "','" & DateTime.Parse(ddate).ToString("yyyy-MM-dd HH:MM") & "')", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.ExecuteNonQuery()








				'IFRS()
				'Dim MyDataAdapter As New SqlClient.SqlDataAdapter
				'MyDataAdapter = New SqlClient.SqlDataAdapter("INSERT INTO [dbo].[tblPensionEnhancementIFRS]([txtSurname],[txtFirstName],[txtMiddleName],[txtPIN],[txtGender],[dteDOB] ,[dteDOR],[txtEmployerName],[txtEmployerCode],[numRSABalance],[numMonthPension],[intAge],[Nc],[NxDxNc],[numEnhancement],[numMaxEnhancement],[numReserve],[numSurplus],dterunfor)	VALUES('" & dt.Rows(0).Item("Surname") & "','" & dt.Rows(0).Item("FirstName") & "','" & dt.Rows(0).Item("MiddleName") & "','" & dt.Rows(0).Item("rsapin") & "','" & dt.Rows(0).Item("sex") & "','" & dt.Rows(0).Item("dateofbirth").ToString & "','" & dt.Rows(0).Item("DateOfResignation").ToString & "','" & dt.Rows(0).Item("EmployerName") & "','" & dt.Rows(0).Item("EmployerCode") & "','" & txtRSABalance & "','" & dt.Rows(0).Item("LastPensionAmount") & "','" & age & "','" & txtNcc & "','" & txtNxDxx & "','" & txtRecommendedAmountt & "','" & txtMaxEnhancedd & "','" & txtReservee & "','" & txtSurplus & "','" & DateTime.Parse(ddate).ToString("yyyy-MM-dd HH:MM") & "')", mycon)
				'MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				'MyDataAdapter.SelectCommand.ExecuteNonQuery()




				'DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM")


			Catch Ex As Exception
				'MsgBox("" & Ex.Message)
			Finally

			End Try

			i = i + 1

		Loop

		mycon.Close()

	End Sub


	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim sarrMyString As String() = Me.txtPIN.Text.ToString.Split(New String() {"PEN"}, StringSplitOptions.None)

		BatchProcessing()
		Exit Sub

		Dim dt, dtEnhancement As New DataTable, cr As New Core
		dt = cr.getPMPersonInformation(Me.txtPIN.Text)
		dtEnhancement = cr.PMgetRetireeForEnhencement(txtPIN.Text)

		If dt.Rows(0).Item("AgeAtRetirement") = 0 Then
			spError.InnerText = "Retirement Date is Not Available for the customer!!!"
			Me.spError.Visible = True
			Exit Sub
		Else
			ViewState("Retiree") = dt
			'ResidentialAddress
		End If

		'MsgBox("" & dt.Rows(0).Item("Frequency"))

		If dt.Rows(0).Item("Frequency") = 1 Then
			Me.txtFreq.Text = "12"


		ElseIf dt.Rows(0).Item("Frequency") = 3 Then

			Me.txtFreq.Text = "12"

		Else
			Me.txtFreq.Text = "12"

		End If


		Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")
		'dor on enpower
		'	Me.txtRetirementDate.Text = dt.Rows(0).Item("DateRetired").ToString
		'dor from bpd
		Me.txtRSABalance.Text = FormatNumber(CDbl(dt.Rows(0).Item("YearEndRFBalance")), 2)
		Me.txtRetirementDate.Text = dtEnhancement.Rows(0).Item("Date Of Retirement").ToString

		Dim dd As Date, age As Integer

		dd = DateAdd(DateInterval.Year, (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtReviewDate.Text))), CDate(Me.txtDOB.Text))

		Me.txtAge.Text = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtReviewDate.Text)))
		age = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtReviewDate.Text)))


		''''''''''''''Anniversary Calculation''''''''''''''''''

		If Month(CDate(Me.txtDOB.Text)) > Month(CDate(Me.txtReviewDate.Text)) Then
			age = age - 1
		Else
		End If
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''
		'	MsgBox("" & Me.txtDOB.Text & " : " & Me.txtReviewDate.Text)
		'Exit 



		''''''''''''''quarterly''''''''''''''''''''


		If dt.Rows(0).Item("Frequency") = 1 Then

			Me.txtFreq.Text = "12"
			Me.txtMonthPencom.Text = dt.Rows(0).Item("LastPensionAmount")

		ElseIf dt.Rows(0).Item("Frequency") = 3 Then

			Me.txtMonthPencom.Text = FormatNumber(CDbl(dt.Rows(0).Item("LastPensionAmount")) / 3, 2)

		Else
			Me.txtFreq.Text = "12"
			Me.txtMonthPencom.Text = dt.Rows(0).Item("LastPensionAmount")
		End If


		'''''''''''''''''''''''''''''''''''''''''''




		Me.txtSex.Text = dt.Rows(0).Item("sex").ToString


		txtNetInterest.Text = FormatNumber(calInterate() * 100, 2)


		If Me.txtSex.Text = "M" Then
			Me.txtNxDx.Text = FormatNumber(cr.getNxDx(1, CInt(age), CInt(txtFreq.Text)), 8)
		Else
			Me.txtNxDx.Text = FormatNumber(cr.getNxDx(0, CInt(age), CInt(txtFreq.Text)), 8)
		End If

		Me.txtNc.Text = FormatNumber(Me.txtNxDx.Text - (11 / 24), 6)
		Dim lstNumbers As New List(Of Double), lstNumbers2 As New List(Of Double), recommendedAmount As Double

		recommendedAmount = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtRSABalance.Text, 0, 1)), 2)

		txtMaxEnhanced.Text = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtRSABalance.Text, 0, 1)), 2)

		lstNumbers.Add(recommendedAmount)

		lstNumbers.Add(txtMonthPencom.Text * (1.5))

		lstNumbers2.Add(Me.txtMonthPencom.Text)
		lstNumbers2.Add(lstNumbers.Min())


		txtRecommendedAmount.Text = lstNumbers2.Max().ToString("#,##.00")

		lstNumbers = New List(Of Double)
		lstNumbers.Clear()


		lstNumbers.Add(0)
		lstNumbers.Add(CDbl(txtRSABalance.Text) + Financial.PV(calInterate() / 12, 2 * Me.txtNc.Text * CInt(Me.txtFreq.Text), CDbl(Me.txtMonthPencom.Text), 0, 1))
		Me.txtReserve.Text = lstNumbers.Max().ToString("#,##.00")


		txtSurplus.Text = (CDbl(txtRSABalance.Text) + Financial.PV(calInterate() / 12, 2 * Me.txtNc.Text * CInt(Me.txtFreq.Text), CDbl(Me.txtMonthPencom.Text), 0, 1)).ToString("#,##.00")


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


		Dim dtusers As New DataTable, roleID As Integer

		'If IsPostBack = False Then



		'	If IsNothing(Session("user")) = True Then


		'		Response.Redirect("Login.aspx")
		'	ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

		'		dtusers = Session("userDetails")
		'		getUserAccessMenu(Session("user"))

		'		If IsNothing(Session("RoleID")) = False Then
		'			roleID = Session("RoleID")
		'			If roleID = 3014 Then
		'				Me.btnUpDateEligibility.Visible = True
		'			Else
		'				Me.btnUpDateEligibility.Visible = False
		'			End If
		'		Else
		'		End If

		'	End If
		'End If


	End Sub

	Private Function populateconsentForm(pin As String) As DataSet

		Dim cr As New Core, dtApplication As New DataTable, i As Integer = 0
		'dtApplication = cr.PMgetApplicationByPIN("PEN100000189215", 2)
		'dtApplication = cr.PMgetApplicationByPIN(pin, appCode)

		Dim ds As New dsBenefitEnhancement
		Dim newSNRow As DataRow
		Dim dt As New DataTable

		dt = ViewState("Retiree")
		'ResidentialAddress

		newSNRow = ds.Tables(0).NewRow

		newSNRow("txtbatchNo") = ""
		newSNRow("dteSubmission") = Now.Date
		newSNRow("txtPFAName") = "LEADWAY PENSURE PFA"
		newSNRow("txtPFACode") = "023"

		newSNRow("txtSurname") = dt.Rows(0).Item("Surname").ToString
		newSNRow("txtFirstName") = dt.Rows(0).Item("FirstName").ToString
		newSNRow("txtMiddleName") = dt.Rows(0).Item("MiddleName").ToString

		newSNRow("dteDOB") = dt.Rows(0).Item("dateofbirth").ToString
		newSNRow("dteDOR") = dt.Rows(0).Item("DateRetired").ToString

		newSNRow("intAge") = Me.txtAge.Text
		newSNRow("txtPIN") = Me.txtPIN.Text
		newSNRow("txtGender") = Me.txtSex.Text
		newSNRow("txtEmployerCode") = ""

		newSNRow("numMonthPension") = Me.txtMonthPencom.Text.Replace(",", "")
		newSNRow("numRSABalance") = Me.txtRSABalance.Text.Replace(",", "")
		newSNRow("numEnhanceAmount") = Me.txtRecommendedAmount.Text.Replace(",", "")

		newSNRow("txtAddress") = dt.Rows(0).Item("ResidentialAddress").ToString

		'newSNRow("dteEffectiveDate") = CDate(Me.txtReviewDate.Text)
		newSNRow("dteEffectiveDate") = CDate("2020-02-29")


		If IsDBNull(dt.Rows(0).Item("LumpSumPayDate")) = True Then

			newSNRow("dteLumpSum") = CDate(dt.Rows(0).Item("FirstPensionDate"))
		Else

			newSNRow("dteLumpSum") = CDate(dt.Rows(0).Item("LumpSumPayDate"))
		End If

		newSNRow("dteState") = dt.Rows(0).Item("CustomerState")
		newSNRow("dteLGA") = dt.Rows(0).Item("CustomerStateLGA")


		ds.Tables(0).Rows.Add(newSNRow)
		Return ds


		'          Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & Year(Me.txtStartDate.Text) & "" & Month(Me.txtStartDate.Text) & "" & Day(Me.txtStartDate.Text) & "_" & Me.dcFund.SelectedValue & "_" & brokers.Item(i) & ".pdf"



	End Function

	Private Sub generateFiles(pin As String, path As String)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		rdoc.Load(Server.MapPath("~/Report/PWEnhancementConsent.rpt"))

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
		'rsource.Export()

	End Sub

	Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click


		Try


			Dim filePath As String = Server.MapPath("~/FileDownLoads/" & Me.txtPIN.Text & ".pdf")

			generateFiles(txtPIN.Text, filePath)
			ViewState("schedulePath") = filePath
			DownLoadSNR()


		Catch ex As Exception

		Finally

			GC.Collect()

		End Try


	End Sub

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

	Protected Sub btnUpDateEligibility_Click(sender As Object, e As EventArgs) Handles btnUpDateEligibility.Click

		Dim cr As New Core, PE As New ApplicationEnhancement

		PE.RSAPin = txtPIN.Text
		PE.RSABalance = CDbl(Me.txtRSABalance.Text)
		PE.CurPension = Me.txtMonthPencom.Text
		PE.Nc = Me.txtNc.Text
		PE.NxDx = Me.txtNxDx.Text
		PE.EnhancedPension = Me.txtRecommendedAmount.Text
		PE.MaxEnhancement = Me.txtMaxEnhanced.Text
		PE.ReviewDate = CDate(Me.txtReviewDate.Text)
		PE.Surplus = Me.txtSurplus.Text
		PE.Reserve = Me.txtReserve.Text

		cr.PMUpdateEnhancementTemplate(PE)
		spUpdateStatus.Visible = True
		spUpdateStatus.InnerText = "Updated Successfully"


	End Sub

End Class
