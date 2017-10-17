Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO
Partial Class PictureLoader
    Inherits System.Web.UI.Page

     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

          If Request.QueryString("ImageID") IsNot Nothing Then

               Dim db As New DbConnection
               Dim myComm As New SqlCommand
               Dim myCon As New SqlConnection
               Dim daUser As New SqlDataAdapter, dtUser As DataTable
               Dim dsUser As New DataSet
               myCon = db.getConnection("EnpowerV4")
               ' Dim strQuery As String = "select Name, ContentType, Data from tblFiles where id=@id"
               'Dim cmd As SqlCommand = New SqlCommand(strQuery)
               ' cmd.Parameters.Add("@id", SqlDbType.Int).Value() = Convert.ToInt32(Request.QueryString("ImageID"))
               'Dim dt As DataTable = GetData(cmd)

               myComm = New SqlClient.SqlCommand("select Picture as Passport from dbo.Biometric where employeeid = @employeeid ", myCon)
               myComm.CommandType = CommandType.Text
               myComm.Parameters.Add(New SqlClient.SqlParameter("@employeeid", SqlDbType.Int))
               ' myComm.Parameters("@employeeid").Value = Convert.ToInt32(Request.QueryString("ImageID"))

               myComm.Parameters("@employeeid").Value = 890142

               daUser.SelectCommand = myComm
               daUser.Fill(dsUser, "memberPassport")
               dtUser = dsUser.Tables("memberPassport")

               If dtUser IsNot Nothing Then
                    Dim bytes() As Byte = CType(dtUser.Rows(0)("Passport"), Byte())
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "image/jpeg" 'dtUser.Rows(0)("ContentType").ToString()
                    'Response.AddHeader("content-disposition", "attachment;filename=" + dtUser.Rows(0)("Picture").ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Test")
                    Response.BinaryWrite(bytes)
                    Response.Flush()
                    Response.End()
               End If
          End If

     End Sub
End Class
