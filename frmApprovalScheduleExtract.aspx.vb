Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Partial Class frmApprovalScheduleExtract
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable

     Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged
          Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)
     End Sub

     Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
          Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
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
          '   MyApplicateionReset(dtt)

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

     Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click
          Try

               Dim apptypeID As Integer = getApprovalType(Me.ddApplicationType.SelectedValue)
               If apptypeID = 3 Then
				BindFieldPW()

               ElseIf apptypeID = 4 Then
				BindFieldAnnuity()

			ElseIf apptypeID = 15 Then
				BindFieldAnnuity()
               Else
                    BindField()
               End If

               getBatchApplications(LTrim(RTrim(Me.txtPencomBatch.Text)), "C", apptypeID)

          Catch ex As Exception

          End Try

     End Sub
     'getting all the types of benefit application types
     Public Function getApprovalTypeID(typeName As String) As Integer


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
     Private Sub getBatchApplications(BatchApprovalCode As String, txtStatus As String, apptypeID As Integer)

          Dim cr As New Core, dt As New DataTable

          dt = cr.PMgetPencomApprovalBatchApplication(BatchApprovalCode, txtStatus, apptypeID)
          ViewState("BatchApprovalCode") = BatchApprovalCode
          ViewState("BatchApplications") = dt

          BindGridApplication(dt, apptypeID)


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

          Dim bfieldApplicationDate As New BoundField()
          bfieldApplicationDate.HeaderText = "Application Date"
          bfieldApplicationDate.DataField = "dteApplicationDate"
          bfieldApplicationDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldApplicationDate)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldValueDate)

          Dim bfieldAmountToPay As New BoundField()
          bfieldAmountToPay.HeaderText = "Amount ToPay"
          bfieldAmountToPay.DataField = "AmountToPay"
          bfieldAmountToPay.DataFormatString = "{0:N}"
          gridApplications.Columns.Add(bfieldAmountToPay)

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

          Dim bfieldApplicationDate As New BoundField()
          bfieldApplicationDate.HeaderText = "Application Date"
          bfieldApplicationDate.DataField = "dteApplicationDate"
          bfieldApplicationDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldApplicationDate)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldValueDate)

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

          Dim bfieldApplicationDate As New BoundField()
          bfieldApplicationDate.HeaderText = "Application Date"
          bfieldApplicationDate.DataField = "dteApplicationDate"
          bfieldApplicationDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldApplicationDate)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridApplications.Columns.Add(bfieldValueDate)

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

     Protected Sub BindGridApplication(dt As DataTable, TypeID As Integer)

          Dim dtApprovalPINs As New DataTable, dtColumn As New DataColumn, i As Integer

          Try

          If IsNothing(ViewState("BatchApplications")) = False Then

               dtColumn = New DataColumn("txtApplicationCode")
               dtApprovalPINs.Columns.Add(dtColumn)

               dtColumn = New DataColumn("txtPIN")
               dtApprovalPINs.Columns.Add(dtColumn)

               dtColumn = New DataColumn("txtFullName")
               dtApprovalPINs.Columns.Add(dtColumn)

               dtColumn = New DataColumn("txtEmployerName")
               dtApprovalPINs.Columns.Add(dtColumn)

               dtColumn = New DataColumn("dteApplicationDate")
               dtApprovalPINs.Columns.Add(dtColumn)

               dtColumn = New DataColumn("ValueDate")
               dtApprovalPINs.Columns.Add(dtColumn)

               If TypeID = 3 Then


                    dtColumn = New DataColumn("LumpSumToPay")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    dtColumn = New DataColumn("PensionToPay")
                    dtApprovalPINs.Columns.Add(dtColumn)

                    dtColumn = New DataColumn("ArrearsToPay")
                    dtApprovalPINs.Columns.Add(dtColumn)


                    ElseIf TypeID = 4 Then


                         dtColumn = New DataColumn("LumpSumToPay")
                         dtApprovalPINs.Columns.Add(dtColumn)

                         dtColumn = New DataColumn("AnnuityToPay")
					dtApprovalPINs.Columns.Add(dtColumn)


				ElseIf TypeID = 15 Then


					dtColumn = New DataColumn("LumpSumToPay")
					dtApprovalPINs.Columns.Add(dtColumn)

					dtColumn = New DataColumn("AnnuityToPay")
					dtApprovalPINs.Columns.Add(dtColumn)

                     
                    Else
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
               newCustomersRow("dteApplicationDate") = Convert.ToDateTime(dt.Rows(i).Item("dteApplicationDate")).ToString("yyyy-MM-dd")
               newCustomersRow("ValueDate") = Convert.ToDateTime(dt.Rows(i).Item("ValueDate")).ToString("yyyy-MM-dd")
               If TypeID = 3 Then

                         newCustomersRow("LumpSumToPay") = dt.Rows(i).Item("LumpSumToPay")
                         newCustomersRow("PensionToPay") = dt.Rows(i).Item("PensionToPay")
                         newCustomersRow("ArrearsToPay") = dt.Rows(i).Item("ArrearsToPay")


                    ElseIf TypeID = 4 Then

                         newCustomersRow("LumpSumToPay") = dt.Rows(i).Item("LumpSumToPay")
					newCustomersRow("AnnuityToPay") = dt.Rows(i).Item("AnnuityToPay")

				ElseIf TypeID = 15 Then

					newCustomersRow("LumpSumToPay") = dt.Rows(i).Item("LumpSumToPay")
					newCustomersRow("AnnuityToPay") = dt.Rows(i).Item("AnnuityToPay")

                    Else
                         newCustomersRow("AmountToPay") = dt.Rows(i).Item("AmountToPay")

                    End If


               '
               dtApprovalPINs.Rows.Add(newCustomersRow)

               i = i + 1
          Loop

			If dtApprovalPINs.Rows.Count > 10 Then
				Me.pnlGrid.Height = Nothing
			Else
			End If

          gridApplications.DataSource = dtApprovalPINs
			gridApplications.DataBind()

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try

     End Sub

     Protected Sub btnExportSchedule_Click(sender As Object, e As EventArgs) Handles btnExportSchedule.Click

          Dim apptypeID As Integer = getApprovalTypeID(Me.ddApplicationType.SelectedValue)
		Dim extractBatch As String = ""
		extractBatch = UCase(LTrim(RTrim(Me.txtPencomBatch.Text))) & Year(Now.Date).ToString & Month(Now.Date).ToString & Day(Now.Date).ToString & Second(Now).ToString
		Try

		
          For Each grow As GridViewRow In Me.gridApplications.Rows
               Dim cb As CheckBox = TryCast(grow.Cells(0).FindControl("ChkApprovalConfirm"), CheckBox)
               Dim dt As New DataTable, lsts As New List(Of PencomApprovalExport)
               Dim i As Integer = 0

               If cb.Checked = True Then
                    Dim cr As New Core
                    Dim lst As New PencomApprovalExport


                    dt = cr.PMgetApprovalPaymentSchedule(LTrim(RTrim(Me.txtPencomBatch.Text)), grow.Cells(2).Text.ToString, apptypeID)
                    Do While i < dt.Rows.Count
                         lst = New PencomApprovalExport

                         lst.EnpowerExportBatch = extractBatch
                         If dt.Rows(i).Item("PlatForm").ToString = "RSA" Then
                              lst.FundID = "1"
                         ElseIf dt.Rows(i).Item("PlatForm").ToString = "RF" Then
                              lst.FundID = "2"
                         End If

                         lst.RSAPIN = dt.Rows(i).Item("txtpin").ToString
                         lst.ApplicationCode = dt.Rows(i).Item("txtApplicationCode").ToString
                         lst.ApplicationType = CInt(dt.Rows(i).Item("fkiAppTypeId"))
                         lst.ApprovalAmount = CDbl(dt.Rows(i).Item("numApproved"))
                         lst.ApprovalDate = CDate(dt.Rows(i).Item("dteApproval"))
                         lst.ValueDate = CDate(dt.Rows(i).Item("dteValueDate"))
                         lst.StartPeriod = CDate(Me.txtStartDate.Text)
                         lst.EndPeriod = CDate(Me.txtEndDate.Text)
                         lsts.Add(lst)

                         i = i + 1

                    Loop

                    cr.PMInsertEnpowerPaymentTemp(lsts)
               Else
               End If
          Next

			getBatchApplications(LTrim(RTrim(Me.txtPencomBatch.Text)), "C", apptypeID)

		Catch ex As Exception

		End Try

     End Sub

     Protected Sub gridApplications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplications.SelectedIndexChanged

     End Sub
     ' generate payment approval schedule for signoff
     Protected Sub btnGenerateSchedule_Click(sender As Object, e As EventArgs) Handles btnGenerateSchedule.Click

          If IsNothing(ViewState("ApprovalBatch")) = False Then
               Dim batchNum As String = ViewState("ApprovalBatch")
               '    Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & batchNum & ".pdf"
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

     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          Dim dtusers As New DataTable
          Try

               If IsPostBack = False Then


                    If IsNothing(Session("user")) = True Then

                         Response.Redirect("Login.aspx")
                    ElseIf IsNothing(Session("user")) = False And IsNothing(Session("userDetails")) = False Then

                         dtusers = Session("userDetails")
                         getUserAccessMenu(Session("user"))
                         getApprovalTypes()

                    End If

               Else
                    ' getApprovalTypes()
                    getUserAccessMenu(Session("user"))
               End If



          Catch ex As Exception
               '   MsgBox("" & ex.Message)
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

End Class
