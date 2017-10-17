
Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub NavigationMenu_MenuItemClick(sender As Object, e As MenuEventArgs) Handles NavigationMenu.MenuItemClick

    End Sub

    Protected Sub btnLogOut_Click(sender As Object, e As ImageClickEventArgs) Handles btnLogOut.Click

        'MsgBox("Just Login Out")

        If IsNothing(Session("User")) = False Then
            DeleteDir(Server.MapPath(Session("user")))
            Response.Redirect("Login.aspx")
        Else

        End If

    End Sub
    Private Sub DeleteDir(ByVal DirPath As String)

        Try
            If IO.Directory.Exists(DirPath) Then
                'File.Delete(DirPath)
                IO.Directory.Delete(DirPath, True)
            Else
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class

