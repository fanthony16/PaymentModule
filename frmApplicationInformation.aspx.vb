Imports System.Data
Imports System.Text
Imports System.IO

Partial Class frmApplicationInformation
     Inherits System.Web.UI.Page
     Dim ApprovalTypeCollection As New Hashtable

     Protected Sub gridApplication_RowDataBound()

     End Sub
     Protected Sub gridSubmittedDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          If IsNothing(ViewState("Documents")) = False Then

               Dim dt As DataTable = ViewState("Documents")
               If e.Row.RowType = DataControlRowType.DataRow Then

                    If dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString = "" Then

                         e.Row.ForeColor = System.Drawing.Color.Red
                         e.Row.Enabled = False

                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString <> "" Then
                         e.Row.ForeColor = System.Drawing.Color.Green
                    ElseIf dt.Rows(e.Row.RowIndex).Item("DateRecived").ToString <> "" And dt.Rows(e.Row.RowIndex).Item("DocumentPath").ToString = "" Then
                         e.Row.ForeColor = System.Drawing.Color.Orange
                         e.Row.Enabled = False
                    End If

               End If
          Else
          End If

     End Sub




     Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged

          Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)

     End Sub

     Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged

          Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)

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


          Dim scriptManagerA As New ScriptManager, dtusers As DataTable
          scriptManagerA = ScriptManager.GetCurrent(Me.Page)
          scriptManagerA.RegisterPostBackControl(Me.gridSubmittedDocuments)

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



               End If

          Catch ex As Exception

          End Try

     End Sub
     Protected Sub refresh()
          ViewState("ApplicationCode") = Nothing
          Dim nw As New List(Of ApplicationProperties)
          populateProperties(nw)
          Dim dt As New DataTable
          populateDocuments(dt)

     End Sub
     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs)


          Dim btnViewDetails As New ImageButton, appCode As String
          btnViewDetails = sender
          Dim i As GridViewRow
          i = btnViewDetails.NamingContainer
          appCode = Me.gridApplication.Rows(i.RowIndex).Cells(1).Text


          Dim typeID As Integer
          ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
          typeID = (CInt(ApprovalTypeCollection.Item(Me.gridApplication.Rows(i.RowIndex).Cells(4).Text)))
          Response.Redirect(String.Format("frmEditApplication.aspx?ApplicationCode={0}&ApplicationTypeID={1}&ReadOnly={2}", Server.UrlEncode(appCode), Server.UrlEncode(typeID), 1))


     End Sub
     Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click

          Dim dt As New DataTable, cr As New Core
          refresh()
          If Me.rdPIN.Checked = True Then

               dt = cr.PMgetApplicationByPIN(RTrim(LTrim(Me.txtPIN.Text)), "")

          ElseIf rdApprovalTypes.Checked = True Then

               If Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" And Me.ddApprovalType.SelectedItem.Text = "" Then
                    dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, 0)
                    'querying by date and application type
               ElseIf Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" And Me.ddApprovalType.SelectedItem.Text <> "" And Me.txtPIN.Text = "" Then

                    If IsNothing(ViewState("ApprovalTypeCollection")) = False Then
                         ApprovalTypeCollection = ViewState("ApprovalTypeCollection")
                         dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, CInt(ApprovalTypeCollection.Item(Me.ddApprovalType.SelectedItem.Text)))
                    Else
                         Response.Redirect("frmApplicationInformation.aspx")

                    End If

               End If

          Else

               If Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" Then
                    dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, 0)
               Else
                    dt = cr.PMgetApplicationByDate(CDate(Now.Date), CDate(Now.Date), False, 0)
               End If

          End If


          ''querying by PIN
          'If Me.txtStartDate.Text = "" And Me.txtEndDate.Text = "" And Me.ddApprovalType.SelectedItem.Text = "" And Me.txtPIN.Text <> "" Then
          '     dt = cr.PMgetApplicationByPIN(RTrim(LTrim(Me.txtPIN.Text)), "")
          '     'querying by date
          'ElseIf Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" And Me.ddApprovalType.SelectedItem.Text = "" And Me.txtPIN.Text = "" Then
          '     dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, 0)
          '     'querying by date and application type
          'ElseIf Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" And Me.ddApprovalType.SelectedItem.Text <> "" And Me.txtPIN.Text = "" Then
          '     dt = cr.PMgetApplicationByDate(CDate(Me.txtStartDate.Text), CDate(Me.txtEndDate.Text), False, 0)
          'End If
          ''    MsgBox("" & dt.Rows.Count)

          ViewState("Application") = dt
          If IsNothing(dt) = False Then
               Me.loadGrid(dt)
          End If


     End Sub

     Protected Sub rdApprovalTypes_CheckedChanged(sender As Object, e As EventArgs) Handles rdApprovalTypes.CheckedChanged
          If Me.rdApprovalTypes.Checked = True Then

               Me.txtPIN.Text = ""
               Me.txtPIN.Enabled = False
               ddApprovalType.Enabled = True

          Else

          End If
     End Sub

     Protected Sub rdPIN_CheckedChanged(sender As Object, e As EventArgs) Handles rdPIN.CheckedChanged

          If Me.rdPIN.Checked = True Then

               Me.ddApprovalType.SelectedIndex = 0
               Me.txtPIN.Enabled = True
               ddApprovalType.Enabled = False

          Else

          End If

     End Sub

     Protected Sub loadGrid(dt As DataTable)
          Try
               ' dt = cr.PMgetApplicationByTpye(typeID, "Documentation")



               gridApplication.DataSource = dt
               gridApplication.DataBind()

               If dt.Rows.Count > 10 Then
                    Me.pnlGrid.Height = Nothing
               Else
                    Me.pnlGrid.Height = 500
               End If
          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try
     End Sub

     Protected Sub gridApplication_PageIndexChanged(sender As Object, e As EventArgs) Handles gridApplication.PageIndexChanged

     End Sub

     Protected Sub gridApplication_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridApplication.PageIndexChanging

          If IsNothing(ViewState("Application")) = False Then

               Dim dt As New DataTable
               Me.gridApplication.PageIndex = e.NewPageIndex
               dt = ViewState("Application")
               Me.loadGrid(dt)

          Else
          End If

     End Sub

     Protected Sub gridApplication_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridApplication.SelectedIndexChanged

          Dim ApplicationProperties As New List(Of ApplicationProperties), dt As New DataTable, cr As New Core, dtPDetails As New DataTable, dtDocuments As New DataTable
          Dim selectedRowIndex As Integer

          selectedRowIndex = Me.gridApplication.SelectedRow.RowIndex

          Dim row As GridViewRow = gridApplication.Rows(selectedRowIndex)

          dt = cr.PMgetApplicationByCode(row.Cells(1).Text.ToString())

          'getting submitted documents per application 
          'dtDocuments = cr.PMgetSubmittedDocument(row.Cells(3).Text.ToString(), CInt(row.Cells(1).Text.ToString().Split("-")(1)))
          dtDocuments = cr.PMgetSubmittedDocument(row.Cells(3).Text.ToString(), CInt(row.Cells(1).Text.ToString()))

          ViewState("ApplicationCode") = row.Cells(1).Text.ToString
          ViewState("PIN") = row.Cells(3).Text.ToString

          'getting customer's personal information details
          dtPDetails = cr.getPMPersonInformation(row.Cells(4).Text.ToString())


          ApplicationProperties = cr.PMgetApplicationDetails(row.Cells(1).Text.ToString(), row.Cells(3).Text.ToString())

          Session("lodgmentProperties") = ApplicationProperties

          'population the grid to the left for other application information
          populateProperties(ApplicationProperties)

          'population the grid at the bottom for submitted required application documents
          Me.populateDocuments(dtDocuments)

          If ApplicationProperties.Count < 10 Then
               pnlLeftGrid.Height = 400
          Else
               pnlLeftGrid.Height = Nothing
          End If

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


     Protected Sub gridSubmittedDocuments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridSubmittedDocuments.SelectedIndexChanged

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

     Protected Sub ViewDocumentDetails_Click(sender As Object, e As EventArgs)

          Dim btnViewDocumentLog As New ImageButton, appCode As String, documentPath As String
          btnViewDocumentLog = sender
          Dim i As GridViewRow
          i = btnViewDocumentLog.NamingContainer


          appCode = Me.gridApplication.Rows(i.RowIndex).Cells(0).Text

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

          Else

          End If


     End Sub

End Class
