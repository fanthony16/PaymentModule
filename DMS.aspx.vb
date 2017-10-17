Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Partial Class DMS
	Inherits System.Web.UI.Page

	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Response.Redirect("Default.aspx")

		'Dim request As HttpWebRequest
		'Dim response As HttpWebResponse = Nothing
		'Dim reader As StreamReader
		'Dim uPWD As String = ConfigurationManager.AppSettings("FileNetUPWD")
		'Dim nc As New NetworkCredential
		'nc.Domain = "pensure-nigeria.com"
		'nc.UserName = "o-taiwo"
		'nc.Password = "fanthony16,..,"


		'Dim prxy As New WebProxy("172.16.0.8:8080", True)
		'prxy.Credentials = nc

		'request = DirectCast(WebRequest.Create("https://www.leadway-pensure.com/XownCMS/jobsubscriber"), HttpWebRequest)
		'request.Proxy = prxy

		'request.Headers("Accept-Language") = "en-US"
		'request.Accept = "text/html, application/xhtml+xml, */*"
		'request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)"

		'ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
		'response = DirectCast(request.GetResponse(), HttpWebResponse)
		'reader = New StreamReader(response.GetResponseStream())
		'Dim rawresp = reader.ReadToEnd()		

		'Dim js As New JavaScriptSerializer
		'Dim JBS = js.Deserialize(Of List(Of JobScriober))(rawresp)

		'MsgBox("" & JBS(0).Email_Address)


	End Sub

	Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

		Dim dms As New PaymentModuleDMSWindow.CEEntry, DocumentID As String
		Try
			Dim uName As String, uPWD As String, uRI As String

			uName = ConfigurationManager.AppSettings("FileNetUName")
			uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
			uRI = ConfigurationManager.AppSettings("FileNetURI")

			'mf.Usernames = ConfigurationManager.AppSettings("mailfilterUser")
			'mf.Passwords = ConfigurationManager.AppSettings("mailfilterPW")
			'mf.Domains = ConfigurationManager.AppSettings("mailfilterDomains")
			'mf.Urls = ConfigurationManager.AppSettings("mailfilterUrls")
			'mf.subject = ConfigurationManager.AppSettings("mailfiltersubject")
			'mf.texts = ConfigurationManager.AppSettings("mailfiltertexts")

			'dms.getConnection("o-taiwo", "fanthony16,,..", "http://172.16.0.32:9080/wsi/FNCEWS40MTOM/")

			dms.getConnection(uName, uPWD, uRI)

			DocumentID = dms.GetDocument("C:\deleted\dms", "{9FEF1493-BAA7-470F-BAD0-C9D93EB5C9F7}", "LPPFA", "")
			'MsgBox("Good : " & DocumentID)

			Me.Label1.Text = "Good : " & DocumentID

		Catch ex As Exception
			'MsgBox("" & ex.Message)
			'Me.Label1.Text = "Good : " & DocumentID
		End Try
		

	End Sub
End Class

<Serializable()> Public Class JobScriober

	Private txtID As String
	Private txtFirst_name As String
	Private txtLast_name As String
	Private txtEmail_Address As String
	Private txtPhone_number As String

	Property ID As String
		Get
			Return txtID
		End Get
		Set(ByVal value As String)
			txtID = value
		End Set
	End Property

	Property First_name As String
		Get
			Return txtFirst_name
		End Get
		Set(ByVal value As String)
			txtFirst_name = value
		End Set
	End Property

	Property Last_name As String
		Get
			Return txtLast_name
		End Get
		Set(ByVal value As String)
			txtLast_name = value
		End Set
	End Property

	Property Email_Address As String
		Get
			Return txtEmail_Address
		End Get
		Set(ByVal value As String)
			txtEmail_Address = value
		End Set
	End Property

	Property Phone_number As String
		Get
			Return txtPhone_number
		End Get
		Set(ByVal value As String)
			txtPhone_number = value
		End Set
	End Property




End Class