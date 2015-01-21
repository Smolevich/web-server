Imports System.Data.OleDb
Imports System.Data

Partial Class Tasks_ChiefTasker
    Inherits System.Web.UI.Page

    Dim СтрокаПодкл As String = ConfigurationManager.ConnectionStrings("database").ConnectionString
    'Dim  = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("/Database.accdb")
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
                   "Tasks.TimeElapsed, Status.TypeStatus, (Users.Surname+"" ""+Users.Name ) AS FI ,Clients.Title," &
                   "Tasks.IdTask,Status.idStatus " &
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
                    txtСlient.Text = tblTask.Rows(0)("Title").ToString()
                    txtAuthorTask.Text = tblTask.Rows(0)("FI").ToString()
                    'если дата изменения меньше текущей, то при сохранении изменений сохраняется текущая дата
                    Dim dat As DateTime
                    Dim st As String()
                    st = dateupdate.Split(New Char() {"."c})
                    Dim value As DateTime = New DateTime(st(2), st(1), st(0))
                    If value < Date.Today Then
                        dat = Date.Now.ToString("dd.MM.yyyy")
                    End If

                    txtUpdateTime.Text = dat.ToString("dd.MM.yyyy")
                    'отображение в зависимости от статуса задачи 
                    Select Case idStatus
                        Case 1 'Новая 
                            txtAuthorTask.Enabled = False
                            txtDateBegin.Enabled = False
                            txtUpdateTime.Enabled = False
                          
                        Case 2 To 3 'В работе и выполнена
                            txtAuthorTask.Enabled = False
                            txtDateBegin.Enabled = False
                            txtUpdateTime.Enabled = False
                       
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
                    txtUpdateTime.Text = DateTime.Now.ToString("dd.MM.yyyy")
                    GridView1.Visible = True
                End If
            Catch ex As Exception
                lblError.Text = ex.Message.ToString
            End Try

        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim com As New OleDbCommand
        Dim query As String
        'сохраняем данные по задаче
        Try
            CONNECTION.Open()
            query = String.Format("UPDATE Tasks SET Tasks.Name = '{0}', Tasks.Description =' {1}'," &
           "Tasks.TimeCreate ='{2}', Tasks.TimeUpdate ='{3}',Tasks.idStatus={4}" &
            "WHERE Tasks.IdTask={5}"
                                )
            'com.CommandText=

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Page.Response.Redirect("~/main.aspx")
        Session("TypeCreate") = Nothing
    End Sub
End Class
