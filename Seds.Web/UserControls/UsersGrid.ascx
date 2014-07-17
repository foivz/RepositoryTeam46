<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersGrid.ascx.cs" Inherits="Seds.Web.UserControls.UsersGrid" %>
<asp:GridView
    ID="gvData" runat="server"
    CssClass="table"
    AutoGenerateColumns="False"
    AllowPaging="true"
    AllowSorting="True"
    PagerSettings-Position="Bottom"
    GridLines="None">
    <PagerStyle CssClass="pagination" />
    <HeaderStyle />

    <Columns>
        <asp:TemplateField
            SortExpression="Id"
            HeaderText="Id">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Id") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="RoleId"
            HeaderText="Uloga">
            <ItemTemplate>
                <%#GetRoleTitle(DataBinder.Eval(Container.DataItem, "RoleId") as int?) %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="FirstName"
            HeaderText="Ime">
            <ItemTemplate>
                <a href="/UserEdit.aspx?UserId=<%# DataBinder.Eval(Container.DataItem,"Id") %>">
                    <%# DataBinder.Eval(Container.DataItem, "FirstName") %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="LastName"
            HeaderText="Prezime">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "LastName") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="UserName"
            HeaderText="Korisničko ime">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "UserName") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField
            SortExpression="Email"
            HeaderText="Email">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Email") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
                <a href="/UserEdit.aspx?UserId=<%# DataBinder.Eval(Container.DataItem,"Id") %>"><i class="icon-pencil"></i></a>
                <asp:LinkButton ID="btnDelete"
                    runat="server"
                    OnClientClick="return confirm('Jeste li sigurni da želite brisati?');"
                    CommandName="delete"
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'><i class="icon-remove"></i>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
