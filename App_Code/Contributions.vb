Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Contributions

    Public Function getRefundDetails(lodgmentID As Integer, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getRefundReversal"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = lodgmentID


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@recordType", SqlDbType.Bit))
            MyCommand.Parameters("@recordType").Value = 1

            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser
    End Function

    Public Function getReversalDetail(lodgmentID As Integer, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getRefundReversal"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = lodgmentID


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@recordType", SqlDbType.Bit))
            MyCommand.Parameters("@recordType").Value = 0

            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser
    End Function

    Public Function getUploadDetails(lodgmentID As String, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getUploadDetails"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)



            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.DateTime))
            MyCommand.Parameters("@startDate").Value = Now.Date


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.DateTime))
            MyCommand.Parameters("@endDate").Value = Now.Date

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgementID", SqlDbType.VarChar, 250))
            MyCommand.Parameters("@lodgementID").Value = lodgmentID


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@All", SqlDbType.Int))
            MyCommand.Parameters("@All").Value = 1


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@Dialy", SqlDbType.Int))
            MyCommand.Parameters("@Dialy").Value = 0


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ByDate", SqlDbType.Int))
            MyCommand.Parameters("@ByDate").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@rtype", SqlDbType.Int))
            MyCommand.Parameters("@rtype").Value = 1


            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser
    End Function

    Public Function getContribution(date1 As Date, date2 As Date, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()
        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getlodgement4Update"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.DateTime))
            MyCommand.Parameters("@startDate").Value = date1


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.DateTime))
            MyCommand.Parameters("@endDate").Value = date2

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@desc", SqlDbType.VarChar, 250))
            MyCommand.Parameters("@desc").Value = ""


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentAmt", SqlDbType.Decimal))
            MyCommand.Parameters("@lodgmentAmt").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
            MyCommand.Parameters("@ClientID").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@filter", SqlDbType.Int))
            MyCommand.Parameters("@filter").Value = 1

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = 0


            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser

    End Function
    Public Function getClientByID(ByVal clientID As Integer, fund As String) As DataTable

        Dim cmdUser As New SqlClient.SqlCommand
        Dim daUser As New SqlClient.SqlDataAdapter
        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection

        Try
            cmdUser = conn.getConnection("Enpower").CreateCommand
            cmdUser.CommandText = "select top 1 pkiClientID,txtsubplanid,txtClientName from enpower_midas.dbo.tblclients where pkiclientid = '" & clientID & "'"
            daUser.SelectCommand = cmdUser
            daUser.Fill(dsUser, "tblclients")
            dtUser = dsUser.Tables("tblclients")
            Return dtUser
            'conn.Close()
            conn.close(fund)
        Catch ex As Exception
            MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error conection!!")

        Finally
            'conn.Close()
        End Try


        Return Nothing


        '  End Using



    End Function

    ' Lodgments by ID
    Public Function getContribution(id As Integer, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()

        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getlodgement4Update"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)





            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.DateTime))
            MyCommand.Parameters("@startDate").Value = Now.Date


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.DateTime))
            MyCommand.Parameters("@endDate").Value = Now.Date

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@desc", SqlDbType.VarChar, 250))
            MyCommand.Parameters("@desc").Value = ""


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentAmt", SqlDbType.Decimal))
            MyCommand.Parameters("@lodgmentAmt").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
            MyCommand.Parameters("@ClientID").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@filter", SqlDbType.Int))
            MyCommand.Parameters("@filter").Value = 9

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = id

            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser

    End Function

    ' Lodgments by amount
    Public Function getContribution(amt As Double, date1 As Date, date2 As Date, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getlodgement4Update"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.DateTime))
            MyCommand.Parameters("@startDate").Value = date1


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.DateTime))
            MyCommand.Parameters("@endDate").Value = date2

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@desc", SqlDbType.VarChar, 250))
            MyCommand.Parameters("@desc").Value = ""


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentAmt", SqlDbType.Decimal))
            MyCommand.Parameters("@lodgmentAmt").Value = amt

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
            MyCommand.Parameters("@ClientID").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@filter", SqlDbType.Int))
            MyCommand.Parameters("@filter").Value = 1

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = 0

            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser
    End Function

    ' Lodgments by employer
    Public Function getContribution(clientID As Integer, date1 As Date, date2 As Date, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getlodgement4Update"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.DateTime))
            MyCommand.Parameters("@startDate").Value = date1


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.DateTime))
            MyCommand.Parameters("@endDate").Value = date2

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@desc", SqlDbType.VarChar, 250))
            MyCommand.Parameters("@desc").Value = ""


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentAmt", SqlDbType.Decimal))
            MyCommand.Parameters("@lodgmentAmt").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
            MyCommand.Parameters("@ClientID").Value = clientID

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@filter", SqlDbType.Int))
            MyCommand.Parameters("@filter").Value = 1


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = 0

            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser
    End Function

    'Lodgment by naration
    Public Function getContribution(desc As String, date1 As Date, date2 As Date, fund As String) As DataSet

        Dim dsUser As New DataSet
        Dim dtUser As New DataTable
        Dim conn As New DbConnection
        Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


        Try
            Dim MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = "ml_sp_getlodgement4Update"
            MyCommand.CommandType = CommandType.StoredProcedure
            MyCommand.Connection = conn.getConnection(fund)


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.DateTime))
            MyCommand.Parameters("@startDate").Value = date1


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.DateTime))
            MyCommand.Parameters("@endDate").Value = date2

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@desc", SqlDbType.VarChar, 250))
            MyCommand.Parameters("@desc").Value = desc


            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentAmt", SqlDbType.Decimal))
            MyCommand.Parameters("@lodgmentAmt").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
            MyCommand.Parameters("@ClientID").Value = 0

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@filter", SqlDbType.Int))
            MyCommand.Parameters("@filter").Value = 1

            MyCommand.Parameters.Add(New SqlClient.SqlParameter("@lodgmentID", SqlDbType.Int))
            MyCommand.Parameters("@lodgmentID").Value = 0

            MyAdapter.SelectCommand = MyCommand
            MyAdapter.Fill(dsUser, "Lodgment")

            MyCommand.Dispose()

        Catch ex As Exception

        Finally
            conn = Nothing

        End Try
        Return dsUser
    End Function
End Class
