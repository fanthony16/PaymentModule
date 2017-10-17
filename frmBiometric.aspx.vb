Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Partial Class frmBiometric
    Inherits System.Web.UI.Page

	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Dim arPINs As New ArrayList
		arPINs.Add("PEN100107954124")
		arPINs.Add("PEN100108080593")
		arPINs.Add("PEN100108687529")
		arPINs.Add("PEN100108752711")
		arPINs.Add("PEN100113158925")
		arPINs.Add("PEN100113484214")
		arPINs.Add("PEN100126801424")
		arPINs.Add("PEN100446984623")
		arPINs.Add("PEN100510211310")
		arPINs.Add("PEN100572570922")
		arPINs.Add("PEN100589688924")
		arPINs.Add("PEN100655816723")
		arPINs.Add("PEN100656235714")
		arPINs.Add("PEN100656236421")
		arPINs.Add("PEN100664903216")
		arPINs.Add("PEN100670443720")
		arPINs.Add("PEN100685398625")
		arPINs.Add("PEN100685666224")
		arPINs.Add("PEN200108715411")
		arPINs.Add("PEN200546466322")
		arPINs.Add("PEN200685398921")

		arPINs.Add("PEN100585276920")
		arPINs.Add("PEN200087568733")
		arPINs.Add("PEN200252665028")
		arPINs.Add("PEN200110022312")



		Dim i As Integer
		Do While i < arPINs.Count
			generateFiles(arPINs.Item(i), "PDF")
			i = i + 1
		Loop
		
		







	End Sub

	Private Function populateSchedule(pin As String) As DataSet

		Dim cr As New Core, dtBiuometric As New DataTable, i As Integer = 0
		dtBiuometric = cr.PMgetBiometric(pin)
		Dim ds As New dsBiometricPrint
		Dim newSNRow As DataRow
		Dim isfullyChecked As Boolean = True
		Dim isfullyVerified As Boolean = True
		Dim isfullyAuthorised As Boolean = True

		
		i = 0

		'Do While i < dtBiuometric.Rows.Count

		newSNRow = ds.Tables(0).NewRow

		newSNRow("txtFirstName") = dtBiuometric.Rows(0).Item("txtSurname")
		newSNRow("txtMiddleName") = dtBiuometric.Rows(0).Item("txtFirstname")
		newSNRow("txtOtherName") = dtBiuometric.Rows(0).Item("txtOtherNames")
		newSNRow("txtIDNo") = dtBiuometric.Rows(0).Item("txtIDNo")

		newSNRow("txtAddress") = dtBiuometric.Rows(0).Item("Address")
		newSNRow("txtPhone") = dtBiuometric.Rows(0).Item("txtCellPhone")

		'newSNRow("imgPassport") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 1)
		'newSNRow("imgRThumb") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 3)
		'newSNRow("imgRIndex1") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 4)
		'newSNRow("imgRIndex2") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 5)
		'newSNRow("imgRIndex3") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 6)
		'newSNRow("imgRIndex4") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 7)

		'newSNRow("imgLThumb") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 8)
		'newSNRow("imgLIndex1") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 9)
		'newSNRow("imgLIndex2") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 10)
		'newSNRow("imgLIndex3") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 11)
		'newSNRow("imgLIndex4") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 12)


		newSNRow("imgPassport") = cr.PMBiometricImage2(dtBiuometric.Rows(0).Item("txtIDNo"), "Picture")
		newSNRow("imgRThumb") = cr.PMBiometricImage2(dtBiuometric.Rows(0).Item("txtIDNo"), "RightThumb")
		'newSNRow("imgRIndex1") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 4)
		'newSNRow("imgRIndex2") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 5)
		'newSNRow("imgRIndex3") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 6)
		'newSNRow("imgRIndex4") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 7)
		newSNRow("imgLThumb") = cr.PMBiometricImage2(dtBiuometric.Rows(0).Item("txtIDNo"), "LeftThumb")
		'newSNRow("imgLIndex1") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 9)
		'newSNRow("imgLIndex2") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 10)
		'newSNRow("imgLIndex3") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 11)
		'newSNRow("imgLIndex4") = cr.PMBiometricImage(dtBiuometric.Rows(0).Item("txtIDNo"), 12)


		'i = i + 1

		ds.Tables(0).Rows.Add(newSNRow)
		'Loop

		Return ds

	End Function

	' generating the pdf extract schedule to enpower
	Private Sub generateFiles(pin As String, fileType As String)

		Dim crExportOptions As New ExportOptions
		Dim crDiskFileDestinationOptions As New DiskFileDestinationOptions
		Dim crFormatypeOption As New PdfRtfWordFormatOptions
		Dim rdoc As New ReportDocument
		Dim rsource As New CrystalDecisions.Web.CrystalReportSource
		Dim sql As String


		Dim filePath As String = Server.MapPath("~/FileDownLoads/" & pin & ".pdf")
		rdoc.Load(Server.MapPath("~/Report/BiometricPrint.rpt"))


		Dim cr As New Core
		Dim ds As DataSet
		ds = populateSchedule(pin)
		ViewState("ApprovalSchedule") = ds.Tables(0)
		rdoc.SetDataSource(ds.Tables(0))

		crDiskFileDestinationOptions.DiskFileName = filePath
		crExportOptions = rdoc.ExportOptions

		With crExportOptions

			.ExportDestinationType = ExportDestinationType.DiskFile
			If fileType = "PDF" Then
				.ExportFormatType = ExportFormatType.PortableDocFormat
			ElseIf fileType = "XLS" Then
				.ExportFormatType = ExportFormatType.ExcelWorkbook
			End If

			.ExportDestinationOptions = crDiskFileDestinationOptions
			.ExportFormatOptions = crFormatypeOption

		End With

		rdoc.Export()



	End Sub



End Class
