Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine


Partial Class frmProcessing
    Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable

     Protected Function getAVCDetails(AVCTax As Double, AVCNoTax As Double, AVCTaxUnit As Decimal, AVCNoTaxUnit As Decimal, payPrice As Decimal) As AVCDetails

		Dim avD As New AVCDetails, curNoTaxAVC As Double, curTaxAVC As Double, TotalTax As Decimal, cr As New Core
		curTaxAVC = CDec(Me.txtAVCUnits.Text) * CDec(Me.txtPayingPrice.Text)

          curNoTaxAVC = CDec(Me.txtNoTAXAVCUnits.Text) * CDec(Me.txtPayingPrice.Text)

		' TotalTax = cr.PMGetAVCTax(((curNoTaxAVC - AVCNoTax)) + cr.PMGetAVCTax((curTaxAVC - AVCTax))) ---- Old



		'TotalTax = cr.PMGetAVCTax(((curNoTaxAVC - AVCNoTax)))

          TotalTax = TotalTax + cr.PMGetAVCTax((curTaxAVC - AVCTax))


		TotalTax = Decimal.Round(TotalTax, 2)

          avD.AVCCurrentValue = curNoTaxAVC + curTaxAVC
          avD.AVCTaxDeduction = TotalTax

          If TotalTax > 0 Then
               avD.AVCNetPayable = (curNoTaxAVC + curTaxAVC) - TotalTax
		ElseIf TotalTax < 0 Or TotalTax = 0 Then
			avD.AVCNetPayable = (curNoTaxAVC + curTaxAVC)
          End If


          'PMGetAVCTax

          Return avD

     End Function

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

          Dim scriptManagerA As New ScriptManager, scriptManagerB As New ScriptManager, dtusers As New DataTable
          scriptManagerA = ScriptManager.GetCurrent(Me.Page)
          scriptManagerB = ScriptManager.GetCurrent(Me.Page)
          scriptManagerA.RegisterPostBackControl(Me.btnSNR)
          scriptManagerB.RegisterPostBackControl(Me.gridSubmittedDocuments)

          Try

               If IsPostBack = False Then

                    If IsNothing(Session("user")) = True Then

                         Response.Redirect("Login.aspx")

                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then
					Dim cr As New Core
                         dtusers = Session("userDetails")
                         getUserAccessMenu(Session("user"))
                         getApprovalTypes()
                         PopulateApplicationStatus()
					getApplicationForProcessing(0)

					Me.gridApplicationSummary.DataSource = cr.PMgetApplicationSummaryByStage("Processing")
					gridApplicationSummary.DataBind()
					mpApplicationSummary.Show()

                    End If

               Else

                    getUserAccessMenu(Session("user"))

               End If

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub PopulateApplicationStatus()

          ddProcessingStatusSingle.Items.Add("")
          ' ddProcessingStatusSingle.Items.Add("Documentation")
          ' ddProcessingStatusSingle.Items.Add("Processing")
          ddProcessingStatusSingle.Items.Add("Confirmation")
          'ddProcessingStatusSingle.Items.Add("Send to Pencom")
          'ddProcessingStatusSingle.Items.Add("Approved/Processing")
          'ddProcessingStatusSingle.Items.Add("Paid")
          'ddProcessingStatusSingle.Items.Add("Terminated")


          ddApplicationStatusBatch.Items.Add("")
          '  ddApplicationStatusBatch.Items.Add("Documentation")
          ' ddApplicationStatusBatch.Items.Add("Processing")
          ddApplicationStatusBatch.Items.Add("Confirmation")
          'ddApplicationStatusBatch.Items.Add("Send to Pencom")
          'ddApplicationStatusBatch.Items.Add("Approved/Processing")
          'ddApplicationStatusBatch.Items.Add("Paid")
          'ddApplicationStatusBatch.Items.Add("Terminated")

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
          ddApprovalType.Items.Clear()
          Do While i < lstAppTypes.Count

               If ddApprovalType.Items.Count = 0 Then
                    ddApprovalType.Items.Add("")
                    ddApprovalType.Items.Add(lstAppTypes.Item(i))
               ElseIf ddApprovalType.Items.Count > 0 Then
                    ddApprovalType.Items.Add(lstAppTypes.Item(i))
               End If
               i = i + 1

          Loop

     End Sub
     Protected Sub getApplicationForProcessing(typeID As Integer)
          Dim cr As New Core, dt As New DataTable, vDate As Date

          vDate = cr.PMgetCurrentValueDate(2)
          Try
               If typeID = 6 Then
                    Me.txtPriceDateBatch.Text = vDate
                    Me.txtPriceDateBatch.Enabled = False
               Else
               End If
			dt = cr.PMgetApplicationByTpye(typeID, "Processing")

			ViewState("ApplicationForProcessing") = dt

			If dt.Rows.Count > 6 Then
				Me.pnlGrid.Height = Nothing
			Else

			End If


               Me.gridProcessing.DataSource = dt
               gridProcessing.DataBind()
          Catch ex As Exception

          End Try

     End Sub
     Private Sub populateApplicationList()
          Dim dt As New DataTable
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
          refresh()
          populateDocuments(dt)
     End Sub
     Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

          Me.txtPriceDateBatch.Text = ""
          populateApplicationList()

	End Sub

	Protected Sub gridProcessing_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridProcessing.PageIndexChanging

		If IsNothing(ViewState("ApplicationForProcessing")) = False Then

			Dim dt As New DataTable
			Me.gridProcessing.PageIndex = e.NewPageIndex
			dt = ViewState("ApplicationForProcessing")

			If dt.Rows.Count > 10 Then
				Me.pnlGrid.Height = Nothing
			Else
			End If

			Me.gridProcessing.DataSource = dt
			gridProcessing.DataBind()

		Else

		End If

	End Sub

     Protected Sub gridProcessing_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridProcessing.SelectedIndexChanged

          Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
          Dim selectedRowIndex As Integer

          selectedRowIndex = Me.gridProcessing.SelectedRow.RowIndex

          Dim row As GridViewRow = gridProcessing.Rows(selectedRowIndex)

          dt = cr.PMgetApplicationByCode(row.Cells(2).Text.ToString())
          
          dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CStr(row.Cells(2).Text.ToString()))

          ViewState("ApplicationCode") = row.Cells(2).Text.ToString
          ViewState("PIN") = row.Cells(4).Text.ToString

          dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())

          ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(2).Text.ToString(), row.Cells(4).Text.ToString())


		If dtPDetails.Rows(0).Item("Legacy") > 0 Then

			mpLagacyPopupExtender.Show()

		Else
		End If

		'retrive the cut-off date for programme withdrawal payment
		If dt.Rows(0).Item("fkiAppTypeId") = 3 Then
			Dim i As Integer = 0
			Do While i < ApplicationProperties.Count

				If ApplicationProperties.Item(i).FieldName = "Set Price Date :" Then
					txtPriceDateBatch.Text = CDate(ApplicationProperties.Item(i).FieldValue).ToString("yyyy-MM-dd")
					txtPriceDateBatch.Enabled = False
				Else
				End If

				i = i + 1
			Loop

		ElseIf dt.Rows(0).Item("fkiAppTypeId") = 14 Then

			Dim i As Integer = 0
			Do While i < ApplicationProperties.Count

				If ApplicationProperties.Item(i).FieldName = "Set Price Date :" Then
					txtPriceDateBatch.Text = CDate(ApplicationProperties.Item(i).FieldValue).ToString("yyyy-MM-dd")
					txtPriceDateBatch.Enabled = False
				Else
				End If

				i = i + 1
			Loop

			'retrive the cut-off date for annuity payment
		ElseIf dt.Rows(0).Item("fkiAppTypeId") = 4 Then
			Dim i As Integer = 0
			Do While i < ApplicationProperties.Count

				If ApplicationProperties.Item(i).FieldName = "Set Price Date :" Then
					txtPriceDateBatch.Text = CDate(ApplicationProperties.Item(i).FieldValue).ToString("yyyy-MM-dd")
					txtPriceDateBatch.Enabled = False
				Else
				End If

				i = i + 1
			Loop

			'retrive the cut-off date for death benefit
		ElseIf dt.Rows(0).Item("fkiAppTypeId") = 5 Then

			Dim i As Integer = 0
			Do While i < ApplicationProperties.Count

				If ApplicationProperties.Item(i).FieldName = "Set Price Date :" Then
					txtPriceDateBatch.Text = CDate(ApplicationProperties.Item(i).FieldValue).ToString("yyyy-MM-dd")
					txtPriceDateBatch.Enabled = False
				Else
				End If



				i = i + 1
			Loop


		End If

		Session("lodgmentProperties") = ApplicationProperties


		populateProperties(ApplicationProperties)
		Me.populateDocuments(dtDocuments)

		If ApplicationProperties.Count < 10 Then
			pnlLeftGrid.Height = 400
		Else
			pnlLeftGrid.Height = Nothing
		End If


		dvActionHardShip.Visible = True
		Me.ddProcessingStatusSingle.SelectedIndex = 0
		Me.txtPriceDateSingle.Text = ""

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

     Protected Sub populateDocuments(dt As DataTable)
          Try
               ViewState("Documents") = dt
               Me.gridSubmittedDocuments.DataSource = dt
               Me.gridSubmittedDocuments.DataBind()

               If dt.Rows.Count > 0 Then

                    '     Me.pnlDocumentDetails.Height = 400
                    'Else
                    Me.pnlDocumentDetails.Height = Nothing
               Else
                    Me.pnlDocumentDetails.Height = 100
               End If

          Catch ex As Exception

          End Try

     End Sub

     Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          'If IsNothing(ViewState("Documents")) = False Then

          '     Dim dt As DataTable = ViewState("Documents")
          '     If e.Row.RowType = DataControlRowType.DataRow Then

          '          If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

          '               e.Row.ForeColor = System.Drawing.Color.Red

          '          ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" Then

          '               e.Row.ForeColor = System.Drawing.Color.Green

          '          End If

          '     End If
          'Else
          'End If


          If IsNothing(ViewState("Documents")) = False Then

               Dim dt As DataTable = ViewState("Documents")
               If e.Row.RowType = DataControlRowType.DataRow Then

                    If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

                         e.Row.ForeColor = System.Drawing.Color.Red
                         e.Row.Enabled = False

				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Or dt.Rows(e.Row.RowIndex).Item("DocumentID").ToString <> "") And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "True" Then
					e.Row.ForeColor = System.Drawing.Color.Green

				ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And (dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Or dt.Rows(e.Row.RowIndex).Item("DocumentID").ToString <> "") And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then
					e.Row.ForeColor = System.Drawing.Color.Blue
					e.Row.Enabled = True
                    End If

               End If
          Else
          End If


     End Sub

     Protected Sub btnHardShipProcessingSingle_Click(sender As Object, e As EventArgs) Handles btnHardShipProcessingSingle.Click

          Dim cr As New Core, dt As New DataTable
          Try

               If Not IsNothing(ViewState("ApplicationCode")) = True And Me.ddProcessingStatusSingle.SelectedItem.Text.ToString <> "" Then
                    '   cr.PMSetApplicationStatus(CStr(ViewState("ApplicationCode")), Me.ddProcessingStatusSingle.SelectedItem.Text.ToString)
               End If

               If Not IsNothing(ViewState("ApplicationCode")) = True And Me.txtPriceDateSingle.Text <> "" Then
                    cr.PMSetPriceDate(CStr(ViewState("ApplicationCode")), DateTime.Parse(Me.txtPriceDateSingle.Text).ToString("yyyy-MM-dd"), CStr(ViewState("PIN")), 1, 0)
                    cr.PMSetApplicationStatus(CStr(ViewState("ApplicationCode")), "Processing", Session("user"))
               End If

               refresh()
               getApplicationForProcessing(0)
               populateDocuments(dt)

          Catch ex As Exception

          End Try

     End Sub

     

     Protected Sub refresh()
          ViewState("ApplicationCode") = Nothing
          Dim nw As New List(Of ApplicationProperties)
          populateProperties(nw)
          dvActionHardShip.Visible = False
     End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = True


          Next


     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = False


          Next


     End Sub

     Protected Sub btnHardShipProcessingBatch_Click(sender As Object, e As EventArgs) Handles btnHardShipProcessingBatch.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         If Me.txtPriceDateBatch.Text <> "" Then
                              cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, 1, 0)
                              cr.PMSetApplicationStatus(grow.Cells(2).Text, "Processing", Session("user"))

                              refresh()
                              ' getApplicationForProcessing(0)

                              ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
                              getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

                              populateDocuments(dt)
                              Me.txtPriceDateBatch.Text = ""
                         End If

                         


                    ElseIf cb.Checked = False Then



                    End If

               Next




               ' dsSNR.
               

               '  Me.Response.Redirect("Mandate.aspx")

               '  dsSNR.dtSNRDataTable = dt

          Catch ex As Exception
               '   MsgBox("" & ex.Message)
          Finally

               GC.Collect()


          End Try


     End Sub

     Protected Sub btnSendBack_Click(sender As Object, e As EventArgs) Handles btnSendBack.Click


          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try


               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         'If Me.ddApplicationStatusBatch.SelectedItem.Text.ToString <> "" Then
                         cr.PMSetApplicationStatus(grow.Cells(2).Text, "Documentation", 1)
                         'Else

                         'End If

                         refresh()

                    ElseIf cb.Checked = False Then

                    End If

               Next
               'getApplicationList()
          Catch ex As Exception

          End Try



     End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As ImageClickEventArgs) Handles btnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = True


          Next

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As ImageClickEventArgs) Handles btnUnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridProcessing.Rows

               grow.FindControl("chkProcessing")

               cb = grow.FindControl("chkProcessing")

               cb.Checked = False


          Next

     End Sub

     Protected Sub calPriceDateBatch_SelectionChanged(sender As Object, e As EventArgs) Handles calPriceDateBatch.SelectionChanged

          Me.calPriceDateBatch_PopupControlExtender.Commit(Me.calPriceDateBatch.SelectedDate)

     End Sub

	Protected Sub AddViewIACComment_Click(sender As Object, e As EventArgs) Handles btnShowIACCommentPopup.Click

		Dim btnAddViewIACComment As New ImageButton
		btnAddViewIACComment = sender
		Dim i As GridViewRow, cr As New Core

		i = btnAddViewIACComment.NamingContainer

		'MsgBox("" & Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text.ToString)

		Me.txtIACApplicationID.Text = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text
		Me.txtApplicationIACComment.Text = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE_IC").Rows(0).Item("txtComment").ToString

		'pops up the comment dialogue
		mpAppIACComments.Show()

	End Sub

     Private Function populateSNR(pin As String, appCode As String) As DataSet

          Dim cr As New Core, dtApplication As New DataTable, i As Integer = 0
          'dtApplication = cr.PMgetApplicationByPIN("PEN100000189215", 2)
          dtApplication = cr.PMgetApplicationByPIN(pin, appCode)
          Dim ds As New dsSNR
          Dim newSNRow As DataRow

          newSNRow = ds.Tables(0).NewRow

          newSNRow("txtPIN") = dtApplication.Rows(0).Item("rsapin")
          newSNRow("txtSurname") = dtApplication.Rows(0).Item("Surname")
          newSNRow("txtFirstName") = dtApplication.Rows(0).Item("FirstName")
          newSNRow("txtOtherName") = dtApplication.Rows(0).Item("MiddleName")
          newSNRow("dteDOB") = dtApplication.Rows(0).Item("dateofbirth")
          newSNRow("dteDOR") = dtApplication.Rows(0).Item("DOR")
          newSNRow("txtPermAddress1") = dtApplication.Rows(0).Item("ResidentialAddress")
          newSNRow("txtPermAddress2") = ""
          newSNRow("txtContactAddress1") = dtApplication.Rows(0).Item("ContactAddress")
          newSNRow("txtContactAddress2") = ""
          newSNRow("txtTelephone1") = dtApplication.Rows(0).Item("Phone")
          newSNRow("txtTelephone2") = ""
          newSNRow("txtEmployerName") = dtApplication.Rows(0).Item("EmployerName")
          newSNRow("txtEmpoyerAddress") = dtApplication.Rows(0).Item("OfficeAddress")
          newSNRow("txtEmployerCode") = dtApplication.Rows(0).Item("EmployerCode")
		newSNRow("numTotalRemuneration") = "0.00"

		newSNRow("Passport") = cr.PMgetParticipantPassport(pin)

		If appCode.Split("-")(0) = "HAR" Then
			newSNRow("numTotalContribution") = dtApplication.Rows(0).Item("Mandatory")
		ElseIf appCode.Split("-")(0) = "AVC" Then
			newSNRow("numTotalContribution") = dtApplication.Rows(0).Item("AVC")
		Else
			newSNRow("numTotalContribution") = dtApplication.Rows(0).Item("numRSABalance")
		End If

          newSNRow("txtPFACode") = "0023"
          newSNRow("txtSignatory") = "Tade Gbadebo"
          newSNRow("txtSignatoryDesignation") = "Ag. Head Benefit Processing"
          newSNRow("txtSex") = dtApplication.Rows(0).Item("sex")
          newSNRow("txtRetirementReason") = dtApplication.Rows(0).Item("txtReason")
          newSNRow("txtMaritalStatus") = dtApplication.Rows(0).Item("MaritalStatus")

          ds.Tables(0).Rows.Add(newSNRow)
          Return ds
          'MsgBox("" & ds.Tables(0).Rows.Count)

          '          Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & Year(Me.txtStartDate.Text) & "" & Month(Me.txtStartDate.Text) & "" & Day(Me.txtStartDate.Text) & "_" & Me.dcFund.SelectedValue & "_" & brokers.Item(i) & ".pdf"





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

     Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
          btnViewDocumentLog = sender
          Dim i As GridViewRow
		i = btnViewDocumentLog.NamingContainer


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


     Private Sub generateFiles(pin As String, appCode As String, path As String)

          Dim crExportOptions As New ExportOptions
          Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
          Dim crFormatypeOption As New PdfRtfWordFormatOptions
          Dim rdoc As New ReportDocument
          Dim rsource As New CrystalDecisions.Web.CrystalReportSource

          rdoc.Load(Server.MapPath("~/Report/SNR.rpt"))

          'If Not Directory.Exists(path) = True Then
          '    Directory.CreateDirectory(path)
          'End If

          Dim ds As DataSet
          ds = populateSNR(pin, appCode)
          rdoc.SetDataSource(ds.Tables(0))

          'crDiskFileDestinationOptions.DiskFileName = path & "MyTest.PDF"
          crDiskFileDestinationOptions.DiskFileName = path
          crExportOptions = rdoc.ExportOptions

          With crExportOptions

               .ExportDestinationType = ExportDestinationType.DiskFile
               .ExportFormatType = ExportFormatType.PortableDocFormat
               .ExportDestinationOptions = crDiskFileDestinationOptions
               .ExportFormatOptions = crFormatypeOption

          End With

          rdoc.Export()
          'rsource.Export()

     End Sub

     Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click

          ' Dim filePath As String = "\\p-midas2\mlive\TradeMandate\SNR.pdf"
          'generateFiles("PEN100000189215", 2, filePath)

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable

          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True And chk < 1 Then

                         ' Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & grow.Cells(4).Text & ".pdf"
                         Dim filePath As String = Server.MapPath("~/FileDownLoads/" & grow.Cells(4).Text & ".pdf")

                         generateFiles(grow.Cells(4).Text, grow.Cells(2).Text, filePath)
                         ViewState("schedulePath") = filePath
                         DownLoadSNR()
                         chk = chk + 1
                    ElseIf cb.Checked = False Then
                    End If

               Next

          Catch ex As Exception

          Finally

               GC.Collect()

          End Try

     End Sub

     Private Sub DownLoadSNR()

          If IsNothing(ViewState("schedulePath")) = False Then

               If CStr(ViewState("schedulePath")).ToString = "" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
               Else
               End If


               Dim schedulePath As String = ViewState("schedulePath")
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
               ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
          End If


     End Sub
     'refreshing the pop up comment list on an application
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
     Protected Sub AddViewComment_Click(sender As Object, e As EventArgs) Handles btnShowCommentPopup.Click

          Dim btnAddViewComment As New ImageButton, dt As DataTable, j As Integer
          btnAddViewComment = sender
          Dim i As GridViewRow, cr As New Core

          i = btnAddViewComment.NamingContainer
          Me.txtApplicationID.Text = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text
          dt = cr.PMgetApplicationComment(Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text, "PRE")
          lstComments.Items.Clear()
          Do While j < dt.Rows.Count

               lstComments.Items.Add(dt.Rows(j).Item(2).ToString & " : " & dt.Rows(j).Item(1).ToString & " : " & dt.Rows(j).Item(0).ToString)
               j = j + 1

          Loop

          'pops up the comment dialogue
          mpAppComments.Show()



     End Sub

     Protected Sub btnHardShipProcessingBatch_Click(sender As Object, e As ImageClickEventArgs) Handles btnHardShipProcessingBatch.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          Try

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then
                         'getting current value of customer on rsa platform for 25% application
                         If Me.txtPriceDateBatch.Text <> "" And Me.rdRSA.Checked = True And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 2 Then

						Dim pAmount As Double, fundType As Integer

						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If

						pAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), fundType)

						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (pAmount / 4))
                              'cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, 1, (pAmount))
                              cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))

                              
					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 11 Then

						Dim pAmount As Double, fundType As Integer
						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If

						pAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), fundType)

						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (pAmount))

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And Me.rdRSA.Checked = True And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 8 Then

						Dim pAmount As Double, dtPDetails As New DataTable, fundType As Integer

						If rdRSA.Checked = True Then
							fundType = 1
						Else
							fundType = 2
						End If

						dtPDetails = cr.getPMPersonInformation(grow.Cells(4).Text)

						pAmount = Convert.ToDecimal(dtPDetails.Rows(0).Item("TotalLegacyUnit")) * (cr.PMUnitPriceByDate(CDate(Me.txtPriceDateBatch.Text), fundType))


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (pAmount))
						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))

						'  refresh()
						'  getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
						' populateDocuments(dt)
						'Me.txtPriceDateBatch.Text = ""


					ElseIf Me.txtPriceDateBatch.Text <> "" And Me.rdRSA.Checked = True And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 7 Then

						Dim pAmount As Double, dtPDetails As New DataTable, aVD As New AVCDetails, fundType As Integer

						If IsNothing(ViewState("AVCDetails")) = True Then
							Exit Sub
						Else
						End If

						' aVD = ViewState("AVCDetails")
						dtPDetails = cr.getPMPersonInformation(grow.Cells(4).Text)
						'  pAmount = Convert.ToDecimal(dtPDetails.Rows(0).Item("TotalLegacyUnit")) * (cr.PMUnitPriceByDate(CDate(Me.txtPriceDateBatch.Text), 1))

						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (Convert.ToDecimal(cr.PMGetInsertedTempRMASRecord(grow.Cells(2).Text).Rows(0).Item("txtNetPayable"))))

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))

						'refresh()
						'getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
						'populateDocuments(dt)
						'Me.txtPriceDateBatch.Text = ""


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 3 Then

						Dim pAmount As Double, dtPDetails As New DataTable, aVD As New AVCDetails, fundType As Integer

						'If rdRSA.Checked = True Then
						'fundType = 1
						'ElseIf rdRF.Checked = True Then
						fundType = 2
						'End If

						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (Convert.ToDecimal(cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text).Rows(0).Item("numRSABalance"))))

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 14 Then


						Dim pAmount As Double, dtPDetails As New DataTable, aVD As New AVCDetails, fundType As Integer

						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If

						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (Convert.ToDecimal(cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text).Rows(0).Item("numRSABalance"))))

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 4 Then

						Dim pAmount As Double, dtPDetails As New DataTable, aVD As New AVCDetails, fundType As Integer


						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (Convert.ToDecimal(cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text).Rows(0).Item("numRSABalance"))))

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 15 Then

						Dim pAmount As Double, dtPDetails As New DataTable, aVD As New AVCDetails, fundType As Integer


						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (Convert.ToDecimal(cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text).Rows(0).Item("numRSABalance"))))

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 5 Then

						Dim pAmount As Double, dtPDetails As New DataTable, aVD As New AVCDetails, fundType As Integer

						If rdRSA.Checked = True Then
							fundType = 1
						ElseIf rdRF.Checked = True Then
							fundType = 2
						End If



						Dim pRSAAmount As Double, pRFAmount As Double
						If Me.rdRSA.Checked = True Then
							pRSAAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
							pAmount = pRSAAmount
							fundType = 1
						ElseIf Me.rdRF.Checked = True Then
							pRFAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 2)
							pAmount = pRFAmount
							fundType = 2
						End If



						'cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, (Convert.ToDecimal(cr.PMGetInsertedDBARecord(grow.Cells(2).Text).Rows(0).Item("numRSABalance"))))

						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, pAmount)

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 6 Then

						Dim appProperties As New List(Of ApplicationProperties)
						Dim dtPDetails As New DataTable, i As Integer = 0, fundType As Integer
						appProperties = Session("lodgmentProperties")

						If Me.rdRF.Checked = True Then
							fundType = 2
						ElseIf Me.rdRSA.Checked = True Then
							fundType = 1
						End If


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, appProperties)
						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 1 Then


						Dim pRSAAmount As Double, pRFAmount As Double, pAmount As Double, fundType As Integer
						If Me.rdRSA.Checked = True Then
							pRSAAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
							pAmount = pRSAAmount
							fundType = 1
						ElseIf Me.rdRF.Checked = True Then
							pRFAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 2)
							pAmount = pRFAmount
							fundType = 2
						End If


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, pAmount)

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 16 Then


						Dim pRSAAmount As Double, pRFAmount As Double, pAmount As Double, fundType As Integer
						If Me.rdRSA.Checked = True Then
							pRSAAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
							pAmount = pRSAAmount
							fundType = 1
						ElseIf Me.rdRF.Checked = True Then
							pRFAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 2)
							pAmount = pRFAmount
							fundType = 2
						End If


						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, pAmount)

						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 11 Then

						Dim pRSAAmount As Double, pRFAmount As Double, pAmount As Double, fundType As Integer

						If Me.rdRSA.Checked = True Then
							pRSAAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
							pAmount = pRSAAmount
							fundType = 1
						ElseIf Me.rdRF.Checked = True Then
							pRFAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 2)
							pAmount = pRFAmount
							fundType = 2
						End If

						cr.PMSetPriceDate(grow.Cells(2).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), grow.Cells(4).Text, fundType, pAmount)
						cr.PMSetApplicationStatus(grow.Cells(2).Text, "Confirmation", Session("user"))

					End If

				ElseIf cb.Checked = False Then

				End If

			Next

               refresh()
               getApplicationForProcessing(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
               populateDocuments(dt)
               Me.txtPriceDateBatch.Text = ""


          Catch ex As Exception

          Finally

               GC.Collect()


          End Try


     End Sub

     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewApplicationLog As New ImageButton, appCode As String
          btnViewApplicationLog = sender
          Dim i As GridViewRow
          i = btnViewApplicationLog.NamingContainer
          appCode = Me.gridProcessing.Rows(i.RowIndex).Cells(2).Text

          Dim typeID As Integer
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          'typeID = (CInt(ApprovalTypeCollection.Item(Me.gridProcessing.Rows(i.RowIndex).Cells(4).Text)))
          typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

          Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}&ReturnPage={3}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1, Server.UrlEncode("frmProcessing")))


          '  Response.Redirect(String.Format("frmApplicationConfirmation.aspx?ApplicationCode={0}&ReturnPage={1}", Server.UrlEncode(appDetail.ApplicationID), Server.UrlEncode("ApplicationDashBoard")))

     End Sub

     Protected Sub btnSendBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnSendBack.Click

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core

          Try


               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then

                         'If Me.ddApplicationStatusBatch.SelectedItem.Text.ToString <> "" Then
                         cr.PMSetApplicationStatus(grow.Cells(2).Text, "Documentation", 1)
                         'Else

                         'End If

                         refresh()

                    ElseIf cb.Checked = False Then

                    End If

               Next
               populateApplicationList()
          Catch ex As Exception

          End Try

     End Sub
     'Private Sub getApplicationList()

     '     Dim dt As New DataTable
     '     ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
     '     getApplicationForDocumentation(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
     '     ' refresh()
     '     ' populateDocuments(dt)

     'End Sub

     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click
		If IsNothing(Session("user")) = False Then

			Dim cr As New Core
			'the first 1 indicate pre-approval comment while the  second 1 indicate a default checklist code
			cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1, 1)
			txtApplicationComment.Text = ""
			refreshCommentList(txtApplicationID.Text)

		Else
			Response.Redirect("login.aspx")
		End If

		
          'Me.mpAppComments.Show()

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

     Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

     End Sub

     Protected Sub btnPreviewProcessing_Click(sender As Object, e As ImageClickEventArgs) Handles btnPreviewProcessing.Click


          Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable, ApplicationProperties As New List(Of ApplicationProperties)
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          Try

               '  MsgBox("" & CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)).ToString)

               For Each grow As GridViewRow In Me.gridProcessing.Rows

                    cb = grow.FindControl("chkProcessing")

                    If cb.Checked = True Then
                         'getting current value of customer on rsa platform for 25% application

                         If Me.txtPriceDateBatch.Text <> "" And Me.rdRSA.Checked = True And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 2 Then


                              'Enter Code Here

						Dim pAmount As Double

						'  pAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
						pAmount = cr.PMgetApplicationByPIN(grow.Cells(4).Text, grow.Cells(2).Text).Rows(0).Item("Mandatory")
						'Mandatory()

                              ApplicationProperties = Session("lodgmentProperties")
                              Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
                              Do While j < ApplicationProperties.Count

                                   appNewProperty = New ApplicationProperties

                                   If ApplicationProperties(j).FieldName = "Set Price Date :" Then
                                        appNewProperty.FieldName = ApplicationProperties(j).FieldName
                                        appNewProperty.FieldValue = Me.txtPriceDateBatch.Text
                                   ElseIf ApplicationProperties(j).FieldName = "RSA Balance :" Then
                                        appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pAmount.ToString("#,###.00")
                                   ElseIf ApplicationProperties(j).FieldName = "25% Payment :" Then
                                        appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = (pAmount / 4).ToString("#,###.00")
                                        '25% Payment :
                                   Else

                                        appNewProperty.FieldName = ApplicationProperties(j).FieldName
                                        appNewProperty.FieldValue = ApplicationProperties(j).FieldValue

                                   End If

                                   appNewProperties.Add(appNewProperty)

                                   j = j + 1
                              Loop
                             

                              populateProperties(appNewProperties)


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 11 Then


						Dim pAmount As Double, fundType As Integer

						If rdRF.Checked = True Then
							fundType = 2
						ElseIf rdRSA.Checked = True Then
							fundType = 1
						End If

						pAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), fundType)

						ApplicationProperties = Session("lodgmentProperties")
						Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
						Do While j < ApplicationProperties.Count

							appNewProperty = New ApplicationProperties

							If ApplicationProperties(j).FieldName = "Set Price Date :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = Me.txtPriceDateBatch.Text

							ElseIf ApplicationProperties(j).FieldName = "RSA Balance:" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pAmount.ToString("#,###.00")

							Else

								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = ApplicationProperties(j).FieldValue

							End If

							appNewProperties.Add(appNewProperty)

							j = j + 1
						Loop


						populateProperties(appNewProperties)


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 1 Then


						Dim pRSAAmount As Double, pRFAmount As Double

						pRSAAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
						pRFAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 2)


						ApplicationProperties = Session("lodgmentProperties")
						Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
						Do While j < ApplicationProperties.Count

							appNewProperty = New ApplicationProperties

							If ApplicationProperties(j).FieldName = "Set Price Date :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = Me.txtPriceDateBatch.Text
							ElseIf ApplicationProperties(j).FieldName = "RSA Balance :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pRSAAmount.ToString("#,##0.00")

							ElseIf ApplicationProperties(j).FieldName = "RF Balance :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pRFAmount.ToString("#,##0.00")
							Else
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = ApplicationProperties(j).FieldValue
							End If

							appNewProperties.Add(appNewProperty)

							j = j + 1
						Loop
						appNewProperty = New ApplicationProperties
						appNewProperty.FieldName = "Enbloc Amount :"
						If Me.rdRSA.Checked = True Then
							appNewProperty.FieldValue = pRSAAmount.ToString("#,##0.00")
						ElseIf Me.rdRF.Checked = True Then
							appNewProperty.FieldValue = pRFAmount.ToString("#,##0.00")
						End If

						appNewProperties.Add(appNewProperty)

						populateProperties(appNewProperties)

						'Me.btnHardShipProcessingBatch.Visible = True



					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 16 Then


						Dim pRSAAmount As Double, pRFAmount As Double

						pRSAAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)
						pRFAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 2)


						ApplicationProperties = Session("lodgmentProperties")
						Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
						Do While j < ApplicationProperties.Count

							appNewProperty = New ApplicationProperties

							If ApplicationProperties(j).FieldName = "Set Price Date :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = Me.txtPriceDateBatch.Text
							ElseIf ApplicationProperties(j).FieldName = "RSA Balance :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pRSAAmount.ToString("#,##0.00")

							ElseIf ApplicationProperties(j).FieldName = "RF Balance :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pRFAmount.ToString("#,##0.00")
							Else
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = ApplicationProperties(j).FieldValue
							End If

							appNewProperties.Add(appNewProperty)

							j = j + 1
						Loop
						appNewProperty = New ApplicationProperties
						appNewProperty.FieldName = "Enbloc Amount :"
						If Me.rdRSA.Checked = True Then
							appNewProperty.FieldValue = pRSAAmount.ToString("#,##0.00")
						ElseIf Me.rdRF.Checked = True Then
							appNewProperty.FieldValue = pRFAmount.ToString("#,##0.00")
						End If

						appNewProperties.Add(appNewProperty)

						populateProperties(appNewProperties)

						'Me.btnHardShipProcessingBatch.Visible = True




					ElseIf Me.txtPriceDateBatch.Text <> "" And Me.rdRSA.Checked = True And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 8 Then

						Dim curPrice As Decimal, crr As New Core, TotalLegacyUnit As Decimal
						curPrice = crr.PMUnitPriceByDate(CDate(txtPriceDateBatch.Text), 1)

						ApplicationProperties = Session("lodgmentProperties")

						Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
						Do While j < ApplicationProperties.Count

							appNewProperty = New ApplicationProperties

							If ApplicationProperties(j).FieldName = "Legacy Total Unit :" Then
								TotalLegacyUnit = ApplicationProperties(j).FieldValue
							End If

							If ApplicationProperties(j).FieldName = "Set Price Date :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = Me.txtPriceDateBatch.Text
							ElseIf ApplicationProperties(j).FieldName = "Legacy Paying Price :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = curPrice
							ElseIf ApplicationProperties(j).FieldName = "Legacy Amt Paid :" Then
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = (TotalLegacyUnit * curPrice).ToString("#,##0.00")
							Else
								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = ApplicationProperties(j).FieldValue
							End If

							appNewProperties.Add(appNewProperty)

							j = j + 1
						Loop

						populateProperties(appNewProperties)


					ElseIf Me.txtPriceDateBatch.Text <> "" And Me.rdRSA.Checked = True And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 7 Then

						Dim curPrice As Decimal, crr As New Core, dtAVCDetails As New DataTable
						If CBool(ViewState("AVCDetails")) = False Then

							Me.txtPaymentPriceDate.Text = Me.txtPriceDateBatch.Text
							curPrice = crr.PMUnitPriceByDate(CDate(txtPriceDateBatch.Text), 1)
							Me.txtPayingPrice.Text = curPrice.ToString("##0.0000")
							Me.txtApplicationCode.Text = grow.Cells(2).Text
							Me.txtAVCAmount.Text = crr.PMgetApplicationByCode(grow.Cells(2).Text).Rows(0).Item("ApplicationAMount")
							Me.mpAVCDetail.Show()

						Else

							'End If


							curPrice = crr.PMUnitPriceByDate(CDate(txtPriceDateBatch.Text), 1)
							'   dtAVCDetails = crr.PMGetAVCDetails(grow.Cells(4).Text)
							ApplicationProperties = Session("lodgmentProperties")

							Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
							Do While j < ApplicationProperties.Count

								appNewProperty = New ApplicationProperties

								If ApplicationProperties(j).FieldName = "Set Price Date :" Then
									appNewProperty.FieldName = ApplicationProperties(j).FieldName
									appNewProperty.FieldValue = Me.txtPriceDateBatch.Text
								Else
									appNewProperty.FieldName = ApplicationProperties(j).FieldName
									appNewProperty.FieldValue = ApplicationProperties(j).FieldValue
								End If

								appNewProperties.Add(appNewProperty)

								j = j + 1
							Loop

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Taxable Total AVC : "
							appNewProperty.FieldValue = Me.txtAVCAmount.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Non-Taxable Total AVC : "
							appNewProperty.FieldValue = Me.txtAVCAmountNoTax.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Total AVC Units : "
							appNewProperty.FieldValue = Me.txtAVCUnits.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Avg. AVC Units : "
							appNewProperty.FieldValue = Me.txtAVGPrice.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Paying AVC Unit Price : "
							appNewProperty.FieldValue = Me.txtPayingPrice.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Current AVC Values : "
							appNewProperty.FieldValue = Me.txtCurrentValue.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "AVC Tax Deduction : "
							appNewProperty.FieldValue = Me.txtTaxDeduction.Text
							appNewProperties.Add(appNewProperty)

							appNewProperty = New ApplicationProperties
							appNewProperty.FieldName = "Net AVC Payable : "
							appNewProperty.FieldValue = Me.txtNetPayable.Text
							appNewProperties.Add(appNewProperty)

							populateProperties(appNewProperties)


						End If


					ElseIf Me.txtPriceDateBatch.Text <> "" And CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)) = 6 Then


						Dim crr As New Core, dtNSITFDetails As New DataTable
						dtNSITFDetails = cr.getPMPersonInformation(grow.Cells(4).Text)

						If CBool(ViewState("NSITFDetails")) = False Then

							Me.txtNSITFApplicationCode.Text = grow.Cells(2).Text
							Me.txtNSITFAmount.Text = CDbl(dtNSITFDetails.Rows(0).Item("TotalRFPayment"))
							Me.txtNSITFAmountRecieved.Text = CDbl(dtNSITFDetails.Rows(0).Item("TotalNSITFUpload"))
							Me.txtNSITFAmountRequested.Text = CDbl(dtNSITFDetails.Rows(0).Item("TotalNSITFValueRSA"))
							Me.mpNSITFDetail.Show()

						Else
						End If


						Dim pAmount As Double, vDate As Date
						vDate = cr.PMgetCurrentValueDate(2)
						' pAmount = cr.PMValueByDate(grow.Cells(4).Text, DateTime.Parse(Me.txtPriceDateBatch.Text).ToString("yyyy-MM-dd"), 1)

						'TotalNSITFValueRSA,TotalNSITFValueRF
						If rdRF.Checked = True Then

							pAmount = cr.getPMPersonInformation(grow.Cells(4).Text).Rows(0).Item("TotalNSITFValueRF")
						ElseIf rdRSA.Checked = True Then

							pAmount = cr.getPMPersonInformation(grow.Cells(4).Text).Rows(0).Item("TotalNSITFValueRSA")
						End If



						ApplicationProperties = Session("lodgmentProperties")
						Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
						Do While j < ApplicationProperties.Count

							appNewProperty = New ApplicationProperties

							If ApplicationProperties(j).FieldName = "Set Price Date :" Then

								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = Me.txtPriceDateBatch.Text

							ElseIf ApplicationProperties(j).FieldName = "Amount Requested into RSA :" Then

								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = pAmount.ToString("#,###.00")

							Else


								appNewProperty.FieldName = ApplicationProperties(j).FieldName
								appNewProperty.FieldValue = ApplicationProperties(j).FieldValue

							End If

							appNewProperties.Add(appNewProperty)

							j = j + 1
						Loop

						populateProperties(appNewProperties)


					End If



				ElseIf cb.Checked = False Then


				End If

               Next

          Catch ex As Exception

          Finally

               GC.Collect()


          End Try


     End Sub

     Protected Sub btnPrintSNR_Click(sender As Object, e As EventArgs) Handles btnPrintSNR.Click
          mpAVCDetail.Show()
     End Sub

     Protected Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click


          ViewState("AVCDetails") = True
          Dim curPrice As Decimal, crr As New Core, dtAVCDetails As New DataTable, ApplicationProperties As New List(Of ApplicationProperties)
          curPrice = crr.PMUnitPriceByDate(CDate(txtPriceDateBatch.Text), 1)
          ApplicationProperties = Session("lodgmentProperties")

          Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
          Do While j < ApplicationProperties.Count

               appNewProperty = New ApplicationProperties

               If ApplicationProperties(j).FieldName = "Set Price Date :" Then
                    appNewProperty.FieldName = ApplicationProperties(j).FieldName
                    appNewProperty.FieldValue = Me.txtPriceDateBatch.Text
                    appNewProperties.Add(appNewProperty)
               ElseIf ApplicationProperties(j).FieldName = "Taxable Total AVC :" Then
               ElseIf ApplicationProperties(j).FieldName = "Non-Taxable Total AVC :" Then
               ElseIf ApplicationProperties(j).FieldName = "Total AVC Units :" Then
               ElseIf ApplicationProperties(j).FieldName = "Avg. AVC Units :" Then
               ElseIf ApplicationProperties(j).FieldName = "Paying AVC Unit Price :" Then
               ElseIf ApplicationProperties(j).FieldName = "Current AVC Values :" Then
               ElseIf ApplicationProperties(j).FieldName = "AVC Tax Deduction :" Then
               ElseIf ApplicationProperties(j).FieldName = "Net AVC Payable :" Then
               Else

                    appNewProperty.FieldName = ApplicationProperties(j).FieldName
                    appNewProperty.FieldValue = ApplicationProperties(j).FieldValue
                    appNewProperties.Add(appNewProperty)
               End If



               j = j + 1
          Loop

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Taxable Total AVC : "
          appNewProperty.FieldValue = Me.txtAVCAmount.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Non-Taxable Total AVC : "
          appNewProperty.FieldValue = Me.txtAVCAmountNoTax.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Total AVC Units : "
          appNewProperty.FieldValue = Me.txtAVCUnits.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Avg. AVC Units : "
          appNewProperty.FieldValue = Me.txtAVGPrice.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Paying AVC Unit Price : "
          appNewProperty.FieldValue = Me.txtPayingPrice.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Current AVC Values : "
          appNewProperty.FieldValue = Me.txtCurrentValue.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "AVC Tax Deduction : "
          appNewProperty.FieldValue = Me.txtTaxDeduction.Text
          appNewProperties.Add(appNewProperty)

          appNewProperty = New ApplicationProperties
          appNewProperty.FieldName = "Net AVC Payable : "
          appNewProperty.FieldValue = Me.txtNetPayable.Text
          appNewProperties.Add(appNewProperty)

          populateProperties(appNewProperties)

     End Sub

     Protected Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
          Try

         
               Dim avD As New AVCDetails, cr As New Core
			avD = Me.getAVCDetails(Me.txtAVCAmount.Text, Me.txtAVCAmountNoTax.Text, Me.txtAVCUnits.Text, Me.txtAVCAmountNoTax.Text, Me.txtPayingPrice.Text)

          Me.txtCurrentValue.Text = avD.AVCCurrentValue.ToString("#,#00.00")
          Me.txtTaxDeduction.Text = avD.AVCTaxDeduction.ToString("#,#00.00")

          Me.txtNetPayable.Text = avD.AVCNetPayable.ToString("#,#00.00")
          avD.ApplicationCode = txtApplicationCode.Text
          avD.TaxableProcessedAVC = Me.txtAVCAmount.Text
          avD.NonTaxableProcessedAVC = Me.txtAVCAmountNoTax.Text

          avD.TaxableAVCProcessedUnit = Me.txtAVCUnits.Text
          avD.NonTaxableAVCProcessedUnit = Me.txtNoTAXAVCUnits.Text
          avD.TotalProcessedAVC = CDbl(txtAVCAmount.Text) + CDbl(txtAVCAmountNoTax.Text)
          avD.AVCPaymentDate = CDate(Me.txtPaymentPriceDate.Text)
          avD.AVCPaymentUnitPrice = Me.txtPayingPrice.Text
               avD.AveragAVCPrice = Me.txtAVGPrice.Text
               cr.PMInsertTempRMASRecord(avD)


               Me.mpAVCDetail.Show()
               
          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub

     Protected Sub btnNSITFApply_Click(sender As Object, e As EventArgs) Handles btnNSITFApply.Click

          ViewState("NSITFDetails") = True

          Dim crr As New Core, dtAVCDetails As New DataTable, ApplicationProperties As New List(Of ApplicationProperties)
          ApplicationProperties = Session("lodgmentProperties")

          Dim j As Integer = 0, appNewProperties As New List(Of ApplicationProperties), appNewProperty As ApplicationProperties
          Do While j < ApplicationProperties.Count

               
               appNewProperty = New ApplicationProperties

               If ApplicationProperties(j).FieldName = "Initial NSITF Amount Paid :" Then

                    appNewProperty.FieldName = ApplicationProperties(j).FieldName
                    appNewProperty.FieldValue = Me.txtNSITFAmount.Text
                    appNewProperties.Add(appNewProperty)

               ElseIf ApplicationProperties(j).FieldName = "Amount Recieved into RSA  :" Then

                    appNewProperty.FieldName = ApplicationProperties(j).FieldName
                    appNewProperty.FieldValue = Me.txtNSITFAmountRecieved.Text
                    appNewProperties.Add(appNewProperty)

               ElseIf ApplicationProperties(j).FieldName = "Amount Requested into RSA :" Then

                    appNewProperty.FieldName = ApplicationProperties(j).FieldName
                    appNewProperty.FieldValue = Me.txtNSITFAmountRequested.Text
                    appNewProperties.Add(appNewProperty)

               Else

                    appNewProperty.FieldName = ApplicationProperties(j).FieldName
                    appNewProperty.FieldValue = ApplicationProperties(j).FieldValue
                    appNewProperties.Add(appNewProperty)

               End If

               j = j + 1
          Loop
          Session("lodgmentProperties") = appNewProperties

          'ApplicationProperties = Session("lodgmentProperties")
          populateProperties(appNewProperties)

     End Sub

	Protected Sub chkCurrentDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkCurrentDate.CheckedChanged

		If Me.chkCurrentDate.Checked = True Then

			Dim cr As New Core
			If Me.rdRSA.Checked = True Then

				Me.txtPriceDateBatch.Text = cr.PMgetCurrentValueDate(1)
				Me.txtPriceDateBatch.Enabled = False

			ElseIf Me.rdRF.Checked = True Then

				Me.txtPriceDateBatch.Text = cr.PMgetCurrentValueDate(2)
				Me.txtPriceDateBatch.Enabled = False

			End If

		Else
			Me.txtPriceDateBatch.Text = ""
			Me.txtPriceDateBatch.Enabled = True
		End If

	End Sub
End Class


