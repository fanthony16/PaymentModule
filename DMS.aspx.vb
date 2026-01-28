Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Data

Partial Class DMS
	Inherits System.Web.UI.Page

	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Dim appDocs(2) As String

		appDocs(0) = "Me"
		appDocs(1) = "You"
		appDocs(2) = "We"

		MsgBox("" & appDocs(2))

		Exit Sub


		Response.Redirect("Default.aspx")


		Dim crm As New CRM.ApplicationDocumentDetail
		Dim crmm As New CRM.BankCls


		Dim crmmm As New CRM.LPPFARequest
		Dim appDetail As New CRM.NPMOnlineApplicationDetail
		Dim appDocDetail As New CRM.ApplicationDocumentDetail

		appDocDetail.dateReceived = Now
		appDocDetail.documentHashValue = Nothing
		appDocDetail.documentLocation = ""
		appDocDetail.documentTypeID = ""
		appDocDetail.documentTypeName = ""
		appDocDetail.isVerified = ""
		appDocDetail.memberApplicationID = ""



		appDetail.accountName = ""
		appDetail.accountNumber = ""
		appDetail.applicationDocuments = Nothing

		appDetail.applicationTypeName = ""
		appDetail.appTypeId = ""
		appDetail.aVCApplicationAmount = ""
		appDetail.createdBy = ""
		appDetail.customerBankBranchID = ""
		appDetail.customerBankID = ""
		appDetail.dateDisengagement = ""
		appDetail.dateRetirement = ""
		appDetail.department = ""
		appDetail.designation = ""
		appDetail.pIN = ""
		appDetail.reason = ""
		appDetail.retirementGround = ""
		appDetail.sourceId = ""
		appDetail.stateId = ""
		appDetail.tIN = ""


		'crmmm.DropBenefitApplication()
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

			DocumentID = dms.DropDocument("C:\deleted\dms\Testing.txt", "Test", "LPPFA_BPD")


			MsgBox("" & DocumentID)

			Exit Sub

			DocumentID = dms.GetDocument("C:\deleted\dms", "{9FEF1493-BAA7-470F-BAD0-C9D93EB5C9F7}", "LPPFA", "")
			'MsgBox("Good : " & DocumentID)



			Me.Label1.Text = "Good : " & DocumentID

		Catch ex As Exception
			'MsgBox("" & ex.Message)
			'Me.Label1.Text = "Good : " & DocumentID
		End Try


	End Sub

	Protected Sub btnAVC_Click(sender As Object, e As EventArgs) Handles btnAVC.Click

		Dim avc As New AVCSimulator.AVCCalculator, lumpSum As Double, fv As Double
		'lumpSum = avc.CalulateLumpSum("", "2497235.21", "20000", "1964/02/18", "1")
		'	lumpSum = avc.CalulateLumpSum("", "338213.05", "20000", "1964/02/18", "1")
		'lumpSum = CalulateLumpSum("", "338213.05", "20000", "1964/02/18", "0")

		'fv = avc.getFV(10000, 65, 338213.05, 2500, "1964/02/18", "20000", 1)

		MsgBox("LumpSum :" & lumpSum & "Future Value : " & fv)

	End Sub

	Public Function getFV(lifeStyle As Double, retirementAge As Integer, rsabalance As Double, monthContribution As Double, dob As Date, txtRecommendedMonthly As Double) As Double
		Dim txtFVDOB As Date, txtYearsToRetirement As Double, txtMonthsToRetirement As Double, txtPeriodicity As Integer = 12, txtNominalMonthly As Decimal, txtNominalAnnual As String = "12%", txtContGrowthRate As Double, txtSalaryGrowthAnnual As String = "1%", txtFVGrowthAsset As Double, txtFVCurAsset As Double, txtFVContribution As Double, txtFV As Double

		txtFVDOB = dob
		'field2
		txtYearsToRetirement = FormatNumber(CInt(retirementAge) - ((DateDiff(DateInterval.Day, CDate(txtFVDOB), Now.Date)) / 365), 6)
		'field3
		txtMonthsToRetirement = FormatNumber(txtYearsToRetirement * CInt(txtPeriodicity), 6)

		'field4
		txtNominalMonthly = (1 + (FormatNumber(CDbl(txtNominalAnnual.Split("%")(0)) / 100, 5))) ^ (1 / txtPeriodicity) - 1
		'field5
		txtContGrowthRate = (1 + (FormatNumber(CDbl(txtSalaryGrowthAnnual.Split("%")(0)) / 100, 5))) ^ (1 / txtPeriodicity) - 1

		'field6
		txtFVGrowthAsset = FormatNumber((monthContribution / (txtNominalMonthly - CDbl(txtContGrowthRate))) * ((1 + txtNominalMonthly) ^ (txtMonthsToRetirement) - (1 + CDbl(txtContGrowthRate)) ^ txtMonthsToRetirement), 2)

		'field7
		txtFVCurAsset = FormatNumber(rsabalance * (1 + FormatNumber(CDbl(txtNominalAnnual.Split("%")(0)) / 100, 5)) ^ txtYearsToRetirement, 2)

		'field8
		txtFVContribution = FormatNumber(CDbl(txtFVGrowthAsset) + CDbl(txtFVCurAsset), 2)

		txtFV = FormatNumber(CDbl(((rsabalance * CDbl(lifeStyle)) / CDbl(txtRecommendedMonthly))), 2)

		Return txtFV

	End Function

	Public Function CalulateLumpSum(pin As String, rsabalance As Double, finalSalary As Double, dob As Date, sex As String) As Double


		Dim age As Integer, txt25LumpSum As Double, txtMinMonthlyDraw As Double, txtNetInterest As Double, txtFreq As Double = 12, txtNxDx As Double, txtNc As Double, txtAdministrativeCharges As Double = 0, txtMaxLumpSum As Double, txtRecommendedLumpSum As Double, txtMinLumpSum As Double, txtMaxMonthlyDraw As Double, txtRecommendedMonthly As Double, cr As New Core



		age = (DateDiff(DateInterval.Year, CDate(dob), CDate(Now.Date)))

		If age < 50 Then


			age = 50

		ElseIf age > 65 Then


			age = 65

		Else

			'Me.txtAgee.Text = (DateDiff(DateInterval.Year, CDate(dob), CDate(Now.Date)))

		End If


		'Me.txtSex.Text = dt.Rows(0).Item("sex").ToString
		'Me.txtRSABalance.Text = FormatNumber(CDbl(rsabalance), 2)

		txt25LumpSum = FormatNumber(CDbl((rsabalance)) * 0.25, 2)

		txtMinMonthlyDraw = FormatNumber((CDbl((finalSalary)) / 12) * 0.5, 2)


		txtNetInterest = FormatNumber(calInterate() * 100, 2)


		If sex = "M" Then

			txtNxDx = FormatNumber(getNxDxPW(1, CInt(age), CInt(txtFreq)), 8)
		Else
			txtNxDx = FormatNumber(getNxDxPW(0, CInt(age), CInt(txtFreq)), 8)

		End If

		txtNc = FormatNumber(txtNxDx - (11 / 24), 6)


		Dim lstNumbers As New List(Of Double), lstNumbers2 As New List(Of Double), recommendedAmount As Double, para1 As Double, para2 As Double

		para1 = (CDbl(rsabalance) - CDbl(txtAdministrativeCharges))
		para2 = Financial.PV(calInterate() / 12, 2 * txtNc * txtFreq, txtMinMonthlyDraw, 0, 1)

		'	recommendedAmount = FormatNumber(((CDbl(Me.txtRSABalance.Text) - CDbl(Me.txtAdministrativeCharges.Text)) + Financial.PV(calInterate() / 12, 2 * Me.txtNc.Text * Me.txtFreq.Text, Me.txtMinMonthlyDraw.Text, 0, 1)), 2)

		recommendedAmount = para1 + para2

		lstNumbers.Add(recommendedAmount)


		lstNumbers.Add(0)

		txtMaxLumpSum = FormatNumber(CDbl(lstNumbers.Max), 2)

		If txtMaxLumpSum > (rsabalance * 0.5) Then
			txtRecommendedLumpSum = FormatNumber(CDbl((rsabalance * 0.5)), 2)
		ElseIf (txtMaxLumpSum < (rsabalance * 0.5)) = True And (txtMaxLumpSum > txt25LumpSum) = True Then
			txtRecommendedLumpSum = FormatNumber(CDbl(txtMaxLumpSum), 2)
		ElseIf txtMaxLumpSum < txt25LumpSum Then
			txtRecommendedLumpSum = FormatNumber(CDbl(txt25LumpSum), 2)
		End If

		txtMaxMonthlyDraw = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * txtNc * txtFreq, (rsabalance - txtMinLumpSum - txtAdministrativeCharges), 0, 1)), 2)

		txtRecommendedMonthly = FormatNumber((-1 * Financial.Pmt(calInterate() / 12, 2 * txtNc * txtFreq, rsabalance - txtRecommendedLumpSum - txtAdministrativeCharges, 0, 1)), 2)


		Return txtRecommendedMonthly

	End Function

	Protected Function getNxDxPW(SexType As Integer, intAge As Integer, freq As Integer) As Decimal

		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim EmployerHisCollection As New Hashtable
		Dim mycon As New SqlClient.SqlConnection
		'mycon = db.getConnection("PaymentModule")
		mycon.ConnectionString = "data Source=enpower40db;Initial Catalog=SurePay;User ID=ibs;Pwd=vaug; Connection Timeout=360;Max Pool Size=600"

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If SexType = 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select numNxDx,numNxDx_QTR from tblMalePencomTemplate_New where intAge =  @intAge ", mycon)

			ElseIf SexType <> 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select numNxDx,numNxDx_QTR from tblFemalePencomFormat_New where intAge =  @intAge ", mycon)

			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intAge", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intAge").Value = intAge

			MyDataAdapter.Fill(dsUser, "PencomTemplate")
			dtUser = dsUser.Tables("PencomTemplate")

			mycon.Close()

			If freq = 4 Then
				Return dtUser.Rows(0).Item(1)
			Else
				Return dtUser.Rows(0).Item(0)
			End If


		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function

	Private Function calInterate() As Double

		Dim C12 As Double
		Dim C13 As Double
		Dim C14 As Double
		Dim txtManagement As String = "5.00%"
		Dim txtRegulator As String = "0.30%"
		Dim txtInterest As String = "8.00%"

		C12 = FormatNumber(CDbl(txtManagement.Split("%")(0)) / 100, 5)
		C13 = FormatNumber(CDbl(txtRegulator.Split("%")(0)) / 100, 5)
		C14 = FormatNumber(CDbl(txtInterest.Split("%")(0)) / 100, 5)

		Return C14 * (1 - (C12 + C13))

	End Function

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