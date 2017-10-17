Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Net

Public Class EmailCls

    Public Sub sendMail(msg As String, PDate As Date)
        Dim sb As New StringBuilder


        sb.Append("Testing")
        Dim MyMailMessage As New MailMessage()
        MyMailMessage.From = New MailAddress("<info@leadway-pensure.com>", "Leadway Pensure PFA")


        MyMailMessage.Bcc.Add("<o-taiwo@leadway-pensure.com>")
        MyMailMessage.Bcc.Add("<o-elegah@leadway-pensure.com>")


        Dim j As Integer = 1

        MyMailMessage.Subject = "Fund Movement Approval AS AT " & Now.Date

        MyMailMessage.IsBodyHtml = True
        MyMailMessage.Body = msg.ToString

        Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", Nothing, "text/plain")

        Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(msg.ToString & "<img src=cid:companylogo>", Nothing, "text/html")



        Dim logo As New LinkedResource("C:\GeneralProject\MasterLodgment\Images\logo.bmp")
        logo.ContentId = "companylogo"
        htmlView.LinkedResources.Add(logo)


        MyMailMessage.AlternateViews.Add(htmlView)
        MyMailMessage.AlternateViews.Add(plainView)

        MyMailMessage.IsBodyHtml = False
        Dim SMPT As New SmtpClient
        SMPT.Host = "172.16.0.13"
        SMPT.Port = 25
        SMPT.UseDefaultCredentials = False
        SMPT.Credentials = New NetworkCredential("rmassms@leadway-pensure.com", "bonus+3aa", "pensure-nigeria.com")
        SMPT.Send(MyMailMessage)


    End Sub

End Class
