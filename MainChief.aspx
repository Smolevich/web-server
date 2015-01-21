<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="MainChief.aspx.vb" Inherits="Tasks_ChiefTasker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 195px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
    
    <asp:Panel runat="server" Id="Panel1">
        <table style="width: 100%;">
        <tr >
            <td class="auto-style1" ><asp:Button ID="btnNewTask" runat="server" Text="Новая задача" Height="33px"  BackColor="#FFFFCC" Width="110px" /></td>
            <td><asp:Button ID="btnAppointTask" runat="server" Text="Назначенные задачи" Width="121px" Height="33px" /></td>
            <td><asp:Button ID="Button1" runat="server" Text="Назначенные" Width="113px" Height="33px" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Клиент"></asp:Label>
               
            </td>
            <td>
                 <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="AccessDataSource1" DataTextField="Title" DataValueField="idClient">
                </asp:DropDownList>
                 <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/Database.accdb" SelectCommand="SELECT idClient, Title FROM Clients"></asp:AccessDataSource>
            </td>
       </tr>
    </table>
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="566px" 
        PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Клиент" />
            <asp:BoundField DataField="Name" HeaderText="Название задачи" />
            <asp:BoundField DataField="TimeCreate" HeaderText="Дата создания" DataFormatString="{0:dd.MM.yyyy}" />
            <asp:BoundField DataField="IdTask" HeaderText="IdTask" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>

    
</asp:Content>


