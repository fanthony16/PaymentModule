Imports System.Data

Partial Class frmPencomTemplate2
	Inherits System.Web.UI.Page

	Protected Sub BatchProcessing()
		Dim dt As New DataTable, cr As New Core, i As Integer, txtMonthPencomm As Double, txtSexx As String, txtRSABalancee As Double, txtAgee As Double, txtNetInterestt As String, txtNxDxx As Double, txtNcc As Decimal, txtRecommendedAmountt As Double, txtVariancee As String, txtFreqq As String, ds As New dsBenefitEnhancement, txtYearNumber As String, txtSurplus As Double, txtDOB As Date, txtRetirementDate As String, txtRSABalance As Double, txtNetInterest As Double, txtReservee As Double, txtMaxEnhancedd As Double

		Dim mycon As New SqlClient.SqlConnection, db As New DbConnection, dtEnhancement As DataTable
		mycon = db.getConnection("PaymentModule")

		txtFreqq = 12

		dtEnhancement = cr.PMgetRetireeForEnhencement()

		Dim txtReviewDate As Date, txtFreq As Double = 12
		txtReviewDate = CDate("2016-12-31")

		'MsgBox("" & dtEnhancement.Rows.Count)


		Do While i < dtEnhancement.Rows.Count


			dt = cr.getPMPersonInformation(dtEnhancement.Rows(i).Item("PIN"))


			If dt.Rows(0).Item("AgeAtRetirement") = 0 Then
				'spError.InnerText = "Retirement Date is Not Available for the customer!!!"
				'Me.spError.Visible = True
				'Exit Sub
			Else
				ViewState("Retiree") = dt
			End If

			txtDOB = dt.Rows(0).Item("dateofbirth")

			txtRetirementDate = dt.Rows(0).Item("DateRetired").ToString

			Dim dd As Date, age As Integer

			dd = DateAdd(DateInterval.Year, (DateDiff(DateInterval.Year, CDate(dt.Rows(0).Item("dateofbirth")), CDate(txtReviewDate))), CDate(dt.Rows(0).Item("dateofbirth")))


			'Me.txtAge.Text = (DateDiff(DateInterval.Year, CDate(dt.Rows(0).Item("dateofbirth")), CDate(txtReviewDate)))
			age = (DateDiff(DateInterval.Year, CDate(dt.Rows(0).Item("dateofbirth")), CDate(txtReviewDate)))


			txtMonthPencomm = dt.Rows(0).Item("LastPensionAmount")
			txtSexx = dt.Rows(0).Item("sex").ToString
			'''''''''''''
			'IFRS Version
			'''''''''

			'MsgBox("" & cr.getPMIFRSBalance(dtEnhancement.Rows(i).Item("EmployeeID")))

			'MsgBox("" & CDbl(cr.getPMIFRSBalance(dtEnhancement.Rows(i).Item("EmployeeID")) * 2.2104))

			'Exit Sub
			txtRSABalance = FormatNumber(CDbl(cr.getPMIFRSBalance(dtEnhancement.Rows(i).Item("EmployeeID")) * 2.2104), 2)

			'txtRSABalance = FormatNumber(CDbl(dt.Rows(0).Item("YearEndRFBalance")) * 2.2104, 2)

			txtNetInterest = FormatNumber(calInterate() * 100, 2)

			If txtSexx = "M" Then
				txtNxDxx = FormatNumber(cr.getNxDx(1, CInt(age)), 8)
			Else
				txtNxDxx = FormatNumber(cr.getNxDx(0, CInt(age)), 8)
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


			Dim ddate As Date = CDate("2016-12-31")

			Try
				'NGAP
				'Dim MyDataAdapter As SqlClient.SqlDataAdapter
				'MyDataAdapter = New SqlClient.SqlDataAdapter("INSERT INTO [dbo].[tblPensionEnhancementIFRS]([txtSurname],[txtFirstName],[txtMiddleName],[txtPIN],[txtGender],[dteDOB] ,[dteDOR],[txtEmployerName],[txtEmployerCode],[numRSABalance],[numMonthPension],[intAge],[Nc],[NxDxNc],[numEnhancement],[numMaxEnhancement],[numReserve],[numSurplus],dterunfor)	VALUES('" & dt.Rows(0).Item("Surname") & "','" & dt.Rows(0).Item("FirstName") & "','" & dt.Rows(0).Item("MiddleName") & "','" & dt.Rows(0).Item("rsapin") & "','" & dt.Rows(0).Item("sex") & "','" & dt.Rows(0).Item("dateofbirth").ToString & "','" & dt.Rows(0).Item("DateOfResignation").ToString & "','" & dt.Rows(0).Item("EmployerName") & "','" & dt.Rows(0).Item("EmployerCode") & "','" & dt.Rows(0).Item("YearEndRFBalance") & "','" & dt.Rows(0).Item("LastPensionAmount") & "','" & age & "','" & txtNcc & "','" & txtNxDxx & "','" & txtRecommendedAmountt & "','" & txtMaxEnhancedd & "','" & txtReservee & "','" & txtSurplus & "','" & DateTime.Parse(ddate).ToString("yyyy-MM-dd HH:MM") & "')", mycon)
				'MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				'MyDataAdapter.SelectCommand.ExecuteNonQuery()

				'IFRS
				Dim MyDataAdapter As New SqlClient.SqlDataAdapter
				MyDataAdapter = New SqlClient.SqlDataAdapter("INSERT INTO [dbo].[tblPensionEnhancementIFRS]([txtSurname],[txtFirstName],[txtMiddleName],[txtPIN],[txtGender],[dteDOB] ,[dteDOR],[txtEmployerName],[txtEmployerCode],[numRSABalance],[numMonthPension],[intAge],[Nc],[NxDxNc],[numEnhancement],[numMaxEnhancement],[numReserve],[numSurplus],dterunfor)	VALUES('" & dt.Rows(0).Item("Surname") & "','" & dt.Rows(0).Item("FirstName") & "','" & dt.Rows(0).Item("MiddleName") & "','" & dt.Rows(0).Item("rsapin") & "','" & dt.Rows(0).Item("sex") & "','" & dt.Rows(0).Item("dateofbirth").ToString & "','" & dt.Rows(0).Item("DateOfResignation").ToString & "','" & dt.Rows(0).Item("EmployerName") & "','" & dt.Rows(0).Item("EmployerCode") & "','" & txtRSABalance & "','" & dt.Rows(0).Item("LastPensionAmount") & "','" & age & "','" & txtNcc & "','" & txtNxDxx & "','" & txtRecommendedAmountt & "','" & txtMaxEnhancedd & "','" & txtReservee & "','" & txtSurplus & "','" & DateTime.Parse(ddate).ToString("yyyy-MM-dd HH:MM") & "')", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.ExecuteNonQuery()




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

		Dim dt As New DataTable, cr As New Core
		dt = cr.getPMPersonInformation(Me.txtPIN.Text)


		If dt.Rows(0).Item("AgeAtRetirement") = 0 Then
			spError.InnerText = "Retirement Date is Not Available for the customer!!!"
			Me.spError.Visible = True
			Exit Sub
		Else
			ViewState("Retiree") = dt
		End If

		Me.txtDOB.Text = dt.Rows(0).Item("dateofbirth")

		Me.txtRetirementDate.Text = dt.Rows(0).Item("DateRetired").ToString

		Dim dd As Date, age As Integer

		dd = DateAdd(DateInterval.Year, (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtReviewDate.Text))), CDate(Me.txtDOB.Text))


		Me.txtAge.Text = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtReviewDate.Text)))
		age = (DateDiff(DateInterval.Year, CDate(Me.txtDOB.Text), CDate(Me.txtReviewDate.Text)))
		

		Me.txtMonthPencom.Text = dt.Rows(0).Item("LastPensionAmount")
		Me.txtSex.Text = dt.Rows(0).Item("sex").ToString
		Me.txtRSABalance.Text = FormatNumber(CDbl(dt.Rows(0).Item("YearEndRFBalance")), 2)

		txtNetInterest.Text = FormatNumber(calInterate() * 100, 2)


		If Me.txtSex.Text = "M" Then
			Me.txtNxDx.Text = FormatNumber(cr.getNxDx(1, CInt(age)), 8)
		Else
			Me.txtNxDx.Text = FormatNumber(cr.getNxDx(0, CInt(age)), 8)
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
End Class
