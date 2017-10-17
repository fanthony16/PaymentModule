Imports System.IO
Imports System.Data
Partial Class FileUpload

	Inherits System.Web.UI.Page
	Dim BankTypeCollection As New Hashtable
	Dim BankBranchTypeCollection As New Hashtable
	Protected Sub UploadButton_Click(sender As Object, e As EventArgs) Handles UploadButton.Click

		If FileUploadControl.HasFiles Then

			'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
			'FileUploadControl.SaveAs(Server.MapPath("~/") + filename)
			'StatusLabel.Text = "Upload status: File uploaded!"

			'MsgBox("" & FileUploadControl.PostedFiles.Count)


			MsgBox("" & FileUploadControl.PostedFiles(0).FileName.ToString)

			For Each uu As HttpPostedFile In FileUploadControl.PostedFiles

				listofuploadedfiles.Text = listofuploadedfiles.Text + String.Format("{0}<br />", uu.FileName)
				MsgBox("" & uu.FileName)

			Next

		End If
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		If IsPostBack = True Then


		Else


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




		End If
	End Sub

	Protected Sub testEmailReader()
		Dim emr As New EmailReader.EmailReader
		Dim lstDate As New List(Of Date)
		emr.UserName = "rmassms"
		emr.Password = "bonus+3aa"
		emr.Domain = "pensure-nigeria.com"
		emr.EmailAccount = "rmassms@leadway-pensure.com"
		emr.ErrorPath = "C:"
		emr.EmailAge = 5
		emr.FilterMsg = "validated successfully"
		lstDate = emr.getValidationDate()
		MsgBox("" & lstDate.Count)
	End Sub

	Protected Sub UploadButton0_Click(sender As Object, e As EventArgs) Handles UploadButton0.Click
		testEmailReader()
	End Sub

	Protected Sub ddBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddBankName.SelectedIndexChanged
		Dim cr As New Core
		BankTypeCollection = ViewState("BankTypeCollection")
		'MsgBox("" & CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text)).ToString)


		BankTypeCollection = ViewState("BankTypeCollection")
		Dim lstBankBranches As DataTable, i As Integer = 0
		lstBankBranches = cr.PMgetBankBranches(CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text)))

		Me.ddBankBranch.Items.Clear()
		Do While i < lstBankBranches.Rows.Count

			If Me.ddBankBranch.Items.Count = 0 Then
				Me.ddBankBranch.Items.Add("")
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
				'BankBranchTypeCollection.Add(lstBankBranches.Rows(i).Item("BranchName"), lstBankBranches.Rows(i).Item("BankBranchID"))
			ElseIf Me.ddBankBranch.Items.Count > 0 Then
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
				'BankBranchTypeCollection.Add(lstBankBranches.Rows(i).Item("BranchName"), lstBankBranches.Rows(i).Item("BankBranchID"))
			End If

			i = i + 1

		Loop

		ViewState("BankBranchTypeCollection") = BankBranchTypeCollection






	End Sub

	Protected Sub ddBankBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddBankBranch.SelectedIndexChanged

		BankBranchTypeCollection = ViewState("BankBranchTypeCollection")
		MsgBox("" & CInt(BankTypeCollection.Item(Me.ddBankBranch.SelectedItem.Text)).ToString)

	End Sub

	Protected Sub UploadButton1_Click(sender As Object, e As EventArgs) Handles UploadButton1.Click
		Try
			Dim fpath As String
			fpath = Server.MapPath("~/FileUploads/o-taiwo/" & "PEN100005214599_Application_letter.pdf")
			File.Copy(fpath, "C:\NPM\PEN100005214599_Application_letter.pdf")

		Catch ex As Exception

		End Try


	End Sub
End Class
