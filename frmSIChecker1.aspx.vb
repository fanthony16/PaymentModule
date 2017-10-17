Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Partial Class frmSIChecker1
    Inherits System.Web.UI.Page

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(gridStandingOrder)

		Try

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					ddRunType.Items.Add("")
					ddRunType.Items.Add("New")
					ddRunType.Items.Add("Renewal")
					ddAnnMonth.Items.Add("")

					ddAnnMonth.Items.Add("January")
					ddAnnMonth.Items.Add("February")
					ddAnnMonth.Items.Add("March")
					ddAnnMonth.Items.Add("April")
					ddAnnMonth.Items.Add("May")
					ddAnnMonth.Items.Add("June")
					ddAnnMonth.Items.Add("July")
					ddAnnMonth.Items.Add("August")
					ddAnnMonth.Items.Add("September")
					ddAnnMonth.Items.Add("October")
					ddAnnMonth.Items.Add("November")
					ddAnnMonth.Items.Add("December")

					Dim intYear As Integer = Now.Year - 10

					Do While intYear <= Now.Year

						If Me.ddAnnYear.Items.Count = 0 Then
							ddAnnYear.Items.Add("")
							ddAnnYear.Items.Add(intYear)
						Else
							ddAnnYear.Items.Add(intYear)
						End If
						intYear = intYear + 1
					Loop


					Dim cr As New Core

					getUserAccessMenu(Session("user"))

				End If

			Else

				getUserAccessMenu(Session("user"))

			End If

		Catch ex As Exception

		End Try

	End Sub


	Protected Function populateSI(SIApprovalID As Integer) As DataSet

		Dim ds As New dsSI, dtSI As New DataTable, cr As New Core, i As Integer, newSNRow As DataRow
		Try

			Dim MonthTypeCollection As New Hashtable
			dtSI = cr.PMViewStandingOrder(SIApprovalID)
			Dim intYear As Integer = CInt(Me.ddAnnYear.Text)
			Dim intMonth As Integer = CInt(Month(CDate(dtSI.Rows(i).Item("dteAnniversary"))))

			Do While i < dtSI.Rows.Count

				newSNRow = ds.Tables(0).NewRow

				newSNRow("txtPFA") = "LEADWAY PENSURE PFA LTD"
				newSNRow("txtPFC") = "UBA PFC"
				newSNRow("txtFullName") = dtSI.Rows(i).Item("txtFullName")
				newSNRow("txtPIN") = dtSI.Rows(i).Item("txtPIN")
				newSNRow("txtAccountNo") = dtSI.Rows(i).Item("txtBankAccount")
				newSNRow("txtBank") = dtSI.Rows(i).Item("BankName")
				newSNRow("numPension") = dtSI.Rows(i).Item("numPension")
				newSNRow("txtFrequency") = dtSI.Rows(i).Item("txtFrequency")
				newSNRow("numRFBalance") = dtSI.Rows(i).Item("RFBalance")
				newSNRow("dteValueDate") = dtSI.Rows(i).Item("dteRun")
				newSNRow("txtCurrency") = "NAIRA"
				newSNRow("txtCurrencyUnit") = "KOBO"

				newSNRow("txtMonth1") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)

				intMonth = intMonth + 1

				If intMonth <= 12 Then
					newSNRow("txtMonth2") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth2") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				If intMonth <= 12 Then
					newSNRow("txtMonth3") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth3") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If


				If intMonth <= 12 Then
					newSNRow("txtMonth4") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth4") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If


				If intMonth <= 12 Then
					newSNRow("txtMonth5") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth5") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				If intMonth <= 12 Then
					newSNRow("txtMonth6") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth6") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				If intMonth <= 12 Then
					newSNRow("txtMonth7") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth7") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				If intMonth <= 12 Then
					newSNRow("txtMonth8") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth8") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				If intMonth <= 12 Then
					newSNRow("txtMonth9") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth9") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If


				If intMonth <= 12 Then
					newSNRow("txtMonth10") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth10") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If


				If intMonth <= 12 Then
					newSNRow("txtMonth11") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth11") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				If intMonth <= 12 Then
					newSNRow("txtMonth12") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				Else
					intMonth = 1
					intYear = intYear + 1
					newSNRow("txtMonth12") = getYearMonthName(intMonth) & "'" & intYear.ToString.Substring(2, 2)
					intMonth = intMonth + 1
				End If

				'newSNRow("imgPreparedBy") = cr.PMSIgetApprovalSignature("o-taiwo")
				'


				If dtSI.Rows(i).Item("txtVettedby").ToString <> "" Then

					newSNRow("imgPreparedBy") = cr.PMSIgetApprovalSignature(dtSI.Rows(i).Item("txtVettedby").ToString)
					newSNRow("txtPreparedBy") = cr.getUserDetails(dtSI.Rows(i).Item("txtVettedby").ToString).Rows(0).Item("FullName") & "                  /  "
				Else
				End If


				If dtSI.Rows(i).Item("txtChecker1").ToString <> "" Then
					newSNRow("imgChecked1") = cr.PMSIgetApprovalSignature(dtSI.Rows(i).Item("txtChecker1").ToString)
					newSNRow("txtChecked1") = cr.getUserDetails(dtSI.Rows(i).Item("txtChecker1").ToString).Rows(0).Item("FullName") & "                  /  "
				Else

				End If

				If dtSI.Rows(i).Item("txtChecker2").ToString <> "" Then
					newSNRow("imgChecked2") = cr.PMSIgetApprovalSignature(dtSI.Rows(i).Item("txtChecker2").ToString)
					newSNRow("txtChecked2") = cr.getUserDetails(dtSI.Rows(i).Item("txtChecker2").ToString).Rows(0).Item("FullName") & "                  /  "
				Else

				End If

				If dtSI.Rows(i).Item("txtApproval1").ToString <> "" Then
					newSNRow("imgAuthorised1") = cr.PMSIgetApprovalSignature(dtSI.Rows(i).Item("txtApproval1").ToString)
					newSNRow("txtAuthorised1") = cr.getUserDetails(dtSI.Rows(i).Item("txtApproval1").ToString).Rows(0).Item("FullName") & "                  /  "
				Else

				End If

				If dtSI.Rows(i).Item("txtApproval2").ToString <> "" Then
					newSNRow("imgAuthorised2") = cr.PMSIgetApprovalSignature(dtSI.Rows(i).Item("txtApproval2").ToString)
					newSNRow("txtAuthorised2") = cr.getUserDetails(dtSI.Rows(i).Item("txtApproval2").ToString).Rows(0).Item("FullName") & "                  /  "
				Else

				End If



				i = i + 1
				ds.Tables(0).Rows.Add(newSNRow)
			Loop



		Catch ex As Exception

		End Try
		Return ds

	End Function


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

	Protected Function getYearMonthName(monthID As Integer) As String

		Dim MonthName As String = ""
		Select Case monthID

			Case Is = 1
				Return "Jan"
			Case Is = 2
				Return "Feb"
			Case Is = 3
				Return "Mar"
			Case Is = 4
				Return "Apr"
			Case Is = 5
				Return "May"
			Case Is = 6
				Return "Jun"
			Case Is = 7
				Return "Jul"
			Case Is = 8
				Return "Aug"
			Case Is = 9
				Return "Sep"
			Case Is = 10
				Return "Oct"
			Case Is = 11
				Return "Nov"
			Case Is = 12
				Return "Dec"

		End Select
		Return ""

	End Function

	Private Sub generateFiles(SIApprovalID As Integer)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		Try

			Dim filePath As String = Server.MapPath("~/FileDownLoads/SI.pdf")
			rdoc.Load(Server.MapPath("~/Report/StandingInstruction.rpt"))


			Dim ds As DataSet
			ds = populateSI(SIApprovalID)
			ViewState("ApprovalSchedule") = ds.Tables(0)
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
	Protected Sub BtnViewSI_Click(sender As Object, e As EventArgs)

		Dim btnViewSI As New ImageButton
		btnViewSI = sender
		Dim j As GridViewRow
		j = btnViewSI.NamingContainer
		generateFiles(CInt(gridStandingOrder.Rows(j.RowIndex).Cells(1).Text))

	End Sub

	Protected Sub gridSIRun_RowDataBound()

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

			j = 0
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

	Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
		generateSI()
	End Sub

	Protected Sub generateSI()

		Dim cr As New Core, intAnnMonth As Integer, dt As New DataTable

		Select Case Me.ddAnnMonth.Text

			Case Is = "January"
				intAnnMonth = 1
			Case Is = "February"
				intAnnMonth = 2
			Case Is = "March"
				intAnnMonth = 3
			Case Is = "April"
				intAnnMonth = 4
			Case Is = "May"
				intAnnMonth = 5
			Case Is = "June"
				intAnnMonth = 6
			Case Is = "July"
				intAnnMonth = 7
			Case Is = "August"
				intAnnMonth = 8
			Case Is = "September"
				intAnnMonth = 9
			Case Is = "October"
				intAnnMonth = 10
			Case Is = "November"
				intAnnMonth = 11
			Case Is = "December"
				intAnnMonth = 12
			Case Else

		End Select



		If ddRunType.Text = "Renewal" Then

			'Dim SI As New StandingPaymentOrder


			'SI.RunBy = "o-taiwo"
			'SI.MonthFor = intAnnMonth
			'SI.YearFor = CInt(ddAnnYear.Text)
			'dt = cr.PMRenewalStandingInstruction(SI)
			dt = cr.PMgetNewStandingOrder("R", "V", intAnnMonth, CInt(ddAnnYear.Text))
			ViewState("Renewal") = dt

		Else

			'cr.PMgenerateStandingInstruction("o-taiwo", intAnnMonth, CInt(ddAnnYear.Text), "N")
			dt = cr.PMgetNewStandingOrder("N", "V", intAnnMonth, CInt(ddAnnYear.Text))
			ViewState("New") = dt
		End If

		ViewState("dt") = dt
		BindGrid(dt)


	End Sub

	Protected Sub BindGrid(dt As DataTable)

		Try

			Me.gridStandingOrder.DataSource = dt
			Me.gridStandingOrder.DataBind()

			If dt.Rows.Count < 10 Then
				pnlValidatdEmail.Height = 400
			Else
				pnlValidatdEmail.Height = Nothing
			End If
			spRowCount.InnerText = dt.Rows.Count & " Row(s) Affected !"
			dvSPRowCount.Visible = True

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub btnConfirmCheck_Click(sender As Object, e As EventArgs) Handles btnConfirmCheck.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable


		Try

			If IsNothing(ViewState("TagAll")) = True Then

				For Each grow As GridViewRow In Me.gridStandingOrder.Rows
					cb = grow.FindControl("ChkPensionerChecked")
					If cb.Checked = True Then
						cr.PMUpdateStandingOrderApproval(CInt(grow.Cells(1).Text), Session("user"), "C")
					End If
				Next

			Else


				If IsNothing(ViewState("New")) = False And IsNothing(ViewState("TagAll")) = False Then

					Dim i As Integer = CInt(ViewState("TagAll"))

					If i = 1 Then

						dt = ViewState("New")

						i = 0

						Do While i < dt.Rows.Count
							cr.PMUpdateStandingOrderApproval(dt.Rows(i).Item("pkiSIApproval"), Session("user"), "C")
							i = i + 1
						Loop


					Else

					End If

				ElseIf IsNothing(ViewState("Renewal")) = False And IsNothing(ViewState("TagAll")) = False Then

					Dim i As Integer = CInt(ViewState("TagAll"))

					If i = 1 Then

						dt = ViewState("Renewal")

						i = 0

						Do While i < dt.Rows.Count
							cr.PMUpdateStandingOrderApproval(dt.Rows(i).Item("pkiSIApproval"), Session("user"), "C")
							i = i + 1
						Loop


					Else

					End If

					'Renewal
				End If

			End If

			

			




			generateSI()
		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try


	End Sub

	Protected Sub gridStandingOrder_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridStandingOrder.PageIndexChanging

		Dim dtSI As New DataTable
		Try

			gridStandingOrder.PageIndex = e.NewPageIndex
			dtSI = ViewState("dt")

			If IsNothing(dtSI) = False Then
				BindGrid(dtSI)
			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub gridStandingOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridStandingOrder.SelectedIndexChanged

	End Sub

	Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridStandingOrder.Rows

			grow.FindControl("ChkPensionerChecked")
			cb = grow.FindControl("ChkPensionerChecked")
			cb.Checked = True

		Next
		ViewState("TagAll") = 1
	End Sub

	Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridStandingOrder.Rows

			grow.FindControl("ChkPensionerChecked")
			cb = grow.FindControl("ChkPensionerChecked")
			cb.Checked = False

		Next

	End Sub
End Class
