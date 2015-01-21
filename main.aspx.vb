Imports System.Data
Imports System.Data.OleDb
Partial Class Default3
    Inherits System.Web.UI.Page


    Public SQL_ORDER As String = ""
    Dim СтрокаПодкл As String = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("Database.accdb")
    Dim IdClient As String = ""
    Dim CONNECTION As New OleDbConnection(СтрокаПодкл)
    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        IdClient = Session("IdClient")
        If Not Page.IsPostBack Then
            Try
                GetGridView1(SQL_ORDER, "")
                GetGridView2()
                DropDownList1.SelectedIndex = -1
            Catch ex As Exception
            End Try
        Else

        End If



    End Sub


    Protected Sub GetGridView1(ByVal SQL_ORDER As String, ByVal SQL_WHERE As String)
        Dim SQL_запрос1 As String = ""
        SQL_запрос1 =
            " SELECT Tasks.Name, Tasks.Description, Tasks.TimeCreate, Tasks.TimeUpdate, " &
            "Tasks.TimeElapsed, Status.TypeStatus, (Users.Surname+"" ""+Users.Name ) AS FI ,Tasks.IdTask " &
            "FROM         ((((Clients INNER JOIN " &
            "Tasks ON Clients.idClient = Tasks.idClient) INNER JOIN " &
            "EmployeeTask ON Tasks.idTask = EmployeeTask.idTask) INNER JOIN " &
            "Status ON Tasks.idStatus = Status.idStatus) INNER JOIN " &
            "Users ON Clients.idClient = Users.idClient AND EmployeeTask.idUser = Users.idUser) where Tasks.IdClient=" & IdClient

        If SQL_WHERE <> "" And SQL_WHERE <> Nothing Then
            SQL_запрос1 += " AND Tasks.IdStatus= " + SQL_WHERE
        End If

        If SQL_ORDER <> "" And SQL_ORDER <> Nothing Then
           
            SQL_запрос1 += " ORDER BY " + SQL_ORDER
        End If
        Try
            Dim COMMAND As New OleDbCommand
            COMMAND.CommandText = SQL_запрос1
            COMMAND.Connection = CONNECTION
            Dim ds As New DataSet
            Dim df As Boolean = Page.IsPostBack


            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(SQL_запрос1, СтрокаПодкл)

            adapter.Fill(ds)
            'GridView1.DataSource = New Data.DataTable
            'GridView1.DataBind()

            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            GridView1.Columns(GridView1.Columns.Count - 3).Visible = False
            GridView1.Visible = True
            GridView1.ViewStateMode = UI.ViewStateMode.Disabled



        Catch ex As Exception

        End Try
    End Sub
    ' формируем грид для полей сортировки
    Protected Sub GetGridView2()
        Dim Massiv(,) As String = {{"Tasks.Name", "Название"}, {"TimeCreate", "Дата создания"},
                                 {"TimeUpdate", "Дата изменения"}, {"TimeElapsed", "Затраченное время"}}
        Dim tbl As DataTable = New DataTable()
        Dim workRow As DataRow
        Dim column1 As DataColumn = New DataColumn()
        Dim column2 As DataColumn = New DataColumn()

        column1.ColumnName = "Value"
        column2.ColumnName = "Name"
        tbl.Columns.Add(column1)
        tbl.Columns.Add(column2)
        Dim h As Integer
        h = Massiv.GetUpperBound(0)
        For i As Integer = 0 To Massiv.GetUpperBound(0) Step 1
            workRow = tbl.NewRow()
            workRow("Value") = Massiv(i, 0).ToString()
            workRow("Name") = Massiv(i, 1).ToString()
            tbl.Rows.Add(workRow)
        Next
        GridView2.DataSource = tbl
        GridView2.DataBind()
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Button5.Click
        'Me.CheckBoxList1.Visible = True
        'Me.Label4.Visible = True
        'Me.Button5.Visible = False
    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim idStatus As Integer
        idStatus = DropDownList1.SelectedValue
        'Dim item As ListItem

        GetGridView1(Session("sort"), idStatus)

    End Sub





    Public Sub chkActive_Clicked(ByVal sender As Object, e As EventArgs)
        Dim cb As CheckBox
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        Dim hd As HiddenField
        For i As Integer = 0 To GridView2.Rows.Count - 1 Step 1
            '  TryCast(GridView2.Rows(i).FindControl("CheckBox777"), CheckBox).Checked = headerCheckBox.Checked
            cb = CType(GridView2.Rows(i).FindControl("CheckBox777"), CheckBox)
            hd = CType(GridView2.Rows(i).FindControl("HiddenField777"), HiddenField)
            If cb.Checked Then
                SQL_ORDER += hd.Value + ","
            End If
        Next
        If SQL_ORDER <> "" Then
            SQL_ORDER = SQL_ORDER.Remove(SQL_ORDER.Length - 1, 1)
            Session.Add("sort", SQL_ORDER)
        End If
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        mviewMain.SetActiveView(viewGrid)
        mviewMain.Visible = True
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.BackColor = System.Drawing.Color.Azure
        End If

    End Sub
    'редактирование задачт
    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim idtask As Integer
        GridView1.EditIndex = e.NewEditIndex 'порядковый номер строки в GridView, которую собираемся редактировать
        idtask = GridView1.SelectedValue
        'иp скрытого столбца в GridView получаем idTask
        idtask = CInt(GridView1.Rows(e.NewEditIndex).Cells(GridView1.Columns.Count - 3).Text)
        'Если в сессии нет обьекта idTask добавляем, если есть, то обновляем 
        If Session("IdTask") Is Nothing Then
            Session.Add("IdTask", idtask)
        Else
            Session("IdTask") = idtask
        End If
        If Session("TypeCreate") Is Nothing Then
            Session.Add("TypeCreate", "Edit")
        Else
            Session("TypeCreate") = "Edit"
        End If
        'переход на страницу редактирования
        Response.Redirect("~/Tasks/EditTask.aspx")
    End Sub
    'удаление задачи
    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim idTask As Integer
        Dim eIndex As Integer = e.RowIndex
        idTask = CInt(GridView1.DataKeys(e.RowIndex).Values("IdTask").ToString)
        If Session("IdTask") Is Nothing Then
            Session.Add("IdTask", idTask)
        Else
            Session("IdTask") = idTask
        End If
        If Session("TypeCreate") Is Nothing Then
            Session.Add("TypeCreate", "Edit")
        Else
            Session("TypeCreate") = "Edit"
        End If
        DeleteTask(idTask)
        GetGridView1("", "")
    End Sub

    Public Sub DeleteTask(ByVal IdTask As Integer)
        Dim SQL_запрос1 As String = ""
        Dim comm As New OleDbCommand
        Dim reader As OleDbDataReader

        Try
            CONNECTION.Open()
            comm.Connection = CONNECTION
            comm = New OleDbCommand("SELECT Users.idUser FROM (((Clients INNER JOIN  " &
            "Tasks ON Clients.idClient = Tasks.idClient) INNER JOIN " &
            "EmployeeTask ON Tasks.idTask = EmployeeTask.idTask) " &
            "INNER JOIN Users ON Clients.idClient = Users.idClient) WHERE Tasks.IdTask=" & IdTask)
            comm.Connection = CONNECTION
            Dim idUser As Integer = CInt(comm.ExecuteScalar)
            comm = New OleDbCommand(String.Format("DELETE FROM EmployeeTask WHERE idTask={0} AND IdUser={1}", IdTask, idUser))
            comm.Connection = CONNECTION
            comm.ExecuteNonQuery()
            comm = New OleDbCommand(String.Format("DELETE FROM Tasks WHERE idTask={0}", IdTask))
            comm.Connection = CONNECTION
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try

    End Sub

    Protected Sub btnNewTask_Click(sender As Object, e As EventArgs) Handles btnNewTask.Click
        If Session("TypeCreate") Is Nothing Then
            Session.Add("TypeCreate", "New")
        Else
            Session("TypeCreate") = "New"
        End If
        Response.Redirect("/Tasks/EditTask.aspx")

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim idStatus As Integer = DropDownList1.SelectedValue
        GetGridView1(SQL_ORDER, idStatus)
    End Sub
End Class

