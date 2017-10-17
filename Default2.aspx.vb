Imports FileNet.Api
Imports System.Data

Partial Class _Default2
     Inherits System.Web.UI.Page




     Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

          'Dim message As String = "Do you want to Submit?"
          'Dim sb As New System.Text.StringBuilder()
          'sb.Append("return alert('")
          'sb.Append(message)
          'sb.Append("');")
          'ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())

          Dim cr As New Core
          '          Response.Write(cr.PMGetAVCTax(8346.2))

          Dim avD As New AVCDetails, curNoTaxAVC As Double, curTaxAVC As Double, TotalTax As Double
          curTaxAVC = CDec(304849.4076) * CDec(2.3892)
          curNoTaxAVC = CDec(0.0) * CDec(2.3892)

          TotalTax = cr.PMGetAVCTax(((curNoTaxAVC - 0.0)))
          TotalTax = TotalTax + cr.PMGetAVCTax((curTaxAVC - 720000))

          avD.AVCCurrentValue = curNoTaxAVC + curTaxAVC
          avD.AVCTaxDeduction = TotalTax
          avD.AVCNetPayable = (curNoTaxAVC + curTaxAVC) - TotalTax
          Response.Write(TotalTax)



          ' Response.Write(String.Format("{0:MM/dd/yy}", DateTime.Now))
          ' Response.Write(String.Format("{0:s}", DateTime.Now))
          'Dim dtTime As DateTime = DateTime.Now
          'If (dtTime.TimeOfDay = TimeSpan.Zero) Then
          '     Response.Write(String.Format("{0:d}", dtTime))
          'Else
          '     Response.Write(String.Format("{0:g}", dtTime))
          'End If
          ' Dim i As FileNet.Api.Action.Checkout
          ' i.GetDateTimeValue()
          ' Me.MPRMASHardShip.Show()
          'Dim pr As New com.sms.test.allpricesResponsePriceresponse
          'Dim i As New com.sms.test.swsinfo
          'pr = i.allprices(com.sms.test.allpricesFund.RSAFund, True, com.sms.test.allpricesRange.Last14Days, True)
          ''pr.priceinfo(0).pricevalue
          'Dim j As Integer = 0
          ''Dim dtPrices As New DataTable, dtColumn As DataColumn
          ''dtPrices = New DataTable

          ''dtColumn = New DataColumn("PriceDate")
          ''dtPrices.Columns.Add(dtColumn)

          ''dtColumn = New DataColumn("UnitPrice")
          ''dtPrices.Columns.Add(dtColumn)

          'Do While j < pr.priceinfo.Length
          '     MsgBox("" & pr.priceinfo(j).pricevalue & " " & pr.priceinfo(j).valuedate.ToString)

          '     'Dim newPriceRow As DataRow
          '     'newPriceRow = dtPrices.NewRow()

          '     'newPriceRow("PriceDate") = pr.priceinfo(j).valuedate.ToString
          '     'newPriceRow("UnitPrice") = pr.priceinfo(j).pricevalue
          '     'dtPrices.Rows.Add(newPriceRow)
          '     j = j + 1
          'Loop
          ''Me.gridApprovalBatch.DataSource = dtPrices
          'Me.gridApprovalBatch.DataBind()


     End Sub

	Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

		'Context.Response.Write("<script language='javascript'>window.open('AccountsStmt.aspx?showledger=" & sledgerGrp & "','_newtab');</script>")

	End Sub
End Class
