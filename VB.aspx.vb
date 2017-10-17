Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Partial Class VB

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
          Dim strQuery As String = "select pkiMemberApplicationID,txtApplicationCode,fkiMemberID,txtPIN from tblMemberApplication"
        Dim cmd As New SqlCommand(strQuery)
          Dim dt As DataTable = GetData(cmd)

        Dim CheckBoxArray As ArrayList
        If ViewState("CheckBoxArray") IsNot Nothing Then
            CheckBoxArray = DirectCast(ViewState("CheckBoxArray"), ArrayList)
        Else
            CheckBoxArray = New ArrayList()
        End If

        If IsPostBack Then
            Dim CheckBoxIndex As Integer
            Dim CheckAllWasChecked As Boolean = False
            Dim chkAll As CheckBox = DirectCast(GridView1.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)

            Dim checkAllIndex As String = "chkAll-" & GridView1.PageIndex

            If chkAll.Checked Then
                If CheckBoxArray.IndexOf(checkAllIndex) = -1 Then
                    CheckBoxArray.Add(checkAllIndex)
                End If
            Else
                If CheckBoxArray.IndexOf(checkAllIndex) <> -1 Then
                    CheckBoxArray.Remove(checkAllIndex)
                    CheckAllWasChecked = True
                End If
               End If


            For i As Integer = 0 To GridView1.Rows.Count - 1
                If GridView1.Rows(i).RowType = DataControlRowType.DataRow Then
                    Dim chk As CheckBox = _
                     DirectCast(GridView1.Rows(i).Cells(0) _
                     .FindControl("CheckBox1"), CheckBox)
                    CheckBoxIndex = GridView1.PageSize * GridView1.PageIndex + (i + 1)
                    If chk.Checked Then
                        If CheckBoxArray.IndexOf(CheckBoxIndex) = -1 And _
                            Not CheckAllWasChecked Then
                            CheckBoxArray.Add(CheckBoxIndex)
                        End If
                    Else
                        If CheckBoxArray.IndexOf(CheckBoxIndex) <> -1 Or _
                            CheckAllWasChecked Then
                            CheckBoxArray.Remove(CheckBoxIndex)
                        End If
                    End If
                End If
               Next


          End If

          ViewState("CheckBoxArray") = CheckBoxArray

        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
          Dim strConnString As [String] = System.Configuration.ConfigurationManager.ConnectionStrings("PaymentModule").ConnectionString
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)

        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
        If ViewState("CheckBoxArray") IsNot Nothing Then
            Dim CheckBoxArray As ArrayList = _
            DirectCast(ViewState("CheckBoxArray"), ArrayList)
            Dim checkAllIndex As String = "chkAll-" & GridView1.PageIndex

            If CheckBoxArray.IndexOf(checkAllIndex) <> -1 Then
                Dim chkAll As CheckBox = _
                DirectCast(GridView1.HeaderRow.Cells(0) _
                .FindControl("chkAll"), CheckBox)
                chkAll.Checked = True
            End If
            For i As Integer = 0 To GridView1.Rows.Count - 1
                If GridView1.Rows(i).RowType = DataControlRowType.DataRow Then
                    If CheckBoxArray.IndexOf(checkAllIndex) <> -1 Then
                        Dim chk As CheckBox = _
                        DirectCast(GridView1.Rows(i).Cells(0) _
                        .FindControl("CheckBox1"), CheckBox)
                        chk.Checked = True
                        GridView1.Rows(i).Attributes.Add("style", "background-color:aqua")
                    Else
                        Dim CheckBoxIndex As Integer = GridView1.PageSize * (GridView1.PageIndex) + (i + 1)
                        If CheckBoxArray.IndexOf(CheckBoxIndex) <> -1 Then
                            Dim chk As CheckBox = _
                            DirectCast(GridView1.Rows(i).Cells(0) _
                            .FindControl("CheckBox1"), CheckBox)
                            chk.Checked = True
                            GridView1.Rows(i).Attributes.Add("style", "background-color:aqua")
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)")
            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)")
        End If
    End Sub
  
End Class
