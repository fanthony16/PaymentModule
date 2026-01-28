Imports System.Data

Partial Class frmFundReturns
	Inherits System.Web.UI.Page

	Protected Sub gridApplicationSummary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplicationSummary.SelectedIndexChanged

	End Sub

	Public Sub getUserAccessMenu(uName As String)

		Dim cr As New Core
		Dim dtAccessModule As New DataTable
		Dim aryAccessModule As New ArrayList
		Dim i As Integer, j As Integer, k As Integer
		'		dtAccessModule = cr.getAccessModule(Session("User"))

		Dim M As New System.Web.UI.WebControls.Menu
		Dim n As New System.Web.UI.WebControls.MenuItem

		M = Master.FindControl("NavigationMenu")
		M.Items.Clear()
		'Do While i < M.Items.Count

		'	M.Items.RemoveAt(i)
		'	i = i + 1

		'Loop


	End Sub


	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


		If Not Page.IsPostBack = True Then

		
		getUserAccessMenu("")

		Dim _fundResult As New List(Of FundReturn.FundReturns)
		'Dim _fundResult_ As New FundReturn.FundReturns
		Dim k As New FundReturn.User

		Dim ar As Array
		Dim i As Integer

		ar = k.getFundReturns(Now.Date)

		'	MsgBox("" & _fundResult_.FundName)
		'Exit Sub

		Dim dtReturns = New DataTable()

		Dim dcReturnFundName = New DataColumn("FundName", GetType(String))
		Dim dcReturnValuedate = New DataColumn("Valuedate", GetType(Date))
		Dim dcReturnCurUnitPrice = New DataColumn("CurUnitPrice", GetType(Decimal))
		Dim dcReturnOpeningUnitPrice = New DataColumn("OpeningUnitPrice", GetType(Decimal))
		Dim dcReturnYearToDate = New DataColumn("YearToDate", GetType(Decimal))
		Dim dcReturnAnnualized = New DataColumn("Annualized", GetType(Decimal))

		dtReturns.Columns.Add(dcReturnFundName)
		dtReturns.Columns.Add(dcReturnValuedate)
		dtReturns.Columns.Add(dcReturnCurUnitPrice)
		dtReturns.Columns.Add(dcReturnOpeningUnitPrice)
		dtReturns.Columns.Add(dcReturnYearToDate)
			dtReturns.Columns.Add(dcReturnAnnualized)

			If Not IsNothing(ar) = True Then

				Do While i < ar.Length

					Dim _fundResult_ As New FundReturn.FundReturns
					_fundResult_ = ar(i)

					dtReturns.Rows.Add(_fundResult_.FundName, _fundResult_.Valuedate, _fundResult_.CurUnitPrice, _fundResult_.OpeningUnitPrice, _fundResult_.YearToDate.ToString("#.##").ToString(), _fundResult_.Annualized.ToString("#.##").ToString())

					i = i + 1

				Loop

			Else
			End If
		

		BindGrid(dtReturns)

		Else

		End If
		'For i = 1 To ar.Length
		'	dtReturns.Rows.Add(ar(i).)
		'Next



		'		'MsgBox("" & k.getFundReturns("", "")(1).FundName)

	End Sub

	Protected Sub BindGrid(dt As DataTable)

		Try

			Me.gridApplicationSummary.DataSource = dt
			Me.gridApplicationSummary.DataBind()

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub


	Protected Sub gridDashBoard_RowDataBound()

	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

		getUserAccessMenu("")

		Dim _fundResult As New List(Of FundReturn.FundReturns)
		'Dim _fundResult_ As New FundReturn.FundReturns
		Dim k As New FundReturn.User

		Dim ar As Array
		Dim i As Integer

		ar = k.getFundReturns(Me.txtValueDate.Text)

		'	MsgBox("" & _fundResult_.FundName)
		'Exit Sub

		Dim dtReturns = New DataTable()

		Dim dcReturnFundName = New DataColumn("FundName", GetType(String))
		Dim dcReturnValuedate = New DataColumn("Valuedate", GetType(Date))
		Dim dcReturnCurUnitPrice = New DataColumn("CurUnitPrice", GetType(Decimal))
		Dim dcReturnOpeningUnitPrice = New DataColumn("OpeningUnitPrice", GetType(Decimal))
		Dim dcReturnYearToDate = New DataColumn("YearToDate", GetType(Decimal))
		Dim dcReturnAnnualized = New DataColumn("Annualized", GetType(Decimal))

		dtReturns.Columns.Add(dcReturnFundName)
		dtReturns.Columns.Add(dcReturnValuedate)
		dtReturns.Columns.Add(dcReturnCurUnitPrice)
		dtReturns.Columns.Add(dcReturnOpeningUnitPrice)
		dtReturns.Columns.Add(dcReturnYearToDate)
		dtReturns.Columns.Add(dcReturnAnnualized)

		Do While i < ar.Length

			Dim _fundResult_ As New FundReturn.FundReturns
			_fundResult_ = ar(i)

			dtReturns.Rows.Add(_fundResult_.FundName, _fundResult_.Valuedate, _fundResult_.CurUnitPrice, _fundResult_.OpeningUnitPrice, _fundResult_.YearToDate.ToString("#.##").ToString(), _fundResult_.Annualized.ToString("#.##").ToString())

			i = i + 1

		Loop

		BindGrid(dtReturns)


	End Sub
End Class
