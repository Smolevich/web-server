<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="main.aspx.vb" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
</asp:Content>


<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
   <table style="width: 631px">
    <tr>
        <td style="text-align:left ; height: 46px;">
           <asp:Button ID="btnNewTask" runat="server" Text="Новая задача" />
    
            <asp:Button ID="btnFilter" runat="server"  Text="Фильтр" Width="87px"  />
    
           <asp:Button ID="btnContract" runat="server" Text="Договора"  />
        </td>
    </tr>
</table>

    
    <asp:MultiView ID="mviewMain" runat="server">
    <asp:View ID="viewGrid" runat="server">
        <table width="80%">
            <tr>
               <td height="220" valign="top">
                   <asp:Label ID="Label1" runat="server" Text="Выбор статуса"></asp:Label>
                   <br>
                   <br>
                   <br></br>
                   <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="AccessDataSource1" DataTextField="TypeStatus" DataValueField="idStatus">
                   </asp:DropDownList>
                   <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Database.accdb" SelectCommand="SELECT [TypeStatus], [idStatus] FROM [Status]"></asp:AccessDataSource>
                   <br></br>
                   </br>
                   </br>
               
                    </td> 
               
           <td>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                     BorderWidth="1px" CellPadding="3" ShowHeader="False" Width="111px">
            <Columns>
                <asp:BoundField DataField="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox777" runat="server" AutoPostBack="True" OnCheckedChanged="chkActive_Clicked" />
                        <asp:HiddenField ID="HiddenField777" runat="server" Value='<%# Bind("Value")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
           </td>
                </tr>
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Сортировка" />   
                </td>
            </tr>
        </table>
   </asp:View>
        </asp:MultiView>
    <br />
    <table width="80%">
        <tr>
            <td valign="top">&nbsp;</td>
            <td valign="top">
                <br />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" style="margin-right: 19px" Width="812px" 
     DataKeyNames="IdTask" 
        >
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Наименование задачи" 
               SortExpression="Name" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="Описание задачи" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
        
            <asp:BoundField DataField="TimeCreate"  HeaderText="Дата создания" DataFormatString="{0:dd.MM.yyyy}" 
                SortExpression="TimeCreate">
            </asp:BoundField>
             <asp:BoundField DataField="TimeUpdate"  HeaderText="Дата изменения" DataFormatString="{0:dd.MM.yyyy}"
                 SortExpression="TimeUpdate" >
            </asp:BoundField>
            <asp:BoundField DataField="TimeElapsed"  HeaderText="Прошедшее время" DataFormatString="{0:hh:MM}" >
            </asp:BoundField>
            <asp:BoundField DataField="TypeStatus" HeaderText="Статус задачи" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
             <asp:BoundField DataField="FI" HeaderText="Автор задачи" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:BoundField DataField="IdTask" HeaderText="IDTask">
            <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
            
            
            
   
        </Columns>
       
    </asp:GridView>
    <asp:AccessDataSource ID="AccessDataSource2" runat="server">

    </asp:AccessDataSource>
    <br />
    <br />
</asp:Content>



