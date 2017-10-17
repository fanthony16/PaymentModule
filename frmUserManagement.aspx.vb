Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports AjaxControlToolkit
Partial Class frmUserManagement
     Inherits System.Web.UI.Page
     Dim UserCollection As New Hashtable
     Dim ModuleCollection As New Hashtable
     Dim RoleModuleCollection As New Hashtable
     Dim RoleCollection As New Hashtable

     Public Sub getUserAccessMenu()

          Dim cr As New Core
          Dim dtAccessModule As New DataTable
          Dim aryAccessModule As New ArrayList
          Dim i As Integer, j As Integer, k As Integer
          dtAccessModule = cr.getAccessModule("System")

          Do While i < dtAccessModule.Rows.Count

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


     End Sub



     Protected Sub DeleteRole_Click()

     End Sub
     
     Protected Sub DeactivateUser_Click(sender As Object, e As EventArgs)

          Dim btnDeactiveUser As New Button, cr As New Core
          btnDeactiveUser = sender
          Dim i As GridViewRow
          i = btnDeactiveUser.NamingContainer
          cr.PMDeactivateUser(Me.gridUsers.Rows(i.RowIndex).Cells(0).Text)

          LoadGridUsers(getUserList("A", ""))


     End Sub

     Protected Sub ActivateUser_Click(sender As Object, e As EventArgs)

          Dim btnActiveUser As New Button, cr As New Core
          btnActiveUser = sender
          Dim i As GridViewRow
          i = btnActiveUser.NamingContainer
          cr.PMActivateUser(Me.gridUsers.Rows(i.RowIndex).Cells(0).Text)
          LoadGridUsers(getUserList("A", ""))
     End Sub

     Public Function getUserList(filterType As String, filterBy As String) As DataTable
          Try

               Dim dc As New UsersDataContext, dtUser As New DataTable, dtColumn As New DataColumn

               dtUser = New DataTable
               dtColumn = New DataColumn("UserID")
               dtUser.Columns.Add(dtColumn)
               dtColumn = New DataColumn("FullName")
               dtUser.Columns.Add(dtColumn)
               dtColumn = New DataColumn("RoleName")
               dtUser.Columns.Add(dtColumn)
               dtColumn = New DataColumn("IsActive")
               dtUser.Columns.Add(dtColumn)


               Dim lstUsers As New List(Of String)

               If filterType = "Z" Then
                    Dim query = From m In dc.tblUsers Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Where m.UserName.Contains(filterBy) _
                           Select New With {m.UserName, m.FullName, n.txtRole, m.IsActive}
                    For Each a In query

                         Dim newCustomersRow As DataRow
                         newCustomersRow = dtUser.NewRow()

                         newCustomersRow("UserID") = a.UserName
                         newCustomersRow("FullName") = a.FullName
                         newCustomersRow("RoleName") = a.txtRole
                         newCustomersRow("IsActive") = a.IsActive

                         dtUser.Rows.Add(newCustomersRow)

                    Next
               ElseIf filterType = "A" Then
                    Dim query = From m In dc.tblUsers Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Where m.IsActive = True And m.UserName.Contains(filterBy) _
                           Select New With {m.UserName, m.FullName, n.txtRole, m.IsActive}
                    For Each a In query

                         Dim newCustomersRow As DataRow
                         newCustomersRow = dtUser.NewRow()

                         newCustomersRow("UserID") = a.UserName
                         newCustomersRow("FullName") = a.FullName
                         newCustomersRow("RoleName") = a.txtRole
                         newCustomersRow("IsActive") = a.IsActive

                         dtUser.Rows.Add(newCustomersRow)

                    Next
               ElseIf filterType = "D" Then
                    Dim query = From m In dc.tblUsers Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Where m.IsActive = False And m.UserName.Contains(filterBy) _
                           Select New With {m.UserName, m.FullName, n.txtRole, m.IsActive}
                    For Each a In query

                         Dim newCustomersRow As DataRow
                         newCustomersRow = dtUser.NewRow()

                         newCustomersRow("UserID") = a.UserName
                         newCustomersRow("FullName") = a.FullName
                         newCustomersRow("RoleName") = a.txtRole
                         newCustomersRow("IsActive") = a.IsActive

                         dtUser.Rows.Add(newCustomersRow)

                    Next
               End If

               Return dtUser
          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message)
               pnlError.Visible = True
               Me.lblError.Text = "Error Loading Users"
          End Try
          Return Nothing
     End Function

     Public Function getModuleList() As DataTable
          Try

               Dim dc As New UsersDataContext, dtModule As New DataTable, dtColumn As New DataColumn

               dtModule = New DataTable
               dtColumn = New DataColumn("ModuleID")
               dtModule.Columns.Add(dtColumn)
               dtColumn = New DataColumn("ModuleDesc")
               dtModule.Columns.Add(dtColumn)
               dtColumn = New DataColumn("IsActive")
               dtModule.Columns.Add(dtColumn)

               Dim query = From m In dc.tblModules _
                           Select New With {m.pkiModuleID, m.txtModule, m.isActive}
               For Each a In query

                    Dim newCustomersRow As DataRow
                    newCustomersRow = dtModule.NewRow()

                    newCustomersRow("ModuleID") = a.pkiModuleID
                    newCustomersRow("ModuleDesc") = a.txtModule
                    newCustomersRow("IsActive") = a.isActive
                    dtModule.Rows.Add(newCustomersRow)

               Next
               Return dtModule

          Catch ex As Exception

               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message)
               pnlError.Visible = True
               Me.lblError.Text = "Error Loading Users"

          End Try

          Return Nothing

     End Function

     Public Function getRoleModuleList(roleID As Integer) As List(Of String)
          Try

               Dim dc As New UsersDataContext

               Dim lstUsers As New List(Of String)
               Dim query = From m In dc.tblMLAccesses Join n In dc.tblRoles On m.fkiRoleID Equals n.pkiRoleID Join O In dc.tblModules On O.pkiModuleID Equals m.fkiModuleID Where n.pkiRoleID = roleID And m.isActive = True _
                           Select New With {m.fkiModuleID, O.txtModule}
               For Each a In query

                    lstUsers.Add(a.fkiModuleID & " | " & a.txtModule)

                    If RoleModuleCollection.ContainsKey(a.txtModule) = True Then
                    Else
                         RoleModuleCollection.Add(a.txtModule, a.fkiModuleID)
                    End If

               Next
               ViewState("RoleModuleCollection") = RoleModuleCollection
               Return lstUsers
          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message)
               pnlError.Visible = True
               Me.lblError.Text = "Error Loading Users"
          End Try
          Return Nothing
     End Function

     Protected Sub gridUsers_RowDataBound(sender As Object, e As GridViewRowEventArgs)
          Dim btnA As New Button, btnD As New Button
          If IsNothing(ViewState("Users")) = False Then

               Dim dt As DataTable = ViewState("Users")
               If e.Row.RowType = DataControlRowType.DataRow Then
                    btnA = e.Row.FindControl("btnActiveUser")
                    btnD = e.Row.FindControl("btnDeactiveUser")
                    If dt.Rows(e.Row.RowIndex).Item("IsActive").ToString = False Then

                         

                         e.Row.ForeColor = System.Drawing.Color.Red
                         btnD.Enabled = False
                         btnA.Enabled = True
                         'e.Row.Enabled = False

                    Else
                         btnD.Enabled = True
                         btnA.Enabled = False
                    End If

               End If
          Else

          End If

     End Sub


     Public Function getRoleList() As DataTable
          Try

               Dim dc As New UsersDataContext, dtRoles As New DataTable, dtColumn As New DataColumn

               dtRoles = New DataTable
               dtColumn = New DataColumn("RoleID")
               dtRoles.Columns.Add(dtColumn)
               dtColumn = New DataColumn("RoleDesc")
               dtRoles.Columns.Add(dtColumn)
               dtColumn = New DataColumn("IsActive")
               dtRoles.Columns.Add(dtColumn)
               


               Dim query = From m In dc.tblRoles _
                           Select New With {m.pkiRoleID, m.txtRole, m.isActive}
               For Each a In query

                    Dim newCustomersRow As DataRow
                    newCustomersRow = dtRoles.NewRow()
                    newCustomersRow("RoleID") = a.pkiRoleID
                    newCustomersRow("RoleDesc") = a.txtRole
                    newCustomersRow("IsActive") = a.isActive
                    dtRoles.Rows.Add(newCustomersRow)

               Next
               Return dtRoles
          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message)
               pnlError.Visible = True
               Me.lblError.Text = "Error Loading Users"
          End Try
          Return Nothing
     End Function

     Public Function getRoleUsers(roleID As Integer) As List(Of String)
          Try

               Dim dc As New UsersDataContext, dtRoles As New DataTable, dtColumn As New DataColumn

               Dim lstUsers As New List(Of String)

               Dim query = From m In dc.tblRoles Join n In dc.tblUsers On m.pkiRoleID Equals n.fkiRoleID Where n.fkiRoleID = roleID And n.IsActive = True _
                           Select New With {n.UserName}

               For Each a In query

                    lstUsers.Add(a.UserName)
                   
               Next

               Return lstUsers

          Catch ex As Exception
               Dim logerr As New Global.Logger.Logger
               logerr.FileSource = "Payment Module"
               logerr.FilePath = Server.MapPath("~/Logs")
               logerr.Logger(ex.Message)
               pnlError.Visible = True
               Me.lblError.Text = "Error Loading Users"
          End Try
          Return Nothing
     End Function

     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
          Dim dtUsers As New DataTable, dtModules As New DataTable, dtRoles As New DataTable, i As Integer = 0

          If IsPostBack = False Then

               dtUsers = getUserList("A", "")
               dtModules = getModuleList()
               dtRoles = getRoleList()
			ViewState("Roles") = dtRoles
			ViewState("Modules") = dtModules
			LoadGridUsers(dtUsers)
               LoadGridModules(dtModules)
               LoadGridRoles(dtRoles)

               populateUnAssigned(dtUsers)

               Do While i < dtRoles.Rows.Count
                    lstRole.Items.Add(dtRoles.Rows(i).Item(1))
                    RoleCollection.Add(dtRoles.Rows(i).Item(1), dtRoles.Rows(i).Item(0))
                    i = i + 1
               Loop

               ViewState("RoleCollection") = RoleCollection
               i = 0
               Do While i < dtModules.Rows.Count

                    Me.lstUnAssignedModules.Items.Add(dtModules.Rows(i).Item(1))
                    ModuleCollection.Add(dtModules.Rows(i).Item(1), dtModules.Rows(i).Item(0))

                    i = i + 1
               Loop
               ViewState("ModuleCollection") = ModuleCollection
               getUserAccessMenu()

          Else
               getUserAccessMenu()
          End If
     End Sub
     Protected Sub populateUnAssigned(dt As DataTable)

          Dim i As Integer = 0
          lstUnAssigned.Items.Clear()
          Do While i < dt.Rows.Count

               If dt.Rows(i).Item("IsActive") = True And dt.Rows(i).Item("RoleName") = "Default" Then
                    Me.lstUnAssigned.Items.Add(dt.Rows(i).Item("UserID"))
               Else
               End If

               i = i + 1

          Loop


     End Sub

     Protected Sub LoadGridRoles(dt As DataTable)
          Me.gridRoles.DataSource = dt
          gridRoles.DataBind()


     End Sub

     Protected Sub LoadGridUsers(dt As DataTable)
          ViewState("Users") = dt
          Me.gridUsers.DataSource = dt
          gridUsers.DataBind()
     End Sub

     Protected Sub LoadGridModules(dt As DataTable)
          Me.gridModule.DataSource = dt
          gridModule.DataBind()
     End Sub

     Protected Sub btnApplyFilter_Click(sender As Object, e As EventArgs) Handles btnApplyFilter.Click
          Dim dt As DataTable
          If Me.rdAll.Checked = True Then
               ' retrieves all users that has made a successful logon attempt to the system
               dt = getUserList("Z", txtSearchFilter.Text)
               LoadGridUsers(dt)
          ElseIf Me.rdInActive.Checked = True Then
               'retrieves all de-activated users
               dt = getUserList("D", txtSearchFilter.Text)
               LoadGridUsers(dt)
          ElseIf Me.rdActive.Checked = True Then
               'retrieves all activate users
               dt = getUserList("A", txtSearchFilter.Text)
               LoadGridUsers(dt)
          End If

     End Sub

     Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

          Dim cr As New Core, i As Integer

          If Me.rdReadOnly.Checked = True Then
			cr.PMInsertUserRole(Me.txtRoleDescription.Text, True, Session("user"))
               Me.txtRoleDescription.Text = ""

               Dim dtRoles As DataTable = getRoleList()
               LoadGridRoles(dtRoles)

               lstRole.Items.Clear()
               Do While i < dtRoles.Rows.Count
                    lstRole.Items.Add(dtRoles.Rows(i).Item(1))
                    i = i + 1
               Loop

          ElseIf Me.rdReadWrite.Checked = True Then

			cr.PMInsertUserRole(Me.txtRoleDescription.Text, False, Session("user"))
               Me.txtRoleDescription.Text = ""
          End If


     End Sub

     Protected Sub populateAssign(lstUsers As List(Of String))
          Dim i As Integer = 0
          lstAssigned.Items.Clear()
          Do While i < lstUsers.Count
               lstAssigned.Items.Add(lstUsers.Item(i))
               i = i + 1
          Loop
	End Sub

	Protected Sub gridRoles_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridRoles.PageIndexChanging

		If IsNothing(ViewState("Roles")) = False Then

			Dim dtRoles As New DataTable
			Me.gridRoles.PageIndex = e.NewPageIndex
			dtRoles = ViewState("Roles")
			LoadGridRoles(dtRoles)

		Else
		End If

	End Sub

     Protected Sub gridRoles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridRoles.SelectedIndexChanged

          Dim rowIndex As Integer = gridRoles.SelectedIndex, roleID As Integer = 0, lstUsers As List(Of String), i As Integer = 0
          roleID = CInt(gridRoles.Rows(rowIndex).Cells(1).Text)
          ViewState("roleID") = roleID
          lstUsers = getRoleUsers(roleID)
          populateAssign(lstUsers)


     End Sub

     Protected Sub imgAddUserToRole_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddUserToRole.Click

          Dim cr As New Core, dtUsers As DataTable, lstUsers As New List(Of String)
          'imgRemoveAllUserFromRole

          If IsNothing(ViewState("roleID")) = False And IsNothing(lstUnAssigned.SelectedItem.Text) = False Then

               cr.PMUpdateUserRole(CInt(ViewState("roleID")), lstUnAssigned.SelectedItem.Text)
               lstUsers = getRoleUsers(CInt(ViewState("roleID")))
               populateAssign(lstUsers)
          Else

          End If
          dtUsers = getUserList("A", "")
          populateUnAssigned(dtUsers)

          



          'populateUnAssigned


     End Sub

     Protected Sub imgRemoveUserFromRole_Click(sender As Object, e As ImageClickEventArgs) Handles imgRemoveUserFromRole.Click

          Dim cr As New Core, dtUsers As DataTable, roleID As Integer, lstUsers As List(Of String)


          If IsNothing(lstAssigned.SelectedItem.Text) = False Then
               cr.PMUpdateUserRole(1, lstAssigned.SelectedItem.Text)
          Else

          End If
          dtUsers = getUserList("A", "")
          populateUnAssigned(dtUsers)

          If IsNothing(ViewState("roleID")) = False Then
               roleID = ViewState("roleID")
               lstUsers = getRoleUsers(roleID)
               populateAssign(lstUsers)
          Else
          End If

          


     End Sub

     Protected Sub lstUnAssigned_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUnAssigned.SelectedIndexChanged

     End Sub

     Protected Sub imgAddModuleToRole_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddModuleToRole.Click
          Dim cr As New Core

          'Dim items As ListItem
          'items = New ListItem(RTrim(LTrim(lstUnAssignedModules.SelectedItem.Text)))

          'If Me.lstAssignedModules.Items.Contains(items) Then
          '     MsgBox("Item Already Exists")
          '     Exit Sub
          '     ' If lstSchedule.Items.FindByText(item).Value.Count > 0 Then
          'Else

          'End If

          If IsNothing(lstUnAssignedModules.SelectedItem.Text) = False And IsNothing(lstRole.SelectedItem.Text) = False Then

               RoleCollection = ViewState("RoleCollection")
               ModuleCollection = ViewState("ModuleCollection")
			cr.PMInsertAccess(CInt(RoleCollection.Item(lstRole.SelectedItem.Text)), CInt(ModuleCollection.Item(lstUnAssignedModules.SelectedItem.Text)), Session("user"))
               refreshRoleModuleList(lstRole.SelectedItem.Text)
          Else

          End If




     End Sub
     Protected Sub refreshRoleModuleList(roleName As String)

          Dim lstModules As List(Of String), i As Integer
          If IsNothing(ViewState("RoleCollection")) = False Then
               RoleCollection = ViewState("RoleCollection")
               lstModules = getRoleModuleList(CInt(RoleCollection.Item(roleName)))

               lstAssignedModules.Items.Clear()
               Do While i < lstModules.Count
                    Me.lstAssignedModules.Items.Add(lstModules.Item(i).Split("|")(1))
                    i = i + 1
               Loop

          Else

          End If

     End Sub
     Protected Sub lstRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRole.SelectedIndexChanged

          ' Dim lstModules As List(Of String), i As Integer
          refreshRoleModuleList(lstRole.SelectedItem.Text)
          'If IsNothing(ViewState("RoleCollection")) = False Then
          '     RoleCollection = ViewState("RoleCollection")
          '     lstModules = getRoleModuleList(CInt(RoleCollection.Item(lstRole.SelectedItem.Text)))

          '     lstAssignedModules.Items.Clear()
          '     Do While i < lstModules.Count
          '          Me.lstAssignedModules.Items.Add(lstModules.Item(i).Split("|")(1))
          '          i = i + 1
          '     Loop

          'Else

          'End If

     End Sub



     Protected Sub imgRemoveModuleFromRole_Click(sender As Object, e As ImageClickEventArgs) Handles imgRemoveModuleFromRole.Click

          Dim cr As New Core

          If IsNothing(lstAssignedModules.SelectedItem.Text) = False And IsNothing(lstRole.SelectedItem.Text) = False Then

               RoleCollection = ViewState("RoleCollection")
               ModuleCollection = ViewState("ModuleCollection")
			cr.PMRemoveAccess(CInt(RoleCollection.Item(lstRole.SelectedItem.Text)), CInt(ModuleCollection.Item(LTrim(RTrim(lstAssignedModules.SelectedItem.Text)))), Session("user"))

               refreshRoleModuleList(lstRole.SelectedItem.Text)

          Else

          End If

     End Sub

     Protected Sub lstUnAssignedModules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUnAssignedModules.SelectedIndexChanged

     End Sub

	Protected Sub gridModule_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridModule.PageIndexChanging

		If IsNothing(ViewState("Modules")) = False Then

			Dim dtModules As New DataTable
			Me.gridModule.PageIndex = e.NewPageIndex
			dtModules = ViewState("Modules")
			LoadGridRoles(dtModules)

		Else
		End If

	End Sub
End Class
