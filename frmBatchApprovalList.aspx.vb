Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Partial Class frmBatchApprovalList
    Inherits System.Web.UI.Page

     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          If Page.IsPostBack = False Then


               getGridData()

               Try

               Catch ex As Exception
                    MsgBox("" & ex.Message)
               End Try

          Else

          End If

     End Sub


     Protected Sub getGridData()

          'Dim dtUser As DataTable
          Dim cr As New Core, lstMovements As New List(Of MovementProperties), dt As New DataTable, i As Integer
          dt = cr.getMovedBatch("F")
          If dt.Rows.Count > 0 Then
               ViewState("mydt") = dt
               Do While i < dt.Rows.Count

                    Dim lstMovement As New MovementProperties

                    lstMovement.BatchNo = dt.Rows(0).Item("txtbatchNo")

                    lstMovement.HomeUnit = dt.Rows(0).Item("HomeUnit")

                    lstMovement.HomeValue = dt.Rows(0).Item("HomeValue")

                    lstMovement.MovementDate = dt.Rows(0).Item("CreatedDate")

                    lstMovements.Add(lstMovement)
                    i = i + 1
               Loop
               convertListObject(lstMovements)

          Else
               convertListObject(lstMovements)
          End If

     End Sub


     Protected Sub gridMovementDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          'If IsNothing(ViewState("mydt")) = False Then

          '     Dim dt As DataTable = ViewState("mydt")
          '     If e.Row.RowType = DataControlRowType.DataRow Then

          '          If dt.Rows(e.Row.RowIndex).Item("txtMandateStatus").ToString = "P" Then

          '               e.Row.ForeColor = System.Drawing.Color.Brown

          '          ElseIf dt.Rows(e.Row.RowIndex).Item("txtMandateStatus").ToString = "F" Then

          '               e.Row.ForeColor = System.Drawing.Color.OrangeRed

          '          ElseIf dt.Rows(e.Row.RowIndex).Item("txtMandateStatus").ToString = "A" Then

          '               e.Row.ForeColor = System.Drawing.Color.Green
          '               e.Row.Enabled = False

          '          Else

          '          End If

          '     End If
          'Else
          'End If

     End Sub


     Protected Sub gridMovementSummary_RowDataBound(sender As Object, e As GridViewRowEventArgs)

          'If IsNothing(ViewState("mydt")) = False Then

          '     Dim dt As DataTable = ViewState("mydt")
          '     If e.Row.RowType = DataControlRowType.DataRow Then

          '          If dt.Rows(e.Row.RowIndex).Item("txtMandateStatus").ToString = "P" Then

          '               e.Row.ForeColor = System.Drawing.Color.Brown

          '          ElseIf dt.Rows(e.Row.RowIndex).Item("txtMandateStatus").ToString = "F" Then

          '               e.Row.ForeColor = System.Drawing.Color.OrangeRed

          '          ElseIf dt.Rows(e.Row.RowIndex).Item("txtMandateStatus").ToString = "A" Then

          '               e.Row.ForeColor = System.Drawing.Color.Green
          '               e.Row.Enabled = False

          '          Else

          '          End If

          '     End If
          'Else
          'End If

     End Sub

     Protected Sub convertListObject(lst As List(Of MovementProperties))

          Dim dtuser As New DataTable
          Dim dsUser As New DataSet

          Dim dc As DataColumn = New DataColumn("BatchNo")
          dtuser.Columns.Add(dc)

          dc = New DataColumn("HomeUnit")
          dtuser.Columns.Add(dc)

          dc = New DataColumn("HomeValue")
          dtuser.Columns.Add(dc)
       
          dc = New DataColumn("MovementDate")
          dtuser.Columns.Add(dc)

          Dim i As Integer = 0
          Try

               Do While i < lst.Count

                    Dim newCustomersRow As DataRow
                    newCustomersRow = dtuser.NewRow()
                    Dim mlst As New MovementProperties
                    mlst = lst(i)
                    newCustomersRow("BatchNo") = mlst.BatchNo
                    newCustomersRow("HomeUnit") = mlst.HomeUnit
                    newCustomersRow("HomeValue") = mlst.HomeValue
                    newCustomersRow("MovementDate") = mlst.MovementDate

                    dtuser.Rows.Add(newCustomersRow)

                    i = i + 1
               Loop
               BindSummary(dtuser)
          Catch ex As Exception
               'MsgBox("" & ex.Message)
          End Try

     End Sub


     Private Sub getSummary(reportType As Integer, fundID As String, pin As String)

          Dim cr As New Core
          Dim dtUser As New DataTable
          dtUser = cr.getMovementData(reportType, fundID, pin, "", "")
          BindSummary(dtUser)

     End Sub


     Protected Sub btnEditView_Click(sender As Object, e As EventArgs) Handles btnShowPopup.Click

          Try


               Dim btnDetails As New ImageButton, cr As New Core, dtUsers As DataTable
               btnDetails = sender
               Dim i As GridViewRow
               i = btnDetails.NamingContainer

               dtUsers = cr.getMovementData(Me.gridMovementDetails.Rows(i.RowIndex).Cells(1).Text)
               gridMovementSummary.DataSource = dtUsers
               gridMovementSummary.DataBind()

               mpMailList.Show()

          Catch ex As Exception
               'MsgBox("" & ex.Message)
          End Try

     End Sub

     Private Sub BindSummary(dt As DataTable)

          gridMovementDetails.DataSource = dt
          gridMovementDetails.DataBind()
          'gridMovementDetails.Visible = True

          If dt.Rows.Count < 10 Then
               Me.pnlMovementDetail.Height = Nothing
          Else
               Me.pnlMovementDetail.Height = 300
          End If

     End Sub

     Protected Sub btnUnTagAll_Click(sender As Object, e As EventArgs) Handles btnUnTagAll.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementDetails.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")
               cb.Checked = False

          Next


     End Sub

     Protected Sub btnTagAll_Click(sender As Object, e As EventArgs) Handles btnTagAll.Click
          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementDetails.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")
               cb.Checked = True

          Next
     End Sub

     Protected Sub gridMovementDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMovementDetails.SelectedIndexChanged



     End Sub

     Protected Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click

          'updateMovementBatch

          Dim cb As CheckBox, chk As Integer = 0, cr As New Core
          For Each grow As GridViewRow In Me.gridMovementDetails.Rows

               grow.FindControl("chkSelect")

               cb = grow.FindControl("chkSelect")

               If cb.Checked = True Then

                    cr.updateMovementBatch("A", (Me.gridMovementDetails.Rows(grow.RowIndex).Cells(1).Text), Session("user"))
                    getGridData()
               ElseIf cb.Checked = False Then

               End If

          Next

          ' getRequests()


     End Sub

End Class
