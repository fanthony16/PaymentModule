Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Partial Class frmApprovalControlCheckView
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable

     Protected Sub Reset_Click(sender As Object, e As EventArgs)

          Dim cr As New Core
          Dim btnResetUnprocessed As New ImageButton, BatchApprovalCode As String = ""
          btnResetUnprocessed = sender
          Dim i As GridViewRow
          i = btnResetUnprocessed.NamingContainer
		cr.PMApprovalPayeeStatus(Me.gridApplications.Rows(i.RowIndex).Cells(4).Text, Session("user"), "P", 0, "Recall")
          Dim dt As New DataTable
          dt = cr.PMgetPencomApprovalBatchByType(0, Me.ddExportedBatches.SelectedItem.Text, True)
          ViewState("BatchApprovals") = dt
          BindGrid(dt)

          ' Else
          ' End If

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

          Try

			Dim scriptManagerA, scriptManagerB, scriptManagerC As New ScriptManager, dtusers As New DataTable
               scriptManagerA = ScriptManager.GetCurrent(Me.Page)
               scriptManagerA.RegisterPostBackControl(btnSchedule)

               scriptManagerB = ScriptManager.GetCurrent(Me.Page)
			scriptManagerB.RegisterPostBackControl(Me.imgDownloadSoft)

			scriptManagerB = ScriptManager.GetCurrent(Me.Page)
			scriptManagerB.RegisterPostBackControl(btnPensionExtract)



               If IsPostBack = False Then

                    If IsNothing(Session("user")) = True Then

                         '   getApprovalType()
                         Response.Redirect("Login.aspx")
                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then


                         dtusers = Session("userDetails")
                         getUserAccessMenu(Session("user"))
                    End If

                    getApprovalTypes()

               Else
                    getUserAccessMenu(Session("user"))
               End If


          Catch ex As Exception

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
               dt = cr.PMgetPencomApprovalBatchByType(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "", False)
          Else

          End If
          'loading all the approval batches per application type
          'ddApprovalBatch.Items.Clear()
          'Me.ddExportedBatches.Items.Clear()
          'Do While i < dt.Rows.Count


          '     If ddApprovalBatch.Items.Count = 0 Then
          '          ddApprovalBatch.Items.Add("")
          '          ddApprovalBatch.Items.Add(dt.Rows(i).Item(1))
          '     ElseIf ddApprovalBatch.Items.Count > 0 Then
          '          ddApprovalBatch.Items.Add(dt.Rows(i).Item(1))
          '     End If
          '     i = i + 1

          'Loop

          '   BindGrid(cr.PMgetPencomApprovalsConfirmation(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "F"))




     End Sub

     'Protected Sub ddApprovalBatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddApprovalBatch.SelectedIndexChanged

     '     Dim cr As New Core, dt As New DataTable, i As Integer
     '     Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
     '     'If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
     '     ' ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
     '     'txtApprovalBatches
     '     dt = cr.PMgetPencomApprovalBatchByType(0, Me.ddApprovalBatch.SelectedItem.Text, False)

     '     'Else
     '     'End If
     '     ddExportedBatches.Items.Clear()
     '     Do While i < dt.Rows.Count


     '          If ddExportedBatches.Items.Count = 0 Then
     '               ddExportedBatches.Items.Add("")
     '               ddExportedBatches.Items.Add(dt.Rows(i).Item(1))
     '          ElseIf ddExportedBatches.Items.Count > 0 And dt.Rows(i).Item(1).ToString <> "" Then
     '               ddExportedBatches.Items.Add(dt.Rows(i).Item(1))
     '          End If
     '          i = i + 1

     '     Loop
     'End Sub

     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim cr As New Core, dt As New DataTable
		Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
		dt = cr.PMgetPencomApprovalBatchByType(apptypeID, Me.ddExportedBatches.SelectedItem.Text, True)
          ViewState("BatchApprovals") = dt
          BindGrid(dt)

     End Sub

     Protected Sub BindGrid(dt As DataTable)

		If dt.Rows.Count > 10 Then
			Me.pnlGrid.Height = Nothing
		Else

		End If

          Me.gridApplications.DataSource = dt
          Me.gridApplications.DataBind()

     End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click


          For Each grow As GridViewRow In Me.gridApplications.Rows


               Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkPINApprovalChecked"), CheckBox)

               If cb.Enabled = True Then
                    cb.Checked = True
               Else
               End If

          Next

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click



          For Each grow As GridViewRow In Me.gridApplications.Rows


               Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkPINApprovalChecked"), CheckBox)

               If cb.Enabled = True Then
                    cb.Checked = False
               Else
               End If

          Next

     End Sub

     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

          'Dim cr As New Core
          'cr.PMUpdateApplicationControlCheck(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, "o-taiwo", 0, "POST")


		Dim cr As New Core
		'the first 2 indicate post-approval comment while the  second 1 indicate a default checklist code
		cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 2, 1)
          txtApplicationComment.Text = ""
          refreshCommentList(txtApplicationID.Text)
          Me.mpAppComments.Show()


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

               ElseIf stage = "Authorize" Then

                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlAuthorisedBy = '" & uName & "',dteAuthorised = null where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()

               End If





               sqlTran.Commit()

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub


     Public Sub PMUpdateApprovalControlCheck(appCode As String, uName As String, stage As String)
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


                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlCheckedBy = '" & uName & "',dteChecked = '" & DateTime.Parse(Now.Date).ToString("yyyy-MM-dd") & "' where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()


                    'updating the approvalS for another level of control verification 
               ElseIf stage = "VERIFIED" Then

                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlVerifiedBy = '" & uName & "',dteVerified = '" & DateTime.Parse(Now.Date).ToString("yyyy-MM-dd") & "' where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()

                    'DateTime.Parse(Now.Date).ToString("yyyy-MM-dd")

                    'updating the approvalS for finance authorisation for payment

               ElseIf stage = "Authorize" Then

                    myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlAuthorisedBy = '" & uName & "',dteAuthorised = '" & DateTime.Parse(Now.Date).ToString("yyyy-MM-dd") & "' where txtApplicationCode = '" & appCode & "'"
                    command.CommandType = CommandType.Text
                    myComm.ExecuteNonQuery()

               End If


               sqlTran.Commit()

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub


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
     Protected Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click

          ' If IsNothing(ViewState("ApprovalBatch")) = False Then
          Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
          Dim batchNum As String = Me.ddExportedBatches.SelectedItem.Text
          '  Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & batchNum & ".pdf"
          Dim filePath As String = Server.MapPath("~/FileDownLoads/" & batchNum.Replace("/", "") & ".pdf")
		generateFiles(Me.ddExportedBatches.SelectedItem.Text, filePath, apptypeID, "PDF")

		If File.Exists(filePath) = True Then

			imgDownloadSoft.Enabled = True
			DownLoadDocument(filePath)

		Else
			'DownLoadDocument(path As String)
			imgDownloadSoft.Enabled = False
		End If

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
		ViewState("ApprovalSchedule") = ds.Tables(0)
		rdoc.SetDataSource(ds.Tables(0))

		crDiskFileDestinationOptions.DiskFileName = path
		crExportOptions = rdoc.ExportOptions

		With crExportOptions

			.ExportDestinationType = ExportDestinationType.DiskFile
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

	Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
		Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
	End Sub

	Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
		Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
	End Sub


     Private Function populateSchedule(approvalBatchNum As String, apptype As Integer) As DataSet

          Dim cr As New Core, dtApplicationSchedule As New DataTable, i As Integer = 0
          dtApplicationSchedule = cr.PMgetApprovalPaymentSchedule(approvalBatchNum, "", apptype)
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

			ElseIf apptype = 4 Then

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


			'If apptype = 7 Then
			'	newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			'Else
			'	newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numApproved")
			'End If


			'If apptype = 5 Then
			'	newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			'Else
			'	newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numApproved")
			'End If

			'If apptype = 6 Then
			'	newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numToPay")
			'Else
			'	newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(i).Item("numApproved")
			'End If


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

			i = i + 1

			ds.Tables(0).Rows.Add(newSNRow)
		Loop

          Return ds

     End Function

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

          

			'       Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
			'       Dim cr As New Core, dt As New DataTable

			'       dt = populateSchedule(Me.ddExportedBatches.SelectedItem.Text, apptypeID).Tables(0)
			'cr.ExtractCSV(dt, "ApprovalPaymentSchedule")


			' If IsNothing(ViewState("ApprovalBatch")) = False Then
			Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
			Dim batchNum As String = Me.ddExportedBatches.SelectedItem.Text
			'  Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & batchNum & ".pdf"
			Dim filePath As String = Server.MapPath("~/FileDownLoads/" & batchNum.Replace("/", "") & ".xlsx")
			generateFiles(Me.ddExportedBatches.SelectedItem.Text, filePath, apptypeID, "XLS")

			If File.Exists(filePath) = True Then
				imgDownloadSoft.Enabled = True

				DownLoadDocument(filePath)

			Else
				'DownLoadDocument(path As String)
				imgDownloadSoft.Enabled = False
			End If




          Catch ex As Exception

          End Try


     End Sub


     Protected Sub gridApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.SelectedIndexChanged

     End Sub

     Protected Sub btnViewExportedBatches_Click(sender As Object, e As EventArgs) Handles btnViewExportedBatches.Click

          Dim cr As New Core, dt As New DataTable, i As Integer
          Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
          'If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
          ' ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		'txtApprovalBatches

		dt = cr.PMgetPencomApprovalBatchByType(0, Me.txtApprovalBatches.Text.Trim, False)

          'Else
          'End If
          ddExportedBatches.Items.Clear()
          Do While i < dt.Rows.Count


               If ddExportedBatches.Items.Count = 0 Then
                    ddExportedBatches.Items.Add("")
                    ddExportedBatches.Items.Add(dt.Rows(i).Item(1))
               ElseIf ddExportedBatches.Items.Count > 0 And dt.Rows(i).Item(1).ToString <> "" Then
                    ddExportedBatches.Items.Add(dt.Rows(i).Item(1))
               End If
               i = i + 1

          Loop

     End Sub

	
	Protected Sub btnPensionExtract_Click(sender As Object, e As EventArgs) Handles btnPensionExtract.Click

		Dim cr As New Core, dt As New DataTable

		dt = cr.PMGetNewPensionEntrants(CDate(txtStartDate.Text), CDate(txtEndDate.Text))

		If dt.Rows.Count > 0 Then
			Me.dvExtractError.Visible = False
			cr.ExtractCSV(dt, "PensionExtract")
		Else
			spExtractError.InnerText = "No Record Found"
			Me.dvExtractError.Visible = True

		End If


	End Sub
End Class
