
Partial Class frmTestExcelPage
    Inherits System.Web.UI.Page

	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Try

			Dim excel = New Microsoft.Office.Interop.Excel.Application()
			Dim wsf As Microsoft.Office.Interop.Excel.WorksheetFunction = excel.WorksheetFunction

		Catch ex As Exception

			spUpdateStatus.InnerText = ex.Message



		End Try
	End Sub
End Class
