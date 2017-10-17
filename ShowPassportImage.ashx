<%@ WebHandler Language="VB" Class="ShowPassportImage" %>

Imports System
Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class ShowPassportImage : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
          Dim errLog As String = ""
          Try
               
          
               'Dim employeeID As Int32, imageTypeID As Int32
               Dim PIN As String, imageTypeID As Int32
               If Not context.Request.QueryString(0) Is Nothing Then
               
                    'PIN = Convert.ToInt32(context.Request.QueryString(0))
                    'imageTypeID = Convert.ToInt32(context.Request.QueryString(1))
                    'LogLocation
                    PIN = (context.Request.QueryString(0)).ToString
                    imageTypeID = Convert.ToInt32(context.Request.QueryString(1))
                    errLog = (context.Request.QueryString(2)).ToString
               Else
                    Throw New ArgumentException("No parameter specified")
               End If
          
               context.Response.ContentType = "image/jpeg"
               Dim strm As Stream = Nothing
          
               If imageTypeID = 1 Then
                    strm = ShowEmpPassport(PIN, errLog)
               ElseIf imageTypeID = 2 Then
                    strm = ShowEmpSignature(PIN, errLog)
               End If
          
          
               Dim buffer As Byte() = New Byte(4095) {}
               Dim byteSeq As Integer = strm.Read(buffer, 0, 4096)
 
               Do While byteSeq > 0
                    context.Response.OutputStream.Write(buffer, 0, byteSeq)
                    byteSeq = strm.Read(buffer, 0, 4096)
               Loop
          Catch ex As Exception

               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = errLog
               logerr.Logger(ex.Message & ": Error Loading Biometric")
               
          End Try
    End Sub
 
     
     
     Public Function ShowEmpPassport(ByVal PIN As String, errlog As String) As Stream
          Dim conn As String = ConfigurationManager.ConnectionStrings("EnpowerV4").ConnectionString
          Dim connection As SqlConnection = New SqlConnection(conn)
          
          Dim sql As String = "select Picture from dbo.Biometric a, employee b where a.employeeid = b.employeeid and rsapin = @PIN"
          
          ' Dim sql As String = "select Picture as Passport from dbo.Biometric where employeeid = @employeeid "
          Dim cmd As SqlCommand = New SqlCommand(sql, connection)
          cmd.CommandType = CommandType.Text
          cmd.Parameters.AddWithValue("@PIN", PIN)
          connection.Open()
          Dim img As Object = cmd.ExecuteScalar()
          Try
               Return New MemoryStream(CType(img, Byte()))
          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = errlog
               logerr.Logger(ex.Message & ": Error Loading Biometric")
               Return Nothing
          Finally
               connection.Close()
          End Try
     End Function
     
     Public Function ShowEmpSignature(ByVal PIN As String, errlog As String) As Stream
          Try
               Dim conn As String = ConfigurationManager.ConnectionStrings("EnpowerV4").ConnectionString
               Dim connection As SqlConnection = New SqlConnection(conn)
               Dim sql As String = "select Signature from dbo.Biometric a, employee b where a.employeeid = b.employeeid and rsapin = @PIN"
               Dim cmd As SqlCommand = New SqlCommand(sql, connection)
               cmd.CommandType = CommandType.Text
               cmd.CommandTimeout = 2000
               cmd.Parameters.AddWithValue("@PIN", PIN)
               connection.Open()
               Dim img As Object = cmd.ExecuteScalar()
               Return New MemoryStream(CType(img, Byte()))
               connection.Close()
          Catch ex As Exception
               
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = errlog
               logerr.Logger(ex.Message & ": Error Loading Biometric")
               
               Return Nothing
          Finally
               
          End Try
     End Function
     
     
     
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class