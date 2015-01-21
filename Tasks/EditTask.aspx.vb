Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Globalization

Partial Class Tasks_EditTask2
    Inherits System.Web.UI.Page

    Dim СтрокаПодкл As String = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("~/Database.accdb")
    Dim CONNECTION As New OleDbConnection(СтрокаПодкл)

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idTask As Integer = CInt(Session("IdTask"))
        Dim Command As New OleDbCommand
        Dim Reader As OleDbDataReader
        Dim SQL_запрос1 As String
        If (Not Page.IsPostBack) Then
            hfIdTask.Value = Session("IdTask")
            AccessDataSource3.SelectCommand = "SELECT idUser, Surname +Name as FIO FROM Users "
            ' "WHERE idUser=" & Session("IDU")
           
            Dim temp As String = Session("TypeCreate")
            Try
                If Session("TypeCreate") = "Edit" Then
                    SQL_запрос1 =
                   " SELECT Tasks.Name, Tasks.Description, Tasks.TimeCreate, Tasks.TimeUpdate, " &
                   "Tasks.TimeElapsed, Status.TypeStatus, (Users.Surname+"" ""+Users.Name ) AS FI ,Tasks.IdTask,Status.idStatus " &
                   "FROM         ((((Clients INNER JOIN Tasks on Tasks.IdClient=Clients.IdClient) " &
                   "INNER JOIN EmployeeTask ON Tasks.idTask = EmployeeTask.idTask) INNER JOIN " &
                   "Status ON Tasks.idStatus = Status.idStatus) INNER JOIN " &
                   "Users ON Clients.idClient = Users.idClient AND EmployeeTask.idUser = Users.idUser) where Tasks.Idtask=" & hfIdTask.Value
                    CONNECTION.Open()
                    Command.Connection = CONNECTION
                    Command.CommandText = SQL_запрос1
                    Dim ds As New DataSet
                    Dim tblTask As New DataTable
                    Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(SQL_запрос1, CONNECTION)
                    Dim datecreate As String
                    Dim dateupdate As String
                    Dim idStatus As Integer
                    adapter.Fill(ds)
                    tblTask = ds.Tables(0)
                    txtComment.Visible = False
                    AddComment.Visible = False
                    idStatus = tblTask.Rows(0)("IdStatus").ToString

                    txtName.Text = tblTask.Rows(0)("Name").ToString
                    txtDescription.Text = tblTask.Rows(0)("Description").ToString
                    datecreate = tblTask.Rows(0)("TimeCreate").ToString.Remove(10)
                    txtDateBegin.Text = datecreate
                    dateupdate = tblTask.Rows(0)("TimeUpdate").ToString.Remove(10)
                    Dim dat As DateTime
                    'dat = DateTime.ParseExact(dateupdate, "dd.MM.yyy", CultureInfo.InvariantCulture)
                    Dim st As String()
                    st = dateupdate.Split(New Char() {"."c})
                    Dim value As DateTime = New DateTime(st(2), st(1), st(0))
                    If value < Date.Today Then
                        dat = Date.Now.ToString("dd.MM.yyyy")
                    End If
                    Switch()
                    txtDateEnd.Text = dat.ToString("dd.MM.yyyy")
                    txtTimeElapsed.Text = tblTask.Rows(0)("TimeElapsed").ToString
                    txtStatusTask.Text = tblTask.Rows(0)("TypeStatus").ToString
                    txtAuthorTask.Text = tblTask.Rows(0)("FI").ToString


                    'отображение в зависимости от статуса задачи 
                    Select Case idStatus
                        Case 1 'Новпя 
                            txtAuthorTask.Enabled = False
                            txtDateBegin.Enabled = False
                            txtDateEnd.Enabled = False
                            txtStatusTask.Enabled = False
                            txtTimeElapsed.Enabled = False
                        Case 2 To 3 'В работе и выполнена
                            txtAuthorTask.Enabled = False
                            txtDateBegin.Enabled = False
                            txtDateEnd.Enabled = False
                            txtStatusTask.Enabled = False
                            txtTimeElapsed.Enabled = False
                            txtName.Enabled = False
                            txtDescription.Enabled = False

                    End Select

                    AccessDataSource1.DataBind()
                    ListView1.DataSourceID = "AccessDataSource1"
                    GridView1.DataSourceID = "AccessDataSource2"
                Else
                    SQL_запрос1 = "SELECT Surname+ ' '+Name as FI FROM Users WHERE idUser=" & Session("IDU")
                    Session("Idtask") = 0
                    CONNECTION.Open()
                    Command.Connection = CONNECTION
                    Command.CommandText = SQL_запрос1
                    txtAuthorTask.Text = CStr(Command.ExecuteScalar)
                    txtDateBegin.Text = DateTime.Now.ToString("dd.MM.yyyy")
                    txtDateEnd.Text = DateTime.Now.ToString("dd.MM.yyyy")
                    txtStatusTask.Text = "Новая"
                    GridView1.Visible = True
                End If
            Catch ex As Exception
                lblError.Text = ex.Message.ToString
            End Try

        End If
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim directorytask As String = "~/img/" & "Task_" & Session("IdTask") & "/"
        Dim path As String = Server.MapPath(directorytask) 'папка для сохранения документов на сервере
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        Dim virtualpath As String = Request.CurrentExecutionFilePath
        Dim fileOK As Boolean = False
        If FileUpload1.HasFile Then
            Dim fileExtension As String
            fileExtension = System.IO.Path. _
                GetExtension(FileUpload1.FileName).ToLower()
            Try
                CONNECTION.Open()
                FileUpload1.PostedFile.SaveAs(path & _
                     FileUpload1.FileName)

                Dim comm As New OleDbCommand("INSERT INTO Documents ( idTask,NameDoc,Url ) VALUES (@IdTask,@NameDoc,@Url) ", CONNECTION)
                Dim fileName As String = path & _FileUpload1.FileName
                Using fileStream As FileStream = File.OpenRead(fileName)
                    Dim ms(FileUpload1.PostedFile.InputStream.Length) As Byte
                    FileUpload1.PostedFile.InputStream.Read(ms, 0, ms.Length)

                    Dim memStream As New MemoryStream()
                    memStream.SetLength(fileStream.Length)
                    fileStream.Read(memStream.GetBuffer(), 0, CInt(Fix(fileStream.Length)))
                    Dim arr As Byte()
                    arr = memStream.GetBuffer
                    comm.Parameters.Add(New OleDbParameter("@IdTask", OleDbType.Integer))
                    comm.Parameters("@IdTask").Value = Session("IdTask")
                    comm.Parameters.Add(New OleDbParameter("@NameDoc", OleDbType.VarChar))
                    comm.Parameters("@NameDoc").Value = FileUpload1.FileName
                    comm.Parameters.Add(New OleDbParameter("@Url", OleDbType.VarChar))
                    comm.Parameters("@Url").Value = directorytask & FileUpload1.FileName
                    comm.ExecuteNonQuery()
                    Label1.Text = "File uploaded!"
                End Using
            Catch ex As Exception
                Label1.Text = "File could not be uploaded."
            End Try
        Else
            Label1.Text = "Cannot accept files of this type."
        End If



    End Sub

    Private Sub SaveTask(ByVal tbl As DataTable)
        Dim Command As New OleDbCommand
        Dim text_sql As String
        If Session("TypeCreate") = "Edit" Then

            text_sql = String.Format("UPDATE Tasks SET Name = '{0}'," &
            "Description='{1}' " &
            "WHERE idTask = {2} ", tbl.Rows(0)("Name").ToString(), tbl.Rows(0)("Description").ToString(),
             tbl.Rows(0)("idTask"))
        Else
            text_sql = String.Format("INSERT INTO Tasks(Name,Description,DateBegin,idStatus,idClient) VALUES(" &
             "'{0'},'{1}','{2}',{3},{4})", txtName.Text, txtDescription.Text, txtDateBegin.Text, "1", Session("IdClient"))

        End If
        Try
            CONNECTION.Open()
            Command.Connection = CONNECTION
            Command.CommandText = text_sql
            Command.ExecuteNonQuery()
            CONNECTION.Close()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString()
        End Try

    End Sub


    'сохранение
  

    'выполнение запросов вставки и обновления
    Public Sub ExecInsertSQL(query As String)
        Dim command As New OleDbCommand
        Try
            CONNECTION.Open()
            command.Connection = CONNECTION
            command.CommandText = query
            command.ExecuteNonQuery()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    'запросы на выборку данных
    Public Function ExecSelectSQL(query As String) As String
        Dim command As New OleDbCommand
        Dim reader As OleDbDataReader
        Dim result As String = ""
        Try
            CONNECTION.Open()
            command.Connection = CONNECTION
            command.CommandText = query
            result = command.ExecuteScalar
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
        Return result
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Page.Response.Redirect("~/main.aspx")
        Session("TypeCreate") = Nothing
    End Sub

    Protected Sub ListView1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles ListView1.ItemCommand
        If String.Equals(e.CommandName, "OpenUrl") Then
            Dim dataitem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            'Dim idDocument = ListView1.DataKeys(dataitem.DisplayIndex).Value
            Dim linkbutton As New LinkButton
            Dim Label As Label

            'linkbuttom = CType(ListView1.Items(dataitem.DisplayIndex).FindControl("LinkOpen"), LinkButton)
            'Label = CType(ListView1.Items(dataitem.DisplayIndex).FindControl("Label1"), Label)

            dataitem = CType(e.Item, ListViewItem)

            Label = CType(dataitem.FindControl("UrlLabel"), Label)

            Response.Redirect(Label.Text)
        ElseIf String.Equals(e.CommandName, "Delete") Then
            Dim dataitem As ListViewDataItem = CType(e.Item, ListViewDataItem)
            Dim Label As Label
            Label = CType(dataitem.FindControl("idDocumentLabel1"), Label)
            AccessDataSource1.DeleteParameters.Add("@IdDocumnet", Label.Text)
        End If

    End Sub

  

    Protected Sub ClearFilter_Click(sender As Object, e As EventArgs) Handles ClearFilter.Click
        AccessDataSource2.SelectCommand = "SELECT Comment.IdComment, Users.Surname + ' ' + Users.Name AS FI FROM" &
            "(Comment INNER JOIN Users ON Comment.IdUser = Users.idUser WHERE idTask=?)"
       
        GridView1.DataBind()
    End Sub

    Protected Sub AddComment_Click(sender As Object, e As EventArgs) Handles AddComment.Click
        Try
            CONNECTION.Open()
            Dim comm As New OleDbCommand
            Dim Sql_запрос As String
            Sql_запрос = String.Format("INSERT INTO Comment(idUser,Comment,idTask) VALUES ({0},'{1}',{2})",
            Session("IDU"), txtComment.Text, Session("IdTask"))
            comm.Connection = CONNECTION
            comm.CommandText = Sql_запрос
            comm.ExecuteNonQuery()
            GridView1.Visible = True
            GridView1.DataBind()
            DropDownList1.DataBind()
            CONNECTION.Close()
        Catch ex As Exception

        End Try
        txtComment.Visible = False
        AddComment.Visible = False
        GridView1.DataBind()
        'Response.Redirect("EditTask.aspx")
    End Sub

   

    Protected Sub AccessDataSource2_Inserting(sender As Object, e As SqlDataSourceCommandEventArgs) Handles AccessDataSource2.Inserting
        e.Command.Parameters("?").Value = Session("IdTask")
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim IdComment = GridView1.SelectedValue

    End Sub

    'Добавление коммееннтария
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        txtComment.Visible = True
        AddComment.Visible = True
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim idAuthor As Integer
        idAuthor = DropDownList1.SelectedValue
        AccessDataSource2.SelectCommand = "SELECT  Users.Surname + ' ' + Users.Name AS FI," &
       "Comment.Comment,Users.idUser,idTask,Comment.IdComment FROM (Comment INNER JOIN Users " &
       "ON Comment.IdUser = Users.idUser) WHERE (Comment.idUser =" & idAuthor & ")"
        GridView1.DataSourceID = "Accessdatasource2"
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim idComment As Integer
        ' idComment = CInt(GridView1.Rows(e.RowIndex).Cells(GridView1.Columns.Count - 1).Text)
        idComment = GridView1.SelectedValue
        Dim comm As New OleDbCommand
        Dim query As String
        Try
            query = "DELETE FROM Comment WHERE idComment=@idComment"
            CONNECTION.Open()
            comm.CommandText = query
            comm.Connection = CONNECTION
            comm.Parameters.AddWithValue("@idComment", idComment)
            comm.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        GridView1.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim Command As New OleDbCommand
        Dim text_sql As String
        Dim text2 As String
        Dim IdNewTask As String
        'если редактирование 
        Try

            If Session("TypeCreate") = "Edit" Then
                text_sql = String.Format("UPDATE Tasks SET Name = '{0}'," &
                "Description='{1}',TimeUpdate='{2}' " &
                "WHERE idTask = {3} ", txtName.Text, txtDescription.Text, txtDateEnd.Text,
                 Session("IdTask"))
                ExecInsertSQL(text_sql)
            Else 'если добавление новой задачи

                text_sql = String.Format("INSERT INTO Tasks(Name,Description,TimeCreate,TimeUpdate,idStatus,idClient) VALUES(" &
                 "'{0}','{1}','{2}','{3}',{4},{5})", txtName.Text, txtDescription.Text, txtDateBegin.Text, txtDateBegin.Text, "1", Session("IdClient"))
                text2 = "Select @@Identity"
                CONNECTION.Open()
                Command.Connection = CONNECTION
                Command.CommandText = text_sql
                Command.ExecuteNonQuery()
                Command.CommandText = text2
                IdNewTask = Command.ExecuteScalar()
                CONNECTION.Close()
                ExecInsertSQL(String.Format("INSERT INTO EmployeeTask(idUser,idTAsk) VALUES({0},{1})", Session("IDU"), IdNewTask))

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
        Page.Response.Redirect("~/main.aspx")
        Session("TypeTask") = Nothing
    End Sub
End Class
