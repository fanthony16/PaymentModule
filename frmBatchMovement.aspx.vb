Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports AjaxControlToolkit

Partial Class frmBatchMovement
     Inherits System.Web.UI.Page

     Protected Sub BtnViewDetails_Click(sender As Object, e As EventArgs) Handles btnShowPopup.Click


          '        select top 10 a.fundid,b.employeeid,b.DateOfBirth,datediff(year,b.DateOfBirth,getdate()) Age,b.RSAPIN, b.FirstName + ' '+	b.LastName +' '+ b.MiddleName as FullName,

          ' (isnull((select sum(isnull(k.UnitValue, 0)) contributions from Contributions k
          'where isnull(k.isConfirmed,0) = 1 and isnull(k.isArchived,0) = 0 and 
          'k.employeeid = b.employeeid and k.fundid = 1 ),0) -

          'isnull((select sum(isnull(k.UnitValue, 0)) payments from Payments k
          'where k.employeeid = b.employeeid and k.fundid = 1 and isnull(k.isConfirmed,0) = 1 and isnull(k.isArchived,0) = 0),0)) NetUnit,

          '(select top 1 UnitPrice from unitprice where fundid = a.fundid order by valuedate desc) as UnitPrice

          'from employeefundmapping a, employee b
          'where a.employeeid = b.employeeid
          'and b.RSAPIN like 'PEN_________%'
          'and not exists (select employeeid from employeefundmapping c where c.employeeID = b.employeeid and fundid = 2) 
          'and a.isActive = 1

     End Sub
     Private Sub getSummary(reportType As Integer, fundID As String, pin As String)

          Dim cr As New Core
          Dim dtUser As New DataTable
          dtUser = cr.getMovementData(reportType, fundID, pin, "", "")
          BindSummary(dtUser)

     End Sub

     Private Sub getDetails(fundID As String, batch As String, fundID2 As String)
          Dim dtUser As New DataTable
          Dim cr As New Core
          If IsNothing(ViewState("PIN")) = True Then
               dtUser = cr.getMovementData(2, fundID, "", batch, fundID2)
          Else
               dtUser = cr.getMovementData(2, fundID, CStr(ViewState("PIN")), "", "")
          End If

          ViewState("Detail") = dtUser
          ViewState("Summary") = dtUser
          gridMovementDetails.DataSource = dtUser
          gridMovementDetails.DataBind()

          If dtUser.Rows.Count < 5 Then
               pnlMovementDetail.Height = 300
          Else
               pnlMovementDetail.Height = Nothing
          End If

     End Sub


     Protected Sub btnGenerateSummary_Click(sender As Object, e As EventArgs) Handles btnGenerateSummary.Click

          ViewState("PIN") = Nothing
          getSummary(1, "", "")
          getDetails(0, "", "")

     End Sub

     Private Sub BindSummary(dt As DataTable)

          ViewState("Summary") = dt
          gridMovementSummary.DataSource = dt
          gridMovementSummary.DataBind()

          If dt.Rows.Count < 10 Then
               Me.pnlFundMovementSummary.Height = Nothing
          Else
               Me.pnlFundMovementSummary.Height = 300
          End If

     End Sub

     Protected Sub gridMovementSummary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMovementSummary.SelectedIndexChanged

          Dim selectedRowIndex As Integer
          Dim selectedLodgment As New ArrayList
          Dim cr As New Core

          selectedRowIndex = gridMovementSummary.SelectedRow.RowIndex
          Dim row As GridViewRow = gridMovementSummary.Rows(selectedRowIndex)

          getDetails((row.Cells(2).Text.ToString()), row.Cells(10).Text.ToString(), (row.Cells(6).Text.ToString()))

          Dim errorDescription As String = ""





     End Sub

     Protected Sub btnSaveSummary_Click(sender As Object, e As ImageClickEventArgs) Handles btnSaveSummary.Click

          If IsNothing(ViewState("Summary")) = False Then
               Dim cr As New Core
               cr.ExtractCSV(ViewState("Summary"), "MovementSummary")
          Else
          End If

     End Sub

     Protected Sub gridMovementDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridMovementDetails.SelectedIndexChanged

         

     End Sub

     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          Dim scriptManagerSummary, scriptManagerDetail As New ScriptManager
          ''dvlabel.Visible = False
          ' dvProcessing.Visible = False

          scriptManagerSummary = ScriptManager.GetCurrent(Me.Page)
          scriptManagerSummary.RegisterPostBackControl(Me.btnSaveSummary)

          scriptManagerDetail = ScriptManager.GetCurrent(Me.Page)
          scriptManagerDetail.RegisterPostBackControl(Me.btnSaveDetails)

          'If IsPostBack Then
          '     If fileUpload.HasFile Then
          '          MsgBox("File Seen")
          '     End If
          'Else

          'End If

     End Sub

     Protected Sub btnSaveDetails_Click(sender As Object, e As ImageClickEventArgs) Handles btnSaveDetails.Click

          If IsNothing(ViewState("Detail")) = False Then
               Dim cr As New Core
               cr.ExtractCSV(ViewState("Detail"), "MovementDetails")
          Else
          End If

     End Sub

     'Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click
     'If Me.fileUpload.HasFile Then
     '     MsgBox("True")
     'End If
     'ViewState("PIN") = Me.txtPIN.Text
     'getSummary(1, 1, CStr(ViewState("PIN")))
     'getDetails(0, "")
     '     Try

     '    MsgBox("" & fileUpload.FileName.ToString)

     'Me.fileUpload.PostedFile.SaveAs(Server.MapPath("FileUploads\" & fileUpload.FileName))
     'Dim cr As New Core, lstMovements As New List(Of MovementProperties)
     'lstMovements = cr.getPINs(Server.MapPath("FileUploads"), fileUpload.FileName)
     'MsgBox("" & lstMovements.Count)
     'AjaxFileUpload1.
     'If AjaxFileUpload1.HasFile Then
     '     MsgBox("Good")
     'Else
     '     MsgBox("Bad")
     'End If

     '    Catch ex As Exception
     ' MsgBox("" & ex.Message)
     '   End Try
     'End Sub


     Protected Sub AjaxFileUploadEvent(ByVal sender As Object, ByVal e As AjaxFileUploadEventArgs)


          'Dim filename As String = System.IO.Path.GetFileName(e.FileName)
          'Dim fullPath As String = System.IO.Path.GetFullPath(e.FileName)

          ''Dim strUploadPath As String = "~/NewFolder1/" + filename

          'Dim strUploadPath As String = "~/FileUploads/"
          'AjaxFileUpload1.SaveAs(Server.MapPath(strUploadPath) + filename)


     End Sub

     Protected Sub btnTag_Click(sender As Object, e As EventArgs) Handles btnTag.Click
          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementSummary.Rows

               grow.FindControl("chkSelect")
               'cb = New CheckBox
               cb = grow.FindControl("chkSelect")
               cb.Checked = True

          Next
     End Sub

     Protected Sub btnUnTag_Click(sender As Object, e As EventArgs) Handles btnUnTag.Click
          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementSummary.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")
               cb.Checked = False

          Next
     End Sub

     Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
          Dim cr As New Core, cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementSummary.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")
               If cb.Checked = True Then

                    cr.updateMovementStatus(grow.Cells(10).Text.ToString(), "F", "", "o-taiwo")

               Else

               End If

          Next

     End Sub

     Protected Sub gridMovementSummary_RowDataBound(sender As Object, e As GridViewRowEventArgs)


          Dim cb As CheckBox
          Dim dt As DataTable = ViewState("Summary")
          If e.Row.RowType = DataControlRowType.DataRow Then
               cb = e.Row.FindControl("chkSelect")

               ' MsgBox("" & dt.Rows(e.Row.RowIndex).Item("MandateStatus").ToString)
               'MandateStatus
               If (dt.Rows(e.Row.RowIndex).Item("Status").ToString.Trim = "F") = True And (dt.Rows(e.Row.RowIndex).Item("MandateStatus").ToString = "") = True Then

                    e.Row.ForeColor = System.Drawing.Color.Red
                    ' e.Row.Enabled = False
                    '  cb.Checked = True

               Else

                    e.Row.ForeColor = System.Drawing.Color.Black

               End If

          End If

     End Sub




    
     Protected Sub btnBTag_Click(sender As Object, e As EventArgs) Handles btnBTag.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementDetails.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")
               cb.Checked = True

          Next

     End Sub

     Protected Sub btnBUnTag_Click(sender As Object, e As EventArgs) Handles btnBUnTag.Click

          Dim cb As CheckBox
          For Each grow As GridViewRow In Me.gridMovementDetails.Rows

               grow.FindControl("chkSelect")
               cb = grow.FindControl("chkSelect")
               cb.Checked = False

          Next

     End Sub

     Protected Sub btnViewTransaction_Click(sender As Object, e As EventArgs) Handles btnViewTransaction.Click

          Dim cr As New Core
          Dim dtUser As New DataTable

          dtUser = cr.getMovementData(Me.txtBatchRef.Text)
          BindSummary(dtUser)
          getDetails(0, "", "")


     End Sub
End Class
