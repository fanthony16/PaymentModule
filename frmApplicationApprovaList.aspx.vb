Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Class frmApplicationApprovaList
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable
     Dim BankTypeCollection As New Hashtable
     Dim DocumentCollection As New Hashtable
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


     Protected Sub gridApplication_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
          Dim dtt As New DataTable
          If IsNothing(ViewState("BatchApplications")) = False Then
               Dim dt As DataTable = ViewState("BatchApplications")
               dtt = dt
               If e.Row.RowType = DataControlRowType.DataRow Then
                    If (CStr(dt.Rows(e.Row.RowIndex).Item("txtStatus"))).Trim = "F" Then

                         e.Row.ForeColor = System.Drawing.Color.Blue

                    ElseIf (CStr(dt.Rows(e.Row.RowIndex).Item("txtStatus"))).Trim = "C" Then

                         e.Row.ForeColor = System.Drawing.Color.Green

                         Dim cb As CheckBox = TryCast(e.Row.FindControl("ChkApprovalConfirm"), CheckBox)
                         cb.Checked = True


                    ElseIf (CStr(dt.Rows(e.Row.RowIndex).Item("txtStatus"))).Trim = "P" Then



                    End If

               End If
          End If
          MyApplicateionReset(dtt)

     End Sub

     Protected Sub MyApplicateionReset(dt As DataTable)

          For Each grow As GridViewRow In Me.gridApplications.Rows

               If grow.Enabled = True Then
                    Dim cb As ImageButton = TryCast(grow.Cells(8).FindControl("btnResetUnprocessed"), ImageButton)

                    Dim chk As CheckBox = TryCast(grow.Cells(0).FindControl("ChkApprovalConfirm"), CheckBox)

                    If dt.Rows(grow.RowIndex).Item("txtStatus").ToString.Trim = "C" Then

                         cb.Enabled = True
                         chk.Enabled = False

                    ElseIf dt.Rows(grow.RowIndex).Item("txtStatus").ToString.Trim = "P" Then

                         cb.Enabled = False
                         chk.Enabled = True

                    ElseIf dt.Rows(grow.RowIndex).Item("txtStatus").ToString.Trim = "F" Then

					'  cb.Enabled = False
                         chk.Enabled = False


                    End If
                    ' cb.Checked = True
               Else
               End If

          Next

     End Sub


     Protected Sub gridRMAS_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)




          If IsNothing(ViewState("Approvals")) = False Then

               Dim dt As DataTable = ViewState("Approvals")
               If e.Row.RowType = DataControlRowType.DataRow Then



                    If IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteConfirmed")) = True Then

                         e.Row.ForeColor = System.Drawing.Color.Green


                    ElseIf IsDBNull(dt.Rows(e.Row.RowIndex).Item("dteConfirmed")) = False Then



                    End If

               End If
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

		Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager
		scriptManagerA = ScriptManager.GetCurrent(Me.Page)
		scriptManagerA.RegisterPostBackControl(Me.gridRecievedDocument)


		scriptManagerB = ScriptManager.GetCurrent(Me.Page)
		scriptManagerB.RegisterPostBackControl(imgExport)


		Try

			If IsPostBack = False And Not Context.Request.QueryString("ApplicationID") Is Nothing Then

				If IsNothing(Session("user")) = True Then
					Response.Redirect("Login.aspx")
				Else
					getApprovalTypes()
					populateBank()
					getUserAccessMenu(Session("user"))

					Dim cr As New Core
					Me.ddApplicationType.SelectedValue = cr.PMgetApprovalTypebyID(CInt(Context.Request.QueryString("ApplicationID")))
					BindGrid(cr.PMgetPencomApprovalsConfirmation(CInt(Context.Request.QueryString("ApplicationID")), "A"))

				End If

			ElseIf IsPostBack = False And Context.Request.QueryString("ApplicationID") Is Nothing Then

				If IsNothing(Session("user")) = True Then
					Response.Redirect("Login.aspx")
				Else
					Dim cr As New Core
					getApprovalTypes()
					populateBank()
					getUserAccessMenu(Session("user"))
					Me.gridApplicationSummary.DataSource = cr.PMgetApplicationSummary
					gridApplicationSummary.DataBind()
					mpApplicationSummary.Show()
				End If

			Else
				getUserAccessMenu(Session("user"))
			End If

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try
	End Sub

     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

          Dim i As Integer = 0, str As String = "", cr As New Core
          Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
          ViewState("apptypeID") = apptypeID
          If apptypeID = 3 Then
               BindFieldPW()
          ElseIf apptypeID = 4 Then
			BindFieldAnnuity()

		ElseIf apptypeID = 15 Then
			BindFieldAnnuity()

          Else
               BindField()
          End If

          Try


               If IsNothing(ViewState("ApprovalTypeCollection")) = False Then

                    ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

                    BindGrid(cr.PMgetPencomApprovalsConfirmation(CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)), "A"))
                    ' BindGridApplication(cr.PMgetPencomApprovalBatchApplication(""))
               Else

               End If

          Catch ex As Exception

               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message & "Approval List")

          End Try

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

     Protected Sub BindGrid(dt As DataTable)

          ViewState("Approvals") = dt
          Me.gridApprovalBatch.DataSource = dt
          Me.gridApprovalBatch.DataBind()

     End Sub

     Protected Sub gridApprovalBatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApprovalBatch.SelectedIndexChanged

          Dim cr As New Core, selectedRowIndex As Integer, dt As New DataTable
          selectedRowIndex = gridApprovalBatch.SelectedRow.RowIndex
          Dim row As GridViewRow = gridApprovalBatch.Rows(selectedRowIndex)
          ViewState("ApprovalBatch") = row.Cells(1).Text.ToString()
          getBatchApplications(row.Cells(1).Text.ToString(), "A", CInt(ViewState("apptypeID")))
		dvBankDetails.Visible = False
		Me.dvMoreDetails.Visible = False

     End Sub

     Protected Sub BindFieldPW()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "txtApplicationCode"
          Me.gridApplications.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "txtPIN"
          gridApplications.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()
          bfieldName.HeaderText = "Full Name"
          bfieldName.DataField = "txtFullName"
          bfieldName.ItemStyle.Width = 150
          gridApplications.Columns.Add(bfieldName)

          Dim bfieldEmployerName As New BoundField()
          bfieldEmployerName.HeaderText = "Employer Name"
          bfieldEmployerName.DataField = "txtEmployerName"
          bfieldEmployerName.ItemStyle.Width = 150
          gridApplications.Columns.Add(bfieldEmployerName)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldValueDate)

          Dim bfieldLumpSum As New BoundField()
          bfieldLumpSum.HeaderText = "LumpSum"
          bfieldLumpSum.DataField = "recommended-lumpsum"
          bfieldLumpSum.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldLumpSum)

          Dim bfieldPension As New BoundField()
          bfieldPension.HeaderText = "Pension"
          bfieldPension.DataField = "monthly-programed-drawndown"
          bfieldPension.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldPension)

          Dim bfieldArrears As New BoundField()
          bfieldArrears.HeaderText = "Arrears"
          bfieldArrears.DataField = "Arrears"
          bfieldArrears.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldArrears)

          Dim bfieldLumpSumToPay As New BoundField()
          bfieldLumpSumToPay.HeaderText = "LumpSum ToPay"
          bfieldLumpSumToPay.DataField = "LumpSumToPay"
          bfieldLumpSumToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldLumpSumToPay)

          Dim bfieldPensionToPay As New BoundField()
          bfieldPensionToPay.HeaderText = "Pension ToPay"
          bfieldPensionToPay.DataField = "PensionToPay"
          bfieldPensionToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldPensionToPay)

          Dim bfieldArrearsToPay As New BoundField()
          bfieldArrearsToPay.HeaderText = "Arrears ToPay"
          bfieldArrearsToPay.DataField = "ArrearsToPay"
          bfieldArrearsToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldArrearsToPay)

     End Sub

     Protected Sub BindFieldAnnuity()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "txtApplicationCode"
          Me.gridApplications.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "txtPIN"
          gridApplications.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()
          bfieldName.HeaderText = "Full Name"
          bfieldName.DataField = "txtFullName"
          bfieldName.ItemStyle.Width = 150
          gridApplications.Columns.Add(bfieldName)

          Dim bfieldEmployerName As New BoundField()
          bfieldEmployerName.HeaderText = "Employer Name"
          bfieldEmployerName.DataField = "txtEmployerName"
          bfieldEmployerName.ItemStyle.Width = 150
          gridApplications.Columns.Add(bfieldEmployerName)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
		gridApplications.Columns.Add(bfieldValueDate)


		Dim bfieldPremium As New BoundField()
		bfieldPremium.HeaderText = "Premium"
		bfieldPremium.DataField = "premium"
		bfieldPremium.DataFormatString = "{0:N}"
		gridApplications.Columns.Add(bfieldPremium)

		'premium

          Dim bfieldLumpSum As New BoundField()
          bfieldLumpSum.HeaderText = "LumpSum"
          bfieldLumpSum.DataField = "lumpsum"
          bfieldLumpSum.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldLumpSum)

          Dim bfieldAnnuity As New BoundField()
          bfieldAnnuity.HeaderText = "Annuity"
          bfieldAnnuity.DataField = "monthly-annuity"
          bfieldAnnuity.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldAnnuity)

          Dim bfieldLumpSumToPay As New BoundField()
          bfieldLumpSumToPay.HeaderText = "LumpSum ToPay"
          bfieldLumpSumToPay.DataField = "LumpSumToPay"
          bfieldLumpSumToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldLumpSumToPay)

          Dim bfieldAnnuityToPay As New BoundField()
          bfieldAnnuityToPay.HeaderText = "Annuity ToPay"
          bfieldAnnuityToPay.DataField = "AnnuityToPay"
          bfieldAnnuityToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldAnnuityToPay)


     End Sub

     Protected Sub BindField()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "txtApplicationCode"
          Me.gridApplications.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "txtPIN"
          gridApplications.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()
          bfieldName.HeaderText = "Full Name"
          bfieldName.DataField = "txtFullName"
          bfieldName.ItemStyle.Width = 150
          gridApplications.Columns.Add(bfieldName)

          Dim bfieldEmployerName As New BoundField()
          bfieldEmployerName.HeaderText = "Employer Name"
          bfieldEmployerName.DataField = "txtEmployerName"
          bfieldEmployerName.ItemStyle.Width = 150
          gridApplications.Columns.Add(bfieldEmployerName)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldValueDate)

          Dim bfieldApprovedAmount As New BoundField()
          bfieldApprovedAmount.HeaderText = "Approved Amount"
          bfieldApprovedAmount.DataField = "ApprovedAmount"
          bfieldApprovedAmount.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldApprovedAmount)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldAmountToPay)


     End Sub


     Private Sub getBatchApplications(BatchApprovalCode As String, txtStatus As String, typeID As Integer)

		Dim cr As New Core, dt As New DataTable
		Try

			dt = cr.PMgetPencomApprovalBatchApplication(BatchApprovalCode, txtStatus, typeID)
			ViewState("BatchApprovalCode") = BatchApprovalCode
			ViewState("BatchApplications") = dt
			BindGridApplication(dt, typeID)

		Catch ex As Exception

		End Try
          


     End Sub


     Protected Sub BindGridApplication(dt As DataTable, typeID As Integer)

          Dim dtApprovalPINs As New DataTable, dtColumn As New DataColumn, i As Integer
          Try

               If IsNothing(ViewState("BatchApprovalCode")) = False Then

                    dtColumn = New DataColumn("txtApplicationCode")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    dtColumn = New DataColumn("txtPIN")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    dtColumn = New DataColumn("txtFullName")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    dtColumn = New DataColumn("txtEmployerName")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    dtColumn = New DataColumn("ValueDate")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    If typeID = 3 Then

                         dtColumn = New DataColumn("recommended-lumpsum")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("monthly-programed-drawndown")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("Arrears")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("LumpSumToPay")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("PensionToPay")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("ArrearsToPay")
                         dtApprovalPINs.Columns.Add(dtColumn)

                    ElseIf typeID = 4 Then

					dtColumn = New DataColumn("premium")
					dtApprovalPINs.Columns.Add(dtColumn)

					dtColumn = New DataColumn("lumpsum")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("monthly-annuity")

                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("LumpSumToPay")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("AnnuityToPay")
					dtApprovalPINs.Columns.Add(dtColumn)

				ElseIf typeID = 15 Then

					dtColumn = New DataColumn("lumpsum")
					dtApprovalPINs.Columns.Add(dtColumn)

					dtColumn = New DataColumn("monthly-annuity")
					dtApprovalPINs.Columns.Add(dtColumn)

					dtColumn = New DataColumn("LumpSumToPay")
					dtApprovalPINs.Columns.Add(dtColumn)

					dtColumn = New DataColumn("AnnuityToPay")
					dtApprovalPINs.Columns.Add(dtColumn)
                         


                    Else

                         dtColumn = New DataColumn("ApprovedAmount")
                         dtApprovalPINs.Columns.Add(dtColumn)
                         dtColumn = New DataColumn("AmountToPay")
                         dtApprovalPINs.Columns.Add(dtColumn)
                    End If


               Else
               End If

          Do While i < dt.Rows.Count

               Dim newCustomersRow As DataRow
               newCustomersRow = dtApprovalPINs.NewRow()
                    newCustomersRow("txtApplicationCode") = dt.Rows(i).Item("txtApplicationCode")
               newCustomersRow("txtPIN") = dt.Rows(i).Item("txtPIN")
               newCustomersRow("txtFullName") = dt.Rows(i).Item("txtFullName")
               newCustomersRow("txtEmployerName") = dt.Rows(i).Item("txtEmployerName")
                    newCustomersRow("ValueDate") = Convert.ToDateTime(dt.Rows(i).Item("ValueDate")).ToString("yyyy-MM-dd")
               If typeID = 3 Then

					newCustomersRow("recommended-lumpsum") = dt.Rows(i).Item("recommended-lumpsum")
					newCustomersRow("monthly-programed-drawndown") = dt.Rows(i).Item("monthly-programed-drawndown")

                    newCustomersRow("Arrears") = dt.Rows(i).Item("Arrears")

                    newCustomersRow("LumpSumToPay") = dt.Rows(i).Item("LumpSumToPay")
                    newCustomersRow("PensionToPay") = dt.Rows(i).Item("PensionToPay")
                    newCustomersRow("ArrearsToPay") = dt.Rows(i).Item("ArrearsToPay")

                    ElseIf typeID = 4 Then
					newCustomersRow("premium") = dt.Rows(i).Item("Premium")
                         newCustomersRow("lumpsum") = dt.Rows(i).Item("lumpsum")
                         newCustomersRow("monthly-annuity") = dt.Rows(i).Item("monthly-annuity")

					'	newCustomersRow("LumpSumToPay") = CDbl((CDbl(dt.Rows(i).Item("MandatoryRF")) - CDbl(dt.Rows(i).Item("RSABalance"))))
					newCustomersRow("LumpSumToPay") = CDbl(dt.Rows(i).Item("Increment"))
					'Increment
					'LumpSumToPay
					newCustomersRow("AnnuityToPay") = dt.Rows(i).Item("AnnuityToPay")

				ElseIf typeID = 15 Then

					newCustomersRow("lumpsum") = dt.Rows(i).Item("lumpsum")
					newCustomersRow("monthly-annuity") = dt.Rows(i).Item("monthly-annuity")

					newCustomersRow("LumpSumToPay") = dt.Rows(i).Item("LumpSumToPay")
					newCustomersRow("AnnuityToPay") = dt.Rows(i).Item("AnnuityToPay")


                    Else

                         newCustomersRow("ApprovedAmount") = dt.Rows(i).Item("ApprovedAmount")
                         newCustomersRow("AmountToPay") = dt.Rows(i).Item("AmountToPay")

                    End If


               '
               dtApprovalPINs.Rows.Add(newCustomersRow)

               i = i + 1
          Loop

			
			ViewState("ApprovalPIN") = dtApprovalPINs
          gridApplications.DataSource = dtApprovalPINs
          gridApplications.DataBind()

			If dtApprovalPINs.Rows.Count > 10 Then
				Me.Panel1.Height = Nothing
			Else
			End If

          Catch ex As Exception

          End Try
     End Sub

     Public Function getBanks() As List(Of String)

		Try

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

		Catch ex As Exception

		End Try

     End Function

     'Populating bank List
	'    Protected Sub populateBank()
	'         Dim myState As New States, i As Integer = 0
	'	Dim lstBank As New List(Of String)
	'	Try

	'		lstBank = getBanks()
	'		Me.ddBankName.Items.Clear()

	'		Do While i < lstBank.Count

	'			If Me.ddBankName.Items.Count = 0 Then
	'				Me.ddBankName.Items.Add("")
	'				Me.ddBankName.Items.Add(lstBank.Item(i))
	'			ElseIf Me.ddBankName.Items.Count > 0 Then
	'				Me.ddBankName.Items.Add(lstBank.Item(i))
	'			End If
	'			i = i + 1

	'		Loop

	'	Catch ex As Exception

	'	End Try
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










     Protected Sub Reset_Click(sender As Object, e As EventArgs)

          Dim cr As New Core
          Dim btnResetUnprocessed As New ImageButton, BatchApprovalCode As String = ""
          btnResetUnprocessed = sender
          Dim i As GridViewRow
          i = btnResetUnprocessed.NamingContainer
          cr.PMApprovalPayeeStatus(Me.gridApplications.Rows(i.RowIndex).Cells(5).Text, Session("user"), "P", 0)

          If IsNothing(ViewState("BatchApprovalCode")) = False Then
               getBatchApplications(CStr(ViewState("BatchApprovalCode")), "A", CInt(ViewState("apptypeID")))
          Else
          End If


     End Sub



     Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

          For Each grow As GridViewRow In Me.gridApplications.Rows

               If grow.Enabled = True Then
                    Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkApprovalConfirm"), CheckBox)
                    cb.Checked = True
               Else
               End If

          Next

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

          For Each grow As GridViewRow In Me.gridApplications.Rows

               If grow.Enabled = True Then
                    Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkApprovalConfirm"), CheckBox)
                    cb.Checked = False
               Else
               End If
          Next

     End Sub



     Private Sub generateSchedule()

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, dt As New DataTable, lstAppSchedules As New List(Of ApplicationDetail)

          Try

               For Each grow As GridViewRow In Me.gridApplications.Rows

                    cb = grow.FindControl("ChkApprovalConfirm")

                    If cb.Checked = True Then

                         Dim lstAppSchedule As New ApplicationDetail

                         lstAppSchedule.ApplicationID = grow.Cells(2).Text.ToString
                         lstAppSchedule.IsScheduleGenerated = 1
                         lstAppSchedule.DateConfirmed = Now
                         lstAppSchedules.Add(lstAppSchedule)

                    ElseIf cb.Checked = False Then

                    End If

               Next

               If lstAppSchedules.Count > 0 Then
                    cr.PMGenerateApprovalSchedule(lstAppSchedules)
               End If

               'Refresh()


          Catch ex As Exception

          End Try

     End Sub
     Private Function populateSchedule(approvalBatchNum As String) As DataSet

          Dim cr As New Core, dtApplicationSchedule As New DataTable, i As Integer = 0
          dtApplicationSchedule = cr.PMgetApprovalPaymentSchedule(approvalBatchNum, "", 0)
          Dim ds As New dsApprovalSchedule
          Dim newSNRow As DataRow

          newSNRow = ds.Tables(0).NewRow

          newSNRow("Name") = dtApplicationSchedule.Rows(0).Item("txtFullName")
          newSNRow("Pin") = dtApplicationSchedule.Rows(0).Item("txtpin")
          newSNRow("Sector") = dtApplicationSchedule.Rows(0).Item("txtSector")
          newSNRow("PlatForm") = dtApplicationSchedule.Rows(0).Item("PlatForm")
          newSNRow("ApprovalDate") = dtApplicationSchedule.Rows(0).Item("dteApproval")
          newSNRow("ApprovedAmount") = dtApplicationSchedule.Rows(0).Item("numApproved")
          newSNRow("LumpSumAmount") = dtApplicationSchedule.Rows(0).Item("numApproved")

          newSNRow("AccountName") = dtApplicationSchedule.Rows(0).Item("txtAccountName")
          newSNRow("BankDetails") = dtApplicationSchedule.Rows(0).Item("BankDetails")
          newSNRow("AccountNo") = dtApplicationSchedule.Rows(0).Item("txtAccountNo")
          newSNRow("Remarks") = dtApplicationSchedule.Rows(0).Item("txtRemarks")

          newSNRow("PreparedBy") = dtApplicationSchedule.Rows(0).Item("txtCreatedBy")
          newSNRow("CheckedBy") = dtApplicationSchedule.Rows(0).Item("txtConfirmedBy")
          newSNRow("VerifiedBy") = dtApplicationSchedule.Rows(0).Item("txtConfirmedBy")
          newSNRow("AuthorizedBy") = dtApplicationSchedule.Rows(0).Item("txtConfirmedBy")


          ds.Tables(0).Rows.Add(newSNRow)
          Return ds

          'MsgBox("" & ds.Tables(0).Rows.Count)

          '          Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & Year(Me.txtStartDate.Text) & "" & Month(Me.txtStartDate.Text) & "" & Day(Me.txtStartDate.Text) & "_" & Me.dcFund.SelectedValue & "_" & brokers.Item(i) & ".pdf"





     End Function
     Protected Sub btnGenerateSchedule_Click(sender As Object, e As EventArgs) Handles btnGenerateSchedule.Click


          If IsNothing(ViewState("ApprovalBatch")) = False Then
               Dim batchNum As String = ViewState("ApprovalBatch")
               ' Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & batchNum & ".pdf"
               Dim filePath As String = Server.MapPath("~/FileDownLoads/" & batchNum & ".pdf")
               generateFiles(batchNum, filePath)
          Else

          End If

     End Sub

     Private Sub generateFiles(approvalBatchNum As String, path As String)

          Dim crExportOptions As New ExportOptions
          Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
          Dim crFormatypeOption As New PdfRtfWordFormatOptions
          Dim rdoc As New ReportDocument
          Dim rsource As New CrystalDecisions.Web.CrystalReportSource

          rdoc.Load(Server.MapPath("~/Report/ApprovalPaymentSchedule.rpt"))


          Dim ds As DataSet
          ds = populateSchedule(approvalBatchNum)
          rdoc.SetDataSource(ds.Tables(0))

          crDiskFileDestinationOptions.DiskFileName = path
          crExportOptions = rdoc.ExportOptions

          With crExportOptions

               .ExportDestinationType = ExportDestinationType.DiskFile
               .ExportFormatType = ExportFormatType.PortableDocFormat
               .ExportDestinationOptions = crDiskFileDestinationOptions
               .ExportFormatOptions = crFormatypeOption

          End With

          rdoc.Export()

     End Sub



     Protected Sub btnNewApproval_Click(sender As Object, e As EventArgs) Handles btnNewApproval.Click

          Response.Redirect("~/frmApplicationApprovals.aspx")

     End Sub

     Protected Sub btnRevalidate_Click(sender As Object, e As EventArgs) Handles btnRevalidate.Click

		If IsNothing(Session("user")) = True Then
			Response.Redirect("login.aspx")
		Else
		End If

		Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)

          For Each grow As GridViewRow In Me.gridApplications.Rows

               ' If grow.Enabled = True Then
               Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkApprovalConfirm"), CheckBox)

               If cb.Checked = True Then
                    Dim cr As New Core

                    ' cr.PMApprovalPayeeStatus(Me.gridApplications.Rows(grow.RowIndex).Cells(2).Text, CStr(Session("user")), "R", 1)
				cr.PMApprovalRevaluation(Me.gridApplications.Rows(grow.RowIndex).Cells(5).Text, Me.gridApplications.Rows(grow.RowIndex).Cells(6).Text, Session("user"), apptypeID)


               Else
               End If

               ' cb.Checked = True
               'Else
               'End If

          Next

		If IsNothing(ViewState("BatchApprovalCode")) = False Then

			getBatchApplications(CStr(ViewState("BatchApprovalCode")), "A", CInt(ViewState("apptypeID")))
		Else

		End If
     End Sub

     Protected Sub btnForApproval_Click(sender As Object, e As EventArgs) Handles btnForApproval.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, lstApprovals As New List(Of PencomApprovalDetails), dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail)

          Try

               For Each grow As GridViewRow In Me.gridApplications.Rows

                    cb = grow.FindControl("ChkApprovalConfirm")


                    If cb.Checked = True And (grow.ForeColor = Drawing.Color.Green) = False Then

                         'P=pending,F= for confirmation, C =Confirmed, E= Exported to Enpower

                         Dim lstApproval As New PencomApprovalDetails
                         ViewState("BatchApprovalCode") = grow.Cells(5).Text.ToString
                         lstApproval.PencomBatch = grow.Cells(5).Text.ToString
                         lstApproval.ConfirmedDate = Now.Date
                         lstApproval.ConfirmedBy = Session("user")
                         lstApproval.ApplicationCode = grow.Cells(5).Text.ToString
                         lstApproval.Status = "F"
                         lstApprovals.Add(lstApproval)

                    ElseIf cb.Checked = False Then

                    End If

               Next

               If lstApprovals.Count > 0 Then

                    cr.PMConfirmPencomApproval(lstApprovals)

               End If


               If IsNothing(ViewState("ApprovalBatch")) = False Then
                    getBatchApplications(CStr(ViewState("ApprovalBatch")), "A", CInt(ViewState("apptypeID")))
               Else
               End If

               'Refresh()


          Catch ex As Exception

          End Try

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

	Protected Sub gridApplications_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridApplications.PageIndexChanging

		'ViewState("ApprovalPIN") = dtApprovalPINs

		Dim dtApprovalPINs As New DataTable
		Try

			gridApplications.PageIndex = e.NewPageIndex
			dtApprovalPINs = ViewState("ApprovalPIN")

			If IsNothing(dtApprovalPINs) = False Then

				gridApplications.DataSource = dtApprovalPINs
				gridApplications.DataBind()

				If dtApprovalPINs.Rows.Count > 10 Then
					Me.Panel1.Height = Nothing
				Else
				End If

			End If

		Catch ex As Exception

		End Try

	End Sub

     

     Protected Sub gridApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.SelectedIndexChanged

          Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
          Dim selectedRowIndex As Integer
          Try

          
          ddBankName.SelectedIndex = 0
               ddBankBranch.Items.Clear()

          selectedRowIndex = Me.gridApplications.SelectedRow.RowIndex

               Dim row As GridViewRow = gridApplications.Rows(selectedRowIndex)
               ViewState("ApplicationID") = row.Cells(5).Text.ToString()

               dt = cr.PMgetApplicationByCode(row.Cells(5).Text.ToString())

          Me.txtAccountName.Text = (dt.Rows(0).Item("txtAccountName")).ToString
          Me.txtAccountNumber.Text = (dt.Rows(0).Item("txtAccountNo")).ToString
			Me.txtBVN.Text = dt.Rows(0).Item("txtBVN").ToString


			If Not dt.Rows(0).Item("fkiBankID") Is DBNull.Value And cr.PMgetBanks(CInt(dt.Rows(0).Item("fkiBankID"))).Rows.Count > 0 Then
				Me.ddBankName.SelectedValue = cr.PMgetBanks(CInt(dt.Rows(0).Item("fkiBankID"))).Rows(0).Item("bankname")
			Else
				ddBankName.SelectedItem.Text = ""
			End If

			'            If Not dt.Rows(0).Item("fkiBranchID") Is DBNull.Value Then
			'                 Dim lstBankBranches As New List(Of String)
			'                 lstBankBranches = getBankBranches(CInt(dt.Rows(0).Item("fkiBankID")))
			'                 lstBankBranches = Nothing
			'                 Me.ddBankBranch.Items.Add(Me.getBankBranchName(CInt(dt.Rows(0).Item("fkiBranchID"))).ToString)
			'            ElseIf ddBankBranch.Items.Count > 0 Then
			'                 ddBankBranch.SelectedItem.Text = ""
			'            Else
			'End If


			If Not dt.Rows(0).Item("fkiBranchID") Is DBNull.Value And cr.PMgetBankBranches(CInt(dt.Rows(0).Item("fkiBankID")), CInt(dt.Rows(0).Item("fkiBranchID"))).Rows.Count > 0 Then
				Dim lstBankBranches As New DataTable
				lstBankBranches = cr.PMgetBankBranches(CInt(dt.Rows(0).Item("fkiBankID")), CInt(dt.Rows(0).Item("fkiBranchID")))
				'lstBankBranches = Nothing
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(0).Item("BranchName") & "                   | " & lstBankBranches.Rows(0).Item("BankBranchID"))
			ElseIf ddBankBranch.Items.Count > 0 Then
				ddBankBranch.SelectedItem.Text = ""
			Else

			End If


			dvBankDetails.Visible = True


			dtDocuments = cr.PMgetSubmittedDocument(row.Cells(6).Text.ToString(), CStr(row.Cells(5).Text.ToString()))
			ViewState("AppCode") = CStr(row.Cells(5).Text.ToString())
			populateDocuments(dtDocuments)

			dvGridRecievedDocument.Visible = True

               ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(5).Text.ToString(), row.Cells(6).Text.ToString())
               Session("lodgmentProperties") = ApplicationProperties

               populateProperties(ApplicationProperties)


               If ApplicationProperties.Count < 10 Then
                    pnlLeftGrid.Height = 400
               Else
                    pnlLeftGrid.Height = Nothing
               End If
               dvMoreDetails.visible = True

          Catch ex As Exception
               ' MsgBox("" & ex.Message)

          End Try


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


     Protected Sub populateProperties(lodgmentProperties As List(Of ApplicationProperties))
          Try


               Me.gridProperties.DataSource = lodgmentProperties
               Me.gridProperties.DataBind()

               If lodgmentProperties.Count > 0 Then
                    pnlLeftGrid.Height = Nothing
               Else
                    pnlLeftGrid.Height = 475
               End If


          Catch ex As Exception

          End Try

     End Sub

     'handles the click event of the comment button on the grid
     Protected Sub AddViewComment_Click(sender As Object, e As EventArgs) Handles btnShowAppPINCommentPopup.Click
          '''''''''''''''''''''''''''''''''''''''''application comments''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
          Dim btnAddViewComment As New ImageButton, dt As DataTable, j As Integer
          Try

          
          btnAddViewComment = sender

          Dim i As GridViewRow, cr As New Core
          i = btnAddViewComment.NamingContainer
          ' MsgBox("" & i.RowIndex)
          'Me.txtApplicationID.Text = Me.gridApplications.Rows(i.RowIndex).Cells(5).Text
          'dt = cr.PMgetApplicationComment(Me.gridApplications.Rows(i.RowIndex).Cells(5).Text, "PRE")
          'lstComments.Items.Clear()
          'Do While j < dt.Rows.Count

          '     lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
          '     j = j + 1

          'Loop
          'pops up the comment dialogue
          ' mpAppComments.Show()
          '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

               ' MsgBox("" & Me.gridApplications.Rows(i.RowIndex).Cells(5).Text)

               dt = cr.PMgetApprovalPerson(Me.gridApplications.Rows(i.RowIndex).Cells(5).Text)
          Me.txtAppCodePINComments.Text = Me.gridApplications.Rows(i.RowIndex).Cells(5).Text
          Me.txtApprovalPINComments.Text = dt.Rows(0).Item("txtPaymentRemarks")

               Me.mpAppPINComments.Show()
          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try

     End Sub

     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewApplicationLog As New ImageButton, appCode As String
          btnViewApplicationLog = sender
          Dim i As GridViewRow
          i = btnViewApplicationLog.NamingContainer
          appCode = Me.gridApplications.Rows(i.RowIndex).Cells(5).Text

          Dim typeID As Integer
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")

          typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApplicationType.SelectedItem.Text)))

          Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1, Server.UrlEncode("frmApplicationApprovaList")))


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

     Protected Sub ddBankName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddBankName.SelectedIndexChanged

		'BankTypeCollection = ViewState("BankTypeCollection")
		'Dim lstBankBranches As New List(Of String), i As Integer = 0
		'lstBankBranches = getBankBranches(CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text)))

		'Me.ddBankBranch.Items.Clear()
		'Do While i < lstBankBranches.Count

		'     If Me.ddBankBranch.Items.Count = 0 Then
		'          Me.ddBankBranch.Items.Add("")
		'          Me.ddBankBranch.Items.Add(lstBankBranches.Item(i))
		'     ElseIf Me.ddBankBranch.Items.Count > 0 Then
		'          Me.ddBankBranch.Items.Add(lstBankBranches.Item(i))
		'     End If

		'     i = i + 1

		'Loop

		BankTypeCollection = ViewState("BankTypeCollection")
		Dim lstBankBranches As New DataTable, i As Integer = 0, cr As New Core
		lstBankBranches = cr.PMgetBankBranches(CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text)))

		Me.ddBankBranch.Items.Clear()
		Do While i < lstBankBranches.Rows.Count

			If Me.ddBankBranch.Items.Count = 0 Then
				Me.ddBankBranch.Items.Add("")
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
			ElseIf Me.ddBankBranch.Items.Count > 0 Then
				Me.ddBankBranch.Items.Add(lstBankBranches.Rows(i).Item("BranchName") & "                   | " & lstBankBranches.Rows(i).Item("BankBranchID"))
			End If

			i = i + 1

		Loop

     End Sub
     'this refresheas all the comments added on the application prior approval from the regulator on the user interface
     Protected Sub refreshCommentList(appCode As String)
          Dim cr As New Core, j As Integer, dt As DataTable
          dt = cr.PMgetApplicationComment(appCode, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop
          mpAppComments.Show()

     End Sub
     'adding comments to the application 
     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click

          Dim cr As New Core
          cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1)
          txtApplicationComment.Text = ""
          refreshCommentList(txtApplicationID.Text)

     End Sub

     Protected Sub btnAppCommentRemove_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentRemove.Click

          Dim cr As New Core
          Try
               If IsNothing(Session("user")) = False Then
                    Dim UName As String = CStr(Session("user"))
                    Dim str() As String = Me.lstComments.SelectedItem.Text.Split(":")
                    If UName = LTrim(RTrim(CStr(str(1)))) Then

                         cr.PMRemoveComment(CInt(str(0)))
                         refreshCommentList(txtApplicationID.Text)

                    Else
                         Me.mpAppComments.Show()
                    End If
               Else
                    Me.mpAppComments.Show()
               End If

          Catch ex As Exception

          End Try

     End Sub
     Public Function getBankBranchID(Name As String) As Integer

          Dim lstBankBranchName As New List(Of String)
          Dim dc As New BanksDataContext
          Dim querys = From m In dc.BankBranches
                      Where m.BranchName = Name
                      Select New With {m.BankBranchID}

          For Each m In querys
               lstBankBranchName.Add(m.BankBranchID)
          Next

          If (lstBankBranchName.Count > 0) Then
               Return CInt(lstBankBranchName(0))
          Else
               Return Nothing
          End If

     End Function
     'updating the bank detail information of the customer
     Protected Sub btnUpdateBankDetails_Click(sender As Object, e As EventArgs) Handles btnUpdateBankDetails.Click
          Dim appDetail As New ApplicationDetail, cr As New Core
          Try

               If IsNothing(ViewState("ApplicationID")) = False Then

                    appDetail.ApplicationID = CStr(ViewState("ApplicationID"))
                    appDetail.AccountName = Me.txtAccountName.Text
                    appDetail.AccountNo = Me.txtAccountNumber.Text
                    appDetail.BVN = Me.txtBVN.Text

				If Not IsNothing(ViewState("BankTypeCollection")) = True Then
					BankTypeCollection = ViewState("BankTypeCollection")
					appDetail.BankID = CInt(BankTypeCollection.Item(Me.ddBankName.SelectedItem.Text.ToString))
				Else
				End If

				'                If Not IsNothing(ViewState("BankBranchCollection")) = True Then
				'                     BankBranchCollection = ViewState("BankBranchCollection")
				'                     appDetail.BranchID = getBankBranchID(Me.ddBankBranch.SelectedItem.Text.ToString)
				'                Else
				'End If


				If Me.ddBankBranch.SelectedItem.Text.ToString <> "" Then
					'BankBranchCollection = ViewState("BankBranchCollection")
					appDetail.BranchID = CInt(Me.ddBankBranch.SelectedItem.Text.ToString.Split("|")(1))
				Else
				End If


                    cr.PMUpdateBankDetails(appDetail, Session("user"))

                    Me.txtAccountName.Text = ""
                    Me.txtAccountNumber.Text = ""
                    Me.txtBVN.Text = ""
                    ddBankName.SelectedIndex = 0
                    ddBankBranch.Items.Clear()

               Else

               End If

          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message & "Bank Details Update")
          End Try

     End Sub

     Protected Sub btnMPAppPINCommentsOK_Click(sender As Object, e As EventArgs) Handles btnMPAppPINCommentsOK.Click
          Try

          
          Dim cr As New Core
          cr.PMUpdateApprovalPersonComment(Me.txtApprovalPINComments.Text, Me.txtAppCodePINComments.Text)
          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message & "Updating Approval PIN Remarks")
          End Try
     End Sub

	Protected Sub btnExportSchedule_Click(sender As Object, e As EventArgs) Handles btnExportSchedule.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, ApprovalDetails As New PencomApprovalDetails, dt As New DataTable, lstApprovedApps As New List(Of ApplicationDetail), typeID As Integer

		typeID = CInt(ViewState("apptypeID"))

		If Not IsNothing(CInt(ViewState("apptypeID"))) = True Then

			'Me.mpEditEnblocApproval.Show()

			For Each grow As GridViewRow In Me.gridApplications.Rows

				cb = grow.FindControl("ChkApprovalConfirm")

				If cb.Checked = True And typeID = 3 And cb.Enabled = True Then

					Me.txtPWApplicationCode.Text = grow.Cells(5).Text

					Me.txtPWLumpSumAmount.Text = grow.Cells(13).Text
					Me.txtPWPensionAmount.Text = grow.Cells(14).Text
					Me.txtPWArrears.Text = grow.Cells(15).Text

					Me.txtPWApprovedLumpSumAmount.Text = grow.Cells(10).Text
					Me.txtPWApprovedPensionAmount.Text = grow.Cells(11).Text
					ViewState("PIN") = grow.Cells(6).Text

					chk = chk + 1

				ElseIf cb.Checked = True And typeID = 4 And cb.Enabled = True Then

					Me.txtAnnApplicationCode.Text = grow.Cells(5).Text
					Me.txtAnnLumpSumAmount.Text = grow.Cells(10).Text
					Me.txtAnnAnnuityAmount.Text = grow.Cells(11).Text
					txtAnnLumpSumApprovedAmount.Text = grow.Cells(13).Text
					txtAnnAnnuityApprovedAmount.Text = grow.Cells(14).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

					'additional annuity application
				ElseIf cb.Checked = True And typeID = 15 And cb.Enabled = True Then

					Me.txtAnnApplicationCode.Text = grow.Cells(5).Text
					Me.txtAnnLumpSumAmount.Text = grow.Cells(10).Text
					Me.txtAnnAnnuityAmount.Text = grow.Cells(11).Text
					txtAnnLumpSumApprovedAmount.Text = grow.Cells(12).Text
					txtAnnAnnuityApprovedAmount.Text = grow.Cells(13).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

					'additional enbloc application
				ElseIf cb.Checked = True And typeID = 1 And cb.Enabled = True Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(5).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(10).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(11).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

					'additional enbloc application
				ElseIf cb.Checked = True And typeID = 16 And cb.Enabled = True Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(5).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(10).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(11).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

					'using the pop screen for enbloc to modify the approved and amountTo Pay for DBA payment by setting values on the text fields

				ElseIf cb.Checked = True And typeID = 5 And cb.Enabled = True Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(5).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(10).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(11).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1


					'using the pop screen for enbloc to modify the approved and amountTo Pay for NSITF payment by setting values on the text fields
				ElseIf cb.Checked = True And typeID = 6 And cb.Enabled = True Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(5).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

					'using the pop screen for AVC to modify the approved and amountTo Pay for NSITF payment by setting values on the text fields
				ElseIf cb.Checked = True And typeID = 7 And cb.Enabled = True Then

					Me.txtAVCApplicationCode.Text = grow.Cells(5).Text
					Me.txtAVCApprovedAmount.Text = grow.Cells(10).Text
					Me.txtAVCAmountToPay.Text = grow.Cells(11).Text
					Me.dvAVCInterest.Visible = False
					ViewState("PIN") = grow.Cells(6).Text

					chk = chk + 1
					'using the pop screen for Legacy to modify the approved and amountTo Pay for NSITF payment by setting values on the text fields
				ElseIf cb.Checked = True And typeID = 8 And cb.Enabled = True Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(5).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(5).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(6).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

					'using the pop screen for employee portion to modify the approved and amountTo Pay for Employee portion payment by setting values on the text fields

				ElseIf cb.Checked = True And typeID = 11 And cb.Enabled = True Then

					Me.txtEnblocApplicationCode.Text = grow.Cells(5).Text
					Me.txtEnblocApprovedAmount.Text = grow.Cells(10).Text
					Me.txtEnblocAmountToPay.Text = grow.Cells(11).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

				ElseIf cb.Checked = True And typeID <> 3 And cb.Enabled = True Then

					Me.txtApplicationCode.Text = grow.Cells(5).Text
					txtEditApprovedAmount.Text = grow.Cells(11).Text
					ViewState("PIN") = grow.Cells(6).Text
					chk = chk + 1

				ElseIf cb.Checked = False Then

				Else
					chk = 0
				End If

			Next

			If chk = 1 And typeID = 3 Then

				mpEditPWApproval.Show()

			ElseIf chk = 1 And typeID = 4 Then

				mpEditAnnApproval.Show()

			ElseIf chk = 1 And typeID = 15 Then
				mpEditAnnApproval.Show()
			ElseIf chk = 1 And typeID = 1 Then
				mpEditEnblocApproval.Show()
			ElseIf chk = 1 And typeID = 16 Then
				mpEditEnblocApproval.Show()
				'using the pop screen for enbloc to modify the approved and amountTo Pay for DBA payment
			ElseIf chk = 1 And typeID = 5 Then
				mpEditEnblocApproval.Show()
				'using the pop screen for enbloc to modify the approved and amountTo Pay for NSITF payment
			ElseIf chk = 1 And typeID = 6 Then
				mpEditEnblocApproval.Show()
				'using the pop screen for enbloc to modify the approved and amountTo Pay for AVC payment
			ElseIf chk = 1 And typeID = 7 Then
				'mpEditEnblocApproval.Show()
				mpEditAVCApproval.Show()

				'using the pop screen for Legacy to modify the approved and amountTo Pay for NSITF payment
			ElseIf chk = 1 And typeID = 8 Then
				mpEditEnblocApproval.Show()
			ElseIf chk = 1 And typeID = 11 Then
				mpEditEnblocApproval.Show()
			ElseIf chk = 1 Then
				mpEditApprovedAmount.Show()
			End If

		Else

		End If

	End Sub

	Protected Sub btnEditAVCApprovedAmount_Click(sender As Object, e As EventArgs) Handles btnEditAVCApprovedAmount.Click
		Dim cr As New Core, updateDetails As New PencomApprovalPeople
		Try

			updateDetails.ApplicationCode = txtAVCApplicationCode.Text
			updateDetails.AmountToPay = Me.txtAVCAmountToPay.Text
			updateDetails.AppTypeID = CInt(ViewState("apptypeID"))
			updateDetails.PIN = CStr(ViewState("PIN"))

			cr.PMEditAmountToPay(updateDetails)
			getBatchApplications(CStr(ViewState("ApprovalBatch")), "A", updateDetails.AppTypeID)

		Catch ex As Exception

		End Try

		'MsgBox("Approve AVC")

	End Sub

	Protected Sub btnEditEnblocApprovedAmountOk_Click(sender As Object, e As EventArgs) Handles btnEditEnblocApprovedAmountOk.Click

		Dim cr As New Core, updateDetails As New PencomApprovalPeople

		updateDetails.ApplicationCode = txtEnblocApplicationCode.Text
		updateDetails.ApprovedAmount = Me.txtEnblocApprovedAmount.Text
		updateDetails.AmountToPay = Me.txtEnblocAmountToPay.Text
		updateDetails.AppTypeID = CInt(ViewState("apptypeID"))
		updateDetails.PIN = CStr(ViewState("PIN"))

		cr.PMEditAmountToPay(updateDetails)
		getBatchApplications(CStr(ViewState("ApprovalBatch")), "A", updateDetails.AppTypeID)

	End Sub

	Protected Sub btnEditAnnApprovalOK_Click(sender As Object, e As EventArgs) Handles btnEditAnnApprovalOK.Click

		MsgBox("Approve Annuity")

	End Sub

	Protected Sub btnSaveApprovedAmount_Click(sender As Object, e As EventArgs) Handles btnSaveApprovedAmount.Click

		Dim cr As New Core, updateDetails As New PencomApprovalPeople

		updateDetails.ApplicationCode = txtApplicationCode.Text
		updateDetails.AmountToPay = Me.txtEditApprovedAmount.Text
		updateDetails.AppTypeID = CInt(ViewState("apptypeID"))
		updateDetails.PIN = CStr(ViewState("PIN"))

		cr.PMEditAmountToPay(updateDetails)
		getBatchApplications(CStr(ViewState("ApprovalBatch")), "A", updateDetails.AppTypeID)

	End Sub

	Protected Sub btnEditPWApprovalOK_Click(sender As Object, e As EventArgs) Handles btnEditPWApprovalOK.Click

		Dim cr As New Core, updateDetails As New PencomApprovalPeople

		updateDetails.ApplicationCode = txtPWApplicationCode.Text

		updateDetails.ApprovedAmount = txtPWApprovedLumpSumAmount.Text
		'holding approved pension
		updateDetails.MonthlyDrawndown = txtPWApprovedPensionAmount.Text
		updateDetails.LumpSumToPay = Me.txtPWLumpSumAmount.Text
		updateDetails.MonthlyDrawndownToPay = Me.txtPWPensionAmount.Text
		updateDetails.ArearsToPay = Me.txtPWArrears.Text

		updateDetails.AppTypeID = CInt(ViewState("apptypeID"))
		updateDetails.PIN = CStr(ViewState("PIN"))

		cr.PMEditAmountToPay(updateDetails)
		getBatchApplications(CStr(ViewState("ApprovalBatch")), "A", updateDetails.AppTypeID)


	End Sub

	Protected Sub imgExport_Click(sender As Object, e As ImageClickEventArgs) Handles imgExport.Click

		If IsNothing(ViewState("BatchApplications")) = False Then
			Dim dtApps As New DataTable, cr As New Core
			dtApps = ViewState("BatchApplications")
			cr.ExtractCSV(dtApps, "ApplicationList")
		Else
		End If


	End Sub
End Class
