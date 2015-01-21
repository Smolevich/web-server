<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contracts.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
    <table style="width:100%;">
        <tr>
            <td width="50%">
                <asp:Label ID="Label4" runat="server" Text="Номер договора"></asp:Label>
            </td>
            <td width="50%">
                <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Дата заключения договора"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Дата окончания договора"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
            
        </tr>
    </table>
    <br />
    <p><a href="mailto:askme@email.com">Напишите нам</a> по вопросам сотрудничества или условиям договора.</p>
</asp:Content>


