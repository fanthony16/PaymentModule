Imports System.Data

Partial Class frmPencomTemplate2
	Inherits System.Web.UI.Page

	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim sarrMyString As String() = Me.txtPIN.Text.ToString.Split(New String() {"PEN"}, StringSplitOptions.None)




		'If sarrMyString.Length > 0 Then
		'	MsgBox("" & sarrMyString(0))
		'	Exit Sub
		'Else
		'	Exit Sub
		'End If

		'Dim str As New List(Of Integer)
		'str.Add(100)
		'str.Add(101)
		'MsgBox("" & str.Min)

		'Exit Sub

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
