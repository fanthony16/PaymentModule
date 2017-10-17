Imports System.Data
Imports System.IO

Partial Class frmStandingPensioner
	Inherits System.Web.UI.Page
	Dim BankTypeCollection As New Hashtable

	Protected Sub gridPensioner_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("dt")) = False Then

			Dim dt As DataTable = ViewState("dt")
			If e.Row.RowType = DataControlRowType.DataRow Then

				If dt.Rows(e.Row.RowIndex).Item("txtDStatus").ToString = "F" Then

					e.Row.ForeColor = System.Drawing.Color.Orange
					'e.Row.Enabled = False

				ElseIf dt.Rows(e.Row.RowIndex).Item("txtRStatus").ToString = "F" Then

					e.Row.ForeColor = System.Drawing.Color.Orange
					'e.Row.Enabled = False
				ElseIf dt.Rows(e.Row.RowIndex).Item("txtStatus").ToString = "F" Then

					e.Row.ForeColor = System.Drawing.Color.Orange

				ElseIf dt.Rows(e.Row.RowIndex).Item("txtDStatus").ToString = "A" And dt.Rows(e.Row.RowIndex).Item("txtRStatus").ToString <> "A" Then

					e.Row.ForeColor = System.Drawing.Color.Red
					'e.Row.Enabled = False

					Dim btnEdit As ImageButton = TryCast(e.Row.FindControl("btnViewPensionerDetail"), ImageButton)
					btnEdit.Enabled = False

					Dim btnCancel As ImageButton = TryCast(e.Row.FindControl("btnCancelApplication"), ImageButton)
					btnCancel.Enabled = False

					Dim btnReactivate As ImageButton = TryCast(e.Row.FindControl("btnActivateApplication"), ImageButton)
					btnReactivate.Enabled = True

				ElseIf dt.Rows(e.Row.RowIndex).Item("txtDStatus").ToString = "A" And dt.Rows(e.Row.RowIndex).Item("txtRStatus").ToString = "A" Then


				End If

			End If
		Else
		End If

	End Sub



	Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewApplicationComments As New ImageButton
		btnViewApplicationComments = sender
		Dim i As GridViewRow, cr As New Core, dt As New DataTable, j As Integer


		i = btnViewApplicationComments.NamingContainer

		'Me.txtApplicationID.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(2).Text
		'dt = cr.PMgetApplicationComment(Me.gridPensioner.Rows(i.RowIndex).Cells(2).Text, "PRE")
		Try

			txtPensionerID.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(1).Text
			Me.txtPensionAmount.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(5).Text
			Me.ddPaymentFrequency.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(6).Text
			Me.txtAccountNumber.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(10).Text
			Try
				Me.ddPensionerBanks.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(8).Text
			Catch ex As Exception

			End Try

			Try
				populateBankBranches(Me.gridPensioner.Rows(i.RowIndex).Cells(8).Text)
			Catch ex As Exception

			End Try

			Me.ddBankBranches.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(9).Text

			MPPensioner.Show()

		Catch ex As Exception



		End Try
		

	End Sub
	Protected Sub AddViewComment_Click(sender As Object, e As EventArgs)

		Dim btnViewApplicationComments As New ImageButton
		btnViewApplicationComments = sender
		Dim i As GridViewRow, cr As New Core, dt As New DataTable, j As Integer

		i = btnViewApplicationComments.NamingContainer

		txtDPensionerID.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(1).Text

		Me.mpDeativationReason.Show()

	End Sub

	Protected Sub BtnActivateApplication_Click(sender As Object, e As EventArgs)

		Dim btnCancelApplication As New ImageButton
		btnCancelApplication = sender
		Dim i As GridViewRow, cr As New Core, dt As New DataTable
		i = btnCancelApplication.NamingContainer
		cr.PMUpdateNewPensionserList("F", CInt(Me.gridPensioner.Rows(i.RowIndex).Cells(1).Text), Session("user"), "R")


		PopulatePensioner()
		'txtDPensionerID.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(1).Text
		'Me.mpDeativationReason.Show()

	End Sub

	Protected Sub BtnCancelApplication_Click(sender As Object, e As EventArgs)

		Dim btnCancelApplication As New ImageButton
		btnCancelApplication = sender
		Dim i As GridViewRow, cr As New Core, dt As New DataTable

		i = btnCancelApplication.NamingContainer

		txtDPensionerID.Text = Me.gridPensioner.Rows(i.RowIndex).Cells(1).Text

		Me.mpDeativationReason.Show()


	End Sub
	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	'Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

	'	Dim cr As New Core, dt As New DataTable
	'	dt = cr.PMgetNewPensionsers(Me.txtStartDate.Text, Me.txtEndDate.Text)
	'	If IsNothing(dt) = False Then
	'		ViewState("dt") = dt
	'		BindGrid(dt)
	'		btnSIForApproval.Enabled = True
	'	Else
	'		dt = cr.PMgetPendingNewPensionsers("P", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))
	'		ViewState("dt") = dt
	'		BindGrid(dt)
	'		btnSIForApproval.Enabled = True
	'	End If

	'End Sub

	Protected Sub populateBank()

		Dim myState As New States, i As Integer = 0, cr As New Core
		Dim lstBank As New DataTable
		lstBank = cr.PMgetBanks
		Me.ddPensionerBanks.Items.Clear()

		Do While i < lstBank.Rows.Count

			If Me.ddPensionerBanks.Items.Count = 0 Then
				Me.ddPensionerBanks.Items.Add("")
				Me.ddPensionerBanks.Items.Add(lstBank.Rows(i).Item("bankname"))
				BankTypeCollection.Add(lstBank.Rows(i).Item("bankname"), lstBank.Rows(i).Item("BankID"))

			ElseIf Me.ddPensionerBanks.Items.Count > 0 Then
				Me.ddPensionerBanks.Items.Add(lstBank.Rows(i).Item("bankname"))
				BankTypeCollection.Add(lstBank.Rows(i).Item("bankname"), lstBank.Rows(i).Item("BankID"))

			End If
			i = i + 1

		Loop
		ViewState("BankTypeCollection") = BankTypeCollection

	End Sub

	Protected Sub BindGrid(dt As DataTable)

		Try

			Me.gridPensioner.DataSource = dt
			Me.gridPensioner.DataBind()

			If dt.Rows.Count < 10 Then
				pnlValidatdEmail.Height = 500
			Else
				pnlValidatdEmail.Height = Nothing
			End If

			spRowCount.InnerText = dt.Rows.Count & " Row(s) Affected !"
			dvSPRowCount.Visible = True

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

	Protected Sub populateFrequency()
		Try
			Me.ddPaymentFrequency.Items.Add("")
			Me.ddPaymentFrequency.Items.Add("Monthly")
			Me.ddPaymentFrequency.Items.Add("Half Yearly")
			Me.ddPaymentFrequency.Items.Add("Quarterly")
			ddDeactivationReason.Items.Add("")
			ddDeactivationReason.Items.Add("ReCall")
			ddDeactivationReason.Items.Add("Death")

		Catch ex As Exception

		End Try
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		'Dim scriptManagerA, scriptManagerB, scriptManagerC As New ScriptManager, dtusers As New DataTable
		'scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		'scriptManagerA.RegisterPostBackControl(imgExportExcel)

		Try

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					Response.Redirect("Login.aspx")

				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

					Dim cr As New Core
					'dtusers = Session("userDetails")
					getUserAccessMenu(Session("user"))
					populateBank()
					populateFrequency()

				End If

			Else

				getUserAccessMenu(Session("user"))

			End If

		Catch ex As Exception

		End Try

	End Sub

	Protected Sub OnInit(obj As Object)
		Try

		Catch ex As Exception

		End Try
	End Sub


	Public Sub populateBankBranches(bankName As String)

		Try

			BankTypeCollection = ViewState("BankTypeCollection")
			Dim lstBankBranches As New DataTable, i As Integer = 0, cr As New Core
			lstBankBranches = cr.PMgetBankBranches(CInt(BankTypeCollection.Item(bankName)))
			Me.ddBankBranches.Items.Clear()
			Do While i < lstBankBranches.Rows.Count

				If Me.ddBankBranches.Items.Count = 0 Then
					Me.ddBankBranches.Items.Add("")
					'Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & " | " & lstBankBranches.Rows(i).Item("BankBranchID"))
					'Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName"))

				ElseIf Me.ddBankBranches.Items.Count > 0 Then

					Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName"))

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

	Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click
		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridPensioner.Rows

			grow.FindControl("ChkPensionerChecked")
			cb = grow.FindControl("ChkPensionerChecked")
			cb.Checked = True
			ViewState("TagAll") = 1

		Next
	End Sub



	Protected Sub btnSIForApproval_Click(sender As Object, e As EventArgs) Handles btnSIForApproval.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

		Try

			For Each grow As GridViewRow In Me.gridPensioner.Rows
				cb = grow.FindControl("ChkPensionerChecked")

				If cb.Checked = True Then
					cr.PMUpdateNewPensionserList("F", grow.Cells(1).Text, Session("user"), "N")

				End If

			Next
			BindGrid(cr.PMgetPendingNewPensionsers("P", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text)))
		Catch ex As Exception

		End Try

	End Sub
	Protected Sub PopulatePensioner()
		Dim cr As New Core, dt As New DataTable
		Try
			dt = cr.PMgetPendingNewPensionsers("A", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))
			ViewState("dt") = dt
			BindGrid(dt)
			btnSIForApproval.Enabled = False
		Catch ex As Exception

		End Try
	End Sub
	Protected Sub btnViewRecord_Click(sender As Object, e As EventArgs) Handles btnViewRecord.Click
		PopulatePensioner()
	End Sub

	Protected Sub gridPensioner_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridPensioner.PageIndexChanging

		If IsNothing(ViewState("dt")) = False Then

			Dim dt As New DataTable
			Me.gridPensioner.PageIndex = e.NewPageIndex
			dt = ViewState("dt")
			BindGrid(dt)

		Else
		End If

	End Sub

	Protected Sub gridPensioner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridPensioner.SelectedIndexChanged

	End Sub

	Protected Sub imgExportExcel_Click(sender As Object, e As ImageClickEventArgs) Handles imgExportExcel.Click

		Dim dt As New DataTable, cr As New Core


		If IsNothing(ViewState("dt")) = False Then

			dt = ViewState("dt")

			If dt.Rows.Count > 0 Then

				cr.ExtractCSV(dt, "NewPensioners")
			Else

				cr.ExtractCSV(dt, "NewPensioners")

			End If

		Else

		End If

	End Sub

	Protected Sub ddPensionerBanks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddPensionerBanks.SelectedIndexChanged

		Try

			BankTypeCollection = ViewState("BankTypeCollection")
			Dim lstBankBranches As New DataTable, i As Integer = 0, cr As New Core
			lstBankBranches = cr.PMgetBankBranches(CInt(BankTypeCollection.Item(Me.ddPensionerBanks.SelectedItem.Text)))

			Me.ddBankBranches.Items.Clear()
			Do While i < lstBankBranches.Rows.Count

				If Me.ddBankBranches.Items.Count = 0 Then
					Me.ddBankBranches.Items.Add("")
					'	Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
					Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName"))
				ElseIf Me.ddBankBranches.Items.Count > 0 Then
					Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName"))
				End If

				i = i + 1

			Loop
			Me.MPPensioner.Show()
		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = "c:"
			logerr.Logger(ex.Message)
		End Try

	End Sub

	Protected Sub btnPensionerOk_Click(sender As Object, e As EventArgs) Handles btnPensionerOk.Click

		Dim cr As New Core, bankID As Integer, branchID As Integer, frequency As Integer

		If Not IsNothing(ViewState("BankTypeCollection")) = True Then

			BankTypeCollection = ViewState("BankTypeCollection")
			bankID = CInt(BankTypeCollection.Item(Me.ddPensionerBanks.SelectedItem.Text.ToString))
			branchID = cr.PMgetBankBranches(bankID, Me.ddBankBranches.Text.ToString).Rows(0).Item("BankBranchID").ToString()

			If ddPaymentFrequency.Text = "Monthly" Then
				frequency = 1
			ElseIf ddPaymentFrequency.Text = "Half Yearly" Then
				frequency = 2
			ElseIf ddPaymentFrequency.Text = "Quarterly" Then
				frequency = 3
			End If

			cr.PMUpdatePendingNewPensionsers(Me.txtPensionAmount.Text, bankID, branchID, Me.txtAccountNumber.Text, frequency, Me.txtPensionerID.Text, Session("user"))
			PopulatePensioner()
		Else

		End If




	End Sub

	Protected Sub ddPensionerBanks_TextChanged(sender As Object, e As EventArgs) Handles ddPensionerBanks.TextChanged

		Try

			BankTypeCollection = ViewState("BankTypeCollection")
			Dim lstBankBranches As New DataTable, i As Integer = 0, cr As New Core
			lstBankBranches = cr.PMgetBankBranches(CInt(BankTypeCollection.Item(Me.ddPensionerBanks.SelectedItem.Text)))

			Me.ddBankBranches.Items.Clear()
			Do While i < lstBankBranches.Rows.Count

				If Me.ddBankBranches.Items.Count = 0 Then
					Me.ddBankBranches.Items.Add("")
					'Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))

					Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName"))

				ElseIf Me.ddBankBranches.Items.Count > 0 Then

					Me.ddBankBranches.Items.Add(lstBankBranches.Rows(i).Item("BranchName"))

				End If

				i = i + 1

			Loop

			Me.MPPensioner.Show()

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = "c:"
			logerr.Logger(ex.Message)
		End Try

	End Sub

	Protected Sub btnDeactivationOk_Click(sender As Object, e As EventArgs) Handles btnDeactivationOk.Click

		Dim cr As New Core

		If Me.ddDeactivationReason.Text = "Death" Then

			cr.PMDeactivatePensionsers(CInt(Me.txtDPensionerID.Text), Session("user"), 2, Me.txtDeactivationComment.Text)
		ElseIf Me.ddDeactivationReason.Text = "ReCall" Then
			cr.PMDeactivatePensionsers(CInt(Me.txtDPensionerID.Text), Session("user"), 1, Me.txtDeactivationComment.Text)

		End If
		Me.txtDeactivationComment.Text = ""
		Me.ddDeactivationReason.Text = ""
		PopulatePensioner()

	End Sub

	Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click
		Dim cb As CheckBox
		For Each grow As GridViewRow In Me.gridPensioner.Rows

			grow.FindControl("ChkPensionerChecked")
			cb = grow.FindControl("ChkPensionerChecked")
			cb.Checked = False

		Next
	End Sub

	Protected Sub UploadButton_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

		Try

			If Me.chkUpload.Checked = True Then

				If FileUploadControl.HasFile = True Then

					'Dim cr As New Core, dt As New DataTable
					'Dim cReader As New CSVReader.CSVReader
					'Dim strFileName As String = DateTime.Now.ToString("ddMMyyyy_HHmmss")
					'Dim strFileType As String = System.IO.Path.GetExtension(FileUploadControl.FileName).ToString().ToLower()
					'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
					'FileUploadControl.SaveAs(Server.MapPath("~/FileUploads/") + strFileName + strFileType)
					'cReader.FilePath = Server.MapPath("~/FileUploads/") + strFileName + strFileType
					'cReader.FileType = "Excel"
					'cReader.DBase = "SurePay"
					'cReader.SServer = "enpowertest"
					'cReader.UName = ""
					'cReader.PWord = ""
					'spUploadFeedback.InnerText = cReader.getData(Server.MapPath("~/Logs/"))

					'dt = cr.PMgetPendingNewPensionsers("P", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))

					'ViewState("dt") = dt
					'BindGrid(dt)
					'btnSIForApproval.Enabled = True


				End If

			Else

				Dim cr As New Core, dt As New DataTable
				dt = cr.PMgetNewPensionsers(Me.txtStartDate.Text, Me.txtEndDate.Text)
				If IsNothing(dt) = False Then

					ViewState("dt") = dt
					BindGrid(dt)
					btnSIForApproval.Enabled = True

				Else

					dt = cr.PMgetPendingNewPensionsers("P", CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text))
					ViewState("dt") = dt
					BindGrid(dt)
					btnSIForApproval.Enabled = True

				End If

			End If

		Catch ex As Exception



		End Try
		




	End Sub

	Private Sub loadNewEntrants2(strfilename As String)

		Dim num_rows As Long
		Dim num_cols As Long
		Dim strarray(1, 1) As String
		Dim retiree As New Pensioner
		Dim retirees As New List(Of Pensioner)
		Dim cr As New Core
		Try


			If File.Exists(strfilename) Then


				Dim tmpstream As StreamReader = File.OpenText(strfilename)
				Dim strlines() As String
				Dim strline() As String
				'Load content of file to strLines array



				strlines = tmpstream.ReadToEnd().Split(Environment.NewLine)
				'Do While tmpstream.Peek() >= 0
				'    MsgBox("Testing :" & tmpstream.ReadLine())
				'Loop


				' Redimension the array.
				num_rows = UBound(strlines)
				strline = strlines(0).Split(",")
				'strline = strlines(0).Split(vbTab)
				num_cols = UBound(strline)


				ReDim strarray(num_rows, num_cols)
				' Copy the data into the array.

				For x = 0 To num_rows

					retiree = New Pensioner

					strline = strlines(x).Split(",")
					'Dim Description As String, LocalAmount As Double, ValueDate As Date, TransDate As Date, Processed As Double, outstanding As Double
					'Description = LTrim(RTrim(strline(0).ToString))
					'LocalAmount = CDbl(LTrim(RTrim(strline(1)).ToString))
					'ValueDate = CDate(LTrim(RTrim(strline(2)).ToString))
					'TransDate = CDate(LTrim(RTrim(strline(3)).ToString))
					'Processed = CDbl(LTrim(RTrim(strline(4)).ToString))
					'outstanding = CDbl(LTrim(RTrim(strline(5)).ToString))


					retiree.PIN = LTrim(RTrim(strline(0).ToString))
					retiree.Fullname = LTrim(RTrim(strline(1)).ToString)
					retiree.PWAmount = CDbl(LTrim(RTrim(strline(2)).ToString))
					retiree.PensionAmount = CDbl(LTrim(RTrim(strline(3)).ToString))
					retiree.AccountNo = LTrim(RTrim(strline(4)).ToString)
					retiree.Bank = LTrim(RTrim(strline(5)).ToString)
					retiree.Branch = LTrim(RTrim(strline(6)).ToString)
					retiree.Frequency = LTrim(RTrim(strline(7)).ToString)
					retiree.AnniversaryDate = CDate(LTrim(RTrim(strline(8)).ToString))
					retirees.Add(retiree)


				Next
				tmpstream.Close()
				tmpstream = Nothing
				cr.PMgetNewPensionsers(retirees)


			End If
		Catch ex As Exception
		Finally

			Dispose()

		End Try
	End Sub


	Private Sub loadNewEntrants3(dt As DataTable)

		Dim i As Integer
		Dim retiree As New Pensioner
		Dim retirees As New List(Of Pensioner)
		Dim cr As New Core
		Try


			If IsNothing(dt) = False Then

				Do While i < dt.Rows.Count

					retiree.PIN = dt.Rows(i).Item("PIN")
					retiree.Fullname = dt.Rows(i).Item("Fullname")
					retiree.PWAmount = dt.Rows(i).Item("PWAmount")
					retiree.PensionAmount = CDbl(dt.Rows(i).Item("PensionAmount"))
					retiree.AccountNo = dt.Rows(i).Item("AccountNo")
					retiree.Bank = dt.Rows(i).Item("Bank")
					retiree.Branch = dt.Rows(i).Item("Branch")
					retiree.Frequency = dt.Rows(i).Item("Frequency")
					retiree.AnniversaryDate = CDate(dt.Rows(i).Item("AnniversaryDate"))
					retirees.Add(retiree)

					i = i + 1
				Loop

				cr.PMgetNewPensionsers(retirees)

			Else

			End If
		Catch ex As Exception
		Finally

			Dispose()

		End Try
	End Sub


	Private Sub loadNewEntrants(filePath As String, fileName As String)

		'C:\GeneralProject\Payment Module\FileUploads
		Dim strFilePath As String = Server.MapPath("~/FileUploads")
		If filePath = "" Then
			MsgBox("Please select a CSV file to upload")
			Exit Sub
		End If
		'If MsgBox("Are You Sure of The Upload Now !!!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
		Dim sConnectionString As String


		'strFilePath = "C:\Federal Ministry"
		'sConnectionString = ("Provider=Microsoft.Jet.OLEDB.4.0;" & _
		'		    "Data Source=" & strFilePath & ";" & _
		'		    "Extended Properties=""text;HDR=NO;FMT=Delimited""")
		'sConnectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;" & _
		'			    "Data Source=" & strFilePath & ";" & _
		'			    "Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2""")
		'Provider=Microsoft.Jet.OLEDB.4.0 to "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + "; Extended Properties='Excel 8.0;HDR=No;IMEX=1'
		'sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"


		sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFilePath & ";" & "Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""


		Dim cmd As New OleDb.OleDbCommand
		Dim query As String = "SELECT * FROM [Sheet1$]"

		'query = "SELECT [Country],[Capital] FROM [Sheet1$] WHERE [Currency]='Rupee'"
		'query = "SELECT [Country],[Capital] FROM [Sheet1$]"

		Dim conn As New OleDb.OleDbConnection
		'Create the connection object
		conn = New OleDb.OleDbConnection(sConnectionString)
		'Open connection

		If conn.State = ConnectionState.Closed = True Then
			conn.Open()
		Else

		End If

		'Create the command object
			cmd = New OleDb.OleDbCommand(query, conn)

		Me.chkUpload.Text = "Connected..."
		Exit Sub


		Dim objConn As New System.Data.OleDb.OleDbConnection(sConnectionString)
			objConn.Open()

			Me.chkUpload.Text = "Connected..."
			Exit Sub
			'Dim t As String = Me.txtfileName.Text '  Try

			Dim t As String = "Try.csv"
			Dim u As String = fileName

			Dim s As String = "select * from " + u
			Dim da As New System.Data.OleDb.OleDbDataAdapter(s, objConn)
			Dim ds As New DataSet(t)
			Dim rowCount As Integer
			da.Fill(ds, u)
			Dim dt As DataTable

			'Dim fullname As String, pin As String, pwAmount As Double, pensionAmount As Double, txtAccountNo As String, txtBank As String, txtBranch As String, intfrequency As Integer, dteAnniversary As Date

			Dim q, r, n, m As Integer, retirees As New List(Of Pensioner), retiree As Pensioner, cr As New Core
			Dim dr() As DataRow
			dt = ds.Tables(u)
			q = dt.Columns.Count
			r = dt.Rows.Count
			n = 0
			m = 0
			dr = dt.Select()
			rowCount = dt.Rows.Count

			Do While n < dt.Rows.Count
				Do While m < dt.Columns.Count
					retiree = New Pensioner
					retiree.PIN = dr(n).Item(0 - m)
					retiree.Fullname = dr(n).Item(1 - m)
					retiree.PWAmount = CDbl(dr(n).Item(2 - m))
					retiree.PensionAmount = CDbl(dr(n).Item(3 - m))
					retiree.AccountNo = dr(n).Item(4 - m)
					retiree.Bank = dr(n).Item(5 - m)
					retiree.Branch = dr(n).Item(6 - m)
					retiree.Frequency = dr(n).Item(7 - m)
					retiree.AnniversaryDate = CDate(dr(n).Item(8 - m))
					retirees.Add(retiree)
					'fullname = dr(n).Item(2 - m)
					'pin = dr(n).Item(1 - m)
					'pwAmount = dr(n).Item(3 - m)
					'pensionAmount = dr(n).Item(4 - m)
					'txtAccountNo = dr(n).Item(5 - m)
					'txtBank = dr(n).Item(6 - m)
					'txtBranch = dr(n).Item(7 - m)
					'intfrequency = dr(n).Item(8 - m)
					'dteAnniversary = CDate(dr(n).Item(9 - m))

					'Dim myCon As New SqlClient.SqlConnection
					'Dim myComm As New SqlClient.SqlCommand
					'Dim de As New SqlClient.SqlDataAdapter
					'myCon.ConnectionString = cont_string
					'myCon.Open()
					'myComm.CommandText = "insert into tblSIPensioneer (txtPIN,txtFullName,numPWAmount,numPension,txtBankAccount,txtBankName,txtBankBranch,txtFrequency,dteAnniversary,txtStatus)  select '" & pin & "','" & fullname & "','" & pwAmount & "','" & pensionAmount & "','" & txtAccountNo & "','" & txtBank & "','" & txtBranch & "','" & intfrequency & "','" & DateTime.Parse(dteAnniversary) & "','P' where not exists (select rsapin from employee where rsapin = '" & pin & "' ); "
					'myComm.CommandType = CommandType.Text
					'myComm.Connection = myCon
					'myComm.ExecuteNonQuery()
					'myCon.Close()
					m = m + q
				Loop
				m = 0
				n = n + 1
			Loop

			cr.PMgetNewPensionsers(retirees)



	End Sub


End Class
