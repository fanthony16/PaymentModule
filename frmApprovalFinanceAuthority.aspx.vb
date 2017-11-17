Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports AjaxControlToolkit
Imports System.IO

Partial Class frmApprovalFinanceAuthority
     Inherits System.Web.UI.Page
	Dim ApprovalTypeCollection As New Hashtable
	Dim DocumentCollection As New Hashtable
	Dim BankTypeCollection As New Hashtable
	Dim BankBranchCollection As New Hashtable


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


     Protected Sub gridExport_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
          Dim dtt As New DataTable
          If IsNothing(ViewState("BatchApprovals")) = False Then
               Dim dt As DataTable = ViewState("BatchApprovals")

               If e.Row.RowType = DataControlRowType.DataRow Then

                    'If ((dt.Rows(e.Row.RowIndex).Item("txtControlCheckedBy"))).ToString.Trim <> "" Then
                    If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteChecked")) = False Then

                         Dim cbChecked As CheckBox = TryCast(e.Row.FindControl("ChkPINApprovalChecked"), CheckBox)
                         cbChecked.Checked = True
                         cbChecked.Enabled = False
                    End If
                    'e.Row.ForeColor = System.Drawing.Color.Blue

                    ' If ((dt.Rows(e.Row.RowIndex).Item("txtControlVerifiedBy"))).ToString.Trim <> "" Then
                    If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteVerified")) = False Then

                         'e.Row.ForeColor = System.Drawing.Color.Green
                         Dim cbVerified As CheckBox = TryCast(e.Row.FindControl("ChkPINApprovalVerified"), CheckBox)
                         cbVerified.Checked = True
                         cbVerified.Enabled = False
                    End If

                    'If ((dt.Rows(e.Row.RowIndex).Item("txtControlAuthorisedBy"))).ToString.Trim <> "" Then
                    If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteAuthorised")) = False Then

                         Dim cbAuthorised As CheckBox = TryCast(e.Row.FindControl("ChkPINApprovalAuthorised"), CheckBox)
                         cbAuthorised.Checked = True
                         cbAuthorised.Enabled = False

				End If

				If CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Rejected" Then
					e.Row.ForeColor = System.Drawing.Color.Red

					'e.Row.ForeColor = Drawing.Color.Green
				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Confirmed" Then

					e.Row.ForeColor = Drawing.Color.Green

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Tentative" Then

					e.Row.ForeColor = Drawing.Color.BlueViolet

				ElseIf CBool(dt.Rows(e.Row.RowIndex).Item("IsAppControlChecked")) = True And dt.Rows(e.Row.RowIndex).Item("txtControlCheckedStatus").ToString = "Open" Then

					'e.Row.ForeColor = Drawing.Color.BlueViolet

				Else
				End If

               End If
          End If

     End Sub
     'btnView_ApplicationComment

     Protected Sub btnView_ApplicationComment(sender As Object, e As EventArgs)


          'Dim btnViewApplicationComments As New ImageButton
          'btnViewApplicationComments = sender
          'Dim i As GridViewRow, cr As New Core

          'i = btnViewApplicationComments.NamingContainer
          'Me.txtAppCodee.Text = Me.gridApplications.Rows(i.RowIndex).Cells(4).Text

          '' Me.txtApplicationComment.Text = cr.PMgetApplicationComment(Me.gridApplications.Rows(i.RowIndex).Cells(4).Text, "POST")
          ''pops up the comment dialogue
          ' mpApplicationComments.Show()



          Dim btnViewApplicationComments As New ImageButton, dt As DataTable, j As Integer
          btnViewApplicationComments = sender
          Dim i As GridViewRow, cr As New Core

          i = btnViewApplicationComments.NamingContainer
          Me.txtApplicationIDD.Text = Me.gridApplications.Rows(i.RowIndex).Cells(4).Text
          ' Me.txtApplicationComment.Text = cr.PMgetApplicationComment(Me.gridApplications.Rows(i.RowIndex).Cells(4).Text, "POST")
          'pops up the comment dialogue
          dt = cr.PMgetApplicationComment(Me.gridApplications.Rows(i.RowIndex).Cells(4).Text, "PRE")
          lstApplicationComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstApplicationComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop
          mpApplicationComments.Show()





     End Sub


     Protected Sub btnView_ApprovalComment(sender As Object, e As EventArgs)


          Dim btnViewComments As New ImageButton, dt As DataTable, j As Integer
          btnViewComments = sender
          Dim i As GridViewRow, cr As New Core

          i = btnViewComments.NamingContainer
          Me.txtApplicationID.Text = Me.gridApplications.Rows(i.RowIndex).Cells(4).Text
          ' Me.txtApplicationComment.Text = cr.PMgetApplicationComment(Me.gridApplications.Rows(i.RowIndex).Cells(4).Text, "POST")
          'pops up the comment dialogue
          dt = cr.PMgetApplicationComment(Me.gridApplications.Rows(i.RowIndex).Cells(4).Text, "POST")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop
          mpAppComments.Show()


     End Sub


     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim scriptManagerA As New ScriptManager, scriptManagerb As New ScriptManager, scriptManagerC As New ScriptManager, dtusers As New DataTable

          scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.btnSchedule)

		scriptManagerb = ScriptManager.GetCurrent(Me.Page)
		scriptManagerb.RegisterPostBackControl(Me.imgDownloadSoft)

		scriptManagerC = ScriptManager.GetCurrent(Me.Page)
		scriptManagerC.RegisterPostBackControl(Me.gridRecievedDocument)


		Try

			If IsPostBack = False Then

				If IsNothing(Session("user")) = True Then

					'   getApprovalType()
					Response.Redirect("Login.aspx")
				ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then


					dtusers = Session("userDetails")
					getUserAccessMenu(Session("user"))
				End If

				getApprovalTypes()
				populateBank()

			Else
				getUserAccessMenu(Session("user"))
			End If


		Catch ex As Exception

		End Try

     End Sub

	Public Function getBanks() As List(Of String)

		Dim lstBankTypes As New List(Of String)
		Dim dc As New BanksDataContext
		Dim query = From m In dc.Banks
				  Select m

		For Each a As Bank In query
			lstBankTypes.Add(a.BankName)
			BankTypeCollection.Add(a.BankName, a.BankID)
		Next

		ViewState("BankTypeCollection") = BankTypeCollection
		Return lstBankTypes

	End Function

	'Populating bank List
	'Protected Sub populateBank()

	'	Dim myState As New States, i As Integer = 0
	'	Dim lstBank As New List(Of String)
	'	lstBank = getBanks()
	'	Me.ddBankName.Items.Clear()

	'	Do While i < lstBank.Count

	'		If Me.ddBankName.Items.Count = 0 Then
	'			Me.ddBankName.Items.Add("")
	'			Me.ddBankName.Items.Add(lstBank.Item(i))
	'		ElseIf Me.ddBankName.Items.Count > 0 Then
	'			Me.ddBankName.Items.Add(lstBank.Item(i))
	'		End If
	'		i = i + 1

	'	Loop

	'End Sub


	Protected Sub populateBank()

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

     Public Function getApprovalType() As List(Of String)


          Dim lstAppTypes As New List(Of String)
          Dim dc As New AppDocumentsDataContext
          Dim query = From m In dc.tblApplicationTypes
                      Select m
          For Each a As tblApplicationType In query
               lstAppTypes.Add(a.txtDescription)
               ApprovalTypeCollection.Add(a.txtDescription, a.pkiAppTypeId)
          Next
          ViewState("ApprovalTypeCollection") = ApprovalTypeCollection
          Return lstAppTypes


     End Function

     Protected Sub getApprovalTypes()

          Dim i As Integer = 0
          Dim lstAppTypes As New List(Of String)
          lstAppTypes = getApprovalType()
          ddApplicationType.Items.Clear()
          Do While i < lstAppTypes.Count

               If ddApplicationType.Items.Count = 0 Then
                    ddApplicationType.Items.Add("")
                    ddApplicationType.Items.Add(lstAppTypes.Item(i))
               ElseIf ddApplicationType.Items.Count > 0 Then
                    ddApplicationType.Items.Add(lstAppTypes.Item(i))
               End If
               i = i + 1

          Loop

     End Sub

     Protected Sub ddApplicationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddApplicationType.SelectedIndexChanged

          Dim cr As New Core, dt As New DataTable, i As Integer
          If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			If Me.rdCurrentApproval.Checked = True Then
				dt = cr.PMgetPencomApprovalBatchByType(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "", False)
			ElseIf Me.rdHistorical.Checked = True Then
				dt = cr.PMgetPencomApprovalBatchByType(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "Historical", False)
			End If

          Else

          End If
		ddApprovalBatch.Items.Clear()
          Do While i < dt.Rows.Count


               If ddApprovalBatch.Items.Count = 0 Then
                    ddApprovalBatch.Items.Add("")
                    ddApprovalBatch.Items.Add(dt.Rows(i).Item(1))
               ElseIf ddApprovalBatch.Items.Count > 0 Then
                    ddApprovalBatch.Items.Add(dt.Rows(i).Item(1))
               End If
               i = i + 1

          Loop

          '   BindGrid(cr.PMgetPencomApprovalsConfirmation(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "F"))




     End Sub

     Protected Sub ddApprovalBatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddApprovalBatch.SelectedIndexChanged



          Dim cr As New Core, dt As New DataTable, i As Integer
          'If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
		' ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

		If rdCurrentApproval.Checked = True Then

			dt = cr.PMgetPencomApprovalBatchByType(0, Me.ddApprovalBatch.SelectedItem.Text, False)

		ElseIf rdHistorical.Checked = True Then

			dt = cr.PMgetPencomApprovalBatchByType(-1, Me.ddApprovalBatch.SelectedItem.Text, False)

		End If
		'Else
		'End If

		ddExportedBatches.Items.Clear()
		Do While i < dt.Rows.Count


			If ddExportedBatches.Items.Count = 0 Then
				ddExportedBatches.Items.Add("")
				ddExportedBatches.Items.Add(dt.Rows(i).Item(1))
			ElseIf ddExportedBatches.Items.Count > 0 Then
				ddExportedBatches.Items.Add(dt.Rows(i).Item(1))
			End If
			i = i + 1

		Loop



	End Sub
	'getting all the types of benefit application types
	Public Function getApprovalType(typeName As String) As Integer


		Dim dc As New AppDocumentsDataContext
		Dim query = From m In dc.tblApplicationTypes
				  Where m.txtDescription = typeName
				  Select New With {m.pkiAppTypeId}

		Dim typeID As Integer
		For Each a In query
			typeID = a.pkiAppTypeId
		Next

		Return typeID

	End Function

     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim cr As New Core, dt As New DataTable
		Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)

		If Me.rdCurrentApproval.Checked = True Then
			dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True, "F")
		ElseIf Me.rdHistorical.Checked = True Then
			dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True, "A")
		End If


          ViewState("BatchApprovals") = dt
          BindGrid(dt)



     End Sub

     Protected Sub BindGrid(dt As DataTable)

          Me.gridApplications.DataSource = dt
		Me.gridApplications.DataBind()

		If dt.Rows.Count > 10 Then
			Me.pnlGrid.Height = Nothing
		Else
		End If

     End Sub

     Protected Sub gridApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.SelectedIndexChanged

		Dim selectedRowIndex As Integer, dtDocuments As New DataTable, cr As New Core, dt As New DataTable

		selectedRowIndex = Me.gridApplications.SelectedRow.RowIndex
		Dim row As GridViewRow = gridApplications.Rows(selectedRowIndex)
		dtDocuments = cr.PMgetSubmittedDocument(row.Cells(5).Text.ToString(), CStr(row.Cells(4).Text.ToString()))
		ViewState("AppCode") = CStr(row.Cells(4).Text.ToString())

		populateDocuments(dtDocuments)
		ddBankBranch.Items.Clear()

		ViewState("ApplicationID") = row.Cells(4).Text.ToString()

		dt = cr.PMgetApplicationByCode(row.Cells(4).Text.ToString())

		Me.txtAccountName.Text = (dt.Rows(0).Item("txtAccountName")).ToString
		Me.txtAccountNumber.Text = (dt.Rows(0).Item("txtAccountNo")).ToString
		Me.txtBVN.Text = dt.Rows(0).Item("txtBVN").ToString

		If Not dt.Rows(0).Item("fkiBankID") Is DBNull.Value And cr.PMgetBanks(CInt(dt.Rows(0).Item("fkiBankID"))).Rows.Count > 0 Then
			cr.PMgetBanks(CInt(dt.Rows(0).Item("fkiBankID")))
			Me.ddBankName.SelectedValue = cr.PMgetBanks(CInt(dt.Rows(0).Item("fkiBankID"))).Rows(0).Item("bankname")
		Else
			ddBankName.SelectedItem.Text = ""
		End If


		'If Not dt.Rows(0).Item("fkiBranchID") Is DBNull.Value Then
		'	Dim lstBankBranches As New List(Of String)
		'	lstBankBranches = getBankBranches(CInt(dt.Rows(0).Item("fkiBankID")))
		'	lstBankBranches = Nothing
		'	Me.ddBankBranch.Items.Add(Me.getBankBranchName(CInt(dt.Rows(0).Item("fkiBranchID"))).ToString)
		'ElseIf ddBankBranch.Items.Count > 0 Then
		'	ddBankBranch.SelectedItem.Text = ""
		'Else
		'End If

		If Not dt.Rows(0).Item("fkiBranchID") Is DBNull.Value And cr.PMgetBankBranches(CInt(dt.Rows(0).Item("fkiBankID")), CInt(dt.Rows(0).Item("fkiBranchID"))).Rows.Count > 0 Then

			Dim lstBankBranches As New DataTable
			lstBankBranches = cr.PMgetBankBranches(CInt(dt.Rows(0).Item("fkiBankID")), CInt(dt.Rows(0).Item("fkiBranchID")))
			Me.ddBankBranch.Items.Add(lstBankBranches.Rows(0).Item("BranchName") & "                   | " & lstBankBranches.Rows(0).Item("BankBranchID"))
		ElseIf ddBankBranch.Items.Count > 0 Then
			ddBankBranch.SelectedItem.Text = ""
		Else

		End If


     End Sub


	'getting the list of bank branches per bank
	Public Function getBankBranchName(id As Integer) As String
		Try


			Dim lstBankBranchName As New List(Of String)
			Dim dc As New BanksDataContext
			Dim querys = From m In dc.BankBranches
					  Where m.BankBranchID = id
					  Select New With {m.BranchName}

			For Each m In querys
				lstBankBranchName.Add(m.BranchName)
			Next



			If (lstBankBranchName.Count > 0) Then
				Return lstBankBranchName(0).ToString
			ElseIf (lstBankBranchName.Count = 0) Then
				'   pnlError.Visible = True
				'  Me.lblError.Text = "Error Loading Bank Branches"
				Return Nothing
			End If

		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
			' pnlError.Visible = True
			' Me.lblError.Text = "Error Loading Bank Branches"
		End Try

	End Function

	'handle the view image button on the submitted document grid on the page
	Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

		Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
		btnViewDocumentLog = sender
		Dim i As GridViewRow
		i = btnViewDocumentLog.NamingContainer
		'   appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

		If Not IsNothing(ViewState("Documents")) = True Then

			Dim dt As DataTable = ViewState("Documents")
			'retrieving the location of the scanned document
			documentPath = dt.Rows(i.RowIndex).Item("DocumentPath").ToString()

			'testing if the file still exist in the saved file path
			If File.Exists(documentPath) = True Then

				DownLoadDocument(documentPath)

			ElseIf File.Exists(documentPath) = False Then

				DownLoadDocument(documentPath)

			End If


			''''dms integration addition'''''''''''''''''''''''''''''''''''''''''''''''''''''''''

			Dim dtDocs As New DataTable, dmsDocumentID As String, dmsDocumentExt As String
			If IsNothing(ViewState("Documents")) = False Then

				dtDocs = ViewState("Documents")
				dmsDocumentID = dtDocs.Rows(i.RowIndex).Item("DocumentID")
				dmsDocumentExt = dtDocs.Rows(i.RowIndex).Item("DocumentExtension")

				Dim dms As New PaymentModuleDMSWindow.CEEntry, DMSDocumentPath As String
				Dim uName As String, uPWD As String, uRI As String

				uName = ConfigurationManager.AppSettings("FileNetUName")
				uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
				uRI = ConfigurationManager.AppSettings("FileNetURI")

				dms.getConnection(uName, uPWD, uRI)
				DMSDocumentPath = dms.GetDocument(Server.MapPath("~/FileDownLoads"), dmsDocumentID, "LPPFA", "." & dmsDocumentExt)
				DownLoadDocument(DMSDocumentPath)

			Else
			End If


			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

		Else

		End If





	End Sub


	Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

		If IsNothing(ViewState("Documents")) = False Then

			Dim dt As DataTable = ViewState("Documents")
			If e.Row.RowType = DataControlRowType.DataRow Then

				'  MsgBox("" & dt.Rows(e.Row.RowIndex).Item("isVerified").ToString)

				If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

					e.Row.ForeColor = System.Drawing.Color.Red
					e.Row.Enabled = False
					'isVerified
				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "True" Then
					e.Row.ForeColor = System.Drawing.Color.Green

				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then
					e.Row.ForeColor = System.Drawing.Color.Blue
					'e.Row.Enabled = False

				End If

			End If
		Else
		End If

	End Sub

	Public Function getBankBranches(bankID As Integer) As List(Of String)
		Try

			Dim dc As New BanksDataContext
			Dim lstBankBranches As New List(Of String)
			Dim query = From m In dc.Banks Join n In dc.BankBranches On m.BankID Equals n.BankID Where m.BankID = bankID _
					  Select New With {n.BankBranchID, n.BranchName}
			For Each a In query

				lstBankBranches.Add(a.BranchName)
				'DocumentCollection.Add(a.BranchName, a.BankBranchID)

				If DocumentCollection.ContainsKey(a.BranchName) = True Then
				Else
					DocumentCollection.Add(a.BranchName, a.BankBranchID)

				End If

			Next
			ViewState("BankBranchCollection") = BankBranchCollection
			Return lstBankBranches
		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs")
			logerr.Logger(ex.Message)

			'  pnlError.Visible = True
			' Me.lblError.Text = "Error Loading Bank Branches"

		End Try
	End Function

	'retrieving the bank name with the ID
	Public Function getBankName(id As Integer) As String
		Try


			Dim lstBankName As New List(Of String)
			Dim dc As New BanksDataContext
			Dim querys = From m In dc.Banks
					  Where m.BankID = id
					  Select New With {m.BankName}

			For Each m In querys
				lstBankName.Add(m.BankName)
			Next

			If (lstBankName.Count > 0) Then
				Return lstBankName(0).ToString
			Else
				Return Nothing
			End If
		Catch ex As Exception
			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
			logerr.Logger(ex.Message)
		End Try

	End Function

	Protected Sub populateDocuments(dt As DataTable)

		Try

			ViewState("Documents") = dt
			Me.gridRecievedDocument.DataSource = dt
			Me.gridRecievedDocument.DataBind()
			If dt.Rows.Count > 0 Then

				Me.pnlUploadDetail.Height = Nothing
			Else
				Me.pnlUploadDetail.Height = 290

			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

          For Each grow As GridViewRow In Me.gridApplications.Rows


               'If roleName = "FIN" Then
               Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkPINApprovalAuthorised"), CheckBox)

               If cb.Enabled = True Then
                    cb.Checked = True
               Else
               End If

               ' End If
          Next

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

          For Each grow As GridViewRow In Me.gridApplications.Rows

               Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkPINApprovalAuthorised"), CheckBox)

               If cb.Enabled = True Then
                    cb.Checked = False
               Else
               End If

          Next

     End Sub

     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

          If IsNothing(Session("user")) = True Then
               Response.Redirect("Login.aspx")
          Else
			Dim cr As New Core
			'the first 2 indicate post-approval comment while the  second 1 indicate a default checklist code
			cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, CStr(Session("user")), 2, 1)
               txtApplicationComment.Text = ""
               refreshCommentList(txtApplicationID.Text)
               Me.mpAppComments.Show()
          End If




     End Sub

     'refreshing the pop up comment list on an application
     Protected Sub refreshCommentList(appCode As String)
          Dim cr As New Core, j As Integer, dt As DataTable
          dt = cr.PMgetApplicationComment(appCode, "POST")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop
          mpAppComments.Show()

     End Sub

     Public Sub PMUndoControlCheck(appCode As String, uName As String, stage As String)
          Try

               Dim db As New DbConnection
               Dim mycon As New SqlClient.SqlConnection
               mycon = db.getConnection("PaymentModule")
               Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

               Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
               myComm = mycon.CreateCommand
               myComm.Transaction = sqlTran

               'updating the approvalS for control verification 
               If stage = "CHECKED" Then


                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlCheckedBy = '" & uName & "',dteChecked = null where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()


                    'updating the approvalS for another level of control verification 
               ElseIf stage = "VERIFIED" Then

                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlVerifiedBy = '" & uName & "',dteVerified = null where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()

                    'DateTime.Parse(Now.Date).ToString("yyyy-MM-dd")

                    'updating the approvalS for finance authorisation for payment

               ElseIf stage = "Authorized" Then

                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlAuthorisedBy = '" & uName & "',dteAuthorised = null where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()

               End If





               sqlTran.Commit()

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub


	Protected Sub PMUpdateApprovalControlCheck(appCode As String, uName As String, stage As String)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			'updating the approvalS for first level internal control checks for payment
			If stage = "CHECKED" Then


				myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlCheckedBy = '" & uName & "',dteChecked = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				'updating the approvalS for another level of internal control verification 
			ElseIf stage = "VERIFIED" Then

				myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlVerifiedBy = '" & uName & "',dteVerified = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()



				'updating the approvalS for finance authorisation for payment
			ElseIf stage = "Authorize" Then

				myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlAuthorisedBy = '" & uName & "',dteAuthorised = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				myComm.CommandText = "insert into tblSIPensioneer (txtPIN,txtFullName,numPWAmount,numPension,intBankID,intBankBranchID,txtBankAccount,txtFrequency,dteAnniversary,txtStatus,txtApplicationcode) select a.txtPIN,replace(a.txtfullName,'|','') as FullName, Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance ,numApplicationAmount,a.fkibankid,a.fkibranchid,a.txtAccountNo,1 as Frequency,cast(getdate() as date) as Anniversary,'P',b.txtApplicationcode from tblMemberApplication a,tblApplicationApprovalPayee b, tblPensionEnhancement c where a.txtapplicationcode = b.txtapplicationcode and c.txtPIN = a.txtPIN  AND b.txtapplicationcode = '" & appCode & "' AND fkiAppTypeId = 17 and not exists (select * from tblSIPensioneer where txtpin = a.txtPencomBatch )"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()



				myComm.CommandText = "update d set d.numPension = a.numApplicationAmount,d.intBankID = a.fkibankid,d.intBankBranchID = a.fkibranchid,d.txtBankAccount = a.txtAccountNo,txtFrequency = 1,dteAnniversary = cast(getdate() as date),txtStatus = 'P',txtApplicationcode = a.txtApplicationcode,txtFullName = replace(a.txtfullName,'|','') from tblMemberApplication a,tblApplicationApprovalPayee b, tblPensionEnhancement c,tblSIPensioneer d where a.txtapplicationcode = b.txtapplicationcode and c.txtPIN = a.txtPIN and d.txtpin = a.txtpin AND b.txtapplicationcode = '" & appCode & "' AND fkiAppTypeId = 17"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


			End If





			sqlTran.Commit()

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try
	End Sub

     Protected Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click

		' If IsNothing(ViewState("ApprovalBatch")) = False Then
		Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
          Dim batchNum As String = Me.ddExportedBatches.SelectedItem.Text
          '  Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & batchNum & ".pdf"
          Dim filePath As String = Server.MapPath("~/FileDownLoads/" & batchNum.Replace("/", "") & ".pdf")
		generateFiles(Me.ddExportedBatches.SelectedItem.Text, filePath, apptypeID, "PDF")
          If File.Exists(filePath) = True Then
               DownLoadDocument(filePath)
          Else
               'DownLoadDocument(path As String)
          End If

          'Else

          'End If

     End Sub
     ' generating the pdf extract schedule to enpower
	Private Sub generateFiles(approvalBatchNum As String, path As String, apptypeID As Integer, fileType As String)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		If apptypeID = 7 Then
			rdoc.Load(Server.MapPath("~/Report/ApprovalPaymentScheduleAVC.rpt"))
		ElseIf apptypeID = 3 Then
			rdoc.Load(Server.MapPath("~/Report/ApprovalPaymentScheduleLPW.rpt"))
		Else
			rdoc.Load(Server.MapPath("~/Report/ApprovalPaymentSchedule.rpt"))
		End If

		Dim ds As DataSet
		ds = populateSchedule(approvalBatchNum, apptypeID)
		rdoc.SetDataSource(ds.Tables(0))

		crDiskFileDestinationOptions.DiskFileName = path
		crExportOptions = rdoc.ExportOptions

		With crExportOptions

			.ExportDestinationType = ExportDestinationType.DiskFile
			'.ExportFormatType = ExportFormatType.PortableDocFormat

			If fileType = "PDF" Then
				.ExportFormatType = ExportFormatType.PortableDocFormat
			ElseIf fileType = "XLS" Then
				.ExportFormatType = ExportFormatType.ExcelWorkbook
			End If

			.ExportDestinationOptions = crDiskFileDestinationOptions
			.ExportFormatOptions = crFormatypeOption

		End With

		rdoc.Export()


	End Sub


	Private Function populateSchedule(approvalBatchNum As String, apptype As Integer) As DataSet

		Dim cr As New Core, dtApplicationSchedule As New DataTable, i As Integer = 0
		'dtApplicationSchedule = cr.PMgetApprovalPaymentSchedule(approvalBatchNum, "", apptype)

		dtApplicationSchedule = cr.PMgetApprovalPaymentSchedule(approvalBatchNum, apptype, "F")

		'PMgetApprovalPaymentSchedule()
		Dim ds As New dsApprovalSchedule
		Dim newSNRow As DataRow
		Dim isfullyChecked As Boolean = True
		Dim isfullyVerified As Boolean = True
		Dim isfullyAuthorised As Boolean = True

		Do While i < dtApplicationSchedule.Rows.Count
			If dtApplicationSchedule.Rows(i).Item("txtControlCheckedBy").ToString = "" Then
				isfullyChecked = False
			Else
			End If

			If dtApplicationSchedule.Rows(i).Item("txtControlVerifiedBy").ToString = "" Then
				isfullyVerified = False
			Else
			End If

			If dtApplicationSchedule.Rows(i).Item("txtControlAuthorisedBy").ToString = "" Then
				isfullyAuthorised = False
			Else
			End If
			i = i + 1
		Loop
		i = 0

		Do While i < dtApplicationSchedule.Rows.Count

			newSNRow = ds.Tables(0).NewRow

			newSNRow("Name") = dtApplicationSchedule.Rows(i).Item("txtFullName")
			newSNRow("Pin") = dtApplicationSchedule.Rows(i).Item("txtpin")
			newSNRow("Sector") = dtApplicationSchedule.Rows(i).Item("txtSector")
			newSNRow("PlatForm") = dtApplicationSchedule.Rows(i).Item("PlatForm")
			newSNRow("ApprovalDate") = dtApplicationSchedule.Rows(i).Item("dteApproval")
			newSNRow("ApprovedAmount") = dtApplicationSchedule.Rows(i).Item("numApproved")

			If apptype = 1 Then
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")

			ElseIf apptype = 16 Then
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")

			ElseIf apptype = 2 Then

				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")

			ElseIf apptype = 3 Then

				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
				newSNRow("TaxAtPayment") = dtApplicationSchedule.Rows(i).Item("Arrears")
				newSNRow("DOR") = dtApplicationSchedule.Rows(i).Item("DOR")
				newSNRow("Pension") = dtApplicationSchedule.Rows(i).Item("PensionToPay")
				newSNRow("ArrearsPeriod") = Math.Abs(CInt(dtApplicationSchedule.Rows(i).Item("Arrears") / dtApplicationSchedule.Rows(i).Item("PensionToPay")))

			ElseIf apptype = 7 Then

				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
				newSNRow("InterestAtPayment") = dtApplicationSchedule.Rows(i).Item("numInterestAtPayment")
				newSNRow("TaxAtPayment") = dtApplicationSchedule.Rows(i).Item("numTaxAtPayment")
				newSNRow("TaxIDNo") = dtApplicationSchedule.Rows(i).Item("txtTIN")
				newSNRow("Location") = dtApplicationSchedule.Rows(i).Item("Location")

			ElseIf apptype = 8 Then
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			ElseIf apptype = 5 Then
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			ElseIf apptype = 6 Then
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			ElseIf apptype = 11 Then
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			Else
				newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numApproved")
			End If


			newSNRow("AccountName") = dtApplicationSchedule.Rows(i).Item("txtAccountName").ToString
			newSNRow("BankDetails") = dtApplicationSchedule.Rows(i).Item("BankDetails").ToString
			newSNRow("AccountNo") = dtApplicationSchedule.Rows(i).Item("txtAccountNo").ToString
			newSNRow("Remarks") = dtApplicationSchedule.Rows(i).Item("txtRemarks").ToString
			newSNRow("PreparedBy") = dtApplicationSchedule.Rows(i).Item("txtCreatedBy").ToString
			newSNRow("ConfirmedBy") = dtApplicationSchedule.Rows(i).Item("txtConfirmedBy").ToString
			newSNRow("AppTypeID") = dtApplicationSchedule.Rows(i).Item("fkiAppTypeId").ToString

			If isfullyChecked = True Then
				newSNRow("CheckedBy") = dtApplicationSchedule.Rows(i).Item("txtControlCheckedBy").ToString
			Else
				newSNRow("CheckedBy") = ""
			End If

			If isfullyVerified = True Then
				newSNRow("VerifiedBy") = dtApplicationSchedule.Rows(i).Item("txtControlVerifiedBy").ToString
			Else
				newSNRow("VerifiedBy") = ""
			End If

			If isfullyAuthorised = True Then
				newSNRow("AuthorizedBy") = dtApplicationSchedule.Rows(i).Item("txtControlAuthorisedBy").ToString
			Else
				newSNRow("AuthorizedBy") = ""
			End If

			ds.Tables(0).Rows.Add(newSNRow)


			i = i + 1
		Loop

		Return ds

	End Function


     Protected Sub btnCancelAuthorized_Click(sender As Object, e As EventArgs) Handles btnCancelAuthorized.Click
          Try

               Dim indx As Integer = Me.gridApplications.SelectedIndex, dt As New DataTable, cr As New Core

			If IsNothing(Session("user")) = True Or IsNothing(ViewState("AppCode")) = True Then
				Response.Redirect("Login.aspx")
			Else
				cr.PMCancelPaymentControls(CStr(ViewState("AppCode")), Session("user"), "Authorized")
				Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
				dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True)
				ViewState("BatchApprovals") = dt
				BindGrid(dt)

			End If

          Catch ex As Exception

          End Try


     End Sub

     Protected Sub btnAuthorised_Click(sender As Object, e As EventArgs) Handles btnAuthorised.Click


          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, dt As New DataTable

          Try
               If IsNothing(Session("user")) = True Then
                    Response.Redirect("Login.aspx")
               Else
                    For Each grow As GridViewRow In Me.gridApplications.Rows

                         cb = grow.FindControl("ChkPINApprovalAuthorised")

                         If cb.Checked = True And cb.Enabled = True Then
                              PMUpdateApprovalControlCheck(grow.Cells(4).Text, CStr(Session("user")), "Authorize")
                         Else
                         End If

                    Next
				Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
				dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True)
                    ViewState("BatchApprovals") = dt
                    BindGrid(dt)

               End If




          Catch ex As Exception


          End Try


     End Sub

     Protected Sub btnAppCommentRemove_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentRemove.Click


          Dim cr As New Core
          Try
               If IsNothing(Session("UserName")) = False Then
                    Dim UName As String = CStr(Session("UserName"))
                    Dim str() As String = Me.lstComments.SelectedItem.Text.Split(":")
                    If UName = LTrim(RTrim(CStr(str(1)))) Then

                         cr.PMRemoveComment(CInt(str(0)))
                         refreshCommentList(txtApplicationID.Text)

                    Else
                         Me.mpAppComments.Show()
                    End If
               Else

               End If

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub lstApplicationComments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstApplicationComments.SelectedIndexChanged

          Dim msg As String = (Me.lstApplicationComments.Items(lstApplicationComments.SelectedIndex).Text).Split(":")(2)
          Me.txtApplicationCommentt.Text = msg
          Me.mpApplicationComments.Show()

     End Sub


	Protected Sub imgDownloadSoft_Click(sender As Object, e As ImageClickEventArgs) Handles imgDownloadSoft.Click

		Try

			'Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
			'Dim cr As New Core, dt As New DataTable

			'dt = populateSchedule(Me.ddExportedBatches.SelectedItem.Text, apptypeID).Tables(0)
			'cr.ExtractCSV(dt, "ApprovalPaymentSchedule")


			Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
			Dim batchNum As String = Me.ddExportedBatches.SelectedItem.Text
			'  Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & batchNum & ".pdf"
			Dim filePath As String = Server.MapPath("~/FileDownLoads/" & batchNum.Replace("/", "") & ".xlsx")
			generateFiles(Me.ddExportedBatches.SelectedItem.Text, filePath, apptypeID, "XLS")

			If File.Exists(filePath) = True Then
				imgDownloadSoft.Enabled = True
				DownLoadDocument(filePath)

			Else
				imgDownloadSoft.Enabled = False
			End If



		Catch ex As Exception

		End Try


	End Sub

	Protected Sub rdCurrentApproval_CheckedChanged(sender As Object, e As EventArgs) Handles rdCurrentApproval.CheckedChanged

		Me.ddApprovalBatch.Items.Clear()
		Me.ddExportedBatches.Items.Clear()
	End Sub

	Protected Sub rdHistorical_CheckedChanged(sender As Object, e As EventArgs) Handles rdHistorical.CheckedChanged
		Me.ddApprovalBatch.Items.Clear()
		Me.ddExportedBatches.Items.Clear()
	End Sub
End Class
