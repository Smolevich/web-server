
Imports System.Data
Imports System.Data.OleDb
Partial Class Default3
    Inherits System.Web.UI.Page
    Public SQL_ORDER As String = ""
    Dim СтрокаПодкл As String = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("Database.accdb")
    Dim IdClient As String = ""
    Dim CONNECTION = New OleDbConnection(СтрокаПодкл)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IdClient = Session("IdClient")
        Dim SQL_запрос1 As String = ""
        Dim num As Long
        Dim dateb As String
        Dim datee As String
        SQL_запрос1 =
        " SELECT Contract.Number, Contract.DateBegin, Contract.DateEnd" &
        " FROM ((Clients INNER JOIN Contract ON Clients.idClient = Contract.idClient) INNER JOIN " &
        "Users ON Clients.idClient = Users.idClient) where Contract.IdClient=" & IdClient
        Try
            CONNECTION.Open()
            Dim COMMAND As New OleDbCommand
            Dim reader As OleDbDataReader

            COMMAND.CommandText = SQL_запрос1
            COMMAND.Connection = CONNECTION
            reader = COMMAND.ExecuteReader

            If reader.Read = True Then
                num = reader.GetValue(0)
                dateb = reader.GetValue(1)
                datee = reader.GetValue(2)
                Session.Add("Number", num)
                Session.Add("DateBegin", dateb)
                Session.Add("DateEnd", datee)
                Me.TextBox3.Text = num
                Me.TextBox4.Text = dateb
                Me.TextBox5.Text = datee

            End If

        Catch ex As Exception

        End Try
        CONNECTION.Close()

    End Sub
End Class
