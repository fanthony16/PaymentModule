
Partial Class Progress2
    Inherits System.Web.UI.Page

    Protected Sub btnInvoke_Click(sender As Object, e As EventArgs) Handles btnInvoke.Click

        System.Threading.Thread.Sleep(3000)
        lblText.Text = "Processing completed"
    End Sub
End Class
