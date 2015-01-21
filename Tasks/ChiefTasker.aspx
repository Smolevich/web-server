<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ChiefTasker.aspx.vb" Inherits="Tasks_ChiefTasker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <table width="100%">
        <tr>
            <td id="col1" >
                <asp:Label ID="Label4" runat="server" Text="Наименование задачи"></asp:Label>
                <asp:HiddenField ID="hfIdTask" runat="server" />
            </td>
            <td id="col2" width="60px">
                <asp:TextBox ID="txtName" runat="server" Height="64px" 
                    TextMode="MultiLine" ></asp:TextBox>
            </td>
            </tr>
        <tr>
            <td id="col1" width="250px" >
                <asp:Label ID="Label1" runat="server" Text="Описание задачи" ></asp:Label>
            </td>
            <td id="col2" width="60px">
                <asp:TextBox ID="txtDescription" runat="server" Height="64px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td id="col1" width="250px">
                <asp:Label ID="Label2" runat="server" Text="Дата начала" ></asp:Label>
            </td>
            <td id="col2" width="60px" >
                <asp:TextBox ID="txtDateBegin" runat="server" CssClass="auto-style3" Width="165px"  ></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td id="col1" width="250px">
                <asp:Label ID="Label3" runat="server" Text="Дата изменения" ></asp:Label>
            </td>
            <td id="col2" width="60px">
                <asp:TextBox ID="txtUpdateTime" runat="server" MaxLength="10"
                    onkeydown="javascript:return dFilter (event.keyCode, this, '##.##.####');" Width="159px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtUpdateTime"
                    Display="Dynamic" ErrorMessage="Введите дату в формате (дд.мм.гггг)" Operator="DataTypeCheck"
                    Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        
        <tr>
            <td id="col1" width="250px">

                <asp:Label ID="Label8" runat="server" Text="Назначить исполнителя"></asp:Label>

                </td>
            <td id="col2" width="60px">
               
                <asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="AccessDataSource4" DataTextField="FI" DataValueField="idUser"></asp:DropDownList>
                <asp:AccessDataSource ID="AccessDataSource4" runat="server" DataFile="~/App_Data/Database.accdb" SelectCommand="SELECT Surname + ' ' + Name AS FI, idUser FROM Users WHERE (idPermission = 3)"></asp:AccessDataSource>
            </td>
        </tr>

        <tr>
            <td id="col1" width="250px">
                <asp:Label ID="Label9" runat="server" Text="Клиент"></asp:Label>
                </td>
            <td id="col2" width="60px">
                <asp:TextBox ID="txtСlient" runat="server"  Width="167px" ></asp:TextBox>  
            </td>
        </tr>

         <tr>
            <td id="col1" width="250px">
                <asp:Label ID="Label7" runat="server" Text="Кем создана " ></asp:Label>
            </td>
            <td id="col2" width="60px">
                
                <asp:TextBox ID="txtAuthorTask" runat="server" Width="167px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td id="сol1" width="250px">
                <asp:Button ID="btnCancel" Text="Отмена" runat="server" Width="87px" />
            </td>
            <td id="col2" width="60px">
                <asp:Button ID="btnSave" Text="Сохранить" runat="server" Width="97px" />
               
    <tr>
        <td width="250px">
             <asp:ListView ID="ListView1" runat="server" DataKeyNames="idDocument" DataSourceID="AccessDataSource1"
                 >
                 <AlternatingItemTemplate>
                     <tr style="">
                         <td>
                             <asp:Label ID="idDocumentLabel" runat="server" Text='<%# Eval("idDocument") %>' Visible="false" />
                         </td>
                         <td>
                             <asp:Label ID="idTaskLabel" runat="server" Text='<%# Eval("idTask") %>' Visible="false" />
                         </td>
                         <td>
                             <asp:Label ID="UrlLabel" runat="server" Text='<%# Eval("Url") %>' Visible ="false" />
                         </td>
                         <td>
                         <asp:LinkButton ID="OpenUrl" CommandName="OpenUrl" runat="server" Text='<%# Eval("Url") %>' />
                          </td>
                     </tr>
                 </AlternatingItemTemplate>
                 <EditItemTemplate>
                     <tr style="">
                         
                         <td>
                             <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Обновить" />
                             <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Отмена" />
                         </td>
                         <td>
                             <asp:Label ID="idDocumentLabel1" runat="server" Text='<%# Eval("idDocument") %>' Visible="false" />
                         </td>
                         <td>
                       
                             <asp:TextBox ID="idTaskTextBox" runat="server" Text='<%# Bind("idTask") %>' Visible="false" />
                         </td>
                        
                         <td>
                             <asp:TextBox ID="UrlTextBox" runat="server" Text='<%# Bind("Url") %>' Visible="false"/>
                         </td>
                         <td>
                         <asp:LinkButton ID="OpenUrl" CommandName="OpenUrl" runat="server" Text='<%# Eval("Url") %>' />
                         </td>
                     </tr>
                 </EditItemTemplate>
                 <EmptyDataTemplate>
                     <table runat="server" style="">
                         <tr>
                             <td>Нет данных.</td>
                         </tr>
                     </table>
                 </EmptyDataTemplate>
                 <InsertItemTemplate>
                     <tr style="">
                         <td>
                             <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Вставить" />
                             <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Очистить" />
                         </td>
                         <td>&nbsp;</td>
                         <td>
                             <asp:TextBox ID="idTaskTextBox" runat="server" Text='<%# Bind("idTask") %>' Visible="false" />
                         </td>
                         <td>
                             <asp:TextBox ID="UrlTextBox" runat="server" Text='<%# Bind("Url") %>' Visible="false" />
                         </td>
                     </tr>
                 </InsertItemTemplate>
                 <ItemTemplate>
                     <tr style="">
                         <td>
                             <asp:Label ID="idDocumentLabel" runat="server" Text='<%# Eval("idDocument") %>' Visible="false"/>
                         </td>
                         <td>
                             <asp:Label ID="idTaskLabel" runat="server" Text='<%# Eval("idTask") %>' Visible="false"/>
                         </td>
                         <td>
                             <asp:Label ID="UrlLabel" runat="server" Text='<%# Eval("Url") %>' Visible="false" />
                         </td>
                          <td>
                             <asp:LinkButton ID="OpenUrl" CommandName="OpenUrl" runat="server" Text='<%# Eval("Url") %>' />
                         </td>
                        
                     </tr>
                 </ItemTemplate>
                 <LayoutTemplate>
                     <table runat="server">
                         <tr runat="server">
                             <td runat="server">
                                 <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                     <tr runat="server" style="">
                                         <th runat ="server" Visible="false">Документы</th>
                                         <th runat="server" Visible="false">idTask</th>
                                         <th runat="server">Ссылки</th>
                                     </tr>
                                     <tr id="itemPlaceholder" runat="server">
                                     </tr>
                                 </table>
                             </td>
                         </tr>
                         <tr runat="server">
                             <td runat="server" style=""></td>
                         </tr>
                     </table>
                 </LayoutTemplate>
                 <SelectedItemTemplate>
                     <tr style="">
                         <td>
                             <asp:Label ID="idDocumentLabel" runat="server" Text='<%# Eval("idDocument") %>' Visible="false"/>
                         </td>
                         <td>
                             <asp:Label ID="idTaskLabel" runat="server" Text='<%# Eval("idTask") %>' Visible="false"/>
                         </td>
                         <td>
                             <asp:Label ID="UrlLabel" runat="server" Text='<%# Eval("Url") %>' Visible="false"/>
                         </td>
                         <td>
                             <asp:LinkButton ID="OpenUrl" runat="server" Text='<%# Eval("Url") %>' />
                         </td>
                         
                     </tr>
                 </SelectedItemTemplate>
                </asp:ListView>
            
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnLoad" runat="server" Text="Загрузить" />
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Database.accdb" DeleteCommand="DELETE FROM Documents WHERE (idDocument = @idDocument)" SelectCommand="SELECT idDocument, idTask, Url FROM Documents WHERE (idTask = @idTask)" >
                <SelectParameters>
                    <asp:SessionParameter Name="@idTask" SessionField="IdTask" />
                </SelectParameters>
            </asp:AccessDataSource>
            <table>
                <tbody>
                    <tr>
                        <td align="left" colspan ="" class="auto-style4">
                            <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="FIO" DataValueField="idUser" 
                ViewStateMode="Disabled" AutoPostBack="True" DataSourceID="AccessDataSource3">
                           </asp:DropDownList>
                             <asp:Button ID="ClearFilter" runat="server" Text="Сброс фильтра" Width="103px" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnAdd" runat="server" Text="Добавить комментарий " Width="145px" />
                        
                        
                        </td>
         
                    <td>
          
                     
             
                            </td>
                        <tr >
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdComment" DataSourceID="AccessDataSource2">
                                    <Columns>
                                        <asp:BoundField DataField="Comment" HeaderText="Комментарий" />
                                        <asp:BoundField DataField="FI" HeaderText="Автор">
                                        <ControlStyle Width="600px" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="True" Visible="False" />
                                        <asp:BoundField DataField="IdTask" Visible="False" />
                                        <asp:BoundField DataField="IdComment" HeaderText="IdComment" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        <tr>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtComment" runat="server" Height="71px" TextMode="MultiLine" Width="323px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:Button ID="AddComment" runat="server" Text="Сохранить" Width="121px" />
                            </td>
                        </tr>
             </Asp:panel>
            </td>
        </tr>
    </table>
    <asp:AccessDataSource ID="AccessDataSource3" runat="server" DataFile="~/App_Data/Database.accdb" SelectCommand="SELECT idUser, Surname + ' ' + Name AS FIO FROM Users">
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="AccessDataSource2" runat="server" DataFile="~/App_Data/Database.accdb" 
        SelectCommand="SELECT  Users.Surname + ' ' + Users.Name AS FI,Comment.Comment,Users.idUser,idTask,Comment.IdComment FROM (Comment INNER JOIN Users ON Comment.IdUser = Users.idUser) WHERE (Comment.idTask = @IdTask)">
        <SelectParameters>
            <asp:SessionParameter Name="IdTask" SessionField="IdTask" />
        </SelectParameters>
        
       
    
    </asp:AccessDataSource>
</asp:Content>


