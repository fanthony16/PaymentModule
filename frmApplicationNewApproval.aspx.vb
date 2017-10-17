Imports System.Data

Partial Class frmApplicationNewApproval
     Inherits System.Web.UI.Page

     'Dim aAmount As New TextBox
     'Dim aLabel As New Label
     'Dim aTempField As New TemplateField
     '     aTempField.ItemTemplate = aLabel
     '     aTempField.ItemStyle.Width = 100
     '     aTempField.EditItemTemplate = aAmount
     '     gridRMAS.Columns.Add(aTempField)

     'Dim cField As New CommandField
     '     cField.ShowEditButton = True
     '     gridRMAS.Columns.Add(cField)



     Protected Sub BindFieldHardShip()

          Dim bfieldAppCode As New BoundField()
          bfieldAppCode.HeaderText = "Application Code"
          bfieldAppCode.DataField = "txtApplicationCode"
          gridRMAS.Columns.Add(bfieldAppCode)

          Dim bfieldPIN As New BoundField()
          bfieldPIN.HeaderText = "PIN"
          bfieldPIN.DataField = "txtPIN"
          gridRMAS.Columns.Add(bfieldPIN)

          Dim bfieldName As New BoundField()
          bfieldName.HeaderText = "Name"
          bfieldName.DataField = "txtFullName"
          bfieldName.ItemStyle.Width = 150
          gridRMAS.Columns.Add(bfieldName)

          Dim bfieldDDate As New BoundField()
          bfieldDDate.HeaderText = "Disengagement Date"
          bfieldDDate.DataField = "dteDisengagement"
          bfieldDDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldDDate)

          'Dim bfieldAmount As New BoundField()
          'bfieldAmount.HeaderText = "Approved Amount"
          'bfieldAmount.DataField = "Amount"
          'bfieldAmount.DataFormatString = "{0:N}"
          'gridRMAS.Columns.Add(bfieldAmount)

          
          Dim aTempField As New TemplateField
          aTempField.HeaderText = "Approved Amount"
          aTempField.ItemStyle.Width = 100

          aTempField.ItemTemplate = New CreateItemTemplate(ListItemType.Item)
          'gdExportFile.Columns.Add(tfObject);

          gridRMAS.Columns.Add(aTempField)

          Dim bfieldValueDate As New BoundField()
          bfieldValueDate.HeaderText = "Value Date"
          bfieldValueDate.DataField = "ValueDate"
          bfieldValueDate.DataFormatString = "{0:d}"
          gridRMAS.Columns.Add(bfieldValueDate)

          Dim bfieldAccName As New BoundField()
          bfieldAccName.HeaderText = "Account Name"
          bfieldAccName.DataField = "txtAccountName"
          gridRMAS.Columns.Add(bfieldAccName)

          Dim bfieldAccNo As New BoundField()
          bfieldAccNo.HeaderText = "AccountNo"
          bfieldAccNo.DataField = "txtAccountNo"
          gridRMAS.Columns.Add(bfieldAccNo)

          Dim bfieldBankName As New BoundField()
          bfieldBankName.HeaderText = "Bank Name"
          bfieldBankName.DataField = "fkiBankID"
          gridRMAS.Columns.Add(bfieldBankName)

          Dim bfieldBranchName As New BoundField()
          bfieldBranchName.HeaderText = "Branch Name"
          bfieldBranchName.DataField = "fkiBranchID"
          gridRMAS.Columns.Add(bfieldBranchName)

          Dim bUpdateAmount As New TemplateField
          bUpdateAmount.HeaderText = "Edit Amount"
          gridRMAS.Columns.Add(bUpdateAmount)

     End Sub
     Protected Sub EditCustomer(ByVal sender As Object, ByVal e As GridViewEditEventArgs)

          gridRMAS.EditIndex = e.NewEditIndex
          BindGrid(getData)

     End Sub
     Protected Sub CancelEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)

          gridRMAS.EditIndex = -1
          BindGrid(getData)

     End Sub
     Protected Sub UpdateCustomer(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

     End Sub
     Protected Function getData() As DataTable
          Return PMgetPaymentData("(SPLOG-2-2016722-3)", 2, Now.Date)
     End Function
     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
          Dim dt As DataTable

          If IsPostBack = False Then

               If Me.gridRMAS.Columns.Count = 1 Then

                    BindFieldHardShip()
                    dt = getData()
                    ViewState("Application") = dt
                    BindGrid(dt)

               Else
               End If
          Else
               dt = ViewState("Application")
               BindGrid(dt)

          End If

         

     End Sub

     Protected Sub BindGrid(dt As DataTable)
          Try

               'ViewState("Applications") = dt
               Me.gridRMAS.DataSource = dt
               Me.gridRMAS.DataBind()
               'Me.txtAcknowledgmentDate.Text = dt.Rows(0).Item("dteAcknowledgment").ToString
               'Me.txtApprovedDate.Text = dt.Rows(0).Item("dteApproved").ToString
               'Me.txtBatchRef.Text = dt.Rows(0).Item("txtPencomBatch").ToString
               'Me.txtTotalApprovedAmount.Text = dt.Rows(0).Item("numApprovalAmount").ToString

          Catch ex As Exception

               Dim loger As New Global.Logger.Logger
               loger.FileSource = "Payment Module "
               loger.FilePath = AppDomain.CurrentDomain.BaseDirectory & "\Logs"
               loger.Logger(ex.Message & " | " & "Location => PaymentModule_GetApplicationBatch()")


               'AppDomain.CurrentDomain.BaseDirectory & "\Logs"

          End Try


     End Sub

     Public Function PMgetPaymentData(batches As String, AppType As Integer, d_ate As Date) As DataTable
          Try


               Dim db As New DbConnection

               Dim mycon As New SqlClient.SqlConnection
               mycon = db.getConnection("PaymentModule")

               Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable, sql As String = ""

               Select Case AppType

                    Case Is = 2

                         sql = "select a.txtApplicationCode,txtPIN,txtFullName,dteDisengagement,d.[twentyfive-percent-rsa-balance]  as Amount ,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, fkiBankID,fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr500 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in ('SPLOG-2-2016722-3')"



               End Select


               MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
               MyDataAdapter.SelectCommand.CommandType = CommandType.Text
               ' MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batches", SqlDbType.VarChar))
               'MyDataAdapter.SelectCommand.Parameters("@batches").Value = batches
               dsUser = New DataSet()
               MyDataAdapter.Fill(dsUser, "RMAS")
               dtUser = dsUser.Tables("RMAS")

               mycon.Close()

               Return dtUser

          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try

     End Function

     Protected Sub gridRMAS_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
          'If e.Row.RowType = DataControlRowType.DataRow Then

          '     Dim txtAmount As New TextBox()
          '     txtAmount.ID = "txtAmount"
          '     txtAmount.Text = TryCast(e.Row.DataItem, DataRowView).Row("Amount").ToString()
          '     e.Row.Cells(5).Controls.Add(txtAmount)

          '     Dim lnkView As New LinkButton()
          '     lnkView.ID = "lnkView"
          '     lnkView.Text = "Edit Amount"
          '     AddHandler lnkView.Click, AddressOf ViewDetails
          '     lnkView.CommandArgument = TryCast(e.Row.DataItem, DataRowView).Row("txtApplicationCode").ToString()

          '     e.Row.Cells(11).Controls.Add(lnkView)
          'End If
     End Sub


     Protected Sub ViewDetails(sender As Object, e As EventArgs)

          Dim lnkView As LinkButton = TryCast(sender, LinkButton)
          Dim row As GridViewRow = TryCast(lnkView.NamingContainer, GridViewRow)
          Dim id As String = lnkView.CommandArgument
          Dim name As String = row.Cells(3).Text
          Dim country As String = TryCast(row.FindControl("txtAmount"), TextBox).Text
          ClientScript.RegisterStartupScript(Me.[GetType](), "alert", (Convert.ToString((Convert.ToString((Convert.ToString("alert('Id: ") & id) + " Name: ") & name) + " Country: ") & country) + "')", True)

     End Sub
End Class
