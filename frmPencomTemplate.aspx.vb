Imports System.Data
Imports CrystalDecisions.Shared
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Partial Class frmPencomTemplate
	Inherits System.Web.UI.Page

	'Protected Sub txtAge_TextChanged(sender As Object, e As EventArgs) Handles txtAge.TextChanged

	Private Function calInterateBatch(intAge As Integer, management As String, regulator As String) As Double

		'		MsgBox("Okay")
		Dim C12 As Double
		Dim C13 As Double
		Dim C14 As Double
		Dim txtInterestt As String


		If CInt(intAge) < 65 Then
			txtInterestt = "8%"
		Else
			txtInterestt = "10%"
		End If

		C12 = FormatNumber(CDbl(management.Split("%")(0)) / 100, 5)
		C13 = FormatNumber(CDbl(regulator.Split("%")(0)) / 100, 5)
		C14 = FormatNumber(CDbl(txtInterestt.Split("%")(0)) / 100, 5)

		'txtNetInterest.Text = FormatNumber(((1 - (C12 + C13)) * C14) * 100, 2)

		'txtNetInterest.Text = ((1 - (C12 + C13)) * C14)
		Return ((1 - (C12 + C13)) * C14)


	End Function

	Private Function calInterate(intAge As Integer) As Double

		'		MsgBox("Okay")
		Dim C12 As Double
		Dim C13 As Double
		Dim C14 As Double



		If CInt(Me.txtAge.Text) < 65 Then
			Me.txtInterest.Text = "8%"
		Else
			Me.txtInterest.Text = "10%"
		End If

		C12 = FormatNumber(CDbl(Me.txtManagement.Text.Split("%")(0)) / 100, 5)
		C13 = FormatNumber(CDbl(Me.txtRegulator.Text.Split("%")(0)) / 100, 5)
		C14 = FormatNumber(CDbl(Me.txtInterest.Text.Split("%")(0)) / 100, 5)

		'txtNetInterest.Text = FormatNumber(((1 - (C12 + C13)) * C14) * 100, 2)

		'txtNetInterest.Text = ((1 - (C12 + C13)) * C14)
		Return ((1 - (C12 + C13)) * C14)


	End Function

	Protected Sub BatchProcessing()
		Dim dt As New DataTable, cr As New Core, i As Integer, txtMonthPencomm As Double, txtSexx As String, txtRSABalancee As Double, txtAgee As Double, txtNetInterestt As String, txtNxDxx As String, txtNcc As String, txtRecommendedAmountt As String, txtVariancee As String, txtFreqq As String, ds As New dsBenefitEnhancement, txtYearNumber As String

		Dim mycon As New SqlClient.SqlConnection, db As New DbConnection
		mycon = db.getConnection("Enpowerv4")

		txtFreqq = 12
		dt = cr.PMgetRetireeForEnhencement()


		Do While i < dt.Rows.Count
			Dim newBERow As DataRow, dtPersion As New DataTable
			dtPersion = cr.getPMPersonInformation(dt.Rows(i).Item(1).ToString)

			txtMonthPencomm = dtPersion.Rows(0).Item("LastPensionAmount")
			txtSexx = dtPersion.Rows(0).Item("sex").ToString
			txtRSABalancee = CDbl(dtPersion.Rows(0).Item("YearEndRFBalance"))
			txtAgee = dtPersion.Rows(0).Item("AgeAtRetirement")
			txtNetInterestt = FormatNumber(calInterateBatch(CInt(dtPersion.Rows(0).Item("AgeAtRetirement")), "5.00%", "0.30%") * 100, 2)
			If txtSexx = "M" Then
				txtNxDxx = FormatNumber(cr.getNxDx(1, CInt(txtAgee)), 10)

				txtYearNumber = FormatNumber((cr.getNxDx(1, CInt(txtAgee))) + (txtNxDxx - (11 / 24)), 10)
			Else
				txtNxDxx = FormatNumber(cr.getNxDx(0, CInt(txtAgee)), 10)

				txtYearNumber = FormatNumber((cr.getNxDx(0, CInt(txtAgee))) + (txtNxDxx - (11 / 24)), 10)

			End If

			txtNcc = FormatNumber(txtNxDxx - (11 / 24), 10)

			txtRecommendedAmountt = FormatNumber((-1 * Financial.Pmt(calInterateBatch(CInt(dtPersion.Rows(0).Item("AgeAtRetirement")), "5.00%", "0.30%") / 12, 2 * txtNcc * txtFreqq, txtRSABalancee, 0, 1)) - 0, 2)
			txtVariancee = FormatNumber((-1 * Financial.Pmt(calInterateBatch(CInt(dtPersion.Rows(0).Item("AgeAtRetirement")), "5.00%", "0.30%") / 12, 2 * txtNcc * txtFreqq, txtRSABalancee, 0, 1)) - dtPersion.Rows(0).Item("LastPensionAmount"), 2)




			newBERow = ds.Tables(0).NewRow
			newBERow("txtBatchNo") = "00000"
			newBERow("dteSubmission") = Now.Date
			newBERow("txtPFAName") = "LEADWAY PENSURE PFA LTD"
			newBERow("txtPFACode") = "023"
			newBERow("txtSurname") = dtPersion.Rows(0).Item("Surname")
			newBERow("txtFirstName") = dtPersion.Rows(0).Item("FirstName")
			newBERow("txtMiddleName") = dtPersion.Rows(0).Item("MiddleName")
			newBERow("txtPIN") = dtPersion.Rows(0).Item("rsapin")
			newBERow("txtGender") = dtPersion.Rows(0).Item("sex")
			newBERow("dteDOB") = dtPersion.Rows(0).Item("dateofbirth")
			newBERow("dteDOR") = dtPersion.Rows(0).Item("DateOfResignation")

			newBERow("txtEmployerName") = dtPersion.Rows(0).Item("EmployerName")
			newBERow("txtEmployerCode") = dtPersion.Rows(0).Item("EmployerCode")

			newBERow("numRSABalance") = dtPersion.Rows(0).Item("YearEndRFBalance")
			newBERow("numMonthPension") = dtPersion.Rows(0).Item("LastPensionAmount")
			newBERow("numCurrentRSABalance") = dtPersion.Rows(0).Item("RFBalance")
			newBERow("numNewPension") = CDbl(txtRecommendedAmountt.Replace(",", ""))

			newBERow("txtBPD") = "Tade Gbadebo"
			'newBERow("imgBPD") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteBPD") = Now.Date

			newBERow("txtCompliance") = "Akindele Fayombo"
			'newBERow("imgCompliance") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteCompliance") = Now.Date

			newBERow("txtMD") = CDbl(txtYearNumber.Replace(",", ""))
			'newBERow("imgBPD") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteMD") = Now.Date

			ds.Tables(0).Rows.Add(newBERow)




			
			Try
				Dim MyDataAdapter As SqlClient.SqlDataAdapter
				MyDataAdapter = New SqlClient.SqlDataAdapter("INSERT INTO [dbo].[tblPencomFormat]([txtSurname],[txtFirstName],[txtMiddleName],[txtPIN],[txtGender],[dteDOB] ,[dteDOR],[txtEmployerName],[txtEmployerCode],[numRSABalance],[numMonthPension],[numCurrentRSABalance],[numNewPension],[NxDxNc])	VALUES('" & dtPersion.Rows(0).Item("Surname") & "','" & dtPersion.Rows(0).Item("FirstName") & "','" & dtPersion.Rows(0).Item("MiddleName") & "','" & dtPersion.Rows(0).Item("rsapin") & "','" & dtPersion.Rows(0).Item("sex") & "','" & dtPersion.Rows(0).Item("dateofbirth").ToString & "','" & dtPersion.Rows(0).Item("DateOfResignation").ToString & "','" & dtPersion.Rows(0).Item("EmployerName") & "','" & dtPersion.Rows(0).Item("EmployerCode") & "','" & dtPersion.Rows(0).Item("YearEndRFBalance") & "','" & dtPersion.Rows(0).Item("LastPensionAmount") & "','" & dtPersion.Rows(0).Item("RFBalance") & "','" & CDbl(txtRecommendedAmountt.Replace(",", "")) & "','" & CDbl(txtYearNumber.Replace(",", "")) & "')", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.ExecuteNonQuery()




			Catch Ex As Exception
				'MsgBox("" & Ex.Message)
			Finally

			End Try






			i = i + 1

		Loop

		mycon.Close()

		'	cr.ExtractCSV(ds.Tables(0), "BenefitEnhancementRequest")



		'Dim crExportOptions As New ExportOptions
		'Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		'Dim crFormatypeOption As New PdfRtfWordFormatOptions
		'Dim rdoc As New ReportDocument
		'Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		'Try

		'	Dim filePath As String = Server.MapPath("~/FileDownLoads/BenefitEnhancementRequest.pdf")
		'	rdoc.Load(Server.MapPath("~/Report/BenefitEnhancementTemplate.rpt"))

		'	rdoc.SetDataSource(ds.Tables(0))

		'	crDiskFileDestinationOptions.DiskFileName = filePath
		'	crExportOptions = rdoc.ExportOptions

		'	With crExportOptions

		'		.ExportDestinationType = ExportDestinationType.DiskFile
		'		.ExportFormatType = ExportFormatType.PortableDocFormat
		'		.ExportDestinationOptions = crDiskFileDestinationOptions
		'		.ExportFormatOptions = crFormatypeOption

		'	End With

		'	rdoc.Export()

		'	If File.Exists(filePath) = True Then

		'		DownLoadDocument(filePath)

		'	Else
		'	End If

		'Catch ex As Exception

		'End Try













	End Sub

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		BatchProcessing()

		Exit Sub

		Dim sarrMyString As String() = Me.txtPIN.Text.ToString.Split(New String() {"PEN"}, StringSplitOptions.None)

		If sarrMyString.Length > 0 Then
			MsgBox("" & sarrMyString(0))
			Exit Sub
		Else
			Exit Sub
		End If



		Dim dt As New DataTable, cr As New Core
		dt = cr.getPMPersonInformation(Me.txtPIN.Text)



		If dt.Rows(0).Item("AgeAtRetirement") = 0 Then
			spError.InnerText = "Retirement Date is Not Available for the customer!!!"
			Me.spError.Visible = True
			Exit Sub
		Else
			ViewState("Retiree") = dt
		End If

		Me.txtMonthPencom.Text = dt.Rows(0).Item("LastPensionAmount")
		Me.txtSex.Text = dt.Rows(0).Item("sex").ToString
		Me.txtRSABalance.Text = CDbl(dt.Rows(0).Item("YearEndRFBalance"))
		Me.txtAge.Text = dt.Rows(0).Item("AgeAtRetirement")

		txtNetInterest.Text = FormatNumber(calInterate(CInt(dt.Rows(0).Item("AgeAtRetirement"))) * 100, 2)

		If Me.txtSex.Text = "M" Then
			Me.txtNxDx.Text = FormatNumber(cr.getNxDx(1, CInt(txtAge.Text)), 10)
		Else
			Me.txtNxDx.Text = FormatNumber(cr.getNxDx(0, CInt(txtAge.Text)), 10)
		End If

		Me.txtNc.Text = FormatNumber(Me.txtNxDx.Text - (11 / 24), 10)

		Me.txtRecommendedAmount.Text = FormatNumber((-1 * Financial.Pmt(calInterate(CInt(dt.Rows(0).Item("AgeAtRetirement"))) / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtRSABalance.Text, 0, 1)) - 0, 2)

		txtVariance.Text = FormatNumber((-1 * Financial.Pmt(calInterate(CInt(dt.Rows(0).Item("AgeAtRetirement"))) / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtRSABalance.Text, 0, 1)) - dt.Rows(0).Item("LastPensionAmount"), 2)


		If CDbl(Me.txtVariance.Text.Replace(",", "")) < 0 Then
			Me.txtStatus.Text = "Depletion"
		ElseIf CDbl(Me.txtVariance.Text.Replace(",", "")) > (dt.Rows(0).Item("LastPensionAmount") * 0.01) Then
			Me.txtStatus.Text = "Surplus"
		Else
			Me.txtStatus.Text = "No Change"
		End If


	End Sub

	Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click
		If Not IsNothing(ViewState("Retiree")) = True Then
			Dim dt As New DataTable
			dt = ViewState("Retiree")

			generateFiles(dt)

		Else

		End If

	End Sub
	Protected Function populateSI(dt As DataTable) As DataSet

		Dim ds As New dsBenefitEnhancement, dtSI As New DataTable, cr As New Core, i As Integer, newBERow As DataRow

		'dt = ViewState("Retiree")
		Try

			newBERow = ds.Tables(0).NewRow
			newBERow("txtBatchNo") = "00000"
			newBERow("dteSubmission") = Now.Date
			newBERow("txtPFAName") = "LEADWAY PENSURE PFA LTD"
			newBERow("txtPFACode") = "023"
			newBERow("txtSurname") = dt.Rows(0).Item("Surname")
			newBERow("txtFirstName") = dt.Rows(0).Item("FirstName")
			newBERow("txtMiddleName") = dt.Rows(0).Item("MiddleName")
			newBERow("txtPIN") = dt.Rows(0).Item("rsapin")
			newBERow("txtGender") = dt.Rows(0).Item("sex")
			newBERow("dteDOB") = dt.Rows(0).Item("dateofbirth")
			newBERow("dteDOR") = dt.Rows(0).Item("DateOfResignation")

			newBERow("txtEmployerName") = dt.Rows(0).Item("EmployerName")
			newBERow("txtEmployerCode") = dt.Rows(0).Item("EmployerCode")

			newBERow("numRSABalance") = dt.Rows(0).Item("YearEndRFBalance")
			newBERow("numMonthPension") = dt.Rows(0).Item("LastPensionAmount")
			newBERow("numCurrentRSABalance") = dt.Rows(0).Item("RFBalance")
			newBERow("numNewPension") = CDbl(Me.txtRecommendedAmount.Text.Replace(",", ""))

			newBERow("txtBPD") = "Tade Gbadebo"
			'newBERow("imgBPD") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteBPD") = Now.Date

			newBERow("txtCompliance") = "Akindele Fayombo"
			'newBERow("imgCompliance") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteCompliance") = Now.Date

			newBERow("txtMD") = "Ronke Adedeji"
			'newBERow("imgBPD") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteMD") = Now.Date

			ds.Tables(0).Rows.Add(newBERow)

			Return ds

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try
		

	End Function

	Private Sub generateFiles(dt As DataTable)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		Try

			Dim filePath As String = Server.MapPath("~/FileDownLoads/BenefitEnhancementRequest.pdf")
			rdoc.Load(Server.MapPath("~/Report/BenefitEnhancementTemplate.rpt"))

			Dim ds As DataSet

			ds = populateSI(dt)
			ViewState("BenefitEnhancementRequest") = ds.Tables(0)
			rdoc.SetDataSource(ds.Tables(0))


			crDiskFileDestinationOptions.DiskFileName = filePath
			crExportOptions = rdoc.ExportOptions

			With crExportOptions

				.ExportDestinationType = ExportDestinationType.DiskFile
				.ExportFormatType = ExportFormatType.PortableDocFormat
				.ExportDestinationOptions = crDiskFileDestinationOptions
				.ExportFormatOptions = crFormatypeOption

			End With

			rdoc.Export()

			If File.Exists(filePath) = True Then
				DownLoadDocument(filePath)
			Else
				'DownLoadDocument(path As String)
			End If

		Catch ex As Exception

			'	MsgBox("" & ex.Message)

		End Try

	End Sub

	Private Sub DownLoadDocument(path As String)

		If Not File.Exists(path) = False Then

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

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.btnFind)



	End Sub
End Class
