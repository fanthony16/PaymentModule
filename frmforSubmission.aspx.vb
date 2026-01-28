Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Partial Class frmforSubmission
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable
     Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          If IsNothing(ViewState("Documents")) = False Then

               Dim dt As DataTable = ViewState("Documents")
               If e.Row.RowType = DataControlRowType.DataRow Then

                    If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

                         e.Row.ForeColor = System.Drawing.Color.Red

                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "True" Then

                         e.Row.ForeColor = System.Drawing.Color.Green

                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("isVerified").ToString = "False" Then

                         e.Row.ForeColor = System.Drawing.Color.Blue

                    End If

               End If
          Else
          End If
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

     Protected Sub getApplicationForSubmission(typeID As Integer)
          Dim cr As New Core, dt As New DataTable

          Try

			dt = cr.PMgetApplicationByTpye(typeID, "Send to Pencom")
			ViewState("ApplicationForSubmission") = dt
			'If dt.Rows.Count > 3 Then
			'	Me.pnlGrid.Height = Nothing
			'Else
			'	Me.pnlGrid.Height = 300
			'End If

               Me.gridProcessing.DataSource = dt
               gridProcessing.DataBind()
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
          Try
			Dim dtusers As New DataTable, scriptManagerA As New ScriptManager, scriptManagerb As New ScriptManager, scriptManagerc As New ScriptManager

               scriptManagerA = ScriptManager.GetCurrent(Me.Page)
			scriptManagerA.RegisterPostBackControl(Me.btnSNR)

			scriptManagerb = ScriptManager.GetCurrent(Me.Page)
			scriptManagerb.RegisterPostBackControl(Me.btnNonSubmissionSchedule)

			scriptManagerc = ScriptManager.GetCurrent(Me.Page)
			scriptManagerc.RegisterPostBackControl(Me.btnHardShipProcessingBatch)




               '  scriptManagerA = ScriptManager.GetCurrent(Me.Page)
               ' scriptManagerb = ScriptManager.GetCurrent(Me.Page)
               'scriptManagerA.RegisterPostBackControl(Me.btnSNR)
               'scriptManagerb.RegisterPostBackControl(Me.gridSubmittedDocuments)


               If IsPostBack = False Then

                    If IsNothing(Session("user")) = True Then

                         Response.Redirect("Login.aspx")

                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

                         dtusers = Session("userDetails")
                         getUserAccessMenu(Session("user"))
                         getApprovalTypes()
                         getApplicationForSubmission(0)

                    End If


                    

               Else
                    getUserAccessMenu(Session("user"))
               End If

          Catch ex As Exception

          End Try
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
     Protected Sub populateDocuments(dt As DataTable)
          Try
               ViewState("Documents") = dt
               Me.gridSubmittedDocuments.DataSource = dt
               Me.gridSubmittedDocuments.DataBind()

               If dt.Rows.Count > 0 Then

                    Me.pnlDocumentDetails.Height = Nothing
               Else
                    Me.pnlDocumentDetails.Height = 100
               End If

          Catch ex As Exception

          End Try

     End Sub
	
	Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

		Dim dt As New DataTable
		ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
		getApplicationForSubmission(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
		refresh()
		populateDocuments(dt)

	End Sub
     Protected Sub refresh()

          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          getApplicationForSubmission(CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
          ViewState("ApplicationCode") = Nothing
          Dim nw As New List(Of ApplicationProperties)
          populateProperties(nw)

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

	Protected Sub gridProcessing_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridProcessing.PageIndexChanging

		If IsNothing(ViewState("ApplicationForSubmission")) = False Then

			Dim dt As New DataTable
			Me.gridProcessing.PageIndex = e.NewPageIndex
			dt = ViewState("ApplicationForSubmission")

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
          ' getting the submitted application doucments
          dtDocuments = cr.PMgetSubmittedDocument(row.Cells(4).Text.ToString(), CStr(row.Cells(2).Text.ToString()))

          ViewState("ApplicationCode") = row.Cells(2).Text.ToString
          ViewState("PIN") = row.Cells(4).Text.ToString

          dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())

          ''0
          'Dim ApplicationProperty As New ApplicationProperties
          'ApplicationProperty.FieldName = "Application Date :"
          'ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
          'ApplicationProperties.Add(ApplicationProperty)

          ''1
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Age :"
          'ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
          'ApplicationProperties.Add(ApplicationProperty)

          ''1
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Date Of Birth :"
          'ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString.Substring(0, 10)
          'ApplicationProperties.Add(ApplicationProperty)

          ''0
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Department :"
          'ApplicationProperty.FieldValue = dt.Rows(0).Item("txtDepartment").ToString
          'ApplicationProperties.Add(ApplicationProperty)

          ''0
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Designation :"
          'ApplicationProperty.FieldValue = dt.Rows(0).Item("txtDesignation").ToString
          'ApplicationProperties.Add(ApplicationProperty)

          ''0
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Reason For Exit :"
          'ApplicationProperty.FieldValue = dt.Rows(0).Item("txtReason").ToString
          'ApplicationProperties.Add(ApplicationProperty)

          ''0
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Date of Exit :"
          'ApplicationProperty.FieldValue = dt.Rows(0).Item("dteDisengagement").ToString.Substring(0, 10)
          'ApplicationProperties.Add(ApplicationProperty)

          ''0 

          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "Set Price Date :"
          'ApplicationProperty.FieldValue = dt.Rows(0).Item("dteConfirmPriceDate") '.ToString("#,000.00")
          'ApplicationProperties.Add(ApplicationProperty)

          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "RSA Balance :"
          'ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numRSABalance")).ToString("#,##0.00") '.ToString("#,000.00")
          'ApplicationProperties.Add(ApplicationProperty)

          ''Convert.ToDecimal(dt.Rows(0).Item("numRSABalance")).ToString("#,##0.00");
          ''dt.Rows(0).Item("numRSABalance")

          ''0
          'ApplicationProperty = New ApplicationProperties
          'ApplicationProperty.FieldName = "25% Payment :"
          'ApplicationProperty.FieldValue = Convert.ToDecimal((CDbl(dt.Rows(0).Item("numRSABalance")) / 4)).ToString("#,##0.00")  '.ToString("#,000.00")
          'ApplicationProperties.Add(ApplicationProperty)

          ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(2).Text.ToString(), row.Cells(4).Text.ToString())

          '123456789.ToString("#,000.00")


          Session("lodgmentProperties") = ApplicationProperties

          populateProperties(ApplicationProperties)
          Me.populateDocuments(dtDocuments)

          If ApplicationProperties.Count < 10 Then
               pnlLeftGrid.Height = 400
          Else
               pnlLeftGrid.Height = Nothing
          End If


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

	Protected Function populateSI(dt As DataTable) As DataSet

		Dim ds As New dsBenefitEnhancement, dtSI As New DataTable, cr As New Core, i As Integer, newBERow As DataRow

		'dt = ViewState("Retiree")
		Try

			newBERow = ds.Tables(0).NewRow
			newBERow("txtBatchNo") = CStr(ViewState("SPLodID"))
			newBERow("dteSubmission") = Now.Date
			newBERow("txtPFAName") = "LEADWAY PENSURE PFA LTD"
			newBERow("txtPFACode") = "023"
			newBERow("txtSurname") = dt.Rows(0).Item("Name").ToString.Split(" ")(0)

			newBERow("txtFirstName") = dt.Rows(0).Item("Name").ToString.Split(" ")(1)

			If dt.Rows(0).Item("Name").ToString.Split(" ").Count > 2 Then
				newBERow("txtMiddleName") = dt.Rows(0).Item("Name").ToString.Split(" ")(2)
			Else
				newBERow("txtMiddleName") = ""
			End If

			newBERow("txtPIN") = dt.Rows(0).Item("txtPIN").ToString
			newBERow("txtGender") = dt.Rows(0).Item("txtSex")
			newBERow("dteDOB") = dt.Rows(0).Item("dteDOB")
			newBERow("dteDOR") = dt.Rows(0).Item("dteDOR")

			newBERow("txtEmployerName") = dt.Rows(0).Item("txtEmployerName")
			newBERow("txtEmployerCode") = dt.Rows(0).Item("txtEmployerCode")

			newBERow("numRSABalance") = dt.Rows(0).Item("numRSABalance")
			newBERow("numMonthPension") = dt.Rows(0).Item("numMonthPension")
			newBERow("numCurrentRSABalance") = dt.Rows(0).Item("numCurrentRSABalance")
			newBERow("numNewPension") = dt.Rows(0).Item("numNewPension")

			newBERow("txtBPD") = "Tade Gbadebo"
			'newBERow("imgBPD") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteBPD") = Now.Date

			newBERow("txtCompliance") = "Akindele Fayombo"
			'newBERow("imgCompliance") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteCompliance") = Now.Date

			newBERow("txtMD") = "Ronke Adedeji"
			'newBERow("imgBPD") = "LEADWAY PENSURE PFA LTD"
			newBERow("dteMD") = Now.Date

			ds.Tables(0).Rows.Add(newBERow)

			Return ds

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try


	End Function

	Private Sub generateFiles(dt As DataTable)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource

		Try

			Dim filePath As String = Server.MapPath("~/FileDownLoads/BenefitEnhancementRequest.pdf")
			rdoc.Load(Server.MapPath("~/Report/BenefitEnhancementTemplate.rpt"))

			Dim ds As DataSet

			ds = populateSI(dt)
			ViewState("BenefitEnhancementRequest") = ds.Tables(0)
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


	Private Sub DownLoadDocument(path As String)

		If Not File.Exists(path) = False Then

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

     Protected Sub btnHardShipProcessingBatch_Click(sender As Object, e As EventArgs) Handles btnHardShipProcessingBatch.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable, lstSchedules As New List(Of RMASSchedule), apptype As String = "", appTypeID As Integer, lstApplicationEnhancement As New List(Of ApplicationEnhancement)

          Try


			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			appTypeID = CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text))
			'Me.spTest.InnerText = appTypeID.ToString

			If appTypeID = 17 Then

				Dim SPLodID As String = cr.PMgetNextSPLogID(CInt(appTypeID), "L")
				ViewState("SPLodID") = SPLodID


				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule, ApplicationEnhancement As New ApplicationEnhancement

						dt = New DataTable

						dt = cr.getPMEnhancemetApplication(grow.Cells(4).Text.ToString())

						'		Me.spTest.InnerText = dt.Rows.Count.ToString
						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString()
						lstSchedules.Add(rmasSch)

						'cr.PMInsertRMASScheduleHardShip(lstSchedules, SPLodID, appTypeID, 1, Session("user"))


						Try

							ApplicationEnhancement.Surname = dt.Rows(0).Item("SURNAME").ToString
							ApplicationEnhancement.FirstName = dt.Rows(0).Item("FIRST NAME").ToString
							ApplicationEnhancement.MiddleName = dt.Rows(0).Item("MIDDLE NAME").ToString
							ApplicationEnhancement.RSAPin = dt.Rows(0).Item("RSA_PIN").ToString
							ApplicationEnhancement.PFA = dt.Rows(0).Item("PFA").ToString
							ApplicationEnhancement.EmployerCode = dt.Rows(0).Item("EMPLOYER CODE").ToString
							ApplicationEnhancement.Sector = dt.Rows(0).Item("SECTOR").ToString
							ApplicationEnhancement.ReviewDate = dt.Rows(0).Item("PENSION REVIEW DATE").ToString
							ApplicationEnhancement.Gender = dt.Rows(0).Item("GENDER").ToString
							ApplicationEnhancement.DOE = dt.Rows(0).Item("DATE OF BIRTH").ToString
							ApplicationEnhancement.DOR = dt.Rows(0).Item("RETIREMENT DATE").ToString
							ApplicationEnhancement.RSABalance = dt.Rows(0).Item("RSA BALANCE AS AT 31 DEC 2016").ToString
							ApplicationEnhancement.CurPension = dt.Rows(0).Item("CURRENT PENSION AS AT 31 DEC 2016").ToString
							ApplicationEnhancement.CurRSABalance = dt.Rows(0).Item("RSA BALANCE AS AT SUBMISSION").ToString
							ApplicationEnhancement.EnhancedPension = dt.Rows(0).Item("ENHANCED PENSION").ToString
							ApplicationEnhancement.Frequency = dt.Rows(0).Item("FREQUENCY (MONTHLY/QUARTERLY)").ToString
							lstApplicationEnhancement.Add(ApplicationEnhancement)

						Catch ex As Exception

							Dim logerr As New Global.Logger.Logger
							logerr.FileSource = "Payment Module"
							logerr.FilePath = Server.MapPath("~/Logs/")
							logerr.Logger(ex.Message & ": Error Loading Biometric")

						End Try
						
						'generateFiles(dt)


					Else

					End If

				Next

				'MsgBox(lstApplicationEnhancement.Count.ToString)


				'Me.spTest.InnerText = lstApplicationEnhancement.Count.ToString
				'Exit Sub
				cr.PMInsertRMASScheduleHardShip(lstSchedules, SPLodID, appTypeID, 1, Session("user"))

				Dim dtApplications As New DataTable, dtColumn As DataColumn, i As Integer

				dtColumn = New DataColumn("SURNAME")
				dtApplications.Columns.Add(dtColumn)


				dtColumn = New DataColumn("FIRST NAME")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("MIDDLE NAME")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("RSA_PIN")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("PFA")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("EMPLOYER CODE")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("SECTOR")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("PENSION REVIEW DATE")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("GENDER")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("DATE OF BIRTH")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("RETIREMENT DATE")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("RSA BALANCE AS AT 31 DEC 2016")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("CURRENT PENSION AS AT 31 DEC 2016")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("RSA BALANCE AS AT SUBMISSION")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("ENHANCED PENSION")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("FREQUENCY (MONTHLY/QUARTERLY)")
				dtApplications.Columns.Add(dtColumn)

				Do While i < lstApplicationEnhancement.Count

					Dim newCustomersRow As DataRow

					newCustomersRow = dtApplications.NewRow()

					newCustomersRow("SURNAME") = lstApplicationEnhancement(i).Surname
					newCustomersRow("FIRST NAME") = lstApplicationEnhancement(i).FirstName
					newCustomersRow("MIDDLE NAME") = lstApplicationEnhancement(i).MiddleName
					newCustomersRow("RSA_PIN") = lstApplicationEnhancement(i).RSAPin
					newCustomersRow("PFA") = lstApplicationEnhancement(i).PFA
					newCustomersRow("EMPLOYER CODE") = lstApplicationEnhancement(i).EmployerCode
					newCustomersRow("SECTOR") = lstApplicationEnhancement(i).Sector
					newCustomersRow("PENSION REVIEW DATE") = lstApplicationEnhancement(i).ReviewDate
					newCustomersRow("GENDER") = lstApplicationEnhancement(i).Gender
					newCustomersRow("DATE OF BIRTH") = lstApplicationEnhancement(i).DOE
					newCustomersRow("RETIREMENT DATE") = lstApplicationEnhancement(i).DOR
					newCustomersRow("RSA BALANCE AS AT 31 DEC 2016") = lstApplicationEnhancement(i).RSABalance
					newCustomersRow("CURRENT PENSION AS AT 31 DEC 2016") = lstApplicationEnhancement(i).CurPension
					newCustomersRow("RSA BALANCE AS AT SUBMISSION") = lstApplicationEnhancement(i).CurRSABalance
					newCustomersRow("ENHANCED PENSION") = lstApplicationEnhancement(i).EnhancedPension
					newCustomersRow("FREQUENCY (MONTHLY/QUARTERLY)") = lstApplicationEnhancement(i).Frequency
					dtApplications.Rows.Add(newCustomersRow)

					i = i + 1
				Loop

				'refresh()

				Dim crr As New Core
				crr.ExtractCSV(dtApplications, "PW Enhancement")


			Else



				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")


					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule, pAmount As Double
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString
						rmasSch.Name = dt.Rows(0).Item("Name").ToString  '
						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName").ToString.Replace("&", " ").Replace("-", " ").Replace(",", " ").Replace(".", " ")
						rmasSch.PIN = grow.Cells(4).Text.ToString()


						Select Case apptype

							Case Is = 1

								rmasSch.RSABalance = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
								rmasSch.EnblocAmount = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
								rmasSch.Nationality = "NG"
								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								If dt.Rows(0).Item("txtReason").ToString = "Retirement" Then
									rmasSch.PaymentReason = "1"
								ElseIf dt.Rows(0).Item("txtReason").ToString = "Relocation" Then
									rmasSch.PaymentReason = "2"
								Else
									rmasSch.PaymentReason = "0"
								End If

							Case Is = 16

								rmasSch.RSABalance = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
								rmasSch.EnblocAmount = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
								rmasSch.Nationality = "NG"
								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								If dt.Rows(0).Item("txtReason").ToString = "Retirement" Then
									rmasSch.PaymentReason = "1"
								ElseIf dt.Rows(0).Item("txtReason").ToString = "Relocation" Then
									rmasSch.PaymentReason = "2"
								Else
									rmasSch.PaymentReason = "0"
								End If


							Case Is = 2

								Dim funtPlatform As Integer = cr.PMgetApplicationByPIN(grow.Cells(4).Text, grow.Cells(2).Text).Rows(0).Item("intFundPlatFormID")
								If funtPlatform = 1 Then
									pAmount = cr.PMgetApplicationByPIN(grow.Cells(4).Text, grow.Cells(2).Text).Rows(0).Item("Mandatory")
									rmasSch.RSABalance = (CDbl(pAmount) / 4) * 4
									rmasSch.Twenty5Percent = CDbl(pAmount) / 4

								ElseIf funtPlatform = 2 Then
									pAmount = cr.PMgetApplicationByPIN(grow.Cells(4).Text, grow.Cells(2).Text).Rows(0).Item("numApplicationAmount")
									rmasSch.RSABalance = (CDbl(pAmount) * 4)
									rmasSch.Twenty5Percent = CDbl(pAmount)
								End If
								rmasSch.DateDisengagement = CDate(dt.Rows(0).Item("dteDisengagement"))


							Case Is = 8
								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))

							Case Is = 6
								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								rmasSch.NSITFInitialAmountPaid = CDbl(dt.Rows(0).Item("numNSITFInitialAmountPaid"))
								rmasSch.NSITFRecievedToRSA = CDbl(dt.Rows(0).Item("numNSITFRecievedToRSA"))
								rmasSch.NSITFRequestedToRSA = CDbl(dt.Rows(0).Item("numNSITFRequestedToRSA"))

							Case Is = 7
								Dim dtAVCDetails As New DataTable
								dtAVCDetails = cr.PMGetInsertedTempRMASRecord(grow.Cells(2).Text.ToString())

								rmasSch.RetirementDate = Now.ToString("yyyy-MM-dd")
								rmasSch.TotalAVC = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtTotalAVCProcessed"))
								rmasSch.TotalAVCAmount = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtCurrentValue"))
								rmasSch.AVCTax = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtTaxDeduction"))
								rmasSch.NetAVCPayable = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtNetPayable"))

							Case Is = 3

								Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails
								dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
								dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
								dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
								dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
								dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
								dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
								dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
								dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
								dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
								dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")
								dtRetirementDetails.AccruedRight = dtRDetails.Rows(0).Item("numAccruedRight")
								dtRetirementDetails.RecommendedLumpSum = dtRDetails.Rows(0).Item("numRecommendedLumpSum")
								dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails.Rows(0).Item("numMonthlyDrowDown")
								rmasSch.RetirementDetails = dtRetirementDetails

								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))

								Dim date2 As Date = Date.Parse(CDate(dt.Rows(0).Item("dteDOB")))
								Dim date1 As Date = Now
								rmasSch.Age = DateDiff(DateInterval.Year, date2, date1)

								If dt.Rows(0).Item("txtReason").ToString = "Retirement" Then
									rmasSch.PaymentReason = "1"
								ElseIf dt.Rows(0).Item("txtReason").ToString = "Relocation" Then
									rmasSch.PaymentReason = "2"
								Else
									rmasSch.PaymentReason = "0"
								End If


							Case Is = 4

								Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails
								dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
								dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
								dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
								dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
								dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
								dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
								dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
								dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
								dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
								dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")

								dtRetirementDetails.InsuranceCoy = dtRDetails.Rows(0).Item("InsurerName").ToString.Replace("&", " ").Replace("-", " ").Replace(",", " ").Replace(".", " ").Replace("/", "")
								dtRetirementDetails.AnnuityCommencement = dtRDetails.Rows(0).Item("dteAnnuityCcommencementDate")
								dtRetirementDetails.Premium = dtRDetails.Rows(0).Item("numPremium")
								dtRetirementDetails.AnnuityLumpSum = dtRDetails.Rows(0).Item("numLumpSum")
								dtRetirementDetails.MonthlyAnnuity = dtRDetails.Rows(0).Item("numMonthlyAnnuity")

								rmasSch.RetirementDetails = dtRetirementDetails

								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))

							Case Is = 15

								Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails
								dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
								dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
								dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
								dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
								dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
								dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
								dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
								dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
								dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
								dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")


								dtRetirementDetails.InsuranceCoy = dtRDetails.Rows(0).Item("txtInsuranceCompanyName")
								dtRetirementDetails.AnnuityCommencement = dtRDetails.Rows(0).Item("dteAnnuityCcommencementDate")
								dtRetirementDetails.Premium = dtRDetails.Rows(0).Item("numPremium")
								dtRetirementDetails.AnnuityLumpSum = dtRDetails.Rows(0).Item("numLumpSum")
								dtRetirementDetails.MonthlyAnnuity = dtRDetails.Rows(0).Item("numMonthlyAnnuity")

								rmasSch.RetirementDetails = dtRetirementDetails

								rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
								rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))

							Case Is = 5

								Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails, dtAppdetail As New DataTable
								dtRDetails = cr.PMGetInsertedDBARecord(grow.Cells(2).Text.ToString())
								dtAppdetail = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

								dtRetirementDetails.RetirementDate = dtRDetails.Rows(0).Item("dteRetirement")
								dtRetirementDetails.DeathDate = dtRDetails.Rows(0).Item("dteDeath")
								dtRetirementDetails.AdminIssuingAuthority = dtRDetails.Rows(0).Item("txtAdminLetterAuthority")
								dtRetirementDetails.AdminIssuingDate = dtRDetails.Rows(0).Item("dteAdminLetter")
								dtRetirementDetails.AdminNOK = dtRDetails.Rows(0).Item("txtAdminNOK")
								dtRetirementDetails.InsuranceProceed = dtRDetails.Rows(0).Item("numInsuranceProceed")
								dtRetirementDetails.AccruedRight = dtRDetails.Rows(0).Item("numAccruedRight")
								dtRetirementDetails.Contribution = dtRDetails.Rows(0).Item("numContribution")
								dtRetirementDetails.InvestmentIncome = dtRDetails.Rows(0).Item("numInvestmentIncome")

								dtRetirementDetails.RSABalance = dtAppdetail.Rows(0).Item("numApplicationAmount")
								dtRetirementDetails.Remarks = "."
								rmasSch.RetirementDetails = dtRetirementDetails

							Case Else


						End Select

						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If



				Next


				If lstSchedules.Count > 0 Then

					'generate sp log batch no
					Dim SPLodID As String = cr.PMgetNextSPLogID(CInt(apptype), "L")

					'creating the rmas schedule for submission to pencom
					cr.PMInsertRMASScheduleHardShip(lstSchedules, SPLodID, apptype, 1, Session("user"))


					Dim typeID As Integer
					ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
					typeID = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

					Response.Redirect(String.Format("frmRMAS.aspx?TypeID={0}&DSent={1}", Server.UrlEncode(typeID), Server.UrlEncode(Me.txtDateSubmission.Text)))


				Else
				End If




			End If



		Catch ex As Exception

			'MsgBox("" & ex.Message)

			'Dim logerr As New Global.Logger.Logger
			'logerr.FileSource = "Payment Module"
			'logerr.FilePath = "C:\BK\PMLive\Payment Module\Logs"
			'logerr.Logger(ex.Message & ": Error Loading Biometric")

		End Try

     End Sub
     Protected Sub calDateSubmission_SelectionChanged(sender As Object, e As EventArgs) Handles calDateSubmission.SelectionChanged

          Me.calDateSubmission_PopupControlExtender.Commit(Me.calDateSubmission.SelectedDate)

     End Sub

     Protected Sub btnNonSubmissionSchedule_Click(sender As Object, e As EventArgs) Handles btnNonSubmissionSchedule.Click

		Dim cb As CheckBox, chk As Integer = 0, cr As New Core, brokers As New List(Of String), dt As New DataTable, lstSchedules As New List(Of RMASSchedule), apptype As String = "", refCode As String = ""

          Try

			ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
			Dim appTypeID As Integer = (CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))

			If appTypeID = 2 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule, pAmount As Double
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)
						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.Sector = dt.Rows(0).Item("txtSector").ToString
						rmasSch.PaymentReason = dt.Rows(0).Item("txtreason").ToString
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName").ToString
						rmasSch.Name = dt.Rows(0).Item("Name").ToString
						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.PIN = grow.Cells(4).Text.ToString()

						pAmount = cr.PMgetApplicationByPIN(grow.Cells(4).Text, grow.Cells(2).Text).Rows(0).Item("Mandatory")
						rmasSch.RSABalance = (CDbl(pAmount) / 4) * 4
						rmasSch.Twenty5Percent = CDbl(pAmount) / 4

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						rmasSch.DateDisengagement = CDate(dt.Rows(0).Item("dteDisengagement"))
						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next

			ElseIf appTypeID = 11 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable

						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.Sector = dt.Rows(0).Item("txtSector").ToString
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName").ToString
						rmasSch.Name = dt.Rows(0).Item("Name").ToString
						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.PIN = grow.Cells(4).Text.ToString()
						rmasSch.RSABalance = (CDbl(dt.Rows(0).Item("numRSABalance")))
						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next


			ElseIf appTypeID = 1 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)
						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If
						rmasSch.Sector = dt.Rows(0).Item("txtSector").ToString
						rmasSch.PaymentReason = dt.Rows(0).Item("txtreason").ToString
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName").ToString
						rmasSch.Name = dt.Rows(0).Item("Name").ToString
						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.PIN = grow.Cells(4).Text.ToString()

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))

						rmasSch.RSABalance = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
						rmasSch.EnblocAmount = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
						rmasSch.Nationality = "NG"
						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						If dt.Rows(0).Item("txtReason").ToString = "Retirement" Then
							rmasSch.PaymentReason = "1"
						ElseIf dt.Rows(0).Item("txtReason").ToString = "Relocation" Then
							rmasSch.PaymentReason = "2"
						Else
							rmasSch.PaymentReason = "0"
						End If

						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next


			ElseIf appTypeID = 16 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)
						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If
						rmasSch.Sector = dt.Rows(0).Item("txtSector").ToString
						rmasSch.PaymentReason = dt.Rows(0).Item("txtreason").ToString
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName").ToString
						rmasSch.Name = dt.Rows(0).Item("Name").ToString
						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.PIN = grow.Cells(4).Text.ToString()

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))

						rmasSch.RSABalance = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
						rmasSch.EnblocAmount = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
						rmasSch.Nationality = "NG"
						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						If dt.Rows(0).Item("txtReason").ToString = "Retirement" Then
							rmasSch.PaymentReason = "1"
						ElseIf dt.Rows(0).Item("txtReason").ToString = "Relocation" Then
							rmasSch.PaymentReason = "2"
						Else
							rmasSch.PaymentReason = "0"
						End If


						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next


			ElseIf appTypeID = 7 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)
						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If
						rmasSch.Sector = dt.Rows(0).Item("txtSector").ToString
						rmasSch.PaymentReason = dt.Rows(0).Item("txtreason").ToString
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName").ToString
						rmasSch.Name = dt.Rows(0).Item("Name").ToString
						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.PIN = grow.Cells(4).Text.ToString()

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))

						rmasSch.RSABalance = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
						rmasSch.EnblocAmount = (CDbl(dt.Rows(0).Item("numApplicationAmount")))
						rmasSch.Nationality = "NG"
						rmasSch.RetirementDate = Now.Date
						If dt.Rows(0).Item("txtReason").ToString = "Retirement" Then
							rmasSch.PaymentReason = "1"
						ElseIf dt.Rows(0).Item("txtReason").ToString = "Relocation" Then
							rmasSch.PaymentReason = "2"
						Else
							rmasSch.PaymentReason = "0"
						End If

						Dim dtAVCDetails As New DataTable
						dtAVCDetails = cr.PMGetInsertedTempRMASRecord(grow.Cells(2).Text.ToString())
						rmasSch.RetirementDate = Now.ToString("yyyy-MM-dd")
						rmasSch.TotalAVC = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtTotalAVCProcessed"))
						rmasSch.TotalAVCAmount = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtCurrentValue"))
						rmasSch.AVCTax = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtTaxDeduction"))
						rmasSch.NetAVCPayable = Convert.ToDecimal(dtAVCDetails.Rows(0).Item("txtNetPayable"))

						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next


			ElseIf appTypeID = 3 Then


				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.Name = dt.Rows(0).Item("Name").ToString  '
						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName")
						rmasSch.PIN = grow.Cells(4).Text.ToString()


						Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails

						dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
						dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
						dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
						dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
						dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
						dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
						dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
						dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
						dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
						dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")
						dtRetirementDetails.AccruedRight = dtRDetails.Rows(0).Item("numAccruedRight")
						dtRetirementDetails.RecommendedLumpSum = dtRDetails.Rows(0).Item("numRecommendedLumpSum")
						dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails.Rows(0).Item("numMonthlyDrowDown")

						dtRetirementDetails.SalaryStructure = dtRDetails.Rows(0).Item("txtSalaryStructure").ToString
						dtRetirementDetails.GradeLevel = dtRDetails.Rows(0).Item("txtGradeLevel").ToString
						dtRetirementDetails.SalaryStep = dtRDetails.Rows(0).Item("txtStep").ToString
						dtRetirementDetails.ReviewedSalary = dtRDetails.Rows(0).Item("numReviewedSalary")

						dtRetirementDetails.PensionArrears = dtRDetails.Rows(0).Item("numPensionArrears")
						dtRetirementDetails.ArrearsMonths = dtRDetails.Rows(0).Item("numArrearsMonths")
						dtRetirementDetails.Frequency = dtRDetails.Rows(0).Item("intFrequency")
						dtRetirementDetails.ProgrammingDate = dtRDetails.Rows(0).Item("dteProgramming")

						rmasSch.RetirementDetails = dtRetirementDetails

						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))


						rmasSch.RetirementDetails = dtRetirementDetails

						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))


						Dim rName As String = dt.Rows(0).Item("Name").ToString.Replace("  ", "|")

						'MsgBox("" & rName)
						'Exit Sub

						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString

						rmasSch.Surname = rName.Split("|")(0)
						rmasSch.MiddleName = rName.Split("|")(1)


						If rName.Split("|").Length > 2 Then

							rmasSch.OtherName = rName.Split("|")(2)

						Else

							rmasSch.OtherName = ""

						End If

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then

							rmasSch.Genderr = "Male"

						Else

							rmasSch.Genderr = "Female"

						End If
			
						rmasSch.Sector = dt.Rows(0).Item("txtSector")

						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next


			ElseIf appTypeID = 4 Then


				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						'txtReferenceApplicationCode
						refCode = dt.Rows(0).Item("txtReferenceApplicationCode")
						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.Name = dt.Rows(0).Item("Name").ToString  '
						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName")
						rmasSch.PIN = grow.Cells(4).Text.ToString()

						Dim dtRDetails, dtRDetails2 As New DataTable, dtRetirementDetails As New RetirementDetails

						If dt.Rows(0).Item("txtReferenceApplicationCode").ToString <> "" Then

							dtRDetails = cr.PMGetInsertedRetirementRecord(dt.Rows(0).Item("txtReferenceApplicationCode").ToString)
							dtRDetails2 = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())


							If dtRDetails.Rows.Count > 0 Then

								dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails.Rows(0).Item("numMonthlyDrowDown")
								dtRetirementDetails.Premium = dtRDetails.Rows(0).Item("numRSABalance") - dtRDetails.Rows(0).Item("numMonthlyDrowDown")
								dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
								dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
								dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
								dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
								dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
								dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
								dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
								dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
								dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")
								dtRetirementDetails.AccruedRight = dtRDetails.Rows(0).Item("numAccruedRight")
								dtRetirementDetails.RecommendedLumpSum = dtRDetails.Rows(0).Item("numRecommendedLumpSum")
								dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails.Rows(0).Item("numMonthlyDrowDown")
								dtRetirementDetails.SalaryStructure = dtRDetails.Rows(0).Item("txtSalaryStructure").ToString
								dtRetirementDetails.GradeLevel = dtRDetails.Rows(0).Item("txtGradeLevel").ToString
								dtRetirementDetails.SalaryStep = dtRDetails.Rows(0).Item("txtStep").ToString
								dtRetirementDetails.FinalSalary = dtRDetails.Rows(0).Item("FinalSalary")
								dtRetirementDetails.ReviewedSalary = dtRDetails.Rows(0).Item("numReviewedSalary")
								dtRetirementDetails.PensionArrears = dtRDetails.Rows(0).Item("numPensionArrears")
								dtRetirementDetails.ArrearsMonths = dtRDetails.Rows(0).Item("numArrearsMonths")
								dtRetirementDetails.Frequency = dtRDetails.Rows(0).Item("intFrequency")
								dtRetirementDetails.ProgrammingDate = dtRDetails.Rows(0).Item("dteProgramming")
								dtRetirementDetails.MonthlyAnnuity = dtRDetails2.Rows(0).Item("numMonthlyAnnuity")
								dtRetirementDetails.MonthlyBuffer = dtRDetails.Rows(0).Item("MonthBuffer")
								dtRetirementDetails.TotalUpfront = dtRDetails.Rows(0).Item("TotalUpfront")

							Else

								dtRetirementDetails.MonthlyProgramedDrawndown = 0
								dtRetirementDetails.Premium = 0

								dtRetirementDetails.BasicSalary = 0
								dtRetirementDetails.HouseRent = 0
								dtRetirementDetails.Transport = 0
								dtRetirementDetails.Utility = 0
								dtRetirementDetails.ConsolidatedAallowance = 0
								dtRetirementDetails.ConsolidatedSalary = 0
								dtRetirementDetails.MonthlyTotal = 0
								dtRetirementDetails.AnnualTotalEmolumentAdj = 0
								dtRetirementDetails.RSABalance = 0
								dtRetirementDetails.AccruedRight = 0
								dtRetirementDetails.RecommendedLumpSum = 0
								dtRetirementDetails.MonthlyProgramedDrawndown = 0
								dtRetirementDetails.SalaryStructure = 0
								dtRetirementDetails.GradeLevel = 0
								dtRetirementDetails.SalaryStep = 0
								dtRetirementDetails.FinalSalary = 0
								dtRetirementDetails.ReviewedSalary = 0
								dtRetirementDetails.PensionArrears = 0
								dtRetirementDetails.ArrearsMonths = 0
								dtRetirementDetails.Frequency = 0
								dtRetirementDetails.ProgrammingDate = Now.Date
								dtRetirementDetails.MonthlyAnnuity = dtRDetails2.Rows(0).Item("numMonthlyAnnuity")
								dtRetirementDetails.MonthlyBuffer = 0
								dtRetirementDetails.TotalUpfront = 0

							End If
							



						Else

							dtRDetails2 = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
							dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails2.Rows(0).Item("numMonthlyDrowDown")
							dtRetirementDetails.Premium = dtRDetails2.Rows(0).Item("numPremium")

						End If



						rmasSch.RetirementDetails = dtRetirementDetails

						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))


						rmasSch.RetirementDetails = dtRetirementDetails

						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))


						Dim rName As String = dt.Rows(0).Item("Name").ToString.Replace("  ", "|")

						rmasSch.PIN = dt.Rows(0).Item("txtPIN").ToString

						rmasSch.Surname = rName.Split("|")(0)
						rmasSch.MiddleName = rName.Split("|")(1)

						If rName.Split("|").Length > 2 Then

							rmasSch.OtherName = rName.Split("|")(2)

						Else

							rmasSch.OtherName = ""

						End If

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then

							rmasSch.Genderr = "Male"

						Else

							rmasSch.Genderr = "Female"

						End If



						rmasSch.Sector = dt.Rows(0).Item("txtSector")





						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next

			ElseIf appTypeID = 14 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.Name = dt.Rows(0).Item("Name").ToString  '
						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName")
						rmasSch.PIN = grow.Cells(4).Text.ToString()


						Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails
						dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
						dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
						dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
						dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
						dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
						dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
						dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
						dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
						dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
						dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")
						dtRetirementDetails.AccruedRight = dtRDetails.Rows(0).Item("numAccruedRight")
						dtRetirementDetails.RecommendedLumpSum = dtRDetails.Rows(0).Item("numRecommendedLumpSum")
						dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails.Rows(0).Item("numMonthlyDrowDown")

						rmasSch.RetirementDetails = dtRetirementDetails

						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))


						rmasSch.RetirementDetails = dtRetirementDetails

						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))

						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next
				
			ElseIf appTypeID = 15 Then

				For Each grow As GridViewRow In Me.gridProcessing.Rows

					cb = grow.FindControl("chkProcessing")

					If cb.Checked = True Then

						Dim rmasSch As New RMASSchedule
						dt = New DataTable
						dt = cr.PMgetApplicationByCode(grow.Cells(2).Text.ToString())

						apptype = dt.Rows(0).Item("fkiAppTypeId").ToString

						rmasSch.Name = dt.Rows(0).Item("Name").ToString  '
						rmasSch.ApplicationCode = grow.Cells(2).Text.ToString
						rmasSch.DateSent = CDate(Me.txtDateSubmission.Text)

						If dt.Rows(0).Item("txtSex").ToString.Trim = "M" Then
							rmasSch.Gender = 1
						ElseIf dt.Rows(0).Item("txtSex").ToString.Trim = "F" Then
							rmasSch.Gender = 0
						End If

						rmasSch.DOB = CDate(dt.Rows(0).Item("dteDOB"))
						rmasSch.DateConfirm = Now.Date
						rmasSch.Employercode = dt.Rows(0).Item("txtEmployerCode")
						rmasSch.EmployerName = dt.Rows(0).Item("txtEmployerName")
						rmasSch.PIN = grow.Cells(4).Text.ToString()

						'Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails
						'dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
						'dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
						'dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
						'dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
						'dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
						'dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
						'dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
						'dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
						'dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
						'dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")
						'dtRetirementDetails.AccruedRight = dtRDetails.Rows(0).Item("numAccruedRight")
						'dtRetirementDetails.RecommendedLumpSum = dtRDetails.Rows(0).Item("numRecommendedLumpSum")
						'dtRetirementDetails.MonthlyProgramedDrawndown = dtRDetails.Rows(0).Item("numMonthlyDrowDown")

						Dim dtRDetails As New DataTable, dtRetirementDetails As New RetirementDetails
						dtRDetails = cr.PMGetInsertedRetirementRecord(grow.Cells(2).Text.ToString())
						dtRetirementDetails.BasicSalary = dtRDetails.Rows(0).Item("numBasicSalary")
						dtRetirementDetails.HouseRent = dtRDetails.Rows(0).Item("numHouseRent")
						dtRetirementDetails.Transport = dtRDetails.Rows(0).Item("numTransport")
						dtRetirementDetails.Utility = dtRDetails.Rows(0).Item("numUtility")
						dtRetirementDetails.ConsolidatedAallowance = dtRDetails.Rows(0).Item("numConsolidatedAallowance")
						dtRetirementDetails.ConsolidatedSalary = dtRDetails.Rows(0).Item("numConsolidatedSalary")
						dtRetirementDetails.MonthlyTotal = dtRDetails.Rows(0).Item("numMonthlyTotal")
						dtRetirementDetails.AnnualTotalEmolumentAdj = dtRDetails.Rows(0).Item("numAnnualTotalEmolumentAdj")
						dtRetirementDetails.RSABalance = dtRDetails.Rows(0).Item("numRSABalance")
						dtRetirementDetails.InsuranceCoy = dtRDetails.Rows(0).Item("InsurerName").ToString.Replace("&", " ").Replace("-", " ").Replace(",", " ").Replace(".", " ").Replace("/", "")
						dtRetirementDetails.AnnuityCommencement = dtRDetails.Rows(0).Item("dteAnnuityCcommencementDate")
						dtRetirementDetails.Premium = dtRDetails.Rows(0).Item("numPremium")
						dtRetirementDetails.AnnuityLumpSum = dtRDetails.Rows(0).Item("numLumpSum")
						dtRetirementDetails.MonthlyAnnuity = dtRDetails.Rows(0).Item("numMonthlyAnnuity")
						rmasSch.RetirementDetails = dtRetirementDetails
						rmasSch.RetirementDate = CDate(dt.Rows(0).Item("dteDOR"))
						rmasSch.EnblocAmount = CDbl(dt.Rows(0).Item("numApplicationAmount"))

						lstSchedules.Add(rmasSch)

					ElseIf cb.Checked = False Then

					End If

				Next

			End If



			If lstSchedules.Count > 0 And Not IsNothing(Session("user")) = True Then

				'generate sp log batch no
				Dim SPLodID As String = cr.PMgetNextSPLogID(CInt(apptype), "L")

				'generate sp log batch no
				cr.PMInsertRMASScheduleHardShip(lstSchedules, SPLodID, apptype, 2, Session("user"))

			Else

				Response.Redirect("login.aspx")

			End If

			If appTypeID = 3 Then


				getNewPWExportData(lstSchedules, appTypeID)

				Exit Sub

			ElseIf appTypeID = 4 Then

				getNewANNExportData(lstSchedules, appTypeID, refCode)

				Exit Sub

			Else

			End If

			'saving the application to excel
			Dim dtApplications As New DataTable, dtColumn As DataColumn




			dtColumn = New DataColumn("Name")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("Ex-Employer")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("PFA")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("PIN")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DOR")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("GENDER")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DOB")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("AGE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("REASON")
			dtApplications.Columns.Add(dtColumn)

			If appTypeID = 2 Then

				dtColumn = New DataColumn("PERIOD")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("RSA BALANCE")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("Twenty5Percent")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("DISMISSAL/SEVERANCE LETTER")
				dtApplications.Columns.Add(dtColumn)

			ElseIf appTypeID = 1 Then

				dtColumn = New DataColumn("Amount")
				dtApplications.Columns.Add(dtColumn)

				dtColumn = New DataColumn("SEVERANCE LETTER")
				dtApplications.Columns.Add(dtColumn)

			End If


			dtColumn = New DataColumn("STATEMENT OF ACCT")
			dtApplications.Columns.Add(dtColumn)


			dtColumn = New DataColumn("SECTOR")
			dtApplications.Columns.Add(dtColumn)

			Dim i As Integer = 0

			Do While i < lstSchedules.Count

				Dim newCustomersRow As DataRow
				newCustomersRow = dtApplications.NewRow()
				newCustomersRow("Name") = lstSchedules(i).Name
				newCustomersRow("Ex-Employer") = lstSchedules(i).EmployerName
				newCustomersRow("PFA") = "LEADWAY PENSURE"
				newCustomersRow("PIN") = lstSchedules(i).PIN

				newCustomersRow("DOR") = lstSchedules(i).DateDisengagement.ToString("yyyy-MM-dd")


				If lstSchedules(i).Genderr = "M" Then
					newCustomersRow("GENDER") = "Male"
				Else
					newCustomersRow("GENDER") = "Female"
				End If
				'
				newCustomersRow("DOB") = lstSchedules(i).DOB.ToString("yyyy-MM-dd")
				newCustomersRow("AGE") = DateDiff(DateInterval.Year, lstSchedules(i).DOB, Now.Date)

				newCustomersRow("REASON") = lstSchedules(i).PaymentReason

				If appTypeID = 1 Then

					newCustomersRow("Amount") = lstSchedules(i).EnblocAmount
					newCustomersRow("SEVERANCE LETTER") = "Yes"

				ElseIf appTypeID = 2 Then

					newCustomersRow("PERIOD") = DateDiff(DateInterval.Month, lstSchedules(i).DateDisengagement, Now.Date)
					newCustomersRow("RSA BALANCE") = lstSchedules(i).RSABalance
					newCustomersRow("Twenty5Percent") = lstSchedules(i).Twenty5Percent
					newCustomersRow("DISMISSAL/SEVERANCE LETTER") = "Yes"

				End If

				newCustomersRow("STATEMENT OF ACCT") = "Yes"

				newCustomersRow("SECTOR") = lstSchedules(i).Sector

				dtApplications.Rows.Add(newCustomersRow)

				i = i + 1
			Loop


			ViewState("ApplicationList") = dtApplications
			If IsNothing(ViewState("ApplicationList")) = False Then

				Dim crr As New Core
				crr.ExtractCSV(ViewState("ApplicationList"), "HardCopyApproval")

			Else
			End If


			dt = New DataTable
			refresh()
			populateDocuments(dt)


		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs/")
			logerr.Logger(ex.Message & ": Error Generating LPW Schedule")

		End Try

	End Sub

	Protected Sub getNewPWExportData(lstSchedules As List(Of RMASSchedule), appTypeID As Integer)


		Try

			Dim dtApplications As New DataTable, dtColumn As DataColumn

			dtColumn = New DataColumn("RSA_PIN")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("SURNAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("FIRST NAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("MIDDLE NAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("GENDER")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DATE OF BIRTH")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DATE OF RETIREMENT")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("EMPLOYER NAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("EMPLOYER CODE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("SECTOR")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("SALARY STRUCTURE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("GRADE LEVEL")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("STEP")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("PFA")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DATE OF PROGRAMMING")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("RSA BALANCE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("FINAL SALARY")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("REVIEWED FINAL SALARY")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("LUMPSUM")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("PENSION")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("PENSION ARREARS")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("NO OF MONTHS")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("FREQUENCY")
			dtApplications.Columns.Add(dtColumn)


			Dim i As Integer = 0

			Do While i < lstSchedules.Count

				Dim newCustomersRow As DataRow
				newCustomersRow = dtApplications.NewRow()
				newCustomersRow("RSA_PIN") = lstSchedules(i).PIN
				newCustomersRow("SURNAME") = lstSchedules(i).Surname
				newCustomersRow("FIRST NAME") = lstSchedules(i).OtherName
				newCustomersRow("MIDDLE NAME") = lstSchedules(i).MiddleName

				newCustomersRow("GENDER") = lstSchedules(i).Genderr
				newCustomersRow("DATE OF BIRTH") = lstSchedules(i).DOB
				newCustomersRow("DATE OF RETIREMENT") = lstSchedules(i).RetirementDate
				newCustomersRow("EMPLOYER NAME") = lstSchedules(i).EmployerName
				newCustomersRow("EMPLOYER CODE") = lstSchedules(i).Employercode

				newCustomersRow("SECTOR") = lstSchedules(i).Sector

				newCustomersRow("SALARY STRUCTURE") = lstSchedules(i).RetirementDetails.SalaryStructure

				newCustomersRow("GRADE LEVEL") = lstSchedules(i).RetirementDetails.GradeLevel

				newCustomersRow("STEP") = lstSchedules(i).RetirementDetails.SalaryStep
				
				newCustomersRow("PFA") = "LEADWAY PENSURE PFA"
				newCustomersRow("DATE OF PROGRAMMING") = lstSchedules(i).RetirementDetails.ProgrammingDate

				newCustomersRow("RSA BALANCE") = lstSchedules(i).RetirementDetails.RSABalance
				
				newCustomersRow("FINAL SALARY") = lstSchedules(i).RetirementDetails.AnnualTotalEmolumentAdj

				newCustomersRow("REVIEWED FINAL SALARY") = lstSchedules(i).RetirementDetails.ReviewedSalary

				newCustomersRow("LUMPSUM") = lstSchedules(i).RetirementDetails.RecommendedLumpSum

				newCustomersRow("PENSION") = lstSchedules(i).RetirementDetails.MonthlyProgramedDrawndown

				newCustomersRow("PENSION ARREARS") = lstSchedules(i).RetirementDetails.PensionArrears
				
				newCustomersRow("NO OF MONTHS") = lstSchedules(i).RetirementDetails.ArrearsMonths

				newCustomersRow("FREQUENCY") = lstSchedules(i).RetirementDetails.Frequency
				

				dtApplications.Rows.Add(newCustomersRow)

				i = i + 1
			Loop

			ViewState("ApplicationList") = dtApplications
			If IsNothing(ViewState("ApplicationList")) = False Then

				Dim crr As New Core
				crr.ExtractCSV(ViewState("ApplicationList"), "HardCopyApproval")

			Else
			End If

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs/")
			logerr.Logger(ex.Message & ": Error Generating LPW Schedule")

		End Try
	End Sub



	Protected Sub getNewANNExportData(lstSchedules As List(Of RMASSchedule), appTypeID As Integer, RefNo As String)


		Try

			Dim dtApplications As New DataTable, dtColumn As DataColumn

			dtColumn = New DataColumn("RSA_PIN")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("SURNAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("FIRST NAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("MIDDLE NAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("GENDER")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DATE OF BIRTH")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DATE OF RETIREMENT")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("EMPLOYER NAME")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("EMPLOYER CODE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("SECTOR")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("SALARY STRUCTURE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("GRADE LEVEL")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("STEP")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("PFA")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("DATE OF PROGRAMMING")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("RSA BALANCE")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("FINAL SALARY")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("REVIEWED FINAL SALARY")
			dtApplications.Columns.Add(dtColumn)



			If RefNo = "" Then
				dtColumn = New DataColumn("LUMPSUM")
				dtApplications.Columns.Add(dtColumn)
			Else

				dtColumn = New DataColumn("RESIDUAL LUMPSUM")
				dtApplications.Columns.Add(dtColumn)

			End If

			dtColumn = New DataColumn("PENSION")
			dtApplications.Columns.Add(dtColumn)



			If RefNo = "" Then

				dtColumn = New DataColumn("PENSION ARREARS")
				dtApplications.Columns.Add(dtColumn)


				dtColumn = New DataColumn("NO OF MONTHS ARREARS")
				dtApplications.Columns.Add(dtColumn)

			Else

				

			End If

			




			dtColumn = New DataColumn("ONE MONTH BUFFER PENSION")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("TOTAL UPFRONT")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("TOTAL PREMIUM")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("ANNUITY AMOUNT")
			dtApplications.Columns.Add(dtColumn)

			dtColumn = New DataColumn("FREQUENCY")
			dtApplications.Columns.Add(dtColumn)


			Dim i As Integer = 0

			Do While i < lstSchedules.Count

				Dim newCustomersRow As DataRow
				newCustomersRow = dtApplications.NewRow()
				newCustomersRow("RSA_PIN") = lstSchedules(i).PIN
				newCustomersRow("SURNAME") = lstSchedules(i).Surname
				newCustomersRow("FIRST NAME") = lstSchedules(i).OtherName
				newCustomersRow("MIDDLE NAME") = lstSchedules(i).MiddleName

				newCustomersRow("GENDER") = lstSchedules(i).Genderr
				newCustomersRow("DATE OF BIRTH") = lstSchedules(i).DOB
				newCustomersRow("DATE OF RETIREMENT") = lstSchedules(i).RetirementDate
				newCustomersRow("EMPLOYER NAME") = lstSchedules(i).EmployerName
				newCustomersRow("EMPLOYER CODE") = lstSchedules(i).Employercode

				newCustomersRow("SECTOR") = lstSchedules(i).Sector

				newCustomersRow("SALARY STRUCTURE") = lstSchedules(i).RetirementDetails.SalaryStructure

				newCustomersRow("GRADE LEVEL") = lstSchedules(i).RetirementDetails.GradeLevel

				newCustomersRow("STEP") = lstSchedules(i).RetirementDetails.SalaryStep

				newCustomersRow("PFA") = "LEADWAY PENSURE PFA"
				newCustomersRow("DATE OF PROGRAMMING") = lstSchedules(i).RetirementDetails.ProgrammingDate

				newCustomersRow("RSA BALANCE") = lstSchedules(i).RetirementDetails.RSABalance

				newCustomersRow("FINAL SALARY") = lstSchedules(i).RetirementDetails.AnnualTotalEmolumentAdj

				newCustomersRow("REVIEWED FINAL SALARY") = lstSchedules(i).RetirementDetails.ReviewedSalary


				If RefNo = "" Then
					
					newCustomersRow("LUMPSUM") = lstSchedules(i).RetirementDetails.RecommendedLumpSum
				Else

					newCustomersRow("RESIDUAL LUMPSUM") = lstSchedules(i).RetirementDetails.RecommendedLumpSum

				End If




				newCustomersRow("PENSION") = lstSchedules(i).RetirementDetails.MonthlyProgramedDrawndown


				If RefNo = "" Then


					newCustomersRow("PENSION ARREARS") = lstSchedules(i).RetirementDetails.PensionArrears

					newCustomersRow("NO OF MONTHS ARREARS") = lstSchedules(i).RetirementDetails.ArrearsMonths

				Else


				End If





				newCustomersRow("ONE MONTH BUFFER PENSION") = lstSchedules(i).RetirementDetails.MonthlyBuffer

				newCustomersRow("TOTAL UPFRONT") = lstSchedules(i).RetirementDetails.TotalUpfront

				newCustomersRow("TOTAL PREMIUM") = lstSchedules(i).RetirementDetails.Premium

				newCustomersRow("ANNUITY AMOUNT") = lstSchedules(i).RetirementDetails.MonthlyAnnuity

				newCustomersRow("FREQUENCY") = lstSchedules(i).RetirementDetails.Frequency


				dtApplications.Rows.Add(newCustomersRow)

				i = i + 1
			Loop

			ViewState("ApplicationList") = dtApplications

			If IsNothing(ViewState("ApplicationList")) = False Then

				Dim crr As New Core
				crr.ExtractCSV(ViewState("ApplicationList"), "HardCopyApproval")

			Else
			End If

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = Server.MapPath("~/Logs/")
			logerr.Logger(ex.Message & ": Error Generating LPW Schedule")

		End Try
	End Sub





     Protected Sub btnAppCommentAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnAppCommentAdd.Click
		Dim cr As New Core

		cr.PMUpdateApplicationComment(Me.txtApplicationComment.Text, Me.txtApplicationID.Text, Session("user"), 1, 1)
          txtApplicationComment.Text = ""
          refreshCommentList(txtApplicationID.Text)
          'Me.mpAppComments.Show()
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

     Protected Sub btnSNR_Click(sender As Object, e As ImageClickEventArgs) Handles btnSNR.Click

          
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
                    '  MsgBox("" & ex.Message)
               End Try

          Else
               ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
          End If


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
		newSNRow("numTotalContribution") = dtApplication.Rows(0).Item("numRSABalance")
		newSNRow("Passport") = cr.PMgetParticipantPassport(pin)
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

	Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

	End Sub
End Class
