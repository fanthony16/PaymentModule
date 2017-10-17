Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data

Partial Class frmApplicationConfirmation
     Inherits System.Web.UI.Page
     Dim returnPage As String
     Public Sub getUserAccessMenu(uName As String)

          Dim cr As New Core
          Dim dtAccessModule As New DataTable
          Dim aryAccessModule As New ArrayList
          Dim i As Integer, j As Integer, k As Integer
          dtAccessModule = cr.getAccessModule(Session("User"))

          Do While i < dtAccessModule.Rows.Count
               'aryAccessModule.Add(dtAccessModule.Rows(i).Item(1))
               ' MsgBox("" & dtAccessModule.Rows(i).Item(0).ToString)
               aryAccessModule.Add(dtAccessModule.Rows(i).Item(1))
               i = i + 1
          Loop
          i = 0
          j = 0
          k = 0
          Dim M As New System.Web.UI.WebControls.Menu
          Dim n As New System.Web.UI.WebControls.MenuItem
          M = Master.FindControl("NavigationMenu")

          Do While i < M.Items.Count

			j = 0
               ''''main menu''''
               If aryAccessModule.Contains(M.Items(i).Value) = False Then

                    M.Items.RemoveAt(i)

               Else
                    ''''sub menu''''
                    Do While j < M.Items(i).ChildItems.Count

                         If aryAccessModule.Contains(M.Items(i).ChildItems.Item(j).Value) = False Then
                              M.Items(i).ChildItems.RemoveAt(j)
                         Else

                              ''''sub---sub menu''''
                              Do While k < M.Items(i).ChildItems(j).ChildItems.Count

                                   If aryAccessModule.Contains(M.Items(i).ChildItems(j).ChildItems.Item(k).Value) = False Then
                                        M.Items(i).ChildItems(j).ChildItems.RemoveAt(k)
                                   Else
                                        k = k + 1

                                   End If

                              Loop

                              j = j + 1
                         End If

                    Loop
                    i = i + 1
               End If


          Loop

          If aryAccessModule.Count = 0 Then
               Response.Redirect("default.aspx")
          Else
          End If

     End Sub
     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          If Not Context.Request.QueryString("ApplicationCode") Is Nothing Then

               If Not IsNothing(Session("appDetail")) = True And Not IsNothing(Session("user")) = True Then

                    Me.spApplicationID.InnerText = Context.Request.QueryString("ApplicationCode")
                    ViewState("ReturnPage") = Context.Request.QueryString("ReturnPage")
                    getUserAccessMenu(Session("user"))

               Else
                    Response.Redirect("Login.aspx")
               End If

               ' Me.spApplicationID.InnerText = Context.Request.QueryString("ApplicationCode")
               ' ViewState("AppTypeID") = Context.Request.QueryString("ApplicationCode")

          Else

               Response.Redirect("Login.aspx")

          End If

     End Sub

     Protected Sub btnPrintReciept_Click(sender As Object, e As EventArgs) Handles btnPrintReciept.Click

          Dim appdetail As New ApplicationDetail
          appdetail = Session("appdetail")
          Dim filePath As String = Server.MapPath("~/FileDownLoads/" & appdetail.PIN & "_ApplicationReciept.pdf")
          generateFiles(appdetail, filePath)
          ViewState("RecieptPath") = filePath
          DownLoadSNR()

     End Sub

     Private Sub DownLoadSNR()

          If IsNothing(ViewState("RecieptPath")) = False Then

               If CStr(ViewState("RecieptPath")).ToString = "" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
               Else
               End If

               Dim schedulePath As String = ViewState("RecieptPath")
               Try

                    Dim str() As String = schedulePath.Split("|")
                    Dim FI As FileInfo, fileExt As String, i As Integer = 0

                    Do While i < str.Length

                         FI = New FileInfo(str(i).Trim.ToString)
                         fileExt = LCase(FI.Extension)

                         'testing the file type to download from the browser
                         Select Case fileExt

                              Case Is = ".xls"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".xlsx"
                                   ' Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Is = ".csv"
                                   'Process.Start("EXCEL", str(i).Trim.ToString)
                                   Response.ContentType = "application/EXCEL"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Is = ".pdf"
                                   'Process.Start("ACRORD32", str(i).Trim.ToString)

                                   Response.ContentType = "application/pdf"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()


                              Case Is = ".doc"
                                   ' Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".docx"

                                   'Process.Start("WINWORD", str(i).Trim.ToString)

                                   Response.ContentType = "application/WORD"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".jpg"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()

                              Case Is = ".png"
                                   ' Process.Start("EXPLORER", str(i).Trim.ToString)

                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                              Case Else
                                   Response.ContentType = "application/EXPLORER"
                                   Response.Clear()
                                   Response.AppendHeader("Content-Disposition", "attachment;Filename=" & str(i).Trim.ToString)
                                   Response.TransmitFile(str(i).Trim.ToString)
                                   Response.End()
                         End Select
                         i = i + 1
                    Loop
               Catch ex As Exception
                    '   MsgBox("" & ex.Message)
               End Try

          Else
               ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "Schedule Not Mapped", True)
          End If


     End Sub

     Private Sub generateFiles(appdetail As ApplicationDetail, path As String)

          Dim crExportOptions As New ExportOptions
          Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
          Dim crFormatypeOption As New PdfRtfWordFormatOptions
          Dim rdoc As New ReportDocument
          Dim rsource As New CrystalDecisions.Web.CrystalReportSource

          rdoc.Load(Server.MapPath("~/Report/ApplicationReciept2.rpt"))

          'If Not Directory.Exists(path) = True Then
          '    Directory.CreateDirectory(path)
          'End If

          Dim ds As DataSet

          ds = Me.populateReciept(appdetail)
          rdoc.SetDataSource(ds.Tables(0))

          crDiskFileDestinationOptions.DiskFileName = path
          crExportOptions = rdoc.ExportOptions

          With crExportOptions

               .ExportDestinationType = ExportDestinationType.DiskFile
               .ExportFormatType = ExportFormatType.PortableDocFormat
               .ExportDestinationOptions = crDiskFileDestinationOptions
               .ExportFormatOptions = crFormatypeOption

          End With

          rdoc.Export()

     End Sub
     Private Function populateReciept(appdetail As ApplicationDetail) As DataSet

          Dim cr As New Core, dtDocs As New DataTable, i As Integer = 0
          Dim ds As New dsApplicationReciept
		Dim newSNRow As DataRow

          '   dtDocs = cr.PMgetSubmittedDocument(appdetail.PIN, CInt(appdetail.ApplicationID.ToString.Split("-")(1)))
          dtDocs = cr.PMgetSubmittedDocument(appdetail.PIN, CStr(appdetail.ApplicationID.ToString))

          Do While i < dtDocs.Rows.Count

               newSNRow = ds.Tables(0).NewRow
               newSNRow("txtApplicationCode") = appdetail.ApplicationID
               newSNRow("dteApplicationDate") = appdetail.ApplicationDate
               newSNRow("txtApplicationType") = appdetail.ApplicationTypeName
               newSNRow("txtFullName") = appdetail.FullName.Replace("|", "")
               newSNRow("txtEmailAddress") = appdetail.Email
			newSNRow("txtTitle") = appdetail.Title
			newSNRow("Signature") = cr.PMgetParticipantSignature(appdetail.PIN)
			newSNRow("Passport") = cr.PMgetParticipantPassport(appdetail.PIN)



			newSNRow("AccountNumber") = appdetail.AccountNo
			newSNRow("BankName") = cr.PMgetBanks(appdetail.BankID).Rows(0).Item("bankname")
			newSNRow("BankBranchName") = cr.PMgetBankBranches(appdetail.BankID, appdetail.BranchID).Rows(0).Item("BranchName")
			newSNRow("CSDOfficer") = dtDocs.Rows(i).Item("txtCreatedBy")

			'txtCreatedBy

			'Signature
			'AccountNumber
			'CSDOfficer
               If Not IsDBNull(dtDocs.Rows(i).Item("DateRecived")) = True Then
                    newSNRow("txtDocumentName") = dtDocs.Rows(i).Item("txtDocumentName") & " - Recieved"
               Else
                    newSNRow("txtDocumentName") = dtDocs.Rows(i).Item("txtDocumentName") & " - Outstanding"
               End If
               ds.Tables(0).Rows.Add(newSNRow)

               i = i + 1
          Loop

          'If ds.Tables(0).Rows.Count = 0 Then
          '     newSNRow = ds.Tables(0).NewRow
          '     newSNRow("txtApplicationCode") = appdetail.ApplicationID
          '     newSNRow("dteApplicationDate") = appdetail.ApplicationDate
          '     newSNRow("txtApplicationType") = appdetail.ApplicationTypeName
          '     newSNRow("txtFullName") = appdetail.FullName.Replace("|", "")
          '     newSNRow("txtEmailAddress") = appdetail.Email
          '     newSNRow("txtTitle") = appdetail.Title
          '     newSNRow("txtDocumentName") = ""
          '     ds.Tables(0).Rows.Add(newSNRow)
          'Else
          'End If
          

          Return ds

     End Function

     Protected Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click

          Dim em As New EmailGateway.EmailGateway
          Dim lstEmails As New List(Of EmailGateway.EmailAddress)
          Dim lstEmail As New EmailGateway.EmailAddress, cr As New Core, appdetail As ApplicationDetail
          appdetail = Session("appDetail")
		'appdetail.Email = "o-taiwo@leadway-pensure.com"

          If cr.IsValidEmailAddress(appdetail.Email) = True Then

               'builds the email addresses to send the acknowledgment mail to
               lstEmail.EmailAddress = appdetail.Email
               lstEmail.Reciever = True
               lstEmails.Add(lstEmail)

               'generating the path to create the acknowledgment slip as the email attachment
               ' Dim filePath As String = "\\p-midas2\mlive\TradeMandate\" & appdetail.PIN & "_ApplicationReciept.pdf"
               Dim filePath As String = Server.MapPath("~/FileDownLoads/" & appdetail.PIN & "_ApplicationReciept.pdf")

               'testing if the file already exit in the location above
               If File.Exists(filePath) = True Then
                    'sending the acknowledgment email to the customer with the file as an attachment
                    em.sendMailWithAttachmentAddess(Me.AMsg(appdetail), "Benefit Application Notification", filePath, lstEmails)

               Else
                    'generate the file if it does not already exist
                    generateFiles(appdetail, filePath)
                    em.sendMailWithAttachmentAddess(Me.AMsg(appdetail), "Benefit Application Notification", filePath, lstEmails)
               End If

               Me.spEmailError.InnerText = "Email Sent Successfully !"
               dvEmailStatus.Visible = True



          Else
               Me.spEmailError.InnerText = "Bad Email Address !"
               dvEmailStatus.Visible = True
          End If

     End Sub


     Private Function AMsg(appDetails As ApplicationDetail) As String

          Dim msg As String = ""
          Dim sb As New StringBuilder
          ' MsgBox("Enter")


          Try


               sb.Append("<!DOCTYPE html>")
               sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>")

               sb.Append("<title></title>")
               sb.Append("<style type='text/css'>")
               sb.Append(".auto -style2")
               sb.Append("{")
               sb.Append("width: 603px;")
               sb.Append("font-family: 'Trebuchet MS';")
               sb.Append("font-size: 12px;")
               sb.Append("}")

               sb.Append(".auto -style3")
               sb.Append("{")
               sb.Append("width: 307px;")
               sb.Append("font-family: 'Trebuchet MS';")
               sb.Append("font-size: 12px;")
               sb.Append("}")

               sb.Append(".auto -style4")
               sb.Append("{")
               sb.Append("}")
               sb.Append(".auto -style5")
               sb.Append("{")
               sb.Append("width: 219px;")
               sb.Append("font-family: 'Trebuchet MS';")
               sb.Append("font-size: 12px;")
               sb.Append("}")

               sb.Append(".style5 {")
               sb.Append("font-family: 'Trebuchet MS';")
               sb.Append("font-size: 12px;")
               sb.Append("}")
               sb.Append(".style7 {")
               sb.Append("font-family: 'Trebuchet MS';")
               sb.Append("font-size: 12px;")
               sb.Append("font-weight: bold;")
               sb.Append("color: #FFFFFF;")
               sb.Append("}")


               sb.Append("</style>")
               sb.Append("</head>")
               sb.Append("<body>")

               sb.Append("<br></br>")
               sb.Append("Dear <b>" & appDetails.Title & "</b> <b>" & appDetails.FullName.Replace("|", "") & "</b>, with respect to your <b>" & appDetails.ApplicationTypeName & "</b> application. We acknowledge reciept of the necessary documentation. The application code is <b>" & appDetails.ApplicationID & "</b> with an application date of <b>" & appDetails.ApplicationDate & "</b>")

               sb.Append("<br></br>")
               sb.Append("For further enquiries kindly contact us via 01-2800850 or info@leadway-pensure.com")


               sb.Append("<br></br>")


               sb.Append("</body>")
               sb.Append("</html>")

               sb.Append("<br></br>")
               sb.Append("<br></br>")
               sb.Append("Yours faithfully,")
               sb.Append("<br></br>")
               sb.Append("<br></br>")
               sb.Append("For: LEADWAY PENSURE PFA LTD.")

               sb.Append("<br></br>")
               sb.Append("<br></br>")


               msg = sb.ToString
          Catch ex As Exception
               MsgBox("" & ex.Message)
          End Try

          Return msg
     End Function

     Protected Sub imgBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack.Click

          If Not IsNothing(ViewState("ReturnPage")) = True Then
               Response.Redirect("" & "frm" & CStr(ViewState("ReturnPage")) & ".aspx?IsReturn=1")
               'Response.Redirect("frmApplicationConfirmation.aspx?ApplicationCode=" & appDetail.ApplicationID)
          Else
               Response.Redirect("Login.aspx")
          End If

     End Sub
End Class
