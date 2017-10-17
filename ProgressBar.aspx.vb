
Partial Class ProgressBar
    Inherits System.Web.UI.Page
    Dim script As String = "setTimeout(""__doPostBack('{0}','')"", 5000);"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


    End Sub

    Protected Sub btnInvoke_Click(sender As Object, e As EventArgs) Handles btnInvoke.Click
        System.Threading.Thread.Sleep(3000)
        lblText.Text = "Processing completed"
    End Sub

     Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
          MsgBox("You Clicked Me")
     End Sub
End Class
