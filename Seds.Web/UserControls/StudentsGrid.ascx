<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentsGrid.ascx.cs" Inherits="Seds.Web.UserControls.StudentsGrid" %>
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
            HeaderText="id">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Id") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="FirstName"
            HeaderText="Ime">
            <ItemTemplate>
                <a href="/StudentEdit.aspx?StudentId=<%# DataBinder.Eval(Container.DataItem,"Id") %>">
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
            SortExpression="BirthLastName"
            HeaderText="Rodno prezime">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "BirthLastName") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="BirthDate"
            HeaderText="Datum rođenja">
            <ItemTemplate>
                <%# ((DateTime)DataBinder.Eval(Container.DataItem, "BirthDate")).ToString("dd.MM.yyyy.") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="JMBG"
            HeaderText="JMBG">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "JMBG") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="OIB"
            HeaderText="OIB">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "OIB") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="UniqueNumber"
            HeaderText="Matični br. studenta">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "UniqueNumber") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
                <a href="/StudentEdit.aspx?StudentId=<%# DataBinder.Eval(Container.DataItem,"Id") %>">
                    <i class="icon-pencil"></i>
                </a>
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
