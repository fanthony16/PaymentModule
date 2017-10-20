Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Text.RegularExpressions.Regex
Imports System.Security.Cryptography
Imports System.Web.Script.Serialization
Public Class Core

	Private Sub testJASON()
		Dim jS As JavaScriptSerializer = New JavaScriptSerializer

	End Sub

	Public Function IsValidEmailAddress(txtEmail As String) As Boolean



		Static emailExpression As New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
		Return emailExpression.IsMatch(txtEmail)

	End Function

	Public Function getPassport(EmployerID As Integer) As Byte()

		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select Picture as Passport from dbo.Biometric where employeeid = ' & EmployerID & ' ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			Dim barrImg As Byte() = MyDataAdapter.SelectCommand.ExecuteScalar
			db.close("EnpowerV4")
			mycon.Close()

			Return barrImg

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function

	Public Function getSignatory() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RSK")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiSignatoryID as ID,txtName as FullName,txtPhone as Telephone,txtBVN	as BVN, txtBankEnrolled as EnrolledBank from [RiskCo].[dbo].[tblSignatoryBVN] ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.Fill(dsUser, "Users")
			dtUser = dsUser.Tables("Users")
			db.close("RSA")

			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
		Return dtUser

	End Function

	Public Function PMgetRetireeForEnhencement() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			'MyDataAdapter = New SqlClient.SqlDataAdapter("select distinct EmployeeID,(select top 1 rsapin from employee a where a.employeeid = b.EmployeeID ) PIN  from payments b where PaymentTypeID in (3,33,17) and month(valuedate) = 12 and year(valuedate)=2016 and IsConfirmed = 1 and isArchived = 0 and IsReversed = 0", mycon)


			MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select  distinct top 2 EmployeeID,(select top 1 rsapin from employee a where a.employeeid = b.EmployeeID ) PIN  from payments b where PaymentTypeID in (3,33,17) and month(valuedate) = 12 and year(valuedate)=2016 and IsConfirmed = 1 and isArchived = 0 and IsReversed = 0) select * from tab a where  not exists (select * from tblPencomFormat where txtPIN = a.pin)", mycon)


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.Fill(dsUser, "Users")
			dtUser = dsUser.Tables("Users")
			db.close("EnpowerV4")
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return dtUser
	End Function



	Public Function getSignatory(UserName As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RSK")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiSignatoryID as ID,txtName as FullName,txtPhone as Telephone,txtBVN	as BVN, txtBankEnrolled as EnrolledBank from [RiskCo].[dbo].[tblSignatoryBVN] where txtCreatedBy = '" & UserName & "' ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.Fill(dsUser, "Users")
			dtUser = dsUser.Tables("Users")
			db.close("RSK")
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return dtUser
	End Function

	Public Sub AddSignatory(name As String, phone As String, bvn As String, enrolledBank As String, userName As String)
		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RSK")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("INSERT INTO [RiskCo].[dbo].[tblSignatoryBVN] ([txtName],[txtPhone],[txtBVN],[txtBankEnrolled],[txtCreatedBy])     VALUES('" & name & "','" & phone & "', '" & bvn & "', '" & enrolledBank & "', '" & userName & "')", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub

	Public Sub updateSignatory(id As Integer, name As String, phone As String, bvn As String, enrolledBank As String, userName As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RSK")
		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("UPDATE [RiskCo].[dbo].[tblSignatoryBVN] SET [txtName] = '" & name & "',[txtPhone] = '" & phone & "',[txtBVN] = '" & bvn & "',[txtBankEnrolled] = '" & enrolledBank & "',[txtCreatedBy] = '" & userName & "'  WHERE pkiSignatoryID = '" & id & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Sub

	Public Sub RemoveBranchHead(groupID As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("DELETE from dbo.IBS_BranchGrouping where ID_Group = '" & groupID & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Sub
	'End Sub
	Public Sub CreateBranchHead(groupID As String, groupName As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")
		Try
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into dbo.IBS_BranchGrouping (ID_Group,GroupName) values ('" & groupID & "', '" & groupName & "')", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			mycon.Close()
			'Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub

	Public Function getBranchUnGrouped() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.ID_Branch , a.Name from dbo.IBS_Branches a where ID_Group is null and not exists (select * from dbo.IBS_BranchGrouping where GroupName  = Name )", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "BranchUnGrouped")
			dtUser = dsUser.Tables("BranchUnGrouped")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function

	Public Function getBranchGrouping() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from dbo.IBS_BranchGrouping", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "BranchGrouping")
			dtUser = dsUser.Tables("BranchGrouping")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function
	Public Function getUnGroupedBranches() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select ID_Branch,Name from dbo.IBS_Branches where ID_Group is null", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "UnGroupedBranches")
			dtUser = dsUser.Tables("UnGroupedBranches")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function
	Public Function getGroupedBranches(groupID As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select ID_Branch,Name from dbo.IBS_Branches where ID_Group ='" & groupID & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "GroupedBranches")
			dtUser = dsUser.Tables("GroupedBranches")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function
	Public Function AddBranchToGroup(branchID As String, groupID As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update dbo.IBS_Branches set ID_Group = '" & groupID & "' where ID_Branch ='" & branchID & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "GroupedBranches")
			dtUser = dsUser.Tables("GroupedBranches")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function
	Public Sub RemoveBranchFromGroup(branchID As String, groupID As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PFA")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update dbo.IBS_Branches set ID_Group = NULL where ID_Branch ='" & branchID & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "GroupedBranches")
			'dtUser = dsUser.Tables("GroupedBranches")

			mycon.Close()
			' Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub




	Public Function getAssignedRole(txtReturnName As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtRole,fkiroleid from rmas_assignedRoleQCR,moneybook_fund..tblroles a where a.pkiroleid = fkiroleid and txtreturnName = '" & txtReturnName & "' ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "AssignedRole")
			dtUser = dsUser.Tables("AssignedRole")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function

	Public Sub PMRemoveComment(commentID As Integer)

		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran
			'adding pre-approval comments 

			myComm.CommandText = "update tblApplicationComments set isDeactived = 1 where pkiCommentID = '" & commentID & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()

		Catch ex As Exception

		End Try

	End Sub

	' retrieved comments entered by the user on each application
	Public Function PMgetApplicationComment(appCode As String, stage As String) As DataTable

		Try

			Dim db As New DbConnection

			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable, sql As String = ""

			If stage = "PRE" Then


				'				sql = "select txtComment,txtCreatedBy,pkiCommentID from tblApplicationComments where txtApplicationCode = '" & appCode & "' and intAppCommentStage = 1 and isDeactived = 0 "


				sql = "select txtComment,txtCreatedBy,pkiCommentID,(Select txtDescription from tblReturnErrorTypes where intErrorID = a.intErrorID) ErrorCategory from tblApplicationComments a where txtApplicationCode = '" & appCode & "' and intAppCommentStage = 1 and isDeactived = 0"


			ElseIf stage = "POST" Then
				'sql = "select txtComment from tblApplicationApprovalPayee where txtApplicationCode = '" & appCode & "'"
				sql = "select txtComment,txtCreatedBy,pkiCommentID from tblApplicationComments where txtApplicationCode = '" & appCode & "' and intAppCommentStage = 2 and isDeactived = 0"

			ElseIf stage = "PRE_IC" Then

				sql = "select txtControlCheckComment as txtComment from tblMemberApplication where txtapplicationcode = '" & appCode & "'"

			End If

			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")

			mycon.Close()

			'If dtUser.Rows.Count > 0 Then
			'     Return dtUser.Rows(0).Item(1).ToString & ":" & dtUser.Rows(0).Item(0).ToString
			'Else
			'     Return ""

			'End If
			Return dtUser

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try


		Return Nothing
	End Function

	Public Sub PMUpdateApplicationControlCheck(comment As String, appCode As String, uName As String, isOK As Integer, stage As String, reviewStatus As String)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			'adding pre-approval comments 

			If stage = "PRE" Then

				If isOK = 1 And LTrim(RTrim(comment)) <> "" And LTrim(RTrim(reviewStatus)) <> "" Then

					myComm.CommandText = "update tblMemberApplication set txtControlCheckComment = '" & comment & "', txtControlCheckedBy = '" & uName & "', dteControlChecked = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtControlCheckedStatus = '" & reviewStatus & "',IsControlChecked = '" & isOK & "' where txtApplicationCode = '" & appCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()
					'DateTime.Parse(Now).ToString("")

				ElseIf isOK = 1 And comment = "" Then

					'		myComm.CommandText = "update tblMemberApplication set  txtControlCheckedBy = '" & uName & "',dteControlChecked = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',IsControlChecked = '" & isOK & "', txtControlCheckedStatus = '" & reviewStatus & "' where txtApplicationCode = '" & appCode & "'"
					'		command.CommandType = CommandType.Text
					'		myComm.ExecuteNonQuery()

				End If

				'adding post-approval comments 
			ElseIf stage = "POST" Then

				myComm.CommandText = "update tblApplicationApprovalPayee set  txtCommentBy = '" & uName & "',dteCommented = '" & Now.Date & "',txtComment = '" & comment & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			End If





			sqlTran.Commit()

		Catch ex As Exception

		End Try
	End Sub

	Public Function getBanks() As DataTable
		Try

		Catch ex As Exception

		End Try
		Return Nothing

	End Function




	Public Sub PMApprovalRevaluation(appCode As String, PIN As String, uName As String, apptype As Integer)


		Try

			Dim db As New DbConnection, cr As New Core
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			Dim dtApp As New DataTable

			dtApp = cr.PMgetApplicationByCode(appCode)
			'getting the currentPriceDate from enpower per fund type
			Dim vDate As Date = PMgetCurrentValueDate(CInt(dtApp.Rows(0).Item("intFundPlatFormID")))

			'calculating the current value for a customer per fund
			Dim amtPaid As Double = PMValueByDate(PIN, vDate, CInt(dtApp.Rows(0).Item("intFundPlatFormID")))

			myComm.CommandText = "update tblApplicationApprovalPayee set dteValueDate = '" & DateTime.Parse(vDate).ToString("yyyy-MM-dd") & "',numPayingAmount = '" & amtPaid & "', txtCreatedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()


			Select Case apptype

				Case Is = 1

					myComm.CommandText = "update awbr400 set numAmountToPay = " & amtPaid & " where txtApplicationCode = '" & appCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				Case Is = 5

					myComm.CommandText = "update awbr600 set numAmountToPay = " & amtPaid & " where txtApplicationCode = '" & appCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				Case Else

			End Select
			

			sqlTran.Commit()

		Catch ex As Exception

		End Try

	End Sub

	Public Sub PMApprovalPayeeStatus(appCode As String, uName As String, txtStatus As Char, fundID As Integer, Source As String)

		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			If txtStatus <> "R" Then

				myComm.CommandText = "update tblApplicationApprovalPayee set txtStatus = '" & txtStatus & "',txtEnpowerExtractBatch = null, txtCreatedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "' and dteChecked is null and dteVerified is null and dteAuthorised is null"

			Else

			End If

			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()

		Catch ex As Exception

		End Try

	End Sub

	Public Sub PMApprovalPayeeStatus(appCode As String, uName As String, txtStatus As Char, fundID As Integer)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			If txtStatus <> "R" Then

				'myComm.CommandText = "update tblApplicationApprovalPayee set txtStatus = '" & txtStatus & "',txtEnpowerExtractBatch = null, txtCreatedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "'"

				myComm.CommandText = "update tblApplicationApprovalPayee set txtStatus = '" & txtStatus & "',txtEnpowerExtractBatch = null, dteChecked = null,txtControlCheckedBy = null,dteVerified = null,txtControlVerifiedBy = null where txtApplicationCode = '" & appCode & "'"

			Else

				Dim vDate As Date = PMgetCurrentValueDate(fundID)
				'cr.PMValueByDate(LTrim(RTrim(Me.txtPIN.Text)), vDate, 1)
				myComm.CommandText = "update tblApplicationApprovalPayee set dteValueDate = '" & DateTime.Parse(vDate).ToString("yyyy-MM-dd") & "', txtCreatedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "'"

			End If


			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()

		Catch ex As Exception

		End Try
	End Sub


	Public Sub PMUpdatePreferences(isFile As Integer, isDMS As Integer)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblPreference set blnIsDMSFileStorage = '" & isDMS & "',  blnIsFileSystemStorage = '" & isFile & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Sub


	Public Sub PMUpdateBankDetails(AppDetails As ApplicationDetail, UName As String)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblMemberApplication set txtAccountName = '" & AppDetails.AccountName & "',  txtAccountNo = '" & AppDetails.AccountNo & "',txtBVN = '" & AppDetails.BVN & "',fkiBankID = '" & AppDetails.BankID & "',fkiBranchID = '" & AppDetails.BranchID & "', txtLastChangedPerson = '" & UName & "' where txtApplicationCode = '" & AppDetails.ApplicationID & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()



			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Sub


	Public Sub PMUpdateApprovalPersonComment(comment As String, appCode As String)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblApplicationApprovalPayee set txtPaymentRemarks = '" & comment & "' where txtApplicationCode = '" & appCode & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			'tblApplicationComments
			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Sub


	Public Sub PMUpdateApplicationShelveNo(appCode As String, uName As String, shelveNo As String, ishardCopyRecieved As Integer)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblMemberApplication set txtShelveNumber = '" & shelveNo & "', txtShelveNoCreatedBy = '" & uName & "', dteShelveNoCreated = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "', isHardDocRecieved = '" & ishardCopyRecieved & "' where txtApplicationCode = '" & appCode & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()


			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Sub


	Public Sub PMUpdateFundPlatform(appCode As String, fundType As Integer)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "Update tblMemberApplication set intFundPlatFormID = '" & fundType & "' where  txtApplicationCode = '" & appCode & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()

		Catch ex As Exception

		End Try
	End Sub

	Public Function PMUpdatePreference(runDate As Date) As DataTable
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable

			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblPreference set dteLastNotificationSent = '" & DateTime.Parse(runDate).ToString("yyyy-MM-dd") & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			mycon.Close()


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function

	Public Function PMARLNotification(errPath As String) As DataTable
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable

			mycon = db.getConnection("PaymentModule")
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select b.txtApplicationCode,txtpin,dteDOR,txtEmployerName ,replace(txtfullname,'|','') FullName,txtSex,(select txtDescription  from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) AppType,dteApplicationDate ,(select [dbo].[fn_GetWorkDays] (dteAcknowledgment,getdate())) AcknowledgmentAge,c.Phone,(c.ContactAddress1+' '+c.ContactAddress2) [Address],c.ContactLGAID, c.ContactStateID,(select StateName  from EnPowerV4..State where stateid = c.ContactStateID ) StateName,(select LgaName  from EnPowerV4..LGA  where stateid = c.ContactStateID and LgaID = c.ContactLGAID) LGA,'' TeamLead from tblARLAcknowledgment a, tblMemberApplication b,EnPowerV4..Employee c, enpowerv4..Employer d where isNotificationActive = 1 and a.txtApplicationCode = b.txtApplicationCode and b.fkiMemberID = c.EmployeeID and c.EmployerID = d.EmployerID and (select [dbo].[fn_GetWorkDays] (dteAcknowledgment,getdate())) > 0 ", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ARLNotification")
			dtUser = dsUser.Tables("ARLNotification")
			mycon.Close()

			Return dtUser

			mycon.Close()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = errPath
			logerr.Logger(ex.Message & " - ARL Notification")

		End Try
	End Function



	Public Function PMAgedARL(errPath As String) As DataTable
		Try

			'Financial.PPmt(0, 0, 0, 0)


			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable

			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As SqlClient.SqlDataAdapter


			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode ApplicationCode	,	txtPIN PIN,	txtFullName FullName,	txtEmployerName EmployerName,	txtSex Sex ,	dteDOB DOB,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId) ApplicationType,b.dteAcknowledgment from tblMemberApplication a, tblARLAcknowledgment b where a.txtApplicationCode = b.txtApplicationCode and txtStatus in ('Entry','Documentation') and (select dbo.[fn_GetWorkDays] (dteAcknowledgment,getdate())) >= 5 ", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser



			'myComm.ExecuteNonQuery()
			'sqlTran.Commit()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = errPath
			logerr.Logger(ex.Message & " - Eligility Notification")

		End Try
	End Function

	Public Function PMEligibilityList(runDate As Date, errPath As String) As DataTable
		Try

			Financial.PPmt(0, 0, 0, 0)


			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable

			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			'MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode ApplicationCode	,	txtPIN PIN,	txtFullName FullName,	txtEmployerName EmployerName,	txtSex Sex ,Mobile ,dteDOB DOB,dteDisengagement Disengagement from tblMemberApplication,Enpowerv4..employee  where EmployeeID = fkiMemberID and  fkiAppTypeId = 2 and datediff(year,dtedob,getdate()) > 40 and txtStatus = 'Paid' and dteDeactivated is null", mycon)

			MyDataAdapter = New SqlClient.SqlDataAdapter("select datediff(year,dtedob,getdate()), txtApplicationCode ApplicationCode	,	txtPIN PIN,	txtFullName FullName,	txtEmployerName EmployerName,	txtSex Sex ,Mobile , dteDOB DOB,dteDisengagement Disengagement from tblMemberApplication a,Enpowerv4..employee  where EmployeeID = fkiMemberID and  fkiAppTypeId = 2 and datediff(year,dtedob,'" & runDate & "') >= 50 and txtStatus = 'Paid' and dteDeactivated is null and month(dtedob) = month('" & DateTime.Parse(runDate) & "') and day(dtedob) = day('" & DateTime.Parse(runDate) & "') and (select count(*) from tblMemberApplication b where b.txtPIN = a.txtPIN and fkiAppTypeId in (3,1) ) = 0", mycon)




			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

			mycon.Close()

			'myComm.ExecuteNonQuery()
			'sqlTran.Commit()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = errPath
			logerr.Logger(ex.Message & " - Eligility Notification")

		End Try
	End Function




	Public Sub PMUpdateApplicationComment(comment As String, appCode As String, uName As String, uCommentStage As Integer, intErrorID As Integer)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "insert into tblApplicationComments (intAppCommentStage,txtApplicationCode,txtComment,txtCreatedBy,intErrorID) values ('" & uCommentStage & "','" & appCode & "','" & comment & "', '" & uName & "', '" & intErrorID & "')"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()



			'tblApplicationComments
			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Sub



	Public Sub PMCancelPaymentControls(appCode As String, uName As String, stage As String)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")
			Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			'cancelling payment internal control check
			If UCase(stage) = "CHECKED" Then


				myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlCheckedBy = null ,dteChecked = null, dteControlLastModified = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "', txtControlLastModifiedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				'cancelling payment internal control verification
			ElseIf UCase(stage) = "VERIFIED" Then

				myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlVerifiedBy = null,dteVerified = null,dteControlLastModified = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "', txtControlLastModifiedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				'cancelling payment finance authorization
			ElseIf UCase(stage) = "AUTHORIZED" Then

				myComm.CommandText = "update tblApplicationApprovalPayee set  txtControlAuthorisedBy = null,dteAuthorised = null, dteControlLastModified = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtControlLastModifiedBy = '" & uName & "' where txtApplicationCode = '" & appCode & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			End If





			sqlTran.Commit()

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try
	End Sub


	Public Sub PMUpdateApplication(AppDetail As ApplicationDetail, AppDocNew As List(Of ApplicationDocumentDetail), AppDocOLD As List(Of ApplicationDocumentDetail), AppAdhocDoc As List(Of AdhocDocuments), userName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

		Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
		myComm = mycon.CreateCommand
		myComm.Transaction = sqlTran

		Try

		

		If AppDetail.DocCompleted = 1 Then

			If AppDetail.IsRetirement = True Then

				sql1 = "update tblMemberApplication set fkiMemberID = " & AppDetail.MemberID & ",fkiAppTypeId = '" & AppDetail.AppTypeId & "',txtSector ='" & AppDetail.Sector & "', dteApplicationDate = '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "',txtApplicationState = '" & AppDetail.ApplicationState & "',txtApplicationOffice = '" & AppDetail.ApplicationOffice & "',txtAccountName = '" & AppDetail.AccountName & "',txtAccountNo = '" & AppDetail.AccountNo & "', txtBVN = '" & AppDetail.BVN & "',fkiBankID = '" & AppDetail.BankID & "',fkiBranchID = '" & AppDetail.BranchID & "',txtComment ='" & AppDetail.Comment & "', txtStatus = '" & AppDetail.Status & "',dteDocumentCompleted = '" & DateTime.Parse(AppDetail.DateDocumentCompleted).ToString("yyyy-MM-dd") & "',IsDocCompleted = '" & AppDetail.DocCompleted & "',txtCommentGroup = '" & AppDetail.CommentGroup & "',fkiEmployerID = '" & AppDetail.EmployerID & "',numRSABalance = '" & AppDetail.RSABalance & "',dteDOR = '" & DateTime.Parse(AppDetail.DOR).ToString("yyyy-MM-dd") & "',txtSex = '" & AppDetail.Sex & "',txtReason = '" & AppDetail.Reason & "', txtDepartment = '" & AppDetail.Department & "', txtDesignation = '" & AppDetail.Designation & "',txtPIN = '" & AppDetail.PIN & "',txtFullName = '" & AppDetail.FullName & "', txtEmployerCode = '" & AppDetail.EmployerCode & "', dteDOB = '" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "',txtEmployerName = '" & AppDetail.EmployerName.Replace("'", "''") & "', txtFundStatus = '" & AppDetail.FundStatus & "',IsSignatureConfirmed = '" & AppDetail.isSignatureConfirmed & "',IsPassportConfirmed = '" & AppDetail.IsPassportConfirmed & "',numNSITFInitialAmountPaid ='" & AppDetail.NSITFInitialAmountPaid & "',numNSITFRecievedToRSA ='" & AppDetail.NSITFRecievedToRSA & "', numNSITFRequestedToRSA = '" & AppDetail.NSITFRequestedToRSA & "' where txtApplicationCode = '" & AppDetail.ApplicationID & "'"

			Else

				sql1 = "update tblMemberApplication set fkiMemberID = " & AppDetail.MemberID & ",fkiAppTypeId = '" & AppDetail.AppTypeId & "',txtSector ='" & AppDetail.Sector & "', dteApplicationDate = '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "',txtApplicationState = '" & AppDetail.ApplicationState & "',txtApplicationOffice = '" & AppDetail.ApplicationOffice & "',txtAccountName = '" & AppDetail.AccountName & "',txtAccountNo = '" & AppDetail.AccountNo & "', txtBVN = '" & AppDetail.BVN & "',fkiBankID = '" & AppDetail.BankID & "',fkiBranchID = '" & AppDetail.BranchID & "',txtComment ='" & AppDetail.Comment & "', txtStatus = '" & AppDetail.Status & "',dteDocumentCompleted = '" & DateTime.Parse(AppDetail.DateDocumentCompleted).ToString("yyyy-MM-dd") & "',IsDocCompleted = '" & AppDetail.DocCompleted & "',txtCommentGroup = '" & AppDetail.CommentGroup & "',fkiEmployerID = '" & AppDetail.EmployerID & "',numRSABalance = '" & AppDetail.RSABalance & "',dteDisengagement = '" & DateTime.Parse(AppDetail.DateDisengagement).ToString("yyyy-MM-dd") & "',txtSex = '" & AppDetail.Sex & "',txtReason = '" & AppDetail.Reason & "', txtDepartment = '" & AppDetail.Department & "', txtDesignation = '" & AppDetail.Designation & "',txtPIN = '" & AppDetail.PIN & "',txtFullName = '" & AppDetail.FullName & "', txtEmployerCode = '" & AppDetail.EmployerCode & "', dteDOB = '" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "',txtEmployerName = '" & AppDetail.EmployerName.Replace("'", "''") & "', txtFundStatus = '" & AppDetail.FundStatus & "' ,IsSignatureConfirmed = '" & AppDetail.isSignatureConfirmed & "',IsPassportConfirmed = '" & AppDetail.IsPassportConfirmed & "', numNSITFInitialAmountPaid ='" & AppDetail.NSITFInitialAmountPaid & "',numNSITFRecievedToRSA ='" & AppDetail.NSITFRecievedToRSA & "', numNSITFRequestedToRSA = '" & AppDetail.NSITFRequestedToRSA & "' where txtApplicationCode = '" & AppDetail.ApplicationID & "'"


			End If

		ElseIf AppDetail.DocCompleted = 0 Then

			If AppDetail.IsRetirement = True Then

				sql1 = "update tblMemberApplication set fkiMemberID = " & AppDetail.MemberID & ",fkiAppTypeId = '" & AppDetail.AppTypeId & "',txtSector ='" & AppDetail.Sector & "', dteApplicationDate = '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "',txtApplicationState = '" & AppDetail.ApplicationState & "',txtApplicationOffice = '" & AppDetail.ApplicationOffice & "',txtAccountName = '" & AppDetail.AccountName & "',txtAccountNo = '" & AppDetail.AccountNo & "', txtBVN = '" & AppDetail.BVN & "',fkiBankID = '" & AppDetail.BankID & "',fkiBranchID = '" & AppDetail.BranchID & "',txtComment ='" & AppDetail.Comment & "', txtStatus = '" & AppDetail.Status & "',IsDocCompleted = '" & AppDetail.DocCompleted & "',txtCommentGroup = '" & AppDetail.CommentGroup & "',fkiEmployerID = '" & AppDetail.EmployerID & "',numRSABalance = '" & AppDetail.RSABalance & "',dteDOR = '" & DateTime.Parse(AppDetail.DOR).ToString("yyyy-MM-dd") & "',txtSex = '" & AppDetail.Sex & "',txtReason = '" & AppDetail.Reason & "', txtDepartment = '" & AppDetail.Department & "', txtDesignation = '" & AppDetail.Designation & "',txtPIN = '" & AppDetail.PIN & "',txtFullName = '" & AppDetail.FullName & "', txtEmployerCode = '" & AppDetail.EmployerCode & "', dteDOB = '" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "',txtEmployerName = '" & AppDetail.EmployerName.Replace("'", "''") & "', txtFundStatus = '" & AppDetail.FundStatus & "' ,IsSignatureConfirmed = '" & AppDetail.isSignatureConfirmed & "',IsPassportConfirmed = '" & AppDetail.IsPassportConfirmed & "' where txtApplicationCode = '" & AppDetail.ApplicationID & "'"

			Else

				sql1 = "update tblMemberApplication set fkiMemberID = " & AppDetail.MemberID & ",fkiAppTypeId = '" & AppDetail.AppTypeId & "',txtSector ='" & AppDetail.Sector & "', dteApplicationDate = '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "',txtApplicationState = '" & AppDetail.ApplicationState & "',txtApplicationOffice = '" & AppDetail.ApplicationOffice & "',txtAccountName = '" & AppDetail.AccountName & "',txtAccountNo = '" & AppDetail.AccountNo & "', txtBVN = '" & AppDetail.BVN & "',fkiBankID = '" & AppDetail.BankID & "',fkiBranchID = '" & AppDetail.BranchID & "',txtComment ='" & AppDetail.Comment & "', txtStatus = '" & AppDetail.Status & "',IsDocCompleted = '" & AppDetail.DocCompleted & "',txtCommentGroup = '" & AppDetail.CommentGroup & "',fkiEmployerID = '" & AppDetail.EmployerID & "',numRSABalance = '" & AppDetail.RSABalance & "',dteDisengagement = '" & DateTime.Parse(AppDetail.DateDisengagement).ToString("yyyy-MM-dd") & "',txtSex = '" & AppDetail.Sex & "',txtReason = '" & AppDetail.Reason & "', txtDepartment = '" & AppDetail.Department & "', txtDesignation = '" & AppDetail.Designation & "',txtPIN = '" & AppDetail.PIN & "',txtFullName = '" & AppDetail.FullName & "', txtEmployerCode = '" & AppDetail.EmployerCode & "', dteDOB = '" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "',txtEmployerName = '" & AppDetail.EmployerName.Replace("'", "''") & "', txtFundStatus = '" & AppDetail.FundStatus & "' ,IsSignatureConfirmed = '" & AppDetail.isSignatureConfirmed & "',IsPassportConfirmed = '" & AppDetail.IsPassportConfirmed & "' where txtApplicationCode = '" & AppDetail.ApplicationID & "'"

			End If


		End If

			myComm.CommandText = sql1
			myComm.ExecuteNonQuery()


			'''''''''''''''''''''''''''''''inserting ARL Acknowledgment'''''''''''''''''''''''''''''''''''
			If AppDetail.IsARLActRecieved = True Then

				myComm.CommandText = "insert into tblARLAcknowledgment(txtApplicationCode,dteAcknowledgment) select '" & AppDetail.ApplicationID & "',GETDATE() where (select count(*) from tblARLAcknowledgment where txtApplicationCode =  '" & AppDetail.ApplicationID & "' ) = 0"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			Else

			End If


			'''''''''''''''dms additions and connecting to dms server'''''''''''''''''''''''
			Dim copyResult As Boolean
			Dim docFileExt As String = ""
			Dim dtAppPreference As DataTable = PMgetApplicationPreference()
			Dim uName As String, uPWD As String, uRI As String
			uName = ConfigurationManager.AppSettings("FileNetUName")
			uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
			uRI = ConfigurationManager.AppSettings("FileNetURI")
			Dim dms As New PaymentModuleDMSWindow.CEEntry, DocumentID As String = "", DocumentExt As String
			'dms.getConnection("o-taiwo", "fanthony16,,..", "http://172.16.0.32:9080/wsi/FNCEWS40MTOM/")
			dms.getConnection(uName, uPWD, uRI)


			'''''''''''''''''''''''''''''''''''''''''''''''''''




			'checking if there were previously saved documents
			If AppDocOLD.Count > 0 Then
				Try






					Dim i As Integer

					'checking if there are documents to save back

					Do While i < AppDocNew.Count
						Dim filePath As String = "", DestinationDir As String = "", DestinationFile As String = ""

						If AppDocNew(i).DocumentLocation.ToString.Split("|").Length > 0 And CStr(AppDocNew(i).DocumentLocation.ToString.Split("|")(0)).Trim() <> "&nbsp;" Then

							'constructing document file name in the temp folder for the logged on user
							filePath = AppDocNew(i).DocumentLocation.ToString.Split("|")(1) + AppDocNew(i).ReceivedBy + "\" + AppDocNew(i).DocumentLocation.ToString.Split("|")(0)
							'the temporary folder for the logon user
							DestinationDir = AppDocNew(i).DocumentLocation.ToString.Split("|")(2)
							' the destination document file name to be saved in central location
							DestinationFile = AppDocNew(i).DocumentLocation.ToString.Split("|")(2) + AppDetail.ApplicationID.ToString.Replace("-", "_") + "_" + AppDocNew(i).DocumentLocation.ToString.Split("|")(0)

						Else

						End If


						''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
						''''''Document Reupload code rewrite 2017-03-16
						''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


						Dim query = From n In AppDocOLD
							  Where n.MemberApplicationID = AppDetail.ApplicationID.ToString _
							  And n.DocumentTypeID = AppDocNew(i).DocumentTypeID


						If query.Count > 0 Then

							Dim OldFile As String = query(0).DocumentLocation.ToString
							DocumentExt = query(0).DMSDocumentExt.ToString
							DocumentID = query(0).DMSDocumentID.ToString
							Dim newFilePath As String = AppDocNew(i).DocumentLocation.ToString.Split("|")(2) & AppDetail.ApplicationID.Replace("-", "_") & "_" & AppDocNew(i).DocumentLocation.ToString.Split("|")(0)




							Dim str() As String


							'''''''''''''''''''old code before dms inclusion''''''''''''''''
							If File.Exists(OldFile) = True And File.Exists(filePath) = True Then

								File.Delete(OldFile)
								dms.DeleteDocument(query(0).DMSDocumentID.ToString, "LPPFA_BPD")

								'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)

								If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
									str = DestinationFile.Split(".")
									Array.Reverse(str)
									docFileExt = str(0)

									If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
										copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
									Else
										copyResult = copyFile(filePath, DestinationDir, DestinationFile)
									End If

									DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
									If File.Exists(DestinationFile) = True Then
										File.Delete(DestinationFile)
									Else
									End If
								Else
									DocumentID = ""
									docFileExt = ""
								End If


								If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
									copyResult = copyFile(filePath, DestinationDir, DestinationFile)
									If copyResult = False Then
										DestinationFile = ""
									Else
									End If
								Else
									DestinationFile = ""
								End If



							ElseIf File.Exists(OldFile) = False And File.Exists(filePath) = True Then

								'copyResult = copyFile(filePath, DestinationDir, DestinationFile)  old code line
								'File.Delete(OldFile)
								dms.DeleteDocument(query(0).DMSDocumentID.ToString, "LPPFA_BPD")

								'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)

								If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
									str = DestinationFile.Split(".")
									Array.Reverse(str)
									docFileExt = str(0)

									If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
										copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
									Else
										copyResult = copyFile(filePath, DestinationDir, DestinationFile)
									End If

									DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
									If File.Exists(DestinationFile) = True Then
										File.Delete(DestinationFile)
									Else
									End If
								Else
									DocumentID = ""
									docFileExt = ""
								End If


								If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then

									copyResult = copyFile(filePath, DestinationDir, DestinationFile)

									If copyResult = False Then
										DestinationFile = ""
									Else
									End If

								Else

									DestinationFile = ""

								End If


							Else

								If query(0).DocumentLocation.Split(".").Length = 1 Then
									DestinationFile = ""
								Else
									DestinationFile = query(0).DocumentLocation
								End If

								DocumentID = query(0).DMSDocumentID
								docFileExt = query(0).DMSDocumentExt

							End If

							'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


							'checking if document is to be dropped in DMS or fileSystem
							'Dim copyResult As Boolean
							'Dim str() As String
							'Dim docFileExt As String = ""


							'If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
							'	str = DestinationFile.Split(".")
							'	Array.Reverse(str)
							'	docFileExt = str(0)

							'	If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
							'		copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
							'	Else
							'		copyResult = copyFile(filePath, DestinationDir, DestinationFile)
							'	End If

							'	DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA")
							'	If File.Exists(DestinationFile) = True Then
							'		File.Delete(DestinationFile)
							'	Else
							'	End If
							'Else
							'	DocumentID = ""
							'	docFileExt = ""
							'End If



							'If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
							'	copyResult = copyFile(filePath, DestinationDir, DestinationFile)
							'Else
							'	DestinationFile = ""
							'End If



							'Dim str() As String
							'str = DestinationFile.Split(".")
							'Array.Reverse(str)

							'If str.Length = 1 Then

							'	DocumentID = OldFile
							'	DestinationFile = ""
							'Else
							'	DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA")
							'	DocumentExt = str(0)
							'End If





							'updating recieved document with the latest scanned document where there were cases of previously mapped scanned document
							myComm.CommandText = "update tblMemberDocument set dteReceived =  '" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "',txtdocumentSource = '" & AppDetail.ApplicationOffice & "',txtReceivedBy = '" & AppDocNew(i).ReceivedBy & "',txtDocumentPath = '" & DestinationFile & "', txtDMSDocumentID = '" & DocumentID & "',txtDMSDocumentExt = '" & docFileExt & "' where fkiDocumentTypeID = '" & AppDocNew(i).DocumentTypeID & "' and  fkiMemberApplicationID = '" & AppDetail.ApplicationID.ToString & "'  "
							command.CommandType = CommandType.Text
							myComm.ExecuteNonQuery()


							'insert new document with the scanned document
						ElseIf query.Count = 0 Then ''And DestinationFile <> "" And File.Exists(DestinationFile) = True Then

							Dim str() As String
							'str = DestinationFile.Split(".")
							'Array.Reverse(str)
							'DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA")


							If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
								str = DestinationFile.Split(".")
								Array.Reverse(str)
								docFileExt = str(0)

								If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
									copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
								Else
									copyResult = copyFile(filePath, DestinationDir, DestinationFile)
								End If

								DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
								If File.Exists(DestinationFile) = True Then
									File.Delete(DestinationFile)
								Else
								End If
							Else
								DocumentID = ""
								docFileExt = ""
							End If


							If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
								copyResult = copyFile(filePath, DestinationDir, DestinationFile)

								If copyResult = False Then
									DestinationFile = ""
								Else
								End If

							Else
								DestinationFile = ""
							End If








							'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)
							'copyResult = copyFile(filePath, DestinationDir, DestinationFile)

							If File.Exists(DestinationFile) = True Or DocumentID <> "" Then

								'Dim str() As String
								'str = DestinationFile.Split(".")
								'Array.Reverse(str)
								'DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA")



								myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtDocumentPath,txtApplicationCode,isVerified,txtDMSDocumentID,txtDMSDocumentExt) values ('" & AppDocNew(i).DocumentTypeID & "','" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDocNew(i).ReceivedBy & "','" & DestinationFile & "','" & AppDetail.ApplicationID.ToString & "','" & AppDocNew(i).IsVerified & "','" & DocumentID & "','" & str(0) & "') "
								command.CommandType = CommandType.Text
								myComm.ExecuteNonQuery()










							ElseIf File.Exists(DestinationFile) = False Then





								myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtApplicationCode,isVerified) values ('" & AppDocNew(i).DocumentTypeID & "','" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDocNew(i).ReceivedBy & "','" & AppDetail.ApplicationID.ToString & "','" & AppDocNew(i).IsVerified & "') "
								command.CommandType = CommandType.Text
								myComm.ExecuteNonQuery()

								'added today on 2017-06-09
								''''Setting the application status to entry for ANY case of missing document in the central location
								AppDetail.DocCompleted = 0
								AppDetail.Status = "Entry"
								sql1 = "update tblMemberApplication set IsDocCompleted = '" & AppDetail.DocCompleted & "', txtStatus = '" & AppDetail.Status & "' where txtApplicationCode = '" & AppDetail.ApplicationID & "'"
								myComm.CommandText = sql1
								myComm.ExecuteNonQuery()

							End If


						ElseIf query.Count = 0 And DestinationFile = "" Then

							myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtApplicationCode,isVerified) values ('" & AppDocNew(i).DocumentTypeID & "','" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDocNew(i).ReceivedBy & "','" & AppDetail.ApplicationID.ToString & "','" & AppDocNew(i).IsVerified & "') "
							command.CommandType = CommandType.Text
							myComm.ExecuteNonQuery()

						End If

						i = i + 1

					Loop

					i = 0

					'inserting new other(adhoc) application document if it exists

					Do While i < AppAdhocDoc.Count

						Dim filePath As String = AppAdhocDoc(i).DocPath.ToString.Split("|")(1) + AppAdhocDoc(i).RecievedBy + "\" + AppAdhocDoc(i).DocPath.ToString.Split("|")(0)
						Dim DestinationDir As String = AppAdhocDoc(i).DocPath.ToString.Split("|")(2)
						Dim DestinationFile As String = AppAdhocDoc(i).DocPath.ToString.Split("|")(2) + AppDetail.ApplicationID.ToString.Replace("-", "_") + "_" + AppAdhocDoc(i).DocPath.ToString.Split("|")(0)
						'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)
						copyResult = copyFile(filePath, DestinationDir, DestinationFile)

						If copyResult = True Then

							Dim sqll As String
							sqll = "insert into tblAdhocApplicationDocuments (txtDescription,txtDocPath,txtApplicationCode,dteRecieved,txtRecievedBy,txtDocumentSource,isVerified) values ('" & AppAdhocDoc(i).Description & "','" & DestinationFile & "','" & AppDetail.ApplicationID.ToString & "','" & DateTime.Parse(AppAdhocDoc(i).RecievedDate).ToString("yyyy-MM-dd HH:MM") & "','" & AppAdhocDoc(i).RecievedBy & "','" & AppDetail.ApplicationOffice & "','" & AppAdhocDoc(i).IsVerified & "') "
							myComm.CommandText = sqll
							command.CommandType = CommandType.Text
							myComm.ExecuteNonQuery()

						Else
						End If

						i = i + 1

					Loop






				Catch ex As Exception
					'MsgBox("" & ex.Message)
				End Try


				'if there were no old files found
			Else


				Dim i As Integer
				'insert new document for the application where documents never existed before
				Do While i < AppDocNew.Count

					Dim DestinationFile As String = ""

					If AppDocNew(i).DocumentLocation.ToString.Split("|").Length > 0 And CStr(AppDocNew(i).DocumentLocation.ToString.Split("|")(0)).Trim() <> "&nbsp;" Then
						' the destination document file name to be saved in central location
						DestinationFile = AppDocNew(i).DocumentLocation.ToString.Split("|")(2) + AppDetail.ApplicationID + "_" + AppDocNew(i).DocumentLocation.ToString.Split("|")(0)

					Else
					End If






					If DestinationFile = "" Then

						myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtApplicationCode) values ('" & AppDocNew(i).DocumentTypeID & "','" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDocNew(i).ReceivedBy & "','" & AppDetail.ApplicationID.ToString & "') "
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()
						'.Split("-")(1)
					ElseIf DestinationFile <> "" Then

						Dim filePath, DestinationDir As String
						'constructing document file name in the temp folder for the logged on user
						filePath = AppDocNew(i).DocumentLocation.ToString.Split("|")(1) + AppDocNew(i).ReceivedBy + "\" + AppDocNew(i).DocumentLocation.ToString.Split("|")(0)
						'the temporary folder for the logon user
						DestinationDir = AppDocNew(i).DocumentLocation.ToString.Split("|")(2)


						'moving the attached document to the central location 'odd code before dms addition
						'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)


						'checking if document is to be dropped in DMS or fileSystem
						'Dim copyResult As Boolean
						Dim str() As String
						'Dim docFileExt As String = ""


						If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
							str = DestinationFile.Split(".")
							Array.Reverse(str)
							docFileExt = str(0)

							If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
								copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
							Else
								copyResult = copyFile(filePath, DestinationDir, DestinationFile)
							End If

							DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
							If File.Exists(DestinationFile) = True Then
								File.Delete(DestinationFile)
							Else
							End If
						Else
							DocumentID = ""
							docFileExt = ""
						End If


						If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
							copyResult = copyFile(filePath, DestinationDir, DestinationFile)
						Else
							DestinationFile = ""
						End If


						If File.Exists(DestinationFile) = True Or DocumentID <> "" Then

							myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtDocumentPath,txtApplicationCode,txtDMSDocumentID,txtDMSDocumentExt,isVerified) values ('" & AppDocNew(i).DocumentTypeID & "','" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDocNew(i).ReceivedBy & "','" & DestinationFile & "','" & AppDetail.ApplicationID.ToString & "','" & DocumentID & "','" & docFileExt & "','" & AppDocNew(i).IsVerified & "') "
							'.Split("-")(1)

						Else

							myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtApplicationCode) values ('" & AppDocNew(i).DocumentTypeID & "','" & DateTime.Parse(AppDocNew(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDocNew(i).ReceivedBy & "','" & AppDetail.ApplicationID.ToString & "') "
							command.CommandType = CommandType.Text
							myComm.ExecuteNonQuery()

							'.Split("-")(1)

						End If

						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					End If

					i = i + 1
				Loop

			End If



			'checking if dere already submitted document to be deleted.
			''''''''''''''''''''''''''''''
			If AppDocNew.Count > 0 Then

				Dim i As Integer = 0

				Do While i < AppDocOLD.Count


					Dim query = From n In AppDocNew _
							  Where n.DocumentTypeID = AppDocOLD(i).DocumentTypeID

					If query.Count > 0 Then


					ElseIf query.Count = 0 Then

						myComm.CommandText = "update tblMemberDocument set IsDeactivated = 1,dteDeactivated = '" & DateTime.Parse(Now) & "', txtDeactivatedBy = '" & AppDocNew(i).ReceivedBy & "' where fkiDocumentTypeID = '" & AppDocOLD(i).DocumentTypeID & "' and fkiMemberApplicationID = '" & AppDetail.ApplicationID.ToString & "'"
						'.Split("-")(1)
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()


					End If

					i = i + 1
				Loop

			Else
				'delete all saved documents
				Dim i As Integer = 0
				Do While i < AppDocOLD.Count

					Dim OldFile As String = AppDocOLD(i).DocumentLocation

					'delete from the central location if scanned document exist and the document is not required again
					If File.Exists(OldFile) = True Then
						File.Delete(OldFile)
					Else
					End If

					myComm.CommandText = "update tblMemberDocument set IsDeactivated = 1,dteDeactivated = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM:SS") & "', txtDeactivatedBy = '" & AppDocOLD(i).ReceivedBy & "' where fkiDocumentTypeID = '" & AppDocOLD(i).DocumentTypeID & "' and fkiMemberApplicationID = '" & AppDetail.ApplicationID.ToString & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					'.Split("-")(1)
					'End If

					i = i + 1
				Loop

			End If


			'myComm.CommandText = "sp_pm_CleanUploadedApplicationDocuments"
			'command.CommandType = CommandType.StoredProcedure
			'command.Parameters.Add(New SqlClient.SqlParameter("@txtapplicationcode", SqlDbType.VarChar))
			'command.Parameters("@txtapplicationcode").Value = AppDetail.ApplicationID
			'myComm.ExecuteNonQuery()


			If IsNothing(AppDetail.RetirementDetails) = False Then

				If AppDetail.RetirementDetails.isProgramWithdrawal = True Then


					myComm.CommandText = "delete from [dbo].[tmpPWAnnuityDetails] where txtApplicationCode = '" & AppDetail.ApplicationID & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "INSERT INTO [dbo].[tmpPWAnnuityDetails] ([txtApplicationCode],[numBasicSalary],[numHouseRent],[numTransport],[numUtility],[numConsolidatedAallowance],[numConsolidatedSalary],[numMonthlyTotal],[numAnnualTotalEmolumentAdj],[numAccruedRight],[numRecommendedLumpSum],[numMonthlyDrowDown],[numRSABalance],[dtePriceDate]) VALUES ('" & AppDetail.ApplicationID & "','" & AppDetail.RetirementDetails.BasicSalary & "','" & AppDetail.RetirementDetails.HouseRent & "','" & AppDetail.RetirementDetails.Transport & "','" & AppDetail.RetirementDetails.Utility & "', '" & AppDetail.RetirementDetails.ConsolidatedAallowance & "', '" & AppDetail.RetirementDetails.ConsolidatedSalary & "'   ,'" & AppDetail.RetirementDetails.MonthlyTotal & "', '" & AppDetail.RetirementDetails.AnnualTotalEmolumentAdj & "', '" & AppDetail.RetirementDetails.AccruedRight & "', '" & AppDetail.RetirementDetails.RecommendedLumpSum & "', '" & AppDetail.RetirementDetails.MonthlyProgramedDrawndown & "','" & AppDetail.RetirementDetails.RSABalance & "','" & DateTime.Parse(AppDetail.RetirementDetails.PriceDate).ToString("yyyy-MM-dd") & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				ElseIf AppDetail.RetirementDetails.isAnnuity = True Then

					myComm.CommandText = "delete from [dbo].[tmpPWAnnuityDetails] where txtApplicationCode = '" & AppDetail.ApplicationID & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					myComm.CommandText = "INSERT INTO [dbo].[tmpPWAnnuityDetails] ([txtApplicationCode],[numBasicSalary],[numHouseRent],[numTransport],[numUtility],[numConsolidatedAallowance],[numConsolidatedSalary],[numMonthlyTotal],[numAnnualTotalEmolumentAdj],[txtInsuranceCompanyName],[dteAnnuityCcommencementDate],[numRSABalance],[numPremium],[numLumpSum],[numMonthlyAnnuity],[dtePriceDate]) VALUES ('" & AppDetail.ApplicationID & "','" & AppDetail.RetirementDetails.BasicSalary & "','" & AppDetail.RetirementDetails.HouseRent & "','" & AppDetail.RetirementDetails.Transport & "','" & AppDetail.RetirementDetails.Utility & "', '" & AppDetail.RetirementDetails.ConsolidatedAallowance & "', '" & AppDetail.RetirementDetails.ConsolidatedSalary & "'   ,'" & AppDetail.RetirementDetails.MonthlyTotal & "', '" & AppDetail.RetirementDetails.AnnualTotalEmolumentAdj & "', '" & AppDetail.RetirementDetails.InsuranceCoy & "', '" & DateTime.Parse(AppDetail.RetirementDetails.AnnuityCommencement).ToString("yyyy-MM-dd") & "', '" & AppDetail.RetirementDetails.RSABalance & "', '" & AppDetail.RetirementDetails.Premium & "', '" & AppDetail.RetirementDetails.AnnuityLumpSum & "', '" & AppDetail.RetirementDetails.MonthlyAnnuity & "', '" & DateTime.Parse(AppDetail.RetirementDetails.PriceDate).ToString("yyyy-MM-dd") & "')"

					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				ElseIf AppDetail.RetirementDetails.IsDeathBenefit = True Then

					myComm.CommandText = "delete from [dbo].[tmpDB] where txtApplicationCode = '" & AppDetail.ApplicationID & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					myComm.CommandText = "INSERT INTO [dbo].[tmpDB] ([txtApplicationCode],[dteRetirement],[dteDeath],[txtAdminLetterAuthority],[dteAdminLetter],[txtAdminNOK],[numInsuranceProceed],[numAccruedRight],[numContribution],[numInvestmentIncome],[numRSABalance],[txtRemarks],[dtePriceDate]) VALUES ('" & AppDetail.ApplicationID & "','" & DateTime.Parse(AppDetail.RetirementDetails.RetirementDate).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(AppDetail.RetirementDetails.DeathDate).ToString("yyyy-MM-dd") & "','" & AppDetail.RetirementDetails.AdminIssuingAuthority & "','" & DateTime.Parse(AppDetail.RetirementDetails.AdminIssuingDate).ToString("yyyy-MM-dd") & "', '" & AppDetail.RetirementDetails.AdminNOK & "', '" & AppDetail.RetirementDetails.InsuranceProceed & "'   ,'" & AppDetail.RetirementDetails.AccruedRight & "', '" & AppDetail.RetirementDetails.Contribution & "', '" & AppDetail.RetirementDetails.InvestmentIncome & "', '" & AppDetail.RetirementDetails.RSABalance & "', '" & AppDetail.RetirementDetails.Remarks & "', '" & DateTime.Parse(AppDetail.RetirementDetails.PriceDate).ToString("yyyy-MM-dd") & "')"

					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				End If


			Else

			End If


			sqlTran.Commit()


		Catch ex As Exception
			MsgBox("" & ex.StackTrace)
		End Try






	End Sub

	Public Function PMgetApprovalTypeCodebyID(AppTypeID As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from tblApplicationType a where pkiAppTypeId  = @AppTypeID", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppTypeID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@AppTypeID").Value = AppTypeID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblApplicationType")
			dtUser = dsUser.Tables("tblApplicationType")
			mycon.Close()

			Return dtUser

		Catch ex As Exception

		End Try

		Return Nothing

	End Function


	Public Function PMgetApprovalTypebyID(AppTypeID As Integer) As String

		Dim lstAppTypes As New List(Of String)
		Dim dc As New AppDocumentsDataContext, appTypeName As String = ""
		Dim query = From m In dc.tblApplicationTypes Where m.pkiAppTypeId = AppTypeID
				  Select m

		For Each a As tblApplicationType In query
			appTypeName = a.txtDescription
		Next

		Return appTypeName

	End Function

	Public Function PMgetSubmitAdhocDocument(PIN As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select intDocumentID,isnull(isRetiree,0) isRetiree,cast(dteDOR as date ) dteDOR,fkiDocumentTypeID,txtDescription,fkiDocumentTypeID,dteRecieved,txtDocPath,txtDMSDocumentID,txtDMSDocumentExt,(select txtDocumentName from tblDocumentType where pkiDocumentTypeID = a.fkiDocumentTypeID ) documentName from tblAdhocApplicationDocuments a where txtPIN  = @PIN", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblAdhocApplicationDocuments")
			dtUser = dsUser.Tables("tblAdhocApplicationDocuments")
			mycon.Close()

			Return dtUser

		Catch ex As Exception

		End Try

		Return Nothing


	End Function

	Public Function PMSubmitAdhocDocument(AppAdhocDoc As AdhocDocuments, userName As String) As Boolean

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""
		Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
		myComm = mycon.CreateCommand
		myComm.Transaction = sqlTran


		Try

			Dim dtAppPreference As DataTable = PMgetApplicationPreference()
			Dim uName As String, uPWD As String, uRI As String
			uName = ConfigurationManager.AppSettings("FileNetUName")
			uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
			uRI = ConfigurationManager.AppSettings("FileNetURI")

			Dim dms As New PaymentModuleDMSWindow.CEEntry, DocumentID As String = ""
			'dms.getConnection("", "", "http://172.16.0.32:9080/wsi/FNCEWS40MTOM/")
			dms.getConnection(uName, uPWD, uRI)




			Dim filePath As String = AppAdhocDoc.DocPath.ToString.Split("|")(1) + userName + "\" + AppAdhocDoc.DocPath.ToString.Split("|")(0)
			Dim DestinationDir As String = AppAdhocDoc.DocPath.ToString.Split("|")(2)
			Dim DestinationFile As String = AppAdhocDoc.DocPath.ToString.Split("|")(2) + "NAN" + "_" + AppAdhocDoc.DocPath.ToString.Split("|")(0)
			'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)

			Dim copyResult As Boolean
			Dim str() As String
			Dim docFileExt As String = ""


			If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then

				str = DestinationFile.Split(".")
				Array.Reverse(str)
				docFileExt = str(0)

				If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
					copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
				Else
					copyResult = copyFile(filePath, DestinationDir, DestinationFile)
				End If

				DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
				If File.Exists(DestinationFile) = True Then
					File.Delete(DestinationFile)
				Else
				End If

			Else

				DocumentID = ""
				docFileExt = ""

			End If


			If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
				copyResult = copyFile(filePath, DestinationDir, DestinationFile)
			Else
				DestinationFile = ""
			End If

			If copyResult = True Then

				Dim sqll As String

				sqll = "insert into tblAdhocApplicationDocuments (txtDescription,txtDocPath,dteRecieved,txtRecievedBy,isVerified,txtPIN,fkiDocumentTypeID,dteDOR,isRetiree,txtDMSDocumentID,txtDMSDocumentExt) values ('" & AppAdhocDoc.Description & "','" & DestinationFile & "','" & DateTime.Parse(AppAdhocDoc.RecievedDate).ToString("yyyy-MM-dd HH:MM") & "','" & AppAdhocDoc.RecievedBy & "','" & AppAdhocDoc.IsVerified & "','" & AppAdhocDoc.PIN & "','" & AppAdhocDoc.DocumentTypeID & "','" & DateTime.Parse(AppAdhocDoc.RetirementDate).ToString("yyyy-MM-dd") & "', '" & AppAdhocDoc.IsRetiree & "','" & DocumentID & "','" & docFileExt & "') "

				myComm.CommandText = sqll
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			Else
			End If

			sqlTran.Commit()
			Return True



		Catch ex As Exception
			Return False
		End Try

	End Function

	Public Function PMSubmitApplication(AppDetail As ApplicationDetail, AppDoc As List(Of ApplicationDocumentDetail), AppAdhocDoc As List(Of AdhocDocuments), userName As String, logPath As String, AppCheckList As ApplicationCheckList, AppCheckListDBA As ApplicationCheckListDOB) As Boolean



		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

		Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
		myComm = mycon.CreateCommand
		myComm.Transaction = sqlTran
		Try
			If AppDetail.DocCompleted = 1 Then

				If AppDetail.IsRetirement = True Then

					sql1 = "insert into tblMemberApplication (txtApplicationCode,fkiMemberID,fkiAppTypeId,txtSector,dteApplicationDate,txtApplicationState,txtApplicationOffice,txtAccountName,txtAccountNo,txtBVN,fkiBankID,fkiBranchID,txtComment,txtStatus,dteDocumentCompleted,IsDocCompleted,txtCommentGroup,fkiEmployerID,numRSABalance,dteDOR,txtSex,txtReason,txtDepartment,txtDesignation,txtPIN,txtFullName,txtEmployerCode,dteDOB,txtEmployerName,txtFundStatus,IsPassportConfirmed,	IsSignatureConfirmed,txtLastChangedPerson,txtFileNo,txtTIN,numNSITFInitialAmountPaid,numNSITFRecievedToRSA,numNSITFRequestedToRSA,txtCreatedBy,blnAgeOverriden,txtReferenceApplicationCode) values ('" & AppDetail.ApplicationID & "'," & AppDetail.MemberID & ",'" & AppDetail.AppTypeId & "', '" & AppDetail.Sector & "', '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationState & "','" & AppDetail.ApplicationOffice & "','" & AppDetail.AccountName.Replace("'", "''") & "','" & AppDetail.AccountNo & "','" & AppDetail.BVN & "','" & AppDetail.BankID & "','" & AppDetail.BranchID & "','" & AppDetail.Comment.Replace("'", "''") & "','" & AppDetail.Status & "','" & DateTime.Parse(AppDetail.DateDocumentCompleted).ToString("yyyy-MM-dd HH:MM") & "','" & AppDetail.DocCompleted & "','" & AppDetail.CommentGroup & "','" & AppDetail.EmployerID & "','" & AppDetail.RSABalance & "','" & DateTime.Parse(AppDetail.DOR).ToString("yyyy-MM-dd") & "','" & AppDetail.Sex & "','" & AppDetail.Reason & "','" & AppDetail.Department & "','" & AppDetail.Designation & "','" & AppDetail.PIN & "','" & AppDetail.FullName.Replace("'", "''") & "','" & AppDetail.EmployerCode & "','" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "','" & AppDetail.EmployerName.Replace("'", "''") & "','" & AppDetail.FundStatus & "','" & AppDetail.IsPassportConfirmed & "','" & AppDetail.isSignatureConfirmed & "','" & AppDetail.CreatedBy & "','" & AppDetail.FileNumber & "','" & AppDetail.TIN & "','" & AppDetail.NSITFInitialAmountPaid & "','" & AppDetail.NSITFRecievedToRSA & "','" & AppDetail.NSITFRequestedToRSA & "','" & userName & "','" & AppDetail.AgeConstrainstOverwitten & "','" & AppDetail.ReferenceApplicationCode & "')"

				Else

					sql1 = "insert into tblMemberApplication (txtApplicationCode,fkiMemberID,fkiAppTypeId,txtSector,dteApplicationDate,txtApplicationState,txtApplicationOffice,txtAccountName,txtAccountNo,txtBVN,fkiBankID,fkiBranchID,txtComment,txtStatus,dteDocumentCompleted,IsDocCompleted,txtCommentGroup,fkiEmployerID,numRSABalance,txtSex,txtReason,txtDepartment,txtDesignation,dteDisengagement,txtPIN,txtFullName,txtEmployerCode,dteDOB,txtEmployerName,txtFundStatus,IsPassportConfirmed,	IsSignatureConfirmed,txtLastChangedPerson,txtFileNo,txtTIN,numNSITFInitialAmountPaid,numNSITFRecievedToRSA,numNSITFRequestedToRSA,txtCreatedBy,blnAgeOverriden,txtReferenceApplicationCode) values ('" & AppDetail.ApplicationID & "'," & AppDetail.MemberID & ",'" & AppDetail.AppTypeId & "', '" & AppDetail.Sector & "', '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationState & "','" & AppDetail.ApplicationOffice & "','" & AppDetail.AccountName.Replace("'", "''") & "','" & AppDetail.AccountNo & "','" & AppDetail.BVN & "','" & AppDetail.BankID & "','" & AppDetail.BranchID & "','" & AppDetail.Comment.Replace("'", "''") & "','" & AppDetail.Status & "','" & DateTime.Parse(AppDetail.DateDocumentCompleted).ToString("yyyy-MM-dd HH:MM") & "','" & AppDetail.DocCompleted & "','" & AppDetail.CommentGroup & "','" & AppDetail.EmployerID & "','" & AppDetail.RSABalance & "','" & AppDetail.Sex & "','" & AppDetail.Reason & "','" & AppDetail.Department & "','" & AppDetail.Designation & "','" & DateTime.Parse(AppDetail.DateDisengagement).ToString("yyyy-MM-dd") & "','" & AppDetail.PIN & "','" & AppDetail.FullName.Replace("'", "''") & "','" & AppDetail.EmployerCode & "','" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "','" & AppDetail.EmployerName.Replace("'", "''") & "','" & AppDetail.FundStatus & "', '" & AppDetail.IsPassportConfirmed & "','" & AppDetail.isSignatureConfirmed & "','" & AppDetail.CreatedBy & "','" & AppDetail.FileNumber & "','" & AppDetail.TIN & "','" & AppDetail.NSITFInitialAmountPaid & "','" & AppDetail.NSITFRecievedToRSA & "','" & AppDetail.NSITFRequestedToRSA & "','" & userName & "','" & AppDetail.AgeConstrainstOverwitten & "','" & AppDetail.ReferenceApplicationCode & "')"

				End If

			ElseIf AppDetail.DocCompleted = 0 Then

				If AppDetail.IsRetirement = True Then
					sql1 = "insert into tblMemberApplication (txtApplicationCode,fkiMemberID,fkiAppTypeId,txtSector,dteApplicationDate,txtApplicationState,txtApplicationOffice,txtAccountName,txtAccountNo,txtBVN,fkiBankID,fkiBranchID,txtComment,txtStatus,IsDocCompleted,txtCommentGroup,fkiEmployerID,numRSABalance,dteDOR,txtSex,txtReason,txtPIN,txtFullName,txtEmployerCode,dteDOB,txtEmployerName,txtFundStatus,IsPassportConfirmed,	IsSignatureConfirmed,txtLastChangedPerson,txtFileNo,txtTIN,numNSITFInitialAmountPaid,numNSITFRecievedToRSA,numNSITFRequestedToRSA,txtCreatedBy,blnAgeOverriden,txtReferenceApplicationCode) values ('" & AppDetail.ApplicationID & "'," & AppDetail.MemberID & ",'" & AppDetail.AppTypeId & "', '" & AppDetail.Sector & "', '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationState & "','" & AppDetail.ApplicationOffice & "','" & AppDetail.AccountName.Replace("'", "''") & "','" & AppDetail.AccountNo & "','" & AppDetail.BVN & "','" & AppDetail.BankID & "','" & AppDetail.BranchID & "','" & AppDetail.Comment.Replace("'", "''") & "','" & AppDetail.Status & "','" & AppDetail.DocCompleted & "','" & AppDetail.CommentGroup & "','" & AppDetail.EmployerID & "','" & AppDetail.RSABalance & "','" & DateTime.Parse(AppDetail.DOR).ToString("yyyy-MM-dd") & "','" & AppDetail.Sex & "','" & AppDetail.Reason & "','" & AppDetail.PIN & "','" & AppDetail.FullName.Replace("'", "''") & "','" & AppDetail.EmployerCode & "','" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "','" & AppDetail.EmployerName.Replace("'", "''") & "','" & AppDetail.FundStatus & "','" & AppDetail.IsPassportConfirmed & "','" & AppDetail.isSignatureConfirmed & "','" & AppDetail.CreatedBy & "','" & AppDetail.FileNumber & "','" & AppDetail.TIN & "','" & AppDetail.NSITFInitialAmountPaid & "','" & AppDetail.NSITFRecievedToRSA & "','" & AppDetail.NSITFRequestedToRSA & "','" & userName & "','" & AppDetail.AgeConstrainstOverwitten & "','" & AppDetail.ReferenceApplicationCode & "')"

				Else

					sql1 = "insert into tblMemberApplication (txtApplicationCode,fkiMemberID,fkiAppTypeId,txtSector,dteApplicationDate,txtApplicationState,txtApplicationOffice,txtAccountName,txtAccountNo,txtBVN,fkiBankID,fkiBranchID,txtComment,txtStatus,IsDocCompleted,txtCommentGroup,fkiEmployerID,numRSABalance,txtSex,txtReason,txtDepartment,txtDesignation,dteDisengagement,txtPIN,txtFullName,txtEmployerCode,dteDOB,txtEmployerName,txtFundStatus,IsPassportConfirmed,	IsSignatureConfirmed,txtLastChangedPerson,txtFileNo,txtTIN,numNSITFInitialAmountPaid,numNSITFRecievedToRSA,numNSITFRequestedToRSA,txtCreatedBy,blnAgeOverriden,txtReferenceApplicationCode) values ('" & AppDetail.ApplicationID & "'," & AppDetail.MemberID & ",'" & AppDetail.AppTypeId & "', '" & AppDetail.Sector & "', '" & DateTime.Parse(AppDetail.ApplicationDate).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationState & "','" & AppDetail.ApplicationOffice & "','" & AppDetail.AccountName.Replace("'", "''") & "','" & AppDetail.AccountNo & "','" & AppDetail.BVN & "','" & AppDetail.BankID & "','" & AppDetail.BranchID & "','" & AppDetail.Comment.Replace("'", "''") & "','" & AppDetail.Status & "','" & AppDetail.DocCompleted & "','" & AppDetail.CommentGroup & "','" & AppDetail.EmployerID & "','" & AppDetail.RSABalance & "','" & AppDetail.Sex & "','" & AppDetail.Reason & "','" & AppDetail.Department & "','" & AppDetail.Designation & "','" & DateTime.Parse(AppDetail.DateDisengagement).ToString("yyyy-MM-dd") & "','" & AppDetail.PIN & "','" & AppDetail.FullName.Replace("'", "''") & "','" & AppDetail.EmployerCode & "','" & DateTime.Parse(AppDetail.DOB).ToString("yyyy-MM-dd") & "','" & AppDetail.EmployerName.Replace("'", "''") & "','" & AppDetail.FundStatus & "','" & AppDetail.IsPassportConfirmed & "','" & AppDetail.isSignatureConfirmed & "','" & AppDetail.CreatedBy & "','" & AppDetail.FileNumber & "','" & AppDetail.TIN & "','" & AppDetail.NSITFInitialAmountPaid & "','" & AppDetail.NSITFRecievedToRSA & "','" & AppDetail.NSITFRequestedToRSA & "','" & userName & "','" & AppDetail.AgeConstrainstOverwitten & "','" & AppDetail.ReferenceApplicationCode & "')"

				End If

			End If
			'txtReferenceApplicationCode

			myComm.CommandText = sql1
			myComm.ExecuteNonQuery()


			Dim sqlRef As String
			sqlRef = "update tblMemberApplication set txtStatus = 'Terminated', dteTerminated ='" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where txtapplicationCode = '" & AppDetail.ReferenceApplicationCode & "'"
			myComm.CommandText = sqlRef
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()



			''''''''''''''''''''updating the checkList for the application''''''''''''''''''''''''''

			If AppDetail.AppTypeId = 5 Then
			

				sqlRef = "update tblMemberApplication set isLOAAffidavitChecked = '" & AppCheckListDBA.LOAAffidavitChecked & "', isLOANumbersChecked = '" & AppCheckListDBA.LOANumbersChecked & "',isLOAIntroLetter = '" & AppCheckListDBA.LOAIntroLetter & "',isLOAEmployerIntroLetter = '" & AppCheckListDBA.LOAEmployerIntroLetter & "',isLOASignatories = '" & AppCheckListDBA.LOASignatories & "',isPOA = '" & AppCheckListDBA.POA & "',isMinorBirthCert = '" & AppCheckListDBA.MinorBirthCert & "',isNOKMOIs = '" & AppCheckListDBA.NOKMOIs & "',isMOIDocs = '" & AppCheckListDBA.MOIDocs & "',isNamesDOB = '" & AppCheckListDBA.NamesDOB & "',isDODName = '" & AppCheckListDBA.NamesDOB & "' where txtapplicationCode = '" & AppCheckListDBA.ApplicationCode & "'"
			myComm.CommandText = sqlRef
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			Else

				sqlRef = "update tblMemberApplication set isFundingStatusChecked = '" & AppCheckList.FundingStatusChecked & "', isLegAVCChecked = '" & AppCheckList.LegAVCChecked & "',isDOBChecked = '" & AppCheckList.DOBChecked & "',isNamesChecked = '" & AppCheckList.NamesChecked & "',isExitDocChecked = '" & AppCheckList.ExitDocChecked & "',isDataEntryChecked = '" & AppCheckList.DataEntryChecked & "',isValidDocChecked = '" & AppCheckList.ValidDocChecked & "' where txtapplicationCode = '" & AppCheckList.ApplicationCode & "'"
				myComm.CommandText = sqlRef
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


			End If



			''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''







			'''''''''''''''''''''''''''''''ARL Acknowledgment'''''''''''''''''''''''''''''''''''
			If AppDetail.IsARLActRecieved = True Then

				sqlRef = "insert into tblARLAcknowledgment (txtApplicationCode,dteAcknowledgment) values ('" & AppDetail.ApplicationID & "','" & DateTime.Parse(AppDetail.ARLAcknowledgmentDate).ToString("yyyy-MM-dd") & "')"

				myComm.CommandText = sqlRef
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			Else

				sqlRef = "update tblMemberApplication set isARLRecieved = 1 where txtapplicationCode = '" & AppDetail.ApplicationID & "'"

				myComm.CommandText = sqlRef
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			End If
			'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



			'IsARLActRecieved



			Dim dtAppPreference As DataTable = PMgetApplicationPreference()
			Dim uName As String, uPWD As String, uRI As String
			uName = ConfigurationManager.AppSettings("FileNetUName")
			uPWD = ConfigurationManager.AppSettings("FileNetUPWD")
			uRI = ConfigurationManager.AppSettings("FileNetURI")

			Dim dms As New PaymentModuleDMSWindow.CEEntry, DocumentID As String
			dms.getConnection(uName, uPWD, uRI)


			Dim i As Integer

			Do While i < AppAdhocDoc.Count

				Dim filePath As String = AppAdhocDoc(i).DocPath.ToString.Split("|")(1) + userName + "\" + AppAdhocDoc(i).DocPath.ToString.Split("|")(0)
				Dim DestinationDir As String = AppAdhocDoc(i).DocPath.ToString.Split("|")(2)
				Dim DestinationFile As String = AppAdhocDoc(i).DocPath.ToString.Split("|")(2) + AppDetail.ApplicationID.ToString.Replace("-", "_") + "_" + AppAdhocDoc(i).DocPath.ToString.Split("|")(0)

				'Dim copyResult As Boolean = copyFile(filePath, DestinationDir, DestinationFile)

				'checking if document is to be dropped in DMS or fileSystem
				Dim copyResult As Boolean
				Dim str() As String
				Dim docFileExt As String = ""


				If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
					str = DestinationFile.Split(".")
					Array.Reverse(str)
					docFileExt = str(0)

					If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
						copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
					Else
						copyResult = copyFile(filePath, DestinationDir, DestinationFile)
					End If

					DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
					If File.Exists(DestinationFile) = True Then
						File.Delete(DestinationFile)
					Else
					End If
				Else
					DocumentID = ""
					docFileExt = ""
				End If



				If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
					copyResult = copyFile(filePath, DestinationDir, DestinationFile)
				Else
					DestinationFile = ""
				End If



				If copyResult = True Then

					Dim sqll As String
					sqll = "insert into tblAdhocApplicationDocuments (txtDescription,txtDocPath,txtApplicationCode,dteRecieved,txtRecievedBy,txtDocumentSource,isVerified,txtDMSDocumentID,txtDMSDocumentExt) values ('" & AppAdhocDoc(i).Description & "','" & DestinationFile & "','" & AppDetail.ApplicationID.ToString & "','" & DateTime.Parse(AppAdhocDoc(i).RecievedDate).ToString("yyyy-MM-dd HH:MM") & "','" & AppAdhocDoc(i).RecievedBy & "','" & AppDetail.ApplicationOffice & "','" & AppAdhocDoc(i).IsVerified & "','" & DocumentID & "','" & docFileExt & "') "
					myComm.CommandText = sqll
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				Else
				End If

				i = i + 1

			Loop


			i = 0

			DocumentID = ""


			Do While i < AppDoc.Count

				'appDocDetail.DocumentTypeID = CInt(DocumentCollection.Item(row.Cells(1).Text))
				'appDocDetail.DateReceived = CDate((row.Cells(2).Text))


				Dim filePath As String = AppDoc(i).DocumentLocation.ToString.Split("|")(1) + userName + "\" + AppDoc(i).DocumentLocation.ToString.Split("|")(0)
				Dim DestinationDir As String = AppDoc(i).DocumentLocation.ToString.Split("|")(2)
				Dim DestinationFile As String = AppDoc(i).DocumentLocation.ToString.Split("|")(2) + AppDetail.ApplicationID.ToString.Replace("-", "_") + "_" + AppDoc(i).DocumentLocation.ToString.Split("|")(0)


				'checking if document is to be dropped in DMS or fileSystem
				Dim copyResult As Boolean
				Dim str() As String
				Dim docFileExt As String = ""


				If CBool(dtAppPreference.Rows(0).Item("blnIsDMSFileStorage")) = True Then
					str = DestinationFile.Split(".")
					Array.Reverse(str)
					docFileExt = str(0)

					If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
						copyResult = copyFileDMS(filePath, DestinationDir, DestinationFile)
					Else
						copyResult = copyFile(filePath, DestinationDir, DestinationFile)
					End If

					DocumentID = dms.DropDocument(DestinationFile, Path.GetFileNameWithoutExtension(DestinationFile), "LPPFA_BPD")
					If File.Exists(DestinationFile) = True Then
						File.Delete(DestinationFile)
					Else
					End If
				Else
					DocumentID = ""
					docFileExt = ""
				End If


				If CBool(dtAppPreference.Rows(0).Item("blnIsFileSystemStorage")) = True Then
					copyResult = copyFile(filePath, DestinationDir, DestinationFile)
				Else
					DestinationFile = ""
				End If




				If copyResult = True Then

					Dim sqll As String
					sqll = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtDocumentPath,txtApplicationCode,isVerified,txtDMSDocumentID,txtDMSDocumentExt) values ('" & AppDoc(i).DocumentTypeID & "','" & DateTime.Parse(AppDoc(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDetail.CreatedBy & "','" & DestinationFile & "','" & AppDetail.ApplicationID.ToString & "','" & AppDoc(i).IsVerified & "','" & DocumentID & "','" & docFileExt & "') "
					myComm.CommandText = sqll
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Else


					myComm.CommandText = "insert into tblMemberDocument (fkiDocumentTypeID,dteReceived,fkiMemberApplicationID,txtdocumentSource,txtReceivedBy,txtApplicationCode,isVerified) values ('" & AppDoc(i).DocumentTypeID & "','" & DateTime.Parse(AppDoc(i).DateReceived).ToString("yyyy-MM-dd") & "','" & AppDetail.ApplicationID.ToString & "','" & AppDetail.ApplicationOffice & "','" & AppDetail.CreatedBy & "','" & AppDetail.ApplicationID.ToString & "','" & AppDoc(i).IsVerified & "') "
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				End If

				i = i + 1
			Loop


			'myComm.CommandText = "sp_pm_CleanUploadedApplicationDocuments"
			'command.CommandType = CommandType.StoredProcedure
			'command.Parameters.Add(New SqlClient.SqlParameter("@txtapplicationcode", SqlDbType.VarChar))
			'command.Parameters("@txtapplicationcode").Value = AppDetail.ApplicationID
			'myComm.ExecuteNonQuery()



			If CStr(AppDetail.Comment) <> "" Then

				myComm.CommandText = "insert into tblApplicationComments (intAppCommentStage,txtApplicationCode,txtComment,txtCreatedBy) values ('1','" & AppDetail.ApplicationID & "','" & AppDetail.Comment & "', '" & AppDetail.CreatedBy & "')"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()
			Else

			End If


			If IsNothing(AppDetail.RetirementDetails) = False Then

				If AppDetail.RetirementDetails.isProgramWithdrawal = True Then

					myComm.CommandText = "INSERT INTO [dbo].[tmpPWAnnuityDetails] ([txtApplicationCode],[numBasicSalary],[numHouseRent],[numTransport],[numUtility],[numConsolidatedAallowance],[numConsolidatedSalary],[numMonthlyTotal],[numAnnualTotalEmolumentAdj],[numAccruedRight],[numRecommendedLumpSum],[numMonthlyDrowDown],[numRSABalance],[dtePriceDate]) VALUES ('" & AppDetail.ApplicationID & "','" & AppDetail.RetirementDetails.BasicSalary & "','" & AppDetail.RetirementDetails.HouseRent & "','" & AppDetail.RetirementDetails.Transport & "','" & AppDetail.RetirementDetails.Utility & "', '" & AppDetail.RetirementDetails.ConsolidatedAallowance & "', '" & AppDetail.RetirementDetails.ConsolidatedSalary & "'   ,'" & AppDetail.RetirementDetails.MonthlyTotal & "', '" & AppDetail.RetirementDetails.AnnualTotalEmolumentAdj & "', '" & AppDetail.RetirementDetails.AccruedRight & "', '" & AppDetail.RetirementDetails.RecommendedLumpSum & "', '" & AppDetail.RetirementDetails.MonthlyProgramedDrawndown & "','" & AppDetail.RetirementDetails.RSABalance & "','" & DateTime.Parse(AppDetail.RetirementDetails.PriceDate).ToString("yyyy-MM-dd") & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				ElseIf AppDetail.RetirementDetails.isAnnuity = True Then

					myComm.CommandText = "INSERT INTO [dbo].[tmpPWAnnuityDetails] ([txtApplicationCode],[numBasicSalary],[numHouseRent],[numTransport],[numUtility],[numConsolidatedAallowance],[numConsolidatedSalary],[numMonthlyTotal],[numAnnualTotalEmolumentAdj],[txtInsuranceCompanyName],[dteAnnuityCcommencementDate],[numRSABalance],[numPremium],[numLumpSum],[numMonthlyAnnuity],[dtePriceDate]) VALUES ('" & AppDetail.ApplicationID & "','" & AppDetail.RetirementDetails.BasicSalary & "','" & AppDetail.RetirementDetails.HouseRent & "','" & AppDetail.RetirementDetails.Transport & "','" & AppDetail.RetirementDetails.Utility & "', '" & AppDetail.RetirementDetails.ConsolidatedAallowance & "', '" & AppDetail.RetirementDetails.ConsolidatedSalary & "'   ,'" & AppDetail.RetirementDetails.MonthlyTotal & "', '" & AppDetail.RetirementDetails.AnnualTotalEmolumentAdj & "', '" & AppDetail.RetirementDetails.InsuranceCoy & "', '" & DateTime.Parse(AppDetail.RetirementDetails.AnnuityCommencement).ToString("yyyy-MM-dd") & "', '" & AppDetail.RetirementDetails.RSABalance & "', '" & AppDetail.RetirementDetails.Premium & "', '" & AppDetail.RetirementDetails.AnnuityLumpSum & "', '" & AppDetail.RetirementDetails.MonthlyAnnuity & "', '" & DateTime.Parse(AppDetail.RetirementDetails.PriceDate).ToString("yyyy-MM-dd") & "')"

					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				ElseIf AppDetail.RetirementDetails.IsDeathBenefit = True Then

					myComm.CommandText = "INSERT INTO [dbo].[tmpDB] ([txtApplicationCode],[dteRetirement],[dteDeath],[txtAdminLetterAuthority],[dteAdminLetter],[txtAdminNOK],[numInsuranceProceed],[numAccruedRight],[numContribution],[numInvestmentIncome],[numRSABalance],[txtRemarks],[dtePriceDate]) VALUES ('" & AppDetail.ApplicationID & "','" & DateTime.Parse(AppDetail.RetirementDetails.RetirementDate).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(AppDetail.RetirementDetails.DeathDate).ToString("yyyy-MM-dd") & "','" & AppDetail.RetirementDetails.AdminIssuingAuthority & "','" & DateTime.Parse(AppDetail.RetirementDetails.AdminIssuingDate).ToString("yyyy-MM-dd") & "', '" & AppDetail.RetirementDetails.AdminNOK & "', '" & AppDetail.RetirementDetails.InsuranceProceed & "'   ,'" & AppDetail.RetirementDetails.AccruedRight & "', '" & AppDetail.RetirementDetails.Contribution & "', '" & AppDetail.RetirementDetails.InvestmentIncome & "', '" & AppDetail.RetirementDetails.RSABalance & "', '" & AppDetail.RetirementDetails.Remarks & "', '" & DateTime.Parse(AppDetail.RetirementDetails.PriceDate).ToString("yyyy-MM-dd") & "')"

					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				End If


			Else

			End If

			sqlTran.Commit()
			Return True
		Catch ex As Exception
			sqlTran.Rollback()

			Dim loger As New Global.Logger.Logger
			loger.FileSource = "Sure Pay Benefit Applicatiom "
			loger.FilePath = logPath
			loger.Logger(ex.Message & "Core - Application Submission")

			Return False
		End Try


	End Function

	Private Function copyFileDMS(ByVal path As String, ByVal destinationDir As String, ByVal destinationFile As String) As Boolean

		Try

			If File.Exists(path) And Directory.Exists(destinationDir) And Not File.Exists(destinationFile) Then

				'move attached document to the permanent location if it does not already exists
				File.Copy(path, destinationFile)
				'delete document from the temp folder(folder named after the username)
				'File.Delete(path)

				Return True

			ElseIf File.Exists(path) And Directory.Exists(destinationDir) And File.Exists(destinationFile) Then

				'delete document from permanent location if it already exists
				File.Delete(destinationFile)
				'move attached document to the permanent location if it does not already exists
				File.Copy(path, destinationFile)
				'delete document from the temp folder(folder named after the username)
				'File.Delete(path)
				Return True

			Else

				'File.Delete(path)
				Return False

			End If

		Catch ex As Exception

			' MsgBox("" & ex.Message)

		End Try
		Return False
	End Function

	Private Function copyFile(ByVal path As String, ByVal destinationDir As String, ByVal destinationFile As String) As Boolean

		Try

			If File.Exists(path) And Directory.Exists(destinationDir) And Not File.Exists(destinationFile) Then

				'move attached document to the permanent location if it does not already exists
				File.Copy(path, destinationFile)
				'delete document from the temp folder(folder named after the username)
				File.Delete(path)

				Return True

			ElseIf File.Exists(path) And Directory.Exists(destinationDir) And File.Exists(destinationFile) Then

				'delete document from permanent location if it already exists
				File.Delete(destinationFile)
				'move attached document to the permanent location if it does not already exists
				File.Copy(path, destinationFile)
				'delete document from the temp folder(folder named after the username)
				File.Delete(path)
				Return True

			Else

				File.Delete(path)
				Return False

			End If

		Catch ex As Exception

			' MsgBox("" & ex.Message)

		End Try
		Return False
	End Function

	Public Function PMUnitPriceByDate(PriceDate As Date, FundID As Integer) As Double

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("Enpowerv4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select top 1 isnull(UnitPrice,0.0000)  from Enpowerv4.dbo.UnitPrice where ValueDate = @priceDate and  FundID = @FundID ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@priceDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@priceDate").Value = DateTime.Parse(PriceDate).ToString("yyyy-MM-dd")

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@FundID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@FundID").Value = FundID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			If FundID = 1 Then
				Return dtUser.Rows(0).Item(0)
			ElseIf FundID = 2 Then
				Return dtUser.Rows(0).Item(1)
			End If

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
		Return 0.0
	End Function

	Public Sub PMEUpdateEnpowerBiometrics(pin As String, UName As String)
		Try

			Dim conn As New SqlClient.SqlConnection, command As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim db As New DbConnection


			'Dim cmdSelect = New SqlClient.SqlCommand("select a.Picture , b.Picture_Data,b.member_reg_num,c.employeeid into #aa from biometric a, [p-enpower].pencom_standalone.dbo.employee b, employee c where a.EmployeeID = c.employeeid and b.member_Reg_num = c.rsapin and b.member_Reg_num = '" & pin & "' update b set b.Picture = a.Picture_Data from #aa a,biometric b, dbo.employee c where b.employeeid = c.employeeid and a.member_reg_num = c.rsapin and a.member_reg_num = '" & pin & "'", conn)

			'daUser.SelectCommand = cmdSelect
			'daUser.SelectCommand.CommandType = CommandType.Text
			'daUser.SelectCommand.ExecuteNonQuery()



			conn = db.getConnection("EnpowerV4")
			Dim sqlTran As SqlClient.SqlTransaction = conn.BeginTransaction
			command = conn.CreateCommand
			command.Transaction = sqlTran
			command.CommandTimeout = 2000

			'command.CommandText = "insert into [tblAdhocBiometricUpdateHistory] select a.EmployeeID, getdate(), Picture,[Signature],LeftThumb ,RightThumb,'" & UName & "'    from biometric a, employee b where a.EmployeeID = b.EmployeeID and b.RSAPIN = '" & pin & "' "
			'command.CommandType = CommandType.Text
			'command.ExecuteNonQuery()



			command.CommandText = "select a.Picture , b.Picture_Data,b.member_reg_num,c.employeeid,b.signature_data,b.leftthumbprint,b.rightthumbprint into #aa from biometric a, [p-enpower].pencom_standalone.dbo.employee b, employee c where a.EmployeeID = c.employeeid and b.member_Reg_num = c.rsapin and b.member_Reg_num = '" & pin & "' update b set b.Picture = a.Picture_Data,[Signature] = a.signature_data,LeftThumb = a.leftthumbprint,RightThumb = a.rightthumbprint,DateModified = getdate() from #aa a,biometric b, dbo.employee c where b.employeeid = c.employeeid and a.member_reg_num = c.rsapin and a.member_reg_num = '" & pin & "'"
			command.CommandType = CommandType.Text
			command.ExecuteNonQuery()


			sqlTran.Commit()

			conn.Close()


		Catch ex As Exception

		End Try
	End Sub




	Public Function PMValueByDate(PIn As String, PriceDate As Date, FundID As Integer) As Double

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("Enpowerv4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select  [dbo].[GetFundBalanceByDate](a.employeeid,1,@priceDate) RSABalance,[dbo].[GetFundBalanceByDate](a.employeeid,2,@priceDate) RFBalance, (select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,@priceDate)) as Mandatory from dbo.Employee a where rsapin = @pin ", mycon)


			'     (select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,@priceDate))) as Mandatory

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@priceDate", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@priceDate").Value = DateTime.Parse(PriceDate).ToString("yyyy-MM-dd")

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pin").Value = PIn

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			If FundID = 1 Then
				Return dtUser.Rows(0).Item(0)
			ElseIf FundID = 2 Then
				Return dtUser.Rows(0).Item(1)
			End If

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
		Return 0.0
	End Function
	' get the list of pencombatches,extract batches and detail application pins for control checks into enpower
	Public Function PMgetPencomApprovalBatchByType(appTypeID As Integer, batch As String, isDetails As Boolean) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try


			Dim MyDataAdapter As New SqlClient.SqlDataAdapter


			If appTypeID > 0 And isDetails = False And batch = "" Then

				'txtControlCheckedStatus()

				MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E' and isnull(a.txtControlCheckedBy,'')='' or	isnull(a.txtControlVerifiedBy,'')='' or	isnull(a.txtControlAuthorisedBy,'') = '' ) select distinct  appTypeID,txtPencomBatch from tab where appTypeID = @appTypeID", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@appTypeID", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@appTypeID").Value = appTypeID


			ElseIf appTypeID = 0 And isDetails = False Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E' and isnull(a.txtControlCheckedBy,'')='' or	isnull(a.txtControlVerifiedBy,'')='' or	isnull(a.txtControlAuthorisedBy,'') = '' ) select distinct  txtPencomBatch,isnull(txtEnpowerExtractBatch,'') txtEnpowerExtractBatch from tab where txtPencomBatch = @batch and txtEnpowerExtractBatch <> ''", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batch", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@batch").Value = batch


			ElseIf appTypeID < 0 And isDetails = False Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E' and isnull(a.txtControlCheckedBy,'')<> '' or	isnull(a.txtControlVerifiedBy,'') <> '' or	isnull(a.txtControlAuthorisedBy,'') <> '' ) select distinct  txtPencomBatch,isnull(txtEnpowerExtractBatch,'') txtEnpowerExtractBatch from tab where txtPencomBatch = @batch and txtEnpowerExtractBatch <> ''", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batch", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@batch").Value = batch


			ElseIf appTypeID > 0 And isDetails = False And batch <> "" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E' and isnull(a.txtControlCheckedBy,'') <> '' and isnull(a.txtControlVerifiedBy,'') <> '' and isnull(a.txtControlAuthorisedBy,'') <> '' ) select distinct  appTypeID,txtPencomBatch from tab where appTypeID = @appTypeID", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@appTypeID", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@appTypeID").Value = appTypeID

			ElseIf appTypeID > 0 And isDetails = True Then

				Select Case appTypeID

					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)

					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.premium as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr700 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)

					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)

					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr500 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy, a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr800 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)


					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr300 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)


					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr600 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)

					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr200 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch ", mycon)

					Case Else

						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus ,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select txtApplicationCode,PIN,replace(FullName,'|','') FullName,Sector,numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab where txtEnpowerExtractBatch = @batch ", mycon)

				End Select

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batch", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@batch").Value = batch

			End If

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApproval")
			dtUser = dsUser.Tables("MemberApproval")
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
		Return dtUser
	End Function




	' get the list of pencombatches,extract batches and detail application pins for control checks into enpower per control status
	Public Function PMgetPencomApprovalBatchByType(appTypeID As Integer, batch As String, isDetails As Boolean, CStatus As Char) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		'JsonConvert()

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try


			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If appTypeID <> 0 And isDetails = True Then

				Select Case appTypeID

					Case Is = 1

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null ", mycon)


						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null ", mycon)


						End If



					Case Is = 4

						If CStatus = "C" Then
							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.premium as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr700 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.premium as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr700 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.premium as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr700 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null", mycon)


						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.premium as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr700 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null", mycon)

						End If



					Case Is = 16

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null ", mycon)


						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr400 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null ", mycon)

						End If

					Case Is = 2

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr500 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr500 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr500 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null", mycon)

						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr500 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null", mycon)

						End If



					Case Is = 7

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy, a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr800 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy, a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr800 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy, a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr800 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null", mycon)

						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy, a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr800 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null", mycon)

						End If




					Case Is = 8

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr300 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr300 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr300 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null", mycon)

						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus, (select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr300 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null", mycon)

						End If




					Case Is = 5

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr600 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr600 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr600 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null", mycon)


						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr600 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null", mycon)

						End If


					Case Is = 6

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr200 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr200 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr200 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null", mycon)

						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID, (select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select a.txtApplicationCode,a.PIN,replace(FullName,'|','') FullName,Sector,b.numAmountToPay as numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab a,awbr200 b where a.txtApplicationCode = b.txtApplicationCode and txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null", mycon)

						End If



					Case Else

						If CStatus = "C" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus ,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select txtApplicationCode,PIN,replace(FullName,'|','') FullName,Sector,numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab where txtEnpowerExtractBatch = @batch and dteChecked is null and dteVerified is null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "V" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus ,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select txtApplicationCode,PIN,replace(FullName,'|','') FullName,Sector,numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab where txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "F" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus ,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select txtApplicationCode,PIN,replace(FullName,'|','') FullName,Sector,numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab where txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is null ", mycon)

						ElseIf CStatus = "A" Then

							MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select (select fkiAppTypeId from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) appTypeID,(select txtControlCheckedStatus from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtControlCheckedStatus ,(select txtPIN from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) PIN, (select txtFullName  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) FullName, (select txtSector  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) Sector,(select IsControlChecked  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) IsAppControlChecked,(select txtControlCheckedBy  from tblMemberApplication where txtApplicationCode  = a.txtApplicationCode) txtAppControlCheckedBy,a.* from [dbo].[tblApplicationApprovalPayee] a where a.txtStatus = 'E') select txtApplicationCode,PIN,replace(FullName,'|','') FullName,Sector,numApproved,dteValueDate,dteStartPeriod,dteEndPeriod,txtCreatedBy,txtControlCheckedBy,txtControlVerifiedBy,txtControlAuthorisedBy,dteChecked,dteVerified,dteAuthorised,IsAppControlChecked,txtAppControlCheckedBy,txtControlCheckedStatus from tab where txtEnpowerExtractBatch = @batch and dteChecked is not null and dteVerified is not null and dteAuthorised is not null ", mycon)

						End If


				End Select

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batch", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@batch").Value = batch

			End If

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApproval")
			dtUser = dsUser.Tables("MemberApproval")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function






	Public Function getApplicationBatches(sdate As Date, edate As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter
		Dim mycon As New SqlClient.SqlConnection

		Try
			mycon = db.getConnection("PaymentModule")
			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from tblSPLog where cast(dteGenerated as date) between @date1 and @date2 order by txtBatchNo asc", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = sdate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = edate

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblSPLog")
			dtUser = dsUser.Tables("tblSPLog")
			mycon.Close()

			Return dtUser

		Catch ex As Exception

		End Try
		Return Nothing



	End Function

	Public Function PMgetPencomApprovalBatchApplication(batchNo As String, rtype As Char, typeID As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try
			'P=pending,F= for confirmation, C =Confirmed, E= Exported to Enpower

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If rtype = "F" Then

				Select Case typeID

					Case Is = 3

						'numCalculatedArrears
						'MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] [recommended-lumpsum],	c.[numApprovedPension] [monthly-programed-drawndown],((DATEDIFF(month,dteDOR,DATEADD(year,50,c.[birth-date]))) * c.[monthly-programed-drawndown]) as Arrears,c.numLumpSumToPay as LumpSumToPay	,c.numPensionToPay as PensionToPay	,c.numApprovedArrears as ArrearsToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr100 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] [recommended-lumpsum],	c.[numApprovedPension] [monthly-programed-drawndown],c.numCalculatedArrears as Arrears,c.numLumpSumToPay as LumpSumToPay	,c.numPensionToPay as PensionToPay	,c.numApprovedArrears as ArrearsToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr100 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 15

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.numAmountToPay,c.[twentyfive-percent-rsa-balance]) as ApprovedAmount,	isnull(c.numAmountToPay,c.[twentyfive-percent-rsa-balance]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr500 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount,	isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr400 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount,	isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr400 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount-requested-under-nsitf-from-rsa]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount-requested-under-nsitf-from-rsa]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr200 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[total-rsa-balance]) ApprovedAmount,	isnull(c.numAmountToPay,c.[total-rsa-balance]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr600 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc ", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount-payable-net-tax]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount-payable-net-tax]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr800 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount,	isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr300 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbrEEP c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Else

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount from dbo.tblMemberApplication a, tblApplicationApprovalPayee b where a.txtApplicationCode = b.txtApplicationCode and isnull(b.txtstatus,'P') = 'F' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)


				End Select



			ElseIf rtype = "A" Then

				Select Case typeID

					Case Is = 3

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] [recommended-lumpsum],	c.[numApprovedPension] [monthly-programed-drawndown],c.numCalculatedArrears as Arrears,c.numLumpSumToPay as LumpSumToPay	,c.numPensionToPay as PensionToPay	,c.numApprovedArrears as ArrearsToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr100 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)


					Case Is = 4

						'	MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay, c.premium as Premium, cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) - [rsa-balance]) as decimal(18,2)) as Increment,[rsa-balance] as RSABalance from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)


						MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay, c.premium as Premium, cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) - [rsa-balance]) as decimal(18,2)) as Increment,[rsa-balance] as RSABalance, cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))) as decimal(18,2)) as BalAsToday,b.pkiApprovalPayeeID,b.intOrderID from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo and lumpsum = 1 union all select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay, c.premium as Premium, (cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) - [rsa-balance]) as decimal(18,2))) + lumpsum as Increment,[rsa-balance] as RSABalance, cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))) as decimal(18,2)) as BalAsToday, b.pkiApprovalPayeeID,b.intOrderID from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo and lumpsum != 1) select * from tab order by pkiApprovalPayeeID,intOrderID asc", mycon)












						'[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))

						'isnull((select MandatoryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))),0) as MandatoryRF



					Case Is = 15

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)


					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.numAmountToPay,c.[twentyfive-percent-rsa-balance]) as ApprovedAmount,	isnull(c.numAmountToPay,c.[twentyfive-percent-rsa-balance]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr500 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount, isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr400 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount, isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr400 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[total-rsa-balance]) ApprovedAmount,	isnull(c.numAmountToPay,c.[total-rsa-balance]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr600 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc ", mycon)

					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount-requested-under-nsitf-from-rsa]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount-requested-under-nsitf-from-rsa]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr200 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)


					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount-payable-net-tax]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount-payable-net-tax]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr800 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount,	isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr300 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbrEEP c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)


					Case Else


						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount from dbo.tblMemberApplication a, tblApplicationApprovalPayee b where a.txtApplicationCode = b.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

				End Select


			ElseIf rtype = "C" Then

				Select Case typeID


					Case Is = 3

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] [recommended-lumpsum],	c.[numApprovedPension] [monthly-programed-drawndown],c.numCalculatedArrears as Arrears,c.numLumpSumToPay as LumpSumToPay	,c.numPensionToPay as PensionToPay	,c.numApprovedArrears as ArrearsToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr100 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 15

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,c.[numApprovedLumpSum] lumpsum,	c.[numApprovedAnnuity] [monthly-annuity],c.numLumpSumToPay as LumpSumToPay	,c.numAnnuityToPay as AnnuityToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,c.[twentyfive-percent-rsa-balance] as numApproved,isnull(c.numAmountToPay,0) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr500 c  where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and b.txtstatus = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,c.[enbloc-payment] ApprovedAmount, isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr400 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,c.[enbloc-payment] ApprovedAmount, isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr400 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[total-rsa-balance]) ApprovedAmount,	isnull(c.numAmountToPay,c.[total-rsa-balance]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr600 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc ", mycon)

					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount-requested-under-nsitf-from-rsa]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount-requested-under-nsitf-from-rsa]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr200 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount-payable-net-tax]) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount-payable-net-tax]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr800 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[enbloc-payment]) ApprovedAmount,	isnull(c.numAmountToPay,c.[enbloc-payment]) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr300 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved, isnull(b.numPayingAmount,0) numPayingAmount,isnull(c.[numApprovedAmount],c.[amount] ) ApprovedAmount,	isnull(c.numAmountToPay,c.[amount] ) AmountToPay from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbrEEP c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

					Case Else

						MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode,txtPIN,	replace(txtFullName,'|','') txtFullName,	txtEmployerName, dteApplicationDate,(select dteValueDate from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode ) as ValueDate,isnull(b.txtstatus,'P') as txtstatus,b.numApproved,isnull(b.numPayingAmount,0) numPayingAmount from dbo.tblMemberApplication a, tblApplicationApprovalPayee b where a.txtApplicationCode = b.txtApplicationCode and b.txtstatus = 'C' and a.txtPencomBatch = @batchNo order by b.pkiApprovalPayeeID,b.intOrderID asc", mycon)

				End Select

			End If


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batchNo", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@batchNo").Value = batchNo

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
		Return dtUser
	End Function
	'converting last use pwd into hash components
	Public Function PMGetHash(pwd As String) As String
		Dim bytedwp() As Byte, hash As String = ""
		Try

			bytedwp = Encoding.UTF8.GetBytes(pwd)
			hash = Convert.ToBase64String(MD5.Create.ComputeHash(bytedwp))

		Catch ex As Exception

		End Try
		Return LTrim(RTrim(hash))


	End Function

	Public Function PMgetUserHashPWD(ByVal userName As String) As String

		Dim conn As New DbConnection
		Dim Mycommand As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable

		Try

			Mycommand.CommandText = "select isnull(LastUsedwpCode,'') as PWD from tblUsers where UserName = '" & RTrim(LTrim(userName)) & "'"
			Mycommand.CommandType = CommandType.Text
			Mycommand.Connection = conn.getConnection("PaymentModule")
			daUser.SelectCommand = Mycommand
			daUser.Fill(dsUser, "tblusers")
			dtUser = dsUser.Tables("tblusers")

			If (dtUser.Rows.Count) > 0 Then

				Return dtUser.Rows(0).Item(0).ToString

			Else
				Return ""

			End If


		Catch ex As Exception
			MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error conection!!")
		Finally
			Mycommand.Dispose()

		End Try
		Return True


	End Function

	'updating user table with the last use pwd
	Public Sub PMUpdateUserLogin(uName As String, pwd As String)

		Dim conn As New DbConnection
		Dim MyCommand As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter

		Try

			MyCommand.CommandText = "update tblusers set LastUsedwpCode = '" & pwd & "' where [UserName] = '" & RTrim(LTrim(uName)) & "'"
			MyCommand.CommandType = CommandType.Text
			MyCommand.Connection = conn.getConnection("PaymentModule")
			MyCommand.ExecuteNonQuery()
			MyCommand.Dispose()

		Catch ex As Exception
			MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error conection!!")
		End Try

	End Sub
	'inserting a new user in the user table
	Public Sub PMInsertNewUserLogin(uName As String, fullName As String, pwd As String)

		Dim conn As New DbConnection
		Dim MyCommand As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter

		Try

			MyCommand.CommandText = "insert into tblusers ([UserName],[AccessLevel],[AccessStatus],[fkiRoleID],[FullName],LastUsedwpCode) VALUES ('" & RTrim(LTrim(uName)) & "', '0','0','1','" & RTrim(LTrim(fullName)) & "','" & pwd & "' ) "
			MyCommand.CommandType = CommandType.Text
			MyCommand.Connection = conn.getConnection("PaymentModule")
			MyCommand.ExecuteNonQuery()
			MyCommand.Dispose()
		Catch ex As Exception
			MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error conection!!")
		End Try



	End Sub
	'checking if user already exists in the user table
	Public Function PMCheckUserIn(ByVal userName As String) As DataTable

		Dim cmdUser As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Try
			Dim MyCommand As SqlCommand = New SqlCommand()
			MyCommand.CommandText = "select * from tblusers  where UserName = '" & userName & "'"
			MyCommand.CommandType = CommandType.Text
			MyCommand.Connection = mycon
			daUser.SelectCommand = MyCommand
			daUser.Fill(dsUser, "PMUsers")
			dtUser = dsUser.Tables("PMUsers")
			MyCommand.Dispose()

		Catch ex As Exception
			MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error conection!!")
		End Try

		Return dtUser

	End Function
	'checking if the user a;lready has access
	Public Function PMCheckUserApplicationAccess(ByVal userName As String) As Boolean

		Dim conn As New DbConnection
		Dim Mycommand As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable

		Try

			Mycommand.CommandText = "select IsActive as Access from tblUsers where UserName = '" & RTrim(LTrim(userName)) & "'"
			Mycommand.CommandType = CommandType.Text
			Mycommand.Connection = conn.getConnection("PaymentModule")
			daUser.SelectCommand = Mycommand
			daUser.Fill(dsUser, "tblusers")
			dtUser = dsUser.Tables("tblusers")

			If CInt(dtUser.Rows(0).Item(0)) = 0 Then

				Return False
				Exit Function

			ElseIf CInt(dtUser.Rows(0).Item(0)) = 1 Then


			End If


		Catch ex As Exception
			MsgBox("Error: " & ex.Source & ": " & ex.Message, MsgBoxStyle.OkOnly, "Error conection!!")
		Finally
			Mycommand.Dispose()

		End Try
		Return True


	End Function


	Public Function PMgetApplicationSummaryDetails(date1 As Date, date2 As Date, type As Char, UName As String) As DataTable

		'dashboard Summary detail Type definition
		'N ------ All application with incomplete documentation
		'C ------ All application with complete documentation but yet to be processed
		'P ------ All application with complete documentation and already being process or processed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If type = "C" Then

				'retrieving the rows of application with complete documentation for the dashboard
				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes,txtStatus,dteApplicationDate from tblMemberApplication where IsDocCompleted = 1 and dteDocumentCompleted is not null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtcreatedby = '" & UName & "' ", mycon)

			ElseIf type = "N" Then

				'retrieving the rows of application with incomplete documentation for the dashboard
				' MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes from tblMemberApplication where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 ", mycon)

				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes,txtStatus,txtCreatedby,txtApplicationOffice,dteApplicationDate from tblMemberApplication where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtcreatedby = '" & UName & "' ", mycon)

			ElseIf type = "P" Then

				'retrieving the rows of application already being processing or processed for the dashboard
				' MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes from tblMemberApplication where IsDocCompleted = 1 and dteDocumentCompleted is not null and txtStatus != 'Documentation' and txtStatus !='Complete Document' and dteApplicationDate between @date1 and @date2 ", mycon)

				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes,txtStatus,dteApplicationDate from tblMemberApplication where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0  and txtcreatedby = '" & UName & "'", mycon)

			Else

			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try

		Return dtUser
	End Function


	Public Function PMgetApplicationSummaryDetails(date1 As Date, date2 As Date, type As Char) As DataTable

		'dashboard Summary detail Type definition
		'N ------ All application with incomplete documentation
		'C ------ All application with complete documentation but yet to be processed
		'P ------ All application with complete documentation and already being process or processed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If type = "C" Then

				'retrieving the rows of application with complete documentation for the dashboard
				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes,txtStatus,txtCreatedby,txtApplicationOffice,dteApplicationDate from tblMemberApplication where IsDocCompleted = 1 and dteDocumentCompleted is not null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 ", mycon)

			ElseIf type = "N" Then

				'retrieving the rows of application with incomplete documentation for the dashboard
				' MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes from tblMemberApplication where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 ", mycon)

				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes,txtStatus,txtCreatedby,txtApplicationOffice,dteApplicationDate from tblMemberApplication where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 ", mycon)

			ElseIf type = "P" Then

				'retrieving the rows of application already being processing or processed for the dashboard
				' MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes from tblMemberApplication where IsDocCompleted = 1 and dteDocumentCompleted is not null and txtStatus != 'Documentation' and txtStatus !='Complete Document' and dteApplicationDate between @date1 and @date2 ", mycon)

				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtApplicationCode,txtPIN,replace(txtFullName,'|','') as txtFullName,txtEmployerName,(select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId ) ApplicationTypes,txtStatus,txtCreatedby,txtApplicationOffice,dteApplicationDate from tblMemberApplication where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 ", mycon)

			Else

			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try

		Return dtUser
	End Function
	Public Sub PMCleanDuplicateDocument(AppCode As String)

		'myComm.CommandText = "sp_pm_CleanUploadedApplicationDocuments"
		'Command.CommandType = CommandType.StoredProcedure
		'Command.Parameters.Add(New SqlClient.SqlParameter("@txtapplicationcode", SqlDbType.VarChar))
		'Command.Parameters("@txtapplicationcode").Value = AppDetail.ApplicationID
		'myComm.ExecuteNonQuery()

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter


			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_CleanUploadedApplicationDocuments", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtapplicationcode", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtapplicationcode").Value = AppCode


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationLifeCycle")
			dtUser = dsUser.Tables("ApplicationLifeCycle")

			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try



	End Sub
	Public Function PMgetApplicationLifeCycle(startDate As Date, endDate As Date, AppType As Integer) As DataTable


		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter


			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_updatePaidApplication", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
			MyDataAdapter.SelectCommand.ExecuteNonQuery()


			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_applicationlifeCycle", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@startDate").Value = startDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@endDate").Value = endDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@appTypeID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@appTypeID").Value = AppType

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationLifeCycle")
			dtUser = dsUser.Tables("ApplicationLifeCycle")


			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function

	Public Function PMgetApplicationSummary(startDate As Date, endDate As Date, AppType As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_applicationreportsummary", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@startDate").Value = startDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@endDate").Value = endDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@reportType", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@reportType").Value = AppType

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationLifeCycle")
			dtUser = dsUser.Tables("ApplicationLifeCycle")

			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function

	Public Function PMgetApplicationByStatus(date1 As Date, date2 As Date, AppType As Integer, status As String, isSentToPencomBased As Integer, prev As Integer) As DataTable


		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter


			'If AppType = 0 And status = "ALL" And coverage = 0 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 order by txtapplicationCode asc,dteApplicationDate asc ", mycon)
			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'ElseIf AppType = 0 And status = "ALL" And coverage = 1 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc,dteApplicationDate asc ", mycon)
			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'ElseIf AppType = 0 And status = "ALL" And prev = 1 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate < @date1 and isnull(IsDeactivated,0) = 0 and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc,dteApplicationDate asc ", mycon)
			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'ElseIf AppType = 0 And status <> "ALL" And coverage = 0 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus order by txtapplicationCode asc,dteApplicationDate asc ", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			'	MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			'ElseIf AppType = 0 And status <> "ALL" And coverage = 1 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc,dteApplicationDate asc ", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			'	MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status


			'ElseIf AppType = 0 And status <> "ALL" And prev = 1 Then


			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate < @date1 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc,dteApplicationDate asc ", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			'	MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status




			'ElseIf AppType <> 0 And status = "ALL" And coverage = 0 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0  and fkiAppTypeId = @fkiAppTypeId order by txtapplicationCode asc,dteApplicationDate asc", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
			'	MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType

			'ElseIf AppType <> 0 And status = "ALL" And coverage = 1 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0  and fkiAppTypeId = @fkiAppTypeId and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc,dteApplicationDate asc", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
			'	MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType


			'ElseIf AppType <> 0 And status = "ALL" And prev = 1 Then


			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate < @date1 and isnull(IsDeactivated,0) = 0  and fkiAppTypeId = @fkiAppTypeId and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc,dteApplicationDate asc", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
			'	MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType



			'ElseIf AppType <> 0 And status <> "ALL" And coverage = 0 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and fkiAppTypeId = @fkiAppTypeId order by txtapplicationCode asc, dteApplicationDate asc", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			'	MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
			'	MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType

			'ElseIf AppType <> 0 And status <> "ALL" And coverage = 1 And prev = 0 Then

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and fkiAppTypeId = @fkiAppTypeId and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc, dteApplicationDate asc", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			'	MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
			'	MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType

			'ElseIf AppType <> 0 And status <> "ALL" And prev = 1 Then


			'	MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus,(select top 1 (txtdocumentSource) txtdocumentSource from tblMemberDocument where txtApplicationCode =  txtApplicationCode ) txtdocumentSource from tblMemberApplication a where dteApplicationDate < @date1 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and fkiAppTypeId = @fkiAppTypeId and dteProcessing between @date1 and @date2 and dteSentToPencom is not null order by txtapplicationCode asc, dteApplicationDate asc", mycon)

			'	MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			'	MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			'	MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
			'	MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType


			'End If

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			'MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			'MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2



			If AppType = 0 And status = "ALL" Then

				'MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 order by txtapplicationCode asc,dteApplicationDate asc ", mycon)

				If isSentToPencomBased = 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin , txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus , (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],dteApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate,a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteApplicationDate between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				ElseIf isSentToPencomBased = 1 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin , txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus , (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],dteApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate,a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteSendtoPencom between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				End If

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			ElseIf AppType = 0 And status <> "ALL" Then

				'MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus order by txtapplicationCode asc,dteApplicationDate asc ", mycon)

				If isSentToPencomBased = 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin , txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus , (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],dteApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate,a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteApplicationDate between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 and txtStatus = @txtStatus order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				ElseIf isSentToPencomBased = 1 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin , txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus , (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],dteApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate,a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteSendtoPencom between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 and txtStatus = @txtStatus order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				End If



				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			ElseIf AppType <> 0 And status = "ALL" Then

				'MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0  and fkiAppTypeId = @fkiAppTypeId order by txtapplicationCode asc,dteApplicationDate asc", mycon)


				If isSentToPencomBased = 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin , txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus , (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],dteApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate, a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteApplicationDate between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 and fkiAppTypeId = @fkiAppTypeId order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				ElseIf isSentToPencomBased = 1 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin , txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus , (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],dteApplicationDate ,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate, a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteSendtoPencom between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 and fkiAppTypeId = @fkiAppTypeId order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				End If



				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType

			ElseIf AppType <> 0 And status <> "ALL" Then

				'MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,txtStatus from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and fkiAppTypeId = @fkiAppTypeId order by txtapplicationCode asc, dteApplicationDate asc", mycon)


				If isSentToPencomBased = 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin PIN, txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus [Status], (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],[dteApplicationDate] ApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate,a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteApplicationDate between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 and txtStatus = @txtStatus and fkiAppTypeId = @fkiAppTypeId order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				ElseIf isSentToPencomBased = 1 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select (select Value from enpowerv4..titles where Titleid = b.Titleid) Title,replace(txtfullname,'|','') Fullname,txtpin PIN, txtEmployerName Employer,(case when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.fkiemployerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,a.txtApplicationCode [Application No],(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType,ISNULL(b.Phone,b.Mobile) [Contact Phone]   ,txtStatus [Status], (case when (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.fkiMemberID and ContributionTypeID in (11,12,13,14)) > 0 then 'Funded' else 'Un-Funded' end) [Accrued Right Status], (case when a.IsDocCompleted = 0 then 'Incomplete Document' else 'Complete Document' end ) [Document Status],(select top 1 REPLACE(txtComment,',','')  from [dbo].[tblApplicationComments] where dteCreated = (select max(dteCreated) from tblApplicationComments where txtApplicationCode = a.txtApplicationCode) and txtApplicationCode = a.txtApplicationCode) Comments,(case when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 1 then 'Verified' when (select isVerified from tblMemberDocument where txtApplicationCode =  a.txtApplicationCode and fkiDocumentTypeID = 49) = 0 then 'Not Verified' else '' end) [DB-LOA Verification status],[dteApplicationDate] ApplicationDate,dteProcessing [Reviewed Date],dteSendtoPencom as [Sent to Pencom],(select dteAcknowledgment from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [Approval acknowlodgement date],(select txtRefNo  from tblApplicationApprovals where txtRefNo = a.txtPencomBatch) [PenCom approval Ref],a.dteDOB DOB,a.dteDOR DOR,b.FirstEmploymentDate DOF, (select top 1 txtdocumentSource  from tblMemberDocument where txtApplicationCode = a.txtApplicationCode) [Document Source],b.RegistrationDate,a.IsControlChecked,a.txtControlCheckedStatus from tblMemberApplication a, enpowerv4..employee b where b.EmployeeID = a.fkiMemberID  and dteSendtoPencom between @date1 and @date2 and isnull(a.IsDeactivated,0) = 0 and txtStatus = @txtStatus and fkiAppTypeId = @fkiAppTypeId order by a.txtapplicationCode asc,dteApplicationDate asc", mycon)

				End If


				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = AppType

			End If

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function

	Public Function PMgetApplicationByStatus(date1 As Date, date2 As Date, IsSummary As Boolean, status As String, UName As String) As DataTable

		'fetchtype definition
		'A ------ All record base on application type
		'F ------ Few records base on application type that are yet to be confirmed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If UName = "" Then

				If IsSummary = True Then
					'retrieving the summary(all,complete document and processing) application log by date
					MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) [All],(select count(*) from tblMemberApplication a where txtStatus = @txtStatus and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) StatusApplications,dteApplicationDate from [dbo].[tblMemberApplication] b where dteApplicationDate between @date1 and @date2 and pkiMemberApplicationID = b.pkiMemberApplicationID and isnull(IsDeactivated,0) = 0", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				ElseIf IsSummary = False Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApplicationTypes,txtStatus,dteApplicationDate from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus", mycon)


					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				End If

			ElseIf UName <> "" Then

				If IsSummary = True Then
					'retrieving the summary(all,complete document and processing) application log by date
					MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) [All],(select count(*) from tblMemberApplication a where txtStatus = @txtStatus and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtCreatedBy ='" & UName & "') StatusApplications,dteApplicationDate from [dbo].[tblMemberApplication] b where dteApplicationDate between @date1 and @date2 and pkiMemberApplicationID = b.pkiMemberApplicationID and isnull(IsDeactivated,0) = 0 and txtCreatedBy ='" & UName & "'", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				ElseIf IsSummary = False Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApplicationTypes,txtStatus,dteApplicationDate from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus and txtCreatedBy ='" & UName & "'", mycon)


					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				End If

			End If



			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function


	Public Function PMgetApplicationByStatus(date1 As Date, date2 As Date, IsSummary As Boolean, status As String) As DataTable

		'fetchtype definition
		'A ------ All record base on application type
		'F ------ Few records base on application type that are yet to be confirmed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If IsSummary = True Then
				'retrieving the summary(all,complete document and processing) application log by date
				MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) [All],(select count(*) from tblMemberApplication a where txtStatus = @txtStatus and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) StatusApplications from [dbo].[tblMemberApplication] b where dteApplicationDate between @date1 and @date2 and pkiMemberApplicationID = b.pkiMemberApplicationID and isnull(IsDeactivated,0) = 0", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			ElseIf IsSummary = False Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') txtfullname,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApplicationTypes,txtStatus from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtStatus = @txtStatus", mycon)


				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			End If

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function

	Public Function PMgetApplicationByDate(date1 As Date, date2 As Date, IsSummary As Boolean, AppType As Integer, UName As String) As DataTable

		'fetchtype definition
		'A ------ All record base on application type
		'F ------ Few records base on application type that are yet to be confirmed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If UName = "" Then
				'retrieving all the application by date
				If IsSummary = True Then
					'retrieving the summary(all,complete document and processing) application log by date
					MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) [All],(select count(*) from tblMemberApplication a where IsDocCompleted = 1 and dteDocumentCompleted is not null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) completeDocument,(select count(*) from tblMemberApplication a where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) UncompleteDocument,(select count(*) from tblMemberApplication a where IsDocCompleted = 1 and dteDocumentCompleted is not null and txtStatus != 'Documentation' and txtStatus !='Complete Document' and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) processing from [dbo].[tblMemberApplication] b where dteApplicationDate between @date1 and @date2 and pkiMemberApplicationID = b.pkiMemberApplicationID and isnull(IsDeactivated,0) = 0", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				ElseIf IsSummary = False And AppType = 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				ElseIf IsSummary = False And AppType > 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and fkiAppTypeId = @AppType", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

					MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppType", SqlDbType.Int))
					MyDataAdapter.SelectCommand.Parameters("@AppType").Value = AppType

				End If



			ElseIf UName <> "" Then
				'retrieving all the application by date and by the user


				If IsSummary = True Then
					'retrieving the summary(all,complete document and processing) application log by date
					MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) [All],(select count(*) from tblMemberApplication a where IsDocCompleted = 1 and dteDocumentCompleted is not null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtCreatedBy = '" & UName & "') completeDocument,(select count(*) from tblMemberApplication a where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtCreatedBy = '" & UName & "') UncompleteDocument,(select count(*) from tblMemberApplication a where IsDocCompleted = 1 and dteDocumentCompleted is not null and txtStatus != 'Documentation' and txtStatus !='Complete Document' and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtCreatedBy = '" & UName & "') processing from [dbo].[tblMemberApplication] b where dteApplicationDate between @date1 and @date2 and pkiMemberApplicationID = b.pkiMemberApplicationID and isnull(IsDeactivated,0) = 0 and txtCreatedBy = '" & UName & "'", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				ElseIf IsSummary = False And AppType = 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtCreatedBy = '" & UName & "'", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				ElseIf IsSummary = False And AppType > 0 Then

					MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and txtCreatedBy = '" & UName & "' and fkiAppTypeId = @AppType", mycon)

					MyDataAdapter.SelectCommand.CommandType = CommandType.Text

					MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppType", SqlDbType.Int))
					MyDataAdapter.SelectCommand.Parameters("@AppType").Value = AppType

				End If



			End If






			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function
	'retrieves all benefit applications per splog batches
	Public Function PMgetApplicationByDate(SPBatch As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		Try

			mycon = db.getConnection("PaymentModule")
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			'retrieving the application log by splog batch number
			MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId= a.fkiAppTypeId) ApprovalType from tblMemberApplication a, tblSPLog b where a.txtSPBatchNo = b.txtBatchNo and isnull(IsDeactivated,0) = 0 and b.txtBatchNo = @SPBatch ", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@SPBatch", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@SPBatch").Value = SPBatch
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblSPLog")
			dtUser = dsUser.Tables("tblSPLog")
			mycon.Close()
			Return dtUser


		Catch ex As Exception

		End Try



	End Function

	Public Function PMgetApplicationByDate(date1 As Date, date2 As Date, IsSummary As Boolean, AppType As Integer) As DataTable

		'fetchtype definition
		'A ------ All record base on application type
		'F ------ Few records base on application type that are yet to be confirmed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If IsSummary = True Then
				'retrieving the summary(all,complete document and processing) application log by date
				MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) [All],(select count(*) from tblMemberApplication a where IsDocCompleted = 1 and dteDocumentCompleted is not null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) completeDocument,(select count(*) from tblMemberApplication a where IsDocCompleted = 0 and dteDocumentCompleted is null and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) UncompleteDocument,(select count(*) from tblMemberApplication a where IsDocCompleted = 1 and dteDocumentCompleted is not null and txtStatus != 'Documentation' and txtStatus !='Complete Document' and dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0) processing from [dbo].[tblMemberApplication] b where dteApplicationDate between @date1 and @date2 and pkiMemberApplicationID = b.pkiMemberApplicationID and isnull(IsDeactivated,0) = 0", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			ElseIf IsSummary = False And AppType = 0 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			ElseIf IsSummary = False And AppType > 0 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,*,(select txtDescription from tblApplicationType where pkiAppTypeId=fkiAppTypeId) ApprovalType from tblMemberApplication a where dteApplicationDate between @date1 and @date2 and isnull(IsDeactivated,0) = 0 and fkiAppTypeId = @AppType", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppType", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@AppType").Value = AppType

			End If


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception

			'MsgBox("" & Ex.Message)

		Finally

		End Try
		Return dtUser
	End Function


	Public Function PMgetPencomApprovalsConfirmation(AppType As Integer, FetchType As Char) As DataTable

		'fetchtype definition
		'A ------ All record base on application type
		'F ------ Few records base on application type that are yet to be confirmed

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If FetchType = "F" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtRefNo,	dteApproval,	dteAcknowledgment,	(isnull(numApprovalAmount,0) + isnull(numApprovalLumpSum,0) + isnull(numApprovalPension,0) + isnull(numApprovalAnnuity,0)) numApprovalAmount,dteConfirmed from tblApplicationApprovals where fkiAppTypeID = @AppID and dteConfirmed is null and exists (select * from [dbo].[tblApplicationApprovalPayee] where txtPencomBatch =  txtRefNo and txtStatus <> 'E') order by 1 asc", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppID", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@AppID").Value = AppType

			ElseIf FetchType = "A" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtRefNo,	dteApproval,	dteAcknowledgment,	(isnull(numApprovalAmount,0) + isnull(numApprovalLumpSum,0) + isnull(numApprovalPension,0) + isnull(numApprovalAnnuity,0)) numApprovalAmount,dteConfirmed from tblApplicationApprovals a where fkiAppTypeID = @AppID and exists (select * from [dbo].[tblApplicationApprovalPayee] where txtPencomBatch =  txtRefNo and txtStatus <> 'E')  order by 1 asc", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppID", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@AppID").Value = AppType

			End If


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PencomApprovals")
			dtUser = dsUser.Tables("PencomApprovals")
			mycon.Close()

		Catch Ex As Exception
			'  MsgBox("" & Ex.Message)
		Finally

		End Try
		Return dtUser
	End Function

	Public Function PMgetSubmittedDocument(PIn As String, AppID As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try
			'txtApplicationCode
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			'	MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select b.*,c.txtApplicationCode,(select FullName from tblUsers where UserName = c.txtCreatedBy) txtCreatedBy from dbo.tblAppDocumentType a, dbo.tblDocumentType b, tblMemberApplication c where a.fkidocumenttypeid = b.pkidocumenttypeid and  a.apptypeid = c.fkiAppTypeid and a.txtsector = replace(c.txtsector,'No Sector','Private') and c.txtpin = @PIn and c.txtApplicationCode = @AppID and ltrim(rtrim((b.txtDocumentName))) != 'Others') select *,(select dteReceived from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) DateRecived,(select txtDocumentPath from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) DocumentPath,(select isnull(isVerified,0) from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) isVerified from tab a union all select 0 pkiDocumentTypeID,txtDescription,txtApplicationCode,(select FullName from tblUsers where UserName = a.txtRecievedBy ) txtCreatedBy,dteRecieved as DateRecived,txtDocPath as DocumentPath,isVerified from tblAdhocApplicationDocuments a where txtApplicationCode = @AppID", mycon)



			MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select b.*,c.txtApplicationCode,(select FullName from tblUsers where UserName = c.txtCreatedBy) txtCreatedBy from dbo.tblAppDocumentType a, dbo.tblDocumentType b, tblMemberApplication c where a.fkidocumenttypeid = b.pkidocumenttypeid and  a.apptypeid = c.fkiAppTypeid and a.txtsector = replace(c.txtsector,'No Sector','Private') and c.txtpin = @PIn and c.txtApplicationCode = @AppID and ltrim(rtrim((b.txtDocumentName))) != 'Others') select *,(select dteReceived from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) DateRecived,(select txtDocumentPath from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) DocumentPath,(select isnull(isVerified,0) from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) isVerified,(select txtDMSDocumentID from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) DocumentID,(select txtDMSDocumentExt from tblMemberDocument where fkiDocumentTypeID = pkiDocumentTypeID and fkiMemberApplicationID = a.txtApplicationCode and IsDeactivated = 0) DocumentExtension  from tab a union all select 0 pkiDocumentTypeID,txtDescription,txtApplicationCode,(select FullName from tblUsers where UserName = a.txtRecievedBy ) txtCreatedBy,dteRecieved as DateRecived,txtDocPath as DocumentPath,isVerified,txtDMSDocumentID DocumentID,txtDMSDocumentExt DocumentExtension from tblAdhocApplicationDocuments a where txtApplicationCode = @AppID", mycon)




			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIn", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIn").Value = PIn

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppID", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@AppID").Value = AppID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

		Catch Ex As Exception
			'  MsgBox("" & Ex.Message)
		Finally

		End Try
		Return dtUser
	End Function

	Public Sub PMUpdatePencomApprovedAmount(ApptypeID As Integer, ApprovedAmount As Decimal, applicationCode As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update awbr500 set numApprovedAmount = @ApprovedAmount where txtApplicationCode = @txtApplicationCode"

			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			sqlTran.Commit()

		Catch ex As Exception
			'  MsgBox("" & ex.Message)
		End Try

	End Sub


	Public Sub PMGenerateApprovalSchedule(lstApprovalSchs As List(Of ApplicationDetail))

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer, lstApplicationDetails As New List(Of ApplicationDetail)

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran


			Do While i < lstApprovalSchs.Count


				myComm.CommandText = "update tblMemberApplication set isScheduleGenerated = '" & DateTime.Parse(lstApprovalSchs(i).IsScheduleGenerated).ToString("yyyy-MM-dd") & "' and dteScheduleGenerated = '" & DateTime.Parse(lstApprovalSchs(i).DateConfirmed).ToString("yyyy-MM-dd") & "', txtStatus ='Approved/Processed' where txtApplicationCode = '" & lstApprovalSchs(i).ApplicationID & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				myComm.CommandText = "select txtFullName,txtPIN,txtSector,'RSA' as platform, tblMemberApplication set isScheduleGenerated = '" & DateTime.Parse(lstApprovalSchs(i).IsScheduleGenerated).ToString("yyyy-MM-dd") & "' and dteScheduleGenerated = '" & DateTime.Parse(lstApprovalSchs(i).DateConfirmed).ToString("yyyy-MM-dd") & "', txtStatus ='Approved/Processed' where txtApplicationCode = '" & lstApprovalSchs(i).ApplicationID & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()



				i = i + 1

			Loop

			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMGenerateSchedule(lstApprovals As List(Of PencomApprovalDetails))

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran


			Do While i < lstApprovals.Count

				myComm.CommandText = "update tblApplicationApprovals set dteConfirmed = '" & DateTime.Parse(lstApprovals(i).ConfirmedDate).ToString("yyyy-MM-dd") & "' ,txtConfirmedBy  = '" & lstApprovals(i).ConfirmedBy & "' where txtRefNo = '" & lstApprovals(i).PencomBatch & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()



				myComm.CommandText = "update tblMemberApplication set dteApprovalConfirmed = '" & DateTime.Parse(lstApprovals(i).ConfirmedDate).ToString("yyyy-MM-dd") & "', txtStatus ='Approved/Processing' where txtPencomBatch = '" & lstApprovals(i).PencomBatch & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

				i = i + 1

			Loop

			sqlTran.Commit()

		Catch ex As Exception
			' MsgBox("" & ex.Message)
		End Try

	End Sub


	'confirmation of the approvals recieved from pencom
	Public Sub PMConfirmPencomApproval(lstApprovals As List(Of PencomApprovalDetails))

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran


			Do While i < lstApprovals.Count




				If lstApprovals.Item(i).Status = "F" Then

					'updating each approval application for confirmation
					myComm.CommandText = "update tblApplicationApprovalPayee set txtStatus = '" & lstApprovals(i).Status & "' where txtApplicationCode = '" & lstApprovals(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					'updating the lumpSum for annuity application
					myComm.CommandText = "update c set c.numLumpSumToPay = ((cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) - [rsa-balance]) as decimal(18,2))) + lumpsum ) from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtApplicationCode = '" & lstApprovals(i).ApplicationCode & "' and lumpsum > 1"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					'updating the lumpSum for annuity application
					myComm.CommandText = "update c set c.numLumpSumToPay = (cast((Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) - [rsa-balance]) as decimal(18,2))) from dbo.tblMemberApplication a, tblApplicationApprovalPayee b,awbr700 c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and isnull(b.txtstatus,'P') <> 'E' and a.txtApplicationCode = '" & lstApprovals(i).ApplicationCode & "' and lumpsum = 1"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				ElseIf lstApprovals.Item(i).Status = "C" Then

					myComm.CommandText = "update tblApplicationApprovals set dteConfirmed = '" & DateTime.Parse(lstApprovals(i).ConfirmedDate).ToString("yyyy-MM-dd") & "' ,txtConfirmedBy  = '" & lstApprovals(i).ConfirmedBy & "' where txtRefNo = '" & lstApprovals(i).PencomBatch & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					'update the application detail with the approval confirmationdetails
					myComm.CommandText = "update tblMemberApplication set dteApprovalConfirmed = '" & DateTime.Parse(lstApprovals(i).ConfirmedDate).ToString("yyyy-MM-dd") & "', txtStatus ='Approved/Processing' where txtApplicationCode = '" & lstApprovals(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					'confirmation of each PIN on the approval schedule
					myComm.CommandText = "update tblApplicationApprovalPayee set dteConfirmed = '" & DateTime.Parse(lstApprovals(i).ConfirmedDate).ToString("yyyy-MM-dd") & "', txtConfirmedBy = '" & lstApprovals(i).ConfirmedBy & "', txtStatus = '" & lstApprovals(i).Status & "' where txtApplicationCode = '" & lstApprovals(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()
				End If





				i = i + 1

			Loop

			sqlTran.Commit()

		Catch ex As Exception
			'  MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMActivateUser(userID As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblUsers set isActive = 1 where UserName = '" & userID & "' "
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()
			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMDeleteAdhocDocument(DocumentID As Integer)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "delete from tblAdhocApplicationDocuments where intDocumentID = '" & DocumentID & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()
			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMDeactivateUser(userID As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblUsers set isActive = 0, dteDeativated = '" & DateTime.Parse(Now) & "' where UserName = '" & userID & "' "
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()
			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMRemoveAccess(roleID As Integer, moduleID As Integer, UName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "delete from tblMLAccess where fkiModuleID = '" & moduleID & "' and fkiRoleID = '" & roleID & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()
			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Function PMGetInsertedTempRMASRecord(AppCode As String) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Dim sql As String
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable

		sql = "select * from tmpAVCDetails where txtApplicationCode = @txtApplicationCode"

		MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", SqlDbType.VarChar))
		MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = AppCode
		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "tmpAVCDetails")
		dtUser = dsUser.Tables("tmpAVCDetails")


		mycon.Close()


		Return dtUser


	End Function


	Public Function PMGetInsertedRetirementRecord(AppCode As String) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Dim sql As String
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable
		Try

			'sql = "select *, (select InsurerName  from EnPowerV4..Insurer where InsurerID = a.txtInsuranceCompanyName) InsurerName from tmpPWAnnuityDetails a where txtApplicationCode = @txtApplicationCode"

			sql = "select *, isnull((select InsurerName  from EnPowerV4..Insurer where cast(InsurerID as varchar(225)) = a.txtInsuranceCompanyName),a.txtInsuranceCompanyName) InsurerName from tmpPWAnnuityDetails a where txtApplicationCode = @txtApplicationCode"

			'sql = "select * from tmpPWAnnuityDetails where txtApplicationCode = @txtApplicationCode"

			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = AppCode
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tmpAVCDetails")
			dtUser = dsUser.Tables("tmpAVCDetails")


			mycon.Close()


			Return dtUser

		Catch ex As Exception
			MsgBox("Get retirementRecord" & ex.Message)
		End Try
	End Function


	Public Function PMGetInsertedDBARecord(AppCode As String) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		Dim sql As String
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable

		sql = "select * from tmpDB where txtApplicationCode = @txtApplicationCode"

		MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text
		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", SqlDbType.VarChar))
		MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = AppCode
		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "tmpDB")
		dtUser = dsUser.Tables("tmpDB")

		mycon.Close()

		Return dtUser

	End Function



	'inserting avc details temporarily for internal confirmation and approval before sending to Pencom
	Public Sub PMInsertTempRMASRecord(avD As AVCDetails)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			myComm = mycon.CreateCommand
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm.Transaction = sqlTran

			'remove from the table if the application already exists dere
			myComm.CommandText = "delete from [dbo].[tmpAVCDetails] where [txtApplicationCode] = '" & avD.ApplicationCode & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			'inserting the application in the table temporarily
			myComm.CommandText = "INSERT INTO [dbo].[tmpAVCDetails] ([txtApplicationCode],[txtAVCProcessed],[txtNoTaxAVCProcessed],[txtAVCProcessedUnit],[txtNoTaxAVCProcessedUnit],[txtTotalAVCProcessed],[txtAvgPrice],[txtPaymentDate],[txtPaymentPrice],[txtCurrentValue],[txtTaxDeduction],[txtNetPayable])               VALUES('" & avD.ApplicationCode & "','" & avD.TaxableProcessedAVC & "','" & avD.NonTaxableProcessedAVC & "','" & avD.TaxableAVCProcessedUnit & "','" & avD.NonTaxableAVCProcessedUnit & "','" & avD.TotalProcessedAVC & "','" & avD.AveragAVCPrice & "','" & avD.AVCPaymentDate & "','" & avD.AVCPaymentUnitPrice & "','" & CDbl(avD.AVCCurrentValue) & "','" & CDbl(avD.AVCTaxDeduction) & "','" & CDbl(avD.AVCNetPayable) & "')"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()


			sqlTran.Commit()

		Catch ex As Exception

			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMInsertAccess(roleID As Integer, moduleID As Integer, UName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			myComm = mycon.CreateCommand
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm.Transaction = sqlTran
			myComm.CommandText = "insert into tblMLAccess (fkiModuleID, fkiRoleID,txtCreatedBy) values ('" & moduleID & "','" & roleID & "','" & UName & "')"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()


			sqlTran.Commit()

		Catch ex As Exception

			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMUpdateUserRole(roleID As Integer, UName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			myComm.CommandText = "update tblUsers set fkiRoleID = '" & roleID & "' where UserName = '" & UName & "'"
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()


			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub


	Public Sub PMInsertUserRole(roleName As String, isReadOnly As Boolean, UName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand



		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			If isReadOnly = True Then
				'pkiRoleID '" & roleID & "',
				myComm.CommandText = "insert into tblRoles (txtRole,blnAdd,blnEdit,blnDelete,blnUpdate,txtCreator) values ('" & roleName & "','0','0','0','0','" & UName & "') "

				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


			ElseIf isReadOnly = False Then
				'pkiRoleID '" & roleID & "',
				myComm.CommandText = "insert into tblRoles (txtRole,blnAdd,blnEdit,blnDelete,blnUpdate,txtCreator) values ('" & roleName & "','1','1','1','1','" & UName & "') "
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()
			End If


			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Function PMGetPWAnnuityDetails(ApplicationCode As String) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		mycon = db.getConnection("PaymentModule")

		Dim dt As New DataTable
		Dim dsUser As DataSet
		Dim dtUser As New DataTable
		'
		command = mycon.CreateCommand
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select *, b.fkiAppTypeId from tmpPWAnnuityDetails a,tblMemberApplication b where  a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = @ApplicationCode ", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@ApplicationCode", _
			SqlDbType.VarChar))
		MyDataAdapter.SelectCommand.Parameters("@ApplicationCode").Value = ApplicationCode

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "PWAnnuityDetails")
		dt = dsUser.Tables("PWAnnuityDetails")

		Return dt

	End Function

	Public Function PMGetDocument(DocumentID As Integer) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		mycon = db.getConnection("PaymentModule")

		Dim dt As New DataTable
		Dim dsUser As DataSet
		Dim dtUser As New DataTable
		'
		command = mycon.CreateCommand
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select * from tblDocumentType where pkiDocumentTypeID = @pkiDocumentTypeID ", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiDocumentTypeID", _
			SqlDbType.VarChar))
		MyDataAdapter.SelectCommand.Parameters("@pkiDocumentTypeID").Value = DocumentID

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "tblDocumentType")
		dt = dsUser.Tables("tblDocumentType")

		Return dt

	End Function


	Public Function PMGetDeathDetails(ApplicationCode As String) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		mycon = db.getConnection("PaymentModule")

		Dim dt As New DataTable
		Dim dsUser As DataSet
		Dim dtUser As New DataTable
		'
		command = mycon.CreateCommand
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select *, b.fkiAppTypeId from tmpDB a,tblMemberApplication b where  a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = @ApplicationCode ", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@ApplicationCode", _
			SqlDbType.VarChar))
		MyDataAdapter.SelectCommand.Parameters("@ApplicationCode").Value = ApplicationCode

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "DBDetails")
		dt = dsUser.Tables("DBDetails")

		Return dt

	End Function

	Dim encryptor, decryptor As ICryptoTransform
	Dim encoder As New UTF8Encoding()

	Public Function Encrypt(unencrypted As String) As String

		Return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)))

	End Function

	Public Function Decrypt(encrypted As String) As String

		Return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)))

	End Function

	Public Function Encrypt(buffer As Byte()) As Byte()

		Return Transform(buffer, encryptor)

	End Function

	Public Function Decrypt(buffer As Byte()) As Byte()

		Return Transform(buffer, decryptor)

	End Function

	Protected Function Transform(buffer As Byte(), transforms As ICryptoTransform) As Byte()

		Dim stream = New MemoryStream()
		Using cs As New CryptoStream(stream, transforms, CryptoStreamMode.Write)
			cs.Write(buffer, 0, buffer.Length)

		End Using
		Return stream.ToArray()

	End Function

	Public Function PMIsRMASSMSSent(reportDate As Date, logPath As String) As Boolean


		Try

			Dim db As New DbConnection

			' instatiating a connection to surePay dbase 
			Dim myConnectionPM As SqlClient.SqlConnection = db.getConnection("PaymentModule")

			Dim myCommand As New SqlClient.SqlCommand
			Dim cmdUser As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim sql As String


			sql = "select dteSentFor from tblPMRMASSMSHistory where dteSentFor = '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "'"


			daUser = New SqlClient.SqlDataAdapter(sql, myConnectionPM)
			daUser.SelectCommand.CommandType = CommandType.Text
			daUser.Fill(dsUser, "PaymentModuleSMS")
			dtUser = dsUser.Tables("PaymentModuleSMS")

			If dtUser.Rows.Count > 0 Then
				Return True
			Else
				Return False
			End If

			'Return CDate(dtUser.Rows(0).Item(0))

			myConnectionPM.Close()


		Catch ex As Exception

			Dim loger As New Global.Logger.Logger
			loger.FileSource = "Sure Pay RMAS SMS - "
			loger.FilePath = logPath
			loger.Logger(ex.StackTrace & " | " & "Location => RMAS SMS")

		Finally

		End Try
		Return False

		' Return Now.Date
	End Function


	'logging documentation SMS from payment module
	Public Sub PMPaymentModuleRMASSMSInsert(reportDate As Date, logPath As String)

		Try

			Dim db As New DbConnection
			' instatiating a connection to enpower_midas dbase where the smslog table exists on
			Dim myConnectionMIDAS As SqlClient.SqlConnection = db.getConnection("Midas")
			' instatiating a connection to payment module dbase to fetching new documentation sms
			Dim myConnectionPM As SqlClient.SqlConnection = db.getConnection("PaymentModule")

			Dim myCommand As New SqlClient.SqlCommand
			Dim cmdUser As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim sql As String = ""

			''''retrieving documentation sms from payment module dbase

			sql = "with tab as ( select pin,datesent,'awbr100' RMASFileName from [dbo].[awbr100] where datesent is not null and cast(datesent as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select pin,datesent, 'awbr200' RMASFileName from [dbo].[awbr200] where datesent is not null and cast(datesent as date)='" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select pin,[date-sent] , 'awbr300' RMASFileName from [dbo].[awbr300] where [date-sent] is not null and cast([date-sent] as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select pin,[date-sent], 'awbr400' RMASFileName  from [dbo].[awbr400] where [date-sent] is not null and cast([date-sent] as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select pin,[date-sent], 'awbr500' RMASFileName from [dbo].[awbr500] where [date-sent] is not null and cast([date-sent] as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select [pin-dba] as pin,datesent, 'awbr600' RMASFileName from [dbo].[awbr600] where datesent is not null and cast(datesent as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select pin,datesent,'awbr700' RMASFileName from [dbo].[awbr700] where datesent is not null and cast(datesent as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "' union all select pin,[date-sent],'awbr800' RMASFileName from [dbo].[awbr800] where [date-sent] is not null and cast([date-sent] as date)= '" & DateTime.Parse(reportDate).ToString("yyyy-MM-dd") & "') select rsapin,isnull(mobile,phone) as Telephone,cast(datesent as date) as ApplicationDate,RMASFileName from tab a, enpowerv4.dbo.employee b where RSAPIN = pin and not exists (select * from tblPMRMASSMSHistory where txtPIN = pin and dteSentFor = cast(datesent as date)) and isnull(mobile,phone) is not null"


			Dim j As Integer
			daUser = New SqlClient.SqlDataAdapter(sql, myConnectionPM)
			daUser.SelectCommand.CommandType = CommandType.Text
			daUser.Fill(dsUser, "PaymentModuleSMS")
			dtUser = dsUser.Tables("PaymentModuleSMS")


			Do While j < dtUser.Rows.Count

				Dim msg As String = "Please be informed that your benefit application has been forwarded to PenCom for payment approval. Payment would be made upon receipt of approval"

				cmdUser = New SqlClient.SqlCommand
				cmdUser.Connection = myConnectionMIDAS

				'inserting the documentation sms one after the other into sms log table on midas
				cmdUser.CommandText = "insert into tblsmslog (txtUserID,txtIDNo,txtDestination,txtMessage,dteSubmittedTime,txtOwnerName,txtSenderName,txtTag,dteProcessedFor,intMessageID,messageSent,numRetries) VALUES (@txtUserID,@txtIDNo,@txtDestination,@txtMessage,@dteSubmittedTime,@txtOwnerName,@txtSenderName,@txtTag,@dteProcessedFor,@intMessageID,@messageSent,@numRetries)"

				'''''adding parameter values
				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtUserID", SqlDbType.VarChar))
				cmdUser.Parameters("@txtUserID").Value = "BPD"

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@numRetries", SqlDbType.Int))
				cmdUser.Parameters("@numRetries").Value = 0

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtIDNo", SqlDbType.VarChar))
				cmdUser.Parameters("@txtIDNo").Value = dtUser.Rows(j).Item("rsapin")

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtDestination", SqlDbType.VarChar))
				cmdUser.Parameters("@txtDestination").Value = dtUser.Rows(j).Item("Telephone")

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtMessage", SqlDbType.VarChar))
				cmdUser.Parameters("@txtMessage").Value = msg

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@dteSubmittedTime", SqlDbType.DateTime))
				cmdUser.Parameters("@dteSubmittedTime").Value = Now

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtOwnerName", SqlDbType.VarChar))
				cmdUser.Parameters("@txtOwnerName").Value = "WEB-SERVICES"

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtSenderName", SqlDbType.VarChar))
				cmdUser.Parameters("@txtSenderName").Value = "LeadwayPPFA"

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtTag", SqlDbType.VarChar))
				cmdUser.Parameters("@txtTag").Value = "BPD_Documentation_SMS"

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@dteProcessedFor", SqlDbType.DateTime))
				cmdUser.Parameters("@dteProcessedFor").Value = dtUser.Rows(j).Item("ApplicationDate")

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@intMessageID", SqlDbType.Int))
				cmdUser.Parameters("@intMessageID").Value = 0

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@messageSent", SqlDbType.Int))
				cmdUser.Parameters("@messageSent").Value = 0

				cmdUser.CommandType = CommandType.Text
				cmdUser.ExecuteNonQuery()



				cmdUser = New SqlClient.SqlCommand
				cmdUser.Connection = myConnectionPM
				'inserting the document sms in an history to disallow multiple sms per application and PIN
				cmdUser.CommandText = "insert into tblPMRMASSMSHistory (txtPIN,dteSentFor,txtApprovalRMASName) VALUES (@txtPIN,@dteSentFor,@txtApprovalRMASName)"

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtPIN", SqlDbType.VarChar))
				cmdUser.Parameters("@txtPIN").Value = dtUser.Rows(j).Item("rsapin") ' "PEN000000000000" '

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@dteSentFor", SqlDbType.Date))
				cmdUser.Parameters("@dteSentFor").Value = dtUser.Rows(j).Item("ApplicationDate") 'DateTime.Parse(reportDate).ToString("yyyy-MM-dd")   '

				cmdUser.Parameters.Add(New SqlClient.SqlParameter("@txtApprovalRMASName", SqlDbType.VarChar))
				cmdUser.Parameters("@txtApprovalRMASName").Value = dtUser.Rows(j).Item("RMASFileName") '"TEST" '

				cmdUser.CommandType = CommandType.Text
				cmdUser.ExecuteNonQuery()


				If dtUser.Rows(j).Item("RMASFileName").ToString = "awbr100" Then


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr100 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where pin = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr100 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


				ElseIf dtUser.Rows(j).Item("RMASFileName").ToString = "awbr200" Then

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr200 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where pin = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr200 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()



				ElseIf dtUser.Rows(j).Item("RMASFileName").ToString = "awbr300" Then

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr300 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where pin = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr300 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()

				ElseIf dtUser.Rows(j).Item("RMASFileName").ToString = "awbr400" Then

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr400 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where pin = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr400 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


				ElseIf dtUser.Rows(j).Item("RMASFileName").ToString = "awbr500" Then

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr500 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where pin = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr500 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


				ElseIf dtUser.Rows(j).Item("RMASFileName").ToString = "awbr600" Then

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr600 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where [pin-dba] = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr600 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()


				ElseIf dtUser.Rows(j).Item("RMASFileName").ToString = "awbr700" Then

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update awbr700 set isSMSSent = 1, dteSMSSubmitted = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "' where pin = '" & dtUser.Rows(j).Item("rsapin").ToString & "'"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()

					cmdUser = New SqlClient.SqlCommand
					cmdUser.Connection = myConnectionPM
					cmdUser.CommandText = "update a set a.dtesmssubmitted = b.dtesmssubmitted from tblMemberApplication a, awbr700 b 					where a.txtApplicationCode = b.txtApplicationCode and b.dtesmssubmitted is not null"
					cmdUser.CommandType = CommandType.Text
					cmdUser.ExecuteNonQuery()

				End If


				j = j + 1

			Loop


			myConnectionMIDAS.Close()
			myConnectionPM.Close()



		Catch ex As Exception

			Dim loger As New Global.Logger.Logger
			loger.FileSource = "Sure Pay RMAS SMS - "
			loger.FilePath = logPath
			loger.Logger(ex.StackTrace & " | " & "Location => RMAS SMS")

		Finally

		End Try

	End Sub







	Public Function PMGetNewPensionEntrants(date1 As Date, date2 As Date) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""
		Dim dt As New DataTable
		Dim dsUser As DataSet
		Dim dtUser As New DataTable
		Try

			mycon = db.getConnection("PaymentModule")
			command = mycon.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtPIN PIN,REPLACE (a.txtFullName,'|','') FullName,replace(a.txtEmployerName,'|',' ') EmployerName,txtSector Sector,(select dteApproval from [dbo].[tblApplicationApprovals] where txtRefNo = a.txtPencomBatch) [DATE OF APPPROVAL]  ,isnull(numApprovedPension,0) [APPROVED PENSION],isnull(b.numPensionToPay,0) [PENSION TO PAY], ((select BankName from EnPowerV4..Bank  where BankID = a.fkiBankID  ) +' '+(select BranchName from EnPowerV4..BankBranch   where BankID = a.fkiBankID  and BankBranchID = a.fkiBranchID )) [Bank Details], a.txtAccountNo [A/C NUMBER],c.txtPaymentRemarks [Remarks] from tblMemberApplication a, awbr100 b, tblApplicationApprovalPayee c where a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = c.txtApplicationCode and a.dtePaid between @date1 and @date2 and c.txtStatus = 'E' and dteChecked is not null and dteVerified is not null and dteAuthorised is not null ", mycon)
			'and dteChecked is not null and dteVerified is not null and dteAuthorised is not null
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
				SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
				SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewEntrantSchedule")
			dt = dsUser.Tables("NewEntrantSchedule")




		Catch ex As Exception


		End Try

		Return dt

	End Function

	Public Function PMGetManagerReport(date1 As Date, date2 As Date, type As Integer) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""
		Dim dt As New DataTable
		Dim dsUser As DataSet
		Dim dtUser As New DataTable

		Try

			mycon = db.getConnection("PaymentModule")
			command = mycon.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_getManagerReport", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
				SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
				SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@type", _
				SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@type").Value = type

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ManagerDetails")
			dt = dsUser.Tables("ManagerDetails")

			Return dt

		Catch ex As Exception

		End Try
		

	End Function



	Public Function PMGetAVCDetails(txtPIN As String) As DataTable

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer

		mycon = db.getConnection("EnpowerV4")

		Dim dt As New DataTable
		Dim dsUser As DataSet
		Dim dtUser As New DataTable

		command = mycon.CreateCommand
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select avcamount,netamount,unitprice,unitvalue,(select sum(AmountFromAVC) from [dbo].[Payments] where EmployeeID = a.EmployeeID and PaymentTypeID = 7) as AVCPayment from contributions a, employee b where  a.EmployeeID = b.EmployeeID and b.RSAPIN = @PIN and avcamount > 0", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			SqlDbType.VarChar))
		MyDataAdapter.SelectCommand.Parameters("@PIN").Value = txtPIN

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "AVCDetails")
		dt = dsUser.Tables("AVCDetails")

		Return dt

	End Function




	'calculating Tax on voluntary contribution
	Public Function PMGetAVCTax(AVCAmount As Double) As Double
		Dim AVCRateCollection As New Hashtable, i As Integer, amtTax As Decimal, vatAmount As Double
		Dim taxRates() As Integer = {7, 11, 15, 19, 21, 24}

		AVCRateCollection.Add(7, 300000.0)
		AVCRateCollection.Add(11, 300000.0)
		AVCRateCollection.Add(15, 500000.0)
		AVCRateCollection.Add(19, 500000.0)
		AVCRateCollection.Add(21, 1600000.0)
		AVCRateCollection.Add(24, 3200000.0)
		'AVCRateCollection.Add()
		i = 0
		Do While i < taxRates.Count

			If i = taxRates.Count - 1 Then

				vatAmount = (CInt(taxRates(i)) / 100)
				amtTax = amtTax + (CDbl(AVCAmount) * vatAmount)

			Else
				If AVCAmount <= CDbl(AVCRateCollection.Item(CInt(taxRates(i)))) Then

					vatAmount = (CInt(taxRates(i)) / 100)
					amtTax = amtTax + (AVCAmount * vatAmount)
					i = AVCRateCollection.Count

				Else

					vatAmount = (CInt(taxRates(i)) / 100)
					amtTax = amtTax + (CDbl(AVCRateCollection.Item(CInt(taxRates(i)))) * vatAmount)
					AVCAmount = AVCAmount - CDbl(AVCRateCollection.Item(CInt(taxRates(i))))

				End If
			End If


			i = i + 1


		Loop

		Return amtTax

	End Function
	'inserting pencom approval details
	Public Function PMIsPencomApprovalExisting(pencomBatch As String, errPath As String) As Boolean

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from tblApplicationApprovals where txtRefNo = @txtRefNo", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtRefNo", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtRefNo").Value = pencomBatch

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationApprovals")
			dtUser = dsUser.Tables("ApplicationApprovals")
			mycon.Close()

			If dtUser.Rows.Count > 0 Then
				Return True
			Else
				Return False
			End If

		Catch Ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = errpath
			logerr.Logger(Ex.Message)

		Finally

		End Try


		Return True
	End Function
	Public Sub PMInsertPencomApproval(ApprovalDetails As PencomApprovalDetails, lstApprovedApp As List(Of ApplicationDetail), updateType As Integer, fundID As Integer, errPath As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = "", i As Integer
		Dim vDate As Date
		vDate = Me.PMgetCurrentValueDate(fundID)

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			If updateType = 0 Then

				myComm.CommandText = "insert into tblApplicationApprovals (txtRefNo,dteApproval,dteAcknowledgment,numApprovalAmount,txtCreatedBy,dteCreated,fkiAppTypeID,numApprovalLumpSum,numApprovalPension,numApprovalAnnuity) values ('" & ApprovalDetails.PencomBatch & "','" & DateTime.Parse(ApprovalDetails.ApprovalDate).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(ApprovalDetails.AcknowledgmentDate).ToString("yyyy-MM-dd") & "','" & ApprovalDetails.TotalApprovalAmount & "', '" & ApprovalDetails.CreatedBy & "','" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "','" & ApprovalDetails.AppType & "','" & ApprovalDetails.TotalLumpSumAmount & "','" & ApprovalDetails.TotalPensionAmount & "','" & ApprovalDetails.TotalAnnuityAmount & "') "
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


			ElseIf updateType = 1 Then

				myComm.CommandText = "update tblApplicationApprovals set txtRefNo = '" & ApprovalDetails.PencomBatch & "',dteApproval = '" & DateTime.Parse(ApprovalDetails.ApprovalDate).ToString("yyyy-MM-dd") & "' ,dteAcknowledgment ='" & DateTime.Parse(ApprovalDetails.AcknowledgmentDate).ToString("yyyy-MM-dd") & "',numApprovalAmount = '" & ApprovalDetails.TotalApprovalAmount & "',txtCreatedBy = '" & ApprovalDetails.CreatedBy & "',dteCreated = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',numApprovalLumpSum = '" & ApprovalDetails.TotalLumpSumAmount & "',numApprovalPension = '" & ApprovalDetails.TotalPensionAmount & "', numApprovalAnnuity = '" & ApprovalDetails.TotalAnnuityAmount & "' where txtRefNo = '" & ApprovalDetails.PencomBatch & "'"
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()

			End If






			If updateType = 0 Then

				Do While i < lstApprovedApp.Count

					myComm.CommandText = "update tblMemberApplication set txtPencomBatch = '" & lstApprovedApp(i).PencomBatch & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					If lstApprovedApp(i).AppTypeId = 3 Then

						myComm.CommandText = "update awbr100 set numLumpSumToPay = '" & lstApprovedApp(i).PayingLumpSum & "',numPensionToPay = '" & lstApprovedApp(i).PayingPension & "',numApprovedArrears= '" & lstApprovedApp(i).PayingArrears & "',numApprovedLumpSum = '" & lstApprovedApp(i).ApprovedAmount & "',numApprovedPension='" & lstApprovedApp(i).AmountToPay & "', numCalculatedArrears='" & lstApprovedApp(i).PayingAnnuity & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()


					ElseIf lstApprovedApp(i).AppTypeId = 4 Then

						myComm.CommandText = "update awbr700 set numApprovedLumpSum = '" & lstApprovedApp(i).ApprovedAmount & "',numApprovedAnnuity='" & lstApprovedApp(i).Annuity & "',numLumpSumToPay = '" & lstApprovedApp(i).PayingLumpSum & "',numAnnuityToPay = '" & lstApprovedApp(i).PayingAnnuity & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()


					ElseIf lstApprovedApp(i).AppTypeId = 15 Then

						myComm.CommandText = "update awbr700 set numApprovedLumpSum = '" & lstApprovedApp(i).ApprovedAmount & "',numApprovedAnnuity='" & lstApprovedApp(i).Annuity & "',numLumpSumToPay = '" & lstApprovedApp(i).PayingLumpSum & "',numAnnuityToPay = '" & lstApprovedApp(i).PayingAnnuity & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()


					ElseIf lstApprovedApp(i).AppTypeId = 2 Then

						myComm.CommandText = "update awbr500 set numApprovedAmount = '" & lstApprovedApp(i).AmountToPay & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 1 Then

						myComm.CommandText = "update awbr400 set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 16 Then

						myComm.CommandText = "update awbr400 set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 5 Then

						myComm.CommandText = "update awbr600 set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 6 Then

						myComm.CommandText = "update awbr200 set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 7 Then

						myComm.CommandText = "update awbr800 set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "',numInterestAtPayment = '" & lstApprovedApp(i).InterestAmount & "', numTaxAtPayment = '" & PMGetAVCTax(lstApprovedApp(i).InterestAmount) & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 8 Then

						myComm.CommandText = "update awbr300 set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					ElseIf lstApprovedApp(i).AppTypeId = 11 Then

						myComm.CommandText = "update awbrEEP set numApprovedAmount = '" & lstApprovedApp(i).ApprovedAmount & "',numAmountToPay='" & lstApprovedApp(i).AmountToPay & "' where txtApplicationCode = '" & lstApprovedApp(i).ApplicationID & "'"
						command.CommandType = CommandType.Text
						myComm.ExecuteNonQuery()

					End If





					' getting the most current available valuedate
					Dim vDates As Date = PMgetCurrentValueDate(fundID)


					' calculation the current value of the customer
					Dim amtPaid As Double = PMValueByDate(lstApprovedApp(i).PIN, vDates, 1)

					If lstApprovedApp(i).AppTypeId = 2 Then

						myComm.CommandText = "insert into tblApplicationApprovalPayee (txtApplicationCode,numApproved,txtCreatedBy,txtPencomBatch,dteValueDate,numPayingAmount,intOrderID) values ('" & lstApprovedApp(i).ApplicationID & "','" & lstApprovedApp(i).ApprovedAmount & "','" & ApprovalDetails.CreatedBy & "','" & ApprovalDetails.PencomBatch & "','" & DateTime.Parse(vDates).ToString("yyyy-MM-dd") & "',  '" & lstApprovedApp(i).AmountToPay & "','" & lstApprovedApp(i).ApprovalOrderID & "') "

					Else

						myComm.CommandText = "insert into tblApplicationApprovalPayee (txtApplicationCode,numApproved,txtCreatedBy,txtPencomBatch,dteValueDate,numPayingAmount,intOrderID) values ('" & lstApprovedApp(i).ApplicationID & "','" & lstApprovedApp(i).ApprovedAmount & "','" & ApprovalDetails.CreatedBy & "','" & ApprovalDetails.PencomBatch & "','" & DateTime.Parse(vDates).ToString("yyyy-MM-dd") & "',  '" & Double.Parse(amtPaid) & "','" & lstApprovedApp(i).ApprovalOrderID & "') "


					End If


					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					i = i + 1

				Loop

			ElseIf updateType = 1 Then


			End If


			sqlTran.Commit()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			logerr.FilePath = errPath
			logerr.Logger(ex.Message)

		End Try

	End Sub


	'exporting payment approval into enpower for further processing
	Public Sub PMInsertEnpowerPaymentTemp(lstApprovalExport As List(Of PencomApprovalExport))

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, i As Integer


		Try

			mycon = db.getConnection("EnpowerV4")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			Do While i < lstApprovalExport.Count

				myComm.CommandText = "INSERT INTO [dbo].[TempPayment] ([FundID],[RSAPIN],[PaymentTypeID],[ApprovalDate],[ValueDate],[StartPeriod],[EndPeriod],[Amount])  VALUES('" & lstApprovalExport.Item(i).FundID & "','" & lstApprovalExport.Item(i).RSAPIN & "','" & lstApprovalExport.Item(i).ApplicationType & "','" & DateTime.Parse(lstApprovalExport.Item(i).ApprovalDate).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstApprovalExport.Item(i).ValueDate).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstApprovalExport.Item(i).StartPeriod).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstApprovalExport.Item(i).EndPeriod).ToString("yyyy-MM-dd") & "','" & lstApprovalExport.Item(i).ApprovalAmount & "') "
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()



				myComm.CommandText = "update  a set a.AccountName = b.txtAccountName ,a.AccountNumber  = b.txtAccountNo ,BankID = b.fkiBankID ,BankBranchID = b.fkiBranchID from EnPowerV4..employee a, SurePay..tblMemberApplication b, EnPowerV4..Bank c, EnPowerV4..BankBranch d  where b.txtPIN = a.RSAPIN and c.BankID = d.BankID and c.BankID = b.fkiBankID and d.BankBranchID = b.fkiBranchID and b.txtApplicationCode  = '" & lstApprovalExport.Item(i).ApplicationCode & "'"

				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				myComm.CommandText = "update  a set a.IsRetired = 1, DateRetired = b.dteDOR from EnPowerV4..employee a, SurePay..tblMemberApplication b, EnPowerV4..Bank c, EnPowerV4..BankBranch d  where b.txtPIN = a.RSAPIN and c.BankID = d.BankID and c.BankID = b.fkiBankID and d.BankBranchID = b.fkiBranchID and b.txtApplicationCode  = '" & lstApprovalExport.Item(i).ApplicationCode & "' and b.dtedor is not null "

				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				'myComm.CommandText = "update b set b.insurerid = a.txtInsuranceCompanyName from SurePay..tmpPWAnnuityDetails a, employee b, SurePay..tblMemberApplication c where c.txtPIN = b.RSAPIN And c.txtapplicationcode = a.txtapplicationcode and a.txtapplicationcode ='" & lstApprovalExport.Item(i).ApplicationCode & "'"
				'command.CommandType = CommandType.Text
				'myComm.ExecuteNonQuery()



				myComm.CommandText = "update SurePay.dbo.tblApplicationApprovalPayee set txtEnpowerExtractBatch = '" & lstApprovalExport.Item(i).EnpowerExportBatch & "', txtStatus = 'E',dteStartPeriod ='" & DateTime.Parse(lstApprovalExport.Item(i).StartPeriod).ToString("yyyy-MM-dd") & "',dteEndPeriod = '" & DateTime.Parse(lstApprovalExport.Item(i).EndPeriod).ToString("yyyy-MM-dd") & "' where txtApplicationCode = '" & lstApprovalExport.Item(i).ApplicationCode & "' "
				command.CommandType = CommandType.Text
				myComm.ExecuteNonQuery()


				i = i + 1

			Loop


			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub PMInsertRMASScheduleHardShip(lstRMASSchedule As List(Of RMASSchedule), SPLogBatchNo As String, apptype As String, submissionType As Integer, uName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String, i As Integer, MyDataAdapter As New SqlDataAdapter


		Try


			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran

			'dteGenerated '" & DateTime.Parse(Now.ToString("yyyy-MM-dd HH:MM")) & "',

			myComm.CommandText = "insert into tblSPLog (txtBatchNo,fkiAppTypeId,txtCreatedBy) values ('" & SPLogBatchNo & "','" & apptype & "','" & uName & "') "
			command.CommandType = CommandType.Text
			myComm.ExecuteNonQuery()

			Do While i < lstRMASSchedule.Count

				'IsSentToPencom = 1 means approval was sent through rmas to pencom
				'IsSentToPencom = 2 means approval was sent through hard copy files to pencom
				'IsSentToPencom = 0 means approval has not been sent at all

				'inserting rmas return schedule for 25%
				If submissionType = 1 And apptype = "2" Then

					sql1 = "insert into awbr500 (pin,[employer-code],gender,[birth-date],[disengagement-date],[rsa-balance],[twentyfive-percent-rsa-balance],[date-sent],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "','" & lstRMASSchedule(i).Gender & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & DateTime.Parse(lstRMASSchedule(i).DateDisengagement).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).RSABalance & "','" & lstRMASSchedule(i).Twenty5Percent & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "')"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()

					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					'inserting rmas return schedule for enbloc
				ElseIf submissionType = 1 And apptype = "1" Then


					sql1 = "insert into awbr400 (pin,[employer-code],nationality,gender,[birth-date],[retirement-date],[reason-for-payment],[rsa-balance],[enbloc-payment],[date-sent],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "','" & lstRMASSchedule(i).Nationality & "','" & lstRMASSchedule(i).Gender & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).PaymentReason & "','" & lstRMASSchedule(i).RSABalance & "','" & lstRMASSchedule(i).EnblocAmount & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "')"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()

					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()




					'inserting rmas return schedule for additional enbloc
				ElseIf submissionType = 1 And apptype = "16" Then


					sql1 = "insert into awbr400 (pin,[employer-code],nationality,gender,[birth-date],[retirement-date],[reason-for-payment],[rsa-balance],[enbloc-payment],[date-sent],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "','" & lstRMASSchedule(i).Nationality & "','" & lstRMASSchedule(i).Gender & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).PaymentReason & "','" & lstRMASSchedule(i).RSABalance & "','" & lstRMASSchedule(i).EnblocAmount & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "')"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()

					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()




				ElseIf submissionType = 1 And apptype = "8" Then


					' sql1 = "insert into awbr300 (pin,[employer-code],[retirement-date],[enbloc-payment],[date-sent],txtApplicationCode) values (@PIN,@Employercode, @RetirementDate,@EnblocAmount,@dataSent,@ApplicationCode)"

					myComm.CommandText = "insert into awbr300 (pin,[employer-code],[retirement-date],[enbloc-payment],[date-sent],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).EnblocAmount & "', '" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				ElseIf submissionType = 1 And apptype = "7" Then


					myComm.CommandText = "insert into awbr800 (pin,[employer-code],[birth-date],[retirement-date],[total-voluntary-contribution],[total-amount],[amount-requested],[tax-deducted],[amount-payable-net-tax],[date-sent],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).TotalAVC & "', '" & lstRMASSchedule(i).TotalAVCAmount & "','" & lstRMASSchedule(i).TotalAVCAmount & "','" & lstRMASSchedule(i).AVCTax & "','" & lstRMASSchedule(i).NetAVCPayable & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				ElseIf submissionType = 1 And apptype = "3" Then


					myComm.CommandText = "insert into awbr100 (pin,[employer-code],[birth-date],[retirement-date],[retirement-age],[gender],[retirement-ground],[annual-total-emolument],[accrued-right],[rsa-balance],[recommended-lumpsum],[monthly-programed-drawndown],[dateSent],[txtApplicationCode]) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).Age) & "','" & lstRMASSchedule(i).Gender & "', '" & lstRMASSchedule(i).PaymentReason & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).RetirementDetails.AccruedRight & "','" & lstRMASSchedule(i).RetirementDetails.RSABalance & "','" & lstRMASSchedule(i).RetirementDetails.RecommendedLumpSum & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyProgramedDrawndown & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					myComm.CommandText = "insert into awbr101 (pin,[employer-name],[basic-salary],[housing-rent],transport,utility,[consolidated-allowance],[consolidated-salary],[monthly-total],[annual-total-emolument-adjusted],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).EmployerName & "', '" & lstRMASSchedule(i).RetirementDetails.BasicSalary & "','" & (lstRMASSchedule(i).RetirementDetails.HouseRent) & "','" & lstRMASSchedule(i).RetirementDetails.Transport & "', '" & lstRMASSchedule(i).RetirementDetails.Utility & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedAallowance & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedSalary & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyTotal & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



				ElseIf submissionType = 1 And apptype = "4" Then


					myComm.CommandText = "insert into awbr700 (pin,[employer-code],[birth-date],[retirement-date],[insurance-company-name],[annuity-commencement-date],[annual-total-emolument],[rsa-balance],premium,lumpsum,[monthly-annuity],[dateSent],[txtApplicationCode]) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).RetirementDetails.InsuranceCoy) & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDetails.AnnuityCommencement).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "', '" & lstRMASSchedule(i).RetirementDetails.RSABalance & "','" & lstRMASSchedule(i).RetirementDetails.Premium & "','" & lstRMASSchedule(i).RetirementDetails.AnnuityLumpSum & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyAnnuity & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					myComm.CommandText = "insert into awbr701 (pin,[employer-name],[basic-salary],[housing-rent],transport,utility,[consolidated-allowance],[consolidated-salary],[monthly-total],[annual-total-emolument-adjusted],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).EmployerName & "', '" & lstRMASSchedule(i).RetirementDetails.BasicSalary & "','" & (lstRMASSchedule(i).RetirementDetails.HouseRent) & "','" & lstRMASSchedule(i).RetirementDetails.Transport & "', '" & lstRMASSchedule(i).RetirementDetails.Utility & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedAallowance & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedSalary & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyTotal & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()






				ElseIf submissionType = 1 And apptype = "15" Then


					myComm.CommandText = "insert into awbr700 (pin,[employer-code],[birth-date],[retirement-date],[insurance-company-name],[annuity-commencement-date],[annual-total-emolument],[rsa-balance],premium,lumpsum,[monthly-annuity],[dateSent],[txtApplicationCode]) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).RetirementDetails.InsuranceCoy) & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDetails.AnnuityCommencement).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "', '" & lstRMASSchedule(i).RetirementDetails.RSABalance & "','" & lstRMASSchedule(i).RetirementDetails.Premium & "','" & lstRMASSchedule(i).RetirementDetails.AnnuityLumpSum & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyAnnuity & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					myComm.CommandText = "insert into awbr701 (pin,[employer-name],[basic-salary],[housing-rent],transport,utility,[consolidated-allowance],[consolidated-salary],[monthly-total],[annual-total-emolument-adjusted],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).EmployerName & "', '" & lstRMASSchedule(i).RetirementDetails.BasicSalary & "','" & (lstRMASSchedule(i).RetirementDetails.HouseRent) & "','" & lstRMASSchedule(i).RetirementDetails.Transport & "', '" & lstRMASSchedule(i).RetirementDetails.Utility & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedAallowance & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedSalary & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyTotal & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()






				ElseIf submissionType = 1 And apptype = "5" Then


					myComm.CommandText = "INSERT INTO [dbo].[awbr600] ([name],[pin-dba],[gender],[employer-code],[retirement-date],[death-date],[administration-letter-issuing-authority],[administration-letter-date],[administration-letter-named-administrator-nok],[life-insurance-proceed],[accured-right],[contributions],[investment-income],[total-rsa-balance],[remark],[dateSent],[txtApplicationCode]) values ('" & lstRMASSchedule(i).Name & "','" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Gender & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDetails.RetirementDate).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDetails.DeathDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).RetirementDetails.AdminIssuingAuthority) & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDetails.AdminIssuingDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).RetirementDetails.AdminNOK & "', '" & lstRMASSchedule(i).RetirementDetails.InsuranceProceed & "','" & lstRMASSchedule(i).RetirementDetails.AccruedRight & "','" & lstRMASSchedule(i).RetirementDetails.Contribution & "','" & lstRMASSchedule(i).RetirementDetails.InvestmentIncome & "','" & lstRMASSchedule(i).RetirementDetails.RSABalance & "','" & lstRMASSchedule(i).RetirementDetails.Remarks & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()




				ElseIf submissionType = 1 And apptype = "6" Then


					myComm.CommandText = "INSERT INTO [dbo].[awbr200] ([pin],[employer-code],[retirement-date],[initial-amount-paid-under-pra],[amount-recieved-nsitf-to-rsa],[amount-requested-under-nsitf-from-rsa],[dateSent],[txtApplicationCode]) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).NSITFInitialAmountPaid) & "','" & lstRMASSchedule(i).NSITFRecievedToRSA & "', '" & lstRMASSchedule(i).NSITFRequestedToRSA & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 1, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					'saving hardcopy for additional lumpSum
				ElseIf submissionType = 2 And apptype = "14" Then


					myComm.CommandText = "insert into awbr100 (pin,[employer-code],[birth-date],[retirement-date],[retirement-age],[gender],[retirement-ground],[annual-total-emolument],[accrued-right],[rsa-balance],[recommended-lumpsum],[monthly-programed-drawndown],[dateSent],[txtApplicationCode],[isRMASApplication]) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).Age) & "','" & lstRMASSchedule(i).Gender & "', '" & lstRMASSchedule(i).PaymentReason & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).RetirementDetails.AccruedRight & "','" & lstRMASSchedule(i).RetirementDetails.RSABalance & "','" & lstRMASSchedule(i).RetirementDetails.RecommendedLumpSum & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyProgramedDrawndown & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "',0)"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					myComm.CommandText = "insert into awbr101 (pin,[employer-name],[basic-salary],[housing-rent],transport,utility,[consolidated-allowance],[consolidated-salary],[monthly-total],[annual-total-emolument-adjusted],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).EmployerName & "', '" & lstRMASSchedule(i).RetirementDetails.BasicSalary & "','" & (lstRMASSchedule(i).RetirementDetails.HouseRent) & "','" & lstRMASSchedule(i).RetirementDetails.Transport & "', '" & lstRMASSchedule(i).RetirementDetails.Utility & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedAallowance & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedSalary & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyTotal & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 2, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					'saving hardCopy for additional annuity application
				ElseIf submissionType = 2 And apptype = "15" Then



					myComm.CommandText = "insert into awbr700 (pin,[employer-code],[birth-date],[retirement-date],[insurance-company-name],[annuity-commencement-date],[annual-total-emolument],[rsa-balance],premium,lumpsum,[monthly-annuity],[dateSent],[txtApplicationCode]) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).RetirementDetails.InsuranceCoy) & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDetails.AnnuityCommencement).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "', '" & lstRMASSchedule(i).RetirementDetails.RSABalance & "','" & lstRMASSchedule(i).RetirementDetails.Premium & "','" & lstRMASSchedule(i).RetirementDetails.AnnuityLumpSum & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyAnnuity & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					myComm.CommandText = "insert into awbr701 (pin,[employer-name],[basic-salary],[housing-rent],transport,utility,[consolidated-allowance],[consolidated-salary],[monthly-total],[annual-total-emolument-adjusted],txtApplicationCode) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).EmployerName & "', '" & lstRMASSchedule(i).RetirementDetails.BasicSalary & "','" & (lstRMASSchedule(i).RetirementDetails.HouseRent) & "','" & lstRMASSchedule(i).RetirementDetails.Transport & "', '" & lstRMASSchedule(i).RetirementDetails.Utility & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedAallowance & "','" & lstRMASSchedule(i).RetirementDetails.ConsolidatedSalary & "','" & lstRMASSchedule(i).RetirementDetails.MonthlyTotal & "','" & lstRMASSchedule(i).RetirementDetails.AnnualTotalEmolumentAdj & "','" & lstRMASSchedule(i).ApplicationCode & "')"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 2, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



					'saving hardcopy for employee portion
				ElseIf submissionType = 2 And apptype = "11" Then


					sql1 = "insert into awbrEEP (pin,[employer-code],[birth-date],[amount],[date-sent],txtApplicationCode,isRMASApplication) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & lstRMASSchedule(i).RSABalance & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "',0)"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()


					Dim dt As DateTime = formatMyDate(Now)
					myComm.CommandText = "update tblMemberApplication set txtStatus = 'Sent To Pencom', IsSentToPencom = 2, dteSentToPencom = cast('" & dt.ToString("yyyy-MM-dd HH:MM") & "' as datetime),txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					'saving hardcopy for 25% applications
				ElseIf submissionType = 2 And apptype = "2" Then


					sql1 = "insert into awbr500 (pin,[employer-code],gender,[birth-date],[disengagement-date],[rsa-balance],[twentyfive-percent-rsa-balance],[date-sent],txtApplicationCode,isRMASApplication) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "','" & lstRMASSchedule(i).Gender & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & DateTime.Parse(lstRMASSchedule(i).DateDisengagement).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).RSABalance & "','" & lstRMASSchedule(i).Twenty5Percent & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "',0)"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()



					myComm.CommandText = "update tblMemberApplication set IsSentToPencom = 2, dteSentToPencom = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					'saving hardcopy for enbloc  application
				ElseIf submissionType = 2 And apptype = "1" Then


					sql1 = "insert into awbr400 (pin,[employer-code],nationality,gender,[birth-date],[retirement-date],[reason-for-payment],[rsa-balance],[enbloc-payment],[date-sent],txtApplicationCode,isRMASApplication) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "','" & lstRMASSchedule(i).Nationality & "','" & lstRMASSchedule(i).Gender & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).PaymentReason & "','" & lstRMASSchedule(i).RSABalance & "','" & lstRMASSchedule(i).EnblocAmount & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "',0)"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()


					myComm.CommandText = "update tblMemberApplication set IsSentToPencom = 2, dteSentToPencom = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					'saving hardcopy for addtional enbloc  application
				ElseIf submissionType = 2 And apptype = "16" Then


					sql1 = "insert into awbr400 (pin,[employer-code],nationality,gender,[birth-date],[retirement-date],[reason-for-payment],[rsa-balance],[enbloc-payment],[date-sent],txtApplicationCode,isRMASApplication) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "','" & lstRMASSchedule(i).Nationality & "','" & lstRMASSchedule(i).Gender & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).PaymentReason & "','" & lstRMASSchedule(i).RSABalance & "','" & lstRMASSchedule(i).EnblocAmount & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & (lstRMASSchedule(i).ApplicationCode) & "',0)"
					myComm.CommandText = sql1
					myComm.ExecuteNonQuery()


					myComm.CommandText = "update tblMemberApplication set IsSentToPencom = 2, dteSentToPencom = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



				ElseIf submissionType = 2 And apptype = "8" Then


					myComm.CommandText = "insert into awbr300 (pin,[employer-code],[retirement-date],[enbloc-payment],[date-sent],txtApplicationCode,isRMASApplication) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).EnblocAmount & "', '" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "',0)"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "update tblMemberApplication set IsSentToPencom = 2, dteSentToPencom = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					'saving hardcopy for avc  application
				ElseIf submissionType = 2 And apptype = "7" Then

					myComm.CommandText = "insert into awbr800 (pin,[employer-code],[birth-date],[retirement-date],[total-voluntary-contribution],[total-amount],[amount-requested],[tax-deducted],[amount-payable-net-tax],[date-sent],txtApplicationCode,isRMASApplication) values ('" & lstRMASSchedule(i).PIN & "','" & lstRMASSchedule(i).Employercode & "', '" & DateTime.Parse(lstRMASSchedule(i).DOB).ToString("yyyy-MM-dd") & "','" & DateTime.Parse(lstRMASSchedule(i).RetirementDate).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).TotalAVC & "', '" & lstRMASSchedule(i).TotalAVCAmount & "','" & lstRMASSchedule(i).TotalAVCAmount & "','" & lstRMASSchedule(i).AVCTax & "','" & lstRMASSchedule(i).NetAVCPayable & "','" & DateTime.Parse(lstRMASSchedule(i).DateSent).ToString("yyyy-MM-dd") & "','" & lstRMASSchedule(i).ApplicationCode & "',0)"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "update tblMemberApplication set IsSentToPencom = 2, dteSentToPencom = '" & DateTime.Parse(Now).ToString("yyyy-MM-dd HH:MM") & "',txtSPBatchNo = '" & SPLogBatchNo & "' where txtApplicationCode = '" & lstRMASSchedule(i).ApplicationCode & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()



				End If

				i = i + 1

			Loop

			sqlTran.Commit()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub
	Private Function formatMyDate(myDate As DateTime) As DateTime

		Return String.Format("{0:s}", myDate)

	End Function


	'confirmation of the approvals recieved from pencom
	Public Sub PMReCallRMASSchedule(pin As String, ddate As Date, Apptype As Integer, UName As String)

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		Dim myComm, command As New SqlClient.SqlCommand, sql1 As String = ""

		Try

			mycon = db.getConnection("PaymentModule")
			Dim sqlTran As SqlClient.SqlTransaction = mycon.BeginTransaction
			myComm = mycon.CreateCommand
			myComm.Transaction = sqlTran


			Select Case Apptype

				Case Is = 2

					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null, b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr500 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr500  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 1


					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation', b.txtLastChangedPerson ='" & UName & "' from awbr400 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr400  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

				Case Is = 16


					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation', b.txtLastChangedPerson ='" & UName & "' from awbr400 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr400  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 8


					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr300 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr300  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 7

					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr800 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr800  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 3

					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr100 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr100  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr101  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 4


					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr700 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr700  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr701  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 15


					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr700 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr700  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()

					myComm.CommandText = "delete from awbr701  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 5


					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr600 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  b.txtpin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr600  where  [pin-dba] = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


				Case Is = 6

					myComm.CommandText = "update b set b.IsSentToPencom = null,b.dteSentToPencom = null,b.txtSPBatchNo = null,b.txtStatus = 'Confirmation',b.txtLastChangedPerson ='" & UName & "' from awbr200 a, tblmemberapplication b   where a.txtapplicationcode = b.txtapplicationcode and  a.pin = '" & pin & "' and fkiAppTypeId = '" & Apptype & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


					myComm.CommandText = "delete from awbr200  where  pin = '" & pin & "'"
					command.CommandType = CommandType.Text
					myComm.ExecuteNonQuery()


			End Select

			sqlTran.Commit()

		Catch ex As Exception
			'  MsgBox("" & ex.Message)
		End Try

	End Sub



	Public Sub PMComfirmRMASSchedule(pin As String, ddate As Date, Apptype As Integer, UName As String)
		Try

			Dim db As New DbConnection

			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable, sql As String = ""

			Select Case Apptype

				Case Is = 2

					sql = "update awbr500 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [date-sent] = @dataSent and pin = @pin"

				Case Is = 1 'enbloc application

					sql = "update awbr400 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [date-sent] = @dataSent and pin = @pin"

				Case Is = 16 'additional enbloc application

					sql = "update awbr400 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [date-sent] = @dataSent and pin = @pin"

				Case Is = 8

					sql = "update awbr300 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [date-sent] = @dataSent and pin = @pin"

				Case Is = 7

					sql = "update awbr800 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [date-sent] = @dataSent and pin = @pin"

				Case Is = 3

					sql = "update awbr100 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [dateSent] = @dataSent and pin = @pin"

				Case Is = 4 'annnuity application

					sql = "update awbr700 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [dateSent] = @dataSent and pin = @pin"

				Case Is = 15 'additional annnuity application

					sql = "update awbr700 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [dateSent] = @dataSent and pin = @pin"

				Case Is = 5

					sql = "update awbr600 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [dateSent] = @dataSent and [pin-dba] = @pin"

				Case Is = 6

					sql = "update awbr200 set blnConfirm = @blnConfirm,dteConfirmed = @dteConfirmed,txtConfirmedBy = @txtConfirmedBy where [dateSent] = @dataSent and pin = @pin"

			End Select


			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@blnConfirm", SqlDbType.Bit))
			MyDataAdapter.SelectCommand.Parameters("@blnConfirm").Value = 1

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteConfirmed", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteConfirmed").Value = DateTime.Parse(Now)

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtConfirmedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtConfirmedBy").Value = UName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pin").Value = pin

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dataSent", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@dataSent").Value = DateTime.Parse(ddate).ToString("yyyy-MM-dd")

			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			
			mycon.Close()

		Catch ex As Exception
			'MsgBox(" " & ex.Message)
		End Try

	End Sub

	Public Function PMgetCurrentValueDate(fundid As Integer) As Date

		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim sql As String
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable

		sql = "select max(valuedate) from [dbo].[UnitPrice] where FundID = @fundID"

		MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text
		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fundID", SqlDbType.Int))
		MyDataAdapter.SelectCommand.Parameters("@fundID").Value = fundid
		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "Enpower")
		dtUser = dsUser.Tables("Enpower")

		mycon.Close()
		If dtUser.Rows.Count > 0 Then
			Return dtUser.Rows(0).Item(0)
		Else
			Return Now.Date
		End If


	End Function

	Public Function PMgetPaymentData(batches As String, AppType As Integer, d_ate As Date) As DataTable
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable, sql As String = ""

			Select Case AppType

				Case Is = 2

					sql = " select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDisengagement,cast(getdate() as date)) dteDisengagement,d.[twentyfive-percent-rsa-balance]  as ApprovedAmount ,isnull(d.numAmountToPay,d.[twentyfive-percent-rsa-balance]) as AmountToPay,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr500 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""

				Case Is = 8

					sql = "select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[enbloc-payment]  as Amount ,isnull(d.[numApprovedAmount],d.[enbloc-payment]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,isnull(d.[numAmountToPay],d.[enbloc-payment]) AmountToPay,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo  from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr300 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""

				Case Is = 1


					sql = " select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[enbloc-payment]  as ApprovedAmount ,isnull(d.numAmountToPay,0.00) as AmountToPay,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo, isnull(a.intFundPlatFormID,0) intFundPlatFormID from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr400  d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""



				Case Is = 16


					sql = " select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[enbloc-payment]  as ApprovedAmount ,isnull(d.numAmountToPay,0.00) as AmountToPay,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo, isnull(a.intFundPlatFormID,0) intFundPlatFormID from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr400  d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""



				Case Is = 7

					sql = "select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[amount-payable-net-tax]  as Amount ,isnull(d.[numApprovedAmount],d.[amount-payable-net-tax]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,isnull(d.[numAmountToPay],d.[amount-payable-net-tax]) AmountToPay,isnull(d.numInterestAtPayment,0) numInterestAtPayment,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo  from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr800 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""

				Case Is = 3


					sql = "select a.dteDOB,a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,isnull(d.numApprovedLumpSum,d.[recommended-lumpsum])  as LumpSum ,isnull(d.numApprovedPension,d.[monthly-programed-drawndown])  as MonthPension ,(DATEDIFF(month,dteDOR,DATEADD(year,50,d.[birth-date]))) ArearsMonths,isnull((select numApproved from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode),d.[rsa-balance]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo,d.[monthly-programed-drawndown],isnull(d.numLumpSumToPay,d.[recommended-lumpsum]) as PayingLumpSum,isnull(d.numPensionToPay,d.[monthly-programed-drawndown]) as PayingPension,isnull(numApprovedArrears,(DATEDIFF(month,dteDOR,DATEADD(year,50,d.[birth-date]))) * d.[monthly-programed-drawndown]) as PayingArrears, isnull(numApprovedArrears,0) numApprovedArrears from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr100 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""


				Case Is = 14
					'additionla lumpSum payment

					sql = "select a.dteDOB,a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,isnull(d.numApprovedLumpSum,d.[recommended-lumpsum])  as LumpSum ,isnull(d.numApprovedPension,d.[monthly-programed-drawndown])  as MonthPension ,(DATEDIFF(month,dteDOR,DATEADD(year,50,d.[birth-date]))) ArearsMonths,isnull((select numApproved from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode),d.[rsa-balance]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo,d.[monthly-programed-drawndown],isnull(d.numLumpSumToPay,d.[recommended-lumpsum]) as PayingLumpSum,isnull(d.numPensionToPay,d.[monthly-programed-drawndown]) as PayingPension,isnull(numApprovedArrears,(DATEDIFF(month,dteDOR,DATEADD(year,50,d.[birth-date]))) * d.[monthly-programed-drawndown]) as PayingArrears, isnull(numApprovedArrears,0) numApprovedArrears from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr100 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""

					'annuity payment

				Case Is = 4


					sql = "select a.dteDOB,a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[rsa-balance]  as Amount ,isnull((select numApproved from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode),d.[rsa-balance]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo,isnull(d.numApprovedLumpSum,d.[lumpsum]) lumpsum,isnull(d.numApprovedAnnuity,d.[monthly-annuity]) [monthly-annuity],isnull((select InsurerName from enpowerv4..Insurer where convert(varchar(255),InsurerID) = d.[insurance-company-name]),d.[insurance-company-name]) [insurance-company-name] ,ISNULL(d.numLumpSumToPay,lumpsum) as PayingLumpSum, ISNULL(d.numAnnuityToPay,[monthly-annuity]) as PayingAnnuity  from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr700 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""

					'additional annuity payment
				Case Is = 15


					sql = "select a.dteDOB,a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[rsa-balance]  as Amount ,isnull((select numApproved from tblApplicationApprovalPayee where txtApplicationCode = a.txtApplicationCode),d.[rsa-balance]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo,isnull(d.numApprovedLumpSum,d.[lumpsum]) lumpsum,isnull(d.numApprovedAnnuity,d.[monthly-annuity]) [monthly-annuity],(select InsurerName from enpowerv4..Insurer where convert(varchar(255),InsurerID) = d.[insurance-company-name]) [insurance-company-name] ,ISNULL(d.numLumpSumToPay,lumpsum) as PayingLumpSum, ISNULL(d.numAnnuityToPay,[monthly-annuity]) as PayingAnnuity  from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr700 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""



				Case Is = 5


					sql = "select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[total-rsa-balance]  as Amount ,isnull(d.[numApprovedAmount],d.[total-rsa-balance]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment, Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,isnull(a.intFundPlatFormID,0),(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = isnull(a.intFundPlatFormID,0))) AmountToPay,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr600 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""


				Case Is = 6


					sql = "select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,d.[amount-requested-under-nsitf-from-rsa]  as Amount ,isnull(d.[numApprovedAmount],d.[amount-requested-under-nsitf-from-rsa]) ApprovedAmount,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,isnull(d.[numAmountToPay],d.[amount-requested-under-nsitf-from-rsa]) AmountToPay,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo  from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbr200 d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""


				Case Is = 11


					sql = "select a.txtApplicationCode,txtPIN,txtFullName,isnull(dteDOR,cast(getdate() as date)) dteDOR,isnull(d.numApprovedAmount,amount)  as ApprovedAmount ,isnull(d.numAmountToPay,amount) as AmountToPay,dteConfirmPriceDate as ValueDate,txtAccountName,txtAccountNo, (select BankName from enpowerv4..Bank where BankID = fkiBankID) fkiBankID,(select BranchName from enpowerv4..[BankBranch] where BankBranchID = fkiBranchID) fkiBranchID,txtPencomBatch,dteApprovalConfirmed,(select dteApproval from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteApproved,(select dteAcknowledgment from tblApplicationApprovals where txtRefNo =  txtPencomBatch) dteAcknowledgment,(select numApprovalAmount from tblApplicationApprovals where txtRefNo =  txtPencomBatch) numApprovalAmount,(select txtRefNo from tblApplicationApprovals where txtRefNo =  txtPencomBatch) txtRefNo, isnull(a.intFundPlatFormID,0) intFundPlatFormID from tblMemberApplication a,tblSPLog b, tblApplicationType c,dbo.awbrEEP  d where txtSPBatchNo = txtBatchNo and c.pkiAppTypeId = a.fkiAppTypeId and d.txtApplicationCode = a.txtApplicationCode and dteApprovalConfirmed is null and  txtSPBatchNo in " & batches & ""



			End Select


			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "RMAS")
			dtUser = dsUser.Tables("RMAS")

			mycon.Close()

			Return dtUser

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Function

	Public Sub PMEditAmountToPay(appDetails As PencomApprovalPeople)
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter, sql As String = ""

			Select Case appDetails.AppTypeId
				'updating amountToPay for 25% to be sent through rmas
				Case Is = 2
					sql = "update awbr500 set numApprovedAmount = @numAmountToPay, numAmountToPay = @numAmountToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"
					'updating amountToPay for employer portion
				Case Is = 11

					sql = "update awbrEEP set numAmountToPay = @numAmountToPay, numApprovedAmount = @numApprovedAmount where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'updating amountToPay for enbloc to be sent through rmas
				Case Is = 1

					sql = "update awbr400 set numAmountToPay = @numAmountToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'updating amountToPay for additional enbloc to be sent through rmas
				Case Is = 16

					sql = "update awbr400 set numAmountToPay = @numAmountToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'populating application for legacy payment to be sent through rmas
				Case Is = 8

					sql = "update awbr300 set  numAmountToPay = @numAmountToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"
					'populating application for AVC payment to be sent through rmas  
				Case Is = 7

					sql = "update awbr800 set  numAmountToPay = @numAmountToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'updating amoutnto pay to lumpSum Payment

				Case Is = 3

					sql = "update awbr100 set  numApprovedLumpSum = @numApprovedLumpSum, numApprovedPension = @numApprovedPension, numApprovedArrears = @numApprovedArrears, numLumpSumToPay = @numLumpSumToPay, numPensionToPay = @numPensionToPay  where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'populating application for additional lumpSum to be sent through rmas
				Case Is = 14
					sql = "update awbr100 set  numApprovedArrears = @numApprovedArrears, numLumpSumToPay = @numLumpSumToPay, numPensionToPay = @numPensionToPay  where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'populating application for Annuity to be sent through rmas
				Case Is = 4

					sql = "update awbr700 set  numLumpSumToPay = @numLumpSumToPay, numAnnuityToPay = @numAnnuityToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'populating application for additional Annuity to be sent through rmas
				Case Is = 15

					sql = "update awbr700 set  numLumpSumToPay = @numLumpSumToPay, numAnnuityToPay = @numAnnuityToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"

					'populating application for Death Benefit to be sent through rmas
				Case Is = 5

					sql = "update awbr600 set  numApprovedAmount = @numApprovedAmount, numAmountToPay = @numAmountToPay where [pin-dba] = @pin and txtApplicationCode = @txtApplicationCode"

					'populating application for NSITF to be sent through rmas
				Case Is = 6

					sql = "update awbr200 set  numAmountToPay = @numAmountToPay where pin = @pin and txtApplicationCode = @txtApplicationCode"

			End Select


			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pin").Value = appDetails.PIN


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = appDetails.ApplicationCode

			If appDetails.AppTypeID = 4 Then

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numLumpSumToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numLumpSumToPay").Value = appDetails.LumpSumToPay


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numAnnuityToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numAnnuityToPay").Value = appDetails.AnnuityToPay

			ElseIf appDetails.AppTypeID = 15 Then

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numLumpSumToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numLumpSumToPay").Value = appDetails.LumpSumToPay


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numAnnuityToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numAnnuityToPay").Value = appDetails.AnnuityToPay

			ElseIf appDetails.AppTypeID = 3 Then

				'sql = "update awbr100 set  numApprovedArrears = @numApprovedArrears, numLumpSumToPay = @numLumpSumToPay, numPensionToPay = @numPensionToPay  where pin = @pin and txtApplicationCode = @txtApplicationCode"

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numApprovedArrears", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numApprovedArrears").Value = appDetails.ArearsToPay


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numLumpSumToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numLumpSumToPay").Value = appDetails.LumpSumToPay


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numPensionToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numPensionToPay").Value = appDetails.MonthlyDrawndownToPay

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numApprovedPension", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numApprovedPension").Value = appDetails.MonthlyDrawndown

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numApprovedLumpSum", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numApprovedLumpSum").Value = appDetails.ApprovedAmount

			ElseIf appDetails.AppTypeID = 5 Then


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numApprovedAmount", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numApprovedAmount").Value = appDetails.ApprovedAmount

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numAmountToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numAmountToPay").Value = appDetails.AmountToPay

			ElseIf appDetails.AppTypeID = 11 Then


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numApprovedAmount", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numApprovedAmount").Value = appDetails.ApprovedAmount

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numAmountToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numAmountToPay").Value = appDetails.AmountToPay

			Else


				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numAmountToPay", SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@numAmountToPay").Value = appDetails.AmountToPay

			End If



			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "RMAS")
			'dtUser = dsUser.Tables("RMAS")

			mycon.Close()


		Catch ex As Exception

		End Try
	End Sub
	'SI getting the signature for the signatory
	Public Function PMSIgetApprovalSignature(ByVal uName As String) As Byte()
		Try
			Dim conn As String = ConfigurationManager.ConnectionStrings("PaymentModule").ConnectionString
			Dim connection As SqlConnection = New SqlConnection(conn)
			Dim sql As String = "select imgSignature from tblusers where username = @uName"
			Dim cmd As SqlCommand = New SqlCommand(sql, connection)
			cmd.CommandType = CommandType.Text
			cmd.CommandTimeout = 2000
			cmd.Parameters.AddWithValue("@uName", uName)
			connection.Open()
			'Dim img As Object = cmd.ExecuteScalar()
			'Return New MemoryStream(CType(img, Byte()))

			Dim barrImg As Byte() = cmd.ExecuteScalar()
			Return barrImg
			connection.Close()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			'logerr.FilePath = errlog
			logerr.Logger(ex.Message & ": Error Loading Biometric")

			Return Nothing
		Finally

		End Try
	End Function

	Public Function PMgetParticipantSignature(ByVal PIN As String) As Byte()
		Try
			Dim conn As String = ConfigurationManager.ConnectionStrings("EnpowerV4").ConnectionString
			Dim connection As SqlConnection = New SqlConnection(conn)
			Dim sql As String = "select Signature from dbo.Biometric a, employee b where a.employeeid = b.employeeid and rsapin = @PIN"
			Dim cmd As SqlCommand = New SqlCommand(sql, connection)
			cmd.CommandType = CommandType.Text
			cmd.CommandTimeout = 2000
			cmd.Parameters.AddWithValue("@PIN", PIN)
			connection.Open()
			'Dim img As Object = cmd.ExecuteScalar()
			'Return New MemoryStream(CType(img, Byte()))

			Dim barrImg As Byte() = cmd.ExecuteScalar()
			Return barrImg
			connection.Close()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			'logerr.FilePath = errlog
			logerr.Logger(ex.Message & ": Error Loading Biometric")

			Return Nothing
		Finally

		End Try
	End Function


	Public Function PMgetParticipantPassport(ByVal PIN As String) As Byte()
		Try
			Dim conn As String = ConfigurationManager.ConnectionStrings("EnpowerV4").ConnectionString
			Dim connection As SqlConnection = New SqlConnection(conn)
			Dim sql As String = "select Picture from dbo.Biometric a, employee b where a.employeeid = b.employeeid and rsapin = @PIN"
			Dim cmd As SqlCommand = New SqlCommand(sql, connection)
			cmd.CommandType = CommandType.Text
			cmd.CommandTimeout = 2000
			cmd.Parameters.AddWithValue("@PIN", PIN)
			connection.Open()
			'Dim img As Object = cmd.ExecuteScalar()
			'Return New MemoryStream(CType(img, Byte()))

			Dim barrImg As Byte() = cmd.ExecuteScalar()
			Return barrImg
			connection.Close()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			'logerr.FilePath = errlog
			logerr.Logger(ex.Message & ": Error Loading Biometric")

			Return Nothing
		Finally

		End Try
	End Function

	Public Sub PMUpdateHardShipRMASData(RSABalance As Double, TwentyFivePercent As Double, pin As String, UName As String)

		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection, sql As String
			mycon = db.getConnection("PaymentModule")
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter


			sql = "update awbr500 set [rsa-balance] = @RSABalance, [twentyfive-percent-rsa-balance] = @TwentyFivePercent,[txtConfirmedBy] = @UName  where [pin] = @pin"

			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@RSABalance", SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@RSABalance").Value = RSABalance

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@TwentyFivePercent", SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@TwentyFivePercent").Value = TwentyFivePercent

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@UName", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@UName").Value = UName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pin").Value = pin

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			mycon.Close()

		Catch ex As Exception

		End Try

	End Sub


	Public Function PMgetRMASData(sql As String, AppType As Integer, d_ate As Date) As DataTable
		Try

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("PaymentModule")

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter, dsUser As DataSet, dtUser As DataTable

			Select Case AppType
				'populating application for 25% to be sent through rmas
				Case Is = 2
					sql = "select [pin],[employer-code] ,[gender] ,[birth-date],[disengagement-date] ,[rsa-balance] ,[twentyfive-percent-rsa-balance],[date-sent],isnull(blnConfirm,0) blnConfirm 	from awbr500 where [date-sent] = @dataSent"
					'populating application for enbloc to be sent through rmas
				Case Is = 1

					sql = "SELECT [pin],[employer-code],[nationality],[gender],[birth-date],[retirement-date],[reason-for-payment],[rsa-balance],[enbloc-payment],[date-Sent],isnull(blnConfirm,0) blnConfirm from awbr400 where [date-sent] = @dataSent"

					'populating application for additional enbloc to be sent through rmas
				Case Is = 16

					sql = "SELECT [pin],[employer-code],[nationality],[gender],[birth-date],[retirement-date],[reason-for-payment],[rsa-balance],[enbloc-payment],[date-Sent],isnull(blnConfirm,0) blnConfirm from awbr400 where [date-sent] = @dataSent"

					'populating application for legacy payment to be sent through rmas
				Case Is = 8

					sql = "SELECT [pin],[employer-code],[retirement-date],[enbloc-payment],[date-Sent],isnull(blnConfirm,0) blnConfirm from awbr300 where [date-sent] = @dataSent"
					'populating application for AVC payment to be sent through rmas
				Case Is = 7

					sql = "SELECT [pin],[employer-code],[birth-date],[retirement-date],[total-voluntary-contribution],[total-amount],[amount-requested],[tax-deducted],[amount-payable-net-tax],[date-Sent],isnull(blnConfirm,0) blnConfirm from awbr800 where [date-sent] = @dataSent"

					'populating application for PW to be sent through rmas
				Case Is = 3
					sql = "SELECT [pin],[employer-code],[birth-date],[retirement-date],[retirement-age],[gender],[retirement-ground],[annual-total-emolument],[accrued-right],[rsa-balance],[recommended-lumpsum],[monthly-programed-drawndown],[dateSent],blnConfirm FROM [dbo].[awbr100] where [dateSent] = @dataSent"

					'populating application for additional lumpSum to be sent through rmas
				Case Is = 14
					sql = "SELECT [pin],[employer-code],[birth-date],[retirement-date],[retirement-age],[gender],[retirement-ground],[annual-total-emolument],[accrued-right],[rsa-balance],[recommended-lumpsum],[monthly-programed-drawndown],[dateSent],blnConfirm FROM [dbo].[awbr100] where [dateSent] = @dataSent"

					'populating application for Annuity to be sent through rmas
				Case Is = 4

					sql = "SELECT [pin],[employer-code],[birth-date],[retirement-date],[insurance-company-name],cast([annuity-commencement-date] as date) [annuity-commencement-date],[annual-total-emolument],[rsa-balance],[premium],[lumpsum],[monthly-annuity],[dateSent],blnConfirm  FROM [dbo].[awbr700] where [dateSent] = @dataSent"

					'populating application for additional Annuity to be sent through rmas
				Case Is = 15

					sql = "SELECT [pin],[employer-code],[birth-date],[retirement-date],[insurance-company-name],cast([annuity-commencement-date] as date) [annuity-commencement-date],[annual-total-emolument],[rsa-balance],[premium],[lumpsum],[monthly-annuity],[dateSent],blnConfirm  FROM [dbo].[awbr700] where [dateSent] = @dataSent"

					'populating application for Death Benefit to be sent through rmas
				Case Is = 5

					sql = "SELECT [name],[pin-dba],[gender],[employer-code],[retirement-date],[death-date],[administration-letter-issuing-authority],[administration-letter-date],[administration-letter-named-administrator-nok],[life-insurance-proceed],[accured-right],[contributions],[investment-income],[total-rsa-balance],[remark],[dateSent],[txtApplicationCode],blnConfirm  FROM [dbo].[awbr600]  where [dateSent] = @dataSent"

					'populating application for NSITF to be sent through rmas
				Case Is = 6

					sql = "SELECT [pin],[employer-code],[retirement-date],[initial-amount-paid-under-pra],[amount-recieved-nsitf-to-rsa],[amount-requested-under-nsitf-from-rsa],[dateSent],[txtApplicationCode],[blnConfirm]  FROM [dbo].[awbr200]  where [dateSent] = @dataSent"

			End Select


			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dataSent", SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@dataSent").Value = DateTime.Parse(d_ate).ToString("yyyy-MM-dd")
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "RMAS")
			dtUser = dsUser.Tables("RMAS")

			mycon.Close()

			Return dtUser

		Catch ex As Exception
			MsgBox("" & ex.Message)
		End Try

	End Function


	Public Sub PMApplicationRollBackStatus(applicationCode As String, Status As String, UName As String)



		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		If applicationCode <> "" And Status <> "" Then


			Try

				Dim MyDataAdapter As New SqlClient.SqlDataAdapter
				Select Case Status
					Case Is = "Documentation"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Processing"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Confirmation"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Send to Pencom"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Approved/Processing"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Paid"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Terminated"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
				End Select


				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = Status

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dte", _
				    SqlDbType.Date))
				MyDataAdapter.SelectCommand.Parameters("@dte").Value = DateTime.Parse(Now)

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLastChangedPerson", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtLastChangedPerson").Value = UName
				MyDataAdapter.SelectCommand.ExecuteNonQuery()
				mycon.Close()


			Catch Ex As Exception
				'MsgBox("" & Ex.Message)
			Finally

			End Try

		Else
			Exit Sub
		End If
	End Sub


	Public Sub PMSetApplicationStatus(applicationCode As String, Status As String, UName As String)



		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		If applicationCode <> "" And Status <> "" Then

			Try

				Dim MyDataAdapter As New SqlClient.SqlDataAdapter
				Select Case Status


					Case Is = "Entry"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Entry',dteSentToEntry = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "UnFunded"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'UnFunded',txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Documentation"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Documentation',txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Processing"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Processing',dteProcessing = @dte,txtLastChangedPerson = @txtLastChangedPerson,dteLocked = null,txtLockedBy = null where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Confirmation"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Confirmation',dteConfirmed = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Send to Pencom"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Send To Pencom',dteSendtoPencom = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Sent to Pencom"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Sent To Pencom',dteSentToPencom = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Approved/Processing"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Approved/Processing',dteApproved = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = "Paid"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Paid',dtePaid = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
					Case Is = "Terminated"
						MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = 'Terminated',dteTerminated = @dte,txtLastChangedPerson = @txtLastChangedPerson where txtApplicationCode = @txtApplicationCode", mycon)
				End Select


				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = Status

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dte", _
				    SqlDbType.DateTime))
				MyDataAdapter.SelectCommand.Parameters("@dte").Value = DateTime.Parse(Now)	'.ToString("yyyy-MM-dd")

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLastChangedPerson", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtLastChangedPerson").Value = UName
				MyDataAdapter.SelectCommand.ExecuteNonQuery()
				mycon.Close()


			Catch Ex As Exception
				'MsgBox("" & Ex.Message)
			Finally

			End Try

		Else
			Exit Sub
		End If
	End Sub

	Public Function PMgetApplicationDetails(AppCode As String, PIN As String) As List(Of ApplicationProperties)

		Dim ApplicationProperties As New List(Of ApplicationProperties), dt As DataTable, dtPDetails As DataTable, cr As New Core
		dt = PMgetApplicationByCode(AppCode)
		dtPDetails = getPMPersonInformation(PIN)
		Try

			'0

			Dim ApplicationProperty As New ApplicationProperties
			ApplicationProperty.FieldName = "Fund Type :"
			ApplicationProperty.FieldValue = "Fund II"
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Application Stage/Status :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtStatus").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Created By :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtCreatedBy").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Reviewed By :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("ReviewedBy").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Processed By :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("ProcessedBy").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Confirmed By :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("ConfirmedBy").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Control Checked By :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtControlCheckedBy").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Date Sent To Pencom :"
			If dt.Rows(0).Item("dteSentToPencom").ToString = "" Then
				ApplicationProperty.FieldValue = ""
			Else
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteSentToPencom").ToString.Substring(0, 10)
			End If
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Approved Date :"
			If dt.Rows(0).Item("ApprovalDate").ToString = "" Then
				ApplicationProperty.FieldValue = ""
			Else
				ApplicationProperty.FieldValue = dt.Rows(0).Item("ApprovalDate").ToString.Substring(0, 10)
			End If
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Acknowledgment Date :"

			If dt.Rows(0).Item("AcknowledgmentDate").ToString = "" Then
				ApplicationProperty.FieldValue = ""
			Else
				ApplicationProperty.FieldValue = dt.Rows(0).Item("AcknowledgmentDate").ToString.Substring(0, 10)
			End If
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Date Paid :"

			If dt.Rows(0).Item("dtePaid").ToString = "" Then
				ApplicationProperty.FieldValue = ""
			Else
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dtePaid").ToString.Substring(0, 10)
			End If

			ApplicationProperties.Add(ApplicationProperty)



			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Bank Payment Date :"

			If dt.Rows(0).Item("dteBankPayment").ToString = "" Then
				ApplicationProperty.FieldValue = ""
			Else
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteBankPayment").ToString.Substring(0, 10)
			End If

			ApplicationProperties.Add(ApplicationProperty)






			'
			'0
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "File Number :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtFileNo").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Shelve Number :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtShelveNumber").ToString
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Is HardCopy Doc Recieved :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("isHardDocRecieved").ToString
			ApplicationProperties.Add(ApplicationProperty)

			'0
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Application Date :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
			ApplicationProperties.Add(ApplicationProperty)

			'ToString("yyyy-MM-dd")
			'intFundPlatFormID
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Fund PlatForm :"

			If dt.Rows(0).Item("intFundPlatFormID").ToString = "1" Then
				ApplicationProperty.FieldValue = "RSA Fund"
			ElseIf dt.Rows(0).Item("intFundPlatFormID").ToString = "2" Then
				ApplicationProperty.FieldValue = "Retiree Fund"
			Else
				ApplicationProperty.FieldValue = "Not Defined"
			End If

			ApplicationProperties.Add(ApplicationProperty)





			'1
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Age :"
			ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Declared Age :"
			If dt.Rows(0).Item("intDeclaredAge") = 0 Then
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
			Else
				ApplicationProperty.FieldValue = dt.Rows(0).Item("intDeclaredAge")
			End If
			ApplicationProperties.Add(ApplicationProperty)

			'2
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Date Of Birth :"
			ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString
			ApplicationProperties.Add(ApplicationProperty)

			'7 

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Set Price Date :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("dteConfirmPriceDate").ToString
			ApplicationProperties.Add(ApplicationProperty)

			'11
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Employer Code :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtEmployerCode").ToString
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Application State :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationState").ToString
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Application Location :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationOffice").ToString
			ApplicationProperties.Add(ApplicationProperty)




			If CInt(dt.Rows(0).Item("fkiAppTypeId")) = 2 Then

				'3
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Department :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtDepartment").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'4
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Designation :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtDesignation").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'5
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Reason For Exit :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtReason").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'6
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date of Exit :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteDisengagement").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)

				'8



				Dim Customerdt As New DataTable, Mandatory As Double
				Customerdt = cr.PMgetApplicationByPIN(PIN, AppCode)
				If Customerdt.Rows(0).Item("intFundPlatFormID") = 1 Then
					Mandatory = Customerdt.Rows(0).Item("Mandatory")

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "RSA Balance :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(Mandatory).ToString("#,##0.00") '.ToString("#,000.00")
					ApplicationProperties.Add(ApplicationProperty)

				ElseIf Customerdt.Rows(0).Item("intFundPlatFormID") = 2 Then
					Mandatory = Customerdt.Rows(0).Item("RFBalance")

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "RF Balance :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(Mandatory).ToString("#,##0.00") '.ToString("#,000.00")
					ApplicationProperties.Add(ApplicationProperty)
				Else

					Mandatory = Customerdt.Rows(0).Item("Mandatory")

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "RSA Balance :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(Mandatory).ToString("#,##0.00") '.ToString("#,000.00")
					ApplicationProperties.Add(ApplicationProperty)

				End If


				'9
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "25% Payment :"
				'ApplicationProperty.FieldValue = Convert.ToDecimal((CDbl(dt.Rows(0).Item("numRSABalance")) / 4)).ToString("#,##0.00")  '.ToString("#,000.00")
				ApplicationProperty.FieldValue = Convert.ToDecimal((CDbl(Mandatory) / 4)).ToString("#,##0.00")	 '.ToString("#,000.00")
				ApplicationProperties.Add(ApplicationProperty)



			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 6 Then

				'3
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Initial NSITF Amount Paid :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numNSITFInitialAmountPaid")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				'4
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Amount Recieved into RSA  :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numNSITFRecievedToRSA")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				'5
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Amount Requested into RSA :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numNSITFRequestedToRSA")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 11 Then

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance:"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numRSABalance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 1 Then

				'8
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("RSABalance")).ToString("#,##0.00") '.ToString("#,000.00")
				ApplicationProperties.Add(ApplicationProperty)

				'8
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RF Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("RFBalance")).ToString("#,##0.00")	'.ToString("#,000.00")
				ApplicationProperties.Add(ApplicationProperty)

				'6
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date of Retirement :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteDOR").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				If IsDBNull(dt.Rows(0).Item("dteConfirmPriceDate")) = False Then

					'6
					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Fund Platform :"
					If dt.Rows(0).Item("intFundPlatFormID").ToString = 1 Then
						ApplicationProperty.FieldValue = "RSA"
					ElseIf dt.Rows(0).Item("intFundPlatFormID").ToString = 2 Then
						ApplicationProperty.FieldValue = "Retiree"
					Else
					End If
					ApplicationProperties.Add(ApplicationProperty)


					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Enbloc Amount :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numApplicationAmount")).ToString("#,##0.00")
					ApplicationProperties.Add(ApplicationProperty)


				Else
				End If



			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 16 Then

				'8
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("RSABalance")).ToString("#,##0.00") '.ToString("#,000.00")
				ApplicationProperties.Add(ApplicationProperty)

				'8
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RF Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("RFBalance")).ToString("#,##0.00")	'.ToString("#,000.00")
				ApplicationProperties.Add(ApplicationProperty)

				'6
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date of Retirement :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteDOR").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				If IsDBNull(dt.Rows(0).Item("dteConfirmPriceDate")) = False Then

					'6
					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Fund Platform :"
					If dt.Rows(0).Item("intFundPlatFormID").ToString = 1 Then
						ApplicationProperty.FieldValue = "RSA"
					ElseIf dt.Rows(0).Item("intFundPlatFormID").ToString = 2 Then
						ApplicationProperty.FieldValue = "Retiree"
					Else
					End If
					ApplicationProperties.Add(ApplicationProperty)


					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Enbloc Amount :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numApplicationAmount")).ToString("#,##0.00")
					ApplicationProperties.Add(ApplicationProperty)


				Else
				End If



			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 8 Then

				' If IsDBNull(dt.Rows(0).Item("LagacyContValueDate")) = False Then
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Legacy ValueDate :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("LagacyContValueDate").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Legacy Amount :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("TotalLegacyAmount")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Legacy Total Unit :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("TotalLegacyUnit")).ToString("###0.0000")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Legacy Unit Price :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtPDetails.Rows(0).Item("LagacyContUnitPrice")).ToString("###0.0000")
				ApplicationProperties.Add(ApplicationProperty)

				If IsDBNull(dt.Rows(0).Item("dteConfirmPriceDate")) = False Then

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Legacy Paying Price :"
					ApplicationProperty.FieldValue = PMUnitPriceByDate(dt.Rows(0).Item("dteConfirmPriceDate"), 1)
					ApplicationProperties.Add(ApplicationProperty)

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Legacy Amt Paid :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numApplicationAmount")).ToString("#,##0.00")
					ApplicationProperties.Add(ApplicationProperty)

				Else
					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Legacy Paying Price :"
					ApplicationProperty.FieldValue = "0.0000"
					ApplicationProperties.Add(ApplicationProperty)

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "Legacy Amt Paid :"
					ApplicationProperty.FieldValue = "0.00"
					ApplicationProperties.Add(ApplicationProperty)
				End If

				'MsgBox("" & CInt(dt.Rows(0).Item("fkiAppTypeId")))

			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 7 And PMGetInsertedTempRMASRecord(AppCode).Rows.Count > 0 Then

				Dim dtemp As DataTable = PMGetInsertedTempRMASRecord(AppCode)

				'0
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "TAX Identification Number :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtTIN").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Taxable Total AVC :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtAVCProcessed")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Non-Taxable Total AVC :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtNoTaxAVCProcessed")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Total AVC Units :"
				ApplicationProperty.FieldValue = (Convert.ToDecimal(dtemp.Rows(0).Item("txtAVCProcessedUnit")) + Convert.ToDecimal(dtemp.Rows(0).Item("txtNoTaxAVCProcessedUnit"))).ToString("###0.0000")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Avg. AVC Units :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtAvgPrice")).ToString("###0.0000")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Paying AVC Unit Price :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtPaymentPrice")).ToString("###0.0000")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Current AVC Values :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtCurrentValue")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "AVC Tax Deduction :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtTaxDeduction")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Net AVC Payable :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dtemp.Rows(0).Item("txtNetPayable")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)





			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 14 And PMGetInsertedRetirementRecord(AppCode).Rows.Count > 0 Then

				Dim dRetirement As DataTable = PMGetInsertedRetirementRecord(AppCode)

				ApplicationProperties.Clear()


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund Type :"
				ApplicationProperty.FieldValue = "Fund II"
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Stage/Status :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtStatus").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Created By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtCreatedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Reviewed By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("ReviewedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Sent To Pencom :"
				If dt.Rows(0).Item("dteSentToPencom").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dteSentToPencom").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Approved Date :"
				If dt.Rows(0).Item("ApprovalDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("ApprovalDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Acknowledgment Date :"

				If dt.Rows(0).Item("AcknowledgmentDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("AcknowledgmentDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Paid :"

				If dt.Rows(0).Item("dtePaid").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dtePaid").ToString.Substring(0, 10)
				End If

				ApplicationProperties.Add(ApplicationProperty)



				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "File Number :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtFileNo").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'0
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Date :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund PlatForm :"
				If dt.Rows(0).Item("intFundPlatFormID") = 1 Then
					ApplicationProperty.FieldValue = "RSA Fund"
				ElseIf dt.Rows(0).Item("intFundPlatFormID") = 2 Then
					ApplicationProperty.FieldValue = "Retiree Fund"
				Else
					ApplicationProperty.FieldValue = "Not Defined"
				End If
				ApplicationProperties.Add(ApplicationProperty)



				'1
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Age :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Declared Age :"
				If dt.Rows(0).Item("intDeclaredAge") = 0 Then
					ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("intDeclaredAge")
				End If
				ApplicationProperties.Add(ApplicationProperty)

				'2
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Of Birth :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)

				'7 

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Set Price Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dtePriceDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				'11
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Employer Code :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtEmployerCode").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application State :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationState").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Location :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationOffice").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Basic Salary :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numBasicSalary")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "House Rent :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numHouseRent")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Transport :"
				ApplicationProperty.FieldValue = (Convert.ToDecimal(dRetirement.Rows(0).Item("numTransport")))
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Utility :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numUtility")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Consolidated Allowance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numConsolidatedAallowance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Monthly Total :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyTotal")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Annual Total Emolument :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAnnualTotalEmolumentAdj")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Accrued Right :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAccruedRight")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRSABalance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Recommended LumpSum :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRecommendedLumpSum")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Monthly DrowDown :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyDrowDown")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				'numRecommendedLumpSum








			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 3 And PMGetInsertedRetirementRecord(AppCode).Rows.Count > 0 Then

				Dim dRetirement As DataTable = PMGetInsertedRetirementRecord(AppCode)




				ApplicationProperties.Clear()


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund Type :"
				ApplicationProperty.FieldValue = "Fund II"
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Stage/Status :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtStatus").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Created By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtCreatedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Reviewed By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("ReviewedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Sent To Pencom :"
				If dt.Rows(0).Item("dteSentToPencom").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dteSentToPencom").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Approved Date :"
				If dt.Rows(0).Item("ApprovalDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("ApprovalDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Acknowledgment Date :"

				If dt.Rows(0).Item("AcknowledgmentDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("AcknowledgmentDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Paid :"

				If dt.Rows(0).Item("dtePaid").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dtePaid").ToString.Substring(0, 10)
				End If

				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "File Number :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtFileNo").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'0
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Date :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties

				ApplicationProperty.FieldName = "Fund PlatForm :"
				If dt.Rows(0).Item("intFundPlatFormID") = 1 Then
					ApplicationProperty.FieldValue = "RSA Fund"
				ElseIf dt.Rows(0).Item("intFundPlatFormID") = 2 Then
					ApplicationProperty.FieldValue = "Retiree Fund"
				Else
					ApplicationProperty.FieldValue = "Not Defined"
				End If
				ApplicationProperties.Add(ApplicationProperty)



				'1
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Age :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Declared Age :"
				If dt.Rows(0).Item("intDeclaredAge") = 0 Then
					ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("intDeclaredAge")
				End If
				ApplicationProperties.Add(ApplicationProperty)

				'2
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Of Birth :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)

				'7 

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Set Price Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dtePriceDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Of Retirement :"
				ApplicationProperty.FieldValue = DateTime.Parse((dt.Rows(0).Item("dteDOR"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Retirement Reason :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtReason").ToString()
				ApplicationProperties.Add(ApplicationProperty)

				'11
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Employer Code :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtEmployerCode").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application State :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationState").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Location :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationOffice").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Basic Salary :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numBasicSalary")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "House Rent :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numHouseRent")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Transport :"
				ApplicationProperty.FieldValue = (Convert.ToDecimal(dRetirement.Rows(0).Item("numTransport")))
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Utility :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numUtility")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Consolidated Allowance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numConsolidatedAallowance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Monthly Total :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyTotal")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Annual Total Emolument :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAnnualTotalEmolumentAdj")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Consolidated Salary :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numConsolidatedSalary")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Accrued Right :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAccruedRight")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRSABalance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Recommended LumpSum :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRecommendedLumpSum")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Monthly DrowDown :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyDrowDown")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				'numRecommendedLumpSum



			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 4 And PMGetInsertedRetirementRecord(AppCode).Rows.Count > 0 Then

				Dim dRetirement As DataTable = PMGetInsertedRetirementRecord(AppCode)

				ApplicationProperties.Clear()



				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund Type :"
				ApplicationProperty.FieldValue = "Fund II"
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Created By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtCreatedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Reviewed By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("ReviewedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Sent To Pencom :"
				If dt.Rows(0).Item("dteSentToPencom").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dteSentToPencom").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Approved Date :"
				If dt.Rows(0).Item("ApprovalDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("ApprovalDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Acknowledgment Date :"

				If dt.Rows(0).Item("AcknowledgmentDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("AcknowledgmentDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Paid :"

				If dt.Rows(0).Item("dtePaid").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dtePaid").ToString.Substring(0, 10)
				End If

				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Stage/Status :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtStatus").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "File Number :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtFileNo").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'0
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Date :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Processing Fund PlatForm :"
				If dt.Rows(0).Item("intFundPlatFormID") = 1 Then
					ApplicationProperty.FieldValue = "RSA Fund"
				ElseIf dt.Rows(0).Item("intFundPlatFormID") = 2 Then
					ApplicationProperty.FieldValue = "Retiree Fund"
				Else
					ApplicationProperty.FieldValue = "Not Defined"
				End If
				ApplicationProperties.Add(ApplicationProperty)



				'1
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Age :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Declared Age :"
				If dt.Rows(0).Item("intDeclaredAge") = 0 Then
					ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("intDeclaredAge")
				End If
				ApplicationProperties.Add(ApplicationProperty)

				'2
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Of Birth :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date of Retirement :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteDOR").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)

				'7 

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Set Price Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dtePriceDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				'11
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Employer Code :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtEmployerCode").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application State :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationState").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Location :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationOffice").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Basic Salary :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numBasicSalary")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "House Rent :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numHouseRent")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Transport :"
				ApplicationProperty.FieldValue = (Convert.ToDecimal(dRetirement.Rows(0).Item("numTransport")))
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Utility :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numUtility")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Consolidated Allowance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numConsolidatedAallowance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Monthly Total :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyTotal")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Annual Total Emolument :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAnnualTotalEmolumentAdj")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Insurance Company Name :"
				ApplicationProperty.FieldValue = (dRetirement.Rows(0).Item("InsurerName")).ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Annuity Commencement Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dteAnnuityCcommencementDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRSABalance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Premium :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numPremium")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "LumpSum :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numLumpSum")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "MonthlyAnnuity :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyAnnuity")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)






			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 15 And PMGetInsertedRetirementRecord(AppCode).Rows.Count > 0 Then

				Dim dRetirement As DataTable = PMGetInsertedRetirementRecord(AppCode)

				ApplicationProperties.Clear()


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund Type :"
				ApplicationProperty.FieldValue = "Fund II"
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Created By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtCreatedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Reviewed By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("ReviewedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Sent To Pencom :"
				If dt.Rows(0).Item("dteSentToPencom").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dteSentToPencom").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Approved Date :"
				If dt.Rows(0).Item("ApprovalDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("ApprovalDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Acknowledgment Date :"

				If dt.Rows(0).Item("AcknowledgmentDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("AcknowledgmentDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Paid :"

				If dt.Rows(0).Item("dtePaid").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dtePaid").ToString.Substring(0, 10)
				End If

				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Stage/Status :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtStatus").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "File Number :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtFileNo").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'0
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Date :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund PlatForm :"
				If dt.Rows(0).Item("intFundPlatFormID") = 1 Then
					ApplicationProperty.FieldValue = "RSA Fund"
				ElseIf dt.Rows(0).Item("intFundPlatFormID") = 2 Then
					ApplicationProperty.FieldValue = "Retiree Fund"
				Else
					ApplicationProperty.FieldValue = "Not Defined"
				End If
				ApplicationProperties.Add(ApplicationProperty)



				'1
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Age :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Declared Age :"
				If dt.Rows(0).Item("intDeclaredAge") = 0 Then
					ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("intDeclaredAge")
				End If
				ApplicationProperties.Add(ApplicationProperty)

				'2
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Of Birth :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)

				'7 

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Set Price Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dtePriceDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				'11
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Employer Code :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtEmployerCode").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application State :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationState").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Location :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationOffice").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Basic Salary :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numBasicSalary")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "House Rent :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numHouseRent")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Transport :"
				ApplicationProperty.FieldValue = (Convert.ToDecimal(dRetirement.Rows(0).Item("numTransport")))
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Utility :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numUtility")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Consolidated Allowance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numConsolidatedAallowance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Monthly Total :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyTotal")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Annual Total Emolument :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAnnualTotalEmolumentAdj")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Insurance Company Name :"
				ApplicationProperty.FieldValue = (dRetirement.Rows(0).Item("InsurerName")).ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Annuity Commencement Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dteAnnuityCcommencementDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "RSA Balance :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRSABalance")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Premium :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numPremium")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "LumpSum :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numLumpSum")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "MonthlyAnnuity :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numMonthlyAnnuity")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)







			ElseIf CInt(dt.Rows(0).Item("fkiAppTypeId")) = 5 And PMGetInsertedDBARecord(AppCode).Rows.Count > 0 Then

				Dim dRetirement As DataTable = PMGetInsertedDBARecord(AppCode)

				ApplicationProperties.Clear()


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Fund Type :"
				ApplicationProperty.FieldValue = "Fund II"
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Created By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtCreatedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Reviewed By :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("ReviewedBy").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Sent To Pencom :"
				If dt.Rows(0).Item("dteSentToPencom").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dteSentToPencom").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Approved Date :"
				If dt.Rows(0).Item("ApprovalDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("ApprovalDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Acknowledgment Date :"

				If dt.Rows(0).Item("AcknowledgmentDate").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("AcknowledgmentDate").ToString.Substring(0, 10)
				End If
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Paid :"

				If dt.Rows(0).Item("dtePaid").ToString = "" Then
					ApplicationProperty.FieldValue = ""
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("dtePaid").ToString.Substring(0, 10)
				End If

				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "File No :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtFileNo").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Stage/Status :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtStatus").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'0
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Date :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("dteApplicationDate").ToString.Substring(0, 10)
				ApplicationProperties.Add(ApplicationProperty)



				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Processing Fund PlatForm :"

				If dt.Rows(0).Item("intFundPlatFormID").ToString = "1" Then
					ApplicationProperty.FieldValue = "RSA Fund"
				ElseIf dt.Rows(0).Item("intFundPlatFormID").ToString = "2" Then
					ApplicationProperty.FieldValue = "Retiree Fund"
				Else
					ApplicationProperty.FieldValue = "Not Defined"
				End If
				ApplicationProperties.Add(ApplicationProperty)


				'1
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Age :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Declared Age :"
				If dt.Rows(0).Item("intDeclaredAge") = 0 Then
					ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("Age")
				Else
					ApplicationProperty.FieldValue = dt.Rows(0).Item("intDeclaredAge")
				End If
				ApplicationProperties.Add(ApplicationProperty)

				'2
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Date Of Birth :"
				ApplicationProperty.FieldValue = dtPDetails.Rows(0).Item("dateofbirth").ToString
				ApplicationProperties.Add(ApplicationProperty)

				'7 

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Set Price Date :"
				ApplicationProperty.FieldValue = DateTime.Parse((dRetirement.Rows(0).Item("dtePriceDate"))).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				'11
				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Employer Code :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtEmployerCode").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application State :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationState").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Application Location :"
				ApplicationProperty.FieldValue = dt.Rows(0).Item("txtApplicationOffice").ToString
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Retirement Date :"
				ApplicationProperty.FieldValue = Convert.ToDateTime(dRetirement.Rows(0).Item("dteRetirement")).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Death Date :"
				ApplicationProperty.FieldValue = Convert.ToDateTime(dRetirement.Rows(0).Item("dteDeath")).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)


				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Admin. Letter Authority :"
				ApplicationProperty.FieldValue = dRetirement.Rows(0).Item("txtAdminLetterAuthority").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Admin. Letter Date :"
				ApplicationProperty.FieldValue = Convert.ToDateTime(dRetirement.Rows(0).Item("dteAdminLetter")).ToString("yyyy-MM-dd")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Admin. NOK Name :"
				ApplicationProperty.FieldValue = dRetirement.Rows(0).Item("txtAdminNOK").ToString
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Insurance Proceed :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numInsuranceProceed")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Accrued Right :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numAccruedRight")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Contribution :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numContribution")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				ApplicationProperty = New ApplicationProperties
				ApplicationProperty.FieldName = "Investment Income :"
				ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numInvestmentIncome")).ToString("#,##0.00")
				ApplicationProperties.Add(ApplicationProperty)

				'ApplicationProperty = New ApplicationProperties
				'ApplicationProperty.FieldName = "RSA Balance :"
				'ApplicationProperty.FieldValue = Convert.ToDecimal(dRetirement.Rows(0).Item("numRSABalance")).ToString("#,##0.00")
				'ApplicationProperties.Add(ApplicationProperty)


				If dt.Rows(0).Item("intFundPlatFormID").ToString = "1" Then

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "RSA Balance :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numApplicationAmount")).ToString("#,##0.00")
					ApplicationProperties.Add(ApplicationProperty)

				ElseIf dt.Rows(0).Item("intFundPlatFormID").ToString = "2" Then

					ApplicationProperty = New ApplicationProperties
					ApplicationProperty.FieldName = "RF Balance :"
					ApplicationProperty.FieldValue = Convert.ToDecimal(dt.Rows(0).Item("numApplicationAmount")).ToString("#,##0.00")
					ApplicationProperties.Add(ApplicationProperty)

				Else
				End If



			End If

			'12
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Account Name :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtAccountName").ToString
			ApplicationProperties.Add(ApplicationProperty)

			'12
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "AccountNo :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("txtAccountNo").ToString
			ApplicationProperties.Add(ApplicationProperty)

			'13
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Bank Name :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("BankName").ToString
			ApplicationProperties.Add(ApplicationProperty)

			'14
			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Branch Name :"
			ApplicationProperty.FieldValue = dt.Rows(0).Item("BranchName").ToString
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Is Funding Status Checked ? :"
			If dt.Rows(0).Item("isFundingStatusChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isFundingStatusChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Is RSA Lagacy/AVC Checked ? :"
			If dt.Rows(0).Item("isLegAVCChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isLegAVCChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If
			ApplicationProperties.Add(ApplicationProperty)


			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Is DOB Comfirmed ? :"
			If dt.Rows(0).Item("isDOBChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isDOBChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Are Names Verified ? :"
			If dt.Rows(0).Item("isNamesChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isNamesChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Are Exit Docs Verified ? :"
			If dt.Rows(0).Item("isExitDocChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isExitDocChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Is Data Entry Verified ? :"
			If dt.Rows(0).Item("isDataEntryChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isDataEntryChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If
			ApplicationProperties.Add(ApplicationProperty)

			ApplicationProperty = New ApplicationProperties
			ApplicationProperty.FieldName = "Is Correct Docs Uploaded ? :"
			If dt.Rows(0).Item("isValidDocChecked").ToString = "" Then
				ApplicationProperty.FieldValue = False
			ElseIf CInt(dt.Rows(0).Item("isValidDocChecked").ToString) = 1 Then
				ApplicationProperty.FieldValue = True
			Else
				ApplicationProperty.FieldValue = False
			End If

			ApplicationProperties.Add(ApplicationProperty)





			Return ApplicationProperties


		Catch ex As Exception
			MsgBox("" & ex.Message)

		End Try

	End Function

	Public Sub PMDeleteApplication(applicationCode As String, UName As String)

		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set IsDeactivated = 1,dteDeactivated = @dteDeactivated,txtDeactivatedBy = @txtDeactivatedBy where txtApplicationCode = @txtApplicationCode", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteDeactivated", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteDeactivated").Value = DateTime.Parse(Now)   '.ToString("yyyy-MM-dd")

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtDeactivatedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtDeactivatedBy").Value = UName
			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			mycon.Close()


		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub

	Public Sub PMSetApplicationStatus(applicationCode As String, Status As String, type As Integer, UName As String)



		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")
		If applicationCode <> "" And Status <> "" Then


			Try

				Dim MyDataAdapter As SqlClient.SqlDataAdapter

				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set txtStatus = @txtStatus,txtLastChangedPerson = @txtLastChangedPerson,dteStatusChange = @dteStatusChange where txtApplicationCode = @txtApplicationCode", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = Status

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteStatusChange", _
				    SqlDbType.Date))
				MyDataAdapter.SelectCommand.Parameters("@dteStatusChange").Value = DateTime.Parse(Now)	'.ToString("yyyy-MM-dd")

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLastChangedPerson", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtLastChangedPerson").Value = UName
				MyDataAdapter.SelectCommand.ExecuteNonQuery()
				mycon.Close()


			Catch Ex As Exception
				'MsgBox("" & Ex.Message)
			Finally

			End Try

		Else
			Exit Sub
		End If
	End Sub

	'getting the current balance as at the date in the input parameter for both RSA and Retiree Fund
	Public Sub PMSetPriceDate(applicationCode As String, ValueDate As Date, PIN As String, fundType As Integer, appProperties As List(Of ApplicationProperties))

		Dim nAmount, nRecieved, nRequest As Double
		Dim db As New DbConnection
		Dim i As New Integer
		Do While i < appProperties.Count

			If appProperties(i).FieldName = "Initial NSITF Amount Paid :" Then

				nAmount = appProperties(i).FieldValue

			ElseIf appProperties(i).FieldName = "Amount Recieved into RSA  :" Then

				nRecieved = appProperties(i).FieldValue

			ElseIf appProperties(i).FieldName = "Amount Requested into RSA :" Then

				nRequest = appProperties(i).FieldValue

			Else
			End If

			i = i + 1
		Loop


		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set dteConfirmPriceDate = @dteConfirmPriceDate,numNSITFInitialAmountPaid = @numNSITFInitialAmountPaid,numNSITFRecievedToRSA=@numNSITFRecievedToRSA,numNSITFRequestedToRSA=@numNSITFRequestedToRSA,intFundPlatFormID = @intFundPlatFormID where txtApplicationCode = @txtApplicationCode", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numNSITFInitialAmountPaid", _
			    SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@numNSITFInitialAmountPaid").Value = nAmount

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numNSITFRecievedToRSA", _
			    SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@numNSITFRecievedToRSA").Value = nRecieved


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numNSITFRequestedToRSA", _
			    SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@numNSITFRequestedToRSA").Value = nRequest


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intFundPlatFormID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intFundPlatFormID").Value = fundType


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteConfirmPriceDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@dteConfirmPriceDate").Value = DateTime.Parse(ValueDate).ToString("yyyy-MM-dd")

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			mycon.Close()




		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub

	'getting the current balance as at the date in the input parameter for both RSA and Retiree Fund
	Public Sub PMSetPriceDate(applicationCode As String, ValueDate As Date, PIN As String, fundType As Integer, benefitAmount As Double)

		Dim Value As Double
		Dim db As New DbConnection
		Value = PMValueByDate(PIN, ValueDate, fundType)
		'If Value = 0 Then
		'     Exit Sub
		'Else
		'End If

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set dteConfirmPriceDate = @dteConfirmPriceDate,numRSABalance = @numRSABalance, numApplicationAmount = @numApplicationAmount,intFundPlatFormID = @intFundPlatFormID where txtApplicationCode = @txtApplicationCode", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numApplicationAmount", _
			    SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@numApplicationAmount").Value = benefitAmount

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intFundPlatFormID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intFundPlatFormID").Value = fundType


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numRSABalance", _
			    SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@numRSABalance").Value = Value

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteConfirmPriceDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@dteConfirmPriceDate").Value = DateTime.Parse(ValueDate).ToString("yyyy-MM-dd")

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			mycon.Close()


		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub

	Public Function PMgetApprovalTypeByCode(appTypeCode As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT pkiAppTypeId, txtDescription FROM tblApplicationType where txtTypeCode  = @appTypeCode", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@appTypeCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@appTypeCode").Value = appTypeCode

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApprovalType")
			dtUser = dsUser.Tables("ApprovalType")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMgetBiometric(PIN As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("Biometric")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select t.txtIDNo ,t.[txtFirstname] , t.txtOtherNames, t.txtSurname, (isnull(t.txtResidAddr1,'') +' '+	isnull(t.txtResidAddr2,'') +' '+	isnull(t.txtResidAddr3,'') +' '+	isnull(t.txtResidCity,'') +' '+	isnull(t.txtResidCode,'')) Address,t.txtCellPhone ,q.fkiPersonID , e.imgRaw ,bt.[pkiEnrollmentBiometricTypeID] ,		bt.txtBiometricType from tblPeople  t join [tblQuickEnrollBiometricDetail]  q on t.txtIDNo = q.txtIDNo join [tblEnrolledBiometrics] e  on  e.fkiQuickEnrollBiometricDetailID = q.pkiQuickEnrollBiometricDetailID join    [tblEnrollmentBiometricTypes] bt on  e.fkiEnrollmentBiometricTypeID  = bt.pkiEnrollmentBiometricTypeID where t.txtIDNo = @txtIDNo", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtIDNo", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtIDNo").Value = PIN


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function


	Public Function PMBiometricImage(ByVal pin As String, ByVal imgNo As Integer) As Byte()
		Try
			'imgFMDMinutiae
			Dim conn As String = ConfigurationManager.ConnectionStrings("Biometric").ConnectionString
			Dim connection As SqlConnection = New SqlConnection(conn)
			Dim sql As String = "select  (e.imgRaw) imgRaw  from tblPeople  t join [tblQuickEnrollBiometricDetail]  q on t.txtIDNo = q.txtIDNo join [tblEnrolledBiometrics] e  on  e.fkiQuickEnrollBiometricDetailID = q.pkiQuickEnrollBiometricDetailID join    [tblEnrollmentBiometricTypes] bt on  e.fkiEnrollmentBiometricTypeID  = bt.pkiEnrollmentBiometricTypeID where t.txtIDNo = @txtIDNo and pkiEnrollmentBiometricTypeID = @pkiEnrollmentBiometricTypeID"

			'Dim sql As String = "select leftThumb from employee a,Biometric b where a.EmployeeID = b.EmployeeID and rsapin = @txtIDNo"


			'isnull(e.imgFMDMinutiae, e.imgRaw)

			Dim cmd As SqlCommand = New SqlCommand(sql, connection)
			cmd.CommandType = CommandType.Text
			cmd.CommandTimeout = 2000

			cmd.Parameters.AddWithValue("@txtIDNo", pin)
			cmd.Parameters.AddWithValue("@pkiEnrollmentBiometricTypeID", imgNo)

			connection.Open()

			Dim barrImg As Byte() = cmd.ExecuteScalar()
			Return barrImg
			connection.Close()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			'logerr.FilePath = errlog
			logerr.Logger(ex.Message & ": Error Loading Biometric")

			Return Nothing
		Finally

		End Try
	End Function


	Public Function PMBiometricImage2(ByVal pin As String, ByVal Field As String) As Byte()
		Try
			'imgFMDMinutiae
			Dim conn As String = ConfigurationManager.ConnectionStrings("EnpowerV4").ConnectionString
			Dim connection As SqlConnection = New SqlConnection(conn)
			'Dim sql As String = "select  (e.imgRaw) imgRaw  from tblPeople  t join [tblQuickEnrollBiometricDetail]  q on t.txtIDNo = q.txtIDNo join [tblEnrolledBiometrics] e  on  e.fkiQuickEnrollBiometricDetailID = q.pkiQuickEnrollBiometricDetailID join    [tblEnrollmentBiometricTypes] bt on  e.fkiEnrollmentBiometricTypeID  = bt.pkiEnrollmentBiometricTypeID where t.txtIDNo = @txtIDNo and pkiEnrollmentBiometricTypeID = @pkiEnrollmentBiometricTypeID"

			Dim sql As String = "select " & Field & " from employee a,Biometric b where a.EmployeeID = b.EmployeeID and rsapin = @txtIDNo"


			'isnull(e.imgFMDMinutiae, e.imgRaw)

			Dim cmd As SqlCommand = New SqlCommand(sql, connection)
			cmd.CommandType = CommandType.Text
			cmd.CommandTimeout = 2000

			cmd.Parameters.AddWithValue("@txtIDNo", pin)
			'cmd.Parameters.AddWithValue("@pkiEnrollmentBiometricTypeID", imgNo)

			connection.Open()

			Dim barrImg As Byte() = cmd.ExecuteScalar()
			Return barrImg
			connection.Close()

		Catch ex As Exception

			Dim logerr As New Global.Logger.Logger
			logerr.FileSource = "Payment Module"
			'logerr.FilePath = errlog
			logerr.Logger(ex.Message & ": Error Loading Biometric")

			Return Nothing
		Finally

		End Try
	End Function




	Public Function PMgetAnnuityPWSubmittedDoc(appCode As String, sector As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiMemberDocID,cast(dteReceived as date) dteReceived,(select txtDocumentName from tblDocumentType where pkiDocumentTypeID = fkiDocumentTypeID) txtDocumentName,fkiMemberApplicationID,fkiDocumentTypeID,txtDocumentPath,isVerified,fkiDocumentTypeID from [dbo].[tblMemberDocument] where txtApplicationCode = @txtApplicationCode and fkiDocumentTypeID in (select fkiDocumentTypeID from [dbo].[tblAppDocumentType] where apptypeid in (3,4) and txtSector = @txtSector group by fkiDocumentTypeID having count(fkiDocumentTypeID) > 1) ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = appCode

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtSector", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtSector").Value = sector

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function

	Public Function PMgetAnnuityPW(pin As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtapplicationcode from tblMemberApplication where txtPin = @PIN and dteDeactivated is null and fkiApptypeid = 3 ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = pin

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMgetCheckList(TypeID As Integer) As DataTable
		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select intErrorID,txtDescription from tblReturnErrorTypes where intAppTypeID = '" & TypeID & "'", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "CheckList")
			dtUser = dsUser.Tables("CheckList")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function



	Public Function PMgetApplicationPreference() As DataTable
		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from tblPreference", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationPreference")
			dtUser = dsUser.Tables("ApplicationPreference")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMgetApplicationDocumentsByCode(applicationCode As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.pkiMemberDocID,cast(a.dteReceived as date) dteReceived,b.txtDocumentName,a.fkiDocumentTypeID,a.fkiMemberApplicationID,(case when isnull(txtDocumentPath,'') = '' then txtDMSDocumentID else txtDocumentPath end) as txtDocumentPath,isVerified,txtDMSDocumentID,txtDMSDocumentExt from tblMemberDocument a,tblDocumentType b where a.IsDeactivated = 0 and pkiDocumentTypeID = fkiDocumentTypeID and exists (select pkiMemberApplicationID from tblMemberApplication where txtApplicationCode = fkiMemberApplicationID and txtApplicationCode = @txtApplicationCode)", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMgetApprovalPaymentSchedule(pencomBatch As String, typeID As Integer, uNITType As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			If uNITType = "A" Then



				Select Case typeID


					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr400 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)

					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr400 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)


					Case Is = 3

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedLumpSum numApproved,d.numLumpSumToPay as numToPay,isnull(d.numApprovedArrears,0) as Arrears ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate,d.[retirement-date] as DOR, isnull(d.numPensionToPay,0) as PensionToPay FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr100 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)


					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.premium numApproved,d.numLumpSumToPay as numToPay,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate,d.[retirement-date] as DOR FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,b.txtTIN,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay , d.numInterestAtPayment, d.numTaxAtPayment, b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select StateName  from enpowerv4.[dbo].[State] m , enpowerv4..employee n where StateID = n.ContactStateID and n.RSAPIN = b.txtPIN) as Location,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr800 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)

					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr600 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)


					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr200 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr300 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null ", mycon)


					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbrEEP D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null ", mycon)

					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr500 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null ", mycon)

					Case Else
						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,a.numApproved ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null or a.dteVerified is not null", mycon)

				End Select
				' a.txtPaymentRemarks as txtRemarks
				' and a.dteChecked is not null and a.dteVerified is not null

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pencomBatch", _
			  SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@pencomBatch").Value = pencomBatch

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = "E"












			ElseIf uNITType = "F" Then


				Select Case typeID


					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr400 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)

					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr400 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)


					Case Is = 3

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedLumpSum numApproved,d.numLumpSumToPay as numToPay,isnull(d.numApprovedArrears,0) as Arrears ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate,d.[retirement-date] as DOR, isnull(d.numPensionToPay,0) as PensionToPay FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr100 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)


					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.premium numApproved,d.numLumpSumToPay as numToPay,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate,d.[retirement-date] as DOR FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,b.txtTIN,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay , d.numInterestAtPayment, d.numTaxAtPayment, b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select StateName  from enpowerv4.[dbo].[State] m , enpowerv4..employee n where StateID = n.ContactStateID and n.RSAPIN = b.txtPIN) as Location,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr800 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)

					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr600 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)


					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr200 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr300 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null ", mycon)


					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbrEEP D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null ", mycon)

					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr500 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null ", mycon)

					Case Else
						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,a.numApproved ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) and a.dteChecked is not null and a.dteVerified is not null", mycon)

				End Select
				' a.txtPaymentRemarks as txtRemarks
				' and a.dteChecked is not null and a.dteVerified is not null

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pencomBatch", _
			  SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@pencomBatch").Value = pencomBatch

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = "E"


			End If


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApprovalSchedule")
			dtUser = dsUser.Tables("ApprovalSchedule")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function






	'generates schedule for extracted payment approval into enpower by application ID and by extracted batches

	Public Function PMgetApprovalPaymentSchedule(pencomBatch As String, appCode As String, typeID As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			If appCode <> "" Then

				Select Case typeID
					''''LPW
					Case Is = 3

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,d.numLumpSumToPay numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy ,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr100 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode union all SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,d.numApprovedArrears numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,a.txtCreatedBy,a.txtConfirmedBy,4 as fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr100 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)
						''Annuity
					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,d.numLumpSumToPay numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,1 fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode union all SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,d.premium numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,a.txtCreatedBy,a.txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)


					Case Is = 15

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,d.numLumpSumToPay numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,1 fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode union all SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,d.numAnnuityToPay numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,a.txtCreatedBy,a.txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)

						''enbloc
					Case Is = 1


						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr400 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)


					Case Is = 16


						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr400 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)


					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr500 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr800 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr300 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbrEEP d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)


					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr600 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)

					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT  b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(select case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' when b.intFundPlatFormID is null then 'GGF' end) as [PlatForm],c.dteApproval ,isnull(d.numAmountToPay,0) numApproved,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c,awbr200 d WHERE       a.txtApplicationCode = b.txtApplicationCode and a.txtApplicationCode = d.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode", mycon)

					Case Else

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,'RSA' as [PlatForm],c.dteApproval ,a.numApproved ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) +', '+ (select BranchName   from EnPowerV4.dbo.BankBranch  where BankBranchID = b.fkiBranchID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,(select isnull(PaymentTypeID_Enpower,0) from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) fkiAppTypeId, a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and b.txtApplicationCode = @txtApplicationCode ", mycon)

				End Select

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			  SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = appCode

			Else


				Select Case typeID

					'     Case Is = 2

					'          MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,a.numApproved ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,'' as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus)", mycon)

					Case Is = 1

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr400 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus)", mycon)


					Case Is = 16

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr400 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)


					Case Is = 3

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedLumpSum numApproved,d.numLumpSumToPay as numToPay,isnull(d.numApprovedArrears,0) as Arrears ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate,d.[retirement-date] as DOR, isnull(d.numPensionToPay,0) as PensionToPay FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr100 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)


					Case Is = 4

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.premium numApproved,d.numLumpSumToPay as numToPay,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate,d.[retirement-date] as DOR FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr700 d WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)

					Case Is = 7

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,b.txtTIN,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay , d.numInterestAtPayment, d.numTaxAtPayment, b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select StateName  from enpowerv4.[dbo].[State] m , enpowerv4..employee n where StateID = n.ContactStateID and n.RSAPIN = b.txtPIN) as Location,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr800 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)

					Case Is = 5

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr600 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)


					Case Is = 6

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr200 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)

					Case Is = 8

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr300 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)


					Case Is = 11

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbrEEP D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)

					Case Is = 2

						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,d.numApprovedAmount numApproved,d.numAmountToPay as numToPay ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c, awbr500 D WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch AND B.txtApplicationCode =  d.txtApplicationCode  and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus) ", mycon)

					Case Else
						MyDataAdapter = New SqlClient.SqlDataAdapter("SELECT     b.txtApplicationCode ,   replace(b.txtFullName,'|','') txtFullName, b.txtpin,b.txtSector,(case when b.intFundPlatFormID = 1 then 'RSA' when b.intFundPlatFormID = 2 then 'RF' end) [PlatForm],c.dteApproval ,a.numApproved ,b.txtAccountName ,(select BankName  from EnPowerV4.dbo.[Bank] where BankID = b.fkiBankID ) as BankDetails, b.txtAccountNo ,a.txtPaymentRemarks as txtRemarks,(select FullName from tblUsers where UserName = a.txtCreatedBy) txtCreatedBy,(select FullName from tblUsers where UserName = a.txtConfirmedBy) txtConfirmedBy,a.txtControlCheckedBy,a.txtControlVerifiedBy,a.txtControlAuthorisedBy,b.fkiAppTypeId,a.dteValueDate FROM tblApplicationApprovalPayee a, tblMemberApplication  b, [tblApplicationApprovals] c WHERE       a.txtApplicationCode = b.txtApplicationCode and c.txtRefNo = a.txtPencomBatch and (a.txtEnpowerExtractBatch = @pencomBatch) AND (a.txtStatus = @txtStatus)", mycon)

				End Select
				' a.txtPaymentRemarks as txtRemarks
				' and a.dteChecked is not null and a.dteVerified is not null

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pencomBatch", _
			  SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@pencomBatch").Value = pencomBatch

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = "E"


			End If


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApprovalSchedule")
			dtUser = dsUser.Tables("ApprovalSchedule")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMIsApplicationAlreadyExist(pin As String, appID As Integer) As Boolean

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter

		Dim mycon As New SqlClient.SqlConnection

		Try

			mycon = db.getConnection("PaymentModule")

			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.txtApplicationCode , b.intFrequence  from tblMemberApplication a,tblApplicationType b where a.fkiapptypeid = b.pkiAppTypeId and txtpin = @pin and a.fkiAppTypeId = @fkiAppTypeId and a.dteDeactivated is null ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pin").Value = pin

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = appID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblApplicationType")
			dtUser = dsUser.Tables("tblApplicationType")
			mycon.Close()


			If dtUser.Rows(0).Item("intFrequence") = 1 Then
				Return True
			Else
				Return False
			End If

		Catch ex As Exception

		End Try


		Return Nothing
	End Function


	Public Function PMIsApplicationTypeUnique(appCode As Integer) As Boolean

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter

		Dim mycon As New SqlClient.SqlConnection

		Try

			mycon = db.getConnection("PaymentModule")

			MyDataAdapter = New SqlClient.SqlDataAdapter("select intFrequence from tblApplicationType where pkiAppTypeId = @pkiAppTypeId", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiAppTypeId", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiAppTypeId").Value = appCode

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblApplicationType")
			dtUser = dsUser.Tables("tblApplicationType")
			mycon.Close()

			If dtUser.Rows(0).Item("intFrequence") = 1 Then
				Return True
			Else
				Return False
			End If

		Catch ex As Exception

		End Try


		Return Nothing
	End Function

	'updating approval status for new pensioners list
	Public Sub PMUpdateNewPensionserList(txtStatus As String, SIID As Integer, UName As String, RequestType As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter
			If RequestType = "N" Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set txtstatus = @txtstatus, txtConfirmedBy = @txtConfirmedBy, dteConfirmed = @dteConfirmed where pkiStandingPensioneer = @pkiStandingPensioneer ", mycon)
			ElseIf RequestType = "D" Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set txtDStatus = @txtstatus, txtConfirmedBy = @txtConfirmedBy, dteAnniversary = @dteConfirmed where pkiStandingPensioneer = @pkiStandingPensioneer ", mycon)
			ElseIf RequestType = "R" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set txtRStatus = @txtstatus, txtConfirmedBy = @txtConfirmedBy, isRecalled = 0,dteRecalled = null,dteReactivated = @dteConfirmed  where pkiStandingPensioneer = @pkiStandingPensioneer ", mycon)

			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtstatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtstatus").Value = txtStatus


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtConfirmedBy", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtConfirmedBy").Value = UName

			'If RequestType <> "R" Then
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteConfirmed", _
		    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteConfirmed").Value = DateTime.Parse(Now.ToString("yyyy-MM-dd"))
			'Else
			'End If

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiStandingPensioneer", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiStandingPensioneer").Value = SIID

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "NewPensioner")
			'dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			'Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Sub


	'updating approval status for new pensioners list
	Public Sub PMUpdateSIApproval(txtStatus As String, SIID As Integer, UName As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set txtstatus = @txtstatus, txtConfirmedBy = @txtConfirmedBy, dteConfirmed = @dteConfirmed where pkiStandingPensioneer = @pkiStandingPensioneer ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtstatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtstatus").Value = txtStatus


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtConfirmedBy", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtConfirmedBy").Value = UName


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteConfirmed", _
			    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteConfirmed").Value = DateTime.Parse(Now.ToString("yyyy-MM-dd HH:MM"))

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiStandingPensioneer", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiStandingPensioneer").Value = SIID

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "NewPensioner")
			'dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			'Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Sub

	'activating pensioner details
	Public Function PMActivatePensionsers(pkiStandingPensioneer As Integer, UName As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter
			'deactivating for deceased pensioner

			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set isDeceased = 0,isRecalled = 0,dteSetDeceased = null,dteRecalled = null, txtLastChangedPerson = @txtLastChangedPerson, txtDStatus = 'F' where pkiStandingPensioneer = @pkiStandingPensioneer", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiStandingPensioneer", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiStandingPensioneer").Value = pkiStandingPensioneer

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLastChangedPerson", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtLastChangedPerson").Value = UName

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function


	'deactivating pensioner details
	Public Function PMDeactivatePensionsers(pkiStandingPensioneer As Integer, UName As String, DeactivationType As Integer, comment As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As New SqlClient.SqlDataAdapter
			'deactivating for deceased pensioner
			If DeactivationType = 2 Then


				'MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set isDeceased = @deactivationType,dteSetDeceased = @deactivationDate, txtLastChangedPerson = @txtLastChangedPerson, txtDStatus = 'F',txtDeactivationComment = @txtDeactivationComment where pkiStandingPensioneer = @pkiStandingPensioneer", mycon)

				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set isDeceased = @deactivationType,dteSetDeceased = @deactivationDate, txtLastChangedPerson = @txtLastChangedPerson, txtDStatus = 'F',txtDeactivationComment = @txtDeactivationComment where pkiStandingPensioneer = @pkiStandingPensioneer", mycon)


				'deactivating for recalled pensioner

			ElseIf DeactivationType = 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set isRecalled = @deactivationType,dteRecalled = @deactivationDate, txtLastChangedPerson = @txtLastChangedPerson, txtDStatus = 'F',txtDeactivationComment = @txtDeactivationComment where pkiStandingPensioneer = @pkiStandingPensioneer", mycon)

			End If


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtDeactivationComment", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtDeactivationComment").Value = comment

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@deactivationType", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@deactivationType").Value = DeactivationType


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@deactivationDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@deactivationDate").Value = Now.Date


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiStandingPensioneer", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiStandingPensioneer").Value = pkiStandingPensioneer


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLastChangedPerson", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtLastChangedPerson").Value = UName


			MyDataAdapter.SelectCommand.ExecuteNonQuery()


			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function




	'SI updating pensioner details
	Public Function PMUpdatePendingNewPensionsers(numPension As Double, intBankID As Integer, intBankBranchID As Integer, txtBankAccount As String, txtFrequency As String, pkiStandingPensioneer As Integer, UName As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblSIPensioneer set numPension = @numPension,intBankID = @intBankID,intBankBranchID = @intBankBranchID , txtBankAccount = @txtBankAccount, txtFrequency = @txtFrequency, txtLastChangedPerson = @txtLastChangedPerson, txtStatus = 'F', dteAnniversary = @dteAnniversary where pkiStandingPensioneer = @pkiStandingPensioneer", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteAnniversary", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@dteAnniversary").Value = Now.Date

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@numPension", _
			    SqlDbType.Decimal))
			MyDataAdapter.SelectCommand.Parameters("@numPension").Value = numPension

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intBankID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intBankID").Value = intBankID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intBankBranchID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intBankBranchID").Value = intBankBranchID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtBankAccount", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtBankAccount").Value = txtBankAccount

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtFrequency", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtFrequency").Value = txtFrequency

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiStandingPensioneer", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiStandingPensioneer").Value = pkiStandingPensioneer

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLastChangedPerson", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtLastChangedPerson").Value = UName

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "NewPensioner")
			'dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function


	'retrieving new pension entrants
	Public Function PMgetPendingNewPensionsers(txtStatus As String, sDate As Date, eDate As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			If txtStatus = "A" Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("select  *,isnull((select bankName from Enpowerv4.dbo.bank where bankid = a.intBankID),txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = a.intBankID and bankbranchid  = a.intBankBranchID),txtBankBranch) BankBranch,(case when txtfrequency = 1 then 'Monthly' when txtfrequency = 2 then 'Half Yearly' when txtfrequency = 3 then 'Quarterly' when txtfrequency = 6 then 'Half Yearly' end) Frequency, 'N' txtRequestType from tblSIPensioneer a where dteAnniversary between @sDate and @eDate ", mycon)

			Else
				MyDataAdapter = New SqlClient.SqlDataAdapter("select  *,isnull((select bankName from Enpowerv4.dbo.bank where bankid = a.intBankID),txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = a.intBankID and bankbranchid  = a.intBankBranchID),txtBankBranch) BankBranch,(case when txtfrequency = 1 then 'Monthly' when txtfrequency = 2 then 'Half Yearly' when txtfrequency = 3 then 'Quarterly' when txtfrequency = 6 then 'Half Yearly' end) Frequency, 'N' txtRequestType from tblSIPensioneer a where txtstatus = @txtstatus and dteAnniversary between @sDate and @eDate union all select  *,isnull((select bankName from Enpowerv4.dbo.bank where bankid = a.intBankID),txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = a.intBankID and bankbranchid  = a.intBankBranchID),txtBankBranch) BankBranch,(case when txtfrequency = 1 then 'Monthly' when txtfrequency = 2 then 'Half Yearly' when txtfrequency = 3 then 'Quarterly' when txtfrequency = 6 then 'Half Yearly' end) Frequency, 'D' txtRequestType from tblSIPensioneer a where txtDStatus = @txtstatus and dteAnniversary between @sDate and @eDate union all select  *,isnull((select bankName from Enpowerv4.dbo.bank where bankid = a.intBankID),txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = a.intBankID and bankbranchid  = a.intBankBranchID),txtBankBranch) BankBranch,(case when txtfrequency = 1 then 'Monthly' when txtfrequency = 2 then 'Half Yearly' when txtfrequency = 3 then 'Quarterly' when txtfrequency = 6 then 'Half Yearly' end) Frequency, 'R' txtRequestType from tblSIPensioneer a where txtRStatus = @txtstatus and dteAnniversary between @sDate and @eDate ", mycon)
			End If


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtstatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtstatus").Value = txtStatus

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@sDate", _
			    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@sDate").Value = sDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@eDate", _
			    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@eDate").Value = eDate


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewPensioner")
			dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function


	'inserting the standing instruction for new entrants
	Public Function PMgenerateStandingInstruction(UName As String, intMonth As Integer, intYear As Integer, SIType As Char) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			'MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblSIRunHistory(txtPIN, numPension,txtStatus,txtRunBy,txtSIType,intMonthFor,intYearFor ) select txtPIN,numPension,'P', @UName ,'N',@intMonth, @intYear from [dbo].[tblSIPensioneer] a where not exists (select * from tblSIRunHistory where txtPIN = a.txtPIN) and txtStatus = 'P'", mycon)

			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblSIRunHistory(txtPIN, numPension,txtStatus,txtRunBy,txtSIType,intMonthFor,intYearFor ) select txtPIN,numPension,'P', @UName ,'N',month(dteAnniversary), year(dteAnniversary) from [dbo].[tblSIPensioneer] a where isDeceased Is null and isnull(txtRStatus,'A') = 'A' and not exists (select * from tblSIRunHistory where txtPIN = a.txtPIN) and txtStatus = 'A'", mycon)

			'where  

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@UName", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@UName").Value = UName

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intMonth", _
			'    SqlDbType.Int))
			'MyDataAdapter.SelectCommand.Parameters("@intMonth").Value = intMonth

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intYear", _
			'    SqlDbType.Int))
			'MyDataAdapter.SelectCommand.Parameters("@intYear").Value = intYear

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewPensioner")
			dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function


	'inserting  standing instruction for renewals
	Public Function PMRenewalStandingInstruction(SI As StandingPaymentOrder) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblSIRunHistory(txtPIN, numPension,txtStatus,txtRunBy,txtSIType,intMonthFor,intYearFor ) select txtpin ,numPension ,'P',@txtRunBy,'R',@intMonthFor,@IntYearFor from [tblSIPensioneer] a where isDeceased Is null And txtStatus = 'A' and isnull(txtRStatus,'A') = 'A' and month(dteAnniversary) = @intMonthFor and  @IntYearFor >= year(dteAnniversary)  and not exists (select * from [tblSIRunHistory] where txtPIN =  a.txtPIN and intYearFor = @IntYearFor) and exists (select * from [tblSIRunHistory] where txtPIN =  a.txtPIN)", mycon)

			'MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblSIRunHistory(txtPIN, numPension,txtStatus,txtRunBy,txtSIType,intMonthFor,intYearFor ) select txtPIN, numPension,'P',@txtRunBy,'R',@intMonthFor,@intYearFor from [dbo].[tblSIPensioneer] where txtPIN =  @txtPIN", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtRunBy", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtRunBy").Value = SI.RunBy

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intMonthFor", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intMonthFor").Value = SI.MonthFor

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@IntYearFor", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@IntYearFor").Value = SI.YearFor

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewPensioner")
			dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function


	'generating list of SIRenewlList
	Public Function PMgenerateSIRenewalList() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select txtPIN,txtFrequency,dteAnniversary,month(dteAnniversary) AnnMonth,(select max(intYearFor) from [tblSIRunHistory] where txtPIN =  a.txtPIN) LastYearRun,(YEAR (dteAnniversary) - (select max(intYearFor) from [tblSIRunHistory] where txtPIN =  a.txtPIN)) RenewalCount from [dbo].[tblSIPensioneer] a where exists (select * from [tblSIRunHistory] where txtPIN =  a.txtPIN) and isDeceased is null and isRecalled is null and txtStatus = 'P') select * from tab where RenewalCount = 0", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewPensioner")
			dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

		Return Nothing
	End Function


	Public Function PMgetRenewalPensionserDetails(txtPIN As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select  a.*,(select bankName from Enpowerv4.dbo.bank where bankid = a.intBankID) BankName,(select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = a.intBankID and bankbranchid  = a.intBankBranchID) BankBranch,(case when txtfrequency = 1 then 'Monthly' when txtfrequency = 2 then 'Half Yearly' when txtfrequency = 3 then 'Quarterly' when txtfrequency = 6 then 'Half Yearly' end) Frequency,Enpowerv4.[dbo].[GetFundBalanceByDate](b.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance from tblSIPensioneer a,tblMemberApplication b where a.txtapplicationcode = b.txtapplicationcode and a.txtpin = @txtpin", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtpin", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtpin").Value = txtPIN

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewPensioner")
			dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function


	'inserting new pension entrants for standing payment order from file
	Public Function PMgetNewPensionsers(retiree As List(Of Pensioner)) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim i As Integer

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			Do While i < retiree.Count
				
				MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblSIPensioneer (txtPIN,txtFullName,numPWAmount,numPension,txtBankAccount,txtBankName,txtBankBranch,txtFrequency,dteAnniversary,txtStatus)  select @pin ,@fullname ,@pwAmount ,@pensionAmount,@txtAccountNo ,@txtBank,@txtBranch ,@intfrequency ,@dteAnniversary,'P' where not exists (select txtPIN from tblSIPensioneer where txtPIN = @pin ) ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@pin").Value = retiree(i).PIN

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fullname", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@fullname").Value = retiree(i).Fullname

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pwAmount", _
			    SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@pwAmount").Value = retiree(i).PWAmount

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pensionAmount", _
			    SqlDbType.Decimal))
				MyDataAdapter.SelectCommand.Parameters("@pensionAmount").Value = retiree(i).PensionAmount

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtAccountNo", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtAccountNo").Value = retiree(i).AccountNo

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtBank", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtBank").Value = retiree(i).Bank

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtBranch", _
			    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtBranch").Value = retiree(i).Branch

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intfrequency", _
			    SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@intfrequency").Value = retiree(i).Frequency

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteAnniversary", _
			    SqlDbType.Date))
				MyDataAdapter.SelectCommand.Parameters("@dteAnniversary").Value = retiree(i).AnniversaryDate

				MyDataAdapter.SelectCommand.ExecuteNonQuery()
				'dsUser = New DataSet()
				'MyDataAdapter.Fill(dsUser, "NewPensioner")
				'dtUser = dsUser.Tables("NewPensioner")
				'mycon.Close()

				'Return dtUser
				i = i + 1
			Loop
			mycon.Close()
		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function

	'inserting new pension entrants for standing payment order from surePay
	Public Function PMgetNewPensionsers(SDate As Date, EDate As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblSIPensioneer (txtPIN,txtFullName,numPWAmount,numPension,intBankID,intBankBranchID,txtBankAccount,txtFrequency,dteAnniversary,txtStatus,txtApplicationcode) select a.txtPIN,replace(a.txtfullName,'|','') as FullName, Enpowerv4.[dbo].[GetFundBalanceByDate](a.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance ,c.numPensionToPay,a.fkibankid,a.fkibranchid,a.txtAccountNo,1 as Frequency,cast(getdate() as date) as Anniversary,'P',c.txtApplicationcode from tblMemberApplication a,tblApplicationApprovalPayee b, awbr100 c where dtepaid  between @sDate and @eDate and a.txtapplicationcode = b.txtapplicationcode and a.txtapplicationcode = c.txtapplicationcode and fkiapptypeid = @fkiapptypeid and b.txtstatus = @txtstatus; select  *,(select bankName from Enpowerv4.dbo.bank where bankid = a.intBankID) BankName,(select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = a.intBankID and bankbranchid  = a.intBankBranchID) BankBranch,(case when txtfrequency = 1 then 'Monthly' when txtfrequency = 2 then 'Half Yearly' when txtfrequency = 3 then 'Quarterly' when txtfrequency = 6 then 'Half Yearly' end) Frequency from tblSIPensioneer a where txtstatus = 'P' and dteAnniversary between @sDate and @eDate ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtstatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtstatus").Value = "E"

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiapptypeid", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@fkiapptypeid").Value = "3"

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@sDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@sDate").Value = SDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@eDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@eDate").Value = EDate

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewPensioner")
			dtUser = dsUser.Tables("NewPensioner")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function


	'retrieving  standing order
	Public Function PMgetNewStandingOrder(SIType As String, status As String, intMonthFor As Integer, intYearFor As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.pkiSIApproval,b.txtPIN ,b.txtFullName ,a.numPension ,b.txtBankAccount,isnull((select bankName from Enpowerv4.dbo.bank where bankid = b.intBankID),b.txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = b.intBankID and bankbranchid  = b.intBankBranchID),b.txtBankBranch) BankBranch,a.txtStatus  from tblSIRunHistory a, tblSIPensioneer b where a.txtPIN = b.txtPIN and a.txtSIType = @txtSIType and a.txtStatus = @txtStatus and intMonthFor = @intMonthFor and intYearFor = @intYearFor", mycon)

			'intMonthFor,intYearFor

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtSIType", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtSIType").Value = SIType


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intMonthFor", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intMonthFor").Value = intMonthFor


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intYearFor", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intYearFor").Value = intYearFor


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewStandingOrder")
			dtUser = dsUser.Tables("NewStandingOrder")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


		Return Nothing
	End Function




	'updating staning order approvals
	Public Sub PMUpdateStandingOrderApproval(SIID As Integer, UName As String, Status As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim str As String = ""

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			'P=Pending, V = Vetted, C= Check, CC= Checked,A= Approve, AA= Approved

			If Status = "V" Then

				str = "update tblSIRunHistory set txtStatus = @txtStatus ,dteVetted = @dte,txtVettedby = @UName where pkiSIApproval = @pkiSIApproval"
			ElseIf Status = "C" Then

				str = "update tblSIRunHistory set txtStatus = @txtStatus ,dteChecker1 = @dte,txtChecker1 = @UName where pkiSIApproval = @pkiSIApproval"
			ElseIf Status = "CC" Then

				str = "update tblSIRunHistory set txtStatus = @txtStatus ,dteChecker2 = @dte,txtChecker2 = @UName where pkiSIApproval = @pkiSIApproval"
			ElseIf Status = "A" Then

				str = "update tblSIRunHistory set txtStatus = @txtStatus ,dteApproval1 = @dte,txtApproval1 = @UName where pkiSIApproval = @pkiSIApproval"
			ElseIf Status = "AA" Then

				str = "update tblSIRunHistory set txtStatus = @txtStatus ,dteApproval2 = @dte,txtApproval2 = @UName where pkiSIApproval = @pkiSIApproval"

			End If

			MyDataAdapter = New SqlClient.SqlDataAdapter(str, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = Status


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiSIApproval", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiSIApproval").Value = SIID


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@UName", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@UName").Value = UName


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dte", _
			    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dte").Value = DateTime.Parse(Now.ToString("yyyy-MM-dd HH:MM"))


			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "NewStandingOrder")
			'dtUser = dsUser.Tables("NewStandingOrder")
			mycon.Close()

			'Return dtUser

		Catch Ex As Exception
			MsgBox("" & Ex.Message)
		Finally

		End Try

		'Return Nothing

	End Sub





	'retrieving new standing order by OrderID
	Public Function PMViewStandingOrder(SIOrderID As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.pkiSIApproval,b.txtPIN ,b.txtFullName ,a.numPension ,b.txtBankAccount,			isnull((select bankName from Enpowerv4.dbo.bank where bankid = b.intBankID),b.txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = b.intBankID and bankbranchid  = b.intBankBranchID),b.txtBankBranch) BankBranch,txtFrequency,Enpowerv4.[dbo].[GetFundBalanceByDate](c.fkiMemberID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance,cast(dteRun as date) dteRun,b.dteAnniversary,a.txtVettedby,a.txtChecker1,a.txtChecker2,a.txtApproval1 ,a.txtApproval2 from tblSIRunHistory a,tblSIPensioneer b, tblMemberApplication c where a.txtPIN = b.txtPIN and c.txtApplicationCode = b.txtApplicationcode and a.pkiSIApproval = @pkiSIApproval union all select a.pkiSIApproval,b.txtPIN ,b.txtFullName ,a.numPension ,b.txtBankAccount,			isnull((select bankName from Enpowerv4.dbo.bank where bankid = b.intBankID),b.txtBankName) BankName,isnull((select top 1 branchName from Enpowerv4.dbo.bankbranch where bankid = b.intBankID and bankbranchid  = b.intBankBranchID),b.txtBankBranch) BankBranch,txtFrequency,Enpowerv4.[dbo].[GetFundBalanceByDate]((select employeeid from enpowerv4..employee where rsapin =a.txtpin),2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance,cast(dteRun as date) dteRun,b.dteAnniversary,a.txtVettedby,a.txtChecker1,a.txtChecker2,a.txtApproval1 ,a.txtApproval2 from tblSIRunHistory a,tblSIPensioneer b where a.txtPIN = b.txtPIN  and a.pkiSIApproval = @pkiSIApproval", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiSIApproval", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@pkiSIApproval").Value = SIOrderID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "NewStandingOrder")
			dtUser = dsUser.Tables("NewStandingOrder")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


		Return Nothing
	End Function
	'getting application by pins
	Public Function PMgetApplicationByPIN(pin As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			MyDataAdapter = New SqlClient.SqlDataAdapter("select rsapin,(select employername from Enpowerv4..Employer where employerid = a.employerid) EmployerName,LastName as Surname,FirstName,MiddleName,dateofbirth,datediff(year,dateofbirth,getdate()) Age, isnull((select isnull(c.Value,'') from Enpowerv4.dbo.NextOfKin b, Enpowerv4.dbo.titles c where b.titleid = c.titleid and employeeid = a.employeeid),'') +'. '+ (select isnull(LastName,'')+' '+isnull(FirstName,'')+' '+isnull(MiddleName,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOK,isnull(OfficeStreetAddress1,'')+' '+ isnull(OfficeStreetAddress2,'')+' '+ isnull(OfficeTown,'') as OfficeAddress ,	OfficeLGAID,	OfficeStateID, isnull(ResidentialAddress1,'')+' '+	isnull(ResidentialAddress2,'') as ResidentialAddress ,	ResidentialStateID,	ResidentialLGAID,isnull(ContactAddress1,'')+' '+ isnull(ContactAddress2,'') as ContactAddress ,	ContactStateID,	ContactLGAID,email,JobTitle,Designation, AccountName,	AccountNumber,	BankID,	BankBranchID,employeeid, Enpowerv4.[dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,Enpowerv4.[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) as AccruedRight, (case when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,isnull(BasicSalary,0.00) BasicSalary,	isnull(Transport,0.00) Transport,	isnull(Housing,0.00) Housing, isnull(phone,mobile) phone,(select employercode from Enpowerv4..Employer where employerid = a.employerid) EmployerCode,employerid,sex,(isnull(dteDOR,dteDisengagement)) DOR,MaritalStatus,txtReason,txtApplicationCode,replace(txtfullname,'|','') Name,(select MandatoryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as Mandatory,(select VoluntaryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as AVC,(select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as Legacy,b.*,(select txtDescription from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) ApprovalType,numRSABalance from Enpowerv4.dbo.Employee a, SurePay..tblMemberApplication b where rsapin = txtPIN and rsapin in " & pin & " and dteDeactivated is null", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", _
			'    SqlDbType.VarChar))
			'MyDataAdapter.SelectCommand.Parameters("@pin").Value = pin

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMgetApplicationByPIN(pin As String, appCode As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As New SqlClient.SqlDataAdapter

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try


			If appCode <> "" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select rsapin,(select employername from Enpowerv4..Employer where employerid = a.employerid) EmployerName,LastName as Surname,FirstName,MiddleName,dateofbirth,datediff(year,dateofbirth,getdate()) Age, isnull((select isnull(c.Value,'') from Enpowerv4.dbo.NextOfKin b, Enpowerv4.dbo.titles c where b.titleid = c.titleid and employeeid = a.employeeid),'') +'. '+ (select isnull(LastName,'')+' '+isnull(FirstName,'')+' '+isnull(MiddleName,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOK,isnull(OfficeStreetAddress1,'')+' '+ isnull(OfficeStreetAddress2,'')+' '+ isnull(OfficeTown,'') as OfficeAddress ,	OfficeLGAID,	OfficeStateID, isnull(ResidentialAddress1,'')+' '+	isnull(ResidentialAddress2,'') as ResidentialAddress ,	ResidentialStateID,	ResidentialLGAID,isnull(ContactAddress1,'')+' '+ isnull(ContactAddress2,'') as ContactAddress ,	ContactStateID,	ContactLGAID,email,JobTitle,Designation, AccountName,	AccountNumber,	BankID,	BankBranchID,employeeid, Enpowerv4.[dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,Enpowerv4.[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) as AccruedRight, (case when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,isnull(BasicSalary,0.00) BasicSalary,	isnull(Transport,0.00) Transport,	isnull(Housing,0.00) Housing, isnull(phone,mobile) phone,(select employercode from Enpowerv4..Employer where employerid = a.employerid) EmployerCode,employerid,sex,(isnull(dteDOR,dteDisengagement)) DOR,MaritalStatus,txtReason,txtApplicationCode [Application No],replace(txtfullname,'|','') Name,isnull((select MandatoryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Mandatory,isnull((select MandatoryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))),0) as MandatoryRF,(select VoluntaryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as AVC,(select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as Legacy,b.*,(select txtDescription from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) ApprovalType,numRSABalance,b.dteApplicationDate ApplicationDate from Enpowerv4.dbo.Employee a, SurePay..tblMemberApplication b where rsapin = txtPIN and txtApplicationCode = @appCode and rsapin = @pin and dteDeactivated is null", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@appCode", _
			  SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@appCode").Value = appCode

			ElseIf appCode = "" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select rsapin,(select employername from Enpowerv4..Employer where employerid = a.employerid) EmployerName,LastName as Surname,FirstName,MiddleName,dateofbirth,datediff(year,dateofbirth,getdate()) Age, isnull((select isnull(c.Value,'') from Enpowerv4.dbo.NextOfKin b, Enpowerv4.dbo.titles c where b.titleid = c.titleid and employeeid = a.employeeid),'') +'. '+ (select isnull(LastName,'')+' '+isnull(FirstName,'')+' '+isnull(MiddleName,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOK,isnull(OfficeStreetAddress1,'')+' '+ isnull(OfficeStreetAddress2,'')+' '+ isnull(OfficeTown,'') as OfficeAddress ,	OfficeLGAID,	OfficeStateID, isnull(ResidentialAddress1,'')+' '+	isnull(ResidentialAddress2,'') as ResidentialAddress ,	ResidentialStateID,	ResidentialLGAID,isnull(ContactAddress1,'')+' '+ isnull(ContactAddress2,'') as ContactAddress ,	ContactStateID,	ContactLGAID,email,JobTitle,Designation, AccountName,	AccountNumber,	BankID,	BankBranchID,employeeid, Enpowerv4.[dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,Enpowerv4.[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) as AccruedRight, (case when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'ST' then 'State' when substring((select employercode from Enpowerv4..Employer where employerid = a.employerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,isnull(BasicSalary,0.00) BasicSalary,	isnull(Transport,0.00) Transport,	isnull(Housing,0.00) Housing, isnull(phone,mobile) phone,(select employercode from Enpowerv4..Employer where employerid = a.employerid) EmployerCode,employerid,sex,(isnull(dteDOR,dteDisengagement)) DOR,MaritalStatus,txtReason,txtApplicationCode [Application No],replace(txtfullname,'|','') Name,(select MandatoryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as Mandatory,(select VoluntaryBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as AVC,(select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))) as Legacy,b.*,(select txtDescription from tblApplicationType where pkiAppTypeId = b.fkiAppTypeId) ApprovalType,numRSABalance,b.dteApplicationDate ApplicationDate from Enpowerv4.dbo.Employee a, SurePay..tblMemberApplication b where rsapin = txtPIN and rsapin = @pin and dteDeactivated is null", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			End If


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pin", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pin").Value = pin

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function PMgetApprovalPerson(applicationCode As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select isnull(txtPaymentRemarks,'') txtPaymentRemarks from tblApplicationApprovalPayee where txtApplicationCode = @txtApplicationCode", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApprovalPerson")
			dtUser = dsUser.Tables("ApprovalPerson")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function


	'getting list of missing pinned customers on everification_audit
	Public Function PMEGetEVAuditMissingRecords(date1 As Date, date2 As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("Enpowerv4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter


			MyDataAdapter = New SqlClient.SqlDataAdapter("select a.EmployeeID,FormReferenceNumber,RSAPIN,(LastName+'  '+ FirstName+'  '+	MiddleName) as FullName,'True' IsRecordAvailable,RSAPINRegistrationDate,'' as comment,b.Picture  from employee a, Biometric b where exists (select * from EVerification_Audit where FormRef = a.FormReferenceNumber ) and 			RSAPINRegistrationDate between '" & DateTime.Parse(date1).ToString("yyyy-MM-dd") & "' and '" & DateTime.Parse(date2).ToString("yyyy-MM-dd") & "' and a.EmployeeID = b.EmployeeID  union all select a.EmployeeID,FormReferenceNumber,RSAPIN,(LastName+'  '+ FirstName+'  '+	MiddleName) as FullName,'False' IsRecordAvailable,RSAPINRegistrationDate,'Missing Record on Everification Audit' as comment,b.Picture  from employee a,Biometric b where  not exists (select * from EVerification_Audit where FormRef = a.FormReferenceNumber ) and RSAPINRegistrationDate between '" & DateTime.Parse(date1).ToString("yyyy-MM-dd") & "' and '" & DateTime.Parse(date2).ToString("yyyy-MM-dd") & "' and a.EmployeeID = b.EmployeeID  ", mycon)


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
			'    SqlDbType.DateTime))
			'MyDataAdapter.SelectCommand.Parameters("@date1").Value = DateTime.Parse(date1).ToString("yyyy-MM-dd")

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
			'    SqlDbType.DateTime))
			'MyDataAdapter.SelectCommand.Parameters("@date2").Value = DateTime.Parse(date2).ToString("yyyy-MM-dd")

			dsUser = New DataSet()

			MyDataAdapter.Fill(dsUser, "EVAuditMissingRecords")
			dtUser = dsUser.Tables("EVAuditMissingRecords")
			mycon.Close()

			'MsgBox("" & dtUser.Rows.Count)

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function

	Public Function PMgetApplicationByCode(applicationCode As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try


			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,isnull(numApplicationAmount,0.00) ApplicationAMount,*,(select txtDescription  from dbo.tblApplicationType where pkiAppTypeId = fkiAppTypeId) TypeName,(select bankName from enpowerv4..bank where BankID  = fkibankid) BankName,(select BranchName  from enpowerv4..BankBranch where BankBranchID = fkiBranchID) BranchName,(select a.dteApproval from [dbo].[tblApplicationApprovals] a, [dbo].[tblApplicationApprovalPayee] b where a.txtRefNo = b.txtPencomBatch and b.txtApplicationCode = c.txtApplicationCode) ApprovalDate, (select a.dteAcknowledgment from [dbo].[tblApplicationApprovals] a, [dbo].[tblApplicationApprovalPayee] b where a.txtRefNo = b.txtPencomBatch and b.txtApplicationCode = c.txtApplicationCode) AcknowledgmentDate,(select FullName from tblUsers where UserName = c.txtCreatedBy) CreatedBy,(select top 1 txtChangedPerson  from tblChangeHistory where fkiMemberApplicationID = c.pkiMemberApplicationID and txtNewValue = 'Processing' and txtOldValue = 'Documentation' order by dteChanged desc) ReviewedBy,(select top 1 txtChangedPerson  from tblChangeHistory where fkiMemberApplicationID = c.pkiMemberApplicationID and txtNewValue = 'Confirmation' and txtOldValue = 'Processing' order by dteChanged desc) ProcessedBy,(select top 1 txtChangedPerson  from tblChangeHistory where fkiMemberApplicationID = c.pkiMemberApplicationID and txtNewValue = 'Send To Pencom' and txtOldValue = 'Confirmation' order by dteChanged desc) ConfirmedBy,txtControlCheckedBy,isFundingStatusChecked,isLegAVCChecked,isDOBChecked,isNamesChecked,isExitDocChecked,isDataEntryChecked,isValidDocChecked from tblMemberApplication c where txtApplicationCode = @txtApplicationCode", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = applicationCode

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function
	'locking record to ensure only 1 user works on a record at every instance
	Public Sub PMLocKRecord(appCode As String, uName As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		Dim mycon As New SqlClient.SqlConnection

		Try

			mycon = db.getConnection("PaymentModule")

			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMemberApplication set dteLocked = @dteLocked , txtLockedBy = @txtLockedBy where txtApplicationCode = @txtApplicationCode", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteLocked", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteLocked").Value = Now
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtLockedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtLockedBy").Value = uName
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApplicationCode", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApplicationCode").Value = appCode
			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			mycon.Close()


		Catch ex As Exception

		End Try

	End Sub

	Public Function PMgetPaidApplication(startDate As Date, endDate As Date, AppTypeID As Integer, fundID As Integer, status As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_getPaidApplication", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@startDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@startDate").Value = startDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@endDate", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@endDate").Value = endDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fundID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@fundID").Value = fundID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@AppTypeID", _
    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@AppTypeID").Value = AppTypeID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@status", _
    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@status").Value = status


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PaidApplication")
			dtUser = dsUser.Tables("PaidApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
	End Function

	Public Function PMgetApplicationSummaryByStage() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select (select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId) ApplicationName, txtCreatedBy CreatedBy  , count(*) ApplicationCount from tblMemberApplication where isnull(txtStatus ,'') <> '' and txtStatus = 'Entry' and IsDeactivated is null and dteDeactivated is null group by fkiAppTypeId,txtCreatedBy order by 3 desc ,1 asc", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@stage", _
			'    SqlDbType.VarChar))
			'MyDataAdapter.SelectCommand.Parameters("@stage").Value = stage

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function

	Public Function PMgetApplicationSummaryByStage(stage As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select (select txtDescription from tblApplicationType where pkiAppTypeId = fkiAppTypeId) txtStatus , count(*) ApplicationCount from tblMemberApplication where isnull(txtStatus ,'') <> '' and txtStatus = @stage and IsDeactivated is null and dteDeactivated is null group by fkiAppTypeId", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@stage", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@stage").Value = stage

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function

	Public Function PMgetApplicationSummary() As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtStatus , count(*) ApplicationCount from tblMemberApplication where isnull(txtStatus ,'') <> '' and IsDeactivated is null and dteDeactivated is null group by txtStatus ", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", _
			'    SqlDbType.Int))
			'MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = intTypeID

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
			'    SqlDbType.VarChar))
			'MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = Status

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function



	Public Function PMgetApplicationByTpye(intTypeID As Integer, Status As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			If Status = "Send to Pencom" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,isnull(dteDOR,dteDisengagement) RetirementDate,* from tblMemberApplication a where fkiAppTypeId = @fkiAppTypeId and IsDocCompleted = 1 and txtStatus = @txtStatus and isnull(IsSentToPencom,0) = 0 and isnull(IsDeactivated,0) = 0", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", _
				    SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = intTypeID

			ElseIf Status <> "Send to Pencom" And intTypeID = 0 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,* from tblMemberApplication a where txtStatus = @txtStatus and isnull(IsDeactivated,0) = 0", mycon)

			Else

				MyDataAdapter = New SqlClient.SqlDataAdapter("select replace(txtfullname,'|','') Name,* from tblMemberApplication a where fkiAppTypeId = @fkiAppTypeId and txtStatus = @txtStatus and isnull(IsDeactivated,0) = 0", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fkiAppTypeId", _
				    SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@fkiAppTypeId").Value = intTypeID

			End If

			

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = Status

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MemberApplication")
			dtUser = dsUser.Tables("MemberApplication")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function


	Public Function PMgetRMASScheduleTpye(intTypeID As Integer, reportDate As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim sql As String = ""
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try
			Select Case intTypeID
				Case Is = 2
					sql = "select [pin],[employer-code],[gender],[birth-date],[disengagement-date],[rsa-balance],[twentyfive-percent-rsa-balance] from awbr500 where [date-sent] = @dateSent"
				Case Else
			End Select

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			'If Status = "Send to Pencom" Then
			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dateSent", _
			    SqlDbType.Date))
			MyDataAdapter.SelectCommand.Parameters("@dateSent").Value = reportDate

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "RMASSchedule")
			dtUser = dsUser.Tables("RMASSchedule")
			mycon.Close()

			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try



	End Function

	Public Function PMgetNextSPLogID(apptype As Integer, batchType As Char) As String

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_getSPBatch", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@Apptype", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@Apptype").Value = apptype

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@batchType", SqlDbType.Char))
			MyDataAdapter.SelectCommand.Parameters("@batchType").Value = batchType

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationID")
			dtUser = dsUser.Tables("ApplicationID")
			mycon.Close()
			Return (dtUser.Rows(0).Item(0)).ToString

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function
	' checking if all the pins in the splog batch has been processed for payment
	Public Function PMCheckSPBatchApplications(SPbatch As String) As Boolean

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) from tblMemberApplication where txtSPBatchNo = @txtSPBatchNo and dteApprovalConfirmed is null", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtSPBatchNo", _
			   SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtSPBatchNo").Value = SPbatch

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "SPBatchApplications")
			dtUser = dsUser.Tables("SPBatchApplications")
			mycon.Close()
			' Return CInt(dtUser.Rows(0).Item(0))

			If CInt(dtUser.Rows(0).Item(0)) > 0 Then
				Return True
			Else
				Return False
			End If

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function


	Public Function PMgetNextApplicationID() As Integer

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("sp_pm_getApplicationID", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "ApplicationID")
			dtUser = dsUser.Tables("ApplicationID")
			mycon.Close()
			Return CInt(dtUser.Rows(0).Item(0))

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function




	''' <summary>
	''' Payment Module Function
	''' </summary>
	''' <param name="PIN"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	''' 

	Public Function PMgetTitles() As DataTable

		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select TitleID,Value from titles", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "titles")
		dtUser = dsUser.Tables("titles")
		mycon.Close()
		Return dtUser


	End Function

	Public Function PMgetBanks() As DataTable
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select bankid, bankname from bank", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "BankList")
		dtUser = dsUser.Tables("BankList")
		mycon.Close()
		Return dtUser


	End Function

	Public Function PMgetBanks(bankID As Integer) As DataTable
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select bankid, bankname from bank where bankID = '" & bankID & "'", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "BankList")
		dtUser = dsUser.Tables("BankList")
		mycon.Close()
		Return dtUser


	End Function

	Public Function PMgetSOONToRetiree(clauses As String) As DataTable
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim sql As String

		sql = "select rsapin,(select value from titles where titleID = a.titleID) title,isnull(FirstName,'')+'  '+ isnull(LastName,'')+'  '+ isnull(MiddleName,'') Name,dateofbirth,datediff(year,dateofbirth,getdate()) age,employerid,datediff(year,firstemploymentdate,getdate()) YearOfService,firstemploymentdate, (select EmployerName from employer where employerid = a.employerid) EmployerName ,(select  case when substring(EmployerCode,1,2) = 'PU' then 'Public' when substring(EmployerCode,1,2) = 'PR' then 'Private' else 'Others' End from employer where employerid = a.employerid) Sector from employee a "

		sql = sql & clauses

		'	sql2 = "select rsapin,(select value from titles where titleID = a.titleID) title,isnull(FirstName,'')+'  '+ isnull(LastName,'')+'  '+ isnull(MiddleName,'') Name,dateofbirth,datediff(year,dateofbirth,getdate()) age,employerid,datediff(year,firstemploymentdate,getdate()) YearOfService,firstemploymentdate, (select EmployerName from employer where employerid = a.employerid) EmployerName ,(select  case when substring(EmployerCode,1,2) = 'PU' then 'Public' when substring(EmployerCode,1,2) = 'PR' then 'Private' else 'Others' End from employer where employerid = a.employerid) Sector from employee a where isretired = 0 and firstemploymentdate is not null "

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter(sql, mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "SoonToRetiree")
		dtUser = dsUser.Tables("SoonToRetiree")
		mycon.Close()
		Return dtUser


	End Function


	Public Function PMgetBankBranches(BankID As Integer) As DataTable
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select 	 BankBranchID,	BranchName from BankBranch where bankid = '" & BankID & "' order by 2 asc", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "BankBranchList")
		dtUser = dsUser.Tables("BankBranchList")
		mycon.Close()
		Return dtUser


	End Function


	'retrieving the bank branch ID
	Public Function PMgetBankBranches(BankID As Integer, BranchName As String) As DataTable
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select 	 BankBranchID,	BranchName from BankBranch where bankid = '" & BankID & "' and BranchName = '" & BranchName & "' order by 2 asc", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "BankBranchList")
		dtUser = dsUser.Tables("BankBranchList")
		mycon.Close()
		Return dtUser


	End Function


	'retrieving the bank branch name
	Public Function PMgetBankBranches(BankID As Integer, BranchID As Integer) As DataTable
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("select 	 BankBranchID,	BranchName from BankBranch where bankid = '" & BankID & "' and BankBranchID = '" & BranchID & "' order by 2 asc", mycon)
		MyDataAdapter.SelectCommand.CommandType = CommandType.Text

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "BankBranchList")
		dtUser = dsUser.Tables("BankBranchList")
		mycon.Close()
		Return dtUser


	End Function


	Public Function getPMPersonInformation(PIN As String, cutOffDate As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select (select employername from Enpowerv4.dbo.employer where employerid = a.employerid) EmployerName,LastName as Surname,FirstName,MiddleName,dateofbirth,isnull(datediff(year,dateofbirth,getdate()),0) Age, isnull((select isnull(c.Value,'') from Enpowerv4.dbo.NextOfKin b, titles c where b.titleid = c.titleid and employeeid = a.employeeid),'') +' '+ (select isnull(LastName,'')+' '+isnull(FirstName,'')+' '+isnull(MiddleName,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOK,isnull(OfficeStreetAddress1,'')+' '+ isnull(OfficeStreetAddress2,'')+' '+ isnull(OfficeTown,'') as OfficeAddress ,	OfficeLGAID,	OfficeStateID, isnull(ResidentialAddress1,'')+' '+	isnull(ResidentialAddress2,'') as ResidentialAddress ,	ResidentialStateID,	ResidentialLGAID,isnull(ContactAddress1,'')+' '+ isnull(ContactAddress2,'') as ContactAddress ,	ContactStateID,	ContactLGAID,email,JobTitle,Designation, AccountName,	AccountNumber,	BankID,	BankBranchID,employeeid, [dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) as AccruedRight, (case when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'ST' then 'Public' when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,isnull(BasicSalary,0.00) BasicSalary,	isnull(Transport,0.00) Transport,	isnull(Housing,0.00) Housing, Phone,(select employercode from Enpowerv4.dbo.employer where employerid = a.employerid) EmployerCode,employerid,sex,isnull((select isnull(c.Value,'') from  titles c where c.titleid = a.TitleID ),'') as Title,isnull((select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Mandatory,isnull((select VoluntaryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as AVC,isnull((select PreActNSITFBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Legacy,(select isnull(Phone ,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOKPhone,isnull((select sum(UnitValue  ) from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2),0.0000) as TotalLegacyUnit,isnull((select sum(NetAmount) from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2),0) as TotalLegacyAmount,(select max( ValueDate)  from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2 ) LagacyContValueDate, isnull((select top 1 UnitPrice  from Contributions b where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID = 2 and valuedate = (select max( ValueDate)  from Contributions where EmployeeID = b.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2 )),0) LagacyContUnitPrice,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1) RSAPriceDate,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2) RFPriceDate,[dbo].udfGetWithdrawalsBF(a.EmployeeID,2,getdate()) as TotalRFPayment,isnull((select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as TotalNSITFValueRSA,isnull((select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))),0) as TotalNSITFValueRF,isnull((select (select sum(([EEPreactNSITFAmount]+[ERPreactNSITFAmount])) NSITFCont From vwContributions where EmployeeID= a.EmployeeID and FundID=1 and ([EEPreactNSITFAmount]+[ERPreactNSITFAmount]) >0) + (select sum(-[AmountFromPreActNSITF]) NSITFCont From vwPayments where EmployeeID= a.EmployeeID and FundID=1 and [AmountFromPreActNSITF] >0)),0) TotalNSITFUpload,rsapin from dbo.Employee a where rsapin  = @PIN", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			'TotalNSITFValueRSA,TotalNSITFValueRF
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PersonalDetails")
			dtUser = dsUser.Tables("PersonalDetails")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function
	'getting participant information of the Lite UI
	Public Function getPMPersonInformationLite(PIN As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("Midas")

		Try


			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtclientName EmployerName,txtSARSNo EmployerCode,b.fkiClientID employerid,pkipersonid employeeid,txtFirstname FirstName,	txtOtherNames MiddleName,	txtSurname Surname,	txtTitle Title,dteDOB dateofbirth,txtIDNo,txtCellPhone Phone,a.txtEMailAddress email,txtSex sex, txtResidAddr1+' '+	txtResidAddr2+' '+	txtResidAddr3 +' '+	txtResidCity +' '+	txtResidCode as FullAddress,(case when substring(txtSARSNo,1,2) = 'PR' then 'Private' when substring(txtSARSNo,1,2) = 'ST' then 'Public' when substring(txtSARSNo,1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector, isnull(datediff(year,dteDOB,getdate()),0) Age from tblpeople a, [dbo].[tblMemberFundInformation] b,	[dbo].[tblClients] c where txtIDNo = @PIN and fkipersonid = pkipersonid and pkiClientID = b.fkiClientID", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			'TotalNSITFValueRSA,TotalNSITFValueRF
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PersonalDetails")
			dtUser = dsUser.Tables("PersonalDetails")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	'finding perticipant account detailed lite information 
	Public Function getPMPersonInformationLitea(PIN As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select [dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, (select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) as AccruedRight,isnull((select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Mandatory,isnull((select VoluntaryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as AVC,isnull((select PreActNSITFBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Legacy,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1) RSAPriceDate,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2) RFPriceDate,rsapin,isnull((select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Mandatory from dbo.Employee a where rsapin  = @PIN ", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			'TotalNSITFValueRSA,TotalNSITFValueRF
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PersonalDetails")
			dtUser = dsUser.Tables("PersonalDetails")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	'finding perticipants detailed full information 
	Public Function getPMPersonInformation(PIN As String, isMultiplePIN As Boolean) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select (select employername from Enpowerv4.dbo.employer where employerid = a.employerid) EmployerName,LastName as Surname,FirstName,MiddleName,dateofbirth,isnull(datediff(year,dateofbirth,getdate()),0) Age, isnull((select isnull(c.Value,'') from Enpowerv4.dbo.NextOfKin b, titles c where b.titleid = c.titleid and employeeid = a.employeeid),'') +' '+ (select isnull(LastName,'')+' '+isnull(FirstName,'')+' '+isnull(MiddleName,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOK,isnull(OfficeStreetAddress1,'')+' '+ isnull(OfficeStreetAddress2,'')+' '+ isnull(OfficeTown,'') as OfficeAddress ,	OfficeLGAID,	OfficeStateID, isnull(ResidentialAddress1,'')+' '+	isnull(ResidentialAddress2,'') as ResidentialAddress ,	ResidentialStateID,	ResidentialLGAID,isnull(ContactAddress1,'')+' '+ isnull(ContactAddress2,'') as ContactAddress ,	ContactStateID,	ContactLGAID,email,JobTitle,Designation, AccountName,	AccountNumber,	BankID,	BankBranchID,employeeid, [dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, ((select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) * (select UnitPrice from Enpowerv4.dbo.UnitPrice where FundID = 2 and  valuedate =  (select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) )as AccruedRight, (case when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'ST' then 'Public' when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,isnull(BasicSalary,0.00) BasicSalary,	isnull(Transport,0.00) Transport,	isnull(Housing,0.00) Housing, Phone,(select employercode from Enpowerv4.dbo.employer where employerid = a.employerid) EmployerCode,employerid,sex,isnull((select isnull(c.Value,'') from  titles c where c.titleid = a.TitleID ),'') as Title,isnull((select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Mandatory,isnull((select VoluntaryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as AVC,isnull((select PreActNSITFBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Legacy,(select isnull(Phone ,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOKPhone,isnull((select sum(UnitValue  ) from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2),0.0000) as TotalLegacyUnit,isnull((select sum(NetAmount) from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2),0) as TotalLegacyAmount,(select max( ValueDate)  from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2 ) LagacyContValueDate, isnull((select top 1 UnitPrice  from Contributions b where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID = 2 and valuedate = (select max( ValueDate)  from Contributions where EmployeeID = b.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2 )),0) LagacyContUnitPrice,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1) RSAPriceDate,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2) RFPriceDate,[dbo].udfGetWithdrawalsBF(a.EmployeeID,2,getdate()) as TotalRFPayment,isnull((select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as TotalNSITFValueRSA,isnull((select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))),0) as TotalNSITFValueRF,isnull((select (select sum(([EEPreactNSITFAmount]+[ERPreactNSITFAmount])) NSITFCont From vwContributions where EmployeeID= a.EmployeeID and FundID=1 and ([EEPreactNSITFAmount]+[ERPreactNSITFAmount]) >0) + (select sum(-[AmountFromPreActNSITF]) NSITFCont From vwPayments where EmployeeID= a.EmployeeID and FundID=1 and [AmountFromPreActNSITF] >0)),0) TotalNSITFUpload,rsapin from dbo.Employee a where rsapin in " & PIN & "", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			'    SqlDbType.VarChar))
			'MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			'TotalNSITFValueRSA,TotalNSITFValueRF
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PersonalDetails")
			dtUser = dsUser.Tables("PersonalDetails")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function getNxDx(SexType As Integer, intAge As Integer) As Decimal
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim EmployerHisCollection As New Hashtable
		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("PaymentModule")


		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			If SexType = 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select numNxDx from tblMalePencomTemplate where intAge =  @intAge ", mycon)

			Else

				MyDataAdapter = New SqlClient.SqlDataAdapter("select numNxDx from tblFemalePencomFormat where intAge =  @intAge ", mycon)

			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intAge", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@intAge").Value = intAge

			MyDataAdapter.Fill(dsUser, "PencomTemplate")
			dtUser = dsUser.Tables("PencomTemplate")


			mycon.Close()

			Return dtUser.Rows(0).Item(0)

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function


	'finding perticipant detailed full information 
	Public Function getPMPersonInformation(PIN As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select (select employername from Enpowerv4.dbo.employer where employerid = a.employerid) EmployerName,LastName as Surname,FirstName,MiddleName,dateofbirth,isnull(datediff(year,dateofbirth,getdate()),0) Age, isnull((select isnull(c.Value,'') from Enpowerv4.dbo.NextOfKin b, titles c where b.titleid = c.titleid and employeeid = a.employeeid),'') +' '+ (select isnull(LastName,'')+' '+isnull(FirstName,'')+' '+isnull(MiddleName,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOK,isnull(OfficeStreetAddress1,'')+' '+ isnull(OfficeStreetAddress2,'')+' '+ isnull(OfficeTown,'') as OfficeAddress ,	OfficeLGAID,	OfficeStateID, isnull(ResidentialAddress1,'')+' '+	isnull(ResidentialAddress2,'') as ResidentialAddress ,	ResidentialStateID,	ResidentialLGAID,isnull(ContactAddress1,'')+' '+ isnull(ContactAddress2,'') as ContactAddress ,	ContactStateID,	ContactLGAID,email,JobTitle,Designation, AccountName,	AccountNumber,	BankID,	BankBranchID,employeeid, [dbo].[GetFundBalanceByDate](a.employeeid,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1)) RSABalance,[dbo].[GetFundBalanceByDate](a.employeeid,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) RFBalance, ((select isnull(sum(isnull(unitvalue,0)),0.000) from Enpowerv4.[dbo].[Contributions] where employeeid = a.employeeid and ContributionTypeID in (11,12,13,14)) * (select UnitPrice from Enpowerv4.dbo.UnitPrice where FundID = 2 and  valuedate =  (select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2)) )as AccruedRight, (case when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'PR' then 'Private' when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'ST' then 'Public' when substring((select employercode from Enpowerv4.dbo.employer where employerid = a.employerid),1,2) = 'PU' then 'Public' else 'No Sector'  end) Sector,isnull(BasicSalary,0.00) BasicSalary,	isnull(Transport,0.00) Transport,	isnull(Housing,0.00) Housing, Phone,(select employercode from Enpowerv4.dbo.employer where employerid = a.employerid) EmployerCode,employerid,sex,isnull((select isnull(c.Value,'') from  titles c where c.titleid = a.TitleID ),'') as Title,isnull((select MandatoryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Mandatory,isnull((select VoluntaryBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as AVC,isnull((select PreActNSITFBalance from [dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as Legacy,(select isnull(Phone ,'') from Enpowerv4.dbo.NextOfKin where employeeid = a.employeeid) as NOKPhone,isnull((select sum(UnitValue  ) from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2),0.0000) as TotalLegacyUnit,isnull((select sum(NetAmount) from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2),0) as TotalLegacyAmount,(select max( ValueDate)  from Contributions where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2 ) LagacyContValueDate, isnull((select top 1 UnitPrice  from Contributions b where EmployeeID = a.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID = 2 and valuedate = (select max( ValueDate)  from Contributions where EmployeeID = b.EmployeeID and (EmployeeLegacyAmount	+ EmployerLegacyAmount) > 0 and IsReversed = 0 and ContributionTypeID =2 )),0) LagacyContUnitPrice,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1) RSAPriceDate,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2) RFPriceDate,[dbo].udfGetWithdrawalsBF(a.EmployeeID,2,getdate()) as TotalRFPayment,isnull((select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,1,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 1))),0) as TotalNSITFValueRSA,isnull((select PreActNSITFBalance from Enpowerv4.[dbo].[udfGetBalanceBySource] (a.EmployeeID,2,(select max(ValueDate) from Enpowerv4.dbo.UnitPrice where FundID = 2))),0) as TotalNSITFValueRF,isnull((select (select sum(([EEPreactNSITFAmount]+[ERPreactNSITFAmount])) NSITFCont From vwContributions where EmployeeID= a.EmployeeID and FundID=1 and ([EEPreactNSITFAmount]+[ERPreactNSITFAmount]) >0) + (select sum(-[AmountFromPreActNSITF]) NSITFCont From vwPayments where EmployeeID= a.EmployeeID and FundID=1 and [AmountFromPreActNSITF] >0)),0) TotalNSITFUpload,rsapin,isnull(datediff(year,dateofbirth,('2016-12-31')),0) AgeAtRetirement,[dbo].[GetFundBalanceByDate](a.employeeid,2,'2016-12-31') YearEndRFBalance,isnull((select top 1 netAmount from payments where PaymentTypeID in (3,33,17) and employeeid = a.EmployeeID order by valueDate desc),0) LastPensionAmount,DateOfResignation from dbo.Employee a where rsapin  = @PIN", mycon)
			'DateOfResignation
			',isnull((select top 1 netAmount from payments here PaymentTypeID in (3,33,17) and employeeid = a.EmployeeID order by valueDate desc),0) LastPensionAmount

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			'TotalNSITFValueRSA,TotalNSITFValueRF
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PersonalDetails")
			dtUser = dsUser.Tables("PersonalDetails")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function

	Public Function getEmployerHistory(PIN As String) As DataTable

		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim EmployerHisCollection As New Hashtable

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("EnpowerV4")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select distinct a.EmployerID,employerName,(select employercode from employer where employerid = a.employerid) EmployerCode,(select max(EndPeriod) from contributions where employeeid = a.employeeid and employerid =a.employerid ) as LastFundDate from contributions a, dbo.Employee b , dbo.Employer c where a.employeeid = b.employeeid and a.employerid = c.employerid and b.rsapin = @PIN ) select * from tab order by 4 desc ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = PIN

			MyDataAdapter.Fill(dsUser, "EmployerHistory")
			dtUser = dsUser.Tables("EmployerHistory")

			'Dim i As Integer = 0
			'Do While i < dtUser.Rows.Count
			'     EmployerHisCollection.Add(dtUser.Rows(i).Item("employerName"), dtUser.Rows(i).Item("EmployerID"))
			'     i = i + 1
			'Loop

			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try

	End Function





	Public Function getAwaitingRole(rowID As Integer, returnName As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("ml_sp_rmasAwaiting", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@roleID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@roleID").Value = rowID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@returnName", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@returnName").Value = returnName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@checkdate", _
			    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@checkdate").Value = Now.Date


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "AwaitingRole")
			dtUser = dsUser.Tables("AwaitingRole")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function

	Public Function getAwaitingRoleFundReturn(rowID As Integer, returnName As String, frequency As Integer, reportDate As Date) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")

		Try

			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			If frequency = 1 Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("select b.txtrole from rmas_assignedRoleQCR a,moneybook_fund..tblroles b where txtReturnName = '" & returnName & "' and b.pkiroleid = a.fkiroleid and not exists (select * from rmas_approvedMFR where intDay = '" & Day(reportDate) & "' And intMonth = '" & Month(reportDate) & "' And intYear = '" & Year(reportDate) & "' and txtReturn = '" & returnName & "' and fkiroleid = '" & rowID & "')", mycon)
			Else
				MyDataAdapter = New SqlClient.SqlDataAdapter("select b.txtrole from rmas_assignedRoleQCR a,moneybook_fund..tblroles b where txtReturnName = 'PFAMFR100' and b.pkiroleid = a.fkiroleid and not exists (select * from rmas_approvedMFR where intDay = 0 And intMonth = '" & Month(reportDate) & "' And intYear = '" & Year(reportDate) & "' and txtReturn = '" & returnName & "' and fkiroleid = '" & rowID & "')", mycon)
			End If


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@roleID", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@roleID").Value = rowID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@returnName", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@returnName").Value = returnName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@checkdate", _
			    SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@checkdate").Value = Now.Date


			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "AwaitingRole")
			dtUser = dsUser.Tables("AwaitingRole")
			mycon.Close()
			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Function

	Public Sub insertRMASApproval(roleID As Integer, returnName As String)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")


		Try
			' mycon.Open()

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("ml_sp_rmasInsertQuarterlyApproval", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@roleID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@roleID").Value = roleID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@returnName", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@returnName").Value = returnName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@checkdate", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@checkdate").Value = Now.Date

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "CompanyProfile")
			'dtUser = dsUser.Tables("CompanyProfile")
			mycon.Close()

		Catch Ex As Exception
			'   MsgBox("" & Ex.Message)
		Finally

		End Try
	End Sub

	Public Sub insertRMASApproval(roleID As Integer, returnName As String, reportDate As Date, approvedBy As String, frequency As Integer)

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")


		Try
			' mycon.Open()

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into rmas_approvedMFR (fkiRoleID,intDay,intMonth,intYear,txtReturn,txtApprovedBy) values (@roleID,@day,@month,@year,@returnName,@ApprovedBy)", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@roleID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@roleID").Value = roleID

			If frequency = 1 Then
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@day", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@day").Value = Day(reportDate)
			Else
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@day", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@day").Value = 0
			End If



			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@month", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@month").Value = Month(reportDate)

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@year", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@year").Value = Year(reportDate)

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@returnName", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@returnName").Value = returnName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@ApprovedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@ApprovedBy").Value = approvedBy

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "CompanyProfile")
			'dtUser = dsUser.Tables("CompanyProfile")
			mycon.Close()

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
	End Sub

	Public Function isReturnAssigned(roleID As Integer, returnName As String) As Boolean

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")


		Try
			' mycon.Open()

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select count(*) from rmas_assignedRoleQCR where fkiroleID = @roleID and txtreturnName = @returnName ", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@roleID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@roleID").Value = roleID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@returnName", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@returnName").Value = returnName



			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "checkAssigned")
			dtUser = dsUser.Tables("checkAssigned")
			mycon.Close()

			If dtUser.Rows(0).Item(0) > 0 Then
				Return True
			Else
				Return False
			End If



		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
	End Function
	Public Function getReportPeriod(obj As Object) As String
		Dim dt As DataTable = obj, qtr As String = ""

		Select Case dt.Rows(0).Item("quarter")

			Case Is = "1"
				qtr = "First Quarter " & dt.Rows(0).Item("y_ear")
			Case Is = "2"
				qtr = "Second Quarter " & dt.Rows(0).Item("y_ear")
			Case Is = "3"
				qtr = "Third Quarter " & dt.Rows(0).Item("y_ear")
			Case Is = "4"
				qtr = "Fourth Quarter " & dt.Rows(0).Item("y_ear")
			Case Else

		End Select
		Return qtr

	End Function
	Public Function isRoleApproved(roleID As Integer, returnName As String) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")


		Try
			' mycon.Open()

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("ml_sp_rmasQuarterlydata", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@roleID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@roleID").Value = roleID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@returnName", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@returnName").Value = returnName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@checkdate", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@checkdate").Value = Now.Date

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "CompanyProfile")
			dtUser = dsUser.Tables("CompanyProfile")
			mycon.Close()



			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
	End Function

	Public Function isRoleApprovedFundReturn(roleID As Integer, returnName As String, returnDate As Date, frequency As Integer) As DataTable

		Dim myPCon As New SqlClient.SqlConnection
		Dim myComm As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection

		Dim mycon As New SqlClient.SqlConnection
		mycon = db.getConnection("RMAS")


		Try
			' mycon.Open()

			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			If frequency = 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select ((select count(*) from rmas_assignedRoleQCR where txtreturnName = '" & returnName & "') - (select count(*) from rmas_approvedmfr where fkiroleid = '" & roleID & "' and intday = '" & Day(CDate(returnDate)) & "' and intmonth = '" & Month(CDate(returnDate)) & "' and intYear = '" & Year(CDate(returnDate)) & "' and txtreturn = '" & returnName & "')) Awaiting, (select count(*) from rmas_assignedRoleQCR where txtreturnName = '" & returnName & "') AllRoles", mycon)

			Else

				MyDataAdapter = New SqlClient.SqlDataAdapter("select ((select count(*) from rmas_assignedRoleQCR where txtreturnName = '" & returnName & "') - (select count(*) from rmas_approvedmfr where fkiroleid = '" & roleID & "' and intday = '0' and intmonth = '" & Month(CDate(returnDate)) & "' and intYear = '" & Year(CDate(returnDate)) & "' and txtreturn = '" & returnName & "')) Awaiting, (select count(*) from rmas_assignedRoleQCR where txtreturnName = '" & returnName & "') AllRoles", mycon)
			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "FundApproval")
			dtUser = dsUser.Tables("FundApproval")
			mycon.Close()



			Return dtUser

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try
	End Function

	Public Function getAccessModule(uName As String) As DataTable
		Dim dtUser As New DataTable
		' Dim conn As New DataConnection

		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("PaymentModule")
		'con.Open()


		Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()
		Try

			Dim MyCommand As SqlCommand = New SqlCommand()
			MyCommand.Connection = con
			MyCommand.CommandText = "select distinct a.fkiroleid,c.txtModule,b.blnAdd,b.blnEdit,b.blnDelete,b.blnUpdate,c.pkiModuleID from tblusers a,tblroles b,tblModules c ,tblmlaccess d where a.username = @uName and a.fkiroleID = d.fkiroleID and d.fkiModuleID = c.pkiModuleID order by c.pkiModuleID asc"


			MyCommand.CommandType = CommandType.Text
			MyCommand.Connection = con

			MyCommand.Parameters.Add(New SqlClient.SqlParameter("@uName", SqlDbType.VarChar))
			MyCommand.Parameters("@uName").Value = uName

			MyAdapter.SelectCommand = MyCommand
			MyAdapter.Fill(dtUser)

			MyCommand.Dispose()
			con.Close()
			con = Nothing


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
		Return dtUser
	End Function

	Public Sub ExtractCSV(ByVal data As DataTable, ByVal fileName As String)

		Dim context As HttpContext = HttpContext.Current
		context.Response.Clear()
		context.Response.ContentType = "text/csv"
		context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".csv")

		'Write column header names
		For i = 0 To data.Columns.Count - 1
			If (i > 0) Then
				context.Response.Write(",")
			End If
			context.Response.Write(data.Columns(i).ColumnName)
		Next
		context.Response.Write(Environment.NewLine)

		For Each row As DataRow In data.Rows
			For i = 0 To data.Columns.Count - 1
				If (i > 0) Then
					context.Response.Write(",")
				End If
				context.Response.Write(row.Item(i).ToString().Replace(",", ""))
			Next
			context.Response.Write(Environment.NewLine)
		Next
		context.Response.End()

	End Sub
	Public Function getPenaltySummary(ByVal startDate As Date, ByVal endDate As Date, ByVal status As Integer, ByVal clientID As Integer, ByVal fund As String) As DataTable

		Dim dsUser As New DataSet
		Dim dtUser As New DataTable

		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fund)

		Dim MyAdapter As SqlDataAdapter = New SqlDataAdapter()


		Try

			Dim MyCommand As SqlCommand = New SqlCommand()

			MyCommand.CommandText = "ml_sp_getPaneltyLodgment"
			MyCommand.CommandType = CommandType.StoredProcedure
			MyCommand.Connection = con


			MyCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", SqlDbType.DateTime))
			MyCommand.Parameters("@date1").Value = startDate


			MyCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", SqlDbType.DateTime))
			MyCommand.Parameters("@date2").Value = endDate

			If status = 1 Then

				MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
				MyCommand.Parameters("@ClientID").Value = 0

			ElseIf status = 2 Then

				MyCommand.Parameters.Add(New SqlClient.SqlParameter("@ClientID", SqlDbType.Int))
				MyCommand.Parameters("@ClientID").Value = clientID

			End If

			MyCommand.Parameters.Add(New SqlClient.SqlParameter("@status", SqlDbType.Int))
			MyCommand.Parameters("@status").Value = status

			MyAdapter.SelectCommand = MyCommand
			MyAdapter.Fill(dsUser, "PenaltySummary")

			MyCommand.Dispose()

		Catch ex As Exception

		Finally
			con = Nothing
		End Try
		Return dsUser.Tables(0)
	End Function

	Public Function getFundMovementSummary(date1 As Date, date2 As Date, status As Integer, fund As String) As DataTable
		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fund)
		Dim dsUser As DataSet
		Dim dtUser As New DataTable

		command = con.CreateCommand
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("ml_sp_uploadExtract_backup", con)
		MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure


		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
			SqlDbType.DateTime))
		MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
		    SqlDbType.DateTime))
		MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@status", _
		    SqlDbType.Int))
		MyDataAdapter.SelectCommand.Parameters("@status").Value = status

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "fundMovement")
		dt = dsUser.Tables("fundMovement")
		db.close(fund)

		Return dt

	End Function

	Public Function getLodgmentSummary(date1 As Date, date2 As Date, fund As String) As DataTable
		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fund)
		Dim dsUser As DataSet
		Dim dtUser As New DataTable

		command = con.CreateCommand
		Dim MyDataAdapter As SqlClient.SqlDataAdapter
		MyDataAdapter = New SqlClient.SqlDataAdapter("ml_sp_getlodgementSummary", con)
		MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure


		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
			SqlDbType.DateTime))
		MyDataAdapter.SelectCommand.Parameters("@date1").Value = date1

		MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
		    SqlDbType.DateTime))
		MyDataAdapter.SelectCommand.Parameters("@date2").Value = date2

		dsUser = New DataSet()
		MyDataAdapter.Fill(dsUser, "lodgementSummary")
		dt = dsUser.Tables("lodgementSummary")
		db.close(fund)

		Return dt

	End Function
	Public Function getLodgmentYearlySummary(date2 As Date, fund As String) As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fund)
		Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter


			MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select year(valuedate) [Year],sum(balance) Lodgment,sum(oustanding) Outstanding from contributioncontrol where balance > 0 and year(valuedate) <= @date2 group by year(valuedate)) select *, ((Outstanding/Lodgment) * 100) as Percentage from tab", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date2", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@date2").Value = Year(date2)

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "YearlySummary")
			dt = dsUser.Tables("YearlySummary")
			db.close(fund)


		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function getLodgmentMonthlySummary(Yearr As Integer, fund As String) As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fund)
		Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			' MyDataAdapter = New SqlClient.SqlDataAdapter("select month(valuedate) , (select case when month(valuedate) = 1 then 'January' when month(valuedate) = 2 then 'February' when month(valuedate) = 3 then 'March' when month(valuedate) = 4 then 'April' when month(valuedate) = 5 then 'May' when month(valuedate) = 6 then 'June' when month(valuedate) = 7 then 'July' when month(valuedate) = 8 then 'August' when month(valuedate) = 9 then 'September' when month(valuedate) = 10 then 'October' when month(valuedate) = 11 then 'November' when month(valuedate) = 12 then 'December' else 'None' end) as [Month], sum(balance) Lodgment,sum(oustanding) Outstanding from contributioncontrol where year(valuedate) = @Year group by month(valuedate) order by 1 asc", con)


			MyDataAdapter = New SqlClient.SqlDataAdapter("with tab as (select month(valuedate) SN, (select case when month(valuedate) = 1 then 'January' when month(valuedate) = 2 then 'February' when month(valuedate) = 3 then 'March' when month(valuedate) = 4 then 'April' when month(valuedate) = 5 then 'May' when month(valuedate) = 6 then 'June' when month(valuedate) = 7 then 'July' when month(valuedate) = 8 then 'August' when month(valuedate) = 9 then 'September' when month(valuedate) = 10 then 'October' when month(valuedate) = 11 then 'November' when month(valuedate) = 12 then 'December' else 'None' end) as [Month], sum(balance) Lodgment,sum(oustanding) Outstanding from contributioncontrol where year(valuedate) = @Year group by month(valuedate) ) select *, ((Outstanding/Lodgment) * 100) Percentage from tab order by 1 asc", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@Year", _
			    SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@Year").Value = Yearr

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "MonthlySummary")
			dt = dsUser.Tables("MonthlySummary")
			db.close(fund)


		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function geteamID(ByVal teamName As String) As Integer

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try
			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select Team_Code from moneybook_fund.dbo.SaleTeam where Team_head = @Team_head ", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@Team_head", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@Team_head").Value = teamName
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")
		Catch ex As Exception

		End Try
		Return dt.Rows(0).Item(0)

	End Function

	Public Function getTeamAccount(team As String) As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiClientID,txtClientName,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus, (CASE WHEN txtDeStatus = 'P' THEN 'Pending Approval' END) as DeallocationStatus,txtStatus,txtDeStatus,(select team_head from saleteam where team_code = teamCode) as TeamName from saleaccount where teamCode = '" & geteamID(team) & "'", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")

		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function getTeamAccount(team As String, AccountName As String) As DataTable

		Dim dt As New DataTable, emp As String
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			emp = "%" & AccountName & "%"
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			If AccountName <> "" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiClientID,txtClientName,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus, (CASE WHEN txtDeStatus = 'P' THEN 'Pending Approval' END) as DeallocationStatus,txtStatus,txtDeStatus,(select team_head from saleteam where team_code = teamCode) as TeamName from saleaccount where teamCode = '" & geteamID(team) & "' and txtClientName like '" & emp & "' ", con)

			Else
				MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiClientID,txtClientName,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus, (CASE WHEN txtDeStatus = 'P' THEN 'Pending Approval' END) as DeallocationStatus,txtStatus,txtDeStatus,(select team_head from saleteam where team_code = teamCode) as TeamName from saleaccount where teamCode = '" & geteamID(team) & "'", con)

			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")





		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function PMgetEscalationEmail(UName As String, SeverityLevel As Integer) As List(Of EmailGateway.EmailAddress)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("PaymentModule")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter
			If SeverityLevel = 1 Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtSupervisorsEmail from tblUsers where UserName = '" & UName & "' ", con)
			Else
				MyDataAdapter = New SqlClient.SqlDataAdapter("select txtSupervisorsEmail from tblUsers where UserName = '" & UName & "' ", con)
			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "EscalationEmails")
			dt = dsUser.Tables("EscalationEmails")
			db.close("EscalationEmails")
			Dim emialAddys As New List(Of EmailGateway.EmailAddress)
			If dt.Rows.Count > 0 And dt.Rows(0).Item(0).ToString <> "" Then

				Dim aryEmail As Array = dt.Rows(0).Item(0).ToString.Split(","), i As Integer

				Do While i < aryEmail.Length
					Dim emialAddy As New EmailGateway.EmailAddress
					emialAddy.EmailAddress = aryEmail(i)
					emialAddy.Reciever = False
					emialAddys.Add(emialAddy)
					i = i + 1
				Loop

				Return emialAddys

			Else

				Return Nothing

			End If

		Catch ex As Exception

		End Try


	End Function

	Public Function PMgetLastNotification() As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("PaymentModule")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select dteLastNotificationSent from tblPreference", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "dteLastNotificationSent")
			dt = dsUser.Tables("dteLastNotificationSent")
			db.close("PaymentModule")

			Return dt

		Catch ex As Exception

		End Try


	End Function


	Public Function getTeamEmail(teamCode As String) As List(Of EmailGateway.EmailAddress)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtContactEmail from SaleTeam where Team_Code = '" & teamCode & "' ", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")
			Dim emialAddys As New List(Of EmailGateway.EmailAddress)
			If dt.Rows.Count > 0 And dt.Rows(0).Item(0) <> "" Then

				Dim aryEmail As Array = dt.Rows(0).Item(0).ToString.Split(","), i As Integer
				'Array.Reverse(aryfile)
				'fileName = aryfile(0)
				'i = aryEmail.Length - 1

				Do While i < aryEmail.Length
					Dim emialAddy As New EmailGateway.EmailAddress
					emialAddy.EmailAddress = aryEmail(i)
					emialAddy.Reciever = True
					emialAddys.Add(emialAddy)
					i = i + 1
				Loop
				Return emialAddys
			Else
				Return Nothing
			End If

		Catch ex As Exception

		End Try


	End Function
	Public Function getAccountTeamCode(ClientID As Integer) As Integer

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select teamCode from saleaccount where pkiClientID = " & ClientID & " ", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")

		Catch ex As Exception

		End Try

		Return dt.Rows(0).Item(0)

	End Function

	Public Function getTeamAccount(status As Integer, AccountName As String) As DataTable

		Dim dt As New DataTable, emp As String
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			emp = "%" & AccountName & "%"
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			'  If AccountName <> "" Then

			MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiClientID,txtClientName,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus, (CASE WHEN txtDeStatus = 'P' THEN 'Pending Approval' END) as DeallocationStatus,txtStatus,txtDeStatus,(select team_head from saleteam where team_code = teamCode) as TeamName from saleaccount where txtClientName like '" & emp & "' and teamCode is not null ", con)

			'Else
			'MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiClientID,txtClientName,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus, (CASE WHEN txtDeStatus = 'P' THEN 'Pending Approval' END) as DeallocationStatus,txtStatus,txtDeStatus,(select team_head from saleteam where team_code = teamCode) as TeamName from saleaccount where teamCode = '" & geteamID(team) & "'", con)

			'End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")





		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function getUpMappedAccount(accountName As String, rowCount As Integer) As DataTable

		Dim dt As New DataTable, emp As String, sql As String = ""
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			If Len(accountName) > 0 Then

				emp = "%" & accountName & "%"

				'sql = "SELECT pkiClientID, txtClientName,txtStatus  FROM SaleAccount where txtClientName like '" & emp & "' and teamCode is null or txtStatus = 'P' order by txtClientName"

				sql = "with tab as(SELECT pkiClientID, txtClientName,txtStatus,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus  FROM SaleAccount where txtClientName like '" & emp & "' and teamCode is null union all SELECT pkiClientID, txtClientName,txtStatus,(CASE WHEN txtStatus = 'P' THEN 'Pending Approval' END) as AllocationStatus  FROM SaleAccount where txtClientName like '" & emp & "' and txtStatus = 'P')select * from tab order by txtClientName asc"

			ElseIf Len(rowCount) > 0 Then
				emp = "top" & " " & rowCount
				sql = "select " & " " & emp & " " & " pkiClientID, txtClientName, txtStatus from saleaccount where teamCode is null or txtStatus = 'P' order by txtClientName"

			End If

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "SaleAccount")
			dt = dsUser.Tables("SaleAccount")
			db.close("MasterLodgment")

		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function getSalesTeam() As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			command.CommandTimeout = 2000
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select Team_head from moneybook_fund.dbo.SaleTeam", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")


		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function getApprovalRequests() As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select pkiClientID,	txtClientName,'Allocation Request' AloRequest,(select team_head from saleteam where team_code = teamCode) OwingTeam,txtMappedUser from saleaccount where txtstatus ='P' union all select pkiClientID,	txtClientName,'De-Allocation Request' AloRequest,(select team_head from saleteam where team_code = teamCode) OwingTeam,txtDeallocatedUser from saleaccount where txtdestatus ='P' ", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("MasterLodgment")


		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Sub sendAccountAllocationApproval(ClientID As Integer, uName As String, status As Char, teamCode As Integer, AllocationType As Integer)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")
		'Dim dsUser As DataSet
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If AllocationType = 0 Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("update SaleAccount set txtStatus = @status,teamCode = @teamCode, txtMappedUser = @txtMappedUser where pkiClientID = @pkiClientID ", con)
			ElseIf AllocationType = 1 Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("update SaleAccount set txtDeStatus = @status, txtDeallocatedUser = @txtMappedUser where pkiClientID = @pkiClientID ", con)
			End If



			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@status", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@status").Value = status

			If AllocationType = 0 Then
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@teamCode", _
							    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@teamCode").Value = teamCode
			ElseIf AllocationType = 1 Then

			End If


			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMappedUser", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtMappedUser").Value = uName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiClientID", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pkiClientID").Value = ClientID

			MyDataAdapter.SelectCommand.ExecuteNonQuery()


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub DeclineAccountAllocation(ClientID As Integer, uName As String, status As Char, RequesType As Integer)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")

		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If RequesType = 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("update SaleAccount set txtStatus = @status,txtApprovedBy = @txtApprovedBy,txtMappedUser = @txtMappedUser,teamCode = @teamCode where pkiClientID = @pkiClientID ", con)

			ElseIf RequesType = 0 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("update SaleAccount set txtDeStatus = @status, txtDeallocationApproval = @txtDeallocationApproval, txtDeallocatedUser = @txtDeallocatedUser where pkiClientID = @pkiClientID ", con)
			End If

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@status", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@status").Value = DBNull.Value

			If RequesType = 1 Then

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApprovedBy", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtApprovedBy").Value = uName

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMappedUser", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtMappedUser").Value = DBNull.Value

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@teamCode", SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@teamCode").Value = DBNull.Value

			ElseIf RequesType = 0 Then

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtDeallocationApproval", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtDeallocationApproval").Value = uName

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtDeallocatedUser", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtDeallocatedUser").Value = DBNull.Value

			End If

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiClientID", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pkiClientID").Value = ClientID

			MyDataAdapter.SelectCommand.ExecuteNonQuery()


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub




	Public Sub ApproveAccountAllocation(ClientID As Integer, uName As String, status As Char, RequesType As Integer)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MasterLodgment")

		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As New SqlClient.SqlDataAdapter

			If RequesType = 1 Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("update SaleAccount set txtStatus = @status,txtApprovedBy = @txtApprovedBy where pkiClientID = @pkiClientID ", con)

			ElseIf RequesType = 0 Then
				MyDataAdapter = New SqlClient.SqlDataAdapter("update SaleAccount set teamCode = null,txtStatus = null,txtApprovedBy = null, txtDeStatus = @status, txtDeallocationApproval = @txtDeallocationApproval where pkiClientID = @pkiClientID ", con)
			End If



			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@status", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@status").Value = status

			If RequesType = 1 Then
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApprovedBy", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtApprovedBy").Value = uName
			ElseIf RequesType = 0 Then
				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtDeallocationApproval", SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtDeallocationApproval").Value = uName
			End If

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@pkiClientID", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@pkiClientID").Value = ClientID

			MyDataAdapter.SelectCommand.ExecuteNonQuery()


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Function getUserDetails(UName As String) As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("PaymentModule")
		Dim dsUser As DataSet
		Try
			'select FullName from tblUsers where UserName = a.txtCreatedBy

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select top 1 *,(select txtRole from tblRoles where pkiRoleID =  a.fkiRoleID ) RoleName from tblusers a where username = @Username", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@Username", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@Username").Value = UName

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("PaymentModule")


		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Sub AddBrokerEmail(fundName As String, brokerID As Integer, email As String, uName As String, isReciever As Integer)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fundName)
		'Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into tblEquityBrokerEmailList (intBrokerID,txtEmailAddress,dteCreatedBy,IsReciever) values(@brokerID,@Email,@uName,@isReciever)", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@brokerID", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@brokerID").Value = brokerID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@Email", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@Email").Value = email

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@uName", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@uName").Value = uName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@isReciever", _
			    SqlDbType.Int))

			MyDataAdapter.SelectCommand.Parameters("@isReciever").Value = isReciever
			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "EmialList")
			'dt = dsUser.Tables("EmialList")
			'db.close("RSK")


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub
	Public Sub DeleteBrokerEmail(fundName As String, brokerID As Integer, email As String)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fundName)
		'Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("delete from tblEquityBrokerEmailList where intBrokerID = @brokerID and txtEmailAddress = @Email", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@brokerID", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@brokerID").Value = brokerID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@Email", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@Email").Value = email

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

			'dsUser = New DataSet()
			'MyDataAdapter.Fill(dsUser, "EmialList")
			'dt = dsUser.Tables("EmialList")
			'db.close("RSK")


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Function getPrepairedByFullName(fundName As String, name As String) As String

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fundName)
		Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable, sql As String
			sql = "select Surname +' '+ Othernames from moneybook_fund..IControl_Users where username = '" & LTrim(RTrim(name.Replace(" ", ""))) & "'"
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter(sql, con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			'MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@prepairedBy", SqlDbType.VarChar))
			'MyDataAdapter.SelectCommand.Parameters("@prepairedBy").Value = LTrim(RTrim(name))



			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "PrepairedBy")
			dt = dsUser.Tables("PrepairedBy")
			db.close(fundName)
			Dim unamsee As String = dt.Rows(0).Item(0).ToString
			Return unamsee

		Catch ex As Exception
			'MsgBox("" & ex.Message)

		End Try

	End Function

	Public Function getBrokerMsg(fundName As String, mandateDate As Date, broker As String, postedBy As String) As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection(fundName)
		Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("ml_getDailyMandate", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@date").Value = mandateDate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@broker", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@broker").Value = broker

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@postedBy", _
			SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@postedBy").Value = postedBy

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "BrokerMsg")
			dt = dsUser.Tables("BrokerMsg")
			db.close(fundName)


		Catch ex As Exception
			'MsgBox("" & ex.Message)

		End Try
		Return dt
	End Function

	Public Function getBrokerEmails(fundName As String, brokerID As String) As List(Of EmailGateway.EmailAddress)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("RSK")
		Dim dsUser As DataSet
		Dim i As Integer = 0
		Dim emialAddys As New List(Of EmailGateway.EmailAddress)
		'Dim emialAddys As New List(Of EmailAddress)
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtEmailAddress,IsReciever from moneytor_inv..brokers a,tblEquityBrokerEmailList b where a.id = intBrokerid and a.brokerid = @brokerID", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@brokerID", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@brokerID").Value = brokerID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "EmialList")
			dt = dsUser.Tables("EmialList")
			db.close("RSK")


			Do While i < dt.Rows.Count

				Dim emialAddy As New EmailGateway.EmailAddress
				'Dim emialAddy As New EmailAddress
				emialAddy.EmailAddress = dt.Rows(i).Item("txtEmailAddress")
				emialAddy.Reciever = dt.Rows(i).Item("IsReciever")
				emialAddys.Add(emialAddy)
				i = i + 1

			Loop


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
		Return emialAddys
	End Function

	Public Function getBrokerEmailsView(fundName As String, brokerID As Integer) As DataTable

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("RSK")
		Dim dsUser As DataSet
		Dim i As Integer = 0
		' Dim emialAddys As New List(Of EmailGateway.EmailAddress)
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select txtEmailAddress,IsReciever from moneytor_inv..brokers a,tblEquityBrokerEmailList b where a.id = intBrokerid and a.id = @brokerID", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@brokerID", _
			    SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@brokerID").Value = brokerID

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "EmialList")
			dt = dsUser.Tables("EmialList")
			db.close("RSK")


			'Do While i < dt.Rows.Count

			'    Dim emialAddy As New EmailGateway.EmailAddress
			'    emialAddy.EmailAddress = dt.Rows(i).Item("txtEmailAddress")
			'    emialAddy.Reciever = dt.Rows(i).Item("IsReciever")
			'    emialAddys.Add(emialAddy)
			'    i = i + 1
			'Loop


		Catch ex As Exception

		End Try
		Return dt
	End Function

	Public Function checkIfApproved(pdate As Date) As Boolean

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("RSA")
		Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from Enpower_midas..tblFundMovementApproval where dteProcessedDate = @date", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@date").Value = pdate

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("RSA")


		Catch ex As Exception

		End Try
		If dt.Rows.Count > 0 Then
			Return True
		Else
			Return False
		End If

	End Function

	Public Function getValidatedEmailSummary(pdate As Date) As Boolean

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("RSA")
		Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("select * from Enpower_midas..tblFundMovementApproval where dteProcessedDate = @date", con)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@date").Value = pdate

			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Users")
			dt = dsUser.Tables("Users")
			db.close("RSA")


		Catch ex As Exception

		End Try
		If dt.Rows.Count > 0 Then
			Return True
		Else
			Return False
		End If

	End Function

	Public Function getMovementFrequency() As DataTable

		Try

			Dim myPCon As New SqlClient.SqlConnection
			Dim myComm As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("MultiFund")
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			Dim str As String = "select intFrequency from tblMovementFrequency"
			MyDataAdapter = New SqlClient.SqlDataAdapter(str, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "tblMovementFrequency")
			dtUser = dsUser.Tables("tblMovementFrequency")
			db.close("MultiFund")

			Return dtUser

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function

	Public Sub InsertMovementFrequency(months As Integer, UName As String)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MultiFund")
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("ml_multifundFrequency", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@frequency", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@frequency").Value = months

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@ChangedPerson", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@ChangedPerson").Value = "o-taiwo"
			MyDataAdapter.SelectCommand.ExecuteNonQuery()


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub

	Public Sub InsertApproval(UName As String, PDate As Date)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("RSA")
		'Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into Enpower_midas..tblFundMovementApproval (txtApprovedBy,dteProcessedDate) values (@UName,@PDate)", con)
			command.CommandText = """ "
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@UName", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@UName").Value = UName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PDate", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@PDate").Value = PDate
			MyDataAdapter.SelectCommand.ExecuteNonQuery()


		Catch ex As Exception

		End Try

	End Sub

	Public Sub ImportLodgment(fundName As String)
		Try
			Dim myPCon As New SqlClient.SqlConnection
			'Dim myCon As New SqlClient.SqlConnection
			Dim myComm As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			' myCon.ConnectionString = cont_string
			'myCon.Open()

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("RSA")



			If fundName = "RSA" Then


				myComm.CommandText = "insert into ContributionControl (ID_JournalDetail,Description,LocalAmount,ValueDate,TransactionDate)select a.ID_JournalDetail,a.Description,a.LocalAmount,a.TransactionDate,a.ValueDate from JournalDetail a, JournalMaster b where b.ID_JournalMaster = a.ID_JournalMaster and b.Status = 'A' and a.GLActNo ='11100002' and a.DrCr ='-1' and substring(b.voucherno,1,3) = 'ext' and a.ID_JournalDetail not in ('5770','9337','9338','9339','9340','9339','9338','9337','9340') and b.id_journalmaster not in (19,564,572) and a.ID_JournalDetail  not in (select ID_JournalDetail from ContributionControl)"

			Else
			End If


			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()


			myComm.CommandText = "update ContributionControl set CSV_Verification='0',Reversals='0',Refunds='0',Balance=LocalAmount,Processed_Fund='0',Oustanding = LocalAmount, intMasterLodgmentID = ID_JournalDetail, blngrouplodgment = '0',EmailStatus = 0  where Balance Is null"
			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()


			myComm.CommandText = "update ContributionUncleared set Update_Status ='M'where Description in (select distinct Description from ContributionUncleared group by Description,Amount,Transaction_Date,Value_Date,Update_Status having count(1) > 1 and Update_Status is null)"
			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()


			myComm.CommandText = "update a  set a.ID_JournalDetail = b.ID_JournalDetail from ContributionUncleared a , ContributionControl b where  a.Description = b.Description and a.Amount = b.LocalAmount and a.Transaction_Date = b.TransactionDate and a.Value_Date = b.ValueDate and Update_Status is null"
			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()



			myComm.CommandText = "update b  set b.Employer_Code = a.Employer_Code from ContributionUncleared a , ContributionControl b where  a.Description = b.Description and a.Amount = b.LocalAmount and a.Transaction_Date = b.TransactionDate and a.Value_Date = b.ValueDate and Update_Status is null"
			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()

			'''''partial removal'''''

			myComm.CommandText = " insert into moneybook_fund.dbo.saleaccount (pkiClientID,txtclientName)select pkiClientID,txtclientName from enpower_midas.dbo.tblclients where pkiClientID not in (select pkiClientID from moneybook_fund.dbo.saleaccount where pkiClientID is not null) "
			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()


			myComm.CommandText = "select pkiClientid from moneybook_fund.dbo.saleaccount where isEnrolled is null"
			daUser.SelectCommand = myComm
			daUser.Fill(dsUser, "saleaccount")
			dtUser = dsUser.Tables("saleaccount")

			Dim i As Integer = 0
			Dim partcount As Integer = 0
			Dim EmpAdd As String
			Dim clientinf As New ArrayList

			Do While i < dtUser.Rows.Count
				clientinf.Clear()

				If getEnrollStatus(dtUser.Rows(i).Item("pkiClientid")) = 0 Then
					partcount = 0
				ElseIf getEnrollStatus(dtUser.Rows(i).Item("pkiClientid")) > 0 Then
					partcount = 1
				Else
				End If

				clientinf = getClientInfo(dtUser.Rows(i).Item("pkiClientid"))
				EmpAdd = clientinf.Item(3)
				EmpAdd = EmpAdd.Replace("'", "")
				myComm.CommandText = " update moneybook_fund.dbo.saleaccount set txtAddress = '" & EmpAdd & "',txtEMailAddress = '" & clientinf.Item(4) & "',isEnrolled='" & partcount & "' where pkiclientid ='" & dtUser.Rows(i).Item("pkiClientid") & "' "
				myComm.CommandType = CommandType.Text
				myComm.Connection = mycon
				myComm.ExecuteNonQuery()

				i = i + 1
			Loop


			myComm.CommandText = "delete from moneybook_fund.dbo.SaleAccount where pkiclientid not in (select pkiclientid from enpower_midas.dbo.tblclients)"
			myComm.CommandType = CommandType.Text
			myComm.Connection = mycon
			myComm.ExecuteNonQuery()



			mycon.Close()
			MsgBox("Master Lodgement Updated Successfully", , "Master Client")

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		End Try
	End Sub

	Public Function getEnrollStatus(ByVal ClientID As Integer) As Integer

		Dim sql As String = ""
		Dim conn As New SqlClient.SqlConnection
		conn.ConnectionString = "data Source=p-enpower;Initial Catalog=Enpower_midas;User ID=ibs;Pwd=vaug; Connection Timeout=360;Max Pool Size=600"
		conn.Open()
		Dim cmdUser As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		cmdUser = conn.CreateCommand
		cmdUser.CommandTimeout = 2000
		Try

			sql = "select count(txtidno) as totalPart from enpower_midas..memberinformationview where pkiClientid ='" & ClientID & "' "


			cmdUser.CommandText = sql
			daUser.SelectCommand = cmdUser
			daUser.Fill(dsUser, "Membership")
			dtUser = dsUser.Tables("Membership")

			Return dtUser.Rows(0).Item("totalPart")

			conn.Close()
		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function
	Public Sub updateMovementMandate(refID As Integer, txtPIN As String, txtPrefferdFund As String, UName As String)

		Try
			Dim myComm As New SqlClient.SqlCommand
			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("MultiFund")
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblmovement set isCustomerMandate = 1,txtMandateRequestedDate = @date1,	txtMandateRequestedBy = @RequestedBy,txtPrefferedFund = @txtFundTo, txtMandateStatus = @txtMandateStatus where txtPIN = @PIN and  intMovementID = @refID", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMandateStatus", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtMandateStatus").Value = "P"

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtFundTo", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtFundTo").Value = txtPrefferdFund

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
				SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = Now

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@RequestedBy", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@RequestedBy").Value = UName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@PIN").Value = txtPIN

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@refID", _
				SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@refID").Value = refID

			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			db.close("RSA")

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally

		End Try


	End Sub

	Public Sub updateMovementBatch(bStatus As String, txtBatch As String, UName As String)

		Try
			Dim myComm As New SqlClient.SqlCommand
			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("MultiFund")
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMovementBatchManagement set txtStatus = @txtStatus,txtApprovedBy = @txtApprovedBy, dteApproved = @dteApproved where txtBatchNo = @txtBatchNo", mycon)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtBatchNo", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtBatchNo").Value = txtBatch

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", _
				SqlDbType.Char))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = bStatus

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteApproved", _
				SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteApproved").Value = Now

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApprovedBy", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApprovedBy").Value = UName

			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			db.close("RSA")

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally
			'Return dtUser
		End Try

	End Sub

	Public Sub updateMovementMandate(refID As Integer, MStatus As Char, UName As String)

		Try
			Dim myComm As New SqlClient.SqlCommand
			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection("MultiFund")
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			If MStatus = "A" Then

				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblmovement set txtMandateStatus = @txtMandateStatus,txtMandateApprovedBy = @txtMandateApprovedBy, txtMandateApprovedDate = @txtMandateApprovedDate where intMovementID = @refID", mycon)

				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMandateStatus", _
					SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtMandateStatus").Value = MStatus

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@refID", _
					SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@refID").Value = refID

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMandateApprovedDate", _
					SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtMandateApprovedDate").Value = Now

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMandateApprovedBy", _
					SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtMandateApprovedBy").Value = UName

			Else

				MyDataAdapter = New SqlClient.SqlDataAdapter("update tblmovement set txtMandateStatus = @txtMandateStatus, txtMandateRequestedBy = @txtMandateRequestedBy where intMovementID = @refID", mycon)
				MyDataAdapter.SelectCommand.CommandType = CommandType.Text

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMandateStatus", _
					SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtMandateStatus").Value = MStatus

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@refID", _
					SqlDbType.Int))
				MyDataAdapter.SelectCommand.Parameters("@refID").Value = refID

				MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtMandateRequestedBy", _
				    SqlDbType.VarChar))
				MyDataAdapter.SelectCommand.Parameters("@txtMandateRequestedBy").Value = UName

			End If

			MyDataAdapter.SelectCommand.ExecuteNonQuery()
			db.close("RSA")

		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally
			'Return dtUser
		End Try


	End Sub

	Public Function getPINDetails(ByVal txtIDNo As String) As DataTable

		Dim db As New DbConnection
		Dim conn As New SqlClient.SqlConnection
		conn = db.getConnection("MultiFund")

		Dim sql As String = ""
		Dim Cinf As New ArrayList

		Cinf.Clear()

		Dim cmdUser As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		'Dim i As Integer
		cmdUser = conn.CreateCommand
		Try
			'sql = "select top 10 txtTitle+' . '+isnull(txtFirstname,'')+'  '+	isnull(txtOtherNames,'') +'  '+	isnull(txtSurname,'') as FullName from tblpeople where txtIDno = '" & txtIDNo & "' "

			sql = "select txtFullName as FullName, (select txtFundDescription from tblFundDefinition where txtFundID = txtPrefferedFund) as PreferredFund, intAge as Age,txtMandateRequestedDate as MandateDate from tblMovement where txtPIN = '" & txtIDNo & "'"

			cmdUser.CommandTimeout = 2000
			cmdUser.CommandText = sql
			daUser.SelectCommand = cmdUser
			daUser.Fill(dsUser, "tblPeople")
			dtUser = dsUser.Tables("tblPeople")

			'Cinf.Add(dtUser.Rows(0).Item("txtaltClientName"))
			'Cinf.Add(dtUser.Rows(0).Item("pkiClientID"))
			'Cinf.Add(dtUser.Rows(0).Item("txtsarsno"))
			'Cinf.Add(dtUser.Rows(0).Item("address"))
			'Cinf.Add(dtUser.Rows(0).Item("txtemailaddress"))
			'Cinf.Add(dtUser.Rows(0).Item("txtsubplanid"))
			'Cinf.Add(dtUser.Rows(0).Item("txtClientName"))
			'Cinf.TrimToSize()

			conn.Close()
		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
		Return dtUser
	End Function


	Public Function getClientInfo(ByVal clientID As Integer) As ArrayList
		Dim sql As String = ""
		Dim Cinf As New ArrayList
		Dim conn As New SqlClient.SqlConnection
		Dim cont_string As String = "data Source=p-enpower;Connection Timeout=360;Max Pool Size=600;Initial Catalog=Enpower;User ID=ibs;Pwd=vaug"

		Cinf.Clear()
		conn.ConnectionString = cont_string
		conn.Open()

		Dim cmdUser As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		'Dim i As Integer
		cmdUser = conn.CreateCommand
		Try
			sql = "select isnull(txtaltClientName,'') as txtaltClientName,pkiClientID,txtsarsno,isnull(txtaddress1,'') +''+isnull(txtaddress2,'')+''+isnull(txtaddress3,'') as address,isnull(txtemailaddress,'') as txtemailaddress,txtsubplanid,isnull(txtClientName,'') as txtClientName from Enpower_midas.dbo.tblclients where pkiClientID ='" & clientID & "' "
			cmdUser.CommandTimeout = 2000
			cmdUser.CommandText = sql
			daUser.SelectCommand = cmdUser
			daUser.Fill(dsUser, "tblclients")
			dtUser = dsUser.Tables("tblclients")

			Cinf.Add(dtUser.Rows(0).Item("txtaltClientName"))
			Cinf.Add(dtUser.Rows(0).Item("pkiClientID"))
			Cinf.Add(dtUser.Rows(0).Item("txtsarsno"))
			Cinf.Add(dtUser.Rows(0).Item("address"))
			Cinf.Add(dtUser.Rows(0).Item("txtemailaddress"))
			Cinf.Add(dtUser.Rows(0).Item("txtsubplanid"))
			Cinf.Add(dtUser.Rows(0).Item("txtClientName"))
			Cinf.TrimToSize()
			Return Cinf
			conn.Close()
		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function

	Public Function ApproveMandate(fundName As String, mdate As Date) As DataTable
		Try
			Dim myPCon As New SqlClient.SqlConnection
			'Dim myCon As New SqlClient.SqlConnection
			Dim myComm As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			' myCon.ConnectionString = cont_string
			'myCon.Open()

			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection(fundName)



			'  Command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			'  MyDataAdapter = New SqlClient.SqlDataAdapter("select Comments,UniqueID,status, (select name from dbo.EqMaster where shareid = a.shareid) as Stock,BrokerID as [Broker],issuedate as [Date of Mandate], unitcost as [Max =N=],qtyunit as [Unit],crdr as [TransactionStatus] from eqtreas a where issuedate = @date1 ", mycon)


			' MyDataAdapter = New SqlClient.SqlDataAdapter("select Comments,UniqueID,status, (select name from EqMaster where shareid = a.shareid) as Stock,BrokerID as [Broker], issuedate as [Date of Mandate], unitcost as [Max =N=],qtyunit as [Unit],crdr as [TransactionStatus], (select case when (select count(fkiUniqueID) from  riskco..tblEquityMandateApproval where txtFund = @fundName and  fkiUniqueID = a.UniqueID )  = 1 then 'A' else 'P' end) as [MandateStatus] from eqtreas a where issuedate = @date1", mycon)

			MyDataAdapter = New SqlClient.SqlDataAdapter("select Comments,ID_ShareMandateDetail as [UniqueID], status,(select name from EqMaster where shareid = a.shareid) as Stock,BrokerID as [Broker],mandatedate as [Date of Mandate],MaxPrice as [Max =N=],Qty as [Unit],TransactionType as [TransactionType],(select case when (select count (fkiUniqueID) from  riskco..tblEquityMandateApproval where txtFund = @fundName and  fkiUniqueID = a.ID_ShareMandateDetail )  = 1 then 'A' else 'P' end) as [MandateStatus]  from dbo.ShareMandateDetails a where mandatedate = @date1", mycon)


			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@date1", _
				SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@date1").Value = mdate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fundName", _
				SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@fundName").Value = fundName.Replace("I", "").Trim
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Mandate")
			dtUser = dsUser.Tables("Mandate")
			db.close("RSA")


			Return dtUser


		Catch Ex As Exception
			'MsgBox("" & Ex.Message)
		Finally
			'Return dtUser
		End Try
	End Function
	Public Function getBrokers(fundName As String) As DataTable


		Try

			Dim myPCon As New SqlClient.SqlConnection
			'Dim myCon As New SqlClient.SqlConnection
			Dim myComm As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection(fundName)
			Dim MyDataAdapter As SqlClient.SqlDataAdapter

			MyDataAdapter = New SqlClient.SqlDataAdapter("select ID,BrokerID,Name from dbo.Brokers order by 3 asc", mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Mandate")
			dtUser = dsUser.Tables("Mandate")
			db.close("RSA")

			Return dtUser

		Catch ex As Exception

		End Try
	End Function
	Public Function getBrokers(fundName As String, brokerName As String) As DataTable


		Try

			Dim myPCon As New SqlClient.SqlConnection
			'Dim myCon As New SqlClient.SqlConnection
			Dim myComm As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			Dim db As New DbConnection
			Dim mycon As New SqlClient.SqlConnection
			mycon = db.getConnection(fundName)
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			Dim broker As String = "%" & brokerName & "%"
			Dim str As String = "select ID,BrokerID,Name from dbo.Brokers where name like '" & broker & "' order by 3 asc"
			MyDataAdapter = New SqlClient.SqlDataAdapter(str, mycon)
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text
			dsUser = New DataSet()
			MyDataAdapter.Fill(dsUser, "Mandate")
			dtUser = dsUser.Tables("Mandate")
			db.close("RSA")

			Return dtUser

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function




	Public Function getState() As Hashtable
		Dim StateCollection As New Hashtable
		StateCollection.Add("AB", "ABIA")
		StateCollection.Add("AD", "ADAMAWA")
		StateCollection.Add("AK", "AKWA IBOM")
		StateCollection.Add("AN", "ANAMBRA")
		StateCollection.Add("BA", "BAUCHI")
		StateCollection.Add("BY", "BAYELSA")
		StateCollection.Add("BE", "BENUE")
		StateCollection.Add("BO", "BORNO")
		StateCollection.Add("CR", "CROSS RIVER")
		StateCollection.Add("DT", "DELTA")
		StateCollection.Add("EB", "EBONYI")
		StateCollection.Add("ED", "EDO")
		StateCollection.Add("EK", "EKITI")
		StateCollection.Add("EN", "ENUGU")
		StateCollection.Add("FC", "FCT")
		StateCollection.Add("GB", "GOMBE")
		StateCollection.Add("IM", "IMO")
		StateCollection.Add("JG", "JIGAWA")
		StateCollection.Add("KD", "KADUNA")
		StateCollection.Add("KN", "KANO")
		StateCollection.Add("KT", "KATSINA")
		StateCollection.Add("KB", "KEBBI")
		StateCollection.Add("KG", "KOGI")
		StateCollection.Add("KW", "KWARA")
		StateCollection.Add("LA", "LAGOS")
		StateCollection.Add("NR", "NASSARAWA")
		StateCollection.Add("NG", "NIGER")
		StateCollection.Add("OG", "OGUN")
		StateCollection.Add("OD", "ONDO")
		StateCollection.Add("OS", "OSUN")
		StateCollection.Add("OY", "OYO")
		StateCollection.Add("PL", "PLATEAU")
		StateCollection.Add("RV", "RIVERS")
		StateCollection.Add("SO", "SOKOTO")
		StateCollection.Add("TB", "TARABA")
		StateCollection.Add("YB", "YOBE")
		StateCollection.Add("ZA", "ZAMFARA")

		Return StateCollection

	End Function
	Public Sub insertMandateApproval(dteMandate As Date, UniqueID As Integer, txtApprovedBy As String, txtInitiatedBy As String, fund As String)



		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("iRSA")
		'Dim dsUser As DataSet
		Try


			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("insert into RiskCo..tblEquityMandateApproval (dteMandate,fkiUniqueID,txtApprovedBy,txtInitiatedBy,txtFund) values (@dteMandate,@UniqueID,@txtApprovedBy,@txtInitiatedBy,@fund)", con)
			command.CommandText = """ "
			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteMandate", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteMandate").Value = dteMandate

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@UniqueID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@UniqueID").Value = UniqueID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApprovedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApprovedBy").Value = txtApprovedBy

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtInitiatedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtInitiatedBy").Value = txtInitiatedBy

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fund", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@fund").Value = fund

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Sub

	Public Function getFundDefinitions() As DataTable
		Try
			Dim sql As String = ""
			Dim db As New DbConnection
			Dim con As New SqlClient.SqlConnection
			con = db.getConnection("MultiFund")

			Dim cmdUser As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			cmdUser = con.CreateCommand
			cmdUser.CommandTimeout = 2000

			sql = "select txtFundID,txtFundDescription,intStartAge,intEndAge from tblFundDefinition"

			cmdUser.CommandText = sql
			daUser.SelectCommand = cmdUser
			daUser.Fill(dsUser, "tblFundDefinition")
			dtUser = dsUser.Tables("tblFundDefinition")
			db.close("MultiFund")
			Return dtUser

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function

	Public Function getMovementData(txtbatchRef As String) As DataTable

		Dim sql As String = ""
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection

		Dim cmdUser As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		cmdUser = con.CreateCommand
		cmdUser.CommandTimeout = 2000

		cmdUser = con.CreateCommand
		Try
			con = db.getConnection("MultiFund")
			daUser = New SqlClient.SqlDataAdapter("with tab as (select  b.txtbatchNo,txtPIN as PIN,txtFundFrom HomeFund, convert(decimal(18,4),intUnit) HomeUnit,intFromUnitPrice HomePrice,convert(decimal(18,2),(intUnit * intFromUnitPrice)) HomeValue,txtFundTo EndFund,convert(decimal(18,4) ,((intUnit * intFromUnitPrice) / intToUnitPrice)) EndUnit, intToUnitPrice EndPrice,convert(decimal(18,2),(intUnit * intFromUnitPrice)) EndValue from tblMovement a,tblMovementBatchManagement b where a.txtBatchNo = b.txtBatchNo and b.txtstatus in ('P','F')) select txtbatchNo BatchNo,(select txtFundDescription from tblFundDefinition where txtFundID = HomeFund) as HomeFund ,sum(HomeUnit) HomeUnit,(select top 1 intFromUnitPrice from tblMovement a,tab b where a.txtBatchNo = b.txtBatchNo) HomePrice, sum(HomeValue) HomeValue, (select txtFundDescription from tblFundDefinition where txtFundID = EndFund) as EndFund,sum(EndUnit) EndUnit,(select top 1 intToUnitPrice from tblMovement a,tab b where a.txtBatchNo = b.txtBatchNo) EndPrice,sum(EndValue) EndValue,(select txtStatus from tblMovementBatchManagement where txtBatchNo = a.txtBatchNo and a.txtBatchNo = @txtbatchNo) as Status,null MandateStatus from tab a group by txtbatchNo,HomeFund,EndFund", con)

			daUser.SelectCommand.CommandType = CommandType.Text

			daUser.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtbatchNo", SqlDbType.VarChar))
			daUser.SelectCommand.Parameters("@txtbatchNo").Value = txtbatchRef

			daUser.Fill(dsUser, "FundMovement")
			dtUser = dsUser.Tables("FundMovement")
			db.close("MultiFund")

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
		Return dtUser
	End Function


	Public Function getMovementData(type As Integer, fundID As String, PIN As String, batch As String, fundID2 As String) As DataTable
		Try
			Dim sql As String = ""
			Dim db As New DbConnection
			Dim con As New SqlClient.SqlConnection
			con = db.getConnection("MultiFund")

			Dim cmdUser As New SqlClient.SqlCommand
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable
			cmdUser = con.CreateCommand
			cmdUser.CommandTimeout = 2000

			cmdUser = con.CreateCommand
			'Dim MyDataAdapter As SqlClient.SqlDataAdapter
			daUser = New SqlClient.SqlDataAdapter("mfund_pickMovementDataRevised", con)

			daUser.SelectCommand.CommandType = CommandType.StoredProcedure

			daUser.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@reportType", SqlDbType.Int))
			daUser.SelectCommand.Parameters("@reportType").Value = type

			daUser.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fundID", SqlDbType.VarChar))
			daUser.SelectCommand.Parameters("@fundID").Value = fundID

			daUser.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@PIN", SqlDbType.VarChar))
			daUser.SelectCommand.Parameters("@PIN").Value = PIN

			daUser.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtBatchNo", SqlDbType.VarChar))
			daUser.SelectCommand.Parameters("@txtBatchNo").Value = batch

			daUser.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@fundID2", SqlDbType.VarChar))
			daUser.SelectCommand.Parameters("@fundID2").Value = fundID2

			'daUser.SelectCommand.

			'cmdUser.CommandText = sql
			'daUser.SelectCommand = cmdUser
			daUser.Fill(dsUser, "FundMovement")
			dtUser = dsUser.Tables("FundMovement")
			db.close("MultiFund")
			Return dtUser


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
	End Function

	Public Sub updateMovementStatus(batchNo As String, status As Char, PIN As String, UName As String)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MultiFund")
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("update tblMovementBatchManagement set txtApprovalRequestedBy = @txtApprovalRequestedBy, dteApprovalRequested= @dteApprovalRequested,txtStatus = @txtStatus", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.Text

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtApprovalRequestedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtApprovalRequestedBy").Value = UName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@dteApprovalRequested", SqlDbType.DateTime))
			MyDataAdapter.SelectCommand.Parameters("@dteApprovalRequested").Value = Now

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtStatus", SqlDbType.Char))
			MyDataAdapter.SelectCommand.Parameters("@txtStatus").Value = status

			MyDataAdapter.SelectCommand.ExecuteNonQuery()

		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try


	End Sub

	Public Function getMovedBatch(AStatus As String) As DataTable

		Dim conn As New SqlClient.SqlConnection, command As New SqlClient.SqlCommand, lstMovements As New List(Of MovementProperties)
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MultiFund")

		command = New SqlClient.SqlCommand
		daUser = New SqlClient.SqlDataAdapter
		dsUser = New DataSet
		dtUser = New DataTable
		command.Connection = con
		command.CommandText = "with tab as (select a.txtMandateStatus,a.intMovementID,a.txtFullName,b.txtbatchNo,txtPIN as PIN,txtFundFrom HomeFund, convert(decimal(18,4),intUnit) HomeUnit,intFromUnitPrice HomePrice,convert(decimal(18,2),(intUnit * intFromUnitPrice)) HomeValue,txtFundTo EndFund,convert(decimal(18,4) ,((intUnit * intFromUnitPrice) / intToUnitPrice)) EndUnit, intToUnitPrice EndPrice,convert(decimal(18,2),(intUnit * intFromUnitPrice)) EndValue from tblMovement a,tblMovementBatchManagement b where a.txtBatchNo = b.txtBatchNo and b.txtStatus in (@AStatus)) select txtbatchNo, sum(HomeUnit) HomeUnit, sum(HomeValue) HomeValue ,sum(EndUnit) EndUnit,sum(EndValue) EndValue, (select top 1 dteBatchCreated from tblMovementBatchManagement where txtBatchNo = a.txtBatchNo) CreatedDate from tab a group by txtbatchNo"

		command.CommandType = CommandType.Text

		command.Parameters.Add(New SqlClient.SqlParameter("@AStatus", SqlDbType.VarChar))
		command.Parameters("@AStatus").Value = AStatus

		daUser.SelectCommand = command
		daUser.Fill(dsUser, "MultiFund")
		dtUser = dsUser.Tables("MultiFund")
		Return dtUser


	End Function

	Public Function getGetMovementFrequency() As Integer

		Dim sql As String = ""
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection


		Dim cmdUser As New SqlClient.SqlCommand
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Try
			cmdUser.CommandTimeout = 2000
			con = db.getConnection("MultiFund")
			cmdUser = con.CreateCommand

			sql = "select isnull(intFrequency,0)  from tblMovementFrequency"

			cmdUser.CommandText = sql
			daUser.SelectCommand = cmdUser
			daUser.Fill(dsUser, "MovementFrequency")
			dtUser = dsUser.Tables("MovementFrequency")
			db.close("MultiFund")



		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try
		Return dtUser.Rows(0).Item(0)
	End Function
	Public Sub InsertFundDefinition(fundID As Integer, fundName As String, startAge As Integer, endAge As Integer)

		Dim dt As New DataTable
		Dim command As New SqlClient.SqlCommand
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MultiFund")
		Try

			Dim dtUser As New DataTable
			command = con.CreateCommand
			Dim MyDataAdapter As SqlClient.SqlDataAdapter
			MyDataAdapter = New SqlClient.SqlDataAdapter("ml_fundDefinition", con)

			MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtFundID", SqlDbType.Int))
			MyDataAdapter.SelectCommand.Parameters("@txtFundID").Value = fundID

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@txtFundDescription", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@txtFundDescription").Value = fundName

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intStartAge", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@intStartAge").Value = startAge

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@intEndAge", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@intEndAge").Value = endAge

			MyDataAdapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@CreatedBy", SqlDbType.VarChar))
			MyDataAdapter.SelectCommand.Parameters("@CreatedBy").Value = "o-taiwo"

			MyDataAdapter.SelectCommand.ExecuteNonQuery()


		Catch ex As Exception
			'MsgBox("" & ex.Message)
		End Try

	End Sub


	Public Function getMovedPIN(pin As String, AStatus As String) As DataTable

		Dim conn As New SqlClient.SqlConnection, command As New SqlClient.SqlCommand, lstMovements As New List(Of MovementProperties)
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MultiFund")

		command = New SqlClient.SqlCommand
		daUser = New SqlClient.SqlDataAdapter
		dsUser = New DataSet
		dtUser = New DataTable
		command.Connection = con
		command.CommandText = "with tab as (select a.txtMandateStatus,a.intMovementID,a.txtFullName,b.txtbatchNo,txtPIN as PIN,txtFundFrom HomeFund, convert(decimal(18,4),intUnit) HomeUnit,intFromUnitPrice HomePrice,convert(decimal(18,2),(intUnit * intFromUnitPrice)) HomeValue,txtFundTo EndFund,convert(decimal(18,4) ,((intUnit * intFromUnitPrice) / intToUnitPrice)) EndUnit, intToUnitPrice EndPrice,convert(decimal(18,2),(intUnit * intFromUnitPrice)) EndValue from tblMovement a,tblMovementBatchManagement b where a.txtBatchNo = b.txtBatchNo and a.txtMandateStatus in (@AStatus)) select PIN,txtbatchNo BatchNo, (select txtFundDescription from tblFundDefinition where txtFundID = HomeFund) as HomeFund ,sum(HomeUnit) HomeUnit,(select top 1 intFromUnitPrice from tblMovement a,tab b where a.txtBatchNo = b.txtBatchNo) HomePrice, sum(HomeValue) HomeValue,	(select txtFundDescription from tblFundDefinition where txtFundID = EndFund) as EndFund ,sum(EndUnit) EndUnit,(select top 1 intToUnitPrice from tblMovement a,tab b where a.txtBatchNo = b.txtBatchNo) EndPrice,sum(EndValue) EndValue , txtFullName,intMovementID as RefID,txtMandateStatus from tab group by txtbatchNo,HomeFund,EndFund,PIN ,txtFullName,intMovementID,txtMandateStatus"

		command.CommandType = CommandType.Text

		command.Parameters.Add(New SqlClient.SqlParameter("@AStatus", SqlDbType.VarChar))
		command.Parameters("@AStatus").Value = AStatus

		daUser.SelectCommand = command
		daUser.Fill(dsUser, "MultiFund")
		dtUser = dsUser.Tables("MultiFund")
		Return dtUser


	End Function


	Public Function getPINs(ByVal path As String, ByVal myfile As String) As List(Of MovementProperties)
		Try
			Dim aryObj As New ArrayList
			Dim conn As New SqlClient.SqlConnection, command As New SqlClient.SqlCommand, lstMovements As New List(Of MovementProperties)
			Dim daUser As New SqlClient.SqlDataAdapter
			Dim dsUser As New DataSet
			Dim dtUser As New DataTable

			'conn.ConnectionString = cont_string
			'conn.Open()

			Dim db As New DbConnection
			Dim con As New SqlClient.SqlConnection
			con = db.getConnection("MultiFund")

			Dim strfilename As String
			Dim num_rows As Long
			Dim num_cols As Long
			Dim x As Integer
			Dim y As Integer
			Dim strarray(1, 1) As String
			Dim pinarray As New ArrayList

			' Load the file.
			strfilename = path & "\" & myfile


			'Check if file exist
			If File.Exists(strfilename) Then
				Dim tmpstream As StreamReader = File.OpenText(strfilename)
				Dim strlines() As String
				Dim strline() As String

				strlines = tmpstream.ReadToEnd().Split(Environment.NewLine)

				num_rows = UBound(strlines)
				strline = strlines(0).Split(",")
				num_cols = UBound(strline)


				ReDim strarray(num_rows, num_cols)

				'                    For x = 0 To num_rows - 1

				Do While x <= num_rows

					' For x = 0 To num_rows

					strline = strlines(x).Split(",")

					Dim PIN As String
					PIN = LTrim(RTrim(strline(0).ToString)).Replace(vbCr, "").Replace(vbLf, "")

					command = New SqlClient.SqlCommand
					daUser = New SqlClient.SqlDataAdapter
					dsUser = New DataSet
					dtUser = New DataTable
					command.Connection = con
					command.CommandText = "mfund_pickMovementDataRevised"
					command.CommandType = CommandType.StoredProcedure
					command.Parameters.Add(New SqlClient.SqlParameter("@reportType", SqlDbType.Int))
					command.Parameters("@reportType").Value = 2

					command.Parameters.Add(New SqlClient.SqlParameter("@fundID", SqlDbType.VarChar))
					command.Parameters("@fundID").Value = ""

					command.Parameters.Add(New SqlClient.SqlParameter("@PIN", SqlDbType.VarChar))
					command.Parameters("@PIN").Value = LTrim(RTrim(PIN))

					command.Parameters.Add(New SqlClient.SqlParameter("@txtBatchNo", SqlDbType.VarChar))
					command.Parameters("@txtBatchNo").Value = ""

					command.Parameters.Add(New SqlClient.SqlParameter("@fundID2", SqlDbType.VarChar))
					command.Parameters("@fundID2").Value = ""


					daUser.SelectCommand = command
					daUser.Fill(dsUser, "MultiFund")
					dtUser = dsUser.Tables("MultiFund")

					If dtUser.Rows.Count > 0 Then
						' MsgBox("" & dtUser.Rows(0).Item("PIN").ToString)
						Dim lstMovement As New MovementProperties
						lstMovement.RefID = dtUser.Rows(0).Item("RefID")
						lstMovement.PIN = dtUser.Rows(0).Item("PIN")
						lstMovement.BatchNo = dtUser.Rows(0).Item("BatchNo")
						lstMovement.HomeFund = dtUser.Rows(0).Item("HomeFund")
						lstMovement.HomeUnit = dtUser.Rows(0).Item("HomeUnit")
						lstMovement.HomePrice = dtUser.Rows(0).Item("HomePrice")
						lstMovement.HomeValue = dtUser.Rows(0).Item("HomeValue")
						lstMovement.EndFund = dtUser.Rows(0).Item("EndFund")
						lstMovement.EndUnit = dtUser.Rows(0).Item("EndUnit")
						lstMovement.EndPrice = dtUser.Rows(0).Item("EndPrice")
						lstMovement.EndValue = dtUser.Rows(0).Item("EndValue")
						lstMovement.MandateStatus = dtUser.Rows(0).Item("txtMandateStatus")
						lstMovements.Add(lstMovement)

					Else

					End If

					' Next

					x = x + 1
				Loop


				'aryObj.Add(lstMovements)
				'aryObj.Add(dtUser)

				Return lstMovements

			End If
			con.Close()
		Catch ex As Exception

			'MsgBox("Sorry! an Error uploading ID:  " & ex.Message)
		Finally

		End Try

	End Function


	Public Function getMovedPIN(pin As String) As DataTable

		Dim conn As New SqlClient.SqlConnection, command As New SqlClient.SqlCommand, lstMovements As New List(Of MovementProperties)
		Dim daUser As New SqlClient.SqlDataAdapter
		Dim dsUser As New DataSet
		Dim dtUser As New DataTable
		Dim db As New DbConnection
		Dim con As New SqlClient.SqlConnection
		con = db.getConnection("MultiFund")

		command = New SqlClient.SqlCommand
		daUser = New SqlClient.SqlDataAdapter
		dsUser = New DataSet
		dtUser = New DataTable
		command.Connection = con
		command.CommandText = "mfund_pickMovementDataRevised"
		command.CommandType = CommandType.StoredProcedure
		command.Parameters.Add(New SqlClient.SqlParameter("@reportType", SqlDbType.Int))
		command.Parameters("@reportType").Value = 2

		command.Parameters.Add(New SqlClient.SqlParameter("@fundID", SqlDbType.VarChar))
		command.Parameters("@fundID").Value = ""

		command.Parameters.Add(New SqlClient.SqlParameter("@PIN", SqlDbType.VarChar))
		command.Parameters("@PIN").Value = LTrim(RTrim(pin))

		command.Parameters.Add(New SqlClient.SqlParameter("@txtBatchNo", SqlDbType.VarChar))
		command.Parameters("@txtBatchNo").Value = ""

		command.Parameters.Add(New SqlClient.SqlParameter("@fundID2", SqlDbType.VarChar))
		command.Parameters("@fundID2").Value = ""

		daUser.SelectCommand = command
		daUser.Fill(dsUser, "MultiFund")
		dtUser = dsUser.Tables("MultiFund")
		Return dtUser


	End Function




End Class

<Serializable()> _
Public Class InvestmentIncome

	Dim intSchemeCode As Integer
	Dim txtReturnName As String
	Dim txtDescription As String
	Dim numCost As Double
	Dim numRevenue As Double
	Dim numGainLoss As Double
	Dim Incomes As List(Of InvestmentIncome)

	Property scheme() As Integer
		Get
			Return intSchemeCode
		End Get
		Set(ByVal value As Integer)
			intSchemeCode = value
		End Set
	End Property
	Property returnName() As String
		Get
			Return txtReturnName
		End Get
		Set(ByVal value As String)
			txtReturnName = value
		End Set
	End Property
	Property item() As String
		Get
			Return txtDescription
		End Get
		Set(ByVal value As String)
			txtDescription = value
		End Set
	End Property

	Property cost() As Double
		Get
			Return numCost
		End Get
		Set(ByVal value As Double)
			numCost = value
		End Set
	End Property
	Property revenue() As Double
		Get
			Return numRevenue
		End Get
		Set(ByVal value As Double)
			numRevenue = value
		End Set
	End Property
	Property net() As Double
		Get
			Return numGainLoss
		End Get
		Set(ByVal value As Double)
			numGainLoss = value
		End Set
	End Property




End Class


Public Class MovementProperties
	Dim mDate As Date
	Dim mRefID As Integer
	Dim mFullName As String
	Dim mPIN As String
	Dim mBatchNo As String
	Dim mHomeFund As String
	Dim mHomeUnit As Decimal
	Dim mHomePrice As Decimal
	Dim mHomeValue As Decimal
	Dim mEndFund As String
	Dim mEndUnit As Decimal
	Dim mEndPrice As Decimal
	Dim mEndValue As Decimal
	Dim mStatus As Char
	Dim mMandateStatus As Char

	Property MovementDate As Date
		Get
			Return mDate
		End Get
		Set(ByVal value As Date)
			mDate = value
		End Set
	End Property

	Property RefID As Integer
		Get
			Return mRefID
		End Get
		Set(ByVal value As Integer)
			mRefID = value
		End Set
	End Property

	Property FullName As String
		Get
			Return mFullName
		End Get
		Set(ByVal value As String)
			mFullName = value
		End Set
	End Property

	Property PIN As String
		Get
			Return mPIN
		End Get
		Set(ByVal value As String)
			mPIN = value
		End Set
	End Property
	Property BatchNo As String
		Get
			Return mBatchNo
		End Get
		Set(ByVal value As String)
			mBatchNo = value
		End Set
	End Property

	Property HomeFund As String
		Get
			Return mHomeFund
		End Get
		Set(ByVal value As String)
			mHomeFund = value
		End Set
	End Property

	Property HomeUnit As Decimal
		Get
			Return mHomeUnit
		End Get
		Set(ByVal value As Decimal)
			mHomeUnit = value
		End Set

	End Property

	Property HomePrice As Decimal
		Get
			Return mHomePrice
		End Get
		Set(ByVal value As Decimal)
			mHomePrice = value
		End Set

	End Property

	Property HomeValue As Decimal
		Get
			Return mHomeValue
		End Get
		Set(ByVal value As Decimal)
			mHomeValue = value
		End Set

	End Property

	Property EndFund As String
		Get
			Return mEndFund
		End Get
		Set(ByVal value As String)
			mEndFund = value
		End Set

	End Property

	Property EndUnit As Decimal
		Get
			Return mEndUnit
		End Get
		Set(ByVal value As Decimal)
			mEndUnit = value
		End Set

	End Property

	Property EndPrice As Decimal
		Get
			Return mEndPrice
		End Get
		Set(ByVal value As Decimal)
			mEndPrice = value
		End Set

	End Property

	Property EndValue As Decimal
		Get
			Return mEndValue
		End Get
		Set(ByVal value As Decimal)
			mEndValue = value
		End Set

	End Property

	Property Status As Char
		Get
			Return mStatus
		End Get
		Set(ByVal value As Char)
			mStatus = value
		End Set

	End Property

	Property MandateStatus As Char
		Get
			Return mMandateStatus
		End Get
		Set(ByVal value As Char)
			mMandateStatus = value
		End Set

	End Property



End Class