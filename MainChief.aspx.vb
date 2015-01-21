Imports System.Data.OleDb
Imports System.Data

Partial Class Tasks_ChiefTasker
    Inherits System.Web.UI.Page

    Dim СтрокаПодкл As String = ConfigurationManager.ConnectionStrings("database").ConnectionString
    'Dim СтрокаПодкл As String = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("~/Database.accdb")
    Dim CONNECTION As New OleDbConnection(СтрокаПодкл)
    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("IDU") <> Nothing Then
                GetGridView1("")
            Else
                GridView1.Visible = False
                Panel1.Visible = False


                'GridView1.DataKeys.Item.
            End If
        End If

    End Sub


    Protected Sub GetGridView1(ByVal SQL_WHERE As String)
        Dim query As String = ""
        query = "SELECT Clients.Title, Tasks.Description, Tasks.TimeCreate,  Tasks.Name,Tasks.IdTask " &
        "FROM (Clients INNER JOIN Tasks ON Clients.idClient = Tasks.idClient) " &
        "INNER JOIN Users ON Clients.idClient = Users.idClient WHERE Tasks.idStatus=1 AND Users.idPermission<>3"

        If SQL_WHERE <> "" And SQL_WHERE <> Nothing Then
            query += SQL_WHERE
        End If
        Try
            Dim COMMAND As New OleDbCommand
            COMMAND.CommandText = query
            COMMAND.Connection = CONNECTION
            Dim ds As New DataSet
            Dim df As Boolean = Page.IsPostBack
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(query, СтрокаПодкл)
            adapter.Fill(ds)
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            GridView1.Columns(GridView1.Columns.Count - 2).Visible = False
            GridView1.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim idClient As Integer
        idClient = DropDownList1.SelectedValue
        GetGridView1(" AND Tasks.IdClient=" & idClient)
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim idTask As Integer
        idTask = CInt(GridView1.Rows(e.NewEditIndex).Cells(GridView1.Columns.Count - 2).Text)
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
        Response.Redirect("~/Tasks/ChiefTasker.aspx")
    End Sub
End Class
