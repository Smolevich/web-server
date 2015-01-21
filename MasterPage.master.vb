Imports System.Data.OleDb
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim user_name As String, id_user As Long, codeauth As Integer, name As String, sname As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' из сессии извлекаем элементы
        'логин и id пользователя
        user_name = Session("nameU")
        name = Session("Name")
        sname = Session("Surname")
        id_user = CLng(Session("IDU"))
        If Session("CodeAuth") = Nothing Then
            codeauth = 0
        Else
            codeauth = Session("CodeAuth")
        End If
        'Если не постбэк(при редиректе по нажатии на кнопку войти, постбэк также false)
        If Page.IsPostBack = False Then
            'Если пользователь успешно авторизован
            If codeauth = 1 Then
                Me.TextBox1.Visible = False
                Me.TextBox2.Visible = False
                Me.Label1.Visible = True
                Me.Label2.Visible = False
                Me.Label3.Visible = False

                Me.Button2.Visible = False
                Me.Button3.Visible = True
                Me.Label1.Text = "Вы зашли как " & name & " " & sname & "."
                '
            ElseIf codeauth = 0 Then
                Me.Label1.Visible = True
                Me.Label2.Visible = True
                Me.Label3.Visible = True
                Me.TextBox1.Visible = True
                Me.TextBox2.Visible = True
                Me.Button2.Visible = True
                Me.Button3.Visible = False
            Else
                Label1.Text = "Неправильное имя или пароль, пожалуйста, зарегистрируйтесь!"

            End If
        Else
            If codeauth = 1 Then
                Me.TextBox1.Visible = False
                Me.TextBox2.Visible = False
                Me.Label1.Visible = True
                Me.Label2.Visible = False
                Me.Label3.Visible = False

                Me.Button2.Visible = False
                Me.Button3.Visible = True
                Me.Label1.Text = "Вы зашли как " & name & " " & sname & "."
            End If
        End If

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim myuser As String, id As Long, permission As Long, IDC As Long
        Dim СтрокаПодкл As String = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("Database.accdb")
        Dim CONNECTION = New OleDbConnection(СтрокаПодкл)
        Dim CodeAuth As Integer
        Try  ' Открытие подключения:
            CONNECTION.Open()
        Catch ex1 As Exception
            Label1.Text = ex1.Message
        End Try
        ' Строка SQL-запроса для проверки имени и пароля:(единая строка)
        Dim SQL_запрос As String = "SELECT idUser,idClient,Name, Surname FROM Users WHERE (Login = '" & TextBox1.Text & "' AND Password = '" & TextBox2.Text & "')"
        ' MsgBox(SQL_запрос)
        myuser = TextBox1.Text
        Dim SQL_запрос1 As String = "SELECT idPermission FROM Users WHERE (Login = '" & TextBox1.Text & "' AND Password = '" & TextBox2.Text & "')"
        ' Создание объекта Command с заданием SQL-запроса:
        Dim COMMAND As New OleDbCommand
        COMMAND.CommandText = SQL_запрос
        COMMAND.Connection = CONNECTION

        Dim COMMAND2 As New OleDbCommand
        COMMAND2.CommandText = SQL_запрос1
        COMMAND2.Connection = CONNECTION

        Try ' Выполнение команды SQL:

            Dim DATAREADER As OleDbDataReader
            DATAREADER = COMMAND.ExecuteReader

            Dim DATAREADER2 As OleDbDataReader
            DATAREADER2 = COMMAND2.ExecuteReader

            If DATAREADER2.Read = True Then
                permission = DATAREADER2.GetValue(0)
                Session.Add("idPermission", permission)
            End If

            If DATAREADER.Read = True Then
                CodeAuth = 1
                'определение значения поля
                id = DATAREADER.GetValue(0)
                IDC = DATAREADER.GetValue(1)
                name = DATAREADER.GetValue(2)
                sname = DATAREADER.GetValue(3)
                Session.Add("IdClient", IDC)
                Session.Add("nameU", myuser)
                Session.Add("IDU", id)
                Session.Add("Name", name)
                Session.Add("Surname", sname)

                '==========
                Me.TextBox1.Visible = False
                Me.TextBox2.Visible = False
                Me.Label2.Visible = False
                Me.Label3.Visible = False
                Me.Label1.Visible = True
                Me.Button2.Visible = False
                Me.Button3.Visible = True
                Me.Label1.Text = "Вы зашли как " & name & " " & sname & "."
            Else
                CodeAuth = -1
                Label1.Text = "Неправильное имя или пароль, пожалуйста, зарегистрируйтесь!"
                Me.Label1.Visible = True
            End If
            Session.Add("CodeAuth", CodeAuth)
        Catch ex2 As Exception
            Label1.Text = Label1.Text & "<br>" & ex2.Message
            Me.Label1.Visible = True
        End Try

        CONNECTION.Close()
        Select Case permission
            Case 1
                Response.Redirect("main.aspx")
            Case 2
                Response.Redirect("MainChief.aspx")
            Case 3
                Response.Redirect("contracts.aspx")
            Case Else
                Response.Redirect("index.aspx")

        End Select

    End Sub



    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Session.Clear()
        Response.Redirect("index.aspx")
        'user_name = ""
        'id_user = 0
    End Sub
End Class

