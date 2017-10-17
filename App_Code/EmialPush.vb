Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.IO
'Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Net
Public Class EmialPush


    Public Sub sendMail(msg As String, PDate As Date)
        Dim sb As New StringBuilder



        Dim MyMailMessage As New MailMessage()
        MyMailMessage.From = New MailAddress("<Fund_Movement@leadway-pensure.com>", "Daily Fund Movement")


        MyMailMessage.Bcc.Add("<o-taiwo@leadway-pensure.com>")
        'MyMailMessage.Bcc.Add("<o-elegah@leadway-pensure.com>")


        Dim j As Integer = 1

        'MyMailMessage.Subject = "Testing Name..." & PDate

        MyMailMessage.Subject = "Fund Movement Approval AS AT " & PDate

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
        SMPT.Host = "venus4"
        SMPT.Port = 25
        SMPT.UseDefaultCredentials = False
        SMPT.Credentials = New NetworkCredential("rmassms@leadway-pensure.com", "bonus+3aa", "pensure-nigeria.com")
        SMPT.Send(MyMailMessage)


    End Sub

    Public Sub sendMailWithAttachment(msg As String, header As String, filePath As String)
        Dim sb As New StringBuilder



        Dim MyMailMessage As New MailMessage()
        MyMailMessage.From = New MailAddress("<info@leadway-pensure.com>", "Trade Mandate Notification")


        MyMailMessage.To.Add("<o-taiwo@leadway-pensure.com>")
        'MyMailMessage.Bcc.Add("<o-elegah@leadway-pensure.com>")


        Dim j As Integer = 1

        'MyMailMessage.Subject = "Testing Name..." & PDate

        'MyMailMessage.Subject = "Trade Mandate Notification AS AT " & PDate
        MyMailMessage.Subject = header

        MyMailMessage.IsBodyHtml = True
        MyMailMessage.Body = msg.ToString

        MyMailMessage.Attachments.Add(New Attachment(filePath))

        Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", Nothing, "text/plain")

        Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(msg.ToString & "<img src=cid:companylogo>", Nothing, "text/html")



        Dim logo As New LinkedResource("C:\GeneralProject\MasterLodgment\Images\logo.bmp")
        logo.ContentId = "companylogo"
        htmlView.LinkedResources.Add(logo)


        MyMailMessage.AlternateViews.Add(htmlView)
        MyMailMessage.AlternateViews.Add(plainView)

        MyMailMessage.IsBodyHtml = False
        Dim SMPT As New SmtpClient
        SMPT.Host = "venus4"
        SMPT.Port = 25
        SMPT.UseDefaultCredentials = False
        SMPT.Credentials = New NetworkCredential("rmassms@leadway-pensure.com", "bonus+3aa", "pensure-nigeria.com")
        SMPT.Send(MyMailMessage)


    End Sub

    Public Sub sendMailWithAttachmentAddess(msg As String, header As String, filePath As String, lstAddress As List(Of EmailAddress))
        Dim sb As New StringBuilder



        Dim MyMailMessage As New MailMessage()
        MyMailMessage.From = New MailAddress("<info@leadway-pensure.com>", "Trade Mandate Notification")
        Dim i As Integer = 0

        Do While i < lstAddress.Count

            If lstAddress(i).Reciever = True Then
                MyMailMessage.To.Add(lstAddress(i).EmailAddress)
            ElseIf lstAddress(i).Reciever = False Then
                MyMailMessage.To.Add(lstAddress(i).EmailAddress)
            End If

            i = i + 1
        Loop

        ' MyMailMessage.To.Add("<o-taiwo@leadway-pensure.com>")
        'MyMailMessage.Bcc.Add("<o-elegah@leadway-pensure.com>")
        'MyMailMessage.Subject = "Testing Name..." & PDate

        'MyMailMessage.Subject = "Trade Mandate Notification AS AT " & PDate
        MyMailMessage.Subject = header

        MyMailMessage.IsBodyHtml = True
        MyMailMessage.Body = msg.ToString

        MyMailMessage.Attachments.Add(New Attachment(filePath))

        Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", Nothing, "text/plain")

        Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(msg.ToString & "<img src=cid:companylogo>", Nothing, "text/html")



        Dim logo As New LinkedResource("C:\GeneralProject\MasterLodgment\Images\logo.bmp")
        logo.ContentId = "companylogo"
        htmlView.LinkedResources.Add(logo)


        MyMailMessage.AlternateViews.Add(htmlView)
        MyMailMessage.AlternateViews.Add(plainView)

        MyMailMessage.IsBodyHtml = False
        Dim SMPT As New SmtpClient
        SMPT.Host = "venus4"
        SMPT.Port = 25
        SMPT.UseDefaultCredentials = False
        SMPT.Credentials = New NetworkCredential("rmassms@leadway-pensure.com", "bonus+3aa", "pensure-nigeria.com")
        SMPT.Send(MyMailMessage)


    End Sub

End Class
Public Class EmailAddress

    Dim txtEmailAddress As String
    Dim IsReciever As Boolean


    Property EmailAddress() As String

        Get
            Return txtEmailAddress
        End Get
        Set(ByVal value As String)
            txtEmailAddress = value
        End Set

    End Property

    Property Reciever() As Boolean

        Get
            Return IsReciever
        End Get
        Set(ByVal value As Boolean)
            IsReciever = value
        End Set

    End Property

End Class



