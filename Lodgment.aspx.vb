Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit


Partial Class Lodgment
    Inherits System.Web.UI.Page
    Dim ClientCollection As New Hashtable


    Public Sub getUserAccessMenu(uName As String)

        Dim cr As New Core
        Dim dtAccessModule As New DataTable
        Dim aryAccessModule As New ArrayList
        Dim i As Integer, j As Integer, k As Integer

        dtAccessModule = cr.getAccessModule(Session("User"))

        Session("RoleID") = dtAccessModule.Rows(0).Item("fkiroleid")


        Do While i < dtAccessModule.Rows.Count

            aryAccessModule.Add(dtAccessModule.Rows(i).Item(1))
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

    End Sub

    Private Sub MakeDirectoryIfExists(ByVal NewDirectory As String)
        Try
            ' Check if directory exists
            If Not Directory.Exists(NewDirectory) Then
                ' Create the directory.
                Directory.CreateDirectory(NewDirectory)
            ElseIf Directory.Exists(NewDirectory) Then
                DeleteDir(NewDirectory)
                Directory.CreateDirectory(NewDirectory)
            End If
        Catch _ex As IOException
            Response.Write(_ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim scriptManagerA, scriptManagerR, scriptManagerRP, scriptManagerAR As New ScriptManager
        

        scriptManagerA = ScriptManager.GetCurrent(Me.Page)
        scriptManagerA.RegisterPostBackControl(Me.btnViewSchedule)


        If Page.IsPostBack = False Then

            If IsNothing(Session("FundName")) = True Then

                Response.Redirect("Login.aspx")

            ElseIf IsNothing(Session("FundName")) = False And IsNothing(Session("userDetails")) = False Then

                getUserAccessMenu(Session("user"))
                Dim NewDir As String = Server.MapPath(Session("user"))
                dvActionSideBox.Visible = False

                If IsNothing(Session("RoleID")) = False And CInt(Session("RoleID")) = 14 Then
                    MakeDirectoryIfExists(NewDir)
                Else
                End If

            Else
            End If
        Else
            getUserAccessMenu(Session("user"))
        End If


    End Sub

    Protected Sub CalEndDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalSEndDate.SelectionChanged

        Me.calSEndDate_PopupControlExtender.Commit(Me.CalSEndDate.SelectedDate)
        Me.mpSchedule.Show()

    End Sub

    Protected Sub calScheduleDate_SelectionChanged(sender As Object, e As EventArgs) Handles calScheduleDate.SelectionChanged

        Me.calScheduleDate_PopupControlExtender.Commit(Me.calScheduleDate.SelectedDate)
        Me.mpSchedule.Show()

    End Sub

    Protected Sub calProcessDate_SelectionChanged(sender As Object, e As EventArgs) Handles calProcessDate.SelectionChanged

        Me.calProcessDate_PopupControlExtender.Commit(Me.calProcessDate.SelectedDate)
        'Me.mpSchedule.Show()
        Me.mpUploadUpdate.Show()

    End Sub



    Protected Sub CalSStartDate_SelectionChanged(sender As Object, e As EventArgs) Handles CalSStartDate.SelectionChanged

        Me.calSStartDate_PopupControlExtender.Commit(Me.CalSStartDate.SelectedDate)
        Me.mpSchedule.Show()

    End Sub

    Protected Sub calSDate_SelectionChanged(sender As Object, e As EventArgs) Handles calSDate.SelectionChanged

        Me.calSDate_PopupControlExtender.Commit(Me.calSDate.SelectedDate)

    End Sub

    Protected Sub txtStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtStartDate.TextChanged

    End Sub

    Protected Sub calEDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEDate.SelectionChanged
        Me.calEDate_PopupControlExtender.Commit(Me.calEDate.SelectedDate)
    End Sub

    Protected Function getClientByFind(employer As String) As DataTable
        Dim mylodgment As New Lodgment
        Dim dsUser As DataSet
        Dim ws As New wsClients.Clients()
        Try
            dsUser = ws.getClients(LTrim(RTrim(employer)))
            setClientArray(dsUser.Tables("Employers"))
            Return dsUser.Tables("Employers")
        Catch ex As Exception

        End Try


        Return Nothing
    End Function

    Protected Sub txtClientName_TextChanged(sender As Object, e As EventArgs) Handles txtClientName.TextChanged
        Dim mylodgment As New Lodgment
        Dim dsUser As DataSet
        Try
            dcClients.Items.Clear()
            Dim ws As New wsClients.Clients()
            dsUser = ws.getClients(LTrim(RTrim(Me.txtClientName.Text)))
            setClientArray(dsUser.Tables("Employers"))
            dcClients.DataSource = dsUser.Tables("Employers")
            dcClients.DataValueField = "pkiClientID"
            dcClients.DataTextField = "txtClientName"
            dcClients.SelectedIndex = -1
            For i = 0 To dsUser.Tables("Employers").Rows.Count - 1
                Me.dcClients.Items.Add(dsUser.Tables("Employers").Rows(i).Item(1))
            Next
            setClientArray(dsUser.Tables("Employers"))
        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try
    End Sub

    Protected Sub setClientArray(dt As DataTable)

        Try
            ClientCollection = New Hashtable
            Dim i As Integer = 0
            Do While i < dt.Rows.Count
                    ClientCollection.Add(dt.Rows(i).Item("txtClientName"), dt.Rows(i).Item("pkiClientID"))

                i = i + 1
            Loop
            ViewState("ClientCollection") = ClientCollection

        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try

    End Sub
    Protected Sub newentry()

        'txtLodgment.Text = ""
        'txtBalance.Text = ""
        'Me.txtNarration.Text = ""
        'Me.txtReversal.Text = ""
        'Me.txtRefund.Text = ""
        'Me.txtProcessed.Text = ""
        'Me.txtUnProcessed.Text = ""
        'txtEmployerName.Text = ""
        'chkCSV.Checked = False
        'chkCSV.Text = "CSV Status"
        'Me.chkSchedule.Checked = False
        'Me.chkSchedule.Text = "Schedule Status"

        Dim lodgmentProperty As New List(Of lodgmentProperties)
        populateProperties(lodgmentProperty)
        pnlLeft.Height = 475

        dcScheduleType.SelectedIndex = 0
        txtSLocation.Text = ""
        txtSPartCount.Text = ""
        Me.txtSComments.Text = ""
        Me.lstSchedule.Items.Clear()
        ViewState("destinationFiles") = Nothing
        Me.txtSStartDate.Text = ""
        Me.txtSEndDate.Text = ""
        Me.dvActionSideBox.Visible = False

        getUploadDetails("")
        getReversalDetail(0)
        getRefundDetail(0)

    End Sub

    Protected Sub getUploadDetails(lodgmentID As String)
        Dim dsUser As New DataSet
        Dim ws As New wsContributions.Contribution
        Dim wss As New Contributions

        ' MsgBox("" & lodgmentID & "  " & Session("FundName").ToString)
        'RSA
        'dsUser = ws.getUploadDetails(lodgmentID, Session("FundName").ToString)

        Try

        dsUser = wss.getUploadDetails(lodgmentID, Session("FundName").ToString)
            ViewState("UploadDetail") = dsUser.Tables(0)
        BindGridUploadDetail(dsUser.Tables(0))

        If dsUser.Tables(0).Rows.Count < 4 Then
            pnlUploadDetail.Height = 100

        Else
            pnlUploadDetail.Height = Nothing
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub getReversalDetail(lodgmentID As Integer)

        Dim dsUser As New DataSet
        ' Dim ws As New wsContributions.Contribution
        Dim wss As New Contributions
        Try

        
        dsUser = wss.getReversalDetail(lodgmentID, Session("FundName").ToString)
        ViewState("ReversalDetail") = dsUser.Tables(0)
        BindGridReversalDetail(dsUser.Tables(0))
        If dsUser.Tables(0).Rows.Count < 4 Then
            pnlReversalDetails.Height = 100

        Else
            pnlReversalDetails.Height = Nothing
        End If

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub getRefundDetail(lodgmentID As Integer)
        Dim dsUser As New DataSet
        'Dim ws As New wsContributions.Contribution
        Dim wss As New Contributions

        Try

        

        dsUser = wss.getRefundDetails(lodgmentID, Session("FundName").ToString)
        
        ViewState("RefundDetail") = dsUser.Tables(0)
        BindGridRefundDetail(dsUser.Tables(0))
        If dsUser.Tables(0).Rows.Count < 10 Then
            pnlRefundDetails.Height = 100
        Else
            pnlRefundDetails.Height = Nothing
        End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub BindGridRefundDetail(dt As DataTable)
        Try

            Me.gridRefundDetail.DataSource = dt
            Me.gridRefundDetail.DataBind()

        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try

    End Sub

    Protected Sub BindGridReversalDetail(dt As DataTable)
        Try

            Me.gridReversalDetail.DataSource = dt
            Me.gridReversalDetail.DataBind()

        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try

    End Sub

    Protected Sub BindGridUploadDetail(dt As DataTable)
        Try

            Me.gridUploadDetails.DataSource = dt
            Me.gridUploadDetails.DataBind()

        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try

    End Sub
    Protected Function getLodmentByID(id As String, fund As String) As DataTable

        Dim cr As New Contributions
        Try
            Return cr.getContribution(id, fund).Tables(0)
        Catch ex As Exception

        End Try
        Return Nothing


    End Function
    Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click

        Dim mylodgment As New Lodgment
        Dim dsUser As DataSet
        newentry()
        Dim ws As New wsContributions.Contribution()
        Dim wss As New Contributions()
        Try


            If Me.txtStartDate.Text = "" Or Me.txtEndDate.Text = "" Then
                Exit Sub
            ElseIf Me.rdClient.Checked = True And Me.dcClients.Text <> "" Then

                ClientCollection = New Hashtable
                ClientCollection = ViewState("ClientCollection")

                If Me.ClientCollection.ContainsKey(Me.dcClients.SelectedItem.Text) Then

                    dsUser = wss.getContribution(CInt(ClientCollection.Item(Me.dcClients.SelectedItem.Text)), CDate(txtStartDate.Text), CDate(txtEndDate.Text), Session("FundName"))
                    ViewState("mydt") = dsUser.Tables("Lodgment")
                    BindGrid(dsUser.Tables("Lodgment"))

                    If dsUser.Tables("Lodgment").Rows.Count < 20 Then
                        pnlGrid.Height = 400
                    Else
                        pnlGrid.Height = Nothing
                    End If

                Else
                End If

            ElseIf Me.rdDesc.Checked = True Then

                dsUser = wss.getContribution(RTrim(LTrim(Me.txtDesc.Text)), CDate(txtStartDate.Text), CDate(txtEndDate.Text), Session("FundName"))
                ViewState("mydt") = dsUser.Tables("Lodgment")
                BindGrid(dsUser.Tables("Lodgment"))

                If dsUser.Tables("Lodgment").Rows.Count < 20 Then
                    pnlGrid.Height = 400
                Else
                    pnlGrid.Height = Nothing
                End If

            ElseIf Me.rdID.Checked = True And Me.txtLodgmentID.Text <> "" Then

                dsUser = wss.getContribution(CInt(txtLodgmentID.Text), Session("FundName"))
                ViewState("mydt") = dsUser.Tables("Lodgment")
                BindGrid(dsUser.Tables("Lodgment"))

                If dsUser.Tables("Lodgment").Rows.Count < 20 Then
                    pnlGrid.Height = 400
                Else
                    pnlGrid.Height = Nothing
                End If

            ElseIf Me.rdAmount.Checked = True And Me.txtAmount.Text <> "" Then

                If Me.ValidateNumber(Me.txtAmount.Text.Replace(",", "")) = True Then

                    dsUser = wss.getContribution(CDbl(RTrim(LTrim(txtAmount.Text.Replace(",", "")))), CDate(txtStartDate.Text), CDate(txtEndDate.Text), Session("FundName"))
                    ViewState("mydt") = dsUser.Tables("Lodgment")
                    BindGrid(dsUser.Tables("Lodgment"))


                    If dsUser.Tables("Lodgment").Rows.Count < 20 Then
                        pnlGrid.Height = 400
                    Else
                        pnlGrid.Height = Nothing
                    End If

                Else
                    dsUser = ws.getContribution(0, CDate(txtStartDate.Text), CDate(txtEndDate.Text), Session("FundName"))
                    ViewState("mydt") = dsUser.Tables("Lodgment")
                    BindGrid(dsUser.Tables("Lodgment"))

                    If dsUser.Tables("Lodgment").Rows.Count < 20 Then
                        pnlGrid.Height = 400
                    Else
                        pnlGrid.Height = Nothing
                    End If

                End If


                ' ElseIf Me.chkID.Checked = False And Me.chkDesc.Checked = False And Me.chkAmount.Checked = False Then
            Else
                ' Dim wss As New Contributions

                'dsUser = ws.getContribution(CDate(txtStartDate.Text), CDate(txtEndDate.Text), "RSA")
                dsUser = wss.getContribution(CDate(txtStartDate.Text), CDate(txtEndDate.Text), Session("FundName"))
                ViewState("mydt") = dsUser.Tables("Lodgment")
                'MsgBox("" & dsUser.Tables("Lodgment").Rows.Count)
                BindGrid(dsUser.Tables("Lodgment"))

                If dsUser.Tables("Lodgment").Rows.Count < 20 Then
                    pnlGrid.Height = 400
                Else
                    pnlGrid.Height = Nothing
                End If

            End If

        Catch ex As Exception
            '  MsgBox("" & ex.Message)
        End Try

    End Sub
    Private Sub viewSchedule()

        If IsNothing(ViewState("schedulePath")) = False Then

            If CStr(ViewState("schedulePath")).ToString = "" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
            Else
            End If
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
            'Dim lnkView As LinkButton = TryCast(sender, LinkButton)
            'Dim row As GridViewRow = TryCast(lnkView.NamingContainer, GridViewRow)
            'Dim id As String = lnkView.CommandArgument
            'Dim name As String = row.Cells(0).Text
            'Dim country As String = TryCast(row.FindControl("txtCountry"), TextBox).Text
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", (Convert.ToString((Convert.ToString((Convert.ToString("alert('Id: ") & id) + " Name: ") & name) + " Country: ") & country) + "')", True)



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
                MsgBox("" & ex.Message)
            End Try

        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
        End If


    End Sub

    Protected Sub gridContribution_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If IsNothing(ViewState("mydt")) = False Then

            Dim dt As DataTable = ViewState("mydt")
            If e.Row.RowType = DataControlRowType.DataRow Then

                If dt.Rows(e.Row.RowIndex).Item("ScheduleType").ToString = "NONE" And CInt(dt.Rows(e.Row.RowIndex).Item("CSV_Verification")) = 1 Then

                    e.Row.ForeColor = System.Drawing.Color.Brown

                ElseIf dt.Rows(e.Row.RowIndex).Item("ScheduleType").ToString <> "NONE" And CInt(dt.Rows(e.Row.RowIndex).Item("CSV_Verification")) = 0 Then

                    e.Row.ForeColor = System.Drawing.Color.Purple

                ElseIf CDbl(dt.Rows(e.Row.RowIndex).Item("Oustanding").ToString) <= 100 And dt.Rows(e.Row.RowIndex).Item("ScheduleType").ToString <> "NONE" And CInt(dt.Rows(e.Row.RowIndex).Item("CSV_Verification")) = 1 Then

                    e.Row.ForeColor = System.Drawing.Color.Green

                ElseIf CInt(dt.Rows(e.Row.RowIndex).Item("CSV_Verification")) = 0 And dt.Rows(e.Row.RowIndex).Item("ScheduleType").ToString = "NONE" Then

                    e.Row.ForeColor = System.Drawing.Color.Red

                End If

            End If
        Else
        End If
    End Sub



    Protected Function ValidateNumber(num As String) As Boolean

        Try
            Dim d As Double
            d = CDbl(num)
            Return True

        Catch ex As Exception
            Return False

        End Try

    End Function
    Protected Sub BindGrid(dt As DataTable)

        Try


            Me.gridContribution.DataSource = dt
            Me.gridContribution.DataBind()


        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try


    End Sub

    Protected Sub gridContribution_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridContribution.PageIndexChanging

        'ViewState("mydt") 
        If IsNothing(ViewState("mydt")) = False Then
            Me.gridContribution.PageIndex = e.NewPageIndex
            Me.BindGrid(ViewState("mydt"))

        Else

        End If

        

    End Sub

    Protected Sub gridContribution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridContribution.SelectedIndexChanged

        Dim sch As Object, csv As Integer
        Dim dt As New DataTable
        Dim selectedRowIndex As Integer
        Dim selectedLodgment As New ArrayList

        selectedRowIndex = gridContribution.SelectedRow.RowIndex

        Dim row As GridViewRow = gridContribution.Rows(selectedRowIndex)

        Dim errorDescription As String = ""
        newentry()


        dt = getLodmentByID(row.Cells(1).Text.ToString(), Session("FundName"))
        selectedLodgment.Add(selectedLodgment)
        selectedLodgment.Add(dt)
        Session("lodgmentID") = dt.Rows(0).Item("ID_JournalDetail")
        sch = dt.Rows(0).Item("CSD_Schedule")
        csv = dt.Rows(0).Item("CSV_Verification")


        Me.txtSlodgmentID.Text = Session("lodgmentID").ToString
        Me.txtSNarration.Text = dt.Rows(0).Item("Description")
        Me.txtSLAmount.Text = dt.Rows(0).Item("Balance")
        Me.txtSPartCount.Text = dt.Rows(0).Item("CSD_Part_No").ToString
        Me.txtSLocation.Text = dt.Rows(0).Item("Schedule_Location").ToString
        Me.txtSComments.Text = dt.Rows(0).Item("CSD_Remarks").ToString
        txtCRUComments.Text = dt.Rows(0).Item("CRUComment").ToString


        Me.txtUAmount.Text = dt.Rows(0).Item("LocalAmount")
        Me.txtUBalance.Text = dt.Rows(0).Item("Balance")
        Me.txtUDescription.Text = dt.Rows(0).Item("Description")
        Me.txtULodgmentID.Text = Session("lodgmentID").ToString
        Me.txtUOutstanding.Text = dt.Rows(0).Item("oustanding")
        Me.txtUProcessed.Text = ""
        Me.txtUReversed.Text = ""

        ViewState("Balance") = dt.Rows(0).Item("Balance")
        ViewState("Oustanding") = dt.Rows(0).Item("oustanding")
        ViewState("PanaltyPaid") = dt.Rows(0).Item("PanaltyPaid")
        ViewState("PenaltyProcessed") = dt.Rows(0).Item("PenaltyProcessed")
        ViewState("PenaltyOutstanding") = dt.Rows(0).Item("PenaltyOutstanding")

        rdProcessed.Checked = True


        If dt.Rows(0).Item("CSD_Recieved_Date").ToString = "" Then
            Me.txtScheduleDate.Text = dt.Rows(0).Item("CSD_Recieved_Date").ToString
        Else
            Me.txtScheduleDate.Text = dt.Rows(0).Item("CSD_Recieved_Date").ToString.Substring(0, 9).Trim
        End If

        If IsNothing(Session("lodgmentID")) = False Then
            Me.dvActionBox.Visible = False
        Else
            Me.dvActionBox.Visible = False
        End If

        getUploadDetails(CInt(row.Cells(1).Text.ToString()))
        getReversalDetail(CInt(row.Cells(1).Text.ToString()))
        getRefundDetail(CInt(row.Cells(1).Text.ToString()))

        txtUUploadComment.Enabled = False

        Dim lodgmentProperties As New List(Of lodgmentProperties)

        '0
        Dim lodgmentProperty As New lodgmentProperties
        lodgmentProperty.FieldName = "Employer Name"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("EmployerName").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '1
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Lodgment ID"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("ID_JournalDetail").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '2
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Transaction Date"
        If dt.Rows(0).Item("TransactionDate").ToString = "" Then
            lodgmentProperty.FieldValue = dt.Rows(0).Item("TransactionDate").ToString
        Else
            lodgmentProperty.FieldValue = dt.Rows(0).Item("TransactionDate").ToString.Substring(0, 9)
        End If
        lodgmentProperties.Add(lodgmentProperty)

        '3
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Value Date"
        If dt.Rows(0).Item("ValueDate").ToString = "" Then
            lodgmentProperty.FieldValue = dt.Rows(0).Item("ValueDate").ToString
        Else
            lodgmentProperty.FieldValue = dt.Rows(0).Item("ValueDate").ToString.Substring(0, 9)
            Session("ValueDate") = dt.Rows(0).Item("ValueDate").ToString.Substring(0, 9)
        End If
        lodgmentProperties.Add(lodgmentProperty)


        '4
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Local Amount"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("LocalAmount").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '5
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Reversal"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CalReversal").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '6
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Refunds"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CalRefunds").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '7
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Balance"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CalBalance").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '8
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Processed Amount"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CalProcessed").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '9
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Outstanding"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CalOutStanding").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '10
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Penalty Amount"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("NetPenalty").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '11
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Penalty Processed Amount"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("PenaltyProcessed").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '12
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Penalty Outstanding"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("PenaltyOutstanding").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '13
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "FAD's Remarks"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("FAD_Remarks").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '14
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Schedule Type"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("ScheduleType").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '15
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Participant Count"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CSD_Part_No").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '16
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "CSD's Remarks"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CSD_Remarks").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '17
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Contribution Period"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("Contribution_Month_Year").ToString
        lodgmentProperties.Add(lodgmentProperty)

        '18
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Schedule Recieved Date"

        If dt.Rows(0).Item("CSD_Recieved_Date").ToString = "" Then
            lodgmentProperty.FieldValue = dt.Rows(0).Item("CSD_Recieved_Date").ToString
        Else
            lodgmentProperty.FieldValue = dt.Rows(0).Item("CSD_Recieved_Date").ToString.Substring(0, 9)
        End If
        lodgmentProperties.Add(lodgmentProperty)


        '19
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "CSV Status"
        If CInt(dt.Rows(0).Item("CSV_Verification").ToString) = 1 Then
            lodgmentProperty.FieldValue = "Is Available"
        Else
            lodgmentProperty.FieldValue = "Not Available"
        End If
        lodgmentProperties.Add(lodgmentProperty)


        '20
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "CSV Recieved Date"
        If dt.Rows(0).Item("Verif_Date").ToString = "" Then
            lodgmentProperty.FieldValue = dt.Rows(0).Item("Verif_Date").ToString
        Else
            lodgmentProperty.FieldValue = dt.Rows(0).Item("Verif_Date").ToString.Substring(0, 9)
        End If
        lodgmentProperties.Add(lodgmentProperty)

        '21
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "Verification's Remarks"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("Verif_Remarks").ToString
        lodgmentProperties.Add(lodgmentProperty)


        '22
        lodgmentProperty = New lodgmentProperties
        lodgmentProperty.FieldName = "CRUComment"
        lodgmentProperty.FieldValue = dt.Rows(0).Item("CRUComment").ToString
        lodgmentProperties.Add(lodgmentProperty)





        If CBool(dt.Rows(0).Item("blnGroupLodgment")) = True Then

            lodgmentProperty = New lodgmentProperties
            lodgmentProperty.FieldName = "Is Grouped"
            lodgmentProperty.FieldValue = "Yes"
            lodgmentProperties.Add(lodgmentProperty)

            lodgmentProperty = New lodgmentProperties
            lodgmentProperty.FieldName = "Grouped ID"
            lodgmentProperty.FieldValue = dt.Rows(0).Item("intMasterLodgmentID").ToString
            lodgmentProperties.Add(lodgmentProperty)

        Else
        End If

        ViewState("schedulePath") = dt.Rows(0).Item("SchedulePath").ToString

        'Me.gridProperties.DataSource = lodgmentProperties
        'Me.gridProperties.DataBind()
        Session("lodgmentProperties") = lodgmentProperties
        'Dim lodgmentProperties As New List(Of lodgmentProperties)
        populateProperties(lodgmentProperties)

        If lodgmentProperties.Count < 10 Then
            pnlLeft.Height = 400
        Else
            pnlLeft.Height = Nothing
        End If

        dvActionSideBox.Visible = True

        'If IsNothing(Session("RoleID")) = False Then
        '    Dim roleID As Integer = CInt(Session("RoleID"))
        '    If roleID = 14 Then
        '
        '        Me.dvLodgmentImport.Visible = False
        '        Me.dvCSVUpdate.Visible = False
        '        'Me.dvUploadUpdate.Visible = False
        '
        '        '  Me.dvViewSchedule.Visible = True
        '        Me.dvScheduleUpdate.Visible = True
        '        Me.dvCRUUpdate.Visible = True
        '
        '    Else
        '
        '        ' Me.dvViewSchedule.Visible = False
        '        Me.dvScheduleUpdate.Visible = False
        '        Me.dvCRUUpdate.Visible = False
        '        Me.dvUploadUpdate.Visible = False
        '        Me.dvCSVUpdate.Visible = False
        '
        '
        '    End If
        'Else
        '
        'End If
        '  Me.dvCRUUpdate.Visible = True
        ' btnUploadUpdate.Visible = True


    End Sub

    Protected Sub populateProperties(lodgmentProperties As List(Of lodgmentProperties))
        Try

        
        Me.gridProperties.DataSource = lodgmentProperties
            Me.gridProperties.DataBind()

        Catch ex As Exception

        End Try

    End Sub



    

    Protected Sub btnImportLodgment_Click(sender As Object, e As EventArgs) Handles btnImportLodgment.Click
        Try

        Catch ex As Exception

        End Try
        Dim cr As New Core
        cr.ImportLodgment("RSA")


    End Sub
    Dim lstFiles As List(Of String), i As Integer
    Protected Sub AjaxFileUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)
        

        Dim filename As String = System.IO.Path.GetFileName(e.FileName)
        Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)

        'Dim strUploadPath As String = "~/NewFolder1/" + filename

        Dim strUploadPath As String = "~/" & Session("user") & "/" + filename
        AjaxFileUpload1.SaveAs(Server.MapPath(strUploadPath) + filename)


    End Sub

    Private Sub scheduleLoader()
        'If AjaxFileUpload1.MaximumNumberOfFiles = 0 Then
        '    Exit Sub
        'Else
        'End If

        Dim Counter As Integer
        Dim lstfileName As New ArrayList
        Dim fname As String, dir As String = "", fileName As String

        'Dim destinationDir As String = "\\earth\ContributionSchedules$"
        'Dim destinationDir As String = "C:\LodgmentNote\" + Session("user")
        Dim destinationDir As String = "\\earth\ContributionSchedules$"

        Dim destinationFile As String, destinationFiles As String = ""

        Dim fund As Integer
        Dim i As Integer = 0, j As Integer = 0, k = 0
        lstfileName.Clear()


        Dim fInfo As List(Of String) = getFiles(Session("user"))
        Do While Counter < fInfo.Count

            fname = fInfo.Item(Counter)
            Dim FI As FileInfo
            FI = New FileInfo(fname)

            Dim aryfile As Array = fname.Split("\")
            Array.Reverse(aryfile)
            fileName = aryfile(0)

            Array.Reverse(aryfile)
            i = aryfile.Length - 1

            Do While j < aryfile.Length - 1

                dir = dir & aryfile(j) & "\"
                j = j + 1

            Loop


            Dim contMonth As String = ""
            i = 0

            If Me.lstSchedule.Items.Count = 0 Then
                MsgBox("Please Enter The Contribution Month Period", MsgBoxStyle.Information)
                Exit Sub
            Else
            End If


            Do While i < lstSchedule.Items.Count

                If contMonth = "" Then

                    contMonth = (lstSchedule.Items(i).Value.ToString)
                Else

                    'contMonth = contMonth & ";" & (lstContDate.Items(i))
                    contMonth = contMonth & "_" & (lstSchedule.Items(i).Value.ToString)
                End If
                i = i + 1
            Loop

            'ViewState("txtClientID")
            getClientID(Me.dcSClientName.Text)
            Dim subplanID As String = Session("txtSubplanID").ToString

            dir = dir & txtSlodgmentID.Text & "_" & subplanID.ToString & FI.Extension.ToString

            '''''''''''''''''''
            ''renaming source just added for web''
            '''''''''''''''''''
            renameFile(fname, dir)

            ''''modification 2014-05-05''''''''
            Dim tmpFile As String = destinationDir & "\" & subplanID.ToString & FI.Extension.ToString
            ' MsgBox("" & destinationDir & subplanID.ToString & FI.Extension.ToString)

            '''just removed for web 20150319''''''''''''''''''''''
            ''''''''' copyFile(fname, destinationDir, tmpFile)''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''


            '''''''''''''''''''''''''''''''''''
            If fInfo.Count = 1 Then



                destinationFile = destinationDir & "\" & txtSlodgmentID.Text & "_" & subplanID.ToString & "_" & CDate(Session("ValueDate")).ToString("yyyy-MM-dd").Replace("-", "") & "_" & contMonth.Replace("-", "") & FI.Extension.ToString
                destinationFiles = destinationFile

            Else

                destinationFile = destinationDir & "\" & subplanID.ToString & "_" & CDate(Session("ValueDate")).ToString("yyyy-MM-dd").Replace("-", "") & "_" & contMonth.Replace("-", "") & "_" & (Counter + 1).ToString & FI.Extension.ToString

                If (Counter) < fInfo.Count - 1 Then
                    destinationFiles = destinationFile & " | " & destinationFiles

                ElseIf (Counter) = fInfo.Count - 1 Then
                    destinationFiles = destinationFiles & " " & destinationFile
                Else

                End If

            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''  copyFile(tmpFile, destinationDir, destinationFile)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''just commented'''
            ''' 

            '  renameFile(tmpFile, destinationFile)

            renameFile(dir, destinationFile)

            'DeleteFile(dir)

            'Next
            Counter = Counter + 1

        Loop

        ViewState("destinationFiles") = destinationFiles
        'MakeDirectoryIfExists(NewDir)


    End Sub


    Protected Function getFiles(path As String) As List(Of String)

        'Dim ScheduleFiles As DirectoryInfo = New DirectoryInfo(Server.MapPath(path))
        'Dim fInfo As FileInfo() = ScheduleFiles.GetFiles
        'Return fInfo

        Dim di As New IO.DirectoryInfo(Server.MapPath(path))
        Dim diar1 As IO.FileInfo() = di.GetFiles()
        Dim dra As IO.FileInfo
        Dim lstFiles As New List(Of String)

        'list the names of all files in the specified directory
        For Each dra In diar1
            'ListBox1.Items.Add(dra.FullName)
            '  MsgBox("" & dra.FullName)
            lstFiles.Add(dra.FullName)
        Next
        Return lstFiles

    End Function


    Protected Sub btnSPAdd_Click(sender As Object, e As ImageClickEventArgs) Handles btnSPAdd.Click


        If Year(CDate(Me.txtSStartDate.Text)) > Year(CDate(Me.txtSEndDate.Text)) Then
            Exit Sub
        ElseIf Year(CDate(Me.txtSStartDate.Text)) = Year(CDate(Me.txtSEndDate.Text)) And Month(CDate(Me.txtSStartDate.Text)) > Month(CDate(Me.txtSEndDate.Text)) Then
            Exit Sub
        End If

        If Month(CDate(Me.txtSStartDate.Text)) = Month(CDate(Me.txtSEndDate.Text)) And Year(CDate(Me.txtSStartDate.Text)) = Year(CDate(Me.txtSEndDate.Text)) Then


            Dim conDate As String = ""
            Dim month As Integer = CDate(CDate(Me.txtSStartDate.Text)).Date.Month



            Select Case month
                Case Is = "1"
                    conDate = "Jan" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "2"
                    conDate = "Feb" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "3"
                    conDate = "Mar" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "4"
                    conDate = "Apr" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "5"
                    conDate = "May" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "6"
                    conDate = "Jun" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "7"
                    conDate = "Jul" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "8"
                    conDate = "Aug" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "9"
                    conDate = "Sep" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "10"
                    conDate = "Oct" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "11"
                    conDate = "Nov" & Year(CDate(Me.txtSStartDate.Text)).ToString

                Case Is = "12"
                    conDate = "Dec" & Year(CDate(Me.txtSStartDate.Text)).ToString

            End Select


            ' Me.lstContDate.Items.Add(conDate)
            addContDate(conDate)
        ElseIf Month(Me.txtSStartDate.Text) > Month(CDate(Me.txtSEndDate.Text)) Or Year(Me.txtSStartDate.Text) <= Year(CDate(Me.txtSEndDate.Text)) Then

            addContDate(getMonthName(Month(Me.txtSStartDate.Text)) & Year(Me.txtSStartDate.Text).ToString & "-" & getMonthName(Month(CDate(Me.txtSEndDate.Text))) & Year(CDate(Me.txtSEndDate.Text)).ToString)


        End If


        Me.mpSchedule.Show()
    End Sub
    Private Function getMonthName(ByVal m As Integer) As String
        Dim conDate As String = ""
        Select Case m
            Case Is = "1"
                conDate = "Jan"

            Case Is = "2"
                conDate = "Feb"

            Case Is = "3"
                conDate = "Mar"

            Case Is = "4"
                conDate = "Apr"

            Case Is = "5"
                conDate = "May"

            Case Is = "6"
                conDate = "Jun"

            Case Is = "7"
                conDate = "Jul"

            Case Is = "8"
                conDate = "Aug"

            Case Is = "9"
                conDate = "Sep"

            Case Is = "10"
                conDate = "Oct"

            Case Is = "11"
                conDate = "Nov"

            Case Is = "12"
                conDate = "Dec"

        End Select

        Return conDate
    End Function

    Private Sub addContDate(ByVal item As String)
        Dim items As ListItem
        items = New ListItem(RTrim(LTrim(item)))

        If Me.lstSchedule.Items.Contains(items) Then

            ' If lstSchedule.Items.FindByText(item).Value.Count > 0 Then
        Else

            Me.lstSchedule.Items.Add(item)

        End If

    End Sub

    Protected Sub btnSPRemove_Click(sender As Object, e As ImageClickEventArgs) Handles btnSPRemove.Click
        Me.mpSchedule.Show()
    End Sub

    Protected Sub rdClient_CheckedChanged(sender As Object, e As EventArgs) Handles rdClient.CheckedChanged

        If Me.rdClient.Checked = True Then
            Me.txtClientName.Enabled = True
        ElseIf Me.rdClient.Checked = False Then
            Me.txtClientName.Enabled = False
        End If

    End Sub

    Protected Sub txtEmployer_TextChanged(sender As Object, e As EventArgs) Handles txtEmployer.TextChanged

        Dim mylodgment As New Lodgment
        Dim dsUser As DataSet
        Try
            dcSClientName.Items.Clear()
            Dim ws As New wsClients.Clients()
            dsUser = ws.getClients(LTrim(RTrim(Me.txtEmployer.Text)))
            setClientArray(dsUser.Tables("Employers"))
            dcSClientName.DataSource = dsUser.Tables("Employers")
            dcSClientName.DataValueField = "pkiClientID"
            dcSClientName.DataTextField = "txtClientName"
            dcSClientName.SelectedIndex = -1
            For i = 0 To dsUser.Tables("Employers").Rows.Count - 1
                Me.dcSClientName.Items.Add(dsUser.Tables("Employers").Rows(i).Item(1))
            Next
            setClientArray(dsUser.Tables("Employers"))
            Me.mpSchedule.Show()
        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try

    End Sub

    Protected Sub chkEmployer_CheckedChanged(sender As Object, e As EventArgs) Handles chkEmployer.CheckedChanged

        If Me.chkEmployer.Checked = True Then
            Me.txtEmployer.Enabled = True
        ElseIf Me.chkEmployer.Checked = False Then
            Me.txtEmployer.Enabled = False
        End If
        Me.mpSchedule.Show()

    End Sub

    Protected Sub btnSUpdateComment_Click(sender As Object, e As EventArgs) Handles btnSUpdateComment.Click

        scheduleLoader()

        'Dim NewDir As String = Server.MapPath(Session("user"))
        'MakeDirectoryIfExists(NewDir)
        'If IsNothing(Session("user")) = False Then
        'DeleteDir(NewDir)
        'Else
        'End If




        If Me.lstSchedule.Items.Count = 0 Then
            MsgBox("Please Specify the Contribution Month", MsgBoxStyle.Information)
            Exit Sub
        Else
        End If

        ' scheduleLoader()

        Try
            Dim chk As Integer
            Dim i As Integer = 0
            Dim myCon As New SqlClient.SqlConnection
            Dim myComm As New SqlClient.SqlCommand
            Dim de, daUser As New SqlClient.SqlDataAdapter
            Dim dsUser As New DataSet
            Dim dtUser As New DataTable
            Dim contMonth As String = ""

            Do While i < Me.lstSchedule.Items.Count

                If contMonth = "" Then
                    contMonth = (lstSchedule.Items(i).Value.ToString)
                Else
                    contMonth = contMonth & ";" & (lstSchedule.Items(i).Value.ToString)
                End If

                i = i + 1
            Loop



            If Me.dcScheduleType.Text = "--Select Type--" Then
                MsgBox("Please Select the Schedule Type", , "Master Lodgment")
            Else
            End If


            Dim db As New DbConnection
            myCon = db.getConnection(Session("FundName"))
            'myCon.Open()
            myComm.CommandType = CommandType.Text
            myComm.Connection = myCon



            'CInt(ClientCollection.Item(clientName))

            If Me.dcSClientName.Text <> "" Then

                myComm.CommandText = "update ContributionControl set CSD_Schedule ='" & Me.dcScheduleType.SelectedValue & "' , Schedule_Location ='" & Me.txtSLocation.Text & "', CSD_Part_No = '" & Me.txtSPartCount.Text & "', CSD_Remarks = '" & RTrim(LTrim(Me.txtSComments.Text)) & "',CSD_Recieved_Date = '" & Now.Date & "', ContributionPeriod = '" & contMonth & "',Contribution_Month_Year = '" & contMonth & "', SchedulePath = '" & ViewState("destinationFiles").ToString & "',employer_code = '" & CInt(ClientCollection.Item(Me.dcSClientName.Text)) & "' where ID_JournalDetail='" & CInt(Session("lodgmentID")) & "' "

            Else

                myComm.CommandText = "update ContributionControl set CSD_Schedule ='" & chk & "' , Schedule_Location ='" & Me.txtSLocation.Text & "', CSD_Part_No = '" & Me.txtSPartCount.Text & "', CSD_Remarks = '" & RTrim(LTrim(Me.txtSComments.Text)) & "',CSD_Recieved_Date = '" & Now.Date & "', ContributionPeriod = '" & contMonth & "',Contribution_Month_Year = '" & contMonth & "', SchedulePath = '" & ViewState("destinationFiles").ToString & "' where ID_JournalDetail='" & CInt(Session("lodgmentID")) & "' "

            End If

            myComm.CommandType = CommandType.Text
            myComm.Connection = myCon
            myComm.ExecuteNonQuery()



            myComm.CommandText = "select * from tblLodgmentForChase where id_journaldetail = '" & Me.txtSlodgmentID.Text & "'"
            myComm.CommandType = CommandType.Text
            myComm.Connection = myCon
            daUser.SelectCommand = myComm
            daUser.Fill(dsUser, "tblLodgmentForChase")
            dtUser = dsUser.Tables("tblLodgmentForChase")

            If dtUser.Rows.Count > 0 Then

                myComm.CommandText = " update tblLodgmentForChase set schedule_status = '1',recieved_Date = '" & Date.Now.Date & "' where id_journaldetail ='" & txtSlodgmentID.Text & "' "
                myComm.CommandType = CommandType.Text
                myComm.Connection = myCon
                myComm.ExecuteNonQuery()

            Else

            End If

            update_details("Schedule", txtSlodgmentID.Text)
            db.close(Session("FundName"))

            MsgBox("Master Lodgment Updated...", , "Master Lodgment")


        Catch Ex As Exception
            '    MsgBox("" & Ex.Message)
        Finally

        End Try
    End Sub


    Public Sub update_details(ByVal actionType As String, ByVal id As Integer)

        Dim myComm As New SqlClient.SqlCommand, db As New DbConnection
        Dim d_ate As Date = DateTime.Now
        Dim conn As SqlClient.SqlConnection = db.getConnection(Session("FundName"))

        Dim cmdUser As New SqlClient.SqlCommand
        Dim daUser As New SqlClient.SqlDataAdapter
        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        cmdUser = conn.CreateCommand
        
        cmdUser.CommandText = "select isnull(activityHistory,'') as History from contributioncontrol where ID_JournalDetail = '" & id & "'"

        daUser.SelectCommand = cmdUser
        daUser.Fill(dsUser, "ContributionControl")
        dtUser = dsUser.Tables("ContributionControl")
        Dim dt As String = (dtUser.Rows(0).Item("History").ToString)
        Dim comment As String = dtUser.Rows(0).Item("History").ToString
        If dt = "" Then
            comment = actionType & " Updated on " & " " & d_ate & " " & "By" & " " & Session("user")

            myComm.CommandText = "Update contributioncontrol set activityHistory ='" & comment & "' where ID_JournalDetail = '" & id & "'"
            myComm.CommandType = CommandType.Text
            myComm.Connection = conn
            myComm.ExecuteNonQuery()

        Else

            comment = comment & "===" & actionType & " Modified on " & " " & d_ate & " " & "By " & " " & Session("user")
            myComm.CommandText = "Update contributioncontrol set activityHistory ='" & comment & "' where ID_JournalDetail = '" & id & "'"
            myComm.CommandType = CommandType.Text
            myComm.Connection = conn
            myComm.ExecuteNonQuery()

        End If

        conn.Close()

        '  MsgBox("Master Lodgment Updated...")
        

    End Sub


    Private Sub renameFile(ByVal oldName As String, ByVal newName As String)


        If File.Exists(oldName) And Not File.Exists(newName) Then
            File.Move(oldName, newName)
            '  MsgBox("Renaming Successful...")

        Else
        End If

    End Sub
    Private Sub copyFile(ByVal path As String, ByVal destinationDir As String, ByVal destinationFile As String)

        Try
            If File.Exists(path) And Directory.Exists(destinationDir) And Not File.Exists(destinationFile) Then

                File.Copy(path, destinationFile)

            Else
            End If

        Catch ex As Exception

            MsgBox("" & ex.Message)

        End Try
    End Sub
    Private Sub DeleteDir(ByVal DirPath As String)

        Try
            If Directory.Exists(DirPath) Then
                'File.Delete(DirPath)
                Directory.Delete("", True)
            Else
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub DeleteFile(ByVal FilePath As String)

        Try
            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            Else
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSLodgmentUpdate_Click(sender As Object, e As EventArgs) Handles btnSLodgmentUpdate.Click

        If IsNothing(ViewState("lstFiles")) = True Then

        Else
            Dim lst As New List(Of String)
            lst = ViewState("lstFiles")
            MsgBox("" & lst.Count)
        End If

    End Sub

    Protected Sub AjaxFileUpload1_Unload(sender As Object, e As EventArgs) Handles AjaxFileUpload1.Unload

        'MsgBox("Test")

    End Sub

    Protected Sub AjaxFileUpload1_UploadComplete(sender As Object, e As AjaxFileUploadEventArgs) Handles AjaxFileUpload1.UploadComplete

    End Sub

    Protected Sub dcSClientName_TextChanged(sender As Object, e As EventArgs) Handles dcSClientName.TextChanged

        '  MsgBox("" & Me.dcSClientName.Text)
        Me.mpSchedule.Show()


    End Sub
    Private Sub getClientID(clientName As String)
        ClientCollection = New Hashtable
        ClientCollection = ViewState("ClientCollection")
        Dim txtClientID As Integer
        Dim txtSubplanID As Integer, cr As New Contributions

        If Me.ClientCollection.ContainsKey(clientName) Then



            Session("txtClientID") = CInt(ClientCollection.Item(clientName))
            Session("txtSubplanID") = cr.getClientByID(CInt(ClientCollection.Item(clientName)), "Enpower").Rows(0).Item("txtsubplanid").ToString



            'dsUser = ws.getContribution(CInt(ClientCollection.Item(Me.dcClients.SelectedItem.Text)), CDate(txtStartDate.Text), CDate(txtEndDate.Text), "RSA")
            'ViewState("mydt") = dsUser.Tables("Lodgment")
            'BindGrid(dsUser.Tables("Lodgment"))

        End If
    End Sub

    Protected Sub btnViewSchedule_Click(sender As Object, e As EventArgs) Handles btnViewSchedule.Click

        If IsNothing(Session("lodgmentID")) = False Then
            Me.viewSchedule()
        Else
        End If

    End Sub

    
    Protected Sub btnUpdateCRUComment_Click(sender As Object, e As EventArgs) Handles btnUpdateCRUComment.Click

        Try

            ' Dim myComm As New SqlClient.SqlCommand
            ' Dim d_ate As Date = DateTime.Now
            'Dim conn As New SqlClient.SqlConnection
            'conn.ConnectionString = cont_string
            'conn.Open()

            Dim myComm As New SqlClient.SqlCommand, db As New DbConnection
            Dim d_ate As Date = DateTime.Now
            Dim conn As SqlClient.SqlConnection = db.getConnection(Session("FundName"))

            Dim cmdUser As New SqlClient.SqlCommand
            Dim daUser As New SqlClient.SqlDataAdapter
            Dim dsUser As New DataSet
            Dim dtUser As New DataTable
            cmdUser = conn.CreateCommand
            myComm.CommandText = "Update ContributionControl set CRUComment= '" & Me.txtCRUComments.Text & "' where ID_JournalDetail = '" & CInt(Session("lodgmentID")) & "'"
            myComm.CommandType = CommandType.Text
            myComm.Connection = conn
            myComm.ExecuteNonQuery()
            conn.Close()
            txtCRUComments.Text = ""
            Me.mpCRUComment.Hide()
            MsgBox("Master Lodgment Updated...")

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Protected Sub rdReversed_CheckedChanged(sender As Object, e As EventArgs) Handles rdReversed.CheckedChanged

        If Me.rdReversed.Checked = True Then
            Me.txtUReversed.Enabled = True
            Me.txtUProcessed.Enabled = False

            ' Me.txtClientName.Enabled = True
        ElseIf Me.rdReversed.Checked = False Then
            ' Me.txtClientName.Enabled = False
            Me.txtUProcessed.Enabled = False

        End If
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub rdProcessed_CheckedChanged(sender As Object, e As EventArgs) Handles rdProcessed.CheckedChanged

        If Me.rdProcessed.Checked = True Then

            Me.txtUProcessed.Enabled = True
            Me.txtUReversed.Enabled = False
            ' Me.txtClientName.Enabled = True
        ElseIf Me.rdReversed.Checked = False Then
            ' Me.txtClientName.Enabled = False
            'Me.txtUProcessDate.Enabled = False
            Me.txtUReversed.Enabled = False

        End If
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub chkUFilterEmployer_CheckedChanged(sender As Object, e As EventArgs) Handles chkUFilterEmployer.CheckedChanged

        If Me.chkUFilterEmployer.Checked = True Then
            txtUEmployerFilter.Enabled = True
        Else
            txtUEmployerFilter.Enabled = False
        End If
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub txtUEmployerFilter_TextChanged(sender As Object, e As EventArgs) Handles txtUEmployerFilter.TextChanged
        Dim dt As New DataTable, i As Integer
        dcUClientName.Items.Clear()
        Try
            dt = getClientByFind(txtUEmployerFilter.Text)
            For i = 0 To dt.Rows.Count - 1
                Me.dcUClientName.Items.Add(dt.Rows(i).Item(1))
            Next
        Catch ex As Exception

        End Try
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub calProcessDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calProcessDate.VisibleMonthChanged
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub calEDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calEDate.VisibleMonthChanged

    End Sub

    Protected Sub CalSStartDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalSStartDate.VisibleMonthChanged
        Me.mpSchedule.Show()
    End Sub

    Protected Sub CalSEndDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles CalSEndDate.VisibleMonthChanged
        Me.mpSchedule.Show()
    End Sub

    Protected Sub calSDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calSDate.VisibleMonthChanged
        'Me.mpSchedule.Show()
    End Sub

    Protected Sub calScheduleDate_VisibleMonthChanged(sender As Object, e As MonthChangedEventArgs) Handles calScheduleDate.VisibleMonthChanged
        Me.mpSchedule.Show()
    End Sub

    Protected Sub txtUProcessed_TextChanged(sender As Object, e As EventArgs) Handles txtUProcessed.TextChanged

        If Me.rdUNormal.Checked = True Then

            If IsNothing(ViewState("Oustanding")) = False And ValidateNumber(txtUProcessed.Text) = True Then

                If (Convert.ToDouble(ViewState("Oustanding")) - Convert.ToDouble(Me.txtUProcessed.Text)) < "-100" Then
                    txtUProcessed.Text = "0"
                    Me.txtUOutstanding.Text = Convert.ToDouble(ViewState("Oustanding"))
                    Me.mpUploadUpdate.Show()
                    Exit Sub

                Else
                    Me.txtUOutstanding.Text = Convert.ToDouble(ViewState("Oustanding")) - Convert.ToDouble(Me.txtUProcessed.Text)
                End If

            Else
            End If

        ElseIf Me.rdUPenalty.Checked = True Then

            'ViewState("PanaltyPaid") = dt.Rows(0).Item("PanaltyPaid")
            'ViewState("PenaltyProcessed") = dt.Rows(0).Item("PenaltyProcessed")
            'ViewState("PenaltyOutstanding") = dt.Rows(0).Item("PenaltyOutstanding")


            If IsNothing(ViewState("PenaltyOutstanding")) = False And ValidateNumber(txtUProcessed.Text) = True Then

                If (Convert.ToDouble(ViewState("PenaltyOutstanding")) - Convert.ToDouble(Me.txtUProcessed.Text)) < "-100" Then
                    txtUProcessed.Text = "0"
                    Me.txtUOutstanding.Text = Convert.ToDouble(ViewState("PenaltyOutstanding"))
                    Me.mpUploadUpdate.Show()
                    Exit Sub

                Else
                    Me.txtUOutstanding.Text = Convert.ToDouble(ViewState("PenaltyOutstanding")) - Convert.ToDouble(Me.txtUProcessed.Text)
                End If

            Else
            End If


        End If



        
        Me.mpUploadUpdate.Show()

    End Sub

    Protected Sub txtUReversed_TextChanged(sender As Object, e As EventArgs) Handles txtUReversed.TextChanged

        Dim LP As New List(Of lodgmentProperties)
        LP = Session("lodgmentProperties")


        

        '10 11
        'ViewState("PanaltyPaid") = dt.Rows(0).Item("PanaltyPaid")
        'ViewState("PenaltyProcessed") = dt.Rows(0).Item("PenaltyProcessed")
        'ViewState("PenaltyOutstanding") = dt.Rows(0).Item("PenaltyOutstanding")

        If Me.rdUNormal.Checked = True Then
            Me.txtUBalance.Text = LP.Item(7).FieldValue
            txtUOutstanding.Text = LP.Item(9).FieldValue
            If IsNothing(Session("lodgmentProperties")) = False And ValidateNumber(txtUReversed.Text) = True Then

                If CDbl(Me.txtUBalance.Text) >= (Convert.ToDouble(ViewState("Oustanding")) + Convert.ToDouble(Me.txtUReversed.Text)) = True Then
                    Me.txtUOutstanding.Text = Convert.ToDouble(ViewState("Oustanding")) + Convert.ToDouble(Me.txtUReversed.Text)
                Else
                    Me.txtUReversed.Text = ""
                End If
            Else
            End If

        ElseIf Me.rdUPenalty.Checked = True Then
            Me.txtUBalance.Text = LP.Item(10).FieldValue
            txtUOutstanding.Text = LP.Item(12).FieldValue
            If IsNothing(Session("lodgmentProperties")) = False And ValidateNumber(txtUReversed.Text) = True Then

                If CDbl(Me.txtUBalance.Text) >= (Convert.ToDouble(ViewState("PenaltyOutstanding")) + Convert.ToDouble(Me.txtUReversed.Text)) = True Then
                    Me.txtUOutstanding.Text = Convert.ToDouble(ViewState("PenaltyOutstanding")) + Convert.ToDouble(Me.txtUReversed.Text)
                Else
                    Me.txtUReversed.Text = ""
                End If
            Else
            End If

        End If



        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub rdUUpload_CheckedChanged(sender As Object, e As EventArgs) Handles rdUUpload.CheckedChanged

        If rdUUpload.Checked = True Then
            ' dvUCommentBox.Disabled = True
            txtUUploadComment.Enabled = False
        Else
        End If
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub rdUComment_CheckedChanged(sender As Object, e As EventArgs) Handles rdUComment.CheckedChanged

        If rdUComment.Checked = True Then
            'dvUCommentBox.Disabled = False
            txtUUploadComment.Enabled = True
        Else
        End If
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub btnUUpdateComment_Click(sender As Object, e As EventArgs) Handles btnUUpdateComment.Click
        ClientCollection = ViewState("ClientCollection")

        If Me.rdUComment.Checked = True Then

            'If Me.txtUUploadComment.Text = "" Then
            '    MsgBox("Please Enter Comment", MsgBoxStyle.Information)
            '    Exit Sub
            'Else
            commentUpdate(CInt(Session("lodgmentID")), Me.txtUUploadComment.Text)
            '    'Session("lodgmentID") = dt.Rows(0).Item("ID_JournalDetail")
            'End If

        ElseIf Me.rdUUpload.Checked = True Then

            contributionUpdate()

        End If

        Me.txtUUpdateRemarks.Text = ""
        Me.txtUUploadComment.Text = ""
        Me.txtUProcessDate.Text = ""
        Me.txtUReversed.Text = ""
        Me.txtUProcessed.Text = ""
        Me.dcUClientName.Items.Clear()
        Me.txtUEmployerFilter.Text = ""
        Me.txtULodgmentID.Text = ""
        Me.txtUAmount.Text = ""
        Me.rdUNormal.Checked = True
        Me.rdUPenalty.Checked = False
        Me.rdUUpload.Checked = True





    End Sub

    Protected Sub commentUpdate(ByVal lodgmentID As Integer, ByVal comment As String)

        Try

            'Dim con As New SqlClient.SqlConnection
            Dim dsUser As New DataSet
            Dim dtUser As New DataTable
            'Dim status As Integer, UploadAmt As Double
            Dim MyDataAdapter As New SqlClient.SqlDataAdapter
            ' Dim myNarration As String
            'con.ConnectionString = cont_string

            Dim myComm As New SqlClient.SqlCommand, db As New DbConnection
            Dim d_ate As Date = DateTime.Now
            Dim conn As SqlClient.SqlConnection = db.getConnection(Session("FundName"))

            Dim myCommand As SqlCommand = New SqlCommand("update contributioncontrol set FAD_Remarks = @Comment where id_journaldetail = @lodgmentID", conn)
            myCommand.CommandType = CommandType.Text

            myCommand.Parameters.Add("@lodgmentID", SqlDbType.Int)
            myCommand.Parameters("@lodgmentID").Value = lodgmentID

            myCommand.Parameters.Add("@Comment", SqlDbType.VarChar)
            myCommand.Parameters("@Comment").Value = comment

            ' con.Open()
            myCommand.ExecuteNonQuery()
            db.close(Session("FundName"))
            'MsgBox("Master Lodgment Updated...")
            ' Me.Close()

        Catch Ex As Exception
            MsgBox("" & Ex.Message)
        End Try

    End Sub

    Protected Sub contributionUpdate()
        Try

            ' Dim con As New SqlClient.SqlConnection
            Dim dsUser As New DataSet
            Dim dtUser As New DataTable
            Dim status As Integer, UploadAmt As Double
            Dim MyDataAdapter As New SqlClient.SqlDataAdapter
            Dim myNarration As String
            'con.ConnectionString = cont_string

            Dim myComm As New SqlClient.SqlCommand, db As New DbConnection
            Dim d_ate As Date = DateTime.Now
            Dim conn As SqlClient.SqlConnection = db.getConnection(Session("FundName"))


            'If Me.lblSubplan.Text = "" Then
            '    MsgBox("Please Select the Client Name...", MsgBoxStyle.Information)
            '    Exit Sub
            'End If

            'If Me.txtRemarks.Text = "" Then
            '    MsgBox("Please Enter Comment For the Update ...", MsgBoxStyle.Information)
            '    Exit Sub
            'End If

            If Me.rdProcessed.Checked = True Then
                status = 1
                UploadAmt = Convert.ToDouble(Me.txtUProcessed.Text)
            ElseIf Me.rdReversed.Checked = True Then
                status = 0
                UploadAmt = Convert.ToDouble(Me.txtUReversed.Text)
            Else
            End If

            myNarration = Me.txtUDescription.Text.Replace("'", "''")

            Dim myCommand As SqlCommand = New SqlCommand("ml_sp_updatelodgement", conn)
            myCommand.CommandType = CommandType.StoredProcedure


            myCommand.Parameters.Add("@lodgmentID", SqlDbType.Int)
            myCommand.Parameters("@lodgmentID").Value = CInt(Session("lodgmentID"))



            myCommand.Parameters.Add("@UpdateAmt", SqlDbType.Decimal)
            myCommand.Parameters("@UpdateAmt").Value = UploadAmt


            myCommand.Parameters.Add(New SqlClient.SqlParameter("@ProcessDate", SqlDbType.DateTime))
            myCommand.Parameters("@ProcessDate").Value = CDate(Me.txtUProcessDate.Text)


            myCommand.Parameters.Add(New SqlClient.SqlParameter("@UStatus", SqlDbType.Int))
            myCommand.Parameters("@UStatus").Value = status


            myCommand.Parameters.Add(New SqlClient.SqlParameter("@Narration", SqlDbType.VarChar))
            myCommand.Parameters("@Narration").Value = myNarration


            myCommand.Parameters.Add(New SqlClient.SqlParameter("@Comment", SqlDbType.VarChar))
            myCommand.Parameters("@Comment").Value = LTrim(RTrim(txtUUpdateRemarks.Text))


            myCommand.Parameters.Add(New SqlClient.SqlParameter("@User", SqlDbType.VarChar))
            myCommand.Parameters("@User").Value = Session("user")

            myCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.VarChar))
            myCommand.Parameters("@ClientID").Value = CInt(ClientCollection.Item(Me.dcUClientName.Text))



            myCommand.Parameters.Add(New SqlClient.SqlParameter("@batchRef", SqlDbType.VarChar))
            myCommand.Parameters("@batchRef").Value = ""

            If Me.rdUNormal.Checked = True Then

                myCommand.Parameters.Add(New SqlClient.SqlParameter("@updateType", SqlDbType.Bit))
                myCommand.Parameters("@updateType").Value = 1

            ElseIf Me.rdUPenalty.Checked = True Then


                myCommand.Parameters.Add(New SqlClient.SqlParameter("@updateType", SqlDbType.Bit))
                myCommand.Parameters("@updateType").Value = 0

            End If

            db.close(Session("FundName"))
            myCommand.ExecuteNonQuery()

            'MsgBox("Master Lodgment Updated...")
            Me.mpUploadUpdate.Hide()

        Catch Ex As Exception
            MsgBox("" & Ex.Message)
        End Try



    End Sub

    Protected Sub btnScheduleUpdate_Click(sender As Object, e As EventArgs) Handles btnScheduleUpdate.Click

    End Sub

    Protected Sub dcUClientName_TextChanged(sender As Object, e As EventArgs) Handles dcUClientName.TextChanged
        Me.mpUploadUpdate.Show()
    End Sub

    Protected Sub gridContribution_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gridContribution.SelectedIndexChanging

    End Sub

    Protected Sub gridUploadDetails_PageIndexChanged(sender As Object, e As EventArgs) Handles gridUploadDetails.PageIndexChanged

        'Dim dt As New DataTable
        'dt = Session("UploadDetail")
        ''ViewState("UploadDetail") = dsUser.Tables(0)
        'BindGridUploadDetail(dt)

        '     getUploadDetails(Session("lodgmentID"))

    End Sub

    Protected Sub gridUploadDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridUploadDetails.PageIndexChanging

       

        If IsNothing(ViewState("UploadDetail")) = False Then
            Me.gridUploadDetails.PageIndex = e.NewPageIndex
            Me.BindGridUploadDetail(ViewState("UploadDetail"))


        Else

        End If


    End Sub

    Protected Sub gridReversalDetail_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridReversalDetail.PageIndexChanging

        If IsNothing(ViewState("ReversalDetail")) = False Then
            Me.gridReversalDetail.PageIndex = e.NewPageIndex
            Me.BindGridReversalDetail(ViewState("ReversalDetail"))
        Else
        End If

    End Sub

    Protected Sub gridRefundDetail_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridRefundDetail.PageIndexChanging

        If IsNothing(ViewState("RefundDetail")) = False Then
            Me.gridRefundDetail.PageIndex = e.NewPageIndex
            Me.BindGridRefundDetail(ViewState("RefundDetail"))
        Else
        End If

    End Sub

    Protected Sub rdUPenalty_CheckedChanged(sender As Object, e As EventArgs) Handles rdUPenalty.CheckedChanged

        If Me.rdUPenalty.Checked = True Then
            '    Me.rdUNormal.Checked = False

            'ViewState("PanaltyPaid") = dt.Rows(0).Item("PanaltyPaid")
            'ViewState("PenaltyProcessed") = dt.Rows(0).Item("PenaltyProcessed")
            'ViewState("PenaltyOutstanding") = dt.Rows(0).Item("PenaltyOutstanding")

            Me.txtUBalance.Text = ViewState("PanaltyPaid")
            Me.txtUOutstanding.Text = ViewState("PenaltyOutstanding")

        Else

        End If

        Me.mpUploadUpdate.Show()


    End Sub

    Protected Sub rdUNormal_CheckedChanged(sender As Object, e As EventArgs) Handles rdUNormal.CheckedChanged

        Dim LP As New List(Of lodgmentProperties)
        LP = Session("lodgmentProperties")

        If Me.rdUNormal.Checked = True Then
            Me.txtUBalance.Text = LP.Item(7).FieldValue
            txtUOutstanding.Text = LP.Item(9).FieldValue
        Else
        End If

        Me.mpUploadUpdate.Show()


    End Sub

    
    Protected Sub btnUploadUpdate_Click(sender As Object, e As EventArgs) Handles btnUploadUpdate.Click

    End Sub

     Protected Sub dcClients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dcClients.SelectedIndexChanged

     End Sub

     Protected Sub rdAmount_CheckedChanged(sender As Object, e As EventArgs) Handles rdAmount.CheckedChanged

     End Sub
End Class

Public Class lodgmentProperties

    Dim fName As String
    Dim fValue As String
    ' Dim LocalAmount As Double
    'Dim ValueDate As Date
    'Dim CSD_Schedule As String
    'Dim CSD_Remarks As String
    'Dim CSD_Recieved_Date As Date
    'Dim CSD_Part_No As Integer
    'Dim CSV_Verification As Integer
    'Dim Verif_Remarks As String
    'Dim Verif_Date As Date
    'Dim Verified_By As String
    'Dim Reversals As Double
    'Dim Refunds As Double
    'Dim ACC_Remarks As Double
    'Dim Balancee As Double
    'Dim Processed_Fund As Double
    'Dim Oustanding As Double

    Property FieldName As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            fName = value
        End Set
    End Property
    Property FieldValue As String
        Get
            Return fValue
        End Get
        Set(ByVal value As String)
            fValue = value
        End Set
    End Property

End Class
